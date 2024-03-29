﻿namespace V6Controls.Forms.Editor
{
    partial class ControlsPropertiesEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlsPropertiesEditorForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnNhapXml = new System.Windows.Forms.Button();
            this.btnXuatXml = new System.Windows.Forms.Button();
            this.lblControlType = new System.Windows.Forms.Label();
            this.lblControlName = new System.Windows.Forms.Label();
            this.btnDefaultData = new System.Windows.Forms.Button();
            this.btnEditCorplan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 43);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(285, 503);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Control_433.png");
            this.imageList1.Images.SetKeyName(1, "Button_668.png");
            this.imageList1.Images.SetKeyName(2, "CheckBox_669.png");
            this.imageList1.Images.SetKeyName(3, "DateOrTimePicker_675.png");
            this.imageList1.Images.SetKeyName(4, "GroupBox_680.png");
            this.imageList1.Images.SetKeyName(5, "NumericUpOrDown_691.png");
            this.imageList1.Images.SetKeyName(6, "Label_684.png");
            this.imageList1.Images.SetKeyName(7, "ListBox_686.png");
            this.imageList1.Images.SetKeyName(8, "RadioButton_701.png");
            this.imageList1.Images.SetKeyName(9, "RichTextBox_702.png");
            this.imageList1.Images.SetKeyName(10, "TabControl_707.png");
            this.imageList1.Images.SetKeyName(11, "TextBox_708.png");
            this.imageList1.Images.SetKeyName(12, "ComboBox_672.png");
            this.imageList1.Images.SetKeyName(13, "DataGrid_674.png");
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(303, 43);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(258, 503);
            this.propertyGrid1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(567, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 468);
            this.panel1.TabIndex = 7;
            this.panel1.TabStop = false;
            this.panel1.Text = "Nội dung hiển thị (theo ngôn ngữ)";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(10, 52);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(324, 413);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên";
            this.columnHeader1.Width = 119;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Giá trị";
            this.columnHeader2.Width = 200;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = ".";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(231, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(33, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "V";
            this.toolTipV6FormControl.SetToolTip(this.checkBox1, "Áp dụng thay đổi ngay cả ngôn ngữ Tiếng Việt");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = ".";
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(273, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(64, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(161, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = ".";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "N.dung";
            // 
            // btnUp
            // 
            this.btnUp.AccessibleDescription = ".";
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::V6Controls.Properties.Resources.Up24;
            this.btnUp.Location = new System.Drawing.Point(567, 2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(74, 35);
            this.btnUp.TabIndex = 10;
            this.btnUp.Text = "Up";
            this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolTipV6FormControl.SetToolTip(this.btnUp, "Hiển thị đối tượng cha (control.Parent).");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnNhapXml
            // 
            this.btnNhapXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapXml.Location = new System.Drawing.Point(827, 5);
            this.btnNhapXml.Name = "btnNhapXml";
            this.btnNhapXml.Size = new System.Drawing.Size(84, 29);
            this.btnNhapXml.TabIndex = 12;
            this.btnNhapXml.Text = "&Nhập xml data";
            this.btnNhapXml.UseVisualStyleBackColor = true;
            this.btnNhapXml.Click += new System.EventHandler(this.btnNhapXml_Click);
            // 
            // btnXuatXml
            // 
            this.btnXuatXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatXml.Location = new System.Drawing.Point(737, 5);
            this.btnXuatXml.Name = "btnXuatXml";
            this.btnXuatXml.Size = new System.Drawing.Size(84, 29);
            this.btnXuatXml.TabIndex = 11;
            this.btnXuatXml.Text = "&Xuất xml data";
            this.btnXuatXml.UseVisualStyleBackColor = true;
            this.btnXuatXml.Click += new System.EventHandler(this.btnXuatXml_Click);
            // 
            // lblControlType
            // 
            this.lblControlType.AccessibleDescription = ".";
            this.lblControlType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlType.Location = new System.Drawing.Point(303, 2);
            this.lblControlType.Name = "lblControlType";
            this.lblControlType.Size = new System.Drawing.Size(258, 38);
            this.lblControlType.TabIndex = 13;
            this.lblControlType.Text = "ControlType";
            // 
            // lblControlName
            // 
            this.lblControlName.AccessibleDescription = ".";
            this.lblControlName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlName.Location = new System.Drawing.Point(12, 2);
            this.lblControlName.Name = "lblControlName";
            this.lblControlName.Size = new System.Drawing.Size(285, 38);
            this.lblControlName.TabIndex = 14;
            this.lblControlName.Text = "ControlName";
            // 
            // btnDefaultData
            // 
            this.btnDefaultData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultData.Location = new System.Drawing.Point(567, 517);
            this.btnDefaultData.Name = "btnDefaultData";
            this.btnDefaultData.Size = new System.Drawing.Size(128, 29);
            this.btnDefaultData.TabIndex = 11;
            this.btnDefaultData.Text = "Giá trị dữ liệu mặc định";
            this.toolTipV6FormControl.SetToolTip(this.btnDefaultData, "Sửa hoặc thêm giá trị mặc định (hoặc tag) cho đối tượng.");
            this.btnDefaultData.UseVisualStyleBackColor = true;
            this.btnDefaultData.Click += new System.EventHandler(this.btnDefaultData_Click);
            // 
            // btnEditCorplan
            // 
            this.btnEditCorplan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCorplan.Location = new System.Drawing.Point(647, 5);
            this.btnEditCorplan.Name = "btnEditCorplan";
            this.btnEditCorplan.Size = new System.Drawing.Size(84, 29);
            this.btnEditCorplan.TabIndex = 11;
            this.btnEditCorplan.Text = "Sửa &ngôn ngữ";
            this.btnEditCorplan.UseVisualStyleBackColor = true;
            this.btnEditCorplan.Click += new System.EventHandler(this.btnEditCorplan_Click);
            // 
            // ControlsPropertiesEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 558);
            this.Controls.Add(this.lblControlName);
            this.Controls.Add(this.lblControlType);
            this.Controls.Add(this.btnNhapXml);
            this.Controls.Add(this.btnDefaultData);
            this.Controls.Add(this.btnEditCorplan);
            this.Controls.Add(this.btnXuatXml);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.treeView1);
            this.Name = "ControlsPropertiesEditorForm";
            this.Text = "ControlsPropertiesEditorForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnNhapXml;
        private System.Windows.Forms.Button btnXuatXml;
        private System.Windows.Forms.Label lblControlType;
        private System.Windows.Forms.Label lblControlName;
        private System.Windows.Forms.Button btnDefaultData;
        private System.Windows.Forms.Button btnEditCorplan;
    }
}