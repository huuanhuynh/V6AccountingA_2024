namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGLTH1T
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
            this.txtTk = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtTk_sc = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtbac_tk = new V6Controls.V6NumberTextBox();
            this.rdo_Chitiet = new System.Windows.Forms.RadioButton();
            this.rdo_All = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(5, 39);
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
            this.groupBox1.Controls.Add(this.txtTk);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(3, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtTk
            // 
            this.txtTk.AccessibleDescription = "FILTERL00027";
            this.txtTk.FieldCaption = "Tài khoản";
            this.txtTk.FieldName = "TK";
            this.txtTk.Location = new System.Drawing.Point(5, 63);
            this.txtTk.Name = "txtTk";
            this.txtTk.Size = new System.Drawing.Size(282, 22);
            this.txtTk.TabIndex = 4;
            this.txtTk.Vvar = "TK";
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
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(131, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(131, 30);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
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
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00136";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(157, 61);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(110, 13);
            this.v6Label1.TabIndex = 9;
            this.v6Label1.Text = "1- TK sổ cái, 2-Tất cả";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00135";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 59);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(75, 13);
            this.v6Label9.TabIndex = 7;
            this.v6Label9.Text = "Loại TK sổ cái";
            // 
            // TxtTk_sc
            // 
            this.TxtTk_sc.AccessibleName = "";
            this.TxtTk_sc.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk_sc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk_sc.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk_sc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk_sc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk_sc.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk_sc.LeaveColor = System.Drawing.Color.White;
            this.TxtTk_sc.LimitCharacters = "12";
            this.TxtTk_sc.Location = new System.Drawing.Point(131, 57);
            this.TxtTk_sc.MaxLength = 1;
            this.TxtTk_sc.Name = "TxtTk_sc";
            this.TxtTk_sc.Size = new System.Drawing.Size(26, 20);
            this.TxtTk_sc.TabIndex = 2;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00137";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(5, 84);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(88, 13);
            this.v6Label2.TabIndex = 10;
            this.v6Label2.Text = "Bậc tài khoản <=";
            // 
            // txtbac_tk
            // 
            this.txtbac_tk.AccessibleName = "";
            this.txtbac_tk.BackColor = System.Drawing.Color.White;
            this.txtbac_tk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtbac_tk.DecimalPlaces = 0;
            this.txtbac_tk.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtbac_tk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtbac_tk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtbac_tk.HoverColor = System.Drawing.Color.Yellow;
            this.txtbac_tk.LeaveColor = System.Drawing.Color.White;
            this.txtbac_tk.Location = new System.Drawing.Point(131, 84);
            this.txtbac_tk.Margin = new System.Windows.Forms.Padding(4);
            this.txtbac_tk.Name = "txtbac_tk";
            this.txtbac_tk.Size = new System.Drawing.Size(26, 20);
            this.txtbac_tk.TabIndex = 3;
            this.txtbac_tk.Text = "0";
            this.txtbac_tk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtbac_tk.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // rdo_Chitiet
            // 
            this.rdo_Chitiet.AccessibleDescription = "FILTERR00015";
            this.rdo_Chitiet.AutoSize = true;
            this.rdo_Chitiet.Checked = true;
            this.rdo_Chitiet.Location = new System.Drawing.Point(131, 112);
            this.rdo_Chitiet.Name = "rdo_Chitiet";
            this.rdo_Chitiet.Size = new System.Drawing.Size(123, 17);
            this.rdo_Chitiet.TabIndex = 12;
            this.rdo_Chitiet.TabStop = true;
            this.rdo_Chitiet.Text = "Lấy tài khoản chi tiết";
            this.rdo_Chitiet.UseVisualStyleBackColor = true;
            // 
            // rdo_All
            // 
            this.rdo_All.AccessibleDescription = "FILTERR00016";
            this.rdo_All.AutoSize = true;
            this.rdo_All.Location = new System.Drawing.Point(5, 112);
            this.rdo_All.Name = "rdo_All";
            this.rdo_All.Size = new System.Drawing.Size(115, 17);
            this.rdo_All.TabIndex = 11;
            this.rdo_All.Text = "Tất cả các bậc TK";
            this.rdo_All.UseVisualStyleBackColor = true;
            // 
            // AGLTH1T
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdo_Chitiet);
            this.Controls.Add(this.rdo_All);
            this.Controls.Add(this.txtbac_tk);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtTk_sc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AGLTH1T";
            this.Size = new System.Drawing.Size(295, 234);
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
        private V6ReportControls.FilterLineVvarTextBox txtTk;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtTk_sc;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6NumberTextBox txtbac_tk;
        private System.Windows.Forms.RadioButton rdo_Chitiet;
        private System.Windows.Forms.RadioButton rdo_All;
    }
}
