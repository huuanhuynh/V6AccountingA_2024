namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ASOBCGIA2
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
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.txtma_gia = new V6ReportControls.FilterLineVvarTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đến ngày";
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
            this.dateNgay_ct2.Location = new System.Drawing.Point(138, 16);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(100, 20);
            this.dateNgay_ct2.TabIndex = 7;
            // 
            // txtma_gia
            // 
            this.txtma_gia.AccessibleDescription = "FILTERL00186";
            this.txtma_gia.FieldCaption = "Mã giá";
            this.txtma_gia.FieldName = "MA_GIA";
            this.txtma_gia.Location = new System.Drawing.Point(5, 52);
            this.txtma_gia.Name = "txtma_gia";
            this.txtma_gia.Size = new System.Drawing.Size(281, 23);
            this.txtma_gia.TabIndex = 6;
            this.txtma_gia.Vvar = "MA_GIA";
            // 
            // ASOBCGIA2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtma_gia);
            this.Controls.Add(this.dateNgay_ct2);
            this.Name = "ASOBCGIA2";
            this.Size = new System.Drawing.Size(303, 133);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private V6ReportControls.FilterLineVvarTextBox txtma_gia;
    }
}
