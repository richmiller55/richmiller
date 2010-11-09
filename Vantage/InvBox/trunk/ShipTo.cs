using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class ShipTo : StreetAddress
    {
	string shipToId = "";
	Epicor.Mfg.BO.CustomerDataSet.ShipToRow shipToRow;
        public ShipTo()
        {
            //
        }
        public ShipTo(Epicor.Mfg.Core.Session session,AddrTypes addrType,
                      string custID,string shipTo)
        {
            this.session = session;
            this.AddressType = (int)addrType;
            this.CustId = custID;
            this.ShipToId = shipTo;
            this.CustDSFromID();
            this.FillCustomerAddress();
        }
        void FillCustomerAddress()
	{
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
        }
        void CustDSFromID()
        {
            Epicor.Mfg.BO.Customer customerObj =
                   new Epicor.Mfg.BO.Customer(session.ConnectionPool);

            string message = "does exception throw";
            Epicor.Mfg.BO.CustomerDataSet custDs;
            try
            {
                custDs = customerObj.GetShipTo(this.CustId, this.ShipToId);
                this.custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)
                          custDs.Customer.Rows[0];
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            try
            {
                custDs = customerObj.GetShipTo(this.CustId, this.ShipToId);
                this.shipToRow = (Epicor.Mfg.BO.CustomerDataSet.ShipToRow)
                          custDs.ShipTo.Rows[0];
            }
            catch (Exception e)
            {
                message = e.Message;
            }
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