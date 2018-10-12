namespace V6ReportControls
{
    partial class FilterLineVvarTextBox
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
            this.v6VvarTextBox1 = new V6Controls.V6VvarTextBox();
            this.SuspendLayout();
            // 
            // v6VvarTextBox1
            // 
            this.v6VvarTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6VvarTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.v6VvarTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6VvarTextBox1.CheckOnLeave = false;
            this.v6VvarTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6VvarTextBox1.F2 = true;
            this.v6VvarTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6VvarTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6VvarTextBox1.Location = new System.Drawing.Point(170, 0);
            this.v6VvarTextBox1.Name = "v6VvarTextBox1";
            this.v6VvarTextBox1.ShowName = true;
            this.v6VvarTextBox1.Size = new System.Drawing.Size(136, 20);
            this.v6VvarTextBox1.TabIndex = 3;
            // 
            // FilterLineVvarTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6VvarTextBox1);
            this.Name = "FilterLineVvarTextBox";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.v6VvarTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6VvarTextBox v6VvarTextBox1;
    }
}
