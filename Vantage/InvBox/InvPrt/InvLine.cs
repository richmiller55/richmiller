# undef DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace InvPrt
{
    public class InvLine
    {
        int invoiceNum;
        int invoiceLineNum;
        int packID;
        int packLine;
        int orderNum;
        string part;
        string partDescription;
        string unitOfMeasure;
        decimal sellingShipQty;
        decimal orderQty;
        decimal openQty;
        decimal unitPrice;
        decimal listPrice;

        decimal discount;
        decimal discountPercent;
        decimal discountOnOrder;  // verify what this thing is a percent or amt
        decimal extPrice;
        decimal sellingFactor;
        string sellingFactorDirection;
        string taxExempt;
        string taxCatID;
        decimal miscCharge;
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
        string shipToId = "";

        public InvLine(OdbcDataReader reader)
        {
            this.InvoiceNum = Convert.ToInt32(reader["InvoiceNum"]);
            this.InvoiceLine = Convert.ToInt32(reader["InvoiceLine"]);
            this.SellingShipQty = Convert.ToDecimal(reader["SellingShipQty"]);
            this.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
            this.ExtPrice = Convert.ToDecimal(reader["ExtPrice"]);
            this.ShipToId = reader["ShipToNum"].ToString();
            this.Part = reader["PartNum"].ToString();
            this.PartDescription = reader["PartDescription"].ToString();
            this.Discount = Convert.ToDecimal(reader["Discount"]);
            this.DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]);
            this.SellingFactor = Convert.ToDecimal(reader["SellingFactor"]);
            this.SellingFactorDirection = reader["SellingFactorDirection"].ToString();
            this.TaxExempt = reader["TaxExempt"].ToString();
            this.TaxCatID = reader["TaxCatID"].ToString();
            this.MiscChrg = Convert.ToDecimal(reader["TotalMiscChrg"]);
            // Regarding ShipToId It's a string regardless of Num
            // to duplicate the Vantage behavoir I should check each line to see if they are
            // all the same and print that see below message
        }
        void CalcDueDate()
        {
            // of course there is more to do here  
            // should this be at invoice level
            this.DueDate = this.InvoiceDate.AddDays(30);
        }
        public int InvoiceLineNo
        {
            get
            {
                return invoiceLineNum;
            }
            set
            {
                invoiceLineNum = value;
            }
        }
        public int InvoiceNo
        {
            get
            {
                return invoiceNum;
            }
            set
            {
                invoiceNum = value;
            }
        }
        public int InvoiceLineNum
        {
            get
            {
                return invoiceLineNum;
            }
            set
            {
                invoiceLineNum = value;
            }
        }
        public int OrderNum
        {
            get
            {
                return orderNum;
            }
            set
            {
                orderNum = value;
            }
        }
        public int InvoiceNum
        {
            get
            {
                return invoiceNum;
            }
            set
            {
                invoiceNum = value;
            }
        }
        public int InvoiceLine
        {
            get
            {
                return invoiceLineNum;
            }
            set
            {
                invoiceLineNum = value;
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
        public string PartDescription
        {
            get
            {
                return partDescription;
            }
            set
            {
                partDescription = value;
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
        public decimal OpenQty
        {
            get
            {
                return openQty;
            }
            set
            {
                openQty = value;
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
        public decimal DiscountPercent
        {
            get
            {
                return discountPercent;
            }
            set
            {
                discountPercent = value;
            }
        }
        public decimal DiscountOnOrder
        {
            get
            {
                return discountOnOrder;
            }
            set
            {
                discountOnOrder = value;
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
        public decimal ListPrice
        {
            get
            {
                return listPrice;
            }
            set
            {
                listPrice = value;
            }
        }
        public decimal MiscCharge
        {
            get
            {
                return miscCharge;
            }
            set
            {
                miscCharge = value;
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
        public string TaxExempt
        {
            get
            {
                return taxExempt;
            }
            set
            {
                taxExempt = value;
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
        public string TaxCatID
        {
            get
            {
                return taxCatID;
            }
            set
            {
                taxCatID = value;
            }
        }
        public decimal MiscChrg
        {
            get
            {
                return miscCharge;
            }
            set
            {
                miscCharge = value;
            }
        }
        public string SellingFactorDirection
        {
            get
            {
                return sellingFactorDirection;
            }
            set
            {
                sellingFactorDirection = value;
            }
        }
        public decimal SellingFactor
        {
            get
            {
                return sellingFactor;
            }
            set
            {
                sellingFactor = value;
            }
        }
    }
}