namespace V6ReportControls
{
    partial class FilterLineDSNS
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
            this.btnChonDSNS = new V6Controls.Controls.V6FormButton();
            this.txtDSNS = new V6Controls.V6VvarTextBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(7, 3);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 3);
            // 
            // btnChonDSNS
            // 
            this.btnChonDSNS.Location = new System.Drawing.Point(0, 0);
            this.btnChonDSNS.Name = "btnChonDSNS";
            this.btnChonDSNS.Size = new System.Drawing.Size(298, 23);
            this.btnChonDSNS.TabIndex = 36;
            this.btnChonDSNS.Text = "Thêm";
            this.btnChonDSNS.UseVisualStyleBackColor = true;
            this.btnChonDSNS.Click += new System.EventHandler(this.btnChonDSNS_Click);
            // 
            // txtDSNS
            // 
            this.txtDSNS.BackColor = System.Drawing.SystemColors.Window;
            this.txtDSNS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDSNS.CheckNotEmpty = true;
            this.txtDSNS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDSNS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDSNS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDSNS.HoverColor = System.Drawing.Color.Yellow;
            this.txtDSNS.LeaveColor = System.Drawing.Color.White;
            this.txtDSNS.Location = new System.Drawing.Point(0, 23);
            this.txtDSNS.Name = "txtDSNS";
            this.txtDSNS.Size = new System.Drawing.Size(298, 20);
            this.txtDSNS.TabIndex = 33;
            // 
            // FilterLineDSNS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnChonDSNS);
            this.Controls.Add(this.txtDSNS);
            this.Name = "FilterLineDSNS";
            this.Size = new System.Drawing.Size(308, 44);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.txtDSNS, 0);
            this.Controls.SetChildIndex(this.btnChonDSNS, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.Controls.V6FormButton btnChonDSNS;
        private V6Controls.V6VvarTextBox txtDSNS;

    }
}
