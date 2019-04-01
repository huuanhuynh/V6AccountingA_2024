namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class Algia2AddEditForm
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
            this.dateNgay_ban = new V6Controls.V6DateTimePicker();
            this.txtGiaBan = new V6Controls.NumberGiaNt();
            this.dateNgayHetHieuLuc = new V6Controls.V6DateTimeColor();
            this.txtMaNT = new V6Controls.V6VvarTextBox();
            this.txtDVT = new V6Controls.V6VvarTextBox();
            this.txtMaVt = new V6Controls.V6VvarTextBox();
            this.txtMaKH = new V6Controls.V6VvarTextBox();
            this.txtMaGia = new V6Controls.V6VvarTextBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNgayHHL = new System.Windows.Forms.Label();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblMaNT = new System.Windows.Forms.Label();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.lblDVT = new System.Windows.Forms.Label();
            this.lblMaVT = new System.Windows.Forms.Label();
            this.lblMaGia = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dateNgay_ban);
            this.groupBox1.Controls.Add(this.txtGiaBan);
            this.groupBox1.Controls.Add(this.dateNgayHetHieuLuc);
            this.groupBox1.Controls.Add(this.txtMaNT);
            this.groupBox1.Controls.Add(this.txtDVT);
            this.groupBox1.Controls.Add(this.txtMaVt);
            this.groupBox1.Controls.Add(this.txtMaKH);
            this.groupBox1.Controls.Add(this.txtMaGia);
            this.groupBox1.Controls.Add(this.lblMaKH);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.lblNgayHHL);
            this.groupBox1.Controls.Add(this.lblGiaBan);
            this.groupBox1.Controls.Add(this.lblMaNT);
            this.groupBox1.Controls.Add(this.lblNgayBan);
            this.groupBox1.Controls.Add(this.lblDVT);
            this.groupBox1.Controls.Add(this.lblMaVT);
            this.groupBox1.Controls.Add(this.lblMaGia);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(698, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dateNgay_ban
            // 
            this.dateNgay_ban.AccessibleName = "ngay_ban";
            this.dateNgay_ban.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ban.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ban.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ban.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ban.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ban.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ban.Location = new System.Drawing.Point(171, 141);
            this.dateNgay_ban.Name = "dateNgay_ban";
            this.dateNgay_ban.Size = new System.Drawing.Size(135, 23);
            this.dateNgay_ban.TabIndex = 9;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.AccessibleName = "gia_nt2";
            this.txtGiaBan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaBan.BackColor = System.Drawing.Color.White;
            this.txtGiaBan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGiaBan.DecimalPlaces = 0;
            this.txtGiaBan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGiaBan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGiaBan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGiaBan.HoverColor = System.Drawing.Color.Yellow;
            this.txtGiaBan.LeaveColor = System.Drawing.Color.White;
            this.txtGiaBan.Location = new System.Drawing.Point(171, 201);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(5);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(135, 23);
            this.txtGiaBan.TabIndex = 13;
            this.txtGiaBan.Text = "0";
            this.txtGiaBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiaBan.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dateNgayHetHieuLuc
            // 
            this.dateNgayHetHieuLuc.AccessibleName = "ngay_hhl";
            this.dateNgayHetHieuLuc.BackColor = System.Drawing.Color.White;
            this.dateNgayHetHieuLuc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgayHetHieuLuc.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayHetHieuLuc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayHetHieuLuc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateNgayHetHieuLuc.GrayText = null;
            this.dateNgayHetHieuLuc.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayHetHieuLuc.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayHetHieuLuc.LeaveColor = System.Drawing.Color.White;
            this.dateNgayHetHieuLuc.Location = new System.Drawing.Point(171, 265);
            this.dateNgayHetHieuLuc.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayHetHieuLuc.Name = "dateNgayHetHieuLuc";
            this.dateNgayHetHieuLuc.Size = new System.Drawing.Size(135, 23);
            this.dateNgayHetHieuLuc.StringValue = "__/__/____";
            this.dateNgayHetHieuLuc.TabIndex = 17;
            this.dateNgayHetHieuLuc.Text = "__/__/____";
            // 
            // txtMaNT
            // 
            this.txtMaNT.AccessibleName = "ma_nt";
            this.txtMaNT.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaNT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaNT.CheckNotEmpty = true;
            this.txtMaNT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNT.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNT.LeaveColor = System.Drawing.Color.White;
            this.txtMaNT.Location = new System.Drawing.Point(171, 170);
            this.txtMaNT.Name = "txtMaNT";
            this.txtMaNT.Size = new System.Drawing.Size(135, 23);
            this.txtMaNT.TabIndex = 11;
            this.txtMaNT.VVar = "ma_nt";
            // 
            // txtDVT
            // 
            this.txtDVT.AccessibleName = "DVT";
            this.txtDVT.BackColor = System.Drawing.SystemColors.Window;
            this.txtDVT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDVT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDVT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDVT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDVT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDVT.HoverColor = System.Drawing.Color.Yellow;
            this.txtDVT.LeaveColor = System.Drawing.Color.White;
            this.txtDVT.Location = new System.Drawing.Point(171, 112);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(135, 23);
            this.txtDVT.TabIndex = 7;
            this.txtDVT.VVar = "dvt1";
            // 
            // txtMaVt
            // 
            this.txtMaVt.AccessibleName = "ma_vt";
            this.txtMaVt.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaVt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaVt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaVt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaVt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaVt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaVt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaVt.LeaveColor = System.Drawing.Color.White;
            this.txtMaVt.Location = new System.Drawing.Point(171, 83);
            this.txtMaVt.Name = "txtMaVt";
            this.txtMaVt.Size = new System.Drawing.Size(135, 23);
            this.txtMaVt.TabIndex = 5;
            this.txtMaVt.VVar = "ma_vt";
            this.txtMaVt.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMaVt_V6LostFocus);
            // 
            // txtMaKH
            // 
            this.txtMaKH.AccessibleName = "ma_kh";
            this.txtMaKH.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaKH.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaKH.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKH.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKH.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKH.LeaveColor = System.Drawing.Color.White;
            this.txtMaKH.Location = new System.Drawing.Point(171, 54);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(135, 23);
            this.txtMaKH.TabIndex = 3;
            this.txtMaKH.VVar = "ma_kh";
            // 
            // txtma_gia
            // 
            this.txtMaGia.AccessibleName = "ma_gia";
            this.txtMaGia.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaGia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaGia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaGia.CheckNotEmpty = true;
            this.txtMaGia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaGia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaGia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaGia.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaGia.LeaveColor = System.Drawing.Color.White;
            this.txtMaGia.Location = new System.Drawing.Point(171, 25);
            this.txtMaGia.Name = "txtMaGia";
            this.txtMaGia.Size = new System.Drawing.Size(135, 23);
            this.txtMaGia.TabIndex = 1;
            this.txtMaGia.VVar = "ma_gia";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AccessibleDescription = "ADDEDITL00472";
            this.lblMaKH.AccessibleName = "";
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(25, 57);
            this.lblMaKH.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(69, 17);
            this.lblMaKH.TabIndex = 2;
            this.lblMaKH.Text = "Mã khách";
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "ADDEDITC00001";
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(171, 234);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleDescription = "ADDEDITL00022";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(25, 234);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 17);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Trạng thái";
            // 
            // lblNgayHHL
            // 
            this.lblNgayHHL.AccessibleDescription = "ADDEDITL00409";
            this.lblNgayHHL.AutoSize = true;
            this.lblNgayHHL.Location = new System.Drawing.Point(28, 268);
            this.lblNgayHHL.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblNgayHHL.Name = "lblNgayHHL";
            this.lblNgayHHL.Size = new System.Drawing.Size(118, 17);
            this.lblNgayHHL.TabIndex = 16;
            this.lblNgayHHL.Text = "Ngày hết hiệu lực";
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AccessibleDescription = "ADDEDITL00484";
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Location = new System.Drawing.Point(28, 204);
            this.lblGiaBan.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(58, 17);
            this.lblGiaBan.TabIndex = 12;
            this.lblGiaBan.Text = "Giá bán";
            // 
            // lblMaNT
            // 
            this.lblMaNT.AccessibleDescription = "ADDEDITL00127";
            this.lblMaNT.AutoSize = true;
            this.lblMaNT.Location = new System.Drawing.Point(28, 173);
            this.lblMaNT.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaNT.Name = "lblMaNT";
            this.lblMaNT.Size = new System.Drawing.Size(50, 17);
            this.lblMaNT.TabIndex = 10;
            this.lblMaNT.Text = "Mã NT";
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.AccessibleDescription = "ADDEDITL00483";
            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.Location = new System.Drawing.Point(25, 146);
            this.lblNgayBan.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(69, 17);
            this.lblNgayBan.TabIndex = 8;
            this.lblNgayBan.Text = "Ngày bán";
            // 
            // lblDVT
            // 
            this.lblDVT.AccessibleDescription = "ADDEDITL00042";
            this.lblDVT.AutoSize = true;
            this.lblDVT.Location = new System.Drawing.Point(25, 115);
            this.lblDVT.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblDVT.Name = "lblDVT";
            this.lblDVT.Size = new System.Drawing.Size(75, 17);
            this.lblDVT.TabIndex = 6;
            this.lblDVT.Text = "Đơn vị tính";
            // 
            // lblMaVT
            // 
            this.lblMaVT.AccessibleDescription = "ADDEDITL00195";
            this.lblMaVT.AutoSize = true;
            this.lblMaVT.Location = new System.Drawing.Point(25, 86);
            this.lblMaVT.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaVT.Name = "lblMaVT";
            this.lblMaVT.Size = new System.Drawing.Size(66, 17);
            this.lblMaVT.TabIndex = 4;
            this.lblMaVT.Text = "Mã vật tư";
            // 
            // lblMaGia
            // 
            this.lblMaGia.AccessibleDescription = "ADDEDITL00457";
            this.lblMaGia.AccessibleName = "";
            this.lblMaGia.AutoSize = true;
            this.lblMaGia.Location = new System.Drawing.Point(25, 28);
            this.lblMaGia.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMaGia.Name = "lblMaGia";
            this.lblMaGia.Size = new System.Drawing.Size(50, 17);
            this.lblMaGia.TabIndex = 0;
            this.lblMaGia.Text = "Mã giá";
            // 
            // Algia2AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Algia2AddEditForm";
            this.Size = new System.Drawing.Size(712, 322);
            this.Load += new System.EventHandler(this.Algia2AddEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMaKH;
        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMaVT;
        private System.Windows.Forms.Label lblMaGia;
        private V6VvarTextBox txtMaGia;
        private V6VvarTextBox txtDVT;
        private V6VvarTextBox txtMaVt;
        private V6VvarTextBox txtMaKH;
        private System.Windows.Forms.Label lblDVT;
        private V6DateTimeColor dateNgayHetHieuLuc;
        private System.Windows.Forms.Label lblNgayHHL;
        private NumberGiaNt txtGiaBan;
        private V6DateTimePicker dateNgay_ban;
        private V6VvarTextBox txtMaNT;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblMaNT;
        private System.Windows.Forms.Label lblNgayBan;
    }
}
