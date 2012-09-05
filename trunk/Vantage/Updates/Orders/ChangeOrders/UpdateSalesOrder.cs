using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace ChangeOrders
{
    public enum col
    {
        orderNum,
        orderLine
    }
    class UpdateSalesOrder
    {
        /*
         * Training   8311
         * Pilot      8331
         * production 8301
         * test       8321
         */
        protected Epicor.Mfg.Core.Session objSess;
        public UpdateSalesOrder()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
        }
        public void ProcessOrder(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string orderStr = split[(int)col.orderNum];
            int orderNum = Convert.ToInt32(orderStr);

            Epicor.Mfg.BO.SalesOrder salesOrderObj = new Epicor.Mfg.BO.SalesOrder(objSess.ConnectionPool);
            Epicor.Mfg.BO.SalesOrderDataSet soDs = salesOrderObj.GetByID(orderNum);

            foreach (Epicor.Mfg.BO.SalesOrderDataSet.OrderDtlRow row in soDs.OrderDtl.Rows)
            {
                if (row.PartNum.Equals("757026202313") && row.OpenLine.Equals(true))
                {
                    row.PartNum = "757026232419";
                    row.PartNumPartDescription = "00255-SRC-CGNC-BLCK-000";
                    try
                    {
                        salesOrderObj.ChangePartNum(soDs, false);
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                    row.UnitPrice = Convert.ToDecimal(52.50);
                    row.DocUnitPrice = Convert.ToDecimal(52.50);
                    row.PricePerCode = "E";
                    row.RowMod = "U";
                } 
                if (row.PartNum.Equals("757026203730") && row.OpenLine.Equals(true))
                {
                    row.PartNum = "757026233355";
                    row.PartNumPartDescription = "00237-SRC-BRZL-BLCK-000";
                    try
                    {
                        salesOrderObj.ChangePartNum(soDs, false);
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                    row.UnitPrice = Convert.ToDecimal(11.25);
                    row.DocUnitPrice = Convert.ToDecimal(11.25);
                    row.PricePerCode = "E";
                    row.RowMod = "U";
                }
            }
            try
            {
                salesOrderObj.Update(soDs);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
    }
}
