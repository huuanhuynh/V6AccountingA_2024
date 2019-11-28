namespace V6ControlManager.FormManager.KhoHangManager.Report
{
    partial class VitriMultiForm
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
            this.txtMavt = new V6Controls.V6VvarTextBox();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMavt
            // 
            this.txtMavt.AccessibleName = "MA_VT";
            this.txtMavt.BackColor = System.Drawing.SystemColors.Window;
            this.txtMavt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMavt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMavt.BrotherFields = "TEN_VT";
            this.txtMavt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMavt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMavt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMavt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMavt.LeaveColor = System.Drawing.Color.White;
            this.txtMavt.Location = new System.Drawing.Point(125, 3);
            this.txtMavt.Name = "txtMavt";
            this.txtMavt.Size = new System.Drawing.Size(100, 20);
            this.txtMavt.TabIndex = 6;
            this.txtMavt.TabStop = false;
            this.txtMavt.VVar = "MA_VT";
            this.txtMavt.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMavt_V6LostFocus);
            // 
            // lblTenHang
            // 
            this.lblTenHang.AccessibleDescription = ".";
            this.lblTenHang.AccessibleName = "TEN_VT";
            this.lblTenHang.AutoSize = true;
            this.lblTenHang.Location = new System.Drawing.Point(231, 6);
            this.lblTenHang.Name = "lblTenHang";
            this.lblTenHang.Size = new System.Drawing.Size(10, 13);
            this.lblTenHang.TabIndex = 5;
            this.lblTenHang.Text = ".";
            // 
            // VitriMultiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 562);
            this.Controls.Add(this.txtMavt);
            this.Controls.Add(this.lblTenHang);
            this.Name = "VitriMultiForm";
            this.Text = "VitriMultiForm";
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.lblTenHang, 0);
            this.Controls.SetChildIndex(this.txtMavt, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public V6Controls.V6VvarTextBox txtMavt;
        private System.Windows.Forms.Label lblTenHang;

    }
}