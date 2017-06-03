namespace Tools
{
    partial class FormZip
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
            this.btnZipfile = new System.Windows.Forms.Button();
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.txtFolder1 = new System.Windows.Forms.TextBox();
            this.btnZipFolder = new System.Windows.Forms.Button();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.txtFolder2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnZipfile
            // 
            this.btnZipfile.Location = new System.Drawing.Point(12, 38);
            this.btnZipfile.Name = "btnZipfile";
            this.btnZipfile.Size = new System.Drawing.Size(75, 23);
            this.btnZipfile.TabIndex = 0;
            this.btnZipfile.Text = "ZipFile";
            this.btnZipfile.UseVisualStyleBackColor = true;
            this.btnZipfile.Click += new System.EventHandler(this.buttonZipFile_Click);
            // 
            // txtFile1
            // 
            this.txtFile1.Location = new System.Drawing.Point(12, 12);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.Size = new System.Drawing.Size(454, 20);
            this.txtFile1.TabIndex = 1;
            // 
            // txtFolder1
            // 
            this.txtFolder1.Location = new System.Drawing.Point(12, 121);
            this.txtFolder1.Name = "txtFolder1";
            this.txtFolder1.Size = new System.Drawing.Size(454, 20);
            this.txtFolder1.TabIndex = 1;
            // 
            // btnZipFolder
            // 
            this.btnZipFolder.Location = new System.Drawing.Point(12, 147);
            this.btnZipFolder.Name = "btnZipFolder";
            this.btnZipFolder.Size = new System.Drawing.Size(75, 23);
            this.btnZipFolder.TabIndex = 2;
            this.btnZipFolder.Text = "Zip folder";
            this.btnZipFolder.UseVisualStyleBackColor = true;
            this.btnZipFolder.Click += new System.EventHandler(this.buttonZipFolder_Click);
            // 
            // txtFile2
            // 
            this.txtFile2.Location = new System.Drawing.Point(93, 41);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.Size = new System.Drawing.Size(373, 20);
            this.txtFile2.TabIndex = 1;
            // 
            // txtFolder2
            // 
            this.txtFolder2.Location = new System.Drawing.Point(93, 150);
            this.txtFolder2.Name = "txtFolder2";
            this.txtFolder2.Size = new System.Drawing.Size(373, 20);
            this.txtFolder2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Unicode to Vni";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 204);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(373, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(75, 283);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(373, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Hoa đầu từ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 443);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnZipFolder);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtFolder2);
            this.Controls.Add(this.txtFolder1);
            this.Controls.Add(this.txtFile2);
            this.Controls.Add(this.txtFile1);
            this.Controls.Add(this.btnZipfile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZipfile;
        private System.Windows.Forms.TextBox txtFile1;
        private System.Windows.Forms.TextBox txtFolder1;
        private System.Windows.Forms.Button btnZipFolder;
        private System.Windows.Forms.TextBox txtFile2;
        private System.Windows.Forms.TextBox txtFolder2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
    }
}

