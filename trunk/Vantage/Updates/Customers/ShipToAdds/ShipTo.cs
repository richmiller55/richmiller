using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace ShipToLoad
{
    public class ShipTo
    {
        string m_custId;
        string m_shipToId;
        string m_Name;
        string m_Address1;
        string m_Address2 = "";
        string m_Address3 = "";
        string m_City = "";
        string m_State;
        string m_Zip;
        string m_Country;
        string m_Phone = "";
        int    m_ShipOrder;
        int    m_CountryNo;
        string m_ShipVia;

        public ShipTo()
        {
            //
        }

        public string CustId
        {
            get
            {
                return m_custId;
            }
            set
            {
                m_custId = value;
            }
        }
        public string ShipToId
        {
            get
            {
                return m_shipToId;
            }
            set
            {
                m_shipToId = value;
            }
        }
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }
        public string Address1
        {
            get
            {
                return m_Address1;
            }
            set
            {
                m_Address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return m_Address2;
            }
            set
            {
                m_Address2 = value;
            }
        }
        public string Address3
        {
            get
            {
                return m_Address3;
            }
            set
            {
                m_Address3 = value;
            }
        }
        public string City
        {
            get
            {
                return m_City;
            }
            set
            {
                m_City = value;
            }
        }
        public string State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }
        public string Zip
        {
            get
            {
                return m_Zip;
            }
            set
            {
                m_Zip = value;
            }
        }
        public string Country
        {
            get
            {
                return m_Country;
            }
            set
            {
                m_Country = value;
            }
        }
        public string Phone
        {
            get
            {
                return m_Phone;
            }
            set
            {
                m_Phone = value;
            }
        }
        public int ShipOrder
        {
            get
            {
                return m_ShipOrder;
            }
            set
            {
                m_ShipOrder = value;
            }
        }
        public int CountryNo
        {
            get
            {
                return m_CountryNo;
            }
            set
            {
                m_CountryNo = value;
            }
        }
        public string ShipVia
        {
            get
            {
                return m_ShipVia;
            }
            set
            {
                m_ShipVia = value;
            }
        }
        public void setCountryNo(string country)
        {
            this.CountryNo = 1;
            this.Country = "USA";
            if (country.ToUpper().Equals("CANADA"))
            {
                this.CountryNo = 36;
                this.Country = "Canada";
            }
            if (country.ToUpper().Equals("PR"))
            {
                this.CountryNo = 149;
                this.Country = "Puerto Rico";
            }
        }
   }
}