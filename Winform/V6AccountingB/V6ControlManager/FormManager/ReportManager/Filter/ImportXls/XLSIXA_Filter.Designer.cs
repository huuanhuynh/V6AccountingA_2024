namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class XLSIXA_Filter
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
            this.btnChon = new System.Windows.Forms.Button();
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
            // btnChon
            // 
            this.btnChon.AccessibleDescription = "FILTERB00005";
            this.btnChon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChon.Image = global::V6ControlManager.Properties.Resources.Excel16;
            this.btnChon.Location = new System.Drawing.Point(37, 77);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(130, 29);
            this.btnChon.TabIndex = 2;
            this.btnChon.Text = "Chọn file Excel";
            this.btnChon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnTim_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(3, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 104);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // checkBox2
            // 
            this.checkBox2.AccessibleDescription = "FILTERC00016";
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(94, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(146, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Xóa dữ liệu nhận từ excel";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "FILTERC00016";
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
            this.txtFile.Size = new System.Drawing.Size(235, 66);
            this.txtFile.TabIndex = 0;
            this.txtFile.Text = "";
            // 
            // btnXemMauExcel
            // 
            this.btnXemMauExcel.AccessibleDescription = "FILTERB00013";
            this.btnXemMauExcel.Location = new System.Drawing.Point(4, 217);
            this.btnXemMauExcel.Name = "btnXemMauExcel";
            this.btnXemMauExcel.Size = new System.Drawing.Size(88, 29);
            this.btnXemMauExcel.TabIndex = 12;
            this.btnXemMauExcel.Text = "Xem mẫu excel";
            this.btnXemMauExcel.UseVisualStyleBackColor = true;
            // 
            // XLSIXA_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnXemMauExcel);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.label1);
            this.Name = "XLSIXA_Filter";
            this.Size = new System.Drawing.Size(275, 256);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private System.Windows.Forms.Button btnChon;
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
