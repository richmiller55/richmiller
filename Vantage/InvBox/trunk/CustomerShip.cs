using System;
using System.Collections.Generic;
using System.Text;

namespace InvBox
{
    class CustomerShip
    {
        Int32 packNo;
        Int32 BillToCustNo;
        Int32 ShipToCustNo;
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow shipHeadRow;
        bool packExists;
        StreetAddress shipTo;
        StreetAddress billTo;
        // please justify all this crap below, all of this should be in the StreetAddress objects
        string shipToName;
        string shipToAddress1;
        string shipToAddress2;
        string shipToCity;
        string shipToState;
        string shipToPostalCode;
        string shipToCountry;
        string customerId;
        string custFrtTerms;

        // suspect member vars
        int CustomerNo;
        public CustomerShip(Epicor.Mfg.Core.Session vanSession,Int32 vanPackNo)
        {
            this.session = vanSession;
            this.packNo = vanPackNo;
            init();
        }
        void init()
        {
            custShipObj = new Epicor.Mfg.BO.CustShip(session.ConnectionPool);
            this.packExists = true;
            try
            {
                custShipDs = custShipObj.GetByID(packNo);
            }
            catch (Exception e)
            {
                this.packExists = false;
            }
            if (this.packExists)
            {
                this.shipHeadRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                this.BillToCustNo = this.shipHeadRow.BTCustNum;
                this.ShipToCustNo = this.shipHeadRow.CustNum;

            }
        }

        enum addrListCol5
        {
            name,
            address1,
            address2,
            cityStateZip,
            country
        }
        enum addrListCol4
        {
            name,
            address1,
            cityStateZip,
            country
        }
        public bool PackExists
        {
            get
            {
                return packExists;
            }
            set
            {
                packExists = value;
            }
        }
        public StreetAddress BillTo
        {
            get
            {
                return billTo;
            }
            set
            {
                billTo = value;
            }
        }
        public StreetAddress ShipTo
        {
            get
            {
                return shipTo;
            }
            set
            {
                shipTo = value;
            }
        }
        public string Name
        {
            get
            {
                return shipToName;
            }
            set
            {
                shipToName = value;
            }
        }
        public string Address1
        {
            get
            {
                return shipToAddress1;
            }
            set
            {
                shipToAddress1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return shipToAddress2;
            }
            set
            {
                shipToAddress2 = value;
            }
        }
        public string City
        {
            get
            {
                return shipToCity;
            }
            set
            {
                shipToCity = value;
            }
        }
        public string CustFrtTerms
        {
            get
            {
                return custFrtTerms;
            }
            set
            {
                custFrtTerms = value;
            }
        }
        public string CustomerId
        {
            get 
            {
                return customerId;
            }
            set
            {
                customerId = value;
            }
        }
        void getCustomerAddressInfo(int custNo, StreetAddress sa)
        {
            Epicor.Mfg.BO.Customer customerObj;
            Epicor.Mfg.BO.CustomerDataSet custDs;
            session = new Epicor.Mfg.Core.Session("rich", "homefed55", "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            customerObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            string message = "OK";
            bool okFound = true;
            try
            {
                custDs = customerObj.GetByID(custNo);
                Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
                this.CustFrtTerms = custRow.ShortChar01;
                this.Address1 = custRow.Address1;
                this.Address2 = custRow.Address2;
                this.Address3 = custRow.Address3;
                this.City = custRow.City;
                this.State = custRow.State;
                this.ZipCode = custRow.Zip;
                this.TermsCode = custRow.TermsCode;
                this.TermsDescr = custRow.TermsDescription;
            }
            catch (Exception e)
            {
                message = e.Message;
                okFound = false;
            }
        }
    }
}
