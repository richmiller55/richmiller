using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class InvLine
    {
        int invoiceNo;
        int invoiceLineNo;
        int packID;
        int packLine;
        string part;
        string description;
        string unitOfMeasure;
        decimal sellingShipQty;
        decimal orderQty;
        decimal unitPrice;
        decimal discount;  // verify what this thing is a percent or amt
        decimal extPrice;

        decimal custMiscCharge;
        decimal taxAmount;
        decimal docLineTotal;
        string custName;
        System.DateTime invoiceDate;
        System.DateTime dueDate;
        string soldToCustID;
        string soldToCustName;
        string billToCustID;
        string billToCustName;
        string termsID;

        public InvLine(Epicor.Mfg.BO.ARInvoiceDataSet.InvcDtlRow row)
        {
            this.InvoiceNo = row.InvoiceNum;
            this.InvoiceLineNo = row.InvoiceLine;
            this.PackID = row.PackNum;  // check this mapping
            this.PackLine = row.PackLine;
            this.Part = row.PartNum;
            this.Description = row.PartNumPartDescription;
            this.UnitOfMeasure = row.OrderUM;
            this.SellingShipQty = row.SellingShipQty;
            this.OrderQty = row.OurOrderQty;
            this.UnitPrice = row.UnitPrice;
            this.Discount = row.Discount;
            this.ExtPrice = row.ExtPrice;	    
            this.BillToCustID = row.BillToCustID;
            this.BillToCustName = row.BTCustName;
            this.SoldToCustID = row.SoldToCustID;
            this.SoldToCustName = row.SoldToCustName;
            this.InvoiceDate = row.InvoiceDate;
            this.DueDate = row.DueDate;
        }
        public int InvoiceNo
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                invoiceNo = value;
            }
        }
        public int InvoiceLineNo
        {
            get
            {
                return invoiceLineNo;
            }
            set
            {
                invoiceLineNo = value;
            }
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
        public string UnitOfMeasure
        {
            get
            {
                return unitOfMeasure;
            }
            set
            {
                unitOfMeasure = value;
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
        public decimal OrderQty
        {
            get
            {
                return orderQty;
            }
            set
            {
                orderQty = value;
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
        public int PackLine
        {
            get
            {
                return packLine;
            }
            set
            {
                packLine = value;
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
        public decimal ExtPrice
        {
            get
            {
                return extPrice;
            }
            set
            {
                extPrice = value;
            }
        }
        public decimal MiscCharge
        {
            get
            {
                return MiscCharge;
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
        public System.DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;
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