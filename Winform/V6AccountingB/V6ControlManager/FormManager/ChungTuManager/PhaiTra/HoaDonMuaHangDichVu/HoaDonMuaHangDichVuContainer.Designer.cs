namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.HoaDonMuaHangDichVu
{
    partial class HoaDonMuaHangDichVuContainer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFull = new System.Windows.Forms.ToolStripButton();
            this.tsClose = new System.Windows.Forms.ToolStripButton();
            this.tsMessage = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 578);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.toolStripSeparator1,
            this.tsFull,
            this.tsClose,
            this.tsMessage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(860, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = global::V6ControlManager.Properties.Resources.Add24;
            this.tsNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(28, 28);
            this.tsNew.Text = "Thêm";
            this.tsNew.Click += new System.EventHandler(this.btnThemHoaDon_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsFull
            // 
            this.tsFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsFull.Image = global::V6ControlManager.Properties.Resources.ZoomIn24;
            this.tsFull.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFull.Name = "tsFull";
            this.tsFull.Size = new System.Drawing.Size(28, 28);
            this.tsFull.Text = "Phóng to";
            this.tsFull.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // tsClose
            // 
            this.tsClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsClose.Image = global::V6ControlManager.Properties.Resources.CloseXbox24;
            this.tsClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsClose.Name = "tsClose";
            this.tsClose.Size = new System.Drawing.Size(28, 28);
            this.tsClose.Text = "Đóng";
            this.tsClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tsMessage
            // 
            this.tsMessage.AutoSize = false;
            this.tsMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsMessage.ForeColor = System.Drawing.Color.Red;
            this.tsMessage.Name = "tsMessage";
            this.tsMessage.Size = new System.Drawing.Size(386, 28);
            this.tsMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HoaDonMuaHangDichVuContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "HoaDonMuaHangDichVuContainer";
            this.Size = new System.Drawing.Size(860, 609);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsClose;
        private System.Windows.Forms.ToolStripButton tsFull;
        private System.Windows.Forms.ToolStripLabel tsMessage;
    }
}
