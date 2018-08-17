using V6Controls;
using V6Controls.Controls;

namespace V6ControlManager.FormManager.ReportManager.ReportD
{
    partial class ReportDViewBase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcelTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelView = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtNumber = new V6Controls.V6ColorTextBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.panel0 = new System.Windows.Forms.Panel();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rTiengViet = new System.Windows.Forms.RadioButton();
            this.rBothLang = new System.Windows.Forms.RadioButton();
            this.rEnglish = new System.Windows.Forms.RadioButton();
            this.grbTienTe = new System.Windows.Forms.GroupBox();
            this.rNgoaiTe = new System.Windows.Forms.RadioButton();
            this.rTienViet = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.txtReportTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtM_TEN_NLB2 = new System.Windows.Forms.TextBox();
            this.txtM_TEN_NLB = new System.Windows.Forms.TextBox();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.cboMauIn = new V6Controls.V6ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.gridViewSummary1 = new V6Controls.Controls.GridViewSummary();
            this.exportToPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.grbDieuKienLoc.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel0.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.grbTienTe.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Control_S = true;
            this.dataGridView1.Location = new System.Drawing.Point(307, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(662, 335);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelTemplate,
            this.exportToExcelView,
            this.exportToExcel,
            this.exportToXmlToolStripMenuItem,
            this.printGrid,
            this.viewDataToolStripMenuItem,
            this.exportToPdfToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 180);
            // 
            // exportToExcelTemplate
            // 
            this.exportToExcelTemplate.Name = "exportToExcelTemplate";
            this.exportToExcelTemplate.Size = new System.Drawing.Size(208, 22);
            this.exportToExcelTemplate.Text = "Export to Excel (template)";
            this.exportToExcelTemplate.Click += new System.EventHandler(this.exportToExcelTemplate_Click);
            // 
            // exportToExcelView
            // 
            this.exportToExcelView.Name = "exportToExcelView";
            this.exportToExcelView.Size = new System.Drawing.Size(208, 22);
            this.exportToExcelView.Text = "Export to Excel (view)";
            this.exportToExcelView.Click += new System.EventHandler(this.exportToExcelView_Click);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(208, 22);
            this.exportToExcel.Text = "Export to Excel (all)";
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // exportToXmlToolStripMenuItem
            // 
            this.exportToXmlToolStripMenuItem.Name = "exportToXmlToolStripMenuItem";
            this.exportToXmlToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportToXmlToolStripMenuItem.Text = "Export to xml";
            this.exportToXmlToolStripMenuItem.Click += new System.EventHandler(this.exportToXmlToolStripMenuItem_Click);
            // 
            // printGrid
            // 
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(208, 22);
            this.printGrid.Text = "Print Grid";
            this.printGrid.Click += new System.EventHandler(this.printGrid_Click);
            // 
            // viewDataToolStripMenuItem
            // 
            this.viewDataToolStripMenuItem.Name = "viewDataToolStripMenuItem";
            this.viewDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.viewDataToolStripMenuItem.Text = "ViewData";
            this.viewDataToolStripMenuItem.Click += new System.EventHandler(this.viewDataToolStripMenuItem_Click);
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.AccessibleDescription = "REPORTL00011";
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDieuKienLoc.Controls.Add(this.panel4);
            this.grbDieuKienLoc.Controls.Add(this.btnIn);
            this.grbDieuKienLoc.Controls.Add(this.btnHuy);
            this.grbDieuKienLoc.Controls.Add(this.btnNhan);
            this.grbDieuKienLoc.Controls.Add(this.panel0);
            this.grbDieuKienLoc.Controls.Add(this.panel1);
            this.grbDieuKienLoc.Location = new System.Drawing.Point(2, 64);
            this.grbDieuKienLoc.Name = "grbDieuKienLoc";
            this.grbDieuKienLoc.Size = new System.Drawing.Size(299, 587);
            this.grbDieuKienLoc.TabIndex = 0;
            this.grbDieuKienLoc.TabStop = false;
            this.grbDieuKienLoc.Text = "Conditional option (Điều Kiện Lọc)";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtNumber);
            this.panel4.Controls.Add(this.btnLast);
            this.panel4.Controls.Add(this.btnNext);
            this.panel4.Controls.Add(this.btnPrevious);
            this.panel4.Controls.Add(this.btnFirst);
            this.panel4.Location = new System.Drawing.Point(10, 146);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(187, 25);
            this.panel4.TabIndex = 34;
            // 
            // txtNumber
            // 
            this.txtNumber.AccessibleDescription = "Nhập số và bấm enter để nhận báo cáo";
            this.txtNumber.AccessibleName = "Phần báo cáo";
            this.txtNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtNumber.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNumber.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNumber.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNumber.HoverColor = System.Drawing.Color.Yellow;
            this.txtNumber.LeaveColor = System.Drawing.Color.White;
            this.txtNumber.LimitCharacters = "0123456789";
            this.txtNumber.Location = new System.Drawing.Point(71, 2);
            this.txtNumber.MaxLength = 5;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(43, 20);
            this.txtNumber.TabIndex = 2;
            this.txtNumber.UseSendTabOnEnter = false;
            this.txtNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            this.txtNumber.Leave += new System.EventHandler(this.txtNumber_Leave);
            // 
            // btnLast
            // 
            this.btnLast.AccessibleDescription = "Hiện phần báo cáo cuối cùng";
            this.btnLast.AccessibleName = "Cuối";
            this.btnLast.Location = new System.Drawing.Point(155, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(29, 23);
            this.btnLast.TabIndex = 4;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.AccessibleDescription = "Hiện phần báo cáo tiếp theo";
            this.btnNext.AccessibleName = "Tới";
            this.btnNext.Location = new System.Drawing.Point(120, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.AccessibleDescription = "Hiện phần báo cáo trước";
            this.btnPrevious.AccessibleName = "Lui";
            this.btnPrevious.Location = new System.Drawing.Point(36, 1);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(29, 23);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.AccessibleDescription = "Hiện phần báo cáo đầu tiên";
            this.btnFirst.AccessibleName = "Đầu";
            this.btnFirst.Location = new System.Drawing.Point(1, 1);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(29, 23);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "REPORTB00006";
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print24;
            this.btnIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnIn.Location = new System.Drawing.Point(94, 100);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(88, 40);
            this.btnIn.TabIndex = 33;
            this.btnIn.Tag = "P, Control";
            this.btnIn.Text = "&In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(182, 100);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 100);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // panel0
            // 
            this.panel0.Controls.Add(this.grbNgonNgu);
            this.panel0.Controls.Add(this.grbTienTe);
            this.panel0.Location = new System.Drawing.Point(6, 14);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(234, 80);
            this.panel0.TabIndex = 32;
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.AccessibleDescription = "REPORTL00010";
            this.grbNgonNgu.Controls.Add(this.rTiengViet);
            this.grbNgonNgu.Controls.Add(this.rBothLang);
            this.grbNgonNgu.Controls.Add(this.rEnglish);
            this.grbNgonNgu.Location = new System.Drawing.Point(4, 41);
            this.grbNgonNgu.Name = "grbNgonNgu";
            this.grbNgonNgu.Size = new System.Drawing.Size(224, 35);
            this.grbNgonNgu.TabIndex = 1;
            this.grbNgonNgu.TabStop = false;
            this.grbNgonNgu.Text = "Ngôn ngữ bc (Rpt Language)";
            // 
            // rTiengViet
            // 
            this.rTiengViet.AccessibleDescription = "REPORTR00004";
            this.rTiengViet.AccessibleName = "Tiếng Việt";
            this.rTiengViet.AutoSize = true;
            this.rTiengViet.Checked = true;
            this.rTiengViet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rTiengViet.Location = new System.Drawing.Point(6, 13);
            this.rTiengViet.Name = "rTiengViet";
            this.rTiengViet.Size = new System.Drawing.Size(73, 17);
            this.rTiengViet.TabIndex = 1;
            this.rTiengViet.TabStop = true;
            this.rTiengViet.Text = "Tiếng Việt";
            this.rTiengViet.UseVisualStyleBackColor = true;
            this.rTiengViet.CheckedChanged += new System.EventHandler(this.rbtLanguage_CheckedChanged);
            // 
            // rBothLang
            // 
            this.rBothLang.AccessibleDescription = "REPORTR00006";
            this.rBothLang.AccessibleName = "English";
            this.rBothLang.AutoSize = true;
            this.rBothLang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rBothLang.Location = new System.Drawing.Point(142, 13);
            this.rBothLang.Name = "rBothLang";
            this.rBothLang.Size = new System.Drawing.Size(71, 17);
            this.rBothLang.TabIndex = 0;
            this.rBothLang.Text = "Song ngữ";
            this.rBothLang.UseVisualStyleBackColor = true;
            this.rBothLang.CheckedChanged += new System.EventHandler(this.rbtLanguage_CheckedChanged);
            // 
            // rEnglish
            // 
            this.rEnglish.AccessibleDescription = "REPORTR00005";
            this.rEnglish.AccessibleName = "English";
            this.rEnglish.AutoSize = true;
            this.rEnglish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rEnglish.Location = new System.Drawing.Point(83, 13);
            this.rEnglish.Name = "rEnglish";
            this.rEnglish.Size = new System.Drawing.Size(59, 17);
            this.rEnglish.TabIndex = 0;
            this.rEnglish.Text = "English";
            this.rEnglish.UseVisualStyleBackColor = true;
            this.rEnglish.CheckedChanged += new System.EventHandler(this.rbtLanguage_CheckedChanged);
            // 
            // grbTienTe
            // 
            this.grbTienTe.AccessibleDescription = "REPORTL00007";
            this.grbTienTe.Controls.Add(this.rNgoaiTe);
            this.grbTienTe.Controls.Add(this.rTienViet);
            this.grbTienTe.Location = new System.Drawing.Point(4, 5);
            this.grbTienTe.Name = "grbTienTe";
            this.grbTienTe.Size = new System.Drawing.Size(224, 35);
            this.grbTienTe.TabIndex = 1;
            this.grbTienTe.TabStop = false;
            this.grbTienTe.Text = "Tiền tệ";
            // 
            // rNgoaiTe
            // 
            this.rNgoaiTe.AccessibleDescription = "REPORTR00002";
            this.rNgoaiTe.AutoSize = true;
            this.rNgoaiTe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rNgoaiTe.Location = new System.Drawing.Point(83, 13);
            this.rNgoaiTe.Name = "rNgoaiTe";
            this.rNgoaiTe.Size = new System.Drawing.Size(65, 17);
            this.rNgoaiTe.TabIndex = 1;
            this.rNgoaiTe.Text = "Ngoại tệ";
            this.rNgoaiTe.UseVisualStyleBackColor = true;
            // 
            // rTienViet
            // 
            this.rTienViet.AccessibleDescription = "REPORTR00001";
            this.rTienViet.AutoSize = true;
            this.rTienViet.Checked = true;
            this.rTienViet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rTienViet.Location = new System.Drawing.Point(6, 13);
            this.rTienViet.Name = "rTienViet";
            this.rTienViet.Size = new System.Drawing.Size(67, 17);
            this.rTienViet.TabIndex = 0;
            this.rTienViet.TabStop = true;
            this.rTienViet.Text = "Tiền Việt";
            this.rTienViet.UseVisualStyleBackColor = true;
            this.rTienViet.CheckedChanged += new System.EventHandler(this.rbtTienTe_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 404);
            this.panel1.TabIndex = 0;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(307, 425);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(662, 224);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer1.DoubleClick += new System.EventHandler(this.crystalReportViewer1_DoubleClick);
            // 
            // timerViewReport
            // 
            this.timerViewReport.Tick += new System.EventHandler(this.timerViewReport_Tick);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportTitle.Location = new System.Drawing.Point(101, 12);
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new System.Drawing.Size(861, 20);
            this.txtReportTitle.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "REPORTL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tiêu đề báo cáo";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Người lập biểu 2";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "REPORTL00004";
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(555, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Người lập biểu";
            // 
            // txtM_TEN_NLB2
            // 
            this.txtM_TEN_NLB2.AccessibleName = "M_TEN_NLB2";
            this.txtM_TEN_NLB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB2.Location = new System.Drawing.Point(847, 38);
            this.txtM_TEN_NLB2.Name = "txtM_TEN_NLB2";
            this.txtM_TEN_NLB2.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB2.TabIndex = 7;
            // 
            // txtM_TEN_NLB
            // 
            this.txtM_TEN_NLB.AccessibleName = "M_TEN_NLB";
            this.txtM_TEN_NLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB.Location = new System.Drawing.Point(636, 38);
            this.txtM_TEN_NLB.Name = "txtM_TEN_NLB";
            this.txtM_TEN_NLB.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB.TabIndex = 8;
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = "REPORTB00003";
            this.btnSuaMau.Enabled = false;
            this.btnSuaMau.Location = new System.Drawing.Point(394, 36);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(41, 23);
            this.btnSuaMau.TabIndex = 21;
            this.btnSuaMau.Text = "Sửa";
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = "REPORTB00001";
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(306, 36);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMauBC.TabIndex = 22;
            this.btnSuaTTMauBC.Text = "Sửa tt";
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = "REPORTB00002";
            this.btnThemMauBC.Location = new System.Drawing.Point(349, 36);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnThemMauBC.TabIndex = 23;
            this.btnThemMauBC.Text = "Thêm";
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // cboMauIn
            // 
            this.cboMauIn.BackColor = System.Drawing.SystemColors.Window;
            this.cboMauIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMauIn.DropDownWidth = 400;
            this.cboMauIn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMauIn.FormattingEnabled = true;
            this.cboMauIn.Location = new System.Drawing.Point(101, 37);
            this.cboMauIn.Name = "cboMauIn";
            this.cboMauIn.Size = new System.Drawing.Size(200, 21);
            this.cboMauIn.TabIndex = 20;
            this.cboMauIn.SelectedIndexChanged += new System.EventHandler(this.cboMauIn_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "REPORTL00003";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Mẫu in báo cáo";
            // 
            // chkHienTatCa
            // 
            this.chkHienTatCa.AccessibleDescription = "REPORTC00001";
            this.chkHienTatCa.AutoSize = true;
            this.chkHienTatCa.Enabled = false;
            this.chkHienTatCa.Location = new System.Drawing.Point(441, 41);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 35;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // gridViewSummary1
            // 
            this.gridViewSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary1.DataGridView = this.dataGridView1;
            this.gridViewSummary1.Location = new System.Drawing.Point(307, 399);
            this.gridViewSummary1.Name = "gridViewSummary1";
            this.gridViewSummary1.Size = new System.Drawing.Size(662, 23);
            this.gridViewSummary1.SumCondition = null;
            this.gridViewSummary1.TabIndex = 0;
            // 
            // exportToPdfToolStripMenuItem
            // 
            this.exportToPdfToolStripMenuItem.Name = "exportToPdfToolStripMenuItem";
            this.exportToPdfToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportToPdfToolStripMenuItem.Text = "Export to PDF";
            this.exportToPdfToolStripMenuItem.Click += new System.EventHandler(this.exportToPdfToolStripMenuItem_Click);
            // 
            // ReportDViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridViewSummary1);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.cboMauIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtM_TEN_NLB2);
            this.Controls.Add(this.txtM_TEN_NLB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReportTitle);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grbDieuKienLoc);
            this.FilterType = "4";
            this.Name = "ReportDViewBase";
            this.Size = new System.Drawing.Size(974, 654);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.ReportRViewBase_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.grbDieuKienLoc.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel0.ResumeLayout(false);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.grbTienTe.ResumeLayout(false);
            this.grbTienTe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grbDieuKienLoc;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        private System.Windows.Forms.GroupBox grbTienTe;
        private System.Windows.Forms.RadioButton rNgoaiTe;
        private System.Windows.Forms.RadioButton rTienViet;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.Timer timerViewReport;
        private System.Windows.Forms.GroupBox grbNgonNgu;
        private System.Windows.Forms.RadioButton rTiengViet;
        private System.Windows.Forms.RadioButton rEnglish;
        private System.Windows.Forms.RadioButton rBothLang;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TextBox txtReportTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Panel panel4;
        private V6Controls.V6ColorTextBox txtNumber;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private V6FormButton btnSuaMau;
        private V6FormButton btnSuaTTMauBC;
        private V6FormButton btnThemMauBC;
        private V6ComboBox cboMauIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelTemplate;
        private System.Windows.Forms.ToolStripMenuItem viewDataToolStripMenuItem;
        private V6Controls.V6CheckBox chkHienTatCa;
        private System.Windows.Forms.ToolStripMenuItem exportToXmlToolStripMenuItem;
        private GridViewSummary gridViewSummary1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelView;
        private System.Windows.Forms.ToolStripMenuItem exportToPdfToolStripMenuItem;




    }
}