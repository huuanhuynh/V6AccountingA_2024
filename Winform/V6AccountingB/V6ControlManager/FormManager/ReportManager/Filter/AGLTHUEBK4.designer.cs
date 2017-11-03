namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLTHUEBK4
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
            this.chkNhomCt = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterLineVvarTextBox11 = new V6ReportControls.FilterLineVvarTextBox();
            this.txtTk_thue_no = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtma_kh = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtmau_bc = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMa_tc = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkNhomCt
            // 
            this.chkNhomCt.AccessibleDescription = "FILTERC00012";
            this.chkNhomCt.AccessibleName = "NHOM_CT";
            this.chkNhomCt.AutoSize = true;
            this.chkNhomCt.Location = new System.Drawing.Point(131, 57);
            this.chkNhomCt.Name = "chkNhomCt";
            this.chkNhomCt.Size = new System.Drawing.Size(123, 17);
            this.chkNhomCt.TabIndex = 2;
            this.chkNhomCt.Text = "Nhóm theo chứng từ";
            this.chkNhomCt.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
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
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(131, 29);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(131, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox11);
            this.groupBox1.Controls.Add(this.txtTk_thue_no);
            this.groupBox1.Controls.Add(this.Txtma_kh);
            this.groupBox1.Controls.Add(this.Txtmau_bc);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(3, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 162);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // filterLineVvarTextBox11
            // 
            this.filterLineVvarTextBox11.AccessibleDescription = "FILTERL00004";
            this.filterLineVvarTextBox11.AccessibleName2 = "MA_CT";
            this.filterLineVvarTextBox11.FieldCaption = "Mã chứng từ";
            this.filterLineVvarTextBox11.FieldName = "MA_CT";
            this.filterLineVvarTextBox11.Location = new System.Drawing.Point(6, 108);
            this.filterLineVvarTextBox11.Name = "filterLineVvarTextBox11";
            this.filterLineVvarTextBox11.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox11.TabIndex = 5;
            this.filterLineVvarTextBox11.Vvar = "MA_CT";
            // 
            // txtTk_thue_no
            // 
            this.txtTk_thue_no.AccessibleDescription = "FILTERL00142";
            this.txtTk_thue_no.AccessibleName2 = "TK_THUE_NO";
            this.txtTk_thue_no.FieldCaption = "Tài khoản thuế";
            this.txtTk_thue_no.FieldName = "TK_THUE_NO";
            this.txtTk_thue_no.Location = new System.Drawing.Point(6, 131);
            this.txtTk_thue_no.Name = "txtTk_thue_no";
            this.txtTk_thue_no.Size = new System.Drawing.Size(282, 22);
            this.txtTk_thue_no.TabIndex = 6;
            this.txtTk_thue_no.Vvar = "TK";
            // 
            // Txtma_kh
            // 
            this.Txtma_kh.AccessibleDescription = "FILTERL00007";
            this.Txtma_kh.AccessibleName2 = "MA_KH";
            this.Txtma_kh.FieldCaption = "Mã khách";
            this.Txtma_kh.FieldName = "MA_KH";
            this.Txtma_kh.Location = new System.Drawing.Point(6, 85);
            this.Txtma_kh.Name = "Txtma_kh";
            this.Txtma_kh.Size = new System.Drawing.Size(282, 22);
            this.Txtma_kh.TabIndex = 4;
            this.Txtma_kh.Vvar = "MA_KH";
            // 
            // Txtmau_bc
            // 
            this.Txtmau_bc.AccessibleDescription = "FILTERL00141";
            this.Txtmau_bc.AccessibleName2 = "MAU_BC";
            this.Txtmau_bc.FieldCaption = "Mẫu BC";
            this.Txtmau_bc.FieldName = "MAU_BC";
            this.Txtmau_bc.Location = new System.Drawing.Point(6, 62);
            this.Txtmau_bc.Name = "Txtmau_bc";
            this.Txtmau_bc.Size = new System.Drawing.Size(282, 22);
            this.Txtmau_bc.TabIndex = 3;
            this.Txtmau_bc.Vvar = "";
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
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 39);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // txtMa_tc
            // 
            this.txtMa_tc.AccessibleName = "MA_TC";
            this.txtMa_tc.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa_tc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_tc.CheckNotEmpty = true;
            this.txtMa_tc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_tc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_tc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_tc.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_tc.LeaveColor = System.Drawing.Color.White;
            this.txtMa_tc.Location = new System.Drawing.Point(131, 80);
            this.txtMa_tc.Name = "txtMa_tc";
            this.txtMa_tc.Size = new System.Drawing.Size(118, 20);
            this.txtMa_tc.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00140";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mã tc";
            // 
            // AGLTHUEBK4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMa_tc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkNhomCt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLTHUEBK4";
            this.Size = new System.Drawing.Size(295, 288);
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
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkNhomCt;
        private V6ReportControls.FilterLineVvarTextBox Txtma_kh;
        private V6ReportControls.FilterLineVvarTextBox Txtmau_bc;
        private V6Controls.V6VvarTextBox txtMa_tc;
        private System.Windows.Forms.Label label3;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox11;
        private V6ReportControls.FilterLineVvarTextBox txtTk_thue_no;
    }
}
