# undef DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace InvPrt
{
    public class InvLineReader
    {
        ArrayList lines = new ArrayList();
        int invoiceNum;
        string shipToId;
        string pilotDsn = "DSN=pilot; HOST=vantagedb1; DB=MfgSys; UID=sysprogress; PWD=sysprogress";
        public InvLineReader(int invoiceNum)
        {
            this.InvoiceNum = invoiceNum;
            this.FillInvoiceLines();
        }
        public void FillInvoiceLines()
        {
            string query = this.GetSelectInvDtl(this.InvoiceNum);
            OdbcConnection connection = new OdbcConnection(this.PilotDsn);
            OdbcCommand command = new OdbcCommand(query, connection);
            connection.Open();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                InvLine ln = new InvLine(reader);
                this.ShipToId = ln.ShipToId;
                lines.Add(ln);
            }
        }
        private string GetSelectInvDtl(int invoiceNum)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" select ");
            query.Append("id.InvoiceNum as InvoiceNum,");
            query.Append("id.InvoiceLine as InvoiceLine,");
            query.Append("id.SellingShipQty as SellingShipQty,");
            query.Append("id.UnitPrice as UnitPrice,");
            query.Append("id.ExtPrice as ExtPrice,");
            query.Append("id.ShipToNum as ShipToNum,");
            query.Append("id.PartNum as PartNum,");
            query.Append("pt.PartDescription as PartDescription,");
            query.Append("id.Discount  as Discount,");
            query.Append("id.DiscountPercent as DiscountPercent,");
            query.Append("id.SellingFactor as SellingFactor,");
            query.Append("id.SellingFactorDirection as SellingFactorDirection,");
            query.Append("id.TaxExempt as TaxExempt,");
            query.Append("id.TaxCatID as TaxCatID,");
            query.Append("id.TotalMiscChrg as TotalMiscChrg,");
            query.Append("1 as filler ");

            query.Append(" from pub.InvcDtl as id");
            query.Append(" left join pub.Part as pt");
            query.Append(" on pt.PartNum = id.PartNum ");
            query.Append("");
            query.Append("");
            query.Append(" where id.InvoiceNum = ");
            query.Append(invoiceNum.ToString());
            return query.ToString();
        }
        public ArrayList Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }
        public int InvoiceNum
        {
            get
            {
                return invoiceNum;
            }
            set
            {
                invoiceNum = value;
            }
        }
        public string PilotDsn
        {
            get
            {
                return pilotDsn;
            }
            set
            {
                pilotDsn = value;
            }
        }
        public string ShipToId
        {
            get
            {
                return shipToId;
            }
            set
            {
                shipToId = value;
            }
        }
    }
}
