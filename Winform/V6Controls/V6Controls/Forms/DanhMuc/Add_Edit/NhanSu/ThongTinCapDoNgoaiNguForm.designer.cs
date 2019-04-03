namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class ThongTinCapDoNgoaiNguForm
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
            this.txtID = new V6Controls.V6ColorTextBox();
            this.lblName = new V6Controls.V6Label();
            this.txtLastName = new V6Controls.V6ColorTextBox();
            this.lblLastName = new V6Controls.V6Label();
            this.txtName = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
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
            this.txtID.Location = new System.Drawing.Point(456, 98);
            this.txtID.Margin = new System.Windows.Forms.Padding(5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(176, 23);
            this.txtID.TabIndex = 2;
            this.txtID.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AccessibleDescription = "";
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 33);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(147, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên cấp độ ngoại ngữ";
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
            this.txtLastName.Location = new System.Drawing.Point(167, 59);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(5);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(490, 23);
            this.txtLastName.TabIndex = 1;
            // 
            // lblLastName
            // 
            this.lblLastName.AccessibleDescription = "";
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(5, 63);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(159, 17);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "Tên cấp độ ngoại ngữ 2";
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
            this.txtName.Location = new System.Drawing.Point(167, 30);
            this.txtName.Margin = new System.Windows.Forms.Padding(5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(490, 23);
            this.txtName.TabIndex = 0;
            // 
            // ThongTinCapDoNgoaiNguForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongTinCapDoNgoaiNguForm";
            this.Size = new System.Drawing.Size(663, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label lblLastName;
        private V6Controls.V6ColorTextBox txtLastName;
        private V6Controls.V6Label lblName;
        private V6ColorTextBox txtID;
        private V6ColorTextBox txtName;
    }
}
