using System;

namespace costUpdate
{
    public class CostDetail
    {
        string partNum;
        decimal poCost;
        // decimal freightCost;
        // decimal burden;
        // decimal customsCost;
        // decimal royalty;
        decimal cost;
        public CostDetail(string upc)
        {
            partNum = upc;
            poCost = 0m;
        }
        public string PartNum
        {
            get { return partNum; }
            set { partNum = value; }
        }
        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public decimal POCost
        {
            get { return poCost; }
            set { poCost = value; }
        }
    }
}