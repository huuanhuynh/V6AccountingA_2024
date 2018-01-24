namespace V6Controls.Forms
{
    partial class EmailSettingForm
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
            this.grbKetNoi = new System.Windows.Forms.GroupBox();
            this.txtConnectPort = new System.Windows.Forms.TextBox();
            this.grbKetNoi.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbKetNoi
            // 
            this.grbKetNoi.Controls.Add(this.txtConnectPort);
            this.grbKetNoi.Location = new System.Drawing.Point(5, 5);
            this.grbKetNoi.Name = "grbKetNoi";
            this.grbKetNoi.Size = new System.Drawing.Size(200, 105);
            this.grbKetNoi.TabIndex = 5;
            this.grbKetNoi.TabStop = false;
            this.grbKetNoi.Text = "Kết nối";
            // 
            // txtConnectPort
            // 
            this.txtConnectPort.Location = new System.Drawing.Point(6, 75);
            this.txtConnectPort.Name = "txtConnectPort";
            this.txtConnectPort.ReadOnly = true;
            this.txtConnectPort.Size = new System.Drawing.Size(188, 20);
            this.txtConnectPort.TabIndex = 3;
            this.txtConnectPort.Text = "Chưa kết nối";
            // 
            // EmailSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 117);
            this.Controls.Add(this.grbKetNoi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmailSettingForm";
            this.Opacity = 0.9D;
            this.Text = "Tìm kiếm";
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.grbKetNoi, 0);
            this.grbKetNoi.ResumeLayout(false);
            this.grbKetNoi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbKetNoi;
        private System.Windows.Forms.TextBox txtConnectPort;

    }
}