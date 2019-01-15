namespace Tools
{
    partial class FormFilterTextFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilterTextFiles));
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txt11 = new System.Windows.Forms.TextBox();
            this.richView = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txt12 = new System.Windows.Forms.TextBox();
            this.txt13 = new System.Windows.Forms.TextBox();
            this.txt01 = new System.Windows.Forms.TextBox();
            this.txt02 = new System.Windows.Forms.TextBox();
            this.txt03 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.chkCase = new System.Windows.Forms.CheckBox();
            this.chkx211 = new System.Windows.Forms.CheckBox();
            this.chkx212 = new System.Windows.Forms.CheckBox();
            this.chkx213 = new System.Windows.Forms.CheckBox();
            this.chkSubFolder = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblFilePath = new System.Windows.Forms.Label();
            this.richFoundInfos = new System.Windows.Forms.RichTextBox();
            this.txtAContains = new System.Windows.Forms.TextBox();
            this.txtBContains = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(216, 197);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 15;
            this.btnSaveFile.Text = "Lọc";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Có chứa";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(67, 4);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(516, 20);
            this.txtFolder.TabIndex = 1;
            this.txtFolder.Text = "E:\\Git\\MicrosoftSource\\Framework35\\Winform";
            this.txtFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolder_KeyDown);
            // 
            // txt11
            // 
            this.txt11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt11.Location = new System.Drawing.Point(284, 27);
            this.txt11.Name = "txt11";
            this.txt11.Size = new System.Drawing.Size(539, 20);
            this.txt11.TabIndex = 7;
            this.txt11.Text = "[A].Text = \"B";
            this.toolTip1.SetToolTip(this.txt11, "[A]Contents[B]");
            // 
            // richView
            // 
            this.richView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richView.Location = new System.Drawing.Point(417, 289);
            this.richView.Name = "richView";
            this.richView.ReadOnly = true;
            this.richView.Size = new System.Drawing.Size(441, 370);
            this.richView.TabIndex = 17;
            this.richView.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thư mục";
            // 
            // btnFolder
            // 
            this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolder.Location = new System.Drawing.Point(589, 2);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 2;
            this.btnFolder.Text = "Folder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(195, 641);
            this.listBox1.TabIndex = 5;
            // 
            // txt12
            // 
            this.txt12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt12.Location = new System.Drawing.Point(284, 47);
            this.txt12.Name = "txt12";
            this.txt12.Size = new System.Drawing.Size(539, 20);
            this.txt12.TabIndex = 8;
            // 
            // txt13
            // 
            this.txt13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt13.Location = new System.Drawing.Point(284, 67);
            this.txt13.Name = "txt13";
            this.txt13.Size = new System.Drawing.Size(539, 20);
            this.txt13.TabIndex = 9;
            // 
            // txt01
            // 
            this.txt01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt01.Location = new System.Drawing.Point(284, 87);
            this.txt01.Name = "txt01";
            this.txt01.Size = new System.Drawing.Size(585, 20);
            this.txt01.TabIndex = 11;
            this.txt01.Text = "[A].AccessibleD";
            this.toolTip1.SetToolTip(this.txt01, "[A]Contents[B]");
            // 
            // txt02
            // 
            this.txt02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt02.Location = new System.Drawing.Point(284, 107);
            this.txt02.Name = "txt02";
            this.txt02.Size = new System.Drawing.Size(585, 20);
            this.txt02.TabIndex = 12;
            this.txt02.Text = "namespace V6Tools";
            // 
            // txt03
            // 
            this.txt03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt03.Location = new System.Drawing.Point(284, 127);
            this.txt03.Name = "txt03";
            this.txt03.Size = new System.Drawing.Size(585, 20);
            this.txt03.TabIndex = 13;
            this.txt03.Text = "H_Controls";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Không chứa";
            // 
            // txtExt
            // 
            this.txtExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExt.Location = new System.Drawing.Point(670, 4);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(120, 20);
            this.txtExt.TabIndex = 4;
            this.txtExt.Text = "cs";
            this.txtExt.Leave += new System.EventHandler(this.txtExt_Leave);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(216, 226);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(195, 433);
            this.listBox2.TabIndex = 16;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // chkCase
            // 
            this.chkCase.AutoSize = true;
            this.chkCase.Location = new System.Drawing.Point(315, 201);
            this.chkCase.Name = "chkCase";
            this.chkCase.Size = new System.Drawing.Size(128, 17);
            this.chkCase.TabIndex = 14;
            this.chkCase.Text = "Phân biệt hoa thường";
            this.chkCase.UseVisualStyleBackColor = true;
            // 
            // chkx211
            // 
            this.chkx211.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkx211.AutoSize = true;
            this.chkx211.Location = new System.Drawing.Point(829, 30);
            this.chkx211.Name = "chkx211";
            this.chkx211.Size = new System.Drawing.Size(37, 17);
            this.chkx211.TabIndex = 14;
            this.chkx211.Text = "x2";
            this.chkx211.UseVisualStyleBackColor = true;
            // 
            // chkx212
            // 
            this.chkx212.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkx212.AutoSize = true;
            this.chkx212.Location = new System.Drawing.Point(829, 49);
            this.chkx212.Name = "chkx212";
            this.chkx212.Size = new System.Drawing.Size(37, 17);
            this.chkx212.TabIndex = 14;
            this.chkx212.Text = "x2";
            this.chkx212.UseVisualStyleBackColor = true;
            // 
            // chkx213
            // 
            this.chkx213.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkx213.AutoSize = true;
            this.chkx213.Location = new System.Drawing.Point(829, 69);
            this.chkx213.Name = "chkx213";
            this.chkx213.Size = new System.Drawing.Size(37, 17);
            this.chkx213.TabIndex = 14;
            this.chkx213.Text = "x2";
            this.chkx213.UseVisualStyleBackColor = true;
            // 
            // chkSubFolder
            // 
            this.chkSubFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSubFolder.AutoSize = true;
            this.chkSubFolder.Checked = true;
            this.chkSubFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSubFolder.Location = new System.Drawing.Point(796, 5);
            this.chkSubFolder.Name = "chkSubFolder";
            this.chkSubFolder.Size = new System.Drawing.Size(72, 17);
            this.chkSubFolder.TabIndex = 14;
            this.chkSubFolder.Text = "sub folder";
            this.chkSubFolder.UseVisualStyleBackColor = true;
            // 
            // lblFilePath
            // 
            this.lblFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(213, 662);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(23, 13);
            this.lblFilePath.TabIndex = 10;
            this.lblFilePath.Text = "File";
            // 
            // richFoundInfos
            // 
            this.richFoundInfos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richFoundInfos.Location = new System.Drawing.Point(417, 226);
            this.richFoundInfos.Name = "richFoundInfos";
            this.richFoundInfos.ReadOnly = true;
            this.richFoundInfos.Size = new System.Drawing.Size(441, 57);
            this.richFoundInfos.TabIndex = 17;
            this.richFoundInfos.Text = "";
            // 
            // txtAContains
            // 
            this.txtAContains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAContains.Location = new System.Drawing.Point(284, 153);
            this.txtAContains.Name = "txtAContains";
            this.txtAContains.Size = new System.Drawing.Size(585, 20);
            this.txtAContains.TabIndex = 12;
            // 
            // txtBContains
            // 
            this.txtBContains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBContains.Location = new System.Drawing.Point(284, 175);
            this.txtBContains.Name = "txtBContains";
            this.txtBContains.Size = new System.Drawing.Size(585, 20);
            this.txtBContains.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "[A] chứa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "[B] chứa";
            // 
            // FormFilterTextFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 678);
            this.Controls.Add(this.chkx213);
            this.Controls.Add(this.chkx212);
            this.Controls.Add(this.chkx211);
            this.Controls.Add(this.chkSubFolder);
            this.Controls.Add(this.chkCase);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.richFoundInfos);
            this.Controls.Add(this.richView);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.txt03);
            this.Controls.Add(this.txt13);
            this.Controls.Add(this.txtBContains);
            this.Controls.Add(this.txtAContains);
            this.Controls.Add(this.txt02);
            this.Controls.Add(this.txt12);
            this.Controls.Add(this.txt01);
            this.Controls.Add(this.txt11);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.txtExt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFilterTextFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormFilterTextFiles";
            this.Load += new System.EventHandler(this.FormFilterTextFiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txt11;
        private System.Windows.Forms.RichTextBox richView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txt12;
        private System.Windows.Forms.TextBox txt13;
        private System.Windows.Forms.TextBox txt01;
        private System.Windows.Forms.TextBox txt02;
        private System.Windows.Forms.TextBox txt03;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExt;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckBox chkCase;
        private System.Windows.Forms.CheckBox chkx211;
        private System.Windows.Forms.CheckBox chkx212;
        private System.Windows.Forms.CheckBox chkx213;
        private System.Windows.Forms.CheckBox chkSubFolder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.RichTextBox richFoundInfos;
        private System.Windows.Forms.TextBox txtAContains;
        private System.Windows.Forms.TextBox txtBContains;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}