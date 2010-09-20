using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class InvoiceFormater
    {
        public ArrayList ra = new ArrayList();
        Invoice i;


        public InvoiceFormater(Invoice inv)
        {
            this.i = inv;
        }
        void Header()
        {
            ra.Add("--------------------------------------------------------+");
            ra.Add("|                                                       |");
            ra.Add("  Invoice " + i.InvoiceNo.ToString());
            ra.Add("California Accessories                        Phone   510.352.4774");
            ra.Add("Invoice Date:  " + i.InvoiceDate.ToShortDateString());
            ra.Add("PO Number " + i.PoNo.ToString());
        }
        void DetailHeading()
        {
            ra.Add(" Line   Quanitiy  Style / Description    Unit Price   Ext Price " );
        }
        void Detail()
        {
            
        }

        public ArrayList ReportArray
        {
            get
            {
                return ra;
            }
            set
            {
                ra = value;
            }
        }
    }
}
