using System;
using System.Data.Odbc;
using System.Collections;

namespace iCost
{
    public class FrtContainer
    {
        Hashtable frtCost;
        Hashtable frtLogs;
        string Dsn;
        public FrtContainer()
        {
            Dsn = "DSN=sys; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
            frtCost = new Hashtable(2000);
            frtLogs = new Hashtable(2000);
            GetData(Dsn);
        }
        public void GetData(string connectionString)
        {
            string queryString = @" 
            select
                ch.ContainerCost    as ContainerCost,
                ch.ContainerID      as ContainerID,
                ch.CostPerVolume    as CostPerVolume,
                ch.ShipDate         as ShipDate,
                ch.Volume           as ContainerVolume,
                cd.ContainerShipQty as LineShipQty,
                cd.Volume           as LineVolume,
                cd.OurUnitCost      as OurUnitCost,
                pod.PartNum         as PartNum,
                pod.PONum           as PONum
            from pub.ContainerHeader as ch
            left join pub.ContainerDetail as cd
                on ch.ContainerID = cd.ContainerID
            left join pub.PODetail as pod
                on pod.PONum = cd.PONum
                  and pod.POLine = cd.POLine
            where pod.PartNum is not null
            order by ch.ContainerID desc
               ";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand(queryString, connection);
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                InitPartFrtCost(reader);
                reader.Close();
            }
        }
        private void InitPartFrtCost(OdbcDataReader reader)
        {
            while (reader.Read())
            {
                // lets start simple OK just make a keyed hash
                string partNum = reader["PartNum"].ToString();
                int containerId = (int)reader["ContainerID"];
                int poNum = (int)reader["PONum"];
                decimal containerCost = (decimal)reader["ContainerCost"];
                decimal lineVolume = (decimal)reader["LineVolume"];
                decimal containerVolume = (decimal)reader["ContainerVolume"];
                decimal lineShipQty = (decimal)reader["LineShipQty"];
                string frtLog = "{FrtLog:";
                frtLog += "{PartNum: " + partNum + "}";
                frtLog += "{PoNum: " + poNum.ToString() + "}";
                frtLog += "{ContainerId: " + containerId.ToString() + "}";
                frtLog += "{ContainerCost: " + decimal.Round(containerCost, 2).ToString() + "}";
                frtLog += "{ContainerVolume: " + decimal.Round(containerVolume, 2).ToString() + "}";
                frtLog += "{LineShipQty: " + decimal.Round(lineShipQty, 2).ToString() + "}";
                frtLog +=  "}";
                decimal frtUnitCost = 0M;
                if (containerVolume.Equals(0M)) 
                { 
                    continue; 
                }
                else
                {
                    frtUnitCost = (lineVolume / containerVolume)
                        * containerCost / lineShipQty;
                }
                if (frtCost.ContainsKey(partNum))
                {
                    continue;
                }
                else
                {
                    frtCost.Add(partNum, frtUnitCost);
                    frtLogs.Add(partNum, frtLog);
                }
            }
        }
        public decimal GetFrtCost(string partNum)
        {
            decimal result = 0M;
            if (frtCost.ContainsKey(partNum))
            {
                result = (decimal)frtCost[partNum];
            }
            return result;
        }
        public string GetFrtLogEntry(string partNum)
        {
            string log = "";
            if (frtLogs.ContainsKey(partNum))
            {
                log = frtLogs[partNum].ToString();
            }
            return log;
        }
    }
}
