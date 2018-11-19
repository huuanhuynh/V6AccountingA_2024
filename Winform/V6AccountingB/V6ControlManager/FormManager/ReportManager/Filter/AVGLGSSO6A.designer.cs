namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AVGLGSSO6A
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
            this.lineMaDVCS = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctDenSo = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.chkLike = new V6Controls.V6CheckBox();
            this.ctTuSo = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lineMaDVCS
            // 
            this.lineMaDVCS.AccessibleDescription = "FILTERL00005";
            this.lineMaDVCS.AccessibleName2 = "MA_DVCS";
            this.lineMaDVCS.Caption = "Mã đơn vị";
            this.lineMaDVCS.FieldName = "MA_DVCS";
            this.lineMaDVCS.Location = new System.Drawing.Point(6, 43);
            this.lineMaDVCS.Name = "lineMaDVCS";
            this.lineMaDVCS.Size = new System.Drawing.Size(282, 22);
            this.lineMaDVCS.TabIndex = 0;
            this.lineMaDVCS.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.lineMaDVCS);
            this.groupBox1.Location = new System.Drawing.Point(0, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 86);
            this.groupBox1.TabIndex = 3;
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
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(131, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(131, 29);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // ctDenSo
            // 
            this.ctDenSo.AccessibleName = "CT_DENSO";
            this.ctDenSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctDenSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctDenSo.Enabled = false;
            this.ctDenSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctDenSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctDenSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctDenSo.LeaveColor = System.Drawing.Color.White;
            this.ctDenSo.Location = new System.Drawing.Point(131, 80);
            this.ctDenSo.Name = "ctDenSo";
            this.ctDenSo.Size = new System.Drawing.Size(100, 20);
            this.ctDenSo.TabIndex = 3;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00022";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(5, 82);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(41, 13);
            this.v6Label7.TabIndex = 63;
            this.v6Label7.Text = "Đến số";
            // 
            // chkLike
            // 
            this.chkLike.AccessibleDescription = "FILTERC00002";
            this.chkLike.AccessibleName = "";
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(89, 57);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(42, 17);
            this.chkLike.TabIndex = 59;
            this.chkLike.Text = "like";
            this.chkLike.UseVisualStyleBackColor = true;
            // 
            // ctTuSo
            // 
            this.ctTuSo.AccessibleName = "CT_TUSO";
            this.ctTuSo.BackColor = System.Drawing.SystemColors.Window;
            this.ctTuSo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.ctTuSo.CheckOnLeave = false;
            this.ctTuSo.EnterColor = System.Drawing.Color.PaleGreen;
            this.ctTuSo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.ctTuSo.HoverColor = System.Drawing.Color.Yellow;
            this.ctTuSo.LeaveColor = System.Drawing.Color.White;
            this.ctTuSo.Location = new System.Drawing.Point(131, 55);
            this.ctTuSo.Name = "ctTuSo";
            this.ctTuSo.Size = new System.Drawing.Size(100, 20);
            this.ctTuSo.TabIndex = 2;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00021";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(5, 57);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(47, 13);
            this.v6Label8.TabIndex = 62;
            this.v6Label8.Text = "CT từ số";
            // 
            // AVGLGSSO5A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctDenSo);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.chkLike);
            this.Controls.Add(this.ctTuSo);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AVGLGSSO5A";
            this.Size = new System.Drawing.Size(295, 254);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox lineMaDVCS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6VvarTextBox ctDenSo;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6CheckBox chkLike;
        private V6Controls.V6VvarTextBox ctTuSo;
        private V6Controls.V6Label v6Label8;
    }
}
