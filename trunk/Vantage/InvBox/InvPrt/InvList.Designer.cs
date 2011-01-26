namespace WindowsApplication1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tb_invoiceList = new System.Windows.Forms.TextBox();
            this.btn_Print = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_invoiceList
            // 
            this.tb_invoiceList.Location = new System.Drawing.Point(33, 12);
            this.tb_invoiceList.Multiline = true;
            this.tb_invoiceList.Name = "tb_invoiceList";
            this.tb_invoiceList.Size = new System.Drawing.Size(163, 232);
            this.tb_invoiceList.TabIndex = 0;
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(205, 12);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 23);
            this.btn_Print.TabIndex = 1;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.tb_invoiceList);
            this.Name = "MainForm";
            this.Text = "InvoicePrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_invoiceList;
        private System.Windows.Forms.Button btn_Print;
    }
}

