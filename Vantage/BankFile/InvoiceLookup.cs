using System;

namespace LB1
{
    public class InvoiceInfo
    {
        Int32 invoiceNo;

        bool invoiceFound = false;
        bool openInvoice = false; 
        bool buyGroupMember = false;
        bool invoiceAmtMatch = false;
        bool custIdMatch = false;

        string custName;
        string custID;
        decimal invoiceBal;
        decimal invoiceAmt;
        DateTime invoiceDate;
        string invoiceSuffix;
        string invoiceType;
        string invoiceTypeDesc;
        string custBTName;
        string custBTId;
        Int32 orderNum;
        string payAmounts;
        decimal payDescAmt;
        string payDates;
        Int32 packSlipNum;
        string termsCode;
        string termsCodeDesc;
        string matchStatus = "No Match";
        public InvoiceInfo(Int32 invoiceNum)
        {
            this.invoiceNo = invoiceNum;
        }
        public void CheckCustomer()
        {
            CustomerLookup x = new CustomerLookup(this.CustID);
            this.CustIdMatch = x.CustomerFound;
            
        }
        public void DetermineMatchLevel(decimal payAmt)
        {
            this.CheckCustomer();
            if (payAmt == this.invoiceBal)
            {
                this.InvoiceAmtMatch = true;
            }
        }
        private void IsBuyGroupMember()
        { 
            // this can't be valid
            bool buyGroupPrefix = custID.StartsWith("6");
            int custIDLen = custID.Length;
            if (buyGroupPrefix && custIDLen == 4) BuyGroupMember = true;
        }
        public string CheckMatch(decimal payAmt)
        {
            this.CheckCustomer();

            if (payAmt == invoiceBal)
            {
                InvoiceAmtMatch = true;
                if (!custID.Equals("null"))
                {
                    if (BuyGroupMember == true)
                    {
                        matchStatus = "Matched BuyGroup";
                    }
                    else if (BuyGroupMember == false)
                    {
                        matchStatus = "Invoice Amt Match";
                    }
                }
            }
            else
            {
                InvoiceAmtMatch = false;
                if (payAmt < invoiceBal)
                {
                    matchStatus = "Invoice Short Pay";
                }
                else
                {
                    matchStatus = "Invoice Overpayment";
                }
            }
            return matchStatus;
        }
        public string CustBTName
        {
            get
            {
                return custBTName;
            }
            set
            {
                custBTName = value;
            }
        }
        public string CustBTId
        {
            get
            {
                return custBTId;
            }
            set
            {
                custBTId = value;
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
        public string CustID
        {
            get
            {
                return custID;
            }
            set
            {
                custID = value;
            }
        }
        public decimal InvoiceAmt
        {
            get
            {
                return invoiceAmt;
            }
            set
            {
                invoiceAmt = value;
            }
        }
        public decimal InvoiceBal
        {
            get
            {
                return invoiceBal;
            }
            set
            {
                invoiceBal = value;
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
        public string InvoiceSuffix
        {
            get
            {
                return invoiceSuffix;
            }
            set
            {
                invoiceSuffix = value;
            }
        }
        public string InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                invoiceType = value;
            }
        }
        public string InvoiceTypeDesc
        {
            get
            {
                return invoiceTypeDesc;
            }
            set
            {
                invoiceTypeDesc = value;
            }
        }
        public bool OpenInvoice
        {
            get
            {
                return openInvoice;
            }
            set
            {
                openInvoice = value;
            }
        }
        public bool CustIdMatch
        {
            get
            {
                return custIdMatch;
            }
            set
            {
                custIdMatch = value;
            }
        }
        public bool InvoiceAmtMatch
        {
            get
            {
                return invoiceAmtMatch;
            }
            set
            {
                invoiceAmtMatch = value;
            }
        }
        public bool InvoiceFound
        {
            get
            {
                return invoiceFound;
            }
            set
            {
                invoiceFound = value;
            }
        }
        public bool BuyGroupMember
        {
            get
            {
                return buyGroupMember;
            }
            set
            {
                buyGroupMember = value;
            }
        }
        public Int32 OrderNum
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
        public string PayAmounts
        {
            get
            {
                return payAmounts;
            }
            set
            {
                payAmounts = value;
            }
        }
        public string PayDates
        {
            get
            {
                return payDates;
            }
            set
            {
                payDates = value;
            }
        }
        public Int32 PackSlipNum
        {
            get
            {
                return packSlipNum;
            }
            set
            {
                packSlipNum = value;
            }
        }
        public string TermsCode
        {
            get
            {
                return termsCode;
            }
            set
            {
                termsCode = value;
            }
        }
        public string TermsCodeDesc
        {
            get
            {
                return termsCodeDesc;
            }
            set
            {
                termsCodeDesc = value;
            }
        }
    }
    public class CustomerLookup
    {
        Epicor.Mfg.BO.Customer custObj;
        Epicor.Mfg.BO.CustomerDataSet custDs;
        Epicor.Mfg.Core.Session objSess;
        string custId;
        bool customerFound = false;
        public CustomerLookup(string cust)
        {
            this.custId = cust;
            this.init();
        }
        void init()
        {
            this.objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.custObj = new Epicor.Mfg.BO.Customer(this.objSess.ConnectionPool);
            try
            {
                this.custDs = this.custObj.GetByCustID(this.custId);
                this.customerFound = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Customer Not on file:");
                Console.WriteLine(e.Message);
                customerFound = false;
            }
        }
        public bool CustomerFound
        {
            get
            {
                return customerFound;
            }
            set
            {
                customerFound = value;
            }
        }
    }
    public class InvoiceLookup
    {
        Epicor.Mfg.BO.ARInvSearchDataSet ds;
        Epicor.Mfg.BO.ARInvSearch arObj;
        Epicor.Mfg.Core.Session objSess;
        InvoiceInfo info;

        public InvoiceLookup(Int32 invoiceNum)
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            arObj = new Epicor.Mfg.BO.ARInvSearch(objSess.ConnectionPool);
            bool invoiceFound = true;
            try
            {
                ds = arObj.GetByID(invoiceNum);
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("invoice Not found:");
                Console.WriteLine(e.Message);
                invoiceFound = false;
            }
            info = new InvoiceInfo(invoiceNum);
            info.InvoiceFound = invoiceFound;
            if (invoiceFound)
            {
                Epicor.Mfg.BO.ARInvSearchDataSet.InvcHeadRow row =
                    (Epicor.Mfg.BO.ARInvSearchDataSet.InvcHeadRow)ds.InvcHead.Rows[0];

                info.CustBTName = row.CustomerBTName;
                string BTcustName = row.BTCustomerName; // is this any different than the above
                info.CustBTId = row.BTCustID;
                
                info.CustID = row.CustomerCustID;
                info.CustName = row.CustomerName;
                info.InvoiceAmt = row.InvoiceAmt;
                info.InvoiceBal = row.InvoiceBal;
                info.InvoiceDate = row.InvoiceDate;
                info.InvoiceSuffix = row.InvoiceSuffix;
                info.InvoiceType = row.InvoiceType;
                info.InvoiceTypeDesc = row.InvoiceTypeDesc;
                info.OpenInvoice = row.OpenInvoice;
                info.OrderNum = row.OrderNum;
                info.PackSlipNum = row.PackSlipNum;
                info.PayAmounts = row.PayAmounts;
                info.PayDates = row.PayDates;
                info.TermsCode = row.TermsCode;
                info.TermsCodeDesc = row.TermsCodeDescription;
            }
        }
        public InvoiceInfo getInfo()
        {
            return info;
        }
    }
}



