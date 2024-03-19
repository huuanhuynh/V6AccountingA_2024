namespace Tools
{
    partial class FormRepxFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepxFilter));
            this.btnFilter = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtXpath = new System.Windows.Forms.TextBox();
            this.richView = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txt02 = new System.Windows.Forms.TextBox();
            this.txt03 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddPadingRight2 = new System.Windows.Forms.Button();
            this.btnRepxVtoE_All = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.richFoundInfos = new System.Windows.Forms.RichTextBox();
            this.txtAContains = new System.Windows.Forms.TextBox();
            this.lblAChua = new System.Windows.Forms.Label();
            this.txtAttributeName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBNoContains = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAContains2 = new System.Windows.Forms.TextBox();
            this.txtBContains2 = new System.Windows.Forms.TextBox();
            this.txtAContains3 = new System.Windows.Forms.TextBox();
            this.txtBContains3 = new System.Windows.Forms.TextBox();
            this.txtAttOper = new System.Windows.Forms.TextBox();
            this.txtBNoContains2 = new System.Windows.Forms.TextBox();
            this.txtAttValue = new System.Windows.Forms.TextBox();
            this.txtBNoContains3 = new System.Windows.Forms.TextBox();
            this.txtReplaceBy = new System.Windows.Forms.TextBox();
            this.lblListBox1Bottom = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRepxVtoE_1 = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblListBox2Bottom = new System.Windows.Forms.Label();
            this.btnFilterTableCell = new System.Windows.Forms.Button();
            this.chkHasDataRef = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(216, 197);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(139, 23);
            this.btnFilter.TabIndex = 15;
            this.btnFilter.Text = "Lọc right no pading";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "xPath";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(67, 4);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(1023, 20);
            this.txtFolder.TabIndex = 1;
            this.txtFolder.Text = "D:\\Code\\Framework_45\\Winform\\V6AccountingB\\V6AccountingB\\bin\\Release\\ReportsDX\\VN" +
    "\\V";
            this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolder_KeyDown);
            this.txtFolder.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtXpath
            // 
            this.txtXpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXpath.Location = new System.Drawing.Point(284, 27);
            this.txtXpath.Name = "txtXpath";
            this.txtXpath.Size = new System.Drawing.Size(806, 20);
            this.txtXpath.TabIndex = 7;
            this.txtXpath.Text = "//Cells/*[@ControlType=\'XRTableCell\' and (@TextAlignment=\'TopRight\' or @TextAlign" +
    "ment=\'MiddleRight\') and  not(@Padding)]";
            this.toolTip1.SetToolTip(this.txtXpath, "[A]Contents[B]");
            this.txtXpath.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtXpath.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // richView
            // 
            this.richView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richView.Location = new System.Drawing.Point(417, 344);
            this.richView.Name = "richView";
            this.richView.ReadOnly = true;
            this.richView.Size = new System.Drawing.Size(948, 354);
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
            this.btnFolder.Location = new System.Drawing.Point(1096, 2);
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
            this.listBox1.Size = new System.Drawing.Size(195, 615);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // txt02
            // 
            this.txt02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt02.Location = new System.Drawing.Point(284, 107);
            this.txt02.Name = "txt02";
            this.txt02.Size = new System.Drawing.Size(1092, 20);
            this.txt02.TabIndex = 12;
            this.txt02.Text = "Borders=\"Left, Right, Bottom\"   BorderDashStyle=\"Solid\"";
            this.txt02.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txt02.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txt03
            // 
            this.txt03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt03.Location = new System.Drawing.Point(284, 127);
            this.txt03.Name = "txt03";
            this.txt03.Size = new System.Drawing.Size(1092, 20);
            this.txt03.TabIndex = 13;
            this.txt03.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txt03.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(623, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Attribute";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(216, 226);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(195, 420);
            this.listBox2.TabIndex = 16;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // btnAddPadingRight2
            // 
            this.btnAddPadingRight2.Location = new System.Drawing.Point(593, 197);
            this.btnAddPadingRight2.Name = "btnAddPadingRight2";
            this.btnAddPadingRight2.Size = new System.Drawing.Size(146, 23);
            this.btnAddPadingRight2.TabIndex = 18;
            this.btnAddPadingRight2.Text = "Add Pading right 2 (select)";
            this.toolTip1.SetToolTip(this.btnAddPadingRight2, "Thay thế  các file đã lọc.");
            this.btnAddPadingRight2.UseVisualStyleBackColor = true;
            this.btnAddPadingRight2.Click += new System.EventHandler(this.btnAddPadingRight2_Click);
            // 
            // btnRepxVtoE_All
            // 
            this.btnRepxVtoE_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRepxVtoE_All.Location = new System.Drawing.Point(12, 691);
            this.btnRepxVtoE_All.Name = "btnRepxVtoE_All";
            this.btnRepxVtoE_All.Size = new System.Drawing.Size(195, 23);
            this.btnRepxVtoE_All.TabIndex = 21;
            this.btnRepxVtoE_All.Text = "REPX V -> E (All)";
            this.toolTip1.SetToolTip(this.btnRepxVtoE_All, "Thay thế  các file đã lọc.");
            this.btnRepxVtoE_All.UseVisualStyleBackColor = true;
            this.btnRepxVtoE_All.Click += new System.EventHandler(this.btnRepxVtoE_All_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(213, 701);
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
            this.richFoundInfos.Size = new System.Drawing.Size(948, 112);
            this.richFoundInfos.TabIndex = 17;
            this.richFoundInfos.Text = "";
            // 
            // txtAContains
            // 
            this.txtAContains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAContains.Location = new System.Drawing.Point(284, 153);
            this.txtAContains.Name = "txtAContains";
            this.txtAContains.Size = new System.Drawing.Size(159, 20);
            this.txtAContains.TabIndex = 12;
            this.txtAContains.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAContains.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // lblAChua
            // 
            this.lblAChua.AutoSize = true;
            this.lblAChua.Location = new System.Drawing.Point(213, 156);
            this.lblAChua.Name = "lblAChua";
            this.lblAChua.Size = new System.Drawing.Size(47, 13);
            this.lblAChua.TabIndex = 6;
            this.lblAChua.Text = "[A] chứa";
            // 
            // txtAttributeName
            // 
            this.txtAttributeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttributeName.Location = new System.Drawing.Point(694, 55);
            this.txtAttributeName.Name = "txtAttributeName";
            this.txtAttributeName.Size = new System.Drawing.Size(159, 20);
            this.txtAttributeName.TabIndex = 4;
            this.txtAttributeName.Text = "Borders";
            this.txtAttributeName.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAttributeName.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(782, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "[A] ko chứa";
            // 
            // txtBNoContains
            // 
            this.txtBNoContains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBNoContains.Location = new System.Drawing.Point(850, 175);
            this.txtBNoContains.Name = "txtBNoContains";
            this.txtBNoContains.Size = new System.Drawing.Size(159, 20);
            this.txtBNoContains.TabIndex = 4;
            this.txtBNoContains.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtBNoContains.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(782, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "[B] ko chứa";
            // 
            // txtAContains2
            // 
            this.txtAContains2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAContains2.Location = new System.Drawing.Point(449, 153);
            this.txtAContains2.Name = "txtAContains2";
            this.txtAContains2.Size = new System.Drawing.Size(159, 20);
            this.txtAContains2.TabIndex = 12;
            this.txtAContains2.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAContains2.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtBContains2
            // 
            this.txtBContains2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBContains2.Location = new System.Drawing.Point(449, 175);
            this.txtBContains2.Name = "txtBContains2";
            this.txtBContains2.Size = new System.Drawing.Size(159, 20);
            this.txtBContains2.TabIndex = 12;
            this.txtBContains2.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtBContains2.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtAContains3
            // 
            this.txtAContains3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAContains3.Location = new System.Drawing.Point(614, 153);
            this.txtAContains3.Name = "txtAContains3";
            this.txtAContains3.Size = new System.Drawing.Size(159, 20);
            this.txtAContains3.TabIndex = 12;
            this.txtAContains3.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAContains3.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtBContains3
            // 
            this.txtBContains3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBContains3.Location = new System.Drawing.Point(614, 175);
            this.txtBContains3.Name = "txtBContains3";
            this.txtBContains3.Size = new System.Drawing.Size(159, 20);
            this.txtBContains3.TabIndex = 12;
            this.txtBContains3.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtBContains3.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtAttOper
            // 
            this.txtAttOper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttOper.Location = new System.Drawing.Point(859, 55);
            this.txtAttOper.Name = "txtAttOper";
            this.txtAttOper.Size = new System.Drawing.Size(159, 20);
            this.txtAttOper.TabIndex = 4;
            this.txtAttOper.Text = "<>";
            this.txtAttOper.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAttOper.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtBNoContains2
            // 
            this.txtBNoContains2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBNoContains2.Location = new System.Drawing.Point(1015, 175);
            this.txtBNoContains2.Name = "txtBNoContains2";
            this.txtBNoContains2.Size = new System.Drawing.Size(159, 20);
            this.txtBNoContains2.TabIndex = 4;
            this.txtBNoContains2.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtBNoContains2.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtAttValue
            // 
            this.txtAttValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttValue.Location = new System.Drawing.Point(1024, 55);
            this.txtAttValue.Name = "txtAttValue";
            this.txtAttValue.Size = new System.Drawing.Size(159, 20);
            this.txtAttValue.TabIndex = 4;
            this.txtAttValue.Text = "Left, Right, Bottom";
            this.txtAttValue.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtAttValue.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtBNoContains3
            // 
            this.txtBNoContains3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBNoContains3.Location = new System.Drawing.Point(1180, 175);
            this.txtBNoContains3.Name = "txtBNoContains3";
            this.txtBNoContains3.Size = new System.Drawing.Size(159, 20);
            this.txtBNoContains3.TabIndex = 4;
            this.txtBNoContains3.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtBNoContains3.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // txtReplaceBy
            // 
            this.txtReplaceBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplaceBy.Location = new System.Drawing.Point(850, 203);
            this.txtReplaceBy.Name = "txtReplaceBy";
            this.txtReplaceBy.Size = new System.Drawing.Size(324, 20);
            this.txtReplaceBy.TabIndex = 20;
            // 
            // lblListBox1Bottom
            // 
            this.lblListBox1Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListBox1Bottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblListBox1Bottom.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblListBox1Bottom.Location = new System.Drawing.Point(12, 646);
            this.lblListBox1Bottom.Name = "lblListBox1Bottom";
            this.lblListBox1Bottom.Size = new System.Drawing.Size(195, 13);
            this.lblListBox1Bottom.TabIndex = 22;
            this.lblListBox1Bottom.Text = "Count: 0";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnRepxVtoE_1
            // 
            this.btnRepxVtoE_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRepxVtoE_1.Location = new System.Drawing.Point(12, 662);
            this.btnRepxVtoE_1.Name = "btnRepxVtoE_1";
            this.btnRepxVtoE_1.Size = new System.Drawing.Size(195, 23);
            this.btnRepxVtoE_1.TabIndex = 21;
            this.btnRepxVtoE_1.Text = "REPX V -> E (1)";
            this.btnRepxVtoE_1.UseVisualStyleBackColor = true;
            this.btnRepxVtoE_1.Click += new System.EventHandler(this.btnRepxVtoE_1_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(1180, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(745, 197);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save (select)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Location = new System.Drawing.Point(370, 197);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(187, 23);
            this.btnApplyAll.TabIndex = 18;
            this.btnApplyAll.Text = "Add Pading Save All (tablecell)";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "TableCell Align Right no padding",
            "Table2 no border LRB",
            "footer_label tổng cộng",
            "Khác"});
            this.comboBox1.Location = new System.Drawing.Point(1096, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 21);
            this.comboBox1.TabIndex = 23;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblListBox2Bottom
            // 
            this.lblListBox2Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListBox2Bottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblListBox2Bottom.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblListBox2Bottom.Location = new System.Drawing.Point(216, 646);
            this.lblListBox2Bottom.Name = "lblListBox2Bottom";
            this.lblListBox2Bottom.Size = new System.Drawing.Size(195, 13);
            this.lblListBox2Bottom.TabIndex = 22;
            this.lblListBox2Bottom.Text = "Count: 0";
            // 
            // btnFilterTableCell
            // 
            this.btnFilterTableCell.Location = new System.Drawing.Point(294, 53);
            this.btnFilterTableCell.Name = "btnFilterTableCell";
            this.btnFilterTableCell.Size = new System.Drawing.Size(217, 23);
            this.btnFilterTableCell.TabIndex = 15;
            this.btnFilterTableCell.Text = "Lọc (xPath)";
            this.btnFilterTableCell.UseVisualStyleBackColor = true;
            this.btnFilterTableCell.Click += new System.EventHandler(this.btnFilterTableCell_Click);
            // 
            // chkHasDataRef
            // 
            this.chkHasDataRef.AutoSize = true;
            this.chkHasDataRef.Checked = true;
            this.chkHasDataRef.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHasDataRef.Location = new System.Drawing.Point(528, 57);
            this.chkHasDataRef.Name = "chkHasDataRef";
            this.chkHasDataRef.Size = new System.Drawing.Size(84, 17);
            this.chkHasDataRef.TabIndex = 24;
            this.chkHasDataRef.Text = "Has data ref";
            this.chkHasDataRef.UseVisualStyleBackColor = true;
            // 
            // FormRepxFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 717);
            this.Controls.Add(this.chkHasDataRef);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblListBox2Bottom);
            this.Controls.Add(this.lblListBox1Bottom);
            this.Controls.Add(this.btnRepxVtoE_1);
            this.Controls.Add(this.btnRepxVtoE_All);
            this.Controls.Add(this.txtReplaceBy);
            this.Controls.Add(this.btnApplyAll);
            this.Controls.Add(this.btnAddPadingRight2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.richFoundInfos);
            this.Controls.Add(this.richView);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAChua);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFilterTableCell);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txt03);
            this.Controls.Add(this.txtBContains3);
            this.Controls.Add(this.txtBContains2);
            this.Controls.Add(this.txtAContains3);
            this.Controls.Add(this.txtAContains2);
            this.Controls.Add(this.txtAContains);
            this.Controls.Add(this.txt02);
            this.Controls.Add(this.txtXpath);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.txtBNoContains3);
            this.Controls.Add(this.txtBNoContains2);
            this.Controls.Add(this.txtBNoContains);
            this.Controls.Add(this.txtAttValue);
            this.Controls.Add(this.txtAttOper);
            this.Controls.Add(this.txtAttributeName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRepxFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRepxFilter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRepxFilter_FormClosing);
            this.Load += new System.EventHandler(this.FormRepxFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtXpath;
        private System.Windows.Forms.RichTextBox richView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txt02;
        private System.Windows.Forms.TextBox txt03;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.RichTextBox richFoundInfos;
        private System.Windows.Forms.TextBox txtAContains;
        private System.Windows.Forms.Label lblAChua;
        private System.Windows.Forms.TextBox txtAttributeName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBNoContains;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAContains2;
        private System.Windows.Forms.TextBox txtBContains2;
        private System.Windows.Forms.TextBox txtAContains3;
        private System.Windows.Forms.TextBox txtBContains3;
        private System.Windows.Forms.TextBox txtAttOper;
        private System.Windows.Forms.TextBox txtBNoContains2;
        private System.Windows.Forms.TextBox txtAttValue;
        private System.Windows.Forms.TextBox txtBNoContains3;
        private System.Windows.Forms.Button btnAddPadingRight2;
        private System.Windows.Forms.TextBox txtReplaceBy;
        private System.Windows.Forms.Button btnRepxVtoE_All;
        private System.Windows.Forms.Label lblListBox1Bottom;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRepxVtoE_1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblListBox2Bottom;
        private System.Windows.Forms.Button btnFilterTableCell;
        private System.Windows.Forms.CheckBox chkHasDataRef;
    }
}