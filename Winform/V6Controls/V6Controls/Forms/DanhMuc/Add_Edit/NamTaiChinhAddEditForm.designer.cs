using System.Drawing;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class NamTaiChinhAddEditForm
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
            this.TxtNam_bd = new V6Controls.V6NumberTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtNgay_dn = new V6Controls.V6DateTimePick();
            this.TxtNgay_ky1 = new V6Controls.V6DateTimePick();
            this.txtStt_rec = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.v6ColorDateTimePick5 = new V6Controls.V6DateTimePick();
            this.v6ColorDateTimePick4 = new V6Controls.V6DateTimePick();
            this.v6ColorDateTimePick3 = new V6Controls.V6DateTimePick();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.v6TabControl1 = new V6Controls.V6TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.v6TabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleName = "groupBox1";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtNam_bd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtNgay_dn);
            this.groupBox1.Controls.Add(this.TxtNgay_ky1);
            this.groupBox1.Controls.Add(this.txtStt_rec);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(755, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtNam_bd
            // 
            this.TxtNam_bd.AccessibleName = "nam_bd";
            this.TxtNam_bd.BackColor = System.Drawing.Color.White;
            this.TxtNam_bd.DecimalPlaces = 0;
            this.TxtNam_bd.EnableColorEffect = true;
            this.TxtNam_bd.EnableColorEffectOnMouseEnter = false;
            this.TxtNam_bd.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtNam_bd.HoverColor = System.Drawing.Color.Yellow;
            this.TxtNam_bd.LeaveColor = System.Drawing.Color.White;
            this.TxtNam_bd.LimitCharacters = null;
            this.TxtNam_bd.Location = new System.Drawing.Point(620, 14);
            this.TxtNam_bd.Margin = new System.Windows.Forms.Padding(4);
            this.TxtNam_bd.MaxNumDecimal = 0;
            this.TxtNam_bd.MaxNumLength = 0;
            this.TxtNam_bd.Name = "TxtNam_bd";
            this.TxtNam_bd.Size = new System.Drawing.Size(110, 23);
            this.TxtNam_bd.TabIndex = 16;
            this.TxtNam_bd.Text = "0";
            this.TxtNam_bd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNam_bd.GrayText = "";
            this.TxtNam_bd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TxtNam_bd.Visible = false;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Năm bắt đầu";
            this.label4.Visible = false;
            // 
            // TxtNgay_dn
            // 
            this.TxtNgay_dn.AccessibleName = "ngay_dn";
            this.TxtNgay_dn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNgay_dn.BackColor = System.Drawing.Color.White;
            this.TxtNgay_dn.CustomFormat = "dd/MM/yyyy";
            this.TxtNgay_dn.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtNgay_dn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtNgay_dn.HoverColor = System.Drawing.Color.Yellow;
            this.TxtNgay_dn.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TxtNgay_dn.LeaveColor = System.Drawing.Color.White;
            this.TxtNgay_dn.Location = new System.Drawing.Point(274, 46);
            this.TxtNgay_dn.Margin = new System.Windows.Forms.Padding(5);
            this.TxtNgay_dn.Name = "TxtNgay_dn";
            this.TxtNgay_dn.Size = new System.Drawing.Size(125, 23);
            this.TxtNgay_dn.TabIndex = 2;
            this.TxtNgay_dn.TextTitle = null;
            // 
            // TxtNgay_ky1
            // 
            this.TxtNgay_ky1.AccessibleName = "ngay_ky1";
            this.TxtNgay_ky1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNgay_ky1.BackColor = System.Drawing.Color.White;
            this.TxtNgay_ky1.CustomFormat = "dd/MM/yyyy";
            this.TxtNgay_ky1.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtNgay_ky1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TxtNgay_ky1.HoverColor = System.Drawing.Color.Yellow;
            this.TxtNgay_ky1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TxtNgay_ky1.LeaveColor = System.Drawing.Color.White;
            this.TxtNgay_ky1.Location = new System.Drawing.Point(274, 79);
            this.TxtNgay_ky1.Margin = new System.Windows.Forms.Padding(5);
            this.TxtNgay_ky1.Name = "TxtNgay_ky1";
            this.TxtNgay_ky1.Size = new System.Drawing.Size(125, 23);
            this.TxtNgay_ky1.TabIndex = 3;
            this.TxtNgay_ky1.TextTitle = null;
            this.TxtNgay_ky1.Leave += new System.EventHandler(this.TxtNgay_ky1_Leave);
            // 
            // txtStt_rec
            // 
            this.txtStt_rec.AccessibleName = "Stt_rec";
            this.txtStt_rec.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtStt_rec.BrotherFields = null;
            this.txtStt_rec.EnableColorEffect = true;
            this.txtStt_rec.EnableColorEffectOnMouseEnter = false;
            this.txtStt_rec.Enabled = false;
            this.txtStt_rec.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtStt_rec.HoverColor = System.Drawing.Color.Yellow;
            this.txtStt_rec.LeaveColor = System.Drawing.Color.White;
            this.txtStt_rec.LimitCharacters = null;
            this.txtStt_rec.Location = new System.Drawing.Point(274, 17);
            this.txtStt_rec.Name = "txtStt_rec";
            this.txtStt_rec.ReadOnly = true;
            this.txtStt_rec.Size = new System.Drawing.Size(126, 23);
            this.txtStt_rec.TabIndex = 1;
            this.txtStt_rec.GrayText = "";
            this.txtStt_rec.Visible = false;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số liệu được nhập bắt đầu ngày";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ngày đầu năm tài chính";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tham số chứng từ";
            this.label1.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(747, 292);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Các tùy chọn";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage1.Controls.Add(this.v6ColorDateTimePick5);
            this.tabPage1.Controls.Add(this.v6ColorDateTimePick4);
            this.tabPage1.Controls.Add(this.v6ColorDateTimePick3);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(747, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin chính";
            // 
            // v6ColorDateTimePick5
            // 
            this.v6ColorDateTimePick5.AccessibleName = "ngay_ck";
            this.v6ColorDateTimePick5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorDateTimePick5.BackColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick5.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick5.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick5.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick5.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick5.Location = new System.Drawing.Point(266, 78);
            this.v6ColorDateTimePick5.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick5.Name = "v6ColorDateTimePick5";
            this.v6ColorDateTimePick5.Size = new System.Drawing.Size(125, 23);
            this.v6ColorDateTimePick5.TabIndex = 6;
            this.v6ColorDateTimePick5.TextTitle = null;
            // 
            // v6ColorDateTimePick4
            // 
            this.v6ColorDateTimePick4.AccessibleName = "ngay_dk";
            this.v6ColorDateTimePick4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorDateTimePick4.BackColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick4.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick4.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick4.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick4.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick4.Location = new System.Drawing.Point(266, 49);
            this.v6ColorDateTimePick4.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick4.Name = "v6ColorDateTimePick4";
            this.v6ColorDateTimePick4.Size = new System.Drawing.Size(125, 23);
            this.v6ColorDateTimePick4.TabIndex = 5;
            this.v6ColorDateTimePick4.TextTitle = null;
            // 
            // v6ColorDateTimePick3
            // 
            this.v6ColorDateTimePick3.AccessibleName = "ngay_ks";
            this.v6ColorDateTimePick3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorDateTimePick3.BackColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick3.CustomFormat = "dd/MM/yyyy";
            this.v6ColorDateTimePick3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorDateTimePick3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.v6ColorDateTimePick3.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorDateTimePick3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.v6ColorDateTimePick3.LeaveColor = System.Drawing.Color.White;
            this.v6ColorDateTimePick3.Location = new System.Drawing.Point(266, 16);
            this.v6ColorDateTimePick3.Margin = new System.Windows.Forms.Padding(5);
            this.v6ColorDateTimePick3.Name = "v6ColorDateTimePick3";
            this.v6ColorDateTimePick3.Size = new System.Drawing.Size(125, 23);
            this.v6ColorDateTimePick3.TabIndex = 4;
            this.v6ColorDateTimePick3.TextTitle = null;
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Thời gian nhập liệu đến ngày";
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 50);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Thời gian nhập liệu từ ngày";
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Khóa số liệu đến ngày";
            // 
            // v6TabControl1
            // 
            this.v6TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6TabControl1.BackColor = Color.FromArgb(246, 243, 226);
            this.v6TabControl1.Controls.Add(this.tabPage1);
            this.v6TabControl1.Controls.Add(this.tabPage2);
            this.v6TabControl1.Controls.Add(this.tabPage3);
            this.v6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl1.Location = new System.Drawing.Point(4, 121);
            this.v6TabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.v6TabControl1.Name = "v6TabControl1";
            this.v6TabControl1.SelectedIndex = 0;
            this.v6TabControl1.Size = new System.Drawing.Size(755, 324);
            this.v6TabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.AccessibleDescription = "thong_tin_cong_no";
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(747, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thông tin ngầm định";
            // 
            // NamTaiChinhAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6TabControl1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NamTaiChinhAddEditForm";
            this.Size = new System.Drawing.Size(763, 449);
            this.Load += new System.EventHandler(this.KhachHangFrom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.v6TabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6VvarTextBox txtStt_rec;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private V6Controls.V6TabControl v6TabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private V6DateTimePick TxtNgay_dn;
        private V6DateTimePick TxtNgay_ky1;
        private V6DateTimePick v6ColorDateTimePick5;
        private V6DateTimePick v6ColorDateTimePick4;
        private V6DateTimePick v6ColorDateTimePick3;
        private System.Windows.Forms.Label label4;
        private V6NumberTextBox TxtNam_bd;
    }
}
