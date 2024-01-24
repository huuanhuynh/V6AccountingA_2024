namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AAPPR_SOA1_F4
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtMa_sonb = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.TxtSo_ct = new V6Controls.V6ColorTextBox();
            this.dateNgayLCT = new V6Controls.V6DateTimePicker();
            this.dateNgayCT = new V6Controls.V6DateTimePicker();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.txtMaDVCS = new V6Controls.V6VvarTextBox();
            this.v6ColorTextBox3 = new V6Controls.V6ColorTextBox();
            this.lblMaDVCS = new V6Controls.V6Label();
            this.txtMaKh = new V6Controls.V6VvarTextBox();
            this.v6ColorTextBox9 = new V6Controls.V6ColorTextBox();
            this.v6Label12 = new V6Controls.V6Label();
            this.txtMaSoThue = new V6Controls.V6ColorTextBox();
            this.txtTenKh = new V6Controls.V6ColorTextBox();
            this.txtDiaChi = new V6Controls.V6ColorTextBox();
            this.v6Label11 = new V6Controls.V6Label();
            this.lblMaKH = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.txtGhiChu01 = new V6Controls.V6ColorTextBox();
            this.v6Label6 = new V6Controls.V6Label();
            this.txtGhiChu02 = new V6Controls.V6ColorTextBox();
            this.TxtMa_nvien = new V6Controls.V6VvarTextBox();
            this.TxtMa_bp = new V6Controls.V6VvarTextBox();
            this.lblBPNV = new V6Controls.V6Label();
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
            this.btnHuy.Location = new System.Drawing.Point(94, 213);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 27;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 213);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 26;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtMa_sonb
            // 
            this.txtMa_sonb.AccessibleName = "ma_sonb";
            this.txtMa_sonb.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMa_sonb.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_sonb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMa_sonb.BrotherFields = "";
            this.txtMa_sonb.CheckNotEmpty = true;
            this.txtMa_sonb.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_sonb.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_sonb.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_sonb.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_sonb.LeaveColor = System.Drawing.Color.White;
            this.txtMa_sonb.Location = new System.Drawing.Point(88, 7);
            this.txtMa_sonb.Name = "txtMa_sonb";
            this.txtMa_sonb.ReadOnly = true;
            this.txtMa_sonb.Size = new System.Drawing.Size(100, 20);
            this.txtMa_sonb.TabIndex = 1;
            this.txtMa_sonb.TabStop = false;
            this.txtMa_sonb.VVar = "ma_sonb";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00039";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(-1, 32);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(49, 13);
            this.v6Label2.TabIndex = 4;
            this.v6Label2.Text = "Số phiếu";
            // 
            // TxtSo_ct
            // 
            this.TxtSo_ct.AccessibleName = "so_ct";
            this.TxtSo_ct.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TxtSo_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtSo_ct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSo_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtSo_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtSo_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtSo_ct.HoverColor = System.Drawing.Color.Yellow;
            this.TxtSo_ct.LeaveColor = System.Drawing.Color.White;
            this.TxtSo_ct.Location = new System.Drawing.Point(88, 29);
            this.TxtSo_ct.Name = "TxtSo_ct";
            this.TxtSo_ct.ReadOnly = true;
            this.TxtSo_ct.Size = new System.Drawing.Size(100, 20);
            this.TxtSo_ct.TabIndex = 5;
            this.TxtSo_ct.TabStop = false;
            // 
            // dateNgayLCT
            // 
            this.dateNgayLCT.AccessibleName = "ngay_lct";
            this.dateNgayLCT.CustomFormat = "dd/MM/yyyy";
            this.dateNgayLCT.Enabled = false;
            this.dateNgayLCT.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayLCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayLCT.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayLCT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayLCT.LeaveColor = System.Drawing.Color.White;
            this.dateNgayLCT.Location = new System.Drawing.Point(299, 29);
            this.dateNgayLCT.Name = "dateNgayLCT";
            this.dateNgayLCT.Size = new System.Drawing.Size(96, 20);
            this.dateNgayLCT.TabIndex = 7;
            // 
            // dateNgayCT
            // 
            this.dateNgayCT.AccessibleName = "ngay_ct";
            this.dateNgayCT.CustomFormat = "dd/MM/yyyy";
            this.dateNgayCT.Enabled = false;
            this.dateNgayCT.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayCT.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayCT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayCT.LeaveColor = System.Drawing.Color.White;
            this.dateNgayCT.Location = new System.Drawing.Point(299, 7);
            this.dateNgayCT.Name = "dateNgayCT";
            this.dateNgayCT.Size = new System.Drawing.Size(96, 20);
            this.dateNgayCT.TabIndex = 3;
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00002";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(210, 31);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(92, 13);
            this.v6Label5.TabIndex = 6;
            this.v6Label5.Text = "Ngày lập hóa đơn";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00001";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(210, 10);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(83, 13);
            this.v6Label3.TabIndex = 2;
            this.v6Label3.Text = "Ngày hạch toán";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00038";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(-1, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(52, 13);
            this.v6Label1.TabIndex = 0;
            this.v6Label1.Text = "Số nội bộ";
            // 
            // txtMaDVCS
            // 
            this.txtMaDVCS.AccessibleName = "MA_DVCS";
            this.txtMaDVCS.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaDVCS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaDVCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaDVCS.BrotherFields = "ten_dvcs";
            this.txtMaDVCS.CheckNotEmpty = true;
            this.txtMaDVCS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaDVCS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaDVCS.LeaveColor = System.Drawing.Color.White;
            this.txtMaDVCS.Location = new System.Drawing.Point(88, 51);
            this.txtMaDVCS.Name = "txtMaDVCS";
            this.txtMaDVCS.ReadOnly = true;
            this.txtMaDVCS.Size = new System.Drawing.Size(100, 20);
            this.txtMaDVCS.TabIndex = 9;
            this.txtMaDVCS.TabStop = false;
            this.txtMaDVCS.VVar = "ma_dvcs";
            // 
            // v6ColorTextBox3
            // 
            this.v6ColorTextBox3.AccessibleName = "ten_dvcs";
            this.v6ColorTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox3.BackColor = System.Drawing.Color.AntiqueWhite;
            this.v6ColorTextBox3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.v6ColorTextBox3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox3.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox3.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox3.Location = new System.Drawing.Point(195, 51);
            this.v6ColorTextBox3.Name = "v6ColorTextBox3";
            this.v6ColorTextBox3.ReadOnly = true;
            this.v6ColorTextBox3.Size = new System.Drawing.Size(361, 20);
            this.v6ColorTextBox3.TabIndex = 10;
            this.v6ColorTextBox3.TabStop = false;
            this.v6ColorTextBox3.Tag = "disable";
            // 
            // lblMaDVCS
            // 
            this.lblMaDVCS.AccessibleDescription = "XULYL00040";
            this.lblMaDVCS.AutoSize = true;
            this.lblMaDVCS.Location = new System.Drawing.Point(-1, 54);
            this.lblMaDVCS.Name = "lblMaDVCS";
            this.lblMaDVCS.Size = new System.Drawing.Size(58, 13);
            this.lblMaDVCS.TabIndex = 8;
            this.lblMaDVCS.Text = "Mã đơn vị ";
            // 
            // txtMaKh
            // 
            this.txtMaKh.AccessibleName = "ma_kh";
            this.txtMaKh.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaKh.BrotherFields = "";
            this.txtMaKh.CheckNotEmpty = true;
            this.txtMaKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKh.LeaveColor = System.Drawing.Color.White;
            this.txtMaKh.Location = new System.Drawing.Point(88, 73);
            this.txtMaKh.Name = "txtMaKh";
            this.txtMaKh.ReadOnly = true;
            this.txtMaKh.Size = new System.Drawing.Size(100, 20);
            this.txtMaKh.TabIndex = 12;
            this.txtMaKh.TabStop = false;
            this.txtMaKh.VVar = "ma_kh";
            // 
            // v6ColorTextBox9
            // 
            this.v6ColorTextBox9.AccessibleName = "dien_giai";
            this.v6ColorTextBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox9.BackColor = System.Drawing.Color.AntiqueWhite;
            this.v6ColorTextBox9.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.v6ColorTextBox9.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox9.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox9.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox9.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox9.Location = new System.Drawing.Point(88, 117);
            this.v6ColorTextBox9.Name = "v6ColorTextBox9";
            this.v6ColorTextBox9.ReadOnly = true;
            this.v6ColorTextBox9.Size = new System.Drawing.Size(467, 20);
            this.v6ColorTextBox9.TabIndex = 18;
            this.v6ColorTextBox9.TabStop = false;
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "XULYL00019";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(-1, 120);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(48, 13);
            this.v6Label12.TabIndex = 17;
            this.v6Label12.Text = "Diễn giải";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.AccessibleName = "ma_so_thue";
            this.txtMaSoThue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSoThue.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaSoThue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaSoThue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaSoThue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSoThue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSoThue.LeaveColor = System.Drawing.Color.White;
            this.txtMaSoThue.Location = new System.Drawing.Point(456, 95);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.ReadOnly = true;
            this.txtMaSoThue.Size = new System.Drawing.Size(100, 20);
            this.txtMaSoThue.TabIndex = 16;
            this.txtMaSoThue.TabStop = false;
            this.txtMaSoThue.Tag = "";
            // 
            // txtTenKh
            // 
            this.txtTenKh.AccessibleName = "ten_kh";
            this.txtTenKh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenKh.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTenKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenKh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenKh.LeaveColor = System.Drawing.Color.White;
            this.txtTenKh.Location = new System.Drawing.Point(195, 73);
            this.txtTenKh.Name = "txtTenKh";
            this.txtTenKh.ReadOnly = true;
            this.txtTenKh.Size = new System.Drawing.Size(361, 20);
            this.txtTenKh.TabIndex = 13;
            this.txtTenKh.TabStop = false;
            this.txtTenKh.Tag = "";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.AccessibleName = "dia_chi";
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtDiaChi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiaChi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDiaChi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.HoverColor = System.Drawing.Color.Yellow;
            this.txtDiaChi.LeaveColor = System.Drawing.Color.White;
            this.txtDiaChi.Location = new System.Drawing.Point(88, 95);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(361, 20);
            this.txtDiaChi.TabIndex = 15;
            this.txtDiaChi.TabStop = false;
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "XULYL00042";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(-1, 98);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(40, 13);
            this.v6Label11.TabIndex = 14;
            this.v6Label11.Text = "Địa chỉ";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AccessibleDescription = "XULYL00041";
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(-1, 76);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(82, 13);
            this.lblMaKH.TabIndex = 11;
            this.lblMaKH.Text = "Mã khách hàng";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00044";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(-1, 142);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(53, 13);
            this.v6Label4.TabIndex = 19;
            this.v6Label4.Text = "Ghi chú 1";
            // 
            // txtGhiChu01
            // 
            this.txtGhiChu01.AccessibleName = "GHI_CHU01";
            this.txtGhiChu01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu01.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtGhiChu01.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGhiChu01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGhiChu01.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGhiChu01.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGhiChu01.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGhiChu01.HoverColor = System.Drawing.Color.Yellow;
            this.txtGhiChu01.LeaveColor = System.Drawing.Color.White;
            this.txtGhiChu01.Location = new System.Drawing.Point(88, 139);
            this.txtGhiChu01.Name = "txtGhiChu01";
            this.txtGhiChu01.ReadOnly = true;
            this.txtGhiChu01.Size = new System.Drawing.Size(467, 20);
            this.txtGhiChu01.TabIndex = 20;
            this.txtGhiChu01.TabStop = false;
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00045";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(-1, 164);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(53, 13);
            this.v6Label6.TabIndex = 21;
            this.v6Label6.Text = "Ghi chú 2";
            // 
            // txtGhiChu02
            // 
            this.txtGhiChu02.AccessibleName = "GHI_CHU02";
            this.txtGhiChu02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu02.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtGhiChu02.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGhiChu02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGhiChu02.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGhiChu02.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGhiChu02.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGhiChu02.HoverColor = System.Drawing.Color.Yellow;
            this.txtGhiChu02.LeaveColor = System.Drawing.Color.White;
            this.txtGhiChu02.Location = new System.Drawing.Point(88, 161);
            this.txtGhiChu02.Name = "txtGhiChu02";
            this.txtGhiChu02.ReadOnly = true;
            this.txtGhiChu02.Size = new System.Drawing.Size(467, 20);
            this.txtGhiChu02.TabIndex = 22;
            this.txtGhiChu02.TabStop = false;
            // 
            // TxtMa_nvien
            // 
            this.TxtMa_nvien.AccessibleName = "MA_NVIEN";
            this.TxtMa_nvien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMa_nvien.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TxtMa_nvien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_nvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMa_nvien.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_nvien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nvien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nvien.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_nvien.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_nvien.Location = new System.Drawing.Point(206, 183);
            this.TxtMa_nvien.Name = "TxtMa_nvien";
            this.TxtMa_nvien.ReadOnly = true;
            this.TxtMa_nvien.Size = new System.Drawing.Size(91, 20);
            this.TxtMa_nvien.TabIndex = 25;
            this.TxtMa_nvien.TabStop = false;
            this.TxtMa_nvien.VVar = "ma_nvien";
            // 
            // TxtMa_bp
            // 
            this.TxtMa_bp.AccessibleName = "MA_BP";
            this.TxtMa_bp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMa_bp.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TxtMa_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_bp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMa_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_bp.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_bp.Location = new System.Drawing.Point(88, 183);
            this.TxtMa_bp.Name = "TxtMa_bp";
            this.TxtMa_bp.ReadOnly = true;
            this.TxtMa_bp.Size = new System.Drawing.Size(97, 20);
            this.TxtMa_bp.TabIndex = 24;
            this.TxtMa_bp.TabStop = false;
            this.TxtMa_bp.VVar = "MA_BP";
            // 
            // lblBPNV
            // 
            this.lblBPNV.AccessibleDescription = "XULYL00046";
            this.lblBPNV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBPNV.AutoSize = true;
            this.lblBPNV.Location = new System.Drawing.Point(-1, 186);
            this.lblBPNV.Name = "lblBPNV";
            this.lblBPNV.Size = new System.Drawing.Size(41, 13);
            this.lblBPNV.TabIndex = 23;
            this.lblBPNV.Text = "BP/NV";
            // 
            // AAPPR_SOA1_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxtMa_nvien);
            this.Controls.Add(this.TxtMa_bp);
            this.Controls.Add(this.lblBPNV);
            this.Controls.Add(this.txtMaDVCS);
            this.Controls.Add(this.v6ColorTextBox3);
            this.Controls.Add(this.lblMaDVCS);
            this.Controls.Add(this.txtMaKh);
            this.Controls.Add(this.txtGhiChu02);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.txtGhiChu01);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6ColorTextBox9);
            this.Controls.Add(this.v6Label12);
            this.Controls.Add(this.txtMaSoThue);
            this.Controls.Add(this.txtTenKh);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.v6Label11);
            this.Controls.Add(this.lblMaKH);
            this.Controls.Add(this.txtMa_sonb);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.TxtSo_ct);
            this.Controls.Add(this.dateNgayLCT);
            this.Controls.Add(this.dateNgayCT);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AAPPR_SOA1_F4";
            this.Size = new System.Drawing.Size(559, 256);
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6VvarTextBox txtMa_sonb;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6ColorTextBox TxtSo_ct;
        private V6Controls.V6DateTimePicker dateNgayLCT;
        private V6Controls.V6DateTimePicker dateNgayCT;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox txtMaDVCS;
        private V6Controls.V6ColorTextBox v6ColorTextBox3;
        private V6Controls.V6Label lblMaDVCS;
        private V6Controls.V6VvarTextBox txtMaKh;
        private V6Controls.V6ColorTextBox v6ColorTextBox9;
        private V6Controls.V6Label v6Label12;
        private V6Controls.V6ColorTextBox txtMaSoThue;
        private V6Controls.V6ColorTextBox txtTenKh;
        private V6Controls.V6ColorTextBox txtDiaChi;
        private V6Controls.V6Label v6Label11;
        private V6Controls.V6Label lblMaKH;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6ColorTextBox txtGhiChu01;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6ColorTextBox txtGhiChu02;
        private V6Controls.V6VvarTextBox TxtMa_nvien;
        private V6Controls.V6VvarTextBox TxtMa_bp;
        private V6Controls.V6Label lblBPNV;




    }
}