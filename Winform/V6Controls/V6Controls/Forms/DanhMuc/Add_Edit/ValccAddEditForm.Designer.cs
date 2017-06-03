namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class ValccAddEditForm
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
            this.TxtTang_giam = new V6Controls.V6NumberTextBox();
            this.dateNgayGiam = new V6Controls.V6DateTimePick();
            this.txtLyDo = new V6Controls.V6ColorTextBox();
            this.txtSoCt = new V6Controls.V6VvarTextBox();
            this.txtMaGiamCC = new V6Controls.V6VvarTextBox();
            this.txtMaCC = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateNgay_pbc = new V6Controls.V6DateTimePick();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DateNgay_pbc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtTang_giam);
            this.groupBox1.Controls.Add(this.dateNgayGiam);
            this.groupBox1.Controls.Add(this.txtLyDo);
            this.groupBox1.Controls.Add(this.txtSoCt);
            this.groupBox1.Controls.Add(this.txtMaGiamCC);
            this.groupBox1.Controls.Add(this.txtMaCC);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(698, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtTang_giam
            // 
            this.TxtTang_giam.AccessibleName = "TANG_GIAM";
            this.TxtTang_giam.BackColor = System.Drawing.Color.White;
            this.TxtTang_giam.Carry = false;
            this.TxtTang_giam.EnableColorEffect = true;
            this.TxtTang_giam.EnableColorEffectOnMouseEnter = false;
            this.TxtTang_giam.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTang_giam.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTang_giam.LeaveColor = System.Drawing.Color.White;
            this.TxtTang_giam.LimitCharacters = null;
            this.TxtTang_giam.Location = new System.Drawing.Point(376, 25);
            this.TxtTang_giam.MaxNumDecimal = 0;
            this.TxtTang_giam.MaxNumLength = 0;
            this.TxtTang_giam.Name = "TxtTang_giam";
            this.TxtTang_giam.Size = new System.Drawing.Size(96, 23);
            this.TxtTang_giam.TabIndex = 11;
            this.TxtTang_giam.Text = "0,000";
            this.TxtTang_giam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTang_giam.GrayText = "";
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
            this.dateNgayGiam.Location = new System.Drawing.Point(171, 80);
            this.dateNgayGiam.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayGiam.Name = "dateNgayGiam";
            this.dateNgayGiam.Size = new System.Drawing.Size(135, 23);
            this.dateNgayGiam.TabIndex = 2;
            this.dateNgayGiam.TextTitle = null;
            // 
            // txtLyDo
            // 
            this.txtLyDo.AccessibleName = "LY_DO_GIAM";
            this.txtLyDo.Carry = false;
            this.txtLyDo.EnableColorEffect = true;
            this.txtLyDo.EnableColorEffectOnMouseEnter = false;
            this.txtLyDo.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLyDo.HoverColor = System.Drawing.Color.Yellow;
            this.txtLyDo.LeaveColor = System.Drawing.Color.White;
            this.txtLyDo.LimitCharacters = null;
            this.txtLyDo.Location = new System.Drawing.Point(171, 164);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(517, 23);
            this.txtLyDo.TabIndex = 5;
            this.txtLyDo.GrayText = "";
            // 
            // txtSoCt
            // 
            this.txtSoCt.AccessibleName = "SO_CT";
            this.txtSoCt.BrotherFields = null;
            this.txtSoCt.Carry = false;
            this.txtSoCt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSoCt.EnableColorEffect = true;
            this.txtSoCt.EnableColorEffectOnMouseEnter = false;
            this.txtSoCt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCt.LeaveColor = System.Drawing.Color.White;
            this.txtSoCt.LimitCharacters = null;
            this.txtSoCt.Location = new System.Drawing.Point(171, 136);
            this.txtSoCt.Name = "txtSoCt";
            this.txtSoCt.Size = new System.Drawing.Size(135, 23);
            this.txtSoCt.TabIndex = 4;
            this.txtSoCt.GrayText = "";
            // 
            // txtMaGiamCC
            // 
            this.txtMaGiamCC.AccessibleName = "MA_GIAM_CC";
            this.txtMaGiamCC.BrotherFields = null;
            this.txtMaGiamCC.Carry = false;
            this.txtMaGiamCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaGiamCC.CheckNotEmpty = true;
            this.txtMaGiamCC.EnableColorEffect = true;
            this.txtMaGiamCC.EnableColorEffectOnMouseEnter = false;
            this.txtMaGiamCC.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaGiamCC.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaGiamCC.LeaveColor = System.Drawing.Color.White;
            this.txtMaGiamCC.LimitCharacters = null;
            this.txtMaGiamCC.Location = new System.Drawing.Point(171, 54);
            this.txtMaGiamCC.Name = "txtMaGiamCC";
            this.txtMaGiamCC.Size = new System.Drawing.Size(135, 23);
            this.txtMaGiamCC.TabIndex = 1;
            this.txtMaGiamCC.GrayText = "";
            this.txtMaGiamCC.VVar = "MA_TG_CC";
            // 
            // txtMaCC
            // 
            this.txtMaCC.AccessibleName = "SO_THE_CC";
            this.txtMaCC.BrotherFields = null;
            this.txtMaCC.Carry = false;
            this.txtMaCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaCC.CheckNotEmpty = true;
            this.txtMaCC.EnableColorEffect = true;
            this.txtMaCC.EnableColorEffectOnMouseEnter = false;
            this.txtMaCC.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCC.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCC.LeaveColor = System.Drawing.Color.White;
            this.txtMaCC.LimitCharacters = null;
            this.txtMaCC.Location = new System.Drawing.Point(171, 25);
            this.txtMaCC.Name = "txtMaCC";
            this.txtMaCC.Size = new System.Drawing.Size(135, 23);
            this.txtMaCC.TabIndex = 0;
            this.txtMaCC.GrayText = "";
            this.txtMaCC.VVar = "so_the_cc";
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
            this.label7.Location = new System.Drawing.Point(25, 167);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Lý do giảm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Số chứng từ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 83);
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
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã công cụ";
            // 
            // DateNgay_pbc
            // 
            this.DateNgay_pbc.AccessibleName = "ngay_pbc";
            this.DateNgay_pbc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateNgay_pbc.BackColor = System.Drawing.Color.White;
            this.DateNgay_pbc.CustomFormat = "dd/MM/yyyy";
            this.DateNgay_pbc.EnterColor = System.Drawing.Color.PaleGreen;
            this.DateNgay_pbc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateNgay_pbc.HoverColor = System.Drawing.Color.Yellow;
            this.DateNgay_pbc.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DateNgay_pbc.LeaveColor = System.Drawing.Color.White;
            this.DateNgay_pbc.Location = new System.Drawing.Point(171, 108);
            this.DateNgay_pbc.Margin = new System.Windows.Forms.Padding(5);
            this.DateNgay_pbc.Name = "DateNgay_pbc";
            this.DateNgay_pbc.Size = new System.Drawing.Size(135, 23);
            this.DateNgay_pbc.TabIndex = 3;
            this.DateNgay_pbc.TextTitle = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ngày thôi phân bổ";
            // 
            // ValccAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ValccAddEditForm";
            this.Size = new System.Drawing.Size(712, 242);
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
        private V6VvarTextBox txtMaCC;
        private V6VvarTextBox txtSoCt;
        private V6VvarTextBox txtMaGiamCC;
        private System.Windows.Forms.Label label1;
        private V6ColorTextBox txtLyDo;
        private System.Windows.Forms.Label label7;
        private V6DateTimePick dateNgayGiam;
        private V6NumberTextBox TxtTang_giam;
        private V6DateTimePick DateNgay_pbc;
        private System.Windows.Forms.Label label4;
    }
}
