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
		        pod.UnitCost as POUnitCost,
                p.PartDescription as PartDescription
             FROM pub.POHeader as poh
		       LEFT JOIN pub.PODetail as pod
		         on poh.PONum = pod.PONum
               left join pub.Part as p
                 on pod.PartNum = p.PartNum
             where pod.PartNum is not null
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
                    Console.WriteLine("PONum={0} part={1}  cost={2}", 
                            reader["PONum"], 
                            reader["PartNum"], 
                            reader["POUnitCost"]);
		            DetermineCost(reader);
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
                style.StyleDescr = Convert.ToString(reader["PartDescription"]);
                decimal QtyOnHand = style.NewQtyOnHand;
                decimal OrderQty = Convert.ToDecimal(reader["OrderQty"]);
                decimal POUnitCost = Convert.ToDecimal(reader["POUnitCost"]);
                if (style.OnHandRemaining > 0)
                {
                    /* 
                     * 
                     * so if you don't know what the cost is when you 
                     * are working back then just price what you know, 
                     * as long as you know something
                     *
                     */
                    if (OrderQty >= QtyOnHand)
                    {
                        style.TotalOnHandValue = style.TotalOnHandValue + (QtyOnHand * POUnitCost);
                        style.PO_Cost = style.TotalOnHandValue / QtyOnHand;
                        style.OnHandRemaining = 0;
                    }
                    else
                    {
                        style.TotalOnHandValue = style.TotalOnHandValue + (OrderQty * POUnitCost);
                    }
                    ht[partNum] = style;
                 }
            }
        }
    }
}
