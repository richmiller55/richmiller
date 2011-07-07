using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB1
{
    public partial class LoginDlg : Form
    {
        MainForm mainForm;
        public LoginDlg(MainForm mf)
        {
            mainForm = mf;
            InitializeComponent();
            btnLogin.Click += new EventHandler(btnLoginForm_Click);
            btnOK.Click += new EventHandler(btnOK_Click);
            tbPassword.Text = "homefed55";
            tbUser.Text = "rich";
            cbVanDatabase.Text = "Test";
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        void btnLoginForm_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text;
            string password = tbPassword.Text;
            string database = cbVanDatabase.Text;
            VanAccess va = new VanAccess(user, password, database);
            mainForm.SetVanAccess(va);  
        }

    }
}