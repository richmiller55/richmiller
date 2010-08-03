using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum enumTransfer
    {
        newTerr,
        oldTerr,
        custId,
        custName,
        salesRepNum,
        filler
    }
    public enum colAddrOld
    {
	CustId,
    CustNum, 
    Name,
    addr1,
	City,
    st,
    zip,
    currentTerr,
    currRep,
    groupCode
    }
    public enum colAddr
    {
        CustId,
        Name,
        filler
    }

    class UpdateTerritoryReader
    {
        string file = "D:/users/rich/data/customerUpdates/terrTransfers109_83_6Jul10.txt";
        StreamReader tr;
        public UpdateTerritoryReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)enumTransfer.custId];
                string NewTerr = split[(int)enumTransfer.newTerr];
                string SalesRepNum = split[(int)enumTransfer.salesRepNum];
                xman.ChangeTerrByID(CustId, NewTerr, SalesRepNum);
            }
        }
    }
}



