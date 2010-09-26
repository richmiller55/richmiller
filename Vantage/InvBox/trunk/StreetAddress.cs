using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public enum AddrTypes
    {
        ShipTo,
        BillTo,
        RemitTo
    }
    public class StreetAddress
    {
        private int  addressType;
        private string custID = "";
        private string custName = "";
        private string address1 = "";
        private string address2 = "";
        private string address3 = "";
        private string city = "";
        private string state = "";
        private string zipCode = "";
        private string country = "";
        private string termsCode = "";
        private string termsDescr = "";
        private bool freightFree = false;
        
        public StreetAddress(AddrTypes addrType)
        {
            this.AddressType = (int)addrType;
            // take the default ctor for now
            // fill the thing with actual data in a init function
            // taking 
        }
        public void init()
        {
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
        public string CustID
        {
            get
            {
                return custID;
            }
            set
            {
                custID = value;
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
    }
}