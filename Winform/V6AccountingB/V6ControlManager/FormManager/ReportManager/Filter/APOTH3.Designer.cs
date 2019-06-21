namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class APOTH3
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtLoaiBaoCao = new V6Controls.V6NumberTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TXTMA_BPHT = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh6 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox2 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.Txtnh_kh1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh2 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox8 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox4 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox7 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh3 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtma_ct = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.txtChiTietTheo = new V6Controls.V6NumberTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkGiamTru = new V6Controls.V6CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lineNH_KH9 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH8 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineNH_KH7 = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00122";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nhóm theo";
            // 
            // txtLoaiBaoCao
            // 
            this.txtLoaiBaoCao.AccessibleName = "LOAI_BC";
            this.txtLoaiBaoCao.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoaiBaoCao.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoaiBaoCao.DecimalPlaces = 0;
            this.txtLoaiBaoCao.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLoaiBaoCao.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoaiBaoCao.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoaiBaoCao.GrayText = null;
            this.txtLoaiBaoCao.HoverColor = System.Drawing.Color.Yellow;
            this.txtLoaiBaoCao.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtLoaiBaoCao.LeaveColor = System.Drawing.Color.White;
            this.txtLoaiBaoCao.LimitCharacters = "12345678";
            this.txtLoaiBaoCao.Location = new System.Drawing.Point(84, 61);
            this.txtLoaiBaoCao.MaxLength = 1;
            this.txtLoaiBaoCao.Name = "txtLoaiBaoCao";
            this.txtLoaiBaoCao.Size = new System.Drawing.Size(17, 20);
            this.txtLoaiBaoCao.TabIndex = 3;
            this.txtLoaiBaoCao.Text = "2";
            this.txtLoaiBaoCao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLoaiBaoCao.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtLoaiBaoCao.TextChanged += new System.EventHandler(this.txtLoaiBaoCao_ChiTietTheo_TextChanged);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(120, 29);
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(120, 3);
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
            this.groupBox1.Controls.Add(this.TXTMA_BPHT);
            this.groupBox1.Controls.Add(this.Txtnh_kh4);
            this.groupBox1.Controls.Add(this.Txtnh_kh5);
            this.groupBox1.Controls.Add(this.Txtnh_kh6);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox2);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.Txtnh_kh1);
            this.groupBox1.Controls.Add(this.Txtnh_kh2);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox8);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox4);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox7);
            this.groupBox1.Controls.Add(this.Txtnh_kh3);
            this.groupBox1.Controls.Add(this.Txtma_ct);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Location = new System.Drawing.Point(0, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 450);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // TXTMA_BPHT
            // 
            this.TXTMA_BPHT.AccessibleDescription = "FILTERL00008";
            this.TXTMA_BPHT.AccessibleName2 = "MA_BPHT";
            this.TXTMA_BPHT.Caption = "Mã BPHT";
            this.TXTMA_BPHT.FieldName = "MA_BPHT";
            this.TXTMA_BPHT.Location = new System.Drawing.Point(6, 420);
            this.TXTMA_BPHT.Name = "TXTMA_BPHT";
            this.TXTMA_BPHT.Size = new System.Drawing.Size(292, 22);
            this.TXTMA_BPHT.TabIndex = 17;
            this.TXTMA_BPHT.Vvar = "MA_BPHT";
            // 
            // Txtnh_kh4
            // 
            this.Txtnh_kh4.AccessibleDescription = "FILTERL00014";
            this.Txtnh_kh4.AccessibleName2 = "NH_KH4";
            this.Txtnh_kh4.Caption = "Nhóm khách hàng 4";
            this.Txtnh_kh4.FieldName = "NH_KH4";
            this.Txtnh_kh4.Location = new System.Drawing.Point(6, 266);
            this.Txtnh_kh4.Name = "Txtnh_kh4";
            this.Txtnh_kh4.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh4.TabIndex = 10;
            this.Txtnh_kh4.Vvar = "NH_KH";
            // 
            // Txtnh_kh5
            // 
            this.Txtnh_kh5.AccessibleDescription = "FILTERL00015";
            this.Txtnh_kh5.AccessibleName2 = "NH_KH5";
            this.Txtnh_kh5.Caption = "Nhóm khách hàng 5";
            this.Txtnh_kh5.FieldName = "NH_KH5";
            this.Txtnh_kh5.Location = new System.Drawing.Point(6, 288);
            this.Txtnh_kh5.Name = "Txtnh_kh5";
            this.Txtnh_kh5.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh5.TabIndex = 11;
            this.Txtnh_kh5.Vvar = "NH_KH";
            // 
            // Txtnh_kh6
            // 
            this.Txtnh_kh6.AccessibleDescription = "FILTERL00016";
            this.Txtnh_kh6.AccessibleName2 = "NH_KH6";
            this.Txtnh_kh6.Caption = "Nhóm khách hàng 6";
            this.Txtnh_kh6.FieldName = "NH_KH6";
            this.Txtnh_kh6.Location = new System.Drawing.Point(6, 310);
            this.Txtnh_kh6.Name = "Txtnh_kh6";
            this.Txtnh_kh6.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh6.TabIndex = 12;
            this.Txtnh_kh6.Vvar = "NH_KH";
            // 
            // filterLineVvarTextBox2
            // 
            this.filterLineVvarTextBox2.AccessibleDescription = "FILTERL00029";
            this.filterLineVvarTextBox2.AccessibleName2 = "MA_NVIEN";
            this.filterLineVvarTextBox2.Caption = "Mã nhân viên";
            this.filterLineVvarTextBox2.FieldName = "MA_NVIEN";
            this.filterLineVvarTextBox2.Location = new System.Drawing.Point(6, 398);
            this.filterLineVvarTextBox2.Name = "filterLineVvarTextBox2";
            this.filterLineVvarTextBox2.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox2.TabIndex = 16;
            this.filterLineVvarTextBox2.Vvar = "MA_NVIEN";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 17);
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
            this.radAnd.Location = new System.Drawing.Point(6, 17);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(130, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Tất cả điều kiện (and)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // Txtnh_kh1
            // 
            this.Txtnh_kh1.AccessibleDescription = "FILTERL00011";
            this.Txtnh_kh1.AccessibleName2 = "NH_KH1";
            this.Txtnh_kh1.Caption = "Nhóm khách hàng 1";
            this.Txtnh_kh1.FieldName = "NH_KH1";
            this.Txtnh_kh1.Location = new System.Drawing.Point(6, 200);
            this.Txtnh_kh1.Name = "Txtnh_kh1";
            this.Txtnh_kh1.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh1.TabIndex = 7;
            this.Txtnh_kh1.Vvar = "NH_KH";
            // 
            // Txtnh_kh2
            // 
            this.Txtnh_kh2.AccessibleDescription = "FILTERL00012";
            this.Txtnh_kh2.AccessibleName2 = "NH_KH2";
            this.Txtnh_kh2.Caption = "Nhóm khách hàng 2";
            this.Txtnh_kh2.FieldName = "NH_KH2";
            this.Txtnh_kh2.Location = new System.Drawing.Point(6, 222);
            this.Txtnh_kh2.Name = "Txtnh_kh2";
            this.Txtnh_kh2.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh2.TabIndex = 8;
            this.Txtnh_kh2.Vvar = "NH_KH";
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00006";
            this.filterLineVvarTextBox1.AccessibleName2 = "MA_KHO";
            this.filterLineVvarTextBox1.Caption = "Mã kho";
            this.filterLineVvarTextBox1.FieldName = "MA_KHO";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(6, 46);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox1.TabIndex = 0;
            this.filterLineVvarTextBox1.Vvar = "MA_KHO";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 68);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(292, 22);
            this.txtMaDvcs.TabIndex = 1;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // filterLineVvarTextBox8
            // 
            this.filterLineVvarTextBox8.AccessibleDescription = "FILTERL00009";
            this.filterLineVvarTextBox8.AccessibleName2 = "MA_NX";
            this.filterLineVvarTextBox8.Caption = "Mã dạng nx";
            this.filterLineVvarTextBox8.FieldName = "MA_NX";
            this.filterLineVvarTextBox8.Location = new System.Drawing.Point(6, 178);
            this.filterLineVvarTextBox8.Name = "filterLineVvarTextBox8";
            this.filterLineVvarTextBox8.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox8.TabIndex = 6;
            this.filterLineVvarTextBox8.Vvar = "MA_NX";
            // 
            // filterLineVvarTextBox4
            // 
            this.filterLineVvarTextBox4.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox4.AccessibleName2 = "MA_BP";
            this.filterLineVvarTextBox4.Caption = "Mã bộ phận";
            this.filterLineVvarTextBox4.FieldName = "MA_BP";
            this.filterLineVvarTextBox4.Location = new System.Drawing.Point(6, 112);
            this.filterLineVvarTextBox4.Name = "filterLineVvarTextBox4";
            this.filterLineVvarTextBox4.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox4.TabIndex = 3;
            this.filterLineVvarTextBox4.Vvar = "MA_BP";
            // 
            // filterLineVvarTextBox7
            // 
            this.filterLineVvarTextBox7.AccessibleDescription = "FILTERL00065";
            this.filterLineVvarTextBox7.AccessibleName2 = "MA_VV";
            this.filterLineVvarTextBox7.Caption = "Mã vụ việc";
            this.filterLineVvarTextBox7.FieldName = "MA_VV";
            this.filterLineVvarTextBox7.Location = new System.Drawing.Point(6, 156);
            this.filterLineVvarTextBox7.Name = "filterLineVvarTextBox7";
            this.filterLineVvarTextBox7.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox7.TabIndex = 5;
            this.filterLineVvarTextBox7.Vvar = "MA_VV";
            // 
            // Txtnh_kh3
            // 
            this.Txtnh_kh3.AccessibleDescription = "FILTERL00013";
            this.Txtnh_kh3.AccessibleName2 = "NH_KH3";
            this.Txtnh_kh3.Caption = "Nhóm khách hàng 3";
            this.Txtnh_kh3.FieldName = "NH_KH3";
            this.Txtnh_kh3.Location = new System.Drawing.Point(6, 244);
            this.Txtnh_kh3.Name = "Txtnh_kh3";
            this.Txtnh_kh3.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh3.TabIndex = 9;
            this.Txtnh_kh3.Vvar = "NH_KH";
            // 
            // Txtma_ct
            // 
            this.Txtma_ct.AccessibleDescription = "FILTERL00004";
            this.Txtma_ct.AccessibleName2 = "MA_CT";
            this.Txtma_ct.Caption = "Mã chứng từ";
            this.Txtma_ct.FieldName = "MA_CT";
            this.Txtma_ct.Location = new System.Drawing.Point(6, 134);
            this.Txtma_ct.Name = "Txtma_ct";
            this.Txtma_ct.Size = new System.Drawing.Size(292, 22);
            this.Txtma_ct.TabIndex = 4;
            this.Txtma_ct.Vvar = "MA_CT";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.AccessibleName2 = "MA_KH";
            this.filterLineVvarTextBox3.Caption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(6, 90);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox3.TabIndex = 2;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // txtChiTietTheo
            // 
            this.txtChiTietTheo.AccessibleName = "CHI_TIET_THEO";
            this.txtChiTietTheo.BackColor = System.Drawing.SystemColors.Window;
            this.txtChiTietTheo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtChiTietTheo.DecimalPlaces = 0;
            this.txtChiTietTheo.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtChiTietTheo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtChiTietTheo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtChiTietTheo.GrayText = null;
            this.txtChiTietTheo.HoverColor = System.Drawing.Color.Yellow;
            this.txtChiTietTheo.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtChiTietTheo.LeaveColor = System.Drawing.Color.White;
            this.txtChiTietTheo.LimitCharacters = "12345678";
            this.txtChiTietTheo.Location = new System.Drawing.Point(202, 61);
            this.txtChiTietTheo.MaxLength = 1;
            this.txtChiTietTheo.Name = "txtChiTietTheo";
            this.txtChiTietTheo.Size = new System.Drawing.Size(17, 20);
            this.txtChiTietTheo.TabIndex = 4;
            this.txtChiTietTheo.Text = "1";
            this.txtChiTietTheo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChiTietTheo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtChiTietTheo.TextChanged += new System.EventHandler(this.txtLoaiBaoCao_ChiTietTheo_TextChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00148";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Chi tiết theo";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "FILTERL00149";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "1-Theo mặt hàng, 2-Theo khách";
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "FILTERL00150";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "3-Theo vv, 4-Theo mã nx, 5-Theo bộ phận";
            // 
            // chkGiamTru
            // 
            this.chkGiamTru.AccessibleDescription = "FILTERC00013";
            this.chkGiamTru.AutoSize = true;
            this.chkGiamTru.Location = new System.Drawing.Point(236, 18);
            this.chkGiamTru.Name = "chkGiamTru";
            this.chkGiamTru.Size = new System.Drawing.Size(65, 17);
            this.chkGiamTru.TabIndex = 2;
            this.chkGiamTru.Text = "Giảm trừ";
            this.chkGiamTru.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "FILTERL00273";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "6-Mã N viên, 7-Mã BPHT, 8-Mã đơn vị";
            // 
            // lineNH_KH9
            // 
            this.lineNH_KH9.AccessibleDescription = "FILTERL00019";
            this.lineNH_KH9.AccessibleName2 = "NH_KH9";
            this.lineNH_KH9.Caption = "Nhóm khách hàng 9";
            this.lineNH_KH9.FieldName = "NH_KH9";
            this.lineNH_KH9.Location = new System.Drawing.Point(6, 376);
            this.lineNH_KH9.Name = "lineNH_KH9";
            this.lineNH_KH9.Size = new System.Drawing.Size(292, 22);
            this.lineNH_KH9.TabIndex = 15;
            this.lineNH_KH9.Vvar = "NH_KH";
            // 
            // lineNH_KH8
            // 
            this.lineNH_KH8.AccessibleDescription = "FILTERL00018";
            this.lineNH_KH8.AccessibleName2 = "NH_KH8";
            this.lineNH_KH8.Caption = "Nhóm khách hàng 8";
            this.lineNH_KH8.FieldName = "NH_KH8";
            this.lineNH_KH8.Location = new System.Drawing.Point(6, 354);
            this.lineNH_KH8.Name = "lineNH_KH8";
            this.lineNH_KH8.Size = new System.Drawing.Size(292, 22);
            this.lineNH_KH8.TabIndex = 14;
            this.lineNH_KH8.Vvar = "NH_KH";
            // 
            // lineNH_KH7
            // 
            this.lineNH_KH7.AccessibleDescription = "FILTERL00017";
            this.lineNH_KH7.AccessibleName2 = "NH_KH7";
            this.lineNH_KH7.Caption = "Nhóm khách hàng 7";
            this.lineNH_KH7.FieldName = "NH_KH7";
            this.lineNH_KH7.Location = new System.Drawing.Point(6, 332);
            this.lineNH_KH7.Name = "lineNH_KH7";
            this.lineNH_KH7.Size = new System.Drawing.Size(292, 22);
            this.lineNH_KH7.TabIndex = 13;
            this.lineNH_KH7.Vvar = "NH_KH";
            // 
            // APOTH3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkGiamTru);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtChiTietTheo);
            this.Controls.Add(this.txtLoaiBaoCao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "APOTH3";
            this.Size = new System.Drawing.Size(299, 593);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox8;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox4;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox7;
        private V6ReportControls.FilterLineVvarTextBox Txtma_ct;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh3;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6NumberTextBox txtLoaiBaoCao;
        private V6Controls.V6NumberTextBox txtChiTietTheo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private V6Controls.V6CheckBox chkGiamTru;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh6;
        private System.Windows.Forms.Label label7;
        private V6ReportControls.FilterLineVvarTextBox TXTMA_BPHT;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH9;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH8;
        private V6ReportControls.FilterLineVvarTextBox lineNH_KH7;
    }
}
