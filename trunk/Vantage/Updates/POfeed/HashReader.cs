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
    class UpdateTextReader
    {
        string file = "I:/data/updates/parts/TGarget_Upload.txt";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {

            PartUpdateXman xman = new PartUpdateXman();

            while ((line = tr.ReadLine()) != null)
            {
                xman.NewPartEx(line);
            }
        }
    }
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
            string file = "I:/data/updates/po/poUpdates_19Aug2012.txt";
            StreamReader tr = new StreamReader(file);
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] segs = line.Split(new Char[] { '\t' });
                Hashtable ht = new Hashtable(10);
                ht["POId"] = segs[(int)vooRec.POId];
                ht["PODate"] = segs[(int)vooRec.POdate];
                ht["TranType"] = segs[(int)vooRec.TranType];
                ht["TypeOfDate"] = segs[(int)vooRec.TypeOfDate];
                vooRecs.Add(ht);
            }
        }
    }
}