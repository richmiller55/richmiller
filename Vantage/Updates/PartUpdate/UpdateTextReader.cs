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
        string fName = "MLB_Drawstring_Bag_part_update_06.25.14.txt";
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
                xman.SetAsDefaultWarehouse(line);
                // xman.MakeInactive(line);
            }
        }
    }
}




