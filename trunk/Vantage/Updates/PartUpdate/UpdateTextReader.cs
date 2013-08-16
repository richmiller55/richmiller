using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace PartUpdate
{
    class UpdateTextReader
    {
        string file = "I:/data/updates/parts/NFL_New_Part_Upload_081413.txt";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            PartUpdateXman xman = new PartUpdateXman();
            while ((line = tr.ReadLine()) != null)
            {
                xman.PartAddUpdate(line);
                xman.SetAsDefaultWarehouse(line);
            }
        }
    }
}




