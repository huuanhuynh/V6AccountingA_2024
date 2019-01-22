namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho
{
    partial class XuatHetKhoDataForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label9 = new V6Controls.V6Label();
            this.txtKieuIn = new V6Controls.V6VvarTextBox();
            this.TxtVttonkho = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtma_lo = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt6 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt4 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt5 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt3 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt1 = new V6ReportControls.FilterLineVvarTextBox();
            this.Txtnh_vt2 = new V6ReportControls.FilterLineVvarTextBox();
            this.filterLineVvarTextBox1 = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtMakho = new V6ReportControls.FilterLineVvarTextBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.TxtMa_vt = new V6ReportControls.FilterLineVvarTextBox();
            this.chkTonHSD = new System.Windows.Forms.CheckBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.chkSoluong = new System.Windows.Forms.CheckBox();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoc
            // 
            this.btnLoc.AccessibleDescription = "AINCTIXAB00035";
            this.btnLoc.Image = global::V6ControlManager.Properties.Resources.Search;
            this.btnLoc.Location = new System.Drawing.Point(12, 6);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(141, 40);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(316, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(447, 515);
            this.dataGridView1.TabIndex = 13;
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "FILTERL00158";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(99, 107);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(170, 13);
            this.v6Label3.TabIndex = 7;
            this.v6Label3.Text = "* - Tất cả, 1- Tồn > 0 , 0 - Tồn <=0";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00157";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(99, 81);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(184, 13);
            this.v6Label1.TabIndex = 4;
            this.v6Label1.Text = "* - Tất cả, 1- Theo dõi tồn , 0 - Không";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "FILTERO00005";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(2, 105);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(70, 13);
            this.v6Label2.TabIndex = 2;
            this.v6Label2.Text = "Lọc hàng tồn";
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERO00004";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(2, 79);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(47, 13);
            this.v6Label9.TabIndex = 1;
            this.v6Label9.Text = "Tồn kho";
            // 
            // txtKieuIn
            // 
            this.txtKieuIn.AccessibleName = "";
            this.txtKieuIn.BackColor = System.Drawing.SystemColors.Window;
            this.txtKieuIn.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKieuIn.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKieuIn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKieuIn.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKieuIn.HoverColor = System.Drawing.Color.Yellow;
            this.txtKieuIn.LeaveColor = System.Drawing.Color.White;
            this.txtKieuIn.LimitCharacters = "*10";
            this.txtKieuIn.Location = new System.Drawing.Point(76, 104);
            this.txtKieuIn.MaxLength = 1;
            this.txtKieuIn.Name = "txtKieuIn";
            this.txtKieuIn.Size = new System.Drawing.Size(18, 20);
            this.txtKieuIn.TabIndex = 5;
            // 
            // TxtVttonkho
            // 
            this.TxtVttonkho.AccessibleName = "";
            this.TxtVttonkho.BackColor = System.Drawing.SystemColors.Window;
            this.TxtVttonkho.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtVttonkho.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtVttonkho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtVttonkho.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtVttonkho.HoverColor = System.Drawing.Color.Yellow;
            this.TxtVttonkho.LeaveColor = System.Drawing.Color.White;
            this.TxtVttonkho.LimitCharacters = "*10";
            this.TxtVttonkho.Location = new System.Drawing.Point(76, 78);
            this.TxtVttonkho.MaxLength = 1;
            this.TxtVttonkho.Name = "TxtVttonkho";
            this.TxtVttonkho.Size = new System.Drawing.Size(18, 20);
            this.TxtVttonkho.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00003";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Đến ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(62, 52);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(101, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Controls.Add(this.txtma_lo);
            this.groupBox1.Controls.Add(this.Txtnh_vt6);
            this.groupBox1.Controls.Add(this.Txtnh_vt4);
            this.groupBox1.Controls.Add(this.Txtnh_vt5);
            this.groupBox1.Controls.Add(this.Txtnh_vt3);
            this.groupBox1.Controls.Add(this.Txtnh_vt1);
            this.groupBox1.Controls.Add(this.Txtnh_vt2);
            this.groupBox1.Controls.Add(this.filterLineVvarTextBox1);
            this.groupBox1.Controls.Add(this.TxtMakho);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.txtMaDvcs);
            this.groupBox1.Controls.Add(this.TxtMa_vt);
            this.groupBox1.Location = new System.Drawing.Point(1, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 290);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // txtma_lo
            // 
            this.txtma_lo.AccessibleDescription = "FILTERL00159";
            this.txtma_lo.Caption = "Mã lô";
            this.txtma_lo.FieldName = "MA_LO";
            this.txtma_lo.Location = new System.Drawing.Point(6, 83);
            this.txtma_lo.Name = "txtma_lo";
            this.txtma_lo.Size = new System.Drawing.Size(292, 22);
            this.txtma_lo.TabIndex = 4;
            this.txtma_lo.Vvar = "MA_LO";
            // 
            // Txtnh_vt6
            // 
            this.Txtnh_vt6.AccessibleDescription = "FILTERL00036";
            this.Txtnh_vt6.Caption = "Nhóm vật tư 6";
            this.Txtnh_vt6.FieldName = "NH_VT6";
            this.Txtnh_vt6.Location = new System.Drawing.Point(6, 264);
            this.Txtnh_vt6.Name = "Txtnh_vt6";
            this.Txtnh_vt6.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt6.TabIndex = 12;
            this.Txtnh_vt6.Vvar = "NH_VT";
            // 
            // Txtnh_vt4
            // 
            this.Txtnh_vt4.AccessibleDescription = "FILTERL00034";
            this.Txtnh_vt4.Caption = "Nhóm vật tư 4";
            this.Txtnh_vt4.FieldName = "NH_VT4";
            this.Txtnh_vt4.Location = new System.Drawing.Point(6, 217);
            this.Txtnh_vt4.Name = "Txtnh_vt4";
            this.Txtnh_vt4.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt4.TabIndex = 10;
            this.Txtnh_vt4.Vvar = "NH_VT";
            // 
            // Txtnh_vt5
            // 
            this.Txtnh_vt5.AccessibleDescription = "FILTERL00035";
            this.Txtnh_vt5.Caption = "Nhóm vật tư 5";
            this.Txtnh_vt5.FieldName = "NH_VT5";
            this.Txtnh_vt5.Location = new System.Drawing.Point(6, 241);
            this.Txtnh_vt5.Name = "Txtnh_vt5";
            this.Txtnh_vt5.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt5.TabIndex = 11;
            this.Txtnh_vt5.Vvar = "NH_VT";
            // 
            // Txtnh_vt3
            // 
            this.Txtnh_vt3.AccessibleDescription = "FILTERL00033";
            this.Txtnh_vt3.Caption = "Nhóm vật tư 3";
            this.Txtnh_vt3.FieldName = "NH_VT3";
            this.Txtnh_vt3.Location = new System.Drawing.Point(6, 195);
            this.Txtnh_vt3.Name = "Txtnh_vt3";
            this.Txtnh_vt3.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt3.TabIndex = 9;
            this.Txtnh_vt3.Vvar = "NH_VT";
            // 
            // Txtnh_vt1
            // 
            this.Txtnh_vt1.AccessibleDescription = "FILTERL00031";
            this.Txtnh_vt1.Caption = "Nhóm vật tư 1";
            this.Txtnh_vt1.FieldName = "NH_VT1";
            this.Txtnh_vt1.Location = new System.Drawing.Point(6, 151);
            this.Txtnh_vt1.Name = "Txtnh_vt1";
            this.Txtnh_vt1.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt1.TabIndex = 7;
            this.Txtnh_vt1.Vvar = "NH_VT";
            // 
            // Txtnh_vt2
            // 
            this.Txtnh_vt2.AccessibleDescription = "FILTERL00032";
            this.Txtnh_vt2.Caption = "Nhóm vật tư 2";
            this.Txtnh_vt2.FieldName = "NH_VT2";
            this.Txtnh_vt2.Location = new System.Drawing.Point(6, 173);
            this.Txtnh_vt2.Name = "Txtnh_vt2";
            this.Txtnh_vt2.Size = new System.Drawing.Size(292, 22);
            this.Txtnh_vt2.TabIndex = 8;
            this.Txtnh_vt2.Vvar = "NH_VT";
            // 
            // filterLineVvarTextBox1
            // 
            this.filterLineVvarTextBox1.AccessibleDescription = "FILTERL00160";
            this.filterLineVvarTextBox1.Caption = "Mã vị trí";
            this.filterLineVvarTextBox1.FieldName = "MA_VITRI";
            this.filterLineVvarTextBox1.Location = new System.Drawing.Point(6, 128);
            this.filterLineVvarTextBox1.Name = "filterLineVvarTextBox1";
            this.filterLineVvarTextBox1.Size = new System.Drawing.Size(292, 22);
            this.filterLineVvarTextBox1.TabIndex = 6;
            this.filterLineVvarTextBox1.Vvar = "MA_VITRI";
            // 
            // TxtMakho
            // 
            this.TxtMakho.AccessibleDescription = "FILTERL00006";
            this.TxtMakho.Caption = "Mã kho";
            this.TxtMakho.FieldName = "MA_KHO";
            this.TxtMakho.Location = new System.Drawing.Point(6, 39);
            this.TxtMakho.Name = "TxtMakho";
            this.TxtMakho.Size = new System.Drawing.Size(292, 22);
            this.TxtMakho.TabIndex = 2;
            this.TxtMakho.Vvar = "MA_KHO";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 16);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "Điều kiện (OR)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 16);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 61);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(292, 22);
            this.txtMaDvcs.TabIndex = 3;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // TxtMa_vt
            // 
            this.TxtMa_vt.AccessibleDescription = "FILTERL00020";
            this.TxtMa_vt.Caption = "Mã vật tư";
            this.TxtMa_vt.FieldName = "MA_VT";
            this.TxtMa_vt.Location = new System.Drawing.Point(6, 105);
            this.TxtMa_vt.Name = "TxtMa_vt";
            this.TxtMa_vt.Size = new System.Drawing.Size(292, 22);
            this.TxtMa_vt.TabIndex = 5;
            this.TxtMa_vt.Vvar = "MA_VT";
            // 
            // chkTonHSD
            // 
            this.chkTonHSD.AccessibleDescription = "FILTERC00028";
            this.chkTonHSD.AutoSize = true;
            this.chkTonHSD.Location = new System.Drawing.Point(77, 129);
            this.chkTonHSD.Name = "chkTonHSD";
            this.chkTonHSD.Size = new System.Drawing.Size(153, 17);
            this.chkTonHSD.TabIndex = 6;
            this.chkTonHSD.Text = "  Lấy tồn theo hạn sử dụng";
            this.chkTonHSD.UseVisualStyleBackColor = true;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 470);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 15;
            this.btnHuy.Tag = "Escape";
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
            this.btnNhan.Location = new System.Drawing.Point(12, 470);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 14;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // chkSoluong
            // 
            this.chkSoluong.AccessibleDescription = "FILTERC00024";
            this.chkSoluong.AutoSize = true;
            this.chkSoluong.Location = new System.Drawing.Point(77, 151);
            this.chkSoluong.Name = "chkSoluong";
            this.chkSoluong.Size = new System.Drawing.Size(103, 17);
            this.chkSoluong.TabIndex = 7;
            this.chkSoluong.Text = " Chỉ lấy số lượng";
            this.chkSoluong.UseVisualStyleBackColor = true;
            // 
            // timerViewReport
            // 
            this.timerViewReport.Tick += new System.EventHandler(this.timerViewReport_Tick);
            // 
            // XuatHetKhoDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 522);
            this.Controls.Add(this.chkSoluong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.chkTonHSD);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.txtKieuIn);
            this.Controls.Add(this.TxtVttonkho);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateNgay_ct2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLoc);
            this.MinimumSize = new System.Drawing.Size(780, 560);
            this.Name = "XuatHetKhoDataForm";
            this.Text = "XuatHetKhoDataForm";
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnLoc, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dateNgay_ct2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.TxtVttonkho, 0);
            this.Controls.SetChildIndex(this.txtKieuIn, 0);
            this.Controls.SetChildIndex(this.v6Label9, 0);
            this.Controls.SetChildIndex(this.v6Label2, 0);
            this.Controls.SetChildIndex(this.v6Label1, 0);
            this.Controls.SetChildIndex(this.v6Label3, 0);
            this.Controls.SetChildIndex(this.chkTonHSD, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.chkSoluong, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoc;
        protected V6Controls.V6ColorDataGridView dataGridView1;
        private V6Controls.V6Label v6Label3;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label9;
        private V6Controls.V6VvarTextBox txtKieuIn;
        private V6Controls.V6VvarTextBox TxtVttonkho;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePicker dateNgay_ct2;
        private System.Windows.Forms.GroupBox groupBox1;
        private V6ReportControls.FilterLineVvarTextBox txtma_lo;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt6;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt4;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt5;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt3;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt1;
        private V6ReportControls.FilterLineVvarTextBox Txtnh_vt2;
        private V6ReportControls.FilterLineVvarTextBox filterLineVvarTextBox1;
        private V6ReportControls.FilterLineVvarTextBox TxtMakho;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6ReportControls.FilterLineVvarTextBox TxtMa_vt;
        private System.Windows.Forms.CheckBox chkTonHSD;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.CheckBox chkSoluong;
        private System.Windows.Forms.Timer timerViewReport;
    }
}