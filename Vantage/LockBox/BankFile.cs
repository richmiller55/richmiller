using System;

using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace LB1
{
    public class BankFile
    {
        Int32 nBatches;
        string dateMM;
        string dateDD;
        decimal fileTotal;
        decimal fileTotalRead;
        string dateYYYY;

        Hashtable bankBatches;
        public BankFile()
        {
            bankBatches = new Hashtable();
            this.fileTotal = 0.0m;
            this.fileTotalRead = 0.0m;
            nBatches = 0;
            DateDD = "";
            DateMM = "";
            DateYYYY = "";

        }
        public void AddBatch(BankBatch batch)
        {
            try
            {
                string batchNo = batch.BatchNo;
                bankBatches.Add(batchNo, batch);
                nBatches++;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Duplicate Batch");
                MessageBox.Show(ae.Message);
            }
        }
        public Hashtable getBankBatches()
        {
            return bankBatches;
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
        public void AddToTotalRead(decimal amount)
        {
            this.fileTotalRead += amount;
        }
        public string getGroupID()
        {
            return "M" + DateYYYY.Substring(2,2) + DateMM.ToString() + DateDD.ToString();
        }
    }
}