using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace PartUpdate
{
    public enum catalogOld
    {
        UPC,
        description,
        ShortChar02,
        ShortChar03,
        ShortChar05,
        Number02,
        Number03,
        RunOut,
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
        style,
        unitPrice,
        listPrice,
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
        string file = "D:/users/rich/data/PartUpdates/Spring2_2011_Catalog.txt";
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
                xman.UpdateCatalog(line);
            }
        }
    }
}



