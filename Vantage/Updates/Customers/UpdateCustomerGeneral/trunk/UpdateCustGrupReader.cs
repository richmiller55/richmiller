using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum colCustGroup
    {
        CustId,
        newGroup,
        filler
    }

    class UpdateCustGrupReader
    {
        string file = "D:/users/rich/data/customerUpdates/kaiserGroups.txt";
        StreamReader tr;
        public UpdateCustGrupReader()
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
                string CustId = split[(int)colCustGroup.CustId];
                string newGrp = split[(int)colCustGroup.newGroup];
                xman.ChangeCustGrp(CustId, newGrp);
            }
        }
    }
}
