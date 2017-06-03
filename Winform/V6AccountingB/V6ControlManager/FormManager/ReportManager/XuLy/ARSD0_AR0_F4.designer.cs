using System.Windows.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class ARSD0_AR0_F4
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
            this.v6Label17 = new V6Controls.V6Label();
            this.v6Label16 = new V6Controls.V6Label();
            this.lbTxtT_CL_NT = new V6Controls.V6Label();
            this.TxtT_CL_NT = new V6Controls.NumberTien();
            this.lbTxtT_tt_qd = new V6Controls.V6Label();
            this.TxtT_tt_qd = new V6Controls.NumberTien();
            this.txtT_Tt_NT0 = new V6Controls.NumberTien();
            this.v6Label8 = new V6Controls.V6Label();
            this.Txtt_tt = new V6Controls.NumberTien();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.txttk = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtma_kh = new V6Controls.V6VvarTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label12 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtngay_ct = new V6Controls.V6DateTimePick();
            this.txtdien_giai = new V6Controls.V6VvarTextBox();
            this.txtma_nt = new V6Controls.V6VvarTextBox();
            this.txtten_nt = new V6Controls.V6LabelTextBox();
            this.txtTen_Kh = new V6Controls.V6LabelTextBox();
            this.txtTen_Tk = new V6Controls.V6LabelTextBox();
            this.txtten_dvcs = new V6Controls.V6LabelTextBox();
            this.txtma_dvcs = new V6Controls.V6VvarTextBox();
            this.txtso_ct = new V6Controls.V6VvarTextBox();
            this.lbT_Tt_NT0 = new V6Controls.V6Label();
            this.txtt_tt_nt = new V6Controls.NumberTienNt();
            this.txtty_gia = new V6Controls.NumberTienNt();
            this.txthan_tt = new V6Controls.NumberTien();
            this.txttat_toan = new V6Controls.NumberTien();
            this.txtT_Tt0 = new V6Controls.NumberTien();
            this.SuspendLayout();
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "XULYL00112";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(256, 377);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(163, 13);
            this.v6Label17.TabIndex = 34;
            this.v6Label17.Text = "0 - Chưa tất toán , 1 - Đã tất toán";
            // 
            // v6Label16
            // 
            this.v6Label16.AccessibleDescription = "XULYL00111";
            this.v6Label16.AutoSize = true;
            this.v6Label16.Location = new System.Drawing.Point(28, 377);
            this.v6Label16.Name = "v6Label16";
            this.v6Label16.Size = new System.Drawing.Size(47, 13);
            this.v6Label16.TabIndex = 32;
            this.v6Label16.Text = "Tất toán";
            // 
            // lbTxtT_CL_NT
            // 
            this.lbTxtT_CL_NT.AccessibleDescription = "XULYL00109";
            this.lbTxtT_CL_NT.AutoSize = true;
            this.lbTxtT_CL_NT.Location = new System.Drawing.Point(26, 324);
            this.lbTxtT_CL_NT.Name = "lbTxtT_CL_NT";
            this.lbTxtT_CL_NT.Size = new System.Drawing.Size(128, 13);
            this.lbTxtT_CL_NT.TabIndex = 28;
            this.lbTxtT_CL_NT.Text = "Số tiền còn phải thu VND";
            // 
            // TxtT_CL_NT
            // 
            this.TxtT_CL_NT.AccessibleName = "T_CL_NT";
            this.TxtT_CL_NT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtT_CL_NT.BackColor = System.Drawing.Color.White;
            this.TxtT_CL_NT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtT_CL_NT.DecimalPlaces = 0;
            this.TxtT_CL_NT.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtT_CL_NT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtT_CL_NT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtT_CL_NT.HoverColor = System.Drawing.Color.Yellow;
            this.TxtT_CL_NT.LeaveColor = System.Drawing.Color.White;
            this.TxtT_CL_NT.Location = new System.Drawing.Point(172, 323);
            this.TxtT_CL_NT.Name = "TxtT_CL_NT";
            this.TxtT_CL_NT.Size = new System.Drawing.Size(146, 20);
            this.TxtT_CL_NT.TabIndex = 29;
            this.TxtT_CL_NT.Text = "0";
            this.TxtT_CL_NT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtT_CL_NT.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lbTxtT_tt_qd
            // 
            this.lbTxtT_tt_qd.AccessibleDescription = "XULYL00108";
            this.lbTxtT_tt_qd.AutoSize = true;
            this.lbTxtT_tt_qd.Location = new System.Drawing.Point(26, 298);
            this.lbTxtT_tt_qd.Name = "lbTxtT_tt_qd";
            this.lbTxtT_tt_qd.Size = new System.Drawing.Size(100, 13);
            this.lbTxtT_tt_qd.TabIndex = 26;
            this.lbTxtT_tt_qd.Text = "Số tiền đã thu VND";
            // 
            // TxtT_tt_qd
            // 
            this.TxtT_tt_qd.AccessibleName = "T_tt_qd";
            this.TxtT_tt_qd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtT_tt_qd.BackColor = System.Drawing.Color.White;
            this.TxtT_tt_qd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtT_tt_qd.DecimalPlaces = 0;
            this.TxtT_tt_qd.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtT_tt_qd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtT_tt_qd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtT_tt_qd.HoverColor = System.Drawing.Color.Yellow;
            this.TxtT_tt_qd.LeaveColor = System.Drawing.Color.White;
            this.TxtT_tt_qd.Location = new System.Drawing.Point(172, 297);
            this.TxtT_tt_qd.Name = "TxtT_tt_qd";
            this.TxtT_tt_qd.Size = new System.Drawing.Size(146, 20);
            this.TxtT_tt_qd.TabIndex = 27;
            this.TxtT_tt_qd.Text = "0";
            this.TxtT_tt_qd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtT_tt_qd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TxtT_tt_qd.TextChanged += new System.EventHandler(this.txtT_Tt_NT0_TextChanged);
            // 
            // txtT_Tt_NT0
            // 
            this.txtT_Tt_NT0.AccessibleName = "T_Tt_NT0";
            this.txtT_Tt_NT0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtT_Tt_NT0.BackColor = System.Drawing.Color.White;
            this.txtT_Tt_NT0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtT_Tt_NT0.DecimalPlaces = 0;
            this.txtT_Tt_NT0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtT_Tt_NT0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtT_Tt_NT0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtT_Tt_NT0.HoverColor = System.Drawing.Color.Yellow;
            this.txtT_Tt_NT0.LeaveColor = System.Drawing.Color.White;
            this.txtT_Tt_NT0.Location = new System.Drawing.Point(172, 271);
            this.txtT_Tt_NT0.Name = "txtT_Tt_NT0";
            this.txtT_Tt_NT0.Size = new System.Drawing.Size(146, 20);
            this.txtT_Tt_NT0.TabIndex = 25;
            this.txtT_Tt_NT0.Text = "0";
            this.txtT_Tt_NT0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtT_Tt_NT0.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtT_Tt_NT0.TextChanged += new System.EventHandler(this.txtT_Tt_NT0_TextChanged);
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00101";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(26, 246);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(81, 13);
            this.v6Label8.TabIndex = 22;
            this.v6Label8.Text = "Tổng tiền VND ";
            // 
            // Txtt_tt
            // 
            this.Txtt_tt.AccessibleName = "t_tt";
            this.Txtt_tt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtt_tt.BackColor = System.Drawing.Color.White;
            this.Txtt_tt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtt_tt.DecimalPlaces = 0;
            this.Txtt_tt.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtt_tt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtt_tt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtt_tt.HoverColor = System.Drawing.Color.Yellow;
            this.Txtt_tt.LeaveColor = System.Drawing.Color.White;
            this.Txtt_tt.Location = new System.Drawing.Point(172, 245);
            this.Txtt_tt.Name = "Txtt_tt";
            this.Txtt_tt.Size = new System.Drawing.Size(146, 20);
            this.Txtt_tt.TabIndex = 23;
            this.Txtt_tt.Text = "0";
            this.Txtt_tt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtt_tt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00091";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(26, 220);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(36, 13);
            this.v6Label6.TabIndex = 20;
            this.v6Label6.Text = "Tỷ giá";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00019";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(26, 147);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(48, 13);
            this.v6Label5.TabIndex = 13;
            this.v6Label5.Text = "Diễn giải";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00107";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(26, 117);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(62, 13);
            this.v6Label4.TabIndex = 10;
            this.v6Label4.Text = "Tk công nợ";
            // 
            // txttk
            // 
            this.txttk.AccessibleName = "tk";
            this.txttk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txttk.BackColor = System.Drawing.Color.White;
            this.txttk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk.BrotherFields = "Ten_Tk";
            this.txttk.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txttk.CheckNotEmpty = true;
            this.txttk.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk.HoverColor = System.Drawing.Color.Yellow;
            this.txttk.LeaveColor = System.Drawing.Color.White;
            this.txttk.Location = new System.Drawing.Point(172, 117);
            this.txttk.Name = "txttk";
            this.txttk.Size = new System.Drawing.Size(145, 20);
            this.txttk.TabIndex = 11;
            this.txttk.VVar = "tk";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00041";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(26, 92);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(65, 13);
            this.v6Label2.TabIndex = 7;
            this.v6Label2.Text = "Khách hàng";
            // 
            // txtma_kh
            // 
            this.txtma_kh.AccessibleName = "ma_kh";
            this.txtma_kh.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_kh.BrotherFields = "Ten_Kh";
            this.txtma_kh.CheckNotEmpty = true;
            this.txtma_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_kh.LeaveColor = System.Drawing.Color.White;
            this.txtma_kh.Location = new System.Drawing.Point(172, 91);
            this.txtma_kh.Name = "txtma_kh";
            this.txtma_kh.Size = new System.Drawing.Size(146, 20);
            this.txtma_kh.TabIndex = 8;
            this.txtma_kh.VVar = "ma_kh";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "XULYL00003";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(26, 44);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(77, 13);
            this.v6Label9.TabIndex = 3;
            this.v6Label9.Text = "Ngày chứng từ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00013";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00086";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(26, 173);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(47, 13);
            this.v6Label3.TabIndex = 15;
            this.v6Label3.Text = "Loại tiền";
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00110";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(28, 350);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(81, 13);
            this.v6Label12.TabIndex = 30;
            this.v6Label12.Text = "Hạn thanh toán";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "XULYL00082";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(26, 195);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(128, 13);
            this.v6Label7.TabIndex = 18;
            this.v6Label7.Text = "Tổng tiền nt trên hóa đơn";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00040";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(26, 18);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(55, 13);
            this.v6Label1.TabIndex = 0;
            this.v6Label1.Text = "Mã đơn vị";
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleDescription = "REPORTB00005";
            this.btnThoat.AccessibleName = "";
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnThoat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnThoat.Location = new System.Drawing.Point(111, 424);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 40);
            this.btnThoat.TabIndex = 36;
            this.btnThoat.Text = "&Hủy";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(23, 424);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 35;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtngay_ct
            // 
            this.txtngay_ct.AccessibleName = "ngay_ct";
            this.txtngay_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtngay_ct.CustomFormat = "dd/MM/yyyy";
            this.txtngay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_ct.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtngay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtngay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_ct.LeaveColor = System.Drawing.Color.White;
            this.txtngay_ct.Location = new System.Drawing.Point(173, 40);
            this.txtngay_ct.Name = "txtngay_ct";
            this.txtngay_ct.Size = new System.Drawing.Size(146, 20);
            this.txtngay_ct.TabIndex = 4;
            // 
            // txtdien_giai
            // 
            this.txtdien_giai.AccessibleName = "dien_giai";
            this.txtdien_giai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdien_giai.BackColor = System.Drawing.Color.White;
            this.txtdien_giai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdien_giai.BrotherFields = "";
            this.txtdien_giai.CheckNotEmpty = true;
            this.txtdien_giai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdien_giai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdien_giai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdien_giai.HoverColor = System.Drawing.Color.Yellow;
            this.txtdien_giai.LeaveColor = System.Drawing.Color.White;
            this.txtdien_giai.Location = new System.Drawing.Point(172, 144);
            this.txtdien_giai.Name = "txtdien_giai";
            this.txtdien_giai.Size = new System.Drawing.Size(447, 20);
            this.txtdien_giai.TabIndex = 14;
            // 
            // txtma_nt
            // 
            this.txtma_nt.AccessibleName = "ma_nt";
            this.txtma_nt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_nt.BackColor = System.Drawing.Color.White;
            this.txtma_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_nt.BrotherFields = "ten_nt";
            this.txtma_nt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_nt.CheckNotEmpty = true;
            this.txtma_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_nt.LeaveColor = System.Drawing.Color.White;
            this.txtma_nt.Location = new System.Drawing.Point(173, 168);
            this.txtma_nt.Name = "txtma_nt";
            this.txtma_nt.Size = new System.Drawing.Size(56, 20);
            this.txtma_nt.TabIndex = 16;
            this.txtma_nt.VVar = "ma_nt";
            this.txtma_nt.TextChanged += new System.EventHandler(this.txtma_nt_TextChanged);
            // 
            // txtten_nt
            // 
            this.txtten_nt.AccessibleName = "ten_nt";
            this.txtten_nt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_nt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_nt.Location = new System.Drawing.Point(235, 171);
            this.txtten_nt.Name = "txtten_nt";
            this.txtten_nt.ReadOnly = true;
            this.txtten_nt.Size = new System.Drawing.Size(342, 13);
            this.txtten_nt.TabIndex = 17;
            this.txtten_nt.TabStop = false;
            this.txtten_nt.Tag = "readonly";
            // 
            // txtTen_Kh
            // 
            this.txtTen_Kh.AccessibleName = "Ten_Kh";
            this.txtTen_Kh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtTen_Kh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTen_Kh.Location = new System.Drawing.Point(334, 94);
            this.txtTen_Kh.Name = "txtTen_Kh";
            this.txtTen_Kh.ReadOnly = true;
            this.txtTen_Kh.Size = new System.Drawing.Size(285, 13);
            this.txtTen_Kh.TabIndex = 9;
            this.txtTen_Kh.TabStop = false;
            this.txtTen_Kh.Tag = "readonly";
            // 
            // txtTen_Tk
            // 
            this.txtTen_Tk.AccessibleName = "Ten_Tk";
            this.txtTen_Tk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtTen_Tk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTen_Tk.Location = new System.Drawing.Point(335, 122);
            this.txtTen_Tk.Name = "txtTen_Tk";
            this.txtTen_Tk.ReadOnly = true;
            this.txtTen_Tk.Size = new System.Drawing.Size(285, 13);
            this.txtTen_Tk.TabIndex = 12;
            this.txtTen_Tk.TabStop = false;
            this.txtTen_Tk.Tag = "readonly";
            // 
            // txtten_dvcs
            // 
            this.txtten_dvcs.AccessibleName = "ten_dvcs";
            this.txtten_dvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_dvcs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_dvcs.Location = new System.Drawing.Point(335, 19);
            this.txtten_dvcs.Name = "txtten_dvcs";
            this.txtten_dvcs.ReadOnly = true;
            this.txtten_dvcs.Size = new System.Drawing.Size(285, 13);
            this.txtten_dvcs.TabIndex = 2;
            this.txtten_dvcs.TabStop = false;
            this.txtten_dvcs.Tag = "readonly";
            // 
            // txtma_dvcs
            // 
            this.txtma_dvcs.AccessibleName = "ma_dvcs";
            this.txtma_dvcs.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_dvcs.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_dvcs.BrotherFields = "ten_dvcs";
            this.txtma_dvcs.CheckNotEmpty = true;
            this.txtma_dvcs.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_dvcs.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_dvcs.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_dvcs.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_dvcs.LeaveColor = System.Drawing.Color.White;
            this.txtma_dvcs.Location = new System.Drawing.Point(172, 16);
            this.txtma_dvcs.Name = "txtma_dvcs";
            this.txtma_dvcs.Size = new System.Drawing.Size(146, 20);
            this.txtma_dvcs.TabIndex = 1;
            this.txtma_dvcs.VVar = "ma_dvcs";
            // 
            // txtso_ct
            // 
            this.txtso_ct.AccessibleName = "so_ct";
            this.txtso_ct.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_ct.BrotherFields = "Ten_Kh";
            this.txtso_ct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtso_ct.CheckNotEmpty = true;
            this.txtso_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_ct.LeaveColor = System.Drawing.Color.White;
            this.txtso_ct.Location = new System.Drawing.Point(173, 65);
            this.txtso_ct.Name = "txtso_ct";
            this.txtso_ct.Size = new System.Drawing.Size(146, 20);
            this.txtso_ct.TabIndex = 6;
            // 
            // lbT_Tt_NT0
            // 
            this.lbT_Tt_NT0.AccessibleDescription = "XULYL00092";
            this.lbT_Tt_NT0.AccessibleName = "";
            this.lbT_Tt_NT0.AutoSize = true;
            this.lbT_Tt_NT0.Location = new System.Drawing.Point(26, 272);
            this.lbT_Tt_NT0.Name = "lbT_Tt_NT0";
            this.lbT_Tt_NT0.Size = new System.Drawing.Size(119, 13);
            this.lbT_Tt_NT0.TabIndex = 24;
            this.lbT_Tt_NT0.Text = "Tổng tiền phải thu VND";
            // 
            // txtt_tt_nt
            // 
            this.txtt_tt_nt.AccessibleName = "T_TT_NT";
            this.txtt_tt_nt.BackColor = System.Drawing.SystemColors.Window;
            this.txtt_tt_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_tt_nt.DecimalPlaces = 0;
            this.txtt_tt_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_tt_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_tt_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_tt_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_tt_nt.LeaveColor = System.Drawing.Color.White;
            this.txtt_tt_nt.Location = new System.Drawing.Point(172, 192);
            this.txtt_tt_nt.Name = "txtt_tt_nt";
            this.txtt_tt_nt.Size = new System.Drawing.Size(146, 20);
            this.txtt_tt_nt.TabIndex = 19;
            this.txtt_tt_nt.Text = "0";
            this.txtt_tt_nt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_tt_nt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtt_tt_nt.TextChanged += new System.EventHandler(this.txtt_tt_nt_TextChanged_1);
            // 
            // txtty_gia
            // 
            this.txtty_gia.AccessibleName = "ty_gia";
            this.txtty_gia.BackColor = System.Drawing.SystemColors.Window;
            this.txtty_gia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtty_gia.DecimalPlaces = 0;
            this.txtty_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtty_gia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtty_gia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtty_gia.HoverColor = System.Drawing.Color.Yellow;
            this.txtty_gia.LeaveColor = System.Drawing.Color.White;
            this.txtty_gia.Location = new System.Drawing.Point(173, 217);
            this.txtty_gia.Name = "txtty_gia";
            this.txtty_gia.Size = new System.Drawing.Size(146, 20);
            this.txtty_gia.TabIndex = 21;
            this.txtty_gia.Text = "0";
            this.txtty_gia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtty_gia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtty_gia.TextChanged += new System.EventHandler(this.txtty_gia_TextChanged);
            // 
            // txthan_tt
            // 
            this.txthan_tt.AccessibleName = "han_tt";
            this.txthan_tt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txthan_tt.BackColor = System.Drawing.Color.White;
            this.txthan_tt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txthan_tt.DecimalPlaces = 0;
            this.txthan_tt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txthan_tt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txthan_tt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txthan_tt.HoverColor = System.Drawing.Color.Yellow;
            this.txthan_tt.LeaveColor = System.Drawing.Color.White;
            this.txthan_tt.Location = new System.Drawing.Point(173, 347);
            this.txthan_tt.Name = "txthan_tt";
            this.txthan_tt.Size = new System.Drawing.Size(57, 20);
            this.txthan_tt.TabIndex = 37;
            this.txthan_tt.Text = "0";
            this.txthan_tt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txthan_tt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txttat_toan
            // 
            this.txttat_toan.AccessibleName = "tat_toan";
            this.txttat_toan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txttat_toan.BackColor = System.Drawing.Color.White;
            this.txttat_toan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttat_toan.DecimalPlaces = 0;
            this.txttat_toan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttat_toan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttat_toan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttat_toan.HoverColor = System.Drawing.Color.Yellow;
            this.txttat_toan.LeaveColor = System.Drawing.Color.White;
            this.txttat_toan.Location = new System.Drawing.Point(172, 374);
            this.txttat_toan.Name = "txttat_toan";
            this.txttat_toan.Size = new System.Drawing.Size(57, 20);
            this.txttat_toan.TabIndex = 38;
            this.txttat_toan.Text = "0";
            this.txttat_toan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttat_toan.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtT_Tt0
            // 
            this.txtT_Tt0.AccessibleName = "T_Tt0";
            this.txtT_Tt0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtT_Tt0.BackColor = System.Drawing.Color.White;
            this.txtT_Tt0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtT_Tt0.DecimalPlaces = 0;
            this.txtT_Tt0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtT_Tt0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtT_Tt0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtT_Tt0.HoverColor = System.Drawing.Color.Yellow;
            this.txtT_Tt0.LeaveColor = System.Drawing.Color.White;
            this.txtT_Tt0.Location = new System.Drawing.Point(334, 272);
            this.txtT_Tt0.Name = "txtT_Tt0";
            this.txtT_Tt0.Size = new System.Drawing.Size(146, 20);
            this.txtT_Tt0.TabIndex = 40;
            this.txtT_Tt0.Text = "0";
            this.txtT_Tt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtT_Tt0.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtT_Tt0.Visible = false;
            // 
            // ARSD0_AR0_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtT_Tt0);
            this.Controls.Add(this.txttat_toan);
            this.Controls.Add(this.txthan_tt);
            this.Controls.Add(this.txtty_gia);
            this.Controls.Add(this.txtt_tt_nt);
            this.Controls.Add(this.txtso_ct);
            this.Controls.Add(this.txtma_dvcs);
            this.Controls.Add(this.txtten_dvcs);
            this.Controls.Add(this.txtTen_Tk);
            this.Controls.Add(this.txtTen_Kh);
            this.Controls.Add(this.txtten_nt);
            this.Controls.Add(this.txtma_nt);
            this.Controls.Add(this.txtdien_giai);
            this.Controls.Add(this.txtngay_ct);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.v6Label16);
            this.Controls.Add(this.lbTxtT_CL_NT);
            this.Controls.Add(this.TxtT_CL_NT);
            this.Controls.Add(this.lbTxtT_tt_qd);
            this.Controls.Add(this.TxtT_tt_qd);
            this.Controls.Add(this.lbT_Tt_NT0);
            this.Controls.Add(this.txtT_Tt_NT0);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.Txtt_tt);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.txttk);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.txtma_kh);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label12);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Name = "ARSD0_AR0_F4";
            this.Size = new System.Drawing.Size(637, 503);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerViewReport;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6Label v6Label16;
        private V6Controls.V6Label lbTxtT_CL_NT;
        private V6Controls.NumberTien TxtT_CL_NT;
        private V6Controls.V6Label lbTxtT_tt_qd;
        private V6Controls.NumberTien TxtT_tt_qd;
        private V6Controls.NumberTien txtT_Tt_NT0;
        private V6Controls.V6Label v6Label8;
        private V6Controls.NumberTien Txtt_tt;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6VvarTextBox txttk;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox txtma_kh;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label12;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6Label v6Label1;
        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        private V6Controls.V6DateTimePick txtngay_ct;
        private V6Controls.V6VvarTextBox txtdien_giai;
        private V6Controls.V6VvarTextBox txtma_nt;
        private V6Controls.V6LabelTextBox txtten_nt;
        private V6Controls.V6LabelTextBox txtTen_Kh;
        private V6Controls.V6LabelTextBox txtTen_Tk;
        private V6Controls.V6LabelTextBox txtten_dvcs;
        private V6Controls.V6VvarTextBox txtma_dvcs;
        private V6Controls.V6VvarTextBox txtso_ct;
        private V6Controls.V6Label lbT_Tt_NT0;
        private V6Controls.NumberTienNt txtt_tt_nt;
        private V6Controls.NumberTienNt txtty_gia;
        private V6Controls.NumberTien txthan_tt;
        private V6Controls.NumberTien txttat_toan;
        private V6Controls.NumberTien txtT_Tt0;
    }
}