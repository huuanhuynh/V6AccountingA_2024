namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLSD1
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
            this.filter_tk = new V6ReportControls.FilterLineVvarTextBox();
            this.filter_ma_dvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct = new V6Controls.V6DateTimePick();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.filter_tk);
            this.groupBox1.Controls.Add(this.filter_ma_dvcs);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // filter_tk
            // 
            this.filter_tk.AccessibleDescription = "FILTERL00027";
            this.filter_tk.FieldCaption = "Tài khoản";
            this.filter_tk.FieldName = "TK";
            this.filter_tk.Location = new System.Drawing.Point(3, 75);
            this.filter_tk.Name = "filter_tk";
            this.filter_tk.Size = new System.Drawing.Size(282, 22);
            this.filter_tk.TabIndex = 6;
            this.filter_tk.Vvar = "MA_DVCS";
            // 
            // filter_ma_dvcs
            // 
            this.filter_ma_dvcs.AccessibleDescription = "FILTERL00005";
            this.filter_ma_dvcs.FieldCaption = "Mã đơn vị";
            this.filter_ma_dvcs.FieldName = "MA_DVCS";
            this.filter_ma_dvcs.Location = new System.Drawing.Point(3, 52);
            this.filter_ma_dvcs.Name = "filter_ma_dvcs";
            this.filter_ma_dvcs.Size = new System.Drawing.Size(282, 22);
            this.filter_ma_dvcs.TabIndex = 5;
            this.filter_ma_dvcs.Vvar = "MA_DVCS";
            // 
            // txtMaDvcs
            // 
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
            // dateNgay_ct
            // 
            this.dateNgay_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct.Location = new System.Drawing.Point(100, 18);
            this.dateNgay_ct.Name = "dateNgay_ct";
            this.dateNgay_ct.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00132";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đầu ngày";
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(80, 18);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // AGLSD1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLSD1";
            this.Size = new System.Drawing.Size(295, 280);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePick dateNgay_ct;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox filter_tk;
        private V6ReportControls.FilterLineVvarTextBox filter_ma_dvcs;
    }
}
