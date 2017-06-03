namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AFABCTSBP
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox16 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox15 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox9 = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtSO_THE_TS = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.txtdau_cuoi = new V6Controls.V6NumberTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 19);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Năm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "1-Đầu kỳ/ 2- Cuối kỳ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kỳ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox16);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox15);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox9);
            this.groupBox1.Controls.Add(this.TxtSO_THE_TS);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(3, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 188);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.IsSelected = false;
            this.txtMaDvcs.Location = new System.Drawing.Point(9, 43);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 2;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.FieldCaption = "Loại TS";
            this.filterLineVvarTextBox1.FieldName = "LOAI_TS";
            this.filterLineVvarTextBox1.IsSelected = false;
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(9, 66);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox1.TabIndex = 3;
            this.filterLineVvarTextBox1.Vvar = "LOAI_TS";
            // 
            // filterLineVvarTextBox16
            // 
            this.filterLineVvarTextBox16.FieldCaption = "Nhóm TS 1";
            this.filterLineVvarTextBox16.FieldName = "NH_TS1";
            this.filterLineVvarTextBox16.IsSelected = false;
            this.filterLineVvarTextBox16.Location = new System.Drawing.Point(9, 112);
            this.filterLineVvarTextBox16.Name = "filterLineVvarTextBox16";
            this.filterLineVvarTextBox16.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox16.TabIndex = 5;
            this.filterLineVvarTextBox16.Vvar = "NH_TS";
            // 
            // filterLineVvarTextBox15
            // 
            this.filterLineVvarTextBox15.FieldCaption = "Nhóm TS 2";
            this.filterLineVvarTextBox15.FieldName = "NH_TS2";
            this.filterLineVvarTextBox15.IsSelected = false;
            this.filterLineVvarTextBox15.Location = new System.Drawing.Point(9, 135);
            this.filterLineVvarTextBox15.Name = "filterLineVvarTextBox15";
            this.filterLineVvarTextBox15.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox15.TabIndex = 6;
            this.filterLineVvarTextBox15.Vvar = "NH_TS";
            // 
            // filterLineVvarTextBox9
            // 
            this.filterLineVvarTextBox9.FieldCaption = "Nhóm TS 3";
            this.filterLineVvarTextBox9.FieldName = "NH_TS3";
            this.filterLineVvarTextBox9.IsSelected = false;
            this.filterLineVvarTextBox9.Location = new System.Drawing.Point(9, 158);
            this.filterLineVvarTextBox9.Name = "filterLineVvarTextBox9";
            this.filterLineVvarTextBox9.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox9.TabIndex = 7;
            this.filterLineVvarTextBox9.Vvar = "NH_TS";
            // 
            // TxtSO_THE_TS
            // 
            this.TxtSO_THE_TS.FieldCaption = "Mã TS";
            this.TxtSO_THE_TS.FieldName = "SO_THE_TS";
            this.TxtSO_THE_TS.IsSelected = false;
            this.TxtSO_THE_TS.Location = new System.Drawing.Point(9, 89);
            this.TxtSO_THE_TS.Name = "TxtSO_THE_TS";
            this.TxtSO_THE_TS.Size = new System.Drawing.Size(282, 22);
            this.TxtSO_THE_TS.TabIndex = 4;
            this.TxtSO_THE_TS.Vvar = "SO_THE_TS";
            // 
            // radOr
            // 
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
            this.txtThang1.Location = new System.Drawing.Point(117, 37);
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
            // txtdau_cuoi
            // 
            this.txtdau_cuoi.BackColor = System.Drawing.SystemColors.Window;
            this.txtdau_cuoi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdau_cuoi.DecimalPlaces = 0;
            this.txtdau_cuoi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdau_cuoi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdau_cuoi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdau_cuoi.HoverColor = System.Drawing.Color.Yellow;
            this.txtdau_cuoi.LeaveColor = System.Drawing.Color.White;
            this.txtdau_cuoi.LimitCharacters = "1,2";
            this.txtdau_cuoi.Location = new System.Drawing.Point(117, 60);
            this.txtdau_cuoi.MaxLength = 1;
            this.txtdau_cuoi.MaxNumLength = 1;
            this.txtdau_cuoi.Name = "txtdau_cuoi";
            this.txtdau_cuoi.Size = new System.Drawing.Size(100, 20);
            this.txtdau_cuoi.TabIndex = 2;
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
            this.txtNam.Location = new System.Drawing.Point(117, 16);
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
            // AFABCTSBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtdau_cuoi);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AFABCTSBP";
            this.Size = new System.Drawing.Size(307, 277);
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
        private V6ReportControls.FilterLineVvarTextBox TxtSO_THE_TS;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
    }
}
