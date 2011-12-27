using System;
using System.Collections;

namespace iCost
{
    public class Burden
    {
        Hashtable ht;
        PartInfo partInfo;

        public Burden(ref Hashtable ht)
        {
            this.ht = ht;
            partInfo = new PartInfo();
            ApplyBurden();
        }
        private void ApplyBurden()
        {
            if (ht.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                ICollection onHand = ht.Keys;
                foreach (object part in onHand) 
                {
                    if (partInfo.ContainsKey(part.ToString()))
                    {
                        decimal burden = partInfo.GetBurden(part.ToString());
                        Style style = (Style)ht[part];
                        style.Burden = burden;
                        ht[part] = style;
                        }
                    }
                }
            }
        }
}
