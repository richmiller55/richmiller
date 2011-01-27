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
        string description;
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
	string  sellingFactorDirection;
	string  taxExempt;
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

        public InvLine(int invoiceNum)
        {
            this.InvoiceNo = invoiceNum;
            string query = this.GetSelectInvDtl(invoiceNum);
            OdbcConnection connection = new OdbcConnection("DSN=test");
            OdbcCommand command = new OdbcCommand(query, connection);
            connection.Open();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.OrderNo = reader["OrderNum"];
                this.InvoiceNum = reader["InvoiceNum"];
                this.InvoiceLine = reader["InvoiceLine"];
                this.SellingShipQty = reader["SellingShipQty"];
                this.UnitPrice = reader["UnitPrice"];
                this.ExtPrice = reader["ExtPrice"];
                this.ShipToId = reader["ShipToNum"];
                this.PartNum = reader["PartNum"];
                this.PartDescription = reader["PartDescription"];
                this.Discount = reader["Discount"];
                this.DiscountPercent = reader["DiscountPercent"];
                this.SellingFactor = reader["SellingFactor"];
                this.SellingFactorDirection = reader["SellingFactorDirection"];
                this.TaxExempt = reader["TaxExempt"];
                this.TaxCatID = reader["TaxCatID"];
                this.TotalMiscChrg = reader["TotalMiscChrg"];
            }
        }

        private string GetSelectInvDtl(int invoiceNum)
        {
            StringBuilder query = new StringBuilder();
	    query.Append(" select ");
            query.Append("id.InvoiceNum as InvoiceNum,");
            query.Append("id.InvoiceLine as InvoiceLine,");
            query.Append("id.SellingShipQty as SellingShipQty,");
            query.Append("id.UnitPrice as UnitPrice,");
            query.Append("id.ExtPrice as ExtPrice,");
            query.Append("id.ShipToNum as ShipToNum,");
            query.Append("id.PartNum as PartNum,");
            query.Append("pt.PartDescription as PartDescription,");
            query.Append("id.Discount  as Discount,");
            query.Append("id.DiscountPercent as DiscountPercent,");
            query.Append("id.SellingFactor as SellingFactor,");
            query.Append("id.SellingFactorDirection as SellingFactorDirection,");
            query.Append("id.TaxExempt as TaxExempt,");
            query.Append("id.TaxCatID as TaxCatID,");
            query.Append("id.TotalMiscChrg as TotalMiscChrg,");
            query.Append("1 as filler ");

            query.Append(" from InvcDtl as id");
            query.Append(" left join Part as pt");
            query.Append(" on pt.PartNum = id.PartNum ");
            query.Append("");
            query.Append("");
            query.Append(" where id.InvoiceNum = ");
            query.Append(invoiceNum.ToString());
            return query.ToString();
        }
# if DEBUG
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
            this.DiscountOnOrder = row.Discount;
            this.ExtPrice = row.ExtPrice;	    
            this.BillToCustID = row.BillToCustID;
            this.BillToCustName = row.BTCustName;
            this.SoldToCustID = row.SoldToCustID;
            this.SoldToCustName = row.SoldToCustName;
            this.InvoiceDate = row.InvoiceDate;
            this.ShipToId = row.ShipToNum;
            this.CalcDueDate();
# endif

        void CalcDueDate()
        {
            // of course there is more to do here
            this.DueDate = this.InvoiceDate.AddDays(30);
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
    }
}