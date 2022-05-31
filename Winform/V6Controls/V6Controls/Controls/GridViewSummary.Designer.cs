namespace V6Controls.Controls
{
    partial class GridViewSummary
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
            this.copyMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // copyMenuStrip1
            // 
            this.copyMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyValue,
            this.menuCopy});
            this.copyMenuStrip1.Name = "contextMenuStrip1";
            this.copyMenuStrip1.Size = new System.Drawing.Size(134, 48);
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(133, 22);
            this.menuCopy.Text = "Copy text";
            // 
            // menuCopyValue
            // 
            this.menuCopyValue.Name = "menuCopyValue";
            this.menuCopyValue.Size = new System.Drawing.Size(133, 22);
            this.menuCopyValue.Text = "Copy value";
            // 
            // GridViewSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "GridViewSummary";
            this.Size = new System.Drawing.Size(200, 22);
            this.copyMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip copyMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuCopyValue;
    }
}
