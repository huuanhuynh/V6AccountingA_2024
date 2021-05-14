using V6Controls.Controls.LichView;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    partial class HPRCONGCT_XL1_F3F4
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
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label17 = new V6Controls.V6Label();
            this.txtGio = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaNhanSu = new V6Controls.V6VvarTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.v6Label7 = new V6Controls.V6Label();
            this.txtTien = new V6Controls.V6NumberTextBox();
            this.dateNgay = new V6Controls.V6DateTimePicker();
            this.Txtten_ns = new V6Controls.V6ColorTextBox();
            this.txtMaBp = new V6Controls.V6LookupTextBox();
            this.txtMaCong = new V6Controls.V6VvarTextBox();
            this.TXTTEN_CONG = new V6Controls.V6ColorTextBox();
            this.Txtgio_vao = new V6Controls.V6NumberTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txtgio_ra = new V6Controls.V6NumberTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Txtsl_td3 = new V6Controls.V6NumberTextBox();
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
            this.btnHuy.TabIndex = 12;
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
            this.btnNhan.TabIndex = 11;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "ADDEDITL00219";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(17, 87);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(49, 13);
            this.v6Label1.TabIndex = 6;
            this.v6Label1.Text = "Mã công";
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "ADDEDITL00218";
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(17, 37);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(63, 13);
            this.v6Label17.TabIndex = 4;
            this.v6Label17.Text = "Mã nhân sự";
            // 
            // txtGio
            // 
            this.txtGio.AccessibleName = "GIO";
            this.txtGio.BackColor = System.Drawing.SystemColors.Window;
            this.txtGio.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGio.DecimalPlaces = 2;
            this.txtGio.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGio.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGio.HoverColor = System.Drawing.Color.Yellow;
            this.txtGio.LeaveColor = System.Drawing.Color.White;
            this.txtGio.Location = new System.Drawing.Point(135, 190);
            this.txtGio.Name = "txtGio";
            this.txtGio.Size = new System.Drawing.Size(120, 20);
            this.txtGio.TabIndex = 9;
            this.txtGio.Text = "0,00";
            this.txtGio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGio.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "ADDEDITL00217";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(17, 12);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(32, 13);
            this.v6Label9.TabIndex = 0;
            this.v6Label9.Text = "Ngày";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "ADDEDITL00223";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số giờ";
            // 
            // txtMaNhanSu
            // 
            this.txtMaNhanSu.AccessibleName = "MA_NS";
            this.txtMaNhanSu.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaNhanSu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaNhanSu.BrotherFields = "ten_ns";
            this.txtMaNhanSu.CheckNotEmpty = true;
            this.txtMaNhanSu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaNhanSu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaNhanSu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaNhanSu.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaNhanSu.LeaveColor = System.Drawing.Color.White;
            this.txtMaNhanSu.Location = new System.Drawing.Point(135, 37);
            this.txtMaNhanSu.Name = "txtMaNhanSu";
            this.txtMaNhanSu.ReadOnly = true;
            this.txtMaNhanSu.Size = new System.Drawing.Size(120, 20);
            this.txtMaNhanSu.TabIndex = 1;
            this.txtMaNhanSu.TabStop = false;
            this.txtMaNhanSu.Tag = "readonly";
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "ADDEDITL00208";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(17, 62);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(47, 13);
            this.v6Label4.TabIndex = 8;
            this.v6Label4.Text = "Bộ phận";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "ADDEDITL00191";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(17, 218);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(28, 13);
            this.v6Label7.TabIndex = 10;
            this.v6Label7.Text = "Tiền";
            // 
            // txtTien
            // 
            this.txtTien.AccessibleName = "TIEN";
            this.txtTien.BackColor = System.Drawing.Color.White;
            this.txtTien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTien.DecimalPlaces = 2;
            this.txtTien.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTien.HoverColor = System.Drawing.Color.Yellow;
            this.txtTien.LeaveColor = System.Drawing.Color.White;
            this.txtTien.Location = new System.Drawing.Point(135, 216);
            this.txtTien.Name = "txtTien";
            this.txtTien.Size = new System.Drawing.Size(120, 20);
            this.txtTien.TabIndex = 10;
            this.txtTien.Text = "0,00";
            this.txtTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTien.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
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
            this.dateNgay.TabIndex = 0;
            this.dateNgay.Tag = "disable";
            // 
            // Txtten_ns
            // 
            this.Txtten_ns.AccessibleName = "TEN_NS";
            this.Txtten_ns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtten_ns.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Txtten_ns.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtten_ns.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtten_ns.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtten_ns.HoverColor = System.Drawing.Color.Yellow;
            this.Txtten_ns.LeaveColor = System.Drawing.Color.White;
            this.Txtten_ns.Location = new System.Drawing.Point(264, 37);
            this.Txtten_ns.Margin = new System.Windows.Forms.Padding(4);
            this.Txtten_ns.Multiline = true;
            this.Txtten_ns.Name = "Txtten_ns";
            this.Txtten_ns.ReadOnly = true;
            this.Txtten_ns.Size = new System.Drawing.Size(433, 20);
            this.Txtten_ns.TabIndex = 2;
            this.Txtten_ns.TabStop = false;
            // 
            // txtMaBp
            // 
            this.txtMaBp.AccessibleName = "MA_BP";
            this.txtMaBp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaBp.BackColor = System.Drawing.Color.White;
            this.txtMaBp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaBp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaBp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaBp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaBp.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaBp.LeaveColor = System.Drawing.Color.White;
            this.txtMaBp.Location = new System.Drawing.Point(135, 62);
            this.txtMaBp.Ma_dm = "HRLSTORGUNIT";
            this.txtMaBp.Margin = new System.Windows.Forms.Padding(5);
            this.txtMaBp.Name = "txtMaBp";
            this.txtMaBp.NeighborFields = "";
            this.txtMaBp.ParentData = null;
            this.txtMaBp.ShowTextField = "NAME";
            this.txtMaBp.Size = new System.Drawing.Size(120, 20);
            this.txtMaBp.TabIndex = 3;
            this.txtMaBp.ValueField = "MA_BP";
            // 
            // txtMaCong
            // 
            this.txtMaCong.AccessibleName = "ma_cong";
            this.txtMaCong.BackColor = System.Drawing.Color.White;
            this.txtMaCong.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCong.BrotherFields = "ten_cong,he_so";
            this.txtMaCong.CheckNotEmpty = true;
            this.txtMaCong.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCong.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCong.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCong.LeaveColor = System.Drawing.Color.White;
            this.txtMaCong.Location = new System.Drawing.Point(135, 87);
            this.txtMaCong.Name = "txtMaCong";
            this.txtMaCong.Size = new System.Drawing.Size(120, 20);
            this.txtMaCong.TabIndex = 4;
            this.txtMaCong.VVar = "MA_cong";
            // 
            // TXTTEN_CONG
            // 
            this.TXTTEN_CONG.AccessibleName = "TEN_CONG";
            this.TXTTEN_CONG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TXTTEN_CONG.BackColor = System.Drawing.Color.AntiqueWhite;
            this.TXTTEN_CONG.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TXTTEN_CONG.EnterColor = System.Drawing.Color.PaleGreen;
            this.TXTTEN_CONG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TXTTEN_CONG.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TXTTEN_CONG.HoverColor = System.Drawing.Color.Yellow;
            this.TXTTEN_CONG.LeaveColor = System.Drawing.Color.White;
            this.TXTTEN_CONG.Location = new System.Drawing.Point(264, 87);
            this.TXTTEN_CONG.Margin = new System.Windows.Forms.Padding(4);
            this.TXTTEN_CONG.Multiline = true;
            this.TXTTEN_CONG.Name = "TXTTEN_CONG";
            this.TXTTEN_CONG.ReadOnly = true;
            this.TXTTEN_CONG.Size = new System.Drawing.Size(433, 20);
            this.TXTTEN_CONG.TabIndex = 5;
            this.TXTTEN_CONG.TabStop = false;
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
            this.Txtgio_vao.Location = new System.Drawing.Point(135, 111);
            this.Txtgio_vao.Name = "Txtgio_vao";
            this.Txtgio_vao.Size = new System.Drawing.Size(120, 20);
            this.Txtgio_vao.TabIndex = 6;
            this.Txtgio_vao.Text = "0,00";
            this.Txtgio_vao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgio_vao.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.Txtgio_vao.TextChanged += new System.EventHandler(this.Tinh_gio);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ADDEDITL00220";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Giờ vào";
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
            this.Txtgio_ra.Location = new System.Drawing.Point(135, 136);
            this.Txtgio_ra.Name = "Txtgio_ra";
            this.Txtgio_ra.Size = new System.Drawing.Size(120, 20);
            this.Txtgio_ra.TabIndex = 7;
            this.Txtgio_ra.Text = "0,00";
            this.Txtgio_ra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtgio_ra.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.Txtgio_ra.TextChanged += new System.EventHandler(this.Tinh_gio);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ADDEDITL00221";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Giờ ra";
            // 
            // Txtsl_td3
            // 
            this.Txtsl_td3.AccessibleName = "SL_TD3";
            this.Txtsl_td3.BackColor = System.Drawing.SystemColors.Window;
            this.Txtsl_td3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtsl_td3.DecimalPlaces = 2;
            this.Txtsl_td3.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtsl_td3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtsl_td3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtsl_td3.HoverColor = System.Drawing.Color.Yellow;
            this.Txtsl_td3.LeaveColor = System.Drawing.Color.White;
            this.Txtsl_td3.Location = new System.Drawing.Point(135, 164);
            this.Txtsl_td3.Name = "Txtsl_td3";
            this.Txtsl_td3.Size = new System.Drawing.Size(120, 20);
            this.Txtsl_td3.TabIndex = 8;
            this.Txtsl_td3.Text = "0,00";
            this.Txtsl_td3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Txtsl_td3.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "ADDEDITL00222";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tổng số giờ";
            // 
            // HPRCONGCT_XL1_F3F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 294);
            this.Controls.Add(this.Txtsl_td3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Txtgio_ra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txtgio_vao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TXTTEN_CONG);
            this.Controls.Add(this.txtMaCong);
            this.Controls.Add(this.txtMaBp);
            this.Controls.Add(this.Txtten_ns);
            this.Controls.Add(this.dateNgay);
            this.Controls.Add(this.txtMaNhanSu);
            this.Controls.Add(this.txtGio);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTien);
            this.Controls.Add(this.v6Label17);
            this.Controls.Add(this.v6Label7);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "HPRCONGCT_XL1_F3F4";
            this.Load += new System.EventHandler(this.Form_Load);
            
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.v6Label1, 0);
            this.Controls.SetChildIndex(this.v6Label4, 0);
            this.Controls.SetChildIndex(this.v6Label7, 0);
            this.Controls.SetChildIndex(this.v6Label17, 0);
            this.Controls.SetChildIndex(this.txtTien, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.v6Label9, 0);
            this.Controls.SetChildIndex(this.txtGio, 0);
            this.Controls.SetChildIndex(this.txtMaNhanSu, 0);
            this.Controls.SetChildIndex(this.dateNgay, 0);
            this.Controls.SetChildIndex(this.Txtten_ns, 0);
            this.Controls.SetChildIndex(this.txtMaBp, 0);
            this.Controls.SetChildIndex(this.txtMaCong, 0);
            this.Controls.SetChildIndex(this.TXTTEN_CONG, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.Txtgio_vao, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.Txtgio_ra, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.Txtsl_td3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label17;
        private V6Controls.V6NumberTextBox txtGio;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6VvarTextBox txtMaNhanSu;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6Label v6Label7;
        private V6Controls.V6NumberTextBox txtTien;
        private V6Controls.V6DateTimePicker dateNgay;
        private V6Controls.V6ColorTextBox Txtten_ns;
        private V6Controls.V6LookupTextBox txtMaBp;
        private V6Controls.V6VvarTextBox txtMaCong;
        private V6Controls.V6ColorTextBox TXTTEN_CONG;
        private V6Controls.V6NumberTextBox Txtgio_vao;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6NumberTextBox Txtgio_ra;
        private System.Windows.Forms.Label label3;
        private V6Controls.V6NumberTextBox Txtsl_td3;
        private System.Windows.Forms.Label label4;




    }
}