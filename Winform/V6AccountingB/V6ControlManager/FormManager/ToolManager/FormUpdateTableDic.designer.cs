namespace V6ControlManager.FormManager.ToolManager
{
    partial class FormUpdateTableDic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateTableDic));
            this.btnDoUpdate = new System.Windows.Forms.Button();
            this.richStatus = new System.Windows.Forms.RichTextBox();
            this.numUpdateLimit = new V6Controls.V6NumberTextBox();
            this.txtDic1 = new V6Controls.V6ColorTextBox();
            this.dicEditButton2 = new V6Controls.Controls.DicEditButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUpdateField = new V6Controls.V6ColorTextBox();
            this.txtTableName = new V6Controls.V6ColorTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKeyField = new V6Controls.V6ColorTextBox();
            this.btnGenUpdateSql = new System.Windows.Forms.Button();
            this.richSQL = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilterField = new V6Controls.V6ColorTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFilterValue = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // btnDoUpdate
            // 
            this.btnDoUpdate.Location = new System.Drawing.Point(15, 192);
            this.btnDoUpdate.Name = "btnDoUpdate";
            this.btnDoUpdate.Size = new System.Drawing.Size(93, 23);
            this.btnDoUpdate.TabIndex = 2;
            this.btnDoUpdate.Text = "Test Insert";
            this.btnDoUpdate.UseVisualStyleBackColor = true;
            this.btnDoUpdate.Click += new System.EventHandler(this.btnDoUpdate_Click);
            // 
            // richStatus
            // 
            this.richStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richStatus.Location = new System.Drawing.Point(12, 262);
            this.richStatus.Name = "richStatus";
            this.richStatus.Size = new System.Drawing.Size(909, 404);
            this.richStatus.TabIndex = 4;
            this.richStatus.Text = "";
            // 
            // numUpdateLimit
            // 
            this.numUpdateLimit.AccessibleDescription = "";
            this.numUpdateLimit.AccessibleName = "han_tt";
            this.numUpdateLimit.BackColor = System.Drawing.SystemColors.Window;
            this.numUpdateLimit.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.numUpdateLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUpdateLimit.EnterColor = System.Drawing.Color.PaleGreen;
            this.numUpdateLimit.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numUpdateLimit.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.numUpdateLimit.HoverColor = System.Drawing.Color.Yellow;
            this.numUpdateLimit.LeaveColor = System.Drawing.Color.White;
            this.numUpdateLimit.Location = new System.Drawing.Point(114, 195);
            this.numUpdateLimit.Name = "numUpdateLimit";
            this.numUpdateLimit.Size = new System.Drawing.Size(55, 20);
            this.numUpdateLimit.TabIndex = 23;
            this.numUpdateLimit.Text = "10";
            this.numUpdateLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpdateLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // txtDic1
            // 
            this.txtDic1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtDic1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDic1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDic1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDic1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDic1.HoverColor = System.Drawing.Color.Yellow;
            this.txtDic1.LeaveColor = System.Drawing.Color.White;
            this.txtDic1.Location = new System.Drawing.Point(321, 41);
            this.txtDic1.Name = "txtDic1";
            this.txtDic1.ReadOnly = true;
            this.txtDic1.Size = new System.Drawing.Size(584, 20);
            this.txtDic1.TabIndex = 24;
            this.txtDic1.TabStop = false;
            // 
            // dicEditButton2
            // 
            this.dicEditButton2.Image = ((System.Drawing.Image)(resources.GetObject("dicEditButton2.Image")));
            this.dicEditButton2.KeyWordList = new string[] {
        "Name:M_TEN_CTY;NOTEMPTY:1;Ptype:FILTER_BROTHER;Field:MA_DVCS;Fname:TEN_DVCS",
        "Name:MA_KHO;NOTEMPTY:0;Ptype:FILTER;Field:MA_KHO",
        "Name:R_MA_DVCS;Ptype:FILTER;Field:MA_DVCS",
        "Name:R_TEN_DVCS;Ptype:FILTER_BROTHER;Field:MA_DVCS;Fname:TEN_DVCS",
        "Name:SOTIENVIETBANGCHU_TIENBAN;Ptype:TABLE2;Field:T_TT",
        "Name:SOTIENVIETBANGCHUE_TIENBANNT;Ptype:TABLE2;Field:NO_CK",
        "Name:SOTIENVIETBANGCHUV_TIENBANNT;Ptype:TABLE2;Field:T_TT",
        "Name:SOTIENVIETBANGCHUV_TIENBAN;Ptype:TABLE2;Field:T_TT",
        "Name:SOTIENVIETBANGCHUE_TIENBANNT;Ptype:TABLE2;Field:T_TT",
        "Name:SOTIENVIETBANGCHUE_TIENBAN;Ptype:TABLE2;Field:T_TT"};
            this.dicEditButton2.Location = new System.Drawing.Point(905, 41);
            this.dicEditButton2.Name = "dicEditButton2";
            this.dicEditButton2.ReferenceControl = this.txtDic1;
            this.dicEditButton2.Separator_Item = "~";
            this.dicEditButton2.Separator_Value = ";";
            this.dicEditButton2.Size = new System.Drawing.Size(21, 21);
            this.dicEditButton2.TabIndex = 152;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = ".";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(275, 13);
            this.label5.TabIndex = 153;
            this.label5.Text = "Dữ liệu thêm vào dạng Dic string Key:Value;Key2:Value2";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = ".";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 154;
            this.label1.Text = "Trường update";
            // 
            // txtUpdateField
            // 
            this.txtUpdateField.BackColor = System.Drawing.SystemColors.Window;
            this.txtUpdateField.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtUpdateField.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtUpdateField.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUpdateField.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtUpdateField.HoverColor = System.Drawing.Color.Yellow;
            this.txtUpdateField.LeaveColor = System.Drawing.Color.White;
            this.txtUpdateField.Location = new System.Drawing.Point(104, 41);
            this.txtUpdateField.Name = "txtUpdateField";
            this.txtUpdateField.Size = new System.Drawing.Size(177, 20);
            this.txtUpdateField.TabIndex = 155;
            this.txtUpdateField.Text = "Name";
            this.txtUpdateField.TextChanged += new System.EventHandler(this.btnGenUpdateSql_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.SystemColors.Window;
            this.txtTableName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTableName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTableName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTableName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTableName.HoverColor = System.Drawing.Color.Yellow;
            this.txtTableName.LeaveColor = System.Drawing.Color.White;
            this.txtTableName.Location = new System.Drawing.Point(104, 11);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(177, 20);
            this.txtTableName.TabIndex = 155;
            this.txtTableName.V6LostFocus += new V6Controls.ControlEventHandle(this.txtTableName_V6LostFocus);
            this.txtTableName.Leave += new System.EventHandler(this.txtTableName_Leave);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = ".";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 154;
            this.label2.Text = "Bảng update";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(321, 67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 17);
            this.checkBox1.TabIndex = 156;
            this.checkBox1.Text = "Đè dữ liệu cũ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(444, 67);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 156;
            this.checkBox2.Text = "checkBox1";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = ".";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 154;
            this.label4.Text = "Trường khóa";
            // 
            // txtKeyField
            // 
            this.txtKeyField.BackColor = System.Drawing.SystemColors.Window;
            this.txtKeyField.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKeyField.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKeyField.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKeyField.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKeyField.HoverColor = System.Drawing.Color.Yellow;
            this.txtKeyField.LeaveColor = System.Drawing.Color.White;
            this.txtKeyField.Location = new System.Drawing.Point(104, 67);
            this.txtKeyField.Name = "txtKeyField";
            this.txtKeyField.Size = new System.Drawing.Size(177, 20);
            this.txtKeyField.TabIndex = 155;
            this.txtKeyField.Text = "UID";
            this.txtKeyField.TextChanged += new System.EventHandler(this.btnGenUpdateSql_Click);
            // 
            // btnGenUpdateSql
            // 
            this.btnGenUpdateSql.Location = new System.Drawing.Point(15, 163);
            this.btnGenUpdateSql.Name = "btnGenUpdateSql";
            this.btnGenUpdateSql.Size = new System.Drawing.Size(93, 23);
            this.btnGenUpdateSql.TabIndex = 2;
            this.btnGenUpdateSql.Text = "Gen Update Sql";
            this.btnGenUpdateSql.UseVisualStyleBackColor = true;
            this.btnGenUpdateSql.Click += new System.EventHandler(this.btnGenUpdateSql_Click);
            // 
            // richSQL
            // 
            this.richSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.richSQL.Location = new System.Drawing.Point(12, 221);
            this.richSQL.Name = "richSQL";
            this.richSQL.ReadOnly = true;
            this.richSQL.Size = new System.Drawing.Size(909, 35);
            this.richSQL.TabIndex = 4;
            this.richSQL.Text = "Update Table Set Field1 = @Value1, Field2=@Value2 Where Cfield = @Cvalue";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = ".";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 154;
            this.label3.Text = "Trường lọc (Where)";
            // 
            // txtFilterField
            // 
            this.txtFilterField.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilterField.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFilterField.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFilterField.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFilterField.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFilterField.HoverColor = System.Drawing.Color.Yellow;
            this.txtFilterField.LeaveColor = System.Drawing.Color.White;
            this.txtFilterField.Location = new System.Drawing.Point(114, 93);
            this.txtFilterField.Name = "txtFilterField";
            this.txtFilterField.Size = new System.Drawing.Size(167, 20);
            this.txtFilterField.TabIndex = 155;
            this.txtFilterField.TextChanged += new System.EventHandler(this.btnGenUpdateSql_Click);
            // 
            // label6
            // 
            this.label6.AccessibleDescription = ".";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 154;
            this.label6.Text = "Giá trị lọc";
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilterValue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFilterValue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFilterValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFilterValue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFilterValue.HoverColor = System.Drawing.Color.Yellow;
            this.txtFilterValue.LeaveColor = System.Drawing.Color.White;
            this.txtFilterValue.Location = new System.Drawing.Point(104, 119);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(177, 20);
            this.txtFilterValue.TabIndex = 155;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.btnGenUpdateSql_Click);
            // 
            // FormUpdateTableDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 678);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.txtFilterField);
            this.Controls.Add(this.txtKeyField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUpdateField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dicEditButton2);
            this.Controls.Add(this.txtDic1);
            this.Controls.Add(this.numUpdateLimit);
            this.Controls.Add(this.richSQL);
            this.Controls.Add(this.richStatus);
            this.Controls.Add(this.btnGenUpdateSql);
            this.Controls.Add(this.btnDoUpdate);
            this.Name = "FormUpdateTableDic";
            this.Text = "InvoiceTest";
            this.Load += new System.EventHandler(this.FormHuuanEditText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDoUpdate;
        private System.Windows.Forms.RichTextBox richStatus;
        private V6Controls.V6NumberTextBox numUpdateLimit;
        private V6Controls.V6ColorTextBox txtDic1;
        private V6Controls.Controls.DicEditButton dicEditButton2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6ColorTextBox txtUpdateField;
        private V6Controls.V6ColorTextBox txtTableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label4;
        private V6Controls.V6ColorTextBox txtKeyField;
        private System.Windows.Forms.Button btnGenUpdateSql;
        private System.Windows.Forms.RichTextBox richSQL;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6ColorTextBox txtFilterField;
        private System.Windows.Forms.Label label6;
        private V6Controls.V6ColorTextBox txtFilterValue;
    }
}