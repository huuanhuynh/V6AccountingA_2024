namespace Bai_Tap_Thep__Povision
{
    partial class frmLoadHoaDon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadHoaDon));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTaiHoaDon = new System.Windows.Forms.Button();
            this.txtThuMuc = new System.Windows.Forms.TextBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.txtUserPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fkey";
            // 
            // txtFKey
            // 
            this.txtFKey.Location = new System.Drawing.Point(92, 93);
            this.txtFKey.Name = "txtFKey";
            this.txtFKey.Size = new System.Drawing.Size(416, 20);
            this.txtFKey.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "UserName";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(92, 39);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(416, 20);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = "dv204admin_service";
            // 
            // txtKetQua
            // 
            this.txtKetQua.Location = new System.Drawing.Point(563, 38);
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.Size = new System.Drawing.Size(260, 20);
            this.txtKetQua.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(511, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kết Quả";
            // 
            // btnTaiHoaDon
            // 
            this.btnTaiHoaDon.Location = new System.Drawing.Point(5, 119);
            this.btnTaiHoaDon.Name = "btnTaiHoaDon";
            this.btnTaiHoaDon.Size = new System.Drawing.Size(416, 42);
            this.btnTaiHoaDon.TabIndex = 7;
            this.btnTaiHoaDon.Text = "Tải Hóa Đơn Dạng PDF";
            this.btnTaiHoaDon.UseVisualStyleBackColor = true;
            this.btnTaiHoaDon.Click += new System.EventHandler(this.btnTaiHoaDon_Click);
            // 
            // txtThuMuc
            // 
            this.txtThuMuc.Location = new System.Drawing.Point(92, 10);
            this.txtThuMuc.Name = "txtThuMuc";
            this.txtThuMuc.Size = new System.Drawing.Size(416, 20);
            this.txtThuMuc.TabIndex = 5;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(514, 10);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(43, 23);
            this.btnChon.TabIndex = 8;
            this.btnChon.Text = "Chọn";
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(5, 167);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(829, 341);
            this.axAcroPDF1.TabIndex = 9;
            // 
            // txtUserPass
            // 
            this.txtUserPass.Location = new System.Drawing.Point(92, 65);
            this.txtUserPass.Name = "txtUserPass";
            this.txtUserPass.Size = new System.Drawing.Size(260, 20);
            this.txtUserPass.TabIndex = 5;
            this.txtUserPass.Text = "123456aA@";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "UserPass";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(427, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(407, 42);
            this.button1.TabIndex = 10;
            this.button1.Text = "Tải Hóa Đơn Dạng XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmLoadHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 505);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axAcroPDF1);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.btnTaiHoaDon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtThuMuc);
            this.Controls.Add(this.txtUserPass);
            this.Controls.Add(this.txtKetQua);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFKey);
            this.Controls.Add(this.label1);
            this.Name = "frmLoadHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLoadHoaDon";
            this.Load += new System.EventHandler(this.frmLoadHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtKetQua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTaiHoaDon;
        private System.Windows.Forms.TextBox txtThuMuc;
        private System.Windows.Forms.Button btnChon;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.TextBox txtUserPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;

    }
}