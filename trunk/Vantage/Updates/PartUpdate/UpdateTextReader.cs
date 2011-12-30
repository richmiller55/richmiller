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
        descr,
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
        UnitPrice,
        Number08,
        ShortChar06,
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
        string file = "I:/data/updates/parts/partKim_Beals_30Dec2011.txt";
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
                xman.NewPartEx(line);
                // xman.SimpleUpdatePart(line);
                // xman.UpdateCatalog(line);
            }
        }
    }
}




