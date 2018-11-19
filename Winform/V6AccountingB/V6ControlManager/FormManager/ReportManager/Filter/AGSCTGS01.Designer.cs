namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class AGSCTGS01
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
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePicker();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.btnSuaSoCTGS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00055";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00147";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Tháng";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(90, 35);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(47, 20);
            this.dateNgay_ct2.TabIndex = 24;
            this.dateNgay_ct2.ValueChanged += new System.EventHandler(this.dateNgay_ct2_ValueChanged);
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "MM";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(90, 9);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(47, 20);
            this.dateNgay_ct1.TabIndex = 22;
            this.dateNgay_ct1.ValueChanged += new System.EventHandler(this.dateNgay_ct1_ValueChanged);
            // 
            // btnChuyen
            // 
            this.btnChuyen.AccessibleDescription = "FILTERB00004";
            this.btnChuyen.Location = new System.Drawing.Point(3, 61);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(223, 23);
            this.btnChuyen.TabIndex = 25;
            this.btnChuyen.Text = "Chuyển sang tháng sau";
            this.btnChuyen.UseVisualStyleBackColor = true;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // btnSuaSoCTGS
            // 
            this.btnSuaSoCTGS.AccessibleDescription = "FILTERB00011";
            this.btnSuaSoCTGS.Location = new System.Drawing.Point(3, 90);
            this.btnSuaSoCTGS.Name = "btnSuaSoCTGS";
            this.btnSuaSoCTGS.Size = new System.Drawing.Size(223, 23);
            this.btnSuaSoCTGS.TabIndex = 25;
            this.btnSuaSoCTGS.Text = "Sửa số chứng từ ghi sổ";
            this.btnSuaSoCTGS.UseVisualStyleBackColor = true;
            this.btnSuaSoCTGS.Click += new System.EventHandler(this.btnSuaSoCTGS_Click);
            // 
            // AGSCTGS01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSuaSoCTGS);
            this.Controls.Add(this.btnChuyen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.dateNgay_ct1);
            this.Name = "AGSCTGS01";
            this.Size = new System.Drawing.Size(229, 144);
            this.Load += new System.EventHandler(this.Filter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private V6Controls.V6DateTimePicker dateNgay_ct1;
        private System.Windows.Forms.Button btnChuyen;
        private System.Windows.Forms.Button btnSuaSoCTGS;
    }
}
