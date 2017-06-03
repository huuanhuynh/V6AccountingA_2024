namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACOVVTH3
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNgay_ct2 = new V6Controls.V6DateTimePick();
            this.txtNgay_ct1 = new V6Controls.V6DateTimePick();
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
            this.groupBox1.Location = new System.Drawing.Point(3, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 129);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
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
            // chk_Luy_ke
            // 
            this.chk_Luy_ke.AccessibleName = "luy_ke";
            this.chk_Luy_ke.AutoSize = true;
            this.chk_Luy_ke.Location = new System.Drawing.Point(120, 65);
            this.chk_Luy_ke.Margin = new System.Windows.Forms.Padding(4);
            this.chk_Luy_ke.Name = "chk_Luy_ke";
            this.chk_Luy_ke.Size = new System.Drawing.Size(66, 17);
            this.chk_Luy_ke.TabIndex = 16;
            this.chk_Luy_ke.Text = "In lũy kế";
            this.chk_Luy_ke.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Từ ngày";
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
            this.txtNgay_ct2.Location = new System.Drawing.Point(120, 29);
            this.txtNgay_ct2.Name = "txtNgay_ct2";
            this.txtNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct2.TabIndex = 23;
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
            this.txtNgay_ct1.Location = new System.Drawing.Point(120, 3);
            this.txtNgay_ct1.Name = "txtNgay_ct1";
            this.txtNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.txtNgay_ct1.TabIndex = 21;
            // 
            // ACOVVTH3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNgay_ct2);
            this.Controls.Add(this.txtNgay_ct1);
            this.Controls.Add(this.chk_Luy_ke);
            this.Controls.Add(this.groupBox1);
            this.Name = "ACOVVTH3";
            this.Size = new System.Drawing.Size(295, 221);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick txtNgay_ct2;
        private V6Controls.V6DateTimePick txtNgay_ct1;
    }
}
