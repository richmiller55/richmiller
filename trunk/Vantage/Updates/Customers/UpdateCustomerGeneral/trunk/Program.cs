using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UpdateCustomerGeneral
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            // UpdateCustomerReader reader = new UpdateCustomerReader();

            // UpdateCustGrupReader reader = new UpdateCustGrupReader();
            // UpdateTerritoryReader reader = new UpdateTerritoryReader();
            UpdateCreditReader reader = new UpdateCreditReader();
            // UpdateRepOnInvoiceReader reader = new UpdateRepOnInvoiceReader();
            // UpdateShipToReader reader = new UpdateShipToReader();
        }
    }
}