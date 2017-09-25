namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ARSD_AR
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.txtma_ct = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.TxtMa_kh = new V6Controls.V6VvarTextBox();
            this.chkLike = new V6Controls.V6CheckBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.rdo_intattoan = new System.Windows.Forms.RadioButton();
            this.rdo_khongintattoan = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(141, 33);
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(141, 12);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // txtma_ct
            // 
            this.txtma_ct.AccessibleDescription = "FILTERL00004";
            this.txtma_ct.AccessibleName = "MA_CT";
            this.txtma_ct.FieldCaption = "Mã chứng từ";
            this.txtma_ct.FieldName = "MA_CT";
            this.txtma_ct.Location = new System.Drawing.Point(4, 50);
            this.txtma_ct.Name = "txtma_ct";
            this.txtma_ct.Size = new System.Drawing.Size(277, 22);
            this.txtma_ct.TabIndex = 5;
            this.txtma_ct.Vvar = "MA_CT";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(4, 77);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(277, 22);
            this.txtMaDvcs.TabIndex = 6;
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
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.txtma_ct);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(9, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 122);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00007";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(5, 67);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(55, 13);
            this.v6Label1.TabIndex = 10;
            this.v6Label1.Text = "Mã khách";
            // 
            // TxtMa_kh
            // 
            this.TxtMa_kh.AccessibleName = "MA_KH";
            this.TxtMa_kh.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_kh.CheckNotEmpty = true;
            this.TxtMa_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_kh.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_kh.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_kh.Location = new System.Drawing.Point(141, 64);
            this.TxtMa_kh.Name = "TxtMa_kh";
            this.TxtMa_kh.Size = new System.Drawing.Size(132, 20);
            this.TxtMa_kh.TabIndex = 2;
            this.TxtMa_kh.VVar = "MA_KH";
            // 
            // chkLike
            // 
            this.chkLike.AccessibleDescription = "FILTERC00002";
            this.chkLike.AccessibleName = "";
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(79, 92);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 13;
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
            this.ctTuSo.Location = new System.Drawing.Point(141, 90);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(132, 20);
            this.ctTuSo.TabIndex = 3;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00021";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(5, 92);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 11;
            this.v6Label8.Text = "CT từ số";
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
            this.ctDenSo.Location = new System.Drawing.Point(141, 116);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(132, 20);
            this.ctDenSo.TabIndex = 4;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00022";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(5, 116);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 14;
            this.v6Label7.Text = "Đến số";
            // 
            // rdo_intattoan
            // 
            this.rdo_intattoan.AccessibleDescription = "FILTERR00003";
            this.rdo_intattoan.AutoSize = true;
            this.rdo_intattoan.Location = new System.Drawing.Point(141, 144);
            this.rdo_intattoan.Name = "rdo_intattoan";
            this.rdo_intattoan.Size = new System.Drawing.Size(73, 17);
            this.rdo_intattoan.TabIndex = 6;
            this.rdo_intattoan.TabStop = true;
            this.rdo_intattoan.Text = "In tất toán";
            this.rdo_intattoan.UseVisualStyleBackColor = true;
            // 
            // rdo_khongintattoan
            // 
            this.rdo_khongintattoan.AccessibleDescription = "FILTERR00004";
            this.rdo_khongintattoan.AutoSize = true;
            this.rdo_khongintattoan.Checked = true;
            this.rdo_khongintattoan.Location = new System.Drawing.Point(14, 144);
            this.rdo_khongintattoan.Name = "rdo_khongintattoan";
            this.rdo_khongintattoan.Size = new System.Drawing.Size(106, 17);
            this.rdo_khongintattoan.TabIndex = 5;
            this.rdo_khongintattoan.TabStop = true;
            this.rdo_khongintattoan.Text = "Không in tất toán";
            this.rdo_khongintattoan.UseVisualStyleBackColor = true;
            // 
            // ARSD_AR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdo_intattoan);
            this.Controls.Add(this.rdo_khongintattoan);
            this.Controls.Add(this.ctDenSo);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.chkLike);
            this.Controls.Add(this.ctTuSo);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.TxtMa_kh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Name = "ARSD_AR";
            this.Size = new System.Drawing.Size(303, 342);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtma_ct;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox TxtMa_kh;
        private V6Controls.V6CheckBox chkLike;
        private V6Controls.V6VvarTextBox ctTuSo;
        private V6Controls.V6Label v6Label8;
        private V6Controls.V6VvarTextBox ctDenSo;
        private V6Controls.V6Label v6Label7;
        private System.Windows.Forms.RadioButton rdo_intattoan;
        private System.Windows.Forms.RadioButton rdo_khongintattoan;
    }
}
