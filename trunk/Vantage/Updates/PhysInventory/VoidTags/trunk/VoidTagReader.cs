using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace VoidTags
{
    public enum tagList
    {
        tagNum,
        groupCode,
        partNum,
        warehouse,
        binNum,
        filler
    }
    class VoidTagReader
    {
        string file = "D:/users/rich/data/VoidTags/VoidTagsList010311.txt";

        StreamReader tr;
        public VoidTagReader()
        {
            tr = new StreamReader(file);
            processFile();
        }
        void processFile()
        {
            string line = "";
            VoidTagXman xman = new VoidTagXman();

            while ((line = tr.ReadLine()) != null)
            {
                xman.VoidTag(line);
            }
        }
    }
}



