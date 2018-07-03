namespace HuuanTetris
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblĐiểm_dòng = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCấp_độ = new System.Windows.Forms.Label();
            this.lbtNhạc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblKỷ_lục = new System.Windows.Forms.Label();
            this.lbtNewGame = new System.Windows.Forms.Label();
            this.lbtPause = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(113, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 445);
            this.panel1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 33;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(314, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 100);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Line:";
            // 
            // lblĐiểm_dòng
            // 
            this.lblĐiểm_dòng.AutoSize = true;
            this.lblĐiểm_dòng.Location = new System.Drawing.Point(368, 153);
            this.lblĐiểm_dòng.Name = "lblĐiểm_dòng";
            this.lblĐiểm_dòng.Size = new System.Drawing.Size(13, 13);
            this.lblĐiểm_dòng.TabIndex = 2;
            this.lblĐiểm_dòng.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lever:";
            // 
            // lblCấp_độ
            // 
            this.lblCấp_độ.AutoSize = true;
            this.lblCấp_độ.Location = new System.Drawing.Point(368, 166);
            this.lblCấp_độ.Name = "lblCấp_độ";
            this.lblCấp_độ.Size = new System.Drawing.Size(13, 13);
            this.lblCấp_độ.TabIndex = 2;
            this.lblCấp_độ.Text = "0";
            // 
            // lbtNhạc
            // 
            this.lbtNhạc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbtNhạc.Location = new System.Drawing.Point(12, 48);
            this.lbtNhạc.Name = "lbtNhạc";
            this.lbtNhạc.Size = new System.Drawing.Size(95, 23);
            this.lbtNhạc.TabIndex = 3;
            this.lbtNhạc.Text = "Nhạc";
            this.lbtNhạc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbtNhạc.Click += new System.EventHandler(this.btnNhạc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "record";
            // 
            // lblKỷ_lục
            // 
            this.lblKỷ_lục.AutoSize = true;
            this.lblKỷ_lục.Location = new System.Drawing.Point(325, 209);
            this.lblKỷ_lục.Name = "lblKỷ_lục";
            this.lblKỷ_lục.Size = new System.Drawing.Size(52, 13);
            this.lblKỷ_lục.TabIndex = 4;
            this.lblKỷ_lục.Text = "no record";
            // 
            // lbtNewGame
            // 
            this.lbtNewGame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbtNewGame.Location = new System.Drawing.Point(12, 87);
            this.lbtNewGame.Name = "lbtNewGame";
            this.lbtNewGame.Size = new System.Drawing.Size(95, 23);
            this.lbtNewGame.TabIndex = 3;
            this.lbtNewGame.Text = "New Game";
            this.lbtNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbtNewGame.Click += new System.EventHandler(this.label4_Click);
            // 
            // lbtPause
            // 
            this.lbtPause.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbtPause.Location = new System.Drawing.Point(12, 126);
            this.lbtPause.Name = "lbtPause";
            this.lbtPause.Size = new System.Drawing.Size(95, 23);
            this.lbtPause.TabIndex = 3;
            this.lbtPause.Text = "Start/Pause";
            this.lbtPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbtPause.Click += new System.EventHandler(this.lbtPause_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblKỷ_lục);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbtNewGame);
            this.Controls.Add(this.lbtPause);
            this.Controls.Add(this.lbtNhạc);
            this.Controls.Add(this.lblĐiểm_dòng);
            this.Controls.Add(this.lblCấp_độ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Size = new System.Drawing.Size(443, 465);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblĐiểm_dòng;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCấp_độ;
        private System.Windows.Forms.Label lbtNhạc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblKỷ_lục;
        private System.Windows.Forms.Label lbtNewGame;
        private System.Windows.Forms.Label lbtPause;
    }
}

