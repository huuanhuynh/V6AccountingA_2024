namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    partial class ThongTinQuocTichForm
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
            this.v6Label4 = new V6Controls.V6Label();
            this.txtLastName = new V6Controls.V6ColorTextBox();
            this.v6Label17 = new V6Controls.V6Label();
            this.txtName = new V6Controls.V6LookupTextBox();
            this.txtID = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(5, 50);
            this.v6Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(106, 17);
            this.v6Label4.TabIndex = 3;
            this.v6Label4.Text = "Tên quốc tịch 2";
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
            this.txtLastName.Location = new System.Drawing.Point(139, 46);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(5);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(493, 23);
            this.txtLastName.TabIndex = 1;
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(5, 21);
            this.v6Label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(94, 17);
            this.v6Label17.TabIndex = 0;
            this.v6Label17.Text = "Tên quốc tịch";
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
            this.txtName.Location = new System.Drawing.Point(139, 19);
            this.txtName.Margin = new System.Windows.Forms.Padding(5);
            this.txtName.Name = "txtName";
            this.txtName.NeighborFields = "RELATION";
            this.txtName.ParentData = null;
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
            this.txtID.Location = new System.Drawing.Point(456, 98);
            this.txtID.Margin = new System.Windows.Forms.Padding(5);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(176, 23);
            this.txtID.TabIndex = 2;
            this.txtID.Visible = false;
            // 
            // ThongTinQuocTichForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.v6Label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongTinQuocTichForm";
            this.Size = new System.Drawing.Size(637, 165);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label v6Label4;
        private V6Controls.V6ColorTextBox txtLastName;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6LookupTextBox txtName;
        private V6ColorTextBox txtID;
    }
}
