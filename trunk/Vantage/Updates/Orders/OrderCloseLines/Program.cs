using System;
using System.Collections.Generic;

namespace OrderCloseLines
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UpdateTextReader reader = new UpdateTextReader();
        }
    }
}