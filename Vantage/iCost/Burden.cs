using System;
using System.Collections;

namespace iCost
{
    public class Burden
    {
        Hashtable oldHt;
        Hashtable newHt;
        PartInfo partInfo;

        public Burden(Hashtable ht)
        {
            oldHt = ht;
            newHt = new Hashtable(oldHt.Count);
            partInfo = new PartInfo();
            ApplyBurden();
        }
        public Hashtable NewHt
        {
            get { return newHt; }
            set { newHt = value; }
        }
        private void ApplyBurden()
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
                        decimal burden = partInfo.GetBurden(part.ToString());
                        decimal casePack = partInfo.GetCasePack(part.ToString());
                        Style style = (Style)oldHt[part];
                        style.Burden = burden * casePack;
                        newHt.Add(part, style);
                    }
                }
            }
        }
    }
}