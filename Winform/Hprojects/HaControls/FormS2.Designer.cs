namespace H_Controls
{
    partial class FormS2
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
            this.bLeft = new System.Windows.Forms.Label();
            this.bRight = new System.Windows.Forms.Label();
            this.bTop = new System.Windows.Forms.Label();
            this.bBottom = new System.Windows.Forms.Label();
            this.bBottomRight = new System.Windows.Forms.Label();
            this.timerResize = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bLeft
            // 
            this.bLeft.BackColor = System.Drawing.Color.MidnightBlue;
            this.bLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.bLeft.Location = new System.Drawing.Point(0, 0);
            this.bLeft.Name = "bLeft";
            this.bLeft.Size = new System.Drawing.Size(5, 300);
            this.bLeft.TabIndex = 0;
            this.bLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bRight
            // 
            this.bRight.BackColor = System.Drawing.Color.MidnightBlue;
            this.bRight.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.bRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.bRight.Location = new System.Drawing.Point(295, 0);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(5, 300);
            this.bRight.TabIndex = 0;
            this.bRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bRight_MouseDown);
            this.bRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bRight_MouseUp);
            // 
            // bTop
            // 
            this.bTop.BackColor = System.Drawing.Color.MidnightBlue;
            this.bTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.bTop.Location = new System.Drawing.Point(5, 0);
            this.bTop.Name = "bTop";
            this.bTop.Size = new System.Drawing.Size(290, 35);
            this.bTop.TabIndex = 0;
            this.bTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bBottom
            // 
            this.bBottom.BackColor = System.Drawing.Color.MidnightBlue;
            this.bBottom.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.bBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bBottom.Location = new System.Drawing.Point(5, 295);
            this.bBottom.Name = "bBottom";
            this.bBottom.Size = new System.Drawing.Size(290, 5);
            this.bBottom.TabIndex = 0;
            this.bBottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bBottom_MouseDown);
            this.bBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bBottom_MouseUp);
            // 
            // bBottomRight
            // 
            this.bBottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bBottomRight.BackColor = System.Drawing.Color.CornflowerBlue;
            this.bBottomRight.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.bBottomRight.Location = new System.Drawing.Point(295, 295);
            this.bBottomRight.Name = "bBottomRight";
            this.bBottomRight.Size = new System.Drawing.Size(5, 5);
            this.bBottomRight.TabIndex = 0;
            this.bBottomRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bBottomRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bBottomRight_MouseDown);
            this.bBottomRight.MouseLeave += new System.EventHandler(this.bBottomRight_MouseLeave);
            this.bBottomRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bBottomRight_MouseUp);
            // 
            // timerResize
            // 
            this.timerResize.Tick += new System.EventHandler(this.timerResize_Tick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(265, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(229, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "O";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.RoyalBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(193, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "_";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(5, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 25);
            this.label4.TabIndex = 1;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bTop);
            this.Controls.Add(this.bBottomRight);
            this.Controls.Add(this.bBottom);
            this.Controls.Add(this.bRight);
            this.Controls.Add(this.bLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormS";
            this.Text = "FormS";
            this.Resize += new System.EventHandler(this.FormS_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label bLeft;
        private System.Windows.Forms.Label bRight;
        private System.Windows.Forms.Label bTop;
        private System.Windows.Forms.Label bBottom;
        private System.Windows.Forms.Label bBottomRight;
        private System.Windows.Forms.Timer timerResize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}