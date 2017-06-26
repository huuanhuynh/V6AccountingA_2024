﻿namespace V6ControlManager.FormManager.ToolManager
{
    partial class FormExportSqlTableToFile
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
            this.listBoxTablesName = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.grbExport = new System.Windows.Forms.GroupBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.chkConvert = new System.Windows.Forms.CheckBox();
            this.btnExportXml = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTablesName
            // 
            this.listBoxTablesName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTablesName.FormattingEnabled = true;
            this.listBoxTablesName.Location = new System.Drawing.Point(9, 22);
            this.listBoxTablesName.Name = "listBoxTablesName";
            this.listBoxTablesName.Size = new System.Drawing.Size(160, 511);
            this.listBoxTablesName.TabIndex = 3;
            this.listBoxTablesName.SelectedIndexChanged += new System.EventHandler(this.listBoxTablesName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
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
            this.dataGridView1.Location = new System.Drawing.Point(175, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(597, 398);
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
            // grbExport
            // 
            this.grbExport.Controls.Add(this.txtTo);
            this.grbExport.Controls.Add(this.label9);
            this.grbExport.Controls.Add(this.txtFrom);
            this.grbExport.Controls.Add(this.chkConvert);
            this.grbExport.Controls.Add(this.btnExportXml);
            this.grbExport.Controls.Add(this.btnExportExcel);
            this.grbExport.Location = new System.Drawing.Point(186, 6);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(392, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 123);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(374, 104);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // FormExportSqlTableToFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 542);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbExport);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBoxTablesName);
            this.Controls.Add(this.label5);
            this.Name = "FormExportSqlTableToFile";
            this.Text = "ToolExportSqlToFile";
            this.Load += new System.EventHandler(this.ToolExportSqlToExcel_Load);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.listBoxTablesName, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.grbExport, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbExport.ResumeLayout(false);
            this.grbExport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTablesName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox grbExport;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.CheckBox chkConvert;
        private System.Windows.Forms.Button btnExportXml;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}