using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InvBox
{
    public class ExReport
    {
        public Hashtable messages;
        public bool evenRow = true;
        public ExReport()
        {
            messages = new Hashtable();
            AddMessage("A1", "Logging Started");
            UpdatePage();
        }
        public void AddMessage(string key, string message)
        {
            messages.Add(key, message);
        }
        private void RefreshStatusPage()
        {
            string fileName = "InvBoxStatus.html";
            using (StreamWriter outfile = new StreamWriter(fileName, false))
            {
                outfile.Write(this.WebPage());
            }
        }
        private string WebPage()
        {
            StringBuilder html = new StringBuilder(5000);
            html.Append(this.Header());
            html.Append(this.Body());
            return html.ToString();
        }
        private string Header()
        {
            StringBuilder html = new StringBuilder(2000);
            html.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 3.2//EN\">");
            html.Append(@"<htlm><head><title>Invoice In a box</title>");
            html.Append(this.StyleSheet());
            html.Append("</head>");
            return html.ToString();
        }
        private string StyleSheet()
        {
            StringBuilder style = new StringBuilder(500);
            return style.ToString();
        }
        private string StyleSheetNoEmbed()
        {
            StringBuilder style = new StringBuilder(500);
            string fileName = "styleSheet.css";
            bool goAhead = true;
            StreamReader tr;
            try
            {
                tr = new StreamReader(fileName);
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    style.Append(line);
                }
            }
            catch (Exception e)
            {
                string message = e.Message;
                goAhead = false;
            }
            if (goAhead)
            {
                return style.ToString();
            }
            else
            {
                return "NoStyle";
            }
        }
        private string Body()
        {
            StringBuilder html = new StringBuilder(5000);
            html.Append(@"<body style=""background-color: #f0fff0; font-family: Verdana; font-size: 8pt;"">>");
            html.Append(this.TableStart());
            html.Append(this.LoopRows());
            return html.ToString();
        }
        private string TableStart()
        {
            return @"<Table width=50%>";
        }
        private string LoopRows()
        {
            StringBuilder html = new StringBuilder(2000);
            foreach (DictionaryEntry de in messages)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                TableRow(de);
            }
            return html.ToString();
        }
        private string TableRow(DictionaryEntry de)
        {
            StringBuilder html = new StringBuilder(200);
            string evenTR = @"<tr style=""background-color: #FFFACD;"">";
            string oddTR  = @"<tr style=""background-color: #ADD8E6;"">";
            string prefix = @"<td>";
            string suffix = @"</td>";
            if (this.evenRow.CompareTo(true) == 0)
            {
                html.Append(evenTR);
                this.evenRow = false;
            }
            else
            {
                html.Append(oddTR);
                this.evenRow = true;
            }
            string tdKey = prefix + de.Key.ToString() + suffix;
            html.Append(tdKey);
            string tdValue = prefix + de.Value.ToString() + suffix;
            html.Append(tdValue);
            html.Append("</tr>");
            return html.ToString();
        }
        public void UpdatePage()
        {
            this.RefreshStatusPage();
        }
        public void SerializeReport()
        {
        }
        public System.DateTime convertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }
    }
}
        /*
        public string accessor
        {
            get
            {
                return totalWeight;
            }
            set
            {
                totalWeight = value;
            }
        }  */

