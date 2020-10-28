namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AAPPR_IND_F10
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
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtMa_Xuly = new V6Controls.V6LookupTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.txtten_xuly = new V6Controls.V6ColorTextBox();
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
            this.btnHuy.Location = new System.Drawing.Point(94, 197);
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
            this.btnNhan.Location = new System.Drawing.Point(6, 197);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtMa_Xuly
            // 
            this.txtMa_Xuly.AccessibleName = "Ma_xuly";
            this.txtMa_Xuly.AccessibleName2 = "Ma_xuly";
            this.txtMa_Xuly.BackColor = System.Drawing.Color.White;
            this.txtMa_Xuly.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_Xuly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMa_Xuly.BrotherFields = "Ten_xuly";
            this.txtMa_Xuly.CheckNotEmpty = true;
            this.txtMa_Xuly.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_Xuly.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_Xuly.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_Xuly.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_Xuly.LeaveColor = System.Drawing.Color.White;
            this.txtMa_Xuly.Location = new System.Drawing.Point(89, 7);
            this.txtMa_Xuly.Ma_dm = "ALXULY";
            this.txtMa_Xuly.Name = "txtMa_Xuly";
            this.txtMa_Xuly.ParentData = null;
            this.txtMa_Xuly.Size = new System.Drawing.Size(100, 20);
            this.txtMa_Xuly.TabIndex = 9;
            this.txtMa_Xuly.ValueField = "MA_XULY";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00038";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(-1, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(46, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Mã xử lý";
            // 
            // txtten_xuly
            // 
            this.txtten_xuly.AccessibleName = "ten_xuly";
            this.txtten_xuly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtten_xuly.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtten_xuly.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtten_xuly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtten_xuly.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtten_xuly.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtten_xuly.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtten_xuly.HoverColor = System.Drawing.Color.Yellow;
            this.txtten_xuly.LeaveColor = System.Drawing.Color.White;
            this.txtten_xuly.Location = new System.Drawing.Point(195, 7);
            this.txtten_xuly.Name = "txtten_xuly";
            this.txtten_xuly.ReadOnly = true;
            this.txtten_xuly.Size = new System.Drawing.Size(361, 20);
            this.txtten_xuly.TabIndex = 32;
            this.txtten_xuly.Tag = "disable";
            // 
            // AAPPR_IND_F10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtten_xuly);
            this.Controls.Add(this.txtMa_Xuly);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AAPPR_IND_F10";
            this.Size = new System.Drawing.Size(559, 240);
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6LookupTextBox txtMa_Xuly;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6ColorTextBox txtten_xuly;




    }
}