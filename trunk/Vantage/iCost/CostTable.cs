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
            WriteTable();
        }
        public void WriteTable()
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

                foreach (object Key in MyKeys)
                {
                    AddOrUpdateRecord(Key.ToString());
                }
            }
        }
        public void AddOrUpdateRecord(string key)
        {
            UD01 ud01Obj = new UD01(objSess.ConnectionPool);
            UD01DataSet UD01_ds; 
            UD01DataSet.UD01Row row;
            try
            {
                UD01_ds = ud01Obj.GetByID(key,"","","","");
                row = (UD01DataSet.UD01Row)UD01_ds.UD01.Rows[0];
            }
            catch (Exception e)
            {
                UD01_ds = new UD01DataSet();
                ud01Obj.GetaNewUD01(UD01_ds);
                row = (UD01DataSet.UD01Row)UD01_ds.UD01.Rows[0];
            }
            Style style = (Style)ht[key];
            row.ShortChar01 = style.StyleDescr;
            row.Key1 = style.Upc;
            row.Number01 = style.Cost;
            row.Number02 = style.PO_Cost;
            row.Number03 = style.Freight;
            row.Number04 = style.Duty;
            row.Number05 = style.Burden;
            row.Number06 = style.Overhead;
            row.Number07 = style.PrintExpense;
            try
            {
                ud01Obj.Update(UD01_ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
}