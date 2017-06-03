namespace H_Controls.Controls.Lookup.Filter
{
    partial class FilterLineDateTimeNullable
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
            this.DateTimeColorTextBox1 = new DateTimeColor();
            this.SuspendLayout();
            // 
            // DateTimeColorTextBox1
            // 
            this.DateTimeColorTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateTimeColorTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.DateTimeColorTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.DateTimeColorTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DateTimeColorTextBox1.LeaveColor = System.Drawing.Color.White;
            this.DateTimeColorTextBox1.Location = new System.Drawing.Point(180, 0);
            this.DateTimeColorTextBox1.Name = "DateTimeColorTextBox1";
            this.DateTimeColorTextBox1.Size = new System.Drawing.Size(136, 20);
            this.DateTimeColorTextBox1.TabIndex = 3;
            this.DateTimeColorTextBox1.GrayTitle = "";
            // 
            // FilterLineDateTimeNullable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DateTimeColorTextBox1);
            this.Name = "FilterLineDateTimeNullable";
            this.Controls.SetChildIndex(this.DateTimeColorTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimeColor DateTimeColorTextBox1;
    }
}
