namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class ThongTinTonGiaoForm
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
            this.txtName2 = new V6Controls.V6ColorTextBox();
            this.lblTen2 = new V6Controls.V6Label();
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
            this.lblName.AccessibleDescription = "ADDEDITL00735";
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 33);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(88, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên tôn giáo";
            // 
            // txtName2
            // 
            this.txtName2.AccessibleName = "NAME2";
            this.txtName2.BackColor = System.Drawing.Color.White;
            this.txtName2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtName2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtName2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtName2.HoverColor = System.Drawing.Color.Yellow;
            this.txtName2.LeaveColor = System.Drawing.Color.White;
            this.txtName2.Location = new System.Drawing.Point(139, 59);
            this.txtName2.Margin = new System.Windows.Forms.Padding(5);
            this.txtName2.Name = "txtName2";
            this.txtName2.Size = new System.Drawing.Size(493, 23);
            this.txtName2.TabIndex = 1;
            // 
            // lblTen2
            // 
            this.lblTen2.AccessibleDescription = "ADDEDITL00736";
            this.lblTen2.AutoSize = true;
            this.lblTen2.Location = new System.Drawing.Point(5, 63);
            this.lblTen2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTen2.Name = "lblTen2";
            this.lblTen2.Size = new System.Drawing.Size(100, 17);
            this.lblTen2.TabIndex = 3;
            this.lblTen2.Text = "Tên tôn giáo 2";
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
            this.txtName.Location = new System.Drawing.Point(139, 30);
            this.txtName.Margin = new System.Windows.Forms.Padding(5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(493, 23);
            this.txtName.TabIndex = 0;
            // 
            // ThongTinTonGiaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.lblTen2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongTinTonGiaoForm";
            this.Size = new System.Drawing.Size(637, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label lblTen2;
        private V6Controls.V6ColorTextBox txtName2;
        private V6Controls.V6Label lblName;
        private V6ColorTextBox txtID;
        private V6ColorTextBox txtName;
    }
}
