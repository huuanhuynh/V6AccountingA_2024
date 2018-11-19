namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    partial class ZACOSXLT_TINHGIA_Filter
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
            this.chkTinhGiaDC = new V6Controls.V6CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineMaBpHt = new V6ReportControls.FilterLineVvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkTinhGiaDC
            // 
            this.chkTinhGiaDC.AccessibleDescription = "XULYC00010";
            this.chkTinhGiaDC.AutoSize = true;
            this.chkTinhGiaDC.Location = new System.Drawing.Point(117, 51);
            this.chkTinhGiaDC.Name = "chkTinhGiaDC";
            this.chkTinhGiaDC.Size = new System.Drawing.Size(119, 17);
            this.chkTinhGiaDC.TabIndex = 4;
            this.chkTinhGiaDC.Text = "Tính giá điều chỉnh";
            this.chkTinhGiaDC.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 86);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(295, 164);
            this.listBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "V6YEARL00006";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "V6YEARL00005";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(117, 25);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(117, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "V6REASKL00007";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lineMaBpHt);
            this.groupBox1.Location = new System.Drawing.Point(0, 265);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 112);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tùy chọn";
            // 
            // lineMaBpHt
            // 
            this.lineMaBpHt.AccessibleDescription = "XULYL00026";
            this.lineMaBpHt.Caption = "Mã bộ phận ht";
            this.lineMaBpHt.FieldName = "MA_BPHT";
            this.lineMaBpHt.Location = new System.Drawing.Point(6, 19);
            this.lineMaBpHt.Name = "lineMaBpHt";
            this.lineMaBpHt.Size = new System.Drawing.Size(280, 22);
            this.lineMaBpHt.TabIndex = 0;
            this.lineMaBpHt.Vvar = "MA_BPHT";
            // 
            // ZACOSXLT_TINHGIA_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkTinhGiaDC);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ZACOSXLT_TINHGIA_Filter";
            this.Size = new System.Drawing.Size(298, 380);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox lineMaBpHt;
        private V6Controls.V6CheckBox chkTinhGiaDC;
    }
}
