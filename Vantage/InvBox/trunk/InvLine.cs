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
        System.DateTime invoiceDate;
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
                return packID;
            }
            set
            {
                packID = value;
            }
        }
        public decimal CustExtPrice
        {
            get
            {
                return custExtPrice;
            }
            set
            {
                custExtPrice = value;
            }
        }
        public decimal CustMiscCharge
        {
            get
            {
                return custMiscCharge;
            }
            set
            {
                custMiscCharge = value;
            }
        }
        public decimal TaxAmount
        {
            get
            {
                return taxAmount;
            }
            set
            {
                taxAmount = value;
            }
        }
        public decimal DocLineTotal
        {
            get
            {
                return docLineTotal;
            }
            set
            {
                docLineTotal = value;
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
        public System.DateTime InvoiceDate
        {
            get
            {
                return invoiceDate;
            }
            set
            {
                invoiceDate = value;
            }
        }
        public string SoldToCustID
        {
            get
            {
                return soldToCustID;
            }
            set
            {
                soldToCustID = value;
            }
        }
        public string SoldToCustName
        {
            get
            {
                return soldToCustName;
            }
            set
            {
                soldToCustName = value;
            }
        }
        public string BillToCustID
        {
            get
            {
                return billToCustID;
            }
            set
            {
                billToCustID = value;
            }
        }
        public string BillToCustName
        {
            get
            {
                return billToCustName;
            }
            set
            {
                billToCustName = value;
            }
        }
        public string TermsID
        {
            get
            {
                return termsID;
            }
            set
            {
                termsID = value;
            }
        }

    }
}