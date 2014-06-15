using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace costUpdate
{
    public enum rowLayout
    {
        UPC,
        partDescription,
        cost,
        filler
    }
    class UpdateTextReader
    {
        string fName = "feedCostUpdate15Jun14.txt";
        string filedir = "I:/data/updates/costs/";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(filedir + fName);
            processFile();
        }
        void processFile()
        {
            string line = "";
            CostXman xman = new CostXman();
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string partNum = split[(int)rowLayout.UPC];

                CostDetail detail = new CostDetail(partNum);
                string sCost = split[(int)rowLayout.cost];
                bool allOK = true;
                try {
                    detail.Cost = System.Convert.ToDecimal(sCost);
                }
                catch (System.OverflowException){
                    allOK = false;
                    System.Console.WriteLine(
                       "The conversion from string to decimal overflowed.");
                }
                catch (System.FormatException)
                {
                    allOK = false;
                    System.Console.WriteLine(
                        "The string is not formatted as a decimal.");
                }
                catch (System.ArgumentNullException)
                {
                    allOK = false;
                    System.Console.WriteLine(
                        "The string is null.");
                }
                if (allOK)
                {
                    xman.UpdateCost(detail);
                }
            }
        }
    }
}




