namespace Tools
{
    partial class FormConvertTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConvertTable));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radFromVNI = new System.Windows.Forms.RadioButton();
            this.radFromUNI = new System.Windows.Forms.RadioButton();
            this.radFromABC = new System.Windows.Forms.RadioButton();
            this.radFromAuto = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radToVNI = new System.Windows.Forms.RadioButton();
            this.radToUNI = new System.Windows.Forms.RadioButton();
            this.radToABC = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(362, 431);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(410, 119);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(362, 431);
            this.dataGridView2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(353, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Convert";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(697, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Save as";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radFromVNI);
            this.groupBox1.Controls.Add(this.radFromUNI);
            this.groupBox1.Controls.Add(this.radFromABC);
            this.groupBox1.Controls.Add(this.radFromAuto);
            this.groupBox1.Location = new System.Drawing.Point(93, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From";
            // 
            // radFromVNI
            // 
            this.radFromVNI.AutoSize = true;
            this.radFromVNI.Location = new System.Drawing.Point(164, 19);
            this.radFromVNI.Name = "radFromVNI";
            this.radFromVNI.Size = new System.Drawing.Size(43, 17);
            this.radFromVNI.TabIndex = 0;
            this.radFromVNI.Text = "VNI";
            this.radFromVNI.UseVisualStyleBackColor = true;
            // 
            // radFromUNI
            // 
            this.radFromUNI.AutoSize = true;
            this.radFromUNI.Location = new System.Drawing.Point(114, 19);
            this.radFromUNI.Name = "radFromUNI";
            this.radFromUNI.Size = new System.Drawing.Size(44, 17);
            this.radFromUNI.TabIndex = 0;
            this.radFromUNI.Text = "UNI";
            this.radFromUNI.UseVisualStyleBackColor = true;
            // 
            // radFromABC
            // 
            this.radFromABC.AutoSize = true;
            this.radFromABC.Location = new System.Drawing.Point(62, 19);
            this.radFromABC.Name = "radFromABC";
            this.radFromABC.Size = new System.Drawing.Size(46, 17);
            this.radFromABC.TabIndex = 0;
            this.radFromABC.Text = "ABC";
            this.radFromABC.UseVisualStyleBackColor = true;
            // 
            // radFromAuto
            // 
            this.radFromAuto.AutoSize = true;
            this.radFromAuto.Checked = true;
            this.radFromAuto.Location = new System.Drawing.Point(9, 19);
            this.radFromAuto.Name = "radFromAuto";
            this.radFromAuto.Size = new System.Drawing.Size(47, 17);
            this.radFromAuto.TabIndex = 0;
            this.radFromAuto.TabStop = true;
            this.radFromAuto.Text = "Auto";
            this.radFromAuto.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radToVNI);
            this.groupBox2.Controls.Add(this.radToUNI);
            this.groupBox2.Controls.Add(this.radToABC);
            this.groupBox2.Location = new System.Drawing.Point(434, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 50);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To";
            // 
            // radToVNI
            // 
            this.radToVNI.AutoSize = true;
            this.radToVNI.Location = new System.Drawing.Point(164, 19);
            this.radToVNI.Name = "radToVNI";
            this.radToVNI.Size = new System.Drawing.Size(43, 17);
            this.radToVNI.TabIndex = 0;
            this.radToVNI.Text = "VNI";
            this.radToVNI.UseVisualStyleBackColor = true;
            // 
            // radToUNI
            // 
            this.radToUNI.AutoSize = true;
            this.radToUNI.Checked = true;
            this.radToUNI.Location = new System.Drawing.Point(114, 19);
            this.radToUNI.Name = "radToUNI";
            this.radToUNI.Size = new System.Drawing.Size(44, 17);
            this.radToUNI.TabIndex = 0;
            this.radToUNI.TabStop = true;
            this.radToUNI.Text = "UNI";
            this.radToUNI.UseVisualStyleBackColor = true;
            // 
            // radToABC
            // 
            this.radToABC.AutoSize = true;
            this.radToABC.Location = new System.Drawing.Point(62, 19);
            this.radToABC.Name = "radToABC";
            this.radToABC.Size = new System.Drawing.Size(46, 17);
            this.radToABC.TabIndex = 0;
            this.radToABC.Text = "ABC";
            this.radToABC.UseVisualStyleBackColor = true;
            // 
            // FormConvertTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormConvertTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormConvertTable";
            this.Load += new System.EventHandler(this.FormConvertTable_Load);
            this.Resize += new System.EventHandler(this.FormConvertTable_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radFromVNI;
        private System.Windows.Forms.RadioButton radFromUNI;
        private System.Windows.Forms.RadioButton radFromABC;
        private System.Windows.Forms.RadioButton radFromAuto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radToVNI;
        private System.Windows.Forms.RadioButton radToUNI;
        private System.Windows.Forms.RadioButton radToABC;
    }
}