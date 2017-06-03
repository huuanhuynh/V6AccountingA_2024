namespace Test
{
    partial class FormTestVvar
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
            this.button1 = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.v6VvarTextBox1 = new V6Controls.V6VvarTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(512, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.v6VvarTextBox1;
            this.propertyGrid1.Size = new System.Drawing.Size(254, 643);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.Validated += new System.EventHandler(this.propertyGrid1_Validated);
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // v6VvarTextBox1
            // 
            this.v6VvarTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.v6VvarTextBox1.ConString = "Data Source=V6Soft;Initial Catalog=V6KD11VP;User ID=sa;Password=v6soft";
            this.v6VvarTextBox1.LoadAutoCompleteSrc = true;
            this.v6VvarTextBox1.Location = new System.Drawing.Point(12, 12);
            this.v6VvarTextBox1.Name = "v6VvarTextBox1";
            this.v6VvarTextBox1.Size = new System.Drawing.Size(494, 20);
            this.v6VvarTextBox1.TabIndex = 0;
            this.v6VvarTextBox1.VVar = "Ma_kh_u";
            this.v6VvarTextBox1.TextChanged += new System.EventHandler(this.v6VvarTextBox1_TextChanged);
            this.v6VvarTextBox1.Validated += new System.EventHandler(this.v6VvarTextBox1_Validated);
            // 
            // FormTestVvar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 667);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.v6VvarTextBox1);
            this.Name = "FormTestVvar";
            this.Text = "FormTestVvar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6VvarTextBox v6VvarTextBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
    }
}