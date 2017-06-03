namespace V6Controls.Forms
{
    partial class ConfirmPassword
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new V6Controls.V6ColorTextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(197, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '#';
            this.txtPassword.Size = new System.Drawing.Size(147, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Enabled = false;
            this.txtUserName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtUserName.HoverColor = System.Drawing.Color.Yellow;
            this.txtUserName.LeaveColor = System.Drawing.Color.White;
            this.txtUserName.Location = new System.Drawing.Point(197, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(147, 20);
            this.txtUserName.TabIndex = 11;
            this.txtUserName.Text = "V6";
            // 
            // lblPassword
            // 
            this.lblPassword.AccessibleDescription = "V6REASKL00003";
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Location = new System.Drawing.Point(110, 45);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.AccessibleDescription = "V6REASKL00002";
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Location = new System.Drawing.Point(110, 19);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(58, 13);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "User name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::V6Controls.Properties.Resources.lock_icon;
            this.pictureBox1.Location = new System.Drawing.Point(8, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // ConfirmPassword
            // 
            this.AccessibleDescription = "V6REASKL00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 81);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmPassword";
            this.Text = "ConfirmPassword";
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.lblPassword, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtPassword;
        private V6Controls.V6ColorTextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}