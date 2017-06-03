namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ATOBCGIAMBP
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
            this.v6Label1 = new V6Controls.V6Label();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.TxtMa_bp = new V6Controls.V6VvarTextBox();
            this.txtThang2 = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLoai_cc = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMa_tg_cc = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox16 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox15 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox9 = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtSO_THE_TS = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00195";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(4, 78);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(88, 13);
            this.v6Label1.TabIndex = 17;
            this.v6Label1.Text = "Bộ phận sử dụng";
            // 
            // txtNam
            // 
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(120, 9);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 0;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // TxtMa_bp
            // 
            this.TxtMa_bp.AccessibleName = "MA_BP";
            this.TxtMa_bp.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_bp.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_bp.Location = new System.Drawing.Point(120, 75);
            this.TxtMa_bp.Name = "TxtMa_bp";
            this.TxtMa_bp.Size = new System.Drawing.Size(100, 20);
            this.TxtMa_bp.TabIndex = 3;
            this.TxtMa_bp.VVar = "MA_BPTS";
            // 
            // txtThang2
            // 
            this.txtThang2.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang2.DecimalPlaces = 0;
            this.txtThang2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang2.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang2.LeaveColor = System.Drawing.Color.White;
            this.txtThang2.Location = new System.Drawing.Point(120, 53);
            this.txtThang2.MaxLength = 2;
            this.txtThang2.MaxNumLength = 2;
            this.txtThang2.Name = "txtThang2";
            this.txtThang2.Size = new System.Drawing.Size(100, 20);
            this.txtThang2.TabIndex = 2;
            this.txtThang2.Text = "0";
            this.txtThang2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang2.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // txtThang1
            // 
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(120, 31);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(100, 20);
            this.txtThang1.TabIndex = 1;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 12);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Năm";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00114";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến kỳ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00110";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ kỳ";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtLoai_cc);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.txtMa_tg_cc);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox16);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox15);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox9);
            this.groupBox1.Controls.Add(this.TxtSO_THE_TS);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 262);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtLoai_cc
            // 
            this.txtLoai_cc.AccessibleDescription = "FILTERL00193";
            this.txtLoai_cc.FieldCaption = "Loại CC";
            this.txtLoai_cc.FieldName = "LOAI_CC";
            this.txtLoai_cc.Location = new System.Drawing.Point(8, 86);
            this.txtLoai_cc.Name = "txtLoai_cc";
            this.txtLoai_cc.Size = new System.Drawing.Size(282, 22);
            this.txtLoai_cc.TabIndex = 10;
            this.txtLoai_cc.Vvar = "LOAI_CC";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(8, 40);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // txtMa_tg_cc
            // 
            this.txtMa_tg_cc.AccessibleDescription = "FILTERL00115";
            this.txtMa_tg_cc.FieldCaption = "Mã giảm";
            this.txtMa_tg_cc.FieldName = "MA_TG_CC";
            this.txtMa_tg_cc.Location = new System.Drawing.Point(8, 63);
            this.txtMa_tg_cc.Name = "txtMa_tg_cc";
            this.txtMa_tg_cc.Size = new System.Drawing.Size(282, 22);
            this.txtMa_tg_cc.TabIndex = 3;
            this.txtMa_tg_cc.Vvar = "MA_TG_CC";
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00200";
            this.filterLineVvarTextBox1.FieldCaption = "Nhóm CC";
            this.filterLineVvarTextBox1.FieldName = "LOAI_CC";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(8, 109);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox1.TabIndex = 5;
            this.filterLineVvarTextBox1.Vvar = "LOAI_CC";
            // 
            // filterLineVvarTextBox16
            // 
            this.filterLineVvarTextBox16.AccessibleDescription = "FILTERL00201";
            this.filterLineVvarTextBox16.FieldCaption = "Nhóm CC 1";
            this.filterLineVvarTextBox16.FieldName = "NH_CC1";
            this.filterLineVvarTextBox16.Location = new System.Drawing.Point(8, 155);
            this.filterLineVvarTextBox16.Name = "filterLineVvarTextBox16";
            this.filterLineVvarTextBox16.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox16.TabIndex = 7;
            this.filterLineVvarTextBox16.Vvar = "NH_CC";
            // 
            // filterLineVvarTextBox15
            // 
            this.filterLineVvarTextBox15.AccessibleDescription = "FILTERL00202";
            this.filterLineVvarTextBox15.FieldCaption = "Nhóm CC 2";
            this.filterLineVvarTextBox15.FieldName = "NH_CC2";
            this.filterLineVvarTextBox15.Location = new System.Drawing.Point(8, 178);
            this.filterLineVvarTextBox15.Name = "filterLineVvarTextBox15";
            this.filterLineVvarTextBox15.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox15.TabIndex = 8;
            this.filterLineVvarTextBox15.Vvar = "NH_CC";
            // 
            // filterLineVvarTextBox9
            // 
            this.filterLineVvarTextBox9.AccessibleDescription = "FILTERL00203";
            this.filterLineVvarTextBox9.FieldCaption = "Nhóm CC 3";
            this.filterLineVvarTextBox9.FieldName = "NH_CC3";
            this.filterLineVvarTextBox9.Location = new System.Drawing.Point(8, 201);
            this.filterLineVvarTextBox9.Name = "filterLineVvarTextBox9";
            this.filterLineVvarTextBox9.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox9.TabIndex = 9;
            this.filterLineVvarTextBox9.Vvar = "NH_CC";
            // 
            // TxtSO_THE_TS
            // 
            this.TxtSO_THE_TS.AccessibleDescription = "FILTERL00194";
            this.TxtSO_THE_TS.FieldCaption = "Mã CC";
            this.TxtSO_THE_TS.FieldName = "SO_THE_CC";
            this.TxtSO_THE_TS.Location = new System.Drawing.Point(8, 132);
            this.TxtSO_THE_TS.Name = "TxtSO_THE_TS";
            this.TxtSO_THE_TS.Size = new System.Drawing.Size(282, 22);
            this.TxtSO_THE_TS.TabIndex = 6;
            this.TxtSO_THE_TS.Vvar = "SO_THE_CC";
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
            // ATOBCGIAMBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.TxtMa_bp);
            this.Controls.Add(this.txtThang2);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ATOBCGIAMBP";
            this.Size = new System.Drawing.Size(314, 363);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6NumberTextBox txtThang2;
        private V6Controls.V6NumberTextBox txtNam;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox16;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox15;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox9;
        private V6ReportControls.FilterLineVvarTextBox TxtSO_THE_TS;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox TxtMa_bp;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMa_tg_cc;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox txtLoai_cc;
    }
}
