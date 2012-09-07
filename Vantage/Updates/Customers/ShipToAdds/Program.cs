using System;
using System.Collections.Generic;
using System.IO;

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
            //           Application.EnableVisualStyles();
            //           Application.SetCompatibleTextRenderingDefault(false);
            // DataReader reader = new DataReader();
            ProcessXmlDir();

        }

        static void ProcessXmlDir()
        {
            string dir = "I:/edi/shipto/xml/";
            DirectoryInfo mainDir = new DirectoryInfo(dir);
            try
            {
                FileSystemInfo[] ediShipTos = mainDir.GetFileSystemInfos();
                foreach (FileSystemInfo shipToFile in ediShipTos)
                {
                    string fileName = shipToFile.Name;
                    XmlReader reader = new XmlReader(dir, fileName);
                    reader.runIt();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
