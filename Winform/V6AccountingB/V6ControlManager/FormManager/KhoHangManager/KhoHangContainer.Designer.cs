namespace V6ControlManager.FormManager.KhoHangManager
{
    partial class KhoHangContainer
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaKho = new V6Controls.V6VvarTextBox();
            this.dateCuoiNgay = new V6Controls.V6DateTimePicker();
            this.btnSuaTTMauBC = new V6Controls.Controls.V6FormButton();
            this.btnThemMauBC = new V6Controls.Controls.V6FormButton();
            this.txtMavt = new V6Controls.V6VvarTextBox();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtTime = new V6Controls.V6NumberTextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 527);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00191";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kho";
            // 
            // txtMaKho
            // 
            this.txtMaKho.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaKho.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKho.CheckNotEmpty = true;
            this.txtMaKho.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKho.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKho.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKho.LeaveColor = System.Drawing.Color.White;
            this.txtMaKho.Location = new System.Drawing.Point(35, 5);
            this.txtMaKho.Name = "txtMaKho";
            this.txtMaKho.Size = new System.Drawing.Size(80, 20);
            this.txtMaKho.TabIndex = 1;
            this.txtMaKho.VVar = "MA_KHO";
            // 
            // dateCuoiNgay
            // 
            this.dateCuoiNgay.CustomFormat = "dd/MM/yyyy";
            this.dateCuoiNgay.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateCuoiNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateCuoiNgay.HoverColor = System.Drawing.Color.Yellow;
            this.dateCuoiNgay.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateCuoiNgay.LeaveColor = System.Drawing.Color.White;
            this.dateCuoiNgay.Location = new System.Drawing.Point(121, 5);
            this.dateCuoiNgay.Name = "dateCuoiNgay";
            this.dateCuoiNgay.Size = new System.Drawing.Size(96, 20);
            this.dateCuoiNgay.TabIndex = 3;
            this.dateCuoiNgay.ValueChanged += new System.EventHandler(this.dateCuoiNgay_ValueChanged);
            // 
            // btnSuaTTMauBC
            // 
            this.btnSuaTTMauBC.AccessibleDescription = "XULYL00192";
            this.btnSuaTTMauBC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuaTTMauBC.Location = new System.Drawing.Point(690, 4);
            this.btnSuaTTMauBC.Name = "btnSuaTTMauBC";
            this.btnSuaTTMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnSuaTTMauBC.TabIndex = 5;
            this.btnSuaTTMauBC.TabStop = false;
            this.btnSuaTTMauBC.Text = "Sửa tt";
            this.btnSuaTTMauBC.UseVisualStyleBackColor = true;
            this.btnSuaTTMauBC.Click += new System.EventHandler(this.btnSuaTTMauBC_Click);
            // 
            // btnThemMauBC
            // 
            this.btnThemMauBC.AccessibleDescription = "XULYL00193";
            this.btnThemMauBC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemMauBC.Location = new System.Drawing.Point(735, 4);
            this.btnThemMauBC.Name = "btnThemMauBC";
            this.btnThemMauBC.Size = new System.Drawing.Size(43, 23);
            this.btnThemMauBC.TabIndex = 6;
            this.btnThemMauBC.TabStop = false;
            this.btnThemMauBC.Text = "Thêm";
            this.btnThemMauBC.UseVisualStyleBackColor = true;
            this.btnThemMauBC.Click += new System.EventHandler(this.btnThemMauBC_Click);
            // 
            // txtMavt
            // 
            this.txtMavt.AccessibleName = "MA_VT";
            this.txtMavt.BackColor = System.Drawing.SystemColors.Window;
            this.txtMavt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMavt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMavt.BrotherFields = "TEN_VT";
            this.txtMavt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMavt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMavt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMavt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMavt.LeaveColor = System.Drawing.Color.White;
            this.txtMavt.Location = new System.Drawing.Point(223, 5);
            this.txtMavt.Name = "txtMavt";
            this.txtMavt.Size = new System.Drawing.Size(100, 20);
            this.txtMavt.TabIndex = 4;
            this.txtMavt.TabStop = false;
            this.txtMavt.VVar = "MA_VT";
            this.txtMavt.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMavt_V6LostFocus);
            // 
            // lblTenHang
            // 
            this.lblTenHang.AccessibleDescription = ".";
            this.lblTenHang.AccessibleName = "TEN_VT";
            this.lblTenHang.AutoSize = true;
            this.lblTenHang.Location = new System.Drawing.Point(329, 8);
            this.lblTenHang.Name = "lblTenHang";
            this.lblTenHang.Size = new System.Drawing.Size(10, 13);
            this.lblTenHang.TabIndex = 0;
            this.lblTenHang.Text = ".";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtTime
            // 
            this.txtTime.AccessibleName = "TIME";
            this.txtTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtTime.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTime.DecimalPlaces = 0;
            this.txtTime.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTime.ForeColor = System.Drawing.Color.Red;
            this.txtTime.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTime.HoverColor = System.Drawing.Color.Yellow;
            this.txtTime.LeaveColor = System.Drawing.Color.White;
            this.txtTime.Location = new System.Drawing.Point(631, 6);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(53, 20);
            this.txtTime.TabIndex = 7;
            this.txtTime.Text = "30";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTime.Value = new decimal(new int[] {
            30000,
            0,
            0,
            196608});
            // 
            // KhoHangContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtMavt);
            this.Controls.Add(this.btnSuaTTMauBC);
            this.Controls.Add(this.btnThemMauBC);
            this.Controls.Add(this.dateCuoiNgay);
            this.Controls.Add(this.txtMaKho);
            this.Controls.Add(this.lblTenHang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "KhoHangContainer";
            this.Size = new System.Drawing.Size(784, 562);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public V6Controls.V6VvarTextBox txtMaKho;
        private V6Controls.Controls.V6FormButton btnSuaTTMauBC;
        private V6Controls.Controls.V6FormButton btnThemMauBC;
        private System.Windows.Forms.Label lblTenHang;
        private System.Windows.Forms.Timer timer1;
        private V6Controls.V6NumberTextBox txtTime;
        public V6Controls.V6DateTimePicker dateCuoiNgay;
        public V6Controls.V6VvarTextBox txtMavt;
    }
}