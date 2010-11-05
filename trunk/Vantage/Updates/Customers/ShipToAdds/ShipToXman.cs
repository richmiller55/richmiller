using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ShipToLoad
{
    public enum sqlColumns
    {
        cGroup,
        soldTo,
        oldSoldTo,
        shipTo,
        parent,
        custName,
        addr1,
        addr2,
        addr3,
        city,
        state,
        zip5,
        areaCode,
        phone,
        creditComment,
        vsFlag,
        creditLimit,
        creditRating,
        salesTaxRate,
        taxableFlag,
        taxExemptNo,
        salesPerson,
        shortContact,
        timeZone,
        source,
        lastStartDate,
        payTerms,
        customerType,
        freightTerms
    }
    public class ShipToXmanager
    {
        public string dataSet;
        public SqlDataReader reader;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Customer customerObj;
        Epicor.Mfg.BO.CustomerDataSet ds;

        public ShipToXmanager()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(objSess.ConnectionPool);

            //
            // insertShipTo();
            // insertBuyGroups();
            // deleteCustomers();
            //
        }
        public bool ShipToExists(ShipTo st)
        {
            string CustID = st.getCustId();
            string ShipToNum = st.getShipTo();
            bool result = true;
            string message = "does exception throw";
            try
            {
                Epicor.Mfg.BO.CustomerDataSet custDs;
                custDs = customerObj.GetShipTo(CustID, ShipToNum);
                Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow =
                    (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)custDs.ShipTo.Rows[0];
                string shipTo = shipToRow.ShipToNum;
            }
            catch (Exception e)
            {
                message = e.Message;
                result = false;
            }
            return result;
        }
        public bool ShipToExists(ExShipTo st)
        {
            string CustID = st.getCustId();
            string ShipToNum = st.getShipTo();
            bool result = true;
            string message = "does exception throw";
            try
            {
                Epicor.Mfg.BO.CustomerDataSet custDs;
                custDs = customerObj.GetShipTo(CustID, ShipToNum);
                Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow =
                    (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)custDs.ShipTo.Rows[0];
                string shipTo = shipToRow.ShipToNum;
            }
            catch (Exception e)
            {
                message = e.Message;
                result = false;
            }
            return result;
        }
        public void addShipTo(ExShipTo st)
        {
            string CustID = st.getCustId();
            string message = "Customer found";
            try
            {
                ds = customerObj.GetByCustID(CustID);
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow CustRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
                int custNum = CustRow.CustNum;
                customerObj.GetNewShipTo(ds, custNum);
                        
                int count = ds.ShipTo.Rows.Count;
                int index = count - 1;
                Epicor.Mfg.BO.CustomerDataSet.ShipToRow ShipToRow = (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)ds.ShipTo.Rows[index];
                ShipToRow.ShipToNum = st.getShipTo();
                ShipToRow.Address1 = st.getCenter();
                ShipToRow.Address2 = st.getAddress();
                ShipToRow.City     = st.getCity();
                ShipToRow.State    = st.getState();
                ShipToRow.ZIP      = st.getZip();
                ShipToRow.Country = st.getCountry();
                ShipToRow.CountryNum = st.getCountryNo();
                ShipToRow.TerritoryID = "10";
                ShipToRow.Name = st.getName();
                ShipToRow.ShipViaCode = st.getShipVia();
                ShipToRow.PhoneNum = st.getPhone();
                ShipToRow.Company = "CA";
                message = "Shipto Added OK";
                customerObj.Update(ds);
                ds.Clear();
            }
                catch (Exception e)
            {
                message = e.Message;
            }
        }
        public int getCustNoFromID(Epicor.Mfg.BO.Customer custObj, string custID)
        {
            Epicor.Mfg.BO.CustomerDataSet ds = new Epicor.Mfg.BO.CustomerDataSet();
            ds = custObj.GetByCustID(custID);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
            return (int)row.CustNum;
        }
     }
}