﻿namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AAPPR_EINVOICE1_F9
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
            this.components = new System.ComponentModel.Container();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.cboAlxuly = new System.Windows.Forms.ComboBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.lblTenXuLy = new V6Controls.V6Label();
            this.SuspendLayout();
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 112);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 112);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // cboAlxuly
            // 
            this.cboAlxuly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlxuly.FormattingEnabled = true;
            this.cboAlxuly.Items.AddRange(new object[] {
            "G",
            "G1",
            "G2",
            "G3",
            "T"});
            this.cboAlxuly.Location = new System.Drawing.Point(197, 49);
            this.cboAlxuly.Name = "cboAlxuly";
            this.cboAlxuly.Size = new System.Drawing.Size(121, 21);
            this.cboAlxuly.TabIndex = 25;
            this.cboAlxuly.SelectedIndexChanged += new System.EventHandler(this.cboAlxuly_SelectedIndexChanged);
            // 
            // v6Label2
            // 
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(97, 52);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(30, 13);
            this.v6Label2.TabIndex = 26;
            this.v6Label2.Text = "Xử lý";
            // 
            // lblTenXuLy
            // 
            this.lblTenXuLy.AutoSize = true;
            this.lblTenXuLy.Location = new System.Drawing.Point(324, 52);
            this.lblTenXuLy.Name = "lblTenXuLy";
            this.lblTenXuLy.Size = new System.Drawing.Size(16, 13);
            this.lblTenXuLy.TabIndex = 27;
            this.lblTenXuLy.Text = "...";
            // 
            // AAPPR_EINVOICE1_F9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 158);
            this.Controls.Add(this.lblTenXuLy);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.cboAlxuly);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AAPPR_EINVOICE1_F9";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.cboAlxuly, 0);
            this.Controls.SetChildIndex(this.v6Label2, 0);
            this.Controls.SetChildIndex(this.lblTenXuLy, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.ComboBox cboAlxuly;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label lblTenXuLy;




    }
}