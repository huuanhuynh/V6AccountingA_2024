namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class V6COPY_RA
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
            this.chkSoDuVaLuyKe = new V6Controls.V6CheckBox();
            this.chkDuLieu = new V6Controls.V6CheckBox();
            this.chkDanhMuc = new V6Controls.V6CheckBox();
            this.txtFileName = new V6Controls.V6ColorTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.label2 = new System.Windows.Forms.Label();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnChonDanhSachDonVi = new System.Windows.Forms.Button();
            this.txtDanhSachDonVi = new V6Controls.V6VvarTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDanhSachDonVi);
            this.panel1.Controls.Add(this.chkSoDuVaLuyKe);
            this.panel1.Controls.Add(this.chkDuLieu);
            this.panel1.Controls.Add(this.chkDanhMuc);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateNgay_ct2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnChonDanhSachDonVi);
            this.panel1.Controls.Add(this.dateNgay_ct1);
            this.panel1.Controls.Add(this.btnSaveAs);
            // 
            // chkSoDuVaLuyKe
            // 
            this.chkSoDuVaLuyKe.AccessibleDescription = "XULYC00007";
            this.chkSoDuVaLuyKe.AutoSize = true;
            this.chkSoDuVaLuyKe.Location = new System.Drawing.Point(317, 202);
            this.chkSoDuVaLuyKe.Name = "chkSoDuVaLuyKe";
            this.chkSoDuVaLuyKe.Size = new System.Drawing.Size(100, 17);
            this.chkSoDuVaLuyKe.TabIndex = 30;
            this.chkSoDuVaLuyKe.TabStop = false;
            this.chkSoDuVaLuyKe.Text = "Số dư và lũy kế";
            this.chkSoDuVaLuyKe.UseVisualStyleBackColor = true;
            // 
            // chkDuLieu
            // 
            this.chkDuLieu.AccessibleDescription = "XULYC00006";
            this.chkDuLieu.AutoSize = true;
            this.chkDuLieu.Location = new System.Drawing.Point(170, 202);
            this.chkDuLieu.Name = "chkDuLieu";
            this.chkDuLieu.Size = new System.Drawing.Size(59, 17);
            this.chkDuLieu.TabIndex = 31;
            this.chkDuLieu.TabStop = false;
            this.chkDuLieu.Text = "Dữ liệu";
            this.chkDuLieu.UseVisualStyleBackColor = true;
            // 
            // chkDanhMuc
            // 
            this.chkDanhMuc.AccessibleDescription = "XULYC00005";
            this.chkDanhMuc.AccessibleName = "LOAI_CK";
            this.chkDanhMuc.AutoSize = true;
            this.chkDanhMuc.Location = new System.Drawing.Point(11, 202);
            this.chkDanhMuc.Name = "chkDanhMuc";
            this.chkDanhMuc.Size = new System.Drawing.Size(75, 17);
            this.chkDanhMuc.TabIndex = 32;
            this.chkDanhMuc.TabStop = false;
            this.chkDanhMuc.Text = "Danh mục";
            this.chkDanhMuc.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            this.txtFileName.AccessibleName = "ten_dvcs";
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtFileName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFileName.HoverColor = System.Drawing.Color.Yellow;
            this.txtFileName.LeaveColor = System.Drawing.Color.White;
            this.txtFileName.Location = new System.Drawing.Point(6, 157);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(450, 20);
            this.txtFileName.TabIndex = 29;
            this.txtFileName.Tag = "disable";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00003";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Đến ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(113, 39);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(101, 20);
            this.dateNgay_ct2.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00002";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Từ ngày";
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(113, 12);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(101, 20);
            this.dateNgay_ct1.TabIndex = 26;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.AccessibleDescription = "XULYB00006";
            this.btnSaveAs.Location = new System.Drawing.Point(6, 128);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(197, 23);
            this.btnSaveAs.TabIndex = 24;
            this.btnSaveAs.Text = "Chọn file lưu";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnChonDanhSachDonVi
            // 
            this.btnChonDanhSachDonVi.AccessibleDescription = "XULYB00005";
            this.btnChonDanhSachDonVi.Location = new System.Drawing.Point(6, 73);
            this.btnChonDanhSachDonVi.Name = "btnChonDanhSachDonVi";
            this.btnChonDanhSachDonVi.Size = new System.Drawing.Size(197, 23);
            this.btnChonDanhSachDonVi.TabIndex = 24;
            this.btnChonDanhSachDonVi.Text = "Chọn danh sách đơn vị";
            this.btnChonDanhSachDonVi.UseVisualStyleBackColor = true;
            this.btnChonDanhSachDonVi.Click += new System.EventHandler(this.btnChonDanhSachDonVi_Click);
            // 
            // txtDanhSachDonVi
            // 
            this.txtDanhSachDonVi.AccessibleName = "ma_dvcs";
            this.txtDanhSachDonVi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDanhSachDonVi.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtDanhSachDonVi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDanhSachDonVi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDanhSachDonVi.BrotherFields = "ten_dvcs";
            this.txtDanhSachDonVi.CheckOnLeave = false;
            this.txtDanhSachDonVi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDanhSachDonVi.F2 = true;
            this.txtDanhSachDonVi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachDonVi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachDonVi.HoverColor = System.Drawing.Color.Yellow;
            this.txtDanhSachDonVi.LeaveColor = System.Drawing.Color.White;
            this.txtDanhSachDonVi.Location = new System.Drawing.Point(6, 102);
            this.txtDanhSachDonVi.Name = "txtDanhSachDonVi";
            this.txtDanhSachDonVi.ReadOnly = true;
            this.txtDanhSachDonVi.Size = new System.Drawing.Size(450, 20);
            this.txtDanhSachDonVi.TabIndex = 33;
            this.txtDanhSachDonVi.VVar = "ma_dvcs";
            // 
            // V6COPY_RA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "V6COPY_RA";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.V6CheckBox chkSoDuVaLuyKe;
        private V6Controls.V6CheckBox chkDuLieu;
        private V6Controls.V6CheckBox chkDanhMuc;
        private V6Controls.V6ColorTextBox txtFileName;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnChonDanhSachDonVi;
        private V6Controls.V6VvarTextBox txtDanhSachDonVi;
    }
}
