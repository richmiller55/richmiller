using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class POCost
    {
        Hashtable oldHt;
        string lastPart;
        string poLog;
        bool costAssigned;
        bool firstTime;
        decimal qtyCosted;
        decimal valueCosted;
        decimal qtyNotCosted;
        PartBin partBin = new PartBin();
        PartInfo partInfo = new PartInfo();
        public POCost(ref Hashtable masterCostHt)
        {
            oldHt = masterCostHt;
            firstTime = true;
            costAssigned = false;
            valueCosted = 0M;
            qtyCosted = 0M;
            qtyNotCosted = 0M;
            poLog = "";
            GetData();
        }
        public void GetData()
        {
            string pilotDsn = "DSN=sys; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            ReadData(pilotDsn);
        }
        public void ReadData(string connectionString)
        {
            string queryString = @" 
             SELECT
                poh.PONum  as PONum,
                poh.OrderDate as OrderDate,
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
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProcessPart(reader);
                    // PrintPartOrder(reader);
                }
                reader.Close();
            }
        }
        private void PrintPartOrder(OdbcDataReader reader)
        {
            Console.WriteLine(reader["PartNum"]);
        }
        private void ProcessPart(OdbcDataReader reader)
        {
            string partNum = (string)reader["PartNum"];
            if (partNum.Equals(lastPart))
            {
                if (CostAssigned)
                {
                    // this part was already costed with a later PO
                    return;
                }
                else
                {
                    decimal orderQty = (decimal)reader["OrderQty"];
                    decimal poUnitCost = Convert.ToDecimal(reader["POUnitCost"]); 
                    if (QtyNotCosted > orderQty)
                    {
                        // save the details
                        QtyNotCosted = QtyNotCosted - orderQty;
                        QtyCosted += orderQty;
                        ValueCosted += orderQty * poUnitCost;
                        PoLog += "{PONum: " + reader["PONum"] + "}";
                        PoLog += "{PODate: " + reader["OrderDate"].ToString().Substring(0, 10) + "}";
                        PoLog += "{POQty: " + reader["OrderQty"] + "}";
                        PoLog += "{POCost: " + decimal.Round(poUnitCost, 4).ToString() + "}";
                    }
                    else
                    {
                        ValueCosted += QtyNotCosted * poUnitCost;
                        QtyCosted += QtyNotCosted;
                        decimal avgPoCost = ValueCosted / QtyCosted;
                        // Style style = new Style(partNum);
                        if (oldHt.ContainsKey(partNum))
                        {
                            Style style = (Style)oldHt[partNum];
                            style.AveragePO_Cost = decimal.Round(avgPoCost, 4);
                            PoLog += "{PONum: " + reader["PONum"] + "}";
                            PoLog += "{PODate: " + reader["OrderDate"].ToString().Substring(0, 10) + "}";
                            PoLog += "{POQty: " + reader["OrderQty"] + "}";
                            PoLog += "{POCost: " + decimal.Round(poUnitCost, 4).ToString() + "}";
                            PoLog += "{Stat:Final}";
                            style.PoLog += PoLog;
                            //FillDescrption(ref style);
                            oldHt[partNum] = style;
                        }
                        PoLog = "";
                        CostAssigned = true;
                        QtyCosted = 0M;
                        QtyNotCosted = 0M;
                        ValueCosted = 0M;
                    }
                }
            }
            else
            {
                if (!CostAssigned && !firstTime)
                { 
                    // assign what you have since you now have a different part number
                    // Style style = new Style(LastPart);
                    if (oldHt.ContainsKey(lastPart))
                    {
                        Style style = (Style)oldHt[lastPart];
                        decimal avgPoCost = ValueCosted / QtyCosted;
                        style.AveragePO_Cost = avgPoCost;
                        PoLog += "{Stat:Ran Out of POs" + "}";
                        // FillDescrption(ref style);
                        oldHt[LastPart] = style;
                    }
                    PoLog = "";
                    CostAssigned = true;
                    QtyCosted = 0M;
                    QtyNotCosted = 0M;
                    ValueCosted = 0M;
                }
                firstTime = false;
                LastPart = partNum;
                // new part to process
                // zero out all the buckets
                QtyCosted = 0M;
                QtyNotCosted = 0M;
                ValueCosted = 0M;
                CostAssigned = false;
                decimal orderQty = (decimal)reader["OrderQty"];
                decimal onHandQty = partBin.GetOnHand(partNum);
                decimal poUnitCost = Convert.ToDecimal(reader["POUnitCost"]);
                if (onHandQty < orderQty)
                {
                    // you are done, just post the cost
                    PoLog += "{PONum: " + reader["PONum"] + "}";
                    PoLog += "{PODate: " + reader["OrderDate"].ToString().Substring(0, 10) + "}";
                    PoLog += "{POQty: " + reader["OrderQty"] + "}";
                    PoLog += "{POCost: " + decimal.Round(poUnitCost, 4).ToString() + "}";
                    PoLog += "{Stat:Final}";
                    // Style style = new Style(partNum);
                    if (oldHt.ContainsKey(partNum))
                    {
                        Style style = (Style)oldHt[partNum];
                        style.AveragePO_Cost = poUnitCost;
                        style.PoLog = PoLog;
                        // FillDescrption(ref style);
                        oldHt[partNum] = style;
                    }
                    PoLog = "";
                    CostAssigned = true;
                    QtyCosted = 0M;
                    QtyNotCosted = 0M;
                    ValueCosted = 0M;
                }
                else
                {
                    // save all the details for average calculation
                    QtyNotCosted = onHandQty - orderQty;
                    QtyCosted += orderQty;
                    ValueCosted += orderQty * Convert.ToDecimal(reader["POUnitCost"]);
                    PoLog += "{PONum: "   + reader["PONum"] + "}";
                    PoLog += "{PODate: " + reader["OrderDate"].ToString().Substring(0,10) + "}";
                    PoLog += "{POQty: "  + reader["OrderQty"] + "}";
                    PoLog += "{POCost: " + decimal.Round(poUnitCost, 4).ToString() + "}";
                    PoLog += "{QtyNotCosted: " + decimal.Round(QtyNotCosted, 0).ToString() + "}";
                    PoLog += "{ValueCosted: " + decimal.Round(ValueCosted, 2).ToString() + "}";
                }
                LastPart = partNum;
            }
        }
        private void FillDescrption(ref Style style)
        {
            string partNum = style.Upc;
            style.StyleDescr = partInfo.GetDescr(partNum);
            style.CasePack = partInfo.GetCasePack(partNum);
            style.UnitPrice = partInfo.GetUnitPrice(partNum);
        }
        public string LastPart
        {
            get { return lastPart; }
            set { lastPart = value; }
        }
        public decimal QtyNotCosted
        {
            get { return decimal.Round(qtyNotCosted, 0); }
            set { qtyNotCosted = value; }
        }
        public decimal QtyCosted
        {
            get { return decimal.Round(qtyCosted, 0); }
            set { qtyCosted = value; }
        }
        public decimal ValueCosted
        {
            get { return decimal.Round(valueCosted, 2); }
            set { valueCosted = value; }
        }
        public bool CostAssigned
        {
            get { return costAssigned; }
            set { costAssigned = value; }
        }
        public string PoLog
        {
            get { return poLog; }
            set { poLog = value; }
        }
    }
}
