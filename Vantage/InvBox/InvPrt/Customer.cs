# undef DEBUG
using System;
using System.Collections;
using System.Collections.Generic;

namespace InvPrt
{
    public class Customer
    {
        protected string m_custId;
        protected string m_taxExempt;
        protected string m_taxAuthorityCode;
        protected string m_taxAuthorityDescr;
        protected string m_taxRegionCode;
        protected string m_taxRegionDescr;
        protected string m_termsCode;
        protected string m_termsDescr;
        protected bool m_customerFound = false;
        protected bool m_custFF = false;

        public Customer(string custId)
        {
            CustId = custId;
            TaxExempt = "";
            LookupCustomer();
        }
        void LookupCustomer()
        {
        }
# if DEBUG
                this.TaxAuthorityCode = row.TATaxAuthorityDescription;
                this.TaxAuthorityDescr = row.TaxAuthorityCode;
                this.TaxExempt = row.TaxExempt;
                this.TaxRegionCode = row.TaxRegionCode;
                this.TaxRegionDescr = row.TaxRegionDescription;
                this.TermsCode = row.TermsCode;
                this.TermsDescr = row.TermsDescription;
                string freightTerms = row.ShortChar01;
                if (freightTerms.CompareTo("FF") == 0)
                {
                    this.CustFF = true;
                }
                custDs.Dispose();
            }
            catch (Exception e)
            {
                message = e.Message;
                this.m_customerFound = false;
            }
        }
# endif
        public string TaxAuthorityCode
        {
            get
            {
                return m_taxAuthorityCode;
            }
            set
            {
                m_taxAuthorityCode = value;
            }
        }
        public string TaxAuthorityDescr
        {
            get
            {
                return m_taxAuthorityDescr;
            }
            set
            {
                m_taxAuthorityDescr = value;
            }
        }
        public string TaxExempt
        {
            get
            {
                return m_taxExempt;
            }
            set
            {
                m_taxExempt = value;
            }
        }
        public string TaxRegionCode
        {
            get
            {
                return m_taxRegionCode;
            }
            set
            {
                m_taxRegionCode = value;
            }
        }
        public string TaxRegionDescr
        {
            get
            {
                return m_taxRegionDescr;
            }
            set
            {
                m_taxRegionDescr = value;
            }
        }
        public string TermsCode
        {
            get
            {
                return m_termsCode;
            }
            set
            {
                m_termsCode = value;
            }
        }
        public string TermsDescr
        {
            get
            {
                return m_termsDescr;
            }
            set
            {
                m_termsDescr = value;
            }
        }
        public string CustId
        {
            get
            {
                return m_custId;
            }
            set
            {
                m_custId = value;
            }
        }
        public bool CustomerFound
        {
            get
            {
                return m_customerFound;
            }
            set
            {
                m_customerFound = value;
            }
        }
        public bool CustFF
        {
            get
            {
                return m_custFF;
            }
            set
            {
                m_custFF = value;
            }
        }
    }
}
