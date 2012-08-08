using System;
using System.IO;

namespace POfeed
{
    public enum col
    {
        PONum,
        POLine,
        upcShortCode,
        RevisedDate04,
        RevisedDate01,
        filler
    }
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
        void LoopOverRows(string[] split, Epicor.Mfg.BO.PODataSet ds)
        {
            bool processDate04 = false;
            string strDate04 = split[(int)col.RevisedDate04];
            int intDate04 = Convert.ToInt32(strDate04);
            System.DateTime UpdateDate04 = System.DateTime.Today;
            if (intDate04.Equals(0) || intDate04.Equals(19000100))
            {
                processDate04 = false;
            }
            else
            {
                UpdateDate04 = ConvertStrToDate(strDate04);
                processDate04 = true;
            }
            bool processDate01 = false;
            string strDate01 = split[(int)col.RevisedDate01];
            int intDate01 = Convert.ToInt32(strDate01);
            System.DateTime UpdateDate01 = System.DateTime.Today;
            if (intDate01.Equals(0) || intDate01.Equals(19000100))
            {
                processDate01 = false;
            }
            else
            {
                UpdateDate01 = ConvertStrToDate(strDate01);
                processDate01 = true;
            }
            foreach (Epicor.Mfg.BO.PODataSet.PODetailRow row in ds.PODetail.Rows)
            {
                if (processDate04) row.Date04 = UpdateDate04;
                if (processDate01) row.Date01 = UpdateDate01;
                try
                {
                    this.poObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        void UpdateLine(string[] split,Epicor.Mfg.BO.PODataSet ds,int rowNum)
        {
            bool processDate04 = false;
            string strDate04 = split[(int)col.RevisedDate04];
            int intDate04 = Convert.ToInt32(strDate04);
            System.DateTime UpdateDate04 = System.DateTime.Today;
            if (intDate04.Equals(0) || intDate04.Equals(19000100))
            {
                processDate04 = false;
            }
            else
            {
                UpdateDate04 = ConvertStrToDate(strDate04);
                processDate04 = true;
            }
            bool processDate01 = false;
            string strDate01 = split[(int)col.RevisedDate01];
            int intDate01 = Convert.ToInt32(strDate01);
            System.DateTime UpdateDate01 = System.DateTime.Today;
            if (intDate01.Equals(0) || intDate01.Equals(19000100))
            {
                processDate01 = false;
            }
            else
            {
                UpdateDate01 = ConvertStrToDate(strDate01);
                processDate01 = true;
            }
            Epicor.Mfg.BO.PODataSet.PODetailRow row =
    (Epicor.Mfg.BO.PODataSet.PODetailRow)ds.PODetail.Rows[rowNum];
            if (processDate04) row.Date04 = UpdateDate04;
            if (processDate01) row.Date01 = UpdateDate01;
            try
            {
                this.poObj.Update(ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public void UpdateExAsia(string line)
        {
        }

        public void PODateUpdate(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string PONumStr = split[(int)col.PONum];
            bool processAllLines = false;
            if (PONumStr.Equals("PO")) return;
            if (PONumStr.Equals("")) return;
            int PONum = Convert.ToInt32(PONumStr);
            string strPOLine = split[(int)col.POLine];
            int POLine = 0;
            // string upc = "";
            if (strPOLine.Equals("ALL")) {
                processAllLines = true;
            } else {
                POLine = Convert.ToInt32(split[(int)col.POLine]);
                processAllLines = false;  // stays false
            }
            
            ds = this.poObj.GetByID(PONum);
            String violationMsg = "";
            Epicor.Mfg.BO.PODataSet.POHeaderRow headRow =
                (Epicor.Mfg.BO.PODataSet.POHeaderRow)ds.POHeader.Rows[0];
            headRow.ApprovalStatus = "U";
            headRow.Approve = false;
            headRow.ReadyToPrint = false;
            try
            {
                System.Boolean approvalValue = false;
                this.poObj.ChangeApproveSwitch(approvalValue, out violationMsg, ds);
                this.poObj.Update(ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
            if (processAllLines)
            {
                LoopOverRows(split, ds);
            }
            else
            {
                UpdateLine(split, ds,POLine -1);
            }
           
            ds = this.poObj.GetByID(PONum);
            violationMsg = "";
            headRow = (Epicor.Mfg.BO.PODataSet.POHeaderRow)ds.POHeader.Rows[0];
            headRow.ApprovalStatus = "A";
            headRow.Approve = true;
            headRow.ReadyToPrint = true;
            try
            {
                this.poObj.ChangeApproveSwitch(true, out violationMsg, ds);
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
     
