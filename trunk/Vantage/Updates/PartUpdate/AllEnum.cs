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
    public enum catalog
    {
        style,
        UPC,
        ShortChar03,
        ShortChar04,
        CasePack,
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
        Character02,
        filler
    }


    public enum catalogPrudy
    {
        UPC,
        style,
        UserChar1,
        ShortChar03,
        ShortChar04,
        subClass,
        purchType,
        type,
        Country,
        ShortChar05,
        loc,
        search,
        CasePack,

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
        type,
        loc,
        casePack,
        search,
        UserChar1,
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