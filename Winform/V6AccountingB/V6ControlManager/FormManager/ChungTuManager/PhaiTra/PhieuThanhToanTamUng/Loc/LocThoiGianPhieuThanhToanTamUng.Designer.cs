
namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuThanhToanTamUng.Loc
{
    partial class LocThoiGianPhieuThanhToanTamUng
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDenNgay = new V6Controls.V6Label();
            this.v6ColorDateTimePick2 = new V6Controls.V6DateTimePicker();
            this.lblTuNgay = new V6Controls.V6Label();
            this.v6ColorDateTimePick1 = new V6Controls.V6DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "SEARCHG00004";
            this.groupBox1.Controls.Add(this.lblDenNgay);
            this.groupBox1.Controls.Add(this.v6ColorDateTimePick2);
            this.groupBox1.Controls.Add(this.lblTuNgay);
            this.groupBox1.Controls.Add(this.v6ColorDateTimePick1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc thời gian";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AccessibleDescription = "SEARCHL00038";
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(183, 22);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(53, 13);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // v6ColorDateTimePick2
            // 
            this.v6ColorDateTimePick2.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick2.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick2.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick2.Location = new System.Drawing.Point(247, 19);
            this.v6ColorDateTimePick2.Name = "v6ColorDateTimePick2";
            this.v6ColorDateTimePick2.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick2.TabIndex = 3;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AccessibleDescription = "SEARCHL00037";
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(6, 22);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(46, 13);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // v6ColorDateTimePick1
            // 
            this.v6ColorDateTimePick1.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick1.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick1.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick1.Location = new System.Drawing.Point(76, 19);
            this.v6ColorDateTimePick1.Name = "v6ColorDateTimePick1";
            this.v6ColorDateTimePick1.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick1.TabIndex = 1;
            // 
            // LocThoiGianPhieuThanhToanTamUng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LocThoiGianPhieuThanhToanTamUng";
            this.Size = new System.Drawing.Size(759, 48);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6Label lblDenNgay;
        private V6Controls.V6DateTimePicker v6ColorDateTimePick2;
        private V6Controls.V6Label lblTuNgay;
        private V6Controls.V6DateTimePicker v6ColorDateTimePick1;
    }
}
