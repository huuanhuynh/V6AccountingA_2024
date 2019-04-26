namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class NhomVuViecAddEditForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txtma_nh = new V6Controls.V6VvarTextBox();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Txtloai_nh = new V6Controls.V6NumberTextBox();
            this.v6ColorTextBox5 = new V6Controls.V6ColorTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtTen_nh = new V6Controls.V6ColorTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.Txtma_nh);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Txtloai_nh);
            this.groupBox1.Controls.Add(this.v6ColorTextBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TxtTen_nh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(754, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Txtma_nh
            // 
            this.Txtma_nh.AccessibleName = "ma_nh";
            this.Txtma_nh.BackColor = System.Drawing.SystemColors.Window;
            this.Txtma_nh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtma_nh.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txtma_nh.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtma_nh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtma_nh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtma_nh.HoverColor = System.Drawing.Color.Yellow;
            this.Txtma_nh.LeaveColor = System.Drawing.Color.White;
            this.Txtma_nh.Location = new System.Drawing.Point(169, 52);
            this.Txtma_nh.Name = "Txtma_nh";
            this.Txtma_nh.Size = new System.Drawing.Size(111, 23);
            this.Txtma_nh.TabIndex = 3;
            this.Txtma_nh.UseLimitCharacters0 = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "ADDEDITC00001";
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 142);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "ADDEDITL00022";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Trạng Thái";
            // 
            // Txtloai_nh
            // 
            this.Txtloai_nh.AccessibleName = "loai_nh";
            this.Txtloai_nh.BackColor = System.Drawing.SystemColors.Window;
            this.Txtloai_nh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtloai_nh.DecimalPlaces = 0;
            this.Txtloai_nh.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtloai_nh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtloai_nh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtloai_nh.HoverColor = System.Drawing.Color.Yellow;
            this.Txtloai_nh.LeaveColor = System.Drawing.Color.White;
            this.Txtloai_nh.LimitCharacters = "1;2;3";
            this.Txtloai_nh.Location = new System.Drawing.Point(169, 22);
            this.Txtloai_nh.MaxLength = 1;
            this.Txtloai_nh.Name = "Txtloai_nh";
            this.Txtloai_nh.Size = new System.Drawing.Size(111, 23);
            this.Txtloai_nh.TabIndex = 1;
            this.Txtloai_nh.Text = "0";
            this.Txtloai_nh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtloai_nh.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6ColorTextBox5
            // 
            this.v6ColorTextBox5.AccessibleName = "ten_nh2";
            this.v6ColorTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox5.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorTextBox5.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox5.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox5.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox5.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox5.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox5.Location = new System.Drawing.Point(169, 112);
            this.v6ColorTextBox5.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6ColorTextBox5.Name = "v6ColorTextBox5";
            this.v6ColorTextBox5.Size = new System.Drawing.Size(481, 23);
            this.v6ColorTextBox5.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "ADDEDITL00315";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tên 2";
            // 
            // TxtTen_nh
            // 
            this.TxtTen_nh.AccessibleName = "ten_nh";
            this.TxtTen_nh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTen_nh.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTen_nh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTen_nh.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTen_nh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTen_nh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTen_nh.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTen_nh.LeaveColor = System.Drawing.Color.White;
            this.TxtTen_nh.Location = new System.Drawing.Point(169, 82);
            this.TxtTen_nh.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.TxtTen_nh.Name = "TxtTen_nh";
            this.TxtTen_nh.Size = new System.Drawing.Size(481, 23);
            this.TxtTen_nh.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "ADDEDITL00517";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kiểu phân nhóm";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ADDEDITL00558";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã nhóm vụ việc";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ADDEDITL00314";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên nhóm \r\n";
            // 
            // NhomVuViecAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NhomVuViecAddEditForm";
            this.Size = new System.Drawing.Size(768, 196);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private V6NumberTextBox Txtloai_nh;
        private V6ColorTextBox v6ColorTextBox5;
        private System.Windows.Forms.Label label5;
        private V6ColorTextBox TxtTen_nh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private V6VvarTextBox Txtma_nh;

    }
}
