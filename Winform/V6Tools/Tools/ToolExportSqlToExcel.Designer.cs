namespace Tools
{
    partial class ToolExportSqlToExcel
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
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxDatabases = new System.Windows.Forms.ListBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.grbLogin = new System.Windows.Forms.GroupBox();
            this.chkLogin = new System.Windows.Forms.CheckBox();
            this.listBoxTablesName = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.txtDatabaseNameAttach = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAttachResult = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowLdf = new System.Windows.Forms.Button();
            this.radAttachSP = new System.Windows.Forms.RadioButton();
            this.radAttachSql = new System.Windows.Forms.RadioButton();
            this.btnBrowMdf = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogFileAttach = new System.Windows.Forms.TextBox();
            this.txtDatabaseFileAttach = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.grbExport = new System.Windows.Forms.GroupBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.chkConvert = new System.Windows.Forms.CheckBox();
            this.btnExportXml = new System.Windows.Forms.Button();
            this.grbLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(12, 25);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(157, 20);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtServerName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ServerName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database list";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(6, 32);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(160, 20);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "UserName";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(6, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(160, 20);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password";
            // 
            // listBoxDatabases
            // 
            this.listBoxDatabases.FormattingEnabled = true;
            this.listBoxDatabases.Location = new System.Drawing.Point(9, 64);
            this.listBoxDatabases.Name = "listBoxDatabases";
            this.listBoxDatabases.Size = new System.Drawing.Size(160, 173);
            this.listBoxDatabases.TabIndex = 3;
            this.listBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.listBoxDatabases_SelectedIndexChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(91, 97);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // grbLogin
            // 
            this.grbLogin.Controls.Add(this.label3);
            this.grbLogin.Controls.Add(this.btnLogin);
            this.grbLogin.Controls.Add(this.txtUserName);
            this.grbLogin.Controls.Add(this.txtPassword);
            this.grbLogin.Controls.Add(this.label4);
            this.grbLogin.Enabled = false;
            this.grbLogin.Location = new System.Drawing.Point(175, 31);
            this.grbLogin.Name = "grbLogin";
            this.grbLogin.Size = new System.Drawing.Size(175, 125);
            this.grbLogin.TabIndex = 5;
            this.grbLogin.TabStop = false;
            this.grbLogin.Text = "Login";
            // 
            // chkLogin
            // 
            this.chkLogin.AutoSize = true;
            this.chkLogin.Location = new System.Drawing.Point(175, 8);
            this.chkLogin.Name = "chkLogin";
            this.chkLogin.Size = new System.Drawing.Size(52, 17);
            this.chkLogin.TabIndex = 6;
            this.chkLogin.Text = "Login";
            this.chkLogin.UseVisualStyleBackColor = true;
            this.chkLogin.CheckedChanged += new System.EventHandler(this.chkLogin_CheckedChanged);
            // 
            // listBoxTablesName
            // 
            this.listBoxTablesName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTablesName.FormattingEnabled = true;
            this.listBoxTablesName.Location = new System.Drawing.Point(9, 256);
            this.listBoxTablesName.Name = "listBoxTablesName";
            this.listBoxTablesName.Size = new System.Drawing.Size(160, 407);
            this.listBoxTablesName.TabIndex = 3;
            this.listBoxTablesName.SelectedIndexChanged += new System.EventHandler(this.listBoxTablesName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Table list";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(175, 256);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(534, 407);
            this.dataGridView1.TabIndex = 7;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(17, 52);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 8;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(233, 120);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(95, 23);
            this.btnAttach.TabIndex = 9;
            this.btnAttach.Text = "Attach (local)";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // txtDatabaseNameAttach
            // 
            this.txtDatabaseNameAttach.Location = new System.Drawing.Point(99, 17);
            this.txtDatabaseNameAttach.Name = "txtDatabaseNameAttach";
            this.txtDatabaseNameAttach.Size = new System.Drawing.Size(160, 20);
            this.txtDatabaseNameAttach.TabIndex = 0;
            this.txtDatabaseNameAttach.Text = "NewName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAttachResult);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnBrowLdf);
            this.groupBox1.Controls.Add(this.radAttachSP);
            this.groupBox1.Controls.Add(this.radAttachSql);
            this.groupBox1.Controls.Add(this.btnBrowMdf);
            this.groupBox1.Controls.Add(this.btnAttach);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLogFileAttach);
            this.groupBox1.Controls.Add(this.txtDatabaseFileAttach);
            this.groupBox1.Controls.Add(this.txtDatabaseNameAttach);
            this.groupBox1.Location = new System.Drawing.Point(375, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 148);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AttachDatabase";
            // 
            // lblAttachResult
            // 
            this.lblAttachResult.AutoSize = true;
            this.lblAttachResult.Location = new System.Drawing.Point(6, 125);
            this.lblAttachResult.Name = "lblAttachResult";
            this.lblAttachResult.Size = new System.Drawing.Size(32, 13);
            this.lblAttachResult.TabIndex = 1;
            this.lblAttachResult.Text = "result";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "ldf path";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "mdf path";
            // 
            // btnBrowLdf
            // 
            this.btnBrowLdf.Location = new System.Drawing.Point(265, 67);
            this.btnBrowLdf.Name = "btnBrowLdf";
            this.btnBrowLdf.Size = new System.Drawing.Size(31, 23);
            this.btnBrowLdf.TabIndex = 9;
            this.btnBrowLdf.Text = "...";
            this.btnBrowLdf.UseVisualStyleBackColor = true;
            this.btnBrowLdf.Click += new System.EventHandler(this.btnBrowLdf_Click);
            // 
            // radAttachSP
            // 
            this.radAttachSP.AutoSize = true;
            this.radAttachSP.Checked = true;
            this.radAttachSP.Location = new System.Drawing.Point(9, 96);
            this.radAttachSP.Name = "radAttachSP";
            this.radAttachSP.Size = new System.Drawing.Size(86, 17);
            this.radAttachSP.TabIndex = 6;
            this.radAttachSP.TabStop = true;
            this.radAttachSP.Text = "exec sys.sp..";
            this.radAttachSP.UseVisualStyleBackColor = true;
            // 
            // radAttachSql
            // 
            this.radAttachSql.AutoSize = true;
            this.radAttachSql.Location = new System.Drawing.Point(102, 97);
            this.radAttachSql.Name = "radAttachSql";
            this.radAttachSql.Size = new System.Drawing.Size(71, 17);
            this.radAttachSql.TabIndex = 6;
            this.radAttachSql.Text = "sql create";
            this.radAttachSql.UseVisualStyleBackColor = true;
            // 
            // btnBrowMdf
            // 
            this.btnBrowMdf.Location = new System.Drawing.Point(265, 41);
            this.btnBrowMdf.Name = "btnBrowMdf";
            this.btnBrowMdf.Size = new System.Drawing.Size(31, 23);
            this.btnBrowMdf.TabIndex = 9;
            this.btnBrowMdf.Text = "...";
            this.btnBrowMdf.UseVisualStyleBackColor = true;
            this.btnBrowMdf.Click += new System.EventHandler(this.btnBrowMdf_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "DatabaseName";
            // 
            // txtLogFileAttach
            // 
            this.txtLogFileAttach.Location = new System.Drawing.Point(99, 69);
            this.txtLogFileAttach.Name = "txtLogFileAttach";
            this.txtLogFileAttach.Size = new System.Drawing.Size(160, 20);
            this.txtLogFileAttach.TabIndex = 0;
            this.txtLogFileAttach.Text = "databasefile.ldf";
            // 
            // txtDatabaseFileAttach
            // 
            this.txtDatabaseFileAttach.Location = new System.Drawing.Point(99, 43);
            this.txtDatabaseFileAttach.Name = "txtDatabaseFileAttach";
            this.txtDatabaseFileAttach.Size = new System.Drawing.Size(160, 20);
            this.txtDatabaseFileAttach.TabIndex = 0;
            this.txtDatabaseFileAttach.Text = "databasefile.mdf";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtQuery);
            this.groupBox2.Location = new System.Drawing.Point(175, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 88);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execute query (F5)";
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(9, 19);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(290, 56);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
            // 
            // grbExport
            // 
            this.grbExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbExport.Controls.Add(this.txtTo);
            this.grbExport.Controls.Add(this.label9);
            this.grbExport.Controls.Add(this.txtFrom);
            this.grbExport.Controls.Add(this.chkConvert);
            this.grbExport.Controls.Add(this.btnExportXml);
            this.grbExport.Controls.Add(this.btnExportExcel);
            this.grbExport.Location = new System.Drawing.Point(509, 162);
            this.grbExport.Name = "grbExport";
            this.grbExport.Size = new System.Drawing.Size(200, 88);
            this.grbExport.TabIndex = 12;
            this.grbExport.TabStop = false;
            this.grbExport.Text = "Export";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(153, 16);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(31, 20);
            this.txtTo.TabIndex = 12;
            this.txtTo.Text = "A";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(131, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "to";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(94, 16);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(31, 20);
            this.txtFrom.TabIndex = 10;
            this.txtFrom.Text = "U";
            // 
            // chkConvert
            // 
            this.chkConvert.AutoSize = true;
            this.chkConvert.Checked = true;
            this.chkConvert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConvert.Location = new System.Drawing.Point(6, 19);
            this.chkConvert.Name = "chkConvert";
            this.chkConvert.Size = new System.Drawing.Size(86, 17);
            this.chkConvert.TabIndex = 9;
            this.chkConvert.Text = "Convert from";
            this.chkConvert.UseVisualStyleBackColor = true;
            // 
            // btnExportXml
            // 
            this.btnExportXml.Location = new System.Drawing.Point(99, 52);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(75, 23);
            this.btnExportXml.TabIndex = 8;
            this.btnExportXml.Text = "Export Xml";
            this.btnExportXml.UseVisualStyleBackColor = true;
            this.btnExportXml.Visible = false;
            this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
            // 
            // ToolExportSqlToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 676);
            this.Controls.Add(this.grbExport);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chkLogin);
            this.Controls.Add(this.grbLogin);
            this.Controls.Add(this.listBoxTablesName);
            this.Controls.Add(this.listBoxDatabases);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServerName);
            this.Name = "ToolExportSqlToExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolExportSqlToFile";
            this.Load += new System.EventHandler(this.ToolExportSqlToExcel_Load);
            this.grbLogin.ResumeLayout(false);
            this.grbLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grbExport.ResumeLayout(false);
            this.grbExport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxDatabases;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.GroupBox grbLogin;
        private System.Windows.Forms.CheckBox chkLogin;
        private System.Windows.Forms.ListBox listBoxTablesName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.TextBox txtDatabaseNameAttach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBrowMdf;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDatabaseFileAttach;
        private System.Windows.Forms.Label lblAttachResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLogFileAttach;
        private System.Windows.Forms.Button btnBrowLdf;
        private System.Windows.Forms.RadioButton radAttachSP;
        private System.Windows.Forms.RadioButton radAttachSql;
        private System.Windows.Forms.GroupBox grbExport;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.CheckBox chkConvert;
        private System.Windows.Forms.Button btnExportXml;
    }
}