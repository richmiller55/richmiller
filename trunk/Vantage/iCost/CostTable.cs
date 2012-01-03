using System;
using System.Collections;
// using System.Collections.Generic;
using System.Text;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
namespace iCost
{
    class CostTable
    {
        Epicor.Mfg.Core.Session objSess;
        Hashtable ht;
        public CostTable(Hashtable inHash)
        {
            ht = inHash;
            objSess = new Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8331", Session.LicenseType.Default);
            // DeleteTable();
            WriteTable();
            // WriteText();
        }
        public void DeleteSome()
        {
            UD01 ud01Obj = new UD01(objSess.ConnectionPool);
            ud01Obj.DeleteByID("", "", "", "", "");
            ud01Obj.DeleteByID("757026220485", "", "", "", "");
        }
        public void DeleteTable()
        {
            System.Collections.ICollection MyKeys;
            if (ht.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                MyKeys = ht.Keys;
                UD01 ud01Obj = new UD01(objSess.ConnectionPool);
                foreach (object Key in MyKeys)
                {
                    ud01Obj.DeleteByID(Key.ToString(),"","","","");
                }
            }
        }
        public void WriteTable()
        {
            System.Collections.ICollection MyKeys;
            int count = 0;
            if (ht.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                MyKeys = ht.Keys;
                UD01 ud01Obj = new UD01(objSess.ConnectionPool);
                foreach (object Key in MyKeys)
                {
                    count++;
                    // if (count < 137)
                    {
                        AddOrUpdateRecord(ud01Obj, Key.ToString(), count);
                    }
                }
            }
        }
        public void AddOrUpdateRecord(UD01 ud01Obj, string key,int count)
        {
            // UD01 ud01Obj = new UD01(objSess.ConnectionPool)
            // if (key == "757026220485") { return; }
            UD01DataSet UD01_ds; 
            UD01DataSet.UD01Row row;
            try
            {
                UD01_ds = ud01Obj.GetByID(key,"","","","");
            }
            catch (Exception e)
            {
                UD01_ds = new UD01DataSet();
                ud01Obj.GetaNewUD01(UD01_ds);
                // row = (UD01DataSet.UD01Row)UD01_ds.UD01.Rows[0];
            }
            row = (UD01DataSet.UD01Row)UD01_ds.UD01.Rows[0];
            Style style = (Style)ht[key];
            row.ShortChar01 = Chop(style.StyleDescr,50);
            row.Key1 = style.Upc;
            row.Number01 = style.Cost;
            row.Number02 = style.AveragePO_Cost;
            row.Number03 = style.Freight;
            row.Number04 = style.Duty;
            row.Number05 = style.Burden;
            row.Number06 = style.Overhead;
            row.Number07 = style.PrintExpense;
            row.Number08 = style.LastPO_Cost;
            row.Number09 = style.UnitPrice;
            row.Number10 = style.CasePack;
            row.Character01 = Chop(style.PoLog, 1000);
            row.Character02 = Chop(style.BomLog, 1000);
            row.Character03 = Chop(style.FreightLog, 1000);
            try
            {
                ud01Obj.Update(UD01_ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        private string Chop(string log, int length)
        {
            if (log.Length > length)
            {
                return log.Substring(0, length);
            }
            else { return log; }
        }
        public void WriteText()
        {
            System.Collections.ICollection MyKeys;
            int count = 0;
            if (ht.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                MyKeys = ht.Keys;
                foreach (object key in MyKeys)
                {
                    count++;
                    {
                        Style style = (Style)ht[key];
                        StringBuilder output = new StringBuilder();
                        output.Append(style.Upc + "\t");
                        output.Append(style.StyleDescr + "\t");
                        output.Append(style.Cost + "\t");                
                        output.Append(style.AveragePO_Cost + "\t");                
                        output.Append(style.Freight + "\t");                
                        output.Append(style.Duty + "\t");                
                        output.Append(style.Burden + "\t");                
                        output.Append(style.Overhead + "\t");                
                        output.Append(style.PoLog + "\t");
                        output.Append(style.BomLog + "\t");                
                        Console.WriteLine(output);
                    }
                }
            }

        }

    }
}