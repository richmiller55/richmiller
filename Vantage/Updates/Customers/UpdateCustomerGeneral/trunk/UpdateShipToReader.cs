using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace UpdateCustomerGeneral
{
    class UpdateShipToReader
    {
        string file = "D:/users/rich/data/customerUpdates/correctShipToRepTerr_6Jul10.txt";
        StreamReader tr;
        public UpdateShipToReader()
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
                xman.ChangeShipToRep(line);
            }
        }
    }
}



