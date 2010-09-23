using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace InvBox
{
    class PackSlip
    {
        Epicor.Mfg.Core.Session session;
        Epicor.Mfg.BO.CustShip custShipObj;
        Epicor.Mfg.BO.CustShipDataSet custShipDs;
        Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow dtlRow;
        Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow custShipRow;
        bool invoiced;
        int packNum;
        int CustNum;
        int OrderNum;
        bool orderFF;
        bool packFound;
        bool orderFound;
        bool packNeedsTracking;
        bool isBuyGroup;
        string orderShipVia;
        string customerTerms;
        bool customerFF;
        string newInvoices = string.Empty;
        public PackSlip(Epicor.Mfg.Core.Session vanSession, int pack)
        {
            packFound = false;
            packNum = pack;
            session = vanSession;
            orderFF = false;
            packNeedsTracking = true;
            orderShipVia = "";
            
            customerFF = false;
            InitCustShip();
            if (packFound)
            {
                GetOrderInfo();
                GetCustomerInfo();
            }
        }
        public void ExtactAddress(StreetAddress sa)
        {
            sa.Address1 = this.custShipRow.BillAddr;
        }
        void InitCustShip()
        {
            custShipObj = new Epicor.Mfg.BO.CustShip(session.ConnectionPool);
            bool result = true;
            string message;
            try
            {
                custShipDs = custShipObj.GetByID(packNum);

                this.custShipRow = (Epicor.Mfg.BO.CustShipDataSet.ShipHeadRow)custShipDs.ShipHead.Rows[0];
                CustNum = custShipRow.CustNum;

                BuyGroup bgCheck = new BuyGroup(session, CustNum);
                this.IsBuyGroup = bgCheck.GetBuyGroupMember();
                string trackingNum = custShipRow.TrackingNumber;
                
                if (trackingNum.Length > 0)
                {
                    packNeedsTracking = false;
                }

                dtlRow = (Epicor.Mfg.BO.CustShipDataSet.ShipDtlRow)custShipDs.ShipDtl.Rows[0];
                this.OrderNum = dtlRow.OrderNum;
                invoiced = custShipRow.Invoiced;
                // custShipDs.Dispose();
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
                result = false;
            }
            if (result)
            {
                packFound = true;
            }
        }
        void GetOrderInfo()
        {
            Epicor.Mfg.BO.SalesOrder salesOrderObj;
            salesOrderObj = new Epicor.Mfg.BO.SalesOrder(session.ConnectionPool);
            Epicor.Mfg.BO.SalesOrderDataSet soDs = new Epicor.Mfg.BO.SalesOrderDataSet();
            bool result = true;
            string message;
            try
            {
                soDs = salesOrderObj.GetByID(OrderNum);
                Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow row = (Epicor.Mfg.BO.SalesOrderDataSet.OrderHedRow)soDs.OrderHed.Rows[0];
                orderFF = row.CheckBox03;
                orderShipVia = row.ShipViaCode;
                soDs.Dispose();
            }
            catch (Exception e)
            {
                // header did not post
                message = e.Message;
                result = false;
            }
            if (result)
            {
                orderFound = true;
            }
        }
        void GetCustomerInfo()
        {
            Epicor.Mfg.BO.Customer customerObj;
            customerObj = new Epicor.Mfg.BO.Customer(session.ConnectionPool);
            Epicor.Mfg.BO.CustomerDataSet ds;
            ds = customerObj.GetByID(CustNum);
            Epicor.Mfg.BO.CustomerDataSet.CustomerRow row = (Epicor.Mfg.BO.CustomerDataSet.CustomerRow)ds.Customer.Rows[0];
            customerTerms = row.ShortChar01;
            if (customerTerms.CompareTo("FF") == 0)
            {
                customerFF = true;
            }
        }
        public bool NeedsTracking
        {
            get
            {
                return packNeedsTracking;
            }
            set
            {
                packNeedsTracking = value;
            }
        }
        public bool CustomerFF
        {
            get
            {
                return customerFF;
            }
            set
            {
                customerFF = value;
            }
        }
        public bool Invoiced
        {
            get
            {
                return invoiced;
            }
            set
            {
                invoiced = value;
            }
        }
        public bool OrderFF
        {
            get
            {
                return orderFF;
            }
            set
            {
                orderFF = value;
            }
        }
        public int PackNum
        {
            get
            {
                return packNum;
            }
            set
            {
                packNum = value;
            }
        }
        public bool IsBuyGroup
        {
            get
            {
                return isBuyGroup;
            }
            set
            {
                isBuyGroup = value;
            }
        }
    }
}
        