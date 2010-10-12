using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class TermsConditions
    {
        private string invoiceTerms = "";
        public TermsConditions()
        {
            invoiceTerms += "Terms and Conditions: A Late Payment Penalty of ";
            invoiceTerms += @"1-1/2% Per Month or 18% Per Annum Will be Charged ";
            invoiceTerms += @"on all Amounts Due Beyond 30 days from the date of ";
            invoiceTerms += @"invoice. Goods may not be returned without prior ";
            invoiceTerms += @"authorization. Authorization must be obtained ";
            invoiceTerms += @"within 30 days of receipt of product unless ";
            invoiceTerms += @"goods are covered by warranty. Claims for  ";
            invoiceTerms += @"freight damage must be made with the carrier ";
        }
        public string InvoiceTerms
        {
            get
            {
                return invoiceTerms;
            }
            set
            {
                invoiceTerms = value;
            }
        }
    }
}