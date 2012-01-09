using System;
using System.Collections.Generic;
using System.Text;

namespace iCost
{
    class VanPart
    {          
        string partNum;       
        string partDescription;
	    string loc;
	    string prodCode;
        string compRetail;
        decimal unitPrice;
        decimal dutyRate;
    	decimal burden;
        decimal casePack;
        public VanPart(string partNum)
        {
            this.partNum = partNum;
            unitPrice = 0M;
            casePack = 1M;
	        partDescription = "";
            compRetail = "";
	        prodCode = "";
        }
        public string PartNum
        {
            get { return partNum; }
            set { partNum = value; }
        }
        public string PartDescription
        {
            get { return partDescription; }
            set { partDescription = value; }
        }
        public string ProdCode
        {
            get { return prodCode; }
            set { prodCode = value; }
        }
        public string Loc
        {
            get { return loc; }
            set { loc = value; }
        }
        public string CompRetail
        {
            get { return compRetail; }
            set { compRetail = value; }
        }
        public decimal UnitPrice
        {
            get { return decimal.Round(unitPrice,2); }
            set { unitPrice = value; }
        }
        public decimal DutyRate
        {
            get { return decimal.Round(dutyRate,2); }
            set { dutyRate = value; }
        }
        public decimal Burden
        {
            get { return decimal.Round(burden,2); }
            set { burden = value; }
        }
        public decimal CasePack
        {
            get { return decimal.Round(casePack, 0); }
            set { casePack = value; }
        }

    }
}
