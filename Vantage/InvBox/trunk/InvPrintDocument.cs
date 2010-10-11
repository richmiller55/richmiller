#undef trace
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace InvBox
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
        int count = 0;
        public InvPrintDocument(Invoice inv)
        {
            this.inv = inv;
            invoiceTotal = 0;
            this.PrinterSettings.PrinterName = "Adobe PDF";
//            this.PrinterSettings.PrinterName = "HP LaserJet 4350 PCL 6";
            this.PrinterSettings.PrintFileName = "rich.pdf";
            
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
            float penSize = 1.0f;
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
            text += "Packing Slip " + inv.PackID.ToString();
            return text;
        }
        private string InfoRectTextCenter(PrintPageEventArgs e)
        {
            string crlf = "\n";
            string text = "Payment Terms " + crlf + inv.PaymentTermsText + crlf;
            text += "Order Dt " + inv.OrderDate.ToShortDateString() + crlf;
            text += "Order No. " + inv.SalesOrder.ToString();
            return text;
        }
        private string InfoRectTextRight(PrintPageEventArgs e)
        {
            string crlf = "\n";
            string text = "Invoice Date " + inv.InvoiceDate.ToShortDateString() + crlf;
            text += "Ship Via " + inv.ShipVia + crlf;
            text += "Ship Date " + crlf;
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
            WriteTermsConditions(e);
            e.HasMorePages = false;
        }
        void SetPageSize(PrintPageEventArgs e)
        {
            LineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
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
            e.Graphics.DrawString(inv.BillTo.AddressStr, printFont, Brushes.Black, rect);
        }
        void Header(PrintPageEventArgs e)
        {
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));

            e.Graphics.DrawString("California Accessories", printFont, Brushes.Black, leftMargin, yPos);
            string phone = "Phone   510.352.4774";
            SizeF phoneSize = RightJust(e, phone); 
            e.Graphics.DrawString(phone , printFont, Brushes.Black, rightMargin - phoneSize.Width, yPos);
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString("Invoice " + inv.InvoiceNo.ToString(), printFont, Brushes.Black, leftMargin, yPos);
            count += 7; // skip lines where the address boxes go
        }
        void SetColumnWidths(PrintPageEventArgs e)
        {
            columns = new ArrayList();
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            float width = rightMargin - leftMargin;
            columns.Add(width * .07f);
            columns.Add(width * .30f);
            columns.Add(width * .65f);
            columns.Add(width * .70f);
            columns.Add(width * .80f);
        }
        void Totals(PrintPageEventArgs e)
        {
        }
        void DetailHeading(PrintPageEventArgs e)
        {
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            float width = rightMargin - leftMargin;
            float column = (rightMargin - leftMargin) / 6;
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));

            int colNo = 0;
            e.Graphics.DrawString("Line", printFont, Brushes.Black, leftMargin, yPos);
            e.Graphics.DrawString("Style", printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);
            e.Graphics.DrawString("Descr", printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);

            e.Graphics.DrawString("Qty", printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);
            SizeF result = RightJust(e, "Unit Price");
            e.Graphics.DrawString("Unit Price", printFont, Brushes.Black,
                                  leftMargin + (float)columns[colNo++] + column - result.Width, yPos);
            result = RightJust(e, "Ext Price");
            e.Graphics.DrawString("Ext Price", printFont, Brushes.Black,
                                  leftMargin + (float)columns[colNo++] + column  - result.Width, yPos);
        }
        void DetailLines(PrintPageEventArgs e)
        {
        }
        void DetailLinesOld(PrintPageEventArgs e)
        {
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            float column = (rightMargin - leftMargin) / 6;
            
            foreach (InvLine l in inv.Lines)
            {
                int colNo = 0;
                yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(l.InvoiceLineNo.ToString(), printFont, Brushes.Black, leftMargin, yPos);
                e.Graphics.DrawString(l.Part, printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);
                e.Graphics.DrawString(l.Description, printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);
                e.Graphics.DrawString(l.SellingShipQty.ToString(), printFont, Brushes.Black, leftMargin + (float)columns[colNo++], yPos);

                SizeF result = RightJust(e, l.UnitPrice.ToString("#,###,##0.00"));
                // extra column right justtify adjustment
                float xPos = leftMargin + (float)columns[colNo++] + column - result.Width; 
                e.Graphics.DrawString(l.UnitPrice.ToString(), printFont, Brushes.Black, 
                                      xPos, yPos);
                result = RightJust(e, l.ExtPrice.ToString("#,###,##0.00"));
                xPos = leftMargin + (float)columns[colNo++] + column - result.Width;
                e.Graphics.DrawString(l.ExtPrice.ToString("#,###,##0.00"), printFont, Brushes.Black, 
                                      xPos , yPos);
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
