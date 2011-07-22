using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateSalesTax
{
    public class XMan
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.SalesTax taxObj;
        Epicor.Mfg.BO.SalesTaxDataSet ds;
        public XMan()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55", "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            taxObj = new Epicor.Mfg.BO.SalesTax(objSess.ConnectionPool);
        }
        public void setNewRate(string zip, string newRate)
        {
            string message = "OK";
            bool okToUpdate = true;
            try
            {
                ds = taxObj.GetByID(zip);
            }
            catch (Exception e)
            {
                message = e.Message;
                message = "NoGo";
                okToUpdate = false;
            }
            if (okToUpdate)
            {
                Epicor.Mfg.BO.SalesTaxDataSet.SalesTaxRow taxRow = (Epicor.Mfg.BO.SalesTaxDataSet.SalesTaxRow)ds.SalesTax.Rows[0];
                decimal rate = Convert.ToDecimal(newRate);
                taxRow.Percent = rate;
                try
                {
                    taxObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
    }
}
