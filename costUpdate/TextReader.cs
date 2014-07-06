#define working
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
            ProcessFile();
            StringBuilder sbReport = auditFile();
            writeAuditReport(sbReport);
        }
        void writeAuditReport(StringBuilder report)
        {
            string fileName = "stdcostreport_NotOnHandpost5Jul2014.txt";
            StreamWriter tw = new StreamWriter(fileName);
            tw.WriteLine(report.ToString());
            tw.Close();
        }
        void AddLineToReport(StringBuilder sb, PartBinRecord pbr)
        {
            string tab = "\t";
            foreach (BinAtom bin in pbr.Bins)
            {
                string rl = pbr.PartNum + tab + bin.Warehouse + tab + bin.BinNum + tab + bin.OnhandQty.ToString() + "\n";
                sb.Append(rl);
            }
        }
        StreamReader OpenFile()
        {
            // string fName = "stdCostUpdate_NotOnHand5Jul2014.txt";
            // string fName = "stdCostUpdate_WithOnHand5Jul2014.txt";
            string fName = "stdCostUpdate_WH9_5Jul2014.txt";
            string filedir = "I:/data/updates/costs/";
            StreamReader tr = new StreamReader(filedir + fName);
            return tr;
        }
        StringBuilder auditFile()
        {
            StreamReader tr = OpenFile();   
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
            StreamReader tr = OpenFile();   
            CostXman xman = new CostXman();
            while ((line = tr.ReadLine()) != null)
            {
                string[] split = line.Split(new Char[] { '\t' });
                string partNum = split[(int)rowLayout.UPC];
                if (xman.IsPartInactive(partNum)) continue;
                PartBinRecord pbr = xman.GetOnHandForPart(partNum);
                
                bool writeOff = true;
                xman.WriteAllBins(writeOff, pbr);
                xman.updateCostMethod(line);
                xman.updateStdCost(line);
                writeOff = false;
                xman.WriteAllBins(writeOff, pbr);
            }
            tr.Close();
        }
#endif
    }
}


