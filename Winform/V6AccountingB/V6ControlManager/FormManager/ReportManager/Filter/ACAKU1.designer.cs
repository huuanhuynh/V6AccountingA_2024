namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACAKU1
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
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.filterLineVvarTextBox7 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox10 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku3 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_ku1 = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.TxtTk = new V6Controls.V6VvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(150, 29);
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(150, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
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
            this.filterLineVvarTextBox7.AccessibleName2 = "MA_VV";
            this.filterLineVvarTextBox7.Caption = "Mã vụ việc";
            this.filterLineVvarTextBox7.FieldName = "MA_VV";
            this.filterLineVvarTextBox7.Location = new System.Drawing.Point(6, 94);
            this.filterLineVvarTextBox7.Name = "filterLineVvarTextBox7";
            this.filterLineVvarTextBox7.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox7.TabIndex = 4;
            this.filterLineVvarTextBox7.Vvar = "MA_VV";
            // 
            // filterLineVvarTextBox10
            // 
            this.filterLineVvarTextBox10.AccessibleDescription = "FILTERL00064";
            this.filterLineVvarTextBox10.AccessibleName2 = "MA_KU";
            this.filterLineVvarTextBox10.Caption = "Mã khế ước";
            this.filterLineVvarTextBox10.FieldName = "MA_KU";
            this.filterLineVvarTextBox10.Location = new System.Drawing.Point(6, 71);
            this.filterLineVvarTextBox10.Name = "filterLineVvarTextBox10";
            this.filterLineVvarTextBox10.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox10.TabIndex = 3;
            this.filterLineVvarTextBox10.Vvar = "MA_KU";
            // 
            // Txtnh_ku3
            // 
            this.Txtnh_ku3.AccessibleDescription = "FILTERL00073";
            this.Txtnh_ku3.AccessibleName2 = "NH_KU3";
            this.Txtnh_ku3.Caption = "Nhóm khế ước 3";
            this.Txtnh_ku3.FieldName = "NH_KU3";
            this.Txtnh_ku3.Location = new System.Drawing.Point(6, 163);
            this.Txtnh_ku3.Name = "Txtnh_ku3";
            this.Txtnh_ku3.Size = new System.Drawing.Size(282, 22);
            this.Txtnh_ku3.TabIndex = 7;
            this.Txtnh_ku3.Vvar = "NH_KU";
            // 
            // Txtnh_ku2
            // 
            this.Txtnh_ku2.AccessibleDescription = "FILTERL00072";
            this.Txtnh_ku2.AccessibleName2 = "NH_KU2";
            this.Txtnh_ku2.Caption = "Nhóm khế ước 2";
            this.Txtnh_ku2.FieldName = "NH_KU2";
            this.Txtnh_ku2.Location = new System.Drawing.Point(6, 140);
            this.Txtnh_ku2.Name = "Txtnh_ku2";
            this.Txtnh_ku2.Size = new System.Drawing.Size(282, 22);
            this.Txtnh_ku2.TabIndex = 6;
            this.Txtnh_ku2.Vvar = "NH_KU";
            // 
            // Txtnh_ku1
            // 
            this.Txtnh_ku1.AccessibleDescription = "FILTERL00071";
            this.Txtnh_ku1.AccessibleName2 = "NH_KU1";
            this.Txtnh_ku1.Caption = "Nhóm khế ước 1";
            this.Txtnh_ku1.FieldName = "NH_KU1";
            this.Txtnh_ku1.Location = new System.Drawing.Point(6, 117);
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
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.Txtnh_ku1);
            this.groupBox1.Controls.Add(this.Txtnh_ku2);
            this.groupBox1.Controls.Add(this.Txtnh_ku3);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox10);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox7);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 199);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 48);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00027";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(5, 59);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(55, 13);
            this.v6Label2.TabIndex = 31;
            this.v6Label2.Text = "Tài khoản";
            // 
            // TxtTk
            // 
            this.TxtTk.AccessibleName = "TK";
            this.TxtTk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk.LeaveColor = System.Drawing.Color.White;
            this.TxtTk.Location = new System.Drawing.Point(150, 55);
            this.TxtTk.Name = "TxtTk";
            this.TxtTk.Size = new System.Drawing.Size(100, 20);
            this.TxtTk.TabIndex = 2;
            this.TxtTk.VVar = "TK";
            // 
            // ACAKU1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.TxtTk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ACAKU1";
            this.Size = new System.Drawing.Size(295, 284);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.RadioButton radOr;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox7;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox10;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku3;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_ku1;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox TxtTk;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
    }
}
