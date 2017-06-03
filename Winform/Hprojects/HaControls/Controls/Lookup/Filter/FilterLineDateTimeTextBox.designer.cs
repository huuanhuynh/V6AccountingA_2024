namespace H_Controls.Controls.Lookup.Filter
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
            this._dateTimeTextBox1 = new DateTimePick();
            this.SuspendLayout();
            // 
            // _dateTimeTextBox1
            // 
            this._dateTimeTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dateTimeTextBox1.CustomFormat = "dd/MM/yyyy";
            this._dateTimeTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this._dateTimeTextBox1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this._dateTimeTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this._dateTimeTextBox1.LeaveColor = System.Drawing.Color.White;
            this._dateTimeTextBox1.Location = new System.Drawing.Point(180, 0);
            this._dateTimeTextBox1.Name = "_dateTimeTextBox1";
            this._dateTimeTextBox1.Size = new System.Drawing.Size(136, 20);
            this._dateTimeTextBox1.TabIndex = 3;
            this._dateTimeTextBox1.TextTitle = "";
            // 
            // FilterLineDateTimeTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dateTimeTextBox1);
            this.Name = "FilterLineDateTimeTextBox";
            this.Controls.SetChildIndex(this._dateTimeTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimePick _dateTimeTextBox1;
    }
}
