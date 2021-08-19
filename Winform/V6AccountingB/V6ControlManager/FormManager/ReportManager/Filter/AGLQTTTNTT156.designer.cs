namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLQTTTNTT156
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtNam1 = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtThang2 = new V6Controls.V6NumberTextBox();
            this.txtNam2 = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.btnSuaCTMau = new V6Controls.Controls.V6FormButton();
            this.btnSuaTTMau = new V6Controls.Controls.V6FormButton();
            this.btnThemMau = new V6Controls.Controls.V6FormButton();
            this.txtma_maubc = new V6Controls.V6VvarTextBox();
            this.cboMaubc = new V6Controls.V6ComboBox();
            this.v6Label20 = new V6Controls.V6Label();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.txtALINFOR = new V6Controls.Controls.V6FormButton();
            this.btnKetXuatXmlHTKK = new V6Controls.Controls.V6FormButton();
            this.txtFileName = new V6Controls.V6ColorTextBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 39);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 92);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 16);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
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
            // txtNam1
            // 
            this.txtNam1.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam1.DecimalPlaces = 0;
            this.txtNam1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam1.LeaveColor = System.Drawing.Color.White;
            this.txtNam1.Location = new System.Drawing.Point(115, 42);
            this.txtNam1.MaxLength = 4;
            this.txtNam1.MaxNumLength = 4;
            this.txtNam1.Name = "txtNam1";
            this.txtNam1.Size = new System.Drawing.Size(100, 20);
            this.txtNam1.TabIndex = 3;
            this.txtNam1.Text = "0";
            this.txtNam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtThang1
            // 
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(115, 16);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(100, 20);
            this.txtThang1.TabIndex = 1;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang2_TextChanged);
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(6, 45);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 2;
            this.v6Label9.Text = "Năm";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00053";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Từ tháng";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00054";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Đến tháng";
            // 
            // txtThang2
            // 
            this.txtThang2.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang2.DecimalPlaces = 0;
            this.txtThang2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang2.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang2.LeaveColor = System.Drawing.Color.White;
            this.txtThang2.Location = new System.Drawing.Point(115, 70);
            this.txtThang2.MaxLength = 2;
            this.txtThang2.MaxNumLength = 2;
            this.txtThang2.Name = "txtThang2";
            this.txtThang2.Size = new System.Drawing.Size(100, 20);
            this.txtThang2.TabIndex = 5;
            this.txtThang2.Text = "0";
            this.txtThang2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang2.TextChanged += new System.EventHandler(this.txtThang2_TextChanged);
            // 
            // txtNam2
            // 
            this.txtNam2.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam2.DecimalPlaces = 0;
            this.txtNam2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam2.LeaveColor = System.Drawing.Color.White;
            this.txtNam2.Location = new System.Drawing.Point(115, 98);
            this.txtNam2.MaxLength = 4;
            this.txtNam2.MaxNumLength = 4;
            this.txtNam2.Name = "txtNam2";
            this.txtNam2.Size = new System.Drawing.Size(100, 20);
            this.txtNam2.TabIndex = 7;
            this.txtNam2.Text = "0";
            this.txtNam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00109";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 101);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(29, 13);
            this.v6Label1.TabIndex = 6;
            this.v6Label1.Text = "Năm";
            // 
            // btnSuaCTMau
            // 
            this.btnSuaCTMau.AccessibleDescription = "REPORTB00013";
            this.btnSuaCTMau.Location = new System.Drawing.Point(169, 149);
            this.btnSuaCTMau.Name = "btnSuaCTMau";
            this.btnSuaCTMau.Size = new System.Drawing.Size(48, 23);
            this.btnSuaCTMau.TabIndex = 13;
            this.btnSuaCTMau.Text = "Sửa ct";
            this.btnSuaCTMau.UseVisualStyleBackColor = true;
            this.btnSuaCTMau.Click += new System.EventHandler(this.btnSuaCTMau_Click);
            // 
            // btnSuaTTMau
            // 
            this.btnSuaTTMau.AccessibleDescription = "REPORTB00001";
            this.btnSuaTTMau.Location = new System.Drawing.Point(125, 149);
            this.btnSuaTTMau.Name = "btnSuaTTMau";
            this.btnSuaTTMau.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMau.TabIndex = 12;
            this.btnSuaTTMau.Text = "Sửa tt";
            this.btnSuaTTMau.UseVisualStyleBackColor = true;
            this.btnSuaTTMau.Click += new System.EventHandler(this.btnSuaTTMau_Click);
            // 
            // btnThemMau
            // 
            this.btnThemMau.AccessibleDescription = "REPORTB00002";
            this.btnThemMau.Location = new System.Drawing.Point(81, 149);
            this.btnThemMau.Name = "btnThemMau";
            this.btnThemMau.Size = new System.Drawing.Size(43, 23);
            this.btnThemMau.TabIndex = 11;
            this.btnThemMau.Text = "Thêm";
            this.btnThemMau.UseVisualStyleBackColor = true;
            this.btnThemMau.Click += new System.EventHandler(this.btnThemMau_Click);
            // 
            // txtma_maubc
            // 
            this.txtma_maubc.AccessibleName = "MA_MABC";
            this.txtma_maubc.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_maubc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_maubc.CheckNotEmpty = true;
            this.txtma_maubc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_maubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_maubc.LeaveColor = System.Drawing.Color.White;
            this.txtma_maubc.Location = new System.Drawing.Point(82, 172);
            this.txtma_maubc.Name = "txtma_maubc";
            this.txtma_maubc.Size = new System.Drawing.Size(132, 20);
            this.txtma_maubc.TabIndex = 14;
            this.txtma_maubc.Visible = false;
            // 
            // cboMaubc
            // 
            this.cboMaubc.AccessibleName = "MAU_BC";
            this.cboMaubc.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaubc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaubc.DropDownWidth = 300;
            this.cboMaubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaubc.FormattingEnabled = true;
            this.cboMaubc.Location = new System.Drawing.Point(51, 128);
            this.cboMaubc.Name = "cboMaubc";
            this.cboMaubc.Size = new System.Drawing.Size(198, 21);
            this.cboMaubc.TabIndex = 9;
            this.cboMaubc.TabStop = false;
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "FILTERL00138";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(5, 131);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(28, 13);
            this.v6Label20.TabIndex = 8;
            this.v6Label20.Text = "Mẫu";
            // 
            // chkHienTatCa
            // 
            this.chkHienTatCa.AccessibleDescription = "REPORTC00001";
            this.chkHienTatCa.AutoSize = true;
            this.chkHienTatCa.Enabled = false;
            this.chkHienTatCa.Location = new System.Drawing.Point(252, 130);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 10;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // txtALINFOR
            // 
            this.txtALINFOR.AccessibleDescription = ".";
            this.txtALINFOR.Location = new System.Drawing.Point(3, 238);
            this.txtALINFOR.Name = "txtALINFOR";
            this.txtALINFOR.Size = new System.Drawing.Size(115, 23);
            this.txtALINFOR.TabIndex = 17;
            this.txtALINFOR.Text = "...ALINFOR";
            this.txtALINFOR.UseVisualStyleBackColor = true;
            this.txtALINFOR.Click += new System.EventHandler(this.txtALINFOR_Click);
            // 
            // btnKetXuatXmlHTKK
            // 
            this.btnKetXuatXmlHTKK.AccessibleDescription = ".";
            this.btnKetXuatXmlHTKK.Location = new System.Drawing.Point(142, 238);
            this.btnKetXuatXmlHTKK.Name = "btnKetXuatXmlHTKK";
            this.btnKetXuatXmlHTKK.Size = new System.Drawing.Size(147, 23);
            this.btnKetXuatXmlHTKK.TabIndex = 18;
            this.btnKetXuatXmlHTKK.Text = "Kết xuất XML => HTKK";
            this.btnKetXuatXmlHTKK.UseVisualStyleBackColor = true;
            this.btnKetXuatXmlHTKK.Click += new System.EventHandler(this.btnKetXuatXmlHTKK_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.AccessibleName = "FileName";
            this.txtFileName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtFileName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFileName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFileName.GrayText = "Chọn file lưu";
            this.txtFileName.HoverColor = System.Drawing.Color.Yellow;
            this.txtFileName.LeaveColor = System.Drawing.Color.White;
            this.txtFileName.Location = new System.Drawing.Point(3, 212);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(246, 20);
            this.txtFileName.TabIndex = 15;
            this.txtFileName.TabStop = false;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(255, 210);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(36, 23);
            this.btnChon.TabIndex = 16;
            this.btnChon.Text = "...";
            this.filterBaseToolTip1.SetToolTip(this.btnChon, "Chọn file lưu");
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // AGLQTTTNTT156
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaCTMau);
            this.Controls.Add(this.btnSuaTTMau);
            this.Controls.Add(this.btnKetXuatXmlHTKK);
            this.Controls.Add(this.txtALINFOR);
            this.Controls.Add(this.btnThemMau);
            this.Controls.Add(this.txtma_maubc);
            this.Controls.Add(this.cboMaubc);
            this.Controls.Add(this.v6Label20);
            this.Controls.Add(this.txtNam2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.txtNam1);
            this.Controls.Add(this.txtThang2);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLQTTTNTT156";
            this.Size = new System.Drawing.Size(295, 391);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6NumberTextBox txtNam1;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6NumberTextBox txtThang2;
        private V6Controls.V6NumberTextBox txtNam2;
        private V6Controls.V6Label v6Label1;
        private V6Controls.Controls.V6FormButton btnSuaCTMau;
        private V6Controls.Controls.V6FormButton btnSuaTTMau;
        private V6Controls.Controls.V6FormButton btnThemMau;
        private V6Controls.V6VvarTextBox txtma_maubc;
        private V6Controls.V6ComboBox cboMaubc;
        private V6Controls.V6Label v6Label20;
        private V6Controls.V6CheckBox chkHienTatCa;
        private V6Controls.Controls.V6FormButton txtALINFOR;
        private V6Controls.Controls.V6FormButton btnKetXuatXmlHTKK;
        private V6Controls.V6ColorTextBox txtFileName;
        private System.Windows.Forms.Button btnChon;
    }
}
