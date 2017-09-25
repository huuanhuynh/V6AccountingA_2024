namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class APDMO_AP
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
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.chkLike = new V6Controls.V6CheckBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.txtma_ct = new V6ReportControls.FilterLineVvarTextBox();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.taiKhoan = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctDenSo
            // 
            this.ctDenSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctDenSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctDenSo.Enabled = false;
            this.ctDenSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctDenSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctDenSo.LeaveColor = System.Drawing.Color.White;
            this.ctDenSo.Location = new System.Drawing.Point(140, 122);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(132, 20);
            this.ctDenSo.TabIndex = 21;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00022";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(5, 122);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 28;
            this.v6Label7.Text = "Đến số";
            // 
            // chkLike
            // 
            this.chkLike.AccessibleDescription = "FILTERC00002";
            this.chkLike.AccessibleName = "";
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(79, 98);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 27;
            this.chkLike.Text = "like";
            this.chkLike.UseVisualStyleBackColor = true;
            this.chkLike.CheckedChanged += new System.EventHandler(this.chkLike_CheckedChanged);
            // 
            // ctTuSo
            // 
            this.ctTuSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctTuSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctTuSo.CheckOnLeave = false;
            this.ctTuSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctTuSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctTuSo.LeaveColor = System.Drawing.Color.White;
            this.ctTuSo.Location = new System.Drawing.Point(140, 96);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(132, 20);
            this.ctTuSo.TabIndex = 2;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00021";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(5, 98);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 26;
            this.v6Label8.Text = "CT từ số";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.txtma_ct);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.taiKhoan);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(5, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 185);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.FieldCaption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(6, 49);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(276, 22);
            this.filterLineVvarTextBox3.TabIndex = 2;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
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
            // txtma_ct
            // 
            this.txtma_ct.AccessibleDescription = "FILTERL00004";
            this.txtma_ct.AccessibleName = "MA_CT";
            this.txtma_ct.Enabled = false;
            this.txtma_ct.FieldCaption = "Mã chứng từ";
            this.txtma_ct.FieldName = "MA_CT";
            this.txtma_ct.Location = new System.Drawing.Point(6, 76);
            this.txtma_ct.Name = "txtma_ct";
            this.txtma_ct.Size = new System.Drawing.Size(277, 22);
            this.txtma_ct.TabIndex = 3;
            this.txtma_ct.Vvar = "MA_CT";
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
            // taiKhoan
            // 
            this.taiKhoan.AccessibleDescription = "FILTERL00027";
            this.taiKhoan.AccessibleName = "";
            this.taiKhoan.FieldCaption = "Tài khoản";
            this.taiKhoan.FieldName = "TK";
            this.taiKhoan.Location = new System.Drawing.Point(6, 131);
            this.taiKhoan.Name = "taiKhoan";
            this.taiKhoan.Size = new System.Drawing.Size(277, 22);
            this.taiKhoan.TabIndex = 5;
            this.taiKhoan.Vvar = "TK";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 103);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(277, 22);
            this.txtMaDvcs.TabIndex = 4;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Từ ngày";
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(140, 39);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(140, 18);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // APDMO_AP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctDenSo);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.chkLike);
            this.Controls.Add(this.ctTuSo);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Name = "APDMO_AP";
            this.Size = new System.Drawing.Size(309, 336);
            this.Load += new System.EventHandler(this.Filter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6VvarTextBox ctDenSo;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6CheckBox chkLike;
        private V6Controls.V6VvarTextBox ctTuSo;
        private V6Controls.V6Label v6Label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private V6ReportControls.FilterLineVvarTextBox txtma_ct;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private V6ReportControls.FilterLineVvarTextBox taiKhoan;

    }
}
