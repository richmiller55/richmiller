using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class FullPart
    {
        Hashtable partHash;
        string Dsn;
        public FullPart()
        {
            Dsn = "DSN=sys; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            partHash = new Hashtable(4000);
            GetData(Dsn);
        }
        public void GetData(string connectionString)
        {
            string queryString = @" 
             SELECT
                p.PartNum as PartNum,
                p.PartDescription  as PartDescription,
                p.UnitPrice as UnitPrice,
                p.ShortChar02 as loc,
                p.ProdCode as ProdCode,
                p.Number01 as CasePack,
                pg.Number01 as DutyRate,
                pg.Number02 as Burden,
                pg.ShortChar01 as CompRetail
                FROM pub.Part as p
                LEFT JOIN pub.ProdGrup as pg
                ON p.ProdCode = pg.ProdCode
                WHERE pg.ProdCode is not null
               ";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                InitStyleHash(reader);
                reader.Close();
            }
        }
        public Hashtable NewHt
        {
            get { return partHash; }
            set { partHash = value; }
        }
        public bool ContainsKey(string partNum)
        {
            return partHash.ContainsKey(partNum);
        }
        private void InitStyleHash(OdbcDataReader reader)
        {
            while (reader.Read())
            {
                string partNum = reader["partNum"].ToString();
                if (!partHash.ContainsKey(partNum))
                {
                    Style style = new Style(partNum);
                    style.StyleDescr = reader["PartDescription"].ToString();
                    style.CasePack = System.Convert.ToDecimal(reader["CasePack"]);
                    style.UnitPrice = System.Convert.ToDecimal(reader["UnitPrice"]);
                    partHash[partNum] = style;
                }

            }
        }
    }
}
