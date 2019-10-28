namespace Bai_Tap_Thep__Povision
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.xửLýHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDownloadHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xửLýHóaĐơnToolStripMenuItem,
            this.loadDownloadHóaĐơnToolStripMenuItem,
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(642, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // xửLýHóaĐơnToolStripMenuItem
            // 
            this.xửLýHóaĐơnToolStripMenuItem.Name = "xửLýHóaĐơnToolStripMenuItem";
            this.xửLýHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(157, 20);
            this.xửLýHóaĐơnToolStripMenuItem.Text = "Phát Hành & Xử lý Hóa Đơn";
            this.xửLýHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.xửLýHóaĐơnToolStripMenuItem_Click);
            // 
            // loadDownloadHóaĐơnToolStripMenuItem
            // 
            this.loadDownloadHóaĐơnToolStripMenuItem.Name = "loadDownloadHóaĐơnToolStripMenuItem";
            this.loadDownloadHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(155, 20);
            this.loadDownloadHóaĐơnToolStripMenuItem.Text = "Load & Download Hóa Đơn";
            this.loadDownloadHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.loadDownloadHóaĐơnToolStripMenuItem_Click);
            // 
            // convertDSKháchHàngSangXMLZIPToolStripMenuItem
            // 
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem.Name = "convertDSKháchHàngSangXMLZIPToolStripMenuItem";
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem.Size = new System.Drawing.Size(224, 20);
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem.Text = "Convert DS Khách Hàng Sang XML_ZIP";
            this.convertDSKháchHàngSangXMLZIPToolStripMenuItem.Click += new System.EventHandler(this.convertDSKháchHàngSangXMLZIPToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "_";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 413);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xửLýHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDownloadHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertDSKháchHàngSangXMLZIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}

