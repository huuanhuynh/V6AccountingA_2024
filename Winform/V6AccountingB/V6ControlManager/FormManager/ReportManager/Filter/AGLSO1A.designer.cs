﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLSO1A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AGLSO1A));
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.label1 = new V6Controls.V6Label();
            this.label2 = new V6Controls.V6Label();
            this.rdo_goptk = new System.Windows.Forms.RadioButton();
            this.rdo_khonggoptk = new System.Windows.Forms.RadioButton();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtTk = new V6Controls.V6VvarTextBox();
            this.Txtten_tk = new V6Controls.V6ColorTextBox();
            this.dateSelectButton1 = new V6Controls.Controls.DateSelectButton();
            this.dateSelectButton2 = new V6Controls.Controls.DateSelectButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 41);
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
            this.groupBox1.Location = new System.Drawing.Point(0, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 74);
            this.groupBox1.TabIndex = 8;
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
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(131, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(131, 29);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // rdo_goptk
            // 
            this.rdo_goptk.AccessibleDescription = "FILTERR00005";
            this.rdo_goptk.AccessibleName = "RDO_GOP_TK";
            this.rdo_goptk.AutoSize = true;
            this.rdo_goptk.Location = new System.Drawing.Point(161, 140);
            this.rdo_goptk.Name = "rdo_goptk";
            this.rdo_goptk.Size = new System.Drawing.Size(92, 17);
            this.rdo_goptk.TabIndex = 7;
            this.rdo_goptk.Text = "Gộp tài khoản";
            this.rdo_goptk.UseVisualStyleBackColor = true;
            // 
            // rdo_khonggoptk
            // 
            this.rdo_khonggoptk.AccessibleDescription = "FILTERR00006";
            this.rdo_khonggoptk.AccessibleName = "RDO_KHONG_GOP_TK";
            this.rdo_khonggoptk.AutoSize = true;
            this.rdo_khonggoptk.Checked = true;
            this.rdo_khonggoptk.Location = new System.Drawing.Point(25, 140);
            this.rdo_khonggoptk.Name = "rdo_khonggoptk";
            this.rdo_khonggoptk.Size = new System.Drawing.Size(93, 17);
            this.rdo_khonggoptk.TabIndex = 6;
            this.rdo_khonggoptk.TabStop = true;
            this.rdo_khonggoptk.Text = "Không gộp Tk";
            this.rdo_khonggoptk.UseVisualStyleBackColor = true;
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00027";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 57);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Tài khoản";
            // 
            // TxtTk
            // 
            this.TxtTk.AccessibleName = "TK";
            this.TxtTk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk.BrotherFields = "TEN_TK";
            this.TxtTk.BrotherFields2 = "TEN_TK2";
            this.TxtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk.LeaveColor = System.Drawing.Color.White;
            this.TxtTk.Location = new System.Drawing.Point(131, 55);
            this.TxtTk.Name = "TxtTk";
            this.TxtTk.Size = new System.Drawing.Size(100, 20);
            this.TxtTk.TabIndex = 5;
            this.TxtTk.VVar = "TK";
            // 
            // Txtten_tk
            // 
            this.Txtten_tk.AccessibleName = "TEN_TK";
            this.Txtten_tk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtten_tk.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Txtten_tk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtten_tk.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtten_tk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtten_tk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtten_tk.HoverColor = System.Drawing.Color.Yellow;
            this.Txtten_tk.LeaveColor = System.Drawing.Color.White;
            this.Txtten_tk.Location = new System.Drawing.Point(5, 86);
            this.Txtten_tk.Margin = new System.Windows.Forms.Padding(4);
            this.Txtten_tk.Multiline = true;
            this.Txtten_tk.Name = "Txtten_tk";
            this.Txtten_tk.ReadOnly = true;
            this.Txtten_tk.Size = new System.Drawing.Size(284, 46);
            this.Txtten_tk.TabIndex = 44;
            this.Txtten_tk.TabStop = false;
            // 
            // dateSelectButton1
            // 
            this.dateSelectButton1.Image = ((System.Drawing.Image)(resources.GetObject("dateSelectButton1.Image")));
            this.dateSelectButton1.Location = new System.Drawing.Point(231, 3);
            this.dateSelectButton1.Name = "dateSelectButton1";
            this.dateSelectButton1.ReferenceControl = this.dateNgay_ct1;
            this.dateSelectButton1.Size = new System.Drawing.Size(21, 21);
            this.dateSelectButton1.TabIndex = 45;
            // 
            // dateSelectButton2
            // 
            this.dateSelectButton2.Image = ((System.Drawing.Image)(resources.GetObject("dateSelectButton2.Image")));
            this.dateSelectButton2.Location = new System.Drawing.Point(231, 29);
            this.dateSelectButton2.Name = "dateSelectButton2";
            this.dateSelectButton2.ReferenceControl = this.dateNgay_ct2;
            this.dateSelectButton2.Size = new System.Drawing.Size(21, 21);
            this.dateSelectButton2.TabIndex = 45;
            // 
            // AGLSO1A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateSelectButton2);
            this.Controls.Add(this.dateSelectButton1);
            this.Controls.Add(this.Txtten_tk);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtTk);
            this.Controls.Add(this.rdo_goptk);
            this.Controls.Add(this.rdo_khonggoptk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLSO1A";
            this.Size = new System.Drawing.Size(295, 239);
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
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6Label label1;
        private V6Controls.V6Label label2;
        private System.Windows.Forms.RadioButton rdo_goptk;
        private System.Windows.Forms.RadioButton rdo_khonggoptk;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtTk;
        private V6Controls.V6ColorTextBox Txtten_tk;
        private V6Controls.Controls.DateSelectButton dateSelectButton1;
        private V6Controls.Controls.DateSelectButton dateSelectButton2;
    }
}
