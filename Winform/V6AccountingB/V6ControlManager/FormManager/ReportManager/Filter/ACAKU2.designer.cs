namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACAKU2
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
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.filterLineVvarTextBox7 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku3 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku1 = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Tk_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox10 = new V6ReportControls.FilterLineVvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNgay_ct4 = new V6Controls.V6DateTimePick();
            this.txtNgay_ct3 = new V6Controls.V6DateTimePick();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNgay_ct2 = new V6Controls.V6DateTimePick();
            this.txtNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            // filterLineVvarTextBox7
            // 
            this.filterLineVvarTextBox7.AccessibleDescription = "FILTERL00065";
            this.filterLineVvarTextBox7.FieldCaption = "Mã vụ việc";
            this.filterLineVvarTextBox7.FieldName = "MA_VV";
            this.filterLineVvarTextBox7.Location = new System.Drawing.Point(6, 64);
            this.filterLineVvarTextBox7.Name = "filterLineVvarTextBox7";
            this.filterLineVvarTextBox7.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox7.TabIndex = 3;
            this.filterLineVvarTextBox7.Vvar = "MA_VV";
            // 
            // Txtnh_ku3
            // 
            this.Txtnh_ku3.AccessibleDescription = "FILTERL00073";
            this.Txtnh_ku3.FieldCaption = "Nhóm khế ước 3";
            this.Txtnh_ku3.FieldName = "NH_KU3";
            this.Txtnh_ku3.Location = new System.Drawing.Point(6, 152);
            this.Txtnh_ku3.Name = "Txtnh_ku3";
            this.Txtnh_ku3.Size = new System.Drawing.Size(282, 22);
            this.Txtnh_ku3.TabIndex = 7;
            this.Txtnh_ku3.Vvar = "NH_KU";
            // 
            // Txtnh_ku2
            // 
            this.Txtnh_ku2.AccessibleDescription = "FILTERL00072";
            this.Txtnh_ku2.FieldCaption = "Nhóm khế ước 2";
            this.Txtnh_ku2.FieldName = "NH_KU2";
            this.Txtnh_ku2.Location = new System.Drawing.Point(6, 130);
            this.Txtnh_ku2.Name = "Txtnh_ku2";
            this.Txtnh_ku2.Size = new System.Drawing.Size(282, 22);
            this.Txtnh_ku2.TabIndex = 6;
            this.Txtnh_ku2.Vvar = "NH_KU";
            // 
            // Txtnh_ku1
            // 
            this.Txtnh_ku1.AccessibleDescription = "FILTERL00071";
            this.Txtnh_ku1.FieldCaption = "Nhóm khế ước 1";
            this.Txtnh_ku1.FieldName = "NH_KU1";
            this.Txtnh_ku1.Location = new System.Drawing.Point(6, 108);
            this.Txtnh_ku1.Name = "Txtnh_ku1";
            this.Txtnh_ku1.Size = new System.Drawing.Size(282, 22);
            this.Txtnh_ku1.TabIndex = 5;
            this.Txtnh_ku1.Vvar = "NH_KU";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Tk_filterLine);
            this.groupBox1.Controls.Add(this.Txtnh_ku1);
            this.groupBox1.Controls.Add(this.Txtnh_ku2);
            this.groupBox1.Controls.Add(this.Txtnh_ku3);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox10);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox7);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 194);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // Tk_filterLine
            // 
            this.Tk_filterLine.AccessibleDescription = "FILTERL00027";
            this.Tk_filterLine.FieldCaption = "Tài khoản";
            this.Tk_filterLine.FieldName = "TK";
            this.Tk_filterLine.Location = new System.Drawing.Point(6, 86);
            this.Tk_filterLine.Name = "Tk_filterLine";
            this.Tk_filterLine.Size = new System.Drawing.Size(282, 22);
            this.Tk_filterLine.TabIndex = 4;
            this.Tk_filterLine.Vvar = "TK";
            // 
            // filterLineVvarTextBox10
            // 
            this.filterLineVvarTextBox10.AccessibleDescription = "FILTERL00064";
            this.filterLineVvarTextBox10.FieldCaption = "Mã khế ước";
            this.filterLineVvarTextBox10.FieldName = "MA_KU";
            this.filterLineVvarTextBox10.Location = new System.Drawing.Point(6, 42);
            this.filterLineVvarTextBox10.Name = "filterLineVvarTextBox10";
            this.filterLineVvarTextBox10.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox10.TabIndex = 2;
            this.filterLineVvarTextBox10.Vvar = "MA_KU";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00003";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Đến ngày";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00067";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Kỳ trước từ ngày";
            // 
            // txtNgay_ct4
            // 
            this.txtNgay_ct4.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgay_ct4.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct4.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct4.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtNgay_ct4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct4.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct4.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct4.Location = new System.Drawing.Point(150, 78);
            this.txtNgay_ct4.Name = "txtNgay_ct4";
            this.txtNgay_ct4.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct4.TabIndex = 3;
            // 
            // txtNgay_ct3
            // 
            this.txtNgay_ct3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgay_ct3.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct3.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtNgay_ct3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct3.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct3.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct3.Location = new System.Drawing.Point(150, 53);
            this.txtNgay_ct3.Name = "txtNgay_ct3";
            this.txtNgay_ct3.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00066";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Kỳ này từ ngày";
            // 
            // txtNgay_ct2
            // 
            this.txtNgay_ct2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct2.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct2.Location = new System.Drawing.Point(150, 28);
            this.txtNgay_ct2.Name = "txtNgay_ct2";
            this.txtNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct2.TabIndex = 1;
            // 
            // txtNgay_ct1
            // 
            this.txtNgay_ct1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.txtNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNgay_ct1.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.txtNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.txtNgay_ct1.Location = new System.Drawing.Point(150, 3);
            this.txtNgay_ct1.Name = "txtNgay_ct1";
            this.txtNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct1.TabIndex = 0;
            // 
            // ACAKU2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNgay_ct4);
            this.Controls.Add(this.txtNgay_ct3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNgay_ct2);
            this.Controls.Add(this.txtNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ACAKU2";
            this.Size = new System.Drawing.Size(295, 302);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.RadioButton radOr;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox7;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku3;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private V6Controls.V6DateTimePick txtNgay_ct4;
        private V6Controls.V6DateTimePick txtNgay_ct3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick txtNgay_ct2;
        private V6Controls.V6DateTimePick txtNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox10;
        private V6ReportControls.FilterLineVvarTextBox Tk_filterLine;
    }
}
