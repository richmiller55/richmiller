using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace InvBox
{
    class ARInvoice
    {
        Epicor.Mfg.BO.ARInvoice arInvoice;
        string invGroup;
        Hashtable PackInvoices;
        Hashtable InvoicePacks;
        string packSlip;
        int lastInvoiceNo;
        string batchName;
        string newInvoices = string.Empty;
        public ARInvoice(Epicor.Mfg.Core.Session vanSession, string arInvGroup, string pack)
        {
            arInvoice = new Epicor.Mfg.BO.ARInvoice(vanSession.ConnectionPool);
            this.invGroup = arInvGroup;
            
            PackInvoices = new Hashtable();
            InvoicePacks = new Hashtable();

            int packNo = Convert.ToInt32(pack);
            this.packSlip = pack;
            string invoices = string.Empty;
            string errors = string.Empty;
            this.InvoicePack(pack, out invoices, out errors);
            // this.GetInvoice(pack, out invoices, out errors);
            this.SetBatchStats();
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
        public int SetBatchStats()
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
            arInvoice.GetShipments(this.invGroup, custList, packSlip, plant, billToFlag,
                                   overBillDay, out invoices, out errors);
            Epicor.Mfg.BO.InvcHeadListDataSet InvList = new Epicor.Mfg.BO.InvcHeadListDataSet();
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
        public void GetNewInvcMisc(decimal amount, string trackingNo)
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
            miscRow.Description = "FedEx Freight Charge";
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
}
