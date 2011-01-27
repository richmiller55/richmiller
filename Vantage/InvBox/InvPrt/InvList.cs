using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InvPrt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            btn_Print.Click += new EventHandler(btn_Print_Click);
        }

        void btn_Print_Click(object sender, EventArgs e)
        {
            string invoiceList = tb_invoiceList.Text;
        }
    }
}