namespace V6MDBtoSQLthread
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.start1By1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hienChuongTrinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.thoatChuongTrinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRunning = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.checkBoxRunWithSystem = new System.Windows.Forms.CheckBox();
            this.checkBoxShowOnStart = new System.Windows.Forms.CheckBox();
            this.timerSleep = new System.Windows.Forms.Timer(this.components);
            this.btn1By1 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblSleep_s = new System.Windows.Forms.Label();
            this.btnHide = new System.Windows.Forms.Button();
            this.dgvServerConfig = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Server = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Run = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdbxml = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqlxml = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdbWhere = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqlWhere = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLoadXmlTable = new System.Windows.Forms.Button();
            this.btnSaveXmlTable = new System.Windows.Forms.Button();
            this.btnEditTableFile = new System.Windows.Forms.Button();
            this.checkBoxRunSyncOnStart = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxStopWhenFinish = new System.Windows.Forms.CheckBox();
            this.btnSetSleepTime = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSleepTime_m = new System.Windows.Forms.TextBox();
            this.txtSleepTime_s = new System.Windows.Forms.TextBox();
            this.lblSleep_m = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMultiThreadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStartNew1By1 = new System.Windows.Forms.Button();
            this.contextMenuStripSystemTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerConfig)).BeginInit();
            this.groupBoxOptions.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripSystemTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Single";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.hienChuongTrinhToolStripMenuItem_Click);
            // 
            // contextMenuStripSystemTray
            // 
            this.contextMenuStripSystemTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.start1By1ToolStripMenuItem,
            this.StopToolStripMenuItem,
            this.hienChuongTrinhToolStripMenuItem,
            this.toolStripSeparator1,
            this.thoatChuongTrinhToolStripMenuItem});
            this.contextMenuStripSystemTray.Name = "contextMenuStripSystemTray";
            this.contextMenuStripSystemTray.Size = new System.Drawing.Size(153, 98);
            // 
            // start1By1ToolStripMenuItem
            // 
            this.start1By1ToolStripMenuItem.Name = "start1By1ToolStripMenuItem";
            this.start1By1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.start1By1ToolStripMenuItem.Text = "Start 1 by 1";
            this.start1By1ToolStripMenuItem.Click += new System.EventHandler(this.start1By1ToolStripMenuItem_Click);
            // 
            // StopToolStripMenuItem
            // 
            this.StopToolStripMenuItem.Name = "StopToolStripMenuItem";
            this.StopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.StopToolStripMenuItem.Text = "Stop";
            this.StopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // hienChuongTrinhToolStripMenuItem
            // 
            this.hienChuongTrinhToolStripMenuItem.Name = "hienChuongTrinhToolStripMenuItem";
            this.hienChuongTrinhToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hienChuongTrinhToolStripMenuItem.Text = "Show program";
            this.hienChuongTrinhToolStripMenuItem.Click += new System.EventHandler(this.hienChuongTrinhToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // thoatChuongTrinhToolStripMenuItem
            // 
            this.thoatChuongTrinhToolStripMenuItem.Name = "thoatChuongTrinhToolStripMenuItem";
            this.thoatChuongTrinhToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.thoatChuongTrinhToolStripMenuItem.Text = "Exit";
            this.thoatChuongTrinhToolStripMenuItem.Click += new System.EventHandler(this.thoatChuongTrinhToolStripMenuItem_Click);
            // 
            // timerRunning
            // 
            this.timerRunning.Interval = 200;
            this.timerRunning.Tick += new System.EventHandler(this.timerRunning_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(705, 518);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // checkBoxRunWithSystem
            // 
            this.checkBoxRunWithSystem.AutoSize = true;
            this.checkBoxRunWithSystem.Location = new System.Drawing.Point(6, 19);
            this.checkBoxRunWithSystem.Name = "checkBoxRunWithSystem";
            this.checkBoxRunWithSystem.Size = new System.Drawing.Size(134, 17);
            this.checkBoxRunWithSystem.TabIndex = 0;
            this.checkBoxRunWithSystem.Text = "Run with system (local)";
            this.checkBoxRunWithSystem.UseVisualStyleBackColor = true;
            this.checkBoxRunWithSystem.CheckedChanged += new System.EventHandler(this.checkBoxRun_CheckedChanged);
            // 
            // checkBoxShowOnStart
            // 
            this.checkBoxShowOnStart.AutoSize = true;
            this.checkBoxShowOnStart.Location = new System.Drawing.Point(6, 65);
            this.checkBoxShowOnStart.Name = "checkBoxShowOnStart";
            this.checkBoxShowOnStart.Size = new System.Drawing.Size(126, 17);
            this.checkBoxShowOnStart.TabIndex = 2;
            this.checkBoxShowOnStart.Text = "Show form on startup";
            this.checkBoxShowOnStart.UseVisualStyleBackColor = true;
            this.checkBoxShowOnStart.CheckedChanged += new System.EventHandler(this.checkBoxShow_CheckedChanged);
            // 
            // timerSleep
            // 
            this.timerSleep.Interval = 1000;
            this.timerSleep.Tick += new System.EventHandler(this.timerSleep_Tick);
            // 
            // btn1By1
            // 
            this.btn1By1.Location = new System.Drawing.Point(194, 103);
            this.btn1By1.Name = "btn1By1";
            this.btn1By1.Size = new System.Drawing.Size(97, 28);
            this.btn1By1.TabIndex = 3;
            this.btn1By1.Text = "Start";
            this.btn1By1.UseVisualStyleBackColor = true;
            this.btn1By1.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(297, 103);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(77, 28);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblSleep_s
            // 
            this.lblSleep_s.AutoSize = true;
            this.lblSleep_s.Location = new System.Drawing.Point(349, 69);
            this.lblSleep_s.Name = "lblSleep_s";
            this.lblSleep_s.Size = new System.Drawing.Size(13, 13);
            this.lblSleep_s.TabIndex = 5;
            this.lblSleep_s.Text = "0";
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHide.Location = new System.Drawing.Point(624, 518);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(75, 23);
            this.btnHide.TabIndex = 11;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // dgvServerConfig
            // 
            this.dgvServerConfig.AllowUserToAddRows = false;
            this.dgvServerConfig.AllowUserToDeleteRows = false;
            this.dgvServerConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Server,
            this.Database,
            this.Run,
            this.status,
            this.mdbxml,
            this.sqlxml,
            this.mdbWhere,
            this.sqlWhere});
            this.dgvServerConfig.Location = new System.Drawing.Point(12, 152);
            this.dgvServerConfig.Name = "dgvServerConfig";
            this.dgvServerConfig.Size = new System.Drawing.Size(768, 336);
            this.dgvServerConfig.TabIndex = 2;
            this.dgvServerConfig.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgvServerConfig.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServerConfig_CellLeave);
            this.dgvServerConfig.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dgvServerConfig.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvServerConfig_ColumnHeaderMouseDoubleClick);
            this.dgvServerConfig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvServerConfig_KeyDown);
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STT.DefaultCellStyle = dataGridViewCellStyle1;
            this.STT.Frozen = true;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.Width = 40;
            // 
            // Server
            // 
            this.Server.DataPropertyName = "Server";
            this.Server.Frozen = true;
            this.Server.HeaderText = "Server";
            this.Server.MaxInputLength = 50;
            this.Server.Name = "Server";
            this.Server.ReadOnly = true;
            this.Server.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Database
            // 
            this.Database.DataPropertyName = "Database";
            this.Database.HeaderText = "Database";
            this.Database.MaxInputLength = 50;
            this.Database.Name = "Database";
            this.Database.ReadOnly = true;
            this.Database.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Database.Visible = false;
            // 
            // Run
            // 
            this.Run.DataPropertyName = "Run";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Run.DefaultCellStyle = dataGridViewCellStyle2;
            this.Run.HeaderText = "Run";
            this.Run.MaxInputLength = 1;
            this.Run.Name = "Run";
            this.Run.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Run.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Run.Width = 40;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 200;
            // 
            // mdbxml
            // 
            this.mdbxml.DataPropertyName = "mdbxml";
            this.mdbxml.HeaderText = "mdbxml";
            this.mdbxml.Name = "mdbxml";
            this.mdbxml.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mdbxml.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sqlxml
            // 
            this.sqlxml.DataPropertyName = "sqlxml";
            this.sqlxml.HeaderText = "sqlxml";
            this.sqlxml.Name = "sqlxml";
            // 
            // mdbWhere
            // 
            this.mdbWhere.DataPropertyName = "mdbWhere";
            this.mdbWhere.HeaderText = "mdbWhere";
            this.mdbWhere.Name = "mdbWhere";
            // 
            // sqlWhere
            // 
            this.sqlWhere.DataPropertyName = "sqlWhere";
            this.sqlWhere.HeaderText = "sqlWhere";
            this.sqlWhere.Name = "sqlWhere";
            // 
            // btnLoadXmlTable
            // 
            this.btnLoadXmlTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadXmlTable.Location = new System.Drawing.Point(12, 494);
            this.btnLoadXmlTable.Name = "btnLoadXmlTable";
            this.btnLoadXmlTable.Size = new System.Drawing.Size(102, 23);
            this.btnLoadXmlTable.TabIndex = 8;
            this.btnLoadXmlTable.Text = "Load config";
            this.btnLoadXmlTable.UseVisualStyleBackColor = true;
            this.btnLoadXmlTable.Click += new System.EventHandler(this.btnLoadXmlTable_Click);
            // 
            // btnSaveXmlTable
            // 
            this.btnSaveXmlTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveXmlTable.Location = new System.Drawing.Point(123, 494);
            this.btnSaveXmlTable.Name = "btnSaveXmlTable";
            this.btnSaveXmlTable.Size = new System.Drawing.Size(102, 23);
            this.btnSaveXmlTable.TabIndex = 9;
            this.btnSaveXmlTable.Text = "Save config";
            this.btnSaveXmlTable.UseVisualStyleBackColor = true;
            this.btnSaveXmlTable.Click += new System.EventHandler(this.btnSaveXmlTable_Click);
            // 
            // btnEditTableFile
            // 
            this.btnEditTableFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditTableFile.Location = new System.Drawing.Point(362, 494);
            this.btnEditTableFile.Name = "btnEditTableFile";
            this.btnEditTableFile.Size = new System.Drawing.Size(142, 23);
            this.btnEditTableFile.TabIndex = 10;
            this.btnEditTableFile.Text = "Edit config file";
            this.btnEditTableFile.UseVisualStyleBackColor = true;
            this.btnEditTableFile.Click += new System.EventHandler(this.btnEditTableFile_Click);
            // 
            // checkBoxRunSyncOnStart
            // 
            this.checkBoxRunSyncOnStart.AutoSize = true;
            this.checkBoxRunSyncOnStart.Location = new System.Drawing.Point(6, 42);
            this.checkBoxRunSyncOnStart.Name = "checkBoxRunSyncOnStart";
            this.checkBoxRunSyncOnStart.Size = new System.Drawing.Size(152, 17);
            this.checkBoxRunSyncOnStart.TabIndex = 1;
            this.checkBoxRunSyncOnStart.Text = "Run when application start";
            this.checkBoxRunSyncOnStart.UseVisualStyleBackColor = true;
            this.checkBoxRunSyncOnStart.Click += new System.EventHandler(this.checkBoxRunSyncOnStart_Click);
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxStopWhenFinish);
            this.groupBoxOptions.Controls.Add(this.btnSetSleepTime);
            this.groupBoxOptions.Controls.Add(this.label5);
            this.groupBoxOptions.Controls.Add(this.label2);
            this.groupBoxOptions.Controls.Add(this.label1);
            this.groupBoxOptions.Controls.Add(this.txtSleepTime_m);
            this.groupBoxOptions.Controls.Add(this.txtSleepTime_s);
            this.groupBoxOptions.Controls.Add(this.checkBoxRunWithSystem);
            this.groupBoxOptions.Controls.Add(this.checkBoxShowOnStart);
            this.groupBoxOptions.Controls.Add(this.checkBoxRunSyncOnStart);
            this.groupBoxOptions.Controls.Add(this.lblSleep_m);
            this.groupBoxOptions.Controls.Add(this.lblSleep_s);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 12);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(768, 88);
            this.groupBoxOptions.TabIndex = 0;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // checkBoxStopWhenFinish
            // 
            this.checkBoxStopWhenFinish.AutoSize = true;
            this.checkBoxStopWhenFinish.Location = new System.Drawing.Point(202, 19);
            this.checkBoxStopWhenFinish.Name = "checkBoxStopWhenFinish";
            this.checkBoxStopWhenFinish.Size = new System.Drawing.Size(104, 17);
            this.checkBoxStopWhenFinish.TabIndex = 3;
            this.checkBoxStopWhenFinish.Text = "Stop when finish";
            this.checkBoxStopWhenFinish.UseVisualStyleBackColor = true;
            this.checkBoxStopWhenFinish.Click += new System.EventHandler(this.checkBoxStopWhenFinish_Click);
            // 
            // btnSetSleepTime
            // 
            this.btnSetSleepTime.Location = new System.Drawing.Point(418, 38);
            this.btnSetSleepTime.Name = "btnSetSleepTime";
            this.btnSetSleepTime.Size = new System.Drawing.Size(75, 23);
            this.btnSetSleepTime.TabIndex = 6;
            this.btnSetSleepTime.Text = "Set";
            this.btnSetSleepTime.UseVisualStyleBackColor = true;
            this.btnSetSleepTime.Click += new System.EventHandler(this.btnSetSleepTime_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(388, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "sec";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "min";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sleep time:";
            // 
            // txtSleepTime_m
            // 
            this.txtSleepTime_m.Location = new System.Drawing.Point(262, 40);
            this.txtSleepTime_m.Name = "txtSleepTime_m";
            this.txtSleepTime_m.Size = new System.Drawing.Size(53, 20);
            this.txtSleepTime_m.TabIndex = 5;
            this.txtSleepTime_m.Text = "5";
            this.txtSleepTime_m.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSleepTime_KeyDown);
            this.txtSleepTime_m.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // txtSleepTime_s
            // 
            this.txtSleepTime_s.Location = new System.Drawing.Point(350, 40);
            this.txtSleepTime_s.Name = "txtSleepTime_s";
            this.txtSleepTime_s.Size = new System.Drawing.Size(32, 20);
            this.txtSleepTime_s.TabIndex = 5;
            this.txtSleepTime_s.Text = "5";
            this.txtSleepTime_s.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSleepTime_KeyDown);
            this.txtSleepTime_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberOnly);
            // 
            // lblSleep_m
            // 
            this.lblSleep_m.AutoSize = true;
            this.lblSleep_m.Location = new System.Drawing.Point(266, 69);
            this.lblSleep_m.Name = "lblSleep_m";
            this.lblSleep_m.Size = new System.Drawing.Size(13, 13);
            this.lblSleep_m.TabIndex = 5;
            this.lblSleep_m.Text = "0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblProgress,
            this.lblMultiThreadStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblProgress
            // 
            this.lblProgress.ForeColor = System.Drawing.Color.Red;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(97, 17);
            this.lblProgress.Text = ".ıllı.ıllı.ıllı.ıllı.ıllı.ıllı";
            // 
            // lblMultiThreadStatus
            // 
            this.lblMultiThreadStatus.Name = "lblMultiThreadStatus";
            this.lblMultiThreadStatus.Size = new System.Drawing.Size(39, 17);
            this.lblMultiThreadStatus.Text = "Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(294, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(416, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Column Run : Double click left->1 all, Double click right->0 all, Double click Ro" +
    "w -> 1/0";
            // 
            // btnStartNew1By1
            // 
            this.btnStartNew1By1.Location = new System.Drawing.Point(592, 103);
            this.btnStartNew1By1.Name = "btnStartNew1By1";
            this.btnStartNew1By1.Size = new System.Drawing.Size(119, 28);
            this.btnStartNew1By1.TabIndex = 4;
            this.btnStartNew1By1.Text = "Start New 1 by 1";
            this.btnStartNew1By1.UseVisualStyleBackColor = true;
            this.btnStartNew1By1.Visible = false;
            this.btnStartNew1By1.Click += new System.EventHandler(this.btnStartNew1By1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHide;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.btnLoadXmlTable);
            this.Controls.Add(this.dgvServerConfig);
            this.Controls.Add(this.btnEditTableFile);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnSaveXmlTable);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btn1By1);
            this.Controls.Add(this.btnStartNew1By1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "V6MDBtoSQL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHuuanSms_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormHuuanSms_Resize);
            this.contextMenuStripSystemTray.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerConfig)).EndInit();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
        private System.Windows.Forms.ToolStripMenuItem hienChuongTrinhToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem thoatChuongTrinhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem;
        private System.Windows.Forms.Timer timerRunning;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox checkBoxRunWithSystem;
        private System.Windows.Forms.CheckBox checkBoxShowOnStart;
        private System.Windows.Forms.Timer timerSleep;
        private System.Windows.Forms.Button btn1By1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblSleep_s;
        private System.Windows.Forms.Button btnHide;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView dgvServerConfig;
        private System.Windows.Forms.Button btnLoadXmlTable;
        private System.Windows.Forms.Button btnSaveXmlTable;
        private System.Windows.Forms.Button btnEditTableFile;
        private System.Windows.Forms.CheckBox checkBoxRunSyncOnStart;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Button btnSetSleepTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSleepTime_s;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.CheckBox checkBoxStopWhenFinish;
        private System.Windows.Forms.ToolStripMenuItem start1By1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblMultiThreadStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Server;
        private System.Windows.Forms.DataGridViewTextBoxColumn Database;
        private System.Windows.Forms.DataGridViewTextBoxColumn Run;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdbxml;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlxml;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdbWhere;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlWhere;
        private System.Windows.Forms.TextBox txtSleepTime_m;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSleep_m;
        private System.Windows.Forms.Button btnStartNew1By1;
    }
}