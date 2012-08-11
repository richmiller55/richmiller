using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

using SharpCouch;
using LitJson;

namespace POfeed
{
    class VooRec
    {
        public string Customer;
        public string LineDesc;
        public Dictionary<string, int> CustomerShipDate;
        public Dictionary<string, int> PromiseDt;
        public Dictionary<string, int> RequestedShipDate;
        public Dictionary<string, int> ProformaDeliveryDate;
        public Dictionary<string, int> ExAsiaDate;
        public Dictionary<string, int> DueDate;
        public int poLine;
        public string IUM;
        public int RelQty;
        public int poRelNum;
        public int poNum;
        public int OpenLine;
        public float UnitCost;
        public string PartNum;
        public string WarehouseCode;
        public string poid;
        public string VendorName;
        public int ContainerID;
        public VooRec()
        {
            Dictionary<string, int> ProformaDeliveryDate = new Dictionary<string, int>();
            Dictionary<string, int> ExAsiaDate = new Dictionary<string, int>();
            Dictionary<string, int> CustomerShipDate = new Dictionary<string, int>();
            Dictionary<string, int> PromiseDt = new Dictionary<string, int>();
            Dictionary<string, int> RequestedShipDate = new Dictionary<string, int>();
            Dictionary<string, int> DueDate = new Dictionary<string, int>();
        }
        public int POLine
        {
            get
            {
                return poLine;
            }
            set
            {
                poLine = value;
            }
        }
        public int PONum
        {
            get
            {
                return poNum;
            }
            set
            {
                poNum = value;
            }
        }

    }
    class VooDoc
    {
        public VooDoc(string jsondoc)
        {
            Dictionary<string, VooRec> doc = new Dictionary<string, VooRec>();
        }

    }
    class DateUnit
    {
        string POID;
        int current;
        int vantage;
        public DateUnit()
        {
            current = 0;
            vantage = 0;
        }
        public int Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
            }
        }
        public int Vantage
        {
            get
            {
                return vantage;
            }
            set
            {
                vantage = value;
            }
        }
    }

    class CouchReader
    {
        private string mServer;
        private string mDB;
        private DB mCouchWrap = new DB();

        public CouchReader()
        {
            string server = "http://ec2-75-101-132-254.compute-1.amazonaws.com:5984/";
            string db = "chain";
            string docID = "VooDoc";
            string doc = mCouchWrap.GetDocument(server, db, docID);

            JsonReader reader = new JsonReader(doc);
            VooRec vooRec = new VooRec();
            while (reader.Read())
            {
                if (reader.Token.ToString().Equals("PropertyName")) {
                    switch (reader.Value.ToString())
                    {
                        case "PONum":
                            reader.Read();
                            if (reader.Token.ToString().Equals("Int"))
                            {
                                vooRec.PONum = System.Convert.ToInt32(reader.Value);
                            }
                            break;
                        case "POLine":
                            reader.Read();
                            if (reader.Token.ToString().Equals("Int"))
                            {
                                vooRec.POLine = System.Convert.ToInt32(reader.Value);
                            }
                            break;
                        case "ProformaDeliveryDate":
                            bool readingDate = true;
                            while (readingDate)
                            {
                                reader.Read();
                                if (reader.Token.ToString().Equals("ObjectStart"))
                                {
                                    readingDate = true;
                                }
                                if (reader.Token.ToString().Equals("ObjectEnd"))
                                {
                                    readingDate = false;
                                    // save  DataUnit object
                                }
                                if (reader.Token.ToString().Equals("PropertyName"))
                                {
                                    if (reader.Value.ToString().Equals("current"))
                                    {
                                        reader.Read();
                                        if (reader.Token.ToString().Equals("Int"))
                                        {
                                            vooRec.ProformaDeliveryDate["current"] = 
                                                System.Convert.ToInt32(reader.Value);
                                        }
                                    }
                                    if (reader.Value.ToString().Equals("vantage"))
                                    {
                                        reader.Read();
                                        if (reader.Token.ToString().Equals("Int"))
                                        {
                                            vooRec.ProformaDeliveryDate["vantage"] =
                                                System.Convert.ToInt32(reader.Value);
                                        }
                                    }
                                }
                            }
                            break;
                        case "ExAsiaDate":
                            readingDate = true;
                            while (readingDate)
                            {
                                reader.Read();
                                if (reader.Token.ToString().Equals("ObjectStart"))
                                {
                                    readingDate = true;
                                }
                                if (reader.Token.ToString().Equals("ObjectEnd"))
                                {
                                    readingDate = false;
                                    // save  DataUnit object
                                }
                                if (reader.Token.ToString().Equals("PropertyName"))
                                {
                                    if (reader.Value.Equals("current"))
                                    {
                                        reader.Read();
                                        if (reader.Token.ToString().Equals("Int"))
                                        {
                                            vooRec.ExAsiaDate["current"] = 
                                                System.Convert.ToInt32(reader.Value); 
                                        }
                                    }
                                    if (reader.Value.ToString().Equals("vantage"))
                                    {
                                        reader.Read();
                                        if (reader.Token.ToString().Equals("Int"))
                                        {
                                            vooRec.ExAsiaDate["vantage"] =
                                                System.Convert.ToInt32(reader.Value); 
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
        
                                

            
            // VooDoc mydoc = LitJson.JsonMapper.ToObject<VooDoc>(doc);
            // Object a = reader.Read();
        
            



