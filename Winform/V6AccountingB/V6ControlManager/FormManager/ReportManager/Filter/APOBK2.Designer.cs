﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class APOBK2
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
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txtnh_kh6 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh2 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_kh3 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox17 = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.filterLineVvarTextBox8 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox4 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox7 = new V6ReportControls.FilterLineVvarTextBox();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.TxtMa_vt = new V6Controls.V6VvarTextBox();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.ftl_ma_gd = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00006";
            this.filterLineVvarTextBox1.FieldCaption = "Mã kho";
            this.filterLineVvarTextBox1.FieldName = "MA_KHO";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(6, 128);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox1.TabIndex = 4;
            this.filterLineVvarTextBox1.Vvar = "MA_KHO";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 106);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(292, 22);
            this.txtMaDvcs.TabIndex = 3;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.FieldCaption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(6, 172);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox3.TabIndex = 6;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.v6Label3);
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Controls.Add(this.ftl_ma_gd);
            this.groupBox1.Controls.Add(this.Txtnh_kh6);
            this.groupBox1.Controls.Add(this.Txtnh_kh5);
            this.groupBox1.Controls.Add(this.Txtnh_kh4);
            this.groupBox1.Controls.Add(this.Txtnh_kh1);
            this.groupBox1.Controls.Add(this.Txtnh_kh2);
            this.groupBox1.Controls.Add(this.Txtnh_kh3);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox17);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox8);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox4);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox7);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 410);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // Txtnh_kh6
            // 
            this.Txtnh_kh6.AccessibleDescription = "FILTERL00016";
            this.Txtnh_kh6.FieldCaption = "Nhóm khách hàng 6";
            this.Txtnh_kh6.FieldName = "NH_KH6";
            this.Txtnh_kh6.Location = new System.Drawing.Point(6, 377);
            this.Txtnh_kh6.Name = "Txtnh_kh6";
            this.Txtnh_kh6.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh6.TabIndex = 15;
            this.Txtnh_kh6.Vvar = "NH_KH";
            // 
            // Txtnh_kh5
            // 
            this.Txtnh_kh5.AccessibleDescription = "FILTERL00015";
            this.Txtnh_kh5.FieldCaption = "Nhóm khách hàng 5";
            this.Txtnh_kh5.FieldName = "NH_KH5";
            this.Txtnh_kh5.Location = new System.Drawing.Point(6, 353);
            this.Txtnh_kh5.Name = "Txtnh_kh5";
            this.Txtnh_kh5.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh5.TabIndex = 14;
            this.Txtnh_kh5.Vvar = "NH_KH";
            // 
            // Txtnh_kh4
            // 
            this.Txtnh_kh4.AccessibleDescription = "FILTERL00014";
            this.Txtnh_kh4.FieldCaption = "Nhóm khách hàng 4";
            this.Txtnh_kh4.FieldName = "NH_KH4";
            this.Txtnh_kh4.Location = new System.Drawing.Point(6, 331);
            this.Txtnh_kh4.Name = "Txtnh_kh4";
            this.Txtnh_kh4.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh4.TabIndex = 13;
            this.Txtnh_kh4.Vvar = "NH_KH";
            // 
            // Txtnh_kh1
            // 
            this.Txtnh_kh1.AccessibleDescription = "FILTERL00011";
            this.Txtnh_kh1.FieldCaption = "Nhóm khách hàng 1";
            this.Txtnh_kh1.FieldName = "NH_KH1";
            this.Txtnh_kh1.Location = new System.Drawing.Point(6, 265);
            this.Txtnh_kh1.Name = "Txtnh_kh1";
            this.Txtnh_kh1.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh1.TabIndex = 10;
            this.Txtnh_kh1.Vvar = "NH_KH";
            // 
            // Txtnh_kh2
            // 
            this.Txtnh_kh2.AccessibleDescription = "FILTERL00012";
            this.Txtnh_kh2.FieldCaption = "Nhóm khách hàng 2";
            this.Txtnh_kh2.FieldName = "NH_KH2";
            this.Txtnh_kh2.Location = new System.Drawing.Point(6, 287);
            this.Txtnh_kh2.Name = "Txtnh_kh2";
            this.Txtnh_kh2.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh2.TabIndex = 11;
            this.Txtnh_kh2.Vvar = "NH_KH";
            // 
            // Txtnh_kh3
            // 
            this.Txtnh_kh3.AccessibleDescription = "FILTERL00013";
            this.Txtnh_kh3.FieldCaption = "Nhóm khách hàng 3";
            this.Txtnh_kh3.FieldName = "NH_KH3";
            this.Txtnh_kh3.Location = new System.Drawing.Point(6, 309);
            this.Txtnh_kh3.Name = "Txtnh_kh3";
            this.Txtnh_kh3.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_kh3.TabIndex = 12;
            this.Txtnh_kh3.Vvar = "NH_KH";
            // 
            // filterLineVvarTextBox17
            // 
            this.filterLineVvarTextBox17.AccessibleDescription = "FILTERL00004";
            this.filterLineVvarTextBox17.FieldCaption = "Mã chứng từ";
            this.filterLineVvarTextBox17.FieldName = "MA_CT";
            this.filterLineVvarTextBox17.Location = new System.Drawing.Point(6, 150);
            this.filterLineVvarTextBox17.Name = "filterLineVvarTextBox17";
            this.filterLineVvarTextBox17.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox17.TabIndex = 5;
            this.filterLineVvarTextBox17.Vvar = "MA_CT";
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
            // filterLineVvarTextBox8
            // 
            this.filterLineVvarTextBox8.AccessibleDescription = "FILTERL00009";
            this.filterLineVvarTextBox8.FieldCaption = "Mã dạng nx";
            this.filterLineVvarTextBox8.FieldName = "MA_NX";
            this.filterLineVvarTextBox8.Location = new System.Drawing.Point(6, 242);
            this.filterLineVvarTextBox8.Name = "filterLineVvarTextBox8";
            this.filterLineVvarTextBox8.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox8.TabIndex = 9;
            this.filterLineVvarTextBox8.Vvar = "MA_NX";
            // 
            // filterLineVvarTextBox4
            // 
            this.filterLineVvarTextBox4.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox4.FieldCaption = "Mã bộ phận";
            this.filterLineVvarTextBox4.FieldName = "MA_BP";
            this.filterLineVvarTextBox4.Location = new System.Drawing.Point(6, 195);
            this.filterLineVvarTextBox4.Name = "filterLineVvarTextBox4";
            this.filterLineVvarTextBox4.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox4.TabIndex = 7;
            this.filterLineVvarTextBox4.Vvar = "MA_BP";
            // 
            // filterLineVvarTextBox7
            // 
            this.filterLineVvarTextBox7.AccessibleDescription = "FILTERL00065";
            this.filterLineVvarTextBox7.FieldCaption = "Mã vụ việc";
            this.filterLineVvarTextBox7.FieldName = "MA_VV";
            this.filterLineVvarTextBox7.Location = new System.Drawing.Point(6, 219);
            this.filterLineVvarTextBox7.Name = "filterLineVvarTextBox7";
            this.filterLineVvarTextBox7.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox7.TabIndex = 8;
            this.filterLineVvarTextBox7.Vvar = "MA_VV";
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(120, 3);
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(120, 27);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00020";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 54);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 3;
            this.v6Label9.Text = "Mã  vật tư";
            // 
            // TxtMa_vt
            // 
            this.TxtMa_vt.AccessibleName = "MA_VT";
            this.TxtMa_vt.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_vt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_vt.CheckNotEmpty = true;
            this.TxtMa_vt.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_vt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_vt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_vt.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_vt.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_vt.Location = new System.Drawing.Point(120, 53);
            this.TxtMa_vt.Name = "TxtMa_vt";
            this.TxtMa_vt.Size = new System.Drawing.Size(100, 20);
            this.TxtMa_vt.TabIndex = 2;
            this.TxtMa_vt.VVar = "MA_VT";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00153";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6Label3.Location = new System.Drawing.Point(33, 84);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(198, 13);
            this.v6Label3.TabIndex = 50;
            this.v6Label3.Text = "7 -Nhập khẩu, 8 -Chi phí, 9 - Nhập khác";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00152";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6Label2.Location = new System.Drawing.Point(33, 67);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(251, 13);
            this.v6Label2.TabIndex = 49;
            this.v6Label2.Text = "1-Nhập mua, 2- Nhập SX, 3- Trả lại, 6- Điều chuyển";
            // 
            // ftl_ma_gd
            // 
            this.ftl_ma_gd.AccessibleDescription = "FILTERL00151";
            this.ftl_ma_gd.FieldCaption = "Loại phiếu";
            this.ftl_ma_gd.FieldName = "MA_GD";
            this.ftl_ma_gd.Location = new System.Drawing.Point(7, 44);
            this.ftl_ma_gd.Name = "ftl_ma_gd";
            this.ftl_ma_gd.Size = new System.Drawing.Size(292, 22);
            this.ftl_ma_gd.TabIndex = 2;
            this.ftl_ma_gd.Vvar = "";
            // 
            // APOBK2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.TxtMa_vt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "APOBK2";
            this.Size = new System.Drawing.Size(307, 491);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox8;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox4;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox7;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox17;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox TxtMa_vt;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh6;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh2;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_kh3;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label2;
        private V6ReportControls.FilterLineVvarTextBox ftl_ma_gd;
    }
}
