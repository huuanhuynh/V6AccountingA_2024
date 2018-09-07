using V6Controls.Controls.LichView;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    partial class STICK_NOTE_F3F4
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.lblKieu = new V6Controls.V6Label();
            this.v6Label17 = new V6Controls.V6Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.txtMaNVien = new V6Controls.V6VvarTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.dateNgay = new V6Controls.V6DateTimePick();
            this.Txtten_ns = new V6Controls.V6ColorTextBox();
            this.Txtgio_vao = new V6Controls.V6NumberTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txtgio_ra = new V6Controls.V6NumberTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.v6VvarTextBox1 = new V6Controls.V6VvarTextBox();
            this.v6VvarTextBox2 = new V6Controls.V6VvarTextBox();
            this.txtNote = new V6Controls.V6ColorTextBox();
            this.txtNote1 = new V6Controls.V6ColorTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 242);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 18;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 242);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 17;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // lblKieu
            // 
            this.lblKieu.AutoSize = true;
            this.lblKieu.Location = new System.Drawing.Point(17, 84);
            this.lblKieu.Name = "lblKieu";
            this.lblKieu.Size = new System.Drawing.Size(28, 13);
            this.lblKieu.TabIndex = 7;
            this.lblKieu.Text = "Kiểu";
            // 
            // v6Label17
            // 
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(17, 36);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(72, 13);
            this.v6Label17.TabIndex = 2;
            this.v6Label17.Text = "Mã nhân viên";
            // 
            // v6Label9
            // 
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(17, 12);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(32, 13);
            this.v6Label9.TabIndex = 0;
            this.v6Label9.Text = "Ngày";
            // 
            // txtMaNVien
            // 
            this.txtMaNVien.AccessibleName = "MA_NVIEN";
            this.txtMaNVien.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNVien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNVien.BrotherFields = "ten_nvien";
            this.txtMaNVien.CheckNotEmpty = true;
            this.txtMaNVien.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNVien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNVien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNVien.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNVien.LeaveColor = System.Drawing.Color.White;
            this.txtMaNVien.Location = new System.Drawing.Point(135, 35);
            this.txtMaNVien.Name = "txtMaNVien";
            this.txtMaNVien.ReadOnly = true;
            this.txtMaNVien.Size = new System.Drawing.Size(120, 20);
            this.txtMaNVien.TabIndex = 3;
            this.txtMaNVien.Tag = "readonly";
            this.txtMaNVien.VVar = "MA_NVIEN";
            // 
            // v6Label4
            // 
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(17, 60);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(41, 13);
            this.v6Label4.TabIndex = 5;
            this.v6Label4.Text = "Ưu tiên";
            // 
            // dateNgay
            // 
            this.dateNgay.AccessibleName = "NGAY";
            this.dateNgay.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgay.CustomFormat = "dd/MM/yyyy";
            this.dateNgay.Enabled = false;
            this.dateNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay.LeaveColor = System.Drawing.Color.White;
            this.dateNgay.Location = new System.Drawing.Point(135, 12);
            this.dateNgay.Name = "dateNgay";
            this.dateNgay.ReadOnly = true;
            this.dateNgay.Size = new System.Drawing.Size(120, 20);
            this.dateNgay.TabIndex = 1;
            this.dateNgay.Tag = "disable";
            // 
            // Txtten_ns
            // 
            this.Txtten_ns.AccessibleName = "TEN_NVIEN";
            this.Txtten_ns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtten_ns.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Txtten_ns.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtten_ns.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtten_ns.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.HoverColor = System.Drawing.Color.Yellow;
            this.Txtten_ns.LeaveColor = System.Drawing.Color.White;
            this.Txtten_ns.Location = new System.Drawing.Point(264, 35);
            this.Txtten_ns.Margin = new System.Windows.Forms.Padding(4);
            this.Txtten_ns.Multiline = true;
            this.Txtten_ns.Name = "Txtten_ns";
            this.Txtten_ns.ReadOnly = true;
            this.Txtten_ns.Size = new System.Drawing.Size(433, 20);
            this.Txtten_ns.TabIndex = 4;
            this.Txtten_ns.TabStop = false;
            // 
            // Txtgio_vao
            // 
            this.Txtgio_vao.AccessibleName = "GIO_VAO";
            this.Txtgio_vao.BackColor = System.Drawing.SystemColors.Window;
            this.Txtgio_vao.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtgio_vao.DecimalPlaces = 2;
            this.Txtgio_vao.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtgio_vao.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtgio_vao.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtgio_vao.HoverColor = System.Drawing.Color.Yellow;
            this.Txtgio_vao.LeaveColor = System.Drawing.Color.White;
            this.Txtgio_vao.Location = new System.Drawing.Point(135, 104);
            this.Txtgio_vao.Name = "Txtgio_vao";
            this.Txtgio_vao.Size = new System.Drawing.Size(120, 20);
            this.Txtgio_vao.TabIndex = 10;
            this.Txtgio_vao.Text = "0,00";
            this.Txtgio_vao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgio_vao.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Từ giờ";
            // 
            // Txtgio_ra
            // 
            this.Txtgio_ra.AccessibleName = "GIO_RA";
            this.Txtgio_ra.BackColor = System.Drawing.SystemColors.Window;
            this.Txtgio_ra.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtgio_ra.DecimalPlaces = 2;
            this.Txtgio_ra.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtgio_ra.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtgio_ra.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtgio_ra.HoverColor = System.Drawing.Color.Yellow;
            this.Txtgio_ra.LeaveColor = System.Drawing.Color.White;
            this.Txtgio_ra.Location = new System.Drawing.Point(135, 127);
            this.Txtgio_ra.Name = "Txtgio_ra";
            this.Txtgio_ra.Size = new System.Drawing.Size(120, 20);
            this.Txtgio_ra.TabIndex = 12;
            this.Txtgio_ra.Text = "0,00";
            this.Txtgio_ra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgio_ra.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Đến giờ";
            // 
            // v6VvarTextBox1
            // 
            this.v6VvarTextBox1.AccessibleName = "ABC";
            this.v6VvarTextBox1.BackColor = System.Drawing.Color.White;
            this.v6VvarTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6VvarTextBox1.CheckNotEmpty = true;
            this.v6VvarTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6VvarTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6VvarTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6VvarTextBox1.Location = new System.Drawing.Point(135, 58);
            this.v6VvarTextBox1.Name = "v6VvarTextBox1";
            this.v6VvarTextBox1.Size = new System.Drawing.Size(120, 20);
            this.v6VvarTextBox1.TabIndex = 6;
            // 
            // v6VvarTextBox2
            // 
            this.v6VvarTextBox2.AccessibleName = "TYPE";
            this.v6VvarTextBox2.BackColor = System.Drawing.Color.White;
            this.v6VvarTextBox2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6VvarTextBox2.CheckNotEmpty = true;
            this.v6VvarTextBox2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6VvarTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6VvarTextBox2.HoverColor = System.Drawing.Color.Yellow;
            this.v6VvarTextBox2.LeaveColor = System.Drawing.Color.White;
            this.v6VvarTextBox2.Location = new System.Drawing.Point(135, 81);
            this.v6VvarTextBox2.Name = "v6VvarTextBox2";
            this.v6VvarTextBox2.Size = new System.Drawing.Size(120, 20);
            this.v6VvarTextBox2.TabIndex = 8;
            // 
            // txtNote
            // 
            this.txtNote.AccessibleName = "NOTE";
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.BackColor = System.Drawing.Color.White;
            this.txtNote.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNote.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNote.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNote.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNote.HoverColor = System.Drawing.Color.Yellow;
            this.txtNote.LeaveColor = System.Drawing.Color.White;
            this.txtNote.Location = new System.Drawing.Point(135, 150);
            this.txtNote.Margin = new System.Windows.Forms.Padding(4);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(562, 42);
            this.txtNote.TabIndex = 14;
            this.txtNote.TabStop = false;
            // 
            // txtNote1
            // 
            this.txtNote1.AccessibleName = "NOTE1";
            this.txtNote1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote1.BackColor = System.Drawing.Color.White;
            this.txtNote1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNote1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNote1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNote1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNote1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNote1.LeaveColor = System.Drawing.Color.White;
            this.txtNote1.Location = new System.Drawing.Point(135, 195);
            this.txtNote1.Margin = new System.Windows.Forms.Padding(4);
            this.txtNote1.Multiline = true;
            this.txtNote1.Name = "txtNote1";
            this.txtNote1.Size = new System.Drawing.Size(562, 42);
            this.txtNote1.TabIndex = 16;
            this.txtNote1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ghi chú";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ghi chú 2";
            // 
            // STICK_NOTE_F3F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 294);
            this.Controls.Add(this.v6VvarTextBox2);
            this.Controls.Add(this.v6VvarTextBox1);
            this.Controls.Add(this.Txtgio_ra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txtgio_vao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNote1);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.Txtten_ns);
            this.Controls.Add(this.dateNgay);
            this.Controls.Add(this.txtMaNVien);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.lblKieu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "STICK_NOTE_F3F4";
            this.Text = "STICK_NOTE_F3F4";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.lblKieu, 0);
            this.Controls.SetChildIndex(this.v6Label4, 0);
            this.Controls.SetChildIndex(this.v6Label17, 0);
            this.Controls.SetChildIndex(this.v6Label9, 0);
            this.Controls.SetChildIndex(this.txtMaNVien, 0);
            this.Controls.SetChildIndex(this.dateNgay, 0);
            this.Controls.SetChildIndex(this.Txtten_ns, 0);
            this.Controls.SetChildIndex(this.txtNote, 0);
            this.Controls.SetChildIndex(this.txtNote1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.Txtgio_vao, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.Txtgio_ra, 0);
            this.Controls.SetChildIndex(this.v6VvarTextBox1, 0);
            this.Controls.SetChildIndex(this.v6VvarTextBox2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6Label lblKieu;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox txtMaNVien;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6DateTimePick dateNgay;
        private V6Controls.V6ColorTextBox Txtten_ns;
        private V6Controls.V6NumberTextBox Txtgio_vao;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6NumberTextBox Txtgio_ra;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6VvarTextBox v6VvarTextBox1;
        private V6Controls.V6VvarTextBox v6VvarTextBox2;
        private V6Controls.V6ColorTextBox txtNote;
        private V6Controls.V6ColorTextBox txtNote1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;




    }
}