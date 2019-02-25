namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AFATANGNG_F4
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
            this.txtSo_the_ts = new V6Controls.V6VvarTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label12 = new V6Controls.V6Label();
            this.txtdien_giai = new V6Controls.V6ColorTextBox();
            this.txtSoCt = new V6Controls.V6ColorTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.dateNgayCT = new V6Controls.V6DateTimePicker();
            this.v6Label3 = new V6Controls.V6Label();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.TxtMa_NV = new V6Controls.V6VvarTextBox();
            this.txtLyDoTang = new V6Controls.V6VvarTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.v6Label5 = new V6Controls.V6Label();
            this.txtnguyen_gia = new V6Controls.NumberTien();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.Txtgt_da_kh = new V6Controls.NumberTien();
            this.v6Label10 = new V6Controls.V6Label();
            this.TxtSo_ky = new V6Controls.NumberTien();
            this.v6Label11 = new V6Controls.V6Label();
            this.txtgt_cl = new V6Controls.NumberTien();
            this.DateNgay_tg = new V6Controls.V6DateTimePicker();
            this.v6Label15 = new V6Controls.V6Label();
            this.Txtgt_kh_ky = new V6Controls.NumberTien();
            this.txtTs0 = new V6Controls.V6NumberTextBox();
            this.txtTang_giam = new V6Controls.V6NumberTextBox();
            this.txtMaCt = new V6Controls.V6ColorTextBox();
            this.v6Label14 = new V6Controls.V6Label();
            this.v6Label13 = new V6Controls.V6Label();
            this.TxtTen_ts = new V6Controls.V6ColorTextBox();
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
            this.btnHuy.Location = new System.Drawing.Point(94, 366);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 16;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 366);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 15;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtSo_the_ts
            // 
            this.txtSo_the_ts.AccessibleName = "so_the_ts";
            this.txtSo_the_ts.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtSo_the_ts.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSo_the_ts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSo_the_ts.BrotherFields = "";
            this.txtSo_the_ts.CheckNotEmpty = true;
            this.txtSo_the_ts.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSo_the_ts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSo_the_ts.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSo_the_ts.HoverColor = System.Drawing.Color.Yellow;
            this.txtSo_the_ts.LeaveColor = System.Drawing.Color.White;
            this.txtSo_the_ts.Location = new System.Drawing.Point(106, 7);
            this.txtSo_the_ts.Name = "txtSo_the_ts";
            this.txtSo_the_ts.ReadOnly = true;
            this.txtSo_the_ts.Size = new System.Drawing.Size(147, 20);
            this.txtSo_the_ts.TabIndex = 0;
            this.txtSo_the_ts.TabStop = false;
            this.txtSo_the_ts.VVar = "SO_THE_TS";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00005";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(9, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(56, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Mã tài sản";
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00019";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(9, 335);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(48, 13);
            this.v6Label12.TabIndex = 24;
            this.v6Label12.Text = "Diễn giải";
            // 
            // txtdien_giai
            // 
            this.txtdien_giai.AccessibleName = "DIEN_GIAI";
            this.txtdien_giai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdien_giai.BackColor = System.Drawing.SystemColors.Window;
            this.txtdien_giai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdien_giai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdien_giai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdien_giai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdien_giai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdien_giai.HoverColor = System.Drawing.Color.Yellow;
            this.txtdien_giai.LeaveColor = System.Drawing.Color.White;
            this.txtdien_giai.Location = new System.Drawing.Point(106, 332);
            this.txtdien_giai.Name = "txtdien_giai";
            this.txtdien_giai.Size = new System.Drawing.Size(522, 20);
            this.txtdien_giai.TabIndex = 14;
            // 
            // txtSoCt
            // 
            this.txtSoCt.AccessibleName = "so_ct";
            this.txtSoCt.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoCt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCt.LeaveColor = System.Drawing.Color.White;
            this.txtSoCt.Location = new System.Drawing.Point(106, 182);
            this.txtSoCt.Name = "txtSoCt";
            this.txtSoCt.Size = new System.Drawing.Size(146, 20);
            this.txtSoCt.TabIndex = 8;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "XULYL00013";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(9, 185);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(65, 13);
            this.v6Label7.TabIndex = 15;
            this.v6Label7.Text = "Số chứng từ";
            // 
            // dateNgayCT
            // 
            this.dateNgayCT.AccessibleName = "ngay_ct";
            this.dateNgayCT.CustomFormat = "dd/MM/yyyy";
            this.dateNgayCT.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayCT.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayCT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayCT.LeaveColor = System.Drawing.Color.White;
            this.dateNgayCT.Location = new System.Drawing.Point(106, 157);
            this.dateNgayCT.Name = "dateNgayCT";
            this.dateNgayCT.Size = new System.Drawing.Size(146, 20);
            this.dateNgayCT.TabIndex = 7;
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00003";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(9, 160);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(77, 13);
            this.v6Label3.TabIndex = 35;
            this.v6Label3.Text = "Ngày chứng từ";
            // 
            // txtNam
            // 
            this.txtNam.AccessibleName = "NAM";
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(106, 32);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(146, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtThang1
            // 
            this.txtThang1.AccessibleName = "KY";
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(106, 57);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(146, 20);
            this.txtThang1.TabIndex = 3;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "XULYL00009";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(9, 35);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 39;
            this.v6Label9.Text = "Năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00010";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Kỳ";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00011";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(9, 85);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(76, 13);
            this.v6Label2.TabIndex = 41;
            this.v6Label2.Text = "Mã nguồn vốn";
            // 
            // TxtMa_NV
            // 
            this.TxtMa_NV.AccessibleName = "MA_NV";
            this.TxtMa_NV.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_NV.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_NV.CheckNotEmpty = true;
            this.TxtMa_NV.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_NV.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_NV.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_NV.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_NV.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_NV.Location = new System.Drawing.Point(106, 82);
            this.TxtMa_NV.Name = "TxtMa_NV";
            this.TxtMa_NV.Size = new System.Drawing.Size(146, 20);
            this.TxtMa_NV.TabIndex = 4;
            this.TxtMa_NV.VVar = "MA_NV";
            // 
            // txtLyDoTang
            // 
            this.txtLyDoTang.AccessibleName = "MA_TG_TS";
            this.txtLyDoTang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyDoTang.BackColor = System.Drawing.Color.White;
            this.txtLyDoTang.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLyDoTang.BrotherFields = "TEN_TG_TS";
            this.txtLyDoTang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLyDoTang.CheckNotEmpty = true;
            this.txtLyDoTang.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLyDoTang.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLyDoTang.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLyDoTang.HoverColor = System.Drawing.Color.Yellow;
            this.txtLyDoTang.LeaveColor = System.Drawing.Color.White;
            this.txtLyDoTang.Location = new System.Drawing.Point(106, 107);
            this.txtLyDoTang.Name = "txtLyDoTang";
            this.txtLyDoTang.Size = new System.Drawing.Size(146, 20);
            this.txtLyDoTang.TabIndex = 5;
            this.txtLyDoTang.VVar = "MA_TG_TS";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00051";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(9, 110);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(46, 13);
            this.v6Label4.TabIndex = 43;
            this.v6Label4.Text = "Mã tăng";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00052";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(9, 135);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(56, 13);
            this.v6Label5.TabIndex = 44;
            this.v6Label5.Text = "Ngày tăng";
            // 
            // txtnguyen_gia
            // 
            this.txtnguyen_gia.AccessibleName = "nguyen_gia";
            this.txtnguyen_gia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnguyen_gia.BackColor = System.Drawing.Color.White;
            this.txtnguyen_gia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtnguyen_gia.DecimalPlaces = 0;
            this.txtnguyen_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtnguyen_gia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtnguyen_gia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtnguyen_gia.HoverColor = System.Drawing.Color.Yellow;
            this.txtnguyen_gia.LeaveColor = System.Drawing.Color.White;
            this.txtnguyen_gia.Location = new System.Drawing.Point(106, 207);
            this.txtnguyen_gia.Name = "txtnguyen_gia";
            this.txtnguyen_gia.Size = new System.Drawing.Size(147, 20);
            this.txtnguyen_gia.TabIndex = 9;
            this.txtnguyen_gia.Text = "0";
            this.txtnguyen_gia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtnguyen_gia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtnguyen_gia.V6LostFocus += new V6Controls.ControlEventHandle(this.txtnguyen_gia_V6LostFocus);
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00014";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(9, 210);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(61, 13);
            this.v6Label6.TabIndex = 46;
            this.v6Label6.Text = "Nguyên giá";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00015";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(9, 235);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(69, 13);
            this.v6Label8.TabIndex = 48;
            this.v6Label8.Text = "Đã khấu hao";
            // 
            // Txtgt_da_kh
            // 
            this.Txtgt_da_kh.AccessibleName = "gt_da_kh";
            this.Txtgt_da_kh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtgt_da_kh.BackColor = System.Drawing.Color.White;
            this.Txtgt_da_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtgt_da_kh.DecimalPlaces = 0;
            this.Txtgt_da_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtgt_da_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtgt_da_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtgt_da_kh.HoverColor = System.Drawing.Color.Yellow;
            this.Txtgt_da_kh.LeaveColor = System.Drawing.Color.White;
            this.Txtgt_da_kh.Location = new System.Drawing.Point(106, 232);
            this.Txtgt_da_kh.Name = "Txtgt_da_kh";
            this.Txtgt_da_kh.Size = new System.Drawing.Size(147, 20);
            this.Txtgt_da_kh.TabIndex = 10;
            this.Txtgt_da_kh.Text = "0";
            this.Txtgt_da_kh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgt_da_kh.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Txtgt_da_kh.V6LostFocus += new V6Controls.ControlEventHandle(this.txtnguyen_gia_V6LostFocus);
            // 
            // v6Label10
            // 
            this.v6Label10.AccessibleDescription = "XULYL00017";
            this.v6Label10.AutoSize = true;
            this.v6Label10.Location = new System.Drawing.Point(9, 285);
            this.v6Label10.Name = "v6Label10";
            this.v6Label10.Size = new System.Drawing.Size(82, 13);
            this.v6Label10.TabIndex = 52;
            this.v6Label10.Text = "Số kỳ khấu hao";
            // 
            // TxtSo_ky
            // 
            this.TxtSo_ky.AccessibleName = "so_ky";
            this.TxtSo_ky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSo_ky.BackColor = System.Drawing.Color.White;
            this.TxtSo_ky.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtSo_ky.DecimalPlaces = 0;
            this.TxtSo_ky.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtSo_ky.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtSo_ky.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtSo_ky.HoverColor = System.Drawing.Color.Yellow;
            this.TxtSo_ky.LeaveColor = System.Drawing.Color.White;
            this.TxtSo_ky.Location = new System.Drawing.Point(106, 282);
            this.TxtSo_ky.Name = "TxtSo_ky";
            this.TxtSo_ky.Size = new System.Drawing.Size(147, 20);
            this.TxtSo_ky.TabIndex = 12;
            this.TxtSo_ky.Text = "0";
            this.TxtSo_ky.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSo_ky.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TxtSo_ky.V6LostFocus += new V6Controls.ControlEventHandle(this.txtnguyen_gia_V6LostFocus);
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "XULYL00016";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(9, 260);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(39, 13);
            this.v6Label11.TabIndex = 50;
            this.v6Label11.Text = "Còn lại";
            // 
            // txtgt_cl
            // 
            this.txtgt_cl.AccessibleName = "gt_cl";
            this.txtgt_cl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtgt_cl.BackColor = System.Drawing.Color.White;
            this.txtgt_cl.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtgt_cl.DecimalPlaces = 0;
            this.txtgt_cl.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtgt_cl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtgt_cl.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtgt_cl.HoverColor = System.Drawing.Color.Yellow;
            this.txtgt_cl.LeaveColor = System.Drawing.Color.White;
            this.txtgt_cl.Location = new System.Drawing.Point(106, 257);
            this.txtgt_cl.Name = "txtgt_cl";
            this.txtgt_cl.Size = new System.Drawing.Size(147, 20);
            this.txtgt_cl.TabIndex = 11;
            this.txtgt_cl.Text = "0";
            this.txtgt_cl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtgt_cl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtgt_cl.V6LostFocus += new V6Controls.ControlEventHandle(this.txtnguyen_gia_V6LostFocus);
            // 
            // DateNgay_tg
            // 
            this.DateNgay_tg.AccessibleName = "ngay_tg";
            this.DateNgay_tg.CustomFormat = "dd/MM/yyyy";
            this.DateNgay_tg.EnterColor = System.Drawing.Color.PaleGreen;
            this.DateNgay_tg.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateNgay_tg.HoverColor = System.Drawing.Color.Yellow;
            this.DateNgay_tg.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DateNgay_tg.LeaveColor = System.Drawing.Color.White;
            this.DateNgay_tg.Location = new System.Drawing.Point(106, 132);
            this.DateNgay_tg.Name = "DateNgay_tg";
            this.DateNgay_tg.Size = new System.Drawing.Size(146, 20);
            this.DateNgay_tg.TabIndex = 6;
            // 
            // v6Label15
            // 
            this.v6Label15.AccessibleDescription = "XULYL00018";
            this.v6Label15.AutoSize = true;
            this.v6Label15.Location = new System.Drawing.Point(9, 310);
            this.v6Label15.Name = "v6Label15";
            this.v6Label15.Size = new System.Drawing.Size(80, 13);
            this.v6Label15.TabIndex = 57;
            this.v6Label15.Text = "Gt khấu hao kỳ";
            // 
            // Txtgt_kh_ky
            // 
            this.Txtgt_kh_ky.AccessibleName = "gt_kh_ky";
            this.Txtgt_kh_ky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtgt_kh_ky.BackColor = System.Drawing.Color.White;
            this.Txtgt_kh_ky.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtgt_kh_ky.DecimalPlaces = 0;
            this.Txtgt_kh_ky.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtgt_kh_ky.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtgt_kh_ky.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtgt_kh_ky.HoverColor = System.Drawing.Color.Yellow;
            this.Txtgt_kh_ky.LeaveColor = System.Drawing.Color.White;
            this.Txtgt_kh_ky.Location = new System.Drawing.Point(106, 307);
            this.Txtgt_kh_ky.Name = "Txtgt_kh_ky";
            this.Txtgt_kh_ky.Size = new System.Drawing.Size(147, 20);
            this.Txtgt_kh_ky.TabIndex = 13;
            this.Txtgt_kh_ky.Text = "0";
            this.Txtgt_kh_ky.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgt_kh_ky.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTs0
            // 
            this.txtTs0.AccessibleName = "Ts0";
            this.txtTs0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTs0.BackColor = System.Drawing.Color.White;
            this.txtTs0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTs0.DecimalPlaces = 0;
            this.txtTs0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTs0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTs0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTs0.HoverColor = System.Drawing.Color.Yellow;
            this.txtTs0.LeaveColor = System.Drawing.Color.White;
            this.txtTs0.Location = new System.Drawing.Point(385, 51);
            this.txtTs0.Name = "txtTs0";
            this.txtTs0.Size = new System.Drawing.Size(16, 20);
            this.txtTs0.TabIndex = 59;
            this.txtTs0.Tag = "cancelset";
            this.txtTs0.Text = "1";
            this.txtTs0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTs0.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTs0.Visible = false;
            // 
            // txtTang_giam
            // 
            this.txtTang_giam.AccessibleName = "Tang_giam";
            this.txtTang_giam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTang_giam.BackColor = System.Drawing.Color.White;
            this.txtTang_giam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTang_giam.DecimalPlaces = 0;
            this.txtTang_giam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTang_giam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTang_giam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTang_giam.HoverColor = System.Drawing.Color.Yellow;
            this.txtTang_giam.LeaveColor = System.Drawing.Color.White;
            this.txtTang_giam.Location = new System.Drawing.Point(405, 51);
            this.txtTang_giam.Name = "txtTang_giam";
            this.txtTang_giam.Size = new System.Drawing.Size(16, 20);
            this.txtTang_giam.TabIndex = 58;
            this.txtTang_giam.Tag = "cancelset";
            this.txtTang_giam.Text = "1";
            this.txtTang_giam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTang_giam.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTang_giam.Visible = false;
            // 
            // txtMaCt
            // 
            this.txtMaCt.AccessibleName = "MA_CT";
            this.txtMaCt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaCt.BackColor = System.Drawing.Color.White;
            this.txtMaCt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCt.Enabled = false;
            this.txtMaCt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCt.LeaveColor = System.Drawing.Color.White;
            this.txtMaCt.Location = new System.Drawing.Point(428, 51);
            this.txtMaCt.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCt.Name = "txtMaCt";
            this.txtMaCt.Size = new System.Drawing.Size(50, 20);
            this.txtMaCt.TabIndex = 60;
            this.txtMaCt.Visible = false;
            // 
            // v6Label14
            // 
            this.v6Label14.AccessibleDescription = "XULYL00053";
            this.v6Label14.AutoSize = true;
            this.v6Label14.Location = new System.Drawing.Point(259, 286);
            this.v6Label14.Name = "v6Label14";
            this.v6Label14.Size = new System.Drawing.Size(64, 13);
            this.v6Label14.TabIndex = 54;
            this.v6Label14.Text = "(Tăng thêm)";
            // 
            // v6Label13
            // 
            this.v6Label13.AccessibleDescription = "XULYL00053";
            this.v6Label13.AutoSize = true;
            this.v6Label13.Location = new System.Drawing.Point(259, 212);
            this.v6Label13.Name = "v6Label13";
            this.v6Label13.Size = new System.Drawing.Size(64, 13);
            this.v6Label13.TabIndex = 53;
            this.v6Label13.Text = "(Tăng thêm)";
            // 
            // TxtTen_ts
            // 
            this.TxtTen_ts.AccessibleName = "ten_ts";
            this.TxtTen_ts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTen_ts.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TxtTen_ts.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTen_ts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTen_ts.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTen_ts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTen_ts.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTen_ts.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTen_ts.LeaveColor = System.Drawing.Color.White;
            this.TxtTen_ts.Location = new System.Drawing.Point(256, 7);
            this.TxtTen_ts.Name = "TxtTen_ts";
            this.TxtTen_ts.ReadOnly = true;
            this.TxtTen_ts.Size = new System.Drawing.Size(375, 20);
            this.TxtTen_ts.TabIndex = 1;
            this.TxtTen_ts.TabStop = false;
            this.TxtTen_ts.Tag = "disable";
            // 
            // AFATANGNG_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMaCt);
            this.Controls.Add(this.txtTang_giam);
            this.Controls.Add(this.txtTs0);
            this.Controls.Add(this.v6Label15);
            this.Controls.Add(this.Txtgt_kh_ky);
            this.Controls.Add(this.DateNgay_tg);
            this.Controls.Add(this.v6Label14);
            this.Controls.Add(this.v6Label13);
            this.Controls.Add(this.v6Label10);
            this.Controls.Add(this.TxtSo_ky);
            this.Controls.Add(this.v6Label11);
            this.Controls.Add(this.txtgt_cl);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.Txtgt_da_kh);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.txtnguyen_gia);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.txtLyDoTang);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.TxtMa_NV);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgayCT);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.TxtTen_ts);
            this.Controls.Add(this.txtdien_giai);
            this.Controls.Add(this.v6Label12);
            this.Controls.Add(this.txtSo_the_ts);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.txtSoCt);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AFATANGNG_F4";
            this.Size = new System.Drawing.Size(638, 418);
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6VvarTextBox txtSo_the_ts;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label12;
        public V6Controls.V6ColorTextBox txtSoCt;
        public V6Controls.V6ColorTextBox txtdien_giai;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6DateTimePicker dateNgayCT;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox TxtMa_NV;
        private V6Controls.V6VvarTextBox txtLyDoTang;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6Label v6Label5;
        private V6Controls.NumberTien txtnguyen_gia;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label8;
        private V6Controls.NumberTien Txtgt_da_kh;
        private V6Controls.V6Label v6Label10;
        private V6Controls.NumberTien TxtSo_ky;
        private V6Controls.V6Label v6Label11;
        private V6Controls.NumberTien txtgt_cl;
        private V6Controls.V6DateTimePicker DateNgay_tg;
        private V6Controls.V6Label v6Label15;
        private V6Controls.NumberTien Txtgt_kh_ky;
        private V6Controls.V6NumberTextBox txtTs0;
        private V6Controls.V6NumberTextBox txtTang_giam;
        private V6Controls.V6ColorTextBox txtMaCt;
        private V6Controls.V6Label v6Label14;
        private V6Controls.V6Label v6Label13;
        private V6Controls.V6ColorTextBox TxtTen_ts;
    }
}