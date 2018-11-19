namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AARSD2
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
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct0 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.TxtMa_kh = new V6Controls.V6VvarTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtTk = new V6Controls.V6VvarTextBox();
            this.filterLineVvarTextBox2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtten_KH = new V6Controls.V6ColorTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 44);
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
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 80);
            this.groupBox1.TabIndex = 5;
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
            // dateNgay_ct0
            // 
            this.dateNgay_ct0.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct0.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct0.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct0.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct0.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct0.Location = new System.Drawing.Point(100, 9);
            this.dateNgay_ct0.Name = "dateNgay_ct0";
            this.dateNgay_ct0.Size = new System.Drawing.Size(130, 20);
            this.dateNgay_ct0.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00045";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuối ngày";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00007";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(5, 63);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(55, 13);
            this.v6Label1.TabIndex = 12;
            this.v6Label1.Text = "Mã khách";
            // 
            // TxtMa_kh
            // 
            this.TxtMa_kh.AccessibleName = "MA_KH";
            this.TxtMa_kh.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_kh.BrotherFields = "TEN_KH";
            this.TxtMa_kh.CheckNotEmpty = true;
            this.TxtMa_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_kh.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_kh.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_kh.Location = new System.Drawing.Point(100, 60);
            this.TxtMa_kh.Name = "TxtMa_kh";
            this.TxtMa_kh.Size = new System.Drawing.Size(132, 20);
            this.TxtMa_kh.TabIndex = 3;
            this.TxtMa_kh.VVar = "MA_KH";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00027";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 39);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 11;
            this.v6Label9.Text = "Tài khoản";
            // 
            // TxtTk
            // 
            this.TxtTk.AccessibleName = "TK";
            this.TxtTk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk.CheckNotEmpty = true;
            this.TxtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk.LeaveColor = System.Drawing.Color.White;
            this.TxtTk.Location = new System.Drawing.Point(100, 35);
            this.TxtTk.Name = "TxtTk";
            this.TxtTk.Size = new System.Drawing.Size(132, 20);
            this.TxtTk.TabIndex = 2;
            this.TxtTk.VVar = "TK";
            // 
            // filterLineVvarTextBox2
            // 
            this.filterLineVvarTextBox2.Caption = "Mã đơn vị";
            this.filterLineVvarTextBox2.FieldName = null;
            this.filterLineVvarTextBox2.Location = new System.Drawing.Point(6, 60);
            this.filterLineVvarTextBox2.Name = "filterLineVvarTextBox2";
            this.filterLineVvarTextBox2.Size = new System.Drawing.Size(282, 22);
            this.filterLineVvarTextBox2.TabIndex = 3;
            this.filterLineVvarTextBox2.Vvar = "MA_DVCS";
            // 
            // Txtten_KH
            // 
            this.Txtten_KH.AccessibleName = "TEN_KH";
            this.Txtten_KH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtten_KH.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Txtten_KH.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtten_KH.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtten_KH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtten_KH.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtten_KH.HoverColor = System.Drawing.Color.Yellow;
            this.Txtten_KH.LeaveColor = System.Drawing.Color.White;
            this.Txtten_KH.Location = new System.Drawing.Point(4, 92);
            this.Txtten_KH.Margin = new System.Windows.Forms.Padding(4);
            this.Txtten_KH.Multiline = true;
            this.Txtten_KH.Name = "Txtten_KH";
            this.Txtten_KH.ReadOnly = true;
            this.Txtten_KH.Size = new System.Drawing.Size(284, 46);
            this.Txtten_KH.TabIndex = 44;
            this.Txtten_KH.TabStop = false;
            // 
            // AARSD2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Txtten_KH);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.TxtMa_kh);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtTk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct0);
            this.Controls.Add(this.groupBox1);
            this.Name = "AARSD2";
            this.Size = new System.Drawing.Size(295, 224);
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
        private V6Controls.V6DateTimePicker dateNgay_ct0;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox TxtMa_kh;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtTk;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox2;
        private V6Controls.V6ColorTextBox Txtten_KH;
    }
}
