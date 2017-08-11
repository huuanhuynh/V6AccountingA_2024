namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class DanhMucTruongHocForm
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
            this.txtName = new V6Controls.V6ColorTextBox();
            this.txtID = new V6Controls.V6ColorTextBox();
            this.v6Label17 = new V6Controls.V6Label();
            this.txtName2 = new V6Controls.V6ColorTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.SuspendLayout();
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
            this.txtID.Location = new System.Drawing.Point(456, 69);
            this.txtID.Margin = new System.Windows.Forms.Padding(5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(176, 23);
            this.txtID.TabIndex = 2;
            this.txtID.Visible = false;
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(5, 11);
            this.v6Label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(81, 17);
            this.v6Label17.TabIndex = 0;
            this.v6Label17.Text = "Trường học";
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
            this.txtName2.Location = new System.Drawing.Point(139, 42);
            this.txtName2.Margin = new System.Windows.Forms.Padding(5);
            this.txtName2.Name = "txtName2";
            this.txtName2.Size = new System.Drawing.Size(493, 23);
            this.txtName2.TabIndex = 1;
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(5, 46);
            this.v6Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(93, 17);
            this.v6Label4.TabIndex = 3;
            this.v6Label4.Text = "Trường học 2";
            // 
            // DanhMucTruongHocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.v6Label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DanhMucTruongHocForm";
            this.Size = new System.Drawing.Size(637, 102);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label v6Label4;
        private V6Controls.V6ColorTextBox txtName2;
        private V6Controls.V6Label v6Label17;
        private V6ColorTextBox txtID;
        private V6ColorTextBox txtName;
    }
}
