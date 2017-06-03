namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class KhaiBaoNgayNghiLe
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
            this.txtten_kh = new V6Controls.V6LabelTextBox();
            this.txtMaCong = new V6Controls.V6VvarTextBox();
            this.TXTHE_SO = new V6Controls.V6NumberTextBox();
            this.TXTSO_GIO = new V6Controls.V6NumberTextBox();
            this.txtLoaiNgay = new V6Controls.V6ComboBox();
            this.v6Label6 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNgay = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtNgay);
            this.groupBox1.Controls.Add(this.txtten_kh);
            this.groupBox1.Controls.Add(this.txtMaCong);
            this.groupBox1.Controls.Add(this.TXTHE_SO);
            this.groupBox1.Controls.Add(this.TXTSO_GIO);
            this.groupBox1.Controls.Add(this.txtLoaiNgay);
            this.groupBox1.Controls.Add(this.v6Label6);
            this.groupBox1.Controls.Add(this.v6Label7);
            this.groupBox1.Controls.Add(this.v6Label8);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Size = new System.Drawing.Size(519, 264);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            // 
            // txtten_kh
            // 
            this.txtten_kh.AccessibleName = "ten_cong";
            this.txtten_kh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_kh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_kh.Location = new System.Drawing.Point(245, 50);
            this.txtten_kh.Name = "txtten_kh";
            this.txtten_kh.ReadOnly = true;
            this.txtten_kh.Size = new System.Drawing.Size(251, 16);
            this.txtten_kh.TabIndex = 2;
            this.txtten_kh.TabStop = false;
            this.txtten_kh.Tag = "readonly";
            // 
            // txtMaCong
            // 
            this.txtMaCong.AccessibleName = "ma_cong";
            this.txtMaCong.BackColor = System.Drawing.Color.White;
            this.txtMaCong.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCong.BrotherFields = "ten_cong,he_so";
            this.txtMaCong.CheckNotEmpty = true;
            this.txtMaCong.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCong.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCong.LeaveColor = System.Drawing.Color.White;
            this.txtMaCong.Location = new System.Drawing.Point(187, 47);
            this.txtMaCong.Name = "txtMaCong";
            this.txtMaCong.Size = new System.Drawing.Size(52, 23);
            this.txtMaCong.TabIndex = 1;
            this.txtMaCong.VVar = "MA_cong";
            // 
            // TXTHE_SO
            // 
            this.TXTHE_SO.AccessibleName = "HE_SO";
            this.TXTHE_SO.BackColor = System.Drawing.Color.White;
            this.TXTHE_SO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TXTHE_SO.DecimalPlaces = 2;
            this.TXTHE_SO.EnterColor = System.Drawing.Color.PaleGreen;
            this.TXTHE_SO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TXTHE_SO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TXTHE_SO.HoverColor = System.Drawing.Color.Yellow;
            this.TXTHE_SO.LeaveColor = System.Drawing.Color.White;
            this.TXTHE_SO.Location = new System.Drawing.Point(187, 131);
            this.TXTHE_SO.Margin = new System.Windows.Forms.Padding(4);
            this.TXTHE_SO.Name = "TXTHE_SO";
            this.TXTHE_SO.Size = new System.Drawing.Size(134, 23);
            this.TXTHE_SO.TabIndex = 5;
            this.TXTHE_SO.Text = "0,00";
            this.TXTHE_SO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TXTHE_SO.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // TXTSO_GIO
            // 
            this.TXTSO_GIO.AccessibleName = "SO_GIO";
            this.TXTSO_GIO.BackColor = System.Drawing.Color.White;
            this.TXTSO_GIO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TXTSO_GIO.DecimalPlaces = 2;
            this.TXTSO_GIO.EnterColor = System.Drawing.Color.PaleGreen;
            this.TXTSO_GIO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TXTSO_GIO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TXTSO_GIO.HoverColor = System.Drawing.Color.Yellow;
            this.TXTSO_GIO.LeaveColor = System.Drawing.Color.White;
            this.TXTSO_GIO.Location = new System.Drawing.Point(187, 75);
            this.TXTSO_GIO.Margin = new System.Windows.Forms.Padding(4);
            this.TXTSO_GIO.Name = "TXTSO_GIO";
            this.TXTSO_GIO.Size = new System.Drawing.Size(134, 23);
            this.TXTSO_GIO.TabIndex = 3;
            this.TXTSO_GIO.Text = "0,00";
            this.TXTSO_GIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TXTSO_GIO.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtLoaiNgay
            // 
            this.txtLoaiNgay.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoaiNgay.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoaiNgay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoaiNgay.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoaiNgay.FormattingEnabled = true;
            this.txtLoaiNgay.Location = new System.Drawing.Point(187, 102);
            this.txtLoaiNgay.Name = "txtLoaiNgay";
            this.txtLoaiNgay.Size = new System.Drawing.Size(276, 24);
            this.txtLoaiNgay.TabIndex = 4;
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(30, 78);
            this.v6Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(102, 17);
            this.v6Label6.TabIndex = 90;
            this.v6Label6.Text = "Số giờ qui định";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(30, 105);
            this.v6Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(70, 17);
            this.v6Label7.TabIndex = 91;
            this.v6Label7.Text = "Loại ngày";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(30, 134);
            this.v6Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(86, 17);
            this.v6Label8.TabIndex = 92;
            this.v6Label8.Text = "Hệ số đi làm";
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(187, 223);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Có sử dụng?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "status";
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(30, 224);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 17);
            this.label15.TabIndex = 89;
            this.label15.Text = "Trạng thái";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 88;
            this.label1.Text = "Ngày";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 87;
            this.label5.Text = "Mã công đi làm";
            // 
            // txtNgay
            // 
            this.txtNgay.AccessibleName = "ngay";
            this.txtNgay.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgay.CustomFormat = "dd/MM/yyyy";
            this.txtNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay.LeaveColor = System.Drawing.Color.White;
            this.txtNgay.Location = new System.Drawing.Point(187, 18);
            this.txtNgay.Name = "txtNgay";
            this.txtNgay.ReadOnly = false;
            this.txtNgay.Size = new System.Drawing.Size(134, 23);
            this.txtNgay.TabIndex = 0;
            this.txtNgay.TextTitle = null;
            this.txtNgay.Value = new System.DateTime(2017, 3, 20, 0, 0, 0, 0);
            // 
            // KhaiBaoNgayNghiLe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KhaiBaoNgayNghiLe";
            this.Size = new System.Drawing.Size(537, 278);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6VvarTextBox txtMaCong;
        private V6NumberTextBox TXTHE_SO;
        private V6NumberTextBox TXTSO_GIO;
        private V6ComboBox txtLoaiNgay;
        private V6Label v6Label6;
        private V6Label v6Label7;
        private V6Label v6Label8;
        private V6CheckBox checkBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private V6LabelTextBox txtten_kh;
        private V6DateTimePick txtNgay;
    }
}
