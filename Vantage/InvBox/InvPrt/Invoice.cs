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
        StreetAddress shipTo;

        string shipToId = "";

        Customer soldToCustomer;
        // freight related
        decimal freightCharge = 0.0M;
        decimal totalFreight = 0.0M;
        string trackingNo;
        string shipVia;

        string soldToCustID;
        int soldToCustNum;
        string soldToCustName;
        string soldToAddressList;
        string billToCustID;
        int billToCustNum;
        string billToCustName;
        bool billToInvoiceAddress;
        string poNum;

        string paymentTerms;
        string paymentTermsText;
        string salesRepCode1;
        string salesRepName1;
        string salesRepPhone;
        string salesRepList;

        bool orderFF = false;
        bool invoiced;
        bool packFound = false;
        bool orderFound;
        bool packNeedsTracking = false;
        bool memberBuyGroup = false;
        bool printShipToAddr = false;
        string newInvoices = string.Empty;
        string pilotDsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
        public Invoice(int invoiceNum)
        {
            this.InvoiceNo = invoiceNum;
            this.FillInvoiceHead();
            this.GetInvoiceLines();
            this.GetAddresses();
        }
        public void GetInvoiceLines()
        {
            InvLineReader reader = new InvLineReader(this.InvoiceNo);
            this.Lines = reader.Lines;
            this.ShipToId = reader.ShipToId;
        }
        public void GetAddresses()
        {
            string empty = "";
            this.BillTo = new StreetAddress(AddrTypes.BillTo, this.BillToCustNum, empty);
            this.SoldTo = new StreetAddress(AddrTypes.SoldTo, this.SoldToCustNum, empty);
            this.ShipTo = new StreetAddress(AddrTypes.ShipTo, this.SoldToCustNum, this.ShipToId);
        }
        public void FillInvoiceHead()
        {
            string query = this.GetSelectInvHead(this.InvoiceNo);
            OdbcConnection connection = new OdbcConnection(this.PilotDsn);
            OdbcCommand command = new OdbcCommand(query, connection);
            connection.Open();

            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.SalesOrder = Convert.ToInt32(reader["OrderNum"]);
                this.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
                //                this.CreditMemo = reader["CreditMemo"];
                this.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                this.PoNo = reader["PONum"].ToString();
                this.ShipVia = reader["ShipViaCode"].ToString();
                this.SalesRepList = reader["SalesRepList"].ToString();
                this.BillToCustNum = Convert.ToInt32(reader["BillToCustNum"]);
                this.SoldToCustNum = Convert.ToInt32(reader["SoldToCustNum"]);

            }
        }
        private string GetSelectInvHead(int invoiceNum)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("ih.InvoiceNum as InvoiceNum,");
            query.Append("ih.OrderNum as OrderNum,");
            query.Append("ih.InvoiceDate as InvoiceDate,");
            query.Append("ih.CreditMemo as CreditMemo,");
            query.Append("ih.InvoiceAmt as InvoiceAmt,");
            query.Append("oh.OrderDate as OrderDate,");
            query.Append("oh.PONum as PONum,");
            query.Append("oh.ShipViaCode as ShipViaCode,");
            query.Append("ih.SalesRepList as SalesRepList,");
            query.Append("ih.CustNum as BillToCustNum,");
            query.Append("ih.SoldToCustNum as SoldToCustNum,");
            query.Append("1 as filler ");

            query.Append(" FROM pub.InvcHead as ih");
            query.Append(" left join pub.OrderHed as oh");
            query.Append(" on oh.OrderNum = ih.OrderNum ");
            query.Append(" where ih.InvoiceNum = ");
            query.Append(invoiceNum.ToString());
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

            this.SoldToCustomer = new Customer(session, SoldToCustID);

            this.GetOrderInfo();
# endif
        void FillAddresses()
        {
        }
        void GetOrderInfo()
        {
# if DEBUG
                this.OrderFF = row.CheckBox03;
                this.ShipVia = row.ShipViaCode;
                this.OrderDate = row.OrderDate;
# endif
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
        public string PilotDsn
        {
            get
            {
                return pilotDsn;
            }
            set
            {
                pilotDsn = value;
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
        public StreetAddress ShipTo
        {
            get
            {
                return shipTo;
            }
            set
            {
                shipTo = value;
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
        public int BillToCustNum
        {
            get
            {
                return billToCustNum;
            }
            set
            {
                billToCustNum = value;
            }
        }
        public int SoldToCustNum
        {
            get
            {
                return soldToCustNum;
            }
            set
            {
                soldToCustNum = value;
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
                return poNum;
            }
            set
            {
                poNum = value;
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
        public string SalesRepList
        {
            get
            {
                return salesRepList;
            }
            set
            {
                salesRepList = value;
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
