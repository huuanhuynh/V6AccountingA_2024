namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class XuLy48Base
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.printGridMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.grbDieuKienLoc = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.dataGridView2 = new V6Controls.V6ColorDataGridView();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.gridViewSummary1 = new V6Controls.Controls.GridViewSummary();
            this.btnExport3 = new wyDay.Controls.SplitButton();
            this.btnSuaLine = new V6Controls.Controls.V6FormButton();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbDieuKienLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelMenu,
            this.printGridMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 48);
            // 
            // exportToExcelMenu
            // 
            this.exportToExcelMenu.AccessibleDescription = "REPORTM00001";
            this.exportToExcelMenu.Name = "exportToExcelMenu";
            this.exportToExcelMenu.Size = new System.Drawing.Size(153, 22);
            this.exportToExcelMenu.Text = "Export To Excel";
            this.exportToExcelMenu.Click += new System.EventHandler(this.exportToExcelMenu_Click);
            // 
            // printGridMenu
            // 
            this.printGridMenu.AccessibleDescription = "REPORTM00007";
            this.printGridMenu.Name = "printGridMenu";
            this.printGridMenu.Size = new System.Drawing.Size(153, 22);
            this.printGridMenu.Text = "Print Grid";
            this.printGridMenu.Click += new System.EventHandler(this.printGrid_Click);
            // 
            // timerViewReport
            // 
            this.timerViewReport.Tick += new System.EventHandler(this.timerViewReport_Tick);
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
            this.dataGridView1.Location = new System.Drawing.Point(307, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(662, 625);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // grbDieuKienLoc
            // 
            this.grbDieuKienLoc.AccessibleDescription = "REPORTL00011";
            this.grbDieuKienLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDieuKienLoc.Controls.Add(this.panel1);
            this.grbDieuKienLoc.Location = new System.Drawing.Point(2, 66);
            this.grbDieuKienLoc.Name = "grbDieuKienLoc";
            this.grbDieuKienLoc.Size = new System.Drawing.Size(299, 585);
            this.grbDieuKienLoc.TabIndex = 0;
            this.grbDieuKienLoc.TabStop = false;
            this.grbDieuKienLoc.Text = "Conditional option (Điều Kiện Lọc)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 560);
            this.panel1.TabIndex = 0;
            this.panel1.Leave += new System.EventHandler(this.panel1_Leave);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(96, 3);
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
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(8, 3);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView2.Location = new System.Drawing.Point(307, 556);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(662, 95);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = ".";
            this.btnSuaTTMauBC.Image = global::V6ControlManager.Properties.Resources.Setting24;
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(193, 3);
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
            this.btnThemMauBC.Location = new System.Drawing.Point(223, 3);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(30, 30);
            this.btnThemMauBC.TabIndex = 7;
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // gridViewSummary1
            // 
            this.gridViewSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary1.DataGridView = this.dataGridView1;
            this.gridViewSummary1.Location = new System.Drawing.Point(307, 628);
            this.gridViewSummary1.Name = "gridViewSummary1";
            this.gridViewSummary1.Size = new System.Drawing.Size(662, 23);
            this.gridViewSummary1.SumCondition = null;
            this.gridViewSummary1.TabIndex = 0;
            // 
            // btnExport3
            // 
            this.btnExport3.AccessibleDescription = ".";
            this.btnExport3.AutoSize = true;
            this.btnExport3.ContextMenuStrip = this.contextMenuStrip1;
            this.btnExport3.Image = global::V6ControlManager.Properties.Resources.Export24;
            this.btnExport3.Location = new System.Drawing.Point(253, 3);
            this.btnExport3.Name = "btnExport3";
            this.btnExport3.Size = new System.Drawing.Size(50, 30);
            this.btnExport3.SplitMenuStrip = this.contextMenuStrip1;
            this.btnExport3.TabIndex = 17;
            this.btnExport3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport3.UseVisualStyleBackColor = true;
            this.btnExport3.Click += new System.EventHandler(this.btnExport3_Click);
            // 
            // btnSuaLine
            // 
            this.btnSuaLine.AccessibleDescription = ".";
            this.btnSuaLine.Image = global::V6ControlManager.Properties.Resources.LineEdit24;
            this.btnSuaLine.Location = new System.Drawing.Point(193, 33);
            this.btnSuaLine.Name = "btnSuaLine";
            this.btnSuaLine.Size = new System.Drawing.Size(30, 30);
            this.btnSuaLine.TabIndex = 19;
            this.toolTipV6FormControl.SetToolTip(this.btnSuaLine, "Sửa line");
            this.btnSuaLine.UseVisualStyleBackColor = true;
            this.btnSuaLine.Click += new System.EventHandler(this.btnSuaLine_Click);
            // 
            // XuLy48Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSuaLine);
            this.Controls.Add(this.btnExport3);
            this.Controls.Add(this.gridViewSummary1);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grbDieuKienLoc);
            this.Controls.Add(this.dataGridView2);
            this.FilterType = "4";
            this.Name = "XuLy48Base";
            this.Size = new System.Drawing.Size(974, 654);
            this.Load += new System.EventHandler(this.Form_Load);
            this.SizeChanged += new System.EventHandler(this.XuLy48Base_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.XuLy48Base_VisibleChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbDieuKienLoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDieuKienLoc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelMenu;
        private System.Windows.Forms.ToolStripMenuItem printGridMenu;
        protected System.Windows.Forms.Timer timerViewReport;
        public System.Windows.Forms.Panel panel1;
        protected V6Controls.V6ColorDataGridView dataGridView1;
        public System.Windows.Forms.Button btnNhan;
        public System.Windows.Forms.Button btnHuy;
        protected V6Controls.V6ColorDataGridView dataGridView2;
        private V6Controls.Controls.V6FormButton btnSuaTTMauBC;
        private V6Controls.Controls.V6FormButton btnThemMauBC;
        private V6Controls.Controls.GridViewSummary gridViewSummary1;
        private wyDay.Controls.SplitButton btnExport3;
        private V6Controls.Controls.V6FormButton btnSuaLine;




    }
}