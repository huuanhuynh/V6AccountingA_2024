namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class ThongTinBaoHiem
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
            this.buttonSua = new System.Windows.Forms.Button();
            this.buttonHuy = new System.Windows.Forms.Button();
            this.buttonNhan = new System.Windows.Forms.Button();
            this.txtSttRec = new V6Controls.V6ColorTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNGAY_BD_BH = new V6Controls.V6DateTimeColor();
            this.label1 = new System.Windows.Forms.Label();
            this.chkbhxh_yn = new V6Controls.V6CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkbhyt_yn = new V6Controls.V6CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkbhtn_yn = new V6Controls.V6CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSI_NO = new V6Controls.V6VvarTextBox();
            this.txtSI_DATE = new V6Controls.V6DateTimeColor();
            this.txtSI_DATE2 = new V6Controls.V6DateTimeColor();
            this.txtHI_NO = new V6Controls.V6VvarTextBox();
            this.txtNGAY_BD_YT = new V6Controls.V6DateTimeColor();
            this.txtHI_DATE = new V6Controls.V6DateTimeColor();
            this.txtHI_DATE2 = new V6Controls.V6DateTimeColor();
            this.txtNOI_KHAM = new V6Controls.V6VvarTextBox();
            this.txtDT_BHYT = new V6Controls.V6VvarTextBox();
            this.txtngay_bd_tn = new V6Controls.V6DateTimeColor();
            this.txtngay_kt_tn = new V6Controls.V6DateTimeColor();
            this.SuspendLayout();
            // 
            // buttonSua
            // 
            this.buttonSua.AccessibleDescription = "REPORTB00004";
            this.buttonSua.AccessibleName = "";
            this.buttonSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSua.Image = global::V6Controls.Properties.Resources.EditPage;
            this.buttonSua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSua.Location = new System.Drawing.Point(11, 436);
            this.buttonSua.Name = "buttonSua";
            this.buttonSua.Size = new System.Drawing.Size(88, 40);
            this.buttonSua.TabIndex = 16;
            this.buttonSua.Tag = "Return, Control";
            this.buttonSua.Text = "&Sửa";
            this.buttonSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSua.UseVisualStyleBackColor = true;
            this.buttonSua.Click += new System.EventHandler(this.buttonSua_Click_1);
            // 
            // buttonHuy
            // 
            this.buttonHuy.AccessibleDescription = "REPORTB00005";
            this.buttonHuy.AccessibleName = "";
            this.buttonHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonHuy.Image = global::V6Controls.Properties.Resources.Cancel;
            this.buttonHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonHuy.Location = new System.Drawing.Point(199, 436);
            this.buttonHuy.Name = "buttonHuy";
            this.buttonHuy.Size = new System.Drawing.Size(88, 40);
            this.buttonHuy.TabIndex = 18;
            this.buttonHuy.Tag = "Escape";
            this.buttonHuy.Text = "&Hủy";
            this.buttonHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonHuy.UseVisualStyleBackColor = true;
            this.buttonHuy.Click += new System.EventHandler(this.buttonHuy_Click_1);
            // 
            // buttonNhan
            // 
            this.buttonNhan.AccessibleDescription = "REPORTB00004";
            this.buttonNhan.AccessibleName = "";
            this.buttonNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.buttonNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNhan.Location = new System.Drawing.Point(105, 436);
            this.buttonNhan.Name = "buttonNhan";
            this.buttonNhan.Size = new System.Drawing.Size(88, 40);
            this.buttonNhan.TabIndex = 17;
            this.buttonNhan.Tag = "Return, Control";
            this.buttonNhan.Text = "&Nhận";
            this.buttonNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNhan.UseVisualStyleBackColor = true;
            this.buttonNhan.Click += new System.EventHandler(this.buttonNhan_Click_1);
            // 
            // txtSttRec
            // 
            this.txtSttRec.AccessibleName = "STT_REC";
            this.txtSttRec.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSttRec.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSttRec.Enabled = false;
            this.txtSttRec.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSttRec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.GrayText = "STT_REC";
            this.txtSttRec.HoverColor = System.Drawing.Color.Yellow;
            this.txtSttRec.LeaveColor = System.Drawing.Color.White;
            this.txtSttRec.Location = new System.Drawing.Point(512, 5);
            this.txtSttRec.Margin = new System.Windows.Forms.Padding(5);
            this.txtSttRec.Name = "txtSttRec";
            this.txtSttRec.Size = new System.Drawing.Size(109, 23);
            this.txtSttRec.TabIndex = 127;
            this.txtSttRec.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 55);
            this.label18.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 17);
            this.label18.TabIndex = 167;
            this.label18.Text = "Ngày cấp sổ BHXH";
            // 
            // txtNGAY_BD_BH
            // 
            this.txtNGAY_BD_BH.AccessibleDescription = "";
            this.txtNGAY_BD_BH.AccessibleName = "NGAY_BD_BH";
            this.txtNGAY_BD_BH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNGAY_BD_BH.BackColor = System.Drawing.Color.White;
            this.txtNGAY_BD_BH.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNGAY_BD_BH.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNGAY_BD_BH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_BD_BH.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_BD_BH.GrayText = null;
            this.txtNGAY_BD_BH.HoverColor = System.Drawing.Color.Yellow;
            this.txtNGAY_BD_BH.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNGAY_BD_BH.LeaveColor = System.Drawing.Color.White;
            this.txtNGAY_BD_BH.Location = new System.Drawing.Point(164, 52);
            this.txtNGAY_BD_BH.Margin = new System.Windows.Forms.Padding(5);
            this.txtNGAY_BD_BH.Name = "txtNGAY_BD_BH";
            this.txtNGAY_BD_BH.Size = new System.Drawing.Size(134, 23);
            this.txtNGAY_BD_BH.StringValue = "__/__/____";
            this.txtNGAY_BD_BH.TabIndex = 2;
            this.txtNGAY_BD_BH.Text = "__/__/____";
            this.txtNGAY_BD_BH.TextChanged += new System.EventHandler(this.txtworkdate2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 168;
            this.label1.Text = "Số sổ BHXH";
            // 
            // chkbhxh_yn
            // 
            this.chkbhxh_yn.AccessibleName = "bhxh_yn";
            this.chkbhxh_yn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbhxh_yn.AutoSize = true;
            this.chkbhxh_yn.Location = new System.Drawing.Point(164, 0);
            this.chkbhxh_yn.Margin = new System.Windows.Forms.Padding(5);
            this.chkbhxh_yn.Name = "chkbhxh_yn";
            this.chkbhxh_yn.Size = new System.Drawing.Size(103, 21);
            this.chkbhxh_yn.TabIndex = 0;
            this.chkbhxh_yn.Text = "Đóng BHXH";
            this.chkbhxh_yn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 17);
            this.label2.TabIndex = 170;
            this.label2.Text = "Ngày tham gia BHXH";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 171;
            this.label3.Text = "Ngày nghỉ BHXH";
            // 
            // chkbhyt_yn
            // 
            this.chkbhyt_yn.AccessibleName = "bhyt_yn";
            this.chkbhyt_yn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbhyt_yn.AutoSize = true;
            this.chkbhyt_yn.Location = new System.Drawing.Point(164, 141);
            this.chkbhyt_yn.Margin = new System.Windows.Forms.Padding(5);
            this.chkbhyt_yn.Name = "chkbhyt_yn";
            this.chkbhyt_yn.Size = new System.Drawing.Size(102, 21);
            this.chkbhyt_yn.TabIndex = 5;
            this.chkbhyt_yn.Text = "Đóng BHYT";
            this.chkbhyt_yn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 173;
            this.label4.Text = "Số BHYT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 198);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 174;
            this.label5.Text = "Ngày cấp sổ BHYT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 226);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 17);
            this.label6.TabIndex = 175;
            this.label6.Text = "Ngày tham gia BHYT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 254);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 17);
            this.label7.TabIndex = 176;
            this.label7.Text = "Ngày hết hạn BHYT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 282);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 17);
            this.label8.TabIndex = 177;
            this.label8.Text = "Nơi đăng ký KCB";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 310);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 17);
            this.label9.TabIndex = 178;
            this.label9.Text = "Đối tượng hưởng BHYT";
            // 
            // chkbhtn_yn
            // 
            this.chkbhtn_yn.AccessibleName = "bhtn_yn";
            this.chkbhtn_yn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbhtn_yn.AutoSize = true;
            this.chkbhtn_yn.Location = new System.Drawing.Point(164, 343);
            this.chkbhtn_yn.Margin = new System.Windows.Forms.Padding(5);
            this.chkbhtn_yn.Name = "chkbhtn_yn";
            this.chkbhtn_yn.Size = new System.Drawing.Size(103, 21);
            this.chkbhtn_yn.TabIndex = 12;
            this.chkbhtn_yn.Text = "Đóng BHTN";
            this.chkbhtn_yn.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 374);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 17);
            this.label10.TabIndex = 180;
            this.label10.Text = "Ngày tham gia BHTN";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 399);
            this.label11.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 17);
            this.label11.TabIndex = 181;
            this.label11.Text = "Ngày kết thúc BHTN";
            // 
            // txtSI_NO
            // 
            this.txtSI_NO.AccessibleDescription = "";
            this.txtSI_NO.AccessibleName = "SI_NO";
            this.txtSI_NO.BackColor = System.Drawing.SystemColors.Window;
            this.txtSI_NO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSI_NO.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSI_NO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSI_NO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSI_NO.HoverColor = System.Drawing.Color.Yellow;
            this.txtSI_NO.LeaveColor = System.Drawing.Color.White;
            this.txtSI_NO.Location = new System.Drawing.Point(164, 25);
            this.txtSI_NO.Name = "txtSI_NO";
            this.txtSI_NO.Size = new System.Drawing.Size(134, 23);
            this.txtSI_NO.TabIndex = 1;
            // 
            // txtSI_DATE
            // 
            this.txtSI_DATE.AccessibleDescription = "";
            this.txtSI_DATE.AccessibleName = "SI_DATE";
            this.txtSI_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSI_DATE.BackColor = System.Drawing.Color.White;
            this.txtSI_DATE.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSI_DATE.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSI_DATE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSI_DATE.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSI_DATE.GrayText = null;
            this.txtSI_DATE.HoverColor = System.Drawing.Color.Yellow;
            this.txtSI_DATE.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSI_DATE.LeaveColor = System.Drawing.Color.White;
            this.txtSI_DATE.Location = new System.Drawing.Point(164, 79);
            this.txtSI_DATE.Margin = new System.Windows.Forms.Padding(5);
            this.txtSI_DATE.Name = "txtSI_DATE";
            this.txtSI_DATE.Size = new System.Drawing.Size(134, 23);
            this.txtSI_DATE.StringValue = "__/__/____";
            this.txtSI_DATE.TabIndex = 3;
            this.txtSI_DATE.Text = "__/__/____";
            // 
            // txtSI_DATE2
            // 
            this.txtSI_DATE2.AccessibleName = "SI_DATE2";
            this.txtSI_DATE2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSI_DATE2.BackColor = System.Drawing.Color.White;
            this.txtSI_DATE2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSI_DATE2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSI_DATE2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSI_DATE2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSI_DATE2.GrayText = null;
            this.txtSI_DATE2.HoverColor = System.Drawing.Color.Yellow;
            this.txtSI_DATE2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSI_DATE2.LeaveColor = System.Drawing.Color.White;
            this.txtSI_DATE2.Location = new System.Drawing.Point(164, 106);
            this.txtSI_DATE2.Margin = new System.Windows.Forms.Padding(5);
            this.txtSI_DATE2.Name = "txtSI_DATE2";
            this.txtSI_DATE2.Size = new System.Drawing.Size(134, 23);
            this.txtSI_DATE2.StringValue = "__/__/____";
            this.txtSI_DATE2.TabIndex = 4;
            this.txtSI_DATE2.Text = "__/__/____";
            // 
            // txtHI_NO
            // 
            this.txtHI_NO.AccessibleDescription = "";
            this.txtHI_NO.AccessibleName = "HI_NO";
            this.txtHI_NO.BackColor = System.Drawing.SystemColors.Window;
            this.txtHI_NO.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtHI_NO.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtHI_NO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHI_NO.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtHI_NO.HoverColor = System.Drawing.Color.Yellow;
            this.txtHI_NO.LeaveColor = System.Drawing.Color.White;
            this.txtHI_NO.Location = new System.Drawing.Point(164, 167);
            this.txtHI_NO.Name = "txtHI_NO";
            this.txtHI_NO.Size = new System.Drawing.Size(134, 23);
            this.txtHI_NO.TabIndex = 6;
            // 
            // txtNGAY_BD_YT
            // 
            this.txtNGAY_BD_YT.AccessibleDescription = "";
            this.txtNGAY_BD_YT.AccessibleName = "NGAY_BD_YT";
            this.txtNGAY_BD_YT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNGAY_BD_YT.BackColor = System.Drawing.Color.White;
            this.txtNGAY_BD_YT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNGAY_BD_YT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNGAY_BD_YT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_BD_YT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_BD_YT.GrayText = null;
            this.txtNGAY_BD_YT.HoverColor = System.Drawing.Color.Yellow;
            this.txtNGAY_BD_YT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNGAY_BD_YT.LeaveColor = System.Drawing.Color.White;
            this.txtNGAY_BD_YT.Location = new System.Drawing.Point(164, 195);
            this.txtNGAY_BD_YT.Margin = new System.Windows.Forms.Padding(5);
            this.txtNGAY_BD_YT.Name = "txtNGAY_BD_YT";
            this.txtNGAY_BD_YT.Size = new System.Drawing.Size(134, 23);
            this.txtNGAY_BD_YT.StringValue = "__/__/____";
            this.txtNGAY_BD_YT.TabIndex = 7;
            this.txtNGAY_BD_YT.Text = "__/__/____";
            // 
            // txtHI_DATE
            // 
            this.txtHI_DATE.AccessibleDescription = "";
            this.txtHI_DATE.AccessibleName = "HI_DATE";
            this.txtHI_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHI_DATE.BackColor = System.Drawing.Color.White;
            this.txtHI_DATE.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtHI_DATE.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtHI_DATE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHI_DATE.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtHI_DATE.GrayText = null;
            this.txtHI_DATE.HoverColor = System.Drawing.Color.Yellow;
            this.txtHI_DATE.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHI_DATE.LeaveColor = System.Drawing.Color.White;
            this.txtHI_DATE.Location = new System.Drawing.Point(164, 223);
            this.txtHI_DATE.Margin = new System.Windows.Forms.Padding(5);
            this.txtHI_DATE.Name = "txtHI_DATE";
            this.txtHI_DATE.Size = new System.Drawing.Size(134, 23);
            this.txtHI_DATE.StringValue = "__/__/____";
            this.txtHI_DATE.TabIndex = 8;
            this.txtHI_DATE.Text = "__/__/____";
            // 
            // txtHI_DATE2
            // 
            this.txtHI_DATE2.AccessibleDescription = "";
            this.txtHI_DATE2.AccessibleName = "HI_DATE2";
            this.txtHI_DATE2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHI_DATE2.BackColor = System.Drawing.Color.White;
            this.txtHI_DATE2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtHI_DATE2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtHI_DATE2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHI_DATE2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtHI_DATE2.GrayText = null;
            this.txtHI_DATE2.HoverColor = System.Drawing.Color.Yellow;
            this.txtHI_DATE2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHI_DATE2.LeaveColor = System.Drawing.Color.White;
            this.txtHI_DATE2.Location = new System.Drawing.Point(164, 251);
            this.txtHI_DATE2.Margin = new System.Windows.Forms.Padding(5);
            this.txtHI_DATE2.Name = "txtHI_DATE2";
            this.txtHI_DATE2.Size = new System.Drawing.Size(134, 23);
            this.txtHI_DATE2.StringValue = "__/__/____";
            this.txtHI_DATE2.TabIndex = 9;
            this.txtHI_DATE2.Text = "__/__/____";
            // 
            // txtNOI_KHAM
            // 
            this.txtNOI_KHAM.AccessibleName = "NOI_KHAM";
            this.txtNOI_KHAM.BackColor = System.Drawing.SystemColors.Window;
            this.txtNOI_KHAM.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNOI_KHAM.BrotherFields = "TEN_NOI_KHAM";
            this.txtNOI_KHAM.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNOI_KHAM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNOI_KHAM.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNOI_KHAM.HoverColor = System.Drawing.Color.Yellow;
            this.txtNOI_KHAM.LeaveColor = System.Drawing.Color.White;
            this.txtNOI_KHAM.Location = new System.Drawing.Point(164, 279);
            this.txtNOI_KHAM.Name = "txtNOI_KHAM";
            this.txtNOI_KHAM.Size = new System.Drawing.Size(134, 23);
            this.txtNOI_KHAM.TabIndex = 10;
            // 
            // txtDT_BHYT
            // 
            this.txtDT_BHYT.AccessibleName = "DT_BHYT";
            this.txtDT_BHYT.BackColor = System.Drawing.SystemColors.Window;
            this.txtDT_BHYT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDT_BHYT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDT_BHYT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDT_BHYT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDT_BHYT.HoverColor = System.Drawing.Color.Yellow;
            this.txtDT_BHYT.LeaveColor = System.Drawing.Color.White;
            this.txtDT_BHYT.Location = new System.Drawing.Point(164, 307);
            this.txtDT_BHYT.Name = "txtDT_BHYT";
            this.txtDT_BHYT.Size = new System.Drawing.Size(282, 23);
            this.txtDT_BHYT.TabIndex = 11;
            // 
            // txtngay_bd_tn
            // 
            this.txtngay_bd_tn.AccessibleDescription = "";
            this.txtngay_bd_tn.AccessibleName = "ngay_bd_tn";
            this.txtngay_bd_tn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtngay_bd_tn.BackColor = System.Drawing.Color.White;
            this.txtngay_bd_tn.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtngay_bd_tn.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_bd_tn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtngay_bd_tn.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtngay_bd_tn.GrayText = null;
            this.txtngay_bd_tn.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_bd_tn.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_bd_tn.LeaveColor = System.Drawing.Color.White;
            this.txtngay_bd_tn.Location = new System.Drawing.Point(164, 370);
            this.txtngay_bd_tn.Margin = new System.Windows.Forms.Padding(5);
            this.txtngay_bd_tn.Name = "txtngay_bd_tn";
            this.txtngay_bd_tn.Size = new System.Drawing.Size(134, 23);
            this.txtngay_bd_tn.StringValue = "__/__/____";
            this.txtngay_bd_tn.TabIndex = 13;
            this.txtngay_bd_tn.Text = "__/__/____";
            // 
            // txtngay_kt_tn
            // 
            this.txtngay_kt_tn.AccessibleDescription = "";
            this.txtngay_kt_tn.AccessibleName = "ngay_kt_tn";
            this.txtngay_kt_tn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtngay_kt_tn.BackColor = System.Drawing.Color.White;
            this.txtngay_kt_tn.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtngay_kt_tn.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtngay_kt_tn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtngay_kt_tn.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtngay_kt_tn.GrayText = null;
            this.txtngay_kt_tn.HoverColor = System.Drawing.Color.Yellow;
            this.txtngay_kt_tn.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtngay_kt_tn.LeaveColor = System.Drawing.Color.White;
            this.txtngay_kt_tn.Location = new System.Drawing.Point(164, 397);
            this.txtngay_kt_tn.Margin = new System.Windows.Forms.Padding(5);
            this.txtngay_kt_tn.Name = "txtngay_kt_tn";
            this.txtngay_kt_tn.Size = new System.Drawing.Size(134, 23);
            this.txtngay_kt_tn.StringValue = "__/__/____";
            this.txtngay_kt_tn.TabIndex = 14;
            this.txtngay_kt_tn.Text = "__/__/____";
            // 
            // ThongTinBaoHiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtngay_kt_tn);
            this.Controls.Add(this.txtngay_bd_tn);
            this.Controls.Add(this.txtDT_BHYT);
            this.Controls.Add(this.txtNOI_KHAM);
            this.Controls.Add(this.txtHI_DATE2);
            this.Controls.Add(this.txtHI_DATE);
            this.Controls.Add(this.txtNGAY_BD_YT);
            this.Controls.Add(this.txtHI_NO);
            this.Controls.Add(this.txtSI_DATE2);
            this.Controls.Add(this.txtSI_DATE);
            this.Controls.Add(this.txtSI_NO);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkbhtn_yn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkbhyt_yn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkbhxh_yn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNGAY_BD_BH);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.buttonSua);
            this.Controls.Add(this.buttonHuy);
            this.Controls.Add(this.buttonNhan);
            this.Controls.Add(this.txtSttRec);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongTinBaoHiem";
            this.Size = new System.Drawing.Size(670, 487);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Button buttonSua;
        protected System.Windows.Forms.Button buttonHuy;
        protected System.Windows.Forms.Button buttonNhan;
        private V6ColorTextBox txtSttRec;
        private System.Windows.Forms.Label label18;
        private V6DateTimeColor txtNGAY_BD_BH;
        private System.Windows.Forms.Label label1;
        private V6CheckBox chkbhxh_yn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private V6CheckBox chkbhyt_yn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private V6CheckBox chkbhtn_yn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private V6VvarTextBox txtSI_NO;
        private V6DateTimeColor txtSI_DATE;
        private V6DateTimeColor txtSI_DATE2;
        private V6VvarTextBox txtHI_NO;
        private V6DateTimeColor txtNGAY_BD_YT;
        private V6DateTimeColor txtHI_DATE;
        private V6DateTimeColor txtHI_DATE2;
        private V6VvarTextBox txtNOI_KHAM;
        private V6VvarTextBox txtDT_BHYT;
        private V6DateTimeColor txtngay_bd_tn;
        private V6DateTimeColor txtngay_kt_tn;
    }
}
