﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACOVVTH2F5
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
            this.ma_vt_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.ma_vv_filterLine = new V6ReportControls.FilterLineVvarTextBox();
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
            this.groupBox1.Controls.Add(this.ma_vt_filterLine);
            this.groupBox1.Controls.Add(this.ma_vv_filterLine);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // ma_vt_filterLine
            // 
            this.ma_vt_filterLine.AccessibleDescription = "FILTERL00020";
            this.ma_vt_filterLine.AccessibleName2 = "MA_VT";
            this.ma_vt_filterLine.Enabled = false;
            this.ma_vt_filterLine.Caption = "Mã vật tư";
            this.ma_vt_filterLine.FieldName = "MA_VT";
            this.ma_vt_filterLine.Location = new System.Drawing.Point(5, 62);
            this.ma_vt_filterLine.Name = "ma_vt_filterLine";
            this.ma_vt_filterLine.Operator = "=";
            this.ma_vt_filterLine.Size = new System.Drawing.Size(285, 22);
            this.ma_vt_filterLine.TabIndex = 6;
            this.ma_vt_filterLine.Vvar = "MA_VT";
            // 
            // ma_vv_filterLine
            // 
            this.ma_vv_filterLine.AccessibleDescription = "FILTERL00065";
            this.ma_vv_filterLine.AccessibleName2 = "MA_VV";
            this.ma_vv_filterLine.Enabled = false;
            this.ma_vv_filterLine.Caption = "Mã vụ việc";
            this.ma_vv_filterLine.FieldName = "MA_VV";
            this.ma_vv_filterLine.Location = new System.Drawing.Point(5, 40);
            this.ma_vv_filterLine.Name = "ma_vv_filterLine";
            this.ma_vv_filterLine.Operator = "=";
            this.ma_vv_filterLine.Size = new System.Drawing.Size(285, 22);
            this.ma_vv_filterLine.TabIndex = 7;
            this.ma_vv_filterLine.Vvar = "MA_VV";
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
            // ACOVVTH2F5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ACOVVTH2F5";
            this.Size = new System.Drawing.Size(299, 158);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox ma_vv_filterLine;
        private V6ReportControls.FilterLineVvarTextBox ma_vt_filterLine;
    }
}
