namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AAPPR_EINVOICE1_Filter
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtma_thue = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox11 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.txtTk_thue_co = new V6ReportControls.FilterLineVvarTextBox();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.Txtma_kh = new V6ReportControls.FilterLineVvarTextBox();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChuyenExcelHTKK = new System.Windows.Forms.Button();
            this.btnChuyenExcelTaxOnline = new System.Windows.Forms.Button();
            this.txtFileName = new V6Controls.V6ColorTextBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.chkNhomCt = new System.Windows.Forms.CheckBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtLoai = new V6Controls.V6VvarTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.cboSendType = new System.Windows.Forms.ComboBox();
            this.btnSuaChiTieu = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 39);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtma_thue);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox11);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.txtTk_thue_co);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.Txtma_kh);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtma_thue
            // 
            this.txtma_thue.AccessibleDescription = "FILTERL00143";
            this.txtma_thue.AccessibleName2 = "MA_THUE";
            this.txtma_thue.Caption = "Mã thuế";
            this.txtma_thue.FieldName = "MA_THUE";
            this.txtma_thue.Location = new System.Drawing.Point(6, 131);
            this.txtma_thue.Name = "txtma_thue";
            this.txtma_thue.Size = new System.Drawing.Size(282, 22);
            this.txtma_thue.TabIndex = 6;
            this.txtma_thue.Vvar = "MA_THUE";
            // 
            // filterLineVvarTextBox11
            // 
            this.filterLineVvarTextBox11.AccessibleDescription = "FILTERL00004";
            this.filterLineVvarTextBox11.AccessibleName2 = "MA_CT";
            this.filterLineVvarTextBox11.Caption = "Mã chứng từ";
            this.filterLineVvarTextBox11.FieldName = "MA_CT";
            this.filterLineVvarTextBox11.Location = new System.Drawing.Point(6, 85);
            this.filterLineVvarTextBox11.Name = "filterLineVvarTextBox11";
            this.filterLineVvarTextBox11.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox11.TabIndex = 4;
            this.filterLineVvarTextBox11.Vvar = "MA_CT";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 16);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
            this.radOr.Text = "Điều kiện (OR)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // txtTk_thue_co
            // 
            this.txtTk_thue_co.AccessibleDescription = "FILTERL00142";
            this.txtTk_thue_co.AccessibleName2 = "TK_THUE_CO";
            this.txtTk_thue_co.Caption = "Tài khoản thuế";
            this.txtTk_thue_co.FieldName = "TK_THUE_CO";
            this.txtTk_thue_co.Location = new System.Drawing.Point(6, 108);
            this.txtTk_thue_co.Name = "txtTk_thue_co";
            this.txtTk_thue_co.Size = new System.Drawing.Size(282, 22);
            this.txtTk_thue_co.TabIndex = 5;
            this.txtTk_thue_co.Vvar = "TK";
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 16);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // Txtma_kh
            // 
            this.Txtma_kh.AccessibleDescription = "FILTERL00007";
            this.Txtma_kh.AccessibleName2 = "MA_KH";
            this.Txtma_kh.Caption = "Mã khách";
            this.Txtma_kh.FieldName = "MA_KH";
            this.Txtma_kh.Location = new System.Drawing.Point(6, 62);
            this.Txtma_kh.Name = "Txtma_kh";
            this.Txtma_kh.Size = new System.Drawing.Size(282, 22);
            this.Txtma_kh.TabIndex = 3;
            this.Txtma_kh.Vvar = "MA_KH";
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(131, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(131, 29);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // btnChuyenExcelHTKK
            // 
            this.btnChuyenExcelHTKK.AccessibleDescription = "FILTERB00002";
            this.btnChuyenExcelHTKK.Location = new System.Drawing.Point(3, 193);
            this.btnChuyenExcelHTKK.Name = "btnChuyenExcelHTKK";
            this.btnChuyenExcelHTKK.Size = new System.Drawing.Size(136, 23);
            this.btnChuyenExcelHTKK.TabIndex = 8;
            this.btnChuyenExcelHTKK.Text = "Chuyển Excel HTKK";
            this.btnChuyenExcelHTKK.UseVisualStyleBackColor = true;
            this.btnChuyenExcelHTKK.Click += new System.EventHandler(this.btnChuyenExcelHTKK_Click);
            // 
            // btnChuyenExcelTaxOnline
            // 
            this.btnChuyenExcelTaxOnline.AccessibleDescription = "FILTERB00003";
            this.btnChuyenExcelTaxOnline.Location = new System.Drawing.Point(145, 193);
            this.btnChuyenExcelTaxOnline.Name = "btnChuyenExcelTaxOnline";
            this.btnChuyenExcelTaxOnline.Size = new System.Drawing.Size(146, 23);
            this.btnChuyenExcelTaxOnline.TabIndex = 9;
            this.btnChuyenExcelTaxOnline.Text = "Chuyển Excel Tax online";
            this.btnChuyenExcelTaxOnline.UseVisualStyleBackColor = true;
            this.btnChuyenExcelTaxOnline.Click += new System.EventHandler(this.btnChuyenExcelTaxOnline_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.AccessibleName = "FileName";
            this.txtFileName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtFileName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFileName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFileName.HoverColor = System.Drawing.Color.Yellow;
            this.txtFileName.LeaveColor = System.Drawing.Color.White;
            this.txtFileName.Location = new System.Drawing.Point(3, 164);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(246, 20);
            this.txtFileName.TabIndex = 5;
            this.txtFileName.TabStop = false;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(255, 162);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(36, 23);
            this.btnChon.TabIndex = 6;
            this.btnChon.Text = "...";
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // chkNhomCt
            // 
            this.chkNhomCt.AccessibleDescription = "FILTERC00012";
            this.chkNhomCt.AccessibleName = "NHOM_CT";
            this.chkNhomCt.AutoSize = true;
            this.chkNhomCt.Location = new System.Drawing.Point(131, 58);
            this.chkNhomCt.Name = "chkNhomCt";
            this.chkNhomCt.Size = new System.Drawing.Size(123, 17);
            this.chkNhomCt.TabIndex = 4;
            this.chkNhomCt.Text = "Nhóm theo chứng từ";
            this.chkNhomCt.UseVisualStyleBackColor = true;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00092";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(5, 84);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(27, 13);
            this.v6Label2.TabIndex = 12;
            this.v6Label2.Text = "Loại";
            // 
            // txtLoai
            // 
            this.txtLoai.AccessibleName = "LOAI";
            this.txtLoai.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLoai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoai.HoverColor = System.Drawing.Color.Yellow;
            this.txtLoai.LeaveColor = System.Drawing.Color.White;
            this.txtLoai.LimitCharacters = "012";
            this.txtLoai.Location = new System.Drawing.Point(84, 81);
            this.txtLoai.MaxLength = 1;
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.Size = new System.Drawing.Size(18, 20);
            this.txtLoai.TabIndex = 13;
            this.txtLoai.Text = "0";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00144";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(108, 84);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(132, 13);
            this.v6Label1.TabIndex = 12;
            this.v6Label1.Text = "0 - Không lấy hóa đơn hủy";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00145";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(108, 97);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(116, 13);
            this.v6Label3.TabIndex = 12;
            this.v6Label3.Text = "1 - Chỉ lấy hóa đơn hủy";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00146";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(108, 110);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(53, 13);
            this.v6Label4.TabIndex = 12;
            this.v6Label4.Text = "2 - Tất cả";
            // 
            // cboSendType
            // 
            this.cboSendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendType.FormattingEnabled = true;
            this.cboSendType.Items.AddRange(new object[] {
            "VIETTEL",
            "VNPT",
            "BKAV",
            "VNPT_TOKEN"});
            this.cboSendType.Location = new System.Drawing.Point(3, 134);
            this.cboSendType.Name = "cboSendType";
            this.cboSendType.Size = new System.Drawing.Size(121, 21);
            this.cboSendType.TabIndex = 24;
            // 
            // btnSuaChiTieu
            // 
            this.btnSuaChiTieu.AccessibleDescription = "FILTERB00001";
            this.btnSuaChiTieu.Location = new System.Drawing.Point(137, 133);
            this.btnSuaChiTieu.Name = "btnSuaChiTieu";
            this.btnSuaChiTieu.Size = new System.Drawing.Size(184, 23);
            this.btnSuaChiTieu.TabIndex = 23;
            this.btnSuaChiTieu.Text = "Sửa thông tin dữ liệu và kết nối";
            this.btnSuaChiTieu.UseVisualStyleBackColor = true;
            this.btnSuaChiTieu.Click += new System.EventHandler(this.btnSuaChiTieu_Click);
            // 
            // AAPPR_EINVOICE1_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboSendType);
            this.Controls.Add(this.btnSuaChiTieu);
            this.Controls.Add(this.txtLoai);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.chkNhomCt);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.btnChuyenExcelTaxOnline);
            this.Controls.Add(this.btnChuyenExcelHTKK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AAPPR_EINVOICE1_Filter";
            this.Size = new System.Drawing.Size(295, 426);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChuyenExcelHTKK;
        private System.Windows.Forms.Button btnChuyenExcelTaxOnline;
        private V6Controls.V6ColorTextBox txtFileName;
        private System.Windows.Forms.Button btnChon;
        private System.Windows.Forms.CheckBox chkNhomCt;
        private V6ReportControls.FilterLineVvarTextBox txtma_thue;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox11;
        private V6ReportControls.FilterLineVvarTextBox txtTk_thue_co;
        private V6ReportControls.FilterLineVvarTextBox Txtma_kh;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox txtLoai;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private System.Windows.Forms.ComboBox cboSendType;
        private System.Windows.Forms.Button btnSuaChiTieu;
    }
}
