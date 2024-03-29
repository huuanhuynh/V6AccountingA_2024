﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AAPPR_AR12_filter
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
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtMa_ct = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineTrangThai = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaXuly = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH9 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH8 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH7 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh6 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh3 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineSO_SERI = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaMauHD = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMA_SONB = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaKho = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox8 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox4 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.TxtXtag = new V6Controls.V6VvarTextBox();
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.chkLike = new V6Controls.V6CheckBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.chkHoaDonDaIn = new V6Controls.V6CheckBox();
            this.v6Label5 = new V6Controls.V6Label();
            this.btnSuaChiTieu = new System.Windows.Forms.Button();
            this.cboSendType = new V6Controls.Controls.V6IndexComboBox();
            this.grbTienTe = new System.Windows.Forms.GroupBox();
            this.rNgoaiTe = new System.Windows.Forms.RadioButton();
            this.rTienViet = new System.Windows.Forms.RadioButton();
            this.btnCheckConnection = new V6Controls.Controls.V6FormButton();
            this.groupBox1.SuspendLayout();
            this.grbTienTe.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00004";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 48);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(70, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Mã  chứng từ";
            // 
            // TxtMa_ct
            // 
            this.TxtMa_ct.AccessibleName = "MA_CT";
            this.TxtMa_ct.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_ct.CheckNotEmpty = true;
            this.TxtMa_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_ct.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_ct.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_ct.Location = new System.Drawing.Point(112, 46);
            this.TxtMa_ct.Name = "TxtMa_ct";
            this.TxtMa_ct.Size = new System.Drawing.Size(100, 20);
            this.TxtMa_ct.TabIndex = 2;
            this.TxtMa_ct.VVar = "MA_CT";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(112, 25);
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(112, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lineTrangThai);
            this.groupBox1.Controls.Add(this.lineMaXuly);
            this.groupBox1.Controls.Add(this.lineNH_KH9);
            this.groupBox1.Controls.Add(this.lineNH_KH8);
            this.groupBox1.Controls.Add(this.lineNH_KH7);
            this.groupBox1.Controls.Add(this.Txtnh_kh6);
            this.groupBox1.Controls.Add(this.Txtnh_kh5);
            this.groupBox1.Controls.Add(this.Txtnh_kh4);
            this.groupBox1.Controls.Add(this.Txtnh_kh1);
            this.groupBox1.Controls.Add(this.Txtnh_kh2);
            this.groupBox1.Controls.Add(this.Txtnh_kh3);
            this.groupBox1.Controls.Add(this.lineSO_SERI);
            this.groupBox1.Controls.Add(this.lineMaMauHD);
            this.groupBox1.Controls.Add(this.lineMA_SONB);
            this.groupBox1.Controls.Add(this.lineMaDvcs);
            this.groupBox1.Controls.Add(this.lineMaKho);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox8);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox4);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 464);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineTrangThai
            // 
            this.lineTrangThai.AccessibleDescription = "FILTERL00272";
            this.lineTrangThai.AccessibleName2 = "STATUS_IN";
            this.lineTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineTrangThai.Caption = "Trạng thái";
            this.lineTrangThai.FieldName = "STATUS_IN";
            this.lineTrangThai.Location = new System.Drawing.Point(4, 149);
            this.lineTrangThai.Name = "lineTrangThai";
            this.lineTrangThai.Size = new System.Drawing.Size(294, 22);
            this.lineTrangThai.TabIndex = 7;
            this.lineTrangThai.Vvar = "";
            // 
            // lineMaXuly
            // 
            this.lineMaXuly.AccessibleDescription = "FILTERL00271";
            this.lineMaXuly.AccessibleName2 = "MA_XULY";
            this.lineMaXuly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaXuly.Caption = "Mã xử lý";
            this.lineMaXuly.FieldName = "MA_XULY";
            this.lineMaXuly.Location = new System.Drawing.Point(4, 127);
            this.lineMaXuly.Name = "lineMaXuly";
            this.lineMaXuly.Size = new System.Drawing.Size(294, 22);
            this.lineMaXuly.TabIndex = 6;
            this.lineMaXuly.Vvar = "MA_XULY";
            // 
            // lineNH_KH9
            // 
            this.lineNH_KH9.AccessibleDescription = "FILTERL00019";
            this.lineNH_KH9.AccessibleName2 = "NH_KH9";
            this.lineNH_KH9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineNH_KH9.Caption = "Nhóm khách hàng 9";
            this.lineNH_KH9.FieldName = "NH_KH9";
            this.lineNH_KH9.Location = new System.Drawing.Point(4, 435);
            this.lineNH_KH9.Name = "lineNH_KH9";
            this.lineNH_KH9.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH9.TabIndex = 20;
            this.lineNH_KH9.Vvar = "NH_KH";
            // 
            // lineNH_KH8
            // 
            this.lineNH_KH8.AccessibleDescription = "FILTERL00018";
            this.lineNH_KH8.AccessibleName2 = "NH_KH8";
            this.lineNH_KH8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineNH_KH8.Caption = "Nhóm khách hàng 8";
            this.lineNH_KH8.FieldName = "NH_KH8";
            this.lineNH_KH8.Location = new System.Drawing.Point(4, 413);
            this.lineNH_KH8.Name = "lineNH_KH8";
            this.lineNH_KH8.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH8.TabIndex = 19;
            this.lineNH_KH8.Vvar = "NH_KH";
            // 
            // lineNH_KH7
            // 
            this.lineNH_KH7.AccessibleDescription = "FILTERL00017";
            this.lineNH_KH7.AccessibleName2 = "NH_KH7";
            this.lineNH_KH7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineNH_KH7.Caption = "Nhóm khách hàng 7";
            this.lineNH_KH7.FieldName = "NH_KH7";
            this.lineNH_KH7.Location = new System.Drawing.Point(4, 391);
            this.lineNH_KH7.Name = "lineNH_KH7";
            this.lineNH_KH7.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH7.TabIndex = 18;
            this.lineNH_KH7.Vvar = "NH_KH";
            // 
            // Txtnh_kh6
            // 
            this.Txtnh_kh6.AccessibleDescription = "FILTERL00016";
            this.Txtnh_kh6.AccessibleName2 = "NH_KH6";
            this.Txtnh_kh6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh6.Caption = "Nhóm khách hàng 6";
            this.Txtnh_kh6.FieldName = "NH_KH6";
            this.Txtnh_kh6.Location = new System.Drawing.Point(4, 369);
            this.Txtnh_kh6.Name = "Txtnh_kh6";
            this.Txtnh_kh6.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh6.TabIndex = 17;
            this.Txtnh_kh6.Vvar = "NH_KH";
            // 
            // Txtnh_kh5
            // 
            this.Txtnh_kh5.AccessibleDescription = "FILTERL00015";
            this.Txtnh_kh5.AccessibleName2 = "NH_KH5";
            this.Txtnh_kh5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh5.Caption = "Nhóm khách hàng 5";
            this.Txtnh_kh5.FieldName = "NH_KH5";
            this.Txtnh_kh5.Location = new System.Drawing.Point(4, 347);
            this.Txtnh_kh5.Name = "Txtnh_kh5";
            this.Txtnh_kh5.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh5.TabIndex = 16;
            this.Txtnh_kh5.Vvar = "NH_KH";
            // 
            // Txtnh_kh4
            // 
            this.Txtnh_kh4.AccessibleDescription = "FILTERL00014";
            this.Txtnh_kh4.AccessibleName2 = "NH_KH4";
            this.Txtnh_kh4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh4.Caption = "Nhóm khách hàng 4";
            this.Txtnh_kh4.FieldName = "NH_KH4";
            this.Txtnh_kh4.Location = new System.Drawing.Point(4, 325);
            this.Txtnh_kh4.Name = "Txtnh_kh4";
            this.Txtnh_kh4.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh4.TabIndex = 15;
            this.Txtnh_kh4.Vvar = "NH_KH";
            // 
            // Txtnh_kh1
            // 
            this.Txtnh_kh1.AccessibleDescription = "FILTERL00011";
            this.Txtnh_kh1.AccessibleName2 = "NH_KH1";
            this.Txtnh_kh1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh1.Caption = "Nhóm khách hàng 1";
            this.Txtnh_kh1.FieldName = "NH_KH1";
            this.Txtnh_kh1.Location = new System.Drawing.Point(4, 259);
            this.Txtnh_kh1.Name = "Txtnh_kh1";
            this.Txtnh_kh1.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh1.TabIndex = 12;
            this.Txtnh_kh1.Vvar = "NH_KH";
            // 
            // Txtnh_kh2
            // 
            this.Txtnh_kh2.AccessibleDescription = "FILTERL00012";
            this.Txtnh_kh2.AccessibleName2 = "NH_KH2";
            this.Txtnh_kh2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh2.Caption = "Nhóm khách hàng 2";
            this.Txtnh_kh2.FieldName = "NH_KH2";
            this.Txtnh_kh2.Location = new System.Drawing.Point(4, 281);
            this.Txtnh_kh2.Name = "Txtnh_kh2";
            this.Txtnh_kh2.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh2.TabIndex = 13;
            this.Txtnh_kh2.Vvar = "NH_KH";
            // 
            // Txtnh_kh3
            // 
            this.Txtnh_kh3.AccessibleDescription = "FILTERL00013";
            this.Txtnh_kh3.AccessibleName2 = "NH_KH3";
            this.Txtnh_kh3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Txtnh_kh3.Caption = "Nhóm khách hàng 3";
            this.Txtnh_kh3.FieldName = "NH_KH3";
            this.Txtnh_kh3.Location = new System.Drawing.Point(4, 303);
            this.Txtnh_kh3.Name = "Txtnh_kh3";
            this.Txtnh_kh3.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh3.TabIndex = 14;
            this.Txtnh_kh3.Vvar = "NH_KH";
            // 
            // lineSO_SERI
            // 
            this.lineSO_SERI.AccessibleDescription = "FILTERF00003";
            this.lineSO_SERI.AccessibleName2 = "SO_SERI";
            this.lineSO_SERI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineSO_SERI.Caption = "Số seri";
            this.lineSO_SERI.FieldName = "SO_SERI";
            this.lineSO_SERI.Location = new System.Drawing.Point(4, 105);
            this.lineSO_SERI.Name = "lineSO_SERI";
            this.lineSO_SERI.Size = new System.Drawing.Size(294, 22);
            this.lineSO_SERI.TabIndex = 5;
            this.lineSO_SERI.Vvar = "";
            // 
            // lineMaMauHD
            // 
            this.lineMaMauHD.AccessibleDescription = "FILTERF00002";
            this.lineMaMauHD.AccessibleName2 = "MA_MAUHD";
            this.lineMaMauHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaMauHD.Caption = "Mã mẫu hóa đơn";
            this.lineMaMauHD.FieldName = "MA_MAUHD";
            this.lineMaMauHD.Location = new System.Drawing.Point(4, 83);
            this.lineMaMauHD.Name = "lineMaMauHD";
            this.lineMaMauHD.Size = new System.Drawing.Size(294, 22);
            this.lineMaMauHD.TabIndex = 4;
            this.lineMaMauHD.Vvar = "";
            // 
            // lineMA_SONB
            // 
            this.lineMA_SONB.AccessibleDescription = "FILTERL00079";
            this.lineMA_SONB.AccessibleName2 = "MA_SONB";
            this.lineMA_SONB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMA_SONB.Caption = "Mã số nội bộ";
            this.lineMA_SONB.FieldName = "MA_SONB";
            this.lineMA_SONB.Location = new System.Drawing.Point(4, 61);
            this.lineMA_SONB.Name = "lineMA_SONB";
            this.lineMA_SONB.Size = new System.Drawing.Size(294, 22);
            this.lineMA_SONB.TabIndex = 3;
            this.lineMA_SONB.Vvar = "";
            // 
            // lineMaDvcs
            // 
            this.lineMaDvcs.AccessibleDescription = "FILTERL00005";
            this.lineMaDvcs.AccessibleName2 = "MA_DVCS";
            this.lineMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaDvcs.Caption = "Mã đơn vị";
            this.lineMaDvcs.FieldName = "MA_DVCS";
            this.lineMaDvcs.Location = new System.Drawing.Point(4, 39);
            this.lineMaDvcs.Name = "lineMaDvcs";
            this.lineMaDvcs.Size = new System.Drawing.Size(294, 22);
            this.lineMaDvcs.TabIndex = 2;
            this.lineMaDvcs.Vvar = "MA_DVCS";
            // 
            // lineMaKho
            // 
            this.lineMaKho.AccessibleDescription = "FILTERL00006";
            this.lineMaKho.AccessibleName2 = "MA_KHO";
            this.lineMaKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaKho.Caption = "Mã kho";
            this.lineMaKho.FieldName = "MA_KHO";
            this.lineMaKho.Location = new System.Drawing.Point(4, 171);
            this.lineMaKho.Name = "lineMaKho";
            this.lineMaKho.Size = new System.Drawing.Size(294, 22);
            this.lineMaKho.TabIndex = 8;
            this.lineMaKho.Vvar = "MA_KHO";
            // 
            // filterLineVvarTextBox8
            // 
            this.filterLineVvarTextBox8.AccessibleDescription = "FILTERL00009";
            this.filterLineVvarTextBox8.AccessibleName2 = "MA_NX";
            this.filterLineVvarTextBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox8.Caption = "Mã dạng nx";
            this.filterLineVvarTextBox8.FieldName = "MA_NX";
            this.filterLineVvarTextBox8.Location = new System.Drawing.Point(4, 237);
            this.filterLineVvarTextBox8.Name = "filterLineVvarTextBox8";
            this.filterLineVvarTextBox8.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox8.TabIndex = 11;
            this.filterLineVvarTextBox8.Vvar = "MA_NX";
            // 
            // filterLineVvarTextBox4
            // 
            this.filterLineVvarTextBox4.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox4.AccessibleName2 = "MA_BP";
            this.filterLineVvarTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox4.Caption = "Mã bộ phận";
            this.filterLineVvarTextBox4.FieldName = "MA_BP";
            this.filterLineVvarTextBox4.Location = new System.Drawing.Point(4, 215);
            this.filterLineVvarTextBox4.Name = "filterLineVvarTextBox4";
            this.filterLineVvarTextBox4.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox4.TabIndex = 10;
            this.filterLineVvarTextBox4.Vvar = "MA_BP";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.AccessibleName2 = "MA_KH";
            this.filterLineVvarTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox3.Caption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(4, 193);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox3.TabIndex = 9;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 15);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(156, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "Một trong các điều kiện (or)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 15);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(130, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Tất cả điều kiện (and)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERO00001";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(9, 72);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(70, 13);
            this.v6Label2.TabIndex = 6;
            this.v6Label2.Text = "Lọc chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00024";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(132, 86);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(75, 13);
            this.v6Label3.TabIndex = 9;
            this.v6Label3.Text = "2-Duyệt sổ cái";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00023";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(132, 69);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(137, 13);
            this.v6Label4.TabIndex = 8;
            this.v6Label4.Text = "0-Chưa duyệt, 1- Duyệt kho";
            // 
            // TxtXtag
            // 
            this.TxtXtag.AccessibleName = "CHK_LOC_CT";
            this.TxtXtag.BackColor = System.Drawing.SystemColors.Window;
            this.TxtXtag.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtXtag.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtXtag.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtXtag.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtXtag.HoverColor = System.Drawing.Color.Yellow;
            this.TxtXtag.LeaveColor = System.Drawing.Color.White;
            this.TxtXtag.LimitCharacters = "0125";
            this.TxtXtag.Location = new System.Drawing.Point(114, 68);
            this.TxtXtag.MaxLength = 1;
            this.TxtXtag.Name = "TxtXtag";
            this.TxtXtag.Size = new System.Drawing.Size(18, 20);
            this.TxtXtag.TabIndex = 3;
            // 
            // ctDenSo
            // 
            this.ctDenSo.AccessibleName = "CT_DENSO";
            this.ctDenSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctDenSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctDenSo.Enabled = false;
            this.ctDenSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctDenSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctDenSo.LeaveColor = System.Drawing.Color.White;
            this.ctDenSo.Location = new System.Drawing.Point(115, 190);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(100, 20);
            this.ctDenSo.TabIndex = 6;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00022";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(10, 188);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 19;
            this.v6Label7.Text = "Đến số";
            // 
            // chkLike
            // 
            this.chkLike.AccessibleDescription = "FILTERC00002";
            this.chkLike.AccessibleName = "";
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(69, 165);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 4;
            this.chkLike.Text = "like";
            this.chkLike.UseVisualStyleBackColor = true;
            this.chkLike.CheckedChanged += new System.EventHandler(this.chkLike_CheckedChanged);
            // 
            // ctTuSo
            // 
            this.ctTuSo.AccessibleName = "CT_TUSO";
            this.ctTuSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctTuSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctTuSo.CheckOnLeave = false;
            this.ctTuSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctTuSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctTuSo.LeaveColor = System.Drawing.Color.White;
            this.ctTuSo.Location = new System.Drawing.Point(115, 164);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(100, 20);
            this.ctTuSo.TabIndex = 5;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00021";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(10, 164);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 17;
            this.v6Label8.Text = "CT từ số";
            // 
            // chkHoaDonDaIn
            // 
            this.chkHoaDonDaIn.AccessibleDescription = "FILTERC00001";
            this.chkHoaDonDaIn.AccessibleName = "CHK_DA_IN";
            this.chkHoaDonDaIn.AutoSize = true;
            this.chkHoaDonDaIn.Location = new System.Drawing.Point(117, 124);
            this.chkHoaDonDaIn.Name = "chkHoaDonDaIn";
            this.chkHoaDonDaIn.Size = new System.Drawing.Size(95, 17);
            this.chkHoaDonDaIn.TabIndex = 4;
            this.chkHoaDonDaIn.Text = "Hóa đơn đã in";
            this.chkHoaDonDaIn.UseVisualStyleBackColor = true;
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "FILTERL00025";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(132, 103);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(78, 13);
            this.v6Label5.TabIndex = 20;
            this.v6Label5.Text = "5-Hóa đơn hủy";
            // 
            // btnSuaChiTieu
            // 
            this.btnSuaChiTieu.AccessibleDescription = "FILTERB00001";
            this.btnSuaChiTieu.Location = new System.Drawing.Point(145, 140);
            this.btnSuaChiTieu.Name = "btnSuaChiTieu";
            this.btnSuaChiTieu.Size = new System.Drawing.Size(150, 23);
            this.btnSuaChiTieu.TabIndex = 21;
            this.btnSuaChiTieu.Text = "Cấu hình dữ liệu, kết nối";
            this.filterBaseToolTip1.SetToolTip(this.btnSuaChiTieu, "Shift:Cấu hình con, Ctrl:Xuất excel");
            this.btnSuaChiTieu.UseVisualStyleBackColor = true;
            this.btnSuaChiTieu.Click += new System.EventHandler(this.btnSuaChiTieu_Click);
            // 
            // cboSendType
            // 
            this.cboSendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendType.FormattingEnabled = true;
            this.cboSendType.Items.AddRange(new object[] {
            "VIETTEL",
            "VNPT",
            "BKAV",
            "VNPT_TOKEN",
            "SOFTDREAMS",
            "THAI_SON",
            "MONET",
            "M_INVOICE",
            "VIN",
            "MISA",
            "CYBER"});
            this.cboSendType.Location = new System.Drawing.Point(11, 140);
            this.cboSendType.Name = "cboSendType";
            this.cboSendType.Size = new System.Drawing.Size(121, 21);
            this.cboSendType.TabIndex = 22;
            this.cboSendType.SelectedIndexChanged += new System.EventHandler(this.cboSendType_SelectedIndexChanged);
            // 
            // grbTienTe
            // 
            this.grbTienTe.AccessibleDescription = "REPORTL00007";
            this.grbTienTe.Controls.Add(this.rNgoaiTe);
            this.grbTienTe.Controls.Add(this.rTienViet);
            this.grbTienTe.Location = new System.Drawing.Point(13, 211);
            this.grbTienTe.Name = "grbTienTe";
            this.grbTienTe.Size = new System.Drawing.Size(224, 35);
            this.grbTienTe.TabIndex = 23;
            this.grbTienTe.TabStop = false;
            this.grbTienTe.Text = "Tiền tệ";
            this.grbTienTe.Visible = false;
            // 
            // rNgoaiTe
            // 
            this.rNgoaiTe.AccessibleDescription = "REPORTR00002";
            this.rNgoaiTe.AccessibleName = "Tiếng Việt";
            this.rNgoaiTe.AutoSize = true;
            this.rNgoaiTe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rNgoaiTe.Location = new System.Drawing.Point(83, 13);
            this.rNgoaiTe.Name = "rNgoaiTe";
            this.rNgoaiTe.Size = new System.Drawing.Size(65, 17);
            this.rNgoaiTe.TabIndex = 1;
            this.rNgoaiTe.Text = "Ngoại tệ";
            this.rNgoaiTe.UseVisualStyleBackColor = true;
            // 
            // rTienViet
            // 
            this.rTienViet.AccessibleDescription = "REPORTR00001";
            this.rTienViet.AccessibleName = "English";
            this.rTienViet.AutoSize = true;
            this.rTienViet.Checked = true;
            this.rTienViet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rTienViet.Location = new System.Drawing.Point(6, 13);
            this.rTienViet.Name = "rTienViet";
            this.rTienViet.Size = new System.Drawing.Size(67, 17);
            this.rTienViet.TabIndex = 0;
            this.rTienViet.TabStop = true;
            this.rTienViet.Text = "Tiền Việt";
            this.rTienViet.UseVisualStyleBackColor = true;
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.AccessibleDescription = ".";
            this.btnCheckConnection.Image = global::V6ControlManager.Properties.Resources.Network24;
            this.btnCheckConnection.Location = new System.Drawing.Point(255, 164);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(30, 30);
            this.btnCheckConnection.TabIndex = 25;
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // AAPPR_AR12_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCheckConnection);
            this.Controls.Add(this.cboSendType);
            this.Controls.Add(this.btnSuaChiTieu);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.chkHoaDonDaIn);
            this.Controls.Add(this.ctDenSo);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.chkLike);
            this.Controls.Add(this.ctTuSo);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.TxtXtag);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtMa_ct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbTienTe);
            this.Name = "AAPPR_AR12_filter";
            this.Size = new System.Drawing.Size(299, 685);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbTienTe.ResumeLayout(false);
            this.grbTienTe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6ReportControls.FilterLineVvarTextBox lineMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox lineMaKho;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox8;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox4;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtMa_ct;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6VvarTextBox TxtXtag;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh6;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh3;
        private V6Controls.V6VvarTextBox ctDenSo;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6CheckBox chkLike;
        private V6Controls.V6VvarTextBox ctTuSo;
        private V6Controls.V6Label v6Label8;
        private V6Controls.V6CheckBox chkHoaDonDaIn;
        private V6Controls.V6Label v6Label5;
        private System.Windows.Forms.Button btnSuaChiTieu;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH9;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH8;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH7;
        private V6Controls.Controls.V6IndexComboBox cboSendType;
        private System.Windows.Forms.GroupBox grbTienTe;
        private System.Windows.Forms.RadioButton rNgoaiTe;
        private System.Windows.Forms.RadioButton rTienViet;
        private V6ReportControls.FilterLineVvarTextBox lineSO_SERI;
        private V6ReportControls.FilterLineVvarTextBox lineMaMauHD;
        private V6ReportControls.FilterLineVvarTextBox lineMA_SONB;
        private V6ReportControls.FilterLineVvarTextBox lineTrangThai;
        private V6ReportControls.FilterLineVvarTextBox lineMaXuly;
        private V6Controls.Controls.V6FormButton btnCheckConnection;
    }
}
