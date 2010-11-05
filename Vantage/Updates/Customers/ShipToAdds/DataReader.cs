using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ShipToLoad
{
    public enum colEx
    {
        soldTo,
        shipTo,
        name,
        center,
        address,
        city,
        state,
        zip,
        country,
        phone,
        fax,
        storeRegion,
        StoreArea,
        StoreGM,
        RDO,
        LPM,
        bUnit
    }
    public enum col
    {
        soldTo,
        shipTo,
        name,
        center,
        address,
        city,
        state,
        zip,
        country,
        phone
    }
    public enum wmgln
    {
        shipTo,
        gln,
        duns,
        storeName,
        storeType,
        address,
        city,
        state,
        zip,
        mailAddress,
        mailCity,
        mailState,
        mailZip,
        phone
    }
    class DataReader
    {
        StreamReader tr;
        ShipToXmanager mgr;

        public DataReader()
        {
            this.mgr = new ShipToXmanager();
            this.standardInsert();
            // this.walmartInsert();  // this loads from a gln listing
        }
        void specialUpdate()
        {
            string dir = "D:/users/rich/data/lc/shipTo/";
            string file1 = "PV_Storelist_6Apr10.txt";
            this.tr = new StreamReader(dir + file1);
            this.processExtended();
        }
        void standardInsert()
        {
            string dir = "D:/users/rich/data/lc/shipTo/";
            string file1 = "LuxShip1.txt";
            string file2 = "LuxShip2.txt";
            tr = new StreamReader(dir + file1);
            processFile();
            tr.Close();
            tr = new StreamReader(dir + file2);
            processFile();
            tr.Close();
        }
        void walmartInsert()
        {
            string glnFile = "D:/users/rich/data/US_gln_locations_Walmart.txt";
            tr = new StreamReader(glnFile);
            AddSelectedShipTo();
        }
        void AddSelectedShipTo()
        { 
            bool addStore = false;
            string line;
            string[] addStores = new string[] {"1066","1088","1095","1260","2715","2717","5079","805","916","970"};
            while ((line = tr.ReadLine()) != null)
            {
                addStore = false;
                string[] split = line.Split(new Char[] { '\t' });
                string shipTo = split[(int)wmgln.shipTo];
                foreach (string store in addStores)
                {
                    if (String.Compare(shipTo, store) == 0)
                    {
                        addStore = true;
                        break;
                    }
                }
                if (addStore)
                {
                    ExShipTo st = new ExShipTo();
                    st.setCustId("90000");
                    st.setShipVia("UGND");
                    st.setShipTo(shipTo);
                    string storeName = "Wal-Mart " + shipTo;
                    st.setName(storeName);
                    st.setCenter(split[(int)wmgln.address]);
                    st.setAddress("");
                    st.setCity(split[(int)wmgln.city]);
                    st.setState(split[(int)wmgln.state]);
                    string phone = split[(int)wmgln.phone];
                    if (phone.Length > 20)
                    {
                        st.setPhone(phone.Substring(0, 20));
                    }
                    else
                    {
                        st.setPhone(phone);
                    }
                    st.setCountryNo("1");
                    st.setZip(split[(int)wmgln.zip]);
                    if (!this.mgr.ShipToExists(st))
                    {
                        this.mgr.addShipTo(st);
                    }
                }
            }
        }
        void processExtended()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                ExShipTo st = new ExShipTo();

                st.setCustId(split[(int)colEx.soldTo]);
                st.setShipTo(split[(int)colEx.shipTo]);
                st.setName(split[(int)colEx.name]);
                st.setCenter(split[(int)colEx.center]);
                st.setAddress(split[(int)colEx.address]);
                st.setCity(split[(int)colEx.city]);
                st.setState(split[(int)colEx.state]);
                st.setZip(split[(int)colEx.zip]);
                st.setCountryNo(split[(int)colEx.country]);
                st.setPhone(split[(int)colEx.phone]);
                st.fax = split[(int)colEx.fax];
                st.businessUnit = split[(int)colEx.bUnit];
                st.lpm = split[(int)colEx.LPM];
                st.rdo = split[(int)colEx.RDO];

                st.setShipVia("UGND");
                st.FixNameCorp();
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
            }
        }
        void processFile()
        {
            string line = "";
            
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                
        		ExShipTo st = new ExShipTo();

                st.setCustId(split[(int)col.soldTo]);
                st.setShipTo(split[(int)col.shipTo]);
                st.setName(split[(int)col.name]);
                st.setCenter(split[(int)col.center]);
                st.setAddress(split[(int)col.address]);
                st.setCity(split[(int)col.city]);
                st.setState(split[(int)col.state]);
                st.setZip(split[(int)col.zip]);
                st.setCountryNo(split[(int)col.country]);
                st.setPhone(split[(int)col.phone]);
                st.setShipVia("UGND");
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
            }
        }
    }
}



