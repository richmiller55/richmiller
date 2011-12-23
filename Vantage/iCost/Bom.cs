using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class Bom
    {
        Hashtable bomHash;
        Hashtable ht;
        public Bom(Hashtable ht)
        {
            this.ht = ht;
            bomHash = new Hashtable(2000);
            GetData();
            ChildIsSumOfParents();
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
                pm.PartNum  as ParentPart,
                pm.MtlSeq as MtlSeq,
        		pm.MtlPartNum as MtlPartNum
                FROM pub.PartMtl as pm
                ";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                InitBomHash(reader);
                reader.Close();
            }
        }
        private void ChildIsSumOfParents()
        {
            if (ht.Count == 0)
            {
                string message = "no records";
            }
            else
            {
                ICollection onHand = ht.Keys;
                foreach (object Key in onHand)
                {
                    if (bomHash.ContainsValue(Key))
                    {
                        ICollection boms = bomHash.Values;
                        Style result = new Style(Key.ToString());
                        foreach (object child in boms)
                        {
                            if (Key.ToString() == child.ToString())
                            {
                                
                                // string parent = bomHash[child].ToString();
                                result = result + (Style)ht[child];
                            }
                        }
                    }
                }
            }
        }
        private void InitBomHash(OdbcDataReader reader)
        {
            while (reader.Read())
            {
                if (!bomHash.ContainsKey(reader["MtlPartNum"]))
                {
                    bomHash.Add(reader["MtlPartNum"], reader["ParentPart"]);
                }
            }
        }
    }
}
