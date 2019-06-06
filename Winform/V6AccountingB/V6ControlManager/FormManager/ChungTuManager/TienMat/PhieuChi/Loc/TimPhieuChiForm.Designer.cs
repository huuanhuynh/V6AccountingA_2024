namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc
{
    partial class TimPhieuChiForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbTuyChon = new System.Windows.Forms.GroupBox();
            this.lblStatusDescription = new V6Controls.V6Label();
            this.txtTrangThai = new V6Controls.V6ColorTextBox();
            this.chkNSD = new V6Controls.V6CheckBox();
            this.v6Label21 = new V6Controls.V6Label();
            this.v6Label20 = new V6Controls.V6Label();
            this.locThongTinChiTiet1 = new V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc.LocTTChiTietPhieuChi();
            this.locThongTin1 = new V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc.LocThongTinPhieuChi();
            this.locThoiGian1 = new V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc.LocThoiGianPhieuChi();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.lblDocSoTien = new V6Controls.V6Label();
            this.panel1.SuspendLayout();
            this.grbTuyChon.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grbTuyChon);
            this.panel1.Controls.Add(this.locThongTinChiTiet1);
            this.panel1.Controls.Add(this.locThongTin1);
            this.panel1.Controls.Add(this.locThoiGian1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 615);
            this.panel1.TabIndex = 0;
            // 
            // grbTuyChon
            // 
            this.grbTuyChon.AccessibleDescription = "SEARCHG00003";
            this.grbTuyChon.Controls.Add(this.lblStatusDescription);
            this.grbTuyChon.Controls.Add(this.txtTrangThai);
            this.grbTuyChon.Controls.Add(this.chkNSD);
            this.grbTuyChon.Controls.Add(this.v6Label21);
            this.grbTuyChon.Controls.Add(this.v6Label20);
            this.grbTuyChon.Location = new System.Drawing.Point(4, 514);
            this.grbTuyChon.Name = "grbTuyChon";
            this.grbTuyChon.Size = new System.Drawing.Size(759, 83);
            this.grbTuyChon.TabIndex = 3;
            this.grbTuyChon.TabStop = false;
            this.grbTuyChon.Text = "Lọc tùy chọn";
            // 
            // lblStatusDescription
            // 
            this.lblStatusDescription.AccessibleDescription = ".";
            this.lblStatusDescription.Location = new System.Drawing.Point(137, 42);
            this.lblStatusDescription.Name = "lblStatusDescription";
            this.lblStatusDescription.Size = new System.Drawing.Size(616, 35);
            this.lblStatusDescription.TabIndex = 14;
            this.lblStatusDescription.Text = "* Tất cả, 0- Chưa ghi vào sổ cái, 2- Đã ghi sổ cái";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.AccessibleName = "status";
            this.txtTrangThai.BackColor = System.Drawing.SystemColors.Window;
            this.txtTrangThai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTrangThai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTrangThai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTrangThai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTrangThai.HoverColor = System.Drawing.Color.Yellow;
            this.txtTrangThai.LeaveColor = System.Drawing.Color.White;
            this.txtTrangThai.LimitCharacters = "*012";
            this.txtTrangThai.Location = new System.Drawing.Point(87, 39);
            this.txtTrangThai.MaxLength = 1;
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(44, 20);
            this.txtTrangThai.TabIndex = 13;
            // 
            // chkNSD
            // 
            this.chkNSD.AccessibleName = "";
            this.chkNSD.AutoSize = true;
            this.chkNSD.Location = new System.Drawing.Point(87, 16);
            this.chkNSD.Name = "chkNSD";
            this.chkNSD.Size = new System.Drawing.Size(15, 14);
            this.chkNSD.TabIndex = 3;
            this.chkNSD.UseVisualStyleBackColor = true;
            // 
            // v6Label21
            // 
            this.v6Label21.AccessibleDescription = "SEARCHL00045";
            this.v6Label21.AutoSize = true;
            this.v6Label21.Location = new System.Drawing.Point(6, 42);
            this.v6Label21.Name = "v6Label21";
            this.v6Label21.Size = new System.Drawing.Size(55, 13);
            this.v6Label21.TabIndex = 4;
            this.v6Label21.Text = "Trạng thái";
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "SEARCHL00044";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(6, 16);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(69, 13);
            this.v6Label20.TabIndex = 2;
            this.v6Label20.Text = "Lọc theo nsd";
            // 
            // locThongTinChiTiet1
            // 
            this.locThongTinChiTiet1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locThongTinChiTiet1.Location = new System.Drawing.Point(3, 276);
            this.locThongTinChiTiet1.Name = "locThongTinChiTiet1";
            this.locThongTinChiTiet1.Size = new System.Drawing.Size(759, 232);
            this.locThongTinChiTiet1.TabIndex = 2;
            // 
            // locThongTin1
            // 
            this.locThongTin1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locThongTin1.Location = new System.Drawing.Point(3, 57);
            this.locThongTin1.Name = "locThongTin1";
            this.locThongTin1.Size = new System.Drawing.Size(759, 212);
            this.locThongTin1.TabIndex = 1;
            // 
            // locThoiGian1
            // 
            this.locThoiGian1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locThoiGian1.Location = new System.Drawing.Point(3, 4);
            this.locThoiGian1.Name = "locThoiGian1";
            this.locThoiGian1.Size = new System.Drawing.Size(759, 48);
            this.locThoiGian1.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(93, 623);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.Location = new System.Drawing.Point(5, 623);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // lblDocSoTien
            // 
            this.lblDocSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocSoTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocSoTien.Location = new System.Drawing.Point(187, 628);
            this.lblDocSoTien.Name = "lblDocSoTien";
            this.lblDocSoTien.Size = new System.Drawing.Size(587, 37);
            this.lblDocSoTien.TabIndex = 14;
            this.lblDocSoTien.Text = "0";
            this.lblDocSoTien.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TimPhieuChiForm
            // 
            this.AccessibleDescription = "SEARCHL00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 669);
            this.Controls.Add(this.lblDocSoTien);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.btnHuy);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "TimPhieuChiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm chứng từ";
            this.Activated += new System.EventHandler(this.TimPhieuChiForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimPhieuChiForm_FormClosing);
            this.Load += new System.EventHandler(this.TimHoaDonForm_Load);
            this.VisibleChanged += new System.EventHandler(this.TimPhieuChiForm_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TimPhieuChiForm_KeyDown);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblDocSoTien, 0);
            this.panel1.ResumeLayout(false);
            this.grbTuyChon.ResumeLayout(false);
            this.grbTuyChon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private LocThoiGianPhieuChi locThoiGian1;
        private LocTTChiTietPhieuChi locThongTinChiTiet1;
        private LocThongTinPhieuChi locThongTin1;
        private V6Controls.V6Label lblDocSoTien;
        private System.Windows.Forms.GroupBox grbTuyChon;
        private V6Controls.V6Label lblStatusDescription;
        private V6Controls.V6ColorTextBox txtTrangThai;
        private V6Controls.V6CheckBox chkNSD;
        private V6Controls.V6Label v6Label21;
        private V6Controls.V6Label v6Label20;
    }
}