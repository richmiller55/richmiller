using System;
using System.Collections;

namespace iCost
{
    public class Freight
    {
        Hashtable oldHt;
        Hashtable newHt;
        PartInfo partInfo;
        FrtContainer frt;
        public Freight(Hashtable ht)
        {
            oldHt = ht;
            newHt = new Hashtable(oldHt.Count);
            partInfo = new PartInfo();
            frt = new FrtContainer();
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
                    // container freight is best
                    decimal freight = frt.GetFrtCost(part.ToString());
                    string frtLog = "";
                    if (!freight.Equals(0M))
                    {
                        frtLog = frt.GetFrtLogEntry(part.ToString());
                    }
                    else
                    {
                        // default freight if no container
                        if (partInfo.ContainsKey(part.ToString()))
                        {
                            freight = partInfo.GetFreight(part.ToString());
                        }
                    }
                    Style style = (Style)oldHt[part];
                    style.Freight = freight;
                    style.FreightLog = frtLog;
                    newHt.Add(part, style);
                }
            }
        }
    }
}