using V6Controls;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    partial class ChangeCodeFormBase
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOldCode = new V6Controls.V6VvarTextBox();
            this.txtName = new V6Controls.V6ColorTextBox();
            this.txtNewCode = new V6Controls.V6ColorTextBox();
            this.lblNewCode = new System.Windows.Forms.Label();
            this.lblCurrentCode = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleName = "groupBox1";
            this.groupBox1.Controls.Add(this.txtOldCode);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtNewCode);
            this.groupBox1.Controls.Add(this.lblNewCode);
            this.groupBox1.Controls.Add(this.lblCurrentCode);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(710, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtOldCode
            // 
            this.txtOldCode.AccessibleName = "";
            this.txtOldCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtOldCode.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtOldCode.Enabled = false;
            this.txtOldCode.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtOldCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOldCode.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtOldCode.HoverColor = System.Drawing.Color.Yellow;
            this.txtOldCode.LeaveColor = System.Drawing.Color.White;
            this.txtOldCode.Location = new System.Drawing.Point(155, 20);
            this.txtOldCode.Name = "txtOldCode";
            this.txtOldCode.Size = new System.Drawing.Size(144, 20);
            this.txtOldCode.TabIndex = 1;
            this.txtOldCode.VVar = "ma_kh";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtName.Enabled = false;
            this.txtName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtName.HoverColor = System.Drawing.Color.Yellow;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(306, 20);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(381, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtNewCode
            // 
            this.txtNewCode.AccessibleName = "";
            this.txtNewCode.BackColor = System.Drawing.Color.White;
            this.txtNewCode.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNewCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewCode.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNewCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewCode.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNewCode.HoverColor = System.Drawing.Color.Yellow;
            this.txtNewCode.LeaveColor = System.Drawing.Color.White;
            this.txtNewCode.Location = new System.Drawing.Point(155, 48);
            this.txtNewCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new System.Drawing.Size(144, 20);
            this.txtNewCode.TabIndex = 4;
            // 
            // lblNewCode
            // 
            this.lblNewCode.AccessibleDescription = "CHANGECODEL00002";
            this.lblNewCode.AutoSize = true;
            this.lblNewCode.Location = new System.Drawing.Point(32, 52);
            this.lblNewCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewCode.Name = "lblNewCode";
            this.lblNewCode.Size = new System.Drawing.Size(41, 13);
            this.lblNewCode.TabIndex = 3;
            this.lblNewCode.Text = "Mã mới";
            // 
            // lblCurrentCode
            // 
            this.lblCurrentCode.AccessibleDescription = "CHANGECODEL0000";
            this.lblCurrentCode.AutoSize = true;
            this.lblCurrentCode.Location = new System.Drawing.Point(32, 20);
            this.lblCurrentCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentCode.Name = "lblCurrentCode";
            this.lblCurrentCode.Size = new System.Drawing.Size(59, 13);
            this.lblCurrentCode.TabIndex = 0;
            this.lblCurrentCode.Text = "Mã hiện tại";
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "CHANGECODEB00002";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 89);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "CHANGECODEB00001";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 89);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 4;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // ChangeCodeFormBase
            // 
            this.AccessibleDescription = "CHANGECODEL00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 143);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeCodeFormBase";
            this.Text = "Đổi mã";
            this.Load += new System.EventHandler(this.KhachHangChangeCodeForm_Load);
            
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6VvarTextBox txtOldCode;
        private V6ColorTextBox txtName;
        private V6ColorTextBox txtNewCode;
        private System.Windows.Forms.Label lblNewCode;
        private System.Windows.Forms.Label lblCurrentCode;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
    }
}