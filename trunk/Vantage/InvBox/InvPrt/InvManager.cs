using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
// using Microsoft.Data.Odbc;
using System.Data.Odbc;
using System.Text.RegularExpressions;


namespace InvPrt
{
    public class InvManager
    {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t','\r' };
        public InvManager(string invoiceList)
        {
            string cleanList = invoiceList.Replace("\n", "\t");
            string extraClean = cleanList.Replace("\r",""); 
            string[] invoices = extraClean.Split(this.delimiterChars);
            foreach (string invoiceStr in invoices)
            {
                int invoiceNum = Convert.ToInt32(invoiceStr);
                Invoice invoice = new Invoice(invoiceNum);
                InvPrintDocument invPrtDoc = new InvPrintDocument(invoice);
                // invPrtDoc.Print();
                invPrtDoc.Print();
            }
        }
    }
}