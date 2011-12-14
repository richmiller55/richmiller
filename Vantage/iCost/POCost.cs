using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class POCost
    {
        Hashtable ht;
        public POCost(Hashtable hashtable)
        {
            GetData();
        }
        public void GetData()
        {
            string pilotDsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            ReadData(pilotDsn);
        }
        public void ReadData(string connectionString)
        {
            string queryString = @" 
             SELECT
                poh.PONum  as PONum,
		pod.PartNum as PartNum,
		pod.UnitCost as UnitCost
                FROM pub.POHeader as poh
		LEFT JOIN pub.PODetail as pod
		on poh.PONum = pod.PONum
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();

                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
		    Console.WriteLine("PONum={0}", reader[0]);
		    // AddStyleToHash(reader);
                }
                // Call Close when done reading.
                reader.Close();
            }
        }
        private void AddCostToHash(OdbcDataReader reader)
        {
            Style st = new Style(System.Convert.ToString(reader[0]));
            st.NewQtyOnHand = System.Convert.ToDecimal(reader[1]);
            ht.Add(st.Upc, st);
        }
    }
}
