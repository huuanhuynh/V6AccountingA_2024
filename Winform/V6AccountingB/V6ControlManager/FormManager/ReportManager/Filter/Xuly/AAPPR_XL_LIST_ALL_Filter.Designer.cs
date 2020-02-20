namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AAPPR_XL_LIST_ALL_Filter
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
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.TxtXtag = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.cboMa_xuly = new V6Controls.V6ComboBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.lblMaDanhMuc = new V6Controls.V6Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkView_all = new V6Controls.V6CheckBox();
            this.lineMa_xuly = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtMaDMProc = new V6Controls.V6LookupProc();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label3
            // 
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(132, 83);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(83, 13);
            this.v6Label3.TabIndex = 9;
            this.v6Label3.Text = "2-Duyệt kế toán";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00023";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(132, 69);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(137, 13);
            this.v6Label4.TabIndex = 8;
            this.v6Label4.Text = "0-Chưa duyệt, 1- Duyệt kho";
            // 
            // TxtXtag
            // 
            this.TxtXtag.AccessibleName = "CHK_LOC_CT";
            this.TxtXtag.BackColor = System.Drawing.SystemColors.Window;
            this.TxtXtag.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtXtag.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtXtag.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtXtag.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtXtag.HoverColor = System.Drawing.Color.Yellow;
            this.TxtXtag.LeaveColor = System.Drawing.Color.White;
            this.TxtXtag.LimitCharacters = "012";
            this.TxtXtag.Location = new System.Drawing.Point(114, 68);
            this.TxtXtag.MaxLength = 1;
            this.TxtXtag.Name = "TxtXtag";
            this.TxtXtag.Size = new System.Drawing.Size(18, 20);
            this.TxtXtag.TabIndex = 3;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERO00001";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(9, 72);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(70, 13);
            this.v6Label2.TabIndex = 6;
            this.v6Label2.Text = "Lọc chứng từ";
            // 
            // cboMa_xuly
            // 
            this.cboMa_xuly.AccessibleName = "MA_XULY1";
            this.cboMa_xuly.BackColor = System.Drawing.SystemColors.Window;
            this.cboMa_xuly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMa_xuly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMa_xuly.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMa_xuly.FormattingEnabled = true;
            this.cboMa_xuly.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboMa_xuly.Location = new System.Drawing.Point(112, 161);
            this.cboMa_xuly.Name = "cboMa_xuly";
            this.cboMa_xuly.Size = new System.Drawing.Size(170, 21);
            this.cboMa_xuly.TabIndex = 7;
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00010";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6Label1.Location = new System.Drawing.Point(9, 163);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(89, 13);
            this.v6Label1.TabIndex = 10;
            this.v6Label1.Text = "Xử lý chứng từ";
            // 
            // lblMaDanhMuc
            // 
            this.lblMaDanhMuc.AutoSize = true;
            this.lblMaDanhMuc.Location = new System.Drawing.Point(8, 48);
            this.lblMaDanhMuc.Name = "lblMaDanhMuc";
            this.lblMaDanhMuc.Size = new System.Drawing.Size(72, 13);
            this.lblMaDanhMuc.TabIndex = 4;
            this.lblMaDanhMuc.Text = "Mã danh mục";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkView_all);
            this.groupBox1.Controls.Add(this.lineMa_xuly);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 121);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // chkView_all
            // 
            this.chkView_all.AccessibleDescription = "";
            this.chkView_all.AccessibleName = "";
            this.chkView_all.AutoSize = true;
            this.chkView_all.Location = new System.Drawing.Point(182, 70);
            this.chkView_all.Name = "chkView_all";
            this.chkView_all.Size = new System.Drawing.Size(57, 17);
            this.chkView_all.TabIndex = 21;
            this.chkView_all.Text = "Tất cả";
            this.chkView_all.UseVisualStyleBackColor = true;
            this.chkView_all.CheckedChanged += new System.EventHandler(this.chkView_all_CheckedChanged);
            // 
            // lineMa_xuly
            // 
            this.lineMa_xuly.AccessibleDescription = "FILTERL00009";
            this.lineMa_xuly.AccessibleName2 = "MA_XULY";
            this.lineMa_xuly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMa_xuly.Caption = "Mã xử lý";
            this.lineMa_xuly.FieldName = "MA_XULY";
            this.lineMa_xuly.Location = new System.Drawing.Point(4, 42);
            this.lineMa_xuly.Name = "lineMa_xuly";
            this.lineMa_xuly.Size = new System.Drawing.Size(294, 22);
            this.lineMa_xuly.TabIndex = 15;
            this.lineMa_xuly.Vvar = "MA_XULY";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 15);
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
            this.radAnd.Location = new System.Drawing.Point(6, 15);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(130, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Tất cả điều kiện (and)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // txtMaDMProc
            // 
            this.txtMaDMProc.AccessibleName = "MA_DM";
            this.txtMaDMProc.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaDMProc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaDMProc.CheckNotEmpty = true;
            this.txtMaDMProc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaDMProc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaDMProc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaDMProc.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaDMProc.LeaveColor = System.Drawing.Color.White;
            this.txtMaDMProc.Location = new System.Drawing.Point(112, 46);
            this.txtMaDMProc.MA_CT = "MA_DM";
            this.txtMaDMProc.Ma_dm = "VPA_GET_ALDM";
            this.txtMaDMProc.Name = "txtMaDMProc";
            this.txtMaDMProc.ParentData = null;
            this.txtMaDMProc.ShowName = true;
            this.txtMaDMProc.ShowTextField = "MA_DM";
            this.txtMaDMProc.Size = new System.Drawing.Size(100, 20);
            this.txtMaDMProc.TabIndex = 2;
            this.txtMaDMProc.ValueField = "MA_DM";
            this.txtMaDMProc.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMa_ct_V6LostFocus);
            // 
            // AAPPR_XL_LIST_ALL_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.TxtXtag);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.cboMa_xuly);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.lblMaDanhMuc);
            this.Controls.Add(this.txtMaDMProc);
            this.Controls.Add(this.groupBox1);
            this.Name = "AAPPR_XL_LIST_ALL_Filter";
            this.Size = new System.Drawing.Size(299, 623);
            this.Load += new System.EventHandler(this.AAPPR_XL_LIST_ALL_Filter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6Label lblMaDanhMuc;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6ComboBox cboMa_xuly;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6VvarTextBox TxtXtag;
        private V6ReportControls.FilterLineVvarTextBox lineMa_xuly;
        private V6Controls.V6CheckBox chkView_all;
        private V6Controls.V6LookupProc txtMaDMProc;
    }
}
