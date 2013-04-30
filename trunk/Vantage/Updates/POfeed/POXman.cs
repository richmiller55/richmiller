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
            if (tran.UpdateOk)
            {
                UnApprovePO(tran);
                UpdateLine(tran);
                ApprovePO(tran);
            }
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
                foreach (Epicor.Mfg.BO.PODataSet.PORelRow relRow in ds.PORel.Rows) 
                if (row.PartNum.Equals(relRow.POLinePartNum))
                {
                    if (tran.TypeOfDate.Equals("exAsia_")) row.Date01 = tran.PODate;
                    else if (tran.TypeOfDate.Equals("profor_"))
                    {
                        relRow.PromiseDt = tran.PODate;
                        // yes you could pull it from the database, 
                        // relRow.PromiseDt = row.Date04;
                    }
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
