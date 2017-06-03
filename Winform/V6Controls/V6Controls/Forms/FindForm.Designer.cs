namespace V6Controls.Forms
{
    partial class FindForm
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
            this.txtFindText = new V6Controls.V6ColorTextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radDown = new System.Windows.Forms.RadioButton();
            this.radUp = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFindText
            // 
            this.txtFindText.BackColor = System.Drawing.SystemColors.Window;
            this.txtFindText.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtFindText.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtFindText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFindText.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtFindText.HoverColor = System.Drawing.Color.Yellow;
            this.txtFindText.LeaveColor = System.Drawing.Color.White;
            this.txtFindText.Location = new System.Drawing.Point(12, 12);
            this.txtFindText.Name = "txtFindText";
            this.txtFindText.Size = new System.Drawing.Size(183, 20);
            this.txtFindText.TabIndex = 0;
            this.txtFindText.UseSendTabOnEnter = false;
            this.txtFindText.TextChanged += new System.EventHandler(this.txtFindText_TextChanged);
            this.txtFindText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindText_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(201, 10);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "Find next";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "V6REASKL00006";
            this.groupBox1.Controls.Add(this.radDown);
            this.groupBox1.Controls.Add(this.radUp);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // radDown
            // 
            this.radDown.AccessibleDescription = "V6REASKR00002";
            this.radDown.AutoSize = true;
            this.radDown.Checked = true;
            this.radDown.Location = new System.Drawing.Point(66, 19);
            this.radDown.Name = "radDown";
            this.radDown.Size = new System.Drawing.Size(53, 17);
            this.radDown.TabIndex = 0;
            this.radDown.TabStop = true;
            this.radDown.Text = "Down";
            this.radDown.UseVisualStyleBackColor = true;
            // 
            // radUp
            // 
            this.radUp.AccessibleDescription = "V6REASKR00001";
            this.radUp.AutoSize = true;
            this.radUp.Location = new System.Drawing.Point(6, 19);
            this.radUp.Name = "radUp";
            this.radUp.Size = new System.Drawing.Size(39, 17);
            this.radUp.TabIndex = 0;
            this.radUp.Text = "Up";
            this.radUp.UseVisualStyleBackColor = true;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtFindText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindForm";
            this.Opacity = 0.9D;
            this.Text = "Tìm kiếm";
            this.Controls.SetChildIndex(this.txtFindText, 0);
            this.Controls.SetChildIndex(this.btnFind, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6ColorTextBox txtFindText;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radDown;
        private System.Windows.Forms.RadioButton radUp;
    }
}