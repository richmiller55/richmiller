using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace POfeed
{
    class POReader
    {
        string file = "I:/data/updates/po/MarcatiUpload061212.txt";
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




