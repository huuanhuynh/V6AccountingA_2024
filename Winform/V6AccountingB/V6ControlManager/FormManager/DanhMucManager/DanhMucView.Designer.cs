namespace V6ControlManager.FormManager.DanhMucManager
{
    partial class DanhMucView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DanhMucView));
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewListInfoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDoiMa = new System.Windows.Forms.Button();
            this.btnFull = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNhom = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.cboFilter = new V6Controls.V6ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100",
            "500",
            "1000"});
            this.comboBox1.Location = new System.Drawing.Point(729, 345);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(62, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "DANHMUCVIEWL00002";
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(620, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Lượng hiển thị";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AccessibleDescription = "DANHMUCVIEWL00003";
            this.lblTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(210, 350);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(59, 13);
            this.lblTotalPage.TabIndex = 17;
            this.lblTotalPage.Text = "Tổng cộng";
            this.lblTotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCurrentPage.Location = new System.Drawing.Point(79, 345);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(48, 20);
            this.txtCurrentPage.TabIndex = 14;
            this.txtCurrentPage.TextChanged += new System.EventHandler(this.txtCurrentPage_TextChanged);
            this.txtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPage_KeyPress);
            this.txtCurrentPage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPage_KeyUp);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(3, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(788, 277);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            this.UID.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewListInfoMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(208, 26);
            // 
            // viewListInfoMenu
            // 
            this.viewListInfoMenu.AccessibleDescription = "INVOICEM00049";
            this.viewListInfoMenu.Name = "viewListInfoMenu";
            this.viewListInfoMenu.Size = new System.Drawing.Size(207, 22);
            this.viewListInfoMenu.Text = "Xem thông tin danh mục";
            this.viewListInfoMenu.Click += new System.EventHandler(this.viewListInfoMenu_Click);
            // 
            // btnDoiMa
            // 
            this.btnDoiMa.AccessibleDescription = "DANHMUCVIEWB00005";
            this.btnDoiMa.Image = global::V6ControlManager.Properties.Resources.Replace;
            this.btnDoiMa.Location = new System.Drawing.Point(251, 3);
            this.btnDoiMa.Name = "btnDoiMa";
            this.btnDoiMa.Size = new System.Drawing.Size(56, 55);
            this.btnDoiMa.TabIndex = 4;
            this.btnDoiMa.Tag = "F6";
            this.btnDoiMa.Text = "Đổi mã";
            this.btnDoiMa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDoiMa.UseVisualStyleBackColor = true;
            this.btnDoiMa.Click += new System.EventHandler(this.btnDoiMa_Click);
            // 
            // btnFull
            // 
            this.btnFull.AccessibleDescription = "DANHMUCVIEWB00011";
            this.btnFull.Image = global::V6ControlManager.Properties.Resources.ZoomIn32;
            this.btnFull.Location = new System.Drawing.Point(623, 3);
            this.btnFull.Name = "btnFull";
            this.btnFull.Size = new System.Drawing.Size(56, 55);
            this.btnFull.TabIndex = 10;
            this.btnFull.Tag = "F11";
            this.btnFull.Text = "Phóng";
            this.btnFull.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFull.UseVisualStyleBackColor = true;
            this.btnFull.Click += new System.EventHandler(this.btnFull_Click);
            // 
            // btnLast
            // 
            this.btnLast.AccessibleDescription = ".";
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLast.Enabled = false;
            this.btnLast.Image = global::V6ControlManager.Properties.Resources.Last;
            this.btnLast.Location = new System.Drawing.Point(172, 343);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(32, 23);
            this.btnLast.TabIndex = 16;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.AccessibleDescription = ".";
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Enabled = false;
            this.btnNext.Image = global::V6ControlManager.Properties.Resources.Forward;
            this.btnNext.Location = new System.Drawing.Point(134, 343);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 23);
            this.btnNext.TabIndex = 15;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.AccessibleDescription = ".";
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Image = global::V6ControlManager.Properties.Resources.Back;
            this.btnPrevious.Location = new System.Drawing.Point(41, 343);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 23);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.AccessibleDescription = ".";
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFirst.Enabled = false;
            this.btnFirst.Image = global::V6ControlManager.Properties.Resources.First;
            this.btnFirst.Location = new System.Drawing.Point(3, 343);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(32, 23);
            this.btnFirst.TabIndex = 12;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AccessibleDescription = "DANHMUCVIEWB00003";
            this.btnXoa.Image = global::V6ControlManager.Properties.Resources.Delete32;
            this.btnXoa.Location = new System.Drawing.Point(127, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(56, 55);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Tag = "F8";
            this.btnXoa.Text = "Xóa";
            this.btnXoa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.AccessibleDescription = "DANHMUCVIEWB00002";
            this.btnSua.Image = global::V6ControlManager.Properties.Resources.Editpage;
            this.btnSua.Location = new System.Drawing.Point(65, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(56, 55);
            this.btnSua.TabIndex = 1;
            this.btnSua.Tag = "F3";
            this.btnSua.Text = "Sửa";
            this.btnSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleDescription = "DANHMUCVIEWB00012";
            this.btnBack.Image = global::V6ControlManager.Properties.Resources.Back2;
            this.btnBack.Location = new System.Drawing.Point(685, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(56, 55);
            this.btnBack.TabIndex = 20;
            this.btnBack.Tag = "Escape";
            this.btnBack.Text = "Quay ra";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAll
            // 
            this.btnAll.AccessibleDescription = "DANHMUCVIEWB00010";
            this.btnAll.Image = global::V6ControlManager.Properties.Resources.Refresh;
            this.btnAll.Location = new System.Drawing.Point(561, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(56, 55);
            this.btnAll.TabIndex = 9;
            this.btnAll.Tag = "F10";
            this.btnAll.Text = "Tất cả";
            this.btnAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNhom
            // 
            this.btnNhom.AccessibleDescription = "DANHMUCVIEWB00009";
            this.btnNhom.Enabled = false;
            this.btnNhom.Image = global::V6ControlManager.Properties.Resources.IconTree32;
            this.btnNhom.Location = new System.Drawing.Point(499, 3);
            this.btnNhom.Name = "btnNhom";
            this.btnNhom.Size = new System.Drawing.Size(56, 55);
            this.btnNhom.TabIndex = 8;
            this.btnNhom.Tag = "F6, Control";
            this.btnNhom.Text = "Nhóm";
            this.btnNhom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNhom.UseVisualStyleBackColor = true;
            this.btnNhom.Click += new System.EventHandler(this.btnNhom_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "DANHMUCVIEWB00008";
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print;
            this.btnIn.Location = new System.Drawing.Point(437, 3);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(56, 55);
            this.btnIn.TabIndex = 7;
            this.btnIn.Tag = "F7";
            this.btnIn.Text = "In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnXem
            // 
            this.btnXem.AccessibleDescription = "DANHMUCVIEWB00007";
            this.btnXem.Image = global::V6ControlManager.Properties.Resources.ViewDetails;
            this.btnXem.Location = new System.Drawing.Point(375, 3);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(56, 55);
            this.btnXem.TabIndex = 6;
            this.btnXem.Tag = "F2";
            this.btnXem.Text = "Xem";
            this.btnXem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnFind
            // 
            this.btnFind.AccessibleDescription = "DANHMUCVIEWB00006";
            this.btnFind.Image = global::V6ControlManager.Properties.Resources.Search;
            this.btnFind.Location = new System.Drawing.Point(313, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 55);
            this.btnFind.TabIndex = 5;
            this.btnFind.Tag = "F5;F5, Shift";
            this.btnFind.Text = "Tìm";
            this.btnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleDescription = "DANHMUCVIEWB00004";
            this.btnCopy.Image = global::V6ControlManager.Properties.Resources.Copy;
            this.btnCopy.Location = new System.Drawing.Point(189, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(56, 55);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "Copy";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnThem
            // 
            this.btnThem.AccessibleDescription = "DANHMUCVIEWB00001";
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.Location = new System.Drawing.Point(3, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(56, 55);
            this.btnThem.TabIndex = 0;
            this.btnThem.Tag = "F4";
            this.btnThem.Text = "Mới";
            this.btnThem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.EnabledChanged += new System.EventHandler(this.btnThem_EnabledChanged);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // cboFilter
            // 
            this.cboFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilter.BackColor = System.Drawing.SystemColors.Window;
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(499, 345);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(148, 21);
            this.cboFilter.TabIndex = 19;
            this.cboFilter.Visible = false;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AccessibleDescription = "DANHMUCVIEWL00004";
            this.lblFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilter.Location = new System.Drawing.Point(375, 349);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(118, 14);
            this.lblFilter.TabIndex = 17;
            this.lblFilter.Text = "Lọc";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFilter.Visible = false;
            // 
            // DanhMucView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFull);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnNhom);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDoiMa);
            this.EnableCtrlF12 = false;
            this.FilterType = "2";
            this.Name = "DanhMucView";
            this.Size = new System.Drawing.Size(794, 373);
            this.Load += new System.EventHandler(this.DanhMucView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public V6Controls.V6ColorDataGridView dataGridView1;
        public System.Windows.Forms.Button btnThem;
        public System.Windows.Forms.Button btnSua;
        public System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        public System.Windows.Forms.Button btnCopy;
        public System.Windows.Forms.Button btnFind;
        public System.Windows.Forms.Button btnIn;
        public System.Windows.Forms.Button btnAll;
        private V6Controls.V6ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnXem;
        public System.Windows.Forms.Button btnBack;
        public System.Windows.Forms.Button btnFull;
        public System.Windows.Forms.Button btnDoiMa;
        public System.Windows.Forms.Button btnNhom;
        private V6Controls.V6ComboBox cboFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewListInfoMenu;
    }
}
