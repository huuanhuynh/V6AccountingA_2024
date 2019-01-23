namespace V6ControlManager.FormManager.HeThong
{
    partial class ThongTinChuongTrinh
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblContent2 = new System.Windows.Forms.Label();
            this.lblContent1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.linkLabelV6Soft = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHuy = new V6Controls.V6Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 60;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblContent2);
            this.panel1.Controls.Add(this.lblContent1);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(3, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 72);
            this.panel1.TabIndex = 0;
            // 
            // lblContent2
            // 
            this.lblContent2.AccessibleDescription = ".";
            this.lblContent2.AutoSize = true;
            this.lblContent2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContent2.ForeColor = System.Drawing.Color.Blue;
            this.lblContent2.Location = new System.Drawing.Point(64, 40);
            this.lblContent2.Name = "lblContent2";
            this.lblContent2.Size = new System.Drawing.Size(62, 16);
            this.lblContent2.TabIndex = 0;
            this.lblContent2.Text = "Nội dung";
            this.lblContent2.Visible = false;
            // 
            // lblContent1
            // 
            this.lblContent1.AccessibleDescription = ".";
            this.lblContent1.AutoSize = true;
            this.lblContent1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContent1.ForeColor = System.Drawing.Color.Blue;
            this.lblContent1.Location = new System.Drawing.Point(64, 24);
            this.lblContent1.Name = "lblContent1";
            this.lblContent1.Size = new System.Drawing.Size(62, 16);
            this.lblContent1.TabIndex = 0;
            this.lblContent1.Text = "Nội dung";
            this.lblContent1.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = ".";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(34, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(61, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tiêu đề";
            this.lblTitle.Visible = false;
            // 
            // panelLogo
            // 
            this.panelLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLogo.Controls.Add(this.linkLabelV6Soft);
            this.panelLogo.Controls.Add(this.label2);
            this.panelLogo.Location = new System.Drawing.Point(3, 3);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(594, 154);
            this.panelLogo.TabIndex = 0;
            // 
            // linkLabelV6Soft
            // 
            this.linkLabelV6Soft.AccessibleDescription = ".";
            this.linkLabelV6Soft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelV6Soft.Location = new System.Drawing.Point(3, 125);
            this.linkLabelV6Soft.Name = "linkLabelV6Soft";
            this.linkLabelV6Soft.Size = new System.Drawing.Size(165, 25);
            this.linkLabelV6Soft.TabIndex = 1;
            this.linkLabelV6Soft.TabStop = true;
            this.linkLabelV6Soft.Text = "www.v6soft.com.vn";
            this.linkLabelV6Soft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelV6Soft.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = ".";
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Image = global::V6ControlManager.Properties.Resources.V6Logo_5x4;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 125);
            this.label2.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "V6REASKB00002";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuy.Location = new System.Drawing.Point(3, 554);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(76, 40);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Controls.Add(this.panel1);
            this.panelContent.Location = new System.Drawing.Point(3, 163);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(594, 388);
            this.panelContent.TabIndex = 0;
            // 
            // ThongTinChuongTrinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelLogo);
            this.Controls.Add(this.btnHuy);
            this.Name = "ThongTinChuongTrinh";
            this.Size = new System.Drawing.Size(600, 600);
            this.Load += new System.EventHandler(this.ThongTinChuongTrinh_Load);
            this.VisibleChanged += new System.EventHandler(this.ThongTinChuongTrinh_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelLogo.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private V6Controls.V6Label btnHuy;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.LinkLabel linkLabelV6Soft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblContent1;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblContent2;
    }
}
