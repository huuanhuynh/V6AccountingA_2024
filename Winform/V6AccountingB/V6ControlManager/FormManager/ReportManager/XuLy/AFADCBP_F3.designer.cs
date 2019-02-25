namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AFADCBP_F3
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
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.txtSo_the_ts = new V6Controls.V6VvarTextBox();
            this.v6Label1 = new V6Controls.V6Label();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.TxtMa_BP = new V6Controls.V6VvarTextBox();
            this.TxtTk_ts = new V6Controls.V6VvarTextBox();
            this.v6Label4 = new V6Controls.V6Label();
            this.txtMaCt = new V6Controls.V6ColorTextBox();
            this.txtTs0 = new V6Controls.V6NumberTextBox();
            this.v6Label3 = new V6Controls.V6Label();
            this.txtTk_kh = new V6Controls.V6VvarTextBox();
            this.v6Label5 = new V6Controls.V6Label();
            this.TxtTk_cp = new V6Controls.V6VvarTextBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtten_ts = new V6Controls.V6LabelTextBox();
            this.txtten_bp = new V6Controls.V6LabelTextBox();
            this.txtten_tk_ts = new V6Controls.V6LabelTextBox();
            this.txtten_tk_kh = new V6Controls.V6LabelTextBox();
            this.txtten_tk_cp = new V6Controls.V6LabelTextBox();
            this.SuspendLayout();
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // txtSo_the_ts
            // 
            this.txtSo_the_ts.AccessibleName = "so_the_ts";
            this.txtSo_the_ts.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtSo_the_ts.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSo_the_ts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSo_the_ts.BrotherFields = "TEN_TS";
            this.txtSo_the_ts.CheckNotEmpty = true;
            this.txtSo_the_ts.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSo_the_ts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSo_the_ts.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSo_the_ts.HoverColor = System.Drawing.Color.Yellow;
            this.txtSo_the_ts.LeaveColor = System.Drawing.Color.White;
            this.txtSo_the_ts.Location = new System.Drawing.Point(106, 7);
            this.txtSo_the_ts.Name = "txtSo_the_ts";
            this.txtSo_the_ts.ReadOnly = true;
            this.txtSo_the_ts.Size = new System.Drawing.Size(147, 20);
            this.txtSo_the_ts.TabIndex = 0;
            this.txtSo_the_ts.TabStop = false;
            this.txtSo_the_ts.VVar = "so_the_ts";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00005";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(9, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(56, 13);
            this.v6Label1.TabIndex = 8;
            this.v6Label1.Text = "Mã tài sản";
            // 
            // txtNam
            // 
            this.txtNam.AccessibleName = "NAM";
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(106, 33);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(146, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtThang1
            // 
            this.txtThang1.AccessibleName = "KY";
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(106, 57);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(146, 20);
            this.txtThang1.TabIndex = 3;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.TextChanged += new System.EventHandler(this.txtThang12_TextChanged);
            this.txtThang1.Leave += new System.EventHandler(this.txtThang1_Leave);
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "XULYL00009";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(9, 36);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 39;
            this.v6Label9.Text = "Năm";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00010";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Kỳ";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00022";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(9, 84);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(81, 13);
            this.v6Label2.TabIndex = 41;
            this.v6Label2.Text = "Mã bộ phận TS";
            // 
            // TxtMa_BP
            // 
            this.TxtMa_BP.AccessibleName = "MA_BP";
            this.TxtMa_BP.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_BP.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_BP.BrotherFields = "TEN_BP";
            this.TxtMa_BP.CheckNotEmpty = true;
            this.TxtMa_BP.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_BP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_BP.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_BP.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_BP.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_BP.Location = new System.Drawing.Point(106, 83);
            this.TxtMa_BP.Name = "TxtMa_BP";
            this.TxtMa_BP.Size = new System.Drawing.Size(146, 20);
            this.TxtMa_BP.TabIndex = 4;
            this.TxtMa_BP.VVar = "MA_BPTS";
            // 
            // TxtTk_ts
            // 
            this.TxtTk_ts.AccessibleName = "TK_TS";
            this.TxtTk_ts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTk_ts.BackColor = System.Drawing.Color.White;
            this.TxtTk_ts.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk_ts.BrotherFields = "";
            this.TxtTk_ts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtTk_ts.CheckNotEmpty = true;
            this.TxtTk_ts.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk_ts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk_ts.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk_ts.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk_ts.LeaveColor = System.Drawing.Color.White;
            this.TxtTk_ts.Location = new System.Drawing.Point(106, 109);
            this.TxtTk_ts.Name = "TxtTk_ts";
            this.TxtTk_ts.Size = new System.Drawing.Size(146, 20);
            this.TxtTk_ts.TabIndex = 5;
            this.TxtTk_ts.VVar = "TK";
            this.TxtTk_ts.Leave += new System.EventHandler(this.TxtTk_ts_Leave);
            // 
            // v6Label4
            // 
            this.v6Label4.AccessibleDescription = "XULYL00037";
            this.v6Label4.AutoSize = true;
            this.v6Label4.Location = new System.Drawing.Point(9, 109);
            this.v6Label4.Name = "v6Label4";
            this.v6Label4.Size = new System.Drawing.Size(55, 13);
            this.v6Label4.TabIndex = 43;
            this.v6Label4.Text = "TK tài sản";
            // 
            // txtMaCt
            // 
            this.txtMaCt.AccessibleName = "MA_CT";
            this.txtMaCt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaCt.BackColor = System.Drawing.Color.White;
            this.txtMaCt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCt.Enabled = false;
            this.txtMaCt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCt.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCt.LeaveColor = System.Drawing.Color.White;
            this.txtMaCt.Location = new System.Drawing.Point(428, 51);
            this.txtMaCt.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCt.Name = "txtMaCt";
            this.txtMaCt.Size = new System.Drawing.Size(50, 20);
            this.txtMaCt.TabIndex = 60;
            this.txtMaCt.Visible = false;
            // 
            // txtTs0
            // 
            this.txtTs0.AccessibleName = "Ts0";
            this.txtTs0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTs0.BackColor = System.Drawing.Color.White;
            this.txtTs0.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTs0.DecimalPlaces = 0;
            this.txtTs0.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTs0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTs0.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTs0.HoverColor = System.Drawing.Color.Yellow;
            this.txtTs0.LeaveColor = System.Drawing.Color.White;
            this.txtTs0.Location = new System.Drawing.Point(385, 51);
            this.txtTs0.Name = "txtTs0";
            this.txtTs0.Size = new System.Drawing.Size(16, 20);
            this.txtTs0.TabIndex = 59;
            this.txtTs0.Tag = "cancelset";
            this.txtTs0.Text = "1";
            this.txtTs0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTs0.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTs0.Visible = false;
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00023";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(9, 135);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(69, 13);
            this.v6Label3.TabIndex = 62;
            this.v6Label3.Text = "TK khấu hao";
            // 
            // txtTk_kh
            // 
            this.txtTk_kh.AccessibleName = "TK_KH";
            this.txtTk_kh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTk_kh.BackColor = System.Drawing.Color.White;
            this.txtTk_kh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTk_kh.BrotherFields = "";
            this.txtTk_kh.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTk_kh.CheckNotEmpty = true;
            this.txtTk_kh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTk_kh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTk_kh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTk_kh.HoverColor = System.Drawing.Color.Yellow;
            this.txtTk_kh.LeaveColor = System.Drawing.Color.White;
            this.txtTk_kh.Location = new System.Drawing.Point(106, 135);
            this.txtTk_kh.Name = "txtTk_kh";
            this.txtTk_kh.Size = new System.Drawing.Size(146, 20);
            this.txtTk_kh.TabIndex = 6;
            this.txtTk_kh.VVar = "TK";
            this.txtTk_kh.Leave += new System.EventHandler(this.txtTk_kh_Leave);
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "XULYL00024";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(10, 161);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(57, 13);
            this.v6Label5.TabIndex = 64;
            this.v6Label5.Text = "TK chi phí";
            // 
            // TxtTk_cp
            // 
            this.TxtTk_cp.AccessibleName = "TK_CP";
            this.TxtTk_cp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTk_cp.BackColor = System.Drawing.Color.White;
            this.TxtTk_cp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTk_cp.BrotherFields = "";
            this.TxtTk_cp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtTk_cp.CheckNotEmpty = true;
            this.TxtTk_cp.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTk_cp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTk_cp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTk_cp.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTk_cp.LeaveColor = System.Drawing.Color.White;
            this.TxtTk_cp.Location = new System.Drawing.Point(106, 161);
            this.TxtTk_cp.Name = "TxtTk_cp";
            this.TxtTk_cp.Size = new System.Drawing.Size(146, 20);
            this.TxtTk_cp.TabIndex = 7;
            this.TxtTk_cp.VVar = "TK";
            this.TxtTk_cp.Leave += new System.EventHandler(this.TxtTk_cp_Leave);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 208);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 16;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 208);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 8;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtten_ts
            // 
            this.txtten_ts.AccessibleName = "ten_ts";
            this.txtten_ts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_ts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_ts.Location = new System.Drawing.Point(261, 9);
            this.txtten_ts.Name = "txtten_ts";
            this.txtten_ts.ReadOnly = true;
            this.txtten_ts.Size = new System.Drawing.Size(367, 13);
            this.txtten_ts.TabIndex = 74;
            this.txtten_ts.TabStop = false;
            this.txtten_ts.Tag = "readonly";
            // 
            // txtten_bp
            // 
            this.txtten_bp.AccessibleName = "ten_bp";
            this.txtten_bp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_bp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_bp.Location = new System.Drawing.Point(261, 86);
            this.txtten_bp.Name = "txtten_bp";
            this.txtten_bp.ReadOnly = true;
            this.txtten_bp.Size = new System.Drawing.Size(367, 13);
            this.txtten_bp.TabIndex = 75;
            this.txtten_bp.TabStop = false;
            this.txtten_bp.Tag = "readonly";
            // 
            // txtten_tk_ts
            // 
            this.txtten_tk_ts.AccessibleName = "ten_tk_ts";
            this.txtten_tk_ts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_tk_ts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_tk_ts.Location = new System.Drawing.Point(261, 113);
            this.txtten_tk_ts.Name = "txtten_tk_ts";
            this.txtten_tk_ts.ReadOnly = true;
            this.txtten_tk_ts.Size = new System.Drawing.Size(367, 13);
            this.txtten_tk_ts.TabIndex = 76;
            this.txtten_tk_ts.TabStop = false;
            this.txtten_tk_ts.Tag = "readonly";
            // 
            // txtten_tk_kh
            // 
            this.txtten_tk_kh.AccessibleName = "ten_tk_kh";
            this.txtten_tk_kh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_tk_kh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_tk_kh.Location = new System.Drawing.Point(261, 138);
            this.txtten_tk_kh.Name = "txtten_tk_kh";
            this.txtten_tk_kh.ReadOnly = true;
            this.txtten_tk_kh.Size = new System.Drawing.Size(367, 13);
            this.txtten_tk_kh.TabIndex = 77;
            this.txtten_tk_kh.TabStop = false;
            this.txtten_tk_kh.Tag = "readonly";
            // 
            // txtten_tk_cp
            // 
            this.txtten_tk_cp.AccessibleName = "ten_tk_cp";
            this.txtten_tk_cp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.txtten_tk_cp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtten_tk_cp.Location = new System.Drawing.Point(261, 168);
            this.txtten_tk_cp.Name = "txtten_tk_cp";
            this.txtten_tk_cp.ReadOnly = true;
            this.txtten_tk_cp.Size = new System.Drawing.Size(367, 13);
            this.txtten_tk_cp.TabIndex = 78;
            this.txtten_tk_cp.TabStop = false;
            this.txtten_tk_cp.Tag = "readonly";
            // 
            // AFADCBP_F3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtten_tk_cp);
            this.Controls.Add(this.txtten_tk_kh);
            this.Controls.Add(this.txtten_tk_ts);
            this.Controls.Add(this.txtten_bp);
            this.Controls.Add(this.txtten_ts);
            this.Controls.Add(this.v6Label5);
            this.Controls.Add(this.TxtTk_cp);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.txtTk_kh);
            this.Controls.Add(this.txtMaCt);
            this.Controls.Add(this.txtTs0);
            this.Controls.Add(this.v6Label4);
            this.Controls.Add(this.TxtTk_ts);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.TxtMa_BP);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSo_the_ts);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AFADCBP_F3";
            this.Size = new System.Drawing.Size(638, 260);
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6VvarTextBox txtSo_the_ts;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6VvarTextBox TxtMa_BP;
        private V6Controls.V6VvarTextBox TxtTk_ts;
        private V6Controls.V6Label v6Label4;
        private V6Controls.V6ColorTextBox txtMaCt;
        private V6Controls.V6NumberTextBox txtTs0;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6VvarTextBox txtTk_kh;
        private V6Controls.V6Label v6Label5;
        private V6Controls.V6VvarTextBox TxtTk_cp;
        private V6Controls.V6LabelTextBox txtten_ts;
        private V6Controls.V6LabelTextBox txtten_bp;
        private V6Controls.V6LabelTextBox txtten_tk_ts;
        private V6Controls.V6LabelTextBox txtten_tk_kh;
        private V6Controls.V6LabelTextBox txtten_tk_cp;




    }
}