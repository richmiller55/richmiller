using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class ShipToStreetAddress : StreetAddress
    {
        int shipToNo;

        public ShipToStreetAddress(Epicor.Mfg.Core.Session session,
                             AddrTypes addrType, string custID, int shipTo)
        {
            this.session = session;
            this.AddressType = (int)addrType;
            this.CustID = custID;
            this.ShipToNo = shipTo;
            this.CustDSFromID();
            this.FillCustomerAddress();
        }
        public int ShipToNo
        {
            get
            {
                return shipToNo;
            }
            set
            {
                shipToNo = value;
            }
        }
        void CustDSFromID()
        {
            Epicor.Mfg.BO.Customer customerObj =
                   new Epicor.Mfg.BO.Customer(session.ConnectionPool);

            try
            {
                // Epicor.Mfg.BO.CustShip custShipDs = customerObj.GetByCustID
                // Epicor.Mfg.BO.CustomerDataSet custDs = customerObj.GetByCustID(this.CustID);

//                Epicor.Mfg.BO.CustomerDataSet custDs = customerObj.GetShipTo(CustID, ShipToNo);
//                Epicor.Mfg.BO.CustShipDataSet shipToDS = (Epicor.Mfg.BO.CustShipDataSet)custDs.ShipTo.Rows[0];
//                sting addr1 = shipToDS.
// Epicor.Mfg.BO.CustomerDataSet.ShipToDataTable shipToData = (Epicor.Mfg.BO.CustomerDataSet.ShipToDataTable)custDs.ShipTo;
//                Epicor.Mfg.BO.CustomerDataSet shipToData = (Epicor.Mfg.BO.CustomerDataSet.ShipToDataTable)custDs.ShipTo;
                //this.custRow = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)custDs.Customer.Rows[0];

            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
}
