﻿using V6Controls.Controls;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu
{
    partial class InChungTuViewBase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExporttoExceltemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.btnLs = new System.Windows.Forms.Button();
            this.btnLt = new System.Windows.Forms.Button();
            this.grbSoLien = new System.Windows.Forms.GroupBox();
            this.numSoLien = new System.Windows.Forms.NumericUpDown();
            this.grbCrossLine = new System.Windows.Forms.GroupBox();
            this.chkCrossModify = new V6Controls.V6CheckBox();
            this.numCrossAdd = new System.Windows.Forms.NumericUpDown();
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
            this.cboMauIn = new V6Controls.V6ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtM_TEN_NLB2 = new System.Windows.Forms.TextBox();
            this.txtM_TEN_NLB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.chkHienTatCa = new V6Controls.V6CheckBox();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crystalReportViewer3 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panelCRview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.grbDieuKienLoc.SuspendLayout();
            this.grbSoLien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLien)).BeginInit();
            this.grbCrossLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCrossAdd)).BeginInit();
            this.panel0.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.grbTienTe.SuspendLayout();
            this.panelCRview.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(307, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(662, 94);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExporttoExceltemplate,
            this.exportToExcel,
            this.printGrid});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 70);
            // 
            // ExporttoExceltemplate
            // 
            this.ExporttoExceltemplate.Name = "ExporttoExceltemplate";
            this.ExporttoExceltemplate.Size = new System.Drawing.Size(208, 22);
            this.ExporttoExceltemplate.Text = "Export to Excel (template)";
            this.ExporttoExceltemplate.Click += new System.EventHandler(this.ExporttoExceltemplate_Click);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(208, 22);
            this.exportToExcel.Text = "Export To Excel";
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // printGrid
            // 
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(208, 22);
            this.printGrid.Text = "Print Grid";
            this.printGrid.Click += new System.EventHandler(this.printGrid_Click);
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.AccessibleDescription = "ASOCTSOA-INL00003";
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDieuKienLoc.Controls.Add(this.btnLs);
            this.grbDieuKienLoc.Controls.Add(this.btnLt);
            this.grbDieuKienLoc.Controls.Add(this.grbSoLien);
            this.grbDieuKienLoc.Controls.Add(this.grbCrossLine);
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
            // btnLs
            // 
            this.btnLs.Location = new System.Drawing.Point(48, 144);
            this.btnLs.Name = "btnLs";
            this.btnLs.Size = new System.Drawing.Size(34, 25);
            this.btnLs.TabIndex = 34;
            this.btnLs.Text = ">";
            this.btnLs.UseVisualStyleBackColor = true;
            this.btnLs.Click += new System.EventHandler(this.btnLs_Click);
            // 
            // btnLt
            // 
            this.btnLt.Location = new System.Drawing.Point(6, 144);
            this.btnLt.Name = "btnLt";
            this.btnLt.Size = new System.Drawing.Size(34, 25);
            this.btnLt.TabIndex = 35;
            this.btnLt.Text = "<";
            this.btnLt.UseVisualStyleBackColor = true;
            this.btnLt.Click += new System.EventHandler(this.btnLt_Click);
            // 
            // grbSoLien
            // 
            this.grbSoLien.AccessibleDescription = "ASOCTSOA-INL00015";
            this.grbSoLien.Controls.Add(this.numSoLien);
            this.grbSoLien.Location = new System.Drawing.Point(5, 178);
            this.grbSoLien.Name = "grbSoLien";
            this.grbSoLien.Size = new System.Drawing.Size(82, 41);
            this.grbSoLien.TabIndex = 33;
            this.grbSoLien.TabStop = false;
            this.grbSoLien.Text = "Số liên";
            // 
            // numSoLien
            // 
            this.numSoLien.Enabled = false;
            this.numSoLien.Location = new System.Drawing.Point(6, 16);
            this.numSoLien.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLien.Name = "numSoLien";
            this.numSoLien.Size = new System.Drawing.Size(40, 20);
            this.numSoLien.TabIndex = 0;
            this.numSoLien.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grbCrossLine
            // 
            this.grbCrossLine.AccessibleDescription = "ASOCTSOA-INO00010";
            this.grbCrossLine.Controls.Add(this.chkCrossModify);
            this.grbCrossLine.Controls.Add(this.numCrossAdd);
            this.grbCrossLine.Location = new System.Drawing.Point(93, 154);
            this.grbCrossLine.Name = "grbCrossLine";
            this.grbCrossLine.Size = new System.Drawing.Size(154, 65);
            this.grbCrossLine.TabIndex = 33;
            this.grbCrossLine.TabStop = false;
            this.grbCrossLine.Text = "Gạch chéo";
            // 
            // chkCrossModify
            // 
            this.chkCrossModify.AccessibleDescription = "ASOCTSOA-INO00009";
            this.chkCrossModify.AutoSize = true;
            this.chkCrossModify.Location = new System.Drawing.Point(6, 19);
            this.chkCrossModify.Name = "chkCrossModify";
            this.chkCrossModify.Size = new System.Drawing.Size(126, 17);
            this.chkCrossModify.TabIndex = 1;
            this.chkCrossModify.Text = "Điều chỉnh tăng giảm";
            this.chkCrossModify.UseVisualStyleBackColor = true;
            this.chkCrossModify.CheckedChanged += new System.EventHandler(this.chkCrossModify_CheckedChanged);
            // 
            // numCrossAdd
            // 
            this.numCrossAdd.Enabled = false;
            this.numCrossAdd.Location = new System.Drawing.Point(6, 39);
            this.numCrossAdd.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numCrossAdd.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numCrossAdd.Name = "numCrossAdd";
            this.numCrossAdd.Size = new System.Drawing.Size(40, 20);
            this.numCrossAdd.TabIndex = 0;
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "ASOCTSOA-INB00004";
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print24;
            this.btnIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnIn.Location = new System.Drawing.Point(94, 100);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(88, 40);
            this.btnIn.TabIndex = 0;
            this.btnIn.Tag = "P, Control";
            this.btnIn.Text = "&In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "ASOCTSOA-INB00007";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(182, 100);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "ASOCTSOA-INB00006";
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
            this.panel0.TabIndex = 3;
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.AccessibleDescription = "ASOCTSOA-INL00017";
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
            this.rTiengViet.AccessibleDescription = "ASOCTSOA-INL00018";
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
            this.rBothLang.AccessibleDescription = "ASOCTSOA-INL00020";
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
            this.rEnglish.AccessibleDescription = "ASOCTSOA-INL00019";
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
            this.grbTienTe.AccessibleDescription = "ASOCTSOA-INL00016";
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
            this.rNgoaiTe.AccessibleDescription = "ASOCTSOA-INO00012";
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
            this.rTienViet.AccessibleDescription = "ASOCTSOA-INO00011";
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 225);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 356);
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
            this.crystalReportViewer1.DisplayToolbar = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(1, 3);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReuseParameterValuesOnRefresh = true;
            this.crystalReportViewer1.ShowCloseButton = false;
            this.crystalReportViewer1.ShowCopyButton = false;
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.ShowLogo = false;
            this.crystalReportViewer1.ShowParameterPanelButton = false;
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(664, 456);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer1.DoubleClick += new System.EventHandler(this.crystalReportViewer1_DoubleClick);
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
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
            this.label1.AccessibleDescription = "ASOCTSOA-INL00001";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tiêu đề báo cáo";
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
            this.cboMauIn.TabIndex = 17;
            this.cboMauIn.SelectedIndexChanged += new System.EventHandler(this.cboMauIn_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ASOCTSOA-INL00014";
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(757, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Người lập biểu 2";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ASOCTSOA-INL00013";
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(555, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Người lập biểu";
            // 
            // txtM_TEN_NLB2
            // 
            this.txtM_TEN_NLB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB2.Location = new System.Drawing.Point(847, 38);
            this.txtM_TEN_NLB2.Name = "txtM_TEN_NLB2";
            this.txtM_TEN_NLB2.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB2.TabIndex = 13;
            // 
            // txtM_TEN_NLB
            // 
            this.txtM_TEN_NLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtM_TEN_NLB.Location = new System.Drawing.Point(636, 38);
            this.txtM_TEN_NLB.Name = "txtM_TEN_NLB";
            this.txtM_TEN_NLB.Size = new System.Drawing.Size(115, 20);
            this.txtM_TEN_NLB.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "ASOCTSOA-INL00002";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Mẫu in báo cáo";
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = "ASOCTSOA-INB00009";
            this.btnThemMauBC.Location = new System.Drawing.Point(351, 37);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnThemMauBC.TabIndex = 18;
            this.btnThemMauBC.Text = "Thêm";
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = "ASOCTSOA-INB00010";
            this.btnSuaMau.Location = new System.Drawing.Point(396, 37);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(36, 23);
            this.btnSuaMau.TabIndex = 18;
            this.btnSuaMau.Text = "Sửa";
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = "ASOCTSOA-INB00008";
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(306, 37);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMauBC.TabIndex = 18;
            this.btnSuaTTMauBC.Text = "Sửa tt";
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // chkHienTatCa
            // 
            this.chkHienTatCa.AccessibleDescription = "ASOCTSOA-INB00005";
            this.chkHienTatCa.AutoSize = true;
            this.chkHienTatCa.Location = new System.Drawing.Point(438, 40);
            this.chkHienTatCa.Name = "chkHienTatCa";
            this.chkHienTatCa.Size = new System.Drawing.Size(37, 17);
            this.chkHienTatCa.TabIndex = 36;
            this.chkHienTatCa.Text = "All";
            this.chkHienTatCa.UseVisualStyleBackColor = true;
            this.chkHienTatCa.CheckedChanged += new System.EventHandler(this.chkHienTatCa_CheckedChanged);
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.DisplayStatusBar = false;
            this.crystalReportViewer2.DisplayToolbar = false;
            this.crystalReportViewer2.Location = new System.Drawing.Point(0, 3);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.ReuseParameterValuesOnRefresh = true;
            this.crystalReportViewer2.ShowCloseButton = false;
            this.crystalReportViewer2.ShowCopyButton = false;
            this.crystalReportViewer2.ShowGroupTreeButton = false;
            this.crystalReportViewer2.ShowLogo = false;
            this.crystalReportViewer2.ShowParameterPanelButton = false;
            this.crystalReportViewer2.ShowRefreshButton = false;
            this.crystalReportViewer2.Size = new System.Drawing.Size(667, 457);
            this.crystalReportViewer2.TabIndex = 37;
            this.crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer2.Visible = false;
            this.crystalReportViewer2.DoubleClick += new System.EventHandler(this.crystalReportViewer1_DoubleClick);
            // 
            // crystalReportViewer3
            // 
            this.crystalReportViewer3.ActiveViewIndex = -1;
            this.crystalReportViewer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer3.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer3.DisplayStatusBar = false;
            this.crystalReportViewer3.DisplayToolbar = false;
            this.crystalReportViewer3.Location = new System.Drawing.Point(0, 3);
            this.crystalReportViewer3.Name = "crystalReportViewer3";
            this.crystalReportViewer3.ReuseParameterValuesOnRefresh = true;
            this.crystalReportViewer3.ShowCloseButton = false;
            this.crystalReportViewer3.ShowCopyButton = false;
            this.crystalReportViewer3.ShowGroupTreeButton = false;
            this.crystalReportViewer3.ShowLogo = false;
            this.crystalReportViewer3.ShowParameterPanelButton = false;
            this.crystalReportViewer3.ShowRefreshButton = false;
            this.crystalReportViewer3.Size = new System.Drawing.Size(667, 457);
            this.crystalReportViewer3.TabIndex = 38;
            this.crystalReportViewer3.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer3.Visible = false;
            this.crystalReportViewer3.DoubleClick += new System.EventHandler(this.crystalReportViewer1_DoubleClick);
            // 
            // panelCRview
            // 
            this.panelCRview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCRview.AutoScroll = true;
            this.panelCRview.Controls.Add(this.crystalReportViewer1);
            this.panelCRview.Controls.Add(this.crystalReportViewer3);
            this.panelCRview.Controls.Add(this.crystalReportViewer2);
            this.panelCRview.Location = new System.Drawing.Point(307, 189);
            this.panelCRview.Name = "panelCRview";
            this.panelCRview.Size = new System.Drawing.Size(667, 460);
            this.panelCRview.TabIndex = 1;
            // 
            // InChungTuViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCRview);
            this.Controls.Add(this.chkHienTatCa);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.cboMauIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtM_TEN_NLB2);
            this.Controls.Add(this.txtM_TEN_NLB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReportTitle);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grbDieuKienLoc);
            this.Name = "InChungTuViewBase";
            this.Size = new System.Drawing.Size(974, 654);
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.grbDieuKienLoc.ResumeLayout(false);
            this.grbSoLien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLien)).EndInit();
            this.grbCrossLine.ResumeLayout(false);
            this.grbCrossLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCrossAdd)).EndInit();
            this.panel0.ResumeLayout(false);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.grbTienTe.ResumeLayout(false);
            this.grbTienTe.PerformLayout();
            this.panelCRview.ResumeLayout(false);
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
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grbCrossLine;
        private V6Controls.V6CheckBox chkCrossModify;
        private System.Windows.Forms.NumericUpDown numCrossAdd;
        private System.Windows.Forms.Button btnIn;
        private V6Controls.V6ComboBox cboMauIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB2;
        private System.Windows.Forms.TextBox txtM_TEN_NLB;
        private System.Windows.Forms.Label label4;
        private V6FormButton btnThemMauBC;
        private V6FormButton btnSuaMau;
        private V6FormButton btnSuaTTMauBC;
        private V6Controls.V6CheckBox chkHienTatCa;
        private System.Windows.Forms.GroupBox grbSoLien;
        private System.Windows.Forms.NumericUpDown numSoLien;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer3;
        private System.Windows.Forms.Button btnLs;
        private System.Windows.Forms.Button btnLt;
        public System.Windows.Forms.Panel panelCRview;
        private System.Windows.Forms.ToolStripMenuItem ExporttoExceltemplate;
    }
}