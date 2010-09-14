using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InvBox
{
    class Watcher
    {
        string dir = @"d:/users/UPS";
        FileSystemWatcher watcher;
        Epicor.Mfg.Core.Session session;
        public Watcher()
        {

            session = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8321", Epicor.Mfg.Core.Session.LicenseType.Default);
            
            watcher = new FileSystemWatcher(dir, "*.*");
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            // watcher.Created += OnError;
            watcher.EnableRaisingEvents = true;
            while (Console.Read() != 'q') ;
        }
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
/*
            watcher.EnableRaisingEvents = false;
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                UPSReader reader = new UPSReader(this.session);
            }
            watcher.EnableRaisingEvents = true;
*/
        }
        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                UPSReader reader = new UPSReader(this.session);
            }
        }
        private static void OnError(object source, ErrorEventArgs e)
        {
            Console.WriteLine("error message");
            Console.WriteLine(e.GetException());
        }
    }
}
