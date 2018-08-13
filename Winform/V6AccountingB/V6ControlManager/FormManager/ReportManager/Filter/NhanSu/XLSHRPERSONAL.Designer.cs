namespace V6ControlManager.FormManager.ReportManager.Filter.NhanSu
{
    partial class XLSHRPERSONAL
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
            this.txtDanhSachCot1 = new V6Controls.V6ColorTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.txtDongBatDau = new V6Controls.V6NumberTextBox();
            this.v6Label5 = new V6Controls.V6Label();
            this.txtSoCotMaNhanSu = new V6Controls.V6NumberTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6CheckBox1 = new V6Controls.V6CheckBox();
            this.checkBox2 = new V6Controls.V6CheckBox();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.comboBox2 = new V6Controls.V6ComboBox();
            this.txtFile = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label6 = new V6Controls.V6Label();
            this.txtDanhSachCot2 = new V6Controls.V6ColorTextBox();
            this.btnEditXml = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00205";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(8, 170);
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
            this.v6Label3.Location = new System.Drawing.Point(9, 143);
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
            this.comboBox1.Location = new System.Drawing.Point(123, 137);
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
            this.groupBox1.Controls.Add(this.btnEditXml);
            this.groupBox1.Controls.Add(this.txtDanhSachCot2);
            this.groupBox1.Controls.Add(this.v6Label6);
            this.groupBox1.Controls.Add(this.txtDanhSachCot1);
            this.groupBox1.Controls.Add(this.v6Label4);
            this.groupBox1.Controls.Add(this.txtDongBatDau);
            this.groupBox1.Controls.Add(this.v6Label5);
            this.groupBox1.Controls.Add(this.txtSoCotMaNhanSu);
            this.groupBox1.Controls.Add(this.v6Label1);
            this.groupBox1.Controls.Add(this.v6CheckBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(3, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 240);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // txtDanhSachCot1
            // 
            this.txtDanhSachCot1.AccessibleName = "DSCOT1";
            this.txtDanhSachCot1.BackColor = System.Drawing.SystemColors.Window;
            this.txtDanhSachCot1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDanhSachCot1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDanhSachCot1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot1.HoverColor = System.Drawing.Color.Yellow;
            this.txtDanhSachCot1.LeaveColor = System.Drawing.Color.White;
            this.txtDanhSachCot1.Location = new System.Drawing.Point(120, 115);
            this.txtDanhSachCot1.Multiline = true;
            this.txtDanhSachCot1.Name = "txtDanhSachCot1";
            this.txtDanhSachCot1.Size = new System.Drawing.Size(154, 50);
            this.txtDanhSachCot1.TabIndex = 12;
            // 
            // v6Label4
            // 
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(6, 118);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(77, 13);
            this.v6Label4.TabIndex = 11;
            this.v6Label4.Text = "Danh sách cột";
            // 
            // txtDongBatDau
            // 
            this.txtDongBatDau.AccessibleName = "DongBatDau";
            this.txtDongBatDau.BackColor = System.Drawing.SystemColors.Window;
            this.txtDongBatDau.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDongBatDau.DecimalPlaces = 0;
            this.txtDongBatDau.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDongBatDau.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDongBatDau.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDongBatDau.HoverColor = System.Drawing.Color.Yellow;
            this.txtDongBatDau.LeaveColor = System.Drawing.Color.White;
            this.txtDongBatDau.Location = new System.Drawing.Point(120, 63);
            this.txtDongBatDau.Name = "txtDongBatDau";
            this.txtDongBatDau.Size = new System.Drawing.Size(49, 20);
            this.txtDongBatDau.TabIndex = 10;
            this.txtDongBatDau.Text = "1";
            this.txtDongBatDau.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDongBatDau.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // v6Label5
            // 
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(6, 66);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(73, 13);
            this.v6Label5.TabIndex = 8;
            this.v6Label5.Text = "Dòng bắt đầu";
            // 
            // txtSoCotMaNhanSu
            // 
            this.txtSoCotMaNhanSu.AccessibleName = "SO_COT";
            this.txtSoCotMaNhanSu.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoCotMaNhanSu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCotMaNhanSu.DecimalPlaces = 0;
            this.txtSoCotMaNhanSu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCotMaNhanSu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCotMaNhanSu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCotMaNhanSu.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCotMaNhanSu.LeaveColor = System.Drawing.Color.White;
            this.txtSoCotMaNhanSu.Location = new System.Drawing.Point(120, 89);
            this.txtSoCotMaNhanSu.Name = "txtSoCotMaNhanSu";
            this.txtSoCotMaNhanSu.Size = new System.Drawing.Size(49, 20);
            this.txtSoCotMaNhanSu.TabIndex = 10;
            this.txtSoCotMaNhanSu.Text = "0";
            this.txtSoCotMaNhanSu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoCotMaNhanSu.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label1
            // 
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 92);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(105, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Số cột mã nhân viên";
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
            this.checkBox2.Location = new System.Drawing.Point(120, 19);
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
            this.comboBox2.Location = new System.Drawing.Point(123, 164);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(154, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(3, 56);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(273, 75);
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
            // v6Label6
            // 
            this.v6Label6.AutoSize = true;
            this.v6Label6.Location = new System.Drawing.Point(5, 174);
            this.v6Label6.Name = "v6Label6";
            this.v6Label6.Size = new System.Drawing.Size(86, 13);
            this.v6Label6.TabIndex = 11;
            this.v6Label6.Text = "Danh sách cột 2";
            // 
            // txtDanhSachCot2
            // 
            this.txtDanhSachCot2.AccessibleName = "DSCOT2";
            this.txtDanhSachCot2.BackColor = System.Drawing.SystemColors.Window;
            this.txtDanhSachCot2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDanhSachCot2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDanhSachCot2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDanhSachCot2.HoverColor = System.Drawing.Color.Yellow;
            this.txtDanhSachCot2.LeaveColor = System.Drawing.Color.White;
            this.txtDanhSachCot2.Location = new System.Drawing.Point(119, 171);
            this.txtDanhSachCot2.Multiline = true;
            this.txtDanhSachCot2.Name = "txtDanhSachCot2";
            this.txtDanhSachCot2.Size = new System.Drawing.Size(154, 50);
            this.txtDanhSachCot2.TabIndex = 12;
            this.txtDanhSachCot2.TextChanged += new System.EventHandler(this.txtDanhSachCot2_TextChanged);
            // 
            // btnEditXml
            // 
            this.btnEditXml.AccessibleName = "";
            this.btnEditXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditXml.Location = new System.Drawing.Point(51, 196);
            this.btnEditXml.Name = "btnEditXml";
            this.btnEditXml.Size = new System.Drawing.Size(40, 25);
            this.btnEditXml.TabIndex = 75;
            this.btnEditXml.Text = "...";
            this.btnEditXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditXml.UseVisualStyleBackColor = true;
            this.btnEditXml.Click += new System.EventHandler(this.btnEditXml_Click);
            // 
            // XLSHRPERSONAL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.v6Label3);
            this.Name = "XLSHRPERSONAL";
            this.Size = new System.Drawing.Size(285, 434);
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
        private V6Controls.V6NumberTextBox txtSoCotMaNhanSu;
        private V6Controls.V6ColorTextBox txtDanhSachCot1;
        private V6Controls.V6Label v6Label4;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6NumberTextBox txtDongBatDau;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6ColorTextBox txtDanhSachCot2;
        private V6Controls.V6Label v6Label6;
        protected System.Windows.Forms.Button btnEditXml;
    }
}
