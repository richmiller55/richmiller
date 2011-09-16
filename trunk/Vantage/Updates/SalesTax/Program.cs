using System;
using System.Collections.Generic;

namespace UpdateSalesTax
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
           
            SalesTaxReader reader = new SalesTaxReader();
        }
    }
}