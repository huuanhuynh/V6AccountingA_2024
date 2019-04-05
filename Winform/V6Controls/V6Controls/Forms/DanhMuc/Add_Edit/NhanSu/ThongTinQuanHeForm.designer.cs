namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class ThongTinQuanHeForm
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
            this.lblName2 = new V6Controls.V6Label();
            this.txtLastName = new V6Controls.V6ColorTextBox();
            this.lblPhuThuoc = new V6Controls.V6Label();
            this.lblName = new V6Controls.V6Label();
            this.chkPhuThuoc = new System.Windows.Forms.CheckBox();
            this.txtID = new V6Controls.V6ColorTextBox();
            this.lblNhom = new V6Controls.V6Label();
            this.radclass2 = new System.Windows.Forms.RadioButton();
            this.radclass1 = new System.Windows.Forms.RadioButton();
            this.txtclass = new V6Controls.V6ColorTextBox();
            this.txtName = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // lblName2
            // 
            this.lblName2.AccessibleDescription = "ADDEDITL00734";
            this.lblName2.AutoSize = true;
            this.lblName2.Location = new System.Drawing.Point(5, 40);
            this.lblName2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(127, 17);
            this.lblName2.TabIndex = 3;
            this.lblName2.Text = "Tên mối quan hệ 2";
            // 
            // txtLastName
            // 
            this.txtLastName.AccessibleName = "NAME2";
            this.txtLastName.BackColor = System.Drawing.Color.White;
            this.txtLastName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLastName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLastName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLastName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLastName.HoverColor = System.Drawing.Color.Yellow;
            this.txtLastName.LeaveColor = System.Drawing.Color.White;
            this.txtLastName.Location = new System.Drawing.Point(139, 36);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(5);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(493, 23);
            this.txtLastName.TabIndex = 1;
            // 
            // lblPhuThuoc
            // 
            this.lblPhuThuoc.AccessibleDescription = "";
            this.lblPhuThuoc.AutoSize = true;
            this.lblPhuThuoc.Location = new System.Drawing.Point(5, 63);
            this.lblPhuThuoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhuThuoc.Name = "lblPhuThuoc";
            this.lblPhuThuoc.Size = new System.Drawing.Size(72, 17);
            this.lblPhuThuoc.TabIndex = 5;
            this.lblPhuThuoc.Text = "Phụ thuộc";
            // 
            // lblName
            // 
            this.lblName.AccessibleDescription = "ADDEDITL00733";
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 11);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(115, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên mối quan hệ";
            // 
            // chkPhuThuoc
            // 
            this.chkPhuThuoc.AutoSize = true;
            this.chkPhuThuoc.Location = new System.Drawing.Point(139, 66);
            this.chkPhuThuoc.Margin = new System.Windows.Forms.Padding(4);
            this.chkPhuThuoc.Name = "chkPhuThuoc";
            this.chkPhuThuoc.Size = new System.Drawing.Size(15, 14);
            this.chkPhuThuoc.TabIndex = 2;
            this.chkPhuThuoc.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.AccessibleName = "ID";
            this.txtID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtID.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtID.Enabled = false;
            this.txtID.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtID.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtID.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtID.GrayText = "ID";
            this.txtID.HoverColor = System.Drawing.Color.Yellow;
            this.txtID.LeaveColor = System.Drawing.Color.White;
            this.txtID.Location = new System.Drawing.Point(456, 83);
            this.txtID.Margin = new System.Windows.Forms.Padding(5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(176, 23);
            this.txtID.TabIndex = 2;
            this.txtID.Visible = false;
            // 
            // lblNhom
            // 
            this.lblNhom.AccessibleDescription = "";
            this.lblNhom.AutoSize = true;
            this.lblNhom.Location = new System.Drawing.Point(5, 89);
            this.lblNhom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhom.Name = "lblNhom";
            this.lblNhom.Size = new System.Drawing.Size(49, 17);
            this.lblNhom.TabIndex = 7;
            this.lblNhom.Text = "Nhóm ";
            // 
            // radclass2
            // 
            this.radclass2.AutoSize = true;
            this.radclass2.Location = new System.Drawing.Point(139, 114);
            this.radclass2.Name = "radclass2";
            this.radclass2.Size = new System.Drawing.Size(143, 21);
            this.radclass2.TabIndex = 4;
            this.radclass2.TabStop = true;
            this.radclass2.Text = "Bố mẹ, anh chị em";
            this.radclass2.UseVisualStyleBackColor = true;
            // 
            // radclass1
            // 
            this.radclass1.AutoSize = true;
            this.radclass1.Checked = true;
            this.radclass1.Location = new System.Drawing.Point(139, 87);
            this.radclass1.Name = "radclass1";
            this.radclass1.Size = new System.Drawing.Size(139, 21);
            this.radclass1.TabIndex = 3;
            this.radclass1.TabStop = true;
            this.radclass1.Text = "Vợ chồng, con cái";
            this.radclass1.UseVisualStyleBackColor = true;
            // 
            // txtclass
            // 
            this.txtclass.AccessibleName = "CLASS";
            this.txtclass.BackColor = System.Drawing.Color.White;
            this.txtclass.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtclass.Enabled = false;
            this.txtclass.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtclass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtclass.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtclass.HoverColor = System.Drawing.Color.Yellow;
            this.txtclass.LeaveColor = System.Drawing.Color.White;
            this.txtclass.Location = new System.Drawing.Point(456, 112);
            this.txtclass.Margin = new System.Windows.Forms.Padding(5);
            this.txtclass.Name = "txtclass";
            this.txtclass.Size = new System.Drawing.Size(16, 23);
            this.txtclass.TabIndex = 18;
            this.txtclass.Visible = false;
            this.txtclass.TextChanged += new System.EventHandler(this.txtclass_TextChanged);
            // 
            // txtName
            // 
            this.txtName.AccessibleName = "NAME";
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtName.HoverColor = System.Drawing.Color.Yellow;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(139, 8);
            this.txtName.Margin = new System.Windows.Forms.Padding(5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(493, 23);
            this.txtName.TabIndex = 0;
            // 
            // ThongTinQuanHeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtclass);
            this.Controls.Add(this.radclass2);
            this.Controls.Add(this.radclass1);
            this.Controls.Add(this.lblNhom);
            this.Controls.Add(this.chkPhuThuoc);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblPhuThuoc);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblName2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongTinQuanHeForm";
            this.Size = new System.Drawing.Size(637, 148);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label lblName2;
        private V6Controls.V6ColorTextBox txtLastName;
        private V6Controls.V6Label lblPhuThuoc;
        private V6Controls.V6Label lblName;
        private System.Windows.Forms.CheckBox chkPhuThuoc;
        private V6ColorTextBox txtID;
        private V6Label lblNhom;
        private System.Windows.Forms.RadioButton radclass2;
        private System.Windows.Forms.RadioButton radclass1;
        private V6ColorTextBox txtclass;
        private V6ColorTextBox txtName;
    }
}
