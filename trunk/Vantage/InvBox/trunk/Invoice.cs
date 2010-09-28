using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class Invoice
    {
        Epicor.Mfg.Core.Session session;
        private ArrayList lines = new ArrayList();

        int invoiceNo;
        int packID;
        int salesOrder;
        System.DateTime invoiceDate;
        System.DateTime orderDate;
        System.DateTime shipDate;
//         StreetAddress remitTo; implement
        StreetAddress soldTo;
        StreetAddress billTo;
//        StreetAddress shipTo;  implement ship to type of StreetAddress that looks up from 
//                               the shipTo file

        string soldToCustID;
        string soldToCustName;
        string soldToAddressList;
        bool soldToInvoiceAddress;  // what the hell is this for?
        string billToCustID;
        string billToCustName;
        bool billToInvoiceAddress;
        string poNo;
        string shipVia;

        string paymentTerms;
        string paymentTermsText;
        string salesRepCode1;
        string salesRepName1;
        bool orderFF;
        bool invoiced;

        bool packFound;
        bool orderFound;
        bool packNeedsTracking;
        bool memberBuyGroup;

        string newInvoices = string.Empty;

        public Invoice()
        {
            packFound = false;
            orderFF = false;
            packNeedsTracking = true;


            // init on your own for testing
        }

        public Invoice(Epicor.Mfg.Core.Session session,
                       Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow row)
        {
            this.session = session;
            this.InvoiceNo = row.InvoiceNum;
            this.PackID = row.PackSlipNum;
            this.SalesOrder = row.OrderNum;

            this.InvoiceDate = row.InvoiceDate;
            // todo ShipDate and OrderDate
            this.SoldToCustID = row.SoldToCustID;
            this.SoldToCustName = row.SoldToCustomerName;
            this.SoldToInvoiceAddress = row.SoldToInvoiceAddress;
            this.SoldToAddressList = row.SoldToAddressList;

            this.BillToCustID = row.BTCustID;
            this.BillToCustName = row.BTCustomerName;
            this.BillToInvoiceAddress = row.BillToInvoiceAddress;

            this.PoNo = row.PONum;
            this.PaymentTerms = row.TermsCode;
            this.PaymentTermsText = row.TermsCodeDescription;

            this.SalesRepCode1 = row.SalesRepCode1;
            this.SalesRepName1 = row.SalesRepName1;
            this.BillTo = new StreetAddress(this.session, AddrTypes.BillTo, this.BillToCustID);
            this.SoldTo = new StreetAddress(this.session, AddrTypes.SoldTo, this.SoldToCustID);
            this.GetOrderInfo();

        }
        void FillAddresses()
        {
        }

        public void AddLine(InvLine line)
        {
            this.lines.Add(line);
        }
        public ArrayList Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
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
        public DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
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
        public StreetAddress BillTo
        {
            get
            {
                return billTo;
            }
            set
            {
                billTo = value;
            }
        }
        public StreetAddress SoldTo
        {
            get
            {
                return soldTo;
            }
            set
            {
                soldTo = value;
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
        public string ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                shipVia = value;
            }
        }
        public string PaymentTerms
        {
            get
            {
                return paymentTerms;
            }
            set
            {
                paymentTerms = value;
            }
        }
        public string PaymentTermsText
        {
            get
            {
                return paymentTermsText;
            }
            set
            {
                paymentTermsText = value;
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
        public bool PackFound
        {
            get
            {
                return packFound;
            }
            set
            {
                packFound = value;
            }
        }
        public bool OrderFound
        {
            get
            {
                return orderFound;
            }
            set
            {
                orderFound = value;
            }
        }
        public bool MemberBuyGroup
        {
            get
            {
                return memberBuyGroup;
            }
            set
            {
                memberBuyGroup = value;
            }
        }
        void GetOrderInfo()
        {
            Epicor.Mfg.BO.SalesOrder salesOrderObj;
            salesOrderObj = new Epicor.Mfg.BO.SalesOrder(session.ConnectionPool);
            Epicor.Mfg.BO.SalesOrderDataSet soDs = new Epicor.Mfg.BO.SalesOrderDataSet();

            string message;
            try
            {
                soDs = salesOrderObj.GetByID(this.SalesOrder);
                Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow row = (Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow)soDs.OrderHed.Rows[0];
                this.OrderFF = row.CheckBox03;
                this.ShipVia = row.ShipViaCode;
                soDs.Dispose();
            }
            catch (Exception e)
            {
                message = e.Message;
                this.OrderFound = false;
            }
        }
        public bool NeedsTracking
        {
            get
            {
                return packNeedsTracking;
            }
            set
            {
                packNeedsTracking = value;
            }
        }
        public bool Invoiced
        {
            get
            {
                return invoiced;
            }
            set
            {
                invoiced = value;
            }
        }
        public bool OrderFF
        {
            get
            {
                return orderFF;
            }
            set
            {
                orderFF = value;
            }
        }
    }
}

