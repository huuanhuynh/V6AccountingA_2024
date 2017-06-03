namespace V6Controls
{
    partial class SortListView
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
            this.contextMenuStripListView1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripListView1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripListView1
            // 
            this.contextMenuStripListView1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStripListView1.Name = "contextMenuStripListView1";
            this.contextMenuStripListView1.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // SortListView
            // 
            this.ContextMenuStrip = this.contextMenuStripListView1;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.this_ColumnClick);
            this.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.SortListView_DrawColumnHeader);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.this_MouseClick);
            this.contextMenuStripListView1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListView1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}
