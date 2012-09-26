using System;
using System.Data.Odbc;
using System.Collections;
using System.Text;

namespace Pack
{
    public class TranPack
    {
        public TranPack()
        {
            ArrayList packs = GetData();
            // DropTable();
            // CreateTable();
            UpdateTable(packs);
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
            using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                 al = PackHash(reader);
                reader.Close();
            }
            return al;
        }
        private void CreateTable()
        {
            string queryString = @" 
                create table t_CurrentPacks (
                 PackNum  int,
                 CustNum  int,
                 OrderNum int,
                 PONum varchar(14),
                 ShipToNum varchar(14),
                 ShipViaCode varchar(4),
                 ShipDate    varchar(15),
                 index t_currentPacksIdx1(PackNum)
                )";
            ExecuteUpdate(queryString);
        }
        private void DropTable()
        {
            string queryString = @" 
                drop table if exists t_CurrentPacks ";
            ExecuteUpdate(queryString);
        }
        private string GetMySqlDsn()
        {
            return "DSN=GC; HOST=gc.rlm5.com; DB=coinet_db1; UID=focus; PWD=focusgroup";
        }
        private void UpdateTable(ArrayList al)
        {
            foreach (Hashtable ht in al)
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.AppendLine("insert into t_CurrentPacks ");
                sqlUpdate.AppendLine("set PackNum = " + ht["PackNum"]);
                sqlUpdate.AppendLine(",CustNum = " + ht["CustNum"]);
                sqlUpdate.AppendLine(",ShipToNum = " + "'" + ht["ShipToNum"] + "'");
                sqlUpdate.AppendLine(",ShipViaCode = " + "'" + ht["ShipViaCode"] + "'");
                sqlUpdate.AppendLine(",ShipDate = " + "'" + ht["ShipDate"] + "'");
                if (RecordNotThere(System.Convert.ToInt32(ht["PackNum"]))) ExecuteUpdate(sqlUpdate.ToString());
            }
        }
        private bool RecordNotThere(int PackNum)
        {
            bool result = true;
            string sql = @"
            select PackNum from t_CurrentPacks where PackNum = " + PackNum.ToString();
            string Dsn = GetMySqlDsn();
            using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(sql, connection);
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    try
                    {
                        string message = e.Message;
                        // wait a second
                        System.Threading.Thread.Sleep(1000);
                        connection.Open();
                    }
                    catch (Exception e2)
                    {
                        string message = e2.Message;
                        Environment.Exit(0);
                    }
                }        
                OdbcDataReader reader = command.ExecuteReader();
                if (reader.HasRows) result = false;
                reader.Close();
            }
            return result;
        }
        private void ExecuteUpdate(string sql)
        {
            string Dsn = GetMySqlDsn();
            using (OdbcConnection connection = new OdbcConnection(Dsn))
            {
                OdbcCommand command = new OdbcCommand(sql, connection);
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    try
                    {
                        string message = e.Message;
                        // wait a second
                        System.Threading.Thread.Sleep(1000);
                        connection.Open();
                    }
                    catch (Exception e2)
                    {
                        string message = e2.Message;
                        Environment.Exit(0);
                    }
                }
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
        private ArrayList PackHash(OdbcDataReader reader)
        {
            ArrayList al = new ArrayList();
            while (reader.Read())
            {
                Hashtable packT = new Hashtable();
                packT["ShipToNum"] = reader["ShiptoNum"].ToString();
                packT["ShipDate"] = reader["ShipDate"].ToString();
                packT["PackNum"] = reader["PackNum"].ToString();
                packT["CustNum"] = reader["custNum"].ToString();
                packT["ShipViaCode"] = reader["ShipViaCode"].ToString();
                packT["Voided"] = reader["Voided"].ToString();
                al.Add(packT);
            }
            return al;
        }
    }
}
