namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class V6IMDATA2_Filter
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
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.v6Label3 = new V6Controls.V6Label();
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoSoCt = new V6Controls.V6CheckBox();
            this.chkDeleteData0 = new V6Controls.V6CheckBox();
            this.chkChuyenMa = new V6Controls.V6CheckBox();
            this.comboBox2 = new V6Controls.V6ComboBox();
            this.txtFile = new System.Windows.Forms.RichTextBox();
            this.btnSuaChiTieu = new System.Windows.Forms.Button();
            this.btnXemMauExcel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.chkAutoF9 = new V6Controls.V6CheckBox();
            this.numAuto1 = new System.Windows.Forms.NumericUpDown();
            this.lblPhut = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAuto1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00199";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "File\r\nexcel";
            this.label1.Visible = false;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00205";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(1, 109);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(48, 13);
            this.v6Label2.TabIndex = 5;
            this.v6Label2.Text = "Mã đích";
            // 
            // btnBrowse
            // 
            this.btnBrowse.AccessibleDescription = "FILTERB00005";
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::V6ControlManager.Properties.Resources.Excel16;
            this.btnBrowse.Location = new System.Drawing.Point(37, 321);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(137, 24);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Chọn file Excel";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00204";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(2, 82);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(55, 13);
            this.v6Label3.TabIndex = 3;
            this.v6Label3.Text = "Mã nguồn";
            // 
            // comboBox1
            // 
            this.comboBox1.AccessibleName = "kieu_post";
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "TCVN3 (ABC)",
            "VNI"});
            this.comboBox1.Location = new System.Drawing.Point(94, 79);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(154, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00005";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkAutoSoCt);
            this.groupBox1.Controls.Add(this.chkDeleteData0);
            this.groupBox1.Controls.Add(this.chkChuyenMa);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.v6Label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 140);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // chkAutoSoCt
            // 
            this.chkAutoSoCt.AccessibleDescription = "FILTERL00269";
            this.chkAutoSoCt.AutoSize = true;
            this.chkAutoSoCt.Location = new System.Drawing.Point(94, 42);
            this.chkAutoSoCt.Name = "chkAutoSoCt";
            this.chkAutoSoCt.Size = new System.Drawing.Size(144, 17);
            this.chkAutoSoCt.TabIndex = 2;
            this.chkAutoSoCt.Text = "Tự động tạo số chứng từ";
            this.chkAutoSoCt.UseVisualStyleBackColor = true;
            this.chkAutoSoCt.CheckedChanged += new System.EventHandler(this.chkAutoSoCt_CheckedChanged);
            // 
            // chkDeleteData0
            // 
            this.chkDeleteData0.AccessibleDescription = "XULYC00019";
            this.chkDeleteData0.AutoSize = true;
            this.chkDeleteData0.Location = new System.Drawing.Point(94, 19);
            this.chkDeleteData0.Name = "chkDeleteData0";
            this.chkDeleteData0.Size = new System.Drawing.Size(149, 17);
            this.chkDeleteData0.TabIndex = 1;
            this.chkDeleteData0.Text = "Xóa dữ liệu nhận trước đó";
            this.chkDeleteData0.UseVisualStyleBackColor = true;
            this.chkDeleteData0.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chkChuyenMa
            // 
            this.chkChuyenMa.AccessibleDescription = "FILTERC00016";
            this.chkChuyenMa.AutoSize = true;
            this.chkChuyenMa.Location = new System.Drawing.Point(9, 19);
            this.chkChuyenMa.Name = "chkChuyenMa";
            this.chkChuyenMa.Size = new System.Drawing.Size(79, 17);
            this.chkChuyenMa.TabIndex = 0;
            this.chkChuyenMa.Text = "Chuyễn mã";
            this.chkChuyenMa.UseVisualStyleBackColor = true;
            this.chkChuyenMa.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.AccessibleName = "kieu_post";
            this.comboBox2.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "UNICODE"});
            this.comboBox2.Location = new System.Drawing.Point(94, 106);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(154, 21);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(37, 237);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(235, 75);
            this.txtFile.TabIndex = 5;
            this.txtFile.Text = "";
            this.txtFile.Visible = false;
            // 
            // btnSuaChiTieu
            // 
            this.btnSuaChiTieu.AccessibleDescription = "FILTERB00001";
            this.btnSuaChiTieu.Location = new System.Drawing.Point(180, 321);
            this.btnSuaChiTieu.Name = "btnSuaChiTieu";
            this.btnSuaChiTieu.Size = new System.Drawing.Size(75, 23);
            this.btnSuaChiTieu.TabIndex = 7;
            this.btnSuaChiTieu.Text = "Sửa chỉ tiêu";
            this.btnSuaChiTieu.UseVisualStyleBackColor = true;
            this.btnSuaChiTieu.Click += new System.EventHandler(this.btnSuaChiTieu_Click);
            // 
            // btnXemMauExcel
            // 
            this.btnXemMauExcel.AccessibleDescription = "FILTERB00013";
            this.btnXemMauExcel.Location = new System.Drawing.Point(5, 352);
            this.btnXemMauExcel.Name = "btnXemMauExcel";
            this.btnXemMauExcel.Size = new System.Drawing.Size(88, 29);
            this.btnXemMauExcel.TabIndex = 9;
            this.btnXemMauExcel.Text = "Xem mẫu excel";
            this.btnXemMauExcel.UseVisualStyleBackColor = true;
            this.btnXemMauExcel.Click += new System.EventHandler(this.btnXemMauExcel_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00002";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Từ ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(112, 25);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(112, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // chkAutoF9
            // 
            this.chkAutoF9.AccessibleDescription = "XULYC00017";
            this.chkAutoF9.AutoSize = true;
            this.chkAutoF9.Location = new System.Drawing.Point(8, 197);
            this.chkAutoF9.Name = "chkAutoF9";
            this.chkAutoF9.Size = new System.Drawing.Size(112, 17);
            this.chkAutoF9.TabIndex = 10;
            this.chkAutoF9.Text = "Tự động chạy mỗi";
            this.chkAutoF9.UseVisualStyleBackColor = true;
            this.chkAutoF9.CheckedChanged += new System.EventHandler(this.chkAutoF9_CheckedChanged);
            // 
            // numAuto1
            // 
            this.numAuto1.Location = new System.Drawing.Point(164, 197);
            this.numAuto1.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numAuto1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAuto1.Name = "numAuto1";
            this.numAuto1.Size = new System.Drawing.Size(63, 20);
            this.numAuto1.TabIndex = 11;
            this.numAuto1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAuto1.ValueChanged += new System.EventHandler(this.numAuto1_ValueChanged);
            // 
            // lblPhut
            // 
            this.lblPhut.AccessibleDescription = "XULYC00018";
            this.lblPhut.AutoSize = true;
            this.lblPhut.Location = new System.Drawing.Point(233, 199);
            this.lblPhut.Name = "lblPhut";
            this.lblPhut.Size = new System.Drawing.Size(28, 13);
            this.lblPhut.TabIndex = 12;
            this.lblPhut.Text = "phút";
            // 
            // V6IMDATA2_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPhut);
            this.Controls.Add(this.numAuto1);
            this.Controls.Add(this.chkAutoF9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.btnXemMauExcel);
            this.Controls.Add(this.btnSuaChiTieu);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Name = "V6IMDATA2_Filter";
            this.Size = new System.Drawing.Size(275, 392);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAuto1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private System.Windows.Forms.Button btnBrowse;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6ComboBox comboBox2;
        private V6Controls.V6CheckBox chkChuyenMa;
        private V6Controls.V6CheckBox chkDeleteData0;
        private System.Windows.Forms.RichTextBox txtFile;
        private System.Windows.Forms.Button btnSuaChiTieu;
        private System.Windows.Forms.Button btnXemMauExcel;
        private V6Controls.V6CheckBox chkAutoSoCt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6CheckBox chkAutoF9;
        private System.Windows.Forms.NumericUpDown numAuto1;
        private System.Windows.Forms.Label lblPhut;
    }
}
