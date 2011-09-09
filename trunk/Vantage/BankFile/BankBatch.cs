using System;

using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace LB1
{
    public class BankBatch
    {
        string batchNo;
        Int32 checkCount;
        decimal checkTotal;

        decimal readChecksTotal;
        Hashtable checks;
        int readNChecks;
        public BankBatch()
        {
            checks = new Hashtable();
            readChecksTotal = 0.0M;
            readNChecks = 0;
        }
        public void AddCheck(Check check)
        {
            try
            {
                checks.Add(check.CheckNo, check);
                readChecksTotal += check.Amount;
                readNChecks++;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Duplicate Check No");
                MessageBox.Show(ae.Message);
            }
        }
        public Hashtable getChecks()
        {
            return checks;
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
        public Int32 CheckCount
        {
            get
            {
                return checkCount;
            }
            set
            {
                checkCount = value;
            }
        }
        public decimal CheckTotal
        {
            get
            {
                return checkTotal;
            }
            set
            {
                checkTotal = value;
            }
        }
    }
}
