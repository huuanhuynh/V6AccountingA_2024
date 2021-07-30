namespace V6ControlManager.FormManager.NhanSu.Filter
{
    partial class HHRINFOR1_filter
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
            this.v6Label9 = new V6Controls.V6Label();
            this.txtSttRec = new V6Controls.V6VvarTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txtten_ns = new V6Controls.V6ColorTextBox();
            this.lineMaNS = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00004";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(8, 5);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(70, 13);
            this.v6Label9.TabIndex = 4;
            this.v6Label9.Text = "Mã  chứng từ";
            this.v6Label9.Visible = false;
            // 
            // txtSttRec
            // 
            this.txtSttRec.AccessibleName = "STT_REC";
            this.txtSttRec.BackColor = System.Drawing.SystemColors.Window;
            this.txtSttRec.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSttRec.CheckNotEmpty = true;
            this.txtSttRec.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSttRec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.HoverColor = System.Drawing.Color.Yellow;
            this.txtSttRec.LeaveColor = System.Drawing.Color.White;
            this.txtSttRec.Location = new System.Drawing.Point(112, 3);
            this.txtSttRec.Name = "txtSttRec";
            this.txtSttRec.Size = new System.Drawing.Size(141, 20);
            this.txtSttRec.TabIndex = 5;
            this.txtSttRec.Visible = false;
            this.txtSttRec.VVar = "MA_CT";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Txtten_ns);
            this.groupBox1.Controls.Add(this.lineMaNS);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Location = new System.Drawing.Point(0, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // Txtten_ns
            // 
            this.Txtten_ns.AccessibleName = "";
            this.Txtten_ns.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Txtten_ns.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtten_ns.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtten_ns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txtten_ns.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.HoverColor = System.Drawing.Color.Yellow;
            this.Txtten_ns.LeaveColor = System.Drawing.Color.White;
            this.Txtten_ns.Location = new System.Drawing.Point(5, 74);
            this.Txtten_ns.Margin = new System.Windows.Forms.Padding(4);
            this.Txtten_ns.Multiline = true;
            this.Txtten_ns.Name = "Txtten_ns";
            this.Txtten_ns.ReadOnly = true;
            this.Txtten_ns.Size = new System.Drawing.Size(252, 61);
            this.Txtten_ns.TabIndex = 44;
            this.Txtten_ns.TabStop = false;
            // 
            // lineMaNS
            // 
            this.lineMaNS.AccessibleDescription = "FILTERL00235";
            this.lineMaNS.AccessibleName2 = "MA_NS";
            this.lineMaNS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lineMaNS.Caption = "Mã nhân sự";
            this.lineMaNS.FieldName = "MA_NS";
            this.lineMaNS.Location = new System.Drawing.Point(2, 47);
            this.lineMaNS.Name = "lineMaNS";
            this.lineMaNS.Size = new System.Drawing.Size(257, 22);
            this.lineMaNS.TabIndex = 2;
            this.lineMaNS.Vvar = "MA_NS";
            this.lineMaNS.Leave += new System.EventHandler(this.lineMaNS_Leave);
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(133, 18);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(47, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "(OR)";
            this.radOr.UseVisualStyleBackColor = true;
            this.radOr.Visible = false;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(4, 18);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(54, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "(AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            this.radAnd.Visible = false;
            // 
            // HHRINFOR1_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.txtSttRec);
            this.Controls.Add(this.groupBox1);
            this.Name = "HHRINFOR1_filter";
            this.Size = new System.Drawing.Size(265, 174);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox lineMaNS;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox txtSttRec;
        private V6Controls.V6ColorTextBox Txtten_ns;
    }
}
