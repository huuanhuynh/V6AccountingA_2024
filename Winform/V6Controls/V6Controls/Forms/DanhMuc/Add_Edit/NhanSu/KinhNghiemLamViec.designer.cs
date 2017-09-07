namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class KinhNghiemLamViec
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
            this.txtQuocTich = new V6Controls.V6LookupTextBox();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.txtecompany = new V6Controls.V6ColorTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.txteproject = new V6Controls.V6ColorTextBox();
            this.txteposition = new V6Controls.V6ColorTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.txtework = new V6Controls.V6ColorTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.v6Label10 = new V6Controls.V6Label();
            this.txtremarks = new V6Controls.V6ColorTextBox();
            this.txtSttRec0 = new V6Controls.V6ColorTextBox();
            this.txtdate_to = new V6Controls.V6DateTimeColor();
            this.txtdate_from = new V6Controls.V6DateTimeColor();
            this.v6Label5 = new V6Controls.V6Label();
            this.txtperiod = new V6Controls.V6ColorTextBox();
            this.v6Label6 = new V6Controls.V6Label();
            this.txttype = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // buttonSua
            // 
            this.buttonSua.AccessibleDescription = "REPORTB00004";
            this.buttonSua.AccessibleName = "";
            this.buttonSua.Image = global::V6Controls.Properties.Resources.EditPage;
            this.buttonSua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSua.Location = new System.Drawing.Point(20, 320);
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
            this.buttonHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonHuy.Image = global::V6Controls.Properties.Resources.Cancel;
            this.buttonHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonHuy.Location = new System.Drawing.Point(208, 320);
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
            this.buttonNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.buttonNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNhan.Location = new System.Drawing.Point(114, 320);
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
            this.txtSttRec.Location = new System.Drawing.Point(381, 5);
            this.txtSttRec.Margin = new System.Windows.Forms.Padding(5);
            this.txtSttRec.Name = "txtSttRec";
            this.txtSttRec.Size = new System.Drawing.Size(109, 23);
            this.txtSttRec.TabIndex = 127;
            this.txtSttRec.Visible = false;
            // 
            // txtQuocTich
            // 
            this.txtQuocTich.AccessibleName = "ORGUNIT_ID";
            this.txtQuocTich.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuocTich.BackColor = System.Drawing.Color.White;
            this.txtQuocTich.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtQuocTich.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtQuocTich.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtQuocTich.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtQuocTich.HoverColor = System.Drawing.Color.Yellow;
            this.txtQuocTich.LeaveColor = System.Drawing.Color.White;
            this.txtQuocTich.Location = new System.Drawing.Point(122, 8);
            this.txtQuocTich.Ma_dm = "HRLSTORGUNIT";
            this.txtQuocTich.Margin = new System.Windows.Forms.Padding(5);
            this.txtQuocTich.Name = "txtQuocTich";
            this.txtQuocTich.NeighborFields = "";
            this.txtQuocTich.ParentData = null;
            this.txtQuocTich.ShowTextField = "NAME";
            this.txtQuocTich.Size = new System.Drawing.Size(185, 23);
            this.txtQuocTich.TabIndex = 128;
            this.txtQuocTich.ValueField = "ID";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(17, 11);
            this.v6Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(83, 17);
            this.v6Label3.TabIndex = 129;
            this.v6Label3.Text = "Mã bộ phận";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(17, 42);
            this.v6Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(56, 17);
            this.v6Label1.TabIndex = 130;
            this.v6Label1.Text = "Công ty";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "";
            this.v6Label2.AccessibleName = "";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(17, 73);
            this.v6Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(46, 17);
            this.v6Label2.TabIndex = 132;
            this.v6Label2.Text = "Dự án";
            // 
            // txtecompany
            // 
            this.txtecompany.AccessibleName = "ecompany";
            this.txtecompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtecompany.BackColor = System.Drawing.SystemColors.Window;
            this.txtecompany.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtecompany.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtecompany.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtecompany.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtecompany.HoverColor = System.Drawing.Color.Yellow;
            this.txtecompany.LeaveColor = System.Drawing.Color.White;
            this.txtecompany.Location = new System.Drawing.Point(122, 39);
            this.txtecompany.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtecompany.Name = "txtecompany";
            this.txtecompany.Size = new System.Drawing.Size(185, 23);
            this.txtecompany.TabIndex = 133;
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(17, 104);
            this.v6Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(59, 17);
            this.v6Label4.TabIndex = 134;
            this.v6Label4.Text = "Chức vụ";
            // 
            // txteproject
            // 
            this.txteproject.AccessibleName = "eproject";
            this.txteproject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txteproject.BackColor = System.Drawing.SystemColors.Window;
            this.txteproject.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txteproject.EnterColor = System.Drawing.Color.PaleGreen;
            this.txteproject.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txteproject.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txteproject.HoverColor = System.Drawing.Color.Yellow;
            this.txteproject.LeaveColor = System.Drawing.Color.White;
            this.txteproject.Location = new System.Drawing.Point(122, 70);
            this.txteproject.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txteproject.Name = "txteproject";
            this.txteproject.Size = new System.Drawing.Size(185, 23);
            this.txteproject.TabIndex = 135;
            // 
            // txteposition
            // 
            this.txteposition.AccessibleName = "eposition";
            this.txteposition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txteposition.BackColor = System.Drawing.SystemColors.Window;
            this.txteposition.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txteposition.EnterColor = System.Drawing.Color.PaleGreen;
            this.txteposition.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txteposition.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txteposition.HoverColor = System.Drawing.Color.Yellow;
            this.txteposition.LeaveColor = System.Drawing.Color.White;
            this.txteposition.Location = new System.Drawing.Point(122, 101);
            this.txteposition.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txteposition.Name = "txteposition";
            this.txteposition.Size = new System.Drawing.Size(185, 23);
            this.txteposition.TabIndex = 139;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(17, 135);
            this.v6Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(70, 17);
            this.v6Label7.TabIndex = 140;
            this.v6Label7.Text = "Công việc";
            // 
            // txtework
            // 
            this.txtework.AccessibleName = "ework";
            this.txtework.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtework.BackColor = System.Drawing.SystemColors.Window;
            this.txtework.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtework.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtework.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtework.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtework.HoverColor = System.Drawing.Color.Yellow;
            this.txtework.LeaveColor = System.Drawing.Color.White;
            this.txtework.Location = new System.Drawing.Point(122, 132);
            this.txtework.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtework.Name = "txtework";
            this.txtework.Size = new System.Drawing.Size(185, 23);
            this.txtework.TabIndex = 141;
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "";
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(17, 197);
            this.v6Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(60, 17);
            this.v6Label8.TabIndex = 142;
            this.v6Label8.Text = "Từ ngày";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(17, 228);
            this.v6Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(69, 17);
            this.v6Label9.TabIndex = 143;
            this.v6Label9.Text = "Đến ngày";
            // 
            // v6Label10
            // 
            this.v6Label10.AccessibleDescription = "";
            this.v6Label10.AutoSize = true;
            this.v6Label10.Location = new System.Drawing.Point(15, 166);
            this.v6Label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label10.Name = "v6Label10";
            this.v6Label10.Size = new System.Drawing.Size(57, 17);
            this.v6Label10.TabIndex = 146;
            this.v6Label10.Text = "Ghi chú";
            // 
            // txtremarks
            // 
            this.txtremarks.AccessibleName = "remarks";
            this.txtremarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtremarks.BackColor = System.Drawing.SystemColors.Window;
            this.txtremarks.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtremarks.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtremarks.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtremarks.HoverColor = System.Drawing.Color.Yellow;
            this.txtremarks.LeaveColor = System.Drawing.Color.White;
            this.txtremarks.Location = new System.Drawing.Point(120, 163);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(333, 23);
            this.txtremarks.TabIndex = 147;
            // 
            // txtSttRec0
            // 
            this.txtSttRec0.AccessibleName = "STT_REC0";
            this.txtSttRec0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSttRec0.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSttRec0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSttRec0.Enabled = false;
            this.txtSttRec0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSttRec0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSttRec0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSttRec0.GrayText = "STT_REC0";
            this.txtSttRec0.HoverColor = System.Drawing.Color.Yellow;
            this.txtSttRec0.LeaveColor = System.Drawing.Color.White;
            this.txtSttRec0.Location = new System.Drawing.Point(381, 38);
            this.txtSttRec0.Margin = new System.Windows.Forms.Padding(5);
            this.txtSttRec0.Name = "txtSttRec0";
            this.txtSttRec0.Size = new System.Drawing.Size(100, 23);
            this.txtSttRec0.TabIndex = 150;
            this.txtSttRec0.Visible = false;
            // 
            // txtdate_to
            // 
            this.txtdate_to.AccessibleName = "date_to";
            this.txtdate_to.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtdate_to.BackColor = System.Drawing.Color.White;
            this.txtdate_to.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdate_to.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdate_to.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdate_to.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdate_to.GrayText = null;
            this.txtdate_to.HoverColor = System.Drawing.Color.Yellow;
            this.txtdate_to.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtdate_to.LeaveColor = System.Drawing.Color.White;
            this.txtdate_to.Location = new System.Drawing.Point(120, 225);
            this.txtdate_to.Margin = new System.Windows.Forms.Padding(5);
            this.txtdate_to.Name = "txtdate_to";
            this.txtdate_to.Size = new System.Drawing.Size(134, 23);
            this.txtdate_to.StringValue = "__/__/____";
            this.txtdate_to.TabIndex = 152;
            this.txtdate_to.Text = "__/__/____";
            // 
            // txtdate_from
            // 
            this.txtdate_from.AccessibleName = "date_from";
            this.txtdate_from.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtdate_from.BackColor = System.Drawing.Color.White;
            this.txtdate_from.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtdate_from.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtdate_from.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdate_from.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtdate_from.GrayText = null;
            this.txtdate_from.HoverColor = System.Drawing.Color.Yellow;
            this.txtdate_from.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtdate_from.LeaveColor = System.Drawing.Color.White;
            this.txtdate_from.Location = new System.Drawing.Point(120, 194);
            this.txtdate_from.Margin = new System.Windows.Forms.Padding(5);
            this.txtdate_from.Name = "txtdate_from";
            this.txtdate_from.Size = new System.Drawing.Size(134, 23);
            this.txtdate_from.StringValue = "__/__/____";
            this.txtdate_from.TabIndex = 151;
            this.txtdate_from.Text = "__/__/____";
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(17, 259);
            this.v6Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(69, 17);
            this.v6Label5.TabIndex = 153;
            this.v6Label5.Text = "Giai đoạn";
            // 
            // txtperiod
            // 
            this.txtperiod.AccessibleName = "period";
            this.txtperiod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtperiod.BackColor = System.Drawing.SystemColors.Window;
            this.txtperiod.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtperiod.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtperiod.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtperiod.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtperiod.HoverColor = System.Drawing.Color.Yellow;
            this.txtperiod.LeaveColor = System.Drawing.Color.White;
            this.txtperiod.Location = new System.Drawing.Point(120, 256);
            this.txtperiod.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtperiod.Name = "txtperiod";
            this.txtperiod.Size = new System.Drawing.Size(185, 23);
            this.txtperiod.TabIndex = 154;
            // 
            // v6Label6
            // 
            this.v6Label6.AccessibleDescription = "";
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(17, 290);
            this.v6Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(35, 17);
            this.v6Label6.TabIndex = 155;
            this.v6Label6.Text = "Loại";
            // 
            // txttype
            // 
            this.txttype.AccessibleName = "type";
            this.txttype.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txttype.BackColor = System.Drawing.SystemColors.Window;
            this.txttype.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txttype.EnterColor = System.Drawing.Color.PaleGreen;
            this.txttype.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txttype.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txttype.HoverColor = System.Drawing.Color.Yellow;
            this.txttype.LeaveColor = System.Drawing.Color.White;
            this.txttype.Location = new System.Drawing.Point(120, 287);
            this.txttype.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txttype.Name = "txttype";
            this.txttype.Size = new System.Drawing.Size(185, 23);
            this.txttype.TabIndex = 156;
            // 
            // KinhNghiemLamViec2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txttype);
            this.Controls.Add(this.v6Label6);
            this.Controls.Add(this.txtperiod);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.txtdate_to);
            this.Controls.Add(this.txtdate_from);
            this.Controls.Add(this.txtSttRec0);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.v6Label10);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.txtework);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.txteposition);
            this.Controls.Add(this.txteproject);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.txtecompany);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.txtQuocTich);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.buttonSua);
            this.Controls.Add(this.buttonHuy);
            this.Controls.Add(this.buttonNhan);
            this.Controls.Add(this.txtSttRec);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KinhNghiemLamViec2";
            this.Size = new System.Drawing.Size(490, 374);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Button buttonSua;
        protected System.Windows.Forms.Button buttonHuy;
        protected System.Windows.Forms.Button buttonNhan;
        private V6ColorTextBox txtSttRec;
        private V6LookupTextBox txtQuocTich;
        private V6Label v6Label3;
        private V6Label v6Label1;
        private V6Label v6Label2;
        private V6ColorTextBox txtecompany;
        private V6Label v6Label4;
        private V6ColorTextBox txteproject;
        private V6ColorTextBox txteposition;
        private V6Label v6Label7;
        private V6ColorTextBox txtework;
        private V6Label v6Label8;
        private V6Label v6Label9;
        private V6Label v6Label10;
        private V6ColorTextBox txtremarks;
        private V6ColorTextBox txtSttRec0;
        private V6DateTimeColor txtdate_to;
        private V6DateTimeColor txtdate_from;
        private V6Label v6Label5;
        private V6ColorTextBox txtperiod;
        private V6Label v6Label6;
        private V6ColorTextBox txttype;
    }
}
