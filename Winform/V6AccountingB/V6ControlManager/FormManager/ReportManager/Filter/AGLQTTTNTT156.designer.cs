namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLQTTTNTT156
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtThang2 = new V6Controls.V6NumberTextBox();
            this.txtNam2 = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 39);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(3, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 80);
            this.groupBox1.TabIndex = 0;
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
            this.txtNam.Location = new System.Drawing.Point(115, 42);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 11;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtThang1
            // 
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(115, 16);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(100, 20);
            this.txtThang1.TabIndex = 7;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang2_TextChanged);
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(6, 45);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 10;
            this.v6Label9.Text = "Năm";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00053";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Từ tháng";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00054";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Đến tháng";
            // 
            // txtThang2
            // 
            this.txtThang2.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang2.DecimalPlaces = 0;
            this.txtThang2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang2.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang2.LeaveColor = System.Drawing.Color.White;
            this.txtThang2.Location = new System.Drawing.Point(115, 70);
            this.txtThang2.MaxLength = 2;
            this.txtThang2.MaxNumLength = 2;
            this.txtThang2.Name = "txtThang2";
            this.txtThang2.Size = new System.Drawing.Size(100, 20);
            this.txtThang2.TabIndex = 9;
            this.txtThang2.Text = "0";
            this.txtThang2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang2.TextChanged += new System.EventHandler(this.txtThang2_TextChanged);
            // 
            // txtNam2
            // 
            this.txtNam2.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam2.DecimalPlaces = 0;
            this.txtNam2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam2.LeaveColor = System.Drawing.Color.White;
            this.txtNam2.Location = new System.Drawing.Point(115, 98);
            this.txtNam2.MaxLength = 4;
            this.txtNam2.MaxNumLength = 4;
            this.txtNam2.Name = "txtNam2";
            this.txtNam2.Size = new System.Drawing.Size(100, 20);
            this.txtNam2.TabIndex = 13;
            this.txtNam2.Text = "0";
            this.txtNam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00109";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 101);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(29, 13);
            this.v6Label1.TabIndex = 12;
            this.v6Label1.Text = "Năm";
            // 
            // AGLQTTTNTT156
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNam2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang2);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLQTTTNTT156";
            this.Size = new System.Drawing.Size(295, 216);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6NumberTextBox txtThang2;
        private V6Controls.V6NumberTextBox txtNam2;
        private V6Controls.V6Label v6Label1;
    }
}
