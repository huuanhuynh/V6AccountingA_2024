namespace V6ReportControls
{
    partial class FilterLineMauALL
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
            this.btnSuaCTMau = new V6Controls.Controls.V6FormButton();
            this.btnSuaTTMau = new V6Controls.Controls.V6FormButton();
            this.btnThemMau = new V6Controls.Controls.V6FormButton();
            this.txtMa = new V6Controls.V6VvarTextBox();
            this.cboMau = new V6Controls.V6ComboBox();
            this.lblMau = new V6Controls.V6Label();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(7, 3);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 3);
            // 
            // btnSuaCTMau
            // 
            this.btnSuaCTMau.AccessibleDescription = "REPORTB00003";
            this.btnSuaCTMau.Location = new System.Drawing.Point(153, 27);
            this.btnSuaCTMau.Name = "btnSuaCTMau";
            this.btnSuaCTMau.Size = new System.Drawing.Size(48, 23);
            this.btnSuaCTMau.TabIndex = 34;
            this.btnSuaCTMau.Text = "Sửa ct";
            this.btnSuaCTMau.UseVisualStyleBackColor = true;
            this.btnSuaCTMau.Click += new System.EventHandler(this.btnSuaCTMau_Click);
            // 
            // btnSuaTTMau
            // 
            this.btnSuaTTMau.AccessibleDescription = "REPORTB00001";
            this.btnSuaTTMau.Location = new System.Drawing.Point(109, 27);
            this.btnSuaTTMau.Name = "btnSuaTTMau";
            this.btnSuaTTMau.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMau.TabIndex = 35;
            this.btnSuaTTMau.Text = "Sửa tt";
            this.btnSuaTTMau.UseVisualStyleBackColor = true;
            this.btnSuaTTMau.Click += new System.EventHandler(this.btnSuaTTMau_Click);
            // 
            // btnThemMau
            // 
            this.btnThemMau.AccessibleDescription = "REPORTB00002";
            this.btnThemMau.Location = new System.Drawing.Point(65, 27);
            this.btnThemMau.Name = "btnThemMau";
            this.btnThemMau.Size = new System.Drawing.Size(43, 23);
            this.btnThemMau.TabIndex = 36;
            this.btnThemMau.Text = "Thêm";
            this.btnThemMau.UseVisualStyleBackColor = true;
            this.btnThemMau.Click += new System.EventHandler(this.btnThemMau_Click);
            // 
            // txtMa
            // 
            this.txtMa.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa.CheckNotEmpty = true;
            this.txtMa.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa.LeaveColor = System.Drawing.Color.White;
            this.txtMa.Location = new System.Drawing.Point(236, 30);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(44, 20);
            this.txtMa.TabIndex = 33;
            this.txtMa.Visible = false;
            // 
            // cboMau
            // 
            this.cboMau.AccessibleName = "MAU_BC";
            this.cboMau.BackColor = System.Drawing.SystemColors.Window;
            this.cboMau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMau.DropDownWidth = 400;
            this.cboMau.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMau.FormattingEnabled = true;
            this.cboMau.Location = new System.Drawing.Point(65, 0);
            this.cboMau.Name = "cboMau";
            this.cboMau.Size = new System.Drawing.Size(231, 21);
            this.cboMau.TabIndex = 32;
            this.cboMau.TabStop = false;
            // 
            // lblMau
            // 
            this.lblMau.AccessibleDescription = "FILTERL00138";
            this.lblMau.AutoSize = true;
            this.lblMau.Location = new System.Drawing.Point(7, 3);
            this.lblMau.Name = "lblMau";
            this.lblMau.Size = new System.Drawing.Size(28, 13);
            this.lblMau.TabIndex = 31;
            this.lblMau.Text = "Mẫu";
            // 
            // FilterLineMauALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSuaCTMau);
            this.Controls.Add(this.btnSuaTTMau);
            this.Controls.Add(this.btnThemMau);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.cboMau);
            this.Controls.Add(this.lblMau);
            this.Name = "FilterLineMauALL";
            this.Size = new System.Drawing.Size(308, 50);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.lblMau, 0);
            this.Controls.SetChildIndex(this.cboMau, 0);
            this.Controls.SetChildIndex(this.txtMa, 0);
            this.Controls.SetChildIndex(this.btnThemMau, 0);
            this.Controls.SetChildIndex(this.btnSuaTTMau, 0);
            this.Controls.SetChildIndex(this.btnSuaCTMau, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.Controls.V6FormButton btnSuaCTMau;
        private V6Controls.Controls.V6FormButton btnSuaTTMau;
        private V6Controls.Controls.V6FormButton btnThemMau;
        private V6Controls.V6VvarTextBox txtMa;
        private V6Controls.V6ComboBox cboMau;
        private V6Controls.V6Label lblMau;

    }
}
