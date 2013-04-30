using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

using SharpCouch;
using LitJson;

namespace POfeed
{
    public enum vooRec
    {
        Count,
        TranType,
        POId,
        POdate,
        TypeOfDate
    };
    class HashReader
    {
        ArrayList vooRecs;
        public HashReader()
        {
            vooRecs = new ArrayList(250);

        }
        public void RunFile()
        {
            string file = "I:/data/updates/po/poUpdates_30Apr2013.txt";
            StreamReader tr = new StreamReader(file);
            string line = "";
            POXman xman = new POXman();
            while ((line = tr.ReadLine()) != null)
            {
                string[] segs = line.Split(new Char[] { '\t' });
                Hashtable ht = new Hashtable(5);
                ht["POId"] = segs[(int)vooRec.POId];
                ht["seq"] = segs[(int)vooRec.Count];
                ht["PODate"] = segs[(int)vooRec.POdate];
                ht["TransType"] = segs[(int)vooRec.TranType];
                ht["TypeOfDate"] = segs[(int)vooRec.TypeOfDate];
                Tran tran = new Tran(ht);
                xman.PODateUpdate(tran);
            }
        }
    }
}