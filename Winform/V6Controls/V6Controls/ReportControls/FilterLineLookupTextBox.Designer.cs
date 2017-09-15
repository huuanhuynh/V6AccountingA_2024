namespace V6ReportControls
{
    partial class FilterLineLookupTextBox
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
            this.lookupTextBox1 = new V6Controls.V6LookupTextBox();
            this.SuspendLayout();
            // 
            // v6VvarTextBox1
            // 
            this.lookupTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.lookupTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.lookupTextBox1.CheckOnLeave = false;
            this.lookupTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.lookupTextBox1.F2 = true;
            this.lookupTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lookupTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.lookupTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.lookupTextBox1.LeaveColor = System.Drawing.Color.White;
            this.lookupTextBox1.Location = new System.Drawing.Point(170, 0);
            this.lookupTextBox1.Name = "lookupTextBox1";
            this.lookupTextBox1.Size = new System.Drawing.Size(136, 20);
            this.lookupTextBox1.TabIndex = 3;
            // 
            // FilterLineLookupTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookupTextBox1);
            this.Name = "FilterLineLookupTextBox";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lookupTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6LookupTextBox lookupTextBox1;
    }
}
