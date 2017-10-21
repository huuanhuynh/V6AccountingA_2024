namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class V6IMPORTXLS
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.v6Label3 = new V6Controls.V6Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkChiNhapMaMoi = new V6Controls.V6CheckBox();
            this.chkChuyenMa = new V6Controls.V6CheckBox();
            this.comboBox2 = new V6Controls.V6ComboBox();
            this.txtFile = new System.Windows.Forms.RichTextBox();
            this.cboDanhMuc = new V6Controls.V6ComboBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.cboDanhMuc);
            this.panel1.Controls.Add(this.v6Label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtFile);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00127";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "File Excel";
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
            // btnBrowse
            // 
            this.btnBrowse.AccessibleDescription = "XULYB00007";
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::V6ControlManager.Properties.Resources.Search24;
            this.btnBrowse.Location = new System.Drawing.Point(70, 45);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(107, 40);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Tìm file Excel";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.AccessibleName = "kieu_post";
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
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
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "XULYG00001";
            this.groupBox1.Controls.Add(this.chkChiNhapMaMoi);
            this.groupBox1.Controls.Add(this.chkChuyenMa);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.v6Label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 112);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // chkChiNhapMaMoi
            // 
            this.chkChiNhapMaMoi.AccessibleDescription = "FILTERC00021";
            this.chkChiNhapMaMoi.AutoSize = true;
            this.chkChiNhapMaMoi.Checked = true;
            this.chkChiNhapMaMoi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChiNhapMaMoi.Location = new System.Drawing.Point(94, 19);
            this.chkChiNhapMaMoi.Name = "chkChiNhapMaMoi";
            this.chkChiNhapMaMoi.Size = new System.Drawing.Size(125, 17);
            this.chkChiNhapMaMoi.TabIndex = 1;
            this.chkChiNhapMaMoi.Text = "Chỉ cập nhập mã mới";
            this.chkChiNhapMaMoi.UseVisualStyleBackColor = true;
            // 
            // chkChuyenMa
            // 
            this.chkChuyenMa.AccessibleDescription = "FILTERC00016";
            this.chkChuyenMa.AutoSize = true;
            this.chkChuyenMa.Location = new System.Drawing.Point(9, 19);
            this.chkChuyenMa.Name = "chkChuyenMa";
            this.chkChuyenMa.Size = new System.Drawing.Size(79, 17);
            this.chkChuyenMa.TabIndex = 0;
            this.chkChuyenMa.Text = "Chuyễn mã";
            this.chkChuyenMa.UseVisualStyleBackColor = true;
            this.chkChuyenMa.CheckedChanged += new System.EventHandler(this.chkChuyenMa_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.AccessibleName = "kieu_post";
            this.comboBox2.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Enabled = false;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "UNICODE"});
            this.comboBox2.Location = new System.Drawing.Point(98, 74);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(154, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(8, 90);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(270, 75);
            this.txtFile.TabIndex = 5;
            this.txtFile.Text = "";
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.AccessibleName = "kieu_post";
            this.cboDanhMuc.BackColor = System.Drawing.SystemColors.Window;
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDanhMuc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(70, 18);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(209, 21);
            this.cboDanhMuc.TabIndex = 3;
            this.cboDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cboDanhMuc_SelectedIndexChanged);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00126";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(5, 18);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(56, 13);
            this.v6Label1.TabIndex = 2;
            this.v6Label1.Text = "Danh mục";
            // 
            // V6IMPORTXLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "V6IMPORTXLS";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6CheckBox chkChiNhapMaMoi;
        private V6Controls.V6CheckBox chkChuyenMa;
        private V6Controls.V6ComboBox comboBox1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6ComboBox comboBox2;
        private V6Controls.V6Label v6Label2;
        private System.Windows.Forms.RichTextBox txtFile;
        private V6Controls.V6ComboBox cboDanhMuc;
        private V6Controls.V6Label v6Label1;
    }
}
