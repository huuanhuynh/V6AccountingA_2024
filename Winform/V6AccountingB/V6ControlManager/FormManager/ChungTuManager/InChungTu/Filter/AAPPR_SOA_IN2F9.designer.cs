namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    partial class AAPPR_SOA_IN2F9
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
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtStt_rec = new V6Controls.V6VvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 221);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            this.groupBox1.Visible = false;
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
            // v6Label9
            // 
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(21, 57);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(56, 13);
            this.v6Label9.TabIndex = 15;
            this.v6Label9.Text = "STT_REC";
            this.v6Label9.Visible = false;
            // 
            // TxtStt_rec
            // 
            this.TxtStt_rec.AccessibleName = "STT_REC";
            this.TxtStt_rec.BackColor = System.Drawing.SystemColors.Window;
            this.TxtStt_rec.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtStt_rec.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtStt_rec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtStt_rec.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtStt_rec.HoverColor = System.Drawing.Color.Yellow;
            this.TxtStt_rec.LeaveColor = System.Drawing.Color.White;
            this.TxtStt_rec.Location = new System.Drawing.Point(131, 55);
            this.TxtStt_rec.Name = "TxtStt_rec";
            this.TxtStt_rec.Size = new System.Drawing.Size(100, 20);
            this.TxtStt_rec.TabIndex = 16;
            this.TxtStt_rec.Visible = false;
            // 
            // AAPPR_SOA_IN2F9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtStt_rec);
            this.Controls.Add(this.groupBox1);
            this.Name = "AAPPR_SOA_IN2F9";
            this.Size = new System.Drawing.Size(295, 359);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtStt_rec;
    }
}
