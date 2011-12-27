using System;
using System.Collections;

namespace iCost
{
    public class Duty
    {
        Hashtable ht;
        PartInfo partInfo;

        public Duty(ref Hashtable ht)
        {
            this.ht = ht;
            partInfo = new PartInfo();
            ApplyDuty();
        }
        private void ApplyDuty()
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
                        decimal dutyRate = partInfo.GetDutyRate(part.ToString());
                        Style style = (Style)ht[part];
                        style.Duty = style.PO_Cost * dutyRate / 100;
                        ht[part] = style;
                        }
                    }
                }
            }
        }
}
