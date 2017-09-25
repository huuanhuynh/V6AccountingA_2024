﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLSD2
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct = new V6Controls.V6DateTimePick();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtTk = new V6Controls.V6VvarTextBox();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(3, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(3, 53);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 4;
            this.txtMaDvcs.Vvar = "MA_DVCS";
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
            // dateNgay_ct
            // 
            this.dateNgay_ct.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct.Location = new System.Drawing.Point(100, 18);
            this.dateNgay_ct.Name = "dateNgay_ct";
            this.dateNgay_ct.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00003";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đến ngày";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00027";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 56);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 17;
            this.v6Label9.Text = "Tài khoản";
            // 
            // TxtTk
            // 
            this.TxtTk.AccessibleName = "TK";
            this.TxtTk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk.LeaveColor = System.Drawing.Color.White;
            this.TxtTk.Location = new System.Drawing.Point(100, 54);
            this.TxtTk.Name = "TxtTk";
            this.TxtTk.Size = new System.Drawing.Size(100, 20);
            this.TxtTk.TabIndex = 16;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(80, 18);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // AGLSD2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtTk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLSD2";
            this.Size = new System.Drawing.Size(295, 280);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePick dateNgay_ct;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtTk;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
    }
}
