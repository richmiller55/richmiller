using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace TransferTracking
{
    public enum trackDb
    {
        pack_num,
        order_num,
        tracking_no,
        ship_to_name,
        addr1,
        addr2,
        city,
        state,
        zip,
        country,
        weight,
        cost,
        ship_date,
        service,
        billing_opt,
        length,
        width,
        height,
        customer_po,
        package_type,
        carrier,
        mydeleted,
        frt_upd_flag,
        trk_upd_flag
    }
    public class TrackingReader
    {
        public SqlDataReader reader;
        public TrackingReader()
        {
            reader = SetDataReader();
            WriteOutTextFile();
        }
        public SqlDataReader GetReader()
        {
            return reader;
        }
        private string GetTrackingSql()
        {
            string sql = @"
                select 
                    isnull(pack_num,0) as   pack_num,
                    isnull(order_num,'') as   order_num ,
                    isnull(tracking_no  ,'') as tracking_no  ,
                    isnull(ship_to_name ,'') as ship_to_name ,
                    isnull(addr1       ,'') as addr1       ,
                    isnull(addr2       ,'') as addr2       ,
                    isnull(city        ,'') as city        ,
                    isnull(state       ,'') as state       ,
                    isnull(zip         ,'') as zip         ,
                    isnull(country     ,'') as country     ,
                    isnull(weight      ,0) as weight      ,
                    isnull(cost        ,0) as cost        ,
                    isnull(ship_date   ,'') as ship_date   ,
                    isnull(service     ,'') as service     ,
                    isnull(billing_opt ,'') as billing_opt ,
                    isnull(length      ,0) as length      ,
                    isnull(width       ,0) as width       ,
                    isnull(height      ,0) as height      ,
                    isnull(customer_po ,'') as customer_po ,
                    isnull(package_type,'') as package_type,
                    isnull(carrier,'') as carrier,
                    isnull(deleted,'') as mydeleted,
                    isnull(frt_upd_flag ,0) as frt_upd_flag ,
                    isnull(trk_upd_flag ,0) as trk_upd_flag
                    from tracking.dbo.tracking_raw_data
                    where pack_num not in ( 999999, 323824 )
                 ";
            return sql;
        }
        
        public SqlDataReader SetDataReader()
        {
            SqlConnection connection = new SqlConnection("Data Source=app1; Integrated Security=SSPI;" +
                                                        "Initial Catalog=tracking");
            connection.Open();
            string sql = GetTrackingSql();
            SqlCommand myCommand = new SqlCommand(sql, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
        private string GetTodaysDateStr()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1) { month = "0" + month; }
            if (day.Length == 1) { day = "0" + day; }
            string yyyymmdd = year + month + day;
            return yyyymmdd;
        }
        public System.DateTime ConvertStrToDate(string dateStr)
        {
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            return dateObj;
        }
        public void WriteOutTextFile()
        {
            SqlDataReader reader = GetReader();
            if (reader.HasRows)
            {
                string trackOut = "I:/data/updates/trackingDataDump.txt";
                StreamWriter sw = new StreamWriter(trackOut);

                bool thingsToRead = reader.Read();
                while (thingsToRead)
                {
                    StringBuilder sb = new StringBuilder(1000);
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.pack_num)) + "\t");
                    sb.Append(reader.GetString((int)trackDb.order_num) + "\t");
                    sb.Append(reader.GetString((int)trackDb.tracking_no) + "\t");
                    sb.Append(reader.GetString((int)trackDb.ship_to_name) + "\t");
                    sb.Append(reader.GetString((int)trackDb.addr1) + "\t");
                    sb.Append(reader.GetString((int)trackDb.addr2) + "\t");
                    sb.Append(reader.GetString((int)trackDb.city) + "\t");
                    sb.Append(reader.GetString((int)trackDb.state) + "\t");
                    sb.Append(reader.GetString((int)trackDb.zip) + "\t");
                    sb.Append(reader.GetString((int)trackDb.country) + "\t");
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.weight)) + "\t");
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.cost)) + "\t");
                    sb.Append(reader.GetString((int)trackDb.ship_date) + "\t");
                    sb.Append(reader.GetString((int)trackDb.service) + "\t");
                    sb.Append(reader.GetString((int)trackDb.billing_opt) + "\t");
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.length)) + "\t");
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.width)) + "\t");
                    sb.Append(Convert.ToString(reader.GetDecimal((int)trackDb.height)) + "\t");
                    sb.Append(reader.GetString((int)trackDb.customer_po) + "\t");
                    sb.Append(reader.GetString((int)trackDb.package_type) + "\t");
                    sb.Append(reader.GetString((int)trackDb.carrier) + "\t");
                    sb.Append(reader.GetString((int)trackDb.mydeleted) + "\t");
                    sb.Append(Convert.ToString(reader.GetInt32((int)trackDb.frt_upd_flag)) + "\t");
                    sb.Append(Convert.ToString(reader.GetInt32((int)trackDb.trk_upd_flag)) + "\t");
                    sw.WriteLine(sb.ToString());
                    thingsToRead = reader.Read();
                }
                sw.Close();
            }
            
            
        }
    }
}
