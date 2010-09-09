using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace PartUpdate
{
    public enum catalog
    {
        UPC,
        ShortChar03,
        ShortChar04,
        Number01,
        CheckBox02,
        CheckBox03,
        CheckBox04,
        CheckBox05,
        Number05,
        Number06,
        Number07,
        ShortChar07,
        UnitPrice,
        Number08,
        Character01,
        Character02,
        filler
    }
    public enum listPrice
    {
        prodClass,
        UPC,
        partDescr,
        loc,
        currentList,
        unitPrice,
        filler
    }
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
        string file = "D:/users/rich/data/PartUpdates/ListPriceUpdate9Sep10.txt";
        // string file = "D:/users/rich/data/PartUpdates/FallHoliday2010.txt";
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
                xman.UpdateListPrice(line);
                // xman.UpdateCatalog(line);
            }
        }
    }
}



