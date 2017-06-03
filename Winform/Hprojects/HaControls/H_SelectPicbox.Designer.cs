namespace H_Controls
{
    partial class H_SelectPicbox
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
            this.lineTop = new System.Windows.Forms.Label();
            this.lineBottom = new System.Windows.Forms.Label();
            this.lineLeft = new System.Windows.Forms.Label();
            this.lineRight = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.transpCtrl1 = new TranspCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lineTop
            // 
            this.lineTop.BackColor = System.Drawing.Color.White;
            this.lineTop.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.lineTop.Location = new System.Drawing.Point(50, 50);
            this.lineTop.Name = "lineTop";
            this.lineTop.Size = new System.Drawing.Size(50, 2);
            this.lineTop.TabIndex = 1;
            this.lineTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.line_MouseDown);
            this.lineTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.line_MouseUp);
            // 
            // lineBottom
            // 
            this.lineBottom.BackColor = System.Drawing.Color.White;
            this.lineBottom.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.lineBottom.Location = new System.Drawing.Point(50, 98);
            this.lineBottom.Name = "lineBottom";
            this.lineBottom.Size = new System.Drawing.Size(50, 2);
            this.lineBottom.TabIndex = 1;
            this.lineBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.line_MouseDown);
            this.lineBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.line_MouseUp);
            // 
            // lineLeft
            // 
            this.lineLeft.BackColor = System.Drawing.Color.White;
            this.lineLeft.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lineLeft.Location = new System.Drawing.Point(50, 50);
            this.lineLeft.Name = "lineLeft";
            this.lineLeft.Size = new System.Drawing.Size(2, 50);
            this.lineLeft.TabIndex = 1;
            this.lineLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.line_MouseDown);
            this.lineLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.line_MouseUp);
            // 
            // lineRight
            // 
            this.lineRight.BackColor = System.Drawing.Color.White;
            this.lineRight.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lineRight.Location = new System.Drawing.Point(98, 50);
            this.lineRight.Name = "lineRight";
            this.lineRight.Size = new System.Drawing.Size(2, 50);
            this.lineRight.TabIndex = 1;
            this.lineRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.line_MouseDown);
            this.lineRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.line_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(364, 352);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // transpCtrl1
            // 
            this.transpCtrl1.BackColor = System.Drawing.Color.Transparent;
            this.transpCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transpCtrl1.Location = new System.Drawing.Point(0, 0);
            this.transpCtrl1.Name = "transpCtrl1";
            this.transpCtrl1.Opacity = 50;
            this.transpCtrl1.Size = new System.Drawing.Size(364, 352);
            this.transpCtrl1.TabIndex = 3;
            this.transpCtrl1.Text = "transpCtrl1";
            this.transpCtrl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transparentBox1_MouseDown);
            this.transpCtrl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.transparentBox1_MouseUp);
            // 
            // H_SelectPicbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lineBottom);
            this.Controls.Add(this.lineRight);
            this.Controls.Add(this.lineLeft);
            this.Controls.Add(this.lineTop);
            this.Controls.Add(this.transpCtrl1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "H_SelectPicbox";
            this.Size = new System.Drawing.Size(364, 352);
            this.Load += new System.EventHandler(this.H_SelectPicbox_Load);
            this.SizeChanged += new System.EventHandler(this.H_SelectPicbox_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.Label lineTop;
        private System.Windows.Forms.Label lineBottom;
        private System.Windows.Forms.Label lineLeft;
        private System.Windows.Forms.Label lineRight;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private TranspCtrl transpCtrl1;
    }
}
