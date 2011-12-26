using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class PartInfo
    {
        Hashtable vanPartHash;
        string Dsn;
        public PartInfo()
        {
            Dsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            vanPartHash = new Hashtable(2000);
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
                InitVanPartHash(reader);
                reader.Close();
            }
        }
        public string GetDescr(string partNum)
        {
            string result = "NotFound";
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.PartDescription;
            }
            return result;
        }
        public string GetLoc(string partNum)
        {
            string result = "NotFound";
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.Loc;
            }
            return result;
        }
        public string GetProdCode(string partNum)
        {
            string result = "NotFound";
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.ProdCode;
            }
            return result;
        }
        public decimal GetBurden(string partNum)
        {
            decimal result = 0M;
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.Burden;
            }
            return result;
        }
        public decimal GetDutyRate(string partNum)
        {
            decimal result = 0M;
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.DutyRate;
            }
            return result;
        }
        public decimal GetUnitPrice(string partNum)
        {
            decimal result = 0M;
            if (vanPartHash.ContainsKey(partNum))
            {
                VanPart vanPart = (VanPart)vanPartHash[partNum];
                result = vanPart.UnitPrice;
            }
            return result;
        }
        private void InitVanPartHash(OdbcDataReader reader)
        {
            while (reader.Read())
            {
                VanPart vp = new VanPart(reader["PartNum"].ToString());
                vp.ProdCode = reader["ProdCode"].ToString();
                vp.PartDescription = reader["PartDescription"].ToString();
                vp.Loc = reader["Loc"].ToString();
                vp.CompRetail = reader["CompRetail"].ToString();
                vp.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                vp.Burden = Convert.ToDecimal(reader["Burden"]);
                vp.DutyRate = Convert.ToDecimal(reader["DutyRate"]);
                vanPartHash.Add(reader["PartNum"], vp);
            }
        }
    }
}
