using System;
using System.IO;
using System.Data;

namespace LockBox
{
    public class BankFileReader
    {
        BankFile bankFile;
        DataTable table;
        DataRow currentRow;
        Check check;
        BankBatch batch;
        bool firstLine = true;
        public BankFileReader(string fileName, DataTable bft,BankFile bankFileIn)
        {
            this.bankFile = bankFileIn;
            this.check = new Check();
            batch = new BankBatch();
            table = bft;
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        DetermineLineType(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public void DetermineLineType(string input)
        {
            string firstChar = input.Substring(0, 1);
            switch (firstChar)
            {
                case "1":
                    InitBankFile(input);
                    break;
                case "4":
                    // create payment object
                    Read4Line(input);
                    CreatePayment(input);
                    break;
                case "6":
                    Read6Line(input);
                    FinishCheck(input);
                    break;
                case "7":
                    Read7Line(input);
                    FinishBatch(input);
                    break;
                default:
                    Console.WriteLine("invalid bank file input");
                    break;
            }
        }
        public void InitBankFile(string input)
        {
            bankFile.DateMM = input.Substring(1, 2);
            bankFile.DateDD = input.Substring(3, 2);
            bankFile.DateYYYY = input.Substring(5, 4);
        }
        public void FinishBatch(string input)
        {
            table.Rows.Add(currentRow);
            batch.BatchNo = input.Substring(1, 10);
            string strCheckCount = input.Substring(14, 3);
            batch.CheckCount = System.Convert.ToInt32(strCheckCount);
            string strCheckTotal = input.Substring(17, 9);
            batch.CheckTotal = System.Convert.ToDecimal(strCheckTotal);
            bankFile.AddBatch(batch);
            batch = new BankBatch();
        }
        public void FinishCheck(string input)
        {
            check.BatchNo = input.Substring(1, 10);
            check.TransNo = input.Substring(11, 4);
            string strAmount = input.Substring(15, 10);
            check.Amount = System.Convert.ToDecimal(strAmount) / 100;
            check.CheckNo = input.Substring(29, 6);
            check.RtNo = input.Substring(35, 9);
            check.AcctNo = input.Substring(45, 10);
            check.Remitter = input.Substring(54, 20);
            check.DateDD = input.Substring(74, 2);
            check.DateMM = input.Substring(76, 2);
            check.DateYYYY = input.Substring(78, 4);
            batch.AddCheck(check);
            check = new Check();
        }
        public void CreatePayment(string input)
        {
            Payment payment = new Payment();
            payment.BatchNo = input.Substring(1, 10);
            payment.TransNo = input.Substring(11, 4);
            payment.InvoiceNo = input.Substring(15, 13).Trim();
            payment.CustId = input.Substring(28, 13);
            string stubSegStr = input.Substring(51, 4);
            payment.StubSeqStr = stubSegStr;
            payment.StubSeq = System.Convert.ToInt32(stubSegStr);
            string strPaymentAmt = input.Substring(41,10);
            payment.PaymentAmt = System.Convert.ToDecimal(strPaymentAmt) / 100;
            check.AddPayment(payment);
        }
        public void Read4Line(string input)
        {
            if (!firstLine)
                table.Rows.Add(currentRow);
            firstLine = false;
            DataRow row = table.NewRow();
            row["TransNo"] = input.Substring(11, 4);
            Int32 invoiceNo = Convert.ToInt32( input.Substring(15, 13));
            row["InvoiceNo"] = input.Substring(15, 13).Trim();
            row["CustNo"] = input.Substring(28, 13);
            string strPaymentAmt = input.Substring(41,10);
            decimal payAmt = System.Convert.ToDecimal(strPaymentAmt) /100;
            row["PaymentAmt"] = payAmt;
            row["StubSeq"] = input.Substring(51, 4);
            if (invoiceNo > 0)
            {
                InvoiceLookup lookup = new InvoiceLookup(invoiceNo);
                InvoiceInfo info = lookup.getInfo();
                if (info.InvoiceFound)  // is this var set at this time?
                {
                    row["VanOpenAmt"] = info.InvoiceBal;
                    string matchResult = info.CheckMatch(payAmt);
                    row["MatchStatus"] = matchResult;
                }
            }
            bool customerExists = false;
            if (invoiceNo == 0)
            {
                string custId = row["CustNo"].ToString();
                custId.TrimEnd(' ');
                CustomerLookup cust = new CustomerLookup(custId);
                customerExists = cust.CustomerFound;
                if (customerExists)
                {
                    row["MatchStatus"] = "Customer Match";
                }
            }
            row["CheckNo"] = "";  // in case there is no type 6 line
            currentRow = row;
        }
        public void Read6Line(string input)
        {
            currentRow["CheckNo"] = input.Substring(29, 6);
            string RemittersName = input.Substring(54, 20);
	     // bankFile.MorePayment(input);
        }
        public void Read7Line(string input)
        {
            string batchNo = input.Substring(1, 10);
            string checkCountStr = input.Substring(14, 3);
            string checkTotal = input.Substring(17, 9);
        }
        public BankFile getBankFile()
        {
            return bankFile;
        }
    }
}