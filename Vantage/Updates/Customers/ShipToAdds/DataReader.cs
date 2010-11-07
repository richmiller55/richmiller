using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ShipToLoad
{
    public enum col
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
    class DataReader
    {
        StreamReader tr;
        ShipToXman mgr;

        public DataReader()
        {
            this.mgr = new ShipToXman();
            this.standardInsert();
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
                st.Address3 = split[(int)col.address3];

                st.City = split[(int)col.city];
                st.State = split[(int)col.state];
                st.Zip =  split[(int)col.zip];
                st.Country = split[(int)col.country];
                st.Phone =  split[(int)col.phone];
                st.ShipVia = "UGND";
                if (!this.mgr.ShipToExists(st))
                {
                    this.mgr.addShipTo(st);
                }
            }
        }
    }
}



