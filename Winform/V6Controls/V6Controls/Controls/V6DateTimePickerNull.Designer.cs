namespace V6Controls.Controls
{
    partial class V6DateTimePickerNull
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
            this.date1 = new V6Controls.V6DateTimeFullPicker();
            this.chkUSE = new V6Controls.V6CheckBox();
            this.txtSoCtKemt = new V6Controls.V6ColorTextBox();
            this.SuspendLayout();
            // 
            // date1
            // 
            this.date1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.date1.CustomFormat = "HH:mm dd/MM/yyyy";
            this.date1.EnterColor = System.Drawing.Color.PaleGreen;
            this.date1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date1.HoverColor = System.Drawing.Color.Yellow;
            this.date1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.date1.LeaveColor = System.Drawing.Color.White;
            this.date1.Location = new System.Drawing.Point(0, 0);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(130, 20);
            this.date1.TabIndex = 1;
            this.date1.UseTime = true;
            this.date1.Visible = false;
            this.date1.ValueChanged += new System.EventHandler(this.date1_ValueChanged);
            // 
            // chkUSE
            // 
            this.chkUSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUSE.AutoSize = true;
            this.chkUSE.Location = new System.Drawing.Point(133, 3);
            this.chkUSE.Name = "chkUSE";
            this.chkUSE.Size = new System.Drawing.Size(15, 14);
            this.chkUSE.TabIndex = 0;
            this.chkUSE.TabStop = false;
            this.chkUSE.UseVisualStyleBackColor = true;
            this.chkUSE.CheckedChanged += new System.EventHandler(this.chkUSE_CheckedChanged);
            // 
            // txtSoCtKemt
            // 
            this.txtSoCtKemt.BackColor = System.Drawing.Color.White;
            this.txtSoCtKemt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCtKemt.Enabled = false;
            this.txtSoCtKemt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCtKemt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCtKemt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCtKemt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCtKemt.LeaveColor = System.Drawing.Color.White;
            this.txtSoCtKemt.Location = new System.Drawing.Point(0, 0);
            this.txtSoCtKemt.Margin = new System.Windows.Forms.Padding(5);
            this.txtSoCtKemt.Name = "txtSoCtKemt";
            this.txtSoCtKemt.Size = new System.Drawing.Size(130, 20);
            this.txtSoCtKemt.TabIndex = 2;
            this.txtSoCtKemt.Tag = "cancelall";
            this.txtSoCtKemt.Text = "__:__ __/__/____";
            // 
            // V6DateTimePickerNull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkUSE);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.txtSoCtKemt);
            this.Name = "V6DateTimePickerNull";
            this.Size = new System.Drawing.Size(150, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6DateTimeFullPicker date1;
        private V6CheckBox chkUSE;
        private V6ColorTextBox txtSoCtKemt;
    }
}
