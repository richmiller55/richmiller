using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using SharpCouch;

namespace POfeed
{
    class CouchReader
    {
    	private string mServer;
		private string mDB;
		private DB mCouchWrap=new DB();

        public CouchReader()
        {
            string server = "http://ec2-75-101-132-254.compute-1.amazonaws.com:5984/";
            string db = "chain";
            string docID = "VooDoc";
            string doc = mCouchWrap.GetDocument(server, db, docID);
            
        }
    }
}




