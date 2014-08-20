using System;
using System.Collections.Generic;
using System.Text;
using Epicor.Mfg.BO;

namespace PackMan
{
    public class DeletePack
    {
        Epicor.Mfg.Core.Session objSess;
        CustShip CustShip;

        public DeletePack()
        {
            this.objSess = new Epicor.Mfg.Core.Session(
            "rich", "homefed55", "AppServerDC://VantageDB1:8301",
            Epicor.Mfg.Core.Session.LicenseType.Default);
            this.CustShip = new CustShip(objSess.ConnectionPool);
        }
        public void OpenCloseDelete(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string packIdStr = split[0];
            Int32 packId = Convert.ToInt32(packIdStr);
            this.GetShipRow(packId);
        }
        public void GetShipRow(Int32 packId)
        {
            bool keepGoing = false;
            Epicor.Mfg.BO.CustShipDataSet ds;
            try
            {
                ds = this.CustShip.GetByID(packId);
            }
            catch (Exception e)
            {
                keepGoing  = false;
            }
            if (keepGoing)
            {
                CustShipDataSet.ShipHeadRow row = (CustShipDataSet.ShipHeadRow)ds.ShipHead.Rows[0];
                int orderNum = row.OrderNum;
                string warningMessage = "";
                this.CustShip.POGetNew(orderNum, packId, out warningMessage);
                this.CustShip.POGetDtlList(packId, orderNum, "PACK");
                this.CustShip.validateShipped(false, ds);
                string releaseMess;
                string completeMess;
                string shippingMess;
                string lotMess;
                string inventoryMess;
                string lockQtyMess;

                this.CustShip.CheckShipDtl(ds, out releaseMess, out completeMess,
                        out shippingMess, out lotMess, out inventoryMess, out lockQtyMess);
                
                try
                {
                    this.CustShip.Update(ds);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
        }
    }
}
