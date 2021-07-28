namespace V6ControlManager.FormManager.NhanSu
{
    partial class TienLuongView2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TienLuongView2));
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.tochucTree1 = new V6Controls.Controls.ToChucTree.ToChucTreeListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listBoxMenu = new System.Windows.Forms.ListBox();
            this.panelView = new System.Windows.Forms.Panel();
            this.txtLoc = new V6Controls.V6ColorTextBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AccessibleDescription = "DANHMUCVIEWL00003";
            this.lblTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(3, 583);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(59, 13);
            this.lblTotalPage.TabIndex = 16;
            this.lblTotalPage.Text = "Tổng cộng";
            this.lblTotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeListViewAuto1
            // 
            this.tochucTree1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.tochucTree1.Comparer = treeListViewItemCollectionComparer1;
            this.tochucTree1.GridLines = true;
            this.tochucTree1.HideSelection = false;
            this.tochucTree1.ID_Field = "NODE";
            this.tochucTree1.ImageIndex_Field = "PICTURE";
            this.tochucTree1.IsBold_Field = "isBold";
            this.tochucTree1.Location = new System.Drawing.Point(3, 29);
            this.tochucTree1.MultiSelect = false;
            this.tochucTree1.Name = "tochucTree1";
            this.tochucTree1.ParentIdField = "PARENT";
            this.tochucTree1.Size = new System.Drawing.Size(242, 244);
            this.tochucTree1.SmallImageList = this.imageList1;
            this.tochucTree1.Sort_Field = "fsort";
            this.tochucTree1.TabIndex = 22;
            this.tochucTree1.Text_Field = "NAME";
            this.tochucTree1.UseCompatibleStateImageBehavior = false;
            this.tochucTree1.Click += new System.EventHandler(this.treeListViewAuto1_Click);
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
            // listBox1
            // 
            this.listBoxMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxMenu.FormattingEnabled = true;
            this.listBoxMenu.Location = new System.Drawing.Point(3, 279);
            this.listBoxMenu.Name = "listBoxMenu";
            this.listBoxMenu.Size = new System.Drawing.Size(242, 303);
            this.listBoxMenu.TabIndex = 24;
            this.listBoxMenu.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBoxMenu.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.Location = new System.Drawing.Point(251, 3);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(498, 592);
            this.panelView.TabIndex = 25;
            // 
            // txtLoc
            // 
            this.txtLoc.AccessibleName = "";
            this.txtLoc.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLoc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoc.HoverColor = System.Drawing.Color.Yellow;
            this.txtLoc.LeaveColor = System.Drawing.Color.White;
            this.txtLoc.Location = new System.Drawing.Point(3, 3);
            this.txtLoc.Name = "txtLoc";
            this.txtLoc.Size = new System.Drawing.Size(161, 20);
            this.txtLoc.TabIndex = 26;
            // 
            // btnLoc
            // 
            this.btnLoc.AccessibleDescription = "DANHMUCVIEWB00013";
            this.btnLoc.Location = new System.Drawing.Point(170, 1);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 23);
            this.btnLoc.TabIndex = 27;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // TienLuongView2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.txtLoc);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.listBoxMenu);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.tochucTree1);
            this.Name = "TienLuongView2";
            this.Size = new System.Drawing.Size(749, 600);
            this.Load += new System.EventHandler(this.TienLuongView2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.ImageList imageList1;
        private V6Controls.Controls.ToChucTree.ToChucTreeListView tochucTree1;
        private System.Windows.Forms.ListBox listBoxMenu;
        private System.Windows.Forms.Panel panelView;
        private V6Controls.V6ColorTextBox txtLoc;
        private System.Windows.Forms.Button btnLoc;
    }
}
