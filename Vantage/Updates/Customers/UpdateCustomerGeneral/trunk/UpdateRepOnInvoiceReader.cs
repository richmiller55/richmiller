using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{

    public enum colRepCodeUp
    {
        territory,
        CustId,
        SalesRepName,
        RepCode,
        filler
    }
    public enum colCustGrp
    {
    CustId,
    RepCode,
    RepName,
    filler
    }
    class UpdateRepOnInvoiceReader
    {
        string file = "D:/users/rich/data/customerUpdates/convert109SalesRepCode.txt";
        StreamReader tr;
        public UpdateRepOnInvoiceReader()
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
                string CustId = split[(int)colRepCodeUp.CustId];
                string RepCode = split[(int)colRepCodeUp.RepCode];
                string SalesRepName = split[(int)colRepCodeUp.SalesRepName];
                
                xman.ChangeRepOnInvoice(CustId, RepCode, SalesRepName);
            }
        }
    }
}



