using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace InvBox
{
    public partial class PrtForm : Form
    {
        ArrayList ra = new ArrayList();
        Font printFont = new Font("Arial", 10);
        public PrtForm()
        {
            InitializeComponent();
        }
        public PrtForm(ArrayList report)
        {
            InitializeComponent();
            this.ra = report;
            this.PrintIt();
        }
        public void PrintIt()
        {
            try
            {
                printDocument1.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                printDocument1.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName =
                    "P4515PCL6_100";


                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            int totalLinesPrinted = 0;
            foreach (string line in this.ra)
            {
                if (count < linesPerPage)
                {
                    yPos = topMargin + (count *
                       printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black,
                       leftMargin, yPos, new StringFormat());
                    count++;
                }
                totalLinesPrinted += count;
                count = 0;
                // If more lines exist, print another page.
                if (totalLinesPrinted < this.ra.Count)
                    ev.HasMorePages = true;
                else
                    ev.HasMorePages = false;
            }

        }
    }
}



