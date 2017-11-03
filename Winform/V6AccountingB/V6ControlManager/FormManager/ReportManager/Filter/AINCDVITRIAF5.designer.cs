namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AINCDVITRIAF5
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
            this.ma_vt_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtMavitri = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.ma_kh_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ma_vt_filterLine
            // 
            this.ma_vt_filterLine.AccessibleDescription = "FILTERL00020";
            this.ma_vt_filterLine.AccessibleName2 = "MA_VT";
            this.ma_vt_filterLine.Enabled = false;
            this.ma_vt_filterLine.FieldCaption = "Mã vật tư";
            this.ma_vt_filterLine.FieldName = "MA_VT";
            this.ma_vt_filterLine.Location = new System.Drawing.Point(6, 50);
            this.ma_vt_filterLine.Name = "ma_vt_filterLine";
            this.ma_vt_filterLine.Operator = "=";
            this.ma_vt_filterLine.Size = new System.Drawing.Size(282, 22);
            this.ma_vt_filterLine.TabIndex = 4;
            this.ma_vt_filterLine.Vvar = "MA_VT";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtMavitri);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.ma_vt_filterLine);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 131);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // TxtMavitri
            // 
            this.TxtMavitri.AccessibleDescription = "FILTERL00160";
            this.TxtMavitri.AccessibleName2 = "MA_VITRI";
            this.TxtMavitri.Enabled = false;
            this.TxtMavitri.FieldCaption = "Mã vị trí";
            this.TxtMavitri.FieldName = "MA_VITRI";
            this.TxtMavitri.Location = new System.Drawing.Point(6, 74);
            this.TxtMavitri.Name = "TxtMavitri";
            this.TxtMavitri.Operator = "=";
            this.TxtMavitri.Size = new System.Drawing.Size(282, 22);
            this.TxtMavitri.TabIndex = 6;
            this.TxtMavitri.Vvar = "MA_DVCS";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Enabled = false;
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
            this.radAnd.Enabled = false;
            this.radAnd.Location = new System.Drawing.Point(6, 16);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // ma_kh_filterLine
            // 
            this.ma_kh_filterLine.Enabled = false;
            this.ma_kh_filterLine.FieldCaption = "Mã vật tư";
            this.ma_kh_filterLine.FieldName = null;
            this.ma_kh_filterLine.Location = new System.Drawing.Point(6, 49);
            this.ma_kh_filterLine.Name = "ma_kh_filterLine";
            this.ma_kh_filterLine.Size = new System.Drawing.Size(282, 22);
            this.ma_kh_filterLine.TabIndex = 4;
            this.ma_kh_filterLine.Vvar = "MA_VT";
            // 
            // AINCDVITRIAF5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AINCDVITRIAF5";
            this.Size = new System.Drawing.Size(323, 137);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox ma_vt_filterLine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox ma_kh_filterLine;
        private V6ReportControls.FilterLineVvarTextBox TxtMavitri;
    }
}
