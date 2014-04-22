using System;
using System.Collections.Generic;
using System.Text;

namespace NewBins
{
    public class AddBins
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.WhseBin whseBin;
        
        public AddBins()
        {
            this.objSess = new Epicor.Mfg.Core.Session(
            "rich", "homefed55", "AppServerDC://VantageDB1:8321",
            Epicor.Mfg.Core.Session.LicenseType.Default);
            this.whseBin = new Epicor.Mfg.BO.WhseBin(objSess.ConnectionPool);
        }
        public void AddNewBin_AB(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string whseBinNumBase = split[0];
            string whseBinNum_a = whseBinNumBase + "A";
            this.AddNewBin(whseBinNum_a);
            string whseBinNum_b = whseBinNumBase + "B";
            this.AddNewBin(whseBinNum_b);
            
        }
        public void AddNewBin(string whseBinNum)
        {
            string warehouseCode = "01";
            bool okToCreateNew = true;
           
            Epicor.Mfg.BO.WhseBinDataSet whseBinDs = 
                whseBin.GetByID(warehouseCode, whseBinNum);
            whseBinDs = new Epicor.Mfg.BO.WhseBinDataSet();
            whseBin.GetNewWhseBin(whseBinDs, warehouseCode);
            Epicor.Mfg.BO.WhseBinDataSet.WhseBinRow whseBinRow
                = (Epicor.Mfg.BO.WhseBinDataSet.WhseBinRow)whseBinDs.WhseBin.Rows[0];

            whseBinRow.BinNum = whseBinNum;
            whseBinRow.Description = whseBinNum;
            if (okToCreateNew)
            {
                try
                {
                    whseBin.Update(whseBinDs);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
    }
}
