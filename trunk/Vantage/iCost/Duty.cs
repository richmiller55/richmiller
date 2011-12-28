using System;
using System.Collections;

namespace iCost
{
    public class Duty
    {
        Hashtable oldHt;
        Hashtable newHt;   
        PartInfo partInfo;

        public Duty(Hashtable ht)
        {
            oldHt = ht;
            newHt = new Hashtable(oldHt.Count);
            partInfo = new PartInfo();
            ApplyDuty();
        }
        public Hashtable NewHt
        {
            get { return newHt; }
            set { newHt = value; }
        }
        private void ApplyDuty()
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
                        decimal dutyRate = partInfo.GetDutyRate(part.ToString());
                        Style style = (Style)oldHt[part];
                        style.Duty = style.PO_Cost * dutyRate / 100;
                        newHt.Add(part, style);
                    }
                }
            }
        }
    }
}