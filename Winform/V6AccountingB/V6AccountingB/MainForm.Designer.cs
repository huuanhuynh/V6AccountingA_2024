using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6AccountingB
{
    partial class MainForm
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
            V6Controls.MenuButton menuButton1 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton2 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton3 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton4 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton5 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton6 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton7 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton8 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton9 = new V6Controls.MenuButton();
            V6Controls.MenuButton menuButton10 = new V6Controls.MenuButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeDVCSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stickNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDVCS = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHotLine = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuMain = new V6Controls.MenuControl();
            this.quickMenu1 = new V6AccountingB.QuickMenu();
            this.panelMenuShow = new System.Windows.Forms.Panel();
            this.lblMenuShow = new V6Controls.V6VeticalLabel();
            this.lblMainMessage = new V6Controls.V6Label();
            this.panelView = new System.Windows.Forms.Panel();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblV6Message = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMenuShow.SuspendLayout();
            this.panelView.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblStatus2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Resize += new System.EventHandler(this.statusStrip1_Resize);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 17);
            this.lblStatus.Text = ".";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = false;
            this.lblStatus2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus2.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(318, 17);
            this.lblStatus2.Text = ".";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.panelLogin);
            this.panelMenu.Controls.Add(this.menuMain);
            this.panelMenu.Controls.Add(this.quickMenu1);
            this.panelMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMenu.Location = new System.Drawing.Point(0, 12);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(155, 551);
            this.panelMenu.TabIndex = 0;
            // 
            // panelLogin
            // 
            this.panelLogin.ContextMenuStrip = this.contextMenuStrip1;
            this.panelLogin.Controls.Add(this.lblDVCS);
            this.panelLogin.Controls.Add(this.pictureBox1);
            this.panelLogin.Controls.Add(this.lblHotLine);
            this.panelLogin.Controls.Add(this.btnExit);
            this.panelLogin.Controls.Add(this.lblComment);
            this.panelLogin.Controls.Add(this.lblUserName);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLogin.Location = new System.Drawing.Point(0, 404);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(153, 145);
            this.panelLogin.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDVCSToolStripMenuItem,
            this.stickNoteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            // 
            // changeDVCSToolStripMenuItem
            // 
            this.changeDVCSToolStripMenuItem.Name = "changeDVCSToolStripMenuItem";
            this.changeDVCSToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.changeDVCSToolStripMenuItem.Text = "Change Agent";
            this.changeDVCSToolStripMenuItem.Click += new System.EventHandler(this.changeDVCSToolStripMenuItem_Click);
            // 
            // stickNoteToolStripMenuItem
            // 
            this.stickNoteToolStripMenuItem.Name = "stickNoteToolStripMenuItem";
            this.stickNoteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stickNoteToolStripMenuItem.Text = "Stick note";
            this.stickNoteToolStripMenuItem.Click += new System.EventHandler(this.stickNoteToolStripMenuItem_Click);
            // 
            // lblDVCS
            // 
            this.lblDVCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDVCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDVCS.Location = new System.Drawing.Point(4, 66);
            this.lblDVCS.Name = "lblDVCS";
            this.lblDVCS.Size = new System.Drawing.Size(145, 20);
            this.lblDVCS.TabIndex = 15;
            this.lblDVCS.Text = "UserName";
            this.lblDVCS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDVCS_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::V6AccountingB.Properties.Resources.Calculator;
            this.pictureBox1.Location = new System.Drawing.Point(101, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblHotLine
            // 
            this.lblHotLine.AutoSize = true;
            this.lblHotLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHotLine.ForeColor = System.Drawing.Color.Red;
            this.lblHotLine.Location = new System.Drawing.Point(2, 87);
            this.lblHotLine.Name = "lblHotLine";
            this.lblHotLine.Size = new System.Drawing.Size(147, 13);
            this.lblHotLine.TabIndex = 13;
            this.lblHotLine.Text = "HOTLINE: 0936 976 976";
            this.lblHotLine.Click += new System.EventHandler(this.lblHotLine_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::V6AccountingB.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(7, 102);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(78, 39);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblComment
            // 
            this.lblComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(2, 24);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(148, 44);
            this.lblComment.TabIndex = 0;
            this.lblComment.Text = "coment";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(50, 5);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(88, 18);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "UserName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "User:";
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.Highlight;
            this.menuMain.ButtonHeight = 30;
            menuButton1.AccessibleDescription = "tien_mat";
            menuButton1.CodeForm = "";
            menuButton1.Enabled = true;
            menuButton1.Exe = "";
            menuButton1.HotKey = "1";
            menuButton1.Image = global::V6AccountingB.Properties.Resources.money_bani;
            menuButton1.Info = "";
            menuButton1.ItemID = "";
            menuButton1.KeyFields = null;
            menuButton1.ListTable = "";
            menuButton1.MaChungTu = "";
            menuButton1.NhatKy = "";
            menuButton1.ReportFile = null;
            menuButton1.ReportFileF5 = null;
            menuButton1.ReportTitle = null;
            menuButton1.ReportTitle2 = null;
            menuButton1.ReportTitle2F5 = null;
            menuButton1.ReportTitleF5 = null;
            menuButton1.SortField = "";
            menuButton1.StatusNumber = 0;
            menuButton1.TableName = "";
            menuButton1.Tag = null;
            menuButton1.Text = "Tiền mặt";
            menuButton1.UserControlViewName = "TienMat";
            menuButton1.Visible = true;
            menuButton2.AccessibleDescription = "";
            menuButton2.CodeForm = "";
            menuButton2.Enabled = true;
            menuButton2.Exe = "";
            menuButton2.HotKey = "2";
            menuButton2.Image = null;
            menuButton2.Info = "";
            menuButton2.ItemID = "";
            menuButton2.KeyFields = null;
            menuButton2.ListTable = "";
            menuButton2.MaChungTu = "";
            menuButton2.NhatKy = "";
            menuButton2.ReportFile = null;
            menuButton2.ReportFileF5 = null;
            menuButton2.ReportTitle = null;
            menuButton2.ReportTitle2 = null;
            menuButton2.ReportTitle2F5 = null;
            menuButton2.ReportTitleF5 = null;
            menuButton2.SortField = "";
            menuButton2.StatusNumber = 0;
            menuButton2.TableName = "";
            menuButton2.Tag = null;
            menuButton2.Text = "Phải thu";
            menuButton2.UserControlViewName = "PhaiThu";
            menuButton2.Visible = true;
            menuButton3.AccessibleDescription = "";
            menuButton3.CodeForm = "";
            menuButton3.Enabled = true;
            menuButton3.Exe = "";
            menuButton3.HotKey = "3";
            menuButton3.Image = null;
            menuButton3.Info = "";
            menuButton3.ItemID = "";
            menuButton3.KeyFields = null;
            menuButton3.ListTable = "";
            menuButton3.MaChungTu = "";
            menuButton3.NhatKy = "";
            menuButton3.ReportFile = null;
            menuButton3.ReportFileF5 = null;
            menuButton3.ReportTitle = null;
            menuButton3.ReportTitle2 = null;
            menuButton3.ReportTitle2F5 = null;
            menuButton3.ReportTitleF5 = null;
            menuButton3.SortField = "";
            menuButton3.StatusNumber = 0;
            menuButton3.TableName = "";
            menuButton3.Tag = null;
            menuButton3.Text = "Phải trả";
            menuButton3.UserControlViewName = "PhaiTra";
            menuButton3.Visible = true;
            menuButton4.AccessibleDescription = "";
            menuButton4.CodeForm = "";
            menuButton4.Enabled = true;
            menuButton4.Exe = "";
            menuButton4.HotKey = null;
            menuButton4.Image = global::V6AccountingB.Properties.Resources.warehouse_storage;
            menuButton4.Info = "";
            menuButton4.ItemID = "";
            menuButton4.KeyFields = null;
            menuButton4.ListTable = "";
            menuButton4.MaChungTu = "";
            menuButton4.NhatKy = "";
            menuButton4.ReportFile = null;
            menuButton4.ReportFileF5 = null;
            menuButton4.ReportTitle = null;
            menuButton4.ReportTitle2 = null;
            menuButton4.ReportTitle2F5 = null;
            menuButton4.ReportTitleF5 = null;
            menuButton4.SortField = "";
            menuButton4.StatusNumber = 0;
            menuButton4.TableName = "";
            menuButton4.Tag = null;
            menuButton4.Text = "Tồn kho";
            menuButton4.UserControlViewName = "TonKho";
            menuButton4.Visible = true;
            menuButton5.AccessibleDescription = "";
            menuButton5.CodeForm = "";
            menuButton5.Enabled = true;
            menuButton5.Exe = "";
            menuButton5.HotKey = null;
            menuButton5.Image = null;
            menuButton5.Info = "";
            menuButton5.ItemID = "";
            menuButton5.KeyFields = null;
            menuButton5.ListTable = "";
            menuButton5.MaChungTu = "";
            menuButton5.NhatKy = "";
            menuButton5.ReportFile = null;
            menuButton5.ReportFileF5 = null;
            menuButton5.ReportTitle = null;
            menuButton5.ReportTitle2 = null;
            menuButton5.ReportTitle2F5 = null;
            menuButton5.ReportTitleF5 = null;
            menuButton5.SortField = "";
            menuButton5.StatusNumber = 0;
            menuButton5.TableName = "";
            menuButton5.Tag = null;
            menuButton5.Text = "Chi phí / Giá thành";
            menuButton5.UserControlViewName = "ChiPhiGiaThanh";
            menuButton5.Visible = true;
            menuButton6.AccessibleDescription = "";
            menuButton6.CodeForm = "";
            menuButton6.Enabled = true;
            menuButton6.Exe = "";
            menuButton6.HotKey = null;
            menuButton6.Image = null;
            menuButton6.Info = "";
            menuButton6.ItemID = "";
            menuButton6.KeyFields = null;
            menuButton6.ListTable = "";
            menuButton6.MaChungTu = "";
            menuButton6.NhatKy = "";
            menuButton6.ReportFile = null;
            menuButton6.ReportFileF5 = null;
            menuButton6.ReportTitle = null;
            menuButton6.ReportTitle2 = null;
            menuButton6.ReportTitle2F5 = null;
            menuButton6.ReportTitleF5 = null;
            menuButton6.SortField = "";
            menuButton6.StatusNumber = 0;
            menuButton6.TableName = "";
            menuButton6.Tag = null;
            menuButton6.Text = "Tài sản cố định";
            menuButton6.UserControlViewName = "TaiSanCoDinh";
            menuButton6.Visible = true;
            menuButton7.AccessibleDescription = "";
            menuButton7.CodeForm = "";
            menuButton7.Enabled = true;
            menuButton7.Exe = "";
            menuButton7.HotKey = null;
            menuButton7.Image = null;
            menuButton7.Info = "";
            menuButton7.ItemID = "";
            menuButton7.KeyFields = null;
            menuButton7.ListTable = "";
            menuButton7.MaChungTu = "";
            menuButton7.NhatKy = "";
            menuButton7.ReportFile = null;
            menuButton7.ReportFileF5 = null;
            menuButton7.ReportTitle = null;
            menuButton7.ReportTitle2 = null;
            menuButton7.ReportTitle2F5 = null;
            menuButton7.ReportTitleF5 = null;
            menuButton7.SortField = "";
            menuButton7.StatusNumber = 0;
            menuButton7.TableName = "";
            menuButton7.Tag = null;
            menuButton7.Text = "Công cụ";
            menuButton7.UserControlViewName = "CongCu";
            menuButton7.Visible = true;
            menuButton8.AccessibleDescription = "";
            menuButton8.CodeForm = "";
            menuButton8.Enabled = true;
            menuButton8.Exe = "";
            menuButton8.HotKey = null;
            menuButton8.Image = global::V6AccountingB.Properties.Resources.settings;
            menuButton8.Info = "";
            menuButton8.ItemID = "";
            menuButton8.KeyFields = null;
            menuButton8.ListTable = "";
            menuButton8.MaChungTu = "";
            menuButton8.NhatKy = "";
            menuButton8.ReportFile = null;
            menuButton8.ReportFileF5 = null;
            menuButton8.ReportTitle = null;
            menuButton8.ReportTitle2 = null;
            menuButton8.ReportTitle2F5 = null;
            menuButton8.ReportTitleF5 = null;
            menuButton8.SortField = "";
            menuButton8.StatusNumber = 0;
            menuButton8.TableName = "";
            menuButton8.Tag = null;
            menuButton8.Text = "Hệ thống";
            menuButton8.UserControlViewName = "HeThong";
            menuButton8.Visible = true;
            menuButton9.AccessibleDescription = "";
            menuButton9.CodeForm = "";
            menuButton9.Enabled = true;
            menuButton9.Exe = "";
            menuButton9.HotKey = null;
            menuButton9.Image = null;
            menuButton9.Info = "";
            menuButton9.ItemID = "";
            menuButton9.KeyFields = null;
            menuButton9.ListTable = "";
            menuButton9.MaChungTu = "";
            menuButton9.NhatKy = "";
            menuButton9.ReportFile = null;
            menuButton9.ReportFileF5 = null;
            menuButton9.ReportTitle = null;
            menuButton9.ReportTitle2 = null;
            menuButton9.ReportTitle2F5 = null;
            menuButton9.ReportTitleF5 = null;
            menuButton9.SortField = "";
            menuButton9.StatusNumber = 0;
            menuButton9.TableName = "";
            menuButton9.Tag = null;
            menuButton9.Text = "Tổng hợp";
            menuButton9.UserControlViewName = "TongHop";
            menuButton9.Visible = true;
            menuButton10.AccessibleDescription = "report";
            menuButton10.CodeForm = "";
            menuButton10.Enabled = true;
            menuButton10.Exe = "";
            menuButton10.HotKey = null;
            menuButton10.Image = global::V6AccountingB.Properties.Resources.reporticone715932;
            menuButton10.Info = "";
            menuButton10.ItemID = "";
            menuButton10.KeyFields = null;
            menuButton10.ListTable = "";
            menuButton10.MaChungTu = "";
            menuButton10.NhatKy = "";
            menuButton10.ReportFile = null;
            menuButton10.ReportFileF5 = null;
            menuButton10.ReportTitle = null;
            menuButton10.ReportTitle2 = null;
            menuButton10.ReportTitle2F5 = null;
            menuButton10.ReportTitleF5 = null;
            menuButton10.SortField = "";
            menuButton10.StatusNumber = 0;
            menuButton10.TableName = "";
            menuButton10.Tag = null;
            menuButton10.Text = "Báo cáo";
            menuButton10.UserControlViewName = "BaoCao";
            menuButton10.Visible = true;
            this.menuMain.Buttons.Add(menuButton1);
            this.menuMain.Buttons.Add(menuButton2);
            this.menuMain.Buttons.Add(menuButton3);
            this.menuMain.Buttons.Add(menuButton4);
            this.menuMain.Buttons.Add(menuButton5);
            this.menuMain.Buttons.Add(menuButton6);
            this.menuMain.Buttons.Add(menuButton7);
            this.menuMain.Buttons.Add(menuButton8);
            this.menuMain.Buttons.Add(menuButton9);
            this.menuMain.Buttons.Add(menuButton10);
            this.menuMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMain.GradientButtonHoverDark = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(192)))), ((int)(((byte)(91)))));
            this.menuMain.GradientButtonHoverLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.menuMain.GradientButtonNormalDark = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(193)))), ((int)(((byte)(140)))));
            this.menuMain.GradientButtonNormalLight = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(240)))), ((int)(((byte)(207)))));
            this.menuMain.GradientButtonSelectedDark = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(150)))), ((int)(((byte)(21)))));
            this.menuMain.GradientButtonSelectedLight = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.menuMain.Name = "menuMain";
            this.menuMain.SelectedButton = null;
            this.menuMain.Size = new System.Drawing.Size(153, 321);
            this.menuMain.TabIndex = 2;
            this.menuMain.Click += new V6Controls.MenuControl.ButtonClickEventHandler(this.menuMain_Click);
            this.menuMain.Load += new System.EventHandler(this.menuMain_Load);
            // 
            // quickMenu1
            // 
            this.quickMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.quickMenu1.BackColor = System.Drawing.Color.AliceBlue;
            this.quickMenu1.Location = new System.Drawing.Point(0, 322);
            this.quickMenu1.Margin = new System.Windows.Forms.Padding(4);
            this.quickMenu1.Name = "quickMenu1";
            this.quickMenu1.Size = new System.Drawing.Size(151, 81);
            this.quickMenu1.TabIndex = 0;
            // 
            // panelMenuShow
            // 
            this.panelMenuShow.AutoSize = true;
            this.panelMenuShow.BackColor = System.Drawing.Color.Gold;
            this.panelMenuShow.Controls.Add(this.lblMenuShow);
            this.panelMenuShow.Location = new System.Drawing.Point(0, 12);
            this.panelMenuShow.Name = "panelMenuShow";
            this.panelMenuShow.Size = new System.Drawing.Size(175, 189);
            this.panelMenuShow.TabIndex = 2;
            // 
            // lblMenuShow
            // 
            this.lblMenuShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuShow.Location = new System.Drawing.Point(0, 0);
            this.lblMenuShow.Name = "lblMenuShow";
            this.lblMenuShow.RenderingMode = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.lblMenuShow.Size = new System.Drawing.Size(175, 188);
            this.lblMenuShow.TabIndex = 0;
            this.lblMenuShow.Tag = "Ẩn hoặc hiện menu chính";
            this.lblMenuShow.Text = "Hide";
            this.lblMenuShow.TextDrawMode = V6Controls.DrawMode.TopBottom;
            this.lblMenuShow.TransparentBackground = false;
            this.lblMenuShow.Click += new System.EventHandler(this.lblMenuShow_Click);
            // 
            // lblMainMessage
            // 
            this.lblMainMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMainMessage.BackColor = System.Drawing.Color.GreenYellow;
            this.lblMainMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMainMessage.Location = new System.Drawing.Point(632, 0);
            this.lblMainMessage.Name = "lblMainMessage";
            this.lblMainMessage.Size = new System.Drawing.Size(209, 39);
            this.lblMainMessage.TabIndex = 3;
            this.lblMainMessage.Text = "Chào mừng đến với phần mềm V6";
            this.lblMainMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.AutoScroll = true;
            this.panelView.BackColor = System.Drawing.Color.Transparent;
            this.panelView.Controls.Add(this.lblCompanyName);
            this.panelView.Controls.Add(this.progressBar1);
            this.panelView.Location = new System.Drawing.Point(171, 13);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(670, 551);
            this.panelView.TabIndex = 0;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCompanyName.Location = new System.Drawing.Point(3, 3);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(656, 49);
            this.lblCompanyName.TabIndex = 1;
            this.lblCompanyName.Text = "CÔNG TY";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCompanyName.UseMnemonic = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 528);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(670, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblV6Message
            // 
            this.lblV6Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblV6Message.BackColor = System.Drawing.Color.Transparent;
            this.lblV6Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblV6Message.ForeColor = System.Drawing.Color.Blue;
            this.lblV6Message.Location = new System.Drawing.Point(0, 0);
            this.lblV6Message.Name = "lblV6Message";
            this.lblV6Message.Size = new System.Drawing.Size(842, 24);
            this.lblV6Message.TabIndex = 4;
            this.lblV6Message.Text = ".....";
            this.lblV6Message.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::V6AccountingB.Properties.Resources.V6Pic4;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(842, 588);
            this.Controls.Add(this.lblMainMessage);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelMenuShow);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.lblV6Message);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "V6Accounting2016.NET - Công ty cổ phần phát triển ứng dụng phần mềm doanh nghiệp " +
    "V6 (V6Business Software)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.lblV6Message, 0);
            this.Controls.SetChildIndex(this.panelView, 0);
            this.Controls.SetChildIndex(this.panelMenuShow, 0);
            this.Controls.SetChildIndex(this.panelMenu, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.lblMainMessage, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMenuShow.ResumeLayout(false);
            this.panelView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label1;
        private MenuControl menuMain;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelMenuShow;
        private V6Controls.V6VeticalLabel lblMenuShow;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus2;
        private V6Label lblMainMessage;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblV6Message;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblHotLine;
        private QuickMenu quickMenu1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeDVCSToolStripMenuItem;
        private System.Windows.Forms.Label lblDVCS;
        private ToolStripMenuItem stickNoteToolStripMenuItem;
        
    }
}