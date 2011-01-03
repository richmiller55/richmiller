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
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)listPrice.UPC];
            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];

                if (row.IsISOrigCountryNumNull() || row.ISOrigCountryNum == 0)
                {
                    row.ISOrigCountryNum = 42;
                }
                string strUnitPrice = split[(int)listPrice.unitPrice];
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

        public void UpdateCatalog(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)catalog.UPC];
            if (partNum == "Part Number")
            {
                return;
            }
            if (this.partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                string ShortChar03 = split[(int)catalog.ShortChar03];
                if (ShortChar03 != "N/A")
                {
                    row.ShortChar03 = ShortChar03;
                }
                string ShortChar05 = split[(int)catalog.ShortChar05];
                if (ShortChar05 != "N/A")
                {
                    row.ShortChar05 = ShortChar05;
                }
                string strNumber02 = split[(int)catalog.Number02];
                if (strNumber02.CompareTo("") != 0)
                {
                    int Number02 = Convert.ToInt32(strNumber02);
                    row.Number02 = Number02;
                }
                string strNumber03 = split[(int)catalog.Number03];
                if (strNumber03.CompareTo("") != 0)
                {
                    Decimal Number03 = Convert.ToDecimal(strNumber03);
                    row.Number03 = Number03;
                }
                string RunOutStr = split[(int)catalog.RunOut];
                bool runOut = true;
                if (RunOutStr.CompareTo("FALSE") == 0)
                {
                    runOut = false;
                }
                row.RunOut = runOut;
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
            string mfgSys = "MfgSys";
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
                string note = "allRight";
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
                        note = "index out of range";
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
        public void SimpleUpdatePrice(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)priceUpdate.UPC];

            string unitPrice = split[(int)priceUpdate.price];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                
                row.UnitPrice = Convert.ToDecimal(unitPrice);
                if (row.IsISOrigCountryNumNull())
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
        /*
        public void updatePart(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string partNum = split[(int)locUpdate.UPC];

            if (partObj.PartExists(partNum))
            {
                ds = partObj.GetByID(partNum);
                row = (Epicor.Mfg.BO.PartDataSet.PartRow)ds.Part.Rows[0];
                string update = split[(int)locUpdate.update];
                update.Trim();
                int result = update.CompareTo("E");
                if (result == 0)
                {
                    row.ShortChar02 = split[(int)locUpdate.LOC];
                }
                string strUnitPrice = split[(int)locUpdate.unitPrice];
                decimal unitPrice = Convert.ToDecimal(strUnitPrice);

                result = update.CompareTo("F");

                if (result == 0)
                {
                    row.UnitPrice = unitPrice;
                }
                result = update.CompareTo("EF");
                if (result == 0)
                {
                    row.ShortChar02 = split[(int)locUpdate.LOC];
                    row.UnitPrice = unitPrice;
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
         */
    }
}
     