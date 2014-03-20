using System;
using System.IO;
using System.Data.SqlClient;

namespace CloseJobs
{
    public class JobCloseXman
    {
        Epicor.Mfg.Core.Session objSess;
    	Epicor.Mfg.BO.JobClosing jobCloseObj;
        // Epicor.Mfg.BO.JobClosingDataSet jobClosingDs;
        // Epicor.Mfg.BO.JobEntry jobEntryObj;
        // Epicor.Mfg.BO.JobEntryDataSet jobEntryDs;
        // Epicor.Mfg.BO.JobHeadListDataSet jobHeadListDs;
	    // Epicor.Mfg.BO.JobClosingListDataSet jobClosingListDs;

        public JobCloseXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                  "AppServerDC://VantageDB1:8301", 
		  Epicor.Mfg.Core.Session.LicenseType.Default);
        }
        public void CloseJob(string jobNo)
        {
            jobCloseObj = new Epicor.Mfg.BO.JobClosing(objSess.ConnectionPool);
            Epicor.Mfg.BO.JobClosingDataSet jobClosingDs = new Epicor.Mfg.BO.JobClosingDataSet();
            jobCloseObj.GetNewJobClosing(jobClosingDs);
            bool goOn = true;
            string message;
            string exMessage;
            try
            {
                jobCloseObj.OnChangeJobNum(jobNo, jobClosingDs);
                jobCloseObj.OnChangeJobClosed(jobClosingDs);
                
            }
            catch (Exception e)
            {
                exMessage = e.Message;
                goOn = false;
            }
            if (goOn)
            {
                Epicor.Mfg.BO.JobClosingDataSet.JobClosingRow row = (Epicor.Mfg.BO.JobClosingDataSet.JobClosingRow)jobClosingDs.JobClosing.Rows[0];
                System.DateTime dateObj = DateTime.Today;
                //Convert.ToInt32(year),
                //            Convert.ToInt32(month), Convert.ToInt32(day));
                row.ClosedDate = dateObj;
                row.Company = "CA";
                row.JobCompletionDate = dateObj;
                row.JobComplete = true;
                row.JobClosed = true;
                row.QuantityContinue = true;
                string jobNumber = row.JobNum;

                bool result = true;
                jobCloseObj.PreCloseJob(jobClosingDs, out result);
                try
                {
                    jobCloseObj.CloseJob(jobClosingDs, out message);
                    string read = message;
                }
                catch (Exception e)
                {
                    exMessage = e.Message;
                }
            }
        }
    }
}
     
