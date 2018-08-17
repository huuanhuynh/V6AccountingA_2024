namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACOPHITH1F5
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
            this.Tk_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.Tk_du_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.txtma_phi = new V6ReportControls.FilterLineVvarTextBox();
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
            this.groupBox1.Controls.Add(this.Tk_filterLine);
            this.groupBox1.Controls.Add(this.Tk_du_filterLine);
            this.groupBox1.Controls.Add(this.txtma_phi);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 120);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Tk_filterLine
            // 
            this.Tk_filterLine.AccessibleDescription = "FILTERL00027";
            this.Tk_filterLine.AccessibleName2 = "TK";
            this.Tk_filterLine.Enabled = false;
            this.Tk_filterLine.Caption = "Tài khoản";
            this.Tk_filterLine.FieldName = "TK";
            this.Tk_filterLine.IsSelected = true;
            this.Tk_filterLine.Location = new System.Drawing.Point(7, 63);
            this.Tk_filterLine.Name = "Tk_filterLine";
            this.Tk_filterLine.Size = new System.Drawing.Size(282, 22);
            this.Tk_filterLine.TabIndex = 28;
            this.Tk_filterLine.Vvar = "TK";
            // 
            // Tk_du_filterLine
            // 
            this.Tk_du_filterLine.AccessibleDescription = "FILTERL00046";
            this.Tk_du_filterLine.AccessibleName2 = "TK_DU";
            this.Tk_du_filterLine.Enabled = false;
            this.Tk_du_filterLine.Caption = "TK đối ứng";
            this.Tk_du_filterLine.FieldName = "TK_DU";
            this.Tk_du_filterLine.IsSelected = true;
            this.Tk_du_filterLine.Location = new System.Drawing.Point(7, 86);
            this.Tk_du_filterLine.Name = "Tk_du_filterLine";
            this.Tk_du_filterLine.Size = new System.Drawing.Size(282, 22);
            this.Tk_du_filterLine.TabIndex = 27;
            this.Tk_du_filterLine.Vvar = "TK";
            // 
            // txtma_phi
            // 
            this.txtma_phi.AccessibleDescription = "FILTERL00069";
            this.txtma_phi.AccessibleName2 = "MA_PHI";
            this.txtma_phi.Enabled = false;
            this.txtma_phi.Caption = "Mã phí";
            this.txtma_phi.FieldName = "MA_PHI";
            this.txtma_phi.IsSelected = true;
            this.txtma_phi.Location = new System.Drawing.Point(7, 40);
            this.txtma_phi.Name = "txtma_phi";
            this.txtma_phi.Operator = "=";
            this.txtma_phi.Size = new System.Drawing.Size(282, 22);
            this.txtma_phi.TabIndex = 25;
            this.txtma_phi.Vvar = "MA_PHI";
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
            // ACOPHITH1F5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ACOPHITH1F5";
            this.Size = new System.Drawing.Size(307, 149);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox txtma_phi;
        private V6ReportControls.FilterLineVvarTextBox Tk_filterLine;
        private V6ReportControls.FilterLineVvarTextBox Tk_du_filterLine;
    }
}
