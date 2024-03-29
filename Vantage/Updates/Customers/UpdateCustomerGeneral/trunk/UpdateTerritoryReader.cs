using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace UpdateCustomerGeneral
{
    public enum enumTransfer
    {
        custId,
        newTerr,
        salesRepNum,
        filler
    }
    public enum terrTran
    {
        custId,
        shipToId,
        newTerr,
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
    public enum colGrp
    {
        CustId,
        CustGrp,
        filler
    }

    public enum fob
    { 
        CustNum,
        CustId,
        ShipViaCode,
        filler
    }
    class UpdateTerritoryReader
    {
        string file = "I:/data/updates/customers/updateTerr21Oct13.txt";
        StreamReader tr;
        public UpdateTerritoryReader()
        {
            tr = new StreamReader(file);
            // processSimpleFile();
            processFile();
            // custGrpUpdate();
        }

        void SetInvBoxFlag()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)terrTran.custId];
                if (CustId.CompareTo("CustId") == 0) continue;
                xman.SetCheckBox06(CustId);
            }
        }
        void SetDupPOFlag()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                xman.SetNoDupPOFlag(line);
            }
        }

        void custGrpUpdate()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)colGrp.CustId];
                if (CustId.CompareTo("CustId") == 0) continue;
                string NewGrp = split[(int)colGrp.CustGrp];
                xman.ChangeCustGrp(CustId, NewGrp);
            }
        }
        void processFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)enumTransfer.custId];
                if (CustId.CompareTo("CustId") == 0) continue;
                string NewTerr = split[(int)enumTransfer.newTerr];
                string SalesRepNum = split[(int)enumTransfer.salesRepNum];
                xman.ChangeTerrByID(CustId, NewTerr, SalesRepNum);
            }
        }
        void processSimpleFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)terrTran.custId];
                if (CustId.CompareTo("CustId") == 0) continue;
                string NewTerr = split[(int)terrTran.newTerr];
                string SalesRepNum = split[(int)terrTran.salesRepNum];
                xman.ChangeTerrByID(CustId, NewTerr, SalesRepNum);
            }
        }
        void processFOBFile()
        {
            string line = "";
            CustomerXMan xman = new CustomerXMan();

            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string CustId = split[(int)fob.CustId];
                if (CustId.CompareTo("CustId") == 0) continue;
                string CustNum = split[(int)fob.CustNum];
                string NewFOB = "FGRB";
                xman.ChangeFOBByID(CustId, NewFOB);
            }
        }
    }
}



