namespace H_Controls.Controls.Lookup.Filter
{
    partial class FilterLineNumberTextBox
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
            this.NumberTextBox1 = new NumberTextBox();
            this.SuspendLayout();
            // 
            // NumberTextBox1
            // 
            this.NumberTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberTextBox1.EnableColorEffect = true;
            this.NumberTextBox1.EnableColorEffectOnMouseEnter = false;
            this.NumberTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.NumberTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.NumberTextBox1.LeaveColor = System.Drawing.Color.White;
            this.NumberTextBox1.Location = new System.Drawing.Point(180, 0);
            this.NumberTextBox1.MaxNumDecimal = 0;
            this.NumberTextBox1.MaxNumLength = 0;
            this.NumberTextBox1.Name = "NumberTextBox1";
            this.NumberTextBox1.Size = new System.Drawing.Size(136, 20);
            
            this.NumberTextBox1.TabIndex = 3;
            this.NumberTextBox1.Text = "0";
            this.NumberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumberTextBox1.GrayTitle = "";
            this.NumberTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FilterLineNumberTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NumberTextBox1);
            this.Name = "FilterLineNumberTextBox";
            this.Controls.SetChildIndex(this.NumberTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumberTextBox NumberTextBox1;
    }
}
