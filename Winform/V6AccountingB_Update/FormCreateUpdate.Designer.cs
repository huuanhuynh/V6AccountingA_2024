namespace V6AccountingB_Update
{
    partial class FormCreateUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateUpdate));
            this.btnMakeUpdateFile = new System.Windows.Forms.Button();
            this.buttonTestUnzip7z = new System.Windows.Forms.Button();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnMakeUpdateFile
            // 
            this.btnMakeUpdateFile.Location = new System.Drawing.Point(12, 12);
            this.btnMakeUpdateFile.Name = "btnMakeUpdateFile";
            this.btnMakeUpdateFile.Size = new System.Drawing.Size(260, 23);
            this.btnMakeUpdateFile.TabIndex = 0;
            this.btnMakeUpdateFile.Text = "Make update for ftp folder";
            this.btnMakeUpdateFile.UseVisualStyleBackColor = true;
            this.btnMakeUpdateFile.Click += new System.EventHandler(this.btnMakeUpdateFile_Click);
            // 
            // buttonTestUnzip7z
            // 
            this.buttonTestUnzip7z.Location = new System.Drawing.Point(12, 226);
            this.buttonTestUnzip7z.Name = "buttonTestUnzip7z";
            this.buttonTestUnzip7z.Size = new System.Drawing.Size(260, 23);
            this.buttonTestUnzip7z.TabIndex = 0;
            this.buttonTestUnzip7z.Text = "TestUnzip7z";
            this.buttonTestUnzip7z.UseVisualStyleBackColor = true;
            this.buttonTestUnzip7z.Visible = false;
            this.buttonTestUnzip7z.Click += new System.EventHandler(this.buttonTestUnzip7z_Click);
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(12, 41);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(86, 17);
            this.chkAutoUpdate.TabIndex = 1;
            this.chkAutoUpdate.Text = "auto_update";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // FormCreateUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chkAutoUpdate);
            this.Controls.Add(this.buttonTestUnzip7z);
            this.Controls.Add(this.btnMakeUpdateFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCreateUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateMakeFile";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMakeUpdateFile;
        private System.Windows.Forms.Button buttonTestUnzip7z;
        private System.Windows.Forms.CheckBox chkAutoUpdate;

    }
}

