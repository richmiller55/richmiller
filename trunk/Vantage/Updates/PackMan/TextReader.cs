using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;


namespace PackMan
{
    class TextReader
    {
        string fName = "packsToClose_18Aug2014.txt";
        string filedir = "I:/data/updates/packs/";
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




