using System;
using System.Collections.Generic;

using System.IO;

namespace ChangeOrders
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
          RunFile();
        }
        static void RunFile()
        {
            string file = "I:/data/updates/orders/Prop65Cutover.txt";
            StreamReader tr;
            tr = new StreamReader(file);
            UpdateSalesOrder xman = new UpdateSalesOrder(); 
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                xman.ProcessOrder(line);
            }
        }
    }
}