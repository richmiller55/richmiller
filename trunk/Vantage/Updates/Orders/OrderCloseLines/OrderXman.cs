using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Epicor.Mfg.BO;

namespace OrderCloseLines
{

    public class OrderXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        SalesOrder salesOrder;
        // SalesOrderDataSet ds;
        // SalesOrderDataSet.OrderDtlRow row;

        //   pilot 8331
        //   sys 8301

        public OrderXman()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8301", Epicor.Mfg.Core.Session.LicenseType.Default);
            this.salesOrder = new Epicor.Mfg.BO.SalesOrder(objSess.ConnectionPool);
        }
        private bool CheckCustOnCreditHold(int orderNum, string custId)
        {
            string message = "";
            bool allowed = false;
            this.salesOrder.CheckCustOnCreditHold(orderNum ,custId, out message, out allowed);
            return allowed;
        }
        private string CheckOrderLinkToInterCompanyPO(int orderNum)
        {
            string message = "";
            this.salesOrder.CheckOrderLinkToInterCompanyPO(orderNum, out message);
            return message;
        }
        private OrdRelJobProdDataSet GetJobProd(int orderNum, int orderLineNum)
        {
            bool morePages = false;
            OrdRelJobProdDataSet ds = salesOrder.GetJobProd(orderNum, orderLineNum, 1, 1, 1, out morePages);
            return ds;
        }
        public void CloseOrderLine(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string orderNumStr = split[(int)input.orderNum];
            string orderLineStr = split[(int)input.orderLine];
            if (orderNumStr.CompareTo("OrderNum") == 0) return;
            int orderNum = Convert.ToInt32(orderNumStr);
            int orderLine = Convert.ToInt32(orderLineStr);
            string custId = split[(int)input.orderLine];
            this.CheckCustOnCreditHold(orderNum, custId);
            this.CheckOrderLinkToInterCompanyPO(orderNum);
            OrdRelJobProdDataSet ds = this.GetJobProd(orderNum, orderLine);

            try
                {
                    this.salesOrder.CloseOrderLine(orderNum, orderLine);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
}

     
