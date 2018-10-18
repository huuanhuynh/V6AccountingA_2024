namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AFABTPBTSN
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
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtKy1 = new V6Controls.V6NumberTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtKy2 = new V6Controls.V6NumberTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDien_giai = new V6Controls.V6ColorTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 58);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00110";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ kỳ";
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
            this.groupBox1.Location = new System.Drawing.Point(0, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 86);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(10, 43);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 17);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(156, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "Một trong các điều kiện (or)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 17);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(130, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Tất cả điều kiện (and)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // txtKy1
            // 
            this.txtKy1.BackColor = System.Drawing.SystemColors.Window;
            this.txtKy1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKy1.DecimalPlaces = 0;
            this.txtKy1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKy1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKy1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKy1.HoverColor = System.Drawing.Color.Yellow;
            this.txtKy1.LeaveColor = System.Drawing.Color.White;
            this.txtKy1.Location = new System.Drawing.Point(117, 7);
            this.txtKy1.MaxLength = 2;
            this.txtKy1.MaxNumLength = 2;
            this.txtKy1.Name = "txtKy1";
            this.txtKy1.Size = new System.Drawing.Size(100, 20);
            this.txtKy1.TabIndex = 0;
            this.txtKy1.Text = "0";
            this.txtKy1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKy1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKy1.V6LostFocus += new V6Controls.ControlEventHandle(this.txtKy2_V6LostFocus);
            this.txtKy1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // txtNam
            // 
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(117, 55);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNam.TextChanged += new System.EventHandler(this.txtNam_TextChanged);
            // 
            // txtKy2
            // 
            this.txtKy2.BackColor = System.Drawing.SystemColors.Window;
            this.txtKy2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKy2.DecimalPlaces = 0;
            this.txtKy2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKy2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKy2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKy2.HoverColor = System.Drawing.Color.Yellow;
            this.txtKy2.LeaveColor = System.Drawing.Color.White;
            this.txtKy2.Location = new System.Drawing.Point(117, 31);
            this.txtKy2.MaxLength = 2;
            this.txtKy2.MaxNumLength = 2;
            this.txtKy2.Name = "txtKy2";
            this.txtKy2.Size = new System.Drawing.Size(100, 20);
            this.txtKy2.TabIndex = 1;
            this.txtKy2.Text = "0";
            this.txtKy2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKy2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKy2.V6LostFocus += new V6Controls.ControlEventHandle(this.txtKy2_V6LostFocus);
            this.txtKy2.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00114";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Đến kỳ";
            // 
            // txtDien_giai
            // 
            this.txtDien_giai.AccessibleName = "DIEN_GIAI";
            this.txtDien_giai.BackColor = System.Drawing.SystemColors.Window;
            this.txtDien_giai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDien_giai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDien_giai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDien_giai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDien_giai.HoverColor = System.Drawing.Color.Yellow;
            this.txtDien_giai.LeaveColor = System.Drawing.Color.White;
            this.txtDien_giai.Location = new System.Drawing.Point(117, 82);
            this.txtDien_giai.Name = "txtDien_giai";
            this.txtDien_giai.Size = new System.Drawing.Size(444, 20);
            this.txtDien_giai.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "FILTERL00121";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Diễn giải";
            // 
            // AFABTPBTSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDien_giai);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtKy2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtKy1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AFABTPBTSN";
            this.Size = new System.Drawing.Size(594, 226);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6NumberTextBox txtKy1;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtKy2;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6ColorTextBox txtDien_giai;
        private System.Windows.Forms.Label label7;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
    }
}
