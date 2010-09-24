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
        private int addressType;
        private string custID = new String();
        private string custName = new String();
        private string address1 = new String();
        private string address2 = new String();
        private string address3 = new String();
        private string city = new String();
        private string state = new String();
        private string zipCode = new String();
        private string country = new String();
        private string termsCode = new String();
        private string termsDescr = new String();
        private bool freightFree = false;
        
        public StreetAddress(AddrTypes addrType)
        {
            this.AddressType = addrType;
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
    }
}