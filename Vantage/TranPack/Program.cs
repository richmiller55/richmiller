using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Pack
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"d:\users\shared\ibillrunning.txt"))
            {
                System.Environment.Exit(0);
            }
            else
            {
                TranPack mgr = new TranPack();
            }
        }
    }
}
