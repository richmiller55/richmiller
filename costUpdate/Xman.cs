using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace costUpdate
{
    public class CostDetail
    {
        string partNum;
        decimal poCost;
        // decimal freightCost;
        // decimal burden;
        // decimal customsCost;
        // decimal royalty;
        decimal cost;
        public CostDetail(string upc)
        {
            partNum = upc;
            poCost = 0m;
        }
        public string PartNum
        {
            get { return partNum; }
            set { partNum = value; }
        }
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public decimal POCost
        {
            get { return poCost; }
            set { poCost = value; }
        }
    }
    public class CostXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.CostAdjustment costAdjustment;
        Epicor.Mfg.BO.Part partObj;

        //   pilot 8331
        //   test 8321  verify
        //   sys 8301
        public CostXman()
        {
            this.objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.costAdjustment = new Epicor.Mfg.BO.CostAdjustment(objSess.ConnectionPool);
            this.partObj = new Epicor.Mfg.BO.Part(objSess.ConnectionPool);
        }

        public void updateCostMethod(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)rowLayout.UPC];
            if (partNum.Equals("UPC")) return;
            if (partNum.Equals("Part Number")) return;
            if (partNum.Equals("UPC Number")) return;

            if (this.partObj.PartExists(partNum))
            {
                ds = this.partObj.GetByID(partNum);
                Epicor.Mfg.BO.PartDataSet.PartRow row;
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.CostMethod = "S";
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
            row.AvgMtlUnitCost = cost.Cost;
            this.costAdjustment.PreSetCostAdjustment(ds, out requiresUserInput);
            if (requiresUserInput)
            {
                // figuer out what
            }
            this.costAdjustment.SetCostAdjustment(ds);
        }
    }
}        
