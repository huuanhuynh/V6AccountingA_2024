namespace V6ControlManager.FormManager.KhoHangManager
{
    partial class DayHangControl
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
            this.v6VeticalLable1 = new V6Controls.V6VeticalLable();
            this.SuspendLayout();
            // 
            // v6VeticalLable1
            // 
            this.v6VeticalLable1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v6VeticalLable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.v6VeticalLable1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.v6VeticalLable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6VeticalLable1.HideText = "Dãy A";
            this.v6VeticalLable1.Location = new System.Drawing.Point(0, 0);
            this.v6VeticalLable1.Name = "v6VeticalLable1";
            this.v6VeticalLable1.RenderingMode = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.v6VeticalLable1.ShowText = "Dãy A";
            this.v6VeticalLable1.Size = new System.Drawing.Size(29, 116);
            this.v6VeticalLable1.TabIndex = 0;
            this.v6VeticalLable1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.v6VeticalLable1.TextDrawMode = V6Controls.DrawMode.TopBottom;
            this.v6VeticalLable1.TransparentBackground = false;
            this.v6VeticalLable1.Click += new System.EventHandler(this.v6VeticalLable1_Click);
            // 
            // DayHangControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.v6VeticalLable1);
            this.Name = "DayHangControl";
            this.Size = new System.Drawing.Size(150, 121);
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.V6VeticalLable v6VeticalLable1;
    }
}
