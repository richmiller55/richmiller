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
    public enum catalog
    {
        Description,
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
        ShortChar06,
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
    public enum newPart
    {
        UPC,
        style,
        descr,
        type,
        subClass,
        purchType,
        unitPrice,
        country,
        loc,
        casePack,
        search,
        shortChar05,
        purchComments,
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
        string file = "I:/data/updates/parts/Fall22012StyleUpdate30Oct2012.txt";
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




