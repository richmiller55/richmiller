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
        string poLog;
        string bomLog;
        string freightLog;
        decimal cost;              // Number01
        decimal po_cost;           // Number02
        // decimal lastPO_Cost;
        decimal freight;           // Number03
        decimal burden;            // Number04
        decimal overhead;          // Number05
        decimal duty;              // Number06
        decimal unitPrice;         // Number09
        decimal casePack;          // Number10
        decimal printExpense;

        public Style(string upc)
        {
            Upc = upc;
            poLog = "";
            bomLog = "";
            freightLog = "";
            cost = 0.0M;
            po_cost = 0.0M;
            // lastPO_Cost = 0.0M;
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
            s3.AveragePO_Cost = s1.AveragePO_Cost + s2.AveragePO_Cost;
            s3.Freight = s1.Freight + s2.Freight;
            s3.Burden = s1.Burden + s2.Burden;
            s3.Overhead = s1.Overhead + s2.Overhead;
            s3.Duty = s1.Duty + s2.Duty;
            s3.BomLog = s1.BomLog + s2.BomLog;
            s3.PoLog = s1.PoLog + s2.PoLog;
           
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
        public string PoLog
        {
            get { return poLog; }
            set { poLog = value; }
        }
        public string BomLog
        {
            get { return bomLog; }
            set { bomLog = value; }
        }
        public string FreightLog
        {
            get { return freightLog; }
            set { freightLog = value; }
        }
        public string UpcParent
        {
            get { return upcParent; }
            set { upcParent = value; }
        }
        public decimal Cost
        {
            get { cost = AveragePO_Cost + Freight + Duty + Burden + Overhead;
                return decimal.Round(cost,4); }
            set { cost = value; }
        }
        public decimal AveragePO_Cost
        {
            get { return decimal.Round(po_cost,4); }
            set { po_cost = value; }
        }
        public decimal LastPO_Cost
        {
            get { return decimal.Round( po_cost, 4); }
            set { po_cost = value; }
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
        public decimal UnitPrice
        {
            get { return decimal.Round(unitPrice, 0); }
            set { unitPrice = value; }
        }
        public decimal CasePack
        {
            get { return decimal.Round(casePack, 0); }
            set { casePack = value; }
        }
        public decimal PrintExpense
        {
            get { return decimal.Round(printExpense,4); }
            set { printExpense = value; }
        }
    }
}
