namespace V6Controls.Forms.Viewer
{
    partial class ExpressionsCalculatorForm
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
            this.txtBieuThuc = new System.Windows.Forms.RichTextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.lblSelectionStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBieuThuc
            // 
            this.txtBieuThuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBieuThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBieuThuc.Location = new System.Drawing.Point(12, 29);
            this.txtBieuThuc.Name = "txtBieuThuc";
            this.txtBieuThuc.Size = new System.Drawing.Size(382, 156);
            this.txtBieuThuc.TabIndex = 5;
            this.txtBieuThuc.Text = "";
            this.txtBieuThuc.SelectionChanged += new System.EventHandler(this.txtBieuThuc_SelectionChanged);
            this.txtBieuThuc.TextChanged += new System.EventHandler(this.txtBieuThuc_TextChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleDescription = ".";
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(319, 190);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 26);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtKetQua
            // 
            this.txtKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKetQua.Location = new System.Drawing.Point(12, 191);
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.ReadOnly = true;
            this.txtKetQua.Size = new System.Drawing.Size(301, 26);
            this.txtKetQua.TabIndex = 8;
            // 
            // lblSelectionStatus
            // 
            this.lblSelectionStatus.AccessibleDescription = ".";
            this.lblSelectionStatus.AutoSize = true;
            this.lblSelectionStatus.Location = new System.Drawing.Point(12, 9);
            this.lblSelectionStatus.Name = "lblSelectionStatus";
            this.lblSelectionStatus.Size = new System.Drawing.Size(52, 13);
            this.lblSelectionStatus.TabIndex = 9;
            this.lblSelectionStatus.Text = "Length=0";
            // 
            // ExpressionsCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 223);
            this.Controls.Add(this.lblSelectionStatus);
            this.Controls.Add(this.txtKetQua);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtBieuThuc);
            this.Name = "ExpressionsCalculatorForm";
            this.Text = "Máy tính biểu thức";
            this.Controls.SetChildIndex(this.txtBieuThuc, 0);
            this.Controls.SetChildIndex(this.btnCopy, 0);
            this.Controls.SetChildIndex(this.txtKetQua, 0);
            this.Controls.SetChildIndex(this.lblSelectionStatus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBieuThuc;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtKetQua;
        private System.Windows.Forms.Label lblSelectionStatus;
    }
}