using System;
using System.Collections.Generic;
// using System.Windows.Forms;

namespace VoidTags
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
            // Application.Run(new Form1());
            VoidTagReader reader = new VoidTagReader();
        }
    }
}