namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuNhapKho.ChonDeNghiNhap
{
    partial class INY_PNKho_KetQua
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

        #region Component Designer generated code

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
            this.menuChucNang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.thayTheMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.thayTheNhieuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewSummary1 = new V6Controls.Controls.GridViewSummary();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuChucNang.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView1.ContextMenuStrip = this.menuChucNang;
            this.dataGridView1.Control_S = true;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(529, 498);
            this.dataGridView1.Space_Bar = true;
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // menuChucNang
            // 
            this.menuChucNang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thayTheMenu,
            this.thayTheNhieuMenu});
            this.menuChucNang.Name = "menuChucNang";
            this.menuChucNang.Size = new System.Drawing.Size(154, 48);
            // 
            // thayTheMenu
            // 
            this.thayTheMenu.AccessibleDescription = "INVOICEM00008";
            this.thayTheMenu.Name = "thayTheMenu";
            this.thayTheMenu.Size = new System.Drawing.Size(153, 22);
            this.thayTheMenu.Text = "Thay thế";
            this.thayTheMenu.Click += new System.EventHandler(this.thayTheMenu_Click);
            // 
            // thayTheNhieuMenu
            // 
            this.thayTheNhieuMenu.AccessibleDescription = "INVOICEM00027";
            this.thayTheNhieuMenu.Name = "thayTheNhieuMenu";
            this.thayTheNhieuMenu.Size = new System.Drawing.Size(153, 22);
            this.thayTheNhieuMenu.Text = "Thay thế nhiều";
            this.thayTheNhieuMenu.Click += new System.EventHandler(this.thayTheNhieuMenu_Click);
            // 
            // gridViewSummary1
            // 
            this.gridViewSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary1.DataGridView = this.dataGridView1;
            this.gridViewSummary1.Location = new System.Drawing.Point(0, 498);
            this.gridViewSummary1.Name = "gridViewSummary1";
            this.gridViewSummary1.Size = new System.Drawing.Size(529, 22);
            this.gridViewSummary1.SumCondition = null;
            this.gridViewSummary1.TabIndex = 3;
            // 
            // INY_PNKho_KetQua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridViewSummary1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "INY_PNKho_KetQua";
            this.Size = new System.Drawing.Size(529, 520);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuChucNang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public V6Controls.V6ColorDataGridView dataGridView1;
        private V6Controls.Controls.GridViewSummary gridViewSummary1;
        private System.Windows.Forms.ContextMenuStrip menuChucNang;
        private System.Windows.Forms.ToolStripMenuItem thayTheMenu;
        private System.Windows.Forms.ToolStripMenuItem thayTheNhieuMenu;
    }
}
