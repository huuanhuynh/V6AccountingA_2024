namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class V6BACKUP1
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtngay_bk = new V6Controls.V6DateTimeColor();
            this.v6CheckBox1 = new V6Controls.V6CheckBox();
            this.v6CheckBox2 = new V6Controls.V6CheckBox();
            this.txtFileName = new V6Controls.V6ColorTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00213";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày lưu trữ sau cùng";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 15);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            this.groupBox1.Visible = false;
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
            // txtngay_bk
            // 
            this.txtngay_bk.AccessibleName = "ngay_bak";
            this.txtngay_bk.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtngay_bk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtngay_bk.Enabled = false;
            this.txtngay_bk.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_bk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtngay_bk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtngay_bk.GrayText = null;
            this.txtngay_bk.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_bk.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_bk.LeaveColor = System.Drawing.Color.White;
            this.txtngay_bk.Location = new System.Drawing.Point(120, 10);
            this.txtngay_bk.Margin = new System.Windows.Forms.Padding(5);
            this.txtngay_bk.Name = "txtngay_bk";
            this.txtngay_bk.ReadOnly = true;
            this.txtngay_bk.Size = new System.Drawing.Size(102, 20);
            this.txtngay_bk.StringValue = "__/__/____";
            this.txtngay_bk.TabIndex = 115;
            this.txtngay_bk.TabStop = false;
            this.txtngay_bk.Text = "__/__/____";
            // 
            // v6CheckBox1
            // 
            this.v6CheckBox1.AccessibleDescription = "FILTERC00019";
            this.v6CheckBox1.AutoSize = true;
            this.v6CheckBox1.Checked = true;
            this.v6CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.v6CheckBox1.Location = new System.Drawing.Point(11, 63);
            this.v6CheckBox1.Name = "v6CheckBox1";
            this.v6CheckBox1.Size = new System.Drawing.Size(119, 17);
            this.v6CheckBox1.TabIndex = 116;
            this.v6CheckBox1.Text = "Nén dữ liệu với 7zip";
            this.v6CheckBox1.UseVisualStyleBackColor = true;
            this.v6CheckBox1.CheckedChanged += new System.EventHandler(this.v6CheckBox1_CheckedChanged);
            // 
            // v6CheckBox2
            // 
            this.v6CheckBox2.AccessibleDescription = "FILTERC00020";
            this.v6CheckBox2.AutoSize = true;
            this.v6CheckBox2.Checked = true;
            this.v6CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.v6CheckBox2.Location = new System.Drawing.Point(11, 86);
            this.v6CheckBox2.Name = "v6CheckBox2";
            this.v6CheckBox2.Size = new System.Drawing.Size(73, 17);
            this.v6CheckBox2.TabIndex = 116;
            this.v6CheckBox2.Text = "Copy FTP";
            this.v6CheckBox2.UseVisualStyleBackColor = true;
            this.v6CheckBox2.Visible = false;
            this.v6CheckBox2.CheckedChanged += new System.EventHandler(this.v6CheckBox2_CheckedChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtFileName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFileName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFileName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFileName.HoverColor = System.Drawing.Color.Yellow;
            this.txtFileName.LeaveColor = System.Drawing.Color.White;
            this.txtFileName.Location = new System.Drawing.Point(120, 37);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(461, 20);
            this.txtFileName.TabIndex = 117;
            this.txtFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00214";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên file lưu trữ";
            // 
            // V6BACKUP1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.v6CheckBox2);
            this.Controls.Add(this.v6CheckBox1);
            this.Controls.Add(this.txtngay_bk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "V6BACKUP1";
            this.Size = new System.Drawing.Size(594, 149);
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
        private V6Controls.V6DateTimeColor txtngay_bk;
        private V6Controls.V6CheckBox v6CheckBox1;
        private V6Controls.V6CheckBox v6CheckBox2;
        private V6Controls.V6ColorTextBox txtFileName;
        private System.Windows.Forms.Label label2;
    }
}
