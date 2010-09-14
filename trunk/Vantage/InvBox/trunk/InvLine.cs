using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class InvLine
    {
        string part;
        string description;
        decimal sellingShipQty;
        decimal discount;
        decimal unitPrice;
        int packingLine;
        int packId;
        decimal custExtPrice;
        decimal custMiscCharge;
        decimal taxAmount;
        decimal docLineTotal;
        string custName;
        DateTime invoiceDate;
        string soldToCustID;
        string soldToCustName;
        string billToCustID;
        string billToCustName;
        string termsID;

        public void InvLine()
        {
            // ctor stuff	

        }
        public string Part
        {
            get
            {
                return part;
            }
            set
            {
                part = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public decimal SellingShipQty
        {
            get
            {
                return sellingShipQty;
            }
            set
            {
                sellingShipQty = value;
            }
        }
        public decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
            }
        }
        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                unitPrice = value;
            }
        }
        public int PackingLine
        {
            get
            {
                return packingLine;
            }
            set
            {
                packingLine = value;
            }
        }
        public int PackID
        {
            get
            {
                return packId;
            }
            set
            {
                packId = value;
            }
        }
        public decimal 
    }
}