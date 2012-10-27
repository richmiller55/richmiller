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
        School,
        UPC,
        partDescr,
        adjQty,
        reasonCode,
        bin,
        filler
    }
    
    class UpdateTextReader
    {
        string file = "I:/data/updates/parts/CollegiateNonQCLooseStockToInventory26Oct12.txt";
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
                xman.CatalogPartUpdate(line);
            }
        }
    }
}




