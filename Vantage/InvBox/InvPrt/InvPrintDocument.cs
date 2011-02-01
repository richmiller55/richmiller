#undef trace
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace InvPrt
{
    public class InvPrintDocument : PrintDocument
    {
        private Font printFont;
        Graphics gdiPage;
        private ArrayList ra;
        private Invoice inv;
        ArrayList columns;
        decimal invoiceTotal;
        int lineHeight;
        int leftMargin;
        int rightMargin;
        int topMargin;
        int bottomMargin;
        float fltLeftMargin;
        float fltRightMargin;
        float fltTopMargin;
        float fltBottomMargin;
        
        int count = 0;
        public InvPrintDocument(Invoice inv)
        {
            this.inv = inv;
            invoiceTotal = 0;
            this.PrinterSettings.PrinterName = "Adobe PDF";
            // this.PrinterSettings.PrinterName = "HP LaserJet 4350 PCL 6";
            this.PrinterSettings.PrintFileName = "invoice" + this.inv.InvoiceNo.ToString() + ".pdf";
            this.PrinterSettings.PrintToFile = true;

        }
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            printFont = new Font("Verdana", 10);
        }
        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            printFont.Dispose();
        }
        private ArrayList InfoRectangles(PrintPageEventArgs e)
        {
            ArrayList rectangles = new ArrayList(3);
            int y = Top + (LineHeight * 8);  // 8 lines down
            Int32 width = (Right - Left) / 3;
            int length = LineHeight * 6;
            Rectangle rect = new Rectangle(Left, y, width, length);
            rectangles.Add(rect);

            // recalculate x now in the first third
            int x2 = ((Right - Left) / 3) + Left;
            rect = new Rectangle(x2, y, width, length);
            rectangles.Add(rect);
            int x3 = ((Right - Left) / 3) * 2 + Left;
            rect = new Rectangle(x3, y, width, length);
            rectangles.Add(rect);
            // then stuff them with text render the boxes with a light brush, and 
            InfoBoxes(e, rectangles);
            return rectangles;
        }
        void InfoBoxes(PrintPageEventArgs e, ArrayList rectangles)
        {
            float penSize = 2.0f;
            Pen pen = new Pen(Brushes.Black, penSize);
            Rectangle rect = (Rectangle)rectangles[0];
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.DrawString(InfoRectTextLeft(e), printFont, Brushes.Black, rect);
            rect = (Rectangle)rectangles[1];
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.DrawString(InfoRectTextCenter(e), printFont, Brushes.Black, rect);
            rect = (Rectangle)rectangles[2];
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.DrawString(InfoRectTextRight(e), printFont, Brushes.Black, rect);
            count += 5;
        }
        private string InfoRectTextLeft(PrintPageEventArgs e)
        {
            string crlf = "\n";
            string text = "P O No. " + inv.PoNo + crlf;
            text += "Sales Rep " + inv.SalesRepName1 + crlf;
            text += "Rep Phone " + inv.SalesRepPhone + crlf;

            text += "Packing Slip " + inv.PackID.ToString();
            return text;
        }
        private string InfoRectTextCenter(PrintPageEventArgs e)
        {
            string crlf = "\n";
            string text = "Pay Terms " + inv.PaymentTermsText + crlf;
            text += "Order Dt " + inv.OrderDate.ToShortDateString() + crlf;
            text += "Order No. " + inv.SalesOrder.ToString();
            return text;
        }
        private string InfoRectTextRight(PrintPageEventArgs e)
        {
            string crlf = "\n";
            string text = "Invoice Date " + inv.InvoiceDate.ToShortDateString() + crlf;
            text += "Ship Via " + inv.ShipVia + crlf;
            text += "Tracking " + inv.TrackingNo + crlf;
            return text;
        }
        private Rectangle RectLeftAddress(PrintPageEventArgs e)
        {
            int y = Top + (LineHeight * 2);  // 2 lines down
            int width = (Right - Left) / 2;
            int length = LineHeight * 6;
            return new Rectangle(Left, y, width, length);
        }
        private Rectangle RectRightAddress(PrintPageEventArgs e)
        {
            int x = Left + ((Right - Left) / 2);
            int y = Top + (LineHeight * 2);  // 2 lines down
            int width = (Right - Left) / 2;
            int length = LineHeight * 6;
            return new Rectangle(x, y, width, length);
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            gdiPage = e.Graphics;
            SetPageSize(e);
            SetColumnWidths(e);
            Header(e);
            AddressBoxes(e);
            InfoRectangles(e);
            DetailHeading(e);
            DetailLines(e);
            FreightLine(e);
            TotalLine(e);
            WriteTermsConditions(e);
            e.HasMorePages = false;
        }
        void SetPageSize(PrintPageEventArgs e)
        {
            LineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
            fltLeft = e.MarginBounds.Left;
            fltRight = e.MarginBounds.Right;
            fltTop = e.MarginBounds.Top;
            fltBottom = e.MarginBounds.Bottom;
            
            Left = Convert.ToInt32(e.MarginBounds.Left);
            Right = Convert.ToInt32(e.MarginBounds.Right);
            Top = Convert.ToInt32(e.MarginBounds.Top);
            Bottom = Convert.ToInt32(e.MarginBounds.Bottom);
        }
        void AddressBoxes(PrintPageEventArgs e)
        {
            float penSize = 2.0f;
            Pen pen = new Pen(Brushes.Black, penSize);
            Rectangle rect = RectLeftAddress(e);
            e.Graphics.DrawRectangle(pen, rect);
            e.Graphics.DrawString(inv.SoldTo.AddressStr, printFont, Brushes.Black, rect);
            rect = RectRightAddress(e);
            e.Graphics.DrawRectangle(pen, rect);
            if (inv.ShipToId.CompareTo("") == 0)
            {
                e.Graphics.DrawString(inv.BillTo.AddressStr, printFont, Brushes.Black, rect);
            }
            else
            {
                e.Graphics.DrawString(inv.ShipTo.AddressStr, printFont, Brushes.Black, rect);
            }
        }
        void Header(PrintPageEventArgs e)
        {
            float yPos = 0;
            yPos = fltTop + (count * printFont.GetHeight(e.Graphics));

            e.Graphics.DrawString("California Accessories", printFont, Brushes.Black, leftMargin, yPos);
            string phone = "Phone   510.675.8600";
            SizeF phoneSize = RightJust(e, phone); 
            e.Graphics.DrawString(phone , printFont, Brushes.Black, fltRight - phoneSize.Width, yPos);
            HeadingInvoice(e);
            count += 7; // skip lines where the address boxes go
        }
        void HeadingInvoice(PrintPageEventArgs e)
        {
            float currentSize = printFont.SizeInPoints;
            currentSize += 1;
            Font largerFont = new Font(printFont.Name, currentSize);

            float yPos = fltTop + (count++ * largerFont.GetHeight(e.Graphics));
            string invoice = "Original Invoice " + inv.InvoiceNo.ToString();
            SizeF size = e.Graphics.MeasureString(invoice, largerFont);
            float xPos = ((fltRight - fltLeft) / 2) - (size.Width / 2);
            e.Graphics.DrawString(invoice, largerFont, Brushes.Black, xPos, yPos);
        }
        void SetColumnWidths(PrintPageEventArgs e)
        {
            columns = new ArrayList();
            float width = fltRight - fltLeft;
            columns.Add(width * .07f);
            columns.Add(width * .30f);
            columns.Add(width * .65f);
            columns.Add(width * .70f);
            columns.Add(width * .84f);
        }
        void Totals(PrintPageEventArgs e)
        {
        }
        void DetailHeading(PrintPageEventArgs e)
        {
            float yPos = 0;

            float width = fltRight - fltLeft;
            float column = width / 6;
            count += 2;
            yPos = fltTop + (count++ * printFont.GetHeight(e.Graphics));

            int colNo = 0;
            e.Graphics.DrawString("Line", printFont, Brushes.Black, fltLeft, yPos);
            e.Graphics.DrawString("Style", printFont, Brushes.Black, fltLeft  + (float)columns[colNo++], yPos);
            e.Graphics.DrawString("Description", printFont, Brushes.Black, fltLeft + (float)columns[colNo++], yPos);

            SizeF textSize = RightJust(e, "Qty");
            e.Graphics.DrawString("Qty", printFont, Brushes.Black,
                                   fltLeft + (float)columns[colNo++] + column - textSize.Width, yPos);
            textSize = RightJust(e, "Price");
            e.Graphics.DrawString("", printFont, Brushes.Black,
                                  fltLeft + (float)columns[colNo++] + column - textSize.Width, yPos);
            textSize = RightJust(e, "Ext Price");
            e.Graphics.DrawString("Ext Price", printFont, Brushes.Black,
                                  fltLeft + (float)columns[colNo++] + column  - textSize.Width, yPos);
        }
        void FreightLine(PrintPageEventArgs e)
        {
            if (inv.FreightCharge > 0)
            {
                this.InvoiceTotal += inv.TotalFreight;
                float column = (fltRight - fltLeft) / 6;
                int labelColumn = 1;
                string descr = "Freight";
                int lastColNo = columns.Count - 1;

                SizeF textSize = RightJust(e, inv.TotalFreight.ToString("#,###,##0.00"));
                float xPos = fltLeft + (float)columns[lastColNo] + column - textSize.Width;
                float yPos = fltTop + (count++ * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(descr, printFont, Brushes.Black,
                                      fltLeft + (float)columns[labelColumn], yPos);
                e.Graphics.DrawString(inv.TotalFreight.ToString("#,###,##0.00"), printFont, Brushes.Black,
                                      xPos, yPos);
            }
        }
        void TotalLine(PrintPageEventArgs e)
        {
            float column = (fltRight - fltLeft) / 6;
            string descr = "Invoice Total";
            int labelColumn = 1;

            int lastColNo = columns.Count - 1;
            SizeF textSize = RightJust(e, InvoiceTotal.ToString("#,###,##0.00"));
            float xPos = fltLeft + (float)columns[lastColNo] + column - textSize.Width;
            float yPos = fltTop + (count++ * printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString(descr, printFont, Brushes.Black,
                                  fltLeft + (float)columns[labelColumn], yPos);
            e.Graphics.DrawString(InvoiceTotal.ToString("#,###,##0.00"), printFont, Brushes.Black,
                                  xPos, yPos);
        }
        void DetailLines(PrintPageEventArgs e)
        {
            float yPos = 0;
            float column = (fltRight - fltLeft) / 6;
            
            foreach (InvLine l in inv.Lines)
            {
                int colNo = 0;
                yPos = fltTop + (count++ * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(l.InvoiceLineNo.ToString(), printFont, Brushes.Black, fltLeft, yPos);
                e.Graphics.DrawString(l.Part, printFont, Brushes.Black, fltLeft + (float)columns[colNo++], yPos);
                e.Graphics.DrawString(l.PartDescription, printFont, Brushes.Black, fltLeft + (float)columns[colNo++], yPos);

                SizeF textSize = RightJust(e, l.SellingShipQty.ToString("#,###,##0.00"));
                float xPos = fltLeft + (float)columns[colNo++] + column - textSize.Width;
                e.Graphics.DrawString(l.SellingShipQty.ToString("#,###,##0"), printFont, 
                                      Brushes.Black, xPos , yPos);

                textSize = RightJust(e, l.UnitPrice.ToString("#,###,##0.00"));
                xPos = fltLeft + (float)columns[colNo++] + column - textSize.Width;
                e.Graphics.DrawString(l.UnitPrice.ToString("#,###,##0.00"), printFont, Brushes.Black, 
                                      xPos, yPos);

                textSize = RightJust(e, l.ExtPrice.ToString("#,###,##0.00"));
                xPos = fltLeft + (float)columns[colNo++] + column - textSize.Width;
                e.Graphics.DrawString(l.ExtPrice.ToString("#,###,##0.00"), printFont, Brushes.Black,
                                      xPos, yPos);
                this.InvoiceTotal += l.ExtPrice;
            }
        }
        private int LineHeight
        {
            get
            {
                return lineHeight;
            }
            set
            {
                lineHeight = value;
            }
        }
        private int Left
        {
            get
            {
                return leftMargin;
            }
            set
            {
                leftMargin = value;
            }
        }
        private int Right
        {
            get
            {
                return rightMargin;
            }
            set
            {
                rightMargin = value;
            }
        }
        private int Top
        {
            get
            {
                return topMargin;
            }
            set
            {
                topMargin = value;
            }
        }
        private int Bottom
        {
            get
            {
                return bottomMargin;
            }
            set
            {
                bottomMargin = value;
            }
        }
        private float fltLeft
        {
            get
            {
                return fltLeftMargin;
            }
            set
            {
                fltLeftMargin = value;
            }
        }
        private float fltRight
        {
            get
            {
                return fltRightMargin;
            }
            set
            {
                fltRightMargin = value;
            }
        }
        private float fltTop
        {
            get
            {
                return fltTopMargin;
            }
            set
            {
                fltTopMargin = value;
            }
        }
        private float fltBottom
        {
            get
            {
                return fltBottomMargin;
            }
            set
            {
                fltBottomMargin = value;
            }
        }
        private decimal InvoiceTotal
        {
            get
            {
                return invoiceTotal;
            }
            set
            {
                invoiceTotal = value;
            }
        }
        void FillSoldTo(PrintPageEventArgs e)
        {
            string address = inv.SoldTo.AddressStr;
        }
        SizeF RightJust(PrintPageEventArgs e, string number)
        {
            return e.Graphics.MeasureString(number, printFont);
        }
        void WriteTermsConditions(PrintPageEventArgs e)
        {
            float currentSize = printFont.SizeInPoints;
            currentSize -= 1;
            Font smallerFont = new Font(printFont.Name, currentSize);
            int lineHeight = Convert.ToInt32(smallerFont.GetHeight(gdiPage));
            int linesFromBottom = 7;
            int y = Bottom - (lineHeight * linesFromBottom);
            int width = Right - Left;
            int length = lineHeight * 7;
            Rectangle rect = new Rectangle(Left, y, width, length);

            TermsConditions tc = new TermsConditions();
            e.Graphics.DrawString(tc.InvoiceTerms, smallerFont, Brushes.Black, rect);
        }
#if trace
#endif
        public ArrayList ReportArray
        {
            get
            {
                return ra;
            }
            set
            {
                ra = value;
            }
        }
    }
}
