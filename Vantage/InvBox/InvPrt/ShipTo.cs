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