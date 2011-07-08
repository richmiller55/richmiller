namespace LockBox
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
        private

        System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.tbBankFileOpen = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnMatchToBatch = new System.Windows.Forms.Button();
            this.tbErrorMessages = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(29, 28);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(146, 28);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(99, 23);
            this.btnFileOpen.TabIndex = 1;
            this.btnFileOpen.Text = "File Open";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            // 
            // tbBankFileOpen
            // 
            this.tbBankFileOpen.Location = new System.Drawing.Point(251, 31);
            this.tbBankFileOpen.Name = "tbBankFileOpen";
            this.tbBankFileOpen.Size = new System.Drawing.Size(285, 20);
            this.tbBankFileOpen.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-40, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(816, 316);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnMatchToBatch
            // 
            this.btnMatchToBatch.Location = new System.Drawing.Point(557, 31);
            this.btnMatchToBatch.Name = "btnMatchToBatch";
            this.btnMatchToBatch.Size = new System.Drawing.Size(99, 23);
            this.btnMatchToBatch.TabIndex = 5;
            this.btnMatchToBatch.Text = "Write Batch";
            this.btnMatchToBatch.UseVisualStyleBackColor = true;
            // 
            // tbErrorMessages
            // 
            this.tbErrorMessages.Location = new System.Drawing.Point(0, 408);
            this.tbErrorMessages.Multiline = true;
            this.tbErrorMessages.Name = "tbErrorMessages";
            this.tbErrorMessages.Size = new System.Drawing.Size(776, 182);
            this.tbErrorMessages.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 704);
            this.Controls.Add(this.tbErrorMessages);
            this.Controls.Add(this.btnMatchToBatch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbBankFileOpen);
            this.Controls.Add(this.btnFileOpen);
            this.Controls.Add(this.btnLogin);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.TextBox tbBankFileOpen;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnMatchToBatch;
        private System.Windows.Forms.TextBox tbErrorMessages;
    }
}

