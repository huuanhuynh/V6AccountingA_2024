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
            this.cboMaNt = new V6Controls.V6ComboBox();
            this.txtTenKh = new V6Controls.V6VvarTextBox();
            this.txtma_mauhd = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtt_thue = new V6Controls.NumberTien();
            this.v6Label8 = new V6Controls.V6Label();
            this.txtT_thue_nt = new V6Controls.NumberTienNt();
            this.v6Label19 = new V6Controls.V6Label();
            this.txtthue_suat = new V6Controls.V6NumberTextBox();
            this.v6Label6 = new V6Controls.V6Label();
            this.txtT_tien2 = new V6Controls.NumberTien();
            this.txtma_thue = new V6Controls.V6VvarTextBox();
            this.v6Label21 = new V6Controls.V6Label();
            this.v6Label20 = new V6Controls.V6Label();
            this.txtT_tien_nt2 = new V6Controls.NumberTienNt();
            this.txtTyGia = new V6Controls.V6NumberTextBox();
            this.v6Label17 = new V6Controls.V6Label();
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
            this.txtMaSoThue = new V6Controls.V6VvarTextBox();
            this.txtDiaChi = new V6Controls.V6VvarTextBox();
            this.txtngay_ct = new V6Controls.V6DateTimePicker();
            this.lbTxtT_CL_NT = new V6Controls.V6Label();
            this.lbT_Tt_NT0 = new V6Controls.V6Label();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.lblMaKH = new V6Controls.V6Label();
            this.txtMaKh = new V6Controls.V6VvarTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.lblSoCT = new System.Windows.Forms.Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboMaNt
            // 
            this.cboMaNt.AccessibleName = "ma_nt";
            this.cboMaNt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMaNt.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaNt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaNt.FormattingEnabled = true;
            this.cboMaNt.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboMaNt.Location = new System.Drawing.Point(175, 269);
            this.cboMaNt.Name = "cboMaNt";
            this.cboMaNt.Size = new System.Drawing.Size(98, 21);
            this.cboMaNt.TabIndex = 30;
            this.cboMaNt.TabStop = false;
            this.cboMaNt.SelectedIndexChanged += new System.EventHandler(this.cboMaNt_SelectedIndexChanged);
            // 
            // txtTenKh
            // 
            this.txtTenKh.AccessibleName = "ten_kh";
            this.txtTenKh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenKh.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenKh.CheckNotEmpty = true;
            this.txtTenKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenKh.LeaveColor = System.Drawing.Color.White;
            this.txtTenKh.Location = new System.Drawing.Point(175, 101);
            this.txtTenKh.Name = "txtTenKh";
            this.txtTenKh.Size = new System.Drawing.Size(419, 20);
            this.txtTenKh.TabIndex = 16;
            // 
            // txtma_mauhd
            // 
            this.txtma_mauhd.AccessibleName = "ma_mauhd";
            this.txtma_mauhd.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_mauhd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_mauhd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_mauhd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_mauhd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_mauhd.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_mauhd.LeaveColor = System.Drawing.Color.White;
            this.txtma_mauhd.Location = new System.Drawing.Point(437, 77);
            this.txtma_mauhd.Name = "txtma_mauhd";
            this.txtma_mauhd.Size = new System.Drawing.Size(156, 20);
            this.txtma_mauhd.TabIndex = 14;
            this.txtma_mauhd.VVar = "ma_mauhd";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "XULYL00074";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Mẫu hóa đơn";
            // 
            // txtt_thue
            // 
            this.txtt_thue.AccessibleName = "t_thue";
            this.txtt_thue.BackColor = System.Drawing.SystemColors.Window;
            this.txtt_thue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtt_thue.DecimalPlaces = 0;
            this.txtt_thue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtt_thue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtt_thue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtt_thue.HoverColor = System.Drawing.Color.Yellow;
            this.txtt_thue.LeaveColor = System.Drawing.Color.White;
            this.txtt_thue.Location = new System.Drawing.Point(437, 342);
            this.txtt_thue.Name = "txtt_thue";
            this.txtt_thue.Size = new System.Drawing.Size(146, 20);
            this.txtt_thue.TabIndex = 44;
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
            this.v6Label8.TabIndex = 42;
            this.v6Label8.Text = "Tiền thuế nt";
            // 
            // txtT_thue_nt
            // 
            this.txtT_thue_nt.AccessibleName = "t_thue_nt";
            this.txtT_thue_nt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtT_thue_nt.BackColor = System.Drawing.SystemColors.Window;
            this.txtT_thue_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtT_thue_nt.DecimalPlaces = 2;
            this.txtT_thue_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtT_thue_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtT_thue_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtT_thue_nt.HoverColor = System.Drawing.Color.Yellow;
            this.txtT_thue_nt.LeaveColor = System.Drawing.Color.White;
            this.txtT_thue_nt.Location = new System.Drawing.Point(175, 341);
            this.txtT_thue_nt.Name = "txtT_thue_nt";
            this.txtT_thue_nt.Size = new System.Drawing.Size(146, 20);
            this.txtT_thue_nt.TabIndex = 43;
            this.txtT_thue_nt.Text = "0,00";
            this.txtT_thue_nt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtT_thue_nt.Value = new decimal(new int[] {
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
            this.v6Label19.TabIndex = 41;
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
            this.txtthue_suat.TabIndex = 40;
            this.txtthue_suat.TabStop = false;
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
            this.v6Label6.TabIndex = 39;
            this.v6Label6.Text = "Thuế suất";
            // 
            // txtT_tien2
            // 
            this.txtT_tien2.AccessibleDescription = "";
            this.txtT_tien2.AccessibleName = "T_TIEN2";
            this.txtT_tien2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtT_tien2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtT_tien2.DecimalPlaces = 0;
            this.txtT_tien2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtT_tien2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtT_tien2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtT_tien2.HoverColor = System.Drawing.Color.Yellow;
            this.txtT_tien2.LeaveColor = System.Drawing.Color.White;
            this.txtT_tien2.Location = new System.Drawing.Point(437, 295);
            this.txtT_tien2.Name = "txtT_tien2";
            this.txtT_tien2.ReadOnly = true;
            this.txtT_tien2.Size = new System.Drawing.Size(146, 20);
            this.txtT_tien2.TabIndex = 36;
            this.txtT_tien2.TabStop = false;
            this.txtT_tien2.Text = "0";
            this.txtT_tien2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtT_tien2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtT_tien2.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
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
            this.txtma_thue.TabIndex = 38;
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
            this.v6Label21.TabIndex = 37;
            this.v6Label21.Text = "Mã thuế";
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "XULYL00087";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(26, 296);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(64, 13);
            this.v6Label20.TabIndex = 34;
            this.v6Label20.Text = "Tổng tiền nt";
            // 
            // txtT_tien_nt2
            // 
            this.txtT_tien_nt2.AccessibleName = "t_tien_nt2";
            this.txtT_tien_nt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtT_tien_nt2.BackColor = System.Drawing.SystemColors.Window;
            this.txtT_tien_nt2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtT_tien_nt2.DecimalPlaces = 2;
            this.txtT_tien_nt2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtT_tien_nt2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtT_tien_nt2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtT_tien_nt2.HoverColor = System.Drawing.Color.Yellow;
            this.txtT_tien_nt2.LeaveColor = System.Drawing.Color.White;
            this.txtT_tien_nt2.Location = new System.Drawing.Point(175, 293);
            this.txtT_tien_nt2.Name = "txtT_tien_nt2";
            this.txtT_tien_nt2.Size = new System.Drawing.Size(149, 20);
            this.txtT_tien_nt2.TabIndex = 35;
            this.txtT_tien_nt2.Text = "0,00";
            this.txtT_tien_nt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtT_tien_nt2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtT_tien_nt2.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // txtTyGia
            // 
            this.txtTyGia.AccessibleName = "ty_gia";
            this.txtTyGia.BackColor = System.Drawing.SystemColors.Window;
            this.txtTyGia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTyGia.DecimalPlaces = 2;
            this.txtTyGia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTyGia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.HoverColor = System.Drawing.Color.Yellow;
            this.txtTyGia.LeaveColor = System.Drawing.Color.White;
            this.txtTyGia.Location = new System.Drawing.Point(437, 271);
            this.txtTyGia.Name = "txtTyGia";
            this.txtTyGia.Size = new System.Drawing.Size(146, 20);
            this.txtTyGia.TabIndex = 33;
            this.txtTyGia.Text = "0,00";
            this.txtTyGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTyGia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTyGia.V6LostFocus += new V6Controls.ControlEventHandle(this.Txtt_tygia_V6LostFocus);
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "XULYL00091";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(339, 275);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(36, 13);
            this.v6Label17.TabIndex = 32;
            this.v6Label17.Text = "Tỷ giá";
            // 
            // v6Label16
            // 
            this.v6Label16.AccessibleDescription = "XULYL00086";
            this.v6Label16.AutoSize = true;
            this.v6Label16.Location = new System.Drawing.Point(26, 272);
            this.v6Label16.Name = "v6Label16";
            this.v6Label16.Size = new System.Drawing.Size(47, 13);
            this.v6Label16.TabIndex = 29;
            this.v6Label16.Text = "Loại tiền";
            // 
            // txtma_bp
            // 
            this.txtma_bp.AccessibleName = "ma_bp";
            this.txtma_bp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_bp.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_bp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_bp.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_bp.LeaveColor = System.Drawing.Color.White;
            this.txtma_bp.Location = new System.Drawing.Point(175, 221);
            this.txtma_bp.Name = "txtma_bp";
            this.txtma_bp.Size = new System.Drawing.Size(144, 20);
            this.txtma_bp.TabIndex = 26;
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
            this.lbma_bp.TabIndex = 25;
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
            this.txtngay_lct.TabIndex = 10;
            // 
            // lbngay_lct
            // 
            this.lbngay_lct.AccessibleDescription = "XULYL00090";
            this.lbngay_lct.AccessibleName = "";
            this.lbngay_lct.AutoSize = true;
            this.lbngay_lct.Location = new System.Drawing.Point(339, 57);
            this.lbngay_lct.Name = "lbngay_lct";
            this.lbngay_lct.Size = new System.Drawing.Size(94, 13);
            this.lbngay_lct.TabIndex = 9;
            this.lbngay_lct.Text = "Ngày lập chứng từ";
            // 
            // txttk_du
            // 
            this.txttk_du.AccessibleName = "tk_du";
            this.txttk_du.BackColor = System.Drawing.SystemColors.Window;
            this.txttk_du.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_du.CheckNotEmpty = true;
            this.txttk_du.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_du.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttk_du.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_du.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_du.LeaveColor = System.Drawing.Color.White;
            this.txttk_du.Location = new System.Drawing.Point(175, 389);
            this.txttk_du.Name = "txttk_du";
            this.txttk_du.Size = new System.Drawing.Size(146, 20);
            this.txttk_du.TabIndex = 48;
            this.txttk_du.VVar = "tk";
            // 
            // txttk_thue_co
            // 
            this.txttk_thue_co.AccessibleName = "tk_thue_co";
            this.txttk_thue_co.BackColor = System.Drawing.SystemColors.Window;
            this.txttk_thue_co.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttk_thue_co.CheckNotEmpty = true;
            this.txttk_thue_co.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttk_thue_co.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttk_thue_co.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttk_thue_co.HoverColor = System.Drawing.Color.Yellow;
            this.txttk_thue_co.LeaveColor = System.Drawing.Color.White;
            this.txttk_thue_co.Location = new System.Drawing.Point(175, 365);
            this.txttk_thue_co.Name = "txttk_thue_co";
            this.txttk_thue_co.Size = new System.Drawing.Size(146, 20);
            this.txttk_thue_co.TabIndex = 46;
            this.txttk_thue_co.VVar = "tk";
            // 
            // txtghi_chu
            // 
            this.txtghi_chu.AccessibleName = "ghi_chu";
            this.txtghi_chu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtghi_chu.BackColor = System.Drawing.SystemColors.Window;
            this.txtghi_chu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtghi_chu.CheckNotEmpty = true;
            this.txtghi_chu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtghi_chu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.HoverColor = System.Drawing.Color.Yellow;
            this.txtghi_chu.LeaveColor = System.Drawing.Color.White;
            this.txtghi_chu.Location = new System.Drawing.Point(175, 413);
            this.txtghi_chu.Name = "txtghi_chu";
            this.txtghi_chu.Size = new System.Drawing.Size(419, 20);
            this.txtghi_chu.TabIndex = 50;
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00043";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(26, 416);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(44, 13);
            this.v6Label12.TabIndex = 49;
            this.v6Label12.Text = "Ghi chú";
            // 
            // txtten_vt
            // 
            this.txtten_vt.AccessibleName = "ten_vt";
            this.txtten_vt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtten_vt.BackColor = System.Drawing.SystemColors.Window;
            this.txtten_vt.BackColorDisabled = System.Drawing.SystemColors.Control;
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
            this.txtten_vt.TabIndex = 28;
            // 
            // v6Label15
            // 
            this.v6Label15.AccessibleDescription = "XULYL00079";
            this.v6Label15.AutoSize = true;
            this.v6Label15.Location = new System.Drawing.Point(26, 248);
            this.v6Label15.Name = "v6Label15";
            this.v6Label15.Size = new System.Drawing.Size(115, 13);
            this.v6Label15.TabIndex = 27;
            this.v6Label15.Text = "Tên hàng hóa, dịch vụ";
            // 
            // txtma_vv
            // 
            this.txtma_vv.AccessibleName = "ma_vv";
            this.txtma_vv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_vv.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_vv.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_vv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_vv.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_vv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_vv.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_vv.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_vv.LeaveColor = System.Drawing.Color.White;
            this.txtma_vv.Location = new System.Drawing.Point(175, 197);
            this.txtma_vv.Name = "txtma_vv";
            this.txtma_vv.Size = new System.Drawing.Size(144, 20);
            this.txtma_vv.TabIndex = 24;
            this.txtma_vv.VVar = "ma_vv";
            // 
            // v6Label14
            // 
            this.v6Label14.AccessibleDescription = "XULYL00027";
            this.v6Label14.AutoSize = true;
            this.v6Label14.Location = new System.Drawing.Point(26, 200);
            this.v6Label14.Name = "v6Label14";
            this.v6Label14.Size = new System.Drawing.Size(60, 13);
            this.v6Label14.TabIndex = 23;
            this.v6Label14.Text = "Mã vụ việc";
            // 
            // txtma_kho
            // 
            this.txtma_kho.AccessibleName = "ma_kho";
            this.txtma_kho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtma_kho.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_kho.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_kho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtma_kho.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_kho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_kho.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_kho.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_kho.LeaveColor = System.Drawing.Color.White;
            this.txtma_kho.Location = new System.Drawing.Point(175, 173);
            this.txtma_kho.Name = "txtma_kho";
            this.txtma_kho.Size = new System.Drawing.Size(144, 20);
            this.txtma_kho.TabIndex = 22;
            this.txtma_kho.VVar = "ma_kho";
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "XULYL00078";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(26, 176);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(43, 13);
            this.v6Label11.TabIndex = 21;
            this.v6Label11.Text = "Mã kho";
            // 
            // txtso_seri
            // 
            this.txtso_seri.AccessibleName = "so_seri";
            this.txtso_seri.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_seri.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_seri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtso_seri.CheckNotEmpty = true;
            this.txtso_seri.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_seri.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_seri.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_seri.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_seri.LeaveColor = System.Drawing.Color.White;
            this.txtso_seri.Location = new System.Drawing.Point(175, 53);
            this.txtso_seri.Name = "txtso_seri";
            this.txtso_seri.Size = new System.Drawing.Size(146, 20);
            this.txtso_seri.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "XULYL00075";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Số seri";
            // 
            // txtso_ct
            // 
            this.txtso_ct.AccessibleName = "so_ct";
            this.txtso_ct.BackColor = System.Drawing.SystemColors.Window;
            this.txtso_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtso_ct.CheckNotEmpty = true;
            this.txtso_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtso_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtso_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtso_ct.LeaveColor = System.Drawing.Color.White;
            this.txtso_ct.Location = new System.Drawing.Point(437, 29);
            this.txtso_ct.Name = "txtso_ct";
            this.txtso_ct.Size = new System.Drawing.Size(156, 20);
            this.txtso_ct.TabIndex = 6;
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
            this.txtma_dvcs.Location = new System.Drawing.Point(175, 5);
            this.txtma_dvcs.Name = "txtma_dvcs";
            this.txtma_dvcs.Size = new System.Drawing.Size(146, 20);
            this.txtma_dvcs.TabIndex = 1;
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
            // txtMaSoThue
            // 
            this.txtMaSoThue.AccessibleName = "ma_so_thue";
            this.txtMaSoThue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSoThue.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaSoThue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaSoThue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaSoThue.CheckNotEmpty = true;
            this.txtMaSoThue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSoThue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSoThue.LeaveColor = System.Drawing.Color.White;
            this.txtMaSoThue.Location = new System.Drawing.Point(175, 149);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(144, 20);
            this.txtMaSoThue.TabIndex = 20;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.AccessibleName = "dia_chi";
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiaChi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDiaChi.CheckNotEmpty = true;
            this.txtDiaChi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDiaChi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.HoverColor = System.Drawing.Color.Yellow;
            this.txtDiaChi.LeaveColor = System.Drawing.Color.White;
            this.txtDiaChi.Location = new System.Drawing.Point(175, 125);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(419, 20);
            this.txtDiaChi.TabIndex = 18;
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
            this.txtngay_ct.TabIndex = 4;
            // 
            // lbTxtT_CL_NT
            // 
            this.lbTxtT_CL_NT.AccessibleDescription = "XULYL00065";
            this.lbTxtT_CL_NT.AutoSize = true;
            this.lbTxtT_CL_NT.Location = new System.Drawing.Point(26, 392);
            this.lbTxtT_CL_NT.Name = "lbTxtT_CL_NT";
            this.lbTxtT_CL_NT.Size = new System.Drawing.Size(94, 13);
            this.lbTxtT_CL_NT.TabIndex = 47;
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
            this.lbT_Tt_NT0.TabIndex = 45;
            this.lbT_Tt_NT0.Text = "Tài khoản";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00042";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(26, 128);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(40, 13);
            this.v6Label5.TabIndex = 17;
            this.v6Label5.Text = "Địa chỉ";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00076";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(26, 104);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(62, 13);
            this.v6Label4.TabIndex = 15;
            this.v6Label4.Text = "Khách VAT";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AccessibleDescription = "XULYL00041";
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(26, 80);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(55, 13);
            this.lblMaKH.TabIndex = 11;
            this.lblMaKH.Text = "Mã khách";
            // 
            // txtMaKh
            // 
            this.txtMaKh.AccessibleName = "ma_kh";
            this.txtMaKh.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKh.CheckNotEmpty = true;
            this.txtMaKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKh.LeaveColor = System.Drawing.Color.White;
            this.txtMaKh.Location = new System.Drawing.Point(175, 77);
            this.txtMaKh.Name = "txtMaKh";
            this.txtMaKh.Size = new System.Drawing.Size(146, 20);
            this.txtMaKh.TabIndex = 12;
            this.txtMaKh.VVar = "ma_kh";
            this.txtMaKh.V6LostFocus += new V6Controls.ControlEventHandle(this.txtma_kh_V6LostFocus);
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
            // lblSoCT
            // 
            this.lblSoCT.AccessibleDescription = "XULYL00013";
            this.lblSoCT.AutoSize = true;
            this.lblSoCT.Location = new System.Drawing.Point(339, 33);
            this.lblSoCT.Name = "lblSoCT";
            this.lblSoCT.Size = new System.Drawing.Size(65, 13);
            this.lblSoCT.TabIndex = 5;
            this.lblSoCT.Text = "Số chứng từ";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00077";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(26, 152);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(60, 13);
            this.v6Label3.TabIndex = 19;
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
            this.btnHuy.TabIndex = 52;
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
            this.btnNhan.TabIndex = 51;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // AGLTHUE20_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboMaNt);
            this.Controls.Add(this.txtTenKh);
            this.Controls.Add(this.txtma_mauhd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtt_thue);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtT_thue_nt);
            this.Controls.Add(this.v6Label19);
            this.Controls.Add(this.txtthue_suat);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.txtT_tien2);
            this.Controls.Add(this.txtma_thue);
            this.Controls.Add(this.v6Label21);
            this.Controls.Add(this.v6Label20);
            this.Controls.Add(this.txtT_tien_nt2);
            this.Controls.Add(this.txtTyGia);
            this.Controls.Add(this.v6Label17);
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
            this.Controls.Add(this.txtMaSoThue);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtngay_ct);
            this.Controls.Add(this.lbTxtT_CL_NT);
            this.Controls.Add(this.lbT_Tt_NT0);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.lblMaKH);
            this.Controls.Add(this.txtMaKh);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.lblSoCT);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AGLTHUE20_F4";
            this.Size = new System.Drawing.Size(637, 495);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label lbTxtT_CL_NT;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6Label lblMaKH;
        private V6Controls.V6VvarTextBox txtMaKh;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label lblSoCT;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label1;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        private V6Controls.V6DateTimePicker txtngay_ct;
        private V6Controls.V6VvarTextBox txtDiaChi;
        private V6Controls.V6VvarTextBox txtMaSoThue;
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
        private V6Controls.V6Label v6Label16;
        private V6Controls.V6NumberTextBox txtTyGia;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6Label v6Label20;
        private V6Controls.NumberTienNt txtT_tien_nt2;
        private V6Controls.V6VvarTextBox txtma_thue;
        private V6Controls.V6Label v6Label21;
        private V6Controls.NumberTien txtT_tien2;
        private V6Controls.V6Label v6Label19;
        private V6Controls.V6NumberTextBox txtthue_suat;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label8;
        private V6Controls.NumberTienNt txtT_thue_nt;
        private V6Controls.NumberTien txtt_thue;
        private V6Controls.V6VvarTextBox txtma_mauhd;
        private Label label3;
        private V6Controls.V6VvarTextBox txtTenKh;
        private V6Controls.V6VvarTextBox txtso_seri;
        private V6Controls.V6ComboBox cboMaNt;
    }
}