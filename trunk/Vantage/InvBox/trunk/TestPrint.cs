using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;


namespace InvBox
{
    class TestPrint
    {
        Invoice i;
        Epicor.Mfg.Core.Session sess;
        public TestPrint()
        {
            this.i = new Invoice();
            this.FillInvoice();
            InvoiceFormater invFormat = new InvoiceFormater(this.i);
            InvPrintDocument prtDoc = new InvPrintDocument(invFormat.ReportArray);
            prtDoc.Print();
            // PrtForm prtForm = new PrtForm(invFormat.ReportArray);
        }
        public TestPrint(Epicor.Mfg.Core.Session  session)
        {
            this.sess = session;
            PackSlip ps = new PackSlip(this.sess, 1434);  // todo get a working pack in test
            this.i = new Invoice();
            
            InvoiceFormater invFormat = new InvoiceFormater(this.i);
            InvPrintDocument prtDoc = new InvPrintDocument(invFormat.ReportArray);
            prtDoc.Print();
            // PrtForm prtForm = new PrtForm(invFormat.ReportArray);
        }
        private void FillInvoice()
        {
            i.InvoiceNo = 612968;
            i.InvoiceDate = new DateTime(2008, 7, 10);
            i.OrderDate = new DateTime(2008, 7, 4);
            i.ShipDate = new DateTime(2008, 7, 10);

            i.PoNo = "460421:262";
            i.PackID = 1219;
            this.FillAddresses();
            this.FillLine();
        }
        void FillAddresses()
        {
            i.BillToCustName = "Stein Mart";
            // i.BillToInvoiceAddress = "P O Box 48130" + "\n";
            // i.BillToInvoiceAddress += "Jacksonville FL 32247" + "\n";
            i.SoldToCustName = "Stein Mart 262";
            // i.SoldToInvoiceAddress = "25191 Chamber Of Commerce" + "\n";
            // i.SoldToInvoiceAddress += "Bonita Springs FL 34135" + "\n";
        }
        void FillLine()
        {
            InvLine line = new InvLine();
            line.DueDate = new DateTime(2008, 8, 9);
            line.Discount = 0.0M;
            line.DocLineTotal = 3.54M;
            line.ExtPrice = 3.54M;
            line.UnitPrice = .05900M;
            line.UnitOfMeasure = "EA";
            line.Part = "757026161344";
            line.Description = "006X7-SMN-CLTH-ASRT-000";
            line.SellingShipQty = 6.0M;
            line.PackID = 1210;
            line.InvoiceLineNo = 1;
            line.PackLine = 1;
            i.AddLine(line);
        }
        private void PrintInvoice()
        {
        }
    }
}
