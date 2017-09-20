using V6Controls;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc
{
    partial class LocThongTinPhieuChi
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFilter1 = new V6ReportControls.PanelFilter();
            this.chkLike = new V6Controls.V6CheckBox();
            this.loaiPhieu = new V6Controls.V6VvarTextBox();
            this.soTienDen = new V6Controls.V6NumberTextBox();
            this.maBoPhan = new V6Controls.V6VvarTextBox();
            this.taiKhoanNo = new V6Controls.V6VvarTextBox();
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.dienGiai = new V6Controls.V6VvarTextBox();
            this.maNhanVien = new V6Controls.V6VvarTextBox();
            this.maDonVi = new V6Controls.V6VvarTextBox();
            this.soTienTu = new V6Controls.V6NumberTextBox();
            this.maKhach = new V6Controls.V6VvarTextBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label25 = new V6Controls.V6Label();
            this.v6Label24 = new V6Controls.V6Label();
            this.v6Label21 = new V6Controls.V6Label();
            this.v6Label29 = new V6Controls.V6Label();
            this.v6Label23 = new V6Controls.V6Label();
            this.v6Label20 = new V6Controls.V6Label();
            this.v6Label28 = new V6Controls.V6Label();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label26 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "SEARCHG00001";
            this.groupBox1.Controls.Add(this.panelFilter1);
            this.groupBox1.Controls.Add(this.chkLike);
            this.groupBox1.Controls.Add(this.loaiPhieu);
            this.groupBox1.Controls.Add(this.soTienDen);
            this.groupBox1.Controls.Add(this.maBoPhan);
            this.groupBox1.Controls.Add(this.taiKhoanNo);
            this.groupBox1.Controls.Add(this.ctDenSo);
            this.groupBox1.Controls.Add(this.dienGiai);
            this.groupBox1.Controls.Add(this.maNhanVien);
            this.groupBox1.Controls.Add(this.maDonVi);
            this.groupBox1.Controls.Add(this.soTienTu);
            this.groupBox1.Controls.Add(this.maKhach);
            this.groupBox1.Controls.Add(this.ctTuSo);
            this.groupBox1.Controls.Add(this.v6Label25);
            this.groupBox1.Controls.Add(this.v6Label24);
            this.groupBox1.Controls.Add(this.v6Label21);
            this.groupBox1.Controls.Add(this.v6Label29);
            this.groupBox1.Controls.Add(this.v6Label23);
            this.groupBox1.Controls.Add(this.v6Label20);
            this.groupBox1.Controls.Add(this.v6Label28);
            this.groupBox1.Controls.Add(this.v6Label6);
            this.groupBox1.Controls.Add(this.v6Label26);
            this.groupBox1.Controls.Add(this.v6Label7);
            this.groupBox1.Controls.Add(this.v6Label8);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 216);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc thông tin";
            // 
            // panelFilter1
            // 
            this.panelFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFilter1.Location = new System.Drawing.Point(503, 13);
            this.panelFilter1.Name = "panelFilter1";
            this.panelFilter1.Size = new System.Drawing.Size(250, 197);
            this.panelFilter1.TabIndex = 24;
            this.panelFilter1.Tag = "canceldata";
            // 
            // chkLike
            // 
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(225, 21);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 2;
            this.chkLike.Text = "like";
            this.chkLike.UseVisualStyleBackColor = true;
            this.chkLike.CheckedChanged += new System.EventHandler(this.chkLike_CheckedChanged);
            // 
            // loaiPhieu
            // 
            this.loaiPhieu.AccessibleName = "ma_gd";
            this.loaiPhieu.BackColor = System.Drawing.SystemColors.Window;
            this.loaiPhieu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.loaiPhieu.CheckOnLeave = false;
            this.loaiPhieu.EnterColor = System.Drawing.Color.PaleGreen;
            this.loaiPhieu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.loaiPhieu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.loaiPhieu.HoverColor = System.Drawing.Color.Yellow;
            this.loaiPhieu.LeaveColor = System.Drawing.Color.White;
            this.loaiPhieu.Location = new System.Drawing.Point(365, 97);
            this.loaiPhieu.MaxLength = 1;
            this.loaiPhieu.Name = "loaiPhieu";
            this.loaiPhieu.Size = new System.Drawing.Size(132, 20);
            this.loaiPhieu.TabIndex = 16;
            // 
            // soTienDen
            // 
            this.soTienDen.BackColor = System.Drawing.SystemColors.Window;
            this.soTienDen.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.soTienDen.EnterColor = System.Drawing.Color.PaleGreen;
            this.soTienDen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.soTienDen.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.soTienDen.HoverColor = System.Drawing.Color.Yellow;
            this.soTienDen.LeaveColor = System.Drawing.Color.White;
            this.soTienDen.LimitCharacters = ".0123456789";
            this.soTienDen.Location = new System.Drawing.Point(365, 123);
            this.soTienDen.Name = "soTienDen";
            this.soTienDen.Size = new System.Drawing.Size(132, 20);
            this.soTienDen.TabIndex = 20;
            this.soTienDen.Text = "0,000";
            this.soTienDen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.soTienDen.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // maBoPhan
            // 
            this.maBoPhan.AccessibleName = "MA_BP";
            this.maBoPhan.BackColor = System.Drawing.SystemColors.Window;
            this.maBoPhan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.maBoPhan.CheckOnLeave = false;
            this.maBoPhan.EnterColor = System.Drawing.Color.PaleGreen;
            this.maBoPhan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maBoPhan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.maBoPhan.HoverColor = System.Drawing.Color.Yellow;
            this.maBoPhan.LeaveColor = System.Drawing.Color.White;
            this.maBoPhan.Location = new System.Drawing.Point(365, 71);
            this.maBoPhan.Name = "maBoPhan";
            this.maBoPhan.Size = new System.Drawing.Size(132, 20);
            this.maBoPhan.TabIndex = 12;
            this.maBoPhan.VVar = "MA_BP";
            // 
            // taiKhoanNo
            // 
            this.taiKhoanNo.AccessibleName = "TK";
            this.taiKhoanNo.BackColor = System.Drawing.SystemColors.Window;
            this.taiKhoanNo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.taiKhoanNo.CheckOnLeave = false;
            this.taiKhoanNo.EnterColor = System.Drawing.Color.PaleGreen;
            this.taiKhoanNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.taiKhoanNo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.taiKhoanNo.HoverColor = System.Drawing.Color.Yellow;
            this.taiKhoanNo.LeaveColor = System.Drawing.Color.White;
            this.taiKhoanNo.Location = new System.Drawing.Point(365, 45);
            this.taiKhoanNo.Name = "taiKhoanNo";
            this.taiKhoanNo.Size = new System.Drawing.Size(132, 20);
            this.taiKhoanNo.TabIndex = 8;
            this.taiKhoanNo.VVar = "TK";
            // 
            // ctDenSo
            // 
            this.ctDenSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctDenSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctDenSo.CheckOnLeave = false;
            this.ctDenSo.Enabled = false;
            this.ctDenSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctDenSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctDenSo.LeaveColor = System.Drawing.Color.White;
            this.ctDenSo.Location = new System.Drawing.Point(365, 19);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(132, 20);
            this.ctDenSo.TabIndex = 4;
            // 
            // dienGiai
            // 
            this.dienGiai.BackColor = System.Drawing.SystemColors.Window;
            this.dienGiai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dienGiai.EnterColor = System.Drawing.Color.PaleGreen;
            this.dienGiai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dienGiai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dienGiai.HoverColor = System.Drawing.Color.Yellow;
            this.dienGiai.LeaveColor = System.Drawing.Color.White;
            this.dienGiai.Location = new System.Drawing.Point(87, 175);
            this.dienGiai.Name = "dienGiai";
            this.dienGiai.Size = new System.Drawing.Size(410, 20);
            this.dienGiai.TabIndex = 22;
            // 
            // maNhanVien
            // 
            this.maNhanVien.AccessibleName = "MA_NVIEN";
            this.maNhanVien.BackColor = System.Drawing.SystemColors.Window;
            this.maNhanVien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.maNhanVien.CheckOnLeave = false;
            this.maNhanVien.EnterColor = System.Drawing.Color.PaleGreen;
            this.maNhanVien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maNhanVien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.maNhanVien.HoverColor = System.Drawing.Color.Yellow;
            this.maNhanVien.LeaveColor = System.Drawing.Color.White;
            this.maNhanVien.Location = new System.Drawing.Point(87, 97);
            this.maNhanVien.Name = "maNhanVien";
            this.maNhanVien.Size = new System.Drawing.Size(132, 20);
            this.maNhanVien.TabIndex = 14;
            this.maNhanVien.VVar = "MA_NVIEN";
            // 
            // maDonVi
            // 
            this.maDonVi.AccessibleName = "ma_dvcs";
            this.maDonVi.BackColor = System.Drawing.SystemColors.Window;
            this.maDonVi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.maDonVi.CheckOnLeave = false;
            this.maDonVi.EnterColor = System.Drawing.Color.PaleGreen;
            this.maDonVi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maDonVi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.maDonVi.HoverColor = System.Drawing.Color.Yellow;
            this.maDonVi.LeaveColor = System.Drawing.Color.White;
            this.maDonVi.Location = new System.Drawing.Point(87, 71);
            this.maDonVi.Name = "maDonVi";
            this.maDonVi.Size = new System.Drawing.Size(132, 20);
            this.maDonVi.TabIndex = 10;
            this.maDonVi.VVar = "MA_DVCS";
            // 
            // soTienTu
            // 
            this.soTienTu.BackColor = System.Drawing.SystemColors.Window;
            this.soTienTu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.soTienTu.EnterColor = System.Drawing.Color.PaleGreen;
            this.soTienTu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.soTienTu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.soTienTu.HoverColor = System.Drawing.Color.Yellow;
            this.soTienTu.LeaveColor = System.Drawing.Color.White;
            this.soTienTu.LimitCharacters = ".0123456789";
            this.soTienTu.Location = new System.Drawing.Point(87, 123);
            this.soTienTu.Name = "soTienTu";
            this.soTienTu.Size = new System.Drawing.Size(132, 20);
            this.soTienTu.TabIndex = 18;
            this.soTienTu.Text = "0,000";
            this.soTienTu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.soTienTu.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // maKhach
            // 
            this.maKhach.AccessibleName = "ma_kh";
            this.maKhach.BackColor = System.Drawing.SystemColors.Window;
            this.maKhach.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.maKhach.CheckOnLeave = false;
            this.maKhach.EnterColor = System.Drawing.Color.PaleGreen;
            this.maKhach.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maKhach.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.maKhach.HoverColor = System.Drawing.Color.Yellow;
            this.maKhach.LeaveColor = System.Drawing.Color.White;
            this.maKhach.Location = new System.Drawing.Point(87, 45);
            this.maKhach.Name = "maKhach";
            this.maKhach.Size = new System.Drawing.Size(132, 20);
            this.maKhach.TabIndex = 6;
            this.maKhach.VVar = "MA_KH";
            // 
            // ctTuSo
            // 
            this.ctTuSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctTuSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctTuSo.CheckOnLeave = false;
            this.ctTuSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctTuSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctTuSo.LeaveColor = System.Drawing.Color.White;
            this.ctTuSo.Location = new System.Drawing.Point(87, 19);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(132, 20);
            this.ctTuSo.TabIndex = 1;
            // 
            // v6Label25
            // 
            this.v6Label25.AccessibleDescription = "SEARCHL00012";
            this.v6Label25.AutoSize = true;
            this.v6Label25.Location = new System.Drawing.Point(6, 178);
            this.v6Label25.Name = "v6Label25";
            this.v6Label25.Size = new System.Drawing.Size(75, 13);
            this.v6Label25.TabIndex = 21;
            this.v6Label25.Text = "Diễn giải chứa";
            // 
            // v6Label24
            // 
            this.v6Label24.AccessibleDescription = "SEARCHL00007";
            this.v6Label24.AutoSize = true;
            this.v6Label24.Location = new System.Drawing.Point(6, 100);
            this.v6Label24.Name = "v6Label24";
            this.v6Label24.Size = new System.Drawing.Size(69, 13);
            this.v6Label24.TabIndex = 13;
            this.v6Label24.Text = "Mã nviên BH";
            // 
            // v6Label21
            // 
            this.v6Label21.AccessibleDescription = "SEARCHL00043";
            this.v6Label21.AutoSize = true;
            this.v6Label21.Location = new System.Drawing.Point(6, 74);
            this.v6Label21.Name = "v6Label21";
            this.v6Label21.Size = new System.Drawing.Size(55, 13);
            this.v6Label21.TabIndex = 9;
            this.v6Label21.Text = "Mã đơn vị";
            // 
            // v6Label29
            // 
            this.v6Label29.AccessibleDescription = "SEARCHL00036";
            this.v6Label29.AutoSize = true;
            this.v6Label29.Location = new System.Drawing.Point(282, 100);
            this.v6Label29.Name = "v6Label29";
            this.v6Label29.Size = new System.Drawing.Size(56, 13);
            this.v6Label29.TabIndex = 15;
            this.v6Label29.Text = "Loại phiếu";
            // 
            // v6Label23
            // 
            this.v6Label23.AccessibleDescription = "SEARCHL00010";
            this.v6Label23.AutoSize = true;
            this.v6Label23.Location = new System.Drawing.Point(6, 126);
            this.v6Label23.Name = "v6Label23";
            this.v6Label23.Size = new System.Drawing.Size(52, 13);
            this.v6Label23.TabIndex = 17;
            this.v6Label23.Text = "Số tiền từ";
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "SEARCHL00004";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(6, 48);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(55, 13);
            this.v6Label20.TabIndex = 5;
            this.v6Label20.Text = "Mã khách";
            // 
            // v6Label28
            // 
            this.v6Label28.AccessibleDescription = "SEARCHL00011";
            this.v6Label28.AutoSize = true;
            this.v6Label28.Location = new System.Drawing.Point(282, 126);
            this.v6Label28.Name = "v6Label28";
            this.v6Label28.Size = new System.Drawing.Size(27, 13);
            this.v6Label28.TabIndex = 19;
            this.v6Label28.Text = "Đến";
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "SEARCHL00014";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(282, 48);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(70, 13);
            this.v6Label6.TabIndex = 7;
            this.v6Label6.Text = "Tài khoản có";
            // 
            // v6Label26
            // 
            this.v6Label26.AccessibleDescription = "SEARCHL00005";
            this.v6Label26.AutoSize = true;
            this.v6Label26.Location = new System.Drawing.Point(282, 74);
            this.v6Label26.Name = "v6Label26";
            this.v6Label26.Size = new System.Drawing.Size(64, 13);
            this.v6Label26.TabIndex = 11;
            this.v6Label26.Text = "Mã bộ phận";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "SEARCHL00003";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(282, 22);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 3;
            this.v6Label7.Text = "Đến số";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "SEARCHL00002";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(6, 22);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 0;
            this.v6Label8.Text = "CT từ số";
            // 
            // LocThongTinPhieuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LocThongTinPhieuChi";
            this.Size = new System.Drawing.Size(759, 216);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Label v6Label25;
        private V6Label v6Label24;
        private V6Label v6Label21;
        private V6Label v6Label29;
        private V6Label v6Label23;
        private V6Label v6Label20;
        private V6Label v6Label28;
        private V6Label v6Label6;
        private V6Label v6Label26;
        private V6Label v6Label7;
        private V6Label v6Label8;
        private V6VvarTextBox loaiPhieu;
        private V6Controls.V6NumberTextBox soTienDen;
        private V6VvarTextBox maBoPhan;
        private V6VvarTextBox taiKhoanNo;
        private V6VvarTextBox ctDenSo;
        private V6VvarTextBox dienGiai;
        private V6VvarTextBox maNhanVien;
        private V6VvarTextBox maDonVi;
        private V6Controls.V6NumberTextBox soTienTu;
        private V6VvarTextBox maKhach;
        private V6VvarTextBox ctTuSo;
        private V6Controls.V6CheckBox chkLike;
        private V6ReportControls.PanelFilter panelFilter1;
    }
}
