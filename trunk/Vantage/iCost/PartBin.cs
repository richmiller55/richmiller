using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class PartBin
    {
        Hashtable ht;
        public PartBin(Hashtable hashtable)
        {
            this.ht = hashtable;
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
                pb.partNum  as partNum,
                max(p.PartDescription) as PartDescription,
                sum(pb.OnhandQty) as OnhandQty 
                FROM pub.PartBin as pb
                LEFT JOIN pub.Part as p
                on pb.PartNum = p.PartNum
                GROUP BY pb.partNum
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();

                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AddStyleToHash(reader);
                    // Console.WriteLine("PartNum={0}", reader[0]);
                }
                reader.Close();
            }
        }
        private void AddStyleToHash(OdbcDataReader reader)
        {
            Style st = new Style(System.Convert.ToString(reader["partNum"]));
            st.StyleDescr = reader["PartDescription"].ToString();
            st.NewQtyOnHand = System.Convert.ToDecimal(reader["OnHandQty"]);
            st.OnHandRemaining = st.NewQtyOnHand;
            ht.Add(st.Upc, st);
        }
    }
}
