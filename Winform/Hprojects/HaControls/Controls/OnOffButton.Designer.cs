namespace H_Controls.Controls
{
    partial class OnOffButton
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
            this.@switch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // @switch
            // 
            this.@switch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@switch.Location = new System.Drawing.Point(3, 3);
            this.@switch.Name = "@switch";
            this.@switch.Size = new System.Drawing.Size(24, 24);
            this.@switch.TabIndex = 0;
            this.@switch.Text = "off";
            this.@switch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.@switch.Click += new System.EventHandler(this.switch_Click);
            // 
            // OnOffButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@switch);
            this.Name = "OnOffButton";
            this.Size = new System.Drawing.Size(60, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label @switch;
    }
}
