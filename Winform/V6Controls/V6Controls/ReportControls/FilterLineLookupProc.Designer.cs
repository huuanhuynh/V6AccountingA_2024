namespace V6ReportControls
{
    partial class FilterLineLookupProc
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
            this.lookupProc1 = new V6Controls.V6LookupProc();
            this.SuspendLayout();
            // 
            // v6VvarTextBox1
            // 
            this.lookupProc1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupProc1.BackColor = System.Drawing.SystemColors.Window;
            this.lookupProc1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.lookupProc1.CheckOnLeave = false;
            this.lookupProc1.EnterColor = System.Drawing.Color.PaleGreen;
            this.lookupProc1.F2 = true;
            this.lookupProc1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lookupProc1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.lookupProc1.HoverColor = System.Drawing.Color.Yellow;
            this.lookupProc1.LeaveColor = System.Drawing.Color.White;
            this.lookupProc1.Location = new System.Drawing.Point(170, 0);
            this.lookupProc1.Name = "lookupProc1";
            this.lookupProc1.Size = new System.Drawing.Size(136, 20);
            this.lookupProc1.TabIndex = 3;
            // 
            // FilterLineLookupProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookupProc1);
            this.Name = "FilterLineLookupProc";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lookupProc1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6LookupProc lookupProc1;
    }
}
