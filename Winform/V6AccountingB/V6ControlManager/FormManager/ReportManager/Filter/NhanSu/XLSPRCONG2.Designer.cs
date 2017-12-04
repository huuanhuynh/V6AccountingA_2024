namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    partial class XLSPRCONG2
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
            this.v6Label2 = new V6Controls.V6Label();
            this.btnTim = new System.Windows.Forms.Button();
            this.v6Label3 = new V6Controls.V6Label();
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDanhSachCot = new V6Controls.V6ColorTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.txtSoMaCong = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6CheckBox1 = new V6Controls.V6CheckBox();
            this.checkBox2 = new V6Controls.V6CheckBox();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.comboBox2 = new V6Controls.V6ComboBox();
            this.txtFile = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00205";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(5, 178);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(48, 13);
            this.v6Label2.TabIndex = 4;
            this.v6Label2.Text = "Mã đích";
            // 
            // btnTim
            // 
            this.btnTim.AccessibleDescription = "FILTERB00005";
            this.btnTim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTim.Image = global::V6ControlManager.Properties.Resources.Search24;
            this.btnTim.Location = new System.Drawing.Point(72, 3);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(94, 41);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00204";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(6, 151);
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
            this.comboBox1.Location = new System.Drawing.Point(98, 148);
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
            this.groupBox1.Controls.Add(this.txtDanhSachCot);
            this.groupBox1.Controls.Add(this.v6Label4);
            this.groupBox1.Controls.Add(this.txtSoMaCong);
            this.groupBox1.Controls.Add(this.v6Label1);
            this.groupBox1.Controls.Add(this.v6CheckBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.v6Label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 210);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // txtDanhSachCot
            // 
            this.txtDanhSachCot.AccessibleName = "DS_COT";
            this.txtDanhSachCot.BackColor = System.Drawing.SystemColors.Window;
            this.txtDanhSachCot.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDanhSachCot.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDanhSachCot.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot.HoverColor = System.Drawing.Color.Yellow;
            this.txtDanhSachCot.LeaveColor = System.Drawing.Color.White;
            this.txtDanhSachCot.Location = new System.Drawing.Point(98, 92);
            this.txtDanhSachCot.Multiline = true;
            this.txtDanhSachCot.Name = "txtDanhSachCot";
            this.txtDanhSachCot.Size = new System.Drawing.Size(154, 50);
            this.txtDanhSachCot.TabIndex = 12;
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "FILTERL00204";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(6, 92);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(77, 13);
            this.v6Label4.TabIndex = 11;
            this.v6Label4.Text = "Danh sách cột";
            // 
            // txtSoMaCong
            // 
            this.txtSoMaCong.AccessibleName = "SO_COT";
            this.txtSoMaCong.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoMaCong.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoMaCong.DecimalPlaces = 0;
            this.txtSoMaCong.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoMaCong.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoMaCong.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoMaCong.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoMaCong.LeaveColor = System.Drawing.Color.White;
            this.txtSoMaCong.Location = new System.Drawing.Point(98, 63);
            this.txtSoMaCong.Name = "txtSoMaCong";
            this.txtSoMaCong.Size = new System.Drawing.Size(49, 20);
            this.txtSoMaCong.TabIndex = 10;
            this.txtSoMaCong.Text = "0";
            this.txtSoMaCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoMaCong.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00204";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 66);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(82, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Số cột mã công";
            // 
            // v6CheckBox1
            // 
            this.v6CheckBox1.AccessibleDescription = "FILTERC00016";
            this.v6CheckBox1.AccessibleName = "CHK_NGAY_CONG";
            this.v6CheckBox1.AutoSize = true;
            this.v6CheckBox1.Location = new System.Drawing.Point(9, 40);
            this.v6CheckBox1.Name = "v6CheckBox1";
            this.v6CheckBox1.Size = new System.Drawing.Size(138, 17);
            this.v6CheckBox1.TabIndex = 6;
            this.v6CheckBox1.Text = "Tự động tạo ngày công";
            this.v6CheckBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AccessibleDescription = "FILTERC00017";
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(98, 19);
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
            this.comboBox2.Location = new System.Drawing.Point(98, 175);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(154, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(12, 56);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(214, 75);
            this.txtFile.TabIndex = 1;
            this.txtFile.Text = "";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00127";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "File Excel";
            // 
            // XLSPRCONG2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTim);
            this.Name = "XLSPRCONG2";
            this.Size = new System.Drawing.Size(285, 360);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private V6Controls.V6Label v6Label2;
        private System.Windows.Forms.Button btnTim;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6ComboBox comboBox2;
        private V6Controls.V6CheckBox checkBox1;
        private V6Controls.V6CheckBox checkBox2;
        private System.Windows.Forms.RichTextBox txtFile;
        private V6Controls.V6CheckBox v6CheckBox1;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6NumberTextBox txtSoMaCong;
        private V6Controls.V6ColorTextBox txtDanhSachCot;
        private V6Controls.V6Label v6Label4;
        private System.Windows.Forms.Label label1;
    }
}
