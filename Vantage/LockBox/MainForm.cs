using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace LockBox
{
    public partial class MainForm : Form
    {
        string filename;
        DataTable bft;
        BankFile bankFile;
        Epicor.Mfg.Core.Session objSess;
        VanAccess vanAccess;
        public MainForm()
        {
            objSess = new Epicor.Mfg.Core.Session("rich", "homefed55",
                "AppServerDC://VantageDB1:8331", Epicor.Mfg.Core.Session.LicenseType.Default);
            InitializeComponent();
            bft = new DataTable();
            bankFile = new BankFile();
            setupDataTable();
            
            btnLogin.Click += new System.EventHandler(btnLogin_Click);
            btnFileOpen.Click += new System.EventHandler(btnFileOpen_Click);
            // btnReadFile.Click += new EventHandler(btnReadFile_Click);
            btnMatchToBatch.Click += new EventHandler(btnMatchToBatch_Click);
        }

        void btnMatchToBatch_Click(object sender, EventArgs e)
        {
            CashGrp cashGrp = new CashGrp(objSess, bft,bankFile);
            ArrayList errorMessages = cashGrp.GetErrorMessages();
            string allMessages = "";
            foreach (string message in errorMessages)
            {
                allMessages += message + Environment.NewLine;
                tbErrorMessages.Text = allMessages;
            }
        }
        void setupDataTable()
        {
            DataColumn dcTransNo = new DataColumn("TransNo");
            DataColumn dcCustNo = new DataColumn("CustNo");
            DataColumn dcInvoiceNo = new DataColumn("InvoiceNo");
            DataColumn dcPaymentAmt = new DataColumn("PaymentAmt");
            DataColumn dcStubSeq = new DataColumn("StubSeq");
            DataColumn dcCheckNo = new DataColumn("CheckNo");
            DataColumn dcVanOpenAmt = new DataColumn("VanOpenAmt");
            DataColumn dcMatchStatus = new DataColumn("MatchStatus");

            dcTransNo.ColumnName = "TransNo";
            dcCustNo.ColumnName = "CustNo";
            dcInvoiceNo.ColumnName = "InvoiceNo";
            dcPaymentAmt.ColumnName = "PaymentAmt";
            dcStubSeq.ColumnName = "StubSeq";
            dcCheckNo.ColumnName = "CheckNo";
            dcVanOpenAmt.ColumnName = "VanOpenAmt";
            dcMatchStatus.ColumnName = "MatchStatus";
            
            dcTransNo.DataType = System.Type.GetType("System.String");
            dcCustNo.DataType = System.Type.GetType("System.String");
            dcInvoiceNo.DataType = System.Type.GetType("System.String");
            dcPaymentAmt.DataType = System.Type.GetType("System.Decimal");
            dcStubSeq.DataType = System.Type.GetType("System.String");
            dcCheckNo.DataType = System.Type.GetType("System.String");
            dcVanOpenAmt.DataType = System.Type.GetType("System.Decimal");
            dcMatchStatus.DataType = System.Type.GetType("System.String");
            
            dcTransNo.DefaultValue = "default string";
            dcCustNo.DefaultValue = "default string";
            dcInvoiceNo.DefaultValue = "default string";
            dcPaymentAmt.DefaultValue = 0.0;
            dcStubSeq.DefaultValue = "default string";
            dcCheckNo.DefaultValue = "default string";
            dcVanOpenAmt.DefaultValue = 0.0;
            dcMatchStatus.DefaultValue = "default string";

            bft.Columns.Add(dcTransNo);
            bft.Columns.Add(dcCustNo);
            bft.Columns.Add(dcInvoiceNo);
            bft.Columns.Add(dcPaymentAmt);
            bft.Columns.Add(dcStubSeq);
            bft.Columns.Add(dcCheckNo);
            bft.Columns.Add(dcVanOpenAmt);
            bft.Columns.Add(dcMatchStatus);
        }
        InvoiceInfo LookupInvoiceNum(int invoiceNum)
        {
            InvoiceLookup lookup = new InvoiceLookup(invoiceNum);
            InvoiceInfo info = lookup.getInfo();
            return info;
        }
        void btnReadFile_Click(object sender, EventArgs e)
        {
            FlatFileReader reader = new FlatFileReader(filename,bft,bankFile);
            bankFile = reader.getBankFile();
            dataGridView1.DataSource = bft;
        }
        void btnLogin_Click(object sender, EventArgs e)
        {
            LoginDlg dlg = new LoginDlg(this);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // got here
                // string result = "testOk";
            }
        }
        void btnFileOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Text Files (*.txt)|*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFile.FileNames.Length; i++)
                {
                    filename = Path.GetFullPath(openFile.FileNames[i].ToString());
                    tbBankFileOpen.Text = filename;
                }
                FlatFileReader reader = new FlatFileReader(filename, bft, bankFile);
                bankFile = reader.getBankFile();
                dataGridView1.DataSource = bft;
            }
        }
        public void SetVanAccess(VanAccess va)
        {
            vanAccess = va;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

            // this.reportViewer1.RefreshReport();
        }
    }
}