using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace InvBox
{
    public enum fedEx
    {
        trackingNo,
        packSlipNo,
        shipDate,
        serviceClass,
        orderNo,
        weight,
        charge,
        tranType
    }
    public enum ups
    {
        trackingNo,
        packSlipNo,
        shipDate,
        serviceClass,
        weight,
        charge,
        tranType,
        xnumber,
        funnyInt,
        frtBilledTo
    }
    class UPSReader
    {
        string fullPath = @"D:\users\UPS";
        string dumpPath = @"D:\users\rich\ProcessedFrt";
        StreamReader tr;
        ShipMgr m_shipMgr;
        string packSlipStr;
        Epicor.Mfg.Core.Session session;
        public UPSReader(Epicor.Mfg.Core.Session vanSession)
        {
            this.session = vanSession;
            this.m_shipMgr = new ShipMgr();
            // insert try catch block here
            string[] filePaths = Directory.GetFiles(this.fullPath);
            foreach (string fileName in filePaths)
            {
                try
                {
                    tr = new StreamReader(fileName);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
                ProcessFile();
                tr.Close();
                MoveFile(fileName);
                InvoiceShipment();
            }
        }

        public ShipMgr GetShipMgr() { return m_shipMgr; }

        private void MoveFile(string fullName)
        {
            string fileName = Path.GetFileName(fullName);
            string prefix = Path.GetFileNameWithoutExtension(fullName);
            
            DateTime now = DateTime.Now;
            string date = now.Year.ToString("0000") + now.Month.ToString("00") + now.Day.ToString("00");
            string time = now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00");
            File.Move(fullName, dumpPath + "\\" + prefix + "_" + date + "_" + time + ".txt");
        }

        private void InvoiceShipment()
        {
            CAInvoice cainv = new CAInvoice(this.session, "RLM85", this.packSlipStr,GetShipMgr());
        }
        private void ProcessFile()
        {
            string linePre = "";
            
            while ((linePre = tr.ReadLine()) != null)
            {
                string line = linePre.Replace("\"", "");
                string[] split = line.Split(new Char[] { ',' });
                int result = split[0].CompareTo("");
                if (result == 0) continue;

                this.packSlipStr = split[(int)ups.packSlipNo];
                
                result = packSlipStr.CompareTo("po"); // to do modifiy for ups heading
                if (result == 0) continue;

                result = packSlipStr.CompareTo("");
                if (result == 0) continue;

                int packSlip = Convert.ToInt32(packSlipStr);                                
                string trackingNo = split[(int)ups.trackingNo];
                
                string shipDateStr = split[(int)ups.shipDate];

                System.DateTime shipDate = convertStrToDate(shipDateStr);
                string serviceClass = split[(int)ups.serviceClass];

                // string orderStr = split[(int)ups.orderNo];
                // int orderNo = 0;
                // if (orderStr.Length > 0)
                //{
                  //  orderNo = Convert.ToInt32(orderStr);
                //}
                int orderNo = 656565; 
                string weightStr = split[(int)ups.weight];
                decimal weight = Convert.ToDecimal(weightStr);

                string chargeStr = split[(int)ups.charge];
                decimal charge = Convert.ToDecimal(chargeStr);
                decimal zero = 0.0M;
                result = charge.CompareTo(zero);
                // if (result == 0) continue;  // if collect then do not process

                string tranType = split[(int)ups.tranType];
                if (tranType.CompareTo("N") == 0)
                {
                    m_shipMgr.AddShipmentLine(packSlip,trackingNo,shipDate,
                                serviceClass,orderNo,weight,charge);
                }
                else
                {
                    m_shipMgr.RemoveShipmentLine(packSlip, trackingNo);
                }
            }
            m_shipMgr.ShipmentComplete(); // is this step needed?
            // Call ARInvoice, 
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



