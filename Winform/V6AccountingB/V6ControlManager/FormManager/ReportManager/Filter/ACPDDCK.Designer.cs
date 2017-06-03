namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ACPDDCK
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.txtMa_bpht = new V6ReportControls.FilterLineVvarTextBox();
            this.chkdc_ck = new V6Controls.V6CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Từ ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(100, 29);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 1;
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(100, 3);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 0;
            // 
            // txtMa_bpht
            // 
            this.txtMa_bpht.AccessibleDescription = "FILTERL00078";
            this.txtMa_bpht.FieldCaption = "Mã bộ phận HT";
            this.txtMa_bpht.FieldName = "MA_BPHT";
            this.txtMa_bpht.Location = new System.Drawing.Point(21, 89);
            this.txtMa_bpht.Name = "txtMa_bpht";
            this.txtMa_bpht.Size = new System.Drawing.Size(282, 22);
            this.txtMa_bpht.TabIndex = 3;
            this.txtMa_bpht.Vvar = "MA_BPHT";
            // 
            // chkdc_ck
            // 
            this.chkdc_ck.AccessibleDescription = "FILTERC00008";
            this.chkdc_ck.AutoSize = true;
            this.chkdc_ck.Location = new System.Drawing.Point(24, 66);
            this.chkdc_ck.Name = "chkdc_ck";
            this.chkdc_ck.Size = new System.Drawing.Size(132, 17);
            this.chkdc_ck.TabIndex = 2;
            this.chkdc_ck.Text = "Tính lại giá điều chỉnh";
            this.chkdc_ck.UseVisualStyleBackColor = true;
            // 
            // ACPDDCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkdc_ck);
            this.Controls.Add(this.txtMa_bpht);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Name = "ACPDDCK";
            this.Size = new System.Drawing.Size(594, 141);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtMa_bpht;
        private V6Controls.V6CheckBox chkdc_ck;

    }
}
