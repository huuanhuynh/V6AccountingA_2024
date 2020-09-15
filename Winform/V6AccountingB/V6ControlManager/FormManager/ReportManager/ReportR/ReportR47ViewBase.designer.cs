using V6Controls.Controls;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    partial class ReportR47ViewBase
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcelTemplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToXmlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.printGridMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToPdfMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInvoiceInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewListInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.cboMauIn = new V6Controls.V6ComboBox();
            this.lblNguoiLapBieu2 = new System.Windows.Forms.Label();
            this.lblNguoiLapBieu = new System.Windows.Forms.Label();
            this.txtM_TEN_NLB2 = new System.Windows.Forms.TextBox();
            this.txtM_TEN_NLB = new System.Windows.Forms.TextBox();
            this.lblMauIn = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtReportTitle = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.panel0 = new System.Windows.Forms.Panel();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rCurrent = new System.Windows.Forms.RadioButton();
            this.rBothLang = new System.Windows.Forms.RadioButton();
            this.rEnglish = new System.Windows.Forms.RadioButton();
            this.rTiengViet = new System.Windows.Forms.RadioButton();
            this.grbTienTe = new System.Windows.Forms.GroupBox();
            this.rNgoaiTe = new System.Windows.Forms.RadioButton();
            this.rTienViet = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.gridViewSummary1 = new V6Controls.Controls.GridViewSummary();
            this.btnSuaLine = new V6Controls.Controls.V6FormButton();
            this.cboBaoCao = new V6Controls.V6ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.btnExport3 = new wyDay.Controls.SplitButton();
            this.gridViewTopFilter1 = new V6Controls.Controls.GridViewTopFilter();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbDieuKienLoc.SuspendLayout();
            this.panel0.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.grbTienTe.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelTemplateMenu,
            this.exportToExcelViewMenu,
            this.exportToExcelMenu,
            this.exportToXmlMenu,
            this.printGridMenu,
            this.viewDataMenu,
            this.exportToPdfMenu,
            this.viewInvoiceInfoMenu,
            this.viewListInfoMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 202);
            // 
            // exportToExcelTemplateMenu
            // 
            this.exportToExcelTemplateMenu.AccessibleDescription = "REPORTM00003";
            this.exportToExcelTemplateMenu.Name = "exportToExcelTemplateMenu";
            this.exportToExcelTemplateMenu.Size = new System.Drawing.Size(208, 22);
            this.exportToExcelTemplateMenu.Text = "Export to Excel (template)";
            this.exportToExcelTemplateMenu.Click += new System.EventHandler(this.exportToExcelTemplateMenu_Click);
            // 
            // exportToExcelViewMenu
            // 
            this.exportToExcelViewMenu.AccessibleDescription = "REPORTM00004";
            this.exportToExcelViewMenu.Name = "exportToExcelViewMenu";
            this.exportToExcelViewMenu.Size = new System.Drawing.Size(208, 22);
            this.exportToExcelViewMenu.Text = "Export to Excel (view)";
            this.exportToExcelViewMenu.Click += new System.EventHandler(this.exportToExcelViewMenu_Click);
            // 
            // exportToExcelMenu
            // 
            this.exportToExcelMenu.AccessibleDescription = "REPORTM00002";
            this.exportToExcelMenu.Name = "exportToExcelMenu";
            this.exportToExcelMenu.Size = new System.Drawing.Size(208, 22);
            this.exportToExcelMenu.Text = "Export to Excel (all)";
            this.exportToExcelMenu.Click += new System.EventHandler(this.exportToExcelMenu_Click);
            // 
            // exportToXmlMenu
            // 
            this.exportToXmlMenu.AccessibleDescription = "REPORTM00005";
            this.exportToXmlMenu.Name = "exportToXmlMenu";
            this.exportToXmlMenu.Size = new System.Drawing.Size(208, 22);
            this.exportToXmlMenu.Text = "Export to xml";
            this.exportToXmlMenu.Click += new System.EventHandler(this.exportToXmlMenu_Click);
            // 
            // printGridMenu
            // 
            this.printGridMenu.AccessibleDescription = "REPORTM00007";
            this.printGridMenu.Name = "printGridMenu";
            this.printGridMenu.Size = new System.Drawing.Size(208, 22);
            this.printGridMenu.Text = "Print Grid";
            this.printGridMenu.Click += new System.EventHandler(this.printGrid_Click);
            // 
            // viewDataMenu
            // 
            this.viewDataMenu.AccessibleDescription = "REPORTM00009";
            this.viewDataMenu.Name = "viewDataMenu";
            this.viewDataMenu.Size = new System.Drawing.Size(208, 22);
            this.viewDataMenu.Text = "View Data";
            this.viewDataMenu.Click += new System.EventHandler(this.viewDataToolStripMenuItem_Click);
            // 
            // exportToPdfMenu
            // 
            this.exportToPdfMenu.AccessibleDescription = "REPORTM00006";
            this.exportToPdfMenu.Name = "exportToPdfMenu";
            this.exportToPdfMenu.Size = new System.Drawing.Size(208, 22);
            this.exportToPdfMenu.Text = "Export to PDF";
            this.exportToPdfMenu.Visible = false;
            this.exportToPdfMenu.Click += new System.EventHandler(this.exportToPdfToolStripMenuItem_Click);
            // 
            // viewInvoiceInfoMenu
            // 
            this.viewInvoiceInfoMenu.AccessibleDescription = "INVOICEM00048";
            this.viewInvoiceInfoMenu.Name = "viewInvoiceInfoMenu";
            this.viewInvoiceInfoMenu.Size = new System.Drawing.Size(208, 22);
            this.viewInvoiceInfoMenu.Text = "Xem thông tin chứng từ";
            this.viewInvoiceInfoMenu.Click += new System.EventHandler(this.viewInvoiceInfoMenu_Click);
            // 
            // viewListInfoMenu
            // 
            this.viewListInfoMenu.AccessibleDescription = "INVOICEM00049";
            this.viewListInfoMenu.Name = "viewListInfoMenu";
            this.viewListInfoMenu.Size = new System.Drawing.Size(208, 22);
            this.viewListInfoMenu.Text = "Xem thông tin danh mục";
            this.viewListInfoMenu.Click += new System.EventHandler(this.viewListInfoMenu_Click);
            // 
            // timerViewReport
            // 
            this.timerViewReport.Tick += new System.EventHandler(this.timerViewReport_Tick);
            // 
            // cboMauIn
            // 
            this.cboMauIn.BackColor = System.Drawing.SystemColors.Window;
            this.cboMauIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMauIn.DropDownWidth = 400;
            this.cboMauIn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMauIn.FormattingEnabled = true;
            this.cboMauIn.Location = new System.Drawing.Point(101, 3);
            this.cboMauIn.Name = "cboMauIn";
            this.cboMauIn.Size = new System.Drawing.Size(200, 21);
            this.cboMauIn.TabIndex = 4;
            this.cboMauIn.Visible = false;
            this.cboMauIn.SelectedIndexChanged += new System.EventHandler(this.cboMauIn_SelectedIndexChanged);
            // 
            // lblNguoiLapBieu2
            // 
            this.lblNguoiLapBieu2.AccessibleDescription = "REPORTL00005";
            this.lblNguoiLapBieu2.AutoSize = true;
            this.lblNguoiLapBieu2.Location = new System.Drawing.Point(328, 201);
            this.lblNguoiLapBieu2.Name = "lblNguoiLapBieu2";
            this.lblNguoiLapBieu2.Size = new System.Drawing.Size(84, 13);
            this.lblNguoiLapBieu2.TabIndex = 11;
            this.lblNguoiLapBieu2.Text = "Người lập biểu 2";
            this.lblNguoiLapBieu2.Visible = false;
            // 
            // lblNguoiLapBieu
            // 
            this.lblNguoiLapBieu.AccessibleDescription = "REPORTL00004";
            this.lblNguoiLapBieu.AutoSize = true;
            this.lblNguoiLapBieu.Location = new System.Drawing.Point(328, 175);
            this.lblNguoiLapBieu.Name = "lblNguoiLapBieu";
            this.lblNguoiLapBieu.Size = new System.Drawing.Size(75, 13);
            this.lblNguoiLapBieu.TabIndex = 9;
            this.lblNguoiLapBieu.Text = "Người lập biểu";
            this.lblNguoiLapBieu.Visible = false;
            // 
            // txtM_TEN_NLB2
            // 
            this.txtM_TEN_NLB2.AccessibleName = "M_TEN_NLB2";
            this.txtM_TEN_NLB2.Location = new System.Drawing.Point(420, 198);
            this.txtM_TEN_NLB2.Name = "txtM_TEN_NLB2";
            this.txtM_TEN_NLB2.Size = new System.Drawing.Size(194, 20);
            this.txtM_TEN_NLB2.TabIndex = 12;
            this.txtM_TEN_NLB2.Visible = false;
            // 
            // txtM_TEN_NLB
            // 
            this.txtM_TEN_NLB.AccessibleName = "M_TEN_NLB";
            this.txtM_TEN_NLB.Location = new System.Drawing.Point(420, 172);
            this.txtM_TEN_NLB.Name = "txtM_TEN_NLB";
            this.txtM_TEN_NLB.Size = new System.Drawing.Size(194, 20);
            this.txtM_TEN_NLB.TabIndex = 10;
            this.txtM_TEN_NLB.Visible = false;
            // 
            // lblMauIn
            // 
            this.lblMauIn.AccessibleDescription = "REPORTL00003";
            this.lblMauIn.AutoSize = true;
            this.lblMauIn.Location = new System.Drawing.Point(9, 6);
            this.lblMauIn.Name = "lblMauIn";
            this.lblMauIn.Size = new System.Drawing.Size(81, 13);
            this.lblMauIn.TabIndex = 3;
            this.lblMauIn.Text = "Mẫu in báo cáo";
            this.lblMauIn.Visible = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AccessibleDescription = "REPORTL00002";
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(328, 149);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(86, 13);
            this.lblTieuDe.TabIndex = 1;
            this.lblTieuDe.Text = "Tiêu đề báo cáo";
            this.lblTieuDe.Visible = false;
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportTitle.Location = new System.Drawing.Point(420, 146);
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new System.Drawing.Size(525, 20);
            this.txtReportTitle.TabIndex = 2;
            this.txtReportTitle.Visible = false;
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
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Control_S = true;
            this.dataGridView1.Location = new System.Drawing.Point(307, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(664, 576);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.AccessibleDescription = "REPORTL00006";
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDieuKienLoc.Controls.Add(this.panel0);
            this.grbDieuKienLoc.Controls.Add(this.panel1);
            this.grbDieuKienLoc.Location = new System.Drawing.Point(0, 103);
            this.grbDieuKienLoc.Name = "grbDieuKienLoc";
            this.grbDieuKienLoc.Size = new System.Drawing.Size(302, 548);
            this.grbDieuKienLoc.TabIndex = 0;
            this.grbDieuKienLoc.TabStop = false;
            this.grbDieuKienLoc.Text = "Conditional option (Điều Kiện Lọc)";
            // 
            // panel0
            // 
            this.panel0.Controls.Add(this.grbNgonNgu);
            this.panel0.Controls.Add(this.grbTienTe);
            this.panel0.Location = new System.Drawing.Point(1, 14);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(292, 80);
            this.panel0.TabIndex = 32;
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.AccessibleDescription = "REPORTL00010";
            this.grbNgonNgu.Controls.Add(this.rCurrent);
            this.grbNgonNgu.Controls.Add(this.rBothLang);
            this.grbNgonNgu.Controls.Add(this.rEnglish);
            this.grbNgonNgu.Controls.Add(this.rTiengViet);
            this.grbNgonNgu.Location = new System.Drawing.Point(4, 41);
            this.grbNgonNgu.Name = "grbNgonNgu";
            this.grbNgonNgu.Size = new System.Drawing.Size(285, 35);
            this.grbNgonNgu.TabIndex = 1;
            this.grbNgonNgu.TabStop = false;
            this.grbNgonNgu.Text = "Ngôn ngữ bc (Rpt Language)";
            // 
            // rCurrent
            // 
            this.rCurrent.AccessibleDescription = "REPORTR00007";
            this.rCurrent.AutoSize = true;
            this.rCurrent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rCurrent.Location = new System.Drawing.Point(219, 13);
            this.rCurrent.Name = "rCurrent";
            this.rCurrent.Size = new System.Drawing.Size(59, 17);
            this.rCurrent.TabIndex = 2;
            this.rCurrent.Text = "Current";
            this.rCurrent.UseVisualStyleBackColor = true;
            this.rCurrent.CheckedChanged += new System.EventHandler(this.rbtLanguage_CheckedChanged);
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
            // rTiengViet
            // 
            this.rTiengViet.AccessibleDescription = "REPORTR00004";
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
            // grbTienTe
            // 
            this.grbTienTe.AccessibleDescription = "REPORTL00007";
            this.grbTienTe.Controls.Add(this.rNgoaiTe);
            this.grbTienTe.Controls.Add(this.rTienViet);
            this.grbTienTe.Location = new System.Drawing.Point(4, 5);
            this.grbTienTe.Name = "grbTienTe";
            this.grbTienTe.Size = new System.Drawing.Size(285, 35);
            this.grbTienTe.TabIndex = 0;
            this.grbTienTe.TabStop = false;
            this.grbTienTe.Text = "Tiền tệ";
            // 
            // rNgoaiTe
            // 
            this.rNgoaiTe.AccessibleDescription = "REPORTR00002";
            this.rNgoaiTe.AccessibleName = "Tiếng Việt";
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
            this.rTienViet.AccessibleName = "English";
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 446);
            this.panel1.TabIndex = 0;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = ".";
            this.btnSuaMau.Enabled = false;
            this.btnSuaMau.Image = global::V6ControlManager.Properties.Resources.Edit24;
            this.btnSuaMau.Location = new System.Drawing.Point(420, -1);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(30, 30);
            this.btnSuaMau.TabIndex = 9;
            this.toolTipV6FormControl.SetToolTip(this.btnSuaMau, "Sửa Mẫu");
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Visible = false;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
            // 
            // chkHienTatCa
            // 
            this.chkHienTatCa.AccessibleDescription = "REPORTC00001";
            this.chkHienTatCa.AutoSize = true;
            this.chkHienTatCa.Enabled = false;
            this.chkHienTatCa.Location = new System.Drawing.Point(317, 5);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 8;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.Visible = false;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // gridViewSummary1
            // 
            this.gridViewSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary1.DataGridView = this.dataGridView1;
            this.gridViewSummary1.Location = new System.Drawing.Point(307, 628);
            this.gridViewSummary1.Name = "gridViewSummary1";
            this.gridViewSummary1.Size = new System.Drawing.Size(664, 23);
            this.gridViewSummary1.SumCondition = null;
            this.gridViewSummary1.TabIndex = 0;
            // 
            // btnSuaLine
            // 
            this.btnSuaLine.AccessibleDescription = ".";
            this.btnSuaLine.Image = global::V6ControlManager.Properties.Resources.LineEdit24;
            this.btnSuaLine.Location = new System.Drawing.Point(450, -1);
            this.btnSuaLine.Name = "btnSuaLine";
            this.btnSuaLine.Size = new System.Drawing.Size(30, 30);
            this.btnSuaLine.TabIndex = 10;
            this.toolTipV6FormControl.SetToolTip(this.btnSuaLine, "Sửa line");
            this.btnSuaLine.UseVisualStyleBackColor = true;
            this.btnSuaLine.Visible = false;
            this.btnSuaLine.Click += new System.EventHandler(this.btnSuaLine_Click);
            // 
            // cboBaoCao
            // 
            this.cboBaoCao.BackColor = System.Drawing.SystemColors.Window;
            this.cboBaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaoCao.DropDownWidth = 400;
            this.cboBaoCao.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBaoCao.FormattingEnabled = true;
            this.cboBaoCao.Location = new System.Drawing.Point(101, 30);
            this.cboBaoCao.Name = "cboBaoCao";
            this.cboBaoCao.Size = new System.Drawing.Size(200, 21);
            this.cboBaoCao.TabIndex = 6;
            this.cboBaoCao.SelectedIndexChanged += new System.EventHandler(this.cboBaoCao_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "REPORTL00018";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Báo cáo";
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "REPORTB00006";
            this.btnIn.AccessibleName = "";
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print;
            this.btnIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnIn.Location = new System.Drawing.Point(123, 57);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(110, 40);
            this.btnIn.TabIndex = 2;
            this.btnIn.Text = "&In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 57);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(110, 40);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = ".";
            this.btnSuaTTMauBC.Image = global::V6ControlManager.Properties.Resources.Setting24;
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(360, -1);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnSuaTTMauBC.TabIndex = 7;
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Visible = false;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = ".";
            this.btnThemMauBC.Image = global::V6ControlManager.Properties.Resources.SettingAdd24;
            this.btnThemMauBC.Location = new System.Drawing.Point(390, -1);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnThemMauBC.TabIndex = 8;
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Visible = false;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // btnExport3
            // 
            this.btnExport3.AccessibleDescription = ".";
            this.btnExport3.AutoSize = true;
            this.btnExport3.ContextMenuStrip = this.contextMenuStrip1;
            this.btnExport3.Image = global::V6ControlManager.Properties.Resources.Export24;
            this.btnExport3.Location = new System.Drawing.Point(480, -1);
            this.btnExport3.Name = "btnExport3";
            this.btnExport3.Size = new System.Drawing.Size(50, 30);
            this.btnExport3.SplitMenuStrip = this.contextMenuStrip1;
            this.btnExport3.TabIndex = 11;
            this.btnExport3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport3.UseVisualStyleBackColor = true;
            this.btnExport3.Click += new System.EventHandler(this.btnExport3_Click);
            // 
            // gridViewTopFilter1
            // 
            this.gridViewTopFilter1.Auto = false;
            this.gridViewTopFilter1.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewTopFilter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewTopFilter1.DataGridView = this.dataGridView1;
            this.gridViewTopFilter1.Location = new System.Drawing.Point(307, 30);
            this.gridViewTopFilter1.Name = "gridViewTopFilter1";
            this.gridViewTopFilter1.Size = new System.Drawing.Size(664, 22);
            this.gridViewTopFilter1.TabIndex = 1;
            // 
            // ReportR47ViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridViewTopFilter1);
            this.Controls.Add(this.btnExport3);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaLine);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.cboBaoCao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.cboMauIn);
            this.Controls.Add(this.lblNguoiLapBieu2);
            this.Controls.Add(this.lblNguoiLapBieu);
            this.Controls.Add(this.txtM_TEN_NLB2);
            this.Controls.Add(this.txtM_TEN_NLB);
            this.Controls.Add(this.lblMauIn);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.txtReportTitle);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grbDieuKienLoc);
            this.Controls.Add(this.gridViewSummary1);
            this.FilterType = "4";
            this.Name = "ReportR47ViewBase";
            this.Size = new System.Drawing.Size(974, 654);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.ReportR47ViewBase_VisibleChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbDieuKienLoc.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelMenu;
        private System.Windows.Forms.ToolStripMenuItem printGridMenu;
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
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblNguoiLapBieu2;
        private System.Windows.Forms.Label lblNguoiLapBieu;
        private System.Windows.Forms.TextBox txtM_TEN_NLB2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Label lblMauIn;
        private V6Controls.V6ComboBox cboMauIn;
        private V6FormButton btnSuaMau;
        private V6FormButton btnSuaTTMauBC;
        private V6FormButton btnThemMauBC;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem viewDataMenu;
        private V6Controls.V6CheckBox chkHienTatCa;
        private System.Windows.Forms.ToolStripMenuItem exportToXmlMenu;
        private GridViewSummary gridViewSummary1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelViewMenu;
        public System.Windows.Forms.TextBox txtReportTitle;
        private V6FormButton btnSuaLine;
        private System.Windows.Forms.ToolStripMenuItem exportToPdfMenu;
        private V6Controls.V6ComboBox cboBaoCao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rCurrent;
        private wyDay.Controls.SplitButton btnExport3;
        private GridViewTopFilter gridViewTopFilter1;
        private System.Windows.Forms.ToolStripMenuItem viewInvoiceInfoMenu;
        private System.Windows.Forms.ToolStripMenuItem viewListInfoMenu;




    }
}