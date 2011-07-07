using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace LB1
{
    public class Check
    {
//        decimal Amount;
//        string RtNo;
//        string AcctNo;
//        string Remitter;

        int nPayments;
        decimal totalCheck;
        decimal totalMatch;
        decimal totalBuyGroup;
        decimal totalUnmatched;
        decimal totalPayments;
        Hashtable payments;
        string batchNo;
        string checkNo;
        string transNo;
        decimal amount;
        string rtNo;
        string acctNo;
        string remitter;
        string dateMM;
        string dateDD;
        string dateYYYY;
        DataTable bft;
        public Check()
        {
            payments = new Hashtable();
            nPayments = 0;
        }
        public void AddPayment(Payment payment)
        {
            try
            {
                payments.Add(nPayments++, payment);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Duplicate Key");
                MessageBox.Show(ae.Message);
            }
        }
        public Hashtable getPayments()
        {
            return payments;
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
        public string CheckNo
        {
            get
            {
                return checkNo;
            }
            set
            {
                checkNo = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public string RtNo
        {
            get
            {
                return rtNo;
            }
            set
            {
                rtNo = value;
            }
        }
        public string AcctNo
        {
            get
            {
                return acctNo;
            }
            set
            {
                acctNo = value;
            }
        }
        public string Remitter
        {
            get
            {
                return remitter;
            }
            set
            {
                remitter = value;
            }
        }
        public string DateMM
        {
            get
            {
                return dateMM;
            }
            set
            {
                dateMM = value;
            }
        }
        public string DateDD
        {
            get
            {
                return dateDD;
            }
            set
            {
                dateDD = value;
            }
        }
        public string DateYYYY
        {
            get
            {
                return dateYYYY;
            }
            set
            {
                dateYYYY = value;
            }
        }

    }
}