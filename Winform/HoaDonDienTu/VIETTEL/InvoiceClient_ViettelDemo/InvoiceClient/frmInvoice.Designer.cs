namespace InvoiceClient
{
    partial class frmInvoice
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
            this.btnCreateInvoice = new System.Windows.Forms.Button();
            this.btnSaveInvoice = new System.Windows.Forms.Button();
            this.btnSavePDF = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChangeMoney = new System.Windows.Forms.Button();
            this.btnChangeInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.Location = new System.Drawing.Point(61, 29);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(149, 40);
            this.btnCreateInvoice.TabIndex = 0;
            this.btnCreateInvoice.Text = "Lập hóa đơn";
            this.btnCreateInvoice.UseVisualStyleBackColor = true;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // btnSaveInvoice
            // 
            this.btnSaveInvoice.Location = new System.Drawing.Point(234, 29);
            this.btnSaveInvoice.Name = "btnSaveInvoice";
            this.btnSaveInvoice.Size = new System.Drawing.Size(149, 40);
            this.btnSaveInvoice.TabIndex = 1;
            this.btnSaveInvoice.Text = "Tải file XML hóa đơn";
            this.btnSaveInvoice.UseVisualStyleBackColor = true;
            this.btnSaveInvoice.Click += new System.EventHandler(this.btnSaveInvoice_Click);
            // 
            // btnSavePDF
            // 
            this.btnSavePDF.Location = new System.Drawing.Point(234, 85);
            this.btnSavePDF.Name = "btnSavePDF";
            this.btnSavePDF.Size = new System.Drawing.Size(149, 40);
            this.btnSavePDF.TabIndex = 2;
            this.btnSavePDF.Text = "Tải file pdf hóa đơn";
            this.btnSavePDF.UseVisualStyleBackColor = true;
            this.btnSavePDF.Click += new System.EventHandler(this.btnSavePDF_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy hóa đơn";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChangeMoney
            // 
            this.btnChangeMoney.Location = new System.Drawing.Point(61, 85);
            this.btnChangeMoney.Name = "btnChangeMoney";
            this.btnChangeMoney.Size = new System.Drawing.Size(149, 40);
            this.btnChangeMoney.TabIndex = 4;
            this.btnChangeMoney.Text = "Lập hóa đơn điều chỉnh tiền";
            this.btnChangeMoney.UseVisualStyleBackColor = true;
            this.btnChangeMoney.Click += new System.EventHandler(this.btnChangeMoney_Click);
            // 
            // btnChangeInfo
            // 
            this.btnChangeInfo.Location = new System.Drawing.Point(61, 142);
            this.btnChangeInfo.Name = "btnChangeInfo";
            this.btnChangeInfo.Size = new System.Drawing.Size(149, 40);
            this.btnChangeInfo.TabIndex = 5;
            this.btnChangeInfo.Text = "Lập hóa đơn điều chỉnh thông tin";
            this.btnChangeInfo.UseVisualStyleBackColor = true;
            this.btnChangeInfo.Click += new System.EventHandler(this.btnChangeInfo_Click);
            // 
            // frmInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 265);
            this.Controls.Add(this.btnChangeInfo);
            this.Controls.Add(this.btnChangeMoney);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSavePDF);
            this.Controls.Add(this.btnSaveInvoice);
            this.Controls.Add(this.btnCreateInvoice);
            this.Name = "frmInvoice";
            this.Text = "InvoiceClient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateInvoice;
        private System.Windows.Forms.Button btnSaveInvoice;
        private System.Windows.Forms.Button btnSavePDF;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangeMoney;
        private System.Windows.Forms.Button btnChangeInfo;
    }
}

