namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia
{
    partial class BaoGiaF10
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
            this.cboKieuPost = new V6Controls.V6ComboBox();
            this.v6Label20 = new V6Controls.V6Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboKieuPost
            // 
            this.cboKieuPost.AccessibleName = "kieu_post";
            this.cboKieuPost.BackColor = System.Drawing.SystemColors.Window;
            this.cboKieuPost.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.cboKieuPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieuPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKieuPost.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboKieuPost.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.cboKieuPost.FormattingEnabled = true;
            this.cboKieuPost.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboKieuPost.Location = new System.Drawing.Point(95, 29);
            this.cboKieuPost.Name = "cboKieuPost";
            this.cboKieuPost.Size = new System.Drawing.Size(219, 24);
            this.cboKieuPost.TabIndex = 1;
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "ASOCTSOAL00045";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(18, 32);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(55, 13);
            this.v6Label20.TabIndex = 0;
            this.v6Label20.Text = "Trạng thái";
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 94);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 94);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 2;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // BaoGiaF10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(335, 146);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.cboKieuPost);
            this.Controls.Add(this.v6Label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaoGiaF10";
            this.Text = "BaoGiaF10";
            this.Controls.SetChildIndex(this.v6Label20, 0);
            this.Controls.SetChildIndex(this.cboKieuPost, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label v6Label20;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        public V6Controls.V6ComboBox cboKieuPost;
    }
}