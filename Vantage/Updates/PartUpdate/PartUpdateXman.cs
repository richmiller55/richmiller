using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PartUpdate
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>

    public class PartUpdateXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.Part partObj;
        Epicor.Mfg.BO.PartDataSet ds;
        Epicor.Mfg.BO.PartDataSet.PartRow row;
        // Epicor.Mfg.BO.PartDataSet.PartPlantRow plantRow;
        Epicor.Mfg.BO.PartDataSet.PartWhseRow whseRow;
        //   pilot 8331
        //   sys 8301
        public PartUpdateXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.partObj = new Epicor.Mfg.BO.Part(objSess.ConnectionPool);
        }
        public void UpdateListPrice(string line)
        {
            // setup the list price in the first place
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)listPriceOld.UPC];
            if (partNum.CompareTo("UPC") == 0) return;
            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                if (row.IsISOrigCountryNumNull() || row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                string strUnitPrice = split[(int)listPriceOld.unitPrice];
                Decimal UnitPrice = 0.0M;
                UnitPrice = Convert.ToDecimal(strUnitPrice);

                string prodClass = row.ProdCode;
                Regex re = new Regex(@"1[A-Z]");
                MatchCollection mc = re.Matches(prodClass);
                Decimal ListPrice = 0.0M;

                if (mc.Count > 0)
                {
                    ListPrice = UnitPrice / .70M;
                }
                else
                {
                    ListPrice = UnitPrice / .80M;
                }
                Decimal rndListPrice = Math.Round(ListPrice, 2);
                row.Number08 = rndListPrice;
                try
                {
                    this.partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        public void LocUpdate(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)locUpdate.UPC];
            if (partNum.Equals("UPC")) return;
            if (partNum.Equals("Part Number")) return;
            if (partNum.Equals("UPC Number")) return;

            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                if (row.IsISOrigCountryNumNull() || row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }

                row.ShortChar02 = split[(int)locUpdate.newLoc];
                try
                {
                    this.partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        public void MakeManufactured(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)makeManu.UPC];

            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.TypeCode = "M";
                try
                {
                    this.partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        public void MakeInactive(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)catalog.UPC];
            Epicor.Mfg.BO.PartDataSet l_ds = new Epicor.Mfg.BO.PartDataSet();
            if (!this.partObj.PartExists(partNum))
            {
                // do nothing
               
            }
            else
            {
                l_ds = partObj.GetByID(partNum);
            }
            Epicor.Mfg.BO.PartDataSet.PartRow l_row;
            l_row = (Epicor.Mfg.BO.PartDataSet.PartRow)l_ds.Part.Rows[0];
            l_row.PartNum = partNum;
            l_row.InActive = true;
            try
            {
                this.partObj.Update(l_ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }

        }
        public void PartAddUpdate(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)catalog.UPC];

            if (partNum == "Part Number")
            {
                return;
            }
            if (String.Equals( partNum, "PartNum",StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (String.Equals(partNum, "PartNum", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (partNum == "UPC Number")
            {
                return;
            }
            if (partNum == "UPC") { return; }
            Epicor.Mfg.BO.PartDataSet l_ds = new Epicor.Mfg.BO.PartDataSet();
            if (!this.partObj.PartExists(partNum))
            {
                this.partObj.GetNewPart(l_ds);
            }
            else
            {
                l_ds = partObj.GetByID(partNum);
            }
            Epicor.Mfg.BO.PartDataSet.PartRow l_row;
            l_row = (Epicor.Mfg.BO.PartDataSet.PartRow)l_ds.Part.Rows[0];
            // note: not sure that this will work if you are updateing not adding new
            l_row.PartNum = partNum;
            // canned stuff to automate in next pass
            // 10Apr13, so noted but they are not automated at this point,
            // non stock added to the list
            l_row.Company = "CA";
            l_row.ClassID = "FG";
            
            if (DoWeHaveData("style"))
            {
                string Description = split[(int)GetEnumIndex("style")];
                if (Description != "NA")
                {
                    l_row.PartDescription = Description;
                }
            }
            if (DoWeHaveData("subClass"))
            {
                string subClass = split[(int)GetEnumIndex("subClass")];
                if (subClass != "NA")
                {
                    l_row.ProdCode = subClass;
                }
            }
            if (DoWeHaveData("pm_type"))
            {
                string pm_type = split[(int)GetEnumIndex("pm_type")];
                if (pm_type == "Purchased")
                {
                    l_row.TypeCode =  "P";            
                }
            }
            if (DoWeHaveData("Search"))
            {
                string search = split[(int)GetEnumIndex("Search")];
                if (search != "")
                {
                    l_row.SearchWord = search;
                }
            }
            if (DoWeHaveData("QtyBearing"))
            {
                string flag = split[(int)GetEnumIndex("QtyBearing")];
                if (String.Equals(flag, "No", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.QtyBearing = false;
                }
            }
            if (DoWeHaveData("NonStock"))
            {
                string flag = split[(int)GetEnumIndex("NonStock")];
                if (String.Equals(flag, "Yes", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.NonStock = true;
                }
            }
            if (DoWeHaveData("type"))
            {
                string typePurchManuf = split[(int)GetEnumIndex("type")];
                if (typePurchManuf.Equals("Purchased"))
                {
                    l_row.TypeCode = "P";

                }
                else if (typePurchManuf.Equals("Manufactured"))
                {
                    l_row.TypeCode = "M";
                }
            }
            if (DoWeHaveData("Country"))
            {
                string country = split[(int)GetEnumIndex("Country")];
                if (String.Equals(country,"China", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 42;
                }
                else if (String.Equals(country, "Taiwan", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 176;
                }
                else if (String.Equals(country, "Spain", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 166;
                }
                else if (String.Equals(country, "Pakistan", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 140;
                }
                else if (String.Equals(country, "Germany", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 72;
                }
                else if (String.Equals(country, "Korea", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 165;
                }
                else if (String.Equals(country, "Italy", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 93;
                }
                else if (String.Equals(country, "USA", StringComparison.OrdinalIgnoreCase))
                {
                    l_row.ISOrigCountryNum = 1;
                }
                l_row.CountryNumDescription = country;
            }
            
            if (DoWeHaveData("CasePack"))
            {
                string strCasePack = split[(int)GetEnumIndex("CasePack")];
                if (strCasePack != "NA")
                {
                    l_row.Number01 = Convert.ToDecimal(strCasePack);
                }
            }
            if (DoWeHaveData("WorkOrderCost"))
            {
                string strWorkOrderCost = split[(int)GetEnumIndex("WorkOrderCost")];
                if (strWorkOrderCost != "NA")
                {
                    l_row.Number11 = Convert.ToDecimal(strWorkOrderCost);
                }
            }
            if (DoWeHaveData("PurComment"))
            {
                string purComment = split[(int)GetEnumIndex("PurComment")];
                if (purComment != "NA")
                {
                    string newStr = purComment.Replace("; ", ".\n");
                    // string newStr = purComment.Replace(". ", ".\n");
                    l_row.PurComment = newStr;
                }
            }
            if ((DoWeHaveData("ShortChar01")))
            {
                string ShortChar01 = split[(int)GetEnumIndex("ShortChar01")];
                if (ShortChar01 != "NA")
                {
                    l_row.ShortChar01 = ShortChar01;
                }
            }
            if ((DoWeHaveData("ShortChar02")))
            {
                string ShortChar02 = split[(int)GetEnumIndex("ShortChar02")];
                if (ShortChar02 != "NA")
                {
                    l_row.ShortChar02 = ShortChar02;
                }
            }
            if ((DoWeHaveData("loc")))
            {
                string ShortChar02 = split[(int)GetEnumIndex("loc")];
                if (ShortChar02 != "NA")
                {
                    l_row.ShortChar02 = ShortChar02;
                }
            }
            if ((DoWeHaveData("ShortChar03")))
            {
                string ShortChar03 = split[(int)GetEnumIndex("ShortChar03")];
                if (ShortChar03 != "NA")
                {
                    l_row.ShortChar03 = ShortChar03;
                }
            }
            if ((DoWeHaveData("ShortChar04")))
            {
                string ShortChar04 = split[(int)GetEnumIndex("ShortChar04")];
                if (ShortChar04 != "NA")
                {
                    l_row.ShortChar04 = ShortChar04;
                }
            }
            if ((DoWeHaveData("ShortChar05")))
            {
                string ShortChar05 = split[(int)GetEnumIndex("ShortChar05")];
                if (ShortChar05 != "NA")
                {
                    l_row.ShortChar05 = ShortChar05;
                }
            }
            if ((DoWeHaveData("ShortChar06")))
            {
                string ShortChar06 = split[(int)GetEnumIndex("ShortChar06")];
                if (ShortChar06 != "NA")
                {
                    l_row.ShortChar06 = ShortChar06;
                }
            }
            if ((DoWeHaveData("ShortChar07")))
            {
                string ShortChar07 = split[(int)GetEnumIndex("ShortChar07")];
                if (ShortChar07 != "NA")
                {
                    l_row.ShortChar07 = ShortChar07;
                }
            }
            if ((DoWeHaveData("ShortChar08")))
            {
                string ShortChar08 = split[(int)GetEnumIndex("ShortChar08")];
                if (ShortChar08 != "NA")
                {
                    l_row.ShortChar08 = ShortChar08;
                }
            }
            if ((DoWeHaveData("UnitPrice")))
            {
                object UnitPrice = split[(int)GetEnumIndex("UnitPrice")];
                if (!UnitPrice.Equals(""))
                {
                    l_row.UnitPrice = Convert.ToDecimal(UnitPrice);
                }
            }
            if ((DoWeHaveData("Number01")))
            {
                object Number01 = split[(int)GetEnumIndex("Number01")];
                if (!Number01.Equals(""))
                {
                    l_row.Number01 = Convert.ToDecimal(Number01);
                }
            }
            if ((DoWeHaveData("Number02")))
            {
                object Number02 = split[(int)GetEnumIndex("Number02")];
                if (!Number02.Equals(""))
                {
                    l_row.Number02 = Convert.ToDecimal(Number02);
                }
            }
            if ((DoWeHaveData("Number03")))
            {
                object Number03 = split[(int)GetEnumIndex("Number03")];
                if (!Number03.Equals(""))
                {
                    l_row.Number03 = Convert.ToDecimal(Number03);
                }
            }
            if ((DoWeHaveData("Number04")))
            {
                object Number04 = split[(int)GetEnumIndex("Number04")];
                if (!Number04.Equals(""))
                {
                    l_row.Number04 = Convert.ToDecimal(Number04);
                }
            }
            if ((DoWeHaveData("Number05")))
            {
                object Number05 = split[(int)GetEnumIndex("Number05")];
                if (!Number05.Equals(""))
                {
                    l_row.Number05 = Convert.ToDecimal(Number05);
                }
            }
            if ((DoWeHaveData("Number06")))
            {
                object Number06 = split[(int)GetEnumIndex("Number06")];
                if (!Number06.Equals(""))
                {
                    l_row.Number06 = Convert.ToDecimal(Number06);
                }
            }
            if ((DoWeHaveData("Number07")))
            {
                object Number07 = split[(int)GetEnumIndex("Number07")];
                if (!Number07.Equals(""))
                {
                    l_row.Number07 = Convert.ToDecimal(Number07);
                }
            }
            if ((DoWeHaveData("Number08")))
            {
                object Number08 = split[(int)GetEnumIndex("Number08")];
                if (!Number08.Equals(""))
                {
                    l_row.Number08 = Convert.ToDecimal(Number08);
                }
            }
            if ((DoWeHaveData("ListPrice")))
            {
                object Number08 = split[(int)GetEnumIndex("ListPrice")];
                if (!Number08.Equals(""))
                {
                    l_row.Number08 = Convert.ToDecimal(Number08);
                }
            }
            if ((DoWeHaveData("Number09")))
            {
                object Number09 = split[(int)GetEnumIndex("Number09")];
                if (!Number09.Equals(""))
                {
                    l_row.Number09 = Convert.ToDecimal(Number09);
                }
            }
            if ((DoWeHaveData("Number10")))
            {
                object Number10 = split[(int)GetEnumIndex("Number10")];
                if (!Number10.Equals(""))
                {
                    l_row.Number10 = Convert.ToDecimal(Number10);
                }
            }
            if ((DoWeHaveData("Number11")))
            {
                object Number11 = split[(int)GetEnumIndex("Number11")];
                if (!Number11.Equals(""))
                {
                    l_row.Number11 = Convert.ToDecimal(Number11);
                }
            }
            if ((DoWeHaveData("Number12")))
            {
                object Number12 = split[(int)GetEnumIndex("Number12")];
                if (!Number12.Equals(""))
                {
                    l_row.Number12 = Convert.ToDecimal(Number12);
                }
            }
            if ((DoWeHaveData("CheckBox01")))
            {
                object CheckBox01 = split[(int)GetEnumIndex("CheckBox01")];
                if (!CheckBox01.Equals("NA"))
                {
                    if (CheckBox01.Equals("x"))
                    {
                        l_row.CheckBox01 = true;
                    }
                    else
                    {
                        l_row.CheckBox01 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox02")))
            {
                object CheckBox02 = split[(int)GetEnumIndex("CheckBox02")];
                if (!CheckBox02.Equals("NA"))
                {
                    if (CheckBox02.Equals("x"))
                    {
                        l_row.CheckBox02 = true;
                    }
                    else
                    {
                        l_row.CheckBox02 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox03")))
            {
                object CheckBox03 = split[(int)GetEnumIndex("CheckBox03")];
                if (!CheckBox03.Equals("NA"))
                {
                    if (CheckBox03.Equals("x"))
                    {
                        l_row.CheckBox03 = true;
                    }
                    else
                    {
                        l_row.CheckBox03 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox04")))
            {
                object CheckBox04 = split[(int)GetEnumIndex("CheckBox04")];
                if (!CheckBox04.Equals("NA"))
                {
                    if (CheckBox04.Equals("x"))
                    {
                        l_row.CheckBox04 = true;
                    }
                    else
                    {
                        l_row.CheckBox04 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox05")))
            {
                object CheckBox05 = split[(int)GetEnumIndex("CheckBox05")];
                if (!CheckBox05.Equals("NA"))
                {
                    if (CheckBox05.Equals("x"))
                    {
                        l_row.CheckBox05 = true;
                    }
                    else
                    {
                        l_row.CheckBox05 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox06")))
            {
                object CheckBox06 = split[(int)GetEnumIndex("CheckBox06")];
                if (!CheckBox06.Equals("NA"))
                {
                    if (CheckBox06.Equals("x"))
                    {
                        l_row.CheckBox06 = true;
                    }
                    else
                    {
                        l_row.CheckBox06 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox07")))
            {
                object CheckBox07 = split[(int)GetEnumIndex("CheckBox07")];
                if (!CheckBox07.Equals("NA"))
                {
                    if (CheckBox07.Equals("x"))
                    {
                        l_row.CheckBox07 = true;
                    }
                    else
                    {
                        l_row.CheckBox07 = false;
                    }
                }
            }
            if ((DoWeHaveData("CheckBox08")))
            {
                object CheckBox08 = split[(int)GetEnumIndex("CheckBox08")];
                if (!CheckBox08.Equals("NA"))
                {
                    if (CheckBox08.Equals("x"))
                    {
                        l_row.CheckBox08 = true;
                    }
                    else
                    {
                        l_row.CheckBox08 = false;
                    }
                }
            }
            if (DoWeHaveData("UserChar1"))
            {
                string UserChar1 = split[(int)GetEnumIndex("UserChar1")];
                if (!UserChar1.Equals(""))
                    if (UserChar1.Length > 30)
                    {
                        l_row.UserChar1 = UserChar1.Substring(0, 30).Trim();
                    }
                    else
                    {
                        l_row.UserChar1 = UserChar1;
                    }
            }
            if (DoWeHaveData("Character01"))
            {
                string Character01 = split[(int)GetEnumIndex("Character01")];
                if (!Character01.Equals(""))
                    l_row.Character01 = Character01;
            }
            if ((DoWeHaveData("Character02")))
            {
                string Character02 = split[(int)GetEnumIndex("Character02")];
                if (!Character02.Equals(""))
                    l_row.Character02 = Character02;
            }
            if ((DoWeHaveData("Character03")))
            {
                string Character03 = split[(int)GetEnumIndex("Character03")];
                if (!Character03.Equals(""))
                    l_row.Character03 = Character03;
            }
            if ((DoWeHaveData("Character04")))
            {
                string Character04 = split[(int)GetEnumIndex("Character04")];
                if (!Character04.Equals(""))
                    l_row.Character04 = Character04;
            }
            if ((DoWeHaveData("Character05")))
            {
                string Character05 = split[(int)GetEnumIndex("Character05")];
                if (!Character05.Equals(""))
                    l_row.Character05 = Character05;
            }
            if ((DoWeHaveData("Character06")))
            {
                string Character06 = split[(int)GetEnumIndex("Character06")];
                if (!Character06.Equals(""))
                    l_row.Character06 = Character06;
            }
            try
            {
                this.partObj.Update(l_ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        private bool DoWeHaveData(string key)
        {
            bool foundMatch = false;
            foreach (catalog catentry in Enum.GetValues(typeof(catalog)))
            {
                if (String.Equals(key, catentry.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    foundMatch = true;
                    break;
                }
            }
            return foundMatch;
        }
        private int GetEnumIndex(string key)
        {
            int foundIndex = 0;
            int i = 0;
            foreach (catalog catentry in Enum.GetValues(typeof(catalog)))
            {
                if (String.Equals(key, catentry.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    foundIndex = i;
                    break;
                }
                i++;
            }
            return foundIndex;
        }
        public void SetAsDefaultWarehouse(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)catalog.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                whseRow = (Epicor.Mfg.BO.PartDataSet.PartWhseRow)ds.PartWhse.Rows[0];
                whseRow.WarehouseCode = "01";
                // whseRow.PrimBinNum = "1";
                // whseRow.WarehouseDescription = "Western Optical WH";
                whseRow.WarehouseDescription = "Hayward";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
                ds = partObj.GetByID(partNum);
                Epicor.Mfg.BO.PartDataSet.PartPlantRow prow;
                prow = (Epicor.Mfg.BO.PartDataSet.PartPlantRow)ds.PartPlant.Rows[0];
                prow.PrimWhse = "01";
                prow.PrimWhseDescription = "Hayward";
                
                prow.BackFlush = true;
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
        public void AddWarehouse(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)backflush.UPC];
            string mfgSys = "MfgSys";
            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                partObj.GetNewPartWhse(ds, partNum, mfgSys);

                // Epicor.Mfg.BO.PartDataSet.PartWhseRow wrow;
                string message = "AOK";
                bool process = false;
                bool warehouseNotAdded = true;

                int i = 0;
                while (warehouseNotAdded)
                {
                    Epicor.Mfg.BO.PartDataSet.PartWhseRow wrow;
                    try
                    {
                        wrow = (Epicor.Mfg.BO.PartDataSet.PartWhseRow)ds.PartWhse.Rows[i];
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                        break;
                    }
                    if (wrow.WarehouseCode.CompareTo("01") == 0)
                    {
                        warehouseNotAdded = true;
                        break;
                    }
                    if (wrow.WarehouseCode.CompareTo("") == 0)
                    {
                        wrow.WarehouseCode = "01";
                        wrow.WarehouseDescription = "Huntwood";
                        process = true;
                    }
                    i++;
                    if (process)
                    {
                        try
                        {
                            partObj.Update(ds);
                            warehouseNotAdded = false;
                        }
                        catch (Exception e)
                        {
                            message = e.Message;
                        }
                        break;
                    }
                }
            }
        }
    }
}
     
    
