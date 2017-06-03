using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class ARCMO_F9F3
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
            this.txtTienThanhToan = new V6Controls.NumberTien();
            this.txtTienThanhToanNt = new V6Controls.NumberTien();
            this.v6Label8 = new V6Controls.V6Label();
            this.txtTienNtQd = new V6Controls.NumberTien();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtMaNtTienPhaiTra = new V6Controls.V6VvarTextBox();
            this.txtTienPhaiTra = new V6Controls.NumberTienNt();
            this.txtMaNtTienThanhToanNt = new V6Controls.V6VvarTextBox();
            this.txtMaNtTienNtQd = new V6Controls.V6VvarTextBox();
            this.txtMaNtTienThanhToan = new V6Controls.V6VvarTextBox();
            this.chkSuaTienQD = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtTienThanhToan
            // 
            this.txtTienThanhToan.AccessibleName = "T_tt_qd";
            this.txtTienThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienThanhToan.BackColor = System.Drawing.Color.White;
            this.txtTienThanhToan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTienThanhToan.DecimalPlaces = 2;
            this.txtTienThanhToan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTienThanhToan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTienThanhToan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTienThanhToan.HoverColor = System.Drawing.Color.Yellow;
            this.txtTienThanhToan.LeaveColor = System.Drawing.Color.White;
            this.txtTienThanhToan.Location = new System.Drawing.Point(394, 35);
            this.txtTienThanhToan.Name = "txtTienThanhToan";
            this.txtTienThanhToan.Size = new System.Drawing.Size(146, 20);
            this.txtTienThanhToan.TabIndex = 3;
            this.txtTienThanhToan.Text = "0,00";
            this.txtTienThanhToan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienThanhToan.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTienThanhToan.V6LostFocus += new V6Controls.ControlEventHandle(this.txtTienThanhToan_V6LostFocus);
            // 
            // txtTienThanhToanNt
            // 
            this.txtTienThanhToanNt.AccessibleName = "T_Tt_NT0";
            this.txtTienThanhToanNt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienThanhToanNt.BackColor = System.Drawing.Color.White;
            this.txtTienThanhToanNt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTienThanhToanNt.DecimalPlaces = 2;
            this.txtTienThanhToanNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTienThanhToanNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTienThanhToanNt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTienThanhToanNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtTienThanhToanNt.LeaveColor = System.Drawing.Color.White;
            this.txtTienThanhToanNt.Location = new System.Drawing.Point(167, 35);
            this.txtTienThanhToanNt.Name = "txtTienThanhToanNt";
            this.txtTienThanhToanNt.Size = new System.Drawing.Size(146, 20);
            this.txtTienThanhToanNt.TabIndex = 1;
            this.txtTienThanhToanNt.Text = "0,00";
            this.txtTienThanhToanNt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienThanhToanNt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTienThanhToanNt.V6LostFocus += new V6Controls.ControlEventHandle(this.txtTienThanhToanNt_V6LostFocus);
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00106";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(20, 64);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(84, 13);
            this.v6Label8.TabIndex = 5;
            this.v6Label8.Text = "Tiền NT quy đổi";
            // 
            // txtTienNtQd
            // 
            this.txtTienNtQd.AccessibleName = "t_tt";
            this.txtTienNtQd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienNtQd.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTienNtQd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTienNtQd.DecimalPlaces = 2;
            this.txtTienNtQd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTienNtQd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTienNtQd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTienNtQd.HoverColor = System.Drawing.Color.Yellow;
            this.txtTienNtQd.LeaveColor = System.Drawing.Color.White;
            this.txtTienNtQd.Location = new System.Drawing.Point(167, 61);
            this.txtTienNtQd.Name = "txtTienNtQd";
            this.txtTienNtQd.ReadOnly = true;
            this.txtTienNtQd.Size = new System.Drawing.Size(146, 20);
            this.txtTienNtQd.TabIndex = 7;
            this.txtTienNtQd.Text = "0,00";
            this.txtTienNtQd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienNtQd.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "XULYL00105";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(20, 38);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(94, 13);
            this.v6Label6.TabIndex = 0;
            this.v6Label6.Text = "Số tiền thanh toán";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "XULYL00104";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(20, 13);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(139, 13);
            this.v6Label7.TabIndex = 11;
            this.v6Label7.Text = "Số tiền còn phải trả cho HĐ";
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleDescription = "REPORTB00005";
            this.btnThoat.AccessibleName = "";
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnThoat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnThoat.Location = new System.Drawing.Point(111, 103);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 40);
            this.btnThoat.TabIndex = 10;
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
            this.btnNhan.Location = new System.Drawing.Point(23, 103);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 9;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtMaNtTienPhaiTra
            // 
            this.txtMaNtTienPhaiTra.AccessibleName = "ma_nt";
            this.txtMaNtTienPhaiTra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaNtTienPhaiTra.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNtTienPhaiTra.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNtTienPhaiTra.BrotherFields = "ten_nt";
            this.txtMaNtTienPhaiTra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaNtTienPhaiTra.CheckNotEmpty = true;
            this.txtMaNtTienPhaiTra.Enabled = false;
            this.txtMaNtTienPhaiTra.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNtTienPhaiTra.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienPhaiTra.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienPhaiTra.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNtTienPhaiTra.LeaveColor = System.Drawing.Color.White;
            this.txtMaNtTienPhaiTra.Location = new System.Drawing.Point(319, 10);
            this.txtMaNtTienPhaiTra.Name = "txtMaNtTienPhaiTra";
            this.txtMaNtTienPhaiTra.ReadOnly = true;
            this.txtMaNtTienPhaiTra.Size = new System.Drawing.Size(56, 20);
            this.txtMaNtTienPhaiTra.TabIndex = 13;
            this.txtMaNtTienPhaiTra.VVar = "ma_nt";
            this.txtMaNtTienPhaiTra.TextChanged += new System.EventHandler(this.txtMaNt_TextChanged);
            // 
            // txtTienPhaiTra
            // 
            this.txtTienPhaiTra.AccessibleName = "T_TT_NT";
            this.txtTienPhaiTra.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTienPhaiTra.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTienPhaiTra.DecimalPlaces = 2;
            this.txtTienPhaiTra.Enabled = false;
            this.txtTienPhaiTra.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTienPhaiTra.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTienPhaiTra.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTienPhaiTra.HoverColor = System.Drawing.Color.Yellow;
            this.txtTienPhaiTra.LeaveColor = System.Drawing.Color.White;
            this.txtTienPhaiTra.Location = new System.Drawing.Point(167, 10);
            this.txtTienPhaiTra.Name = "txtTienPhaiTra";
            this.txtTienPhaiTra.ReadOnly = true;
            this.txtTienPhaiTra.Size = new System.Drawing.Size(146, 20);
            this.txtTienPhaiTra.TabIndex = 12;
            this.txtTienPhaiTra.Text = "0,00";
            this.txtTienPhaiTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienPhaiTra.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtMaNtTienThanhToanNt
            // 
            this.txtMaNtTienThanhToanNt.AccessibleName = "ma_nt";
            this.txtMaNtTienThanhToanNt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaNtTienThanhToanNt.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNtTienThanhToanNt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNtTienThanhToanNt.BrotherFields = "ten_nt";
            this.txtMaNtTienThanhToanNt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaNtTienThanhToanNt.CheckNotEmpty = true;
            this.txtMaNtTienThanhToanNt.Enabled = false;
            this.txtMaNtTienThanhToanNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNtTienThanhToanNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienThanhToanNt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienThanhToanNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNtTienThanhToanNt.LeaveColor = System.Drawing.Color.White;
            this.txtMaNtTienThanhToanNt.Location = new System.Drawing.Point(319, 35);
            this.txtMaNtTienThanhToanNt.Name = "txtMaNtTienThanhToanNt";
            this.txtMaNtTienThanhToanNt.ReadOnly = true;
            this.txtMaNtTienThanhToanNt.Size = new System.Drawing.Size(56, 20);
            this.txtMaNtTienThanhToanNt.TabIndex = 2;
            this.txtMaNtTienThanhToanNt.VVar = "ma_nt";
            this.txtMaNtTienThanhToanNt.TextChanged += new System.EventHandler(this.txtMaNt_TextChanged);
            // 
            // txtMaNtTienNtQd
            // 
            this.txtMaNtTienNtQd.AccessibleName = "ma_nt";
            this.txtMaNtTienNtQd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaNtTienNtQd.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNtTienNtQd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNtTienNtQd.BrotherFields = "ten_nt";
            this.txtMaNtTienNtQd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaNtTienNtQd.CheckNotEmpty = true;
            this.txtMaNtTienNtQd.Enabled = false;
            this.txtMaNtTienNtQd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNtTienNtQd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienNtQd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienNtQd.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNtTienNtQd.LeaveColor = System.Drawing.Color.White;
            this.txtMaNtTienNtQd.Location = new System.Drawing.Point(319, 61);
            this.txtMaNtTienNtQd.Name = "txtMaNtTienNtQd";
            this.txtMaNtTienNtQd.ReadOnly = true;
            this.txtMaNtTienNtQd.Size = new System.Drawing.Size(56, 20);
            this.txtMaNtTienNtQd.TabIndex = 8;
            this.txtMaNtTienNtQd.VVar = "ma_nt";
            this.txtMaNtTienNtQd.TextChanged += new System.EventHandler(this.txtMaNt_TextChanged);
            // 
            // txtMaNtTienThanhToan
            // 
            this.txtMaNtTienThanhToan.AccessibleName = "ma_nt";
            this.txtMaNtTienThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaNtTienThanhToan.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNtTienThanhToan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNtTienThanhToan.BrotherFields = "ten_nt";
            this.txtMaNtTienThanhToan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaNtTienThanhToan.CheckNotEmpty = true;
            this.txtMaNtTienThanhToan.Enabled = false;
            this.txtMaNtTienThanhToan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNtTienThanhToan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienThanhToan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNtTienThanhToan.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNtTienThanhToan.LeaveColor = System.Drawing.Color.White;
            this.txtMaNtTienThanhToan.Location = new System.Drawing.Point(546, 35);
            this.txtMaNtTienThanhToan.Name = "txtMaNtTienThanhToan";
            this.txtMaNtTienThanhToan.ReadOnly = true;
            this.txtMaNtTienThanhToan.Size = new System.Drawing.Size(56, 20);
            this.txtMaNtTienThanhToan.TabIndex = 4;
            this.txtMaNtTienThanhToan.VVar = "ma_nt";
            this.txtMaNtTienThanhToan.TextChanged += new System.EventHandler(this.txtMaNt_TextChanged);
            // 
            // chkSuaTienQD
            // 
            this.chkSuaTienQD.AccessibleDescription = "XULYC00004";
            this.chkSuaTienQD.AutoSize = true;
            this.chkSuaTienQD.Location = new System.Drawing.Point(116, 63);
            this.chkSuaTienQD.Name = "chkSuaTienQD";
            this.chkSuaTienQD.Size = new System.Drawing.Size(43, 17);
            this.chkSuaTienQD.TabIndex = 6;
            this.chkSuaTienQD.Text = "sửa";
            this.chkSuaTienQD.UseVisualStyleBackColor = true;
            this.chkSuaTienQD.CheckedChanged += new System.EventHandler(this.chkSuaTienQD_CheckedChanged);
            // 
            // ARCMO_F9F3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(621, 148);
            this.Controls.Add(this.chkSuaTienQD);
            this.Controls.Add(this.txtTienPhaiTra);
            this.Controls.Add(this.txtMaNtTienThanhToan);
            this.Controls.Add(this.txtMaNtTienNtQd);
            this.Controls.Add(this.txtMaNtTienThanhToanNt);
            this.Controls.Add(this.txtMaNtTienPhaiTra);
            this.Controls.Add(this.txtTienThanhToan);
            this.Controls.Add(this.txtTienThanhToanNt);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtTienNtQd);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Name = "ARCMO_F9F3";
            this.Text = "PhanBoTrucTiep";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ARCMO_F9F3_FormClosing);
            this.Load += new System.EventHandler(this.ARCMO_F9F3_Load);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnThoat, 0);
            this.Controls.SetChildIndex(this.v6Label7, 0);
            this.Controls.SetChildIndex(this.v6Label6, 0);
            this.Controls.SetChildIndex(this.txtTienNtQd, 0);
            this.Controls.SetChildIndex(this.v6Label8, 0);
            this.Controls.SetChildIndex(this.txtTienThanhToanNt, 0);
            this.Controls.SetChildIndex(this.txtTienThanhToan, 0);
            this.Controls.SetChildIndex(this.txtMaNtTienPhaiTra, 0);
            this.Controls.SetChildIndex(this.txtMaNtTienThanhToanNt, 0);
            this.Controls.SetChildIndex(this.txtMaNtTienNtQd, 0);
            this.Controls.SetChildIndex(this.txtMaNtTienThanhToan, 0);
            this.Controls.SetChildIndex(this.txtTienPhaiTra, 0);
            this.Controls.SetChildIndex(this.chkSuaTienQD, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label v6Label8;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6Label v6Label7;
        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        public V6Controls.V6VvarTextBox txtMaNtTienPhaiTra;
        public V6Controls.V6VvarTextBox txtMaNtTienThanhToanNt;
        public V6Controls.V6VvarTextBox txtMaNtTienNtQd;
        public V6Controls.V6VvarTextBox txtMaNtTienThanhToan;
        public V6Controls.NumberTien txtTienThanhToan;
        public V6Controls.NumberTien txtTienThanhToanNt;
        public V6Controls.NumberTien txtTienNtQd;
        public V6Controls.NumberTienNt txtTienPhaiTra;
        private CheckBox chkSuaTienQD;
    }
}