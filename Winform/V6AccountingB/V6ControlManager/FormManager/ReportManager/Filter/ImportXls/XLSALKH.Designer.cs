namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class XLSALKH
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.btnTim = new System.Windows.Forms.Button();
            this.v6Label3 = new V6Controls.V6Label();
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new V6Controls.V6CheckBox();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.comboBox2 = new V6Controls.V6ComboBox();
            this.txtFile = new System.Windows.Forms.RichTextBox();
            this.btnXemMauExcel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00199";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "File\r\nexcel";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00205";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(5, 77);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(48, 13);
            this.v6Label2.TabIndex = 4;
            this.v6Label2.Text = "Mã đích";
            // 
            // btnTim
            // 
            this.btnTim.AccessibleDescription = "FILTERB00005";
            this.btnTim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTim.Image = global::V6ControlManager.Properties.Resources.Excel16;
            this.btnTim.Location = new System.Drawing.Point(37, 86);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(142, 29);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm file Excel";
            this.btnTim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00204";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(6, 50);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(55, 13);
            this.v6Label3.TabIndex = 2;
            this.v6Label3.Text = "Mã nguồn";
            // 
            // comboBox1
            // 
            this.comboBox1.AccessibleName = "kieu_post";
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "TCVN3 (ABC)",
            "VNI"});
            this.comboBox1.Location = new System.Drawing.Point(98, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(154, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00005";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.v6Label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 104);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // checkBox2
            // 
            this.checkBox2.AccessibleDescription = "FILTERC00017";
            this.checkBox2.AccessibleName = "CAP_NHAT_MA_MOI";
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(94, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(125, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Chỉ cập nhập mã mới";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "FILTERC00016";
            this.checkBox1.AccessibleName = "CHUYEN_MA";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Chuyễn mã";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.AccessibleName = "kieu_post";
            this.comboBox2.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "UNICODE"});
            this.comboBox2.Location = new System.Drawing.Point(98, 74);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(154, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(37, 5);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(235, 75);
            this.txtFile.TabIndex = 1;
            this.txtFile.Text = "";
            // 
            // btnXemMauExcel
            // 
            this.btnXemMauExcel.AccessibleDescription = "FILTERB00013";
            this.btnXemMauExcel.Location = new System.Drawing.Point(3, 226);
            this.btnXemMauExcel.Name = "btnXemMauExcel";
            this.btnXemMauExcel.Size = new System.Drawing.Size(88, 29);
            this.btnXemMauExcel.TabIndex = 11;
            this.btnXemMauExcel.Text = "Xem mẫu excel";
            this.btnXemMauExcel.UseVisualStyleBackColor = true;
            this.btnXemMauExcel.Click += new System.EventHandler(this.btnXemMauExcel_Click);
            // 
            // XLSALKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnXemMauExcel);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.label1);
            this.Name = "XLSALKH";
            this.Size = new System.Drawing.Size(275, 262);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private System.Windows.Forms.Button btnTim;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6ComboBox comboBox2;
        private V6Controls.V6CheckBox checkBox1;
        private V6Controls.V6CheckBox checkBox2;
        private System.Windows.Forms.RichTextBox txtFile;
        private System.Windows.Forms.Button btnXemMauExcel;
    }
}
