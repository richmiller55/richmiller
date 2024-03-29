using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;

namespace InvPrt
{
    public enum AddrTypes
    {
        SoldTo,
        ShipTo,
        BillTo,
        RemitTo
    }
    public class StreetAddress
    {
        protected int  addressType;
        protected string custId = "";
        protected int custNo = 0;
        protected string custName = "";
        protected string shipToID = "";
        protected string address1 = "";
        protected string address2 = "";
        protected string address3 = "";
        protected string city = "";
        protected string state = "";
        protected string zipCode = "";
        protected string country = "";
        protected string termsCode = "";
        protected string territoryID = "";
        protected string salesRepCode = "";
        protected string shipViaCode = "";
        protected string resaleID = "";
        protected string taxExempt = "";
        protected string termsDescr = "";
        protected string custFrtTerms = "";
        protected bool freightFree = false;
        protected decimal taxRate = 0.0M;

        string m_taxAuthorityCode;
        string m_taxAuthorityDescr;
        string m_taxRegionCode;
        string m_taxRegionDescr;
        string pilotDsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
        public StreetAddress(AddrTypes addrType, int custNum, string shipToID)
        {
            this.AddressType = (int)addrType;
            this.CustNo = custNum;
            this.ShipToID = shipToID;
            this.FillCustomerAddress();
        }
        public StreetAddress(AddrTypes addrType, string custID, string shipToID)
        {
            // dump this ctor
            this.AddressType = (int)addrType;
            this.CustId = custID;
            this.ShipToID = shipToID;

        }
        protected void FillCustomerAddress()
        {
            string query = this.GetShipToByCustNum(this.CustNo, this.ShipToID);
            OdbcConnection connection = new OdbcConnection(this.PilotDsn);
            OdbcCommand command = new OdbcCommand(query, connection);
            connection.Open();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.CustName = reader["Name"].ToString();
                this.Address1 = reader["Address1"].ToString();
                this.Address2 = reader["Address2"].ToString();
                this.Address3 = reader["Address3"].ToString();
                this.City = reader["City"].ToString();
                this.State = reader["State"].ToString();
                this.ZipCode = reader["Zip"].ToString();
                this.Country = reader["Country"].ToString();
                this.TerritoryID = reader["TerritoryID"].ToString();
                this.SalesRepCode = reader["SalesRepCode"].ToString();
                this.ShipViaCode = reader["ShipViaCode"].ToString();
                this.ResaleID = reader["ResaleID"].ToString();
                this.TaxExempt = reader["TaxExempt"].ToString();
            }
        }
        private string GetShipToBaseSelect()
        {
            string sql = "select " +
            "st.Name as Name," +
            "st.Address1 as Address1," +
            "st.Address2 as Address2," +
            "st.Address3 as Address3," +
            "st.City as City," +
            "st.State as State," +
            "st.Zip as Zip," +
            "st.Country as Country," +
            "st.TerritoryID as TerritoryID," +
            "st.SalesRepCode as SalesRepCode," +
            "st.ShipViaCode as ShipViaCode," +
            "st.ResaleID as ResaleID," +
            "st.TaxExempt as TaxExempt" +
            " from pub.ShipTo as st " +
            " left join pub.Customer as cm " +
            " on cm.CustNum = st.CustNum ";
            return sql;
        }
        private string GetShipToByCustNum(int custNum, string shipToID)
        {
            string sql = this.GetShipToBaseSelect();
            sql += " where st.ShipToNum = " + "\'" + shipToID + "\'";
            sql += " and cm.CustNum = " + "\'" + custNum.ToString() + "\'";
            return sql;
        }
        public string AddressStr
        {
            get
            {
                string crlf = "\n";
                string buffer = AddressTypeDescr + " " + CustId + crlf;
                buffer += CustName + crlf;
                buffer += Address1 + crlf;
                if (Address2.CompareTo("") != 0)
                {
                    buffer += Address2 + crlf;
                }
                if (Address3.CompareTo("") != 0)
                {
                    buffer += Address3 + crlf;
                }
                buffer += City + ", " + State + " " + ZipCode;
                return buffer;
            }
            set
            {
            }
        }
        public int AddressType
        {
            get
            {
                return addressType;
            }
            set
            {
                addressType = value;
            }
        }
        public string AddressTypeDescr
        {
            get
            {
                string result = "";
                if (AddressType == (int)AddrTypes.BillTo)
                {
                    result = "Bill To";
                }
                if (AddressType == (int)AddrTypes.SoldTo)
                {
                    result = "Sold To";
                }
                if (AddressType == (int)AddrTypes.ShipTo)
                {
                    result = "Ship To";
                }
                return result;
            }
            set
            {

            }
        }
        public string CustId
        {
            get
            {
                return custId;
            }
            set
            {
                custId = value;
            }
        }
        public int CustNo
        {
            get
            {
                return custNo;
            }
            set
            {
                custNo = value;
            }
        }
        public string CustName
        {
            get
            {
                return custName;
            }
            set
            {
                custName = value;
            }
        }
        public string ShipToID
        {
            get
            {
                return shipToID;
            }
            set
            {
                shipToID = value;
            }
        }
        public string Address1
        {
            get
            {
                return address1;
            }
            set
            {
                address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return address2;
            }
            set
            {
                address2 = value;
            }
        }
        public string Address3
        {
            get
            {
                return address3;
            }
            set
            {
                address3 = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public string ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                zipCode = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string TermsCode
        {
            get
            {
                return termsCode;
            }
            set
            {
                termsCode = value;
            }
        }
        public string TermsDescr
        {
            get
            {
                return termsDescr;
            }
            set
            {
                termsDescr = value;
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
        public bool FreightFree
        {
            get
            {
                return freightFree;
            }
            set
            {
                freightFree = value;
            }
        }
        public string TaxAuthorityCode
        {
            get
            {
                return m_taxAuthorityCode;
            }
            set
            {
                m_taxAuthorityCode = value;
            }
        }
        public string TaxAuthorityDescr
        {
            get
            {
                return m_taxAuthorityDescr;
            }
            set
            {
                m_taxAuthorityDescr = value;
            }
        }
        public string TaxRegionCode
        {
            get
            {
                return m_taxRegionCode;
            }
            set
            {
                m_taxRegionCode = value;
            }
        }
        public string TaxRegionDescr
        {
            get
            {
                return m_taxRegionDescr;
            }
            set
            {
                m_taxRegionDescr = value;
            }
        }
        public decimal TaxRate
        {
            get
            {
                return taxRate;
            }
            set
            {
                taxRate = value;
            }
        }
        public string PilotDsn
        {
            get
            {
                return pilotDsn;
            }
            set
            {
                pilotDsn = value;
            }
        }
        public string TerritoryID
        {
            get
            {
                return territoryID;
            }
            set
            {
                territoryID = value;
            }
        }
        public string SalesRepCode
        {
            get
            {
                return salesRepCode;
            }
            set
            {
                salesRepCode = value;
            }
        }
        public string ShipViaCode
        {
            get
            {
                return shipViaCode;
            }
            set
            {
                shipViaCode = value;
            }
        }
        public string ResaleID
        {
            get
            {
                return resaleID;
            }
            set
            {
                resaleID = value;
            }
        }
        public string TaxExempt
        {
            get
            {
                return taxExempt;
            }
            set
            {
                taxExempt = value;
            }
        }

    }
}