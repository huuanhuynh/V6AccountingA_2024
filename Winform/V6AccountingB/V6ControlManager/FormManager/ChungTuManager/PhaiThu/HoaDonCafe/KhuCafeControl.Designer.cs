namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    partial class KhuCafeControl
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
            this.panelTable = new System.Windows.Forms.Panel();
            this.panelInvoice = new System.Windows.Forms.Panel();
            this.txtMaVitri = new V6Controls.V6VvarTextBox();
            this.tsNew = new System.Windows.Forms.Button();
            this.tsFull = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelTable
            // 
            this.panelTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTable.AutoScroll = true;
            this.panelTable.Location = new System.Drawing.Point(99, 0);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(601, 90);
            this.panelTable.TabIndex = 0;
            // 
            // panelInvoice
            // 
            this.panelInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInvoice.Location = new System.Drawing.Point(0, 90);
            this.panelInvoice.Name = "panelInvoice";
            this.panelInvoice.Size = new System.Drawing.Size(700, 410);
            this.panelInvoice.TabIndex = 1;
            // 
            // txtMaVitri
            // 
            this.txtMaVitri.AccessibleName = "";
            this.txtMaVitri.BackColor = System.Drawing.Color.White;
            this.txtMaVitri.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaVitri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaVitri.BrotherFields = "ten_dvcs";
            this.txtMaVitri.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaVitri.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaVitri.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaVitri.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaVitri.LeaveColor = System.Drawing.Color.White;
            this.txtMaVitri.Location = new System.Drawing.Point(0, 3);
            this.txtMaVitri.Name = "txtMaVitri";
            this.txtMaVitri.Size = new System.Drawing.Size(93, 20);
            this.txtMaVitri.TabIndex = 6;
            this.txtMaVitri.VVar = "ma_vitri";
            this.txtMaVitri.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMaVitri_V6LostFocus);
            // 
            // tsNew
            // 
            this.tsNew.FlatAppearance.BorderSize = 0;
            this.tsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tsNew.Image = global::V6ControlManager.Properties.Resources.Add24;
            this.tsNew.Location = new System.Drawing.Point(3, 29);
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(26, 26);
            this.tsNew.TabIndex = 7;
            this.tsNew.UseVisualStyleBackColor = true;
            this.tsNew.Visible = false;
            // 
            // tsFull
            // 
            this.tsFull.FlatAppearance.BorderSize = 0;
            this.tsFull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tsFull.Image = global::V6ControlManager.Properties.Resources.ZoomIn24;
            this.tsFull.Location = new System.Drawing.Point(35, 29);
            this.tsFull.Name = "tsFull";
            this.tsFull.Size = new System.Drawing.Size(26, 26);
            this.tsFull.TabIndex = 8;
            this.tsFull.UseVisualStyleBackColor = true;
            this.tsFull.Click += new System.EventHandler(this.tsFull_Click);
            // 
            // KhuCafeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsFull);
            this.Controls.Add(this.tsNew);
            this.Controls.Add(this.txtMaVitri);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelInvoice);
            this.Name = "KhuCafeControl";
            this.Size = new System.Drawing.Size(700, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Panel panelInvoice;
        private V6Controls.V6VvarTextBox txtMaVitri;
        private System.Windows.Forms.Button tsNew;
        private System.Windows.Forms.Button tsFull;
    }
}
