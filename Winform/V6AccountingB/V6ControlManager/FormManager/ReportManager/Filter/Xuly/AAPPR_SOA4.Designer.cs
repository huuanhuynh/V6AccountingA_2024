﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AAPPR_SOA4
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
            this.txtMa_ct = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineNH_KH9 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH8 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH7 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMa_xuly = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox5 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh6 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh3 = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox8 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox4 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.TxtXtag = new V6Controls.V6VvarTextBox();
            this.cboKieuPost = new V6Controls.V6ComboBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.chkHoaDonDaIn = new V6Controls.V6CheckBox();
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.chkLike = new V6Controls.V6CheckBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.chkMa_bp = new V6Controls.V6CheckBox();
            this.chkMa_nvien = new V6Controls.V6CheckBox();
            this.chkGc_ud1 = new V6Controls.V6CheckBox();
            this.groupBox1.SuspendLayout();
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
            // txtMa_ct
            // 
            this.txtMa_ct.AccessibleName = "MA_CT";
            this.txtMa_ct.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_ct.CheckNotEmpty = true;
            this.txtMa_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_ct.LeaveColor = System.Drawing.Color.White;
            this.txtMa_ct.Location = new System.Drawing.Point(112, 46);
            this.txtMa_ct.Name = "txtMa_ct";
            this.txtMa_ct.Size = new System.Drawing.Size(100, 20);
            this.txtMa_ct.TabIndex = 2;
            this.txtMa_ct.VVar = "MA_CT";
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
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
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
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
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
            this.groupBox1.Controls.Add(this.lineNH_KH9);
            this.groupBox1.Controls.Add(this.lineNH_KH8);
            this.groupBox1.Controls.Add(this.lineNH_KH7);
            this.groupBox1.Controls.Add(this.lineMa_xuly);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox5);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox2);
            this.groupBox1.Controls.Add(this.Txtnh_kh6);
            this.groupBox1.Controls.Add(this.Txtnh_kh5);
            this.groupBox1.Controls.Add(this.Txtnh_kh4);
            this.groupBox1.Controls.Add(this.Txtnh_kh1);
            this.groupBox1.Controls.Add(this.Txtnh_kh2);
            this.groupBox1.Controls.Add(this.Txtnh_kh3);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox8);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox4);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 411);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineNH_KH9
            // 
            this.lineNH_KH9.AccessibleDescription = "FILTERL00019";
            this.lineNH_KH9.AccessibleName2 = "NH_KH9";
            this.lineNH_KH9.Caption = "Nhóm khách hàng 9";
            this.lineNH_KH9.FieldName = "NH_KH9";
            this.lineNH_KH9.Location = new System.Drawing.Point(4, 383);
            this.lineNH_KH9.Name = "lineNH_KH9";
            this.lineNH_KH9.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH9.TabIndex = 21;
            this.lineNH_KH9.Vvar = "NH_KH";
            // 
            // lineNH_KH8
            // 
            this.lineNH_KH8.AccessibleDescription = "FILTERL00018";
            this.lineNH_KH8.AccessibleName2 = "NH_KH8";
            this.lineNH_KH8.Caption = "Nhóm khách hàng 8";
            this.lineNH_KH8.FieldName = "NH_KH8";
            this.lineNH_KH8.Location = new System.Drawing.Point(4, 361);
            this.lineNH_KH8.Name = "lineNH_KH8";
            this.lineNH_KH8.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH8.TabIndex = 20;
            this.lineNH_KH8.Vvar = "NH_KH";
            // 
            // lineNH_KH7
            // 
            this.lineNH_KH7.AccessibleDescription = "FILTERL00017";
            this.lineNH_KH7.AccessibleName2 = "NH_KH7";
            this.lineNH_KH7.Caption = "Nhóm khách hàng 7";
            this.lineNH_KH7.FieldName = "NH_KH7";
            this.lineNH_KH7.Location = new System.Drawing.Point(4, 339);
            this.lineNH_KH7.Name = "lineNH_KH7";
            this.lineNH_KH7.Size = new System.Drawing.Size(294, 22);
            this.lineNH_KH7.TabIndex = 19;
            this.lineNH_KH7.Vvar = "NH_KH";
            // 
            // lineMa_xuly
            // 
            this.lineMa_xuly.AccessibleDescription = "FILTERL00009";
            this.lineMa_xuly.AccessibleName2 = "MA_XULY";
            this.lineMa_xuly.Caption = "Mã xử lý";
            this.lineMa_xuly.FieldName = "MA_XULY";
            this.lineMa_xuly.Location = new System.Drawing.Point(4, 163);
            this.lineMa_xuly.Name = "lineMa_xuly";
            this.lineMa_xuly.Size = new System.Drawing.Size(294, 22);
            this.lineMa_xuly.TabIndex = 8;
            this.lineMa_xuly.Vvar = "MA_XULY";
            // 
            // filterLineVvarTextBox5
            // 
            this.filterLineVvarTextBox5.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox5.AccessibleName2 = "MA_NVIEN";
            this.filterLineVvarTextBox5.Caption = "Mã nhân viên";
            this.filterLineVvarTextBox5.FieldName = "MA_NVIEN";
            this.filterLineVvarTextBox5.Location = new System.Drawing.Point(4, 119);
            this.filterLineVvarTextBox5.Name = "filterLineVvarTextBox5";
            this.filterLineVvarTextBox5.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox5.TabIndex = 6;
            this.filterLineVvarTextBox5.Vvar = "MA_NVIEN";
            // 
            // filterLineVvarTextBox2
            // 
            this.filterLineVvarTextBox2.AccessibleDescription = "FILTERL00009";
            this.filterLineVvarTextBox2.AccessibleName2 = "MA_TD_PH";
            this.filterLineVvarTextBox2.Caption = "Mã TDN( ca)";
            this.filterLineVvarTextBox2.FieldName = "MA_TD_PH";
            this.filterLineVvarTextBox2.Location = new System.Drawing.Point(4, 141);
            this.filterLineVvarTextBox2.Name = "filterLineVvarTextBox2";
            this.filterLineVvarTextBox2.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox2.TabIndex = 7;
            this.filterLineVvarTextBox2.Vvar = "MA_TD";
            // 
            // Txtnh_kh6
            // 
            this.Txtnh_kh6.AccessibleDescription = "FILTERL00016";
            this.Txtnh_kh6.AccessibleName2 = "NH_KH6";
            this.Txtnh_kh6.Caption = "Nhóm khách hàng 6";
            this.Txtnh_kh6.FieldName = "NH_KH6";
            this.Txtnh_kh6.Location = new System.Drawing.Point(4, 317);
            this.Txtnh_kh6.Name = "Txtnh_kh6";
            this.Txtnh_kh6.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh6.TabIndex = 15;
            this.Txtnh_kh6.Vvar = "NH_KH";
            // 
            // Txtnh_kh5
            // 
            this.Txtnh_kh5.AccessibleDescription = "FILTERL00015";
            this.Txtnh_kh5.AccessibleName2 = "NH_KH5";
            this.Txtnh_kh5.Caption = "Nhóm khách hàng 5";
            this.Txtnh_kh5.FieldName = "NH_KH5";
            this.Txtnh_kh5.Location = new System.Drawing.Point(4, 295);
            this.Txtnh_kh5.Name = "Txtnh_kh5";
            this.Txtnh_kh5.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh5.TabIndex = 14;
            this.Txtnh_kh5.Vvar = "NH_KH";
            // 
            // Txtnh_kh4
            // 
            this.Txtnh_kh4.AccessibleDescription = "FILTERL00014";
            this.Txtnh_kh4.AccessibleName2 = "NH_KH4";
            this.Txtnh_kh4.Caption = "Nhóm khách hàng 4";
            this.Txtnh_kh4.FieldName = "NH_KH4";
            this.Txtnh_kh4.Location = new System.Drawing.Point(4, 273);
            this.Txtnh_kh4.Name = "Txtnh_kh4";
            this.Txtnh_kh4.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh4.TabIndex = 13;
            this.Txtnh_kh4.Vvar = "NH_KH";
            // 
            // Txtnh_kh1
            // 
            this.Txtnh_kh1.AccessibleDescription = "FILTERL00011";
            this.Txtnh_kh1.AccessibleName2 = "NH_KH1";
            this.Txtnh_kh1.Caption = "Nhóm khách hàng 1";
            this.Txtnh_kh1.FieldName = "NH_KH1";
            this.Txtnh_kh1.Location = new System.Drawing.Point(4, 207);
            this.Txtnh_kh1.Name = "Txtnh_kh1";
            this.Txtnh_kh1.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh1.TabIndex = 10;
            this.Txtnh_kh1.Vvar = "NH_KH";
            // 
            // Txtnh_kh2
            // 
            this.Txtnh_kh2.AccessibleDescription = "FILTERL00012";
            this.Txtnh_kh2.AccessibleName2 = "NH_KH2";
            this.Txtnh_kh2.Caption = "Nhóm khách hàng 2";
            this.Txtnh_kh2.FieldName = "NH_KH2";
            this.Txtnh_kh2.Location = new System.Drawing.Point(4, 229);
            this.Txtnh_kh2.Name = "Txtnh_kh2";
            this.Txtnh_kh2.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh2.TabIndex = 11;
            this.Txtnh_kh2.Vvar = "NH_KH";
            // 
            // Txtnh_kh3
            // 
            this.Txtnh_kh3.AccessibleDescription = "FILTERL00013";
            this.Txtnh_kh3.AccessibleName2 = "NH_KH3";
            this.Txtnh_kh3.Caption = "Nhóm khách hàng 3";
            this.Txtnh_kh3.FieldName = "NH_KH3";
            this.Txtnh_kh3.Location = new System.Drawing.Point(4, 251);
            this.Txtnh_kh3.Name = "Txtnh_kh3";
            this.Txtnh_kh3.Size = new System.Drawing.Size(294, 22);
            this.Txtnh_kh3.TabIndex = 12;
            this.Txtnh_kh3.Vvar = "NH_KH";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(4, 31);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(294, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00006";
            this.filterLineVvarTextBox1.AccessibleName2 = "MA_KHO";
            this.filterLineVvarTextBox1.Caption = "Mã kho";
            this.filterLineVvarTextBox1.FieldName = "MA_KHO";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(4, 53);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox1.TabIndex = 3;
            this.filterLineVvarTextBox1.Vvar = "MA_KHO";
            // 
            // filterLineVvarTextBox8
            // 
            this.filterLineVvarTextBox8.AccessibleDescription = "FILTERL00009";
            this.filterLineVvarTextBox8.AccessibleName2 = "MA_NX";
            this.filterLineVvarTextBox8.Caption = "Mã dạng nx";
            this.filterLineVvarTextBox8.FieldName = "MA_NX";
            this.filterLineVvarTextBox8.Location = new System.Drawing.Point(4, 185);
            this.filterLineVvarTextBox8.Name = "filterLineVvarTextBox8";
            this.filterLineVvarTextBox8.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox8.TabIndex = 9;
            this.filterLineVvarTextBox8.Vvar = "MA_NX";
            // 
            // filterLineVvarTextBox4
            // 
            this.filterLineVvarTextBox4.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox4.AccessibleName2 = "MA_BP";
            this.filterLineVvarTextBox4.Caption = "Mã bộ phận";
            this.filterLineVvarTextBox4.FieldName = "MA_BP";
            this.filterLineVvarTextBox4.Location = new System.Drawing.Point(4, 97);
            this.filterLineVvarTextBox4.Name = "filterLineVvarTextBox4";
            this.filterLineVvarTextBox4.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox4.TabIndex = 5;
            this.filterLineVvarTextBox4.Vvar = "MA_BP";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.AccessibleName2 = "MA_KH";
            this.filterLineVvarTextBox3.Caption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(4, 75);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(294, 22);
            this.filterLineVvarTextBox3.TabIndex = 4;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 12);
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
            this.radAnd.Location = new System.Drawing.Point(6, 12);
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
            this.v6Label2.Location = new System.Drawing.Point(8, 72);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(70, 13);
            this.v6Label2.TabIndex = 6;
            this.v6Label2.Text = "Lọc chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00024";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(132, 83);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(75, 13);
            this.v6Label3.TabIndex = 9;
            this.v6Label3.Text = "2-Duyệt sổ cái";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00023";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(132, 68);
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
            this.TxtXtag.LimitCharacters = "012";
            this.TxtXtag.Location = new System.Drawing.Point(114, 67);
            this.TxtXtag.MaxLength = 1;
            this.TxtXtag.Name = "TxtXtag";
            this.TxtXtag.Size = new System.Drawing.Size(18, 20);
            this.TxtXtag.TabIndex = 3;
            // 
            // cboKieuPost
            // 
            this.cboKieuPost.AccessibleName = "KIEU_POST";
            this.cboKieuPost.BackColor = System.Drawing.SystemColors.Window;
            this.cboKieuPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieuPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKieuPost.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboKieuPost.FormattingEnabled = true;
            this.cboKieuPost.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboKieuPost.Location = new System.Drawing.Point(114, 96);
            this.cboKieuPost.Name = "cboKieuPost";
            this.cboKieuPost.Size = new System.Drawing.Size(170, 21);
            this.cboKieuPost.TabIndex = 4;
            this.cboKieuPost.Visible = false;
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00010";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6Label1.Location = new System.Drawing.Point(8, 99);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(89, 13);
            this.v6Label1.TabIndex = 10;
            this.v6Label1.Text = "Xử lý chứng từ";
            this.v6Label1.Visible = false;
            // 
            // chkHoaDonDaIn
            // 
            this.chkHoaDonDaIn.AccessibleDescription = "FILTERC00001";
            this.chkHoaDonDaIn.AccessibleName = "CHK_DA_IN";
            this.chkHoaDonDaIn.AutoSize = true;
            this.chkHoaDonDaIn.Location = new System.Drawing.Point(118, 118);
            this.chkHoaDonDaIn.Name = "chkHoaDonDaIn";
            this.chkHoaDonDaIn.Size = new System.Drawing.Size(95, 17);
            this.chkHoaDonDaIn.TabIndex = 5;
            this.chkHoaDonDaIn.Text = "Hóa đơn đã in";
            this.chkHoaDonDaIn.UseVisualStyleBackColor = true;
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
            this.ctDenSo.Location = new System.Drawing.Point(117, 159);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(100, 20);
            this.ctDenSo.TabIndex = 8;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00022";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(5, 157);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 25;
            this.v6Label7.Text = "Đến số";
            // 
            // chkLike
            // 
            this.chkLike.AccessibleDescription = "FILTERC00002";
            this.chkLike.AccessibleName = "";
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(71, 137);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 6;
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
            this.ctTuSo.Location = new System.Drawing.Point(117, 136);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(100, 20);
            this.ctTuSo.TabIndex = 7;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00021";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(5, 136);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 24;
            this.v6Label8.Text = "CT từ số";
            // 
            // chkMa_bp
            // 
            this.chkMa_bp.AccessibleDescription = "FILTERC00001";
            this.chkMa_bp.AccessibleName = "CHK_MA_BP";
            this.chkMa_bp.AutoSize = true;
            this.chkMa_bp.Location = new System.Drawing.Point(219, 121);
            this.chkMa_bp.Name = "chkMa_bp";
            this.chkMa_bp.Size = new System.Drawing.Size(70, 17);
            this.chkMa_bp.TabIndex = 26;
            this.chkMa_bp.Text = "Thiếu BP";
            this.chkMa_bp.UseVisualStyleBackColor = true;
            // 
            // chkMa_nvien
            // 
            this.chkMa_nvien.AccessibleDescription = "FILTERC00001";
            this.chkMa_nvien.AccessibleName = "CHK_MA_NVIEN";
            this.chkMa_nvien.AutoSize = true;
            this.chkMa_nvien.Location = new System.Drawing.Point(219, 140);
            this.chkMa_nvien.Name = "chkMa_nvien";
            this.chkMa_nvien.Size = new System.Drawing.Size(71, 17);
            this.chkMa_nvien.TabIndex = 27;
            this.chkMa_nvien.Text = "Thiếu NV";
            this.chkMa_nvien.UseVisualStyleBackColor = true;
            // 
            // chkGc_ud1
            // 
            this.chkGc_ud1.AccessibleDescription = "FILTERC00001";
            this.chkGc_ud1.AccessibleName = "CHK_GC_UD1";
            this.chkGc_ud1.AutoSize = true;
            this.chkGc_ud1.Location = new System.Drawing.Point(219, 159);
            this.chkGc_ud1.Name = "chkGc_ud1";
            this.chkGc_ud1.Size = new System.Drawing.Size(71, 17);
            this.chkGc_ud1.TabIndex = 28;
            this.chkGc_ud1.Text = "Thiếu GC";
            this.chkGc_ud1.UseVisualStyleBackColor = true;
            // 
            // AAPPR_SOA4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkGc_ud1);
            this.Controls.Add(this.chkMa_nvien);
            this.Controls.Add(this.chkMa_bp);
            this.Controls.Add(this.chkHoaDonDaIn);
            this.Controls.Add(this.ctDenSo);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.chkLike);
            this.Controls.Add(this.ctTuSo);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.cboKieuPost);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.TxtXtag);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.txtMa_ct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AAPPR_SOA4";
            this.Size = new System.Drawing.Size(299, 590);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox8;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox4;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox txtMa_ct;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6VvarTextBox TxtXtag;
        private V6Controls.V6ComboBox cboKieuPost;
        private V6Controls.V6Label v6Label1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh6;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh3;
        private V6Controls.V6CheckBox chkHoaDonDaIn;
        private V6Controls.V6VvarTextBox ctDenSo;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6CheckBox chkLike;
        private V6Controls.V6VvarTextBox ctTuSo;
        private V6Controls.V6Label v6Label8;
        private V6ReportControls.FilterLineVvarTextBox lineMa_xuly;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox5;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox2;
        private V6Controls.V6CheckBox chkMa_bp;
        private V6Controls.V6CheckBox chkMa_nvien;
        private V6Controls.V6CheckBox chkGc_ud1;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH9;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH8;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH7;
    }
}
