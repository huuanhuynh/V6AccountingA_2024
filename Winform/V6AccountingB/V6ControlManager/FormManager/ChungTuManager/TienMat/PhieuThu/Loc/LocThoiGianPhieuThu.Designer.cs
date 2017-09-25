using V6Controls;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuThu.Loc
{
    partial class LocThoiGianPhieuThu
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
            this.v6Label2 = new V6Controls.V6Label();
            this.v6ColorDateTimePick2 = new V6Controls.V6DateTimePick();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6ColorDateTimePick1 = new V6Controls.V6DateTimePick();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "ACACTTA1H00006";
            this.groupBox1.Controls.Add(this.v6Label2);
            this.groupBox1.Controls.Add(this.v6ColorDateTimePick2);
            this.groupBox1.Controls.Add(this.v6Label1);
            this.groupBox1.Controls.Add(this.v6ColorDateTimePick1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc thời gian";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "ACACTTA1H00008";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(183, 22);
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
            this.v6ColorDateTimePick2.Location = new System.Drawing.Point(247, 19);
            this.v6ColorDateTimePick2.Name = "v6ColorDateTimePick2";
            this.v6ColorDateTimePick2.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick2.TabIndex = 3;
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "ACACTTA1H00007";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(6, 22);
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
            this.v6ColorDateTimePick1.Location = new System.Drawing.Point(76, 19);
            this.v6ColorDateTimePick1.Name = "v6ColorDateTimePick1";
            this.v6ColorDateTimePick1.Size = new System.Drawing.Size(102, 20);
            this.v6ColorDateTimePick1.TabIndex = 1;
            // 
            // LocThoiGianPhieuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LocThoiGianPhieuThu";
            this.Size = new System.Drawing.Size(759, 48);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Label v6Label2;
        private V6DateTimePick v6ColorDateTimePick2;
        private V6Label v6Label1;
        private V6DateTimePick v6ColorDateTimePick1;
    }
}
