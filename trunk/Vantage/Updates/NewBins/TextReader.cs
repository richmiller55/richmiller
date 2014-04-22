using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;


namespace NewBins
{
    class TextReader
    {
        string fName = "WHSE1BINS_ab_22Mar2014.txt";
        string filedir = "I:/data/updates/whseBins/";
        StreamReader tr;
        public TextReader()
        {
            tr = new StreamReader(filedir + fName);
            processFile();
        }
        void processFile()
        {
            string line = "";
            AddBins xman = new AddBins();
            while ((line = tr.ReadLine()) != null)
            {
                xman.AddNewBin_AB(line);
            }
        }
    }
}




