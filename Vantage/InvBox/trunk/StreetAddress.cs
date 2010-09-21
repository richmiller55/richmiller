using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class StreetAddress
    {

        private string custID = new String();
        private string custName = new String();
        private string address1 = new String();
        private string city = new String();
        private string state = new String();
        private string zipCode = new String();
        private string country = new String();
        public StreetAddress()
        {

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
    }
}