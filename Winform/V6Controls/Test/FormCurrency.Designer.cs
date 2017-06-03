namespace Test
{
    partial class FormCurrency
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.v6CurrencyTextBox1 = new V6Controls.V6CurrencyTextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // v6CurrencyTextBox1
            // 
            this.v6CurrencyTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.v6CurrencyTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox1.Location = new System.Drawing.Point(12, 12);
            this.v6CurrencyTextBox1.Name = "v6CurrencyTextBox1";
            this.v6CurrencyTextBox1.Size = new System.Drawing.Size(461, 20);
            this.v6CurrencyTextBox1.TabIndex = 0;
            this.v6CurrencyTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6CurrencyTextBox1.TextChanged += new System.EventHandler(this.v6CurrencyTextBox1_TextChanged);
            this.v6CurrencyTextBox1.Validated += new System.EventHandler(this.v6CurrencyTextBox1_Validated);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(162, 38);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.v6CurrencyTextBox1;
            this.propertyGrid1.Size = new System.Drawing.Size(311, 693);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.Validated += new System.EventHandler(this.propertyGrid1_Validated);
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // FormCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 743);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.v6CurrencyTextBox1);
            this.Name = "FormCurrency";
            this.Text = "FormCurrency";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6CurrencyTextBox v6CurrencyTextBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}