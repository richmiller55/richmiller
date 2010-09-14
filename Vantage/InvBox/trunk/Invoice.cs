using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class Invoice
    {
        public Hashtable  lines;

	int invoiceNo;
        int packID;
	int salesOrder;
        System.DateTime invoiceDate;	

        string soldToCustID;
        string soldToCustName;
        string billToCustID;
        string billToCustName;
	string poNo;

        public Invoice(int pack)
        {
	}
        public int InvoiceNo
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                packID = value;
            }
        }
        public int PackID
        {
            get
            {
                return packID;
            }
            set
            {
                packID = value;
            }
        }
        public int SalesOrder
        {
            get
            {
                return salesOrder;
            }
            set
            {
                salesOrder = value;
            }
        }
        public string SoldToCustID
        {
            get
            {
                return soldToCustID;
            }
            set
            {
                soldToCustID = value;
            }
        }
        public string SoldToCustName
        {
            get
            {
                return soldToCustName;
            }
            set
            {
                soldToCustName = value;
            }
        }
        public string BillToCustID
        {
            get
            {
                return billToCustID;
            }
            set
            {
                billToCustID = value;
            }
        }
        public string BillToCustName
        {
            get
            {
                return billToCustName;
            }
            set
            {
                billToCustName = value;
            }
        }
        public string PoNo
        {
            get
            {
                return poNo;
            }
            set
            {
                poNo = value;
            }
        }
