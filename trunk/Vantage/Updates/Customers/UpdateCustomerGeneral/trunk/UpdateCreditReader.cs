using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum col
    {
	    CustId,
	    filler
    }
    class UpdateCreditReader
    {
        string file = "D:/users/rich/data/customerUpdates/resetGlobalCreditFlag26Jan09.txt";
        StreamReader tr;
        public UpdateCreditReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        bool strToBool(string test)
        {
            bool result = true;
            int testResult = test.CompareTo("0");
            if (testResult == 0)
            {
                result = false;
            }
            return result;
        }
        void processFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)col.CustId];

                xman.setGlobalIncFlag(CustId);
            }
        }
    }
}



