namespace H_Controls.Controls.Lookup.Filter
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
            this.LookupTextBox1 = new LookupTextBox();
            this.SuspendLayout();
            // 
            // LookupTextBox1
            // 
            this.LookupTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LookupTextBox1.BrotherFields = null;
            this.LookupTextBox1.CheckOnLeave = false;
            this.LookupTextBox1.EnableColorEffect = true;
            this.LookupTextBox1.EnableColorEffectOnMouseEnter = false;
            this.LookupTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.LookupTextBox1.F2 = true;
            this.LookupTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.LookupTextBox1.LeaveColor = System.Drawing.Color.White;
            this.LookupTextBox1.LimitCharacters = null;
            this.LookupTextBox1.Location = new System.Drawing.Point(180, 0);
            this.LookupTextBox1.Name = "LookupTextBox1";
            this.LookupTextBox1.Size = new System.Drawing.Size(136, 20);
            this.LookupTextBox1.TabIndex = 3;
            this.LookupTextBox1.GrayTitle = "";
            // 
            // FilterLineVvarTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LookupTextBox1);
            this.Name = "FilterLineVvarTextBox";
            this.Controls.SetChildIndex(this.LookupTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LookupTextBox LookupTextBox1;
    }
}
