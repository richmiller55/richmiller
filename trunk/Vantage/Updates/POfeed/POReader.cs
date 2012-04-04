using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace POfeed
{
    public enum col
    {
      PONum,
      POLine,
      RevisedDate,
      filler
    }
    class POReader
    {
        string file = "I:/data/updates/po/PO_Duedate.txt";
        public POReader()
        {
            StreamReader tr = new StreamReader(file);
            processFile(tr);
        }
        void processFile(StreamReader tr)
        {
            string line = "";
            POXman xman = new POXman();
            while ((line = tr.ReadLine()) != null)
            {
                xman.PODateUpdate(line);
            }
        }
    }
}




