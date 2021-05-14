namespace V6AccountingB
{
    partial class FormKeyV6Online
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtSeriClient = new V6Controls.V6ColorTextBox();
            this.txtCodeName = new V6Controls.V6ColorTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(261, 275);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&Nhận";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtSeriClient
            // 
            this.txtSeriClient.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtSeriClient.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSeriClient.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSeriClient.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSeriClient.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSeriClient.HoverColor = System.Drawing.Color.Yellow;
            this.txtSeriClient.LeaveColor = System.Drawing.Color.White;
            this.txtSeriClient.Location = new System.Drawing.Point(51, 58);
            this.txtSeriClient.Multiline = true;
            this.txtSeriClient.Name = "txtSeriClient";
            this.txtSeriClient.ReadOnly = true;
            this.txtSeriClient.Size = new System.Drawing.Size(285, 93);
            this.txtSeriClient.TabIndex = 3;
            // 
            // txtCodeName
            // 
            this.txtCodeName.BackColor = System.Drawing.SystemColors.Window;
            this.txtCodeName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtCodeName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtCodeName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodeName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtCodeName.HoverColor = System.Drawing.Color.Yellow;
            this.txtCodeName.LeaveColor = System.Drawing.Color.White;
            this.txtCodeName.Location = new System.Drawing.Point(51, 157);
            this.txtCodeName.Multiline = true;
            this.txtCodeName.Name = "txtCodeName";
            this.txtCodeName.Size = new System.Drawing.Size(285, 112);
            this.txtCodeName.TabIndex = 5;
            this.txtCodeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serial client:";
            // 
            // cboDatabase
            // 
            this.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabase.Enabled = false;
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(121, 12);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(215, 21);
            this.cboDatabase.TabIndex = 1;
            this.cboDatabase.Visible = false;
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Database";
            this.label7.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(48, 280);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(210, 18);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "_  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  _  ";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(10, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "HOTLINE: 0936 976 976";
            // 
            // FormKeyV6Online
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 310);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboDatabase);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodeName);
            this.Controls.Add(this.txtSeriClient);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKeyV6Online";
            this.Text = "Mã số cài đặt máy online";
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.txtSeriClient, 0);
            this.Controls.SetChildIndex(this.txtCodeName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cboDatabase, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private V6Controls.V6ColorTextBox txtSeriClient;
        private V6Controls.V6ColorTextBox txtCodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
    }
}