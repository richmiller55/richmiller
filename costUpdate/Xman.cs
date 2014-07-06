using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using Epicor.Mfg.BO;

namespace costUpdate
{
    public class BinAtom
    {
        string warehouse;
        string binNum;
        decimal onHandQty;
        public BinAtom(InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow row)
        {
            this.warehouse = row.WareHseCode;
            this.binNum = row.BinNum;
            this.onHandQty = row.OnHandQty;
        }
        public string Warehouse
        {
            get
            {
                return warehouse;
            }
            set
            {
                warehouse = value;
            }
        }
        public string BinNum
        {
            get
            {
                return binNum;
            }
            set
            {
                binNum = value;
            }
        }
        public decimal OnhandQty
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

    
    public class PartBinRecord
    {
        string partNum;
        Hashtable ht;
        ArrayList bins;
        public PartBinRecord(string partNumin)
        {
            this.partNum = partNumin;
            this.ht = new Hashtable(5);
            this.bins = new ArrayList();
        }
        public bool IsThereNegativeOnHand()
        {
            bool result = false;
            foreach (BinAtom atom in this.bins)
            {
                if (atom.OnhandQty < 0)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }
        
        public bool IsThereOnHand()
        {
            return (bins.Count > 0);
        }
        public void AddBinLocationRow(InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow row)
        {
            BinAtom bin = new BinAtom(row);
            this.bins.Add(bin);
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
        public ArrayList Bins
        {
            get
            {
                return bins;
            }
            set
            {
                bins = value;
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
        public void WriteAllBins(bool WriteOff, PartBinRecord pbr)
        {
            if (!pbr.IsThereOnHand()) return;
            foreach (BinAtom bin in pbr.Bins)
            {
                decimal onHandQty = (decimal)bin.OnhandQty;
                if (WriteOff) onHandQty = onHandQty * -1;
                AdjustBinInventory(pbr.PartNum, bin.Warehouse, bin.BinNum, onHandQty);
            }
            return;
        }
        void AdjustBinInventory(string partNum, string whNum, string binNum, decimal adjQty)
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
            row.ReasonCode = "COST";
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
                InventoryQtyAdjBrwDataSet ds;
                string primarybin = "";
                string[] warehouses = { "01", "9" };
                foreach (string whNum in warehouses)
                {
                    ds = this.invAdjustObj.GetInventoryQtyAdjBrw(partNum, whNum, out primarybin);
                    for (int i = 0; i < ds.InventoryQtyAdjBrw.Rows.Count; i++)
                    {
                        InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow row =
                            (InventoryQtyAdjBrwDataSet.InventoryQtyAdjBrwRow)ds.InventoryQtyAdjBrw.Rows[i];
                        partBinRecord.AddBinLocationRow(row);
                    }
                }
                return partBinRecord;
            }
            return partBinRecord;
        }
        public void updateCostMethod(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)rowLayout.UPC];
            
            if (this.partObj.PartExists(partNum))
            {
                PartDataSet ds = this.partObj.GetByID(partNum);
                PartDataSet.PartRow row;
                row = (PartDataSet.PartRow)ds.Part.Rows[0];
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
            CostAdjustmentDataSet ds;
            ds = new CostAdjustmentDataSet();
            this.costAdjustment.GetCostAdjustment(cost.PartNum, ds);
            this.costAdjustment.OnChangeAvgMtlUnitCost(cost.Cost, ds);
            CostAdjustmentDataSet.CostAdjustmentRow row =
                (CostAdjustmentDataSet.CostAdjustmentRow)ds.CostAdjustment.Rows[0];
            row.ReasonCode = "COST";
            row.ReasonCodeDesc = "Cost Adjust";
            row.Reference = "rlm Std Cost Setup 5-Jul-14";
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
