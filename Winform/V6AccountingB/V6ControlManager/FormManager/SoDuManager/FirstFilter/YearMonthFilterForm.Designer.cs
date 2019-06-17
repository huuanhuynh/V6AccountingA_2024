namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    partial class YearMonthFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YearMonthFilterForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYear = new V6Controls.V6NumberTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtThang = new V6Controls.V6NumberTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "V6YEARR00002";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 67);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "V6YEARR00001";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(12, 67);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(88, 32);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "&Nhận";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "V6YEARL00003";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Năm";
            // 
            // txtYear
            // 
            this.txtYear.AccessibleName = "NAM";
            this.txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYear.BackColor = System.Drawing.Color.White;
            this.txtYear.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtYear.DecimalPlaces = 0;
            this.txtYear.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtYear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtYear.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtYear.HoverColor = System.Drawing.Color.Yellow;
            this.txtYear.LeaveColor = System.Drawing.Color.White;
            this.txtYear.Location = new System.Drawing.Point(86, 12);
            this.txtYear.MaxLength = 5;
            this.txtYear.MaxNumLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(100, 20);
            this.txtYear.TabIndex = 0;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYear.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "V6YEARL00004";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tháng";
            // 
            // TxtThang
            // 
            this.TxtThang.AccessibleName = "THANG";
            this.TxtThang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtThang.BackColor = System.Drawing.Color.White;
            this.TxtThang.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtThang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtThang.DecimalPlaces = 0;
            this.TxtThang.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtThang.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtThang.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtThang.HoverColor = System.Drawing.Color.Yellow;
            this.TxtThang.LeaveColor = System.Drawing.Color.White;
            this.TxtThang.Location = new System.Drawing.Point(86, 37);
            this.TxtThang.MaxLength = 5;
            this.TxtThang.MaxNumLength = 4;
            this.TxtThang.Name = "TxtThang";
            this.TxtThang.Size = new System.Drawing.Size(100, 20);
            this.TxtThang.TabIndex = 1;
            this.TxtThang.Text = "0";
            this.TxtThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtThang.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TxtThang.TextChanged += new System.EventHandler(this.TxtThang_TextChanged);
            this.TxtThang.Leave += new System.EventHandler(this.TxtThang_Leave);
            // 
            // YearMonthFilterForm
            // 
            this.AccessibleDescription = "V6YEARR00003";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(200, 102);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtThang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.btnFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YearMonthFilterForm";
            this.ShowInTaskbar = false;
            this.Text = "Lọc năm";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnFilter, 0);
            this.Controls.SetChildIndex(this.txtYear, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.TxtThang, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
        private V6Controls.V6NumberTextBox txtYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6NumberTextBox TxtThang;
    }
}