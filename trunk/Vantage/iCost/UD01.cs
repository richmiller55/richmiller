using System;
using System.Collections.Generic;
using System.Text;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
namespace iCost
{
    class CostTable
    {
        Epicor.Mfg.Core.Session objSess;
        public CostTable()
        {
            objSess = new Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8331", Session.LicenseType.Default);
            this.GetOldRecord();
        }
        public void AddNewRecord()
        {
            UD01 ud01Obj = new UD01(objSess.ConnectionPool);
            UD01DataSet ud01Ds = new UD01DataSet();
            ud01Obj.GetaNewUD01(ud01Ds);
            UD01DataSet.UD01Row row = (UD01DataSet.UD01Row)ud01Ds.UD01.Rows[0];
            row.Character01 = "Rich Miller III";
            row.Key1 = "1200";
            try
            {
                ud01Obj.Update(ud01Ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public void GetOldRecord()
        {
            UD01 ud01Obj = new UD01(objSess.ConnectionPool);
            try
            {
                UD01DataSet oldDS = ud01Obj.GetByID("1200","","","","");
                UD01DataSet.UD01Row row = (UD01DataSet.UD01Row)oldDS.UD01.Rows[0];           
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
}