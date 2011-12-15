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
            ht = hashtable;
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
                pod.OrderQty as OrderQty,
		        pod.UnitCost as UnitCost
             FROM pub.POHeader as poh
		       LEFT JOIN pub.PODetail as pod
		         on poh.PONum = pod.PONum
	         ORDER BY pod.PartNum, poh.OrderDate desc
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();

                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
		    Console.WriteLine("PONum={0} part={1}  cost={3}", reader["PONum"], reader["PartNum"],reader[2]);
		    // AddStyleToHash(reader);
                }
                reader.Close();
            }
        }
        private void DetermineCost(OdbcDataReader reader)
        {
            string partNum = (string)reader["PartNum"];
            if (ht.ContainsKey(partNum))
            {
                Style style = (Style)ht[partNum];
                decimal QtyOnHand = style.NewQtyOnHand;
                decimal OrderQty = Convert.ToDecimal(reader["OrderQty"]);
                decimal UnitCost = Convert.ToDecimal(reader["UnitCost"]);
                if (style.OnHandRemaining > 0)
                {
                    if (OrderQty >= QtyOnHand)
                    {
                        
                        style.TotalOnHandValue = style.TotalOnHandValue + (QtyOnHand * UnitCost);
                        style.PO_Cost = style.TotalOnHandValue / QtyOnHand;
                    }
                    else
                    {
                        style.TotalOnHandValue = style.TotalOnHandValue + (OrderQty * UnitCost);
                    }
                    ht[partNum] = style;
                 }
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
