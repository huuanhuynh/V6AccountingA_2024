namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class TyGiaNgoaiTeAddEditForm
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
            this.TxtNgay_ct = new V6Controls.V6DateTimePicker();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNgayCT = new System.Windows.Forms.Label();
            this.txtTenNt = new V6Controls.V6VvarTextBox();
            this.lblTyGia = new System.Windows.Forms.Label();
            this.TxtTy_gia = new V6Controls.V6NumberTextBox();
            this.txtMa_NT = new V6Controls.V6VvarTextBox();
            this.lblMa = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtNgay_ct);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.lblNgayCT);
            this.groupBox1.Controls.Add(this.txtTenNt);
            this.groupBox1.Controls.Add(this.lblTyGia);
            this.groupBox1.Controls.Add(this.TxtTy_gia);
            this.groupBox1.Controls.Add(this.txtMa_NT);
            this.groupBox1.Controls.Add(this.lblMa);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Size = new System.Drawing.Size(705, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtNgay_ct
            // 
            this.TxtNgay_ct.AccessibleName = "NGAY_CT";
            this.TxtNgay_ct.BackColor = System.Drawing.Color.White;
            this.TxtNgay_ct.CustomFormat = "dd/MM/yyyy";
            this.TxtNgay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtNgay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtNgay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.TxtNgay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TxtNgay_ct.LeaveColor = System.Drawing.Color.White;
            this.TxtNgay_ct.Location = new System.Drawing.Point(175, 18);
            this.TxtNgay_ct.Margin = new System.Windows.Forms.Padding(5);
            this.TxtNgay_ct.Name = "TxtNgay_ct";
            this.TxtNgay_ct.Size = new System.Drawing.Size(140, 23);
            this.TxtNgay_ct.TabIndex = 99;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "ADDEDITC00001";
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(175, 111);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleDescription = "ADDEDITL00022";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(17, 110);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(78, 17);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Trạng Thái";
            // 
            // lblNgayCT
            // 
            this.lblNgayCT.AccessibleDescription = "ADDEDITL00206";
            this.lblNgayCT.AutoSize = true;
            this.lblNgayCT.Location = new System.Drawing.Point(17, 23);
            this.lblNgayCT.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNgayCT.Name = "lblNgayCT";
            this.lblNgayCT.Size = new System.Drawing.Size(100, 17);
            this.lblNgayCT.TabIndex = 98;
            this.lblNgayCT.Text = "Ngày chứng từ";
            // 
            // txtTenNt
            // 
            this.txtTenNt.AccessibleName = "TEN_NT";
            this.txtTenNt.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTenNt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenNt.CheckNotEmpty = true;
            this.txtTenNt.Enabled = false;
            this.txtTenNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenNt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenNt.LeaveColor = System.Drawing.Color.White;
            this.txtTenNt.Location = new System.Drawing.Point(321, 49);
            this.txtTenNt.Name = "txtTenNt";
            this.txtTenNt.ReadOnly = true;
            this.txtTenNt.Size = new System.Drawing.Size(372, 23);
            this.txtTenNt.TabIndex = 2;
            this.txtTenNt.TabStop = false;
            this.txtTenNt.VVar = "DVT";
            // 
            // lblTyGia
            // 
            this.lblTyGia.AccessibleDescription = "ADDEDITL00212";
            this.lblTyGia.AutoSize = true;
            this.lblTyGia.Location = new System.Drawing.Point(17, 81);
            this.lblTyGia.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblTyGia.Name = "lblTyGia";
            this.lblTyGia.Size = new System.Drawing.Size(47, 17);
            this.lblTyGia.TabIndex = 23;
            this.lblTyGia.Text = "Tỷ giá";
            // 
            // TxtTy_gia
            // 
            this.TxtTy_gia.AccessibleName = "TY_GIA";
            this.TxtTy_gia.BackColor = System.Drawing.Color.White;
            this.TxtTy_gia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTy_gia.DecimalPlaces = 5;
            this.TxtTy_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTy_gia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTy_gia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTy_gia.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTy_gia.LeaveColor = System.Drawing.Color.White;
            this.TxtTy_gia.Location = new System.Drawing.Point(175, 80);
            this.TxtTy_gia.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTy_gia.Name = "TxtTy_gia";
            this.TxtTy_gia.Size = new System.Drawing.Size(140, 23);
            this.TxtTy_gia.TabIndex = 3;
            this.TxtTy_gia.Text = "0,00000";
            this.TxtTy_gia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTy_gia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMa_NT
            // 
            this.txtMa_NT.AccessibleName = "MA_NT";
            this.txtMa_NT.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa_NT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_NT.BrotherFields = "TEN_NT";
            this.txtMa_NT.CheckNotEmpty = true;
            this.txtMa_NT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_NT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_NT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_NT.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_NT.LeaveColor = System.Drawing.Color.White;
            this.txtMa_NT.Location = new System.Drawing.Point(175, 49);
            this.txtMa_NT.Name = "txtMa_NT";
            this.txtMa_NT.Size = new System.Drawing.Size(140, 23);
            this.txtMa_NT.TabIndex = 1;
            this.txtMa_NT.VVar = "ma_nt";
            // 
            // lblMa
            // 
            this.lblMa.AccessibleDescription = "ADDEDITL00127";
            this.lblMa.AutoSize = true;
            this.lblMa.Location = new System.Drawing.Point(17, 52);
            this.lblMa.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(82, 17);
            this.lblMa.TabIndex = 0;
            this.lblMa.Text = "Mã ngoại tệ";
            // 
            // TyGiaNgoaiTeAddEditForm
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TyGiaNgoaiTeAddEditForm";
            this.Size = new System.Drawing.Size(717, 153);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMa;
        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label lblStatus;
        private V6VvarTextBox txtMa_NT;
        private System.Windows.Forms.Label lblTyGia;
        private V6NumberTextBox TxtTy_gia;
        private V6VvarTextBox txtTenNt;
        private System.Windows.Forms.Label lblNgayCT;
        private V6DateTimePicker TxtNgay_ct;



    }
}
