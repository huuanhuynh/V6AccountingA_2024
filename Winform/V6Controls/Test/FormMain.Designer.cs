namespace Test
{
    partial class FormMain
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
            this.button2 = new System.Windows.Forms.Button();
            this.btnTestVvar = new System.Windows.Forms.Button();
            this.btntestv6f = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test V6CurrentcyTextBox";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Test V6TabControl";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnTestVvar
            // 
            this.btnTestVvar.Location = new System.Drawing.Point(12, 70);
            this.btnTestVvar.Name = "btnTestVvar";
            this.btnTestVvar.Size = new System.Drawing.Size(178, 23);
            this.btnTestVvar.TabIndex = 2;
            this.btnTestVvar.Text = "Test V6VvarTextBox";
            this.btnTestVvar.UseVisualStyleBackColor = true;
            this.btnTestVvar.Click += new System.EventHandler(this.btnTestVvar_Click);
            // 
            // btntestv6f
            // 
            this.btntestv6f.Location = new System.Drawing.Point(436, 12);
            this.btntestv6f.Name = "btntestv6f";
            this.btntestv6f.Size = new System.Drawing.Size(178, 23);
            this.btntestv6f.TabIndex = 3;
            this.btntestv6f.Text = "Test V6Form";
            this.btntestv6f.UseVisualStyleBackColor = true;
            this.btntestv6f.Click += new System.EventHandler(this.btntestv6f_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 266);
            this.Controls.Add(this.btntestv6f);
            this.Controls.Add(this.btnTestVvar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnTestVvar;
        private System.Windows.Forms.Button btntestv6f;
    }
}