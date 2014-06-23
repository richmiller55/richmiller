using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace costUpdate
{
    public enum rowLayout
    {
        UPC,
        cost,
        filler
    }
    class UpdateTextReader
    {
        string fName = "stdCostUpdate.txt";
        string filedir = "I:/data/updates/costs/";
        StreamReader tr;
        public UpdateTextReader()
        {
            this.tr = new StreamReader(filedir + fName);
            ProcessFile();

        }
        void ProcessFile()
        {
            String line;
            CostXman xman = new CostXman();
            while ((line = this.tr.ReadLine()) != null)
            {
                // xman.updateStdCost(line);
                xman.updateCostMethod(line);
            }
        }
    }
}


