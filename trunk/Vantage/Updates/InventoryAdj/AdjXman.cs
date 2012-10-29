using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace InventoryAdj
{

    public class AdjXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Part partObj;
        Epicor.Mfg.BO.PartDataSet ds;
        // Epicor.Mfg.BO.PartDataSet.PartRow row;

        //   pilot 8331
        //   sys 8301

        public AdjXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.partObj = new Epicor.Mfg.BO.Part(objSess.ConnectionPool);
        }
        public void InventoryAdjust(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)layout.UPC];
            if (partNum.CompareTo("UPC") == 0) return;
            if (this.partObj.PartExists(partNum))
            {
                Epicor.Mfg.BO.InventoryQtyAdj IQA = new Epicor.Mfg.BO.InventoryQtyAdj(objSess.ConnectionPool);
                string wareHouseCode = "01";
                string primaryBin = "";
                Epicor.Mfg.BO.InventoryQtyAdjDataSet ds = IQA.GetInventoryQtyAdj(partNum);
                Epicor.Mfg.BO.InventoryQtyAdjBrwDataSet dsBrw = IQA.GetInventoryQtyAdjBrw(partNum,wareHouseCode,out primaryBin);
                                   
                string kitmessage = "";
                IQA.KitPartStatus(partNum, out kitmessage);
                Console.WriteLine("kit message " + kitmessage);
                string pcWhseCode = "01";
                string pcBinNum = split[(int)layout.bin];
                string pcLotNum = "";
                string pcDimCode = "";
                decimal pdDimConvFactor = 1;
                decimal pdTranQty = System.Convert.ToDecimal(split[(int)layout.adjQty]);
                string partDesc = split[(int)layout.partDescr];
                string pcNeqQtyAction = "";
                string pcMessage = "";
                IQA.NegativeInventoryTest(partNum, pcWhseCode, 
                    pcBinNum, pcLotNum, pcDimCode, pdDimConvFactor, pdTranQty, 
                    out pcNeqQtyAction, out pcMessage);
                Epicor.Mfg.BO.InventoryQtyAdjDataSet.InventoryQtyAdjRow row =
                    (Epicor.Mfg.BO.InventoryQtyAdjDataSet.InventoryQtyAdjRow)ds.InventoryQtyAdj.Rows[0];
                row.Company = "CA";
                row.WareHseCode = pcWhseCode;
                row.BinNum = pcBinNum;
                row.ConvFactor = pdDimConvFactor;
                row.DimCode = pcDimCode;
                row.PartNum = partNum;
                row.PartPartDescription = partDesc;
                row.AdjustQuantity = pdTranQty;
                row.ReasonCodeDescription = "INADJ-Cycle Count Adjustment";
                row.Reference = "collgiateNonQCStock";
                row.ReasonCode = split[(int)layout.reasonCode];
                IQA.SetInventoryQtyAdj(ds);
                
            }
        }
    }
}