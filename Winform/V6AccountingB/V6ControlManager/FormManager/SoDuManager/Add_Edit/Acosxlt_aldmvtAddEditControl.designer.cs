using V6Controls;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    partial class Acosxlt_aldmvtAddEditControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.v6TabControl1 = new V6Controls.V6TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMaCt = new V6Controls.V6ColorTextBox();
            this.txtSttRec = new V6Controls.V6ColorTextBox();
            this.dateNgayHL = new V6Controls.V6DateTimeColor();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaBpht = new V6Controls.V6VvarTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaSp = new V6Controls.V6VvarTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.v6ColorTextBox16 = new V6Controls.V6ColorTextBox();
            this.v6ColorTextBox15 = new V6Controls.V6ColorTextBox();
            this.v6ColorTextBox14 = new V6Controls.V6ColorTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.v6NumberTextBox3 = new V6Controls.V6NumberTextBox();
            this.v6NumberTextBox2 = new V6Controls.V6NumberTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChiTiet = new System.Windows.Forms.TabPage();
            this.detail1 = new V6ControlManager.FormManager.ChungTuManager.HD_Detail();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.MA_VT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_VT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT_REC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT_REC0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabKhac = new System.Windows.Forms.TabPage();
            this.v6TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // v6TabControl1
            // 
            this.v6TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6TabControl1.Controls.Add(this.tabPage1);
            this.v6TabControl1.Controls.Add(this.tabPage2);
            this.v6TabControl1.Controls.Add(this.tabPage3);
            this.v6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl1.Location = new System.Drawing.Point(7, 5);
            this.v6TabControl1.Margin = new System.Windows.Forms.Padding(5);
            this.v6TabControl1.Name = "v6TabControl1";
            this.v6TabControl1.SelectedIndex = 0;
            this.v6TabControl1.Size = new System.Drawing.Size(889, 202);
            this.v6TabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AccessibleDescription = "XULYT00001";
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage1.Controls.Add(this.txtMaCt);
            this.tabPage1.Controls.Add(this.txtSttRec);
            this.tabPage1.Controls.Add(this.dateNgayHL);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtMaBpht);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtMaSp);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage1.Size = new System.Drawing.Size(881, 170);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin chính";
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
            this.txtMaCt.Location = new System.Drawing.Point(656, 9);
            this.txtMaCt.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCt.Name = "txtMaCt";
            this.txtMaCt.Size = new System.Drawing.Size(12, 23);
            this.txtMaCt.TabIndex = 7;
            this.txtMaCt.Visible = false;
            // 
            // txtSttRec
            // 
            this.txtSttRec.AccessibleName = "STT_REC";
            this.txtSttRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSttRec.BackColor = System.Drawing.Color.White;
            this.txtSttRec.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSttRec.Enabled = false;
            this.txtSttRec.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSttRec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSttRec.HoverColor = System.Drawing.Color.Yellow;
            this.txtSttRec.LeaveColor = System.Drawing.Color.White;
            this.txtSttRec.Location = new System.Drawing.Point(636, 9);
            this.txtSttRec.Margin = new System.Windows.Forms.Padding(4);
            this.txtSttRec.Name = "txtSttRec";
            this.txtSttRec.Size = new System.Drawing.Size(12, 23);
            this.txtSttRec.TabIndex = 6;
            this.txtSttRec.Visible = false;
            // 
            // dateNgayHL
            // 
            this.dateNgayHL.AccessibleName = "ngay_hl";
            this.dateNgayHL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgayHL.BackColor = System.Drawing.Color.White;
            this.dateNgayHL.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.dateNgayHL.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayHL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayHL.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.dateNgayHL.GrayText = null;
            this.dateNgayHL.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayHL.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayHL.LeaveColor = System.Drawing.Color.White;
            this.dateNgayHL.Location = new System.Drawing.Point(179, 63);
            this.dateNgayHL.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayHL.Name = "dateNgayHL";
            this.dateNgayHL.Size = new System.Drawing.Size(135, 23);
            this.dateNgayHL.StringValue = "__/__/____";
            this.dateNgayHL.TabIndex = 5;
            this.dateNgayHL.Text = "__/__/____";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "XULYL00026";
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mã bộ phận hạch toán";
            // 
            // txtMaBpht
            // 
            this.txtMaBpht.AccessibleName = "MA_BPHT";
            this.txtMaBpht.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaBpht.BackColor = System.Drawing.Color.White;
            this.txtMaBpht.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaBpht.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaBpht.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaBpht.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaBpht.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaBpht.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaBpht.LeaveColor = System.Drawing.Color.White;
            this.txtMaBpht.Location = new System.Drawing.Point(179, 5);
            this.txtMaBpht.Name = "txtMaBpht";
            this.txtMaBpht.Size = new System.Drawing.Size(202, 23);
            this.txtMaBpht.TabIndex = 1;
            this.txtMaBpht.VVar = "MA_BPHT";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00028";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã sản phẩm";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "XULYL00142";
            this.label7.AccessibleName = "";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Ngày hiệu lực";
            // 
            // txtMaSp
            // 
            this.txtMaSp.AccessibleName = "MA_SP";
            this.txtMaSp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSp.BackColor = System.Drawing.Color.White;
            this.txtMaSp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaSp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaSp.CheckNotEmpty = true;
            this.txtMaSp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaSp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaSp.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSp.LeaveColor = System.Drawing.Color.White;
            this.txtMaSp.Location = new System.Drawing.Point(179, 34);
            this.txtMaSp.Name = "txtMaSp";
            this.txtMaSp.Size = new System.Drawing.Size(202, 23);
            this.txtMaSp.TabIndex = 3;
            this.txtMaSp.VVar = "MA_VT";
            // 
            // tabPage2
            // 
            this.tabPage2.AccessibleDescription = "XULYT00002";
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage2.Controls.Add(this.v6ColorTextBox16);
            this.tabPage2.Controls.Add(this.v6ColorTextBox15);
            this.tabPage2.Controls.Add(this.v6ColorTextBox14);
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.label28);
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Controls.Add(this.v6NumberTextBox3);
            this.tabPage2.Controls.Add(this.v6NumberTextBox2);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(881, 170);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tự định nghĩa";
            // 
            // v6ColorTextBox16
            // 
            this.v6ColorTextBox16.AccessibleName = "gc_td3";
            this.v6ColorTextBox16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox16.BackColor = System.Drawing.Color.White;
            this.v6ColorTextBox16.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox16.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox16.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox16.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox16.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox16.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox16.Location = new System.Drawing.Point(252, 448);
            this.v6ColorTextBox16.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6ColorTextBox16.Name = "v6ColorTextBox16";
            this.v6ColorTextBox16.Size = new System.Drawing.Size(424, 23);
            this.v6ColorTextBox16.TabIndex = 113;
            // 
            // v6ColorTextBox15
            // 
            this.v6ColorTextBox15.AccessibleName = "gc_td2";
            this.v6ColorTextBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox15.BackColor = System.Drawing.Color.White;
            this.v6ColorTextBox15.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox15.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox15.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox15.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox15.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox15.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox15.Location = new System.Drawing.Point(252, 410);
            this.v6ColorTextBox15.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6ColorTextBox15.Name = "v6ColorTextBox15";
            this.v6ColorTextBox15.Size = new System.Drawing.Size(424, 23);
            this.v6ColorTextBox15.TabIndex = 112;
            // 
            // v6ColorTextBox14
            // 
            this.v6ColorTextBox14.AccessibleName = "gc_td1";
            this.v6ColorTextBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox14.BackColor = System.Drawing.Color.White;
            this.v6ColorTextBox14.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox14.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox14.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox14.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox14.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox14.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox14.Location = new System.Drawing.Point(252, 372);
            this.v6ColorTextBox14.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6ColorTextBox14.Name = "v6ColorTextBox14";
            this.v6ColorTextBox14.Size = new System.Drawing.Size(424, 23);
            this.v6ColorTextBox14.TabIndex = 111;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(76, 452);
            this.label29.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 17);
            this.label29.TabIndex = 110;
            this.label29.Text = "Ghi chú ĐN 3";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(76, 414);
            this.label28.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(93, 17);
            this.label28.TabIndex = 109;
            this.label28.Text = "Ghi chú ĐN 2";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(76, 375);
            this.label27.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 17);
            this.label27.TabIndex = 108;
            this.label27.Text = "Ghi chú ĐN 1";
            // 
            // v6NumberTextBox3
            // 
            this.v6NumberTextBox3.AccessibleName = "sl_td3";
            this.v6NumberTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6NumberTextBox3.BackColor = System.Drawing.Color.AntiqueWhite;
            this.v6NumberTextBox3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox3.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox3.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox3.Location = new System.Drawing.Point(252, 334);
            this.v6NumberTextBox3.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6NumberTextBox3.Name = "v6NumberTextBox3";
            this.v6NumberTextBox3.ReadOnly = true;
            this.v6NumberTextBox3.Size = new System.Drawing.Size(118, 23);
            this.v6NumberTextBox3.TabIndex = 107;
            this.v6NumberTextBox3.Text = "0,000";
            this.v6NumberTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox3.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // v6NumberTextBox2
            // 
            this.v6NumberTextBox2.AccessibleName = "sl_td2";
            this.v6NumberTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6NumberTextBox2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.v6NumberTextBox2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6NumberTextBox2.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6NumberTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6NumberTextBox2.HoverColor = System.Drawing.Color.Yellow;
            this.v6NumberTextBox2.LeaveColor = System.Drawing.Color.White;
            this.v6NumberTextBox2.Location = new System.Drawing.Point(252, 295);
            this.v6NumberTextBox2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.v6NumberTextBox2.Name = "v6NumberTextBox2";
            this.v6NumberTextBox2.ReadOnly = true;
            this.v6NumberTextBox2.Size = new System.Drawing.Size(118, 23);
            this.v6NumberTextBox2.TabIndex = 105;
            this.v6NumberTextBox2.Text = "0,000";
            this.v6NumberTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.v6NumberTextBox2.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(76, 337);
            this.label14.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 17);
            this.label14.TabIndex = 106;
            this.label14.Text = "Số lượng ĐN 3";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(76, 299);
            this.label13.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 17);
            this.label13.TabIndex = 104;
            this.label13.Text = "Số lượng ĐN 2";
            // 
            // tabPage3
            // 
            this.tabPage3.AccessibleDescription = "XULYT00003";
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage3.Size = new System.Drawing.Size(881, 170);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Thông tin khác";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabChiTiet);
            this.tabControl1.Controls.Add(this.tabKhac);
            this.tabControl1.Location = new System.Drawing.Point(7, 215);
            this.tabControl1.MinimumSize = new System.Drawing.Size(0, 150);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(887, 347);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Enter += new System.EventHandler(this.tabControl1_Enter);
            // 
            // tabChiTiet
            // 
            this.tabChiTiet.AccessibleDescription = "XULYT00004";
            this.tabChiTiet.Controls.Add(this.detail1);
            this.tabChiTiet.Controls.Add(this.dataGridView1);
            this.tabChiTiet.Location = new System.Drawing.Point(4, 25);
            this.tabChiTiet.Name = "tabChiTiet";
            this.tabChiTiet.Padding = new System.Windows.Forms.Padding(3);
            this.tabChiTiet.Size = new System.Drawing.Size(879, 318);
            this.tabChiTiet.TabIndex = 0;
            this.tabChiTiet.Tag = "canceldata";
            this.tabChiTiet.Text = "Chi tiết";
            this.tabChiTiet.UseVisualStyleBackColor = true;
            // 
            // detail1
            // 
            this.detail1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detail1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detail1.Location = new System.Drawing.Point(2, 2);
            this.detail1.MODE = V6Structs.V6Mode.Init;
            this.detail1.Name = "detail1";
            this.detail1.ShowLblName = false;
            this.detail1.Size = new System.Drawing.Size(875, 49);
            this.detail1.Sua_tien = false;
            this.detail1.TabIndex = 0;
            this.detail1.Tag = "canceldata";
            this.detail1.Vtype = null;
            this.detail1.ClickAdd += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.detail1_ClickAdd);
            this.detail1.ClickEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.detail1_ClickEdit);
            this.detail1.ClickCancelEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.detail1_ClickCancelEdit);
            this.detail1.AddHandle += new V6Controls.HandleData(this.detail1_AddHandle);
            this.detail1.EditHandle += new V6Controls.HandleData(this.detail1_EditHandle);
            this.detail1.DeleteHandle += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.detail1_DeleteHandle);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MA_VT,
            this.TEN_VT,
            this.UID,
            this.STT_REC,
            this.STT_REC0});
            this.dataGridView1.Location = new System.Drawing.Point(2, 52);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(875, 265);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Tag = "cancelall";
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // MA_VT
            // 
            this.MA_VT.DataPropertyName = "MA_VT";
            this.MA_VT.Frozen = true;
            this.MA_VT.HeaderText = "Mã vật tư";
            this.MA_VT.Name = "MA_VT";
            this.MA_VT.ReadOnly = true;
            // 
            // TEN_VT
            // 
            this.TEN_VT.DataPropertyName = "TEN_VT";
            this.TEN_VT.Frozen = true;
            this.TEN_VT.HeaderText = "Tên vật tư";
            this.TEN_VT.Name = "TEN_VT";
            this.TEN_VT.ReadOnly = true;
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.ReadOnly = true;
            this.UID.Visible = false;
            // 
            // STT_REC
            // 
            this.STT_REC.DataPropertyName = "STT_REC";
            this.STT_REC.HeaderText = "Rec";
            this.STT_REC.Name = "STT_REC";
            this.STT_REC.ReadOnly = true;
            this.STT_REC.Visible = false;
            // 
            // STT_REC0
            // 
            this.STT_REC0.DataPropertyName = "STT_REC0";
            this.STT_REC0.HeaderText = "Rec0";
            this.STT_REC0.Name = "STT_REC0";
            this.STT_REC0.ReadOnly = true;
            this.STT_REC0.Visible = false;
            // 
            // tabKhac
            // 
            this.tabKhac.AccessibleDescription = "XULYT00005";
            this.tabKhac.Location = new System.Drawing.Point(4, 25);
            this.tabKhac.Name = "tabKhac";
            this.tabKhac.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhac.Size = new System.Drawing.Size(879, 318);
            this.tabKhac.TabIndex = 1;
            this.tabKhac.Text = "Khác";
            this.tabKhac.UseVisualStyleBackColor = true;
            // 
            // Acosxlt_aldmvtAddEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.v6TabControl1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Acosxlt_aldmvtAddEditControl";
            this.Size = new System.Drawing.Size(904, 568);
            this.Load += new System.EventHandler(this.SoDu2AddEditControl0_Load);
            this.v6TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private V6TabControl v6TabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private V6ColorTextBox v6ColorTextBox16;
        private V6ColorTextBox v6ColorTextBox15;
        private V6ColorTextBox v6ColorTextBox14;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private V6NumberTextBox v6NumberTextBox3;
        private V6NumberTextBox v6NumberTextBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        private V6VvarTextBox txtMaBpht;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage1;
        private V6VvarTextBox txtMaSp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChiTiet;
        private ChungTuManager.HD_Detail detail1;
        private V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_VT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEN_VT;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT_REC;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT_REC0;
        private System.Windows.Forms.TabPage tabKhac;
        private V6DateTimeColor dateNgayHL;
        private V6ColorTextBox txtSttRec;
        private V6ColorTextBox txtMaCt;
    }
}
