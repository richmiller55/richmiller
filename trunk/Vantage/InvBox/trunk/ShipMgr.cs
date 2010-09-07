using System;
// using System.Collections.ArrayList;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class ShipMgr
    {
        public Hashtable shipments; // keyed by packSlip
        decimal totalFreight;
        decimal totalWeight;
        int nTrackingNumbers;
        int nPacks;
        public ShipMgr()
        {
            this.totalFreight = 0;
            this.totalWeight = 0;
            this.nTrackingNumbers = 0;
            this.nPacks = 0;
            this.shipments = new Hashtable();
        }
        public void TotalShipments()
        {
            ICollection ShipKeys = shipments.Keys;
            foreach (object Key in ShipKeys)
            {
                Shipment ship = (Shipment)shipments[Key];
                ship.SumShipment();
                this.nPacks += 1;
            }
        }
        public void AddShipmentLine(int packSlip, 
                                    string trackingNo,
                                    System.DateTime shipDate,
                                    string classOfService,
                                    int orderNo,
                                    decimal weight,
                                    decimal charge)
        {
            Shipment ship = GetShipment(packSlip);
            ship.AddLine(trackingNo, shipDate,classOfService,orderNo,weight, charge);
            this.totalFreight += charge;
            this.totalWeight += weight;
            this.nTrackingNumbers += 1;
        }
        public void RemoveShipmentLine(int packSlip, string trackingNo)
        {
            Shipment ship = GetShipment(packSlip);
            this.totalFreight -= ship.GetTotalCharge();
            this.totalWeight -= ship.GetTotalWeight();
            this.nTrackingNumbers -= 1;
            ship.RemoveLine(trackingNo);
            shipments.Remove(packSlip);
        }
        public decimal TotalWeight
        {
            get
            {
                return totalWeight;
            }
            set
            {
                totalWeight = value;
            }
        }
        public decimal TotalFreight
        {
            get
            {
                return totalFreight;
            }
            set
            {
                totalFreight = value;
            }
        }
        public int TrackingNumbers
        {
            get
            {
                return nTrackingNumbers;
            }
            set
            {
                nTrackingNumbers = value;
            }
        }
        public int Packs
        {
            get
            {
                return nPacks;
            }
            set
            {
                nPacks = value;
            }
        }
        public Hashtable GetShipmentsHash()
        {
            return shipments;
        }
        private bool IsPackListInHash(int packSlip)
        {
            bool found = false;

            if (shipments.Count == 0)
            {
                found = false;
            }
            else
            {
                ICollection MyShips = shipments.Keys;
                foreach (object Key in MyShips)
                {
                    if ((int)Key == packSlip) {found = true; break;}
                }
            } 
            return found;
        }
        public bool IsPackListFedEx(int packSlip)
        {
            return IsPackListInHash(packSlip);
        }
        public Shipment GetShipment(int packSlip)
        {
            if (!IsPackListInHash(packSlip))
            {
                Shipment ship = new Shipment(packSlip);
                shipments.Add(packSlip, ship);
            }
            return (Shipment)shipments[packSlip];
        }
    }
}
	
		


		