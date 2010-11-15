using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InvBox
{
    class Watcher
    {
        FileSystemWatcher watcher;
        Epicor.Mfg.Core.Session session;
        AppVarsMgr appVars;
        public Watcher()
        {
            this.appVars = new AppVarsMgr();
            string dir = appVars.WatchDirectory;
            string user = appVars.User;
            string pass = appVars.Password;
            string connectionStr = @"AppServerDC://VantageDB1:" + appVars.DataPort;
            try
            {
                session = new Epicor.Mfg.Core.Session(user, pass, connectionStr,
                    Epicor.Mfg.Core.Session.LicenseType.Default);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
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
