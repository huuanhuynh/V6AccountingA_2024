namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AINTH2F5
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
            this.ma_nx_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.ma_bp_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.ma_vv_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.ma_kh_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ma_nx_filterLine);
            this.groupBox1.Controls.Add(this.ma_bp_filterLine);
            this.groupBox1.Controls.Add(this.ma_vv_filterLine);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.ma_kh_filterLine);
            this.groupBox1.Location = new System.Drawing.Point(3, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 144);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // ma_nx_filterLine
            // 
            this.ma_nx_filterLine.AccessibleDescription = "FILTERL00097";
            this.ma_nx_filterLine.Enabled = false;
            this.ma_nx_filterLine.FieldCaption = "Mã NX";
            this.ma_nx_filterLine.FieldName = "MA_NX";
            this.ma_nx_filterLine.Location = new System.Drawing.Point(6, 112);
            this.ma_nx_filterLine.Name = "ma_nx_filterLine";
            this.ma_nx_filterLine.Size = new System.Drawing.Size(289, 22);
            this.ma_nx_filterLine.TabIndex = 9;
            this.ma_nx_filterLine.Vvar = "MA_NX";
            // 
            // ma_bp_filterLine
            // 
            this.ma_bp_filterLine.AccessibleDescription = "FILTERL00008";
            this.ma_bp_filterLine.Enabled = false;
            this.ma_bp_filterLine.FieldCaption = "Mã bộ phận";
            this.ma_bp_filterLine.FieldName = "MA_BP";
            this.ma_bp_filterLine.Location = new System.Drawing.Point(6, 88);
            this.ma_bp_filterLine.Name = "ma_bp_filterLine";
            this.ma_bp_filterLine.Operator = "=";
            this.ma_bp_filterLine.Size = new System.Drawing.Size(289, 22);
            this.ma_bp_filterLine.TabIndex = 8;
            this.ma_bp_filterLine.Vvar = "MA_BP";
            // 
            // ma_vv_filterLine
            // 
            this.ma_vv_filterLine.AccessibleDescription = "FILTERL00065";
            this.ma_vv_filterLine.Enabled = false;
            this.ma_vv_filterLine.FieldCaption = "Mã vụ việc";
            this.ma_vv_filterLine.FieldName = "MA_VV";
            this.ma_vv_filterLine.Location = new System.Drawing.Point(6, 64);
            this.ma_vv_filterLine.Name = "ma_vv_filterLine";
            this.ma_vv_filterLine.Operator = "=";
            this.ma_vv_filterLine.Size = new System.Drawing.Size(289, 22);
            this.ma_vv_filterLine.TabIndex = 7;
            this.ma_vv_filterLine.Vvar = "MA_VV";
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
            // ma_kh_filterLine
            // 
            this.ma_kh_filterLine.AccessibleDescription = "FILTERL00007";
            this.ma_kh_filterLine.Enabled = false;
            this.ma_kh_filterLine.FieldCaption = "Mã khách";
            this.ma_kh_filterLine.FieldName = "MA_KH";
            this.ma_kh_filterLine.Location = new System.Drawing.Point(6, 40);
            this.ma_kh_filterLine.Name = "ma_kh_filterLine";
            this.ma_kh_filterLine.Operator = "=";
            this.ma_kh_filterLine.Size = new System.Drawing.Size(289, 22);
            this.ma_kh_filterLine.TabIndex = 6;
            this.ma_kh_filterLine.Vvar = "MA_KH";
            // 
            // AINTH2F5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AINTH2F5";
            this.Size = new System.Drawing.Size(307, 202);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox ma_kh_filterLine;
        private V6ReportControls.FilterLineVvarTextBox ma_nx_filterLine;
        private V6ReportControls.FilterLineVvarTextBox ma_bp_filterLine;
        private V6ReportControls.FilterLineVvarTextBox ma_vv_filterLine;
    }
}
