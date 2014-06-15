using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace costUpdate
{
    class UpdateTextReader
    {
        string fName = "Walmart_BTS_2014_Part_Upload.txt";
        string filedir = "I:/data/updates/parts/";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(filedir + fName);
            processFile();
        }
        void processFile()
        {
            string line = "";
            PartUpdateXman xman = new PartUpdateXman();
            while ((line = tr.ReadLine()) != null)
            {
                xman.PartAddUpdate(line);
                // xman.SetAsDefaultWarehouse(line);
            }
        }
    }
}




