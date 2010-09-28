using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class InvoiceFormater
    {
        ArrayList ra = new ArrayList();
        Invoice i;


        public InvoiceFormater(Invoice inv)
        {
            this.i = inv;
            this.Header();
            this.DetailHeading();
            this.Detail();
        }
        void Header()
        {
            ra.Add("California Accessories                        Phone   510.352.4774");
            ra.Add("Invoice    " + i.InvoiceNo.ToString());
            ra.Add("Invoice Date:  " + i.InvoiceDate.ToShortDateString());
            ra.Add("PO Number " + i.PoNo.ToString());
        }
        void DetailHeading()
        {
            ra.Add(" Line   Qty      Style    Description                Unit Price      Ext Price " );
        }
        void Detail()
        {
            foreach (InvLine l in i.Lines)
            {
                string strOut = l.InvoiceLineNo.ToString() + "     ";
                strOut += l.SellingShipQty.ToString() + "       ";
                strOut += l.Part + "         ";
                strOut += l.Description + "      ";
                strOut += l.UnitPrice + "    ";
                strOut += l.UnitOfMeasure + "    ";
                strOut += l.Discount.ToString() + "   ";
                strOut += l.ExtPrice.ToString();
                ra.Add(strOut);
            }
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
