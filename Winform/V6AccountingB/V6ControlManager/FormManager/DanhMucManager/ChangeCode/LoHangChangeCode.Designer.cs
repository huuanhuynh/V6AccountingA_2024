namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    partial class LoHangChangeCode
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radDoiMaLoVaNgay = new System.Windows.Forms.RadioButton();
            this.radDoiMaLo = new System.Windows.Forms.RadioButton();
            this.radDoiNGay = new System.Windows.Forms.RadioButton();
            this.txtOldCode = new V6Controls.V6VvarTextBox();
            this.txtName = new V6Controls.V6ColorTextBox();
            this.txtNgayHHSD = new V6Controls.V6DateTimeColor();
            this.txtNgayHHSDmoi = new V6Controls.V6DateTimeColor();
            this.lblNgayHHSD = new System.Windows.Forms.Label();
            this.txtMaVt = new V6Controls.V6ColorTextBox();
            this.txtNewCode = new V6Controls.V6ColorTextBox();
            this.lblMaVt = new System.Windows.Forms.Label();
            this.lblHSDMoi = new System.Windows.Forms.Label();
            this.lblNewCode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOldCode = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleName = "groupBox1";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radDoiMaLoVaNgay);
            this.groupBox1.Controls.Add(this.radDoiMaLo);
            this.groupBox1.Controls.Add(this.radDoiNGay);
            this.groupBox1.Controls.Add(this.txtOldCode);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtNgayHHSD);
            this.groupBox1.Controls.Add(this.txtNgayHHSDmoi);
            this.groupBox1.Controls.Add(this.lblNgayHHSD);
            this.groupBox1.Controls.Add(this.txtMaVt);
            this.groupBox1.Controls.Add(this.txtNewCode);
            this.groupBox1.Controls.Add(this.lblMaVt);
            this.groupBox1.Controls.Add(this.lblHSDMoi);
            this.groupBox1.Controls.Add(this.lblNewCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblOldCode);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(816, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // radDoiMaLoVaNgay
            // 
            this.radDoiMaLoVaNgay.AccessibleDescription = "ADDEDITR00003";
            this.radDoiMaLoVaNgay.AutoSize = true;
            this.radDoiMaLoVaNgay.Location = new System.Drawing.Point(292, 15);
            this.radDoiMaLoVaNgay.Name = "radDoiMaLoVaNgay";
            this.radDoiMaLoVaNgay.Size = new System.Drawing.Size(110, 17);
            this.radDoiMaLoVaNgay.TabIndex = 3;
            this.radDoiMaLoVaNgay.Text = "Đổi mã lô và HSD";
            this.radDoiMaLoVaNgay.UseVisualStyleBackColor = true;
            this.radDoiMaLoVaNgay.CheckedChanged += new System.EventHandler(this.rads_CheckedChanged);
            // 
            // radDoiMaLo
            // 
            this.radDoiMaLo.AccessibleDescription = "ADDEDITR00002";
            this.radDoiMaLo.AutoSize = true;
            this.radDoiMaLo.Location = new System.Drawing.Point(217, 15);
            this.radDoiMaLo.Name = "radDoiMaLo";
            this.radDoiMaLo.Size = new System.Drawing.Size(69, 17);
            this.radDoiMaLo.TabIndex = 2;
            this.radDoiMaLo.Text = "Đổi mã lô";
            this.radDoiMaLo.UseVisualStyleBackColor = true;
            this.radDoiMaLo.CheckedChanged += new System.EventHandler(this.rads_CheckedChanged);
            // 
            // radDoiNGay
            // 
            this.radDoiNGay.AccessibleDescription = "ADDEDITR00001";
            this.radDoiNGay.AutoSize = true;
            this.radDoiNGay.Location = new System.Drawing.Point(118, 15);
            this.radDoiNGay.Name = "radDoiNGay";
            this.radDoiNGay.Size = new System.Drawing.Size(93, 17);
            this.radDoiNGay.TabIndex = 1;
            this.radDoiNGay.Text = "Đổi ngày HSD";
            this.radDoiNGay.UseVisualStyleBackColor = true;
            this.radDoiNGay.CheckedChanged += new System.EventHandler(this.rads_CheckedChanged);
            // 
            // txtOldCode
            // 
            this.txtOldCode.AccessibleName = "MA_LO";
            this.txtOldCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtOldCode.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtOldCode.Enabled = false;
            this.txtOldCode.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtOldCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOldCode.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtOldCode.HoverColor = System.Drawing.Color.Yellow;
            this.txtOldCode.LeaveColor = System.Drawing.Color.White;
            this.txtOldCode.Location = new System.Drawing.Point(134, 46);
            this.txtOldCode.Name = "txtOldCode";
            this.txtOldCode.Size = new System.Drawing.Size(144, 20);
            this.txtOldCode.TabIndex = 5;
            this.txtOldCode.VVar = "ma_kh";
            // 
            // txtName
            // 
            this.txtName.AccessibleName = "SO_LOSX";
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtName.Enabled = false;
            this.txtName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtName.HoverColor = System.Drawing.Color.Yellow;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(285, 46);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(487, 20);
            this.txtName.TabIndex = 6;
            this.txtName.TabStop = false;
            // 
            // txtNgayHHSD
            // 
            this.txtNgayHHSD.AccessibleName = "NGAY_HHSD";
            this.txtNgayHHSD.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtNgayHHSD.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgayHHSD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNgayHHSD.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgayHHSD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNgayHHSD.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNgayHHSD.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgayHHSD.LeaveColor = System.Drawing.Color.White;
            this.txtNgayHHSD.Location = new System.Drawing.Point(426, 75);
            this.txtNgayHHSD.Margin = new System.Windows.Forms.Padding(4);
            this.txtNgayHHSD.Name = "txtNgayHHSD";
            this.txtNgayHHSD.ReadOnly = true;
            this.txtNgayHHSD.Size = new System.Drawing.Size(144, 20);
            this.txtNgayHHSD.StringValue = "__/__/____";
            this.txtNgayHHSD.TabIndex = 12;
            this.txtNgayHHSD.TabStop = false;
            this.txtNgayHHSD.Text = "__/__/____";
            // 
            // txtNgayHHSDmoi
            // 
            this.txtNgayHHSDmoi.AccessibleName = "";
            this.txtNgayHHSDmoi.BackColor = System.Drawing.Color.White;
            this.txtNgayHHSDmoi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgayHHSDmoi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNgayHHSDmoi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgayHHSDmoi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNgayHHSDmoi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNgayHHSDmoi.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgayHHSDmoi.LeaveColor = System.Drawing.Color.White;
            this.txtNgayHHSDmoi.Location = new System.Drawing.Point(134, 104);
            this.txtNgayHHSDmoi.Margin = new System.Windows.Forms.Padding(4);
            this.txtNgayHHSDmoi.Name = "txtNgayHHSDmoi";
            this.txtNgayHHSDmoi.Size = new System.Drawing.Size(144, 20);
            this.txtNgayHHSDmoi.StringValue = "__/__/____";
            this.txtNgayHHSDmoi.TabIndex = 10;
            this.txtNgayHHSDmoi.Text = "__/__/____";
            // 
            // lblNgayHHSD
            // 
            this.lblNgayHHSD.AccessibleDescription = "ADDEDITL00712";
            this.lblNgayHHSD.AutoSize = true;
            this.lblNgayHHSD.Location = new System.Drawing.Point(303, 78);
            this.lblNgayHHSD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayHHSD.Name = "lblNgayHHSD";
            this.lblNgayHHSD.Size = new System.Drawing.Size(76, 13);
            this.lblNgayHHSD.TabIndex = 11;
            this.lblNgayHHSD.Text = "Ngày hết HSD";
            // 
            // txtMaVt
            // 
            this.txtMaVt.AccessibleName = "MA_VT";
            this.txtMaVt.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaVt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaVt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaVt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaVt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaVt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaVt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaVt.LeaveColor = System.Drawing.Color.White;
            this.txtMaVt.Location = new System.Drawing.Point(426, 103);
            this.txtMaVt.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaVt.Name = "txtMaVt";
            this.txtMaVt.ReadOnly = true;
            this.txtMaVt.Size = new System.Drawing.Size(144, 20);
            this.txtMaVt.TabIndex = 14;
            this.txtMaVt.TabStop = false;
            // 
            // txtNewCode
            // 
            this.txtNewCode.AccessibleName = "";
            this.txtNewCode.BackColor = System.Drawing.Color.White;
            this.txtNewCode.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNewCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewCode.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNewCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewCode.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNewCode.HoverColor = System.Drawing.Color.Yellow;
            this.txtNewCode.LeaveColor = System.Drawing.Color.White;
            this.txtNewCode.Location = new System.Drawing.Point(134, 74);
            this.txtNewCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new System.Drawing.Size(144, 20);
            this.txtNewCode.TabIndex = 8;
            // 
            // lblMaVt
            // 
            this.lblMaVt.AccessibleDescription = "ADDEDITL00195";
            this.lblMaVt.AutoSize = true;
            this.lblMaVt.Location = new System.Drawing.Point(303, 107);
            this.lblMaVt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaVt.Name = "lblMaVt";
            this.lblMaVt.Size = new System.Drawing.Size(52, 13);
            this.lblMaVt.TabIndex = 13;
            this.lblMaVt.Text = "Mã vật tư";
            // 
            // lblHSDMoi
            // 
            this.lblHSDMoi.AccessibleDescription = "ADDEDITL00713";
            this.lblHSDMoi.AutoSize = true;
            this.lblHSDMoi.Location = new System.Drawing.Point(11, 107);
            this.lblHSDMoi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHSDMoi.Name = "lblHSDMoi";
            this.lblHSDMoi.Size = new System.Drawing.Size(95, 13);
            this.lblHSDMoi.TabIndex = 9;
            this.lblHSDMoi.Text = "Ngày hết HSD mới";
            // 
            // lblNewCode
            // 
            this.lblNewCode.AccessibleDescription = "ADDEDITL00711";
            this.lblNewCode.AutoSize = true;
            this.lblNewCode.Location = new System.Drawing.Point(11, 78);
            this.lblNewCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewCode.Name = "lblNewCode";
            this.lblNewCode.Size = new System.Drawing.Size(41, 13);
            this.lblNewCode.TabIndex = 7;
            this.lblNewCode.Text = "Mã mới";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ADDEDITL00709";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tùy chọn";
            // 
            // lblOldCode
            // 
            this.lblOldCode.AccessibleDescription = "ADDEDITL00710";
            this.lblOldCode.AutoSize = true;
            this.lblOldCode.Location = new System.Drawing.Point(11, 46);
            this.lblOldCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOldCode.Name = "lblOldCode";
            this.lblOldCode.Size = new System.Drawing.Size(59, 13);
            this.lblOldCode.TabIndex = 4;
            this.lblOldCode.Text = "Mã hiện tại";
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 161);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 161);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 6;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // LoHangChangeCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 218);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoHangChangeCode";
            this.Text = "Đổi mã lô - hạn sử dụng";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radDoiMaLoVaNgay;
        private System.Windows.Forms.RadioButton radDoiMaLo;
        private System.Windows.Forms.RadioButton radDoiNGay;
        private V6Controls.V6VvarTextBox txtOldCode;
        private V6Controls.V6ColorTextBox txtName;
        private V6Controls.V6ColorTextBox txtNewCode;
        private System.Windows.Forms.Label lblNewCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOldCode;
        private V6Controls.V6DateTimeColor txtNgayHHSDmoi;
        private System.Windows.Forms.Label lblHSDMoi;
        private V6Controls.V6DateTimeColor txtNgayHHSD;
        private System.Windows.Forms.Label lblNgayHHSD;
        private V6Controls.V6ColorTextBox txtMaVt;
        private System.Windows.Forms.Label lblMaVt;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
    }
}