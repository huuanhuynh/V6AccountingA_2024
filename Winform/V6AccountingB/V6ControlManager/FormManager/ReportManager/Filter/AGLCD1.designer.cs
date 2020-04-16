namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLCD1
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
            this.lineXemBacTk = new V6ReportControls.FilterLineNumberTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.lineXemChoTk = new V6ReportControls.FilterLineVvarTextBox();
            this.lineXemCacTkSoCai = new V6ReportControls.FilterLineVvarTextBox();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtGroupby = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.chkKieu_f5 = new V6Controls.V6CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 39);
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
            this.groupBox1.Controls.Add(this.lineXemBacTk);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.lineXemChoTk);
            this.groupBox1.Controls.Add(this.lineXemCacTkSoCai);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(0, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 229);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lineXemBacTk
            // 
            this.lineXemBacTk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineXemBacTk.Caption = "Xem bậc Tk";
            this.lineXemBacTk.FieldName = null;
            this.lineXemBacTk.Location = new System.Drawing.Point(6, 123);
            this.lineXemBacTk.Name = "lineXemBacTk";
            this.lineXemBacTk.Operator = "<=";
            this.lineXemBacTk.Size = new System.Drawing.Size(282, 22);
            this.lineXemBacTk.TabIndex = 4;
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
            // lineXemChoTk
            // 
            this.lineXemChoTk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineXemChoTk.Caption = "Xem cho Tk";
            this.lineXemChoTk.FieldName = "";
            this.lineXemChoTk.Location = new System.Drawing.Point(6, 95);
            this.lineXemChoTk.Name = "lineXemChoTk";
            this.lineXemChoTk.Size = new System.Drawing.Size(282, 22);
            this.lineXemChoTk.TabIndex = 3;
            this.lineXemChoTk.Vvar = "TK";
            // 
            // lineXemCacTkSoCai
            // 
            this.lineXemCacTkSoCai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineXemCacTkSoCai.Caption = "Xem các Tk sổ cái";
            this.lineXemCacTkSoCai.FieldName = "";
            this.lineXemCacTkSoCai.Location = new System.Drawing.Point(6, 67);
            this.lineXemCacTkSoCai.Name = "lineXemCacTkSoCai";
            this.lineXemCacTkSoCai.Size = new System.Drawing.Size(282, 22);
            this.lineXemCacTkSoCai.TabIndex = 3;
            this.lineXemCacTkSoCai.Vvar = "";
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(87, 4);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(87, 30);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
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
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00125";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(104, 56);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(80, 13);
            this.v6Label1.TabIndex = 24;
            this.v6Label1.Text = "1 -Không bù trừ";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00129";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 53);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(63, 13);
            this.v6Label9.TabIndex = 23;
            this.v6Label9.Text = "Loại cấn trừ";
            // 
            // TxtGroupby
            // 
            this.TxtGroupby.AccessibleName = "GROUP_BY";
            this.TxtGroupby.BackColor = System.Drawing.SystemColors.Window;
            this.TxtGroupby.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtGroupby.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtGroupby.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtGroupby.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtGroupby.HoverColor = System.Drawing.Color.Yellow;
            this.TxtGroupby.LeaveColor = System.Drawing.Color.White;
            this.TxtGroupby.LimitCharacters = "1234";
            this.TxtGroupby.Location = new System.Drawing.Point(86, 53);
            this.TxtGroupby.MaxLength = 1;
            this.TxtGroupby.Name = "TxtGroupby";
            this.TxtGroupby.Size = new System.Drawing.Size(16, 20);
            this.TxtGroupby.TabIndex = 22;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00126";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(104, 73);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(188, 13);
            this.v6Label2.TabIndex = 25;
            this.v6Label2.Text = "2 -Bù trừ số dư TK CN cùng mã khách";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00127";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(104, 91);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(150, 13);
            this.v6Label3.TabIndex = 26;
            this.v6Label3.Text = "3 -Bù trừ số dư theo tài khoản ";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00128";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(104, 109);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(180, 13);
            this.v6Label4.TabIndex = 27;
            this.v6Label4.Text = "4 -Bù trừ số dư CN có tk mẹ cấp trên";
            // 
            // chkKieu_f5
            // 
            this.chkKieu_f5.AccessibleDescription = "FILTERC00025";
            this.chkKieu_f5.AccessibleName = "Kieu_f5";
            this.chkKieu_f5.AutoSize = true;
            this.chkKieu_f5.Location = new System.Drawing.Point(193, 15);
            this.chkKieu_f5.Name = "chkKieu_f5";
            this.chkKieu_f5.Size = new System.Drawing.Size(92, 17);
            this.chkKieu_f5.TabIndex = 28;
            this.chkKieu_f5.Text = "F5 - Ngày  CT";
            this.chkKieu_f5.UseVisualStyleBackColor = true;
            this.chkKieu_f5.CheckedChanged += new System.EventHandler(this.chkKieu_f5_CheckedChanged);
            // 
            // AGLCD1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkKieu_f5);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtGroupby);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLCD1";
            this.Size = new System.Drawing.Size(295, 364);
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
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtGroupby;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6CheckBox chkKieu_f5;
        private V6ReportControls.FilterLineVvarTextBox lineXemChoTk;
        private V6ReportControls.FilterLineVvarTextBox lineXemCacTkSoCai;
        private V6ReportControls.FilterLineNumberTextBox lineXemBacTk;
    }
}
