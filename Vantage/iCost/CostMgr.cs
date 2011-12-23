using System;
using System.Collections;

namespace iCost
{
    public class CostMgr
    {
        Hashtable ht;
        public CostMgr()
        {
            ht = new Hashtable(5000);
            FillOnHand();
        }
        private void FillOnHand()
        {
            PartBin partBin = new PartBin(ht);
            POCost poCost = new POCost(ht);
            Bom bom = new Bom(ht);
            // CostTable table = new CostTable(ht);
        }
    }
}