using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PartUpdate
{
    public class ReaderDescrDataReader
    {
        public SqlDataReader reader;
        public ReaderDescrDataReader()
        {
            reader = SetDataReader();
        }
        public SqlDataReader GetReader()
        {
            return reader;
        }
        public SqlDataReader SetDataReader()
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost; Integrated Security=SSPI;" +
                                                        "Initial Catalog=IntoVantage");
            connection.Open();

            string sql = @"
                select 
                    p.upc  as upc,
		            p.newDescr as styleDescr,
                    p.shortUpc as shortUpc,
                    p.prodGrp as prodGrp
                from IntoVantage.dbo.readerStyleUpdate as p
";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
    }
}