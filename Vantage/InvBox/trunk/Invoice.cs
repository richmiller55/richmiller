using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class Invoice
    {
        public ArrayList lines;

        int invoiceNo;
        int packID;
        int salesOrder;
        System.DateTime invoiceDate;

        string soldToCustID;
        string soldToCustName;
        string soldToAddressList;
        bool   soldToInvoiceAddress;
        string billToCustID;
        string billToCustName;
        bool   billToInvoiceAddress;
        string poNo;
        string salesRepCode1;
        string salesRepName1;

        public Invoice(Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow row)
        {
            lines = new ArrayList();
            this.InvoiceNo = row.InvoiceNum;
            this.PackID = row.PackSlipNum;
            this.SalesOrder = row.OrderNum;
            this.InvoiceDate = row.InvoiceDate;
            this.SoldToCustID = row.SoldToCustID;
            this.SoldToCustName = row.SoldToCustomerName;
            this.SoldToInvoiceAddress = row.SoldToInvoiceAddress;
            this.SoldToAddressList = row.SoldToAddressList;
            
            this.BillToCustID = row.BTCustID;
            this.BillToCustName = row.BTCustomerName;
            this.BillToInvoiceAddress = row.BillToInvoiceAddress;

            this.PoNo = row.PONum;

            this.SalesRepCode1 = row.SalesRepCode1;
            this.SalesRepName1 = row.SalesRepName1;
        }
        public void AddLine(InvLine line)
        {
            this.lines.Add(line);
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
        public DateTime InvoiceDate
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
        public int SalesOrder
        {
            get
            {
                return salesOrder;
            }
            set
            {
                salesOrder = value;
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
        public string SoldToAddressList
        {

            get
            {
                return soldToAddressList;
            }
            set
            {
                soldToAddressList = value;
            }
        }
        public bool SoldToInvoiceAddress
        {

            get
            {
                return soldToInvoiceAddress;
            }
            set
            {
                soldToInvoiceAddress = value;
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
        public bool BillToInvoiceAddress
        {

            get
            {
                return billToInvoiceAddress;
            }
            set
            {
                billToInvoiceAddress = value;
            }
        }
        public string PoNo
        {
            get
            {
                return poNo;
            }
            set
            {
                poNo = value;
            }
        }
        public string SalesRepCode1
        {
            get
            {
                return salesRepCode1;
            }
            set
            {
                salesRepCode1 = value;
            }
        }
        public string SalesRepName1
        {
            get
            {
                return salesRepName1;
            }
            set
            {
                salesRepName1 = value;
            }
        }
    }
}
