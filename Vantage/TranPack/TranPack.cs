using System;
using System.Data.Odbc;
using System.Collections;
using System.Text;

namespace Pack
{
    public class TranPack
    {
        
        ArrayList packs;
        public TranPack()
        {
            packs = GetData(Dsn);

        }
        public ArrayList GetData()
        {

            string queryString = @" 
             SELECT top 1000
	        p.PackNum as PackNum,
                p.Voided as Voided,
                p.CustNum as CustNum,
                p.ShipToNum as ShipToNum,
                p.ShipViaCode as ShipViaCode,
                p.FreightedShipViaCode as FreightedShipViaCode,
                p.ShipDate as ShipDate
                FROM pub.ShipHead as p
                where 
                p.ShipDate = curdate()
                order by p.ShipDate desc
                ";

            string Dsn = "DSN=sys; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            ArrayList al;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                 al = PackHash(reader);
                reader.Close();
            }
            return al;
            
        }
        private void UpdateRemoteSql(ArrayList al)
        {
            string Dsn = "DSN=GC; HOST=gc.rlm5.com; DB=coinet_db1; UID=focus; PWD=focusgroup";
            StringBuilder sqlUpdate = new StringBuilder();
            sqlUpdate.AppendLine("insert qShipHead");
            sqlUpdate.AppendLine("set ");
        }
        private ArrayList PackHash(OdbcDataReader reader)
        {
            ArrayList al = new ArrayList();
            while (reader.Read())
            {
                Hashtable pack = new Hashtable();
                pack["ShipToNum"] = reader["ShiptoNum"].ToString();
                pack["ShipDate"] = reader["ShipDate"].ToString();
                pack["PackNum"] = reader["PackNum"].ToString();
                pack["CustNum"] = reader["custNum"].ToString();
                pack["ShipViaCode"] = reader["ShipViaCode"].ToString();
                pack["Voided"] = reader["Voided"].ToString();
                al.Add(pack);
            }
            return al;
        }
    }
}
