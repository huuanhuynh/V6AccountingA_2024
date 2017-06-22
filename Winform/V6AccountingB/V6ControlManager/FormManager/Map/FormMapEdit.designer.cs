namespace V6ControlManager.FormManager.Map
{
    partial class FormMapEdit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grbEditMode = new System.Windows.Forms.GroupBox();
            this.radCopyFrom = new System.Windows.Forms.RadioButton();
            this.radEditMove = new System.Windows.Forms.RadioButton();
            this.radReplaceNew = new System.Windows.Forms.RadioButton();
            this.radNo = new System.Windows.Forms.RadioButton();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grbKhuVuc = new System.Windows.Forms.GroupBox();
            this.chkNone = new System.Windows.Forms.CheckBox();
            this.buttonChooseImage = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rbtTiengViet = new System.Windows.Forms.RadioButton();
            this.rbtEnglish = new System.Windows.Forms.RadioButton();
            this.btnRightSize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbEditMode.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.grbKhuVuc.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbNgonNgu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 570);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(76, 570);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // grbEditMode
            // 
            this.grbEditMode.Controls.Add(this.radCopyFrom);
            this.grbEditMode.Controls.Add(this.radEditMove);
            this.grbEditMode.Controls.Add(this.radReplaceNew);
            this.grbEditMode.Controls.Add(this.radNo);
            this.grbEditMode.Location = new System.Drawing.Point(470, -1);
            this.grbEditMode.Name = "grbEditMode";
            this.grbEditMode.Size = new System.Drawing.Size(340, 35);
            this.grbEditMode.TabIndex = 3;
            this.grbEditMode.TabStop = false;
            this.grbEditMode.Text = "Edit Mode";
            // 
            // radCopyFrom
            // 
            this.radCopyFrom.AutoSize = true;
            this.radCopyFrom.Location = new System.Drawing.Point(248, 12);
            this.radCopyFrom.Name = "radCopyFrom";
            this.radCopyFrom.Size = new System.Drawing.Size(75, 17);
            this.radCopyFrom.TabIndex = 1;
            this.radCopyFrom.Text = "Copy From";
            this.toolTipV6FormControl.SetToolTip(this.radCopyFrom, "Chọn bên phải, click vào đây.\r\nChọn một ô đã vẽ, kéo sang vị trí mới.\r\nEnter.");
            this.radCopyFrom.UseVisualStyleBackColor = true;
            this.radCopyFrom.CheckedChanged += new System.EventHandler(this.radCopyFrom_CheckedChanged);
            // 
            // radEditMove
            // 
            this.radEditMove.AutoSize = true;
            this.radEditMove.Location = new System.Drawing.Point(153, 12);
            this.radEditMove.Name = "radEditMove";
            this.radEditMove.Size = new System.Drawing.Size(73, 17);
            this.radEditMove.TabIndex = 1;
            this.radEditMove.Text = "Edit Move";
            this.toolTipV6FormControl.SetToolTip(this.radEditMove, "Sửa lại ô đang chọn, kéo thả nguyên ô, các điểm, delete điểm.");
            this.radEditMove.UseVisualStyleBackColor = true;
            this.radEditMove.CheckedChanged += new System.EventHandler(this.radEditMove_CheckedChanged);
            // 
            // radReplaceNew
            // 
            this.radReplaceNew.AutoSize = true;
            this.radReplaceNew.Location = new System.Drawing.Point(55, 12);
            this.radReplaceNew.Name = "radReplaceNew";
            this.radReplaceNew.Size = new System.Drawing.Size(88, 17);
            this.radReplaceNew.TabIndex = 0;
            this.radReplaceNew.Text = "Replace new";
            this.toolTipV6FormControl.SetToolTip(this.radReplaceNew, "Vẽ lại ô mới cho ô hoặc dòng đang chọn");
            this.radReplaceNew.UseVisualStyleBackColor = true;
            this.radReplaceNew.CheckedChanged += new System.EventHandler(this.chkEdit_CheckedChanged);
            // 
            // radNo
            // 
            this.radNo.AutoSize = true;
            this.radNo.Checked = true;
            this.radNo.Location = new System.Drawing.Point(6, 12);
            this.radNo.Name = "radNo";
            this.radNo.Size = new System.Drawing.Size(39, 17);
            this.radNo.TabIndex = 0;
            this.radNo.TabStop = true;
            this.radNo.Text = "No";
            this.radNo.UseVisualStyleBackColor = true;
            this.radNo.CheckedChanged += new System.EventHandler(this.radNo_CheckedChanged);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.ContextMenuStrip = this.contextMenuStrip2;
            this.comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(9, 13);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(208, 21);
            this.comboBoxGroup.TabIndex = 5;
            this.comboBoxGroup.TabStop = false;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(111, 26);
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
            this.grbKhuVuc.Controls.Add(this.chkNone);
            this.grbKhuVuc.Controls.Add(this.comboBoxGroup);
            this.grbKhuVuc.Location = new System.Drawing.Point(3, -2);
            this.grbKhuVuc.Name = "grbKhuVuc";
            this.grbKhuVuc.Size = new System.Drawing.Size(278, 36);
            this.grbKhuVuc.TabIndex = 7;
            this.grbKhuVuc.TabStop = false;
            this.grbKhuVuc.Text = "Khu vực";
            // 
            // chkNone
            // 
            this.chkNone.AutoSize = true;
            this.chkNone.Location = new System.Drawing.Point(223, 16);
            this.chkNone.Name = "chkNone";
            this.chkNone.Size = new System.Drawing.Size(52, 17);
            this.chkNone.TabIndex = 8;
            this.chkNone.Text = "None";
            this.chkNone.UseVisualStyleBackColor = true;
            this.chkNone.CheckedChanged += new System.EventHandler(this.chkNone_CheckedChanged);
            // 
            // buttonChooseImage
            // 
            this.buttonChooseImage.Location = new System.Drawing.Point(827, 6);
            this.buttonChooseImage.Name = "buttonChooseImage";
            this.buttonChooseImage.Size = new System.Drawing.Size(14, 29);
            this.buttonChooseImage.TabIndex = 8;
            this.buttonChooseImage.Text = "Chọn hình bản đồ";
            this.buttonChooseImage.UseVisualStyleBackColor = true;
            this.buttonChooseImage.Visible = false;
            this.buttonChooseImage.Click += new System.EventHandler(this.buttonChooseImage_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Images|*.bmp;*.jpg;*.png;*.gif|BMP|*.bmp|JPG|*.jpg|PNG|*.png";
            this.openFileDialog1.Title = "Chọn tập tin hình ảnh";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbNgonNgu);
            this.splitContainer1.Panel1.Controls.Add(this.buttonChooseImage);
            this.splitContainer1.Panel1.Controls.Add(this.grbKhuVuc);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.grbEditMode);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRightSize);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(935, 611);
            this.splitContainer1.SplitterDistance = 849;
            this.splitContainer1.TabIndex = 9;
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.Controls.Add(this.rbtTiengViet);
            this.grbNgonNgu.Controls.Add(this.rbtEnglish);
            this.grbNgonNgu.Location = new System.Drawing.Point(290, 0);
            this.grbNgonNgu.Name = "grbNgonNgu";
            this.grbNgonNgu.Size = new System.Drawing.Size(174, 34);
            this.grbNgonNgu.TabIndex = 9;
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
            // btnRightSize
            // 
            this.btnRightSize.Location = new System.Drawing.Point(3, 6);
            this.btnRightSize.Name = "btnRightSize";
            this.btnRightSize.Size = new System.Drawing.Size(27, 29);
            this.btnRightSize.TabIndex = 9;
            this.btnRightSize.Text = "<<";
            this.btnRightSize.UseVisualStyleBackColor = true;
            this.btnRightSize.Click += new System.EventHandler(this.btnRightSize_Click);
            // 
            // FormMapEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMapEdit";
            this.Size = new System.Drawing.Size(935, 611);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbEditMode.ResumeLayout(false);
            this.grbEditMode.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.grbKhuVuc.ResumeLayout(false);
            this.grbKhuVuc.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grbEditMode;
        private System.Windows.Forms.RadioButton radReplaceNew;
        private System.Windows.Forms.RadioButton radNo;
        private System.Windows.Forms.RadioButton radEditMove;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.GroupBox grbKhuVuc;
        private System.Windows.Forms.Button buttonChooseImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkNone;
        private System.Windows.Forms.Button btnRightSize;
        private System.Windows.Forms.GroupBox grbNgonNgu;
        private System.Windows.Forms.RadioButton rbtTiengViet;
        private System.Windows.Forms.RadioButton rbtEnglish;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.RadioButton radCopyFrom;
    }
}

