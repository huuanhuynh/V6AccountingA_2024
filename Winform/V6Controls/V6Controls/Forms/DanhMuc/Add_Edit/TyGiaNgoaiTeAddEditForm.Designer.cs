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
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenNt = new V6Controls.V6VvarTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtTy_gia = new V6Controls.V6NumberTextBox();
            this.TxtMa_nt = new V6Controls.V6VvarTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtNgay_ct);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTenNt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtTy_gia);
            this.groupBox1.Controls.Add(this.TxtMa_nt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Size = new System.Drawing.Size(705, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtNgay_ct
            // 
            this.TxtNgay_ct.AccessibleName = "NGAY_CT";
            this.TxtNgay_ct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNgay_ct.BackColor = System.Drawing.Color.White;
            this.TxtNgay_ct.CustomFormat = "dd/MM/yyyy";
            this.TxtNgay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtNgay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtNgay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.TxtNgay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TxtNgay_ct.LeaveColor = System.Drawing.Color.White;
            this.TxtNgay_ct.Location = new System.Drawing.Point(175, 15);
            this.TxtNgay_ct.Margin = new System.Windows.Forms.Padding(5);
            this.TxtNgay_ct.Name = "TxtNgay_ct";
            this.TxtNgay_ct.Size = new System.Drawing.Size(140, 23);
            this.TxtNgay_ct.TabIndex = 99;
            this.TxtNgay_ct.TextTitle = null;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(175, 108);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 109);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Trạng Thái";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 98;
            this.label2.Text = "Ngày chứng từ";
            // 
            // txtTenNt
            // 
            this.txtTenNt.AccessibleName = "TEN_NT";
            this.txtTenNt.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTenNt.CheckNotEmpty = true;
            this.txtTenNt.Enabled = false;
            this.txtTenNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenNt.LeaveColor = System.Drawing.Color.White;
            this.txtTenNt.Location = new System.Drawing.Point(321, 45);
            this.txtTenNt.Name = "txtTenNt";
            this.txtTenNt.ReadOnly = true;
            this.txtTenNt.Size = new System.Drawing.Size(372, 23);
            this.txtTenNt.TabIndex = 2;
            this.txtTenNt.VVar = "DVT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Tỷ giá";
            // 
            // TxtTy_gia
            // 
            this.TxtTy_gia.AccessibleName = "TY_GIA";
            this.TxtTy_gia.BackColor = System.Drawing.Color.White;
            this.TxtTy_gia.DecimalPlaces = 5;
            this.TxtTy_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTy_gia.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTy_gia.LeaveColor = System.Drawing.Color.White;
            this.TxtTy_gia.Location = new System.Drawing.Point(175, 75);
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
            327680});
            // 
            // TxtMa_nt
            // 
            this.TxtMa_nt.AccessibleName = "MA_NT";
            this.TxtMa_nt.BrotherFields = "TEN_NT";
            this.TxtMa_nt.CheckNotEmpty = true;
            this.TxtMa_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_nt.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_nt.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_nt.Location = new System.Drawing.Point(175, 45);
            this.TxtMa_nt.Name = "TxtMa_nt";
            this.TxtMa_nt.Size = new System.Drawing.Size(140, 23);
            this.TxtMa_nt.TabIndex = 1;
            this.TxtMa_nt.VVar = "ma_nt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã ngoại tệ";
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
            this.Size = new System.Drawing.Size(717, 148);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label label8;
        private V6VvarTextBox TxtMa_nt;
        private System.Windows.Forms.Label label4;
        private V6NumberTextBox TxtTy_gia;
        private V6VvarTextBox txtTenNt;
        private System.Windows.Forms.Label label2;
        private V6DateTimePicker TxtNgay_ct;



    }
}
