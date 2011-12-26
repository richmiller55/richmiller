using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class Bom
    {
        Hashtable bomHash;
        Hashtable parentList;
        Hashtable ht;
        PartInfo partInfo;
        string Dsn; 
        public Bom(Hashtable ht)
        {
            this.ht = ht;
            Dsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            partInfo = new PartInfo();
            bomHash = new Hashtable(2000);
            parentList = new Hashtable(2000);
            GetData();
            ChildIsSumOfParents();
        }
        public void GetData()
        {
            ReadData(Dsn);
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
        private void FillDescrption(ref Style style)
        {
            string partNum = style.Upc;
            style.StyleDescr = partInfo.GetDescr(partNum);
        }
        private void ChildIsSumOfParents()
        {
            if (ht.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);

            }
            else
            {
                ICollection onHand = ht.Keys;
                foreach (object part in onHand)  // part needs a cost
                {
                    if (bomHash.ContainsValue(part))
                    {
                        ICollection boms = bomHash.Values;  // parents, base that the components roll to
                        Style result = new Style(part.ToString());
                        foreach (object parent in boms)
                        {
                            if (part.ToString() == parent.ToString())
                            {
                                // now get all the children for that parent
                                // string parent = bomHash[child].ToString();
                                if (parentList.ContainsKey(parent.ToString()))
                                {
                                    ArrayList children = (ArrayList)parentList[parent.ToString()];
                                    IEnumerator childList = children.GetEnumerator();
                                    while (childList.MoveNext())
                                    {
                                        if (ht.ContainsKey(childList.Current))
                                        {
                                            result = result + (Style)ht[childList.Current];
                                            FillDescrption(ref result);
                                        }
                                    }
                                }    
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
                // parents with all their children as a list
                // simple little structure
                // a hashtable with the parent part as the key and
                // a list as the value.
                if (!parentList.ContainsKey(reader["ParentPart"]))
                {
                    ArrayList children = new ArrayList();
                    children.Add(reader["MtlPartNum"]);
                    parentList.Add(reader["ParentPart"], children);
                }
                else
                {
                    string parentPart = reader["ParentPart"].ToString();
                    string childPart = reader["MtlPartNum"].ToString();
                    ArrayList children = (ArrayList)parentList[parentPart];
                    children.Add(childPart);
                    parentList[parentPart] = children;
                }
            }
        }
    }
}
