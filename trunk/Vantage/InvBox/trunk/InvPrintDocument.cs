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
        public InvPrintDocument(ArrayList report)
        {
            this.ra = report;
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
        private Rectangle RectLeftAddress(PrintPageEventArgs e)
        {
            Int32 lineHeight = Convert.ToInt32(printFont.GetHeight(gdiPage));
            Int32 leftMargin = Convert.ToInt32( e.MarginBounds.Left);
            Int32 rightMargin = Convert.ToInt32(e.MarginBounds.Right);
            Int32 topMargin =  Convert.ToInt32(e.MarginBounds.Top);
            int x = leftMargin + 5;
            int y = topMargin + (lineHeight * 6);  // 6 lines down
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

            Int32 x =  leftMargin + ((leftMargin - rightMargin) / 2); 
            Int32 y = topMargin + (lineHeight * 6);  // 6 lines down
            Int32 width = (rightMargin - leftMargin) / 2;
            Int32 length = lineHeight * 6;
            Rectangle rect = new Rectangle(x, y, width, length);
            return rect;
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            gdiPage = e.Graphics;
            float yPos = 0;

            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float topMargin = e.MarginBounds.Top;
            float bottomMargin = e.MarginBounds.Bottom;
            float lineHeight = printFont.GetHeight(gdiPage);
            float linesPerPage = e.MarginBounds.Height / lineHeight;
            int count = 0;
            int totalLinesPrinted = 0;
            float penSize = 2.0f;
            Pen pen = new Pen(Brushes.Black, penSize);
            Rectangle rect = RectLeftAddress(e);
            e.Graphics.DrawRectangle(pen, rect);
            rect = RectRightAddress(e);
            e.Graphics.DrawRectangle(pen, rect);

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
        }
    }
}
