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
            POCost poCost = new POCost(ht);
            ht = poCost.NewHt;
            Freight frt = new Freight(ht);
            ht = frt.NewHt;
            Duty duty = new Duty(ht);
            ht = duty.NewHt;
            Burden burden = new Burden(ht);
            ht = burden.NewHt;
            Bom bom = new Bom(ht);
            ht = bom.NewHt;
            CostTable table = new CostTable(ht);
        }
    }
}