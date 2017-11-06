namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLTCD
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.chk_Luy_ke = new V6Controls.V6CheckBox();
            this.cboMaubc = new V6Controls.V6ComboBox();
            this.v6Label20 = new V6Controls.V6Label();
            this.txtma_maubc = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNgay_ct2 = new V6Controls.V6DateTimePick();
            this.txtNgay_ct1 = new V6Controls.V6DateTimePick();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNgay_ct4 = new V6Controls.V6DateTimePick();
            this.txtNgay_ct3 = new V6Controls.V6DateTimePick();
            this.btnSuaCTMau = new V6Controls.Controls.V6FormButton();
            this.btnSuaTTMau = new V6Controls.Controls.V6FormButton();
            this.btnThemMau = new V6Controls.Controls.V6FormButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 101);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(3, 53);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 4;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 16);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "Điều kiện (OR)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 16);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(80, 18);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // chk_Luy_ke
            // 
            this.chk_Luy_ke.AccessibleDescription = "FILTERC00010";
            this.chk_Luy_ke.AccessibleName = "LUY_KE";
            this.chk_Luy_ke.AutoSize = true;
            this.chk_Luy_ke.Location = new System.Drawing.Point(63, 109);
            this.chk_Luy_ke.Margin = new System.Windows.Forms.Padding(4);
            this.chk_Luy_ke.Name = "chk_Luy_ke";
            this.chk_Luy_ke.Size = new System.Drawing.Size(66, 17);
            this.chk_Luy_ke.TabIndex = 16;
            this.chk_Luy_ke.Text = "In lũy kế";
            this.chk_Luy_ke.UseVisualStyleBackColor = true;
            // 
            // cboMaubc
            // 
            this.cboMaubc.AccessibleName = "MAU_BC";
            this.cboMaubc.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaubc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaubc.FormattingEnabled = true;
            this.cboMaubc.Location = new System.Drawing.Point(63, 133);
            this.cboMaubc.Name = "cboMaubc";
            this.cboMaubc.Size = new System.Drawing.Size(229, 21);
            this.cboMaubc.TabIndex = 18;
            this.cboMaubc.TabStop = false;
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "FILTERL00138";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(3, 136);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(28, 13);
            this.v6Label20.TabIndex = 17;
            this.v6Label20.Text = "Mẫu";
            // 
            // txtma_maubc
            // 
            this.txtma_maubc.AccessibleName = "MA_MAUBC";
            this.txtma_maubc.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_maubc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_maubc.CheckNotEmpty = true;
            this.txtma_maubc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_maubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_maubc.LeaveColor = System.Drawing.Color.White;
            this.txtma_maubc.Location = new System.Drawing.Point(63, 179);
            this.txtma_maubc.Name = "txtma_maubc";
            this.txtma_maubc.Size = new System.Drawing.Size(132, 20);
            this.txtma_maubc.TabIndex = 19;
            this.txtma_maubc.Visible = false;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00066";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Kỳ này từ ngày";
            // 
            // txtNgay_ct2
            // 
            this.txtNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct2.Location = new System.Drawing.Point(120, 35);
            this.txtNgay_ct2.Name = "txtNgay_ct2";
            this.txtNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct2.TabIndex = 23;
            // 
            // txtNgay_ct1
            // 
            this.txtNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct1.Location = new System.Drawing.Point(120, 10);
            this.txtNgay_ct1.Name = "txtNgay_ct1";
            this.txtNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00003";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Đến ngày";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00067";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Kỳ trước từ ngày";
            // 
            // txtNgay_ct4
            // 
            this.txtNgay_ct4.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct4.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct4.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct4.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct4.Location = new System.Drawing.Point(120, 85);
            this.txtNgay_ct4.Name = "txtNgay_ct4";
            this.txtNgay_ct4.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct4.TabIndex = 27;
            // 
            // txtNgay_ct3
            // 
            this.txtNgay_ct3.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct3.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct3.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct3.Location = new System.Drawing.Point(120, 60);
            this.txtNgay_ct3.Name = "txtNgay_ct3";
            this.txtNgay_ct3.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct3.TabIndex = 25;
            // 
            // btnSuaCTMau
            // 
            this.btnSuaCTMau.AccessibleDescription = "REPORTB00003";
            this.btnSuaCTMau.Location = new System.Drawing.Point(151, 155);
            this.btnSuaCTMau.Name = "btnSuaCTMau";
            this.btnSuaCTMau.Size = new System.Drawing.Size(48, 23);
            this.btnSuaCTMau.TabIndex = 31;
            this.btnSuaCTMau.Text = "Sửa ct";
            this.btnSuaCTMau.UseVisualStyleBackColor = true;
            this.btnSuaCTMau.Click += new System.EventHandler(this.btnSuaCTMau_Click);
            // 
            // btnSuaTTMau
            // 
            this.btnSuaTTMau.AccessibleDescription = "REPORTB00001";
            this.btnSuaTTMau.Location = new System.Drawing.Point(107, 155);
            this.btnSuaTTMau.Name = "btnSuaTTMau";
            this.btnSuaTTMau.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMau.TabIndex = 32;
            this.btnSuaTTMau.Text = "Sửa tt";
            this.btnSuaTTMau.UseVisualStyleBackColor = true;
            this.btnSuaTTMau.Click += new System.EventHandler(this.btnSuaTTMau_Click);
            // 
            // btnThemMau
            // 
            this.btnThemMau.AccessibleDescription = "REPORTB00002";
            this.btnThemMau.Location = new System.Drawing.Point(63, 155);
            this.btnThemMau.Name = "btnThemMau";
            this.btnThemMau.Size = new System.Drawing.Size(43, 23);
            this.btnThemMau.TabIndex = 33;
            this.btnThemMau.Text = "Thêm";
            this.btnThemMau.UseVisualStyleBackColor = true;
            this.btnThemMau.Click += new System.EventHandler(this.btnThemMau_Click);
            // 
            // AGLTCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSuaCTMau);
            this.Controls.Add(this.btnSuaTTMau);
            this.Controls.Add(this.btnThemMau);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNgay_ct4);
            this.Controls.Add(this.txtNgay_ct3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNgay_ct2);
            this.Controls.Add(this.txtNgay_ct1);
            this.Controls.Add(this.txtma_maubc);
            this.Controls.Add(this.cboMaubc);
            this.Controls.Add(this.v6Label20);
            this.Controls.Add(this.chk_Luy_ke);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLTCD";
            this.Size = new System.Drawing.Size(295, 309);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6Controls.V6CheckBox chk_Luy_ke;
        private V6Controls.V6ComboBox cboMaubc;
        private V6Controls.V6Label v6Label20;
        private V6Controls.V6VvarTextBox txtma_maubc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick txtNgay_ct2;
        private V6Controls.V6DateTimePick txtNgay_ct1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private V6Controls.V6DateTimePick txtNgay_ct4;
        private V6Controls.V6DateTimePick txtNgay_ct3;
        private V6Controls.Controls.V6FormButton btnSuaCTMau;
        private V6Controls.Controls.V6FormButton btnSuaTTMau;
        private V6Controls.Controls.V6FormButton btnThemMau;
    }
}
