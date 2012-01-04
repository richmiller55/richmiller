using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class PartBin
    {
        Hashtable partOnHand;
        public PartBin()
        {
            partOnHand = new Hashtable(3000);
            LoadHash();
        }
        private void LoadHash()
        {
            string pilotDsn = "DSN=sys; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            ReadData(pilotDsn);
        }
        private void ReadData(string connectionString)
        {
            string queryString = @" 
             SELECT
                pb.partNum  as partNum,
                sum(pb.OnhandQty) as OnhandQty 
                FROM pub.PartBin as pb
                GROUP BY pb.partNum
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AddStyleToHash(reader);
                }
                reader.Close();
            }
        }
        private void AddStyleToHash(OdbcDataReader reader)
        {
            string upc = System.Convert.ToString(reader["partNum"]);
            decimal onHandQty = System.Convert.ToDecimal(reader["OnHandQty"]);
            partOnHand.Add(upc, decimal.Round(onHandQty,1));
        }
        public decimal GetOnHand(string upc)
        {
            decimal result = 0M;
            if (partOnHand.ContainsKey(upc))
            {
                result = System.Convert.ToDecimal(partOnHand[upc]);
            }
            return result;
        }
    }
}
