namespace Test
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnYesNo = new System.Windows.Forms.Button();
            this.btnYesNoCancel = new System.Windows.Forms.Button();
            this.btnOkCancel = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateTab = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.v6NumberTextBox1 = new V6Controls.V6CurrencyTextBox();
            this.V6TabControl1 = new V6TabControlLib.V6TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.vistaButton1 = new V6Controls.VistaButton();
            this.v6TabControl2 = new V6TabControlLib.V6TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.v6CurrencyTextBox2 = new V6Controls.V6CurrencyTextBox();
            this.v6ColorDataGridView1 = new V6Controls.V6ColorDataGridView();
            this.v6ColorTextBox1 = new V6Controls.V6ColorTextBox();
            this.v6HiddenButton1 = new V6Controls.V6HiddenButton();
            this.v6VvarTextBox1 = new V6Controls.V6VvarTextBox();
            this.v6CurrencyTextBox1 = new V6Controls.V6CurrencyTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.V6TabControl1.SuspendLayout();
            this.v6TabControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.v6ColorDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(9, 492);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnYesNo
            // 
            this.btnYesNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYesNo.Location = new System.Drawing.Point(90, 492);
            this.btnYesNo.Name = "btnYesNo";
            this.btnYesNo.Size = new System.Drawing.Size(75, 23);
            this.btnYesNo.TabIndex = 2;
            this.btnYesNo.Text = "Yes/No";
            this.btnYesNo.UseVisualStyleBackColor = true;
            this.btnYesNo.Click += new System.EventHandler(this.btnYesNo_Click);
            // 
            // btnYesNoCancel
            // 
            this.btnYesNoCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYesNoCancel.Location = new System.Drawing.Point(171, 492);
            this.btnYesNoCancel.Name = "btnYesNoCancel";
            this.btnYesNoCancel.Size = new System.Drawing.Size(94, 23);
            this.btnYesNoCancel.TabIndex = 2;
            this.btnYesNoCancel.Text = "Yes/No/Cancel";
            this.btnYesNoCancel.UseVisualStyleBackColor = true;
            this.btnYesNoCancel.Click += new System.EventHandler(this.btnYesNoCancel_Click);
            // 
            // btnOkCancel
            // 
            this.btnOkCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOkCancel.Location = new System.Drawing.Point(271, 492);
            this.btnOkCancel.Name = "btnOkCancel";
            this.btnOkCancel.Size = new System.Drawing.Size(75, 23);
            this.btnOkCancel.TabIndex = 2;
            this.btnOkCancel.Text = "Ok/Cancel";
            this.btnOkCancel.UseVisualStyleBackColor = true;
            this.btnOkCancel.Click += new System.EventHandler(this.btnOkCancel_Click);
            // 
            // btnError
            // 
            this.btnError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnError.Location = new System.Drawing.Point(352, 492);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(75, 23);
            this.btnError.TabIndex = 2;
            this.btnError.Text = "Error";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Location = new System.Drawing.Point(433, 492);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "V6CurrencyTextBox";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "V6VvarTextBox";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "V6ColorTextBox";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 26);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.closeTabToolStripMenuItem.Text = "Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // btnCreateTab
            // 
            this.btnCreateTab.Location = new System.Drawing.Point(190, 221);
            this.btnCreateTab.Name = "btnCreateTab";
            this.btnCreateTab.Size = new System.Drawing.Size(75, 23);
            this.btnCreateTab.TabIndex = 12;
            this.btnCreateTab.Text = "button1";
            this.btnCreateTab.UseVisualStyleBackColor = true;
            this.btnCreateTab.Click += new System.EventHandler(this.btnCreateTab_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "V6currencyTextBox2";
            // 
            // v6NumberTextBox1
            // 
            this.v6NumberTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6NumberTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox1.HoverColor = System.Drawing.Color.White;
            this.v6NumberTextBox1.LeaveColor = System.Drawing.Color.LightGray;
            this.v6NumberTextBox1.Location = new System.Drawing.Point(274, 66);
            this.v6NumberTextBox1.Name = "v6NumberTextBox1";
            this.v6NumberTextBox1.Size = new System.Drawing.Size(406, 20);
            this.v6NumberTextBox1.StringValue = "0";
            this.v6NumberTextBox1.TabIndex = 15;
            this.v6NumberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // V6TabControl1
            // 
            this.V6TabControl1.AllowInActiveAddButton = true;
            this.V6TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.V6TabControl1.Controls.Add(this.tabPage1);
            this.V6TabControl1.Controls.Add(this.tabPage2);
            this.V6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.V6TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.V6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.V6TabControl1.Location = new System.Drawing.Point(12, 242);
            this.V6TabControl1.Name = "V6TabControl1";
            this.V6TabControl1.SelectedIndex = 0;
            this.V6TabControl1.Size = new System.Drawing.Size(223, 244);
            this.V6TabControl1.TabIndex = 13;
            this.V6TabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(215, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(215, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // vistaButton1
            // 
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "Vista button";
            this.vistaButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vistaButton1.Location = new System.Drawing.Point(465, 12);
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(172, 32);
            this.vistaButton1.TabIndex = 16;
            this.vistaButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // v6TabControl2
            // 
            this.v6TabControl2.AllowCloseButton = false;
            this.v6TabControl2.AllowInActiveAddButton = true;
            this.v6TabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6TabControl2.Controls.Add(this.tabPage3);
            this.v6TabControl2.Controls.Add(this.tabPage4);
            this.v6TabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.v6TabControl2.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl2.Location = new System.Drawing.Point(241, 242);
            this.v6TabControl2.Name = "v6TabControl2";
            this.v6TabControl2.SelectedIndex = 0;
            this.v6TabControl2.Size = new System.Drawing.Size(439, 244);
            this.v6TabControl2.TabIndex = 13;
            this.v6TabControl2.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(431, 212);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage1";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(431, 212);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // v6CurrencyTextBox2
            // 
            this.v6CurrencyTextBox2.BackColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6CurrencyTextBox2.HoverColor = System.Drawing.Color.White;
            this.v6CurrencyTextBox2.LeaveColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox2.Location = new System.Drawing.Point(0, 0);
            this.v6CurrencyTextBox2.Name = "v6CurrencyTextBox2";
            this.v6CurrencyTextBox2.Size = new System.Drawing.Size(100, 20);
            this.v6CurrencyTextBox2.StringValue = "0";
            this.v6CurrencyTextBox2.TabIndex = 17;
            this.v6CurrencyTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // v6ColorDataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.v6ColorDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.v6ColorDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.v6ColorDataGridView1.Location = new System.Drawing.Point(8, 149);
            this.v6ColorDataGridView1.Name = "v6ColorDataGridView1";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightYellow;
            this.v6ColorDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.v6ColorDataGridView1.Size = new System.Drawing.Size(668, 60);
            this.v6ColorDataGridView1.TabIndex = 10;
            // 
            // v6ColorTextBox1
            // 
            this.v6ColorTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6ColorTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox1.HoverColor = System.Drawing.Color.White;
            this.v6ColorTextBox1.LeaveColor = System.Drawing.Color.LightGray;
            this.v6ColorTextBox1.Location = new System.Drawing.Point(148, 66);
            this.v6ColorTextBox1.Name = "v6ColorTextBox1";
            this.v6ColorTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6ColorTextBox1.TabIndex = 7;
            // 
            // v6HiddenButton1
            // 
            this.v6HiddenButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.v6HiddenButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.v6HiddenButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.v6HiddenButton1.Location = new System.Drawing.Point(514, 492);
            this.v6HiddenButton1.Name = "v6HiddenButton1";
            this.v6HiddenButton1.Size = new System.Drawing.Size(111, 23);
            this.v6HiddenButton1.TabIndex = 6;
            this.v6HiddenButton1.Text = "v6HiddenButton1";
            this.v6HiddenButton1.UseVisualStyleBackColor = true;
            this.v6HiddenButton1.Click += new System.EventHandler(this.v6HiddenButton1_Click);
            // 
            // v6VvarTextBox1
            // 
            this.v6VvarTextBox1.ConString = "Data Source=THANHLV;Initial Catalog=VanPhong_KD;User ID=sa;Password=v6soft";
            this.v6VvarTextBox1.Location = new System.Drawing.Point(148, 39);
            this.v6VvarTextBox1.Name = "v6VvarTextBox1";
            this.v6VvarTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6VvarTextBox1.TabIndex = 1;
            this.v6VvarTextBox1.VVar = null;
            // 
            // v6CurrencyTextBox1
            // 
            this.v6CurrencyTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6CurrencyTextBox1.HoverColor = System.Drawing.Color.White;
            this.v6CurrencyTextBox1.LeaveColor = System.Drawing.Color.LightGray;
            this.v6CurrencyTextBox1.Location = new System.Drawing.Point(0, 0);
            this.v6CurrencyTextBox1.Name = "v6CurrencyTextBox1";
            this.v6CurrencyTextBox1.Size = new System.Drawing.Size(100, 20);
            this.v6CurrencyTextBox1.StringValue = "0";
            this.v6CurrencyTextBox1.TabIndex = 18;
            this.v6CurrencyTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 527);
            this.Controls.Add(this.v6NumberTextBox1);
            this.Controls.Add(this.V6TabControl1);
            this.Controls.Add(this.vistaButton1);
            this.Controls.Add(this.v6TabControl2);
            this.Controls.Add(this.v6CurrencyTextBox2);
            this.Controls.Add(this.v6ColorDataGridView1);
            this.Controls.Add(this.btnCreateTab);
            this.Controls.Add(this.v6ColorTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.v6HiddenButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.v6VvarTextBox1);
            this.Controls.Add(this.btnOkCancel);
            this.Controls.Add(this.btnYesNoCancel);
            this.Controls.Add(this.btnYesNo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.v6CurrencyTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.V6TabControl1.ResumeLayout(false);
            this.v6TabControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.v6ColorDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6CurrencyTextBox v6CurrencyTextBox1;
        private V6Controls.V6VvarTextBox v6VvarTextBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnYesNo;
        private System.Windows.Forms.Button btnYesNoCancel;
        private System.Windows.Forms.Button btnOkCancel;
        private System.Windows.Forms.Button btnError;
        private System.Windows.Forms.Button btnTest;        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6HiddenButton v6HiddenButton1;
        private V6Controls.V6ColorTextBox v6ColorTextBox1;
        private V6Controls.V6CurrencyTextBox v6CurrencyTextBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private V6Controls.V6ColorDataGridView v6ColorDataGridView1;

        private System.Windows.Forms.Button btnCreateTab;
        private V6TabControlLib.V6TabControl V6TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private V6TabControlLib.V6TabControl v6TabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private V6Controls.V6CurrencyTextBox v6NumberTextBox1;
        private V6Controls.VistaButton vistaButton1;
        
    }
}

