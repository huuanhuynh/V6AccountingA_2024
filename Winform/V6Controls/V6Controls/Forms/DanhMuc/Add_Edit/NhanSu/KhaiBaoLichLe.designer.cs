namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class KhaiBaoLichLe
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
            this.txtghi_chu = new V6Controls.V6ColorTextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtNgay = new V6Controls.V6DateTimePicker();
            this.txtten_kh = new V6Controls.V6LabelTextBox();
            this.txtMaCong = new V6Controls.V6VvarTextBox();
            this.txtSoGio = new V6Controls.V6NumberTextBox();
            this.lblSoGio = new V6Controls.V6Label();
            this.chkStatus = new V6Controls.V6CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblMaCong = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtghi_chu);
            this.groupBox1.Controls.Add(this.lblGhiChu);
            this.groupBox1.Controls.Add(this.txtNgay);
            this.groupBox1.Controls.Add(this.txtten_kh);
            this.groupBox1.Controls.Add(this.txtMaCong);
            this.groupBox1.Controls.Add(this.txtSoGio);
            this.groupBox1.Controls.Add(this.lblSoGio);
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.lblNgay);
            this.groupBox1.Controls.Add(this.lblMaCong);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Size = new System.Drawing.Size(480, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtghi_chu
            // 
            this.txtghi_chu.AccessibleName = "ghi_chu";
            this.txtghi_chu.BackColor = System.Drawing.SystemColors.Window;
            this.txtghi_chu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtghi_chu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtghi_chu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtghi_chu.HoverColor = System.Drawing.Color.Yellow;
            this.txtghi_chu.LeaveColor = System.Drawing.Color.White;
            this.txtghi_chu.Location = new System.Drawing.Point(136, 112);
            this.txtghi_chu.Name = "txtghi_chu";
            this.txtghi_chu.Size = new System.Drawing.Size(309, 23);
            this.txtghi_chu.TabIndex = 3;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AccessibleDescription = "ADDEDITL00036";
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(20, 113);
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(57, 17);
            this.lblGhiChu.TabIndex = 91;
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // txtNgay
            // 
            this.txtNgay.AccessibleName = "ngay";
            this.txtNgay.CustomFormat = "dd/MM/yyyy";
            this.txtNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay.LeaveColor = System.Drawing.Color.White;
            this.txtNgay.Location = new System.Drawing.Point(136, 19);
            this.txtNgay.Name = "txtNgay";
            this.txtNgay.Size = new System.Drawing.Size(134, 23);
            this.txtNgay.TabIndex = 0;
            // 
            // txtten_kh
            // 
            this.txtten_kh.AccessibleName = "ten_cong";
            this.txtten_kh.BackColor = System.Drawing.SystemColors.Control;
            this.txtten_kh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_kh.Location = new System.Drawing.Point(194, 53);
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
            this.txtMaCong.Location = new System.Drawing.Point(136, 50);
            this.txtMaCong.Name = "txtMaCong";
            this.txtMaCong.Size = new System.Drawing.Size(52, 23);
            this.txtMaCong.TabIndex = 1;
            this.txtMaCong.VVar = "MA_cong";
            this.txtMaCong.MouseLeave += new System.EventHandler(this.txtMaCong_MouseLeave);
            // 
            // txtSoGio
            // 
            this.txtSoGio.AccessibleName = "SO_GIO";
            this.txtSoGio.BackColor = System.Drawing.Color.White;
            this.txtSoGio.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoGio.DecimalPlaces = 2;
            this.txtSoGio.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoGio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoGio.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoGio.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoGio.LeaveColor = System.Drawing.Color.White;
            this.txtSoGio.Location = new System.Drawing.Point(136, 81);
            this.txtSoGio.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoGio.Name = "txtSoGio";
            this.txtSoGio.Size = new System.Drawing.Size(134, 23);
            this.txtSoGio.TabIndex = 2;
            this.txtSoGio.Text = "0,00";
            this.txtSoGio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoGio.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSoGio
            // 
            this.lblSoGio.AccessibleDescription = "ADDEDITL00417";
            this.lblSoGio.AutoSize = true;
            this.lblSoGio.Location = new System.Drawing.Point(20, 83);
            this.lblSoGio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoGio.Name = "lblSoGio";
            this.lblSoGio.Size = new System.Drawing.Size(102, 17);
            this.lblSoGio.TabIndex = 90;
            this.lblSoGio.Text = "Số giờ qui định";
            // 
            // chkStatus
            // 
            this.chkStatus.AccessibleDescription = "ADDEDITC00001";
            this.chkStatus.AccessibleName = "status";
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(136, 143);
            this.chkStatus.Margin = new System.Windows.Forms.Padding(4);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(107, 21);
            this.chkStatus.TabIndex = 4;
            this.chkStatus.Text = "Có sử dụng?";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleDescription = "ADDEDITL00022";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 143);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 17);
            this.lblStatus.TabIndex = 89;
            this.lblStatus.Text = "Trạng thái";
            // 
            // lblNgay
            // 
            this.lblNgay.AccessibleDescription = "ADDEDITL00217";
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(20, 23);
            this.lblNgay.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(41, 17);
            this.lblNgay.TabIndex = 88;
            this.lblNgay.Text = "Ngày";
            // 
            // lblMaCong
            // 
            this.lblMaCong.AccessibleDescription = "ADDEDITL00416";
            this.lblMaCong.AutoSize = true;
            this.lblMaCong.Location = new System.Drawing.Point(20, 53);
            this.lblMaCong.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaCong.Name = "lblMaCong";
            this.lblMaCong.Size = new System.Drawing.Size(103, 17);
            this.lblMaCong.TabIndex = 87;
            this.lblMaCong.Text = "Mã công đi làm";
            // 
            // KhaiBaoLichLe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KhaiBaoLichLe";
            this.Size = new System.Drawing.Size(498, 222);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6VvarTextBox txtMaCong;
        private V6NumberTextBox txtSoGio;
        private V6Label lblSoGio;
        private V6CheckBox chkStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblMaCong;
        private V6LabelTextBox txtten_kh;
        private V6DateTimePicker txtNgay;
        private System.Windows.Forms.Label lblGhiChu;
        private V6ColorTextBox txtghi_chu;
    }
}
