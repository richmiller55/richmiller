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
                            case "CustId":
                                st.CustId = reader.Value;
                                break;
                            case "BTCustID":
                                st.CustId = reader.Value;
                                break;
                            case "StoreNumber":
                                st.ShipToId = reader.Value;
                                break;
                            case "StoreName":
                                st.Name = reader.Value;
                                break;
                            case "Address1":
                                st.Address1 = reader.Value;
                                break;
                            case "ShipVia":
                                st.ShipVia = reader.Value;
                                break;
                            case "ShipViaCode":
                                st.ShipVia = reader.Value;
                                break;
                            case "City":
                                st.City = reader.Value;
                                break;
                            case "State":
                                st.State = reader.Value;
                                break;
                            case "ZipCode":
                                st.Zip = reader.Value;
                                break;
                            case "Address2":
                                st.Address2 = reader.Value;
                                break;
                            case "Country":
                                st.Country = reader.Value;
                                break;
                            case "PhoneNum":
                                st.Phone = reader.Value;
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
