using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class InvoiceFormater
    {
        public ArrayList lines = new ArrayList();
        Invoice i;


        public InvoiceFormater(Invoice inv)
        {
            this.i = inv;
        }
        void Header()
        {
            lines.Add("--------------------------------------------------------+");
            lines.Add("|                                                       |");
            lines.Add("  Invoice " + i.InvoiceNo.ToString());
        }
    }
}
