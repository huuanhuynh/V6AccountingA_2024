namespace V6Controls.Forms
{
    partial class SmsModemSettingForm
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
            this.btnTimModem = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtConnectPort = new System.Windows.Forms.TextBox();
            this.btnNgatKetNoi = new System.Windows.Forms.Button();
            this.grbKetNoi.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbKetNoi
            // 
            this.grbKetNoi.Controls.Add(this.btnTimModem);
            this.grbKetNoi.Controls.Add(this.comboBox1);
            this.grbKetNoi.Controls.Add(this.btnKetNoi);
            this.grbKetNoi.Controls.Add(this.txtConnectPort);
            this.grbKetNoi.Controls.Add(this.btnNgatKetNoi);
            this.grbKetNoi.Location = new System.Drawing.Point(5, 5);
            this.grbKetNoi.Name = "grbKetNoi";
            this.grbKetNoi.Size = new System.Drawing.Size(200, 105);
            this.grbKetNoi.TabIndex = 5;
            this.grbKetNoi.TabStop = false;
            this.grbKetNoi.Text = "Kết nối";
            // 
            // btnTimModem
            // 
            this.btnTimModem.Location = new System.Drawing.Point(152, 18);
            this.btnTimModem.Name = "btnTimModem";
            this.btnTimModem.Size = new System.Drawing.Size(42, 23);
            this.btnTimModem.TabIndex = 5;
            this.btnTimModem.Text = "Tìm";
            this.btnTimModem.UseVisualStyleBackColor = true;
            this.btnTimModem.Click += new System.EventHandler(this.btnTimModem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(9, 46);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 2;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
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
            // btnNgatKetNoi
            // 
            this.btnNgatKetNoi.Location = new System.Drawing.Point(119, 47);
            this.btnNgatKetNoi.Name = "btnNgatKetNoi";
            this.btnNgatKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnNgatKetNoi.TabIndex = 4;
            this.btnNgatKetNoi.Text = "Ngắt kết nối";
            this.btnNgatKetNoi.UseVisualStyleBackColor = true;
            this.btnNgatKetNoi.Click += new System.EventHandler(this.btnNgatKetNoi_Click);
            // 
            // SmsModemSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 117);
            this.Controls.Add(this.grbKetNoi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmsModemSettingForm";
            this.Opacity = 0.9D;
            this.Text = "SmsModemSetting";
            this.Controls.SetChildIndex(this.grbKetNoi, 0);
            this.grbKetNoi.ResumeLayout(false);
            this.grbKetNoi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbKetNoi;
        private System.Windows.Forms.Button btnTimModem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.TextBox txtConnectPort;
        private System.Windows.Forms.Button btnNgatKetNoi;

    }
}