﻿namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    partial class AldmvtFilterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AldmvtFilterForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dateTuNgay = new V6Controls.V6DateTimeColor();
            this.dateDenNgay = new V6Controls.V6DateTimePicker();
            this.btnCopyDinhMuc = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNhomVT3 = new V6Controls.V6VvarTextBox();
            this.txtNhomVT2 = new V6Controls.V6VvarTextBox();
            this.txtNhomVT1 = new V6Controls.V6VvarTextBox();
            this.txtMaSP = new V6Controls.V6VvarTextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "FILTERB00008";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(103, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tag = "Escape";
            this.btnCancel.Text = "Đóng";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "FILTERB00007";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnFilter.Location = new System.Drawing.Point(12, 130);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(88, 40);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Tag = "Return, Control";
            this.btnFilter.Text = "&Nhận";
            this.btnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNhomVT3);
            this.panel1.Controls.Add(this.txtNhomVT2);
            this.panel1.Controls.Add(this.txtNhomVT1);
            this.panel1.Controls.Add(this.txtMaSP);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 126);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00006";
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.lblFrom);
            this.groupBox1.Controls.Add(this.dateTuNgay);
            this.groupBox1.Controls.Add(this.dateDenNgay);
            this.groupBox1.Controls.Add(this.btnCopyDinhMuc);
            this.groupBox1.Location = new System.Drawing.Point(319, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 120);
            this.groupBox1.TabIndex = 112;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copy định mức";
            // 
            // lblTo
            // 
            this.lblTo.AccessibleDescription = "FILTERL00255";
            this.lblTo.AccessibleName = "";
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(12, 55);
            this.lblTo.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 13);
            this.lblTo.TabIndex = 112;
            this.lblTo.Text = "Đến";
            // 
            // lblFrom
            // 
            this.lblFrom.AccessibleDescription = "FILTERL00254";
            this.lblFrom.AccessibleName = "";
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(12, 24);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(20, 13);
            this.lblFrom.TabIndex = 111;
            this.lblFrom.Text = "Từ";
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.BackColor = System.Drawing.Color.White;
            this.dateTuNgay.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateTuNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateTuNgay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateTuNgay.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateTuNgay.GrayText = null;
            this.dateTuNgay.HoverColor = System.Drawing.Color.Yellow;
            this.dateTuNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateTuNgay.LeaveColor = System.Drawing.Color.White;
            this.dateTuNgay.Location = new System.Drawing.Point(67, 21);
            this.dateTuNgay.Margin = new System.Windows.Forms.Padding(5);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Size = new System.Drawing.Size(135, 20);
            this.dateTuNgay.StringValue = "__/__/____";
            this.dateTuNgay.TabIndex = 110;
            this.dateTuNgay.Text = "__/__/____";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dateDenNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDenNgay.HoverColor = System.Drawing.Color.Yellow;
            this.dateDenNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateDenNgay.LeaveColor = System.Drawing.Color.White;
            this.dateDenNgay.Location = new System.Drawing.Point(67, 49);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Size = new System.Drawing.Size(135, 20);
            this.dateDenNgay.TabIndex = 8;
            // 
            // btnCopyDinhMuc
            // 
            this.btnCopyDinhMuc.AccessibleDescription = "FILTERB00009";
            this.btnCopyDinhMuc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyDinhMuc.Image = global::V6ControlManager.Properties.Resources.Copy;
            this.btnCopyDinhMuc.Location = new System.Drawing.Point(77, 77);
            this.btnCopyDinhMuc.Name = "btnCopyDinhMuc";
            this.btnCopyDinhMuc.Size = new System.Drawing.Size(114, 40);
            this.btnCopyDinhMuc.TabIndex = 1;
            this.btnCopyDinhMuc.Text = "&Copy";
            this.btnCopyDinhMuc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopyDinhMuc.UseVisualStyleBackColor = true;
            this.btnCopyDinhMuc.Click += new System.EventHandler(this.btnCopyDinhMuc_Click);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "FILTERL00250";
            this.label4.AccessibleName = "";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nhóm sản phẩm 3";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00249";
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhóm sản phẩm 2";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00248";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhóm sản phẩm 1";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00077";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sản phẩm";
            // 
            // txtNhomVT3
            // 
            this.txtNhomVT3.AccessibleName = "NH_VT3";
            this.txtNhomVT3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVT3.BackColor = System.Drawing.Color.White;
            this.txtNhomVT3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNhomVT3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVT3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVT3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT3.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVT3.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVT3.Location = new System.Drawing.Point(111, 87);
            this.txtNhomVT3.Name = "txtNhomVT3";
            this.txtNhomVT3.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVT3.TabIndex = 7;
            this.txtNhomVT3.VVar = "NH_VT";
            // 
            // txtNhomVT2
            // 
            this.txtNhomVT2.AccessibleName = "NH_VT2";
            this.txtNhomVT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVT2.BackColor = System.Drawing.Color.White;
            this.txtNhomVT2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNhomVT2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVT2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVT2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVT2.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVT2.Location = new System.Drawing.Point(111, 61);
            this.txtNhomVT2.Name = "txtNhomVT2";
            this.txtNhomVT2.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVT2.TabIndex = 5;
            this.txtNhomVT2.VVar = "NH_VT";
            // 
            // txtNhomVT1
            // 
            this.txtNhomVT1.AccessibleName = "NH_VT1";
            this.txtNhomVT1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVT1.BackColor = System.Drawing.Color.White;
            this.txtNhomVT1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNhomVT1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVT1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVT1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNhomVT1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVT1.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVT1.Location = new System.Drawing.Point(111, 35);
            this.txtNhomVT1.Name = "txtNhomVT1";
            this.txtNhomVT1.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVT1.TabIndex = 3;
            this.txtNhomVT1.VVar = "NH_VT";
            // 
            // txtMaSP
            // 
            this.txtMaSP.AccessibleName = "MA_SP";
            this.txtMaSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSP.BackColor = System.Drawing.Color.White;
            this.txtMaSP.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaSP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaSP.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaSP.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaSP.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSP.LeaveColor = System.Drawing.Color.White;
            this.txtMaSP.Location = new System.Drawing.Point(111, 9);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(202, 20);
            this.txtMaSP.TabIndex = 1;
            this.txtMaSP.VVar = "MA_VT";
            // 
            // AldmvtFilterForm
            // 
            this.AccessibleDescription = "FILTERF00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 175);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "AldmvtFilterForm";
            this.ShowInTaskbar = false;
            this.Text = "Lọc";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private V6Controls.V6VvarTextBox txtMaSP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6VvarTextBox txtNhomVT3;
        private V6Controls.V6VvarTextBox txtNhomVT2;
        private V6Controls.V6VvarTextBox txtNhomVT1;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6DateTimeColor dateTuNgay;
        private V6Controls.V6DateTimePicker dateDenNgay;
        private System.Windows.Forms.Button btnCopyDinhMuc;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
    }
}