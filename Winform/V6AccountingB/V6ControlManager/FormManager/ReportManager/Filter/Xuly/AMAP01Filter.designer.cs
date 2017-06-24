namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    partial class AMAP01Filter
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
            this.lineMakho = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.lineMaVitri = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh6 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh3 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtStatus = new V6ReportControls.FilterLineVvarTextBox();
            this.chknv_yn = new V6Controls.V6CheckBox();
            this.chkcc_yn = new V6Controls.V6CheckBox();
            this.chkkh_yn = new V6Controls.V6CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chknv_yn);
            this.groupBox1.Controls.Add(this.chkcc_yn);
            this.groupBox1.Controls.Add(this.chkkh_yn);
            this.groupBox1.Controls.Add(this.TxtStatus);
            this.groupBox1.Controls.Add(this.Txtnh_kh6);
            this.groupBox1.Controls.Add(this.Txtnh_kh5);
            this.groupBox1.Controls.Add(this.Txtnh_kh4);
            this.groupBox1.Controls.Add(this.Txtnh_kh1);
            this.groupBox1.Controls.Add(this.Txtnh_kh2);
            this.groupBox1.Controls.Add(this.Txtnh_kh3);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Controls.Add(this.lineMakho);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.lineMaVitri);
            this.groupBox1.Location = new System.Drawing.Point(2, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 407);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineMakho
            // 
            this.lineMakho.AccessibleDescription = "FILTERL00006";
            this.lineMakho.Enabled = false;
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
            // lineMaVitri
            // 
            this.lineMaVitri.AccessibleDescription = "FILTERL00160";
            this.lineMaVitri.Enabled = false;
            this.lineMaVitri.FieldCaption = "Mã vị trí";
            this.lineMaVitri.FieldName = "MA_VITRI";
            this.lineMaVitri.Location = new System.Drawing.Point(6, 65);
            this.lineMaVitri.Name = "lineMaVitri";
            this.lineMaVitri.Operator = "=";
            this.lineMaVitri.Size = new System.Drawing.Size(282, 22);
            this.lineMaVitri.TabIndex = 3;
            this.lineMaVitri.Vvar = "MA_VITRI";
            // 
            // Txtnh_kh6
            // 
            this.Txtnh_kh6.AccessibleDescription = "FILTERL00016";
            this.Txtnh_kh6.FieldCaption = "Nhóm khách hàng 6";
            this.Txtnh_kh6.FieldName = "NH_KH6";
            this.Txtnh_kh6.Location = new System.Drawing.Point(6, 233);
            this.Txtnh_kh6.Name = "Txtnh_kh6";
            this.Txtnh_kh6.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh6.TabIndex = 10;
            this.Txtnh_kh6.Vvar = "NH_KH";
            // 
            // Txtnh_kh5
            // 
            this.Txtnh_kh5.AccessibleDescription = "FILTERL00015";
            this.Txtnh_kh5.FieldCaption = "Nhóm khách hàng 5";
            this.Txtnh_kh5.FieldName = "NH_KH5";
            this.Txtnh_kh5.Location = new System.Drawing.Point(6, 211);
            this.Txtnh_kh5.Name = "Txtnh_kh5";
            this.Txtnh_kh5.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh5.TabIndex = 9;
            this.Txtnh_kh5.Vvar = "NH_KH";
            // 
            // Txtnh_kh4
            // 
            this.Txtnh_kh4.AccessibleDescription = "FILTERL00014";
            this.Txtnh_kh4.FieldCaption = "Nhóm khách hàng 4";
            this.Txtnh_kh4.FieldName = "NH_KH4";
            this.Txtnh_kh4.Location = new System.Drawing.Point(6, 189);
            this.Txtnh_kh4.Name = "Txtnh_kh4";
            this.Txtnh_kh4.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh4.TabIndex = 8;
            this.Txtnh_kh4.Vvar = "NH_KH";
            // 
            // Txtnh_kh1
            // 
            this.Txtnh_kh1.AccessibleDescription = "FILTERL00011";
            this.Txtnh_kh1.FieldCaption = "Nhóm khách hàng 1";
            this.Txtnh_kh1.FieldName = "NH_KH1";
            this.Txtnh_kh1.Location = new System.Drawing.Point(6, 123);
            this.Txtnh_kh1.Name = "Txtnh_kh1";
            this.Txtnh_kh1.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh1.TabIndex = 5;
            this.Txtnh_kh1.Vvar = "NH_KH";
            // 
            // Txtnh_kh2
            // 
            this.Txtnh_kh2.AccessibleDescription = "FILTERL00012";
            this.Txtnh_kh2.FieldCaption = "Nhóm khách hàng 2";
            this.Txtnh_kh2.FieldName = "NH_KH2";
            this.Txtnh_kh2.Location = new System.Drawing.Point(6, 145);
            this.Txtnh_kh2.Name = "Txtnh_kh2";
            this.Txtnh_kh2.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh2.TabIndex = 6;
            this.Txtnh_kh2.Vvar = "NH_KH";
            // 
            // Txtnh_kh3
            // 
            this.Txtnh_kh3.AccessibleDescription = "FILTERL00013";
            this.Txtnh_kh3.FieldCaption = "Nhóm khách hàng 3";
            this.Txtnh_kh3.FieldName = "NH_KH3";
            this.Txtnh_kh3.Location = new System.Drawing.Point(6, 167);
            this.Txtnh_kh3.Name = "Txtnh_kh3";
            this.Txtnh_kh3.Size = new System.Drawing.Size(281, 22);
            this.Txtnh_kh3.TabIndex = 7;
            this.Txtnh_kh3.Vvar = "NH_KH";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.FieldCaption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(6, 100);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(281, 22);
            this.filterLineVvarTextBox3.TabIndex = 4;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // TxtStatus
            // 
            this.TxtStatus.AccessibleDescription = "FILTERL00016";
            this.TxtStatus.FieldCaption = "Trạng thái";
            this.TxtStatus.FieldName = "STATUS";
            this.TxtStatus.Location = new System.Drawing.Point(6, 255);
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.Size = new System.Drawing.Size(281, 22);
            this.TxtStatus.TabIndex = 11;
            this.TxtStatus.Vvar = "";
            // 
            // chknv_yn
            // 
            this.chknv_yn.AccessibleDescription = "FILTERC00013";
            this.chknv_yn.AutoSize = true;
            this.chknv_yn.Location = new System.Drawing.Point(207, 283);
            this.chknv_yn.Name = "chknv_yn";
            this.chknv_yn.Size = new System.Drawing.Size(75, 17);
            this.chknv_yn.TabIndex = 28;
            this.chknv_yn.Text = "Nhân viên";
            this.chknv_yn.UseVisualStyleBackColor = true;
            // 
            // chkcc_yn
            // 
            this.chkcc_yn.AccessibleDescription = "FILTERC00013";
            this.chkcc_yn.AutoSize = true;
            this.chkcc_yn.Location = new System.Drawing.Point(122, 283);
            this.chkcc_yn.Name = "chkcc_yn";
            this.chkcc_yn.Size = new System.Drawing.Size(63, 17);
            this.chkcc_yn.TabIndex = 27;
            this.chkcc_yn.Text = "Nhà CC";
            this.chkcc_yn.UseVisualStyleBackColor = true;
            // 
            // chkkh_yn
            // 
            this.chkkh_yn.AccessibleDescription = "FILTERC00013";
            this.chkkh_yn.AutoSize = true;
            this.chkkh_yn.Location = new System.Drawing.Point(12, 283);
            this.chkkh_yn.Name = "chkkh_yn";
            this.chkkh_yn.Size = new System.Drawing.Size(87, 17);
            this.chkkh_yn.TabIndex = 26;
            this.chkkh_yn.Text = "Khách hàng ";
            this.chkkh_yn.UseVisualStyleBackColor = true;
            // 
            // AMAP01Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AMAP01Filter";
            this.Size = new System.Drawing.Size(295, 427);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox lineMakho;
        private V6ReportControls.FilterLineVvarTextBox lineMaVitri;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh6;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh3;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private V6ReportControls.FilterLineVvarTextBox TxtStatus;
        private V6Controls.V6CheckBox chknv_yn;
        private V6Controls.V6CheckBox chkcc_yn;
        private V6Controls.V6CheckBox chkkh_yn;
    }
}
