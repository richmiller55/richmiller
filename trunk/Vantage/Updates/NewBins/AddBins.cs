using System;
using System.Collections.Generic;
using System.Text;

namespace whseBins
{
    public class AddBins
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Customer customerObj;
        Epicor.Mfg.BO.CustomerDataSet custDs;
        public AddBins()
        {
            this.objSess = new Epicor.Mfg.Core.Session(
            "rich", "homefed55", "AppServerDC://VantageDB1:8301",
            Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);
        }
        public void setGlobalIncFlag(string custId)
        {
            string message = "OK";
            bool okToUpdate = true;
            try
            {
                custDs = customerObj.GetByCustID(custId);
            }
            catch (Exception e)
            {
                message = e.Message;
                message = "NoGo";
                okToUpdate = false;
            }
            if (okToUpdate)
            {
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
                custRow.GlobalCredIncOrd = false;
                // custRow.CreditHold = false;
                try
                {
                    customerObj.Update(custDs);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void setData(int custNum)
        {
            string message = "OK";
            custDs = customerObj.GetByID(custNum);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = 
                (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
            custRow.PrintStatements = true;
            string currentVia = custRow.ShipViaCode;
            int result = currentVia.CompareTo("");
            if (result == 0)
            {
                custRow.ShipViaCode = "FGRB";
            }
            try
            {
                customerObj.Update(custDs);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
        }
        public void ChangeCustGrp(string custId, string newGrp)
        {
            string message = "OK";
            bool okToUpdate = true;
            try
            {
                custDs = customerObj.GetByCustID(custId);
            }
            catch (Exception e)
            {
                message = e.Message;
                message = "NoGo";
                okToUpdate = false;
            }
            if (okToUpdate)
            {
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = 
                    (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
                custRow.GroupCode = newGrp;
                try
                {
                    customerObj.Update(custDs);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void setTerrRep(string custId, 
                string terr,
                string salesRep,
                bool changeT,
                bool changeR,
                bool changeLock) {
            string message = "OK";
            try
            {
                custDs = customerObj.GetByCustID(custId);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
            bool dirty = false;
            if (changeT)
            {
                custRow.TerritoryID = terr;
                dirty = true;
            }
            if (changeR)
            {
                custRow.SalesRepCode = salesRep;
                dirty = true;
            }
            if (changeLock)
            {
                custRow.TerritoryLock = true;
                dirty = true;
            }
            if (dirty)
            {
                try
                {
                    customerObj.Update(custDs);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
    }
}