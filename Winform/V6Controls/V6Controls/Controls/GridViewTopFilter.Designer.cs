namespace V6Controls.Controls
{
    partial class GridViewTopFilter
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
            this.lblHelp = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblReset = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHelp
            // 
            this.lblHelp.Image = global::V6Controls.Properties.Resources.questionwhite;
            this.lblHelp.Location = new System.Drawing.Point(1, 1);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(21, 21);
            this.lblHelp.TabIndex = 0;
            this.lblHelp.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblReset
            // 
            this.lblReset.Image = global::V6Controls.Properties.Resources.Refresh16;
            this.lblReset.Location = new System.Drawing.Point(22, 1);
            this.lblReset.Name = "lblReset";
            this.lblReset.Size = new System.Drawing.Size(21, 21);
            this.lblReset.TabIndex = 1;
            this.toolTip1.SetToolTip(this.lblReset, "Reset");
            this.lblReset.Click += new System.EventHandler(this.lblReset_Click);
            // 
            // GridViewTopFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblReset);
            this.Controls.Add(this.lblHelp);
            this.Name = "GridViewTopFilter";
            this.Size = new System.Drawing.Size(200, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblReset;
    }
}
