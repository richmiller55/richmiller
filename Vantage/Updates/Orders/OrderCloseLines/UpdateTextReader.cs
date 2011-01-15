using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace OrderCloseLines
{
    public enum input
    {
        orderNum,
        orderLine,
        custId,
        filler
    }
    class UpdateTextReader
    {
        string file = "D:/users/rich/data/orderUpdates/conceptEyesCloseOrderLineList_14Jan2011.txt";
        StreamReader tr;
        public UpdateTextReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            OrderXman xman = new OrderXman();
            while ((line = tr.ReadLine()) != null)
            {
                xman.CloseOrderLine(line);
            }
        }
    }
}



