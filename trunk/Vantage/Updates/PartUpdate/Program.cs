using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PartUpdate
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
            UpdateTextReader reader = new UpdateTextReader();
        }
    }
}