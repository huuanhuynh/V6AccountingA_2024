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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listBoxTablesName = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.grbExport = new System.Windows.Forms.GroupBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.chkConvert = new System.Windows.Forms.CheckBox();
            this.btnExportXml = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnRowsToXml = new System.Windows.Forms.Button();
            this.btnImportXml = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBy = new System.Windows.Forms.TextBox();
            this.chkUID = new System.Windows.Forms.CheckBox();
            this.btnGenInsertSQL = new System.Windows.Forms.Button();
            this.menuExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportFoxProMenu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grbExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTablesName
            // 
            this.listBoxTablesName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTablesName.FormattingEnabled = true;
            this.listBoxTablesName.Location = new System.Drawing.Point(9, 22);
            this.listBoxTablesName.Name = "listBoxTablesName";
            this.listBoxTablesName.Size = new System.Drawing.Size(160, 628);
            this.listBoxTablesName.TabIndex = 1;
            this.listBoxTablesName.SelectedIndexChanged += new System.EventHandler(this.listBoxTablesName_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = ".";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Table list";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Control_A = true;
            this.dataGridView1.Control_E = true;
            this.dataGridView1.Control_S = true;
            this.dataGridView1.Location = new System.Drawing.Point(175, 156);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(821, 496);
            this.dataGridView1.Space_Bar = true;
            this.dataGridView1.TabIndex = 9;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.ContextMenuStrip = this.menuExport;
            this.btnExportExcel.Location = new System.Drawing.Point(6, 38);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 4;
            this.btnExportExcel.Text = "Export Excel";
            this.toolTipV6FormControl.SetToolTip(this.btnExportExcel, "Nhấn chuột phải để hiện menu");
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // grbExport
            // 
            this.grbExport.AccessibleDescription = ".";
            this.grbExport.Controls.Add(this.txtTo);
            this.grbExport.Controls.Add(this.label9);
            this.grbExport.Controls.Add(this.txtFrom);
            this.grbExport.Controls.Add(this.chkConvert);
            this.grbExport.Controls.Add(this.btnExportXml);
            this.grbExport.Controls.Add(this.btnExportExcel);
            this.grbExport.Location = new System.Drawing.Point(175, 6);
            this.grbExport.Name = "grbExport";
            this.grbExport.Size = new System.Drawing.Size(211, 65);
            this.grbExport.TabIndex = 2;
            this.grbExport.TabStop = false;
            this.grbExport.Text = "Export";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(153, 16);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(31, 20);
            this.txtTo.TabIndex = 3;
            this.txtTo.Text = "A";
            // 
            // label9
            // 
            this.label9.AccessibleDescription = ".";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(131, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "to";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(94, 16);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(31, 20);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.Text = "U";
            // 
            // chkConvert
            // 
            this.chkConvert.AccessibleDescription = ".";
            this.chkConvert.AutoSize = true;
            this.chkConvert.Location = new System.Drawing.Point(6, 19);
            this.chkConvert.Name = "chkConvert";
            this.chkConvert.Size = new System.Drawing.Size(86, 17);
            this.chkConvert.TabIndex = 0;
            this.chkConvert.Text = "Convert from";
            this.chkConvert.UseVisualStyleBackColor = true;
            // 
            // btnExportXml
            // 
            this.btnExportXml.Location = new System.Drawing.Point(118, 38);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(75, 23);
            this.btnExportXml.TabIndex = 5;
            this.btnExportXml.Text = "Export Xml";
            this.btnExportXml.UseVisualStyleBackColor = true;
            this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = ".";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(392, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 123);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(598, 104);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // btnRowsToXml
            // 
            this.btnRowsToXml.Location = new System.Drawing.Point(175, 73);
            this.btnRowsToXml.Name = "btnRowsToXml";
            this.btnRowsToXml.Size = new System.Drawing.Size(75, 23);
            this.btnRowsToXml.TabIndex = 3;
            this.btnRowsToXml.Text = "Rows to Xml";
            this.btnRowsToXml.UseVisualStyleBackColor = true;
            this.btnRowsToXml.Click += new System.EventHandler(this.btnRowsToXml_Click);
            // 
            // btnImportXml
            // 
            this.btnImportXml.Location = new System.Drawing.Point(309, 73);
            this.btnImportXml.Name = "btnImportXml";
            this.btnImportXml.Size = new System.Drawing.Size(75, 23);
            this.btnImportXml.TabIndex = 5;
            this.btnImportXml.Text = "Import Xml";
            this.btnImportXml.UseVisualStyleBackColor = true;
            this.btnImportXml.Click += new System.EventHandler(this.btnImportXml_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Update Xml By";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnUpdateXmlBy_Click);
            // 
            // txtBy
            // 
            this.txtBy.Location = new System.Drawing.Point(293, 104);
            this.txtBy.Name = "txtBy";
            this.txtBy.Size = new System.Drawing.Size(93, 20);
            this.txtBy.TabIndex = 7;
            this.txtBy.Text = "UID";
            // 
            // chkUID
            // 
            this.chkUID.AccessibleDescription = ".";
            this.chkUID.AutoSize = true;
            this.chkUID.Checked = true;
            this.chkUID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUID.Location = new System.Drawing.Point(256, 77);
            this.chkUID.Name = "chkUID";
            this.chkUID.Size = new System.Drawing.Size(45, 17);
            this.chkUID.TabIndex = 4;
            this.chkUID.Text = "UID";
            this.chkUID.UseVisualStyleBackColor = true;
            // 
            // btnGenInsertSQL
            // 
            this.btnGenInsertSQL.Location = new System.Drawing.Point(175, 127);
            this.btnGenInsertSQL.Name = "btnGenInsertSQL";
            this.btnGenInsertSQL.Size = new System.Drawing.Size(101, 23);
            this.btnGenInsertSQL.TabIndex = 6;
            this.btnGenInsertSQL.Text = "GenInsertSQL";
            this.toolTipV6FormControl.SetToolTip(this.btnGenInsertSQL, "Tạo câu insert từ các dòng đang chọn.");
            this.btnGenInsertSQL.UseVisualStyleBackColor = true;
            this.btnGenInsertSQL.Click += new System.EventHandler(this.btnGenInsertSQL_Click);
            // 
            // menuExport
            // 
            this.menuExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportFoxProMenu});
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(188, 48);
            // 
            // exportFoxProMenu
            // 
            this.exportFoxProMenu.Name = "exportFoxProMenu";
            this.exportFoxProMenu.Size = new System.Drawing.Size(187, 22);
            this.exportFoxProMenu.Text = "EXPORT DBF (FoxPro)";
            this.exportFoxProMenu.Click += new System.EventHandler(this.exportFoxProMenu_Click);
            // 
            // FormExportSqlTableToFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBy);
            this.Controls.Add(this.chkUID);
            this.Controls.Add(this.grbExport);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGenInsertSQL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnImportXml);
            this.Controls.Add(this.btnRowsToXml);
            this.Controls.Add(this.listBoxTablesName);
            this.Controls.Add(this.label5);
            this.Name = "FormExportSqlTableToFile";
            this.Text = "ToolExportSqlToFile";
            this.Load += new System.EventHandler(this.ToolExportSqlToExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grbExport.ResumeLayout(false);
            this.grbExport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.menuExport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTablesName;
        private System.Windows.Forms.Label label5;
        private V6Controls.V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox grbExport;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.CheckBox chkConvert;
        private System.Windows.Forms.Button btnExportXml;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnRowsToXml;
        private System.Windows.Forms.Button btnImportXml;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBy;
        private System.Windows.Forms.CheckBox chkUID;
        private System.Windows.Forms.Button btnGenInsertSQL;
        private System.Windows.Forms.ContextMenuStrip menuExport;
        private System.Windows.Forms.ToolStripMenuItem exportFoxProMenu;
    }
}