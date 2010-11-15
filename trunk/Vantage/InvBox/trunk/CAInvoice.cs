using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Epicor.Mfg.BO;

namespace InvBox
{
    class CAInvoice
    {
        Epicor.Mfg.BO.ARInvoice arInvoice;
        Epicor.Mfg.Core.Session session;
        string invGroup;
        Hashtable PackInvoices;
        Hashtable InvoicePacks;
        string packSlipStr;
        int packSlipNo;
        ShipMgr shipMgr;
        int lastInvoiceNo;
        string newInvoices = string.Empty;
        Invoice inv;
        public CAInvoice(Epicor.Mfg.Core.Session vanSession, string arInvGroup, string pack,
                         ShipMgr shipMgr)
        {
            this.session = vanSession;
            this.arInvoice = new Epicor.Mfg.BO.ARInvoice(vanSession.ConnectionPool);
            this.invGroup = arInvGroup;
            this.ShipMgr = shipMgr;

            PackInvoices = new Hashtable();
            InvoicePacks = new Hashtable();
            this.PackSlipStr = pack;
            this.PackSlipNo = Convert.ToInt32(pack);

            string invoices = string.Empty;
            string errors = string.Empty;
            InvoicePack(pack, out invoices, out errors);
            SetBatchStats();  // remember this sets lastInvoiceNumber
            FillInvoiceInfo();
            if (inv.FreeFreight())
            {
                // logging
            }
            else
            {
                NewInvcMiscChrg(ShipMgr.TotalFreight, ShipMgr.TrackingNumber);
            }
            InvPrintDocument printer = new InvPrintDocument(this.TheInvoice);
            printer.Print();
        }
        public int GetInvoiceFromPack(int pack)
        {
            int invoice = 0;
            string message = "ok";
            try
            {
                invoice = (int)PackInvoices[pack];
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return invoice;
        }
        public int GetInvoiceFromPack(string packNo)
        {
            int pack = Convert.ToInt32(packNo);
            int invoice = this.GetInvoiceFromPack(pack);
            return invoice;
        }
        public int GetPackFromInvoice(string Invoice)
        {
            int invoiceNo = Convert.ToInt32(Invoice);
            return (int)InvoicePacks[invoiceNo];
        }
        private int SetBatchStats()
        {
            Epicor.Mfg.BO.InvcHeadListDataSet InvList = new Epicor.Mfg.BO.InvcHeadListDataSet();
            string query = "GroupID ='" + this.invGroup + "' AND Posted = false BY InvoiceNum";
            bool myout;
            InvList = arInvoice.GetList(query, 0, 0, out myout);
            this.PackInvoices.Clear();
            this.InvoicePacks.Clear();
            foreach (Epicor.Mfg.BO.InvcHeadListDataSet.InvcHeadListRow row in
                  InvList.InvcHeadList.Rows)
//                (Epicor.Mfg.BO.InvcHeadListDataSet.InvcHeadListRow)InvList.InvcHeadList.Rows)
            {
                this.lastInvoiceNo = row.InvoiceNum;
                // these assume multiple invoices in the system
                this.PackInvoices.Add(row.PackSlipNum,row.InvoiceNum);               
                this.InvoicePacks.Add(row.InvoiceNum,row.PackSlipNum);
            }
            return this.lastInvoiceNo;
        }
        void InvoicePack(string packNo, out string invoices, out string errors)
        {
            string custList = "";
            string plant = "CURRENT";
            bool billToFlag = true;
            bool overBillDay = false;
            arInvoice.GetShipments(this.invGroup, custList, packNo, plant, billToFlag,
                                   overBillDay, out invoices, out errors);
        }
        /*
        void GetInvoice(string packNo, out string invoices, out string errors)
        {
            string custList = "";
            string plant = "CURRENT";
            bool billToFlag = true;
            bool overBillDay = false;
            arInvoice.GetShipments(this.invGroup, custList, packSlip, plant, billToFlag, 
                                   overBillDay, out invoices, out errors);
            Epicor.Mfg.BO.InvcHeadListDataSet InvList = new Epicor.Mfg.BO.InvcHeadListDataSet();
            string query = "GroupID = 'RLM85' AND Posted = false BY InvoiceNum";
            bool myout;
            InvList = arInvoice.GetList(query, 0, 0, out myout);
            Epicor.Mfg.BO.InvcHeadListDataSet.InvcHeadListRow row =
                (Epicor.Mfg.BO.InvcHeadListDataSet.InvcHeadListRow)InvList.InvcHeadList.Rows[0]; ;
            this.invoiceNo = row.InvoiceNum;
        }
         */
        public void AddTrackingToInvcHead(string packId,string trackingNo)
        {
            Epicor.Mfg.BO.ARInvoiceDataSet ds = new Epicor.Mfg.BO.ARInvoiceDataSet();
            int invoiceNo = this.GetInvoiceFromPack(packId);
            if (invoiceNo != 0)
            {
                ds = arInvoice.GetByID(invoiceNo);
                Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow row =
                    (Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow)ds.InvcHead.Rows[0];
                row.Character01 = trackingNo;
                row.CheckBox01 = true;
                string message = "OK";
                try
                {
                    arInvoice.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }
        /*
        public int GetInvoiceNo()
        {
            return this.invoiceNo;
        }
         * */
        public void FillInvoiceLines()
        {
            ARInvoiceDataSet ds = new ARInvoiceDataSet();
            ds = arInvoice.GetByID(this.lastInvoiceNo);
            foreach (ARInvoiceDataSet.InvcDtlRow row in ds.InvcDtl.Rows)
            {
                InvLine line = new InvLine(row);
                this.inv.AddLine(line);
            }
        }
        public void FillInvoiceInfo()
        {
            Epicor.Mfg.BO.ARInvoiceDataSet ds = new Epicor.Mfg.BO.ARInvoiceDataSet();
            try
            {
                ds = arInvoice.GetByID(this.lastInvoiceNo);
                Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow row =
                    (Epicor.Mfg.BO.ARInvoiceDataSet.InvcHeadRow)ds.InvcHead.Rows[0];
                inv = new Invoice(this.session, row);

                inv.FreightCharge = this.ShipMgr.FreightCharge;
                inv.TotalFreight = this.ShipMgr.TotalFreight;
                inv.TrackingNo = this.ShipMgr.TrackingNumber;
                FillInvoiceLines();
                this.inv.FillShipTo();
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public void NewInvcMiscChrg(decimal amount, string trackingNo)
        {
            bool billFreight = false;
            if (shipMgr.FreightCharge.CompareTo(0.0M) == 1)
                billFreight = true;
            if (billFreight)
            {
                Epicor.Mfg.BO.ARInvoiceDataSet ds = new Epicor.Mfg.BO.ARInvoiceDataSet();
                ds = arInvoice.GetByID(this.lastInvoiceNo);  // maybe better to lookup from pack
                int invoiceLineDefault = 1;
                arInvoice.GetNewInvcMisc(ds, this.lastInvoiceNo, invoiceLineDefault);
                Epicor.Mfg.BO.ARInvoiceDataSet.InvcMiscRow miscRow =
                    (Epicor.Mfg.BO.ARInvoiceDataSet.InvcMiscRow)ds.InvcMisc.Rows[0];
                string frtMiscCode = "1";
                miscRow.MiscAmt = amount;
                miscRow.DocMiscAmt = amount;
                miscRow.DspDocMiscAmt = amount;
                miscRow.DspMiscAmt = amount;
                miscRow.Description = "Freight Charge";
                miscRow.MiscCode = frtMiscCode;
                miscRow.TaxCatID = "FREIGHT";
                if (trackingNo.Length > 50)
                {
                    miscRow.ShortChar01 = trackingNo.Substring(0, 49);
                }
                else
                {
                    miscRow.ShortChar01 = trackingNo;
                }
                string message = "Posted";
                try
                {
                    arInvoice.Update(ds);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
                this.SetBatchStats();
            }
        }
        public Invoice TheInvoice
        {
            get
            {
                return inv;
            }
            set
            {
                inv = value;
            }
        }
        public ShipMgr ShipMgr
        {
            get
            {
                return shipMgr;
            }
            set
            {
                shipMgr = value;
            }
        }
        public int PackSlipNo
        {
            get
            {
                return packSlipNo;
            }
            set
            {
                packSlipNo = value;
            }
        }
        public string PackSlipStr
        {
            get
            {
                return packSlipStr;
            }
            set
            {
                packSlipStr = value;
            }
        }

    }
}
