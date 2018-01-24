﻿namespace V6ControlManager.FormManager.ReportManager.Filter.Sms
{
    partial class XASENDMAIL
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTenNguoiNhan = new System.Windows.Forms.ComboBox();
            this.timerGuiDanhSach = new System.Windows.Forms.Timer(this.components);
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboChenTT = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTuDuLieu = new System.Windows.Forms.ComboBox();
            this.radTuGoNoiDung = new System.Windows.Forms.RadioButton();
            this.radTuDuLieu = new System.Windows.Forms.RadioButton();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSoDienThoai = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkThemVao = new System.Windows.Forms.CheckBox();
            this.btnXuatFileText = new System.Windows.Forms.Button();
            this.btnChonTuFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIgnore = new System.Windows.Forms.CheckBox();
            this.txtChonTen = new System.Windows.Forms.TextBox();
            this.btnChonTen = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGuiDanhSach = new System.Windows.Forms.Button();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtConnectPort = new System.Windows.Forms.TextBox();
            this.btnBoChonHet = new System.Windows.Forms.Button();
            this.btnDaoLuaChon = new System.Windows.Forms.Button();
            this.btnChonHet = new System.Windows.Forms.Button();
            this.grbThemNguoi = new System.Windows.Forms.GroupBox();
            this.btnThemNguoiNhan = new System.Windows.Forms.Button();
            this.lblThongTinKhac = new System.Windows.Forms.Label();
            this.lblTenNguoiNhan = new System.Windows.Forms.Label();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtThongTin5 = new System.Windows.Forms.TextBox();
            this.txtThongTin4 = new System.Windows.Forms.TextBox();
            this.txtThongTin3 = new System.Windows.Forms.TextBox();
            this.txtThongTin2 = new System.Windows.Forms.TextBox();
            this.txtThongTin1 = new System.Windows.Forms.TextBox();
            this.txtTenNguoiNhan = new System.Windows.Forms.TextBox();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtSendTo = new System.Windows.Forms.TextBox();
            this.grbKetNoi = new System.Windows.Forms.GroupBox();
            this.btnGui1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbThemNguoi.SuspendLayout();
            this.grbKetNoi.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 432);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Tên";
            // 
            // cboTenNguoiNhan
            // 
            this.cboTenNguoiNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboTenNguoiNhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenNguoiNhan.FormattingEnabled = true;
            this.cboTenNguoiNhan.Location = new System.Drawing.Point(426, 429);
            this.cboTenNguoiNhan.Name = "cboTenNguoiNhan";
            this.cboTenNguoiNhan.Size = new System.Drawing.Size(127, 21);
            this.cboTenNguoiNhan.TabIndex = 59;
            // 
            // timerGuiDanhSach
            // 
            this.timerGuiDanhSach.Tick += new System.EventHandler(this.timerGuiDanhSach_Tick);
            // 
            // timerSend
            // 
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel|*.xls;*.xlsx|DatabaseFox|*.dbf|Text|*.txt|Tất cả hỗ trợ|*.xls;*.xlsx;*.dbf;" +
    "*.txt";
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.FalseValue = "";
            this.Check.Frozen = true;
            this.Check.HeaderText = "Gửi";
            this.Check.Name = "Check";
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.ToolTipText = "Chọn được gửi hay không";
            this.Check.TrueValue = "1";
            this.Check.Width = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboChenTT);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboTuDuLieu);
            this.groupBox1.Controls.Add(this.radTuGoNoiDung);
            this.groupBox1.Controls.Add(this.radTuDuLieu);
            this.groupBox1.Controls.Add(this.lblNoiDung);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Location = new System.Drawing.Point(20, 456);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 124);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nội dung tin nhắn";
            // 
            // cboChenTT
            // 
            this.cboChenTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChenTT.FormattingEnabled = true;
            this.cboChenTT.Location = new System.Drawing.Point(406, 18);
            this.cboChenTT.Name = "cboChenTT";
            this.cboChenTT.Size = new System.Drawing.Size(127, 21);
            this.cboChenTT.TabIndex = 38;
            this.cboChenTT.SelectedIndexChanged += new System.EventHandler(this.cboChenTT_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Chèn thông tin";
            // 
            // cboTuDuLieu
            // 
            this.cboTuDuLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTuDuLieu.FormattingEnabled = true;
            this.cboTuDuLieu.Location = new System.Drawing.Point(156, 18);
            this.cboTuDuLieu.Name = "cboTuDuLieu";
            this.cboTuDuLieu.Size = new System.Drawing.Size(127, 21);
            this.cboTuDuLieu.TabIndex = 36;
            // 
            // radTuGoNoiDung
            // 
            this.radTuGoNoiDung.AutoSize = true;
            this.radTuGoNoiDung.Location = new System.Drawing.Point(6, 19);
            this.radTuGoNoiDung.Name = "radTuGoNoiDung";
            this.radTuGoNoiDung.Size = new System.Drawing.Size(53, 17);
            this.radTuGoNoiDung.TabIndex = 1;
            this.radTuGoNoiDung.Text = "Tự gõ";
            this.radTuGoNoiDung.UseVisualStyleBackColor = true;
            this.radTuGoNoiDung.CheckedChanged += new System.EventHandler(this.radTuGoNoiDung_CheckedChanged);
            // 
            // radTuDuLieu
            // 
            this.radTuDuLieu.AutoSize = true;
            this.radTuDuLieu.Checked = true;
            this.radTuDuLieu.Location = new System.Drawing.Point(78, 19);
            this.radTuDuLieu.Name = "radTuDuLieu";
            this.radTuDuLieu.Size = new System.Drawing.Size(72, 17);
            this.radTuDuLieu.TabIndex = 0;
            this.radTuDuLieu.TabStop = true;
            this.radTuDuLieu.Text = "Từ dữ liệu";
            this.radTuDuLieu.UseVisualStyleBackColor = true;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(6, 39);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(53, 13);
            this.lblNoiDung.TabIndex = 22;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Enabled = false;
            this.txtMessage.Location = new System.Drawing.Point(6, 55);
            this.txtMessage.MaxLength = 1600;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(527, 63);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "Xin chao <ten>, cong ty <tt1> dia chi <tt2>.";
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Chọn số điện thoại từ";
            // 
            // cboSoDienThoai
            // 
            this.cboSoDienThoai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSoDienThoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoDienThoai.FormattingEnabled = true;
            this.cboSoDienThoai.Location = new System.Drawing.Point(176, 429);
            this.cboSoDienThoai.Name = "cboSoDienThoai";
            this.cboSoDienThoai.Size = new System.Drawing.Size(127, 21);
            this.cboSoDienThoai.TabIndex = 45;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dataGridView1.Location = new System.Drawing.Point(20, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(539, 341);
            this.dataGridView1.TabIndex = 56;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // chkThemVao
            // 
            this.chkThemVao.AutoSize = true;
            this.chkThemVao.Location = new System.Drawing.Point(214, 30);
            this.chkThemVao.Name = "chkThemVao";
            this.chkThemVao.Size = new System.Drawing.Size(127, 17);
            this.chkThemVao.TabIndex = 55;
            this.chkThemVao.Text = "Thêm vào danh sách";
            this.chkThemVao.UseVisualStyleBackColor = true;
            // 
            // btnXuatFileText
            // 
            this.btnXuatFileText.Location = new System.Drawing.Point(462, 26);
            this.btnXuatFileText.Name = "btnXuatFileText";
            this.btnXuatFileText.Size = new System.Drawing.Size(91, 23);
            this.btnXuatFileText.TabIndex = 54;
            this.btnXuatFileText.Text = "Xuất file text";
            this.btnXuatFileText.UseVisualStyleBackColor = true;
            this.btnXuatFileText.Click += new System.EventHandler(this.btnXuatFileText_Click);
            // 
            // btnChonTuFile
            // 
            this.btnChonTuFile.Location = new System.Drawing.Point(117, 26);
            this.btnChonTuFile.Name = "btnChonTuFile";
            this.btnChonTuFile.Size = new System.Drawing.Size(91, 23);
            this.btnChonTuFile.TabIndex = 53;
            this.btnChonTuFile.Text = "Chọn từ file";
            this.btnChonTuFile.UseVisualStyleBackColor = true;
            this.btnChonTuFile.Click += new System.EventHandler(this.btnChonTuFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(565, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Số điện thoại";
            // 
            // chkIgnore
            // 
            this.chkIgnore.AutoSize = true;
            this.chkIgnore.Location = new System.Drawing.Point(509, 59);
            this.chkIgnore.Name = "chkIgnore";
            this.chkIgnore.Size = new System.Drawing.Size(46, 17);
            this.chkIgnore.TabIndex = 51;
            this.chkIgnore.Text = "H=h";
            this.toolTip1.SetToolTip(this.chkIgnore, "Không phân biệt hoa thường");
            this.chkIgnore.UseVisualStyleBackColor = true;
            // 
            // txtChonTen
            // 
            this.txtChonTen.Location = new System.Drawing.Point(359, 56);
            this.txtChonTen.Name = "txtChonTen";
            this.txtChonTen.Size = new System.Drawing.Size(144, 20);
            this.txtChonTen.TabIndex = 50;
            this.txtChonTen.Text = "Th";
            // 
            // btnChonTen
            // 
            this.btnChonTen.Location = new System.Drawing.Point(270, 53);
            this.btnChonTen.Name = "btnChonTen";
            this.btnChonTen.Size = new System.Drawing.Size(83, 23);
            this.btnChonTen.TabIndex = 49;
            this.btnChonTen.Text = "Chọn theo tên";
            this.btnChonTen.UseVisualStyleBackColor = true;
            this.btnChonTen.Click += new System.EventHandler(this.btnChonTen_Click);
            // 
            // btnGuiDanhSach
            // 
            this.btnGuiDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuiDanhSach.Location = new System.Drawing.Point(571, 534);
            this.btnGuiDanhSach.Name = "btnGuiDanhSach";
            this.btnGuiDanhSach.Size = new System.Drawing.Size(107, 46);
            this.btnGuiDanhSach.TabIndex = 42;
            this.btnGuiDanhSach.Text = "Gửi theo danh sách chọn";
            this.btnGuiDanhSach.UseVisualStyleBackColor = true;
            this.btnGuiDanhSach.Click += new System.EventHandler(this.btnGuiDanhSach_Click);
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(6, 19);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 2;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
            // 
            // txtConnectPort
            // 
            this.txtConnectPort.Location = new System.Drawing.Point(6, 75);
            this.txtConnectPort.Name = "txtConnectPort";
            this.txtConnectPort.ReadOnly = true;
            this.txtConnectPort.Size = new System.Drawing.Size(188, 20);
            this.txtConnectPort.TabIndex = 3;
            this.txtConnectPort.Text = "Chưa kết nối";
            // 
            // btnBoChonHet
            // 
            this.btnBoChonHet.Location = new System.Drawing.Point(190, 53);
            this.btnBoChonHet.Name = "btnBoChonHet";
            this.btnBoChonHet.Size = new System.Drawing.Size(74, 23);
            this.btnBoChonHet.TabIndex = 47;
            this.btnBoChonHet.Text = "Bỏ chọn hết";
            this.btnBoChonHet.UseVisualStyleBackColor = true;
            this.btnBoChonHet.Click += new System.EventHandler(this.btnBoChonHet_Click);
            // 
            // btnDaoLuaChon
            // 
            this.btnDaoLuaChon.Location = new System.Drawing.Point(99, 53);
            this.btnDaoLuaChon.Name = "btnDaoLuaChon";
            this.btnDaoLuaChon.Size = new System.Drawing.Size(85, 23);
            this.btnDaoLuaChon.TabIndex = 46;
            this.btnDaoLuaChon.Text = "Đảo lựa chọn";
            this.btnDaoLuaChon.UseVisualStyleBackColor = true;
            this.btnDaoLuaChon.Click += new System.EventHandler(this.btnDaoLuaChon_Click);
            // 
            // btnChonHet
            // 
            this.btnChonHet.Location = new System.Drawing.Point(20, 53);
            this.btnChonHet.Name = "btnChonHet";
            this.btnChonHet.Size = new System.Drawing.Size(73, 23);
            this.btnChonHet.TabIndex = 44;
            this.btnChonHet.Text = "Chọn tất cả";
            this.btnChonHet.UseVisualStyleBackColor = true;
            this.btnChonHet.Click += new System.EventHandler(this.btnChonHet_Click);
            // 
            // grbThemNguoi
            // 
            this.grbThemNguoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbThemNguoi.Controls.Add(this.btnThemNguoiNhan);
            this.grbThemNguoi.Controls.Add(this.lblThongTinKhac);
            this.grbThemNguoi.Controls.Add(this.lblTenNguoiNhan);
            this.grbThemNguoi.Controls.Add(this.lblSoDienThoai);
            this.grbThemNguoi.Controls.Add(this.txtThongTin5);
            this.grbThemNguoi.Controls.Add(this.txtThongTin4);
            this.grbThemNguoi.Controls.Add(this.txtThongTin3);
            this.grbThemNguoi.Controls.Add(this.txtThongTin2);
            this.grbThemNguoi.Controls.Add(this.txtThongTin1);
            this.grbThemNguoi.Controls.Add(this.txtTenNguoiNhan);
            this.grbThemNguoi.Controls.Add(this.txtSoDienThoai);
            this.grbThemNguoi.Location = new System.Drawing.Point(571, 145);
            this.grbThemNguoi.Name = "grbThemNguoi";
            this.grbThemNguoi.Size = new System.Drawing.Size(200, 275);
            this.grbThemNguoi.TabIndex = 39;
            this.grbThemNguoi.TabStop = false;
            this.grbThemNguoi.Text = "Thêm người nhận";
            // 
            // btnThemNguoiNhan
            // 
            this.btnThemNguoiNhan.Location = new System.Drawing.Point(9, 240);
            this.btnThemNguoiNhan.Name = "btnThemNguoiNhan";
            this.btnThemNguoiNhan.Size = new System.Drawing.Size(91, 23);
            this.btnThemNguoiNhan.TabIndex = 10;
            this.btnThemNguoiNhan.Text = "Thêm vào";
            this.btnThemNguoiNhan.UseVisualStyleBackColor = true;
            this.btnThemNguoiNhan.Click += new System.EventHandler(this.btnThemNguoiNhan_Click);
            // 
            // lblThongTinKhac
            // 
            this.lblThongTinKhac.AutoSize = true;
            this.lblThongTinKhac.Location = new System.Drawing.Point(6, 94);
            this.lblThongTinKhac.Name = "lblThongTinKhac";
            this.lblThongTinKhac.Size = new System.Drawing.Size(79, 13);
            this.lblThongTinKhac.TabIndex = 4;
            this.lblThongTinKhac.Text = "Thông tin khác";
            // 
            // lblTenNguoiNhan
            // 
            this.lblTenNguoiNhan.AutoSize = true;
            this.lblTenNguoiNhan.Location = new System.Drawing.Point(6, 55);
            this.lblTenNguoiNhan.Name = "lblTenNguoiNhan";
            this.lblTenNguoiNhan.Size = new System.Drawing.Size(82, 13);
            this.lblTenNguoiNhan.TabIndex = 2;
            this.lblTenNguoiNhan.Text = "Tên người nhận";
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(6, 16);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(70, 13);
            this.lblSoDienThoai.TabIndex = 0;
            this.lblSoDienThoai.Text = "Số điện thoại";
            // 
            // txtThongTin5
            // 
            this.txtThongTin5.Location = new System.Drawing.Point(6, 214);
            this.txtThongTin5.Name = "txtThongTin5";
            this.txtThongTin5.Size = new System.Drawing.Size(188, 20);
            this.txtThongTin5.TabIndex = 9;
            // 
            // txtThongTin4
            // 
            this.txtThongTin4.Location = new System.Drawing.Point(6, 188);
            this.txtThongTin4.Name = "txtThongTin4";
            this.txtThongTin4.Size = new System.Drawing.Size(188, 20);
            this.txtThongTin4.TabIndex = 8;
            // 
            // txtThongTin3
            // 
            this.txtThongTin3.Location = new System.Drawing.Point(6, 162);
            this.txtThongTin3.Name = "txtThongTin3";
            this.txtThongTin3.Size = new System.Drawing.Size(188, 20);
            this.txtThongTin3.TabIndex = 7;
            // 
            // txtThongTin2
            // 
            this.txtThongTin2.Location = new System.Drawing.Point(6, 136);
            this.txtThongTin2.Name = "txtThongTin2";
            this.txtThongTin2.Size = new System.Drawing.Size(188, 20);
            this.txtThongTin2.TabIndex = 6;
            // 
            // txtThongTin1
            // 
            this.txtThongTin1.Location = new System.Drawing.Point(6, 110);
            this.txtThongTin1.Name = "txtThongTin1";
            this.txtThongTin1.Size = new System.Drawing.Size(188, 20);
            this.txtThongTin1.TabIndex = 5;
            // 
            // txtTenNguoiNhan
            // 
            this.txtTenNguoiNhan.Location = new System.Drawing.Point(6, 71);
            this.txtTenNguoiNhan.Name = "txtTenNguoiNhan";
            this.txtTenNguoiNhan.Size = new System.Drawing.Size(188, 20);
            this.txtTenNguoiNhan.TabIndex = 3;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(6, 32);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(188, 20);
            this.txtSoDienThoai.TabIndex = 1;
            // 
            // txtSendTo
            // 
            this.txtSendTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendTo.Location = new System.Drawing.Point(564, 474);
            this.txtSendTo.Name = "txtSendTo";
            this.txtSendTo.Size = new System.Drawing.Size(153, 20);
            this.txtSendTo.TabIndex = 40;
            // 
            // grbKetNoi
            // 
            this.grbKetNoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbKetNoi.Controls.Add(this.btnKetNoi);
            this.grbKetNoi.Controls.Add(this.txtConnectPort);
            this.grbKetNoi.Location = new System.Drawing.Point(571, 34);
            this.grbKetNoi.Name = "grbKetNoi";
            this.grbKetNoi.Size = new System.Drawing.Size(200, 105);
            this.grbKetNoi.TabIndex = 38;
            this.grbKetNoi.TabStop = false;
            this.grbKetNoi.Text = "Kết nối";
            // 
            // btnGui1
            // 
            this.btnGui1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGui1.Location = new System.Drawing.Point(725, 472);
            this.btnGui1.Name = "btnGui1";
            this.btnGui1.Size = new System.Drawing.Size(46, 23);
            this.btnGui1.TabIndex = 41;
            this.btnGui1.Text = "Gửi 1";
            this.btnGui1.UseVisualStyleBackColor = true;
            this.btnGui1.Click += new System.EventHandler(this.btnGui1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Danh sách số";
            // 
            // XASENDMAIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboTenNguoiNhan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSoDienThoai);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chkThemVao);
            this.Controls.Add(this.btnXuatFileText);
            this.Controls.Add(this.btnChonTuFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkIgnore);
            this.Controls.Add(this.txtChonTen);
            this.Controls.Add(this.btnChonTen);
            this.Controls.Add(this.btnGuiDanhSach);
            this.Controls.Add(this.btnBoChonHet);
            this.Controls.Add(this.btnDaoLuaChon);
            this.Controls.Add(this.btnChonHet);
            this.Controls.Add(this.grbThemNguoi);
            this.Controls.Add(this.txtSendTo);
            this.Controls.Add(this.grbKetNoi);
            this.Controls.Add(this.btnGui1);
            this.Controls.Add(this.label4);
            this.Name = "XASENDMAIL";
            this.Size = new System.Drawing.Size(790, 606);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbThemNguoi.ResumeLayout(false);
            this.grbThemNguoi.PerformLayout();
            this.grbKetNoi.ResumeLayout(false);
            this.grbKetNoi.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTenNguoiNhan;
        private System.Windows.Forms.Timer timerGuiDanhSach;
        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboChenTT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTuDuLieu;
        private System.Windows.Forms.RadioButton radTuGoNoiDung;
        private System.Windows.Forms.RadioButton radTuDuLieu;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSoDienThoai;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkThemVao;
        private System.Windows.Forms.Button btnXuatFileText;
        private System.Windows.Forms.Button btnChonTuFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIgnore;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtChonTen;
        private System.Windows.Forms.Button btnChonTen;
        private System.Windows.Forms.Button btnGuiDanhSach;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.TextBox txtConnectPort;
        private System.Windows.Forms.Button btnBoChonHet;
        private System.Windows.Forms.Button btnDaoLuaChon;
        private System.Windows.Forms.Button btnChonHet;
        private System.Windows.Forms.GroupBox grbThemNguoi;
        private System.Windows.Forms.Button btnThemNguoiNhan;
        private System.Windows.Forms.Label lblThongTinKhac;
        private System.Windows.Forms.Label lblTenNguoiNhan;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtThongTin5;
        private System.Windows.Forms.TextBox txtThongTin4;
        private System.Windows.Forms.TextBox txtThongTin3;
        private System.Windows.Forms.TextBox txtThongTin2;
        private System.Windows.Forms.TextBox txtThongTin1;
        private System.Windows.Forms.TextBox txtTenNguoiNhan;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtSendTo;
        private System.Windows.Forms.GroupBox grbKetNoi;
        private System.Windows.Forms.Button btnGui1;
        private System.Windows.Forms.Label label4;


    }
}