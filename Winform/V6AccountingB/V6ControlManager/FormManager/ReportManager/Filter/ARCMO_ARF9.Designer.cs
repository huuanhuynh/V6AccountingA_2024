namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ARCMO_ARF9
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.taiKhoan = new V6ReportControls.FilterLineVvarTextBox();
            this.maKhachHang = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.taiKhoan);
            this.groupBox1.Controls.Add(this.maKhachHang);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 149);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // taiKhoan
            // 
            this.taiKhoan.AccessibleDescription = "FILTERL00027";
            this.taiKhoan.AccessibleName = "";
            this.taiKhoan.AccessibleName2 = "TK";
            this.taiKhoan.FieldCaption = "Tài khoản";
            this.taiKhoan.FieldName = "TK";
            this.taiKhoan.Location = new System.Drawing.Point(6, 86);
            this.taiKhoan.Name = "taiKhoan";
            this.taiKhoan.Size = new System.Drawing.Size(280, 22);
            this.taiKhoan.TabIndex = 5;
            this.taiKhoan.Vvar = "TK";
            // 
            // maKhachHang
            // 
            this.maKhachHang.AccessibleDescription = "FILTERL00007";
            this.maKhachHang.AccessibleName2 = "MA_KH";
            this.maKhachHang.FieldCaption = "Mã khách hàng";
            this.maKhachHang.FieldName = "MA_KH";
            this.maKhachHang.Location = new System.Drawing.Point(6, 31);
            this.maKhachHang.Name = "maKhachHang";
            this.maKhachHang.Size = new System.Drawing.Size(280, 22);
            this.maKhachHang.TabIndex = 2;
            this.maKhachHang.Vvar = "MA_KH";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName = "";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 59);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(280, 22);
            this.txtMaDvcs.TabIndex = 4;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Từ ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(141, 39);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(141, 18);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // ARCMO_ARF9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Name = "ARCMO_ARF9";
            this.Size = new System.Drawing.Size(292, 230);
            this.Load += new System.EventHandler(this.Filter_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox maKhachHang;
        private V6ReportControls.FilterLineVvarTextBox taiKhoan;

    }
}
