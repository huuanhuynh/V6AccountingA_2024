namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AINVITRI02_filter
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
            this.lineMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineMakho = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaVitri = new V6ReportControls.FilterLineVvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateCuoiNgay = new V6Controls.V6DateTimePick();
            this.v6Label9 = new V6Controls.V6Label();
            this.txtVtTonKho = new V6Controls.V6ColorTextBox();
            this.txtKieuIn = new V6Controls.V6ColorTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lineMaDvcs
            // 
            this.lineMaDvcs.AccessibleDescription = "FILTERL00005";
            this.lineMaDvcs.AccessibleName2 = "MA_DVCS";
            this.lineMaDvcs.FieldCaption = "Mã đơn vị";
            this.lineMaDvcs.FieldName = "MA_DVCS";
            this.lineMaDvcs.Location = new System.Drawing.Point(6, 67);
            this.lineMaDvcs.Name = "lineMaDvcs";
            this.lineMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.lineMaDvcs.TabIndex = 3;
            this.lineMaDvcs.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lineMakho);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.lineMaVitri);
            this.groupBox1.Controls.Add(this.lineMaDvcs);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(2, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 177);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineMakho
            // 
            this.lineMakho.AccessibleDescription = "FILTERL00006";
            this.lineMakho.AccessibleName2 = "MA_KHO";
            this.lineMakho.FieldCaption = "Mã kho";
            this.lineMakho.FieldName = "MA_KHO";
            this.lineMakho.Location = new System.Drawing.Point(6, 39);
            this.lineMakho.Name = "lineMakho";
            this.lineMakho.Operator = "=";
            this.lineMakho.Size = new System.Drawing.Size(282, 22);
            this.lineMakho.TabIndex = 2;
            this.lineMakho.Vvar = "MA_KHO";
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
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00020";
            this.filterLineVvarTextBox1.AccessibleName2 = "MA_VT";
            this.filterLineVvarTextBox1.FieldCaption = "Mã vật tư";
            this.filterLineVvarTextBox1.FieldName = "MA_VT";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(6, 123);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Operator = "=";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox1.TabIndex = 3;
            this.filterLineVvarTextBox1.Vvar = "MA_VT";
            // 
            // lineMaVitri
            // 
            this.lineMaVitri.AccessibleDescription = "FILTERL00160";
            this.lineMaVitri.AccessibleName2 = "MA_VITRI";
            this.lineMaVitri.FieldCaption = "Mã vị trí";
            this.lineMaVitri.FieldName = "MA_VITRI";
            this.lineMaVitri.Location = new System.Drawing.Point(6, 95);
            this.lineMaVitri.Name = "lineMaVitri";
            this.lineMaVitri.Operator = "=";
            this.lineMaVitri.Size = new System.Drawing.Size(282, 22);
            this.lineMaVitri.TabIndex = 3;
            this.lineMaVitri.Vvar = "MA_VITRI";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00045";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cuối ngày";
            // 
            // dateCuoiNgay
            // 
            this.dateCuoiNgay.CustomFormat = "dd/MM/yyyy";
            this.dateCuoiNgay.Enabled = false;
            this.dateCuoiNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateCuoiNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCuoiNgay.HoverColor = System.Drawing.Color.Yellow;
            this.dateCuoiNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateCuoiNgay.LeaveColor = System.Drawing.Color.White;
            this.dateCuoiNgay.Location = new System.Drawing.Point(115, 11);
            this.dateCuoiNgay.Name = "dateCuoiNgay";
            this.dateCuoiNgay.Size = new System.Drawing.Size(100, 20);
            this.dateCuoiNgay.TabIndex = 0;
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00180";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 43);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(74, 13);
            this.v6Label9.TabIndex = 17;
            this.v6Label9.Text = "Vật tư tồn kho";
            // 
            // txtVtTonKho
            // 
            this.txtVtTonKho.AccessibleName = "VT_TONKHO";
            this.txtVtTonKho.BackColor = System.Drawing.Color.White;
            this.txtVtTonKho.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtVtTonKho.Enabled = false;
            this.txtVtTonKho.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtVtTonKho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtVtTonKho.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtVtTonKho.HoverColor = System.Drawing.Color.Yellow;
            this.txtVtTonKho.LeaveColor = System.Drawing.Color.White;
            this.txtVtTonKho.LimitCharacters = "*0123456";
            this.txtVtTonKho.Location = new System.Drawing.Point(115, 38);
            this.txtVtTonKho.Margin = new System.Windows.Forms.Padding(4);
            this.txtVtTonKho.MaxLength = 1;
            this.txtVtTonKho.Name = "txtVtTonKho";
            this.txtVtTonKho.Size = new System.Drawing.Size(20, 20);
            this.txtVtTonKho.TabIndex = 18;
            this.txtVtTonKho.Text = "*";
            // 
            // txtKieuIn
            // 
            this.txtKieuIn.AccessibleName = "KIEU_IN";
            this.txtKieuIn.BackColor = System.Drawing.Color.White;
            this.txtKieuIn.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKieuIn.Enabled = false;
            this.txtKieuIn.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKieuIn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKieuIn.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKieuIn.HoverColor = System.Drawing.Color.Yellow;
            this.txtKieuIn.LeaveColor = System.Drawing.Color.White;
            this.txtKieuIn.LimitCharacters = "*0123456";
            this.txtKieuIn.Location = new System.Drawing.Point(115, 65);
            this.txtKieuIn.Margin = new System.Windows.Forms.Padding(4);
            this.txtKieuIn.MaxLength = 1;
            this.txtKieuIn.Name = "txtKieuIn";
            this.txtKieuIn.Size = new System.Drawing.Size(20, 20);
            this.txtKieuIn.TabIndex = 18;
            this.txtKieuIn.Text = "*";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00176";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(5, 69);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(39, 13);
            this.v6Label1.TabIndex = 17;
            this.v6Label1.Text = "Kiểu in";
            // 
            // AINVITRI02_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtKieuIn);
            this.Controls.Add(this.txtVtTonKho);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateCuoiNgay);
            this.Controls.Add(this.groupBox1);
            this.Name = "AINVITRI02_filter";
            this.Size = new System.Drawing.Size(295, 273);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox lineMaDvcs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox lineMakho;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePick dateCuoiNgay;
        private V6ReportControls.FilterLineVvarTextBox lineMaVitri;
        private V6Controls.V6Label v6Label9;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6Controls.V6ColorTextBox txtVtTonKho;
        private V6Controls.V6ColorTextBox txtKieuIn;
        private V6Controls.V6Label v6Label1;
    }
}
