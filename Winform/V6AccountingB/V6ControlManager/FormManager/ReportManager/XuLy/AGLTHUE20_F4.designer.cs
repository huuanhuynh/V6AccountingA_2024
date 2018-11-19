using System.Windows.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AGLTHUE20_F4
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
            this.txtten_kh = new V6Controls.V6VvarTextBox();
            this.txtma_hd = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtt_thue = new V6Controls.NumberTienNt();
            this.v6Label8 = new V6Controls.V6Label();
            this.txtt_thue_nt = new V6Controls.NumberTien();
            this.v6Label19 = new V6Controls.V6Label();
            this.txtthue_suat = new V6Controls.NumberTienNt();
            this.v6Label6 = new V6Controls.V6Label();
            this.txtt_tien2 = new V6Controls.NumberTienNt();
            this.txtma_thue = new V6Controls.V6VvarTextBox();
            this.v6Label21 = new V6Controls.V6Label();
            this.v6Label20 = new V6Controls.V6Label();
            this.Txtt_tien_nt2 = new V6Controls.NumberTien();
            this.txtty_gia = new V6Controls.NumberTienNt();
            this.v6Label17 = new V6Controls.V6Label();
            this.txtten_nt = new V6Controls.V6LabelTextBox();
            this.txtma_nt = new V6Controls.V6VvarTextBox();
            this.v6Label16 = new V6Controls.V6Label();
            this.txtma_bp = new V6Controls.V6VvarTextBox();
            this.lbma_bp = new V6Controls.V6Label();
            this.txtngay_lct = new V6Controls.V6DateTimePicker();
            this.lbngay_lct = new V6Controls.V6Label();
            this.txttk_du = new V6Controls.V6VvarTextBox();
            this.txttk_thue_co = new V6Controls.V6VvarTextBox();
            this.txtghi_chu = new V6Controls.V6VvarTextBox();
            this.v6Label12 = new V6Controls.V6Label();
            this.txtten_vt = new V6Controls.V6VvarTextBox();
            this.v6Label15 = new V6Controls.V6Label();
            this.txtma_vv = new V6Controls.V6VvarTextBox();
            this.v6Label14 = new V6Controls.V6Label();
            this.txtma_kho = new V6Controls.V6VvarTextBox();
            this.v6Label11 = new V6Controls.V6Label();
            this.txtso_seri = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtso_ct = new V6Controls.V6VvarTextBox();
            this.txtma_dvcs = new V6Controls.V6VvarTextBox();
            this.txtten_dvcs = new V6Controls.V6LabelTextBox();
            this.txtma_so_thue = new V6Controls.V6VvarTextBox();
            this.txtdia_chi = new V6Controls.V6VvarTextBox();
            this.txtngay_ct = new V6Controls.V6DateTimePicker();
            this.lbTxtT_CL_NT = new V6Controls.V6Label();
            this.lbT_Tt_NT0 = new V6Controls.V6Label();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtma_kh = new V6Controls.V6VvarTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
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
            // txtten_kh
            // 
            this.txtten_kh.AccessibleName = "ten_kh";
            this.txtten_kh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtten_kh.BackColor = System.Drawing.SystemColors.Window;
            this.txtten_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtten_kh.BrotherFields = "";
            this.txtten_kh.CheckNotEmpty = true;
            this.txtten_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtten_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtten_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtten_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtten_kh.LeaveColor = System.Drawing.Color.White;
            this.txtten_kh.Location = new System.Drawing.Point(175, 101);
            this.txtten_kh.Name = "txtten_kh";
            this.txtten_kh.Size = new System.Drawing.Size(419, 20);
            this.txtten_kh.TabIndex = 7;
            // 
            // txtma_hd
            // 
            this.txtma_hd.AccessibleName = "ma_hd";
            this.txtma_hd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_hd.BrotherFields = "";
            this.txtma_hd.CheckNotEmpty = true;
            this.txtma_hd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_hd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_hd.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_hd.LeaveColor = System.Drawing.Color.White;
            this.txtma_hd.Location = new System.Drawing.Point(437, 77);
            this.txtma_hd.Name = "txtma_hd";
            this.txtma_hd.Size = new System.Drawing.Size(156, 20);
            this.txtma_hd.TabIndex = 6;
            this.txtma_hd.VVar = "ma_hd";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "XULYL00074";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Mẫu hóa đơn";
            // 
            // txtt_thue
            // 
            this.txtt_thue.AccessibleName = "t_thue";
            this.txtt_thue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_thue.DecimalPlaces = 0;
            this.txtt_thue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_thue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_thue.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_thue.LeaveColor = System.Drawing.Color.White;
            this.txtt_thue.Location = new System.Drawing.Point(437, 342);
            this.txtt_thue.Name = "txtt_thue";
            this.txtt_thue.Size = new System.Drawing.Size(146, 20);
            this.txtt_thue.TabIndex = 21;
            this.txtt_thue.Text = "0";
            this.txtt_thue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_thue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00089";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(26, 344);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(64, 13);
            this.v6Label8.TabIndex = 79;
            this.v6Label8.Text = "Tiền thuế nt";
            // 
            // txtt_thue_nt
            // 
            this.txtt_thue_nt.AccessibleName = "t_thue_nt";
            this.txtt_thue_nt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtt_thue_nt.BackColor = System.Drawing.SystemColors.Window;
            this.txtt_thue_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_thue_nt.DecimalPlaces = 0;
            this.txtt_thue_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_thue_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_thue_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_thue_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_thue_nt.LeaveColor = System.Drawing.Color.White;
            this.txtt_thue_nt.Location = new System.Drawing.Point(175, 341);
            this.txtt_thue_nt.Name = "txtt_thue_nt";
            this.txtt_thue_nt.Size = new System.Drawing.Size(146, 20);
            this.txtt_thue_nt.TabIndex = 20;
            this.txtt_thue_nt.Text = "0";
            this.txtt_thue_nt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_thue_nt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label19
            // 
            this.v6Label19.AutoSize = true;
            this.v6Label19.Location = new System.Drawing.Point(498, 323);
            this.v6Label19.Name = "v6Label19";
            this.v6Label19.Size = new System.Drawing.Size(15, 13);
            this.v6Label19.TabIndex = 77;
            this.v6Label19.Text = "%";
            // 
            // txtthue_suat
            // 
            this.txtthue_suat.AccessibleName = "thue_suat";
            this.txtthue_suat.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtthue_suat.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtthue_suat.DecimalPlaces = 0;
            this.txtthue_suat.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtthue_suat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtthue_suat.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtthue_suat.HoverColor = System.Drawing.Color.Yellow;
            this.txtthue_suat.LeaveColor = System.Drawing.Color.White;
            this.txtthue_suat.Location = new System.Drawing.Point(437, 319);
            this.txtthue_suat.Name = "txtthue_suat";
            this.txtthue_suat.ReadOnly = true;
            this.txtthue_suat.Size = new System.Drawing.Size(56, 20);
            this.txtthue_suat.TabIndex = 19;
            this.txtthue_suat.Text = "0";
            this.txtthue_suat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtthue_suat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtthue_suat.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00083";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(339, 323);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(55, 13);
            this.v6Label6.TabIndex = 76;
            this.v6Label6.Text = "Thuế suất";
            // 
            // txtt_tien2
            // 
            this.txtt_tien2.AccessibleDescription = "";
            this.txtt_tien2.AccessibleName = "T_TIEN2";
            this.txtt_tien2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtt_tien2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_tien2.DecimalPlaces = 0;
            this.txtt_tien2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_tien2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_tien2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_tien2.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_tien2.LeaveColor = System.Drawing.Color.White;
            this.txtt_tien2.Location = new System.Drawing.Point(437, 295);
            this.txtt_tien2.Name = "txtt_tien2";
            this.txtt_tien2.ReadOnly = true;
            this.txtt_tien2.Size = new System.Drawing.Size(146, 20);
            this.txtt_tien2.TabIndex = 17;
            this.txtt_tien2.Text = "0";
            this.txtt_tien2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtt_tien2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtt_tien2.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // txtma_thue
            // 
            this.txtma_thue.AccessibleName = "MA_THUE";
            this.txtma_thue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_thue.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_thue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_thue.BrotherFields = "thue_suat";
            this.txtma_thue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_thue.CheckNotEmpty = true;
            this.txtma_thue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_thue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_thue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_thue.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_thue.LeaveColor = System.Drawing.Color.White;
            this.txtma_thue.Location = new System.Drawing.Point(175, 317);
            this.txtma_thue.Name = "txtma_thue";
            this.txtma_thue.Size = new System.Drawing.Size(148, 20);
            this.txtma_thue.TabIndex = 18;
            this.txtma_thue.VVar = "ma_thue";
            this.txtma_thue.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // v6Label21
            // 
            this.v6Label21.AccessibleDescription = "XULYL00088";
            this.v6Label21.AutoSize = true;
            this.v6Label21.Location = new System.Drawing.Point(26, 320);
            this.v6Label21.Name = "v6Label21";
            this.v6Label21.Size = new System.Drawing.Size(46, 13);
            this.v6Label21.TabIndex = 73;
            this.v6Label21.Text = "Mã thuế";
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "XULYL00087";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(26, 296);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(64, 13);
            this.v6Label20.TabIndex = 70;
            this.v6Label20.Text = "Tổng tiền nt";
            // 
            // Txtt_tien_nt2
            // 
            this.Txtt_tien_nt2.AccessibleName = "t_tien_nt2";
            this.Txtt_tien_nt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtt_tien_nt2.BackColor = System.Drawing.SystemColors.Window;
            this.Txtt_tien_nt2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtt_tien_nt2.DecimalPlaces = 0;
            this.Txtt_tien_nt2.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtt_tien_nt2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtt_tien_nt2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtt_tien_nt2.HoverColor = System.Drawing.Color.Yellow;
            this.Txtt_tien_nt2.LeaveColor = System.Drawing.Color.White;
            this.Txtt_tien_nt2.Location = new System.Drawing.Point(175, 293);
            this.Txtt_tien_nt2.Name = "Txtt_tien_nt2";
            this.Txtt_tien_nt2.Size = new System.Drawing.Size(149, 20);
            this.Txtt_tien_nt2.TabIndex = 16;
            this.Txtt_tien_nt2.Text = "0";
            this.Txtt_tien_nt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtt_tien_nt2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Txtt_tien_nt2.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // txtty_gia
            // 
            this.txtty_gia.AccessibleName = "ty_gia";
            this.txtty_gia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtty_gia.DecimalPlaces = 0;
            this.txtty_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtty_gia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtty_gia.HoverColor = System.Drawing.Color.Yellow;
            this.txtty_gia.LeaveColor = System.Drawing.Color.White;
            this.txtty_gia.Location = new System.Drawing.Point(437, 271);
            this.txtty_gia.Name = "txtty_gia";
            this.txtty_gia.Size = new System.Drawing.Size(146, 20);
            this.txtty_gia.TabIndex = 15;
            this.txtty_gia.Text = "0";
            this.txtty_gia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtty_gia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtty_gia.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "XULYL00091";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(339, 275);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(36, 13);
            this.v6Label17.TabIndex = 68;
            this.v6Label17.Text = "Tỷ giá";
            // 
            // txtten_nt
            // 
            this.txtten_nt.AccessibleName = "ten_nt";
            this.txtten_nt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_nt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_nt.Location = new System.Drawing.Point(235, 272);
            this.txtten_nt.Name = "txtten_nt";
            this.txtten_nt.ReadOnly = true;
            this.txtten_nt.Size = new System.Drawing.Size(86, 13);
            this.txtten_nt.TabIndex = 67;
            this.txtten_nt.TabStop = false;
            this.txtten_nt.Tag = "readonly";
            // 
            // txtma_nt
            // 
            this.txtma_nt.AccessibleName = "ma_nt";
            this.txtma_nt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_nt.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_nt.BrotherFields = "ten_nt";
            this.txtma_nt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_nt.CheckNotEmpty = true;
            this.txtma_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_nt.LeaveColor = System.Drawing.Color.White;
            this.txtma_nt.Location = new System.Drawing.Point(175, 269);
            this.txtma_nt.Name = "txtma_nt";
            this.txtma_nt.Size = new System.Drawing.Size(56, 20);
            this.txtma_nt.TabIndex = 14;
            this.txtma_nt.VVar = "ma_nt";
            // 
            // v6Label16
            // 
            this.v6Label16.AccessibleDescription = "XULYL00086";
            this.v6Label16.AutoSize = true;
            this.v6Label16.Location = new System.Drawing.Point(26, 272);
            this.v6Label16.Name = "v6Label16";
            this.v6Label16.Size = new System.Drawing.Size(47, 13);
            this.v6Label16.TabIndex = 65;
            this.v6Label16.Text = "Loại tiền";
            // 
            // txtma_bp
            // 
            this.txtma_bp.AccessibleName = "ma_bp";
            this.txtma_bp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_bp.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_bp.BrotherFields = "";
            this.txtma_bp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_bp.CheckNotEmpty = true;
            this.txtma_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_bp.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_bp.LeaveColor = System.Drawing.Color.White;
            this.txtma_bp.Location = new System.Drawing.Point(175, 221);
            this.txtma_bp.Name = "txtma_bp";
            this.txtma_bp.Size = new System.Drawing.Size(144, 20);
            this.txtma_bp.TabIndex = 12;
            this.txtma_bp.VVar = "ma_bp";
            // 
            // lbma_bp
            // 
            this.lbma_bp.AccessibleDescription = "XULYL00085";
            this.lbma_bp.AccessibleName = "";
            this.lbma_bp.AutoSize = true;
            this.lbma_bp.Location = new System.Drawing.Point(26, 224);
            this.lbma_bp.Name = "lbma_bp";
            this.lbma_bp.Size = new System.Drawing.Size(64, 13);
            this.lbma_bp.TabIndex = 64;
            this.lbma_bp.Text = "Mã bộ phận";
            // 
            // txtngay_lct
            // 
            this.txtngay_lct.AccessibleName = "ngay_lct";
            this.txtngay_lct.CustomFormat = "dd/MM/yyyy";
            this.txtngay_lct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_lct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtngay_lct.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_lct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_lct.LeaveColor = System.Drawing.Color.White;
            this.txtngay_lct.Location = new System.Drawing.Point(437, 53);
            this.txtngay_lct.Name = "txtngay_lct";
            this.txtngay_lct.Size = new System.Drawing.Size(156, 20);
            this.txtngay_lct.TabIndex = 4;
            // 
            // lbngay_lct
            // 
            this.lbngay_lct.AccessibleDescription = "XULYL00090";
            this.lbngay_lct.AccessibleName = "";
            this.lbngay_lct.AutoSize = true;
            this.lbngay_lct.Location = new System.Drawing.Point(339, 57);
            this.lbngay_lct.Name = "lbngay_lct";
            this.lbngay_lct.Size = new System.Drawing.Size(94, 13);
            this.lbngay_lct.TabIndex = 62;
            this.lbngay_lct.Text = "Ngày lập chứng từ";
            // 
            // txttk_du
            // 
            this.txttk_du.AccessibleName = "tk_du";
            this.txttk_du.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_du.BrotherFields = "";
            this.txttk_du.CheckNotEmpty = true;
            this.txttk_du.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_du.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_du.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_du.LeaveColor = System.Drawing.Color.White;
            this.txttk_du.Location = new System.Drawing.Point(175, 389);
            this.txttk_du.Name = "txttk_du";
            this.txttk_du.Size = new System.Drawing.Size(146, 20);
            this.txttk_du.TabIndex = 23;
            this.txttk_du.VVar = "tk";
            // 
            // txttk_thue_co
            // 
            this.txttk_thue_co.AccessibleName = "tk_thue_co";
            this.txttk_thue_co.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_thue_co.BrotherFields = "";
            this.txttk_thue_co.CheckNotEmpty = true;
            this.txttk_thue_co.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_thue_co.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_thue_co.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_thue_co.LeaveColor = System.Drawing.Color.White;
            this.txttk_thue_co.Location = new System.Drawing.Point(175, 365);
            this.txttk_thue_co.Name = "txttk_thue_co";
            this.txttk_thue_co.Size = new System.Drawing.Size(146, 20);
            this.txttk_thue_co.TabIndex = 22;
            this.txttk_thue_co.VVar = "tk";
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
            this.txtghi_chu.Location = new System.Drawing.Point(175, 413);
            this.txtghi_chu.Name = "txtghi_chu";
            this.txtghi_chu.Size = new System.Drawing.Size(419, 20);
            this.txtghi_chu.TabIndex = 24;
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00043";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(26, 416);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(44, 13);
            this.v6Label12.TabIndex = 60;
            this.v6Label12.Text = "Ghi chú";
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
            this.txtten_vt.Location = new System.Drawing.Point(175, 245);
            this.txtten_vt.Name = "txtten_vt";
            this.txtten_vt.Size = new System.Drawing.Size(418, 20);
            this.txtten_vt.TabIndex = 13;
            // 
            // v6Label15
            // 
            this.v6Label15.AccessibleDescription = "XULYL00079";
            this.v6Label15.AutoSize = true;
            this.v6Label15.Location = new System.Drawing.Point(26, 248);
            this.v6Label15.Name = "v6Label15";
            this.v6Label15.Size = new System.Drawing.Size(115, 13);
            this.v6Label15.TabIndex = 55;
            this.v6Label15.Text = "Tên hàng hóa, dịch vụ";
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
            this.txtma_vv.Location = new System.Drawing.Point(175, 197);
            this.txtma_vv.Name = "txtma_vv";
            this.txtma_vv.Size = new System.Drawing.Size(144, 20);
            this.txtma_vv.TabIndex = 11;
            this.txtma_vv.VVar = "ma_vv";
            // 
            // v6Label14
            // 
            this.v6Label14.AccessibleDescription = "XULYL00027";
            this.v6Label14.AutoSize = true;
            this.v6Label14.Location = new System.Drawing.Point(26, 200);
            this.v6Label14.Name = "v6Label14";
            this.v6Label14.Size = new System.Drawing.Size(60, 13);
            this.v6Label14.TabIndex = 53;
            this.v6Label14.Text = "Mã vụ việc";
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
            this.txtma_kho.Location = new System.Drawing.Point(175, 173);
            this.txtma_kho.Name = "txtma_kho";
            this.txtma_kho.Size = new System.Drawing.Size(144, 20);
            this.txtma_kho.TabIndex = 10;
            this.txtma_kho.VVar = "ma_kho";
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "XULYL00078";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(26, 176);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(43, 13);
            this.v6Label11.TabIndex = 51;
            this.v6Label11.Text = "Mã kho";
            // 
            // txtso_seri
            // 
            this.txtso_seri.AccessibleName = "so_seri";
            this.txtso_seri.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_seri.BrotherFields = "Ten_Kh";
            this.txtso_seri.CheckNotEmpty = true;
            this.txtso_seri.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_seri.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_seri.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_seri.LeaveColor = System.Drawing.Color.White;
            this.txtso_seri.Location = new System.Drawing.Point(175, 53);
            this.txtso_seri.Name = "txtso_seri";
            this.txtso_seri.Size = new System.Drawing.Size(146, 20);
            this.txtso_seri.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "XULYL00075";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Số seri";
            // 
            // txtso_ct
            // 
            this.txtso_ct.AccessibleName = "so_ct";
            this.txtso_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_ct.BrotherFields = "Ten_Kh";
            this.txtso_ct.CheckNotEmpty = true;
            this.txtso_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_ct.LeaveColor = System.Drawing.Color.White;
            this.txtso_ct.Location = new System.Drawing.Point(437, 29);
            this.txtso_ct.Name = "txtso_ct";
            this.txtso_ct.Size = new System.Drawing.Size(156, 20);
            this.txtso_ct.TabIndex = 2;
            // 
            // txtma_dvcs
            // 
            this.txtma_dvcs.AccessibleName = "ma_dvcs";
            this.txtma_dvcs.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_dvcs.BrotherFields = "ten_dvcs";
            this.txtma_dvcs.CheckNotEmpty = true;
            this.txtma_dvcs.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_dvcs.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_dvcs.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_dvcs.LeaveColor = System.Drawing.Color.White;
            this.txtma_dvcs.Location = new System.Drawing.Point(175, 5);
            this.txtma_dvcs.Name = "txtma_dvcs";
            this.txtma_dvcs.Size = new System.Drawing.Size(146, 20);
            this.txtma_dvcs.TabIndex = 0;
            this.txtma_dvcs.VVar = "ma_dvcs";
            // 
            // txtten_dvcs
            // 
            this.txtten_dvcs.AccessibleName = "ten_dvcs";
            this.txtten_dvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_dvcs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_dvcs.Location = new System.Drawing.Point(342, 8);
            this.txtten_dvcs.Name = "txtten_dvcs";
            this.txtten_dvcs.ReadOnly = true;
            this.txtten_dvcs.Size = new System.Drawing.Size(259, 13);
            this.txtten_dvcs.TabIndex = 2;
            this.txtten_dvcs.TabStop = false;
            this.txtten_dvcs.Tag = "readonly";
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
            this.txtma_so_thue.Location = new System.Drawing.Point(175, 149);
            this.txtma_so_thue.Name = "txtma_so_thue";
            this.txtma_so_thue.Size = new System.Drawing.Size(144, 20);
            this.txtma_so_thue.TabIndex = 9;
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
            this.txtdia_chi.Location = new System.Drawing.Point(175, 125);
            this.txtdia_chi.Name = "txtdia_chi";
            this.txtdia_chi.Size = new System.Drawing.Size(419, 20);
            this.txtdia_chi.TabIndex = 8;
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
            this.txtngay_ct.Location = new System.Drawing.Point(175, 29);
            this.txtngay_ct.Name = "txtngay_ct";
            this.txtngay_ct.Size = new System.Drawing.Size(146, 20);
            this.txtngay_ct.TabIndex = 1;
            // 
            // lbTxtT_CL_NT
            // 
            this.lbTxtT_CL_NT.AccessibleDescription = "XULYL00065";
            this.lbTxtT_CL_NT.AutoSize = true;
            this.lbTxtT_CL_NT.Location = new System.Drawing.Point(26, 392);
            this.lbTxtT_CL_NT.Name = "lbTxtT_CL_NT";
            this.lbTxtT_CL_NT.Size = new System.Drawing.Size(94, 13);
            this.lbTxtT_CL_NT.TabIndex = 28;
            this.lbTxtT_CL_NT.Text = "Tài khoản đối ứng";
            // 
            // lbT_Tt_NT0
            // 
            this.lbT_Tt_NT0.AccessibleDescription = "XULYL00062";
            this.lbT_Tt_NT0.AccessibleName = "";
            this.lbT_Tt_NT0.AutoSize = true;
            this.lbT_Tt_NT0.Location = new System.Drawing.Point(26, 368);
            this.lbT_Tt_NT0.Name = "lbT_Tt_NT0";
            this.lbT_Tt_NT0.Size = new System.Drawing.Size(55, 13);
            this.lbT_Tt_NT0.TabIndex = 24;
            this.lbT_Tt_NT0.Text = "Tài khoản";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00042";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(26, 128);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(40, 13);
            this.v6Label5.TabIndex = 13;
            this.v6Label5.Text = "Địa chỉ";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00076";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(26, 104);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(62, 13);
            this.v6Label4.TabIndex = 10;
            this.v6Label4.Text = "Khách VAT";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00041";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(26, 80);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(55, 13);
            this.v6Label2.TabIndex = 7;
            this.v6Label2.Text = "Mã khách";
            // 
            // txtma_kh
            // 
            this.txtma_kh.AccessibleName = "ma_kh";
            this.txtma_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_kh.BrotherFields = "Ten_Kh,dia_chi,ma_so_thue";
            this.txtma_kh.CheckNotEmpty = true;
            this.txtma_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_kh.LeaveColor = System.Drawing.Color.White;
            this.txtma_kh.Location = new System.Drawing.Point(175, 77);
            this.txtma_kh.Name = "txtma_kh";
            this.txtma_kh.Size = new System.Drawing.Size(146, 20);
            this.txtma_kh.TabIndex = 5;
            this.txtma_kh.VVar = "ma_kh";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "XULYL00003";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(26, 32);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(77, 13);
            this.v6Label9.TabIndex = 3;
            this.v6Label9.Text = "Ngày chứng từ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00013";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Số chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00077";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(26, 152);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(60, 13);
            this.v6Label3.TabIndex = 15;
            this.v6Label3.Text = "Mã số thuế";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00040";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(26, 8);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(55, 13);
            this.v6Label1.TabIndex = 0;
            this.v6Label1.Text = "Mã đơn vị";
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(111, 443);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 26;
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
            this.btnNhan.Location = new System.Drawing.Point(23, 443);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 25;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // AGLTHUE20_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtten_kh);
            this.Controls.Add(this.txtma_hd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtt_thue);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtt_thue_nt);
            this.Controls.Add(this.v6Label19);
            this.Controls.Add(this.txtthue_suat);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.txtt_tien2);
            this.Controls.Add(this.txtma_thue);
            this.Controls.Add(this.v6Label21);
            this.Controls.Add(this.v6Label20);
            this.Controls.Add(this.Txtt_tien_nt2);
            this.Controls.Add(this.txtty_gia);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.txtten_nt);
            this.Controls.Add(this.txtma_nt);
            this.Controls.Add(this.v6Label16);
            this.Controls.Add(this.txtma_bp);
            this.Controls.Add(this.lbma_bp);
            this.Controls.Add(this.txtngay_lct);
            this.Controls.Add(this.lbngay_lct);
            this.Controls.Add(this.txttk_du);
            this.Controls.Add(this.txttk_thue_co);
            this.Controls.Add(this.txtghi_chu);
            this.Controls.Add(this.v6Label12);
            this.Controls.Add(this.txtten_vt);
            this.Controls.Add(this.v6Label15);
            this.Controls.Add(this.txtma_vv);
            this.Controls.Add(this.v6Label14);
            this.Controls.Add(this.txtma_kho);
            this.Controls.Add(this.v6Label11);
            this.Controls.Add(this.txtso_seri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtso_ct);
            this.Controls.Add(this.txtma_dvcs);
            this.Controls.Add(this.txtten_dvcs);
            this.Controls.Add(this.txtma_so_thue);
            this.Controls.Add(this.txtdia_chi);
            this.Controls.Add(this.txtngay_ct);
            this.Controls.Add(this.lbTxtT_CL_NT);
            this.Controls.Add(this.lbT_Tt_NT0);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.txtma_kh);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AGLTHUE20_F4";
            this.Size = new System.Drawing.Size(637, 495);
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
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox txtma_kh;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label1;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        private V6Controls.V6DateTimePicker txtngay_ct;
        private V6Controls.V6VvarTextBox txtdia_chi;
        private V6Controls.V6VvarTextBox txtma_so_thue;
        private V6Controls.V6LabelTextBox txtten_dvcs;
        private V6Controls.V6VvarTextBox txtma_dvcs;
        private V6Controls.V6VvarTextBox txtso_ct;
        private V6Controls.V6Label lbT_Tt_NT0;
        private Label label2;
        private V6Controls.V6VvarTextBox txtma_kho;
        private V6Controls.V6Label v6Label11;
        private V6Controls.V6VvarTextBox txtma_vv;
        private V6Controls.V6Label v6Label14;
        private V6Controls.V6VvarTextBox txtten_vt;
        private V6Controls.V6Label v6Label15;
        private V6Controls.V6VvarTextBox txtghi_chu;
        private V6Controls.V6Label v6Label12;
        private V6Controls.V6VvarTextBox txttk_thue_co;
        private V6Controls.V6VvarTextBox txttk_du;
        private V6Controls.V6DateTimePicker txtngay_lct;
        private V6Controls.V6Label lbngay_lct;
        private V6Controls.V6VvarTextBox txtma_bp;
        private V6Controls.V6Label lbma_bp;
        private V6Controls.V6LabelTextBox txtten_nt;
        private V6Controls.V6VvarTextBox txtma_nt;
        private V6Controls.V6Label v6Label16;
        private V6Controls.NumberTienNt txtty_gia;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6Label v6Label20;
        private V6Controls.NumberTien Txtt_tien_nt2;
        private V6Controls.V6VvarTextBox txtma_thue;
        private V6Controls.V6Label v6Label21;
        private V6Controls.NumberTienNt txtt_tien2;
        private V6Controls.V6Label v6Label19;
        private V6Controls.NumberTienNt txtthue_suat;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label8;
        private V6Controls.NumberTien txtt_thue_nt;
        private V6Controls.NumberTienNt txtt_thue;
        private V6Controls.V6VvarTextBox txtma_hd;
        private Label label3;
        private V6Controls.V6VvarTextBox txtten_kh;
        private V6Controls.V6VvarTextBox txtso_seri;
    }
}