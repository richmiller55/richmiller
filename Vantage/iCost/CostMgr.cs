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
            FullPart part = new FullPart();
            Hashtable ht = part.NewHt; 
            POCost poCost = new POCost(ref ht);
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