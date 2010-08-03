using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum colCustGrp
    {
    CustId,
    Name,
    newGrp,
    filler
    }
    class UpdateCustGrpReader
    {
        string file = "D:/users/rich/data/customerUpdates/vsUpdate.txt";
        StreamReader tr;
        public UpdateCustGrpReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)colCustGrp.CustId];
        		string NewGrp = split[(int)colCustGrp.newGrp];
                xman.setCustGrp(CustId, NewGrp);
            }
        }
    }
}



