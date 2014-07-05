#undef working
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace costUpdate
{
    public enum rowLayout
    {
        UPC,
        cost,
        filler
    }
    class UpdateTextReader
    {
        
        public UpdateTextReader()
        {
            // ProcessFile();
            StringBuilder sbReport = auditFile();
            writeAuditReport(sbReport);
        }
        void writeAuditReport(StringBuilder report)
        {
            string fileName = "autditreport.txt";
            StreamWriter tw = new StreamWriter(fileName);
            tw.WriteLine(report.ToString());
            tw.Close();
        }
        void AddLineToReport(StringBuilder sb, PartBinRecord pbr)
        {
            string rl = pbr.PartNum;
            ICollection keyColl = pbr.BinHash.Keys;
            string whNum = "01";
            string tab = "\t";
            foreach (string binNum in keyColl)
            {
                decimal qtyOnHand = (decimal)pbr.BinHash[binNum];
                rl = pbr.PartNum + tab + binNum + tab + qtyOnHand.ToString() + "\n";
                sb.Append(rl);
            }
        }
        StringBuilder auditFile()
        {
            string fName = "stdCostUpdate.txt";
            string filedir = "I:/data/updates/costs/";
            StreamReader tr = new StreamReader(filedir + fName);
            
            string line;
            CostXman xman = new CostXman();
            StringBuilder sbreport = new StringBuilder();
            while ((line = tr.ReadLine()) != null)
            {
                // xman.updateStdCost(line);
                string[] split = line.Split(new Char[] { '\t' });
                string partNum = split[(int)rowLayout.UPC];
                if (xman.IsPartInactive(partNum)) continue;
                PartBinRecord pbr = xman.GetOnHandForPart(partNum);
                AddLineToReport(sbreport, pbr);
                
            }
            return sbreport;
        }

#if working
        void ProcessFile()
        {
            String line;
            CostXman xman = new CostXman();
            while ((line = this.tr.ReadLine()) != null)
            {
                // xman.updateStdCost(line);
                string[] split = line.Split(new Char[] { '\t' });
                string partNum = split[(int)rowLayout.UPC];
                if (xman.IsPartInactive(partNum)) continue;
                PartBinRecord partBin = xman.GetOnHandForPart(partNum);
                bool writeOff = true;
                xman.WriteAllBins(writeOff, partBin);
                xman.updateCostMethod(line);
                xman.updateStdCost(line);
                writeOff = false;
                xman.WriteAllBins(writeOff, partBin);
            }
        }
#endif
    }
}


