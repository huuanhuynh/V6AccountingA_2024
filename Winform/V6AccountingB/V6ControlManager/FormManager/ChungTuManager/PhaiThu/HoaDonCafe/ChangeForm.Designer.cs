namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    partial class ChangeForm
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboKhu2 = new V6Controls.V6ComboBox();
            this.cboBan2 = new V6Controls.V6ComboBox();
            this.txtKhu1 = new V6Controls.V6ColorTextBox();
            this.txtBan1 = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(100, 172);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.Location = new System.Drawing.Point(12, 172);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = ".";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = ".";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "To";
            // 
            // cboKhu2
            // 
            this.cboKhu2.BackColor = System.Drawing.SystemColors.Window;
            this.cboKhu2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhu2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboKhu2.FormattingEnabled = true;
            this.cboKhu2.Location = new System.Drawing.Point(15, 100);
            this.cboKhu2.Name = "cboKhu2";
            this.cboKhu2.Size = new System.Drawing.Size(501, 21);
            this.cboKhu2.TabIndex = 1;
            this.cboKhu2.SelectedIndexChanged += new System.EventHandler(this.cboKhu2_SelectedIndexChanged);
            // 
            // cboBan2
            // 
            this.cboBan2.BackColor = System.Drawing.SystemColors.Window;
            this.cboBan2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBan2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboBan2.FormattingEnabled = true;
            this.cboBan2.Location = new System.Drawing.Point(15, 127);
            this.cboBan2.Name = "cboBan2";
            this.cboBan2.Size = new System.Drawing.Size(501, 21);
            this.cboBan2.TabIndex = 0;
            this.cboBan2.SelectedIndexChanged += new System.EventHandler(this.cboBan2_SelectedIndexChanged);
            // 
            // txtKhu1
            // 
            this.txtKhu1.BackColor = System.Drawing.SystemColors.Window;
            this.txtKhu1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKhu1.Enabled = false;
            this.txtKhu1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKhu1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKhu1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKhu1.HoverColor = System.Drawing.Color.Yellow;
            this.txtKhu1.LeaveColor = System.Drawing.Color.White;
            this.txtKhu1.Location = new System.Drawing.Point(15, 26);
            this.txtKhu1.Name = "txtKhu1";
            this.txtKhu1.Size = new System.Drawing.Size(501, 20);
            this.txtKhu1.TabIndex = 11;
            this.txtKhu1.TabStop = false;
            // 
            // txtBan1
            // 
            this.txtBan1.BackColor = System.Drawing.SystemColors.Window;
            this.txtBan1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtBan1.Enabled = false;
            this.txtBan1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtBan1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBan1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtBan1.HoverColor = System.Drawing.Color.Yellow;
            this.txtBan1.LeaveColor = System.Drawing.Color.White;
            this.txtBan1.Location = new System.Drawing.Point(15, 52);
            this.txtBan1.Name = "txtBan1";
            this.txtBan1.Size = new System.Drawing.Size(501, 20);
            this.txtBan1.TabIndex = 11;
            this.txtBan1.TabStop = false;
            // 
            // ChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(528, 224);
            this.Controls.Add(this.txtBan1);
            this.Controls.Add(this.txtKhu1);
            this.Controls.Add(this.cboBan2);
            this.Controls.Add(this.cboKhu2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeForm";
            this.Text = "ChangeForm";
            this.Load += new System.EventHandler(this.ChangeForm_Load);
            
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cboKhu2, 0);
            this.Controls.SetChildIndex(this.cboBan2, 0);
            this.Controls.SetChildIndex(this.txtKhu1, 0);
            this.Controls.SetChildIndex(this.txtBan1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6ComboBox cboKhu2;
        private V6Controls.V6ComboBox cboBan2;
        private V6Controls.V6ColorTextBox txtKhu1;
        private V6Controls.V6ColorTextBox txtBan1;
    }
}