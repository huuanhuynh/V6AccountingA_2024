namespace V6ControlManager.FormManager.ReportManager.Filter.Sms
{
    partial class XASENDSMSALL
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerGuiDanhSach = new System.Windows.Forms.Timer(this.components);
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkIgnore = new System.Windows.Forms.CheckBox();
            this.grbTest = new System.Windows.Forms.GroupBox();
            this.txtSmsTo = new V6Controls.V6ColorTextBox();
            this.btnGui1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtChonTen = new V6Controls.V6ColorTextBox();
            this.btnChonTheoTen = new System.Windows.Forms.Button();
            this.btnBoChonHet = new System.Windows.Forms.Button();
            this.btnDaoLuaChon = new System.Windows.Forms.Button();
            this.btnChonHet = new System.Windows.Forms.Button();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.filterLineVvarTextBox4 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox3 = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMact = new V6ReportControls.FilterLineVvarTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grbKetNoi = new System.Windows.Forms.GroupBox();
            this.btnTuKetNoi = new System.Windows.Forms.Button();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtConnectPort = new V6Controls.V6ColorTextBox();
            this.btnGuiDanhSach = new System.Windows.Forms.Button();
            this.dataGridView2 = new V6Controls.V6ColorDataGridView();
            this.grbTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbFilter.SuspendLayout();
            this.grbKetNoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
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
            // chkIgnore
            // 
            this.chkIgnore.AccessibleDescription = "MAILSMSC00001";
            this.chkIgnore.AutoSize = true;
            this.chkIgnore.Location = new System.Drawing.Point(800, 9);
            this.chkIgnore.Name = "chkIgnore";
            this.chkIgnore.Size = new System.Drawing.Size(46, 17);
            this.chkIgnore.TabIndex = 7;
            this.chkIgnore.Text = "H=h";
            this.toolTip1.SetToolTip(this.chkIgnore, "Không phân biệt hoa thường");
            this.chkIgnore.UseVisualStyleBackColor = true;
            this.chkIgnore.Visible = false;
            // 
            // grbTest
            // 
            this.grbTest.AccessibleDescription = "MAILSMSG00004";
            this.grbTest.Controls.Add(this.txtSmsTo);
            this.grbTest.Controls.Add(this.btnGui1);
            this.grbTest.Location = new System.Drawing.Point(3, 374);
            this.grbTest.Name = "grbTest";
            this.grbTest.Size = new System.Drawing.Size(302, 81);
            this.grbTest.TabIndex = 9;
            this.grbTest.TabStop = false;
            this.grbTest.Text = "Thử nghiệm";
            // 
            // txtSmsTo
            // 
            this.txtSmsTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtSmsTo.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSmsTo.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSmsTo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSmsTo.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSmsTo.GrayText = "Số điện thoại";
            this.txtSmsTo.HoverColor = System.Drawing.Color.Yellow;
            this.txtSmsTo.LeaveColor = System.Drawing.Color.White;
            this.txtSmsTo.Location = new System.Drawing.Point(6, 19);
            this.txtSmsTo.Name = "txtSmsTo";
            this.txtSmsTo.Size = new System.Drawing.Size(131, 20);
            this.txtSmsTo.TabIndex = 0;
            // 
            // btnGui1
            // 
            this.btnGui1.AccessibleDescription = "MAILSMSB00011";
            this.btnGui1.Location = new System.Drawing.Point(148, 19);
            this.btnGui1.Name = "btnGui1";
            this.btnGui1.Size = new System.Drawing.Size(46, 23);
            this.btnGui1.TabIndex = 1;
            this.btnGui1.Text = "Gửi 1";
            this.btnGui1.UseVisualStyleBackColor = true;
            this.btnGui1.Click += new System.EventHandler(this.btnGui1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dataGridView1.Location = new System.Drawing.Point(311, 32);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(677, 336);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
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
            // txtChonTen
            // 
            this.txtChonTen.BackColor = System.Drawing.SystemColors.Window;
            this.txtChonTen.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtChonTen.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtChonTen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtChonTen.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtChonTen.HoverColor = System.Drawing.Color.Yellow;
            this.txtChonTen.LeaveColor = System.Drawing.Color.White;
            this.txtChonTen.Location = new System.Drawing.Point(650, 6);
            this.txtChonTen.Name = "txtChonTen";
            this.txtChonTen.Size = new System.Drawing.Size(144, 20);
            this.txtChonTen.TabIndex = 6;
            this.txtChonTen.Text = "Th";
            this.txtChonTen.Visible = false;
            // 
            // btnChonTheoTen
            // 
            this.btnChonTheoTen.AccessibleDescription = "MAILSMSB00006";
            this.btnChonTheoTen.Location = new System.Drawing.Point(561, 3);
            this.btnChonTheoTen.Name = "btnChonTheoTen";
            this.btnChonTheoTen.Size = new System.Drawing.Size(83, 23);
            this.btnChonTheoTen.TabIndex = 5;
            this.btnChonTheoTen.Text = "Chọn theo tên";
            this.btnChonTheoTen.UseVisualStyleBackColor = true;
            this.btnChonTheoTen.Visible = false;
            this.btnChonTheoTen.Click += new System.EventHandler(this.btnChonTheoTen_Click);
            // 
            // btnBoChonHet
            // 
            this.btnBoChonHet.AccessibleDescription = "MAILSMSB00005";
            this.btnBoChonHet.Location = new System.Drawing.Point(481, 3);
            this.btnBoChonHet.Name = "btnBoChonHet";
            this.btnBoChonHet.Size = new System.Drawing.Size(74, 23);
            this.btnBoChonHet.TabIndex = 4;
            this.btnBoChonHet.Text = "Bỏ chọn hết";
            this.btnBoChonHet.UseVisualStyleBackColor = true;
            this.btnBoChonHet.Click += new System.EventHandler(this.btnBoChonHet_Click);
            // 
            // btnDaoLuaChon
            // 
            this.btnDaoLuaChon.AccessibleDescription = "MAILSMSB00004";
            this.btnDaoLuaChon.Location = new System.Drawing.Point(390, 3);
            this.btnDaoLuaChon.Name = "btnDaoLuaChon";
            this.btnDaoLuaChon.Size = new System.Drawing.Size(85, 23);
            this.btnDaoLuaChon.TabIndex = 3;
            this.btnDaoLuaChon.Text = "Đảo lựa chọn";
            this.btnDaoLuaChon.UseVisualStyleBackColor = true;
            this.btnDaoLuaChon.Click += new System.EventHandler(this.btnDaoLuaChon_Click);
            // 
            // btnChonHet
            // 
            this.btnChonHet.AccessibleDescription = "MAILSMSB00003";
            this.btnChonHet.Location = new System.Drawing.Point(311, 3);
            this.btnChonHet.Name = "btnChonHet";
            this.btnChonHet.Size = new System.Drawing.Size(73, 23);
            this.btnChonHet.TabIndex = 2;
            this.btnChonHet.Text = "Chọn tất cả";
            this.btnChonHet.UseVisualStyleBackColor = true;
            this.btnChonHet.Click += new System.EventHandler(this.btnChonHet_Click);
            // 
            // grbFilter
            // 
            this.grbFilter.AccessibleDescription = "MAILSMSG00006";
            this.grbFilter.Controls.Add(this.dateNgay_ct2);
            this.grbFilter.Controls.Add(this.dateNgay_ct1);
            this.grbFilter.Controls.Add(this.filterLineVvarTextBox4);
            this.grbFilter.Controls.Add(this.filterLineVvarTextBox3);
            this.grbFilter.Controls.Add(this.lineMaDvcs);
            this.grbFilter.Controls.Add(this.lineMact);
            this.grbFilter.Controls.Add(this.label7);
            this.grbFilter.Controls.Add(this.label8);
            this.grbFilter.Location = new System.Drawing.Point(3, 10);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Size = new System.Drawing.Size(302, 254);
            this.grbFilter.TabIndex = 0;
            this.grbFilter.TabStop = false;
            this.grbFilter.Text = "Lọc dữ liệu";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgay_ct2.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(189, 39);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgay_ct1.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(189, 13);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // filterLineVvarTextBox4
            // 
            this.filterLineVvarTextBox4.AccessibleDescription = "FILTERL00008";
            this.filterLineVvarTextBox4.AccessibleName2 = "MA_BP";
            this.filterLineVvarTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterLineVvarTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox4.Caption = "Mã bộ phận";
            this.filterLineVvarTextBox4.FieldName = "MA_BP";
            this.filterLineVvarTextBox4.Location = new System.Drawing.Point(6, 157);
            this.filterLineVvarTextBox4.Name = "filterLineVvarTextBox4";
            this.filterLineVvarTextBox4.Size = new System.Drawing.Size(287, 22);
            this.filterLineVvarTextBox4.TabIndex = 7;
            this.filterLineVvarTextBox4.Vvar = "MA_BP";
            // 
            // filterLineVvarTextBox3
            // 
            this.filterLineVvarTextBox3.AccessibleDescription = "FILTERL00007";
            this.filterLineVvarTextBox3.AccessibleName2 = "MA_KH";
            this.filterLineVvarTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterLineVvarTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.filterLineVvarTextBox3.Caption = "Mã khách hàng";
            this.filterLineVvarTextBox3.FieldName = "MA_KH";
            this.filterLineVvarTextBox3.Location = new System.Drawing.Point(6, 132);
            this.filterLineVvarTextBox3.Name = "filterLineVvarTextBox3";
            this.filterLineVvarTextBox3.Size = new System.Drawing.Size(287, 22);
            this.filterLineVvarTextBox3.TabIndex = 6;
            this.filterLineVvarTextBox3.Vvar = "MA_KH";
            // 
            // lineMaDvcs
            // 
            this.lineMaDvcs.AccessibleDescription = "FILTERL00005";
            this.lineMaDvcs.AccessibleName2 = "MA_DVCS";
            this.lineMaDvcs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineMaDvcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaDvcs.Caption = "Mã đơn vị";
            this.lineMaDvcs.FieldName = "MA_DVCS";
            this.lineMaDvcs.Location = new System.Drawing.Point(6, 107);
            this.lineMaDvcs.Name = "lineMaDvcs";
            this.lineMaDvcs.Size = new System.Drawing.Size(287, 22);
            this.lineMaDvcs.TabIndex = 5;
            this.lineMaDvcs.Vvar = "MA_DVCS";
            // 
            // lineMact
            // 
            this.lineMact.AccessibleDescription = "FILTERL00004";
            this.lineMact.AccessibleName2 = "";
            this.lineMact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineMact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMact.Caption = "Mã chứng từ";
            this.lineMact.FieldName = "";
            this.lineMact.Location = new System.Drawing.Point(6, 82);
            this.lineMact.Name = "lineMact";
            this.lineMact.Size = new System.Drawing.Size(287, 22);
            this.lineMact.TabIndex = 4;
            this.lineMact.Vvar = "MA_CT";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "FILTERL00003";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Đến ngày";
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "FILTERL00002";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Từ ngày";
            // 
            // grbKetNoi
            // 
            this.grbKetNoi.AccessibleDescription = "MAILSMSG00002";
            this.grbKetNoi.Controls.Add(this.btnTuKetNoi);
            this.grbKetNoi.Controls.Add(this.btnKetNoi);
            this.grbKetNoi.Controls.Add(this.txtConnectPort);
            this.grbKetNoi.Location = new System.Drawing.Point(3, 289);
            this.grbKetNoi.Name = "grbKetNoi";
            this.grbKetNoi.Size = new System.Drawing.Size(302, 79);
            this.grbKetNoi.TabIndex = 1;
            this.grbKetNoi.TabStop = false;
            this.grbKetNoi.Text = "Kết nối sms";
            // 
            // btnTuKetNoi
            // 
            this.btnTuKetNoi.AccessibleDescription = "MAILSMSB00008";
            this.btnTuKetNoi.Location = new System.Drawing.Point(87, 19);
            this.btnTuKetNoi.Name = "btnTuKetNoi";
            this.btnTuKetNoi.Size = new System.Drawing.Size(105, 23);
            this.btnTuKetNoi.TabIndex = 1;
            this.btnTuKetNoi.Text = "Tự kết nối";
            this.btnTuKetNoi.UseVisualStyleBackColor = true;
            this.btnTuKetNoi.Click += new System.EventHandler(this.btnTuKetNoi_Click);
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.AccessibleDescription = "MAILSMSB00007";
            this.btnKetNoi.Location = new System.Drawing.Point(6, 19);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 0;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
            // 
            // txtConnectPort
            // 
            this.txtConnectPort.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtConnectPort.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtConnectPort.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtConnectPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtConnectPort.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtConnectPort.HoverColor = System.Drawing.Color.Yellow;
            this.txtConnectPort.LeaveColor = System.Drawing.Color.White;
            this.txtConnectPort.Location = new System.Drawing.Point(4, 48);
            this.txtConnectPort.Name = "txtConnectPort";
            this.txtConnectPort.ReadOnly = true;
            this.txtConnectPort.Size = new System.Drawing.Size(188, 20);
            this.txtConnectPort.TabIndex = 2;
            this.txtConnectPort.TabStop = false;
            this.txtConnectPort.Text = "Chưa kết nối";
            // 
            // btnGuiDanhSach
            // 
            this.btnGuiDanhSach.AccessibleDescription = "MAILSMSB00010";
            this.btnGuiDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuiDanhSach.Image = global::V6ControlManager.Properties.Resources.sms48;
            this.btnGuiDanhSach.Location = new System.Drawing.Point(7, 482);
            this.btnGuiDanhSach.Name = "btnGuiDanhSach";
            this.btnGuiDanhSach.Size = new System.Drawing.Size(165, 52);
            this.btnGuiDanhSach.TabIndex = 10;
            this.btnGuiDanhSach.Text = "Gửi theo danh sách chọn";
            this.btnGuiDanhSach.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGuiDanhSach.UseVisualStyleBackColor = true;
            this.btnGuiDanhSach.Click += new System.EventHandler(this.btnGuiDanhSach_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(311, 374);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(677, 160);
            this.dataGridView2.TabIndex = 11;
            // 
            // XASENDSMSALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.grbTest);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chkIgnore);
            this.Controls.Add(this.txtChonTen);
            this.Controls.Add(this.btnChonTheoTen);
            this.Controls.Add(this.btnGuiDanhSach);
            this.Controls.Add(this.btnBoChonHet);
            this.Controls.Add(this.btnDaoLuaChon);
            this.Controls.Add(this.btnChonHet);
            this.Controls.Add(this.grbFilter);
            this.Controls.Add(this.grbKetNoi);
            this.Name = "XASENDSMSALL";
            this.Size = new System.Drawing.Size(1000, 540);
            this.grbTest.ResumeLayout(false);
            this.grbTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbFilter.ResumeLayout(false);
            this.grbFilter.PerformLayout();
            this.grbKetNoi.ResumeLayout(false);
            this.grbKetNoi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerGuiDanhSach;
        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private V6Controls.V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkIgnore;
        private System.Windows.Forms.ToolTip toolTip1;
        private V6Controls.V6ColorTextBox txtChonTen;
        private System.Windows.Forms.Button btnChonTheoTen;
        private System.Windows.Forms.Button btnGuiDanhSach;
        private System.Windows.Forms.Button btnKetNoi;
        private V6Controls.V6ColorTextBox txtConnectPort;
        private System.Windows.Forms.Button btnBoChonHet;
        private System.Windows.Forms.Button btnDaoLuaChon;
        private System.Windows.Forms.Button btnChonHet;
        private V6Controls.V6ColorTextBox txtSmsTo;
        private System.Windows.Forms.GroupBox grbKetNoi;
        private System.Windows.Forms.Button btnGui1;
        private System.Windows.Forms.GroupBox grbTest;
        private System.Windows.Forms.Button btnTuKetNoi;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private System.Windows.Forms.GroupBox grbFilter;
        private V6ReportControls.FilterLineVvarTextBox lineMact;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private V6ReportControls.FilterLineVvarTextBox lineMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox4;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox3;
        public V6Controls.V6ColorDataGridView dataGridView2;


    }
}
