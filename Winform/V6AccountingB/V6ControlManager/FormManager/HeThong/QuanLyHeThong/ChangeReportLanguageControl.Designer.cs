namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    partial class ChangeReportLanguageControl
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.grbNgonNgu = new System.Windows.Forms.GroupBox();
            this.rCurrent = new System.Windows.Forms.RadioButton();
            this.rBothLang = new System.Windows.Forms.RadioButton();
            this.rEnglish = new System.Windows.Forms.RadioButton();
            this.rTiengViet = new System.Windows.Forms.RadioButton();
            this.grbNgonNgu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(291, 97);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.Location = new System.Drawing.Point(186, 97);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // grbNgonNgu
            // 
            this.grbNgonNgu.AccessibleDescription = "REPORTL00010";
            this.grbNgonNgu.Controls.Add(this.rCurrent);
            this.grbNgonNgu.Controls.Add(this.rBothLang);
            this.grbNgonNgu.Controls.Add(this.rEnglish);
            this.grbNgonNgu.Controls.Add(this.rTiengViet);
            this.grbNgonNgu.Location = new System.Drawing.Point(119, 37);
            this.grbNgonNgu.Name = "grbNgonNgu";
            this.grbNgonNgu.Size = new System.Drawing.Size(306, 40);
            this.grbNgonNgu.TabIndex = 11;
            this.grbNgonNgu.TabStop = false;
            this.grbNgonNgu.Text = "Report language";
            // 
            // rCurrent
            // 
            this.rCurrent.AccessibleDescription = "REPORTR00007";
            this.rCurrent.AutoSize = true;
            this.rCurrent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rCurrent.Location = new System.Drawing.Point(226, 17);
            this.rCurrent.Name = "rCurrent";
            this.rCurrent.Size = new System.Drawing.Size(59, 17);
            this.rCurrent.TabIndex = 3;
            this.rCurrent.Text = "Current";
            this.rCurrent.UseVisualStyleBackColor = true;
            // 
            // rBothLang
            // 
            this.rBothLang.AccessibleDescription = "REPORTR00006";
            this.rBothLang.AutoSize = true;
            this.rBothLang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rBothLang.Location = new System.Drawing.Point(144, 17);
            this.rBothLang.Name = "rBothLang";
            this.rBothLang.Size = new System.Drawing.Size(82, 17);
            this.rBothLang.TabIndex = 2;
            this.rBothLang.Text = "Multi choice";
            this.rBothLang.UseVisualStyleBackColor = true;
            // 
            // rEnglish
            // 
            this.rEnglish.AccessibleDescription = "REPORTR00005";
            this.rEnglish.AccessibleName = "English";
            this.rEnglish.AutoSize = true;
            this.rEnglish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rEnglish.Location = new System.Drawing.Point(85, 17);
            this.rEnglish.Name = "rEnglish";
            this.rEnglish.Size = new System.Drawing.Size(59, 17);
            this.rEnglish.TabIndex = 1;
            this.rEnglish.Text = "English";
            this.rEnglish.UseVisualStyleBackColor = true;
            // 
            // rTiengViet
            // 
            this.rTiengViet.AccessibleDescription = "REPORTR00004";
            this.rTiengViet.AutoSize = true;
            this.rTiengViet.Checked = true;
            this.rTiengViet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rTiengViet.Location = new System.Drawing.Point(5, 17);
            this.rTiengViet.Name = "rTiengViet";
            this.rTiengViet.Size = new System.Drawing.Size(80, 17);
            this.rTiengViet.TabIndex = 0;
            this.rTiengViet.TabStop = true;
            this.rTiengViet.Text = "Vietnamese";
            this.rTiengViet.UseVisualStyleBackColor = true;
            // 
            // ChangeReportLanguageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbNgonNgu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "ChangeReportLanguageControl";
            this.Size = new System.Drawing.Size(526, 156);
            this.Load += new System.EventHandler(this.ChangeReportLanguageControl_Load);
            this.grbNgonNgu.ResumeLayout(false);
            this.grbNgonNgu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.GroupBox grbNgonNgu;
        private System.Windows.Forms.RadioButton rCurrent;
        private System.Windows.Forms.RadioButton rBothLang;
        private System.Windows.Forms.RadioButton rEnglish;
        private System.Windows.Forms.RadioButton rTiengViet;
    }
}
