using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;


namespace PartUpdate
{
    
    public enum listPrice
    {
        UPC,
        currentList,
        goalPrice,
        filler
    }
    public enum listPriceOld
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
        style,
        UPC,
        unitPrice,
        listPrice,
        filler
    }
    public enum catalogOld
    {
        Description,
        UPC,
        ShortChar03,
        ShortChar04,
        Number01,
        UnitPrice,
        Number08,
        Character01,
        Character02
    }
    public enum runOutUpdate
    {
        style,
        UPC,
        runOut,
        filler
    }
    public enum locUpdate
    {
        style,
        UPC,
        newLoc,
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
    public enum descrUpdate
    {
        UPC,
        style,
        name,
        descr,
        colors,
        filler
    }
    public enum catalog
    {
        UPC,
        style,
        subClass,
        country,
        purchType,
        UnitPrice,
        type,
        loc,
        casePack,
        search,
        UserChar1,
        filler
    }
    public enum prudy
    {
        style,
        UPC,
        flyer,
        descr,
        shortChar04,
        type,
        subClass,
        country,
        shortChar05,
        loc,
        nonStockItem,
        search,
        casePack,
        unitPrice,
        Number08,
        Character01,
        Character02,
        filler
    }

    public enum newPartEx
    {
        UPC, 
        style,
        subClass,
        country,
        type,
        unitPrice,
        purchType,
        loc,
        casePack,
        search,
        filler
    }
    public enum printOption
    {
        UPC,
        printOption,
        filler
    }
    class UpdateTextReader
    {
        string file = "I:/data/updates/parts/partCreateKim_5Jan2013.txt";
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
                // xman.LocUpdate(line);
            }
        }
    }
}




