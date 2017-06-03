namespace Test
{
    partial class UserControl1
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
            this.components = new System.ComponentModel.Container();
            this.v6ColorTextBox1 = new V6Controls.V6ColorTextBox();
            this.v6HiddenButton1 = new V6Controls.V6HiddenButton();
            this.v6CurrencyTextBox1 = new V6Controls.V6CurrencyTextBox();
            this.v6VvarTextBox1 = new V6Controls.V6VvarTextBox();
            this.SuspendLayout();
            // 
            // v6ColorTextBox1
            // 
            this.v6ColorTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6ColorTextBox1.Location = new System.Drawing.Point(3, 3);
            this.v6ColorTextBox1.Name = "v6ColorTextBox1";
            this.v6ColorTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6ColorTextBox1.TabIndex = 0;
            // 
            // v6HiddenButton1
            // 
            this.v6HiddenButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.v6HiddenButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.v6HiddenButton1.Location = new System.Drawing.Point(118, 1);
            this.v6HiddenButton1.Name = "v6HiddenButton1";
            this.v6HiddenButton1.Size = new System.Drawing.Size(75, 23);
            this.v6HiddenButton1.TabIndex = 1;
            this.v6HiddenButton1.Text = "v6HiddenButton1";
            this.v6HiddenButton1.UseVisualStyleBackColor = true;
            // 
            // v6CurrencyTextBox1
            // 
            this.v6CurrencyTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox1.DecimalMark = ",";
            this.v6CurrencyTextBox1.DecimalPlaces = 2;
            this.v6CurrencyTextBox1.DolaMark = "₫";
            this.v6CurrencyTextBox1.Location = new System.Drawing.Point(3, 29);
            this.v6CurrencyTextBox1.Name = "v6CurrencyTextBox1";
            this.v6CurrencyTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6CurrencyTextBox1.TabIndex = 2;
            this.v6CurrencyTextBox1.ThousandMark = " ";
            // 
            // v6VvarTextBox1
            // 
            this.v6VvarTextBox1.ConString = null;
            this.v6VvarTextBox1.Location = new System.Drawing.Point(3, 55);
            this.v6VvarTextBox1.Name = "v6VvarTextBox1";
            this.v6VvarTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6VvarTextBox1.TabIndex = 3;
            this.v6VvarTextBox1.VVar = null;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6VvarTextBox1);
            this.Controls.Add(this.v6CurrencyTextBox1);
            this.Controls.Add(this.v6HiddenButton1);
            this.Controls.Add(this.v6ColorTextBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(259, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6ColorTextBox v6ColorTextBox1;
        private V6Controls.V6HiddenButton v6HiddenButton1;
        private V6Controls.V6CurrencyTextBox v6CurrencyTextBox1;
        private V6Controls.V6VvarTextBox v6VvarTextBox1;
    }
}
