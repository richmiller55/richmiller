using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum eRepCode
    {
        CustId,
        RepCode,
        SalesRepName,
        filler
    }
    class UpdateRepOnInvoiceReader
    {
        string file = @"I:\data\updates\customers\results.txt";
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
                string CustId = split[(int)eRepCode.CustId];
                if (CustId.Equals("CustId"))  continue ;
                string RepCode = split[(int)eRepCode.RepCode];
                string SalesRepName = split[(int)eRepCode.SalesRepName];
                
                xman.ChangeRepOnInvoice(CustId, RepCode, SalesRepName);
            }
        }
    }
}



