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
    public enum price
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
        style
    }
    public enum catalogpucomment
    {
        UPC,
        style,
        PurComment,
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
    public enum catalogNewLastNFL
    {
        UPC,
        style,
        UserChar1,
        ShortChar03,
        ShortChar04,
        subClass,
        purchType,
        ShortChar05,
        loc,
        Search,
        CasePack,
        backflush,
        Country,
        ShortChar06,
        UnitPrice,
        ListPrice,
        Character01,
        filler
    }
    public enum catalogquick
    {
        UPC,
        loc,
        filler
    }
    public enum catalog
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
    public enum catalogGood
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
        search,
        CasePack,
        BackFlush,
        Country,
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