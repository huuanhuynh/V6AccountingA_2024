namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACACNTG
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
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.txtThang2 = new V6Controls.V6NumberTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtAp_tg_gd = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtKieu_tg_gs = new V6Controls.V6NumberTextBox();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label4 = new V6Controls.V6Label();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.v6Label5 = new V6Controls.V6Label();
            this.TxtTk = new V6Controls.V6VvarTextBox();
            this.txtTyGia = new V6Controls.V6NumberTextBox();
            this.v6Label17 = new V6Controls.V6Label();
            this.v6Label6 = new V6Controls.V6Label();
            this.txtLoai_cl = new V6Controls.V6NumberTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00055";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 53);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Năm";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00054";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến tháng";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00053";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ tháng";
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
            this.txtThang1.Location = new System.Drawing.Point(175, 2);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(100, 20);
            this.txtThang1.TabIndex = 0;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.V6LostFocus += new V6Controls.ControlEventHandle(this.Make_date12);
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
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
            this.txtThang2.Location = new System.Drawing.Point(175, 26);
            this.txtThang2.MaxLength = 2;
            this.txtThang2.MaxNumLength = 2;
            this.txtThang2.Name = "txtThang2";
            this.txtThang2.Size = new System.Drawing.Size(100, 20);
            this.txtThang2.TabIndex = 1;
            this.txtThang2.Text = "0";
            this.txtThang2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang2.V6LostFocus += new V6Controls.ControlEventHandle(this.Make_date12);
            this.txtThang2.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
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
            this.txtNam.Location = new System.Drawing.Point(175, 50);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNam.V6LostFocus += new V6Controls.ControlEventHandle(this.Make_date12);
            // 
            // txtAp_tg_gd
            // 
            this.txtAp_tg_gd.AccessibleName = "Ap_tg_gd";
            this.txtAp_tg_gd.BackColor = System.Drawing.SystemColors.Window;
            this.txtAp_tg_gd.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtAp_tg_gd.DecimalPlaces = 0;
            this.txtAp_tg_gd.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtAp_tg_gd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAp_tg_gd.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtAp_tg_gd.HoverColor = System.Drawing.Color.Yellow;
            this.txtAp_tg_gd.LeaveColor = System.Drawing.Color.White;
            this.txtAp_tg_gd.LimitCharacters = "01";
            this.txtAp_tg_gd.Location = new System.Drawing.Point(175, 74);
            this.txtAp_tg_gd.MaxLength = 1;
            this.txtAp_tg_gd.MaxNumLength = 1;
            this.txtAp_tg_gd.Name = "txtAp_tg_gd";
            this.txtAp_tg_gd.Size = new System.Drawing.Size(18, 20);
            this.txtAp_tg_gd.TabIndex = 3;
            this.txtAp_tg_gd.Text = "0";
            this.txtAp_tg_gd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAp_tg_gd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00056";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(200, 77);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(87, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "0 - Không , 1- Có";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00057";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(200, 100);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(169, 13);
            this.v6Label2.TabIndex = 11;
            this.v6Label2.Text = "1 -Tỷ giá tự động, 2- Tỷ giá tự tính";
            // 
            // txtKieu_tg_gs
            // 
            this.txtKieu_tg_gs.AccessibleName = "Kieu_tg_gs";
            this.txtKieu_tg_gs.BackColor = System.Drawing.SystemColors.Window;
            this.txtKieu_tg_gs.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKieu_tg_gs.DecimalPlaces = 0;
            this.txtKieu_tg_gs.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKieu_tg_gs.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKieu_tg_gs.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKieu_tg_gs.HoverColor = System.Drawing.Color.Yellow;
            this.txtKieu_tg_gs.LeaveColor = System.Drawing.Color.White;
            this.txtKieu_tg_gs.LimitCharacters = "12";
            this.txtKieu_tg_gs.Location = new System.Drawing.Point(175, 98);
            this.txtKieu_tg_gs.MaxLength = 1;
            this.txtKieu_tg_gs.MaxNumLength = 1;
            this.txtKieu_tg_gs.Name = "txtKieu_tg_gs";
            this.txtKieu_tg_gs.Size = new System.Drawing.Size(18, 20);
            this.txtKieu_tg_gs.TabIndex = 4;
            this.txtKieu_tg_gs.Text = "0";
            this.txtKieu_tg_gs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKieu_tg_gs.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKieu_tg_gs.TextChanged += new System.EventHandler(this.txtKieu_tg_gs_TextChanged);
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00060";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(5, 77);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(160, 13);
            this.v6Label3.TabIndex = 6;
            this.v6Label3.Text = "Lấy tỷ giá giao dịch từ danh mục";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00061";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(5, 101);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(72, 13);
            this.v6Label4.TabIndex = 9;
            this.v6Label4.Text = "Kiểu lấy tỷ giá";
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
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Location = new System.Drawing.Point(3, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 92);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(9, 44);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(292, 22);
            this.txtMaDvcs.TabIndex = 13;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "FILTERL00027";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(5, 125);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(55, 13);
            this.v6Label5.TabIndex = 14;
            this.v6Label5.Text = "Tài khoản";
            // 
            // TxtTk
            // 
            this.TxtTk.AccessibleName = "TK";
            this.TxtTk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk.LeaveColor = System.Drawing.Color.White;
            this.TxtTk.Location = new System.Drawing.Point(175, 122);
            this.TxtTk.Name = "TxtTk";
            this.TxtTk.Size = new System.Drawing.Size(100, 20);
            this.TxtTk.TabIndex = 5;
            this.TxtTk.VVar = "TK";
            // 
            // txtTyGia
            // 
            this.txtTyGia.AccessibleDescription = "";
            this.txtTyGia.AccessibleName = "ty_gia";
            this.txtTyGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTyGia.BackColor = System.Drawing.Color.White;
            this.txtTyGia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTyGia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTyGia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.HoverColor = System.Drawing.Color.Yellow;
            this.txtTyGia.LeaveColor = System.Drawing.Color.White;
            this.txtTyGia.Location = new System.Drawing.Point(175, 146);
            this.txtTyGia.Name = "txtTyGia";
            this.txtTyGia.Size = new System.Drawing.Size(100, 20);
            this.txtTyGia.TabIndex = 6;
            this.txtTyGia.Text = "0,000";
            this.txtTyGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTyGia.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "FILTERL00062";
            this.v6Label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(5, 149);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(67, 13);
            this.v6Label17.TabIndex = 17;
            this.v6Label17.Text = "Tỷ giá ghi sổ";
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "FILTERL00063";
            this.v6Label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(5, 173);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(152, 13);
            this.v6Label6.TabIndex = 18;
            this.v6Label6.Text = "Phương pháp tính tỷ giá ghi sổ";
            // 
            // txtLoai_cl
            // 
            this.txtLoai_cl.AccessibleName = "Loai_cl";
            this.txtLoai_cl.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoai_cl.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoai_cl.DecimalPlaces = 0;
            this.txtLoai_cl.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLoai_cl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoai_cl.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoai_cl.HoverColor = System.Drawing.Color.Yellow;
            this.txtLoai_cl.LeaveColor = System.Drawing.Color.White;
            this.txtLoai_cl.LimitCharacters = "1345";
            this.txtLoai_cl.Location = new System.Drawing.Point(175, 170);
            this.txtLoai_cl.MaxLength = 1;
            this.txtLoai_cl.MaxNumLength = 1;
            this.txtLoai_cl.Name = "txtLoai_cl";
            this.txtLoai_cl.Size = new System.Drawing.Size(18, 20);
            this.txtLoai_cl.TabIndex = 7;
            this.txtLoai_cl.Text = "0";
            this.txtLoai_cl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLoai_cl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "FILTERL00058";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(204, 176);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(191, 13);
            this.v6Label7.TabIndex = 20;
            this.v6Label7.Text = "1 -Trung bình, 3- Nhập trước xuất trước";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "FILTERL00059";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(204, 195);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(202, 13);
            this.v6Label8.TabIndex = 21;
            this.v6Label8.Text = "4 -Trung bình di động, 5- Tỷ giá giao dịch";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.Enabled = false;
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(290, 26);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 23;
            this.dateNgay_ct2.TabStop = false;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.Enabled = false;
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(290, 2);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 22;
            this.dateNgay_ct1.TabStop = false;
            // 
            // ACACNTG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtLoai_cl);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.txtTyGia);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.TxtTk);
            this.Controls.Add(this.txtKieu_tg_gs);
            this.Controls.Add(this.txtAp_tg_gd);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang2);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ACACNTG";
            this.Size = new System.Drawing.Size(592, 374);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6NumberTextBox txtThang2;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtAp_tg_gd;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6NumberTextBox txtKieu_tg_gs;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label4;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6VvarTextBox TxtTk;
        private V6Controls.V6NumberTextBox txtTyGia;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6Label v6Label6;
        private V6Controls.V6NumberTextBox txtLoai_cl;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6Label v6Label8;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
    }
}
