using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace InvBox
{
    class PackSlip
    {
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow dtlRow;
        Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow custShipRow;
        Invoice inv;
        public PackSlip(Epicor.Mfg.Core.Session vanSession, int pack)
        {
            InitCustShip();
        }
        public void ExtactAddress(StreetAddress sa)
        {
            sa.Address1 = this.custShipRow.BillAddr;
        }
        void InitCustShip()
        {
            custShipObj = new Epicor.Mfg.BO.CustShip(session.ConnectionPool);
            bool result = true;
            string message;
            try
            {
                custShipDs = custShipObj.GetByID(packNum);

                this.custShipRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                CustNum = custShipRow.CustNum;

                BuyGroup bgCheck = new BuyGroup(session, CustNum);
                this.IsBuyGroup = bgCheck.GetBuyGroupMember();
                string trackingNum = custShipRow.TrackingNumber;

                if (trackingNum.Length > 0)
                {
                    packNeedsTracking = false;
                }

                dtlRow = (Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow)custShipDs.ShipDtl.Rows[0];
                this.OrderNum = dtlRow.OrderNum;
                invoiced = custShipRow.Invoiced;
                // custShipDs.Dispose();
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
                result = false;
            }
            if (result)
            {
                packFound = true;
            }
        }
    }
}


        