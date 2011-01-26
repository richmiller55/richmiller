# undef DEBUG
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
// using Microsoft.Data.Odbc;
using System.Data.Odbc;

namespace InvPrt
{
    public class Invoice
    {
        private ArrayList lines = new ArrayList();
        int invoiceNo;
        int packID;
        int salesOrder;
        System.DateTime invoiceDate;
        System.DateTime orderDate;
        System.DateTime shipDate;

        StreetAddress soldTo;
        StreetAddress billTo;
        string shipToId = "";
        ShipTo shipToAddress;
        Customer soldToCustomer;
        // freight related
        decimal freightCharge = 0.0M;
        decimal totalFreight = 0.0M;
        string trackingNo;
        string shipVia;

        string soldToCustID;
        string soldToCustName;
        string soldToAddressList;
        string billToCustID;
        string billToCustName;
        bool billToInvoiceAddress;
        string poNo;

        string paymentTerms;
        string paymentTermsText;
        string salesRepCode1;
        string salesRepName1;
        string salesRepPhone;

        bool orderFF = false;
        bool invoiced;
        bool packFound = false;
        bool orderFound;
        bool packNeedsTracking = false;
        bool memberBuyGroup = false;
        bool printShipToAddr = false;
        string newInvoices = string.Empty;

        public void FillInvoice()
        {
        }
        public Invoice(int invoiceNum)
        {
            this.InvoiceNo = invoiceNum;
	    string query = this.GetSelectInvHead(invoiceNum);
            OdbcConnection connection = new OdbcConnection("DSN=test");
            OdbcCommand command = new OdbcCommand(query,connection);
            connection.Open();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.OrderNo = reader["OrderNum"]; 
                this.InvoiceDate = reader["OrderNum"]; 
		this.InvoiceAmt = reader["InvoiceAmt"];
		this.OrderDate = reader["OrderDate"];
		this.PONum = reader["PONum"];
	    }
        }
	private string GetSelectInvHead(int invoiceNum)
	{
            StringBuilder query = " select ";
	    query += "ih.InvoiceNum as InvoiceNum,";	
	    query += "ih.OrderNum as OrderNum,";	
	    query += "ih.InvoiceDate as InvoiceDate,";	
	    query += "ih.CreditMemo as CreditMemo,";	
	    query += "ih.InvoiceAmt as InvoiceAmt,";	
	    query += "oh.OrderDate as OrderDate,";	
	    query += "oh.PONum as PONum,";	
	    query += "oh.ShipViaCode as ShipViaCode,";	
	    query += "1 as filler ";	

            query += " from InvcHead as ih";
            query += " left join OrderHed as oh" ;
            query += " on oh.OrderNum = ih.OrderNum " ;
	    query += "";
            query += " where ih.InvoiceNum = ";
            query += invoiceNum.ToString();
	    return query.ToString();
	}
# if DEBUG        
            this.InvoiceDate = row.InvoiceDate;
            // todo ShipDate and OrderDate
            this.SoldToCustID = row.SoldToCustID;
            this.SoldToCustName = row.SoldToCustomerName;

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
            this.SoldToCustomer = new Customer(session, SoldToCustID);

            this.GetOrderInfo();
# endif
        }
        public void FillShipTo()
        {
            ShipToAddress = new ShipTo(session, AddrTypes.ShipTo, this.SoldToCustID, this.ShipToId); 
        }
        void FillAddresses()
        {
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
                this.OrderDate = row.OrderDate;
                soDs.Dispose();
            }
            catch (Exception e)
            {
                message = e.Message;
                this.OrderFound = false;
            }
        }
        public void AddLine(InvLine line)
        {
            this.ShipToId = line.ShipToId;
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
        public Customer SoldToCustomer
        {
            get
            {
                return soldToCustomer;
            }
            set
            {
                soldToCustomer = value;
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
        public string TrackingNo
        {
            get
            {
                return trackingNo;
            }
            set
            {
                trackingNo = value;
            }
        }
        public string SalesRepPhone
        {
            get
            {
                return salesRepPhone;
            }
            set
            {
                salesRepPhone = value;
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
        public ShipTo ShipToAddress
        {
            get
            {
                return shipToAddress;
            }
            set
            {
                shipToAddress = value;
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
        public decimal FreightCharge
        {
            get
            {
                return freightCharge;
            }
            set
            {
                freightCharge = value;
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
        public bool PrintShipToAddr
        {
            get
            {
                return printShipToAddr;
            }
            set
            {
                printShipToAddr = value;
            }
        }
    }
}

