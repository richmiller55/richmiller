using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace UpdateSalesTax
{
    public enum col
    {
	    descr,
	    id,
	    oldRate,
	    newRate,
	    newRateX100,
	    filler
    }
    class UpdateSalesTaxReader
    {
     string file = "D:/users/rich/data/salesTax/SalesTaxVantageUpdate_6Jul09.txt";
        StreamReader tr;
        public UpdateSalesTaxReader()
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
            XMan xman = new XMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string zip = split[(int)col.id];
                string newRate = split[(int)col.newRateX100];
    	        xman.setNewRate(zip,newRate);
            }
        }
    }
}



