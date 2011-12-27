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
            UpdateCosts();
        }
        private void UpdateCosts()
        {
            PartBin partBin = new PartBin(ht);
            POCost poCost = new POCost(ht);
            Duty duty = new Duty(ref ht);
            Burden burden = new Burden(ref ht);
            Bom bom = new Bom(ht);
            // CostTable table = new CostTable(ht);
        }
    }
}