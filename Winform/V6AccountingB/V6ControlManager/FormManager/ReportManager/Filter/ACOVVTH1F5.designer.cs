﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACOVVTH1F5
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
            this.TK_DU_filterLine = new V6ReportControls.FilterLineVvarTextBox();
            this.TK_filterLine = new V6ReportControls.FilterLineVvarTextBox();
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
            this.groupBox1.Controls.Add(this.TK_DU_filterLine);
            this.groupBox1.Controls.Add(this.TK_filterLine);
            this.groupBox1.Controls.Add(this.ma_vv_filterLine);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 119);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // TK_DU_filterLine
            // 
            this.TK_DU_filterLine.AccessibleDescription = "FILTERL00096";
            this.TK_DU_filterLine.AccessibleName2 = "tk_du";
            this.TK_DU_filterLine.Enabled = false;
            this.TK_DU_filterLine.Caption = "Tài khoản đối ứng";
            this.TK_DU_filterLine.FieldName = "tk_du";
            this.TK_DU_filterLine.Location = new System.Drawing.Point(6, 87);
            this.TK_DU_filterLine.Name = "TK_DU_filterLine";
            this.TK_DU_filterLine.Size = new System.Drawing.Size(289, 22);
            this.TK_DU_filterLine.TabIndex = 9;
            this.TK_DU_filterLine.Vvar = "tk_du";
            // 
            // TK_filterLine
            // 
            this.TK_filterLine.AccessibleDescription = "FILTERL00027";
            this.TK_filterLine.AccessibleName2 = "tk";
            this.TK_filterLine.Enabled = false;
            this.TK_filterLine.Caption = "Tài khoản";
            this.TK_filterLine.FieldName = "tk";
            this.TK_filterLine.Location = new System.Drawing.Point(6, 63);
            this.TK_filterLine.Name = "TK_filterLine";
            this.TK_filterLine.Size = new System.Drawing.Size(289, 22);
            this.TK_filterLine.TabIndex = 8;
            this.TK_filterLine.Vvar = "tk";
            // 
            // ma_vv_filterLine
            // 
            this.ma_vv_filterLine.AccessibleDescription = "FILTERL00065";
            this.ma_vv_filterLine.AccessibleName2 = "MA_VV";
            this.ma_vv_filterLine.Enabled = false;
            this.ma_vv_filterLine.Caption = "Mã vụ việc";
            this.ma_vv_filterLine.FieldName = "MA_VV";
            this.ma_vv_filterLine.Location = new System.Drawing.Point(6, 39);
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
            // ACOVVTH1F5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ACOVVTH1F5";
            this.Size = new System.Drawing.Size(299, 177);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox TK_DU_filterLine;
        private V6ReportControls.FilterLineVvarTextBox TK_filterLine;
        private V6ReportControls.FilterLineVvarTextBox ma_vv_filterLine;
    }
}
