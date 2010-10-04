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
        int count = 0;
        public InvPrintDocument(Invoice inv)
        {
            
            this.inv = inv;
            this.PrinterSettings.PrinterName = "Adobe PDF";
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
        
        private ArrayList InfoRectangles(PrintEventArgs e)
        {
            ArrayList rectangles = new ArrayList(3);
            Int32 lineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
            Int32 leftMargin = Convert.ToInt32(e.MarginBounds.Left);
            Int32 rightMargin = Convert.ToInt32(e.MarginBounds.Right);
            Int32 topMargin = Convert.ToInt32(e.MarginBounds.Top);
            int x = leftMargin;
            int y = topMargin + (lineHeight * 2);  // 2 lines down
            Int32 width = (rightMargin - leftMargin) / 3;
            int length = lineHeight * 6;
            Rectangle rect = new Rectangle(x, y, width, length);
            rectangles.Add(rect);
            // recalculate x now in the first third
            int x2 = (rightMargin - leftMargin) / 3;
            rect = new Rectangle(x2, y, width, length);
            retangles.Add(rect);
            int x3 = x2 * 2;
            rect = new Rectangle(x3, y, width, length);
            rectangles.Add(rect);
            // then stuff them with text render the boxes with a light brush, and 
            InfoRectText(e, rectangles);
            return rectangles;
        }
        private void InfoRectText(PrintPageEventArgs e, ArrayList rectangles)
        {
            Rectangle rect1 = rectangles[0];
            string crlf = "\n";
            string invDate = "Invoice Date " + inv.InvoiceDate.ToShortDateString();
            string PoNumber = "P O No. " + inv.PoNo;
            string SalesRep = "Sales Rep " + inv.SalesRepName1;
        }
        private Rectangle RectLeftAddress(PrintPageEventArgs e)
        {
            Int32 lineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
            Int32 leftMargin = Convert.ToInt32(e.MarginBounds.Left);
            Int32 rightMargin = Convert.ToInt32(e.MarginBounds.Right);
            Int32 topMargin = Convert.ToInt32(e.MarginBounds.Top);
            int x = leftMargin;
            int y = topMargin + (lineHeight * 2);  // 2 lines down
            Int32 width = (rightMargin - leftMargin) / 2;
            int length = lineHeight * 6;
            Rectangle rect = new Rectangle(x, y, width, length);

            return rect;
        }
        private Rectangle RectRightAddress(PrintPageEventArgs e)
        {
            Int32 lineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
            Int32 leftMargin = Convert.ToInt32(e.MarginBounds.Left);
            Int32 rightMargin = Convert.ToInt32(e.MarginBounds.Right);
            Int32 topMargin = Convert.ToInt32(e.MarginBounds.Top);

            Int32 x = leftMargin + ((rightMargin - leftMargin) / 2);
            Int32 y = topMargin + (lineHeight * 2);  // 2 lines down
            Int32 width = (rightMargin - leftMargin) / 2;
            Int32 length = lineHeight * 6;
            Rectangle rect = new Rectangle(x, y, width, length);
            return rect;
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            gdiPage = e.Graphics;

            SetColumnWidths(e);
            AddressBoxes(e);
            Header(e);
            DetailHeading(e);
            DetailLines(e);
            e.HasMorePages = false;
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
        void FillSoldTo(PrintPageEventArgs e)
        {
            string address = inv.SoldTo.AddressStr;
        }
        SizeF RightJust(PrintPageEventArgs e, string number)
        {
            return e.Graphics.MeasureString(number, printFont);
        }
        void Header(PrintPageEventArgs e)
        {
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));

            e.Graphics.DrawString("California Accessories", printFont, Brushes.Black, leftMargin, yPos);
            e.Graphics.DrawString("Phone   510.352.4774", printFont, Brushes.Black, rightMargin - 100, yPos);
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString("Invoice " + inv.InvoiceNo.ToString(), printFont, Brushes.Black, leftMargin, yPos);
            count += 7; // skip lines where the address boxes go
            yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));
            e.Graphics.DrawString("Date " + inv.InvoiceDate.ToString(), printFont, Brushes.Black, leftMargin, yPos);
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
            columns.Add(width * .75f);
            columns.Add(width * .90f);
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
            }
        }

#if trace
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float topMargin = e.MarginBounds.Top;
            float bottomMargin = e.MarginBounds.Bottom;
            float lineHeight = printFont.GetHeight(gdiPage);
            
            float linesPerPage = e.MarginBounds.Height / lineHeight;
            count = 0;
            int totalLinesPrinted = 0;


            foreach (string line in this.ra)
            {
                if (count < linesPerPage)
                {
                    yPos = topMargin + (count++ * printFont.GetHeight(e.Graphics));
                    e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos);
                    totalLinesPrinted++;
                }
                else
                {
                    count = 0;
                }
                if (totalLinesPrinted < this.ra.Count)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }

        void Detail()
        {
            foreach (InvLine l in i.Lines)
            {
                string strOut = l.InvoiceLineNo.ToString() + "     ";
                strOut += l.SellingShipQty.ToString() + "       ";
                strOut += l.Part + "         ";
                strOut += l.Description + "      ";
                strOut += l.UnitPrice + "    ";
                strOut += l.UnitOfMeasure + "    ";
                strOut += l.Discount.ToString() + "   ";
                strOut += l.ExtPrice.ToString();
                ra.Add(strOut);
            }
        }
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
