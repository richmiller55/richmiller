using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class Shipment
    {
        public Hashtable  weights;
        public Hashtable  charges;
        public string shipDates;
        string trackNumbers;
    	decimal totalFrtCharge;
	    decimal totalWeight;
        int orderNo;
        string classOfService;
        System.DateTime shipDate;

	    // int totalBoxes;
	    int packSlipNo;
	    // int boxCount;
        
        public Shipment(int pack)
        {
	        packSlipNo = pack;
            weights = new Hashtable();
            charges = new Hashtable();
            shipDate = new System.DateTime();
		    // boxCount = 0;
	    }
	    public void AddLine(string trackNo,
                            System.DateTime shipDte,
                            string classOfService,
                            int orderNo,
                            decimal weight,decimal charge)
	    {
	        shipDate = shipDte;
            this.classOfService = classOfService;
            this.orderNo = orderNo;
            weights.Add(trackNo,weight);
	        charges.Add(trackNo,charge);
	    }
	    public void RemoveLine(string trackNo)
	    {
	        weights.Remove(trackNo);
	        charges.Remove(trackNo);
            shipDates = "";
	    }
	    public void SumShipment()
	    {
            ICollection chargeKeys = charges.Keys;
            foreach (object Key in chargeKeys)
            {
		        TotalFrtCharge += (decimal)charges[Key];
		        trackNumbers += (string)Key + ":";
            }
            ICollection weightKeys = weights.Keys;
            foreach (object Key in weightKeys)
            {
                TotalWeight += (decimal)weights[Key];
            }
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
        public decimal TotalFrtCharge
        {
            get
            {
                return totalFrtCharge;
            }
            set
            {
                totalFrtCharge = value;
            }
        }
        public Int32 PackSlipNo
        {
            get
            {
                return packSlipNo;
            }
            set
            {
                packSlipNo = value;
            }
        }
        public string TrackingNumbers
        {
            get
            {
                return trackNumbers;
            }
            set
            {
                trackNumbers = value;
            }
        }
        public DateTime ShipDate
        {
            get
            {
                return shipDate;
            }
            set
            {
                shipDate = value;
            }
        }
        public Int32 OrderNo
        {
            get
            {
                return orderNo;
            }
            set
            {
                orderNo = ValueType;
            }
        }
        public string ClassOfService
        {
            get
            {
                return classOfService;
            }
            set
            {
                classOfService = value;
            }
        }
        public Hashtable GetWeights() { return weights; }
        public Hashtable GetCharges() { return charges; }
    }
}