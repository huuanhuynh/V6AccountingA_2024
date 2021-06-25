namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AAPPR_SOA_F10
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
            this.txtSoCtXuat = new V6Controls.V6ColorTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 106);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 4;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 106);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtSoCtXuat
            // 
            this.txtSoCtXuat.AccessibleName = "so_ctx";
            this.txtSoCtXuat.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoCtXuat.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCtXuat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoCtXuat.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCtXuat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCtXuat.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCtXuat.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCtXuat.LeaveColor = System.Drawing.Color.White;
            this.txtSoCtXuat.Location = new System.Drawing.Point(88, 16);
            this.txtSoCtXuat.Name = "txtSoCtXuat";
            this.txtSoCtXuat.Size = new System.Drawing.Size(100, 20);
            this.txtSoCtXuat.TabIndex = 0;
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "XULYL00050";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(3, 18);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(72, 13);
            this.v6Label7.TabIndex = 15;
            this.v6Label7.Text = "Số ct bắt đầu";
            // 
            // AAPPR_SOA_F10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.txtSoCtXuat);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AAPPR_SOA_F10";
            this.Size = new System.Drawing.Size(279, 149);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6Label v6Label7;
        public V6Controls.V6ColorTextBox txtSoCtXuat;




    }
}