using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace CloseJobs
{
    public enum col
    {
        jobNo,
    	filler
    }
    public class CloseListReader
    {
        StreamReader tr;
        JobCloseXman jobX;
        public CloseListReader()
        {
            //  jobCloseList is the current daily file
            string file = "D:/users/rich/data/jobs/jobCloseList.txt";
            // string file = "I:/data/updates/jobs/closeWO_20140319.txt";
            // string file = "D:/users/rich/data/jobs/closeWO.txt";
            tr = new StreamReader(file);
            jobX = new JobCloseXman();
            processFile();
        }
        void processFile()
        {
            string line = "";
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });

                int result = split[0].CompareTo("");
                if (result == 0) continue;

                string jobNo = split[(int)col.jobNo];
                jobX.CloseJob(jobNo);
            }
	    }
    }
}