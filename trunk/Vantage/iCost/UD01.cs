using System;
using System.Collections.Generic;
using System.Text;
using Epicor.Mfg.BO;
namespace iCost
{
    class CostTable
    {
        Epicor.Mfg.Core.Session objSess;
        public CostTable()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8331", Epicor.Mfg.Core.Session.LicenseType.Default);
            UD01 ud01Obj = new UD01(objSess.ConnectionPool);
            UD01DataSet ud01Ds = new UD01DataSet();
            ud01Obj.GetaNewUD01(ud01Ds);
            // ud01Obj.GetNewUD01(ud01Ds, "1200", "", "", "");
            // UD01DataSet.UD01Row row = (UD01DataSet.UD01Row)
            UD01DataSet.UD01Row row = (UD01DataSet.UD01Row)ud01Ds.UD01.Rows[0];
            row.Character01 = "Rich Miller";
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
    }
}