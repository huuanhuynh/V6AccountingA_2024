namespace V6ControlManager.FormManager.NhanSu
{
    partial class NhanSuView
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
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NhanSuView));
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.treeListViewAuto1 = new V6Controls.Controls.ToChucTree.ToChucTreeListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnDoiMa = new System.Windows.Forms.Button();
            this.btnFull = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AccessibleDescription = "DANHMUCVIEWL00003";
            this.lblTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(3, 582);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(59, 13);
            this.lblTotalPage.TabIndex = 16;
            this.lblTotalPage.Text = "Tổng cộng";
            this.lblTotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeListViewAuto1
            // 
            this.treeListViewAuto1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListViewAuto1.Comparer = treeListViewItemCollectionComparer1;
            this.treeListViewAuto1.GridLines = true;
            this.treeListViewAuto1.HideSelection = false;
            this.treeListViewAuto1.ID_Field = "NODE";
            this.treeListViewAuto1.ImageIndex_Field = "PICTURE";
            this.treeListViewAuto1.IsBold_Field = "isBold";
            this.treeListViewAuto1.Location = new System.Drawing.Point(3, 64);
            this.treeListViewAuto1.MultiSelect = false;
            this.treeListViewAuto1.Name = "treeListViewAuto1";
            this.treeListViewAuto1.ParentIdField = "PARENT";
            this.treeListViewAuto1.Size = new System.Drawing.Size(743, 515);
            this.treeListViewAuto1.SmallImageList = this.imageList1;
            this.treeListViewAuto1.Sort_Field = "fsort";
            this.treeListViewAuto1.TabIndex = 22;
            this.treeListViewAuto1.Text_Field = "NAME";
            this.treeListViewAuto1.UseCompatibleStateImageBehavior = false;
            this.treeListViewAuto1.Click += new System.EventHandler(this.treeListViewAuto1_Click);
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
            this.btnFull.Location = new System.Drawing.Point(561, 3);
            this.btnFull.Name = "btnFull";
            this.btnFull.Size = new System.Drawing.Size(56, 55);
            this.btnFull.TabIndex = 9;
            this.btnFull.Tag = "F11";
            this.btnFull.Text = "Phóng";
            this.btnFull.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFull.UseVisualStyleBackColor = true;
            this.btnFull.Click += new System.EventHandler(this.btnFull_Click);
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
            this.btnBack.Location = new System.Drawing.Point(623, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(56, 55);
            this.btnBack.TabIndex = 19;
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
            this.btnAll.Location = new System.Drawing.Point(499, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(56, 55);
            this.btnAll.TabIndex = 8;
            this.btnAll.Tag = "F10";
            this.btnAll.Text = "Tất cả";
            this.btnAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
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
            this.btnFind.Tag = "F5";
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
            // NhanSuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.treeListViewAuto1);
            this.Controls.Add(this.btnDoiMa);
            this.Controls.Add(this.btnFull);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnThem);
            this.Name = "NhanSuView";
            this.Size = new System.Drawing.Size(749, 600);
            this.Load += new System.EventHandler(this.NhanSuView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFull;
        private System.Windows.Forms.Button btnDoiMa;
        private System.Windows.Forms.ImageList imageList1;
        private V6Controls.Controls.ToChucTree.ToChucTreeListView treeListViewAuto1;
    }
}
