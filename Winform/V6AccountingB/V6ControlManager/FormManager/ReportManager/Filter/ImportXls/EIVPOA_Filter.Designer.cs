namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class EIVPOA_Filter
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoSoCt = new V6Controls.V6CheckBox();
            this.chkDeleteOldExcel = new V6Controls.V6CheckBox();
            this.btnSuaChiTieu = new System.Windows.Forms.Button();
            this.btnXemMauExcel = new System.Windows.Forms.Button();
            this.txtUserName = new V6Controls.V6ColorTextBox();
            this.txtPassword = new V6Controls.V6ColorTextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblCaptcha = new System.Windows.Forms.Label();
            this.txtCaptcha = new V6Controls.V6ColorTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtToken = new V6Controls.V6ColorTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblOK = new System.Windows.Forms.Label();
            this.lblTOTAL = new System.Windows.Forms.Label();
            this.lblFAIL = new System.Windows.Forms.Label();
            this.lblERR = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCQT_DAYS = new V6Controls.V6ColorTextBox();
            this.chkCQT_DAYS = new V6Controls.V6CheckBox();
            this.chkContinueDownload = new V6Controls.V6CheckBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.svgImage = new System.Windows.Forms.PictureBox();
            this.btnCaptcha = new System.Windows.Forms.Button();
            this.cboFields = new System.Windows.Forms.ComboBox();
            this.lblCurrentCode = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(-2, 5);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(58, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User name";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00005";
            this.groupBox1.Controls.Add(this.chkAutoSoCt);
            this.groupBox1.Controls.Add(this.chkDeleteOldExcel);
            this.groupBox1.Location = new System.Drawing.Point(3, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 70);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // chkAutoSoCt
            // 
            this.chkAutoSoCt.AccessibleDescription = "FILTERL00269";
            this.chkAutoSoCt.AutoSize = true;
            this.chkAutoSoCt.Location = new System.Drawing.Point(6, 42);
            this.chkAutoSoCt.Name = "chkAutoSoCt";
            this.chkAutoSoCt.Size = new System.Drawing.Size(144, 17);
            this.chkAutoSoCt.TabIndex = 1;
            this.chkAutoSoCt.Text = "Tự động tạo số chứng từ";
            this.chkAutoSoCt.UseVisualStyleBackColor = true;
            this.chkAutoSoCt.CheckedChanged += new System.EventHandler(this.chkAutoSoCt_CheckedChanged);
            // 
            // chkDeleteOldExcel
            // 
            this.chkDeleteOldExcel.AccessibleDescription = "FILTERC00017";
            this.chkDeleteOldExcel.AutoSize = true;
            this.chkDeleteOldExcel.Checked = true;
            this.chkDeleteOldExcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteOldExcel.Location = new System.Drawing.Point(6, 19);
            this.chkDeleteOldExcel.Name = "chkDeleteOldExcel";
            this.chkDeleteOldExcel.Size = new System.Drawing.Size(146, 17);
            this.chkDeleteOldExcel.TabIndex = 0;
            this.chkDeleteOldExcel.Text = "Xóa dữ liệu nhận từ excel";
            this.chkDeleteOldExcel.UseVisualStyleBackColor = true;
            this.chkDeleteOldExcel.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btnSuaChiTieu
            // 
            this.btnSuaChiTieu.AccessibleDescription = "FILTERB00001";
            this.btnSuaChiTieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSuaChiTieu.Location = new System.Drawing.Point(110, 531);
            this.btnSuaChiTieu.Name = "btnSuaChiTieu";
            this.btnSuaChiTieu.Size = new System.Drawing.Size(88, 29);
            this.btnSuaChiTieu.TabIndex = 20;
            this.btnSuaChiTieu.Text = "Sửa chỉ tiêu";
            this.btnSuaChiTieu.UseVisualStyleBackColor = true;
            this.btnSuaChiTieu.Click += new System.EventHandler(this.btnSuaChiTieu_Click);
            // 
            // btnXemMauExcel
            // 
            this.btnXemMauExcel.AccessibleDescription = "FILTERB00013";
            this.btnXemMauExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXemMauExcel.Location = new System.Drawing.Point(0, 531);
            this.btnXemMauExcel.Name = "btnXemMauExcel";
            this.btnXemMauExcel.Size = new System.Drawing.Size(88, 29);
            this.btnXemMauExcel.TabIndex = 19;
            this.btnXemMauExcel.Text = "Xem mẫu excel";
            this.btnXemMauExcel.UseVisualStyleBackColor = true;
            this.btnXemMauExcel.Click += new System.EventHandler(this.btnXemMauExcel_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.AccessibleName = "USERNAME";
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtUserName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtUserName.HoverColor = System.Drawing.Color.Yellow;
            this.txtUserName.LeaveColor = System.Drawing.Color.White;
            this.txtUserName.Location = new System.Drawing.Point(67, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(202, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleName = "PASSWORD";
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtPassword.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPassword.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtPassword.HoverColor = System.Drawing.Color.Yellow;
            this.txtPassword.LeaveColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(67, 29);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(202, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TabStop = false;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(-3, 32);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblCaptcha
            // 
            this.lblCaptcha.AutoSize = true;
            this.lblCaptcha.Location = new System.Drawing.Point(-3, 104);
            this.lblCaptcha.Name = "lblCaptcha";
            this.lblCaptcha.Size = new System.Drawing.Size(47, 13);
            this.lblCaptcha.TabIndex = 5;
            this.lblCaptcha.Text = "Captcha";
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.AccessibleName = "CAPTCHA";
            this.txtCaptcha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaptcha.BackColor = System.Drawing.SystemColors.Window;
            this.txtCaptcha.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtCaptcha.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtCaptcha.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCaptcha.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtCaptcha.HoverColor = System.Drawing.Color.Yellow;
            this.txtCaptcha.LeaveColor = System.Drawing.Color.White;
            this.txtCaptcha.Location = new System.Drawing.Point(67, 101);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(202, 20);
            this.txtCaptcha.TabIndex = 6;
            this.txtCaptcha.TabStop = false;
            this.txtCaptcha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaptcha_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(0, 127);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(88, 29);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtToken
            // 
            this.txtToken.AccessibleName = "TOKEN";
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.BackColor = System.Drawing.SystemColors.Window;
            this.txtToken.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtToken.Enabled = false;
            this.txtToken.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtToken.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtToken.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtToken.HoverColor = System.Drawing.Color.Yellow;
            this.txtToken.LeaveColor = System.Drawing.Color.White;
            this.txtToken.Location = new System.Drawing.Point(97, 132);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(172, 20);
            this.txtToken.TabIndex = 8;
            this.txtToken.TabStop = false;
            this.txtToken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaptcha_KeyDown);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Từ ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(110, 205);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 12;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(110, 183);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 10;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 231);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(269, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // lblOK
            // 
            this.lblOK.AutoSize = true;
            this.lblOK.Location = new System.Drawing.Point(70, 257);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(40, 13);
            this.lblOK.TabIndex = 15;
            this.lblOK.Text = "Tải ok:";
            // 
            // lblTOTAL
            // 
            this.lblTOTAL.AutoSize = true;
            this.lblTOTAL.Location = new System.Drawing.Point(4, 257);
            this.lblTOTAL.Name = "lblTOTAL";
            this.lblTOTAL.Size = new System.Drawing.Size(35, 13);
            this.lblTOTAL.TabIndex = 14;
            this.lblTOTAL.Text = "Tổng:";
            // 
            // lblFAIL
            // 
            this.lblFAIL.AutoSize = true;
            this.lblFAIL.Location = new System.Drawing.Point(141, 257);
            this.lblFAIL.Name = "lblFAIL";
            this.lblFAIL.Size = new System.Drawing.Size(38, 13);
            this.lblFAIL.TabIndex = 16;
            this.lblFAIL.Text = "Tải lỗi:";
            // 
            // lblERR
            // 
            this.lblERR.AutoSize = true;
            this.lblERR.Location = new System.Drawing.Point(210, 257);
            this.lblERR.Name = "lblERR";
            this.lblERR.Size = new System.Drawing.Size(43, 13);
            this.lblERR.TabIndex = 17;
            this.lblERR.Text = "Đọc lỗi:";
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "FILTERG00005";
            this.groupBox2.Controls.Add(this.txtCQT_DAYS);
            this.groupBox2.Controls.Add(this.chkCQT_DAYS);
            this.groupBox2.Controls.Add(this.chkContinueDownload);
            this.groupBox2.Location = new System.Drawing.Point(3, 273);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 70);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tùy chọn";
            // 
            // txtCQT_DAYS
            // 
            this.txtCQT_DAYS.AccessibleName = "CQT_DAYS";
            this.txtCQT_DAYS.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtCQT_DAYS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtCQT_DAYS.Enabled = false;
            this.txtCQT_DAYS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtCQT_DAYS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCQT_DAYS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtCQT_DAYS.HoverColor = System.Drawing.Color.Yellow;
            this.txtCQT_DAYS.LeaveColor = System.Drawing.Color.White;
            this.txtCQT_DAYS.Location = new System.Drawing.Point(174, 39);
            this.txtCQT_DAYS.Name = "txtCQT_DAYS";
            this.txtCQT_DAYS.ReadOnly = true;
            this.txtCQT_DAYS.Size = new System.Drawing.Size(48, 20);
            this.txtCQT_DAYS.TabIndex = 9;
            this.txtCQT_DAYS.TabStop = false;
            this.txtCQT_DAYS.Text = "1";
            // 
            // chkCQT_DAYS
            // 
            this.chkCQT_DAYS.AutoSize = true;
            this.chkCQT_DAYS.Checked = true;
            this.chkCQT_DAYS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCQT_DAYS.Enabled = false;
            this.chkCQT_DAYS.Location = new System.Drawing.Point(6, 42);
            this.chkCQT_DAYS.Name = "chkCQT_DAYS";
            this.chkCQT_DAYS.Size = new System.Drawing.Size(109, 17);
            this.chkCQT_DAYS.TabIndex = 1;
            this.chkCQT_DAYS.Text = "Số ngày tải tối đa";
            this.chkCQT_DAYS.UseVisualStyleBackColor = true;
            this.chkCQT_DAYS.CheckedChanged += new System.EventHandler(this.chkAutoSoCt_CheckedChanged);
            // 
            // chkContinueDownload
            // 
            this.chkContinueDownload.AutoSize = true;
            this.chkContinueDownload.Checked = true;
            this.chkContinueDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContinueDownload.Location = new System.Drawing.Point(6, 19);
            this.chkContinueDownload.Name = "chkContinueDownload";
            this.chkContinueDownload.Size = new System.Drawing.Size(106, 17);
            this.chkContinueDownload.TabIndex = 0;
            this.chkContinueDownload.Text = "Tải tiếp phiếu lỗi.";
            this.chkContinueDownload.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.AccessibleName = "";
            this.btnReplace.Image = global::V6ControlManager.Properties.Resources.Replace;
            this.btnReplace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReplace.Location = new System.Drawing.Point(9, 471);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(109, 40);
            this.btnReplace.TabIndex = 21;
            this.btnReplace.Tag = "";
            this.btnReplace.Text = "Chuyển DV";
            this.btnReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.AccessibleName = "";
            this.btnMoveUp.Image = global::V6ControlManager.Properties.Resources.ArrowUpBlue3D;
            this.btnMoveUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMoveUp.Location = new System.Drawing.Point(124, 425);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(109, 40);
            this.btnMoveUp.TabIndex = 21;
            this.btnMoveUp.Tag = "";
            this.btnMoveUp.Text = "Chuyển DV";
            this.btnMoveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDichVu
            // 
            this.btnDichVu.AccessibleName = "";
            this.btnDichVu.Image = global::V6ControlManager.Properties.Resources.ArrowDownBlue3D;
            this.btnDichVu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDichVu.Location = new System.Drawing.Point(9, 425);
            this.btnDichVu.Name = "btnDichVu";
            this.btnDichVu.Size = new System.Drawing.Size(109, 40);
            this.btnDichVu.TabIndex = 21;
            this.btnDichVu.Tag = "";
            this.btnDichVu.Text = "Chuyển DV";
            this.btnDichVu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDichVu.UseVisualStyleBackColor = true;
            this.btnDichVu.Click += new System.EventHandler(this.btnDichVu_Click);
            // 
            // svgImage
            // 
            this.svgImage.Location = new System.Drawing.Point(67, 55);
            this.svgImage.Name = "svgImage";
            this.svgImage.Size = new System.Drawing.Size(202, 40);
            this.svgImage.TabIndex = 8;
            this.svgImage.TabStop = false;
            // 
            // btnCaptcha
            // 
            this.btnCaptcha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCaptcha.Image = global::V6ControlManager.Properties.Resources.Refresh;
            this.btnCaptcha.Location = new System.Drawing.Point(0, 55);
            this.btnCaptcha.Name = "btnCaptcha";
            this.btnCaptcha.Size = new System.Drawing.Size(40, 40);
            this.btnCaptcha.TabIndex = 4;
            this.btnCaptcha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCaptcha.UseVisualStyleBackColor = true;
            this.btnCaptcha.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cboFields
            // 
            this.cboFields.AccessibleName = "";
            this.cboFields.BackColor = System.Drawing.SystemColors.Window;
            this.cboFields.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboFields.Location = new System.Drawing.Point(124, 490);
            this.cboFields.Name = "cboFields";
            this.cboFields.Size = new System.Drawing.Size(144, 21);
            this.cboFields.TabIndex = 23;
            // 
            // lblCurrentCode
            // 
            this.lblCurrentCode.AutoSize = true;
            this.lblCurrentCode.Location = new System.Drawing.Point(125, 474);
            this.lblCurrentCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentCode.Name = "lblCurrentCode";
            this.lblCurrentCode.Size = new System.Drawing.Size(75, 13);
            this.lblCurrentCode.TabIndex = 22;
            this.lblCurrentCode.Text = "Trường dữ liệu";
            // 
            // EIVPOA_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboFields);
            this.Controls.Add(this.lblCurrentCode);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnDichVu);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblERR);
            this.Controls.Add(this.lblFAIL);
            this.Controls.Add(this.lblTOTAL);
            this.Controls.Add(this.lblOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.txtCaptcha);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.svgImage);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnXemMauExcel);
            this.Controls.Add(this.btnSuaChiTieu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCaptcha);
            this.Controls.Add(this.btnCaptcha);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtToken);
            this.Name = "EIVPOA_Filter";
            this.Size = new System.Drawing.Size(275, 563);
            this.Load += new System.EventHandler(this.EIVPOA_Filter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnCaptcha;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6CheckBox chkDeleteOldExcel;
        private System.Windows.Forms.Button btnSuaChiTieu;
        private System.Windows.Forms.Button btnXemMauExcel;
        private V6Controls.V6CheckBox chkAutoSoCt;
        private System.Windows.Forms.PictureBox svgImage;
        private V6Controls.V6ColorTextBox txtUserName;
        private V6Controls.V6ColorTextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblCaptcha;
        private V6Controls.V6ColorTextBox txtCaptcha;
        private System.Windows.Forms.Button btnLogin;
        private V6Controls.V6ColorTextBox txtToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lblTOTAL;
        private System.Windows.Forms.Label lblFAIL;
        private System.Windows.Forms.Label lblERR;
        private System.Windows.Forms.GroupBox groupBox2;
        private V6Controls.V6CheckBox chkCQT_DAYS;
        private V6Controls.V6CheckBox chkContinueDownload;
        private V6Controls.V6ColorTextBox txtCQT_DAYS;
        public System.Windows.Forms.Button btnDichVu;
        public System.Windows.Forms.Button btnMoveUp;
        public System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.ComboBox cboFields;
        private System.Windows.Forms.Label lblCurrentCode;
    }
}
