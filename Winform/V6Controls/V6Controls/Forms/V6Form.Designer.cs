namespace V6Controls.Forms
{
    partial class V6Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V6Form));
            this._waitingImages = new System.Windows.Forms.ImageList(this.components);
            this.toolTipV6FormControl = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // _waitingImages
            // 
            this._waitingImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_waitingImages.ImageStream")));
            this._waitingImages.TransparentColor = System.Drawing.Color.Transparent;
            this._waitingImages.Images.SetKeyName(0, "wb32_1.png");
            this._waitingImages.Images.SetKeyName(1, "wb32_2.png");
            this._waitingImages.Images.SetKeyName(2, "wb32_3.png");
            this._waitingImages.Images.SetKeyName(3, "wb32_4.png");
            this._waitingImages.Images.SetKeyName(4, "wb32_5.png");
            this._waitingImages.Images.SetKeyName(5, "wb32_6.png");
            this._waitingImages.Images.SetKeyName(6, "wb32_7.png");
            this._waitingImages.Images.SetKeyName(7, "wb32_8.png");
            // 
            // V6Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "V6Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "V6Form";
            this.Load += new System.EventHandler(this.V6Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList _waitingImages;
        protected System.Windows.Forms.ToolTip toolTipV6FormControl;

    }
}