using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PartUpdate
{
    public class PartUpdateDataReader
    {
        public SqlDataReader reader;
        public PartUpdateDataReader()
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
                    upper(p.style)  as style,
		            p.rmCode as rmCode,
		            p.prodClass as prodClass,
		            rtrim(p.subClass)  as subClass,
		            p.list  as listPrice,
                    p.directPrice as directPrice,
		            substring(p.descr,1,30)   as descr,
                    p.upc     as upc,
					substring(p.style,21,3) as typeCode,
                    isnull(p.loc,'nloc') as loc
                from IntoVantage.dbo.part as p
                    where subClass is not null
";

            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
    }
}