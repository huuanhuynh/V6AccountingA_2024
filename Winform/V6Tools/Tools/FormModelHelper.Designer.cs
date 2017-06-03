namespace Tools
{
    partial class FormModelHelper
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
            this.txtFields = new System.Windows.Forms.RichTextBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReplace2 = new System.Windows.Forms.TextBox();
            this.btnOpenReplace2 = new System.Windows.Forms.Button();
            this.btnOpenReplace1 = new System.Windows.Forms.Button();
            this.txtReplace1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFields
            // 
            this.txtFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFields.Location = new System.Drawing.Point(12, 303);
            this.txtFields.Name = "txtFields";
            this.txtFields.Size = new System.Drawing.Size(162, 440);
            this.txtFields.TabIndex = 5;
            this.txtFields.Text = "";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(180, 303);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(639, 440);
            this.txtResult.TabIndex = 6;
            this.txtResult.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "ReplaceDic2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "ReplaceDic1";
            // 
            // txtReplace2
            // 
            this.txtReplace2.Location = new System.Drawing.Point(87, 59);
            this.txtReplace2.Name = "txtReplace2";
            this.txtReplace2.ReadOnly = true;
            this.txtReplace2.Size = new System.Drawing.Size(360, 20);
            this.txtReplace2.TabIndex = 7;
            this.txtReplace2.Text = "Replace2.txt";
            // 
            // btnOpenReplace2
            // 
            this.btnOpenReplace2.Location = new System.Drawing.Point(463, 57);
            this.btnOpenReplace2.Name = "btnOpenReplace2";
            this.btnOpenReplace2.Size = new System.Drawing.Size(75, 23);
            this.btnOpenReplace2.TabIndex = 9;
            this.btnOpenReplace2.Text = "Open";
            this.btnOpenReplace2.UseVisualStyleBackColor = true;
            // 
            // btnOpenReplace1
            // 
            this.btnOpenReplace1.Location = new System.Drawing.Point(463, 31);
            this.btnOpenReplace1.Name = "btnOpenReplace1";
            this.btnOpenReplace1.Size = new System.Drawing.Size(75, 23);
            this.btnOpenReplace1.TabIndex = 10;
            this.btnOpenReplace1.Text = "Open";
            this.btnOpenReplace1.UseVisualStyleBackColor = true;
            // 
            // txtReplace1
            // 
            this.txtReplace1.Location = new System.Drawing.Point(87, 33);
            this.txtReplace1.Name = "txtReplace1";
            this.txtReplace1.ReadOnly = true;
            this.txtReplace1.Size = new System.Drawing.Size(360, 20);
            this.txtReplace1.TabIndex = 8;
            this.txtReplace1.Text = "Replace1.txt";
            // 
            // FormModelHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 755);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReplace2);
            this.Controls.Add(this.btnOpenReplace2);
            this.Controls.Add(this.btnOpenReplace1);
            this.Controls.Add(this.txtReplace1);
            this.Controls.Add(this.txtFields);
            this.Controls.Add(this.txtResult);
            this.Name = "FormModelHelper";
            this.Text = "FormModelHelper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtFields;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReplace2;
        private System.Windows.Forms.Button btnOpenReplace2;
        private System.Windows.Forms.Button btnOpenReplace1;
        private System.Windows.Forms.TextBox txtReplace1;
    }
}