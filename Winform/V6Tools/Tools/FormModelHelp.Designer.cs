namespace Tools
{
    partial class FormModelHelp
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReplace1 = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnOpenReplace1 = new System.Windows.Forms.Button();
            this.btnOpenReplace2 = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.txtFields = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnUpdate2 = new System.Windows.Forms.Button();
            this.btnReload1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(372, 158);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(87, 12);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(360, 20);
            this.txtFile.TabIndex = 1;
            this.txtFile.Text = "File.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ReplaceDic1";
            // 
            // txtReplace1
            // 
            this.txtReplace1.Location = new System.Drawing.Point(87, 38);
            this.txtReplace1.Name = "txtReplace1";
            this.txtReplace1.ReadOnly = true;
            this.txtReplace1.Size = new System.Drawing.Size(360, 20);
            this.txtReplace1.TabIndex = 1;
            this.txtReplace1.Text = "ReplaceDicForModel.txt";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(463, 10);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnOpenReplace1
            // 
            this.btnOpenReplace1.Location = new System.Drawing.Point(463, 36);
            this.btnOpenReplace1.Name = "btnOpenReplace1";
            this.btnOpenReplace1.Size = new System.Drawing.Size(75, 23);
            this.btnOpenReplace1.TabIndex = 2;
            this.btnOpenReplace1.Text = "Open";
            this.btnOpenReplace1.UseVisualStyleBackColor = true;
            this.btnOpenReplace1.Click += new System.EventHandler(this.btnOpenReplace1_Click);
            // 
            // btnOpenReplace2
            // 
            this.btnOpenReplace2.Location = new System.Drawing.Point(463, 62);
            this.btnOpenReplace2.Name = "btnOpenReplace2";
            this.btnOpenReplace2.Size = new System.Drawing.Size(75, 23);
            this.btnOpenReplace2.TabIndex = 2;
            this.btnOpenReplace2.Text = "Open";
            this.btnOpenReplace2.UseVisualStyleBackColor = true;
            this.btnOpenReplace2.Click += new System.EventHandler(this.btnOpenReplace2_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Enabled = false;
            this.btnSaveFile.Location = new System.Drawing.Point(544, 10);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 2;
            this.btnSaveFile.Text = "Save";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(659, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "New text {Line} new text {LineReplace1} abc={LineReplace2} upper=\"{LineToUpper}\" " +
    "upper1=\"{LineToUpper1}\" lower=\"{LineToLower}\"";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(87, 133);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(120, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Replace1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Replace2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Replace2 to";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(294, 107);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(120, 20);
            this.textBox3.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(284, 133);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(120, 20);
            this.textBox4.TabIndex = 1;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(180, 226);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(517, 440);
            this.txtResult.TabIndex = 4;
            this.txtResult.Text = "";
            // 
            // txtFields
            // 
            this.txtFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFields.Location = new System.Drawing.Point(12, 226);
            this.txtFields.Name = "txtFields";
            this.txtFields.Size = new System.Drawing.Size(162, 440);
            this.txtFields.TabIndex = 4;
            this.txtFields.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(16, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Lines (fields)";
            // 
            // btnUpdate2
            // 
            this.btnUpdate2.Location = new System.Drawing.Point(463, 158);
            this.btnUpdate2.Name = "btnUpdate2";
            this.btnUpdate2.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate2.TabIndex = 2;
            this.btnUpdate2.Text = "Update 2";
            this.btnUpdate2.UseVisualStyleBackColor = true;
            this.btnUpdate2.Click += new System.EventHandler(this.btnUpdate2_Click);
            // 
            // btnReload1
            // 
            this.btnReload1.Location = new System.Drawing.Point(544, 36);
            this.btnReload1.Name = "btnReload1";
            this.btnReload1.Size = new System.Drawing.Size(75, 23);
            this.btnReload1.TabIndex = 2;
            this.btnReload1.Text = "Open";
            this.btnReload1.UseVisualStyleBackColor = true;
            this.btnReload1.Click += new System.EventHandler(this.btnOpenReplace1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(544, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Update 3";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnUpdate3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormModelHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 678);
            this.Controls.Add(this.txtFields);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOpenReplace2);
            this.Controls.Add(this.btnReload1);
            this.Controls.Add(this.btnOpenReplace1);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUpdate2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtReplace1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtFile);
            this.Name = "FormModelHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Model Heo";
            this.Load += new System.EventHandler(this.FormHuuanEditText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReplace1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnOpenReplace1;
        private System.Windows.Forms.Button btnOpenReplace2;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.RichTextBox txtFields;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnUpdate2;
        private System.Windows.Forms.Button btnReload1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}