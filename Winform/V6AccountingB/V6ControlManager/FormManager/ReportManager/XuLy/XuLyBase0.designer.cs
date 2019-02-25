namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class XuLyBase0
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.grbDieuKienLoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcel,
            this.printGrid});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 70);
            // 
            // exportToExcel
            // 
            this.exportToExcel.AccessibleDescription = "REPORTM00001";
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(153, 22);
            this.exportToExcel.Text = "Export To Excel";
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // printGrid
            // 
            this.printGrid.AccessibleDescription = "REPORTM00007";
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(153, 22);
            this.printGrid.Text = "Print Grid";
            // 
            // timerViewReport
            // 
            this.timerViewReport.Tick += new System.EventHandler(this.timerViewReport_Tick);
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDieuKienLoc.Controls.Add(this.panel1);
            this.grbDieuKienLoc.Location = new System.Drawing.Point(3, 3);
            this.grbDieuKienLoc.Name = "grbDieuKienLoc";
            this.grbDieuKienLoc.Size = new System.Drawing.Size(566, 368);
            this.grbDieuKienLoc.TabIndex = 0;
            this.grbDieuKienLoc.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 349);
            this.panel1.TabIndex = 0;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 377);
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
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(0, 377);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // XuLyBase0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.grbDieuKienLoc);
            this.FilterType = "4";
            this.Name = "XuLyBase0";
            this.Size = new System.Drawing.Size(571, 420);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.XuLyBase_VisibleChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.grbDieuKienLoc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDieuKienLoc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        protected System.Windows.Forms.Timer timerViewReport;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnNhan;
        public System.Windows.Forms.Button btnHuy;




    }
}