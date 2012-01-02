using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class Bom
    {
        Hashtable bomHash;
        Hashtable parentList;
        Hashtable oldHt;
        Hashtable newHt;
        PartInfo partInfo;
        string Dsn; 
        public Bom(Hashtable ht)
        {
            oldHt = ht;
            newHt = new Hashtable(oldHt);

            Dsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            partInfo = new PartInfo();
            bomHash = new Hashtable(2000);
            parentList = new Hashtable(2000);
            GetData();
            ChildIsSumOfParents();
        }
        private void AddToNewHash(string key, Style style)
        {
            if (newHt.ContainsKey(key))
            {
                newHt[key] = style;
            }
            else
            {
                newHt.Add(key, style);
            }
        }
        public Hashtable NewHt
        {
            get { return newHt; }
            set { newHt = value; }
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
            if (oldHt.Count == 0)
            {
                string message = "no records";
                Console.WriteLine(message);
            }
            else
            {
                ICollection onHand = oldHt.Keys;
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
                                        if (oldHt.ContainsKey(childList.Current))
                                        {
                                            result = result + (Style)oldHt[childList.Current];
                                            Style child = (Style)oldHt[childList.Current];
                                            
                                            result.BomLog += "BOM: " + childList.Current.ToString() + "\t";
                                            result.BomLog += "Amt:" + child.Cost.ToString() + "\t";
                                            FillDescrption(ref result);
                                            AddToNewHash(result.Upc, result);
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
