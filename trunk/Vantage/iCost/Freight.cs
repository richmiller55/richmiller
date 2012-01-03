using System;
using System.Collections;

namespace iCost
{
    public class Freight
    {
        Hashtable oldHt;
        Hashtable newHt;
        PartInfo partInfo;
        public Freight(Hashtable ht)
        {
            oldHt = ht;
            newHt = new Hashtable(oldHt.Count);
            partInfo = new PartInfo();
            ApplyFreight();
        }
        public Hashtable NewHt
        {
            get { return newHt; }
            set { newHt = value; }
        }
        private void ApplyFreight()
        {
            if (oldHt.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                ICollection onHand = oldHt.Keys;
                foreach (object part in onHand)
                {
                    if (partInfo.ContainsKey(part.ToString()))
                    {
                        decimal freight = partInfo.GetFreight(part.ToString());
                        Style style = (Style)oldHt[part];
                        style.Freight = freight * style.CasePack;
                        newHt.Add(part, style);
                    }
                }
            }
        }
    }
}