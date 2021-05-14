namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AGSCTGS02_F4
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.v6Label1 = new V6Controls.V6Label();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtKy1 = new V6Controls.V6NumberTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.txtKy2 = new V6Controls.V6NumberTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 156);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 156);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 4;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00009";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(29, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Năm";
            // 
            // txtNam
            // 
            this.txtNam.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.Enabled = false;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(178, 10);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.ReadOnly = true;
            this.txtNam.Size = new System.Drawing.Size(107, 20);
            this.txtNam.TabIndex = 0;
            this.txtNam.TabStop = false;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtKy1
            // 
            this.txtKy1.BackColor = System.Drawing.SystemColors.Window;
            this.txtKy1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKy1.DecimalPlaces = 0;
            this.txtKy1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKy1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKy1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKy1.HoverColor = System.Drawing.Color.Yellow;
            this.txtKy1.LeaveColor = System.Drawing.Color.White;
            this.txtKy1.Location = new System.Drawing.Point(178, 40);
            this.txtKy1.MaxLength = 2;
            this.txtKy1.MaxNumLength = 2;
            this.txtKy1.Name = "txtKy1";
            this.txtKy1.Size = new System.Drawing.Size(107, 20);
            this.txtKy1.TabIndex = 1;
            this.txtKy1.Text = "0";
            this.txtKy1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKy1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00054";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(6, 40);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(50, 13);
            this.v6Label2.TabIndex = 11;
            this.v6Label2.Text = "Từ tháng";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00055";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(6, 67);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(57, 13);
            this.v6Label3.TabIndex = 13;
            this.v6Label3.Text = "Đến tháng";
            // 
            // txtKy2
            // 
            this.txtKy2.BackColor = System.Drawing.SystemColors.Window;
            this.txtKy2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKy2.DecimalPlaces = 0;
            this.txtKy2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKy2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKy2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKy2.HoverColor = System.Drawing.Color.Yellow;
            this.txtKy2.LeaveColor = System.Drawing.Color.White;
            this.txtKy2.Location = new System.Drawing.Point(178, 67);
            this.txtKy2.MaxLength = 2;
            this.txtKy2.MaxNumLength = 2;
            this.txtKy2.Name = "txtKy2";
            this.txtKy2.Size = new System.Drawing.Size(107, 20);
            this.txtKy2.TabIndex = 2;
            this.txtKy2.Text = "0";
            this.txtKy2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKy2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "XULYL00040";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 96);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 3;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // AGSCTGS02_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 202);
            this.Controls.Add(this.txtMaDvcs);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.txtKy2);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtKy1);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AGSCTGS02_F4";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.v6Label1, 0);
            this.Controls.SetChildIndex(this.txtKy1, 0);
            this.Controls.SetChildIndex(this.txtNam, 0);
            this.Controls.SetChildIndex(this.v6Label2, 0);
            this.Controls.SetChildIndex(this.txtKy2, 0);
            this.Controls.SetChildIndex(this.v6Label3, 0);
            this.Controls.SetChildIndex(this.txtMaDvcs, 0);
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6Label v6Label1;
        public V6Controls.V6NumberTextBox txtNam;
        public V6Controls.V6NumberTextBox txtKy1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        public V6Controls.V6NumberTextBox txtKy2;
        public V6ReportControls.FilterLineVvarTextBox txtMaDvcs;




    }
}