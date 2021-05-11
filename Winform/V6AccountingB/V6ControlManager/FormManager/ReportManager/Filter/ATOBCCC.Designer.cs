namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ATOBCCC
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
            this.v6Label9 = new V6Controls.V6Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineNhomCC = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.txtLoai_cc0 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineLoai_CC0 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox16 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox15 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox9 = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtSO_THE_CC = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.txtdau_cuoi = new V6Controls.V6NumberTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.TxtMa_bp = new V6Controls.V6VvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 9);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 0;
            this.v6Label9.Text = "Năm";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00119";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "1-Đầu kỳ/ 2- Cuối kỳ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00120";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kỳ";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lineNhomCC);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.txtLoai_cc0);
            this.groupBox1.Controls.Add(this.lineLoai_CC0);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox16);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox15);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox9);
            this.groupBox1.Controls.Add(this.TxtSO_THE_CC);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 271);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineNhomCC
            // 
            this.lineNhomCC.AccessibleDescription = "FILTERL00200";
            this.lineNhomCC.AccessibleName2 = "LOAI_CC";
            this.lineNhomCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineNhomCC.Caption = "Nhóm CC";
            this.lineNhomCC.FieldName = "LOAI_CC";
            this.lineNhomCC.Location = new System.Drawing.Point(9, 109);
            this.lineNhomCC.Name = "lineNhomCC";
            this.lineNhomCC.Size = new System.Drawing.Size(282, 22);
            this.lineNhomCC.TabIndex = 5;
            this.lineNhomCC.Vvar = "MA_PLCC";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(9, 43);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // txtLoai_cc0
            // 
            this.txtLoai_cc0.AccessibleDescription = "FILTERL00192";
            this.txtLoai_cc0.AccessibleName2 = "LOAI_CC0";
            this.txtLoai_cc0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtLoai_cc0.Caption = "Phân loại CC";
            this.txtLoai_cc0.FieldName = "LOAI_CC0";
            this.txtLoai_cc0.Location = new System.Drawing.Point(9, 65);
            this.txtLoai_cc0.Name = "txtLoai_cc0";
            this.txtLoai_cc0.Size = new System.Drawing.Size(282, 22);
            this.txtLoai_cc0.TabIndex = 3;
            this.txtLoai_cc0.Vvar = "LOAI_CC0";
            // 
            // lineLoai_CC0
            // 
            this.lineLoai_CC0.AccessibleDescription = "FILTERL00193";
            this.lineLoai_CC0.AccessibleName2 = "LOAI_CC0";
            this.lineLoai_CC0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineLoai_CC0.Caption = "Loại CC";
            this.lineLoai_CC0.FieldName = "LOAI_CC0";
            this.lineLoai_CC0.Location = new System.Drawing.Point(9, 87);
            this.lineLoai_CC0.Name = "lineLoai_CC0";
            this.lineLoai_CC0.Size = new System.Drawing.Size(282, 22);
            this.lineLoai_CC0.TabIndex = 4;
            this.lineLoai_CC0.Vvar = "LOAI_CC0";
            // 
            // filterLineVvarTextBox16
            // 
            this.filterLineVvarTextBox16.AccessibleDescription = "FILTERL00201";
            this.filterLineVvarTextBox16.AccessibleName2 = "NH_CC1";
            this.filterLineVvarTextBox16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox16.Caption = "Nhóm CC 1";
            this.filterLineVvarTextBox16.FieldName = "NH_CC1";
            this.filterLineVvarTextBox16.Location = new System.Drawing.Point(9, 153);
            this.filterLineVvarTextBox16.Name = "filterLineVvarTextBox16";
            this.filterLineVvarTextBox16.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox16.TabIndex = 7;
            this.filterLineVvarTextBox16.Vvar = "NH_CC";
            // 
            // filterLineVvarTextBox15
            // 
            this.filterLineVvarTextBox15.AccessibleDescription = "FILTERL00202";
            this.filterLineVvarTextBox15.AccessibleName2 = "NH_CC2";
            this.filterLineVvarTextBox15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox15.Caption = "Nhóm CC 2";
            this.filterLineVvarTextBox15.FieldName = "NH_CC2";
            this.filterLineVvarTextBox15.Location = new System.Drawing.Point(9, 175);
            this.filterLineVvarTextBox15.Name = "filterLineVvarTextBox15";
            this.filterLineVvarTextBox15.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox15.TabIndex = 8;
            this.filterLineVvarTextBox15.Vvar = "NH_CC";
            // 
            // filterLineVvarTextBox9
            // 
            this.filterLineVvarTextBox9.AccessibleDescription = "FILTERL00203";
            this.filterLineVvarTextBox9.AccessibleName2 = "NH_CC3";
            this.filterLineVvarTextBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox9.Caption = "Nhóm CC 3";
            this.filterLineVvarTextBox9.FieldName = "NH_CC3";
            this.filterLineVvarTextBox9.Location = new System.Drawing.Point(9, 197);
            this.filterLineVvarTextBox9.Name = "filterLineVvarTextBox9";
            this.filterLineVvarTextBox9.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox9.TabIndex = 9;
            this.filterLineVvarTextBox9.Vvar = "NH_CC";
            // 
            // TxtSO_THE_CC
            // 
            this.TxtSO_THE_CC.AccessibleDescription = "FILTERL00194";
            this.TxtSO_THE_CC.AccessibleName2 = "SO_THE_CC";
            this.TxtSO_THE_CC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.TxtSO_THE_CC.Caption = "Mã CC";
            this.TxtSO_THE_CC.FieldName = "SO_THE_CC";
            this.TxtSO_THE_CC.Location = new System.Drawing.Point(9, 131);
            this.TxtSO_THE_CC.Name = "TxtSO_THE_CC";
            this.TxtSO_THE_CC.Size = new System.Drawing.Size(282, 22);
            this.TxtSO_THE_CC.TabIndex = 6;
            this.TxtSO_THE_CC.Vvar = "SO_THE_CC";
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
            this.txtThang1.Location = new System.Drawing.Point(120, 28);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(100, 20);
            this.txtThang1.TabIndex = 3;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // txtdau_cuoi
            // 
            this.txtdau_cuoi.AccessibleName = "DAU_CUOI";
            this.txtdau_cuoi.BackColor = System.Drawing.SystemColors.Window;
            this.txtdau_cuoi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdau_cuoi.DecimalPlaces = 0;
            this.txtdau_cuoi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdau_cuoi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdau_cuoi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdau_cuoi.HoverColor = System.Drawing.Color.Yellow;
            this.txtdau_cuoi.LeaveColor = System.Drawing.Color.White;
            this.txtdau_cuoi.LimitCharacters = "1,2";
            this.txtdau_cuoi.Location = new System.Drawing.Point(120, 50);
            this.txtdau_cuoi.MaxLength = 1;
            this.txtdau_cuoi.MaxNumLength = 1;
            this.txtdau_cuoi.Name = "txtdau_cuoi";
            this.txtdau_cuoi.Size = new System.Drawing.Size(100, 20);
            this.txtdau_cuoi.TabIndex = 5;
            this.txtdau_cuoi.Text = "0";
            this.txtdau_cuoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtdau_cuoi.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.txtNam.Location = new System.Drawing.Point(120, 6);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 1;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00191";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(8, 78);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(79, 13);
            this.v6Label1.TabIndex = 6;
            this.v6Label1.Text = "Bộ phận CCDC";
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
            this.TxtMa_bp.Location = new System.Drawing.Point(120, 72);
            this.TxtMa_bp.Name = "TxtMa_bp";
            this.TxtMa_bp.Size = new System.Drawing.Size(100, 20);
            this.TxtMa_bp.TabIndex = 7;
            this.TxtMa_bp.VVar = "MA_BPCC";
            // 
            // ATOBCCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.TxtMa_bp);
            this.Controls.Add(this.txtdau_cuoi);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ATOBCCC";
            this.Size = new System.Drawing.Size(309, 385);
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
        private V6Controls.V6NumberTextBox txtdau_cuoi;
        private V6Controls.V6NumberTextBox txtNam;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox16;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox15;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox9;
        private V6ReportControls.FilterLineVvarTextBox TxtSO_THE_CC;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox TxtMa_bp;
        private V6ReportControls.FilterLineVvarTextBox lineLoai_CC0;
        private V6ReportControls.FilterLineVvarTextBox txtLoai_cc0;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox lineNhomCC;
    }
}
