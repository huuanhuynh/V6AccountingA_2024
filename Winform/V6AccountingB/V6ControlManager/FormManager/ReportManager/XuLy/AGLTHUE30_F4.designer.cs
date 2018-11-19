using System.Windows.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AGLTHUE30_F4
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.lbTxtT_CL_NT = new V6Controls.V6Label();
            this.lbTxtT_tt_qd = new V6Controls.V6Label();
            this.txtso_luong = new V6Controls.NumberTien();
            this.v6Label8 = new V6Controls.V6Label();
            this.txtt_thue = new V6Controls.NumberTien();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.txtten_kh = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtma_kh = new V6Controls.V6VvarTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtngay_ct = new V6Controls.V6DateTimePicker();
            this.txtdia_chi = new V6Controls.V6VvarTextBox();
            this.txtma_so_thue = new V6Controls.V6VvarTextBox();
            this.txtten_dvcs = new V6Controls.V6LabelTextBox();
            this.txtma_dvcs = new V6Controls.V6VvarTextBox();
            this.txtso_ct = new V6Controls.V6VvarTextBox();
            this.lbT_Tt_NT0 = new V6Controls.V6Label();
            this.txtt_tien_nt = new V6Controls.NumberTienNt();
            this.txtthue_suat = new V6Controls.NumberTienNt();
            this.txtngay_ct0 = new V6Controls.V6DateTimePicker();
            this.v6Label10 = new V6Controls.V6Label();
            this.v6Label13 = new V6Controls.V6Label();
            this.txtso_ct0 = new V6Controls.V6VvarTextBox();
            this.txtso_seri0 = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtma_hd = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmau_bc = new V6Controls.V6VvarTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtma_kho = new V6Controls.V6VvarTextBox();
            this.v6Label11 = new V6Controls.V6Label();
            this.txtma_vv = new V6Controls.V6VvarTextBox();
            this.v6Label14 = new V6Controls.V6Label();
            this.txtten_vt = new V6Controls.V6VvarTextBox();
            this.v6Label15 = new V6Controls.V6Label();
            this.txtgia = new V6Controls.NumberTienNt();
            this.v6Label18 = new V6Controls.V6Label();
            this.v6Label19 = new V6Controls.V6Label();
            this.txtghi_chu = new V6Controls.V6VvarTextBox();
            this.v6Label12 = new V6Controls.V6Label();
            this.txttk_thue_no = new V6Controls.V6VvarTextBox();
            this.txttk_du = new V6Controls.V6VvarTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcel,
            this.printGrid});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 48);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(153, 22);
            this.exportToExcel.Text = "Export To Excel";
            // 
            // printGrid
            // 
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(153, 22);
            this.printGrid.Text = "Print Grid";
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // lbTxtT_CL_NT
            // 
            this.lbTxtT_CL_NT.AccessibleDescription = "XULYL00065";
            this.lbTxtT_CL_NT.AutoSize = true;
            this.lbTxtT_CL_NT.Location = new System.Drawing.Point(26, 364);
            this.lbTxtT_CL_NT.Name = "lbTxtT_CL_NT";
            this.lbTxtT_CL_NT.Size = new System.Drawing.Size(94, 13);
            this.lbTxtT_CL_NT.TabIndex = 28;
            this.lbTxtT_CL_NT.Text = "Tài khoản đối ứng";
            // 
            // lbTxtT_tt_qd
            // 
            this.lbTxtT_tt_qd.AccessibleDescription = "XULYL00080";
            this.lbTxtT_tt_qd.AutoSize = true;
            this.lbTxtT_tt_qd.Location = new System.Drawing.Point(26, 232);
            this.lbTxtT_tt_qd.Name = "lbTxtT_tt_qd";
            this.lbTxtT_tt_qd.Size = new System.Drawing.Size(49, 13);
            this.lbTxtT_tt_qd.TabIndex = 26;
            this.lbTxtT_tt_qd.Text = "Số lượng";
            // 
            // txtso_luong
            // 
            this.txtso_luong.AccessibleName = "so_luong";
            this.txtso_luong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtso_luong.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_luong.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_luong.DecimalPlaces = 0;
            this.txtso_luong.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_luong.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_luong.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_luong.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_luong.LeaveColor = System.Drawing.Color.White;
            this.txtso_luong.Location = new System.Drawing.Point(175, 228);
            this.txtso_luong.Name = "txtso_luong";
            this.txtso_luong.Size = new System.Drawing.Size(146, 20);
            this.txtso_luong.TabIndex = 15;
            this.txtso_luong.Text = "0";
            this.txtso_luong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtso_luong.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtso_luong.V6LostFocus += new V6Controls.ControlEventHandle(this.txtso_luongV6LostFocus);
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00084";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(26, 320);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(76, 13);
            this.v6Label8.TabIndex = 22;
            this.v6Label8.Text = "Tiền thuế VAT";
            // 
            // txtt_thue
            // 
            this.txtt_thue.AccessibleName = "t_thue";
            this.txtt_thue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtt_thue.BackColor = System.Drawing.SystemColors.Window;
            this.txtt_thue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_thue.DecimalPlaces = 0;
            this.txtt_thue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_thue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_thue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_thue.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_thue.LeaveColor = System.Drawing.Color.White;
            this.txtt_thue.Location = new System.Drawing.Point(175, 316);
            this.txtt_thue.Name = "txtt_thue";
            this.txtt_thue.Size = new System.Drawing.Size(146, 20);
            this.txtt_thue.TabIndex = 19;
            this.txtt_thue.Text = "0";
            this.txtt_thue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_thue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00083";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(26, 298);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(55, 13);
            this.v6Label6.TabIndex = 20;
            this.v6Label6.Text = "Thuế suất";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00042";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(26, 144);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(40, 13);
            this.v6Label5.TabIndex = 13;
            this.v6Label5.Text = "Địa chỉ";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00076";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(26, 122);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(62, 13);
            this.v6Label4.TabIndex = 10;
            this.v6Label4.Text = "Khách VAT";
            // 
            // txtten_kh
            // 
            this.txtten_kh.AccessibleName = "ten_kh";
            this.txtten_kh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtten_kh.BackColor = System.Drawing.SystemColors.Window;
            this.txtten_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtten_kh.BrotherFields = "Ten_Tk";
            this.txtten_kh.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtten_kh.CheckNotEmpty = true;
            this.txtten_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtten_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtten_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtten_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtten_kh.LeaveColor = System.Drawing.Color.White;
            this.txtten_kh.Location = new System.Drawing.Point(175, 118);
            this.txtten_kh.Name = "txtten_kh";
            this.txtten_kh.Size = new System.Drawing.Size(419, 20);
            this.txtten_kh.TabIndex = 9;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00041";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(26, 100);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(55, 13);
            this.v6Label2.TabIndex = 7;
            this.v6Label2.Text = "Mã khách";
            // 
            // txtma_kh
            // 
            this.txtma_kh.AccessibleName = "ma_kh";
            this.txtma_kh.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_kh.BrotherFields = "Ten_Kh,dia_chi,ma_so_thue";
            this.txtma_kh.CheckNotEmpty = true;
            this.txtma_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_kh.LeaveColor = System.Drawing.Color.White;
            this.txtma_kh.Location = new System.Drawing.Point(175, 96);
            this.txtma_kh.Name = "txtma_kh";
            this.txtma_kh.Size = new System.Drawing.Size(146, 20);
            this.txtma_kh.TabIndex = 7;
            this.txtma_kh.VVar = "ma_kh";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "XULYL00003";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(26, 34);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(77, 13);
            this.v6Label9.TabIndex = 3;
            this.v6Label9.Text = "Ngày chứng từ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00013";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00077";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(26, 166);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(60, 13);
            this.v6Label3.TabIndex = 15;
            this.v6Label3.Text = "Mã số thuế";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "XULYL00082";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(26, 276);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(128, 13);
            this.v6Label7.TabIndex = 18;
            this.v6Label7.Text = "Tổng tiền nt trên hóa đơn";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00040";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(26, 12);
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
            this.btnThoat.Location = new System.Drawing.Point(114, 423);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 40);
            this.btnThoat.TabIndex = 24;
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
            this.btnNhan.Location = new System.Drawing.Point(26, 423);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 23;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtngay_ct
            // 
            this.txtngay_ct.AccessibleName = "ngay_ct";
            this.txtngay_ct.CustomFormat = "dd/MM/yyyy";
            this.txtngay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtngay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_ct.LeaveColor = System.Drawing.Color.White;
            this.txtngay_ct.Location = new System.Drawing.Point(175, 30);
            this.txtngay_ct.Name = "txtngay_ct";
            this.txtngay_ct.Size = new System.Drawing.Size(146, 20);
            this.txtngay_ct.TabIndex = 1;
            // 
            // txtdia_chi
            // 
            this.txtdia_chi.AccessibleName = "dia_chi";
            this.txtdia_chi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdia_chi.BackColor = System.Drawing.SystemColors.Window;
            this.txtdia_chi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdia_chi.BrotherFields = "";
            this.txtdia_chi.CheckNotEmpty = true;
            this.txtdia_chi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdia_chi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdia_chi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdia_chi.HoverColor = System.Drawing.Color.Yellow;
            this.txtdia_chi.LeaveColor = System.Drawing.Color.White;
            this.txtdia_chi.Location = new System.Drawing.Point(175, 140);
            this.txtdia_chi.Name = "txtdia_chi";
            this.txtdia_chi.Size = new System.Drawing.Size(419, 20);
            this.txtdia_chi.TabIndex = 10;
            // 
            // txtma_so_thue
            // 
            this.txtma_so_thue.AccessibleName = "ma_so_thue";
            this.txtma_so_thue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_so_thue.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_so_thue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_so_thue.BrotherFields = "";
            this.txtma_so_thue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_so_thue.CheckNotEmpty = true;
            this.txtma_so_thue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_so_thue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_so_thue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_so_thue.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_so_thue.LeaveColor = System.Drawing.Color.White;
            this.txtma_so_thue.Location = new System.Drawing.Point(175, 162);
            this.txtma_so_thue.Name = "txtma_so_thue";
            this.txtma_so_thue.Size = new System.Drawing.Size(144, 20);
            this.txtma_so_thue.TabIndex = 11;
            // 
            // txtten_dvcs
            // 
            this.txtten_dvcs.AccessibleName = "ten_dvcs";
            this.txtten_dvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_dvcs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_dvcs.Location = new System.Drawing.Point(335, 12);
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
            this.txtma_dvcs.Location = new System.Drawing.Point(175, 8);
            this.txtma_dvcs.Name = "txtma_dvcs";
            this.txtma_dvcs.Size = new System.Drawing.Size(146, 20);
            this.txtma_dvcs.TabIndex = 0;
            this.txtma_dvcs.VVar = "ma_dvcs";
            // 
            // txtso_ct
            // 
            this.txtso_ct.AccessibleName = "so_ct";
            this.txtso_ct.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_ct.BrotherFields = "Ten_Kh";
            this.txtso_ct.CheckNotEmpty = true;
            this.txtso_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_ct.LeaveColor = System.Drawing.Color.White;
            this.txtso_ct.Location = new System.Drawing.Point(445, 30);
            this.txtso_ct.Name = "txtso_ct";
            this.txtso_ct.Size = new System.Drawing.Size(149, 20);
            this.txtso_ct.TabIndex = 2;
            // 
            // lbT_Tt_NT0
            // 
            this.lbT_Tt_NT0.AccessibleDescription = "XULYL00062";
            this.lbT_Tt_NT0.AccessibleName = "";
            this.lbT_Tt_NT0.AutoSize = true;
            this.lbT_Tt_NT0.Location = new System.Drawing.Point(26, 342);
            this.lbT_Tt_NT0.Name = "lbT_Tt_NT0";
            this.lbT_Tt_NT0.Size = new System.Drawing.Size(55, 13);
            this.lbT_Tt_NT0.TabIndex = 24;
            this.lbT_Tt_NT0.Text = "Tài khoản";
            // 
            // txtt_tien_nt
            // 
            this.txtt_tien_nt.AccessibleName = "t_tien_nt";
            this.txtt_tien_nt.BackColor = System.Drawing.SystemColors.Window;
            this.txtt_tien_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_tien_nt.DecimalPlaces = 0;
            this.txtt_tien_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_tien_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_tien_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_tien_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_tien_nt.LeaveColor = System.Drawing.Color.White;
            this.txtt_tien_nt.Location = new System.Drawing.Point(175, 272);
            this.txtt_tien_nt.Name = "txtt_tien_nt";
            this.txtt_tien_nt.Size = new System.Drawing.Size(146, 20);
            this.txtt_tien_nt.TabIndex = 17;
            this.txtt_tien_nt.Text = "0";
            this.txtt_tien_nt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_tien_nt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtt_tien_nt.V6LostFocus += new V6Controls.ControlEventHandle(this.txtso_luongV6LostFocus);
            // 
            // txtthue_suat
            // 
            this.txtthue_suat.AccessibleName = "thue_suat";
            this.txtthue_suat.BackColor = System.Drawing.SystemColors.Window;
            this.txtthue_suat.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtthue_suat.DecimalPlaces = 0;
            this.txtthue_suat.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtthue_suat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtthue_suat.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtthue_suat.HoverColor = System.Drawing.Color.Yellow;
            this.txtthue_suat.LeaveColor = System.Drawing.Color.White;
            this.txtthue_suat.Location = new System.Drawing.Point(175, 294);
            this.txtthue_suat.Name = "txtthue_suat";
            this.txtthue_suat.Size = new System.Drawing.Size(56, 20);
            this.txtthue_suat.TabIndex = 18;
            this.txtthue_suat.Text = "0";
            this.txtthue_suat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtthue_suat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtthue_suat.V6LostFocus += new V6Controls.ControlEventHandle(this.txtso_luongV6LostFocus);
            // 
            // txtngay_ct0
            // 
            this.txtngay_ct0.AccessibleName = "ngay_ct0";
            this.txtngay_ct0.CustomFormat = "dd/MM/yyyy";
            this.txtngay_ct0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_ct0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtngay_ct0.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_ct0.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_ct0.LeaveColor = System.Drawing.Color.White;
            this.txtngay_ct0.Location = new System.Drawing.Point(175, 52);
            this.txtngay_ct0.Name = "txtngay_ct0";
            this.txtngay_ct0.Size = new System.Drawing.Size(146, 20);
            this.txtngay_ct0.TabIndex = 3;
            // 
            // v6Label10
            // 
            this.v6Label10.AccessibleDescription = "XULYL00071";
            this.v6Label10.AutoSize = true;
            this.v6Label10.Location = new System.Drawing.Point(26, 56);
            this.v6Label10.Name = "v6Label10";
            this.v6Label10.Size = new System.Drawing.Size(86, 13);
            this.v6Label10.TabIndex = 39;
            this.v6Label10.Text = "Ngày chứng từ 0";
            // 
            // v6Label13
            // 
            this.v6Label13.AccessibleDescription = "XULYL00073";
            this.v6Label13.AutoSize = true;
            this.v6Label13.Location = new System.Drawing.Point(332, 57);
            this.v6Label13.Name = "v6Label13";
            this.v6Label13.Size = new System.Drawing.Size(74, 13);
            this.v6Label13.TabIndex = 42;
            this.v6Label13.Text = "Số chứng từ 0";
            // 
            // txtso_ct0
            // 
            this.txtso_ct0.AccessibleName = "so_ct0";
            this.txtso_ct0.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_ct0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_ct0.BrotherFields = "Ten_Kh";
            this.txtso_ct0.CheckNotEmpty = true;
            this.txtso_ct0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_ct0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_ct0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_ct0.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_ct0.LeaveColor = System.Drawing.Color.White;
            this.txtso_ct0.Location = new System.Drawing.Point(445, 52);
            this.txtso_ct0.Name = "txtso_ct0";
            this.txtso_ct0.Size = new System.Drawing.Size(149, 20);
            this.txtso_ct0.TabIndex = 4;
            // 
            // txtso_seri0
            // 
            this.txtso_seri0.AccessibleName = "so_seri0";
            this.txtso_seri0.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_seri0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_seri0.BrotherFields = "Ten_Kh";
            this.txtso_seri0.CheckNotEmpty = true;
            this.txtso_seri0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_seri0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_seri0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_seri0.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_seri0.LeaveColor = System.Drawing.Color.White;
            this.txtso_seri0.Location = new System.Drawing.Point(445, 96);
            this.txtso_seri0.Name = "txtso_seri0";
            this.txtso_seri0.Size = new System.Drawing.Size(149, 20);
            this.txtso_seri0.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "XULYL00075";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Số seri";
            // 
            // txtma_hd
            // 
            this.txtma_hd.AccessibleName = "ma_hd";
            this.txtma_hd.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_hd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_hd.BrotherFields = "Ten_Kh";
            this.txtma_hd.CheckNotEmpty = true;
            this.txtma_hd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_hd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_hd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_hd.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_hd.LeaveColor = System.Drawing.Color.White;
            this.txtma_hd.Location = new System.Drawing.Point(445, 74);
            this.txtma_hd.Name = "txtma_hd";
            this.txtma_hd.Size = new System.Drawing.Size(149, 20);
            this.txtma_hd.TabIndex = 6;
            this.txtma_hd.VVar = "ma_hd";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "XULYL00074";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Mẫu hóa đơn";
            // 
            // txtmau_bc
            // 
            this.txtmau_bc.AccessibleName = "mau_bc";
            this.txtmau_bc.BackColor = System.Drawing.SystemColors.Window;
            this.txtmau_bc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtmau_bc.BrotherFields = "Ten_Kh";
            this.txtmau_bc.CheckNotEmpty = true;
            this.txtmau_bc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtmau_bc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtmau_bc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtmau_bc.HoverColor = System.Drawing.Color.Yellow;
            this.txtmau_bc.LeaveColor = System.Drawing.Color.White;
            this.txtmau_bc.LimitCharacters = "123458";
            this.txtmau_bc.Location = new System.Drawing.Point(175, 74);
            this.txtmau_bc.MaxLength = 1;
            this.txtmau_bc.Name = "txtmau_bc";
            this.txtmau_bc.Size = new System.Drawing.Size(56, 20);
            this.txtmau_bc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "XULYL00072";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Mẫu báo cáo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "1,2,3,4,5,8";
            // 
            // txtma_kho
            // 
            this.txtma_kho.AccessibleName = "ma_kho";
            this.txtma_kho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_kho.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_kho.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_kho.BrotherFields = "ma_kho";
            this.txtma_kho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_kho.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_kho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_kho.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_kho.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_kho.LeaveColor = System.Drawing.Color.White;
            this.txtma_kho.Location = new System.Drawing.Point(175, 184);
            this.txtma_kho.Name = "txtma_kho";
            this.txtma_kho.Size = new System.Drawing.Size(144, 20);
            this.txtma_kho.TabIndex = 12;
            this.txtma_kho.VVar = "ma_kho";
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "XULYL00078";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(26, 188);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(43, 13);
            this.v6Label11.TabIndex = 51;
            this.v6Label11.Text = "Mã kho";
            // 
            // txtma_vv
            // 
            this.txtma_vv.AccessibleName = "ma_vv";
            this.txtma_vv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_vv.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_vv.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_vv.BrotherFields = "";
            this.txtma_vv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_vv.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_vv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_vv.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_vv.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_vv.LeaveColor = System.Drawing.Color.White;
            this.txtma_vv.Location = new System.Drawing.Point(445, 184);
            this.txtma_vv.Name = "txtma_vv";
            this.txtma_vv.Size = new System.Drawing.Size(148, 20);
            this.txtma_vv.TabIndex = 13;
            this.txtma_vv.VVar = "ma_vv";
            // 
            // v6Label14
            // 
            this.v6Label14.AccessibleDescription = "XULYL00027";
            this.v6Label14.AutoSize = true;
            this.v6Label14.Location = new System.Drawing.Point(332, 188);
            this.v6Label14.Name = "v6Label14";
            this.v6Label14.Size = new System.Drawing.Size(60, 13);
            this.v6Label14.TabIndex = 53;
            this.v6Label14.Text = "Mã vụ việc";
            // 
            // txtten_vt
            // 
            this.txtten_vt.AccessibleName = "ten_vt";
            this.txtten_vt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtten_vt.BackColor = System.Drawing.SystemColors.Window;
            this.txtten_vt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtten_vt.BrotherFields = "ten_nt";
            this.txtten_vt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtten_vt.CheckNotEmpty = true;
            this.txtten_vt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtten_vt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtten_vt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtten_vt.HoverColor = System.Drawing.Color.Yellow;
            this.txtten_vt.LeaveColor = System.Drawing.Color.White;
            this.txtten_vt.Location = new System.Drawing.Point(175, 206);
            this.txtten_vt.Name = "txtten_vt";
            this.txtten_vt.Size = new System.Drawing.Size(418, 20);
            this.txtten_vt.TabIndex = 14;
            // 
            // v6Label15
            // 
            this.v6Label15.AccessibleDescription = "XULYL00079";
            this.v6Label15.AutoSize = true;
            this.v6Label15.Location = new System.Drawing.Point(26, 210);
            this.v6Label15.Name = "v6Label15";
            this.v6Label15.Size = new System.Drawing.Size(115, 13);
            this.v6Label15.TabIndex = 55;
            this.v6Label15.Text = "Tên hàng hóa, dịch vụ";
            // 
            // txtgia
            // 
            this.txtgia.AccessibleName = "gia";
            this.txtgia.BackColor = System.Drawing.SystemColors.Window;
            this.txtgia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtgia.DecimalPlaces = 0;
            this.txtgia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtgia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtgia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtgia.HoverColor = System.Drawing.Color.Yellow;
            this.txtgia.LeaveColor = System.Drawing.Color.White;
            this.txtgia.Location = new System.Drawing.Point(175, 250);
            this.txtgia.Name = "txtgia";
            this.txtgia.Size = new System.Drawing.Size(146, 20);
            this.txtgia.TabIndex = 16;
            this.txtgia.Text = "0";
            this.txtgia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtgia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtgia.V6LostFocus += new V6Controls.ControlEventHandle(this.txtso_luongV6LostFocus);
            // 
            // v6Label18
            // 
            this.v6Label18.AccessibleDescription = "XULYL00081";
            this.v6Label18.AutoSize = true;
            this.v6Label18.Location = new System.Drawing.Point(26, 254);
            this.v6Label18.Name = "v6Label18";
            this.v6Label18.Size = new System.Drawing.Size(44, 13);
            this.v6Label18.TabIndex = 57;
            this.v6Label18.Text = "Đơn giá";
            // 
            // v6Label19
            // 
            this.v6Label19.AutoSize = true;
            this.v6Label19.Location = new System.Drawing.Point(235, 298);
            this.v6Label19.Name = "v6Label19";
            this.v6Label19.Size = new System.Drawing.Size(15, 13);
            this.v6Label19.TabIndex = 59;
            this.v6Label19.Text = "%";
            // 
            // txtghi_chu
            // 
            this.txtghi_chu.AccessibleName = "ghi_chu";
            this.txtghi_chu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtghi_chu.BackColor = System.Drawing.SystemColors.Window;
            this.txtghi_chu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtghi_chu.BrotherFields = "";
            this.txtghi_chu.CheckNotEmpty = true;
            this.txtghi_chu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtghi_chu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.HoverColor = System.Drawing.Color.Yellow;
            this.txtghi_chu.LeaveColor = System.Drawing.Color.White;
            this.txtghi_chu.Location = new System.Drawing.Point(175, 382);
            this.txtghi_chu.Name = "txtghi_chu";
            this.txtghi_chu.Size = new System.Drawing.Size(419, 20);
            this.txtghi_chu.TabIndex = 22;
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00043";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(26, 386);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(44, 13);
            this.v6Label12.TabIndex = 60;
            this.v6Label12.Text = "Ghi chú";
            // 
            // txttk_thue_no
            // 
            this.txttk_thue_no.AccessibleName = "tk_thue_no";
            this.txttk_thue_no.BackColor = System.Drawing.SystemColors.Window;
            this.txttk_thue_no.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_thue_no.BrotherFields = "";
            this.txttk_thue_no.CheckNotEmpty = true;
            this.txttk_thue_no.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_thue_no.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttk_thue_no.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_thue_no.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_thue_no.LeaveColor = System.Drawing.Color.White;
            this.txttk_thue_no.Location = new System.Drawing.Point(175, 338);
            this.txttk_thue_no.Name = "txttk_thue_no";
            this.txttk_thue_no.Size = new System.Drawing.Size(146, 20);
            this.txttk_thue_no.TabIndex = 20;
            this.txttk_thue_no.VVar = "tk";
            // 
            // txttk_du
            // 
            this.txttk_du.AccessibleName = "tk_du";
            this.txttk_du.BackColor = System.Drawing.SystemColors.Window;
            this.txttk_du.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_du.BrotherFields = "";
            this.txttk_du.CheckNotEmpty = true;
            this.txttk_du.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_du.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttk_du.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_du.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_du.LeaveColor = System.Drawing.Color.White;
            this.txttk_du.Location = new System.Drawing.Point(175, 360);
            this.txttk_du.Name = "txttk_du";
            this.txttk_du.Size = new System.Drawing.Size(146, 20);
            this.txttk_du.TabIndex = 21;
            this.txttk_du.VVar = "tk";
            // 
            // AGLTHUE30_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txttk_du);
            this.Controls.Add(this.txttk_thue_no);
            this.Controls.Add(this.txtghi_chu);
            this.Controls.Add(this.v6Label12);
            this.Controls.Add(this.v6Label19);
            this.Controls.Add(this.txtgia);
            this.Controls.Add(this.v6Label18);
            this.Controls.Add(this.txtten_vt);
            this.Controls.Add(this.v6Label15);
            this.Controls.Add(this.txtma_vv);
            this.Controls.Add(this.v6Label14);
            this.Controls.Add(this.txtma_kho);
            this.Controls.Add(this.v6Label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtmau_bc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtma_hd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtso_seri0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtso_ct0);
            this.Controls.Add(this.v6Label13);
            this.Controls.Add(this.txtngay_ct0);
            this.Controls.Add(this.v6Label10);
            this.Controls.Add(this.txtthue_suat);
            this.Controls.Add(this.txtt_tien_nt);
            this.Controls.Add(this.txtso_ct);
            this.Controls.Add(this.txtma_dvcs);
            this.Controls.Add(this.txtten_dvcs);
            this.Controls.Add(this.txtma_so_thue);
            this.Controls.Add(this.txtdia_chi);
            this.Controls.Add(this.txtngay_ct);
            this.Controls.Add(this.lbTxtT_CL_NT);
            this.Controls.Add(this.lbTxtT_tt_qd);
            this.Controls.Add(this.txtso_luong);
            this.Controls.Add(this.lbT_Tt_NT0);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtt_thue);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.txtten_kh);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.txtma_kh);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Name = "AGLTHUE30_F4";
            this.Size = new System.Drawing.Size(637, 466);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Timer timerViewReport;
        private V6Controls.V6Label lbTxtT_CL_NT;
        private V6Controls.V6Label lbTxtT_tt_qd;
        private V6Controls.NumberTien txtso_luong;
        private V6Controls.V6Label v6Label8;
        private V6Controls.NumberTien txtt_thue;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6VvarTextBox txtten_kh;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox txtma_kh;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6Label v6Label1;
        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        private V6Controls.V6DateTimePicker txtngay_ct;
        private V6Controls.V6VvarTextBox txtdia_chi;
        private V6Controls.V6VvarTextBox txtma_so_thue;
        private V6Controls.V6LabelTextBox txtten_dvcs;
        private V6Controls.V6VvarTextBox txtma_dvcs;
        private V6Controls.V6VvarTextBox txtso_ct;
        private V6Controls.V6Label lbT_Tt_NT0;
        private V6Controls.NumberTienNt txtt_tien_nt;
        private V6Controls.NumberTienNt txtthue_suat;
        private V6Controls.V6DateTimePicker txtngay_ct0;
        private V6Controls.V6Label v6Label10;
        private V6Controls.V6Label v6Label13;
        private V6Controls.V6VvarTextBox txtso_ct0;
        private V6Controls.V6VvarTextBox txtso_seri0;
        private Label label2;
        private V6Controls.V6VvarTextBox txtma_hd;
        private Label label3;
        private V6Controls.V6VvarTextBox txtmau_bc;
        private Label label4;
        private Label label5;
        private V6Controls.V6VvarTextBox txtma_kho;
        private V6Controls.V6Label v6Label11;
        private V6Controls.V6VvarTextBox txtma_vv;
        private V6Controls.V6Label v6Label14;
        private V6Controls.V6VvarTextBox txtten_vt;
        private V6Controls.V6Label v6Label15;
        private V6Controls.NumberTienNt txtgia;
        private V6Controls.V6Label v6Label18;
        private V6Controls.V6Label v6Label19;
        private V6Controls.V6VvarTextBox txtghi_chu;
        private V6Controls.V6Label v6Label12;
        private V6Controls.V6VvarTextBox txttk_thue_no;
        private V6Controls.V6VvarTextBox txttk_du;
    }
}