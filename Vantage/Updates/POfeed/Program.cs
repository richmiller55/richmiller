using System;
using System.Collections.Generic;
using System.Text;


namespace POfeed
{
    class Program
    {
        static void Main(string[] args)
        {
            HashReader x = new HashReader();
            x.RunFile();
            // CouchReader x = new CouchReader();
        }
    }
}
