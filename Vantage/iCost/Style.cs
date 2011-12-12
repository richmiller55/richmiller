using System;
using System.Collections.Generic;
using System.Text;

namespace iCost
{
    class Style
    {
        string upc;
        string styleDescr;
        decimal cost;
        decimal po_cost;
        decimal burden;
        decimal freight;
        decimal overhead;
        decimal priorQtyOnHand;
        decimal newQtyOnHand;
        public Style()
        {
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
        public decimal NewQtyOnHand
        {
            get { return newQtyOnHand; }
            set { newQtyOnHand = value; }
        }
    }
}
