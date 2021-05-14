namespace V6ControlManager.FormManager.ToolManager
{
    partial class FormChuyenMa
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.grbNguon = new System.Windows.Forms.GroupBox();
            this.radAuto = new System.Windows.Forms.RadioButton();
            this.radUNI = new System.Windows.Forms.RadioButton();
            this.radVNI = new System.Windows.Forms.RadioButton();
            this.radTCVN3 = new System.Windows.Forms.RadioButton();
            this.grbDich = new System.Windows.Forms.GroupBox();
            this.rad2UNI = new System.Windows.Forms.RadioButton();
            this.rad2VNI = new System.Windows.Forms.RadioButton();
            this.rad2TCVN3 = new System.Windows.Forms.RadioButton();
            this.grbNguon.SuspendLayout();
            this.grbDich.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(598, 213);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            this.richTextBox1.Leave += new System.EventHandler(this.richTextBox1_Leave);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(1, 248);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(598, 194);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            // 
            // btnChuyen
            // 
            this.btnChuyen.Location = new System.Drawing.Point(12, 219);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(118, 23);
            this.btnChuyen.TabIndex = 10;
            this.btnChuyen.Text = "Chuyển";
            this.btnChuyen.UseVisualStyleBackColor = true;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // grbNguon
            // 
            this.grbNguon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbNguon.Controls.Add(this.radAuto);
            this.grbNguon.Controls.Add(this.radUNI);
            this.grbNguon.Controls.Add(this.radVNI);
            this.grbNguon.Controls.Add(this.radTCVN3);
            this.grbNguon.Location = new System.Drawing.Point(136, 214);
            this.grbNguon.Name = "grbNguon";
            this.grbNguon.Size = new System.Drawing.Size(234, 30);
            this.grbNguon.TabIndex = 11;
            this.grbNguon.TabStop = false;
            this.grbNguon.Text = "Mã nguồn";
            // 
            // radAuto
            // 
            this.radAuto.AutoSize = true;
            this.radAuto.Location = new System.Drawing.Point(171, 11);
            this.radAuto.Name = "radAuto";
            this.radAuto.Size = new System.Drawing.Size(55, 17);
            this.radAuto.TabIndex = 0;
            this.radAuto.Text = "AUTO";
            this.radAuto.UseVisualStyleBackColor = true;
            this.radAuto.CheckedChanged += new System.EventHandler(this.radAuto_CheckedChanged);
            // 
            // radUNI
            // 
            this.radUNI.AutoSize = true;
            this.radUNI.Location = new System.Drawing.Point(121, 11);
            this.radUNI.Name = "radUNI";
            this.radUNI.Size = new System.Drawing.Size(44, 17);
            this.radUNI.TabIndex = 0;
            this.radUNI.Text = "UNI";
            this.radUNI.UseVisualStyleBackColor = true;
            this.radUNI.CheckedChanged += new System.EventHandler(this.radAuto_CheckedChanged);
            // 
            // radVNI
            // 
            this.radVNI.AutoSize = true;
            this.radVNI.Location = new System.Drawing.Point(72, 11);
            this.radVNI.Name = "radVNI";
            this.radVNI.Size = new System.Drawing.Size(43, 17);
            this.radVNI.TabIndex = 0;
            this.radVNI.Text = "VNI";
            this.radVNI.UseVisualStyleBackColor = true;
            this.radVNI.CheckedChanged += new System.EventHandler(this.radAuto_CheckedChanged);
            // 
            // radTCVN3
            // 
            this.radTCVN3.AutoSize = true;
            this.radTCVN3.Checked = true;
            this.radTCVN3.Location = new System.Drawing.Point(6, 11);
            this.radTCVN3.Name = "radTCVN3";
            this.radTCVN3.Size = new System.Drawing.Size(60, 17);
            this.radTCVN3.TabIndex = 0;
            this.radTCVN3.TabStop = true;
            this.radTCVN3.Text = "TCVN3";
            this.radTCVN3.UseVisualStyleBackColor = true;
            this.radTCVN3.CheckedChanged += new System.EventHandler(this.radAuto_CheckedChanged);
            // 
            // grbDich
            // 
            this.grbDich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDich.Controls.Add(this.rad2UNI);
            this.grbDich.Controls.Add(this.rad2VNI);
            this.grbDich.Controls.Add(this.rad2TCVN3);
            this.grbDich.Location = new System.Drawing.Point(376, 214);
            this.grbDich.Name = "grbDich";
            this.grbDich.Size = new System.Drawing.Size(183, 30);
            this.grbDich.TabIndex = 11;
            this.grbDich.TabStop = false;
            this.grbDich.Text = "Mã đích";
            // 
            // rad2UNI
            // 
            this.rad2UNI.AutoSize = true;
            this.rad2UNI.Checked = true;
            this.rad2UNI.Location = new System.Drawing.Point(121, 11);
            this.rad2UNI.Name = "rad2UNI";
            this.rad2UNI.Size = new System.Drawing.Size(44, 17);
            this.rad2UNI.TabIndex = 0;
            this.rad2UNI.TabStop = true;
            this.rad2UNI.Text = "UNI";
            this.rad2UNI.UseVisualStyleBackColor = true;
            // 
            // rad2VNI
            // 
            this.rad2VNI.AutoSize = true;
            this.rad2VNI.Location = new System.Drawing.Point(72, 11);
            this.rad2VNI.Name = "rad2VNI";
            this.rad2VNI.Size = new System.Drawing.Size(43, 17);
            this.rad2VNI.TabIndex = 0;
            this.rad2VNI.Text = "VNI";
            this.rad2VNI.UseVisualStyleBackColor = true;
            // 
            // rad2TCVN3
            // 
            this.rad2TCVN3.AutoSize = true;
            this.rad2TCVN3.Location = new System.Drawing.Point(6, 11);
            this.rad2TCVN3.Name = "rad2TCVN3";
            this.rad2TCVN3.Size = new System.Drawing.Size(60, 17);
            this.rad2TCVN3.TabIndex = 0;
            this.rad2TCVN3.Text = "TCVN3";
            this.rad2TCVN3.UseVisualStyleBackColor = true;
            // 
            // FormChuyenMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 443);
            this.Controls.Add(this.grbDich);
            this.Controls.Add(this.grbNguon);
            this.Controls.Add(this.btnChuyen);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "FormChuyenMa";
            this.Text = "Chuyển mã tiếng Việt";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.richTextBox1, 0);
            
            this.Controls.SetChildIndex(this.richTextBox2, 0);
            this.Controls.SetChildIndex(this.btnChuyen, 0);
            this.Controls.SetChildIndex(this.grbNguon, 0);
            this.Controls.SetChildIndex(this.grbDich, 0);
            this.grbNguon.ResumeLayout(false);
            this.grbNguon.PerformLayout();
            this.grbDich.ResumeLayout(false);
            this.grbDich.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button btnChuyen;
        private System.Windows.Forms.GroupBox grbNguon;
        private System.Windows.Forms.RadioButton radAuto;
        private System.Windows.Forms.RadioButton radUNI;
        private System.Windows.Forms.RadioButton radVNI;
        private System.Windows.Forms.RadioButton radTCVN3;
        private System.Windows.Forms.GroupBox grbDich;
        private System.Windows.Forms.RadioButton rad2UNI;
        private System.Windows.Forms.RadioButton rad2VNI;
        private System.Windows.Forms.RadioButton rad2TCVN3;
    }
}

