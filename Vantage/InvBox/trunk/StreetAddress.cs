using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
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
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustomerDataSet.CustomerRow custRow;
        private int  addressType;

        private string custID = "";
        private int custNo = 0;
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
        private string custFrtTerms = "";
        private bool freightFree = false;

        public StreetAddress(Epicor.Mfg.Core.Session session,
                             AddrTypes addrType, string custID)
        {
            this.session = session;
            this.AddressType = (int)addrType;
            this.CustID = custID;
            this.CustDSFromID();
            this.FillCustomerAddress();
        }
        void CustDSFromID()
        {
            Epicor.Mfg.BO.Customer customerObj =
                   new Epicor.Mfg.BO.Customer(session.ConnectionPool);

            try
            {
                Epicor.Mfg.BO.CustomerDataSet custDs = customerObj.GetByCustID(this.CustID);
                this.custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];
            }
            catch (Exception e)
            {
                string message = e.Message;
            }

        }
        void FillCustomerAddress()
        {
            this.CustFrtTerms = custRow.ShortChar01;
            this.CustName = custRow.Name;
            this.Address1 = custRow.Address1;
            this.Address2 = custRow.Address2;
            this.Address3 = custRow.Address3;
            this.City = custRow.City;
            this.State = custRow.State;
            this.ZipCode = custRow.Zip;
            this.TermsCode = custRow.TermsCode;
            this.TermsDescr = custRow.TermsDescription;
            if (custRow.ShortChar01.CompareTo("FF") == 0)
            {
                this.FreightFree = true;
            }
        }
        public string AddressStr
        {
            get
            {
                string crlf = "\n";
                string buffer = AddressTypeDescr + " " + CustID + crlf;
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
    }
}