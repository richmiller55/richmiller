using System;

/// <summary>
/// Summary description for Class1
/// </summary>
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
    public enum catalog
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
    public enum catalogIRC
    {
        UPC,
        style,
        ShortChar05,
        Number02,
        Number03,
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
    public enum makeManu
    {
        UPC,
        ShortChar08,
        filler
    }

    public enum catalogupdateprogram
    {
        UPC,
        ShortChar08,
        filler
    }
    public enum catalogUpdate
    {
        UPC,
        style,
        Character05,
        Character06,
        filler
    }
    public enum catalogNewNFLGood
    {
        UPC,
        style,
        UserChar1,
        ShortChar03,
        ShortChar04,
        subClass,
        purchType,
        type,
        ShortChar05,
        loc,
        Search,
        CasePack,
        backflush,
        Country,
        filler
    }
    /* mlb */
    public enum catalognot
    {
        style,
        UPC,
        Character04,
        ShortChar03,
        subClass,
        purchType,
        type,
        ShortChar05,
        loc,
        ShortChar08,
        Search,
        CasePack,
        backflush,
        Country,
        filler
    }
    public enum catalogno
    {
        
        UPC,
        style,
        UserChar1,
        subClass,
        purchType,
        type,
        ShortChar05,
        loc,
        search,
        CasePack,
        backflush,
        Country,
        filler
    }
    public enum catalognatgeo
    {
        style,
        UPC,
        ShortChar03,
        ShortChar04,
        Number01,
        Character01,
        UnitPrice,
        Number08,
        Character02,
        filler
    }
    public enum cataloggoodalso
    {
        style,
        UPC,
        Character04,
        ShortChar03,
        ShortChar04,
        Number01,
        ShortChar06,
        CheckBox02,
        CheckBox03,
        CheckBox04,
        CheckBox05,
        Number05,
        Number06,
        Number07,
        ShortChar07,
        UnitPrice,
        ListPrice,
        Character01,
        filler
    }
    public enum catalogupdateprice
    {
        UPC,
        style,
        UnitPrice,
        ListPrice,
    }

    public enum catalogpfw
    {
        style,
        UPC,
        UserChar1,
        CasePack,
        backflush,
        Country,
        subClass,
        purchType,
        type,
        ShortChar06,
        loc,
        ShortChar03,
        ShortChar04,
        UnitPrice,
        ListPrice,
        Character01,
        filler
    }
    public enum catalogKim
    {
        UPC,
        style,
        subClass,
        country,
        purchType,
        UnitPrice,
        WorkOrderCost,
        type,
        loc,
        casePack,
        search,
        UserChar1,
        NonStock,
        BackFlush,
        QtyBearing,
        PrimaryWarewhouse,
        PrimaryBin,
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
}