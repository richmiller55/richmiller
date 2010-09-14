using System;
using System.Collections;
using System.Collections.Generic;

namespace InvBox
{
    public class Invoice
    {
        public Hashtable  lines;
        string shipDates;
        string trackNumbers;
    	decimal totalFrtCharge;
	    decimal totalWeight;
        int orderNo;
        string classOfService;
        System.DateTime shipDate;

	    // int totalBoxes;
	    int packSlipNo;
	    // int boxCount;
        
        public Shipment(int pack)
        {
