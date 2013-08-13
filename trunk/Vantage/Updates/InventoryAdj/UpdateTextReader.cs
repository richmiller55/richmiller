using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;


namespace InventoryAdj
{
    public enum layout
    {
        UPC,
        partDescr,
        UPC_match,
        wh,
        bin,
        adjQty,
        qtyOnHand
    }
    
    class UpdateTextReader
    {
        string file = "I:/data/updates/parts/WM_CLG_FINALADJ_073013.txt";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            AdjXman xman = new AdjXman();
            while ((line = tr.ReadLine()) != null)
            {
                xman.InventoryAdjust(line);
            }
        }
    }
}




