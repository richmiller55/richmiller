using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using Epicor.Mfg.BO;

namespace costUpdate
{
    public class PartBinRecord
    {
        string partNum;
        decimal onHandQty;
        Hashtable ht;
        public PartBinRecord(string partNumin)
        {
            this.partNum = partNumin;
            this.ht = new Hashtable(5);
        }
        public void AddBinLocationRow(string binNum, decimal onHandQty)
        {
            ht.Add(binNum, onHandQty);
        }
        public Hashtable BinHash
        {
            get
            {
                return ht;
            }
            set
            {
                ht = value;
            }
        }
        public string PartNum
        {
            get
            {
                return partNum;
            }
            set
            {
                partNum = value;
            }
        }
        public decimal OnHandQty
        {
            get
            {
                return onHandQty;
            }
            set
            {
                onHandQty = value;
            }
        }
    }
    public class CostXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.CostAdjustment costAdjustment;
        Epicor.Mfg.BO.Part partObj;
        Epicor.Mfg.BO.InventoryQtyAdj invAdjustObj;

        //   pilot 8331
        //   test 8321  verify
        //   sys 8301
        public CostXman()
        {
            this.objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.costAdjustment = new CostAdjustment(objSess.ConnectionPool);
            this.partObj = new Part(objSess.ConnectionPool);
            this.invAdjustObj = new InventoryQtyAdj(objSess.ConnectionPool);
        }
        public bool IsPartInactive(string partNum)
        {
            PartDataSet ds = this.partObj.GetByID(partNum);
            PartDataSet.PartRow row = (PartDataSet.PartRow)ds.Part.Rows[0];
            if (row.InActive == true)
            {
                return true;
            }
            return false;
        }


        public void WriteAllBins(bool WriteOff, PartBinRecord partBinRecord)
        {
            ICollection keyColl = partBinRecord.BinHash.Keys;
            string whNum = "01";
            foreach (string binNum in keyColl)
            {
                decimal qtyOnHand = (decimal)partBinRecord.BinHash[binNum];
                if (WriteOff) qtyOnHand = qtyOnHand * -1;
                string reasonCode = (WriteOff) ? "TWOFF" : "TWON";
                AdjustBinInventory(whNum, partBinRecord.PartNum, binNum, qtyOnHand,reasonCode);
            }
        }

        void AdjustBinInventory(string whNum, string partNum, string binNum, decimal adjQty, string reasonCode)
        {
            string kitMessage = "";
            invAdjustObj.KitPartStatus(partNum, out kitMessage);
            string negAction = "";
            string pcMessage = "";
            invAdjustObj.NegativeInventoryTest(partNum, whNum, binNum, "", "", 0m, adjQty, out negAction, out pcMessage);
                
            bool requiresUserInput = false;
            InventoryQtyAdjDataSet ds = invAdjustObj.GetInventoryQtyAdj(partNum);

            invAdjustObj.PreSetInventoryQtyAdj(ds, out requiresUserInput);
            InventoryQtyAdjDataSet.InventoryQtyAdjRow row = (InventoryQtyAdjDataSet.InventoryQtyAdjRow)ds.InventoryQtyAdj.Rows[0];
            row.AdjustQuantity = adjQty;
            row.BinNum = binNum;
            row.WareHseCode = whNum;
            row.ReasonCode = reasonCode;
            try
            {
                invAdjustObj.SetInventoryQtyAdj(ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public PartBinRecord GetOnHandForPart(string partNum)
        {
            PartBinRecord partBinRecord = new PartBinRecord(partNum);
            if (this.partObj.PartExists(partNum))
            {
                Epicor.Mfg.BO.InventoryQtyAdjBrwDataSet ds;
                string primarybin = "";
                
                ds = this.invAdjustObj.GetInventoryQtyAdjBrw(partNum, "01", out primarybin);
                for (int i = 0; i < ds.InventoryQtyAdjBrw.Rows.Count; i++)
                {
                    Epicor.Mfg.BO.InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow row =
                        (Epicor.Mfg.BO.InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow)ds.InventoryQtyAdjBrw.Rows[i];
                    partBinRecord.AddBinLocationRow(row.BinNum, row.OnHandQty);
                }
            }
            return partBinRecord;
        }
        
        public void updateCostMethod(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)rowLayout.UPC];
            
            if (this.partObj.PartExists(partNum))
            {
                Epicor.Mfg.BO.PartDataSet ds = this.partObj.GetByID(partNum);
                Epicor.Mfg.BO.PartDataSet.PartRow row;
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.CostMethod = "S";
                row.UpdatePartPlant = true;

                string partChangesText = "";
                this.partObj.CheckPartChanges(ds, out partChangesText);
                string ruleMessage = "";
                string singleLevelMessage = "";
                string plantSourceType = "P";
                this.partObj.ChangePartPlantSourceType(plantSourceType, out ruleMessage,
                    out singleLevelMessage, ds);
               
                try
                {
                    this.partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        public void updateStdCost(String line)
        {
            AddStdCost(line);
        }
        void AddStdCost(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)rowLayout.UPC];
            string newStdCostStr = split[(int)rowLayout.cost];


            CostDetail detail = new CostDetail(partNum);
            string sCost = split[(int)rowLayout.cost];
            bool allOK = true;
            try
            {
                detail.Cost = System.Convert.ToDecimal(sCost);
            }
            catch (System.OverflowException)
            {
                allOK = false;
                System.Console.WriteLine(
                   "The conversion from string to decimal overflowed.");
            }
            catch (System.FormatException)
            {
                allOK = false;
                System.Console.WriteLine(
                    "The string is not formatted as a decimal.");
            }
            catch (System.ArgumentNullException)
            {
                allOK = false;
                System.Console.WriteLine(
                    "The string is null.");
            }
            if (allOK)
            {
                UpdateCost(detail);
            }
        }
        public void UpdateCost(CostDetail cost)
        {
            Boolean requiresUserInput;
            Epicor.Mfg.BO.CostAdjustmentDataSet ds;
            ds = new Epicor.Mfg.BO.CostAdjustmentDataSet();
            this.costAdjustment.GetCostAdjustment(cost.PartNum, ds);
            this.costAdjustment.OnChangeAvgMtlUnitCost(cost.Cost, ds);
            Epicor.Mfg.BO.CostAdjustmentDataSet.CostAdjustmentRow row =
                (Epicor.Mfg.BO.CostAdjustmentDataSet.CostAdjustmentRow)ds.CostAdjustment.Rows[0];
            row.ReasonCode = "COST";
            row.ReasonCodeDesc = "Std Cost Setup 21-Jun-14";
            row.Reference = "rlm-test";
            row.StdMtlUnitCost = cost.Cost;
            row.StdBurUnitCost = 0.0m;
            //   row.AvgMtlUnitCost = cost.Cost;
            this.costAdjustment.PreSetCostAdjustment(ds, out requiresUserInput);
            if (requiresUserInput)
            {
                // figuer out what
            }
            this.costAdjustment.SetCostAdjustment(ds);
        }
    }
}        
