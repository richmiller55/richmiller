using System;
using System.IO;


namespace POfeed
{

    public class POXman
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.PO poObj;
        Epicor.Mfg.BO.PODataSet ds;
        //   pilot 8331
        //   sys   8301
        public POXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.poObj = new Epicor.Mfg.BO.PO(objSess.ConnectionPool);
        }
        public void PODateUpdate(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string PONumStr = split[(int)col.PONum];
            if (PONumStr.Equals("PO")) return;
            int PONum = Convert.ToInt32(PONumStr);
            int POLine = Convert.ToInt32(split[(int)col.POLine]);
            System.DateTime revisedDate = ConvertStrToDate(split[(int)col.RevisedDate]);
            
            ds = this.poObj.GetByID(PONum);
            Epicor.Mfg.BO.PODataSet.PODetailRow row =
                (Epicor.Mfg.BO.PODataSet.PODetailRow)ds.PODetail.Rows[POLine - 1];
            row.CalcDueDate = revisedDate;
            try
                {
                    this.poObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        
        public System.DateTime  ConvertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }

    }
}
     
