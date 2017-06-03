namespace H_document.Forms
{
    partial class HdocumentEditor
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.ThemDongChuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themDuongKeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themHinhAnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBold = new System.Windows.Forms.ToolStripButton();
            this.btnItalic = new System.Windows.Forms.ToolStripButton();
            this.btnUnderLine = new System.Windows.Forms.ToolStripButton();
            this.btnStreakLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.btnAlignCenter = new System.Windows.Forms.ToolStripButton();
            this.btnAlignRight = new System.Windows.Forms.ToolStripButton();
            this.btnFontSizeIn = new System.Windows.Forms.ToolStripButton();
            this.btnFontSizeOut = new System.Windows.Forms.ToolStripButton();
            this.hdocumentViewer1 = new H_document.Forms.HdocumentViewer();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(3, 54);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(220, 363);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnOpen,
            this.toolStripSeparator3,
            this.btnAdd,
            this.btnCopy,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnBold,
            this.btnItalic,
            this.btnUnderLine,
            this.btnStreakLine,
            this.toolStripSeparator1,
            this.btnAlignLeft,
            this.btnAlignCenter,
            this.btnAlignRight,
            this.btnFontSizeIn,
            this.btnFontSizeOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(600, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::H_document.Properties.Resources.pageblank161;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "Mới";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::H_document.Properties.Resources.save16;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::H_document.Properties.Resources.open16;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Mở";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAdd
            // 
            this.btnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThemDongChuToolStripMenuItem,
            this.themDuongKeToolStripMenuItem,
            this.themHinhAnhToolStripMenuItem});
            this.btnAdd.Image = global::H_document.Properties.Resources.themefont;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 22);
            this.btnAdd.Text = "Thêm";
            // 
            // ThemDongChuToolStripMenuItem
            // 
            this.ThemDongChuToolStripMenuItem.Name = "ThemDongChuToolStripMenuItem";
            this.ThemDongChuToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ThemDongChuToolStripMenuItem.Text = "Thêm dòng chữ";
            this.ThemDongChuToolStripMenuItem.Click += new System.EventHandler(this.ThemDongChuToolStripMenuItem_Click);
            // 
            // themDuongKeToolStripMenuItem
            // 
            this.themDuongKeToolStripMenuItem.Name = "themDuongKeToolStripMenuItem";
            this.themDuongKeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.themDuongKeToolStripMenuItem.Text = "Thêm đường kẻ/khung";
            this.themDuongKeToolStripMenuItem.Click += new System.EventHandler(this.themDuongKeToolStripMenuItem_Click);
            // 
            // themHinhAnhToolStripMenuItem
            // 
            this.themHinhAnhToolStripMenuItem.Name = "themHinhAnhToolStripMenuItem";
            this.themHinhAnhToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.themHinhAnhToolStripMenuItem.Text = "Thêm hình ảnh";
            this.themHinhAnhToolStripMenuItem.Click += new System.EventHandler(this.themHinhAnhToolStripMenuItem_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::H_document.Properties.Resources.copy16;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::H_document.Properties.Resources.cancel_16;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(47, 22);
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBold
            // 
            this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBold.Image = global::H_document.Properties.Resources.bold16;
            this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(23, 22);
            this.btnBold.Text = "In đậm";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItalic.Image = global::H_document.Properties.Resources.italic16;
            this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(23, 22);
            this.btnItalic.Text = "In nghiêng";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnUnderLine
            // 
            this.btnUnderLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnderLine.Image = global::H_document.Properties.Resources.underline16;
            this.btnUnderLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnderLine.Name = "btnUnderLine";
            this.btnUnderLine.Size = new System.Drawing.Size(23, 22);
            this.btnUnderLine.Text = "Gạch chân";
            this.btnUnderLine.Click += new System.EventHandler(this.btnUnderLine_Click);
            // 
            // btnStreakLine
            // 
            this.btnStreakLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStreakLine.Image = global::H_document.Properties.Resources.strikethru16;
            this.btnStreakLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStreakLine.Name = "btnStreakLine";
            this.btnStreakLine.Size = new System.Drawing.Size(23, 22);
            this.btnStreakLine.Text = "Gạch giữa";
            this.btnStreakLine.Click += new System.EventHandler(this.btnStreakLine_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAlignLeft
            // 
            this.btnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignLeft.Image = global::H_document.Properties.Resources.textalignleft16;
            this.btnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlignLeft.Name = "btnAlignLeft";
            this.btnAlignLeft.Size = new System.Drawing.Size(23, 22);
            this.btnAlignLeft.Text = "Canh trái";
            this.btnAlignLeft.Click += new System.EventHandler(this.btnAlignLeft_Click);
            // 
            // btnAlignCenter
            // 
            this.btnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignCenter.Image = global::H_document.Properties.Resources.textaligncenter16;
            this.btnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlignCenter.Name = "btnAlignCenter";
            this.btnAlignCenter.Size = new System.Drawing.Size(23, 22);
            this.btnAlignCenter.Text = "Canh giữa";
            this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);
            // 
            // btnAlignRight
            // 
            this.btnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlignRight.Image = global::H_document.Properties.Resources.textalignright16;
            this.btnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlignRight.Name = "btnAlignRight";
            this.btnAlignRight.Size = new System.Drawing.Size(23, 22);
            this.btnAlignRight.Text = "Canh phải";
            this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);
            // 
            // btnFontSizeIn
            // 
            this.btnFontSizeIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontSizeIn.Image = global::H_document.Properties.Resources.fontsizeincrease16;
            this.btnFontSizeIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontSizeIn.Name = "btnFontSizeIn";
            this.btnFontSizeIn.Size = new System.Drawing.Size(23, 22);
            this.btnFontSizeIn.Text = "Chữ lớn lên";
            // 
            // btnFontSizeOut
            // 
            this.btnFontSizeOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontSizeOut.Image = global::H_document.Properties.Resources.fontsizedecrease16;
            this.btnFontSizeOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontSizeOut.Name = "btnFontSizeOut";
            this.btnFontSizeOut.Size = new System.Drawing.Size(23, 22);
            this.btnFontSizeOut.Text = "Chữ nhỏ lại";
            // 
            // hdocumentViewer1
            // 
            this.hdocumentViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdocumentViewer1.AutoScroll = true;
            this.hdocumentViewer1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.hdocumentViewer1.Location = new System.Drawing.Point(229, 28);
            this.hdocumentViewer1.MouseHoverDocumentObject = null;
            this.hdocumentViewer1.Name = "hdocumentViewer1";
            this.hdocumentViewer1.Size = new System.Drawing.Size(368, 389);
            this.hdocumentViewer1.TabIndex = 3;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Trang chính";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(103, 28);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(84, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "Phần chi tiết";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // HdocumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.hdocumentViewer1);
            this.Name = "HdocumentEditor";
            this.Size = new System.Drawing.Size(600, 420);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HdocumentViewer hdocumentViewer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnBold;
        private System.Windows.Forms.ToolStripButton btnItalic;
        private System.Windows.Forms.ToolStripButton btnUnderLine;
        private System.Windows.Forms.ToolStripButton btnStreakLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAlignLeft;
        private System.Windows.Forms.ToolStripDropDownButton btnAdd;
        private System.Windows.Forms.ToolStripMenuItem ThemDongChuToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnAlignCenter;
        private System.Windows.Forms.ToolStripButton btnAlignRight;
        private System.Windows.Forms.ToolStripButton btnFontSizeIn;
        private System.Windows.Forms.ToolStripButton btnFontSizeOut;
        private System.Windows.Forms.ToolStripMenuItem themDuongKeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themHinhAnhToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}
