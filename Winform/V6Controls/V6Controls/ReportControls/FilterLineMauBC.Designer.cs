namespace V6ReportControls
{
    partial class FilterLineMauBC
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
            this.txtma_maubc = new V6Controls.V6VvarTextBox();
            this.cboMaubc = new V6Controls.V6ComboBox();
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
            // txtma_maubc
            // 
            this.txtma_maubc.AccessibleName = "MA_MAUBC";
            this.txtma_maubc.BackColor = System.Drawing.SystemColors.Window;
            this.txtma_maubc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtma_maubc.CheckNotEmpty = true;
            this.txtma_maubc.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtma_maubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtma_maubc.HoverColor = System.Drawing.Color.Yellow;
            this.txtma_maubc.LeaveColor = System.Drawing.Color.White;
            this.txtma_maubc.Location = new System.Drawing.Point(236, 30);
            this.txtma_maubc.Name = "txtma_maubc";
            this.txtma_maubc.Size = new System.Drawing.Size(44, 20);
            this.txtma_maubc.TabIndex = 33;
            this.txtma_maubc.Visible = false;
            // 
            // cboMaubc
            // 
            this.cboMaubc.AccessibleName = "MAU_BC";
            this.cboMaubc.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaubc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaubc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaubc.FormattingEnabled = true;
            this.cboMaubc.Location = new System.Drawing.Point(65, 0);
            this.cboMaubc.Name = "cboMaubc";
            this.cboMaubc.Size = new System.Drawing.Size(231, 21);
            this.cboMaubc.TabIndex = 32;
            this.cboMaubc.TabStop = false;
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
            // FilterLineMauBC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSuaCTMau);
            this.Controls.Add(this.btnSuaTTMau);
            this.Controls.Add(this.btnThemMau);
            this.Controls.Add(this.txtma_maubc);
            this.Controls.Add(this.cboMaubc);
            this.Controls.Add(this.lblMau);
            this.Name = "FilterLineMauBC";
            this.Size = new System.Drawing.Size(308, 50);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.lblMau, 0);
            this.Controls.SetChildIndex(this.cboMaubc, 0);
            this.Controls.SetChildIndex(this.txtma_maubc, 0);
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
        private V6Controls.V6VvarTextBox txtma_maubc;
        private V6Controls.V6ComboBox cboMaubc;
        private V6Controls.V6Label lblMau;

    }
}
