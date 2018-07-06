namespace V6Controls.Controls.Barcode
{
    partial class FormTestQR
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.qRview1 = new V6Controls.Controls.Barcode.QRview();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 349);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 104);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "areV6softwareV6softwareV6softwareV6softwareV6softwareV6software\r\n";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(445, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.qRview1;
            this.propertyGrid1.Size = new System.Drawing.Size(300, 524);
            this.propertyGrid1.TabIndex = 2;
            // 
            // qRview1
            // 
            this.qRview1.AutoSize = true;
            this.qRview1.LinkControl = this.textBox1;
            this.qRview1.Location = new System.Drawing.Point(52, 22);
            this.qRview1.Name = "qRview1";
            this.qRview1.QRvalue = "areV6softwareV6softwareV6softwareV6softwareV6softwareV6software\r\n";
            this.qRview1.Size = new System.Drawing.Size(164, 164);
            this.qRview1.TabIndex = 0;
            // 
            // FormTestQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 548);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.qRview1);
            this.Name = "FormTestQR";
            this.Text = "FormTestQR";
            this.Load += new System.EventHandler(this.FormTestQR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private QRview qRview1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}