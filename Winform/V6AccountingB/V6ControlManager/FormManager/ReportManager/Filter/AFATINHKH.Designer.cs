namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AFATINHKH
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
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKy = new V6Controls.V6NumberTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 36);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00120";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kỳ";
            // 
            // txtKy
            // 
            this.txtKy.BackColor = System.Drawing.SystemColors.Window;
            this.txtKy.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKy.DecimalPlaces = 0;
            this.txtKy.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKy.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKy.HoverColor = System.Drawing.Color.Yellow;
            this.txtKy.LeaveColor = System.Drawing.Color.White;
            this.txtKy.Location = new System.Drawing.Point(117, 10);
            this.txtKy.MaxLength = 2;
            this.txtKy.MaxNumLength = 2;
            this.txtKy.Name = "txtKy";
            this.txtKy.Size = new System.Drawing.Size(100, 20);
            this.txtKy.TabIndex = 1;
            this.txtKy.Text = "0";
            this.txtKy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKy.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKy.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            // 
            // txtNam
            // 
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(117, 33);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 5;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // AFATINHKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtKy);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Name = "AFATINHKH";
            this.Size = new System.Drawing.Size(235, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6NumberTextBox txtKy;
        private V6Controls.V6NumberTextBox txtNam;
    }
}
