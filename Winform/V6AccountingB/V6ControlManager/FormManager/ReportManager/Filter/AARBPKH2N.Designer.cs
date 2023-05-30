namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AARBPKH2N
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTuNam = new V6Controls.V6NumberTextBox();
            this.txtDenNam = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.txtTaiKhoan = new V6Controls.V6VvarTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00044";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00043";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ năm";
            // 
            // txtTuNam
            // 
            this.txtTuNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtTuNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTuNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTuNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTuNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTuNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtTuNam.LeaveColor = System.Drawing.Color.White;
            this.txtTuNam.Location = new System.Drawing.Point(117, 3);
            this.txtTuNam.MaxLength = 4;
            this.txtTuNam.MaxNumLength = 4;
            this.txtTuNam.Name = "txtTuNam";
            this.txtTuNam.Size = new System.Drawing.Size(100, 20);
            this.txtTuNam.TabIndex = 1;
            this.txtTuNam.Text = "0";
            this.txtTuNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTuNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTuNam.LostFocus += new System.EventHandler(this.txtTuNam_LostFocus);
            this.txtTuNam.TextChanged += new System.EventHandler(this.txtTuNam_TextChanged);
            // 
            // txtDenNam
            // 
            this.txtDenNam.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtDenNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDenNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDenNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDenNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDenNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtDenNam.LeaveColor = System.Drawing.Color.White;
            this.txtDenNam.Location = new System.Drawing.Point(117, 29);
            this.txtDenNam.MaxLength = 4;
            this.txtDenNam.MaxNumLength = 4;
            this.txtDenNam.Name = "txtDenNam";
            this.txtDenNam.ReadOnly = true;
            this.txtDenNam.Size = new System.Drawing.Size(100, 20);
            this.txtDenNam.TabIndex = 3;
            this.txtDenNam.TabStop = false;
            this.txtDenNam.Text = "0";
            this.txtDenNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDenNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00027";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 58);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(55, 13);
            this.v6Label9.TabIndex = 6;
            this.v6Label9.Text = "Tài khoản";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.AccessibleName = "TK";
            this.txtTaiKhoan.BackColor = System.Drawing.SystemColors.Window;
            this.txtTaiKhoan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTaiKhoan.CheckNotEmpty = true;
            this.txtTaiKhoan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTaiKhoan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTaiKhoan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTaiKhoan.HoverColor = System.Drawing.Color.Yellow;
            this.txtTaiKhoan.LeaveColor = System.Drawing.Color.White;
            this.txtTaiKhoan.Location = new System.Drawing.Point(117, 55);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(100, 20);
            this.txtTaiKhoan.TabIndex = 7;
            this.txtTaiKhoan.VVar = "TK";
            // 
            // AARBPKH2N
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.txtDenNam);
            this.Controls.Add(this.txtTuNam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AARBPKH2N";
            this.Size = new System.Drawing.Size(243, 92);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6NumberTextBox txtTuNam;
        private V6Controls.V6NumberTextBox txtDenNam;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox txtTaiKhoan;
    }
}
