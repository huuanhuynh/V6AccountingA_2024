namespace V6ControlManager.FormManager.Map
{
    partial class FormMapManagerAutoHide
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_Pic1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuPic1ReportKhu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPic1ReportDay = new System.Windows.Forms.ToolStripMenuItem();
            this.Baocao1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Baocao2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Baocao3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Baocao4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Thongke1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Thongke2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Thongke3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Thongke4 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1_Image = new System.Windows.Forms.Panel();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip_Reload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grbKhuVuc = new System.Windows.Forms.GroupBox();
            this.chkNone = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelContainer1 = new System.Windows.Forms.Panel();
            this.btnPrintPicture = new System.Windows.Forms.Button();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rbtTiengViet = new System.Windows.Forms.RadioButton();
            this.rbtEnglish = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip_DataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Pic2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuPic2Report = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPictureReport = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip_Pic1.SuspendLayout();
            this.panel1_Image.SuspendLayout();
            this.contextMenuStrip_Reload.SuspendLayout();
            this.grbKhuVuc.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.contextMenuStrip_DataGrid.SuspendLayout();
            this.contextMenuStrip_Pic2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip_Pic1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // contextMenuStrip_Pic1
            // 
            this.contextMenuStrip_Pic1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPic1ReportKhu,
            this.menuPic1ReportDay,
            this.Baocao1,
            this.Baocao2,
            this.Baocao3,
            this.Baocao4,
            this.Thongke1,
            this.Thongke2,
            this.Thongke3,
            this.Thongke4});
            this.contextMenuStrip_Pic1.Name = "contextMenuStrip_Pic1";
            this.contextMenuStrip_Pic1.Size = new System.Drawing.Size(140, 224);
            // 
            // menuPic1ReportKhu
            // 
            this.menuPic1ReportKhu.Name = "menuPic1ReportKhu";
            this.menuPic1ReportKhu.Size = new System.Drawing.Size(139, 22);
            this.menuPic1ReportKhu.Text = "Báo cáo khu";
            this.menuPic1ReportKhu.Click += new System.EventHandler(this.menuPic1ReportKhu_Click);
            // 
            // menuPic1ReportDay
            // 
            this.menuPic1ReportDay.Name = "menuPic1ReportDay";
            this.menuPic1ReportDay.Size = new System.Drawing.Size(139, 22);
            this.menuPic1ReportDay.Text = "Báo cáo dãy";
            this.menuPic1ReportDay.Click += new System.EventHandler(this.menuPic1ReportDay_Click);
            // 
            // Baocao1
            // 
            this.Baocao1.Name = "Baocao1";
            this.Baocao1.Size = new System.Drawing.Size(139, 22);
            this.Baocao1.Text = "Báo cáo 1";
            this.Baocao1.Click += new System.EventHandler(this.Baocao1_Click);
            // 
            // Baocao2
            // 
            this.Baocao2.Name = "Baocao2";
            this.Baocao2.Size = new System.Drawing.Size(139, 22);
            this.Baocao2.Text = "Báo cáo 2";
            this.Baocao2.Click += new System.EventHandler(this.Baocao2_Click);
            // 
            // Baocao3
            // 
            this.Baocao3.Name = "Baocao3";
            this.Baocao3.Size = new System.Drawing.Size(139, 22);
            this.Baocao3.Text = "Báo cáo 3";
            this.Baocao3.Click += new System.EventHandler(this.Baocao3_Click);
            // 
            // Baocao4
            // 
            this.Baocao4.Name = "Baocao4";
            this.Baocao4.Size = new System.Drawing.Size(139, 22);
            this.Baocao4.Text = "Báo cáo 4";
            this.Baocao4.Click += new System.EventHandler(this.Baocao4_Click);
            // 
            // Thongke1
            // 
            this.Thongke1.Name = "Thongke1";
            this.Thongke1.Size = new System.Drawing.Size(139, 22);
            this.Thongke1.Text = "Thống kê 1";
            this.Thongke1.Click += new System.EventHandler(this.Thongke1_Click);
            // 
            // Thongke2
            // 
            this.Thongke2.Name = "Thongke2";
            this.Thongke2.Size = new System.Drawing.Size(139, 22);
            this.Thongke2.Text = "Thống kê 2";
            this.Thongke2.Click += new System.EventHandler(this.Thongke2_Click);
            // 
            // Thongke3
            // 
            this.Thongke3.Name = "Thongke3";
            this.Thongke3.Size = new System.Drawing.Size(139, 22);
            this.Thongke3.Text = "Thống kê 3";
            this.Thongke3.Click += new System.EventHandler(this.Thongke3_Click);
            // 
            // Thongke4
            // 
            this.Thongke4.Name = "Thongke4";
            this.Thongke4.Size = new System.Drawing.Size(139, 22);
            this.Thongke4.Text = "Thống kê 4";
            this.Thongke4.Click += new System.EventHandler(this.Thongke4_Click);
            // 
            // panel1_Image
            // 
            this.panel1_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1_Image.AutoScroll = true;
            this.panel1_Image.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1_Image.Controls.Add(this.pictureBox1);
            this.panel1_Image.Location = new System.Drawing.Point(4, 54);
            this.panel1_Image.Name = "panel1_Image";
            this.panel1_Image.Size = new System.Drawing.Size(781, 548);
            this.panel1_Image.TabIndex = 1;
            this.panel1_Image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_Image_MouseMove);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.ContextMenuStrip = this.contextMenuStrip_Reload;
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(9, 16);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(333, 21);
            this.comboBoxGroup.TabIndex = 5;
            this.comboBoxGroup.TabStop = false;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // contextMenuStrip_Reload
            // 
            this.contextMenuStrip_Reload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.contextMenuStrip_Reload.Name = "contextMenuStrip2";
            this.contextMenuStrip_Reload.Size = new System.Drawing.Size(111, 26);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // grbKhuVuc
            // 
            this.grbKhuVuc.AccessibleDescription = "XULYG00003";
            this.grbKhuVuc.Controls.Add(this.chkNone);
            this.grbKhuVuc.Controls.Add(this.comboBoxGroup);
            this.grbKhuVuc.Location = new System.Drawing.Point(4, 4);
            this.grbKhuVuc.Name = "grbKhuVuc";
            this.grbKhuVuc.Size = new System.Drawing.Size(406, 44);
            this.grbKhuVuc.TabIndex = 7;
            this.grbKhuVuc.TabStop = false;
            this.grbKhuVuc.Text = "Khu vực";
            // 
            // chkNone
            // 
            this.chkNone.AccessibleDescription = ".";
            this.chkNone.AutoSize = true;
            this.chkNone.Location = new System.Drawing.Point(348, 20);
            this.chkNone.Name = "chkNone";
            this.chkNone.Size = new System.Drawing.Size(52, 17);
            this.chkNone.TabIndex = 8;
            this.chkNone.Text = "None";
            this.chkNone.UseVisualStyleBackColor = true;
            this.chkNone.CheckedChanged += new System.EventHandler(this.chkNone_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Images|*.bmp;*.jpg;*.png;*.gif|BMP|*.bmp|JPG|*.jpg|PNG|*.png";
            this.openFileDialog1.Title = "Chọn tập tin hình ảnh";
            // 
            // panelContainer1
            // 
            this.panelContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainer1.Controls.Add(this.btnPrintPicture);
            this.panelContainer1.Controls.Add(this.grbNgonNgu);
            this.panelContainer1.Controls.Add(this.grbKhuVuc);
            this.panelContainer1.Controls.Add(this.panel1_Image);
            this.panelContainer1.Location = new System.Drawing.Point(1, 5);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.Size = new System.Drawing.Size(791, 605);
            this.panelContainer1.TabIndex = 4;
            this.panelContainer1.MouseHover += new System.EventHandler(this.panelContainer1_MouseHover);
            this.panelContainer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelContainer1_MouseMove);
            // 
            // btnPrintPicture
            // 
            this.btnPrintPicture.AccessibleDescription = "XULYB00013";
            this.btnPrintPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintPicture.Location = new System.Drawing.Point(710, 13);
            this.btnPrintPicture.Name = "btnPrintPicture";
            this.btnPrintPicture.Size = new System.Drawing.Size(75, 23);
            this.btnPrintPicture.TabIndex = 9;
            this.btnPrintPicture.Text = "Lưu hình";
            this.btnPrintPicture.UseVisualStyleBackColor = true;
            this.btnPrintPicture.Click += new System.EventHandler(this.btnPrintPicture_Click);
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.AccessibleDescription = "XULYG00004";
            this.grbNgonNgu.Controls.Add(this.rbtTiengViet);
            this.grbNgonNgu.Controls.Add(this.rbtEnglish);
            this.grbNgonNgu.Location = new System.Drawing.Point(416, 13);
            this.grbNgonNgu.Name = "grbNgonNgu";
            this.grbNgonNgu.Size = new System.Drawing.Size(174, 35);
            this.grbNgonNgu.TabIndex = 8;
            this.grbNgonNgu.TabStop = false;
            this.grbNgonNgu.Text = "Ngôn ngữ (Language)";
            // 
            // rbtTiengViet
            // 
            this.rbtTiengViet.AccessibleDescription = "Hiển thị báo cáo bằng tiếng Việt";
            this.rbtTiengViet.AccessibleName = "Tiếng Việt";
            this.rbtTiengViet.AutoSize = true;
            this.rbtTiengViet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbtTiengViet.Location = new System.Drawing.Point(90, 13);
            this.rbtTiengViet.Name = "rbtTiengViet";
            this.rbtTiengViet.Size = new System.Drawing.Size(73, 17);
            this.rbtTiengViet.TabIndex = 1;
            this.rbtTiengViet.Text = "Tiếng Việt";
            this.rbtTiengViet.UseVisualStyleBackColor = true;
            this.rbtTiengViet.CheckedChanged += new System.EventHandler(this.rbtEnglish_CheckedChanged);
            // 
            // rbtEnglish
            // 
            this.rbtEnglish.AccessibleDescription = "Hiển thị báo cáo bằng tiếng Anh";
            this.rbtEnglish.AccessibleName = "English";
            this.rbtEnglish.AutoSize = true;
            this.rbtEnglish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbtEnglish.Location = new System.Drawing.Point(10, 13);
            this.rbtEnglish.Name = "rbtEnglish";
            this.rbtEnglish.Size = new System.Drawing.Size(59, 17);
            this.rbtEnglish.TabIndex = 0;
            this.rbtEnglish.Text = "English";
            this.rbtEnglish.UseVisualStyleBackColor = true;
            this.rbtEnglish.CheckedChanged += new System.EventHandler(this.rbtEnglish_CheckedChanged);
            // 
            // contextMenuStrip_DataGrid
            // 
            this.contextMenuStrip_DataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcel,
            this.printGrid});
            this.contextMenuStrip_DataGrid.Name = "contextMenuStrip1";
            this.contextMenuStrip_DataGrid.Size = new System.Drawing.Size(154, 48);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(153, 22);
            this.exportToExcel.Text = "Export To Excel";
            // 
            // printGrid
            // 
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(153, 22);
            this.printGrid.Text = "Print Grid";
            // 
            // contextMenuStrip_Pic2
            // 
            this.contextMenuStrip_Pic2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPic2Report,
            this.menuPictureReport});
            this.contextMenuStrip_Pic2.Name = "contextMenuStrip_Pic1";
            this.contextMenuStrip_Pic2.Size = new System.Drawing.Size(156, 48);
            // 
            // menuPic2Report
            // 
            this.menuPic2Report.Name = "menuPic2Report";
            this.menuPic2Report.Size = new System.Drawing.Size(155, 22);
            this.menuPic2Report.Text = "Báo cáo lô";
            // 
            // menuPictureReport
            // 
            this.menuPictureReport.Name = "menuPictureReport";
            this.menuPictureReport.Size = new System.Drawing.Size(155, 22);
            this.menuPictureReport.Text = "Báo cáo có ảnh";
            this.menuPictureReport.Click += new System.EventHandler(this.ItemPictureReport_Click);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // FormMapManagerAutoHide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer1);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "FormMapManagerAutoHide";
            this.Size = new System.Drawing.Size(795, 611);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip_Pic1.ResumeLayout(false);
            this.panel1_Image.ResumeLayout(false);
            this.panel1_Image.PerformLayout();
            this.contextMenuStrip_Reload.ResumeLayout(false);
            this.grbKhuVuc.ResumeLayout(false);
            this.grbKhuVuc.PerformLayout();
            this.panelContainer1.ResumeLayout(false);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.contextMenuStrip_DataGrid.ResumeLayout(false);
            this.contextMenuStrip_Pic2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1_Image;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.GroupBox grbKhuVuc;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        //private System.Windows.Forms.SplitContainer splitContainer1;

        private System.Windows.Forms.Panel panelContainer1;

        private System.Windows.Forms.CheckBox chkNone;
        private System.Windows.Forms.GroupBox grbNgonNgu;
        private System.Windows.Forms.RadioButton rbtTiengViet;
        private System.Windows.Forms.RadioButton rbtEnglish;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_DataGrid;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Reload;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Pic1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Pic2;
        private System.Windows.Forms.ToolStripMenuItem menuPic1ReportKhu;
        private System.Windows.Forms.ToolStripMenuItem menuPic2Report;
        private System.Windows.Forms.ToolStripMenuItem menuPic1ReportDay;
        private System.Windows.Forms.ToolStripMenuItem menuPictureReport;
        private System.Windows.Forms.ToolStripMenuItem Baocao1;
        private System.Windows.Forms.ToolStripMenuItem Baocao2;
        private System.Windows.Forms.ToolStripMenuItem Baocao3;
        private System.Windows.Forms.ToolStripMenuItem Baocao4;
        private System.Windows.Forms.ToolStripMenuItem Thongke1;
        private System.Windows.Forms.ToolStripMenuItem Thongke2;
        private System.Windows.Forms.ToolStripMenuItem Thongke3;
        private System.Windows.Forms.ToolStripMenuItem Thongke4;
        private System.Windows.Forms.Button btnPrintPicture;
    }
}

