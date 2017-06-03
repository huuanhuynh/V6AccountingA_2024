namespace V6Controls.Controls
{
    partial class GioiTinhControl
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
            this.radNam = new System.Windows.Forms.RadioButton();
            this.radNu = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radNam
            // 
            this.radNam.AccessibleName = "";
            this.radNam.AutoSize = true;
            this.radNam.Location = new System.Drawing.Point(0, 0);
            this.radNam.Margin = new System.Windows.Forms.Padding(0);
            this.radNam.Name = "radNam";
            this.radNam.Size = new System.Drawing.Size(47, 17);
            this.radNam.TabIndex = 0;
            this.radNam.TabStop = true;
            this.radNam.Text = "Nam";
            this.radNam.UseVisualStyleBackColor = true;
            // 
            // radNu
            // 
            this.radNu.AutoSize = true;
            this.radNu.Location = new System.Drawing.Point(65, 0);
            this.radNu.Margin = new System.Windows.Forms.Padding(0);
            this.radNu.Name = "radNu";
            this.radNu.Size = new System.Drawing.Size(39, 17);
            this.radNu.TabIndex = 1;
            this.radNu.TabStop = true;
            this.radNu.Text = "Nữ";
            this.radNu.UseVisualStyleBackColor = true;
            // 
            // GioiTinhControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radNam);
            this.Controls.Add(this.radNu);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GioiTinhControl";
            this.Size = new System.Drawing.Size(130, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radNam;
        private System.Windows.Forms.RadioButton radNu;
    }
}
