using System;
using System.Collections.Generic;
using System.Text;

namespace iCost
{
    class Style
    {                              // UT01 mapping
        string upc;                // key01  
        string styleDescr;         // ShortChar01
        string upcParent;
        string diag;
        decimal cost;              // Number01
        decimal po_cost;           // Number02
        decimal freight;           // Number03
        decimal burden;            // Number04
        decimal overhead;          // Number05
        decimal duty;              // Number06
    
        decimal printExpense;
        // this is not thought out. is it.
        decimal onHandRemaining;
        decimal totalOnHandValue;
        decimal priorQtyOnHand;
        decimal newQtyOnHand;

        public Style(string upc)
        {
            Upc = upc;
            diag = "";
            TotalOnHandValue = 0.0M;
            cost = 0.0M;
            po_cost = 0.0M;
            freight = 0M;
            burden = 0M;
            overhead = 0M;
            printExpense = 0M;
        }
        public static Style operator +(Style s1, Style s2)
        {
            Style s3 = new Style(s1.Upc);
            s3.StyleDescr = s1.StyleDescr;
            s3.Cost = s1.Cost + s2.Cost;
            s3.PO_Cost = s1.PO_Cost + s2.PO_Cost;
            s3.Freight = s1.Freight + s2.Freight;
            s3.Burden = s1.Burden + s2.Burden;
            s3.Overhead = s1.Overhead + s2.Overhead;
            s3.Duty = s1.Duty + s2.Duty;
            s3.Diag = s1.Diag + s2.Diag;
            return s3;
        }
        public string Upc
        {
            get { return upc; }
            set { upc = value; }
        }
        public string StyleDescr
        {
            get { return styleDescr; }
            set { styleDescr = value; }
        }
        public string Diag
        {
            get { return diag; }
            set { diag = value; }
        }
        public string UpcParent
        {
            get { return upcParent; }
            set { upcParent = value; }
        }
        public decimal Cost
        {
            get { return decimal.Round(cost,4); }
            set { cost = value; }
        }
        public decimal PO_Cost
        {
            get { return decimal.Round(po_cost,4); }
            set { po_cost = value; }
        }
        public decimal TotalOnHandValue
        {
            get { return totalOnHandValue; }
            set { totalOnHandValue = value; }
        }
        public decimal Burden
        {
            get { return decimal.Round(burden,4); }
            set { burden = value; }
        }
        public decimal Freight
        {
            get { return decimal.Round(freight,4); }
            set { freight = value; }
        }
        public decimal Overhead
        {
            get { return decimal.Round(overhead,4); }
            set { overhead = value; }
        }
        public decimal Duty
        {
            get { return decimal.Round(duty,4); }
            set { duty = value; }
        }
        public decimal PrintExpense
        {
            get { return decimal.Round(printExpense,4); }
            set { printExpense = value; }
        }
        public decimal PriorQtyOnHand
        {
            get { return priorQtyOnHand; }
            set { priorQtyOnHand = value; }
        }
        public decimal OnHandRemaining
        {
            get { return onHandRemaining; }
            set { onHandRemaining = value; }
        }
        public decimal NewQtyOnHand
        {
            get { return newQtyOnHand; }
            set { newQtyOnHand = value; }
        }
    }
}
