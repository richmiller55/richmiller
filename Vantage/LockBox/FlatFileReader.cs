using System;
using System.IO;
using System.Data;

namespace LockBox
{
    public enum wmCheck
    {
        poNum,
        invoiceNum,
        dcNum,
        storeNum,
        division,
        microfilmNum,
        invoiceDate,
        invoiceAmt,
        paidDate,
        discount,
        amountPaid,
        filler
    }
    public class FlatFileReader
    {
        BankFile bankFile;
        DataTable table;
        Check check;
        BankBatch batch = new BankBatch();
        bool firstLine = true;
        decimal checkTotal;
        public FlatFileReader(string fileName, DataTable bft,BankFile bankFileIn)
        {
            batch.BatchNo = "1";
            batch.CheckCount = 1;
            this.checkTotal = System.Convert.ToDecimal( 31390.59);
            this.bankFile = bankFileIn;
            this.check = new Check();
            this.batch = new BankBatch();
            this.table = bft;
            this.InitBankFile();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        CreatePayment(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            FinishCheck();
            FinishBatch();
        }
        private string GetCustId()
        {
            return "90000";
        }
        public void CreatePayment(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string invoiceNum = split[(int)wmCheck.invoiceNum];
  
            Payment payment = new Payment();
            payment.BatchNo = this.batch.BatchNo;
            string strPaymentAmt = split[(int)wmCheck.amountPaid];
            payment.PaymentAmt = System.Convert.ToDecimal(strPaymentAmt);
            payment.InvoiceNo = split[(int)wmCheck.invoiceNum].Trim();
            payment.CustId = this.GetCustId();
            check.AddPayment(payment);

            DataRow row = table.NewRow();
            row["TransNo"] = split[(int)wmCheck.poNum];
            row["InvoiceNo"] = payment.InvoiceNo;
            row["CustNo"] = payment.CustId;
            row["PaymentAmt"] = payment.PaymentAmt;
            row["StubSeq"] = "null";
            Int32 invoiceNo = Convert.ToInt32(payment.InvoiceNo);
            if (invoiceNo > 0)
            {
                InvoiceLookup lookup = new InvoiceLookup(invoiceNo);
                InvoiceInfo info = lookup.getInfo();
                if (info.InvoiceFound)  // is this var set at this time?
                {
                    row["VanOpenAmt"] = info.InvoiceBal;
                    string matchResult = info.CheckMatch(payment.PaymentAmt);
                    row["MatchStatus"] = matchResult;
                }
            }
            table.Rows.Add(row); 
        }
        private string GetDateMM() { return "07"; }
        private string GetDateDD() { return "07"; }
        private string GetDateYYYY() { return "2011"; }
        public void InitBankFile()
        {
            bankFile.DateMM = GetDateMM();
            bankFile.DateDD = GetDateDD();
            bankFile.DateYYYY = GetDateYYYY();
        }
        public void FinishBatch()
        {
            batch.CheckTotal = System.Convert.ToDecimal(this.checkTotal);
            bankFile.AddBatch(batch);
        }
        public void FinishCheck()
        {
            check.BatchNo = this.batch.BatchNo;
            check.TransNo = "tranNo";
            check.Amount = System.Convert.ToDecimal(this.checkTotal);
            check.CheckNo = "5276243";
            check.RtNo = "RtNo";
            check.AcctNo = "AccountNo";
            check.Remitter = "WalMart";
            check.DateDD = GetDateDD();
            check.DateMM = GetDateMM();
            check.DateYYYY = GetDateYYYY();
            batch.AddCheck(check);
        }
        public BankFile getBankFile()
        {
            return bankFile;
        }
    }
}