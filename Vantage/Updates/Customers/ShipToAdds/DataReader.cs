using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ShipToLoad
{
    public enum wmedi
    {
        shipTo,
        name,
        gln,
        address1,
        city,
        state,
        zip
    }

    public enum e_LOR
    {
        soldTo,
        shipTo,
        name,
        address1,
        address2,
        address3,
        city,
        state,
        zip,
        country,
        phone
    }

    public enum col
    {
        soldTo,
        shipTo,
        name,
        address1,
        address2,
        city,
        state,
        zip,
        country,
        phone
    }
    public enum wm2Rslt
    {
        soldTo,
        shipTo,
        CityState,
        storeType,
        address1,
        city,
        state,
        zip,
        country,
        filler
    }
    public enum wmShip
    {
        soldTo,
        shipTo,
        name,
        storeType,
        address1,
        city,
        state,
        zip,
        country,
        filler
    }
    class DataReader
    {
        StreamReader tr;
        ShipToXman mgr;

        public DataReader()
        {
            this.mgr = new ShipToXman();
            this.luxRetailInsert();
            // this.searsInsert();
            // this.wmInsert();  // for DC adds
            // ResultsFromWm2();
            // this.luxRetailFix();
            int insertTotal = this.mgr.NewShipTo;
        }
        void luxRetailInsert()
        {
            string dir = "I:/edi/shipTo/";
            string file1 = "LuxShip1.txt";
            string file2 = "LuxShip2.txt";
            string file3 = "LuxShip3.txt";
            tr = new StreamReader(dir + file1);
            processFile();
            tr.Close();
            tr = new StreamReader(dir + file2);
            processFile();
            tr.Close();
            tr = new StreamReader(dir + file3);
            processFileLOR();
            tr.Close();
        }
        void luxRetailFix()
        {
            string dir = "I:/edi/shipTo/";
            string file1 = "LuxCorrect.txt";
            tr = new StreamReader(dir + file1);
            processFileFix();
            tr.Close();
        }
        void searsInsert()
        {
            string dir = "I:/edi/shipTo/";
            string file1 = "Sears_28Feb2011.txt";
            tr = new StreamReader(dir + file1);
            processFile();
            tr.Close();
        }
        void wmInsert()
        {
            string dir = @"I:\edi\shipto\";
            string file1 = "results.txt";

            tr = new StreamReader(dir + file1);
            processEDI();
            tr.Close();
        }
        void processFile()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = split[(int)col.soldTo];
                st.ShipToId = split[(int)col.shipTo];
                st.Name = split[(int)col.name];
                
                st.Address1 = split[(int)col.address1];
                st.Address2 = split[(int)col.address2];
            // st.Address3 = split[(int)col.address3];

                st.City = split[(int)col.city];
                st.State = split[(int)col.state];
                st.Zip = split[(int)col.zip];
                st.Country = split[(int)col.country];
                st.Phone = split[(int)col.phone];
                st.ShipVia = "UGND";
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
            }
        }
        void processFileLOR()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = split[(int)e_LOR.soldTo];
                st.ShipToId = split[(int)e_LOR.shipTo];
                st.Name = split[(int)e_LOR.name];

                st.Address1 = split[(int)e_LOR.address1];
                st.Address2 = split[(int)e_LOR.address2];
                st.Address3 = split[(int)e_LOR.address3];

                st.City = split[(int)e_LOR.city];
                st.State = split[(int)e_LOR.state];
                st.Zip = split[(int)e_LOR.zip];
                st.Country = split[(int)e_LOR.country];
                st.Phone = split[(int)e_LOR.phone];
                st.ShipVia = "UGND";
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
            }
        }
        void processFileFix()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = split[(int)col.soldTo];
                st.ShipToId = split[(int)col.shipTo];
                st.Name = split[(int)col.name];

                st.Address1 = split[(int)col.address1];
                st.Address2 = split[(int)col.address2];
                // st.Address3 = split[(int)col.address3];

                st.City = split[(int)col.city];
                st.State = split[(int)col.state];
                st.Zip = split[(int)col.zip];
                st.Country = split[(int)col.country];
                st.Phone = split[(int)col.phone];
                st.ShipVia = "UGND";
                if (this.mgr.ShipToExists(st))
                {
                    this.mgr.UpdateShipTo(st);
                }
            }
        }
        void processFileWM()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = split[(int)wmShip.soldTo];
                st.ShipToId = split[(int)wmShip.shipTo];
                st.Name = "WalMart " + split[(int)wmShip.storeType] + " " + st.ShipToId;
                st.Address1 = split[(int)wmShip.address1];
                st.City = split[(int)wmShip.city];
                st.State = split[(int)wmShip.state];
                st.Zip = split[(int)wmShip.zip];
                st.Country = split[(int)wmShip.country];
                st.Phone = "";
                st.ShipVia = "UGND";
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
                else
                {
                    this.mgr.UpdateShipTo(st);
                }
            }
        }

        void ResultsFromWm2()
        {
            string dir = @"I:\edi\shipto\";
            string file1 = "results.txt";
            tr = new StreamReader(dir + file1);
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = split[(int)wm2Rslt.soldTo];
                st.ShipToId = split[(int)wm2Rslt.shipTo];
                st.Name = "WalMart " + split[(int)wm2Rslt.storeType] + " " + st.ShipToId;
                st.Address1 = split[(int)wm2Rslt.address1];
                st.City = split[(int)wm2Rslt.city];
                st.State = split[(int)wm2Rslt.state];
                st.Zip = split[(int)wm2Rslt.zip];
                st.Country = split[(int)wm2Rslt.country];
                st.Phone = "";
                st.ShipVia = "UGND";
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
                else
                {
                    this.mgr.UpdateShipTo(st);
                }
            }
            tr.Close();
        }
        void processEDI()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ShipTo st = new ShipTo();
                st.CustId = "90000";
                st.ShipToId = split[(int)wmedi.shipTo];
                st.Name = split[(int)wmedi.name];
                st.Address1 = split[(int)wmedi.address1];
                st.City = split[(int)wmedi.city];
                st.State = split[(int)wmedi.state];
                st.Zip = split[(int)wmedi.zip];
                st.Country = "US";
                st.Phone = "";
                st.ShipVia = "WMLT";
                
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
                else
                {
                    this.mgr.UpdateShipTo(st);
                }
            }
        }
    }
}



