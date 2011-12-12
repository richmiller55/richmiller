using System;
using System.Data.Odbc;

namespace iCost
{
    public class PartBin
    {
        public PartBin()
        {

        }
        public void GetPOCost()
        {
            string pilotDsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            ReadData(pilotDsn);
        }
        public static void ReadData(string connectionString)
        {
            string queryString = @"     select
                pb.partNum  as partNum,  -- char 50
                pb.WarehouseCode as WarehouseCode, -- char 8
                pb.BinNum as BinNum, -- char 10
                pb.OnhandQty as OnhandQty, -- decimal 12,2
                pb.LotNum as LotNum  -- char 30
                FROM  pub.PartBin as pb
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);

                connection.Open();

                // Execute the DataReader and access the data.
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("PartNum={0}", reader[0]);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}
