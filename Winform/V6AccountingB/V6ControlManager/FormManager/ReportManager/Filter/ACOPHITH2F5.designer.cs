namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACOPHITH2F5
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
            this.txtma_phi = new V6ReportControls.FilterLineVvarTextBox();
            this.txtTk_du = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtma_phi);
            this.groupBox1.Controls.Add(this.txtTk_du);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtma_phi
            // 
            this.txtma_phi.AccessibleDescription = "FILTERL00069";
            this.txtma_phi.AccessibleName2 = "MA_PHI";
            this.txtma_phi.Enabled = false;
            this.txtma_phi.Caption = "Mã phí";
            this.txtma_phi.FieldName = "MA_PHI";
            this.txtma_phi.Location = new System.Drawing.Point(5, 64);
            this.txtma_phi.Name = "txtma_phi";
            this.txtma_phi.Operator = "=";
            this.txtma_phi.Size = new System.Drawing.Size(282, 22);
            this.txtma_phi.TabIndex = 12;
            this.txtma_phi.Vvar = "MA_PHI";
            // 
            // txtTk_du
            // 
            this.txtTk_du.AccessibleDescription = "FILTERL00046";
            this.txtTk_du.AccessibleName2 = "TK_DU";
            this.txtTk_du.Enabled = false;
            this.txtTk_du.Caption = "TK đối ứng";
            this.txtTk_du.FieldName = "TK_DU";
            this.txtTk_du.Location = new System.Drawing.Point(5, 42);
            this.txtTk_du.Name = "txtTk_du";
            this.txtTk_du.Size = new System.Drawing.Size(282, 22);
            this.txtTk_du.TabIndex = 11;
            this.txtTk_du.Vvar = "TK";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(132, 17);
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
            this.radAnd.Location = new System.Drawing.Point(4, 17);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(130, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Tất cả điều kiện (and)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // ACOPHITH2F5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ACOPHITH2F5";
            this.Size = new System.Drawing.Size(307, 158);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox txtma_phi;
        private V6ReportControls.FilterLineVvarTextBox txtTk_du;
    }
}
