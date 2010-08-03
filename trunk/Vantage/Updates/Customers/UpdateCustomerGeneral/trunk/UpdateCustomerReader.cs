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
	    terr,
	    salesRep,
	    changeT,
	    changeR,
	    changeLock,
	    filler
    }
    class UpdateCustomerReader
    {
     string file = "D:/users/rich/data/customerUpdates/updateCustomers15Nov08.txt";
        StreamReader tr;
        public UpdateCustomerReader()
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
                string terr   = split[(int)col.terr];
                string salesRep = split[(int)col.salesRep];
		        bool changeT = strToBool(split[(int)col.changeT]);
                bool changeR  = strToBool(split[(int)col.changeR]);
                bool changeLock  = strToBool(split[(int)col.changeLock]);

    	        xman.setTerrRep(CustId,terr,salesRep,
	                			changeT,changeR,changeLock);
            }
        }
    }
}



