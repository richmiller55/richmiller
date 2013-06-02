using System;
using System.Collections.Generic;

namespace TransferTracking
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TrackingReader reader = new TrackingReader();
        }
    }
}