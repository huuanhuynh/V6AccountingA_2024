namespace V6ReportControls
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
            this.v6DateTimeTextBox1 = new V6Controls.V6DateTimeColor();
            this.dateSelectButton1 = new V6Controls.Controls.DateSelectButton();
            this.SuspendLayout();
            // 
            // v6DateTimeTextBox1
            // 
            this.v6DateTimeTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6DateTimeTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.v6DateTimeTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6DateTimeTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6DateTimeTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6DateTimeTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6DateTimeTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6DateTimeTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6DateTimeTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6DateTimeTextBox1.Location = new System.Drawing.Point(170, 0);
            this.v6DateTimeTextBox1.Name = "v6DateTimeTextBox1";
            this.v6DateTimeTextBox1.Size = new System.Drawing.Size(113, 20);
            this.v6DateTimeTextBox1.StringValue = "__/__/____";
            this.v6DateTimeTextBox1.TabIndex = 3;
            this.v6DateTimeTextBox1.Text = "__/__/____";
            // 
            // dateSelectButton1
            // 
            this.dateSelectButton1.Image = global::V6Controls.Properties.Resources.Calendar3124;
            this.dateSelectButton1.Location = new System.Drawing.Point(283, 0);
            this.dateSelectButton1.Name = "dateSelectButton1";
            this.dateSelectButton1.ReferenceControl = this.v6DateTimeTextBox1;
            this.dateSelectButton1.Size = new System.Drawing.Size(21, 21);
            this.dateSelectButton1.TabIndex = 4;
            // 
            // FilterLineDateTimeNullable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateSelectButton1);
            this.Controls.Add(this.v6DateTimeTextBox1);
            this.Name = "FilterLineDateTimeNullable";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.v6DateTimeTextBox1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.dateSelectButton1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6DateTimeColor v6DateTimeTextBox1;
        private V6Controls.Controls.DateSelectButton dateSelectButton1;
    }
}
