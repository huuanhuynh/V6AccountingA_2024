namespace V6ThuePost
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSend = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXmlFile = new System.Windows.Forms.TextBox();
            this.txtDbfFile = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.RichTextBox();
            this.btnReadS = new System.Windows.Forms.Button();
            this.btnSendS = new System.Windows.Forms.Button();
            this.btnReadT = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtDbfExcel = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSend0 = new System.Windows.Forms.Button();
            this.btnIssue0 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(453, 594);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 19;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 64);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(592, 435);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(9, 562);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(519, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "errorMessage";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(45, 6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(127, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(229, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '_';
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(109, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "User";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Data";
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRead.Location = new System.Drawing.Point(176, 594);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 13;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(45, 32);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(559, 20);
            this.txtURL.TabIndex = 5;
            this.txtURL.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "URL";
            this.label1.Visible = false;
            // 
            // txtXmlFile
            // 
            this.txtXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtXmlFile.Location = new System.Drawing.Point(12, 594);
            this.txtXmlFile.Name = "txtXmlFile";
            this.txtXmlFile.Size = new System.Drawing.Size(158, 20);
            this.txtXmlFile.TabIndex = 10;
            this.txtXmlFile.Text = "V6ThuePost.xml";
            // 
            // txtDbfFile
            // 
            this.txtDbfFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDbfFile.Location = new System.Drawing.Point(12, 620);
            this.txtDbfFile.Name = "txtDbfFile";
            this.txtDbfFile.Size = new System.Drawing.Size(158, 20);
            this.txtDbfFile.TabIndex = 11;
            this.txtDbfFile.Text = "tPrint_soa.DBF";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.Location = new System.Drawing.Point(12, 505);
            this.lblResult.Name = "lblResult";
            this.lblResult.ReadOnly = true;
            this.lblResult.Size = new System.Drawing.Size(592, 54);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "";
            // 
            // btnReadS
            // 
            this.btnReadS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReadS.Location = new System.Drawing.Point(257, 623);
            this.btnReadS.Name = "btnReadS";
            this.btnReadS.Size = new System.Drawing.Size(75, 23);
            this.btnReadS.TabIndex = 14;
            this.btnReadS.Text = "ReadS";
            this.btnReadS.UseVisualStyleBackColor = true;
            this.btnReadS.Click += new System.EventHandler(this.btnReadS_Click);
            // 
            // btnSendS
            // 
            this.btnSendS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendS.Enabled = false;
            this.btnSendS.Location = new System.Drawing.Point(376, 623);
            this.btnSendS.Name = "btnSendS";
            this.btnSendS.Size = new System.Drawing.Size(75, 23);
            this.btnSendS.TabIndex = 20;
            this.btnSendS.Text = "SendS";
            this.btnSendS.UseVisualStyleBackColor = true;
            this.btnSendS.Click += new System.EventHandler(this.btnSendS_Click);
            // 
            // btnReadT
            // 
            this.btnReadT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReadT.Location = new System.Drawing.Point(176, 623);
            this.btnReadT.Name = "btnReadT";
            this.btnReadT.Size = new System.Drawing.Size(75, 23);
            this.btnReadT.TabIndex = 15;
            this.btnReadT.Text = "ReadT";
            this.btnReadT.UseVisualStyleBackColor = true;
            this.btnReadT.Click += new System.EventHandler(this.btnReadT_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(376, 645);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 21;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExcel.Location = new System.Drawing.Point(176, 647);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 16;
            this.btnExportExcel.Text = "ExportExcel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtDbfExcel
            // 
            this.txtDbfExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDbfExcel.Location = new System.Drawing.Point(12, 647);
            this.txtDbfExcel.Name = "txtDbfExcel";
            this.txtDbfExcel.Size = new System.Drawing.Size(158, 20);
            this.txtDbfExcel.TabIndex = 12;
            this.txtDbfExcel.Text = "tPrint_soa_excel.DBF";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(453, 568);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 17;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSend0
            // 
            this.btnSend0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend0.Enabled = false;
            this.btnSend0.Location = new System.Drawing.Point(534, 594);
            this.btnSend0.Name = "btnSend0";
            this.btnSend0.Size = new System.Drawing.Size(75, 23);
            this.btnSend0.TabIndex = 18;
            this.btnSend0.Text = "Send 0";
            this.btnSend0.UseVisualStyleBackColor = true;
            this.btnSend0.Click += new System.EventHandler(this.btnSend0_Click);
            // 
            // btnIssue0
            // 
            this.btnIssue0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIssue0.Enabled = false;
            this.btnIssue0.Location = new System.Drawing.Point(534, 620);
            this.btnIssue0.Name = "btnIssue0";
            this.btnIssue0.Size = new System.Drawing.Size(75, 23);
            this.btnIssue0.TabIndex = 18;
            this.btnIssue0.Text = "Issue 0";
            this.btnIssue0.UseVisualStyleBackColor = true;
            this.btnIssue0.Click += new System.EventHandler(this.btnIssue0_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 679);
            this.Controls.Add(this.btnIssue0);
            this.Controls.Add(this.btnSend0);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtDbfExcel);
            this.Controls.Add(this.txtDbfFile);
            this.Controls.Add(this.txtXmlFile);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnReadT);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnReadS);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnSendS);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "V6ThuePost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXmlFile;
        private System.Windows.Forms.TextBox txtDbfFile;
        private System.Windows.Forms.RichTextBox lblResult;
        private System.Windows.Forms.Button btnReadS;
        private System.Windows.Forms.Button btnSendS;
        private System.Windows.Forms.Button btnReadT;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.TextBox txtDbfExcel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSend0;
        private System.Windows.Forms.Button btnIssue0;
    }
}

