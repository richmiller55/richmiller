using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace VoidTags
{
    public class AppVars
    {
        string m_user = "";
        string m_password = "";
        string m_database = "test";
        string m_dataPort;

        string file = "appVars.xml";
        public AppVars()
        {
            ParseFile();
        }
        protected void ParseFile()
        {
            XmlTextReader reader = new XmlTextReader(file);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            string element = "";
            string readValue = "";
            while (reader.Read())
            {
                bool ready = false;
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        element = reader.Name;
                        ready = false;
                        break;
                    case XmlNodeType.Text:
                        readValue = reader.Value;
                        ready = true;
                        break;
                }
                if (ready)
                {
                    switch (element)
                    {
                        case "user":
                            User = readValue;
                            break;
                        case "password":
                            Password = readValue;
                            break;
                        case "database":
                            break;
                        case "dataPort":
                            DataPort = readValue;
                            break;
                    }
                }
            }
        }
        public string User
        {
            get
            {
                return m_user;
            }
            set
            {
                m_user = value;
            }
        }
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }
        public string Database
        {
            get
            {
                return m_database;
            }
            set
            {
                m_database = value;
            }
        }
        public string DataPort
        {
            get
            {
                return m_dataPort;
            }
            set
            {
                m_dataPort = value;
            }
        }
    }
}


