using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ShipToLoad
{
    class XmlReader
    {
        public string fileName;
        string currentElement;
        ShipToXman mgr;
        public XmlReader(string dir, string file)
        {
            this.mgr = new ShipToXman();
            fileName = dir + file;
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
        public void runIt()
        {
            XmlTextReader reader = new XmlTextReader(fileName);
            ShipTo st = new ShipTo();
            st.CustId = "90000";
            st.ShipVia = "WMLT";
            st.CountryNo = 1;
            st.Country = "USA";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        currentElement = reader.Name;
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        switch (currentElement)
                        {
                            case "city":
                                st.City = reader.Value;
                                break;
                            case "state":
                                st.State = reader.Value;
                                break;
                            case "zipCode":
                                st.Zip = reader.Value;
                                break;
                            case "StoreNum":
                                st.ShipToId = reader.Value;
                                break;
                            case "Address1":
                                st.Address1 = reader.Value;
                                break;
                            case "ShipViaCode":
                                st.ShipVia = "WMLT";
                                break;
                            case "Address2":
                                st.Address2 = reader.Value;
                                break;
                            case "StoreName":
                                st.Name = reader.Value;
                                break;
                            case "Country":
                                st.Country = reader.Value;
                                break;
                            case "phoneNum":
                                st.Phone = reader.Value;
                                break;
                            case "BTCustID":
                                st.CustId = "90000";
                                break;
                        }
                        break;
                        //Console.WriteLine(reader.Value);
                        
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }
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
