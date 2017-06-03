namespace V6ReportControls
{
    partial class FilterLineDateTimeTextBox
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
            this.v6DateTimeTextBox1 = new V6Controls.V6DateTimePick();
            this.SuspendLayout();
            // 
            // v6DateTimeTextBox1
            // 
            this.v6DateTimeTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6DateTimeTextBox1.CustomFormat = "dd/MM/yyyy";
            this.v6DateTimeTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6DateTimeTextBox1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6DateTimeTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6DateTimeTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6DateTimeTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6DateTimeTextBox1.Location = new System.Drawing.Point(170, 0);
            this.v6DateTimeTextBox1.Name = "v6DateTimeTextBox1";
            this.v6DateTimeTextBox1.Size = new System.Drawing.Size(136, 20);
            this.v6DateTimeTextBox1.TabIndex = 3;
            this.v6DateTimeTextBox1.TextTitle = "";
            // 
            // FilterLineDateTimeTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6DateTimeTextBox1);
            this.Name = "FilterLineDateTimeTextBox";
            this.Controls.SetChildIndex(this.v6DateTimeTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6DateTimePick v6DateTimeTextBox1;
    }
}
