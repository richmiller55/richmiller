using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InvBox
{
    public class ExReport
    {
        public SortedDictionary<string,string> messages;
        public bool evenRow = true;
        public int messageNumber = 100;
        int messCount = 100;
        string messPrefix = "ExReport_";
        protected string statFileName = "InvBoxStatus.html";
        protected string statDir = @"d:\users\message\";
        public ExReport()
        {
            messages = new SortedDictionary<string,string>();
            SavePriorStatFile();

            AddMessage(GetNextMessageKey(), "Message Reporter Started");
            UpdatePage();
        }
        public void AddMessage(string key, string message)
        {
            string compositeKey = "a_" + messageNumber.ToString() + "_" + key;
            messageNumber += 1;
            messages.Add(compositeKey, message);
        }
        private void RefreshStatusPage()
        {
            string fileName = this.statDir + this.statFileName;
            using (StreamWriter outfile = new StreamWriter(fileName, false))
            {
                outfile.Write(this.WebPage());
                outfile.Close();
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
            html.Append(@"<body style=""background-color: #f0fff0; font-family: Courier; font-size: 8pt;"">>");
            html.Append(this.TableStart());
            html.Append(this.LoopRows());
            return html.ToString();
        }
        private string TableStart()
        {
            return @"<Table width=90%>";
        }
        private string LoopRows()
        {
            StringBuilder html = new StringBuilder(2000);
            foreach (KeyValuePair<string, string> de in messages)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                html.Append(TableRow(de));
            }
            return html.ToString();
        }
        private string TableRow(KeyValuePair<string, string> de)
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
        private void SavePriorStatFile()
        {
            string fullName = this.statDir + this.statFileName;
            string fileName = Path.GetFileName(fullName);
            string prefix = Path.GetFileNameWithoutExtension(fullName);
            string dumpPath = @"D:\users\rich\ProcessedFrt";
            DateTime now = DateTime.Now;
            string date = now.Year.ToString("0000") + now.Month.ToString("00") + now.Day.ToString("00");
            string time = now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00");
            string newFileName = prefix + "_" + date + "_" + time + ".txt";
            File.Move(fullName, dumpPath + "\\" + newFileName);
            string message = "Saved Old Stats file " + newFileName;
            AddMessage(GetNextMessageKey(), message);
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
        public string GetNextMessageKey()
        {
            string messKey = this.messPrefix + this.messCount.ToString();
            this.messCount += 1;
            return messKey;
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

