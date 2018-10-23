namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class ValtsAddEditForm
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
            this.DateNgay_khc = new V6Controls.V6DateTimePick();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtTang_giam = new V6Controls.V6NumberTextBox();
            this.dateNgayGiam = new V6Controls.V6DateTimePick();
            this.txtLyDo = new V6Controls.V6ColorTextBox();
            this.txtSoCt = new V6Controls.V6VvarTextBox();
            this.txtMaGiamTS = new V6Controls.V6VvarTextBox();
            this.txtMaTS = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenTS = new V6Controls.V6LabelTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTenTS);
            this.groupBox1.Controls.Add(this.DateNgay_khc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtTang_giam);
            this.groupBox1.Controls.Add(this.dateNgayGiam);
            this.groupBox1.Controls.Add(this.txtLyDo);
            this.groupBox1.Controls.Add(this.txtSoCt);
            this.groupBox1.Controls.Add(this.txtMaGiamTS);
            this.groupBox1.Controls.Add(this.txtMaTS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(698, 222);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // DateNgay_khc
            // 
            this.DateNgay_khc.AccessibleName = "ngay_khc";
            this.DateNgay_khc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateNgay_khc.BackColor = System.Drawing.Color.White;
            this.DateNgay_khc.CustomFormat = "dd/MM/yyyy";
            this.DateNgay_khc.EnterColor = System.Drawing.Color.PaleGreen;
            this.DateNgay_khc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateNgay_khc.HoverColor = System.Drawing.Color.Yellow;
            this.DateNgay_khc.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DateNgay_khc.LeaveColor = System.Drawing.Color.White;
            this.DateNgay_khc.Location = new System.Drawing.Point(171, 108);
            this.DateNgay_khc.Margin = new System.Windows.Forms.Padding(5);
            this.DateNgay_khc.Name = "DateNgay_khc";
            this.DateNgay_khc.Size = new System.Drawing.Size(135, 23);
            this.DateNgay_khc.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Ngày thôi khấu hao";
            // 
            // TxtTang_giam
            // 
            this.TxtTang_giam.AccessibleName = "TANG_GIAM";
            this.TxtTang_giam.BackColor = System.Drawing.Color.White;
            this.TxtTang_giam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTang_giam.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTang_giam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTang_giam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTang_giam.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTang_giam.LeaveColor = System.Drawing.Color.White;
            this.TxtTang_giam.Location = new System.Drawing.Point(592, 130);
            this.TxtTang_giam.Name = "TxtTang_giam";
            this.TxtTang_giam.Size = new System.Drawing.Size(96, 23);
            this.TxtTang_giam.TabIndex = 10;
            this.TxtTang_giam.Text = "0,000";
            this.TxtTang_giam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTang_giam.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            this.TxtTang_giam.Visible = false;
            // 
            // dateNgayGiam
            // 
            this.dateNgayGiam.AccessibleName = "ngay_giam";
            this.dateNgayGiam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgayGiam.BackColor = System.Drawing.Color.White;
            this.dateNgayGiam.CustomFormat = "dd/MM/yyyy";
            this.dateNgayGiam.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayGiam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayGiam.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayGiam.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayGiam.LeaveColor = System.Drawing.Color.White;
            this.dateNgayGiam.Location = new System.Drawing.Point(171, 81);
            this.dateNgayGiam.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayGiam.Name = "dateNgayGiam";
            this.dateNgayGiam.Size = new System.Drawing.Size(135, 23);
            this.dateNgayGiam.TabIndex = 2;
            // 
            // txtLyDo
            // 
            this.txtLyDo.AccessibleName = "LY_DO_GIAM";
            this.txtLyDo.BackColor = System.Drawing.SystemColors.Window;
            this.txtLyDo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLyDo.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLyDo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLyDo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLyDo.HoverColor = System.Drawing.Color.Yellow;
            this.txtLyDo.LeaveColor = System.Drawing.Color.White;
            this.txtLyDo.Location = new System.Drawing.Point(171, 161);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(517, 23);
            this.txtLyDo.TabIndex = 5;
            // 
            // txtSoCt
            // 
            this.txtSoCt.AccessibleName = "SO_CT";
            this.txtSoCt.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoCt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSoCt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCt.LeaveColor = System.Drawing.Color.White;
            this.txtSoCt.Location = new System.Drawing.Point(171, 134);
            this.txtSoCt.Name = "txtSoCt";
            this.txtSoCt.Size = new System.Drawing.Size(135, 23);
            this.txtSoCt.TabIndex = 4;
            // 
            // txtMaGiamTS
            // 
            this.txtMaGiamTS.AccessibleName = "MA_GIAM_TS";
            this.txtMaGiamTS.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaGiamTS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaGiamTS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaGiamTS.CheckNotEmpty = true;
            this.txtMaGiamTS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaGiamTS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaGiamTS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaGiamTS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaGiamTS.LeaveColor = System.Drawing.Color.White;
            this.txtMaGiamTS.Location = new System.Drawing.Point(171, 54);
            this.txtMaGiamTS.Name = "txtMaGiamTS";
            this.txtMaGiamTS.Size = new System.Drawing.Size(135, 23);
            this.txtMaGiamTS.TabIndex = 1;
            this.txtMaGiamTS.VVar = "MA_TG_TS";
            // 
            // txtMaTS
            // 
            this.txtMaTS.AccessibleName = "SO_THE_TS";
            this.txtMaTS.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaTS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaTS.BrotherFields = "TEN_TS";
            this.txtMaTS.BrotherFields2 = "TEN_TS2";
            this.txtMaTS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaTS.CheckNotEmpty = true;
            this.txtMaTS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaTS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaTS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaTS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaTS.LeaveColor = System.Drawing.Color.White;
            this.txtMaTS.Location = new System.Drawing.Point(171, 25);
            this.txtMaTS.Name = "txtMaTS";
            this.txtMaTS.Size = new System.Drawing.Size(135, 23);
            this.txtMaTS.TabIndex = 0;
            this.txtMaTS.UseChangeTextOnSetFormData = true;
            this.txtMaTS.VVar = "so_the_ts";
            // 
            // label3
            // 
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã giảm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 164);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Lý do giảm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 136);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Số chứng từ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày giảm";
            // 
            // label2
            // 
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã tài sản";
            // 
            // txtTenTS
            // 
            this.txtTenTS.AccessibleName = "TEN_TS";
            this.txtTenTS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtTenTS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTenTS.Location = new System.Drawing.Point(312, 28);
            this.txtTenTS.Name = "txtTenTS";
            this.txtTenTS.ReadOnly = true;
            this.txtTenTS.Size = new System.Drawing.Size(376, 16);
            this.txtTenTS.TabIndex = 12;
            this.txtTenTS.Tag = "readonly";
            // 
            // ValtsAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ValtsAddEditForm";
            this.Size = new System.Drawing.Size(712, 234);
            this.Load += new System.EventHandler(this.Algia2AddEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private V6VvarTextBox txtMaTS;
        private V6VvarTextBox txtSoCt;
        private V6VvarTextBox txtMaGiamTS;
        private System.Windows.Forms.Label label1;
        private V6ColorTextBox txtLyDo;
        private System.Windows.Forms.Label label7;
        private V6DateTimePick dateNgayGiam;
        private V6NumberTextBox TxtTang_giam;
        private V6DateTimePick DateNgay_khc;
        private System.Windows.Forms.Label label4;
        private V6LabelTextBox txtTenTS;
    }
}
