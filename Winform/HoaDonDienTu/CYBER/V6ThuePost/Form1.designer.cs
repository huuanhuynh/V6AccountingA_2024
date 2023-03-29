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
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMST = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXmlFile = new System.Windows.Forms.TextBox();
            this.txtDbfFile = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnGetMeta = new System.Windows.Forms.Button();
            this.btnSignHSM = new System.Windows.Forms.Button();
            this.btnDownloadPDF = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radM = new System.Windows.Forms.RadioButton();
            this.radS = new System.Windows.Forms.RadioButton();
            this.radT = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(570, 759);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 95);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(911, 568);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.Location = new System.Drawing.Point(9, 666);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(914, 60);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "content";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(9, 726);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(636, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "errorMessage";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(12, 26);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(158, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(176, 26);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '_';
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(190, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "User name";
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
            this.label5.Location = new System.Drawing.Point(12, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Body";
            // 
            // txtMST
            // 
            this.txtMST.Location = new System.Drawing.Point(405, 26);
            this.txtMST.Name = "txtMST";
            this.txtMST.ReadOnly = true;
            this.txtMST.Size = new System.Drawing.Size(158, 20);
            this.txtMST.TabIndex = 5;
            this.txtMST.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(402, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "MST";
            this.label6.Visible = false;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRead.Location = new System.Drawing.Point(488, 758);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 10;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(50, 52);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(559, 20);
            this.txtURL.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "URL";
            // 
            // txtXmlFile
            // 
            this.txtXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtXmlFile.Location = new System.Drawing.Point(16, 758);
            this.txtXmlFile.Name = "txtXmlFile";
            this.txtXmlFile.Size = new System.Drawing.Size(158, 20);
            this.txtXmlFile.TabIndex = 1;
            this.txtXmlFile.Text = "V6ThuePost.xml";
            // 
            // txtDbfFile
            // 
            this.txtDbfFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDbfFile.Location = new System.Drawing.Point(180, 758);
            this.txtDbfFile.Name = "txtDbfFile";
            this.txtDbfFile.Size = new System.Drawing.Size(158, 20);
            this.txtDbfFile.TabIndex = 1;
            this.txtDbfFile.Text = "tPrint_soa.DBF";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(744, 733);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnGetMeta
            // 
            this.btnGetMeta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetMeta.Location = new System.Drawing.Point(825, 732);
            this.btnGetMeta.Name = "btnGetMeta";
            this.btnGetMeta.Size = new System.Drawing.Size(75, 23);
            this.btnGetMeta.TabIndex = 10;
            this.btnGetMeta.Text = "Get Meta";
            this.btnGetMeta.UseVisualStyleBackColor = true;
            this.btnGetMeta.Click += new System.EventHandler(this.btnGetMeta_Click);
            // 
            // btnSignHSM
            // 
            this.btnSignHSM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignHSM.Enabled = false;
            this.btnSignHSM.Location = new System.Drawing.Point(744, 756);
            this.btnSignHSM.Name = "btnSignHSM";
            this.btnSignHSM.Size = new System.Drawing.Size(75, 23);
            this.btnSignHSM.TabIndex = 11;
            this.btnSignHSM.Text = "Sign HSM";
            this.btnSignHSM.UseVisualStyleBackColor = true;
            this.btnSignHSM.Click += new System.EventHandler(this.btnSignHSM_Click);
            // 
            // btnDownloadPDF
            // 
            this.btnDownloadPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadPDF.Enabled = false;
            this.btnDownloadPDF.Location = new System.Drawing.Point(825, 756);
            this.btnDownloadPDF.Name = "btnDownloadPDF";
            this.btnDownloadPDF.Size = new System.Drawing.Size(75, 23);
            this.btnDownloadPDF.TabIndex = 12;
            this.btnDownloadPDF.Text = "Download PDF";
            this.btnDownloadPDF.UseVisualStyleBackColor = true;
            this.btnDownloadPDF.Click += new System.EventHandler(this.btnDownloadPDF_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.radT);
            this.groupBox1.Controls.Add(this.radS);
            this.groupBox1.Controls.Add(this.radM);
            this.groupBox1.Location = new System.Drawing.Point(344, 726);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 56);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // radM
            // 
            this.radM.AutoSize = true;
            this.radM.Checked = true;
            this.radM.Location = new System.Drawing.Point(6, 19);
            this.radM.Name = "radM";
            this.radM.Size = new System.Drawing.Size(34, 17);
            this.radM.TabIndex = 0;
            this.radM.TabStop = true;
            this.radM.Text = "M";
            this.radM.UseVisualStyleBackColor = true;
            // 
            // radS
            // 
            this.radS.AutoSize = true;
            this.radS.Location = new System.Drawing.Point(46, 19);
            this.radS.Name = "radS";
            this.radS.Size = new System.Drawing.Size(32, 17);
            this.radS.TabIndex = 0;
            this.radS.Text = "S";
            this.radS.UseVisualStyleBackColor = true;
            // 
            // radT
            // 
            this.radT.AutoSize = true;
            this.radT.Location = new System.Drawing.Point(87, 19);
            this.radT.Name = "radT";
            this.radT.Size = new System.Drawing.Size(32, 17);
            this.radT.TabIndex = 0;
            this.radT.Text = "T";
            this.radT.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 790);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDownloadPDF);
            this.Controls.Add(this.btnSignHSM);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtMST);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtDbfFile);
            this.Controls.Add(this.txtXmlFile);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnGetMeta);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "V6ThuePost";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMST;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXmlFile;
        private System.Windows.Forms.TextBox txtDbfFile;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnGetMeta;
        private System.Windows.Forms.Button btnSignHSM;
        private System.Windows.Forms.Button btnDownloadPDF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radT;
        private System.Windows.Forms.RadioButton radS;
        private System.Windows.Forms.RadioButton radM;
    }
}

