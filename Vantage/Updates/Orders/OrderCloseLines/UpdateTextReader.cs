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
        string file = "I:/data/updates/orders/WalmartCloseOrders9Jun11.txt";
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
                xman.CloseOrder(line);
            }
        }
    }
}



