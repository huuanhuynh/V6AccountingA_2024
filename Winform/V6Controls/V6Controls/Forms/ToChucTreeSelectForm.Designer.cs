namespace V6Controls.Forms
{
    partial class ToChucTreeSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToChucTreeSelectForm));
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toChucTreeListView1 = new V6Controls.Controls.ToChucTree.ToChucTreeListView();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleDescription = "REPORTB00005";
            this.btnThoat.AccessibleName = "";
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::V6Controls.Properties.Resources.Cancel;
            this.btnThoat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnThoat.Location = new System.Drawing.Point(90, 479);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 40);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Tag = "Escape";
            this.btnThoat.Text = "&Hủy";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(2, 479);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
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
            this.imageList1.Images.SetKeyName(7, "HammerBlack16.png");
            this.imageList1.Images.SetKeyName(8, "LightBlack16.png");
            this.imageList1.Images.SetKeyName(9, "ToolBox16.png");
            this.imageList1.Images.SetKeyName(10, "UserBlack16.png");
            this.imageList1.Images.SetKeyName(11, "UserList16.png");
            // 
            // toChucTreeListView1
            // 
            this.toChucTreeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toChucTreeListView1.CheckBoxes = System.Windows.Forms.CheckBoxesTypes.Recursive;
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.toChucTreeListView1.Comparer = treeListViewItemCollectionComparer1;
            this.toChucTreeListView1.GridLines = true;
            this.toChucTreeListView1.HideSelection = false;
            this.toChucTreeListView1.ID_Field = "NODE";
            this.toChucTreeListView1.ImageIndex_Field = "picture";
            this.toChucTreeListView1.IsBold_Field = "isBold";
            this.toChucTreeListView1.Location = new System.Drawing.Point(2, 2);
            this.toChucTreeListView1.MultiSelect = false;
            this.toChucTreeListView1.Name = "toChucTreeListView1";
            this.toChucTreeListView1.ParentIdField = "PARENT";
            this.toChucTreeListView1.Size = new System.Drawing.Size(430, 471);
            this.toChucTreeListView1.SmallImageList = this.imageList1;
            this.toChucTreeListView1.Sort_Field = "fsort";
            this.toChucTreeListView1.TabIndex = 0;
            this.toChucTreeListView1.Text_Field = "NAME";
            this.toChucTreeListView1.UseCompatibleStateImageBehavior = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(243, 479);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "&Chọn hết";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnSelect
            // 
            this.btnUnSelect.Location = new System.Drawing.Point(324, 479);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUnSelect.TabIndex = 4;
            this.btnUnSelect.Text = "&Bỏ chọn";
            this.btnUnSelect.UseVisualStyleBackColor = true;
            this.btnUnSelect.Click += new System.EventHandler(this.btnUnSelect_Click);
            // 
            // ToChucTreeSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 522);
            this.Controls.Add(this.btnUnSelect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.toChucTreeListView1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Name = "ToChucTreeSelectForm";
            this.Text = "ToChucTreeSelectForm";
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnThoat, 0);
            this.Controls.SetChildIndex(this.toChucTreeListView1, 0);
            this.Controls.SetChildIndex(this.btnSelectAll, 0);
            this.Controls.SetChildIndex(this.btnUnSelect, 0);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.ToChucTree.ToChucTreeListView toChucTreeListView1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnSelect;
    }
}