using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace InvBox
{
    class PostTrackNo
    {
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        public PostTrackNo(Epicor.Mfg.Core.Session vanSession, int pack)
        {
            packFound = false;
            packNum = pack;
            session = vanSession;
            orderFF = false;
            packNeedsTracking = true;
            orderShipVia = "";
            
            customerFF = false;
            InitCustShip();
            if (packFound)
            {
                GetOrderInfo();
                GetCustomerInfo();
            }
        }
        public void PostTrackingToPack(string trackingStr, decimal weight, Shipment ship)
        {
            bool result = true;
            string message;
            try
            {
                Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow custShipRow;
                custShipRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                custShipRow.Weight = weight;
                string[] trackingSplit = trackingStr.Split(':');
                this.PostDetailTrackingNumbers(trackingSplit, ship);

                string tracking = trackingSplit[0];
                if (tracking.Length > 50)
                {
                    custShipRow.TrackingNumber = tracking.Substring(0, 49);
                }
                else
                {
                    custShipRow.TrackingNumber = tracking;
                }
                if (trackingStr.Length > 1000)
                {
                    custShipRow.Character01 = trackingStr.Substring(0, 999);
                }
                else
                {
                    custShipRow.Character01 = trackingStr;
                }
                try
                {
                    custShipObj.Update(custShipDs);
                }
                catch (Exception e)
                {
                    // tracking no did not post
                    message = e.Message;
                    result = false;
                }
            }
            catch (Exception e)
            {
                // tracking no did not post
                message = e.Message;
                result = false;
            }
        }
        private void PostDetailTrackingNumbers(string[] trackingSplit,Shipment ship)
        {
            Epicor.Mfg.BO.TrackingDtl trackDtlObj = new Epicor.Mfg.BO.TrackingDtl(session.ConnectionPool);
            string blank = "";
            foreach (string trackNumber in trackingSplit)
            {

                if (trackNumber.Equals(blank)) // .Equals(blank,StringComparison.Ordinal))
                {
                    continue;
                }
                Epicor.Mfg.BO.TrackingDtlDataSet ds = new
                    Epicor.Mfg.BO.TrackingDtlDataSet();
                trackDtlObj.GetNewTrackingDtl(ds, this.OrderNum);
                Epicor.Mfg.BO.TrackingDtlDataSet.TrackingDtlRow row =
                    (Epicor.Mfg.BO.TrackingDtlDataSet.TrackingDtlRow)ds.TrackingDtl.Rows[0];
                row.OrderNum = this.OrderNum;
                row.CaseNum = "";
                row.Company = "CA";
                row.PackNum = this.PackNum;
                row.TrackingNumber = trackNumber;
                Hashtable hashWeights = ship.GetWeights();
                row.Weight = (decimal)hashWeights[trackNumber];
                row.ShipmentType = "CUST";
                string message = "OK";

                try
                {
                    
                    trackDtlObj.Update(ds);
                }
                catch (Exception e)
                {
                    // tracking no did not post
                    message = e.Message;
                }
            }
        }
