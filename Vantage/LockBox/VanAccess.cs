using System;
using System.Windows.Forms;

namespace LB1
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class VanAccess
    {
        string userId;
        string vanPassword;
        string vanDb;
	    Epicor.Mfg.Core.Session vanSession;
        public VanAccess(string user, string password, string database)
        {
            userId = user;
            vanPassword = password;
            vanDb = database;
            string appPort = GetPortNumber(database);
            try
            {
                vanSession = new Epicor.Mfg.Core.Session(userId, vanPassword,
        "AppServerDC://VantageDB1:" + appPort, Epicor.Mfg.Core.Session.LicenseType.Default);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public Epicor.Mfg.Core.Session getSession()
        {
            return vanSession;
        }
	string GetPortNumber(string database)
	{
		string port = "8321";
		if (String.Compare(database,"Pilot",true) == 0) 
		{	
			port = "8331";
			return port;
		}
		else if (String.Compare(database,"System",true) == 0) 
		{	
			port = "8301";
			return port;
		}
		else if (String.Compare(database,"Test",true) == 0) 
		{	
			port = "8321";
			return port;
		}
		else if (String.Compare(database,"Training",true) == 0) 
		{	
			port = "8311";
			return port;
		}
		else 
		{
			 return "null";
		}
	}
    }
}
