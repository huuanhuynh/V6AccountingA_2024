namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class V6IMDATA2TH2_Filter
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
            this.lineMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.chkAutoSoCt = new V6Controls.V6CheckBox();
            this.chkDeleteData0 = new V6Controls.V6CheckBox();
            this.btnSuaChiTieu = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoF9 = new V6Controls.V6CheckBox();
            this.numAuto1 = new System.Windows.Forms.NumericUpDown();
            this.lblPhut = new System.Windows.Forms.Label();
            this.lblDate3 = new System.Windows.Forms.Label();
            this.dateNgay_ct3 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.numHHFrom = new System.Windows.Forms.NumericUpDown();
            this.numHHTo = new System.Windows.Forms.NumericUpDown();
            this.lblHHFrom = new System.Windows.Forms.Label();
            this.lblHHTo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAuto1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHHFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHHTo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00005";
            this.groupBox1.Controls.Add(this.lineMaDvcs);
            this.groupBox1.Controls.Add(this.chkAutoSoCt);
            this.groupBox1.Controls.Add(this.chkDeleteData0);
            this.groupBox1.Location = new System.Drawing.Point(3, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 140);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // lineMaDvcs
            // 
            this.lineMaDvcs.AccessibleDescription = "FILTERL00005";
            this.lineMaDvcs.AccessibleName2 = "MA_DVCS";
            this.lineMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaDvcs.Caption = "Mã đơn vị";
            this.lineMaDvcs.FieldName = "MA_DVCS";
            this.lineMaDvcs.Location = new System.Drawing.Point(5, 65);
            this.lineMaDvcs.Name = "lineMaDvcs";
            this.lineMaDvcs.Size = new System.Drawing.Size(263, 22);
            this.lineMaDvcs.TabIndex = 3;
            this.lineMaDvcs.Vvar = "MA_DVCS";
            // 
            // chkAutoSoCt
            // 
            this.chkAutoSoCt.AccessibleDescription = "FILTERL00269";
            this.chkAutoSoCt.AutoSize = true;
            this.chkAutoSoCt.Location = new System.Drawing.Point(8, 42);
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
            this.chkDeleteData0.Location = new System.Drawing.Point(8, 19);
            this.chkDeleteData0.Name = "chkDeleteData0";
            this.chkDeleteData0.Size = new System.Drawing.Size(149, 17);
            this.chkDeleteData0.TabIndex = 1;
            this.chkDeleteData0.Text = "Xóa dữ liệu nhận trước đó";
            this.chkDeleteData0.UseVisualStyleBackColor = true;
            this.chkDeleteData0.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btnSuaChiTieu
            // 
            this.btnSuaChiTieu.AccessibleDescription = "FILTERB00001";
            this.btnSuaChiTieu.Location = new System.Drawing.Point(180, 350);
            this.btnSuaChiTieu.Name = "btnSuaChiTieu";
            this.btnSuaChiTieu.Size = new System.Drawing.Size(75, 23);
            this.btnSuaChiTieu.TabIndex = 7;
            this.btnSuaChiTieu.Text = "Sửa chỉ tiêu";
            this.btnSuaChiTieu.UseVisualStyleBackColor = true;
            this.btnSuaChiTieu.Click += new System.EventHandler(this.btnSuaChiTieu_Click);
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
            // chkAutoF9
            // 
            this.chkAutoF9.AccessibleDescription = "XULYC00017";
            this.chkAutoF9.AutoSize = true;
            this.chkAutoF9.Location = new System.Drawing.Point(8, 278);
            this.chkAutoF9.Name = "chkAutoF9";
            this.chkAutoF9.Size = new System.Drawing.Size(112, 17);
            this.chkAutoF9.TabIndex = 10;
            this.chkAutoF9.Text = "Tự động chạy mỗi";
            this.chkAutoF9.UseVisualStyleBackColor = true;
            this.chkAutoF9.CheckedChanged += new System.EventHandler(this.chkAutoF9_CheckedChanged);
            // 
            // numAuto1
            // 
            this.numAuto1.Location = new System.Drawing.Point(164, 275);
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
            15,
            0,
            0,
            0});
            this.numAuto1.ValueChanged += new System.EventHandler(this.numAuto1_ValueChanged);
            // 
            // lblPhut
            // 
            this.lblPhut.AccessibleDescription = "XULYC00018";
            this.lblPhut.AutoSize = true;
            this.lblPhut.Location = new System.Drawing.Point(233, 277);
            this.lblPhut.Name = "lblPhut";
            this.lblPhut.Size = new System.Drawing.Size(28, 13);
            this.lblPhut.TabIndex = 12;
            this.lblPhut.Text = "phút";
            // 
            // lblDate3
            // 
            this.lblDate3.AutoSize = true;
            this.lblDate3.Location = new System.Drawing.Point(8, 199);
            this.lblDate3.Name = "lblDate3";
            this.lblDate3.Size = new System.Drawing.Size(79, 13);
            this.lblDate3.TabIndex = 13;
            this.lblDate3.Text = "Ngày tạo phiếu";
            // 
            // dateNgay_ct3
            // 
            this.dateNgay_ct3.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct3.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct3.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct3.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct3.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct3.Location = new System.Drawing.Point(112, 197);
            this.dateNgay_ct3.Name = "dateNgay_ct3";
            this.dateNgay_ct3.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct3.TabIndex = 14;
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(127, 27);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 16;
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(127, 5);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 15;
            // 
            // numHHFrom
            // 
            this.numHHFrom.Location = new System.Drawing.Point(164, 223);
            this.numHHFrom.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numHHFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHHFrom.Name = "numHHFrom";
            this.numHHFrom.Size = new System.Drawing.Size(63, 20);
            this.numHHFrom.TabIndex = 17;
            this.numHHFrom.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // numHHTo
            // 
            this.numHHTo.Location = new System.Drawing.Point(164, 249);
            this.numHHTo.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numHHTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHHTo.Name = "numHHTo";
            this.numHHTo.Size = new System.Drawing.Size(63, 20);
            this.numHHTo.TabIndex = 17;
            this.numHHTo.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // lblHHFrom
            // 
            this.lblHHFrom.AutoSize = true;
            this.lblHHFrom.Location = new System.Drawing.Point(34, 230);
            this.lblHHFrom.Name = "lblHHFrom";
            this.lblHHFrom.Size = new System.Drawing.Size(63, 13);
            this.lblHHFrom.TabIndex = 2;
            this.lblHHFrom.Text = "Thời gian từ";
            // 
            // lblHHTo
            // 
            this.lblHHTo.AutoSize = true;
            this.lblHHTo.Location = new System.Drawing.Point(34, 251);
            this.lblHHTo.Name = "lblHHTo";
            this.lblHHTo.Size = new System.Drawing.Size(26, 13);
            this.lblHHTo.TabIndex = 2;
            this.lblHHTo.Text = "đến";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "giờ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "giờ";
            // 
            // V6IMDATA2TH2_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numHHTo);
            this.Controls.Add(this.numHHFrom);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.lblDate3);
            this.Controls.Add(this.dateNgay_ct3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPhut);
            this.Controls.Add(this.numAuto1);
            this.Controls.Add(this.chkAutoF9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHHTo);
            this.Controls.Add(this.lblHHFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSuaChiTieu);
            this.Name = "V6IMDATA2TH2_Filter";
            this.Size = new System.Drawing.Size(275, 427);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAuto1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHHFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHHTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6CheckBox chkDeleteData0;
        private System.Windows.Forms.Button btnSuaChiTieu;
        private V6Controls.V6CheckBox chkAutoSoCt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6CheckBox chkAutoF9;
        private System.Windows.Forms.NumericUpDown numAuto1;
        private System.Windows.Forms.Label lblPhut;
        private System.Windows.Forms.Label lblDate3;
        private V6Controls.V6DateTimePicker dateNgay_ct3;
        private V6ReportControls.FilterLineVvarTextBox lineMaDvcs;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private System.Windows.Forms.NumericUpDown numHHFrom;
        private System.Windows.Forms.NumericUpDown numHHTo;
        private System.Windows.Forms.Label lblHHFrom;
        private System.Windows.Forms.Label lblHHTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}
