using System;
using System.Collections.Generic;
using System.Text;

namespace ShipScrn
{
    class BuyGroup
    {
        Epicor.Mfg.BO.Customer custObj;
        Epicor.Mfg.BO.CustomerDataSet ds;
        bool buyGroupMember = false;
        public BuyGroup(Epicor.Mfg.Core.Session session, int custNum)
        {
            this.custObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            this.ds = this.custObj.GetByID(custNum);
            this.DetermineIfBuyGroup();
        }
        public BuyGroup(Epicor.Mfg.Core.Session session, string custId)
        {
            this.custObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            this.ds = this.custObj.GetByCustID(custId);
            this.DetermineIfBuyGroup();
        }
        private void DetermineIfBuyGroup()
        {
            int arrayLen = this.ds.CustBillTo.Rows.Count;
            while (arrayLen > 0)
            {
                Epicor.Mfg.BO.CustomerDataSet.CustBillToRow btRow =
                (Epicor.Mfg.BO.CustomerDataSet.CustBillToRow)this.ds.CustBillTo.Rows[arrayLen - 1];

                if (btRow.DefaultBillTo)
                {
                    this.buyGroupMember = true;
                    break;
                }
                arrayLen--;
            }
        }
        public bool GetBuyGroupMember()
        {
            return this.buyGroupMember;
        }
    }
}