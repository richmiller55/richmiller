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
        Epicor.Mfg.BO.PartDataSet.PartPlantRow plantRow;
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
        public void PriceUpdate(string line)
        {
            // using this for the listPrice conversion
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)listPrice.UPC];
            if (partNum.CompareTo("UPC") == 0) return;
            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                if (row.IsISOrigCountryNumNull() || row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }

                row.UnitPrice = Convert.ToDecimal(split[(int)listPrice.currentList]);
                row.Number09 = Convert.ToDecimal(split[(int)listPrice.goalPrice]);

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

        public void NewPart(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)newPart.UPC];
            if (partNum == "Part Number")
            {
                return;
            }
            if (this.partObj.PartExists(partNum))
            {
                // throw a fit
            }
            else
            {
                Epicor.Mfg.BO.PartDataSet ds = new Epicor.Mfg.BO.PartDataSet();
                this.partObj.GetNewPart(ds);
                Epicor.Mfg.BO.PartDataSet.PartRow row;
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.Company = "CA";
                row.PartNum = split[(int)newPart.UPC];
                row.PartDescription = split[(int)newPart.style];
                row.UserChar1 = split[(int)newPart.style];
                
                row.ShortChar02 = split[(int)newPart.loc];
                string casePack = split[(int)newPart.casePack];
                row.Number01 = Convert.ToDecimal(casePack);
                // string country = split[(int)newPart.country];
                string country = "China";
                if (country.CompareTo("China") == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                else if (country.CompareTo("Taiwan") == 0)
                {
                    row.ISOrigCountryNum = 176;
                }
                row.ProdCode = split[(int)newPart.subClass];
                row.ClassID = "FG";
                row.TypeCode = "P";
                string unitPrice = split[(int)newPart.unitPrice];
                row.UnitPrice = Convert.ToDecimal(unitPrice);
                string purchCommentsRaw = split[(int)newPart.purchComments];
                string purchComments = purchCommentsRaw.Replace(".", ".\n");
                row.PurComment = purchComments;
                string message = "posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void PrudyNewPart(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)prudy.UPC];
            if (partNum == "Part Number")
            {
                return;
            }
            if (partNum == "UPC Number")
            {
                return;
            }
            if (this.partObj.PartExists(partNum))
            {
                // throw a fit
            }
            else
            {
                Epicor.Mfg.BO.PartDataSet ds = new Epicor.Mfg.BO.PartDataSet();
                this.partObj.GetNewPart(ds);
                Epicor.Mfg.BO.PartDataSet.PartRow row;
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.Company = "CA";
                row.PartNum = split[(int)prudy.UPC];
                row.PartDescription = split[(int)prudy.style];
                row.ShortChar03 = split[(int)prudy.flyer];
                row.ShortChar04 = split[(int)prudy.shortChar04];
              
                row.SearchWord = split[(int)prudy.search];
                row.ShortChar02 = split[(int)prudy.loc];
                string casePack = split[(int)prudy.casePack];
                row.Number01 = Convert.ToDecimal(casePack);

                string country = split[(int)prudy.country];
                if (country.CompareTo("China") == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                else if (country.CompareTo("USA") == 0)
                {
                    row.ISOrigCountryNum = 1;
                }

                row.ProdCode = split[(int)prudy.subClass];
                row.ClassID = "FG";
                row.TypeCode = "M";  // SPECIAL FOR PRINT ITEMS
                string unitPrice = split[(int)prudy.unitPrice];
                row.UnitPrice = Convert.ToDecimal(unitPrice);
                string listPrice = split[(int)prudy.Number08];
                row.Number08 = Convert.ToDecimal(listPrice);
                row.NonStock = true;
                row.Character01 = split[(int)prudy.Character01];
                row.Character02 = split[(int)prudy.Character02];
                string message = "posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void NewPartEx(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)newPart.UPC];
            if (partNum == "UPC")
            {
                return;
            }
            if (this.partObj.PartExists(partNum))
            {
                // throw a fit
            }
            else
            {
                Epicor.Mfg.BO.PartDataSet ds = new Epicor.Mfg.BO.PartDataSet();
                this.partObj.GetNewPart(ds);
                Epicor.Mfg.BO.PartDataSet.PartRow row;
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.Company = "CA";
                row.PartNum = split[(int)newPartEx.UPC];
                row.PartDescription = split[(int)newPartEx.style];
                row.UserChar1 = split[(int)newPartEx.style];

                row.ShortChar02 = split[(int)newPartEx.loc];
                string casePack = split[(int)newPartEx.casePack];
                row.Number01 = Convert.ToDecimal(casePack);
                string country = split[(int)newPartEx.country];
                // string country = "China";
                if (country.CompareTo("China") == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                else if (country.CompareTo("Taiwan") == 0)
                {
                    row.ISOrigCountryNum = 176;
                }
                row.ProdCode = split[(int)newPartEx.subClass];
                row.ClassID = "FG";
                row.TypeCode = "P";
                string unitPrice = split[(int)newPartEx.unitPrice];
                row.UnitPrice = Convert.ToDecimal(unitPrice);
                // string purchCommentsRaw = split[(int)newPart.purchComments];
                // string purchComments = purchCommentsRaw.Replace(".", ".\n");
                // row.PurComment = purchComments;
                row.SearchWord = split[(int)newPartEx.search];
                string message = "posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdateCatalog(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)catalog.UPC];
            if (partNum == "Part Number")
            {
                return;
            }
            if (partNum == "UPC")
            {
                return;
            }
            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                string ShortChar03 = split[(int)catalog.ShortChar03];
                if (ShortChar03 != "NA")
                {
                    row.ShortChar03 = ShortChar03;
                }
                string ShortChar04 = split[(int)catalog.ShortChar04];
                if (ShortChar04 != "NA")
                {
                    row.ShortChar04 = ShortChar04;
                }
                /*
                string ShortChar05 = split[(int)catalog.ShortChar05];
                if (ShortChar05 != "NA")
                {
                    row.ShortChar05 = ShortChar05;
                }
                */
                string ShortChar06 = split[(int)catalog.ShortChar06];
                if (ShortChar06.CompareTo("") != 0)
                {
                    row.ShortChar06 = ShortChar06;
                }
                string ShortChar07 = split[(int)catalog.ShortChar07];
                if (ShortChar07.CompareTo("NA") != 0)
                {
                    row.ShortChar07 = ShortChar07;
                }
                string Character01 = split[(int)catalog.Character01];
                if (Character01 != "")
                {
                    row.Character01 = Character01;
                }
                string Character02 = split[(int)catalog.Character02];
                if (Character02 != "")
                {
                    row.Character02 = Character02;
                }
                string strNumber01 = split[(int)catalog.Number01];
                if (strNumber01.CompareTo("NA") != 0)
                {
                    row.Number01 = Convert.ToInt32(strNumber01);
                }
                string strNumber05 = split[(int)catalog.Number05];
                if (strNumber05.CompareTo("NA") != 0 & strNumber05.CompareTo("") != 0)
                {
                    row.Number05 = Convert.ToInt32(strNumber05);
                }
                string strNumber06 = split[(int)catalog.Number06];
                if (strNumber06.CompareTo("NA") != 0 & strNumber06.CompareTo("") != 0)
                {
                    row.Number06 = Convert.ToInt32(strNumber06);
                }
                string strNumber07 = split[(int)catalog.Number07];
                if (strNumber07.CompareTo("NA") != 0 & strNumber07.CompareTo("") != 0)
                {
                    row.Number07 = Convert.ToInt32(strNumber07);
                }
                string strNumber08 = split[(int)catalog.Number08];
                if (strNumber08.CompareTo("NA") != 0 & strNumber08.CompareTo("") != 0)
                {
                    row.Number08 = Convert.ToDecimal(strNumber08);
                }
                string strUnitPrice = split[(int)catalog.UnitPrice];
                if (strUnitPrice.CompareTo("NA") != 0)
                {
                    row.UnitPrice = Convert.ToDecimal(strUnitPrice);
                }
                string strCheckBox02 = split[(int)catalog.CheckBox02];
                if (strCheckBox02.CompareTo("NA") != 0)
                {
                    if (strCheckBox02.Equals("1"))
                    {
                        row.CheckBox02 = true;
                    }
                    else
                    {
                        row.CheckBox02 = false;
                    }
                }
                string strCheckBox03 = split[(int)catalog.CheckBox03];
                if (strCheckBox03.CompareTo("NA") != 0)
                {
                    if (strCheckBox03.Equals("1"))
                    {
                        row.CheckBox03 = true;
                    }
                    else
                    {
                        row.CheckBox03 = false;
                    }
                }
                string strCheckBox04 = split[(int)catalog.CheckBox04];
                if (strCheckBox04.CompareTo("NA") != 0)
                {
                    if (strCheckBox04.Equals("1"))
                    {
                        row.CheckBox04 = true;
                    }
                    else
                    {
                        row.CheckBox04 = false;
                    }
                }
                string strCheckBox05 = split[(int)catalog.CheckBox05];
                if (strCheckBox05.CompareTo("NA") != 0)
                {
                    if (strCheckBox05.Equals("1"))
                    {
                        row.CheckBox05 = true;
                    }
                    else
                    {
                        row.CheckBox05 = false;
                    }
                }
                if (row.IsISOrigCountryNumNull())
                {
                    row.ISOrigCountryNum = 42;
                }
                if (row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                
                /*
                string strNumber08 = split[(int)catalog.Number08];
                if (strNumber08 != "N/A")
                {
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
                }  */

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
        public void SetAsDefaultWarehouse(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)backflush.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                whseRow = (Epicor.Mfg.BO.PartDataSet.PartWhseRow)ds.PartWhse.Rows[0];
                whseRow.WarehouseCode = "01";
                whseRow.WarehouseDescription = "Hayward";
                string note = "";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                    note = "update failed";
                }

                ds = partObj.GetByID(partNum);
                Epicor.Mfg.BO.PartDataSet.PartPlantRow prow;
                prow = (Epicor.Mfg.BO.PartDataSet.PartPlantRow)ds.PartPlant.Rows[0];
                prow.PrimWhse = "01";
                prow.PrimWhseDescription = "Hayward";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                    note = "update failed";
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
                partObj.GetNewPartWhse(ds,partNum,mfgSys);

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
                        string note = "index out of range";
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
        public void UpdateInfo(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)infoUpdate.UPC];
            string aicDesc = split[(int)infoUpdate.aicDescr];
            string flyerNickname = split[(int)infoUpdate.flyerNickname];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                // row.UnitPrice = Convert.ToDecimal(unitPrice);
                row.UserChar1 = aicDesc;
                row.ShortChar04 = flyerNickname;

                // row.ISOrigCountryNum = 42;
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdatePrintOptions(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)printOption.UPC];
            string printOptions = split[(int)printOption.printOption];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                row.ShortChar06 = printOptions;
                if (row.IsISOrigCountryNumNull() || row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdateDescr(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)descrUpdate.UPC];
            string name = split[(int)descrUpdate.name];
            string descr = split[(int)descrUpdate.descr];
            string colors = split[(int)descrUpdate.colors];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                // row.UnitPrice = Convert.ToDecimal(unitPrice);
                row.ShortChar04 = name;
                row.Character02 = descr;
                row.Character01 = colors;
                row.ISOrigCountryNum = 42;
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }

        public void UpdatePrice(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)subClassPriceUpdate.UPC];
            string unitPrice = split[(int)subClassPriceUpdate.direct];
            string listPrice = split[(int)subClassPriceUpdate.list];
            string subClass = split[(int)subClassPriceUpdate.subClass];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                
                row.UnitPrice = Convert.ToDecimal(unitPrice);
                row.Number08 = Convert.ToDecimal(listPrice);
                row.ProdCode = subClass;
                row.ISOrigCountryNum = 42;
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdateBackflush(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)backflush.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                plantRow = (Epicor.Mfg.BO.PartDataSet.PartPlantRow)ds.PartPlant.Rows[0];
                plantRow.BackFlush = true;

               // row.ISOrigCountryNum = 42;
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdateWOS(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)wosUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                
                string orderType = split[(int)wosUpdate.orderType];
                orderType.Trim();

                row.ShortChar05 = orderType;
                string minWOSStr = split[(int)wosUpdate.minWos];
                int minWOS = Convert.ToInt32(minWOSStr);
                if (minWOS > 0)
                {
                    row.Number02 = minWOS;
                }

                string maxWOSStr = split[(int)wosUpdate.maxWos];
                int maxWOS = Convert.ToInt32(maxWOSStr);
                if (maxWOS > 0)
                {
                    row.Number03 = maxWOS;
                }
                row.ISOrigCountryNum = 42;
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void SimpleRunOutFlag(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)runOutUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                string runOutFlag = split[(int)runOutUpdate.runOut];
                runOutFlag.Trim();
                string lowRunOut = runOutFlag.ToLower();
                int result = lowRunOut.CompareTo("1");
                if (result == 0) { row.RunOut = true; }

                row.ISOrigCountryNum = 42;
                // row.CountryNumDescription = "China";
                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void UpdateRunOutFlag(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)runOutUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                
                string runOutFlag = split[(int)runOutUpdate.runOut];
                runOutFlag.Trim();
                string lowRunOut = runOutFlag.ToLower();
                int result = lowRunOut.CompareTo("1");
                if (result == 0) { row.RunOut = true; }

/*
 *              string inactiveFlag = split[(int)runOutUpdate.inactive];
                inactiveFlag.Trim();
                string lowInactive = inactiveFlag.ToLower();
                result = lowInactive.CompareTo("y");
                if (result == 0) { row.InActive = true; }
*/
/*
                string pmFlag = split[(int)runOutUpdate.pmFlag]; // purch vs manufact
                pmFlag.Trim();
                string lowPmFlag = pmFlag.ToLower();
                result = lowPmFlag.CompareTo("m");
                if (result == 0) { row.TypeCode = "M"; }
                result = lowPmFlag.CompareTo("p");
                if (result == 0) { row.TypeCode = "P"; }
                // row.ISOrigCountry = "42";
                row.ISOrigCountryNum = 42;
                // row.CountryNumDescription = "China";

                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
 */
            }
 
        }
        public void UpdatePartCasePack(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)casePackUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                string casePackStr = split[(int)casePackUpdate.casePack];
                row.Number01 = Convert.ToInt32(casePackStr);

                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        public void SimpleUpdatePart(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)locUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                row.ShortChar02 = split[(int)locUpdate.LOC];

                string message = "Posted";
                try
                {
                    partObj.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
    }
}
     
