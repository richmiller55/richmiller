using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace LockBox
{
    public class Payment
    {
        string batchNo;
        string transNo;
        string invoiceNo;
        string custId;
        decimal paymentAmt;
        Int32 stubSeq;
        string stubSeqStr;
        public Payment()
        {
        }
        public string BatchNo
        {
            get 
            {
	            return batchNo;
            }
            set 
            {
                batchNo = value;
            }
        }
        public string TransNo
        {
            get 
            {
	            return transNo;
            }
            set 
            {
                transNo = value;
            }
        }
        public string InvoiceNo
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
        public string CustId
        {
            get
            {
                return custId;
            }
            set
            {
                custId = value;
            }
        }
        public decimal PaymentAmt
        {
            get 
            {
	            return paymentAmt;
            }
            set 
            {
                paymentAmt = value;
            }
        }
        public string StubSeqStr
        {
            get
            {
                return stubSeqStr;
            }
            set
            {
                stubSeqStr = value;
            }
        }
        public Int32 StubSeq
        {
            get 
            {
	            return stubSeq;
            }
            set 
            {
                stubSeq = value;
            }
        }
    }
    }