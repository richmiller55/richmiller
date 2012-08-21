using System;
using System.IO;

namespace POfeed
{
    public class POXman
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.PO poObj;
        //   pilot 8331
        //   sys   8301
        public POXman()
        {
            // keep old
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.poObj = new Epicor.Mfg.BO.PO(objSess.ConnectionPool);
        }
        public void PODateUpdate(Tran tran)
        {
            UnApprovePO(tran);
            UpdateLine(tran);
            ApprovePO(tran);
        }
        private void UnApprovePO(Tran tran)
        {
            // new
            Epicor.Mfg.BO.PODataSet ds = this.poObj.GetByID(tran.PONum);
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
        }
        private void ApprovePO(Tran tran)
        {
            Epicor.Mfg.BO.PODataSet ds = this.poObj.GetByID(tran.PONum);
            String violationMsg = "";
            Epicor.Mfg.BO.PODataSet.POHeaderRow headRow =
                (Epicor.Mfg.BO.PODataSet.POHeaderRow)ds.POHeader.Rows[0];
            headRow.ApprovalStatus = "A";
            headRow.Approve = true;
            headRow.ReadyToPrint = true;
            try
            {
                System.Boolean approvalValue = true;
                this.poObj.ChangeApproveSwitch(approvalValue, out violationMsg, ds);
                this.poObj.Update(ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        private void UpdateLine(Tran tran)
        {
            Epicor.Mfg.BO.PODataSet ds = this.poObj.GetByID(tran.PONum);

            foreach (Epicor.Mfg.BO.PODataSet.PODetailRow row in ds.PODetail.Rows)
            {
                if (row.POLine.Equals(tran.POLine))
                {
                    // Epicor.Mfg.BO.PODataSet.PODetailRow row =
                    //(Epicor.Mfg.BO.PODataSet.PODetailRow)ds.PODetail.Rows[rowNum];
                    if (tran.TypeOfDate.Equals("exAsia_")) row.Date01 = tran.PODate;
                    else if (tran.TypeOfDate.Equals("profor_")) row.Date04 = tran.PODate;
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
        }
    }
}
