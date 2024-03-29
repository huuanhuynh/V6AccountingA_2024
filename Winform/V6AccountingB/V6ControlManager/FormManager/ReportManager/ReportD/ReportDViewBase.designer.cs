﻿using V6Controls;
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
            this.exportToExcelTemplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToXmlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.printGridMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToPdfMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInvoiceInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewListInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.rCurrent = new System.Windows.Forms.RadioButton();
            this.rTiengViet = new System.Windows.Forms.RadioButton();
            this.rBothLang = new System.Windows.Forms.RadioButton();
            this.rEnglish = new System.Windows.Forms.RadioButton();
            this.grbTienTe = new System.Windows.Forms.GroupBox();
            this.rNgoaiTe = new System.Windows.Forms.RadioButton();
            this.rTienViet = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.txtReportTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtM_TEN_NLB2 = new System.Windows.Forms.TextBox();
            this.txtM_TEN_NLB = new System.Windows.Forms.TextBox();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.cboMauIn = new V6Controls.V6ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.gridViewSummary1 = new V6Controls.Controls.GridViewSummary();
            this.btnSuaLine = new V6Controls.Controls.V6FormButton();
            this.lblSummary = new System.Windows.Forms.Label();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.btnExport3 = new wyDay.Controls.SplitButton();
            this.gridViewTopFilter1 = new V6Controls.Controls.GridViewTopFilter();
            this.btnEditPara = new V6Controls.Controls.V6FormButton();
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
            this.dataGridView1.Location = new System.Drawing.Point(307, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(662, 312);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 202);
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
            this.printGridMenu.Click += new System.EventHandler(this.printGrid_Click);
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
            this.grbDieuKienLoc.Location = new System.Drawing.Point(0, 64);
            this.grbDieuKienLoc.Name = "grbDieuKienLoc";
            this.grbDieuKienLoc.Size = new System.Drawing.Size(302, 589);
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
            this.rCurrent.TabIndex = 2;
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
            this.panel1.Location = new System.Drawing.Point(0, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 407);
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
            this.txtReportTitle.Size = new System.Drawing.Size(870, 20);
            this.txtReportTitle.TabIndex = 5;
            this.txtReportTitle.TabStop = false;
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
            // label2
            // 
            this.label2.AccessibleDescription = "REPORTL00004";
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(635, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Người lập biểu 1/2";
            // 
            // txtM_TEN_NLB2
            // 
            this.txtM_TEN_NLB2.AccessibleName = "M_TEN_NLB2";
            this.txtM_TEN_NLB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB2.Location = new System.Drawing.Point(854, 38);
            this.txtM_TEN_NLB2.Name = "txtM_TEN_NLB2";
            this.txtM_TEN_NLB2.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB2.TabIndex = 7;
            this.txtM_TEN_NLB2.TabStop = false;
            // 
            // txtM_TEN_NLB
            // 
            this.txtM_TEN_NLB.AccessibleName = "M_TEN_NLB";
            this.txtM_TEN_NLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB.Location = new System.Drawing.Point(735, 38);
            this.txtM_TEN_NLB.Name = "txtM_TEN_NLB";
            this.txtM_TEN_NLB.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB.TabIndex = 8;
            this.txtM_TEN_NLB.TabStop = false;
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = ".";
            this.btnSuaMau.Enabled = false;
            this.btnSuaMau.Image = global::V6ControlManager.Properties.Resources.Edit24;
            this.btnSuaMau.Location = new System.Drawing.Point(419, 33);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(30, 30);
            this.btnSuaMau.TabIndex = 21;
            this.btnSuaMau.TabStop = false;
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
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
            this.cboMauIn.TabStop = false;
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
            this.chkHienTatCa.Location = new System.Drawing.Point(307, 39);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 35;
            this.chkHienTatCa.TabStop = false;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // gridViewSummary1
            // 
            this.gridViewSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary1.DataGridView = this.dataGridView1;
            this.gridViewSummary1.Location = new System.Drawing.Point(307, 398);
            this.gridViewSummary1.Name = "gridViewSummary1";
            this.gridViewSummary1.Size = new System.Drawing.Size(662, 23);
            this.gridViewSummary1.SumCondition = null;
            this.gridViewSummary1.TabIndex = 0;
            // 
            // btnSuaLine
            // 
            this.btnSuaLine.AccessibleDescription = ".";
            this.btnSuaLine.Image = global::V6ControlManager.Properties.Resources.LineEdit24;
            this.btnSuaLine.Location = new System.Drawing.Point(449, 33);
            this.btnSuaLine.Name = "btnSuaLine";
            this.btnSuaLine.Size = new System.Drawing.Size(30, 30);
            this.btnSuaLine.TabIndex = 36;
            this.btnSuaLine.TabStop = false;
            this.toolTipV6FormControl.SetToolTip(this.btnSuaLine, "Sửa line");
            this.btnSuaLine.UseVisualStyleBackColor = true;
            this.btnSuaLine.Click += new System.EventHandler(this.btnSuaLine_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.AccessibleDescription = ".";
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(307, 403);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(126, 15);
            this.lblSummary.TabIndex = 38;
            this.lblSummary.Text = "Summary FOOTER";
            this.lblSummary.Visible = false;
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = ".";
            this.btnSuaTTMauBC.Image = global::V6ControlManager.Properties.Resources.Setting24;
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(359, 33);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnSuaTTMauBC.TabIndex = 22;
            this.btnSuaTTMauBC.TabStop = false;
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = ".";
            this.btnThemMauBC.Image = global::V6ControlManager.Properties.Resources.SettingAdd24;
            this.btnThemMauBC.Location = new System.Drawing.Point(389, 33);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnThemMauBC.TabIndex = 23;
            this.btnThemMauBC.TabStop = false;
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // btnExport3
            // 
            this.btnExport3.AccessibleDescription = ".";
            this.btnExport3.AutoSize = true;
            this.btnExport3.ContextMenuStrip = this.contextMenuStrip1;
            this.btnExport3.Image = global::V6ControlManager.Properties.Resources.Export24;
            this.btnExport3.Location = new System.Drawing.Point(479, 33);
            this.btnExport3.Name = "btnExport3";
            this.btnExport3.Size = new System.Drawing.Size(50, 30);
            this.btnExport3.SplitMenuStrip = this.contextMenuStrip1;
            this.btnExport3.TabIndex = 39;
            this.btnExport3.TabStop = false;
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
            this.gridViewTopFilter1.Location = new System.Drawing.Point(307, 64);
            this.gridViewTopFilter1.Name = "gridViewTopFilter1";
            this.gridViewTopFilter1.Size = new System.Drawing.Size(662, 22);
            this.gridViewTopFilter1.TabIndex = 1;
            // 
            // btnEditPara
            // 
            this.btnEditPara.AccessibleDescription = ".";
            this.btnEditPara.Image = global::V6ControlManager.Properties.Resources.UserMale24;
            this.btnEditPara.Location = new System.Drawing.Point(535, 33);
            this.btnEditPara.Name = "btnEditPara";
            this.btnEditPara.Size = new System.Drawing.Size(30, 30);
            this.btnEditPara.TabIndex = 41;
            this.btnEditPara.TabStop = false;
            this.toolTipV6FormControl.SetToolTip(this.btnEditPara, "Sửa giá trị tham số");
            this.btnEditPara.UseVisualStyleBackColor = true;
            this.btnEditPara.Click += new System.EventHandler(this.btnEditPara_Click);
            // 
            // ReportDViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditPara);
            this.Controls.Add(this.gridViewTopFilter1);
            this.Controls.Add(this.btnExport3);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnSuaLine);
            this.Controls.Add(this.gridViewSummary1);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.cboMauIn);
            this.Controls.Add(this.label4);
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
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.thisForm_MouseClick);
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
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TextBox txtReportTitle;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.ToolStripMenuItem exportToExcelTemplateMenu;
        private System.Windows.Forms.ToolStripMenuItem viewDataMenu;
        private V6Controls.V6CheckBox chkHienTatCa;
        private System.Windows.Forms.ToolStripMenuItem exportToXmlMenu;
        private GridViewSummary gridViewSummary1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelViewMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToPdfMenu;
        private V6FormButton btnSuaLine;
        private System.Windows.Forms.RadioButton rCurrent;
        private System.Windows.Forms.Label lblSummary;
        private wyDay.Controls.SplitButton btnExport3;
        private GridViewTopFilter gridViewTopFilter1;
        private System.Windows.Forms.ToolStripMenuItem viewInvoiceInfoMenu;
        private System.Windows.Forms.ToolStripMenuItem viewListInfoMenu;
        private V6FormButton btnEditPara;
    }
}