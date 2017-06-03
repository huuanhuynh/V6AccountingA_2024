namespace V6RptEditor
{
    partial class FormRptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRptEditor));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.ovalShape1 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelPgrid = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStripTree1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyFormulaNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStripFormula = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_EditFormula = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripTree1.SuspendLayout();
            this.contextMenuStripFormula.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.AutoScroll = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BackColor = System.Drawing.Color.DarkGray;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.shapeContainer1);
            this.mainPanel.Location = new System.Drawing.Point(218, 27);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(778, 626);
            this.mainPanel.TabIndex = 2;
            this.mainPanel.TabStop = true;
            this.mainPanel.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.mainPanel.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape3,
            this.ovalShape1,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(776, 624);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape3
            // 
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 200;
            this.lineShape3.X2 = 154;
            this.lineShape3.Y1 = 100;
            this.lineShape3.Y2 = 192;
            // 
            // ovalShape1
            // 
            this.ovalShape1.Location = new System.Drawing.Point(150, 170);
            this.ovalShape1.Name = "ovalShape1";
            this.ovalShape1.Size = new System.Drawing.Size(80, 80);
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 100;
            this.lineShape2.X2 = 140;
            this.lineShape2.Y1 = 250;
            this.lineShape2.Y2 = 100;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 60;
            this.lineShape1.X2 = 100;
            this.lineShape1.Y1 = 100;
            this.lineShape1.Y2 = 250;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "rpt";
            this.saveFileDialog1.Filter = "Crystal report *.rpt|*.rpt";
            this.saveFileDialog1.Title = "Save as...";
            // 
            // panelPgrid
            // 
            this.panelPgrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelPgrid.Location = new System.Drawing.Point(0, 322);
            this.panelPgrid.Name = "panelPgrid";
            this.panelPgrid.Size = new System.Drawing.Size(212, 331);
            this.panelPgrid.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.saveasToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveAsToolStripMenuItem.Text = "Save &as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem1.Text = "&Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem1.Text = "&Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveasToolStripMenuItem1
            // 
            this.saveasToolStripMenuItem1.Name = "saveasToolStripMenuItem1";
            this.saveasToolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.saveasToolStripMenuItem1.Text = "Save &as...";
            this.saveasToolStripMenuItem1.Click += new System.EventHandler(this.saveasToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.status2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 656);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status1
            // 
            this.status1.AutoSize = false;
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(158, 17);
            this.status1.Text = "Open file name:";
            this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status2
            // 
            this.status2.Name = "status2";
            this.status2.Size = new System.Drawing.Size(22, 17);
            this.status2.Text = "___";
            this.status2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStripTree1;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(212, 252);
            this.treeView1.TabIndex = 9;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStripTree1
            // 
            this.contextMenuStripTree1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFormulaNameToolStripMenuItem});
            this.contextMenuStripTree1.Name = "contextMenuStripTree1";
            this.contextMenuStripTree1.Size = new System.Drawing.Size(179, 26);
            // 
            // copyFormulaNameToolStripMenuItem
            // 
            this.copyFormulaNameToolStripMenuItem.Name = "copyFormulaNameToolStripMenuItem";
            this.copyFormulaNameToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.copyFormulaNameToolStripMenuItem.Text = "CopyFormulaName";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DatabaseFields.png");
            this.imageList1.Images.SetKeyName(1, "FormulaFields.png");
            this.imageList1.Images.SetKeyName(2, "GroupNameFields.png");
            this.imageList1.Images.SetKeyName(3, "ParameterFields.png");
            this.imageList1.Images.SetKeyName(4, "RunningTotalFields.png");
            this.imageList1.Images.SetKeyName(5, "SpecialFields.png");
            this.imageList1.Images.SetKeyName(6, "UnboundFields.png");
            this.imageList1.Images.SetKeyName(7, "DataTable.png");
            this.imageList1.Images.SetKeyName(8, "FieldUnuse.png");
            this.imageList1.Images.SetKeyName(9, "FieldUsed.png");
            // 
            // contextMenuStripFormula
            // 
            this.contextMenuStripFormula.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_EditFormula});
            this.contextMenuStripFormula.Name = "contextMenuStripTree1";
            this.contextMenuStripFormula.Size = new System.Drawing.Size(140, 26);
            // 
            // toolStripMenuItem_EditFormula
            // 
            this.toolStripMenuItem_EditFormula.Name = "toolStripMenuItem_EditFormula";
            this.toolStripMenuItem_EditFormula.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_EditFormula.Text = "Edit formula";
            this.toolStripMenuItem_EditFormula.Click += new System.EventHandler(this.toolStripMenuItem_EditFormula_Click);
            // 
            // FormRptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 678);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelPgrid);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormRptEditor";
            this.Text = "V6RptEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripTree1.ResumeLayout(false);
            this.contextMenuStripFormula.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Panel panelPgrid;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.ToolStripStatusLabel status2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTree1;
        private System.Windows.Forms.ToolStripMenuItem copyFormulaNameToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFormula;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_EditFormula;
    }
}

