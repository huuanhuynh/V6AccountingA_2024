﻿namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class NgoaiTeAddEditForm
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
            this.number = new V6Controls.V6NumberTextBox();
            this.only2 = new V6Controls.V6ColorTextBox();
            this.endpoint2 = new V6Controls.V6ColorTextBox();
            this.only1 = new V6Controls.V6ColorTextBox();
            this.end2 = new V6Controls.V6ColorTextBox();
            this.endpoint1 = new V6Controls.V6ColorTextBox();
            this.point2 = new V6Controls.V6ColorTextBox();
            this.end1 = new V6Controls.V6ColorTextBox();
            this.begin2 = new V6Controls.V6ColorTextBox();
            this.point1 = new V6Controls.V6ColorTextBox();
            this.begin1 = new V6Controls.V6ColorTextBox();
            this.TxtMa_nt = new V6Controls.V6ColorTextBox();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.v6ColorTextBox5 = new V6Controls.V6ColorTextBox();
            this.lblDocSoTien = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtTen_nt = new V6Controls.V6ColorTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.v6TabControl1 = new V6Controls.V6TabControl();
            this.tabThongTinChinh = new System.Windows.Forms.TabPage();
            this.tabTuDinhNghia = new System.Windows.Forms.TabPage();
            this.txtMaS3 = new V6Controls.V6ColorTextBox();
            this.txtMaS2 = new V6Controls.V6ColorTextBox();
            this.txtMaS1 = new V6Controls.V6ColorTextBox();
            this.txtSLS6 = new V6Controls.V6NumberTextBox();
            this.txtSLS5 = new V6Controls.V6NumberTextBox();
            this.txtSLS4 = new V6Controls.V6NumberTextBox();
            this.lblSLS6 = new System.Windows.Forms.Label();
            this.lblSLS5 = new System.Windows.Forms.Label();
            this.lblSLS4 = new System.Windows.Forms.Label();
            this.lblNgayS9 = new System.Windows.Forms.Label();
            this.lblNgayS8 = new System.Windows.Forms.Label();
            this.lblNgayS7 = new System.Windows.Forms.Label();
            this.lblMaS3 = new System.Windows.Forms.Label();
            this.lblMaS2 = new System.Windows.Forms.Label();
            this.lblMaS1 = new System.Windows.Forms.Label();
            this.dateNgayS9 = new V6Controls.V6DateTimeColor();
            this.dateNgayS8 = new V6Controls.V6DateTimeColor();
            this.dateNgayS7 = new V6Controls.V6DateTimeColor();
            this.txtGC_TD3 = new V6Controls.V6VvarTextBox();
            this.txtGC_TD2 = new V6Controls.V6VvarTextBox();
            this.txtGC_TD1 = new V6Controls.V6VvarTextBox();
            this.txtMA_TD3 = new V6Controls.V6VvarTextBox();
            this.txtMA_TD2 = new V6Controls.V6VvarTextBox();
            this.txtMA_TD1 = new V6Controls.V6VvarTextBox();
            this.v6NumberTextBox3 = new V6Controls.V6NumberTextBox();
            this.v6NumberTextBox2 = new V6Controls.V6NumberTextBox();
            this.v6NumberTextBox1 = new V6Controls.V6NumberTextBox();
            this.v6ColorDateTimePick3 = new V6Controls.V6DateTimeColor();
            this.v6ColorDateTimePick2 = new V6Controls.V6DateTimeColor();
            this.v6ColorDateTimePick1 = new V6Controls.V6DateTimeColor();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.v6TabControl1.SuspendLayout();
            this.tabThongTinChinh.SuspendLayout();
            this.tabTuDinhNghia.SuspendLayout();
            this.SuspendLayout();
            // 
            // number
            // 
            this.number.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.number.BackColor = System.Drawing.Color.White;
            this.number.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.number.EnterColor = System.Drawing.Color.PaleGreen;
            this.number.ForeColor = System.Drawing.SystemColors.WindowText;
            this.number.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.number.HoverColor = System.Drawing.Color.Yellow;
            this.number.LeaveColor = System.Drawing.Color.White;
            this.number.Location = new System.Drawing.Point(173, 166);
            this.number.Margin = new System.Windows.Forms.Padding(5);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(135, 23);
            this.number.TabIndex = 20;
            this.number.Text = "987 654 321,000";
            this.number.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.number.Value = new decimal(new int[] {
            -188157080,
            229,
            0,
            196608});
            this.number.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // only2
            // 
            this.only2.AccessibleName = "Only2";
            this.only2.BackColor = System.Drawing.SystemColors.Window;
            this.only2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.only2.EnterColor = System.Drawing.Color.PaleGreen;
            this.only2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.only2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.only2.HoverColor = System.Drawing.Color.Yellow;
            this.only2.LeaveColor = System.Drawing.Color.White;
            this.only2.Location = new System.Drawing.Point(478, 135);
            this.only2.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.only2.Name = "only2";
            this.only2.Size = new System.Drawing.Size(71, 23);
            this.only2.TabIndex = 18;
            this.only2.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // endpoint2
            // 
            this.endpoint2.AccessibleName = "EndPoint2";
            this.endpoint2.BackColor = System.Drawing.SystemColors.Window;
            this.endpoint2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.endpoint2.EnterColor = System.Drawing.Color.PaleGreen;
            this.endpoint2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.endpoint2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.endpoint2.HoverColor = System.Drawing.Color.Yellow;
            this.endpoint2.LeaveColor = System.Drawing.Color.White;
            this.endpoint2.Location = new System.Drawing.Point(402, 135);
            this.endpoint2.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.endpoint2.Name = "endpoint2";
            this.endpoint2.Size = new System.Drawing.Size(71, 23);
            this.endpoint2.TabIndex = 17;
            this.endpoint2.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // only1
            // 
            this.only1.AccessibleName = "Only1";
            this.only1.BackColor = System.Drawing.SystemColors.Window;
            this.only1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.only1.EnterColor = System.Drawing.Color.PaleGreen;
            this.only1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.only1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.only1.HoverColor = System.Drawing.Color.Yellow;
            this.only1.LeaveColor = System.Drawing.Color.White;
            this.only1.Location = new System.Drawing.Point(478, 104);
            this.only1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.only1.Name = "only1";
            this.only1.Size = new System.Drawing.Size(71, 23);
            this.only1.TabIndex = 11;
            this.only1.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // end2
            // 
            this.end2.AccessibleName = "End2";
            this.end2.BackColor = System.Drawing.SystemColors.Window;
            this.end2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.end2.EnterColor = System.Drawing.Color.PaleGreen;
            this.end2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.end2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.end2.HoverColor = System.Drawing.Color.Yellow;
            this.end2.LeaveColor = System.Drawing.Color.White;
            this.end2.Location = new System.Drawing.Point(249, 135);
            this.end2.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.end2.Name = "end2";
            this.end2.Size = new System.Drawing.Size(71, 23);
            this.end2.TabIndex = 15;
            this.end2.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // endpoint1
            // 
            this.endpoint1.AccessibleName = "EndPoint1";
            this.endpoint1.BackColor = System.Drawing.SystemColors.Window;
            this.endpoint1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.endpoint1.EnterColor = System.Drawing.Color.PaleGreen;
            this.endpoint1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.endpoint1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.endpoint1.HoverColor = System.Drawing.Color.Yellow;
            this.endpoint1.LeaveColor = System.Drawing.Color.White;
            this.endpoint1.Location = new System.Drawing.Point(402, 104);
            this.endpoint1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.endpoint1.Name = "endpoint1";
            this.endpoint1.Size = new System.Drawing.Size(71, 23);
            this.endpoint1.TabIndex = 10;
            this.endpoint1.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // point2
            // 
            this.point2.AccessibleName = "Point2";
            this.point2.BackColor = System.Drawing.SystemColors.Window;
            this.point2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.point2.EnterColor = System.Drawing.Color.PaleGreen;
            this.point2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.point2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.point2.HoverColor = System.Drawing.Color.Yellow;
            this.point2.LeaveColor = System.Drawing.Color.White;
            this.point2.Location = new System.Drawing.Point(326, 135);
            this.point2.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.point2.Name = "point2";
            this.point2.Size = new System.Drawing.Size(71, 23);
            this.point2.TabIndex = 16;
            this.point2.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // end1
            // 
            this.end1.AccessibleName = "End1";
            this.end1.BackColor = System.Drawing.SystemColors.Window;
            this.end1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.end1.EnterColor = System.Drawing.Color.PaleGreen;
            this.end1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.end1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.end1.HoverColor = System.Drawing.Color.Yellow;
            this.end1.LeaveColor = System.Drawing.Color.White;
            this.end1.Location = new System.Drawing.Point(249, 104);
            this.end1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.end1.Name = "end1";
            this.end1.Size = new System.Drawing.Size(71, 23);
            this.end1.TabIndex = 8;
            this.end1.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // begin2
            // 
            this.begin2.AccessibleName = "Begin2";
            this.begin2.BackColor = System.Drawing.SystemColors.Window;
            this.begin2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.begin2.EnterColor = System.Drawing.Color.PaleGreen;
            this.begin2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.begin2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.begin2.HoverColor = System.Drawing.Color.Yellow;
            this.begin2.LeaveColor = System.Drawing.Color.White;
            this.begin2.Location = new System.Drawing.Point(173, 135);
            this.begin2.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.begin2.Name = "begin2";
            this.begin2.Size = new System.Drawing.Size(71, 23);
            this.begin2.TabIndex = 14;
            this.begin2.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // point1
            // 
            this.point1.AccessibleName = "Point1";
            this.point1.BackColor = System.Drawing.SystemColors.Window;
            this.point1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.point1.EnterColor = System.Drawing.Color.PaleGreen;
            this.point1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.point1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.point1.HoverColor = System.Drawing.Color.Yellow;
            this.point1.LeaveColor = System.Drawing.Color.White;
            this.point1.Location = new System.Drawing.Point(326, 104);
            this.point1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.point1.Name = "point1";
            this.point1.Size = new System.Drawing.Size(71, 23);
            this.point1.TabIndex = 9;
            this.point1.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // begin1
            // 
            this.begin1.AccessibleName = "Begin1";
            this.begin1.BackColor = System.Drawing.SystemColors.Window;
            this.begin1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.begin1.EnterColor = System.Drawing.Color.PaleGreen;
            this.begin1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.begin1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.begin1.HoverColor = System.Drawing.Color.Yellow;
            this.begin1.LeaveColor = System.Drawing.Color.White;
            this.begin1.Location = new System.Drawing.Point(173, 104);
            this.begin1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.begin1.Name = "begin1";
            this.begin1.Size = new System.Drawing.Size(71, 23);
            this.begin1.TabIndex = 7;
            this.begin1.TextChanged += new System.EventHandler(this.only2_TextChanged);
            // 
            // TxtMa_nt
            // 
            this.TxtMa_nt.AccessibleName = "ma_nt";
            this.TxtMa_nt.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_nt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtMa_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nt.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_nt.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_nt.Location = new System.Drawing.Point(173, 11);
            this.TxtMa_nt.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.TxtMa_nt.Name = "TxtMa_nt";
            this.TxtMa_nt.Size = new System.Drawing.Size(129, 23);
            this.TxtMa_nt.TabIndex = 1;
            this.TxtMa_nt.UseLimitCharacters0 = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "ADDEDITC00001";
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(173, 350);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "ADDEDITL00022";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 351);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Trạng Thái";
            // 
            // v6ColorTextBox5
            // 
            this.v6ColorTextBox5.AccessibleName = "ten_nt2";
            this.v6ColorTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox5.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorTextBox5.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox5.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox5.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox5.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox5.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox5.Location = new System.Drawing.Point(173, 73);
            this.v6ColorTextBox5.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6ColorTextBox5.Name = "v6ColorTextBox5";
            this.v6ColorTextBox5.Size = new System.Drawing.Size(472, 23);
            this.v6ColorTextBox5.TabIndex = 5;
            // 
            // lblDocSoTien
            // 
            this.lblDocSoTien.AccessibleDescription = ".";
            this.lblDocSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocSoTien.Location = new System.Drawing.Point(170, 205);
            this.lblDocSoTien.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblDocSoTien.Name = "lblDocSoTien";
            this.lblDocSoTien.Size = new System.Drawing.Size(475, 140);
            this.lblDocSoTien.TabIndex = 21;
            this.lblDocSoTien.Text = "lblDocSoTien";
            // 
            // label6
            // 
            this.label6.AccessibleDescription = "ADDEDITL00511";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 173);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Số mẫu";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "ADDEDITL00510";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cách đọc tiền tiếng Anh";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ADDEDITL00509";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cách đọc tiền tiếng Việt";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "ADDEDITL00508";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 77);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên tiền tệ 2";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "ADDEDITL00506";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã ngoại tệ";
            // 
            // TxtTen_nt
            // 
            this.TxtTen_nt.AccessibleName = "ten_nt";
            this.TxtTen_nt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTen_nt.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTen_nt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTen_nt.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTen_nt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTen_nt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTen_nt.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTen_nt.LeaveColor = System.Drawing.Color.White;
            this.TxtTen_nt.Location = new System.Drawing.Point(173, 42);
            this.TxtTen_nt.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.TxtTen_nt.Name = "TxtTen_nt";
            this.TxtTen_nt.Size = new System.Drawing.Size(472, 23);
            this.TxtTen_nt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ADDEDITL00507";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên tiền tệ";
            // 
            // v6TabControl1
            // 
            this.v6TabControl1.Controls.Add(this.tabThongTinChinh);
            this.v6TabControl1.Controls.Add(this.tabTuDinhNghia);
            this.v6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl1.Location = new System.Drawing.Point(4, 4);
            this.v6TabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.v6TabControl1.Name = "v6TabControl1";
            this.v6TabControl1.SelectedIndex = 0;
            this.v6TabControl1.Size = new System.Drawing.Size(757, 406);
            this.v6TabControl1.TabIndex = 3;
            // 
            // tabThongTinChinh
            // 
            this.tabThongTinChinh.AccessibleDescription = "ADDEDITT00001";
            this.tabThongTinChinh.AutoScroll = true;
            this.tabThongTinChinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabThongTinChinh.Controls.Add(this.number);
            this.tabThongTinChinh.Controls.Add(this.TxtMa_nt);
            this.tabThongTinChinh.Controls.Add(this.only2);
            this.tabThongTinChinh.Controls.Add(this.label2);
            this.tabThongTinChinh.Controls.Add(this.endpoint2);
            this.tabThongTinChinh.Controls.Add(this.TxtTen_nt);
            this.tabThongTinChinh.Controls.Add(this.only1);
            this.tabThongTinChinh.Controls.Add(this.label1);
            this.tabThongTinChinh.Controls.Add(this.end2);
            this.tabThongTinChinh.Controls.Add(this.label5);
            this.tabThongTinChinh.Controls.Add(this.endpoint1);
            this.tabThongTinChinh.Controls.Add(this.label3);
            this.tabThongTinChinh.Controls.Add(this.point2);
            this.tabThongTinChinh.Controls.Add(this.label7);
            this.tabThongTinChinh.Controls.Add(this.end1);
            this.tabThongTinChinh.Controls.Add(this.label6);
            this.tabThongTinChinh.Controls.Add(this.begin2);
            this.tabThongTinChinh.Controls.Add(this.lblDocSoTien);
            this.tabThongTinChinh.Controls.Add(this.point1);
            this.tabThongTinChinh.Controls.Add(this.v6ColorTextBox5);
            this.tabThongTinChinh.Controls.Add(this.begin1);
            this.tabThongTinChinh.Controls.Add(this.label4);
            this.tabThongTinChinh.Controls.Add(this.checkBox1);
            this.tabThongTinChinh.Location = new System.Drawing.Point(4, 28);
            this.tabThongTinChinh.Margin = new System.Windows.Forms.Padding(4);
            this.tabThongTinChinh.Name = "tabThongTinChinh";
            this.tabThongTinChinh.Padding = new System.Windows.Forms.Padding(4);
            this.tabThongTinChinh.Size = new System.Drawing.Size(749, 374);
            this.tabThongTinChinh.TabIndex = 0;
            this.tabThongTinChinh.Text = "Thông tin chính";
            // 
            // tabTuDinhNghia
            // 
            this.tabTuDinhNghia.AccessibleDescription = "ADDEDITT00003";
            this.tabTuDinhNghia.AutoScroll = true;
            this.tabTuDinhNghia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabTuDinhNghia.Controls.Add(this.txtMaS3);
            this.tabTuDinhNghia.Controls.Add(this.txtMaS2);
            this.tabTuDinhNghia.Controls.Add(this.txtMaS1);
            this.tabTuDinhNghia.Controls.Add(this.txtSLS6);
            this.tabTuDinhNghia.Controls.Add(this.txtSLS5);
            this.tabTuDinhNghia.Controls.Add(this.txtSLS4);
            this.tabTuDinhNghia.Controls.Add(this.lblSLS6);
            this.tabTuDinhNghia.Controls.Add(this.lblSLS5);
            this.tabTuDinhNghia.Controls.Add(this.lblSLS4);
            this.tabTuDinhNghia.Controls.Add(this.lblNgayS9);
            this.tabTuDinhNghia.Controls.Add(this.lblNgayS8);
            this.tabTuDinhNghia.Controls.Add(this.lblNgayS7);
            this.tabTuDinhNghia.Controls.Add(this.lblMaS3);
            this.tabTuDinhNghia.Controls.Add(this.lblMaS2);
            this.tabTuDinhNghia.Controls.Add(this.lblMaS1);
            this.tabTuDinhNghia.Controls.Add(this.dateNgayS9);
            this.tabTuDinhNghia.Controls.Add(this.dateNgayS8);
            this.tabTuDinhNghia.Controls.Add(this.dateNgayS7);
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD3);
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD2);
            this.tabTuDinhNghia.Controls.Add(this.txtGC_TD1);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD3);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD2);
            this.tabTuDinhNghia.Controls.Add(this.txtMA_TD1);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox3);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox2);
            this.tabTuDinhNghia.Controls.Add(this.v6NumberTextBox1);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick3);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick2);
            this.tabTuDinhNghia.Controls.Add(this.v6ColorDateTimePick1);
            this.tabTuDinhNghia.Controls.Add(this.label29);
            this.tabTuDinhNghia.Controls.Add(this.label28);
            this.tabTuDinhNghia.Controls.Add(this.label27);
            this.tabTuDinhNghia.Controls.Add(this.label16);
            this.tabTuDinhNghia.Controls.Add(this.label17);
            this.tabTuDinhNghia.Controls.Add(this.label22);
            this.tabTuDinhNghia.Controls.Add(this.label30);
            this.tabTuDinhNghia.Controls.Add(this.label31);
            this.tabTuDinhNghia.Controls.Add(this.label32);
            this.tabTuDinhNghia.Controls.Add(this.label33);
            this.tabTuDinhNghia.Controls.Add(this.label34);
            this.tabTuDinhNghia.Controls.Add(this.label35);
            this.tabTuDinhNghia.Location = new System.Drawing.Point(4, 28);
            this.tabTuDinhNghia.Margin = new System.Windows.Forms.Padding(4);
            this.tabTuDinhNghia.Name = "tabTuDinhNghia";
            this.tabTuDinhNghia.Padding = new System.Windows.Forms.Padding(4);
            this.tabTuDinhNghia.Size = new System.Drawing.Size(749, 374);
            this.tabTuDinhNghia.TabIndex = 2;
            this.tabTuDinhNghia.Text = "Tự định nghĩa";
            // 
            // txtMaS3
            // 
            this.txtMaS3.AccessibleName = "S3";
            this.txtMaS3.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaS3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaS3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaS3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaS3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaS3.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaS3.LeaveColor = System.Drawing.Color.White;
            this.txtMaS3.Location = new System.Drawing.Point(523, 52);
            this.txtMaS3.Margin = new System.Windows.Forms.Padding(5);
            this.txtMaS3.Name = "txtMaS3";
            this.txtMaS3.Size = new System.Drawing.Size(200, 23);
            this.txtMaS3.TabIndex = 125;
            // 
            // txtMaS2
            // 
            this.txtMaS2.AccessibleName = "S2";
            this.txtMaS2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaS2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaS2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaS2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaS2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaS2.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaS2.LeaveColor = System.Drawing.Color.White;
            this.txtMaS2.Location = new System.Drawing.Point(523, 28);
            this.txtMaS2.Margin = new System.Windows.Forms.Padding(5);
            this.txtMaS2.Name = "txtMaS2";
            this.txtMaS2.Size = new System.Drawing.Size(200, 23);
            this.txtMaS2.TabIndex = 123;
            // 
            // txtMaS1
            // 
            this.txtMaS1.AccessibleName = "S1";
            this.txtMaS1.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaS1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaS1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaS1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaS1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaS1.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaS1.LeaveColor = System.Drawing.Color.White;
            this.txtMaS1.Location = new System.Drawing.Point(523, 4);
            this.txtMaS1.Margin = new System.Windows.Forms.Padding(5);
            this.txtMaS1.Name = "txtMaS1";
            this.txtMaS1.Size = new System.Drawing.Size(200, 23);
            this.txtMaS1.TabIndex = 121;
            // 
            // txtSLS6
            // 
            this.txtSLS6.AccessibleName = "S6";
            this.txtSLS6.BackColor = System.Drawing.Color.White;
            this.txtSLS6.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSLS6.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSLS6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSLS6.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSLS6.HoverColor = System.Drawing.Color.Yellow;
            this.txtSLS6.LeaveColor = System.Drawing.Color.White;
            this.txtSLS6.Location = new System.Drawing.Point(523, 196);
            this.txtSLS6.Margin = new System.Windows.Forms.Padding(5);
            this.txtSLS6.Name = "txtSLS6";
            this.txtSLS6.Size = new System.Drawing.Size(200, 23);
            this.txtSLS6.TabIndex = 137;
            this.txtSLS6.Text = "0,000";
            this.txtSLS6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSLS6.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSLS5
            // 
            this.txtSLS5.AccessibleName = "S5";
            this.txtSLS5.BackColor = System.Drawing.Color.White;
            this.txtSLS5.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSLS5.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSLS5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSLS5.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSLS5.HoverColor = System.Drawing.Color.Yellow;
            this.txtSLS5.LeaveColor = System.Drawing.Color.White;
            this.txtSLS5.Location = new System.Drawing.Point(523, 172);
            this.txtSLS5.Margin = new System.Windows.Forms.Padding(5);
            this.txtSLS5.Name = "txtSLS5";
            this.txtSLS5.Size = new System.Drawing.Size(200, 23);
            this.txtSLS5.TabIndex = 135;
            this.txtSLS5.Text = "0,000";
            this.txtSLS5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSLS5.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSLS4
            // 
            this.txtSLS4.AccessibleName = "S4";
            this.txtSLS4.BackColor = System.Drawing.Color.White;
            this.txtSLS4.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSLS4.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSLS4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSLS4.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSLS4.HoverColor = System.Drawing.Color.Yellow;
            this.txtSLS4.LeaveColor = System.Drawing.Color.White;
            this.txtSLS4.Location = new System.Drawing.Point(523, 148);
            this.txtSLS4.Margin = new System.Windows.Forms.Padding(5);
            this.txtSLS4.Name = "txtSLS4";
            this.txtSLS4.Size = new System.Drawing.Size(200, 23);
            this.txtSLS4.TabIndex = 133;
            this.txtSLS4.Text = "0,000";
            this.txtSLS4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSLS4.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSLS6
            // 
            this.lblSLS6.AccessibleDescription = "M_S6,S6";
            this.lblSLS6.AutoSize = true;
            this.lblSLS6.Location = new System.Drawing.Point(363, 199);
            this.lblSLS6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSLS6.Name = "lblSLS6";
            this.lblSLS6.Size = new System.Drawing.Size(46, 17);
            this.lblSLS6.TabIndex = 136;
            this.lblSLS6.Text = "SL S6";
            // 
            // lblSLS5
            // 
            this.lblSLS5.AccessibleDescription = "M_S5,S5";
            this.lblSLS5.AutoSize = true;
            this.lblSLS5.Location = new System.Drawing.Point(363, 175);
            this.lblSLS5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSLS5.Name = "lblSLS5";
            this.lblSLS5.Size = new System.Drawing.Size(46, 17);
            this.lblSLS5.TabIndex = 134;
            this.lblSLS5.Text = "SL S5";
            // 
            // lblSLS4
            // 
            this.lblSLS4.AccessibleDescription = "M_S4,S4";
            this.lblSLS4.AutoSize = true;
            this.lblSLS4.Location = new System.Drawing.Point(363, 151);
            this.lblSLS4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSLS4.Name = "lblSLS4";
            this.lblSLS4.Size = new System.Drawing.Size(46, 17);
            this.lblSLS4.TabIndex = 132;
            this.lblSLS4.Text = "SL S4";
            // 
            // lblNgayS9
            // 
            this.lblNgayS9.AccessibleDescription = "M_S9,S9";
            this.lblNgayS9.AutoSize = true;
            this.lblNgayS9.Location = new System.Drawing.Point(363, 127);
            this.lblNgayS9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNgayS9.Name = "lblNgayS9";
            this.lblNgayS9.Size = new System.Drawing.Size(62, 17);
            this.lblNgayS9.TabIndex = 130;
            this.lblNgayS9.Text = "Ngày S9";
            // 
            // lblNgayS8
            // 
            this.lblNgayS8.AccessibleDescription = "M_S8,S8";
            this.lblNgayS8.AutoSize = true;
            this.lblNgayS8.Location = new System.Drawing.Point(363, 103);
            this.lblNgayS8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNgayS8.Name = "lblNgayS8";
            this.lblNgayS8.Size = new System.Drawing.Size(62, 17);
            this.lblNgayS8.TabIndex = 128;
            this.lblNgayS8.Text = "Ngày S8";
            // 
            // lblNgayS7
            // 
            this.lblNgayS7.AccessibleDescription = "M_S7,S7";
            this.lblNgayS7.AutoSize = true;
            this.lblNgayS7.Location = new System.Drawing.Point(363, 79);
            this.lblNgayS7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNgayS7.Name = "lblNgayS7";
            this.lblNgayS7.Size = new System.Drawing.Size(62, 17);
            this.lblNgayS7.TabIndex = 126;
            this.lblNgayS7.Text = "Ngày S7";
            // 
            // lblMaS3
            // 
            this.lblMaS3.AccessibleDescription = "M_S3,S3";
            this.lblMaS3.AutoSize = true;
            this.lblMaS3.Location = new System.Drawing.Point(363, 55);
            this.lblMaS3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMaS3.Name = "lblMaS3";
            this.lblMaS3.Size = new System.Drawing.Size(48, 17);
            this.lblMaS3.TabIndex = 124;
            this.lblMaS3.Text = "Mã S3";
            // 
            // lblMaS2
            // 
            this.lblMaS2.AccessibleDescription = "M_S2,S2";
            this.lblMaS2.AutoSize = true;
            this.lblMaS2.Location = new System.Drawing.Point(363, 31);
            this.lblMaS2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMaS2.Name = "lblMaS2";
            this.lblMaS2.Size = new System.Drawing.Size(48, 17);
            this.lblMaS2.TabIndex = 122;
            this.lblMaS2.Text = "Mã S2";
            // 
            // lblMaS1
            // 
            this.lblMaS1.AccessibleDescription = "M_S1,S1";
            this.lblMaS1.AutoSize = true;
            this.lblMaS1.Location = new System.Drawing.Point(363, 7);
            this.lblMaS1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMaS1.Name = "lblMaS1";
            this.lblMaS1.Size = new System.Drawing.Size(48, 17);
            this.lblMaS1.TabIndex = 120;
            this.lblMaS1.Text = "Mã S1";
            // 
            // dateNgayS9
            // 
            this.dateNgayS9.AccessibleName = "S9";
            this.dateNgayS9.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgayS9.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgayS9.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayS9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayS9.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateNgayS9.GrayText = null;
            this.dateNgayS9.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayS9.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayS9.LeaveColor = System.Drawing.Color.White;
            this.dateNgayS9.Location = new System.Drawing.Point(523, 124);
            this.dateNgayS9.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayS9.Name = "dateNgayS9";
            this.dateNgayS9.Size = new System.Drawing.Size(200, 23);
            this.dateNgayS9.StringValue = "__/__/____";
            this.dateNgayS9.TabIndex = 131;
            this.dateNgayS9.Text = "__/__/____";
            // 
            // dateNgayS8
            // 
            this.dateNgayS8.AccessibleName = "S8";
            this.dateNgayS8.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgayS8.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgayS8.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayS8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayS8.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateNgayS8.GrayText = null;
            this.dateNgayS8.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayS8.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayS8.LeaveColor = System.Drawing.Color.White;
            this.dateNgayS8.Location = new System.Drawing.Point(523, 100);
            this.dateNgayS8.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayS8.Name = "dateNgayS8";
            this.dateNgayS8.Size = new System.Drawing.Size(200, 23);
            this.dateNgayS8.StringValue = "__/__/____";
            this.dateNgayS8.TabIndex = 129;
            this.dateNgayS8.Text = "__/__/____";
            // 
            // dateNgayS7
            // 
            this.dateNgayS7.AccessibleName = "S7";
            this.dateNgayS7.BackColor = System.Drawing.Color.White;
            this.dateNgayS7.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgayS7.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayS7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayS7.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateNgayS7.GrayText = null;
            this.dateNgayS7.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayS7.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayS7.LeaveColor = System.Drawing.Color.White;
            this.dateNgayS7.Location = new System.Drawing.Point(523, 76);
            this.dateNgayS7.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayS7.Name = "dateNgayS7";
            this.dateNgayS7.Size = new System.Drawing.Size(200, 23);
            this.dateNgayS7.StringValue = "__/__/____";
            this.dateNgayS7.TabIndex = 127;
            this.dateNgayS7.Text = "__/__/____";
            // 
            // txtGC_TD3
            // 
            this.txtGC_TD3.AccessibleName = "GC_TD3";
            this.txtGC_TD3.BackColor = System.Drawing.Color.White;
            this.txtGC_TD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD3.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD3.Location = new System.Drawing.Point(156, 312);
            this.txtGC_TD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD3.Name = "txtGC_TD3";
            this.txtGC_TD3.Size = new System.Drawing.Size(567, 23);
            this.txtGC_TD3.TabIndex = 113;
            // 
            // txtGC_TD2
            // 
            this.txtGC_TD2.AccessibleName = "GC_TD2";
            this.txtGC_TD2.BackColor = System.Drawing.Color.White;
            this.txtGC_TD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD2.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD2.Location = new System.Drawing.Point(156, 284);
            this.txtGC_TD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD2.Name = "txtGC_TD2";
            this.txtGC_TD2.Size = new System.Drawing.Size(567, 23);
            this.txtGC_TD2.TabIndex = 112;
            // 
            // txtGC_TD1
            // 
            this.txtGC_TD1.AccessibleName = "GC_TD1";
            this.txtGC_TD1.BackColor = System.Drawing.Color.White;
            this.txtGC_TD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_TD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_TD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_TD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_TD1.LeaveColor = System.Drawing.Color.White;
            this.txtGC_TD1.Location = new System.Drawing.Point(156, 256);
            this.txtGC_TD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_TD1.Name = "txtGC_TD1";
            this.txtGC_TD1.Size = new System.Drawing.Size(567, 23);
            this.txtGC_TD1.TabIndex = 111;
            // 
            // txtMA_TD3
            // 
            this.txtMA_TD3.AccessibleName = "MA_TD3";
            this.txtMA_TD3.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_TD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD3.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD3.Location = new System.Drawing.Point(156, 60);
            this.txtMA_TD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD3.Name = "txtMA_TD3";
            this.txtMA_TD3.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD3.TabIndex = 95;
            // 
            // txtMA_TD2
            // 
            this.txtMA_TD2.AccessibleName = "MA_TD2";
            this.txtMA_TD2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_TD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD2.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD2.Location = new System.Drawing.Point(156, 32);
            this.txtMA_TD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD2.Name = "txtMA_TD2";
            this.txtMA_TD2.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD2.TabIndex = 93;
            // 
            // txtMA_TD1
            // 
            this.txtMA_TD1.AccessibleName = "MA_TD1";
            this.txtMA_TD1.BackColor = System.Drawing.Color.White;
            this.txtMA_TD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_TD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_TD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_TD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_TD1.LeaveColor = System.Drawing.Color.White;
            this.txtMA_TD1.Location = new System.Drawing.Point(156, 4);
            this.txtMA_TD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_TD1.Name = "txtMA_TD1";
            this.txtMA_TD1.Size = new System.Drawing.Size(135, 23);
            this.txtMA_TD1.TabIndex = 91;
            // 
            // v6NumberTextBox3
            // 
            this.v6NumberTextBox3.AccessibleName = "sl_td3";
            this.v6NumberTextBox3.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox3.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox3.Location = new System.Drawing.Point(156, 228);
            this.v6NumberTextBox3.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox3.Name = "v6NumberTextBox3";
            this.v6NumberTextBox3.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox3.TabIndex = 107;
            this.v6NumberTextBox3.Text = "0,000";
            this.v6NumberTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6NumberTextBox2
            // 
            this.v6NumberTextBox2.AccessibleName = "sl_td2";
            this.v6NumberTextBox2.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox2.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox2.Location = new System.Drawing.Point(156, 200);
            this.v6NumberTextBox2.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox2.Name = "v6NumberTextBox2";
            this.v6NumberTextBox2.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox2.TabIndex = 105;
            this.v6NumberTextBox2.Text = "0,000";
            this.v6NumberTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6NumberTextBox1
            // 
            this.v6NumberTextBox1.AccessibleName = "sl_td1";
            this.v6NumberTextBox1.BackColor = System.Drawing.Color.White;
            this.v6NumberTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox1.Location = new System.Drawing.Point(156, 172);
            this.v6NumberTextBox1.Margin = new System.Windows.Forms.Padding(5);
            this.v6NumberTextBox1.Name = "v6NumberTextBox1";
            this.v6NumberTextBox1.Size = new System.Drawing.Size(135, 23);
            this.v6NumberTextBox1.TabIndex = 103;
            this.v6NumberTextBox1.Text = "0,000";
            this.v6NumberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6ColorDateTimePick3
            // 
            this.v6ColorDateTimePick3.AccessibleName = "ngay_td3";
            this.v6ColorDateTimePick3.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorDateTimePick3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick3.GrayText = null;
            this.v6ColorDateTimePick3.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick3.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick3.Location = new System.Drawing.Point(156, 144);
            this.v6ColorDateTimePick3.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick3.Name = "v6ColorDateTimePick3";
            this.v6ColorDateTimePick3.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick3.StringValue = "__/__/____";
            this.v6ColorDateTimePick3.TabIndex = 101;
            this.v6ColorDateTimePick3.Text = "__/__/____";
            // 
            // v6ColorDateTimePick2
            // 
            this.v6ColorDateTimePick2.AccessibleName = "ngay_td2";
            this.v6ColorDateTimePick2.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorDateTimePick2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick2.GrayText = null;
            this.v6ColorDateTimePick2.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick2.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick2.Location = new System.Drawing.Point(156, 116);
            this.v6ColorDateTimePick2.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick2.Name = "v6ColorDateTimePick2";
            this.v6ColorDateTimePick2.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick2.StringValue = "__/__/____";
            this.v6ColorDateTimePick2.TabIndex = 99;
            this.v6ColorDateTimePick2.Text = "__/__/____";
            // 
            // v6ColorDateTimePick1
            // 
            this.v6ColorDateTimePick1.AccessibleName = "ngay_td1";
            this.v6ColorDateTimePick1.BackColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorDateTimePick1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorDateTimePick1.GrayText = null;
            this.v6ColorDateTimePick1.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick1.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.Location = new System.Drawing.Point(156, 88);
            this.v6ColorDateTimePick1.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick1.Name = "v6ColorDateTimePick1";
            this.v6ColorDateTimePick1.Size = new System.Drawing.Size(135, 23);
            this.v6ColorDateTimePick1.StringValue = "__/__/____";
            this.v6ColorDateTimePick1.TabIndex = 97;
            this.v6ColorDateTimePick1.Text = "__/__/____";
            // 
            // label29
            // 
            this.label29.AccessibleDescription = "M_GC_TD3,GC_TD3";
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 315);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 17);
            this.label29.TabIndex = 110;
            this.label29.Text = "Ghi chú ĐN 3";
            // 
            // label28
            // 
            this.label28.AccessibleDescription = "M_GC_TD2,GC_TD2";
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 287);
            this.label28.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(93, 17);
            this.label28.TabIndex = 109;
            this.label28.Text = "Ghi chú ĐN 2";
            // 
            // label27
            // 
            this.label27.AccessibleDescription = "M_GC_TD1,GC_TD1";
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(5, 259);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 17);
            this.label27.TabIndex = 108;
            this.label27.Text = "Ghi chú ĐN 1";
            // 
            // label16
            // 
            this.label16.AccessibleDescription = "M_SL_TD3,SL_TD3";
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 231);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 17);
            this.label16.TabIndex = 106;
            this.label16.Text = "SL ĐN 3";
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "M_SL_TD2,SL_TD2";
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 203);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 17);
            this.label17.TabIndex = 104;
            this.label17.Text = "SL ĐN 2";
            // 
            // label22
            // 
            this.label22.AccessibleDescription = "M_SL_TD1,SL_TD1";
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 175);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 17);
            this.label22.TabIndex = 102;
            this.label22.Text = "SL ĐN 1";
            // 
            // label30
            // 
            this.label30.AccessibleDescription = "M_NGAY_TD3,NGAY_TD3";
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 147);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 17);
            this.label30.TabIndex = 100;
            this.label30.Text = "Ngày ĐN 3";
            // 
            // label31
            // 
            this.label31.AccessibleDescription = "M_NGAY_TD2,NGAY_TD2";
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(5, 119);
            this.label31.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 17);
            this.label31.TabIndex = 98;
            this.label31.Text = "Ngày ĐN 2";
            // 
            // label32
            // 
            this.label32.AccessibleDescription = "M_NGAY_TD1,NGAY_TD1";
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 91);
            this.label32.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 17);
            this.label32.TabIndex = 96;
            this.label32.Text = "Ngày ĐN 1";
            // 
            // label33
            // 
            this.label33.AccessibleDescription = "M_MA_TD3,MA_TD3";
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(5, 63);
            this.label33.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(63, 17);
            this.label33.TabIndex = 94;
            this.label33.Text = "Mã ĐN 3";
            // 
            // label34
            // 
            this.label34.AccessibleDescription = "M_MA_TD2,MA_TD2";
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 35);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(63, 17);
            this.label34.TabIndex = 92;
            this.label34.Text = "Mã ĐN 2";
            // 
            // label35
            // 
            this.label35.AccessibleDescription = "M_MA_TD1,MA_TD1";
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 7);
            this.label35.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(63, 17);
            this.label35.TabIndex = 90;
            this.label35.Text = "Mã ĐN 1";
            // 
            // NgoaiTeAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6TabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NgoaiTeAddEditForm";
            this.Size = new System.Drawing.Size(763, 410);
            this.v6TabControl1.ResumeLayout(false);
            this.tabThongTinChinh.ResumeLayout(false);
            this.tabThongTinChinh.PerformLayout();
            this.tabTuDinhNghia.ResumeLayout(false);
            this.tabTuDinhNghia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private V6ColorTextBox v6ColorTextBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private V6ColorTextBox TxtTen_nt;
        private System.Windows.Forms.Label label2;
        private V6ColorTextBox TxtMa_nt;
        private V6ColorTextBox begin1;
        private System.Windows.Forms.Label label3;
        private V6ColorTextBox endpoint1;
        private V6ColorTextBox end1;
        private V6ColorTextBox point1;
        private V6ColorTextBox only2;
        private V6ColorTextBox endpoint2;
        private V6ColorTextBox only1;
        private V6ColorTextBox end2;
        private V6ColorTextBox point2;
        private V6ColorTextBox begin2;
        private System.Windows.Forms.Label lblDocSoTien;
        private System.Windows.Forms.Label label6;
        private V6NumberTextBox number;
        private System.Windows.Forms.Label label7;
        private V6TabControl v6TabControl1;
        private System.Windows.Forms.TabPage tabThongTinChinh;
        private System.Windows.Forms.TabPage tabTuDinhNghia;
        private V6ColorTextBox txtMaS3;
        private V6ColorTextBox txtMaS2;
        private V6ColorTextBox txtMaS1;
        private V6NumberTextBox txtSLS6;
        private V6NumberTextBox txtSLS5;
        private V6NumberTextBox txtSLS4;
        private System.Windows.Forms.Label lblSLS6;
        private System.Windows.Forms.Label lblSLS5;
        private System.Windows.Forms.Label lblSLS4;
        private System.Windows.Forms.Label lblNgayS9;
        private System.Windows.Forms.Label lblNgayS8;
        private System.Windows.Forms.Label lblNgayS7;
        private System.Windows.Forms.Label lblMaS3;
        private System.Windows.Forms.Label lblMaS2;
        private System.Windows.Forms.Label lblMaS1;
        private V6DateTimeColor dateNgayS9;
        private V6DateTimeColor dateNgayS8;
        private V6DateTimeColor dateNgayS7;
        private V6VvarTextBox txtGC_TD3;
        private V6VvarTextBox txtGC_TD2;
        private V6VvarTextBox txtGC_TD1;
        private V6VvarTextBox txtMA_TD3;
        private V6VvarTextBox txtMA_TD2;
        private V6VvarTextBox txtMA_TD1;
        private V6NumberTextBox v6NumberTextBox3;
        private V6NumberTextBox v6NumberTextBox2;
        private V6NumberTextBox v6NumberTextBox1;
        private V6DateTimeColor v6ColorDateTimePick3;
        private V6DateTimeColor v6ColorDateTimePick2;
        private V6DateTimeColor v6ColorDateTimePick1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
    }
}
