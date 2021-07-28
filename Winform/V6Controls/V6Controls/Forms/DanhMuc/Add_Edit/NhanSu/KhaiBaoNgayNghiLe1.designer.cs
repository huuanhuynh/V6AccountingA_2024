namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class KhaiBaoNgayNghiLe
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
            this.txtNgay = new V6Controls.V6DateTimePicker();
            this.txtten_kh = new V6Controls.V6LabelTextBox();
            this.txtMaCong = new V6Controls.V6VvarTextBox();
            this.TXTHE_SO = new V6Controls.V6NumberTextBox();
            this.TXTSO_GIO = new V6Controls.V6NumberTextBox();
            this.txtLoaiNgay = new V6Controls.V6ComboBox();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblMaCong = new System.Windows.Forms.Label();
            this.v6TabControl1 = new V6Controls.V6TabControl();
            this.tabThongTinChinh = new System.Windows.Forms.TabPage();
            this.tabTuDinhNghia = new System.Windows.Forms.TabPage();
            this.txtGC_TD3 = new V6Controls.V6VvarTextBox();
            this.txtGC_TD2 = new V6Controls.V6VvarTextBox();
            this.txtGC_TD1 = new V6Controls.V6VvarTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtMA_TD3 = new V6Controls.V6VvarTextBox();
            this.txtMA_TD2 = new V6Controls.V6VvarTextBox();
            this.txtMA_TD1 = new V6Controls.V6VvarTextBox();
            this.v6NumberTextBox3 = new V6Controls.V6NumberTextBox();
            this.v6NumberTextBox2 = new V6Controls.V6NumberTextBox();
            this.v6NumberTextBox1 = new V6Controls.V6NumberTextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.v6ColorDateTimePick3 = new V6Controls.V6DateTimeColor();
            this.v6ColorDateTimePick2 = new V6Controls.V6DateTimeColor();
            this.v6ColorDateTimePick1 = new V6Controls.V6DateTimeColor();
            this.tabThongTinKhac = new System.Windows.Forms.TabPage();
            this.v6TabControl1.SuspendLayout();
            this.tabThongTinChinh.SuspendLayout();
            this.tabTuDinhNghia.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNgay
            // 
            this.txtNgay.AccessibleName = "ngay";
            this.txtNgay.CustomFormat = "dd/MM/yyyy";
            this.txtNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay.LeaveColor = System.Drawing.Color.White;
            this.txtNgay.Location = new System.Drawing.Point(179, 9);
            this.txtNgay.Name = "txtNgay";
            this.txtNgay.Size = new System.Drawing.Size(134, 23);
            this.txtNgay.TabIndex = 0;
            // 
            // txtten_kh
            // 
            this.txtten_kh.AccessibleName = "ten_cong";
            this.txtten_kh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_kh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_kh.Location = new System.Drawing.Point(237, 41);
            this.txtten_kh.Name = "txtten_kh";
            this.txtten_kh.ReadOnly = true;
            this.txtten_kh.Size = new System.Drawing.Size(354, 16);
            this.txtten_kh.TabIndex = 2;
            this.txtten_kh.TabStop = false;
            this.txtten_kh.Tag = "readonly";
            // 
            // txtMaCong
            // 
            this.txtMaCong.AccessibleName = "ma_cong";
            this.txtMaCong.BackColor = System.Drawing.Color.White;
            this.txtMaCong.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCong.BrotherFields = "ten_cong,he_so";
            this.txtMaCong.CheckNotEmpty = true;
            this.txtMaCong.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCong.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCong.LeaveColor = System.Drawing.Color.White;
            this.txtMaCong.Location = new System.Drawing.Point(179, 38);
            this.txtMaCong.Name = "txtMaCong";
            this.txtMaCong.Size = new System.Drawing.Size(52, 23);
            this.txtMaCong.TabIndex = 1;
            this.txtMaCong.VVar = "MA_cong";
            this.txtMaCong.MouseLeave += new System.EventHandler(this.txtMaCong_MouseLeave);
            // 
            // TXTHE_SO
            // 
            this.TXTHE_SO.AccessibleName = "HE_SO";
            this.TXTHE_SO.BackColor = System.Drawing.Color.White;
            this.TXTHE_SO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TXTHE_SO.DecimalPlaces = 2;
            this.TXTHE_SO.EnterColor = System.Drawing.Color.PaleGreen;
            this.TXTHE_SO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TXTHE_SO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TXTHE_SO.HoverColor = System.Drawing.Color.Yellow;
            this.TXTHE_SO.LeaveColor = System.Drawing.Color.White;
            this.TXTHE_SO.Location = new System.Drawing.Point(179, 122);
            this.TXTHE_SO.Margin = new System.Windows.Forms.Padding(4);
            this.TXTHE_SO.Name = "TXTHE_SO";
            this.TXTHE_SO.Size = new System.Drawing.Size(134, 23);
            this.TXTHE_SO.TabIndex = 5;
            this.TXTHE_SO.Text = "0,00";
            this.TXTHE_SO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TXTHE_SO.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // TXTSO_GIO
            // 
            this.TXTSO_GIO.AccessibleName = "SO_GIO";
            this.TXTSO_GIO.BackColor = System.Drawing.Color.White;
            this.TXTSO_GIO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TXTSO_GIO.DecimalPlaces = 2;
            this.TXTSO_GIO.EnterColor = System.Drawing.Color.PaleGreen;
            this.TXTSO_GIO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TXTSO_GIO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TXTSO_GIO.HoverColor = System.Drawing.Color.Yellow;
            this.TXTSO_GIO.LeaveColor = System.Drawing.Color.White;
            this.TXTSO_GIO.Location = new System.Drawing.Point(179, 66);
            this.TXTSO_GIO.Margin = new System.Windows.Forms.Padding(4);
            this.TXTSO_GIO.Name = "TXTSO_GIO";
            this.TXTSO_GIO.Size = new System.Drawing.Size(134, 23);
            this.TXTSO_GIO.TabIndex = 3;
            this.TXTSO_GIO.Text = "0,00";
            this.TXTSO_GIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TXTSO_GIO.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtLoaiNgay
            // 
            this.txtLoaiNgay.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoaiNgay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoaiNgay.FormattingEnabled = true;
            this.txtLoaiNgay.Location = new System.Drawing.Point(179, 93);
            this.txtLoaiNgay.Name = "txtLoaiNgay";
            this.txtLoaiNgay.Size = new System.Drawing.Size(276, 24);
            this.txtLoaiNgay.TabIndex = 4;
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "ADDEDITL00549";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(8, 69);
            this.v6Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(102, 17);
            this.v6Label6.TabIndex = 90;
            this.v6Label6.Text = "Số giờ qui định";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "ADDEDITL00550";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(8, 96);
            this.v6Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(70, 17);
            this.v6Label7.TabIndex = 91;
            this.v6Label7.Text = "Loại ngày";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "ADDEDITL00551";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(8, 125);
            this.v6Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(86, 17);
            this.v6Label8.TabIndex = 92;
            this.v6Label8.Text = "Hệ số đi làm";
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "ADDEDITC00001";
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(179, 153);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Có sử dụng?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "ADDEDITL00022";
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 154);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 17);
            this.label15.TabIndex = 89;
            this.label15.Text = "Trạng thái";
            // 
            // lblNgay
            // 
            this.lblNgay.AccessibleDescription = "ADDEDITL00217";
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(8, 14);
            this.lblNgay.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(41, 17);
            this.lblNgay.TabIndex = 88;
            this.lblNgay.Text = "Ngày";
            // 
            // lblMaCong
            // 
            this.lblMaCong.AccessibleDescription = "ADDEDITL00548";
            this.lblMaCong.AutoSize = true;
            this.lblMaCong.Location = new System.Drawing.Point(8, 41);
            this.lblMaCong.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaCong.Name = "lblMaCong";
            this.lblMaCong.Size = new System.Drawing.Size(103, 17);
            this.lblMaCong.TabIndex = 87;
            this.lblMaCong.Text = "Mã công đi làm";
            // 
            // v6TabControl1
            // 
            this.v6TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6TabControl1.Controls.Add(this.tabThongTinChinh);
            this.v6TabControl1.Controls.Add(this.tabTuDinhNghia);
            this.v6TabControl1.Controls.Add(this.tabThongTinKhac);
            this.v6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl1.Location = new System.Drawing.Point(0, 0);
            this.v6TabControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6TabControl1.Name = "v6TabControl1";
            this.v6TabControl1.SelectedIndex = 0;
            this.v6TabControl1.Size = new System.Drawing.Size(600, 350);
            this.v6TabControl1.TabIndex = 84;
            // 
            // tabThongTinChinh
            // 
            this.tabThongTinChinh.AccessibleDescription = "ADDEDITT00001";
            this.tabThongTinChinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabThongTinChinh.Controls.Add(this.txtNgay);
            this.tabThongTinChinh.Controls.Add(this.txtten_kh);
            this.tabThongTinChinh.Controls.Add(this.lblMaCong);
            this.tabThongTinChinh.Controls.Add(this.txtMaCong);
            this.tabThongTinChinh.Controls.Add(this.lblNgay);
            this.tabThongTinChinh.Controls.Add(this.TXTHE_SO);
            this.tabThongTinChinh.Controls.Add(this.label15);
            this.tabThongTinChinh.Controls.Add(this.TXTSO_GIO);
            this.tabThongTinChinh.Controls.Add(this.checkBox1);
            this.tabThongTinChinh.Controls.Add(this.txtLoaiNgay);
            this.tabThongTinChinh.Controls.Add(this.v6Label8);
            this.tabThongTinChinh.Controls.Add(this.v6Label6);
            this.tabThongTinChinh.Controls.Add(this.v6Label7);
            this.tabThongTinChinh.Location = new System.Drawing.Point(4, 28);
            this.tabThongTinChinh.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabThongTinChinh.Name = "tabThongTinChinh";
            this.tabThongTinChinh.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabThongTinChinh.Size = new System.Drawing.Size(592, 318);
            this.tabThongTinChinh.TabIndex = 0;
            this.tabThongTinChinh.Text = "Thông tin chính";
            // 
            // tabTuDinhNghia
            // 
            this.tabTuDinhNghia.AccessibleDescription = "ADDEDITT00003";
            this.tabTuDinhNghia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD3);
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD2);
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD1);
            this.tabTuDinhNghia.Controls.Add(this.label29);
            this.tabTuDinhNghia.Controls.Add(this.label28);
            this.tabTuDinhNghia.Controls.Add(this.label27);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD3);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD2);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD1);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox3);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox2);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox1);
            this.tabTuDinhNghia.Controls.Add(this.label26);
            this.tabTuDinhNghia.Controls.Add(this.label30);
            this.tabTuDinhNghia.Controls.Add(this.label31);
            this.tabTuDinhNghia.Controls.Add(this.label32);
            this.tabTuDinhNghia.Controls.Add(this.label33);
            this.tabTuDinhNghia.Controls.Add(this.label34);
            this.tabTuDinhNghia.Controls.Add(this.label35);
            this.tabTuDinhNghia.Controls.Add(this.label36);
            this.tabTuDinhNghia.Controls.Add(this.label37);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick3);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick2);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick1);
            this.tabTuDinhNghia.Location = new System.Drawing.Point(4, 28);
            this.tabTuDinhNghia.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabTuDinhNghia.Name = "tabTuDinhNghia";
            this.tabTuDinhNghia.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabTuDinhNghia.Size = new System.Drawing.Size(592, 356);
            this.tabTuDinhNghia.TabIndex = 1;
            this.tabTuDinhNghia.Text = "Tự định nghĩa";
            // 
            // txtGC_TD3
            // 
            this.txtGC_TD3.AccessibleName = "GC_TD3";
            this.txtGC_TD3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_TD3.BackColor = System.Drawing.Color.White;
            this.txtGC_TD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD3.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD3.Location = new System.Drawing.Point(148, 286);
            this.txtGC_TD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD3.Name = "txtGC_TD3";
            this.txtGC_TD3.Size = new System.Drawing.Size(440, 23);
            this.txtGC_TD3.TabIndex = 161;
            // 
            // txtGC_TD2
            // 
            this.txtGC_TD2.AccessibleName = "GC_TD2";
            this.txtGC_TD2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_TD2.BackColor = System.Drawing.Color.White;
            this.txtGC_TD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD2.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD2.Location = new System.Drawing.Point(148, 261);
            this.txtGC_TD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD2.Name = "txtGC_TD2";
            this.txtGC_TD2.Size = new System.Drawing.Size(440, 23);
            this.txtGC_TD2.TabIndex = 160;
            // 
            // txtGC_TD1
            // 
            this.txtGC_TD1.AccessibleName = "GC_TD1";
            this.txtGC_TD1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_TD1.BackColor = System.Drawing.Color.White;
            this.txtGC_TD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD1.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD1.Location = new System.Drawing.Point(148, 236);
            this.txtGC_TD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD1.Name = "txtGC_TD1";
            this.txtGC_TD1.Size = new System.Drawing.Size(440, 23);
            this.txtGC_TD1.TabIndex = 159;
            // 
            // label29
            // 
            this.label29.AccessibleDescription = "M_GC_TD3,GC_TD3";
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 289);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 17);
            this.label29.TabIndex = 158;
            this.label29.Text = "Ghi chú ĐN 3";
            // 
            // label28
            // 
            this.label28.AccessibleDescription = "M_GC_TD2,GC_TD2";
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(13, 264);
            this.label28.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(93, 17);
            this.label28.TabIndex = 157;
            this.label28.Text = "Ghi chú ĐN 2";
            // 
            // label27
            // 
            this.label27.AccessibleDescription = "M_GC_TD1,GC_TD1";
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(13, 239);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 17);
            this.label27.TabIndex = 156;
            this.label27.Text = "Ghi chú ĐN 1";
            // 
            // txtMA_TD3
            // 
            this.txtMA_TD3.AccessibleName = "MA_TD3";
            this.txtMA_TD3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_TD3.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_TD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD3.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD3.Location = new System.Drawing.Point(148, 61);
            this.txtMA_TD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD3.Name = "txtMA_TD3";
            this.txtMA_TD3.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD3.TabIndex = 143;
            // 
            // txtMA_TD2
            // 
            this.txtMA_TD2.AccessibleName = "MA_TD2";
            this.txtMA_TD2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_TD2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_TD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD2.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD2.Location = new System.Drawing.Point(148, 36);
            this.txtMA_TD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD2.Name = "txtMA_TD2";
            this.txtMA_TD2.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD2.TabIndex = 141;
            // 
            // txtMA_TD1
            // 
            this.txtMA_TD1.AccessibleName = "MA_TD1";
            this.txtMA_TD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_TD1.BackColor = System.Drawing.Color.White;
            this.txtMA_TD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD1.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD1.Location = new System.Drawing.Point(148, 11);
            this.txtMA_TD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD1.Name = "txtMA_TD1";
            this.txtMA_TD1.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD1.TabIndex = 139;
            // 
            // v6NumberTextBox3
            // 
            this.v6NumberTextBox3.AccessibleName = "sl_td3";
            this.v6NumberTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6NumberTextBox3.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox3.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox3.Location = new System.Drawing.Point(148, 211);
            this.v6NumberTextBox3.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox3.Name = "v6NumberTextBox3";
            this.v6NumberTextBox3.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox3.TabIndex = 155;
            this.v6NumberTextBox3.Text = "0,000";
            this.v6NumberTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6NumberTextBox2
            // 
            this.v6NumberTextBox2.AccessibleName = "sl_td2";
            this.v6NumberTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6NumberTextBox2.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox2.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox2.Location = new System.Drawing.Point(148, 186);
            this.v6NumberTextBox2.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox2.Name = "v6NumberTextBox2";
            this.v6NumberTextBox2.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox2.TabIndex = 153;
            this.v6NumberTextBox2.Text = "0,000";
            this.v6NumberTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6NumberTextBox1
            // 
            this.v6NumberTextBox1.AccessibleName = "sl_td1";
            this.v6NumberTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6NumberTextBox1.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox1.Location = new System.Drawing.Point(148, 161);
            this.v6NumberTextBox1.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox1.Name = "v6NumberTextBox1";
            this.v6NumberTextBox1.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox1.TabIndex = 151;
            this.v6NumberTextBox1.Text = "0,000";
            this.v6NumberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AccessibleDescription = "M_SL_TD3,SL_TD3";
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(13, 214);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(61, 17);
            this.label26.TabIndex = 154;
            this.label26.Text = "SL ĐN 3";
            // 
            // label30
            // 
            this.label30.AccessibleDescription = "M_SL_TD2,SL_TD2";
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(13, 189);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(61, 17);
            this.label30.TabIndex = 152;
            this.label30.Text = "SL ĐN 2";
            // 
            // label31
            // 
            this.label31.AccessibleDescription = "M_SL_TD1,SL_TD1";
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(13, 164);
            this.label31.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(61, 17);
            this.label31.TabIndex = 150;
            this.label31.Text = "SL ĐN 1";
            // 
            // label32
            // 
            this.label32.AccessibleDescription = "M_NGAY_TD3,NGAY_TD3";
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(13, 139);
            this.label32.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 17);
            this.label32.TabIndex = 148;
            this.label32.Text = "Ngày ĐN 3";
            // 
            // label33
            // 
            this.label33.AccessibleDescription = "M_NGAY_TD2,NGAY_TD2";
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(13, 114);
            this.label33.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(77, 17);
            this.label33.TabIndex = 146;
            this.label33.Text = "Ngày ĐN 2";
            // 
            // label34
            // 
            this.label34.AccessibleDescription = "M_NGAY_TD1,NGAY_TD1";
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(13, 89);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(77, 17);
            this.label34.TabIndex = 144;
            this.label34.Text = "Ngày ĐN 1";
            // 
            // label35
            // 
            this.label35.AccessibleDescription = "M_MA_TD3,MA_TD3";
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(13, 64);
            this.label35.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(63, 17);
            this.label35.TabIndex = 142;
            this.label35.Text = "Mã ĐN 3";
            // 
            // label36
            // 
            this.label36.AccessibleDescription = "M_MA_TD2,MA_TD2";
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(13, 39);
            this.label36.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(63, 17);
            this.label36.TabIndex = 140;
            this.label36.Text = "Mã ĐN 2";
            // 
            // label37
            // 
            this.label37.AccessibleDescription = "M_MA_TD1,MA_TD1";
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(13, 14);
            this.label37.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(63, 17);
            this.label37.TabIndex = 138;
            this.label37.Text = "Mã ĐN 1";
            // 
            // v6ColorDateTimePick3
            // 
            this.v6ColorDateTimePick3.AccessibleName = "ngay_td3";
            this.v6ColorDateTimePick3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6ColorDateTimePick3.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorDateTimePick3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick3.GrayText = null;
            this.v6ColorDateTimePick3.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick3.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick3.Location = new System.Drawing.Point(148, 136);
            this.v6ColorDateTimePick3.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick3.Name = "v6ColorDateTimePick3";
            this.v6ColorDateTimePick3.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick3.StringValue = "__/__/____";
            this.v6ColorDateTimePick3.TabIndex = 149;
            this.v6ColorDateTimePick3.Text = "__/__/____";
            // 
            // v6ColorDateTimePick2
            // 
            this.v6ColorDateTimePick2.AccessibleName = "ngay_td2";
            this.v6ColorDateTimePick2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6ColorDateTimePick2.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorDateTimePick2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick2.GrayText = null;
            this.v6ColorDateTimePick2.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick2.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick2.Location = new System.Drawing.Point(148, 111);
            this.v6ColorDateTimePick2.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick2.Name = "v6ColorDateTimePick2";
            this.v6ColorDateTimePick2.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick2.StringValue = "__/__/____";
            this.v6ColorDateTimePick2.TabIndex = 147;
            this.v6ColorDateTimePick2.Text = "__/__/____";
            // 
            // v6ColorDateTimePick1
            // 
            this.v6ColorDateTimePick1.AccessibleName = "ngay_td1";
            this.v6ColorDateTimePick1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6ColorDateTimePick1.BackColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick1.GrayText = null;
            this.v6ColorDateTimePick1.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick1.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.Location = new System.Drawing.Point(148, 86);
            this.v6ColorDateTimePick1.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick1.Name = "v6ColorDateTimePick1";
            this.v6ColorDateTimePick1.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick1.StringValue = "__/__/____";
            this.v6ColorDateTimePick1.TabIndex = 145;
            this.v6ColorDateTimePick1.Text = "__/__/____";
            // 
            // tabThongTinKhac
            // 
            this.tabThongTinKhac.AccessibleDescription = "ADDEDITT00004";
            this.tabThongTinKhac.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabThongTinKhac.Location = new System.Drawing.Point(4, 28);
            this.tabThongTinKhac.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabThongTinKhac.Name = "tabThongTinKhac";
            this.tabThongTinKhac.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabThongTinKhac.Size = new System.Drawing.Size(592, 356);
            this.tabThongTinKhac.TabIndex = 2;
            this.tabThongTinKhac.Text = "Thông tin khác";
            // 
            // KhaiBaoNgayNghiLe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6TabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KhaiBaoNgayNghiLe";
            this.Size = new System.Drawing.Size(600, 350);
            this.v6TabControl1.ResumeLayout(false);
            this.tabThongTinChinh.ResumeLayout(false);
            this.tabThongTinChinh.PerformLayout();
            this.tabTuDinhNghia.ResumeLayout(false);
            this.tabTuDinhNghia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private V6VvarTextBox txtMaCong;
        private V6NumberTextBox TXTHE_SO;
        private V6NumberTextBox TXTSO_GIO;
        private V6ComboBox txtLoaiNgay;
        private V6Label v6Label6;
        private V6Label v6Label7;
        private V6Label v6Label8;
        private V6CheckBox checkBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblMaCong;
        private V6LabelTextBox txtten_kh;
        private V6DateTimePicker txtNgay;
        private V6TabControl v6TabControl1;
        private System.Windows.Forms.TabPage tabThongTinChinh;
        private System.Windows.Forms.TabPage tabTuDinhNghia;
        private V6VvarTextBox txtGC_TD3;
        private V6VvarTextBox txtGC_TD2;
        private V6VvarTextBox txtGC_TD1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private V6VvarTextBox txtMA_TD3;
        private V6VvarTextBox txtMA_TD2;
        private V6VvarTextBox txtMA_TD1;
        private V6NumberTextBox v6NumberTextBox3;
        private V6NumberTextBox v6NumberTextBox2;
        private V6NumberTextBox v6NumberTextBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private V6DateTimeColor v6ColorDateTimePick3;
        private V6DateTimeColor v6ColorDateTimePick2;
        private V6DateTimeColor v6ColorDateTimePick1;
        private System.Windows.Forms.TabPage tabThongTinKhac;
    }
}
