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
        cost,
        filler
    }
    class UpdateTextReader
    {
        string fName = "stdCostUpdate.txt";
        string filedir = "I:/data/updates/costs/";
        StreamReader tr;
        public UpdateTextReader()
        {
            this.tr = new StreamReader(filedir + fName);
            
        }
        void ProcessFile()
        {
            while ((line = this.tr.ReadLine()) != null)
            {

                AddStdCost(line);
                SetPartToStandardCost(line);
            }
        }
        void AddStdCost(string line)
        {
                    string[] split = line.Split(new Char[] { '\t' });
                string partNum = split[(int)rowLayout.UPC];
                string newStdCostStr = split[(int)rowLayout.cost];
            CostXman xman = new CostXman();

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




