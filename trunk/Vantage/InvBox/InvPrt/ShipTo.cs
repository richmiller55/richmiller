using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace InvPrt
{
    public class ShipTo : StreetAddress
    {
	string shipToId = "";

        public ShipTo()
        {
            //
        }
        public ShipTo(AddrTypes addrType,string custID,string shipTo)
        {
            this.AddressType = (int)addrType;
            this.CustId = custID;
            this.ShipToId = shipTo;
            this.CustDSFromID();
            this.FillCustomerAddress();
        }
        void FillCustomerAddress()
        {
# if DEBUG
            this.CustFrtTerms = shipToRow.ShortChar01;
            this.CustName = shipToRow.Name;
            this.Address1 = shipToRow.Address1;
            this.Address2 = shipToRow.Address2;
            this.Address3 = shipToRow.Address3;
            this.City = shipToRow.City;
            this.State = shipToRow.State;
            this.ZipCode = shipToRow.ZIP;
            this.TermsCode = custRow.TermsCode;
            this.TermsDescr = custRow.TermsDescription;
            this.TaxAuthorityCode = custRow.TATaxAuthorityDescription;
            this.TaxAuthorityDescr = custRow.TaxAuthorityCode;
            this.TaxExempt = custRow.TaxExempt;
            this.TaxRegionCode = custRow.TaxRegionCode;
            this.TaxRegionDescr = custRow.TaxRegionDescription;
            this.TermsCode = custRow.TermsCode;
            this.TermsDescr = custRow.TermsDescription;
            if (custRow.ShortChar01.CompareTo("FF") == 0)
            {
                this.FreightFree = true;
            }
# endif
        }
        void CustDSFromID()
        {

        }
        public string ShipToId
        {
            get
            {
                return shipToId;
            }
            set
            {
                shipToId = value;
            }
        }

   }
}