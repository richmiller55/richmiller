using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateCustomerGeneral
{
    public enum colShipToUp
    {
        CustPrimeSalesRep,
        CustId,
        custTerritory,
        SalesRepName,
        shipToNo,
        currentShipToTerr,
        currentShipToSalesRep,
        filler
    }
    public class CustomerXMan
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Customer customerObj;
        Epicor.Mfg.BO.CustomerDataSet custDs;
        public CustomerXMan()
        {
            this.objSess = new Epicor.Mfg.Core.Session(
            "rich", "homefed55", "AppServerDC://VantageDB1:8301",
            Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);
        }
        public void setTermsCode(string custId)
        {
            string message = "OK";
            string newTerms = "CC1T";
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
                custRow.CreditCardOrder = true;
                custRow.TermsCode = newTerms;
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
        public void ChangeShipToTerr(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string CustId = split[(int)colShipToUp.CustId];
        }
        public void ChangeShipToRep(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string CustId = split[(int)colShipToUp.CustId];
            string message = "ok";
            bool okToUpdate = true;
            try
            {
                custDs = this.customerObj.GetByCustID(CustId);
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
                int custNum = custRow.CustNum;
                string shipToNum = split[(int)colShipToUp.shipToNo];
                string territoryId = split[(int)colShipToUp.custTerritory];
                string salesRepCode = split[(int)colShipToUp.CustPrimeSalesRep];
                string salesRepName = split[(int)colShipToUp.SalesRepName];

                Epicor.Mfg.BO.ShipTo shipto = new Epicor.Mfg.BO.ShipTo(this.objSess.ConnectionPool);
                Epicor.Mfg.BO.ShipToDataSet shipToDs = shipto.GetByID(custNum, shipToNum);
                Epicor.Mfg.BO.ShipToDataSet.ShipToRow shipToRow =
                    (Epicor.Mfg.BO.ShipToDataSet.ShipToRow)shipToDs.ShipTo.Rows[0];
                shipToRow.TerritoryID = territoryId;
                shipToRow.SalesRepCode = salesRepCode;
                shipToRow.SalesRepName = salesRepName;
                message = "OK";
                try
                {
                    shipto.Update(shipToDs);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void ChangeTerrByID(string custId, string newTerr, string salesRepNum)
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
                custRow.TerritoryID = newTerr;
                custRow.SalesRepCode = salesRepNum;
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
        public void ChangeRepOnInvoice(string custId, string repNum, string SalesRepName)
        {
            string message = "OK";
            bool goOn = true;
            try
            {
                custDs = customerObj.GetByCustID(custId);
            }
            catch (Exception e)
            {
                message = e.Message;
                goOn = false;
            }
            if (goOn)
            {
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
                custRow.SalesRepCode = repNum;
                custRow.SalesRepName = SalesRepName;
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
        public void ChangeTerr(int custNum, string newTerr)
        {
            string message = "OK";
            custDs = customerObj.GetByID(custNum);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
            custRow.TerritoryID = newTerr;
            custRow.SalesRepCode = "43";

            try
            {
                customerObj.Update(custDs);
            }
            catch (Exception e)
            {
                message = e.Message;
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
        public void setCustGrp(string custId, string newGrp)
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
        public void setTerrRep(string custId, string terr,string salesRep,
                                           bool changeT,bool changeR,bool changeLock)
        {
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