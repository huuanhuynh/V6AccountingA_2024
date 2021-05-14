namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    partial class AlkmbFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlkmbFilterForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6ColorDateTimePick2 = new V6Controls.V6DateTimePicker();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6ColorDateTimePick1 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaDVCS = new V6Controls.V6VvarTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "FILTERB00008";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "FILTERB00007";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(12, 127);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 32);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "&Nhận";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.v6Label2);
            this.panel1.Controls.Add(this.v6ColorDateTimePick2);
            this.panel1.Controls.Add(this.v6Label1);
            this.panel1.Controls.Add(this.v6ColorDateTimePick1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMaDVCS);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 120);
            this.panel1.TabIndex = 0;
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERL00003";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(11, 38);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(53, 13);
            this.v6Label2.TabIndex = 2;
            this.v6Label2.Text = "Đến ngày";
            // 
            // v6ColorDateTimePick2
            // 
            this.v6ColorDateTimePick2.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick2.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick2.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick2.Location = new System.Drawing.Point(111, 35);
            this.v6ColorDateTimePick2.Name = "v6ColorDateTimePick2";
            this.v6ColorDateTimePick2.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick2.TabIndex = 3;
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00002";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(11, 12);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(46, 13);
            this.v6Label1.TabIndex = 0;
            this.v6Label1.Text = "Từ ngày";
            // 
            // v6ColorDateTimePick1
            // 
            this.v6ColorDateTimePick1.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick1.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick1.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.Location = new System.Drawing.Point(111, 9);
            this.v6ColorDateTimePick1.Name = "v6ColorDateTimePick1";
            this.v6ColorDateTimePick1.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00005";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã đơn vị";
            // 
            // txtMaDvcs
            // 
            this.txtMaDVCS.AccessibleName = "MA_DVCS";
            this.txtMaDVCS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaDVCS.BackColor = System.Drawing.Color.White;
            this.txtMaDVCS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaDVCS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaDVCS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaDVCS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaDVCS.LeaveColor = System.Drawing.Color.White;
            this.txtMaDVCS.Location = new System.Drawing.Point(111, 61);
            this.txtMaDVCS.Name = "txtMaDVCS";
            this.txtMaDVCS.Size = new System.Drawing.Size(202, 20);
            this.txtMaDVCS.TabIndex = 5;
            this.txtMaDVCS.VVar = "MA_DVCS";
            // 
            // AlkmbFilterForm
            // 
            this.AccessibleDescription = "FILTERF00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 162);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "AlkmbFilterForm";
            this.ShowInTaskbar = false;
            this.Text = "Lọc";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnFilter, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private V6Controls.V6VvarTextBox txtMaDVCS;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6DateTimePicker v6ColorDateTimePick2;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6DateTimePicker v6ColorDateTimePick1;
    }
}