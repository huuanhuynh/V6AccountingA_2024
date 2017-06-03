namespace V6MDBtoSQLthread
{
    partial class FormXmlTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXmlTable));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveXml = new System.Windows.Forms.Button();
            this.btnLoadXml = new System.Windows.Forms.Button();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDataSetName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnRemoveAllRows = new System.Windows.Forms.Button();
            this.btnRemoveAllColumns = new System.Windows.Forms.Button();
            this.chkDeleteRow = new System.Windows.Forms.CheckBox();
            this.chkDeleteColumn = new System.Windows.Forms.CheckBox();
            this.chkAddAtIndex = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 90);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(688, 367);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeRowToolStripMenuItem,
            this.removeColumnToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // removeRowToolStripMenuItem
            // 
            this.removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
            this.removeRowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeRowToolStripMenuItem.Text = "RemoveRow";
            this.removeRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
            // 
            // removeColumnToolStripMenuItem
            // 
            this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
            this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeColumnToolStripMenuItem.Text = "RemoveColumn";
            this.removeColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(205, 62);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(75, 23);
            this.btnAddColumn.TabIndex = 1;
            this.btnAddColumn.Text = "Add column";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // txtColumnName
            // 
            this.txtColumnName.Location = new System.Drawing.Point(99, 64);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(100, 20);
            this.txtColumnName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Column name";
            // 
            // btnSaveXml
            // 
            this.btnSaveXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveXml.Location = new System.Drawing.Point(96, 463);
            this.btnSaveXml.Name = "btnSaveXml";
            this.btnSaveXml.Size = new System.Drawing.Size(75, 23);
            this.btnSaveXml.TabIndex = 4;
            this.btnSaveXml.Text = "Save Xml";
            this.btnSaveXml.UseVisualStyleBackColor = true;
            this.btnSaveXml.Click += new System.EventHandler(this.btnSaveXml_Click);
            // 
            // btnLoadXml
            // 
            this.btnLoadXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadXml.Location = new System.Drawing.Point(15, 463);
            this.btnLoadXml.Name = "btnLoadXml";
            this.btnLoadXml.Size = new System.Drawing.Size(75, 23);
            this.btnLoadXml.TabIndex = 4;
            this.btnLoadXml.Text = "Load Xml";
            this.btnLoadXml.UseVisualStyleBackColor = true;
            this.btnLoadXml.Click += new System.EventHandler(this.btnLoadXml_Click);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNew.Location = new System.Drawing.Point(193, 463);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(133, 23);
            this.btnSaveNew.TabIndex = 5;
            this.btnSaveNew.Text = "Save New Xml Struct";
            this.btnSaveNew.UseVisualStyleBackColor = true;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "DataSet Name";
            // 
            // txtDataSetName
            // 
            this.txtDataSetName.Location = new System.Drawing.Point(99, 12);
            this.txtDataSetName.Name = "txtDataSetName";
            this.txtDataSetName.Size = new System.Drawing.Size(100, 20);
            this.txtDataSetName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Table Name";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(99, 38);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(100, 20);
            this.txtTableName.TabIndex = 2;
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Location = new System.Drawing.Point(415, 36);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(112, 23);
            this.btnRemoveColumn.TabIndex = 6;
            this.btnRemoveColumn.Text = "Remove Column";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Location = new System.Drawing.Point(415, 12);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(112, 23);
            this.btnRemoveRow.TabIndex = 7;
            this.btnRemoveRow.Text = "Remove Row";
            this.btnRemoveRow.UseVisualStyleBackColor = true;
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btnRemoveAllRows
            // 
            this.btnRemoveAllRows.Location = new System.Drawing.Point(533, 12);
            this.btnRemoveAllRows.Name = "btnRemoveAllRows";
            this.btnRemoveAllRows.Size = new System.Drawing.Size(145, 23);
            this.btnRemoveAllRows.TabIndex = 7;
            this.btnRemoveAllRows.Text = "Remove all rows";
            this.btnRemoveAllRows.UseVisualStyleBackColor = true;
            this.btnRemoveAllRows.Click += new System.EventHandler(this.btnRemoveAllRows_Click);
            // 
            // btnRemoveAllColumns
            // 
            this.btnRemoveAllColumns.Location = new System.Drawing.Point(533, 36);
            this.btnRemoveAllColumns.Name = "btnRemoveAllColumns";
            this.btnRemoveAllColumns.Size = new System.Drawing.Size(145, 23);
            this.btnRemoveAllColumns.TabIndex = 6;
            this.btnRemoveAllColumns.Text = "Remove all columns";
            this.btnRemoveAllColumns.UseVisualStyleBackColor = true;
            this.btnRemoveAllColumns.Click += new System.EventHandler(this.btnRemoveAllColumns_Click);
            // 
            // chkDeleteRow
            // 
            this.chkDeleteRow.AutoSize = true;
            this.chkDeleteRow.Location = new System.Drawing.Point(293, 16);
            this.chkDeleteRow.Name = "chkDeleteRow";
            this.chkDeleteRow.Size = new System.Drawing.Size(116, 17);
            this.chkDeleteRow.TabIndex = 9;
            this.chkDeleteRow.Text = "Xóa không cần hỏi";
            this.chkDeleteRow.UseVisualStyleBackColor = true;
            // 
            // chkDeleteColumn
            // 
            this.chkDeleteColumn.AutoSize = true;
            this.chkDeleteColumn.Location = new System.Drawing.Point(293, 40);
            this.chkDeleteColumn.Name = "chkDeleteColumn";
            this.chkDeleteColumn.Size = new System.Drawing.Size(116, 17);
            this.chkDeleteColumn.TabIndex = 9;
            this.chkDeleteColumn.Text = "Xóa không cần hỏi";
            this.chkDeleteColumn.UseVisualStyleBackColor = true;
            // 
            // chkAddAtIndex
            // 
            this.chkAddAtIndex.AutoSize = true;
            this.chkAddAtIndex.Location = new System.Drawing.Point(293, 68);
            this.chkAddAtIndex.Name = "chkAddAtIndex";
            this.chkAddAtIndex.Size = new System.Drawing.Size(159, 17);
            this.chkAddAtIndex.TabIndex = 10;
            this.chkAddAtIndex.Text = "Thêm trước vị trí đang chọn";
            this.chkAddAtIndex.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(603, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormXmlTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 498);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkAddAtIndex);
            this.Controls.Add(this.btnSaveNew);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.chkDeleteRow);
            this.Controls.Add(this.chkDeleteColumn);
            this.Controls.Add(this.btnRemoveAllRows);
            this.Controls.Add(this.btnLoadXml);
            this.Controls.Add(this.btnRemoveColumn);
            this.Controls.Add(this.btnRemoveAllColumns);
            this.Controls.Add(this.btnSaveXml);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.txtDataSetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtColumnName);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(720, 532);
            this.Name = "FormXmlTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormXmlTable";
            this.Load += new System.EventHandler(this.FormXmlTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.TextBox txtColumnName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveXml;
        private System.Windows.Forms.Button btnLoadXml;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDataSetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button btnRemoveColumn;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeColumnToolStripMenuItem;
        private System.Windows.Forms.Button btnRemoveAllRows;
        private System.Windows.Forms.Button btnRemoveAllColumns;
        private System.Windows.Forms.CheckBox chkDeleteRow;
        private System.Windows.Forms.CheckBox chkDeleteColumn;
        private System.Windows.Forms.CheckBox chkAddAtIndex;
        private System.Windows.Forms.Button btnClose;
    }
}