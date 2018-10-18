namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AINSO1
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtMakho = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtMa_vt = new V6Controls.V6VvarTextBox();
            this.Chk_Tinh_dc = new V6Controls.V6CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 73);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 3;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtMakho);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 114);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // TxtMakho
            // 
            this.TxtMakho.AccessibleDescription = "FILTERL00006";
            this.TxtMakho.AccessibleName2 = "MA_KHO";
            this.TxtMakho.Caption = "Mã kho";
            this.TxtMakho.FieldName = "MA_KHO";
            this.TxtMakho.Location = new System.Drawing.Point(6, 48);
            this.TxtMakho.Name = "TxtMakho";
            this.TxtMakho.Size = new System.Drawing.Size(282, 22);
            this.TxtMakho.TabIndex = 2;
            this.TxtMakho.Vvar = "MA_KHO";
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
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(101, 38);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00003";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đến ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00002";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Từ ngày";
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(101, 10);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00020";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(6, 69);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Mã  vật tư";
            // 
            // TxtMa_vt
            // 
            this.TxtMa_vt.AccessibleName = "MA_VT";
            this.TxtMa_vt.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_vt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_vt.CheckNotEmpty = true;
            this.TxtMa_vt.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_vt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_vt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_vt.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_vt.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_vt.Location = new System.Drawing.Point(102, 66);
            this.TxtMa_vt.Name = "TxtMa_vt";
            this.TxtMa_vt.Size = new System.Drawing.Size(100, 20);
            this.TxtMa_vt.TabIndex = 5;
            this.TxtMa_vt.VVar = "MA_VT";
            // 
            // Chk_Tinh_dc
            // 
            this.Chk_Tinh_dc.AccessibleDescription = "FILTERC00014";
            this.Chk_Tinh_dc.AccessibleName = "CHK_TINH_DC";
            this.Chk_Tinh_dc.AutoSize = true;
            this.Chk_Tinh_dc.Location = new System.Drawing.Point(55, 94);
            this.Chk_Tinh_dc.Name = "Chk_Tinh_dc";
            this.Chk_Tinh_dc.Size = new System.Drawing.Size(149, 17);
            this.Chk_Tinh_dc.TabIndex = 8;
            this.Chk_Tinh_dc.Text = "Tính PS điều chuyển kho";
            this.Chk_Tinh_dc.UseVisualStyleBackColor = true;
            // 
            // AINSO1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Chk_Tinh_dc);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtMa_vt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AINSO1";
            this.Size = new System.Drawing.Size(295, 268);
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
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private V6ReportControls.FilterLineVvarTextBox TxtMakho;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtMa_vt;
        private V6Controls.V6CheckBox Chk_Tinh_dc;
    }
}
