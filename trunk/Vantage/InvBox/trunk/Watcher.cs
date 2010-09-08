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
        public Watcher()
        {
            watcher = new FileSystemWatcher(dir, "*.*");
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            // watcher.Created += OnError;
            this.runLoop();
        }
        public void runLoop()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                string test = "now";
            }
        }
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            UPSReader reader = new UPSReader();
        }
        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            // I think I want to install a error handler here
            // throw new Exception("The method or operation is not implemented.");
        }
        private static void OnError(object source, ErrorEventArgs e)
        {
            Console.WriteLine("error message");
            Console.WriteLine(e.GetException());
        }
    }
}
