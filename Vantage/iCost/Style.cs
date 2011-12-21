using System;
using System.Collections.Generic;
using System.Text;

namespace iCost
{
    class Style
    {                              // UT01 mapping
        string upc;                // key01  
        string styleDescr;         // ShortChar01
        decimal cost;              // Number01
        decimal po_cost;           // Number02
        decimal freight;           // Number03
        decimal burden;            // Number04
        decimal overhead;          // Number05
        // this is not thought out. is it.
        decimal onHandRemaining;
        decimal totalOnHandValue;
        decimal priorQtyOnHand;
        decimal newQtyOnHand;
        public Style(string upc)
        {
            Upc = upc;
            TotalOnHandValue = 0.0M;
            cost = 0.0M;
            po_cost = 0.0M;
            freight = 0M;
            burden = 0M;
            overhead = 0M;

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
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public decimal PO_Cost
        {
            get { return po_cost; }
            set { po_cost = value; }
        }
        public decimal TotalOnHandValue
        {
            get { return totalOnHandValue; }
            set { totalOnHandValue = value; }
        }
        public decimal Burden
        {
            get { return burden; }
            set { burden = value; }
        }
        public decimal Freight
        {
            get { return freight; }
            set { freight = value; }
        }
        public decimal Overhead
        {
            get { return overhead; }
            set { overhead = value; }
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
