﻿using System.Windows.Forms;
using V6Controls.Controls;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    partial class ReportTreeView_DX
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
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportTreeView_DX));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcelTemplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToXmlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.printGridMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToPdfMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToWordMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInvoiceInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewListInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEXCELXtraMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEXCELDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToHTMLMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.cboMauIn = new V6Controls.V6ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtM_TEN_NLB2 = new System.Windows.Forms.TextBox();
            this.txtM_TEN_NLB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReportTitle = new System.Windows.Forms.TextBox();
            this.treeListViewAuto1 = new V6Controls.Controls.TreeView.TreeListViewAuto();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.panel0 = new System.Windows.Forms.Panel();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rCurrent = new System.Windows.Forms.RadioButton();
            this.rTiengViet = new System.Windows.Forms.RadioButton();
            this.rBothLang = new System.Windows.Forms.RadioButton();
            this.rEnglish = new System.Windows.Forms.RadioButton();
            this.grbTienTe = new System.Windows.Forms.GroupBox();
            this.rNgoaiTe = new System.Windows.Forms.RadioButton();
            this.rTienViet = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.btnExport3 = new wyDay.Controls.SplitButton();
            this.btnSuaLine = new V6Controls.Controls.V6FormButton();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.lblSummary = new System.Windows.Forms.Label();
            this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.copyMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditPara = new V6Controls.Controls.V6FormButton();
            this.contextMenuStrip1.SuspendLayout();
            this.grbDieuKienLoc.SuspendLayout();
            this.panel0.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.grbTienTe.SuspendLayout();
            this.copyMenuStrip1.SuspendLayout();
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
            this.exportToWordMenu,
            this.viewInvoiceInfoMenu,
            this.viewListInfoMenu,
            this.exportEXCELXtraMenu,
            this.exportEXCELDataMenu,
            this.exportToHTMLMenu,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 334);
            // 
            // exportToExcelTemplateMenu
            // 
            this.exportToExcelTemplateMenu.AccessibleDescription = "REPORTM00003";
            this.exportToExcelTemplateMenu.Name = "exportToExcelTemplateMenu";
            this.exportToExcelTemplateMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToExcelTemplateMenu.Text = "Export to Excel (template)";
            this.exportToExcelTemplateMenu.Click += new System.EventHandler(this.exportToExcelTemplateMenu_Click);
            // 
            // exportToExcelViewMenu
            // 
            this.exportToExcelViewMenu.AccessibleDescription = "REPORTM00004";
            this.exportToExcelViewMenu.Name = "exportToExcelViewMenu";
            this.exportToExcelViewMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToExcelViewMenu.Text = "Export to Excel (view)";
            this.exportToExcelViewMenu.Click += new System.EventHandler(this.exportToExcelView_Click);
            // 
            // exportToExcelMenu
            // 
            this.exportToExcelMenu.AccessibleDescription = "REPORTM00002";
            this.exportToExcelMenu.Name = "exportToExcelMenu";
            this.exportToExcelMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToExcelMenu.Text = "Export to Excel (all)";
            this.exportToExcelMenu.Click += new System.EventHandler(this.exportToExcelMenu_Click);
            // 
            // exportToXmlMenu
            // 
            this.exportToXmlMenu.AccessibleDescription = "REPORTM00005";
            this.exportToXmlMenu.Name = "exportToXmlMenu";
            this.exportToXmlMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToXmlMenu.Text = "Export to xml";
            this.exportToXmlMenu.Click += new System.EventHandler(this.exportToXmlMenu_Click);
            // 
            // printGridMenu
            // 
            this.printGridMenu.AccessibleDescription = "REPORTM00007";
            this.printGridMenu.Name = "printGridMenu";
            this.printGridMenu.Size = new System.Drawing.Size(210, 22);
            this.printGridMenu.Text = "Print Grid";
            // 
            // viewDataMenu
            // 
            this.viewDataMenu.AccessibleDescription = "REPORTM00009";
            this.viewDataMenu.Name = "viewDataMenu";
            this.viewDataMenu.Size = new System.Drawing.Size(210, 22);
            this.viewDataMenu.Text = "View Data";
            this.viewDataMenu.Click += new System.EventHandler(this.viewDataMenu_Click);
            // 
            // exportToPdfMenu
            // 
            this.exportToPdfMenu.AccessibleDescription = "REPORTM00006";
            this.exportToPdfMenu.Name = "exportToPdfMenu";
            this.exportToPdfMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToPdfMenu.Text = "Export to PDF";
            this.exportToPdfMenu.Click += new System.EventHandler(this.exportToPdfMenu_Click);
            // 
            // exportToWordMenu
            // 
            this.exportToWordMenu.Name = "exportToWordMenu";
            this.exportToWordMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToWordMenu.Text = "Export to Word";
            this.exportToWordMenu.Click += new System.EventHandler(this.exportToWordMenu_Click);
            // 
            // viewInvoiceInfoMenu
            // 
            this.viewInvoiceInfoMenu.AccessibleDescription = "INVOICEM00048";
            this.viewInvoiceInfoMenu.Name = "viewInvoiceInfoMenu";
            this.viewInvoiceInfoMenu.Size = new System.Drawing.Size(210, 22);
            this.viewInvoiceInfoMenu.Text = "Xem thông tin chứng từ";
            this.viewInvoiceInfoMenu.Click += new System.EventHandler(this.viewInvoiceInfoMenu_Click);
            // 
            // viewListInfoMenu
            // 
            this.viewListInfoMenu.AccessibleDescription = "INVOICEM00049";
            this.viewListInfoMenu.Name = "viewListInfoMenu";
            this.viewListInfoMenu.Size = new System.Drawing.Size(210, 22);
            this.viewListInfoMenu.Text = "Xem thông tin danh mục";
            this.viewListInfoMenu.Click += new System.EventHandler(this.viewListInfoMenu_Click);
            // 
            // exportEXCELXtraMenu
            // 
            this.exportEXCELXtraMenu.Name = "exportEXCELXtraMenu";
            this.exportEXCELXtraMenu.Size = new System.Drawing.Size(210, 22);
            this.exportEXCELXtraMenu.Text = "Export EXCEL (Xtra)";
            this.exportEXCELXtraMenu.Click += new System.EventHandler(this.exportEXCELXtraMenu_Click);
            // 
            // exportEXCELDataMenu
            // 
            this.exportEXCELDataMenu.Name = "exportEXCELDataMenu";
            this.exportEXCELDataMenu.Size = new System.Drawing.Size(210, 22);
            this.exportEXCELDataMenu.Text = "Export EXCEL (Data)";
            this.exportEXCELDataMenu.Click += new System.EventHandler(this.exportEXCELDataMenu_Click);
            // 
            // exportToHTMLMenu
            // 
            this.exportToHTMLMenu.Name = "exportToHTMLMenu";
            this.exportToHTMLMenu.Size = new System.Drawing.Size(210, 22);
            this.exportToHTMLMenu.Text = "Export to HTML";
            this.exportToHTMLMenu.Click += new System.EventHandler(this.exportReportToHtmlMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = "INVOICEM00048";
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
            this.toolStripMenuItem1.Text = "Xem thông tin chứng từ";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.viewInvoiceInfoMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.AccessibleDescription = "INVOICEM00049";
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 22);
            this.toolStripMenuItem2.Text = "Xem thông tin danh mục";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.viewListInfoMenu_Click);
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
            this.cboMauIn.Location = new System.Drawing.Point(101, 38);
            this.cboMauIn.Name = "cboMauIn";
            this.cboMauIn.Size = new System.Drawing.Size(200, 21);
            this.cboMauIn.TabIndex = 4;
            this.cboMauIn.SelectedIndexChanged += new System.EventHandler(this.cboMauIn_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "REPORTL00004";
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(637, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Người lập biểu 1/2";
            // 
            // txtM_TEN_NLB2
            // 
            this.txtM_TEN_NLB2.AccessibleName = "M_TEN_NLB2";
            this.txtM_TEN_NLB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB2.Location = new System.Drawing.Point(854, 38);
            this.txtM_TEN_NLB2.Name = "txtM_TEN_NLB2";
            this.txtM_TEN_NLB2.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB2.TabIndex = 13;
            // 
            // txtM_TEN_NLB
            // 
            this.txtM_TEN_NLB.AccessibleName = "M_TEN_NLB";
            this.txtM_TEN_NLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB.Location = new System.Drawing.Point(736, 38);
            this.txtM_TEN_NLB.Name = "txtM_TEN_NLB";
            this.txtM_TEN_NLB.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "REPORTL00003";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mẫu in báo cáo";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "REPORTL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tiêu đề báo cáo";
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportTitle.Location = new System.Drawing.Point(101, 12);
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new System.Drawing.Size(868, 20);
            this.txtReportTitle.TabIndex = 2;
            // 
            // treeListViewAuto1
            // 
            this.treeListViewAuto1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListViewAuto1.Comparer = treeListViewItemCollectionComparer1;
            this.treeListViewAuto1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeListViewAuto1.GridLines = true;
            this.treeListViewAuto1.HideSelection = false;
            this.treeListViewAuto1.Location = new System.Drawing.Point(307, 64);
            this.treeListViewAuto1.MultiSelect = false;
            this.treeListViewAuto1.Name = "treeListViewAuto1";
            this.treeListViewAuto1.Size = new System.Drawing.Size(662, 338);
            this.treeListViewAuto1.SmallImageList = this.imageList1;
            this.treeListViewAuto1.TabIndex = 14;
            this.treeListViewAuto1.UseCompatibleStateImageBehavior = false;
            this.treeListViewAuto1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.treeListViewAuto1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "house.png");
            this.imageList1.Images.SetKeyName(1, "persongroup.png");
            this.imageList1.Images.SetKeyName(2, "person.png");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.AccessibleDescription = "REPORTL00006";
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "REPORTB00006";
            this.btnIn.AccessibleName = "";
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print24;
            this.btnIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnIn.Location = new System.Drawing.Point(94, 100);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(88, 40);
            this.btnIn.TabIndex = 2;
            this.btnIn.Text = "&In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(182, 100);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 100);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
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
            this.grbNgonNgu.Controls.Add(this.rTiengViet);
            this.grbNgonNgu.Controls.Add(this.rBothLang);
            this.grbNgonNgu.Controls.Add(this.rEnglish);
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
            this.rCurrent.TabIndex = 3;
            this.rCurrent.Text = "Current";
            this.rCurrent.UseVisualStyleBackColor = true;
            this.rCurrent.CheckedChanged += new System.EventHandler(this.rbtLanguage_CheckedChanged);
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
            this.panel1.Location = new System.Drawing.Point(6, 146);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 435);
            this.panel1.TabIndex = 0;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = ".";
            this.btnSuaTTMauBC.Image = global::V6ControlManager.Properties.Resources.Setting24;
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(366, 33);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnSuaTTMauBC.TabIndex = 6;
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = ".";
            this.btnThemMauBC.Image = global::V6ControlManager.Properties.Resources.SettingAdd24;
            this.btnThemMauBC.Location = new System.Drawing.Point(396, 33);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnThemMauBC.TabIndex = 7;
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // chkHienTatCa
            // 
            this.chkHienTatCa.AccessibleDescription = "REPORTC00001";
            this.chkHienTatCa.AutoSize = true;
            this.chkHienTatCa.Enabled = false;
            this.chkHienTatCa.Location = new System.Drawing.Point(307, 42);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 5;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // btnExport3
            // 
            this.btnExport3.AccessibleDescription = ".";
            this.btnExport3.AutoSize = true;
            this.btnExport3.ContextMenuStrip = this.contextMenuStrip1;
            this.btnExport3.Image = global::V6ControlManager.Properties.Resources.Export24;
            this.btnExport3.Location = new System.Drawing.Point(486, 33);
            this.btnExport3.Name = "btnExport3";
            this.btnExport3.Size = new System.Drawing.Size(50, 30);
            this.btnExport3.SplitMenuStrip = this.contextMenuStrip1;
            this.btnExport3.TabIndex = 10;
            this.btnExport3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport3.UseVisualStyleBackColor = true;
            this.btnExport3.Click += new System.EventHandler(this.btnExport3_Click);
            // 
            // btnSuaLine
            // 
            this.btnSuaLine.AccessibleDescription = ".";
            this.btnSuaLine.Image = global::V6ControlManager.Properties.Resources.LineEdit24;
            this.btnSuaLine.Location = new System.Drawing.Point(456, 33);
            this.btnSuaLine.Name = "btnSuaLine";
            this.btnSuaLine.Size = new System.Drawing.Size(30, 30);
            this.btnSuaLine.TabIndex = 9;
            this.toolTipV6FormControl.SetToolTip(this.btnSuaLine, "Sửa line");
            this.btnSuaLine.UseVisualStyleBackColor = true;
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = ".";
            this.btnSuaMau.Image = global::V6ControlManager.Properties.Resources.Edit24;
            this.btnSuaMau.Location = new System.Drawing.Point(426, 33);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(30, 30);
            this.btnSuaMau.TabIndex = 8;
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.AccessibleDescription = ".";
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(307, 405);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(126, 15);
            this.lblSummary.TabIndex = 15;
            this.lblSummary.Text = "Summary FOOTER";
            this.lblSummary.Visible = false;
            this.lblSummary.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LabelSummary_MouseClick);
            // 
            // documentViewer1
            // 
            this.documentViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentViewer1.IsMetric = false;
            this.documentViewer1.Location = new System.Drawing.Point(307, 421);
            this.documentViewer1.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewer1.Name = "documentViewer1";
            this.documentViewer1.RequestDocumentCreation = false;
            this.documentViewer1.Size = new System.Drawing.Size(662, 224);
            this.documentViewer1.Status = "V6SOFT";
            this.documentViewer1.TabIndex = 16;
            this.documentViewer1.ZoomChanged += new System.EventHandler(this.documentViewer1_ZoomChanged);
            this.documentViewer1.DoubleClick += new System.EventHandler(this.documentViewer1_DoubleClick);
            // 
            // copyMenuStrip1
            // 
            this.copyMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyValue,
            this.menuCopy,
            this.menuCopyAll});
            this.copyMenuStrip1.Name = "contextMenuStrip1";
            this.copyMenuStrip1.Size = new System.Drawing.Size(134, 70);
            // 
            // menuCopyValue
            // 
            this.menuCopyValue.Name = "menuCopyValue";
            this.menuCopyValue.Size = new System.Drawing.Size(133, 22);
            this.menuCopyValue.Text = "Copy value";
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(133, 22);
            this.menuCopy.Text = "Copy text";
            // 
            // menuCopyAll
            // 
            this.menuCopyAll.Name = "menuCopyAll";
            this.menuCopyAll.Size = new System.Drawing.Size(133, 22);
            this.menuCopyAll.Text = "Copy all";
            // 
            // btnEditPara
            // 
            this.btnEditPara.AccessibleDescription = ".";
            this.btnEditPara.Image = global::V6ControlManager.Properties.Resources.UserMale24;
            this.btnEditPara.Location = new System.Drawing.Point(542, 34);
            this.btnEditPara.Name = "btnEditPara";
            this.btnEditPara.Size = new System.Drawing.Size(30, 30);
            this.btnEditPara.TabIndex = 17;
            this.toolTipV6FormControl.SetToolTip(this.btnEditPara, "Sửa giá trị tham số");
            this.btnEditPara.UseVisualStyleBackColor = true;
            this.btnEditPara.Click += new System.EventHandler(this.btnEditPara_Click);
            // 
            // ReportTreeView_DX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditPara);
            this.Controls.Add(this.documentViewer1);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnSuaLine);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.btnExport3);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.cboMauIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtM_TEN_NLB2);
            this.Controls.Add(this.txtM_TEN_NLB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReportTitle);
            this.Controls.Add(this.treeListViewAuto1);
            this.Controls.Add(this.grbDieuKienLoc);
            this.FilterType = "4";
            this.Name = "ReportTreeView_DX";
            this.Size = new System.Drawing.Size(974, 654);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.ReportTreeView_DX_VisibleChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.thisForm_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.grbDieuKienLoc.ResumeLayout(false);
            this.panel0.ResumeLayout(false);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.grbTienTe.ResumeLayout(false);
            this.grbTienTe.PerformLayout();
            this.copyMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.Controls.TreeView.TreeListViewAuto treeListViewAuto1;
        private System.Windows.Forms.GroupBox grbDieuKienLoc;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelMenu;
        private System.Windows.Forms.ToolStripMenuItem printGridMenu;
        private System.Windows.Forms.GroupBox grbTienTe;
        private System.Windows.Forms.RadioButton rNgoaiTe;
        private System.Windows.Forms.RadioButton rTienViet;
        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.Timer timerViewReport;
        private System.Windows.Forms.GroupBox grbNgonNgu;
        private System.Windows.Forms.RadioButton rTiengViet;
        private System.Windows.Forms.RadioButton rEnglish;
        private System.Windows.Forms.RadioButton rBothLang;
        private System.Windows.Forms.TextBox txtReportTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Label label4;
        private V6Controls.V6ComboBox cboMauIn;
        private V6FormButton btnSuaTTMauBC;
        private V6FormButton btnThemMauBC;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem viewDataMenu;
        private V6Controls.V6CheckBox chkHienTatCa;
        private System.Windows.Forms.ToolStripMenuItem exportToXmlMenu;
        private ImageList imageList1;
        private ToolStripMenuItem exportToExcelViewMenu;
        private ToolStripMenuItem exportToPdfMenu;
        private RadioButton rCurrent;
        private wyDay.Controls.SplitButton btnExport3;
        private ToolStripMenuItem viewInvoiceInfoMenu;
        private ToolStripMenuItem viewListInfoMenu;
        private V6FormButton btnSuaLine;
        private V6FormButton btnSuaMau;
        private Label lblSummary;
        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
        private ToolStripMenuItem exportEXCELXtraMenu;
        private ToolStripMenuItem exportEXCELDataMenu;
        private ToolStripMenuItem exportToHTMLMenu;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem exportToWordMenu;
        private ContextMenuStrip copyMenuStrip1;
        private ToolStripMenuItem menuCopyValue;
        private ToolStripMenuItem menuCopy;
        private ToolStripMenuItem menuCopyAll;
        private V6FormButton btnEditPara;
    }
}