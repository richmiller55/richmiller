using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShipToLoad
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
            DataReader reader = new DataReader();
            Application.Run(new Form1());
        }
    }
}