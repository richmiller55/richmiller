using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace UpdateSalesTax
{
    public enum AS_basic
    {
        ZIP_CODE,
        STATE_ABBREV,
        COUNTY_NAME,
        CITY_NAME,
        STATE_SALES_TAX,
        STATE_USE_TAX,
        COUNTY_SALES_TAX,
        COUNTY_USE_TAX,
        CITY_SALES_TAX,
        CITY_USE_TAX,
        TOTAL_SALES_TAX,
        TOTAL_USE_TAX,
        TAX_SHIPPING_ALONE,
        TAX_SHIPPING_AND_HANDLING_TOGETHER
    }
    class SalesTaxReader
    {
        string file = @"I:\data\salesTax\AS_basic.txt";
        StreamReader tr;
        public SalesTaxReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        bool strToBool(string test)
        {
            bool result = true;
            int testResult = test.CompareTo("0");
            if (testResult == 0)
            {
                result = false;
            }
            return result;
        }
        void processFile()
        {
            string line = "";
            XMan xman = new XMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string zip = split[(int)AS_basic.ZIP_CODE];
                string newRate = split[(int)AS_basic.TOTAL_SALES_TAX];
    	        xman.setNewRate(zip,newRate);
            }
        }
    }
}



