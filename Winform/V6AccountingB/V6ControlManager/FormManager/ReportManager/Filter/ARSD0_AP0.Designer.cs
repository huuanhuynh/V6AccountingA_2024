namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ARSD0_AP0
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
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.txtMa_kh = new V6ReportControls.FilterLineVvarTextBox();
            this.SuspendLayout();
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName = "MA_DVCS";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 95);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(292, 22);
            this.txtMaDvcs.TabIndex = 20;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00002";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 21;
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(100, 33);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 24;
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
            this.dateNgay_ct1.Location = new System.Drawing.Point(100, 7);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct1.TabIndex = 22;
            // 
            // txtMa_kh
            // 
            this.txtMa_kh.AccessibleDescription = "FILTERL00007";
            this.txtMa_kh.FieldCaption = "Mã khách hàng";
            this.txtMa_kh.FieldName = "MA_KH";
            this.txtMa_kh.Location = new System.Drawing.Point(6, 67);
            this.txtMa_kh.Name = "txtMa_kh";
            this.txtMa_kh.Size = new System.Drawing.Size(292, 22);
            this.txtMa_kh.TabIndex = 25;
            this.txtMa_kh.Vvar = "MA_KH";
            // 
            // ARSD0_AP0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMa_kh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Controls.Add(this.txtMaDvcs);
            this.Name = "ARSD0_AP0";
            this.Size = new System.Drawing.Size(334, 136);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private V6ReportControls.FilterLineVvarTextBox txtMa_kh;
    }
}
