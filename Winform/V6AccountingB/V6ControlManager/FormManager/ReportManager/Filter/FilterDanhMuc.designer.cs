namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class FilterDanhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDanhMuc));
            this.date2 = new V6Controls.V6DateTimeColor();
            this.date1 = new V6Controls.V6DateTimeColor();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.panel1 = new V6ReportControls.PanelFilter();
            this.dateSelectButton2 = new V6Controls.Controls.DateSelectButton();
            this.dateSelectButton1 = new V6Controls.Controls.DateSelectButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // date2
            // 
            this.date2.AccessibleName = "ngay_td1";
            this.date2.BackColor = System.Drawing.Color.White;
            this.date2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.date2.EnterColor = System.Drawing.Color.PaleGreen;
            this.date2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.date2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.date2.GrayText = null;
            this.date2.HoverColor = System.Drawing.Color.Yellow;
            this.date2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.date2.LeaveColor = System.Drawing.Color.White;
            this.date2.Location = new System.Drawing.Point(140, 32);
            this.date2.Margin = new System.Windows.Forms.Padding(5);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(109, 20);
            this.date2.StringValue = "__/__/____";
            this.date2.TabIndex = 4;
            this.date2.Text = "__/__/____";
            // 
            // date1
            // 
            this.date1.AccessibleName = "ngay_td1";
            this.date1.BackColor = System.Drawing.Color.White;
            this.date1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.date1.EnterColor = System.Drawing.Color.PaleGreen;
            this.date1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.date1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.date1.GrayText = null;
            this.date1.HoverColor = System.Drawing.Color.Yellow;
            this.date1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.date1.LeaveColor = System.Drawing.Color.White;
            this.date1.Location = new System.Drawing.Point(140, 6);
            this.date1.Margin = new System.Windows.Forms.Padding(5);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(109, 20);
            this.date1.StringValue = "__/__/____";
            this.date1.TabIndex = 1;
            this.date1.Text = "__/__/____";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(2, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 467);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(177, 0);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
            this.radOr.Text = "Điều kiện (OR)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(74, 0);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.LeftMargin = 0;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 448);
            this.panel1.TabIndex = 2;
            // 
            // dateSelectButton2
            // 
            this.dateSelectButton2.Image = ((System.Drawing.Image)(resources.GetObject("dateSelectButton2.Image")));
            this.dateSelectButton2.Location = new System.Drawing.Point(249, 32);
            this.dateSelectButton2.Name = "dateSelectButton2";
            this.dateSelectButton2.ReferenceControl = this.date2;
            this.dateSelectButton2.Size = new System.Drawing.Size(21, 21);
            this.dateSelectButton2.TabIndex = 5;
            // 
            // dateSelectButton1
            // 
            this.dateSelectButton1.Image = ((System.Drawing.Image)(resources.GetObject("dateSelectButton1.Image")));
            this.dateSelectButton1.Location = new System.Drawing.Point(249, 6);
            this.dateSelectButton1.Name = "dateSelectButton1";
            this.dateSelectButton1.ReferenceControl = this.date1;
            this.dateSelectButton1.Size = new System.Drawing.Size(21, 21);
            this.dateSelectButton1.TabIndex = 2;
            // 
            // FilterDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.dateSelectButton2);
            this.Controls.Add(this.dateSelectButton1);
            this.Controls.Add(this.date2);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FilterDanhMuc";
            this.Size = new System.Drawing.Size(285, 530);
            this.Load += new System.EventHandler(this.FilterDanhMuc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimeColor date1;
        private V6Controls.V6DateTimeColor date2;
        private V6ReportControls.PanelFilter panel1;
        private V6Controls.Controls.DateSelectButton dateSelectButton2;
        private V6Controls.Controls.DateSelectButton dateSelectButton1;
    }
}
