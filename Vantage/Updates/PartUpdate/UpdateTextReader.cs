using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace PartUpdate
{
    public enum priceUpdate
    {
        UPC,
        price,
        filler
    }
    public enum infoUpdate
    {
        UPC,
        aicDescr,
        flyerNickname,
        filler
    }
    public enum locUpdate
    {
	    UPC,
	    LOC,
        filler
    }
    public enum casePackUpdate
    {
        UPC,
        casePack,
        filler
    }
    public enum runOutUpdate
    {
        style,
        UPC,
        runOut,
        filler
    }
    public enum backflush
    {
        UPC,
        filler
    }
    public enum wosUpdate
    {
        style,
        UPC,
        orderType,
        minWos,
        maxWos,
        filler
    }
    public enum subClassPriceUpdate
    {
        UPC,
        subClass,
        direct,
        list,
        filler
    }
    public enum descrUpdate
    {
        UPC,
        style,
        name,
        descr,
        colors,
        filler
    }

    class UpdateTextReader
    {
        // string file = "D:/users/rich/data/PartUpdates/ReaderPriceUpdate102809.txt";
        // string file = "D:/users/rich/data/PartUpdates/PartsCode04May10.txt";
        // string file = "D:/users/rich/data/PartUpdates/updatePartWhsToHayward.txt";
        // string file = "D:/users/rich/data/PartUpdates/CasePriceUpdate27May10.txt";
        string file = "D:/users/rich/data/PartUpdates/FlagRunOuts071510.txt";
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
                //xman.SetAsDefaultWarehouse(line);
                //xman.SimpleUpdatePrice(line);
                xman.SimpleRunOutFlag(line);
            }
        }
    }
}



