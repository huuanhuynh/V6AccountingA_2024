using System.Windows.Forms;
using V6Controls;
using V6Controls.Controls;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuThu
{
    partial class PhieuThuControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChiTiet = new System.Windows.Forms.TabPage();
            this.detail1 = new V6ControlManager.FormManager.ChungTuManager.HD_Detail();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.TK_I = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_TK_I = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT_REC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT_REC0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabKhac = new System.Windows.Forms.TabPage();
            this.btnInfos = new System.Windows.Forms.Button();
            this.txtGhiChuChung = new V6Controls.V6VvarTextBox();
            this.txtSoCtKemt = new V6Controls.V6VvarTextBox();
            this.txtGC_UD3 = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGC_UD2 = new V6Controls.V6VvarTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGC_UD1 = new V6Controls.V6VvarTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSL_UD3 = new V6Controls.V6NumberTextBox();
            this.txtSL_UD2 = new V6Controls.V6NumberTextBox();
            this.txtSL_UD1 = new V6Controls.V6NumberTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMA_UD3 = new V6Controls.V6VvarTextBox();
            this.txtMA_UD2 = new V6Controls.V6VvarTextBox();
            this.txtMA_UD1 = new V6Controls.V6VvarTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtNGAY_UD3 = new V6Controls.V6DateTimeColor();
            this.txtNGAY_UD2 = new V6Controls.V6DateTimeColor();
            this.txtNGAY_UD1 = new V6Controls.V6DateTimeColor();
            this.tabChiTietBoSung = new System.Windows.Forms.TabPage();
            this.gridViewSummary3 = new V6Controls.Controls.GridViewSummary();
            this.dataGridView3 = new V6Controls.V6ColorDataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_TK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detail3 = new V6ControlManager.FormManager.ChungTuManager.HD_Detail();
            this.group5 = new System.Windows.Forms.GroupBox();
            this.panelVND = new System.Windows.Forms.Panel();
            this.txtTongThanhToan = new V6Controls.NumberTien();
            this.txtTongTangGiam = new V6Controls.NumberTienNt();
            this.panelNT = new System.Windows.Forms.Panel();
            this.lblTongTangGiam = new V6Controls.V6Label();
            this.lblTTT = new V6Controls.V6Label();
            this.txtTongTangGiamNt = new V6Controls.NumberTienNt();
            this.txtTongThanhToanNt = new V6Controls.NumberTienNt();
            this.group4 = new System.Windows.Forms.GroupBox();
            this.chkAutoNext = new V6Controls.V6CheckBox();
            this.txtSoct_tt = new V6Controls.V6VvarTextBox();
            this.btnChonHD = new V6Controls.Controls.V6FormButton();
            this.lblKieuPostColor = new V6Controls.V6Label();
            this.cboChuyenData = new V6Controls.V6ComboBox();
            this.v6Label28 = new V6Controls.V6Label();
            this.btnChucNang = new V6Controls.Controls.DropDownButton();
            this.menuChucNang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ThuCongNoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ThuCuocContMent = new System.Windows.Forms.ToolStripMenuItem();
            this.TroGiupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chonTuExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.xuLyKhacMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.thayTheMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.thayTheNhieuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.thayThe2Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.thuNoTaiKhoanMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportXmlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importXmlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSuaTien = new V6Controls.V6CheckBox();
            this.cboKieuPost = new V6Controls.V6ColorComboBox();
            this.lblTongSoDong = new V6Controls.V6Label();
            this.v6Label20 = new V6Controls.V6Label();
            this.group3 = new System.Windows.Forms.GroupBox();
            this.txtTenGiaoDich = new V6Controls.V6VvarTextBox();
            this.v6Label14 = new V6Controls.V6Label();
            this.btnChonNhieuHD = new V6Controls.Controls.V6FormButton();
            this.txtMaDVCS = new V6Controls.V6VvarTextBox();
            this.txtTenDVCS = new V6Controls.V6VvarTextBox();
            this.lblMaDVCS = new V6Controls.V6Label();
            this.txtTyGia = new V6Controls.V6NumberTextBox();
            this.cboMaNt = new V6Controls.V6ComboBox();
            this.txtLoaiPhieu = new V6Controls.V6VvarTextBox();
            this.txtMaKh = new V6Controls.V6VvarTextBox();
            this.v6Label17 = new V6Controls.V6Label();
            this.Txtdien_giai = new V6Controls.V6VvarTextBox();
            this.v6Label12 = new V6Controls.V6Label();
            this.txtMaSoThue = new V6Controls.V6VvarTextBox();
            this.txtTenKh = new V6Controls.V6VvarTextBox();
            this.txtDiaChi = new V6Controls.V6VvarTextBox();
            this.v6Label11 = new V6Controls.V6Label();
            this.lblMaKH = new V6Controls.V6Label();
            this.group2 = new System.Windows.Forms.GroupBox();
            this.TxtTen_tk = new V6Controls.V6VvarTextBox();
            this.Txtma_nvien = new V6Controls.V6VvarTextBox();
            this.TxtMa_bp = new V6Controls.V6VvarTextBox();
            this.v6Label7 = new V6Controls.V6Label();
            this.v6Label8 = new V6Controls.V6Label();
            this.lblTK = new V6Controls.V6Label();
            this.v6ColorTextBox1 = new V6Controls.V6VvarTextBox();
            this.txtTk = new V6Controls.V6VvarTextBox();
            this.group1 = new System.Windows.Forms.GroupBox();
            this.txtMa_sonb = new V6Controls.V6VvarTextBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.dateNgayLCT = new V6Controls.V6DateTimePicker();
            this.dateNgayCT = new V6Controls.V6DateTimePicker();
            this.v6Label5 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.txtSoPhieu = new V6Controls.V6VvarTextBox();
            this.Txtma_nk = new V6Controls.V6VvarTextBox();
            this.txtMa_ct = new V6Controls.V6VvarTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDocSoTien = new V6Controls.V6Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnQuayRa = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnIn = new wyDay.Controls.SplitButton();
            this.menuBtnIn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inPhieuHachToanMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.inKhacMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnTim = new wyDay.Controls.SplitButton();
            this.menuBtnTim = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timTopCuoiKyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timKhacMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnViewInfoData = new System.Windows.Forms.Button();
            this.lblNameT = new V6Controls.V6Label();
            this.tabControl1.SuspendLayout();
            this.tabChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabKhac.SuspendLayout();
            this.tabChiTietBoSung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.group5.SuspendLayout();
            this.panelVND.SuspendLayout();
            this.panelNT.SuspendLayout();
            this.group4.SuspendLayout();
            this.menuChucNang.SuspendLayout();
            this.group3.SuspendLayout();
            this.group2.SuspendLayout();
            this.group1.SuspendLayout();
            this.menuBtnIn.SuspendLayout();
            this.menuBtnTim.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabChiTiet);
            this.tabControl1.Controls.Add(this.tabKhac);
            this.tabControl1.Controls.Add(this.tabChiTietBoSung);
            this.tabControl1.Location = new System.Drawing.Point(8, 182);
            this.tabControl1.MinimumSize = new System.Drawing.Size(0, 150);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 216);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.Tag = "";
            this.tabControl1.SizeChanged += new System.EventHandler(this.tabControl1_SizeChanged);
            this.tabControl1.Enter += new System.EventHandler(this.tabControl1_Enter);
            // 
            // tabChiTiet
            // 
            this.tabChiTiet.AccessibleDescription = "ACACTTA1P00001";
            this.tabChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabChiTiet.Controls.Add(this.detail1);
            this.tabChiTiet.Controls.Add(this.dataGridView1);
            this.tabChiTiet.Location = new System.Drawing.Point(4, 22);
            this.tabChiTiet.Name = "tabChiTiet";
            this.tabChiTiet.Padding = new System.Windows.Forms.Padding(3);
            this.tabChiTiet.Size = new System.Drawing.Size(880, 190);
            this.tabChiTiet.TabIndex = 0;
            this.tabChiTiet.Tag = "cancelall";
            this.tabChiTiet.Text = "Chi tiết";
            // 
            // detail1
            // 
            this.detail1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.detail1.CarryData = null;
            this.detail1.FixControl = 3;
            this.detail1.Location = new System.Drawing.Point(2, 2);
            this.detail1.MODE = V6Structs.V6Mode.Init;
            this.detail1.Name = "detail1";
            this.detail1.ShowLblName = false;
            this.detail1.Size = new System.Drawing.Size(876, 50);
            this.detail1.Sua_tien = false;
            this.detail1.TabIndex = 2;
            this.detail1.Tag = "cancelall";
            this.detail1.Vtype = null;
            this.detail1.ClickAdd += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.Detail1_ClickAdd);
            this.detail1.ClickEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.phieuThuDetail1_ClickEdit);
            this.detail1.ClickCancelEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.phieuThuDetail1_ClickCancelEdit);
            this.detail1.AddHandle += new V6Controls.HandleData(this.hoaDonDetail1_AddHandle);
            this.detail1.EditHandle += new V6Controls.HandleData(this.hoaDonDetail1_EditHandle);
            this.detail1.ClickDelete += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.hoaDonDetail1_ClickDelete);
            this.detail1.LabelNameTextChanged += new System.EventHandler(this.detail1_LabelNameTextChanged);
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
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TK_I,
            this.TEN_TK_I,
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
            this.dataGridView1.Size = new System.Drawing.Size(876, 138);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Tag = "cancelall";
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // TK_I
            // 
            this.TK_I.DataPropertyName = "TK_I";
            this.TK_I.Frozen = true;
            this.TK_I.HeaderText = "Tài khoản";
            this.TK_I.Name = "TK_I";
            this.TK_I.ReadOnly = true;
            // 
            // TEN_TK_I
            // 
            this.TEN_TK_I.DataPropertyName = "TEN_TK_I";
            this.TEN_TK_I.Frozen = true;
            this.TEN_TK_I.HeaderText = "Tên tài khoản";
            this.TEN_TK_I.Name = "TEN_TK_I";
            this.TEN_TK_I.ReadOnly = true;
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
            this.tabKhac.AccessibleDescription = "ACACTTA1P00035";
            this.tabKhac.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabKhac.Controls.Add(this.btnInfos);
            this.tabKhac.Controls.Add(this.txtGhiChuChung);
            this.tabKhac.Controls.Add(this.txtSoCtKemt);
            this.tabKhac.Controls.Add(this.txtGC_UD3);
            this.tabKhac.Controls.Add(this.label2);
            this.tabKhac.Controls.Add(this.txtGC_UD2);
            this.tabKhac.Controls.Add(this.label1);
            this.tabKhac.Controls.Add(this.txtGC_UD1);
            this.tabKhac.Controls.Add(this.label29);
            this.tabKhac.Controls.Add(this.label28);
            this.tabKhac.Controls.Add(this.label27);
            this.tabKhac.Controls.Add(this.txtSL_UD3);
            this.tabKhac.Controls.Add(this.txtSL_UD2);
            this.tabKhac.Controls.Add(this.txtSL_UD1);
            this.tabKhac.Controls.Add(this.label16);
            this.tabKhac.Controls.Add(this.label17);
            this.tabKhac.Controls.Add(this.label22);
            this.tabKhac.Controls.Add(this.txtMA_UD3);
            this.tabKhac.Controls.Add(this.txtMA_UD2);
            this.tabKhac.Controls.Add(this.txtMA_UD1);
            this.tabKhac.Controls.Add(this.label30);
            this.tabKhac.Controls.Add(this.label31);
            this.tabKhac.Controls.Add(this.label32);
            this.tabKhac.Controls.Add(this.label33);
            this.tabKhac.Controls.Add(this.label34);
            this.tabKhac.Controls.Add(this.label35);
            this.tabKhac.Controls.Add(this.txtNGAY_UD3);
            this.tabKhac.Controls.Add(this.txtNGAY_UD2);
            this.tabKhac.Controls.Add(this.txtNGAY_UD1);
            this.tabKhac.Location = new System.Drawing.Point(4, 22);
            this.tabKhac.Name = "tabKhac";
            this.tabKhac.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhac.Size = new System.Drawing.Size(880, 190);
            this.tabKhac.TabIndex = 1;
            this.tabKhac.Text = "Khác";
            // 
            // btnInfos
            // 
            this.btnInfos.AccessibleDescription = "ASOCTSOAB00040";
            this.btnInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfos.Location = new System.Drawing.Point(673, 9);
            this.btnInfos.Name = "btnInfos";
            this.btnInfos.Size = new System.Drawing.Size(197, 29);
            this.btnInfos.TabIndex = 0;
            this.btnInfos.TabStop = false;
            this.btnInfos.Text = "&Thông tin về các trường định nghĩa";
            this.btnInfos.UseVisualStyleBackColor = true;
            this.btnInfos.Click += new System.EventHandler(this.btnInfos_Click);
            // 
            // txtGhiChuChung
            // 
            this.txtGhiChuChung.AccessibleName = "GHI_CHU0";
            this.txtGhiChuChung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChuChung.BackColor = System.Drawing.Color.White;
            this.txtGhiChuChung.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGhiChuChung.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGhiChuChung.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGhiChuChung.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGhiChuChung.HoverColor = System.Drawing.Color.Yellow;
            this.txtGhiChuChung.LeaveColor = System.Drawing.Color.White;
            this.txtGhiChuChung.Location = new System.Drawing.Point(436, 145);
            this.txtGhiChuChung.Margin = new System.Windows.Forms.Padding(5);
            this.txtGhiChuChung.Multiline = true;
            this.txtGhiChuChung.Name = "txtGhiChuChung";
            this.txtGhiChuChung.Size = new System.Drawing.Size(432, 37);
            this.txtGhiChuChung.TabIndex = 28;
            // 
            // txtSoCtKemt
            // 
            this.txtSoCtKemt.AccessibleName = "SO_CT_KEMT";
            this.txtSoCtKemt.BackColor = System.Drawing.Color.White;
            this.txtSoCtKemt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoCtKemt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoCtKemt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoCtKemt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoCtKemt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoCtKemt.LeaveColor = System.Drawing.Color.White;
            this.txtSoCtKemt.Location = new System.Drawing.Point(143, 145);
            this.txtSoCtKemt.Margin = new System.Windows.Forms.Padding(5);
            this.txtSoCtKemt.Name = "txtSoCtKemt";
            this.txtSoCtKemt.Size = new System.Drawing.Size(135, 20);
            this.txtSoCtKemt.TabIndex = 14;
            // 
            // txtGC_UD3
            // 
            this.txtGC_UD3.AccessibleName = "GC_UD3";
            this.txtGC_UD3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_UD3.BackColor = System.Drawing.Color.White;
            this.txtGC_UD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_UD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_UD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_UD3.LeaveColor = System.Drawing.Color.White;
            this.txtGC_UD3.Location = new System.Drawing.Point(436, 121);
            this.txtGC_UD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_UD3.Name = "txtGC_UD3";
            this.txtGC_UD3.Size = new System.Drawing.Size(432, 20);
            this.txtGC_UD3.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ACACTTA1H00005";
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Ghi chú chung";
            // 
            // txtGC_UD2
            // 
            this.txtGC_UD2.AccessibleName = "GC_UD2";
            this.txtGC_UD2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_UD2.BackColor = System.Drawing.Color.White;
            this.txtGC_UD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_UD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_UD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_UD2.LeaveColor = System.Drawing.Color.White;
            this.txtGC_UD2.Location = new System.Drawing.Point(436, 97);
            this.txtGC_UD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_UD2.Name = "txtGC_UD2";
            this.txtGC_UD2.Size = new System.Drawing.Size(432, 20);
            this.txtGC_UD2.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "INVOICEL00008";
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Số chứng từ kèm theo";
            // 
            // txtGC_UD1
            // 
            this.txtGC_UD1.AccessibleName = "GC_UD1";
            this.txtGC_UD1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGC_UD1.BackColor = System.Drawing.Color.White;
            this.txtGC_UD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtGC_UD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtGC_UD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtGC_UD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtGC_UD1.LeaveColor = System.Drawing.Color.White;
            this.txtGC_UD1.Location = new System.Drawing.Point(436, 73);
            this.txtGC_UD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtGC_UD1.Name = "txtGC_UD1";
            this.txtGC_UD1.Size = new System.Drawing.Size(432, 20);
            this.txtGC_UD1.TabIndex = 22;
            // 
            // label29
            // 
            this.label29.AccessibleDescription = "M_GC_TD3,GC_UD3";
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(304, 127);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 13);
            this.label29.TabIndex = 25;
            this.label29.Text = "Ghi chú ĐN 3";
            // 
            // label28
            // 
            this.label28.AccessibleDescription = "M_GC_TD2,GC_UD2";
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(304, 103);
            this.label28.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(72, 13);
            this.label28.TabIndex = 23;
            this.label28.Text = "Ghi chú ĐN 2";
            // 
            // label27
            // 
            this.label27.AccessibleDescription = "M_GC_TD1,GC_UD1";
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(304, 79);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(72, 13);
            this.label27.TabIndex = 21;
            this.label27.Text = "Ghi chú ĐN 1";
            // 
            // txtSL_UD3
            // 
            this.txtSL_UD3.AccessibleName = "SL_UD3";
            this.txtSL_UD3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSL_UD3.BackColor = System.Drawing.Color.White;
            this.txtSL_UD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSL_UD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSL_UD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtSL_UD3.LeaveColor = System.Drawing.Color.White;
            this.txtSL_UD3.Location = new System.Drawing.Point(436, 51);
            this.txtSL_UD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtSL_UD3.Name = "txtSL_UD3";
            this.txtSL_UD3.Size = new System.Drawing.Size(135, 20);
            this.txtSL_UD3.TabIndex = 20;
            this.txtSL_UD3.Text = "0,000";
            this.txtSL_UD3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSL_UD3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSL_UD2
            // 
            this.txtSL_UD2.AccessibleName = "SL_UD2";
            this.txtSL_UD2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSL_UD2.BackColor = System.Drawing.Color.White;
            this.txtSL_UD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSL_UD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSL_UD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtSL_UD2.LeaveColor = System.Drawing.Color.White;
            this.txtSL_UD2.Location = new System.Drawing.Point(436, 27);
            this.txtSL_UD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtSL_UD2.Name = "txtSL_UD2";
            this.txtSL_UD2.Size = new System.Drawing.Size(135, 20);
            this.txtSL_UD2.TabIndex = 18;
            this.txtSL_UD2.Text = "0,000";
            this.txtSL_UD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSL_UD2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSL_UD1
            // 
            this.txtSL_UD1.AccessibleName = "SL_UD1";
            this.txtSL_UD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSL_UD1.BackColor = System.Drawing.Color.White;
            this.txtSL_UD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSL_UD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSL_UD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSL_UD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtSL_UD1.LeaveColor = System.Drawing.Color.White;
            this.txtSL_UD1.Location = new System.Drawing.Point(436, 4);
            this.txtSL_UD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtSL_UD1.Name = "txtSL_UD1";
            this.txtSL_UD1.Size = new System.Drawing.Size(135, 20);
            this.txtSL_UD1.TabIndex = 16;
            this.txtSL_UD1.Text = "0,000";
            this.txtSL_UD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSL_UD1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AccessibleDescription = "M_SL_TD3,SL_UD3";
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(304, 54);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "SL ĐN 3";
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "M_SL_TD2,SL_UD2";
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(304, 29);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "SL ĐN 2";
            // 
            // label22
            // 
            this.label22.AccessibleDescription = "M_SL_TD1,SL_UD1";
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(304, 7);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 15;
            this.label22.Text = "SL ĐN 1";
            // 
            // txtMA_UD3
            // 
            this.txtMA_UD3.AccessibleName = "MA_UD3";
            this.txtMA_UD3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_UD3.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_UD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_UD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_UD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_UD3.LeaveColor = System.Drawing.Color.White;
            this.txtMA_UD3.Location = new System.Drawing.Point(143, 51);
            this.txtMA_UD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_UD3.Name = "txtMA_UD3";
            this.txtMA_UD3.Size = new System.Drawing.Size(135, 20);
            this.txtMA_UD3.TabIndex = 6;
            // 
            // txtMA_UD2
            // 
            this.txtMA_UD2.AccessibleName = "MA_UD2";
            this.txtMA_UD2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_UD2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMA_UD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_UD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_UD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_UD2.LeaveColor = System.Drawing.Color.White;
            this.txtMA_UD2.Location = new System.Drawing.Point(143, 27);
            this.txtMA_UD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_UD2.Name = "txtMA_UD2";
            this.txtMA_UD2.Size = new System.Drawing.Size(135, 20);
            this.txtMA_UD2.TabIndex = 4;
            // 
            // txtMA_UD1
            // 
            this.txtMA_UD1.AccessibleName = "MA_UD1";
            this.txtMA_UD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMA_UD1.BackColor = System.Drawing.Color.White;
            this.txtMA_UD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_UD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_UD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_UD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_UD1.LeaveColor = System.Drawing.Color.White;
            this.txtMA_UD1.Location = new System.Drawing.Point(143, 4);
            this.txtMA_UD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtMA_UD1.Name = "txtMA_UD1";
            this.txtMA_UD1.Size = new System.Drawing.Size(135, 20);
            this.txtMA_UD1.TabIndex = 2;
            // 
            // label30
            // 
            this.label30.AccessibleDescription = "M_NGAY_TD3,NGAY_UD3";
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(11, 127);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 13);
            this.label30.TabIndex = 11;
            this.label30.Text = "Ngày ĐN 3";
            // 
            // label31
            // 
            this.label31.AccessibleDescription = "M_NGAY_TD2,NGAY_UD2";
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 103);
            this.label31.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(60, 13);
            this.label31.TabIndex = 9;
            this.label31.Text = "Ngày ĐN 2";
            // 
            // label32
            // 
            this.label32.AccessibleDescription = "M_NGAY_TD1,NGAY_UD1";
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(11, 79);
            this.label32.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 7;
            this.label32.Text = "Ngày ĐN 1";
            // 
            // label33
            // 
            this.label33.AccessibleDescription = "M_MA_TD3,MA_UD3";
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(11, 54);
            this.label33.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(50, 13);
            this.label33.TabIndex = 5;
            this.label33.Text = "Mã ĐN 3";
            // 
            // label34
            // 
            this.label34.AccessibleDescription = "M_MA_TD2,MA_UD2";
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(11, 29);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(50, 13);
            this.label34.TabIndex = 3;
            this.label34.Text = "Mã ĐN 2";
            // 
            // label35
            // 
            this.label35.AccessibleDescription = "M_MA_TD1,MA_UD1";
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(11, 7);
            this.label35.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Mã ĐN 1";
            // 
            // txtNGAY_UD3
            // 
            this.txtNGAY_UD3.AccessibleName = "NGAY_UD3";
            this.txtNGAY_UD3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNGAY_UD3.BackColor = System.Drawing.SystemColors.Window;
            this.txtNGAY_UD3.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNGAY_UD3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNGAY_UD3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD3.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD3.GrayText = null;
            this.txtNGAY_UD3.HoverColor = System.Drawing.Color.Yellow;
            this.txtNGAY_UD3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNGAY_UD3.LeaveColor = System.Drawing.Color.White;
            this.txtNGAY_UD3.Location = new System.Drawing.Point(143, 121);
            this.txtNGAY_UD3.Margin = new System.Windows.Forms.Padding(5);
            this.txtNGAY_UD3.Name = "txtNGAY_UD3";
            this.txtNGAY_UD3.Size = new System.Drawing.Size(135, 20);
            this.txtNGAY_UD3.StringValue = "__/__/____";
            this.txtNGAY_UD3.TabIndex = 12;
            this.txtNGAY_UD3.Text = "__/__/____";
            // 
            // txtNGAY_UD2
            // 
            this.txtNGAY_UD2.AccessibleName = "NGAY_UD2";
            this.txtNGAY_UD2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNGAY_UD2.BackColor = System.Drawing.SystemColors.Window;
            this.txtNGAY_UD2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNGAY_UD2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNGAY_UD2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD2.GrayText = null;
            this.txtNGAY_UD2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNGAY_UD2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNGAY_UD2.LeaveColor = System.Drawing.Color.White;
            this.txtNGAY_UD2.Location = new System.Drawing.Point(143, 97);
            this.txtNGAY_UD2.Margin = new System.Windows.Forms.Padding(5);
            this.txtNGAY_UD2.Name = "txtNGAY_UD2";
            this.txtNGAY_UD2.Size = new System.Drawing.Size(135, 20);
            this.txtNGAY_UD2.StringValue = "__/__/____";
            this.txtNGAY_UD2.TabIndex = 10;
            this.txtNGAY_UD2.Text = "__/__/____";
            // 
            // txtNGAY_UD1
            // 
            this.txtNGAY_UD1.AccessibleName = "NGAY_UD1";
            this.txtNGAY_UD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNGAY_UD1.BackColor = System.Drawing.Color.White;
            this.txtNGAY_UD1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNGAY_UD1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNGAY_UD1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNGAY_UD1.GrayText = null;
            this.txtNGAY_UD1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNGAY_UD1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtNGAY_UD1.LeaveColor = System.Drawing.Color.White;
            this.txtNGAY_UD1.Location = new System.Drawing.Point(143, 73);
            this.txtNGAY_UD1.Margin = new System.Windows.Forms.Padding(5);
            this.txtNGAY_UD1.Name = "txtNGAY_UD1";
            this.txtNGAY_UD1.Size = new System.Drawing.Size(135, 20);
            this.txtNGAY_UD1.StringValue = "__/__/____";
            this.txtNGAY_UD1.TabIndex = 8;
            this.txtNGAY_UD1.Text = "__/__/____";
            // 
            // tabChiTietBoSung
            // 
            this.tabChiTietBoSung.AccessibleDescription = "ACACTTA1L00070";
            this.tabChiTietBoSung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabChiTietBoSung.Controls.Add(this.gridViewSummary3);
            this.tabChiTietBoSung.Controls.Add(this.detail3);
            this.tabChiTietBoSung.Controls.Add(this.dataGridView3);
            this.tabChiTietBoSung.Location = new System.Drawing.Point(4, 22);
            this.tabChiTietBoSung.Name = "tabChiTietBoSung";
            this.tabChiTietBoSung.Padding = new System.Windows.Forms.Padding(3);
            this.tabChiTietBoSung.Size = new System.Drawing.Size(880, 190);
            this.tabChiTietBoSung.TabIndex = 2;
            this.tabChiTietBoSung.Tag = "cancelall";
            this.tabChiTietBoSung.Text = "Chi tiết bổ sung";
            // 
            // gridViewSummary3
            // 
            this.gridViewSummary3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewSummary3.DataGridView = this.dataGridView3;
            this.gridViewSummary3.Location = new System.Drawing.Point(2, 167);
            this.gridViewSummary3.Name = "gridViewSummary3";
            this.gridViewSummary3.NoSumColumns = "MAU_BC;GIA;GIA_NT;TY_GIA;HAN_TT;THUE_SUAT";
            this.gridViewSummary3.Size = new System.Drawing.Size(876, 23);
            this.gridViewSummary3.SumCondition = null;
            this.gridViewSummary3.TabIndex = 0;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.TEN_TK,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9});
            this.dataGridView3.Location = new System.Drawing.Point(2, 52);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView3.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView3.Size = new System.Drawing.Size(876, 115);
            this.dataGridView3.TabIndex = 3;
            this.dataGridView3.Tag = "cancelall";
            this.dataGridView3.DataSourceChanged += new System.EventHandler(this.dataGridView3_SelectionChanged);
            this.dataGridView3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView3_ColumnAdded);
            this.dataGridView3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView3_DataError);
            this.dataGridView3.SelectionChanged += new System.EventHandler(this.dataGridView3_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "TK_I";
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Tài khoản";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // TEN_TK
            // 
            this.TEN_TK.DataPropertyName = "TEN_TK";
            this.TEN_TK.Frozen = true;
            this.TEN_TK.HeaderText = "Tên tài khoản";
            this.TEN_TK.Name = "TEN_TK";
            this.TEN_TK.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "UID";
            this.dataGridViewTextBoxColumn7.HeaderText = "UID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "STT_REC";
            this.dataGridViewTextBoxColumn8.HeaderText = "Rec";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "STT_REC0";
            this.dataGridViewTextBoxColumn9.HeaderText = "Rec0";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // detail3
            // 
            this.detail3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detail3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.detail3.CarryData = null;
            this.detail3.FixControl = 3;
            this.detail3.Location = new System.Drawing.Point(2, 2);
            this.detail3.MODE = V6Structs.V6Mode.Init;
            this.detail3.Name = "detail3";
            this.detail3.ShowLblName = false;
            this.detail3.Size = new System.Drawing.Size(876, 50);
            this.detail3.Sua_tien = false;
            this.detail3.TabIndex = 4;
            this.detail3.Tag = "cancelall";
            this.detail3.Vtype = null;
            this.detail3.ClickAdd += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.Detail3_ClickAdd);
            this.detail3.ClickEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.Detail3_ClickEdit);
            this.detail3.ClickCancelEdit += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.Detail3_ClickCancelEdit);
            this.detail3.AddHandle += new V6Controls.HandleData(this.Detail3_AddHandle);
            this.detail3.EditHandle += new V6Controls.HandleData(this.Detail3_EditHandle);
            this.detail3.ClickDelete += new V6ControlManager.FormManager.ChungTuManager.HD_Detail.ClickHandle(this.Detail3_ClickDelete);
            this.detail3.LabelNameTextChanged += new System.EventHandler(this.detail3_LabelNameTextChanged);
            // 
            // group5
            // 
            this.group5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.group5.Controls.Add(this.panelVND);
            this.group5.Controls.Add(this.panelNT);
            this.group5.Location = new System.Drawing.Point(486, 399);
            this.group5.Name = "group5";
            this.group5.Size = new System.Drawing.Size(410, 117);
            this.group5.TabIndex = 5;
            this.group5.TabStop = false;
            // 
            // panelVND
            // 
            this.panelVND.Controls.Add(this.txtTongThanhToan);
            this.panelVND.Controls.Add(this.txtTongTangGiam);
            this.panelVND.Location = new System.Drawing.Point(264, 11);
            this.panelVND.Name = "panelVND";
            this.panelVND.Size = new System.Drawing.Size(146, 84);
            this.panelVND.TabIndex = 1;
            // 
            // txtTongThanhToan
            // 
            this.txtTongThanhToan.AccessibleDescription = "";
            this.txtTongThanhToan.AccessibleName = "t_tt";
            this.txtTongThanhToan.BackColor = System.Drawing.SystemColors.Window;
            this.txtTongThanhToan.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTongThanhToan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongThanhToan.DecimalPlaces = 0;
            this.txtTongThanhToan.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTongThanhToan.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTongThanhToan.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTongThanhToan.HoverColor = System.Drawing.Color.Yellow;
            this.txtTongThanhToan.LeaveColor = System.Drawing.Color.White;
            this.txtTongThanhToan.Location = new System.Drawing.Point(3, 34);
            this.txtTongThanhToan.Name = "txtTongThanhToan";
            this.txtTongThanhToan.Size = new System.Drawing.Size(136, 20);
            this.txtTongThanhToan.TabIndex = 1;
            this.txtTongThanhToan.Tag = "readonly";
            this.txtTongThanhToan.Text = "0";
            this.txtTongThanhToan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongThanhToan.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTongTangGiam
            // 
            this.txtTongTangGiam.AccessibleDescription = "";
            this.txtTongTangGiam.AccessibleName = "t_add";
            this.txtTongTangGiam.BackColor = System.Drawing.SystemColors.Window;
            this.txtTongTangGiam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTongTangGiam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTangGiam.DecimalPlaces = 0;
            this.txtTongTangGiam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTongTangGiam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTongTangGiam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTongTangGiam.HoverColor = System.Drawing.Color.Yellow;
            this.txtTongTangGiam.LeaveColor = System.Drawing.Color.White;
            this.txtTongTangGiam.Location = new System.Drawing.Point(3, 9);
            this.txtTongTangGiam.Name = "txtTongTangGiam";
            this.txtTongTangGiam.Size = new System.Drawing.Size(136, 20);
            this.txtTongTangGiam.TabIndex = 0;
            this.txtTongTangGiam.Tag = "readonly";
            this.txtTongTangGiam.Text = "0";
            this.txtTongTangGiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongTangGiam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // panelNT
            // 
            this.panelNT.Controls.Add(this.lblTongTangGiam);
            this.panelNT.Controls.Add(this.lblTTT);
            this.panelNT.Controls.Add(this.txtTongTangGiamNt);
            this.panelNT.Controls.Add(this.txtTongThanhToanNt);
            this.panelNT.Location = new System.Drawing.Point(9, 11);
            this.panelNT.Name = "panelNT";
            this.panelNT.Size = new System.Drawing.Size(249, 84);
            this.panelNT.TabIndex = 0;
            // 
            // lblTongTangGiam
            // 
            this.lblTongTangGiam.AccessibleDescription = "ACACTTA1L00067";
            this.lblTongTangGiam.AutoSize = true;
            this.lblTongTangGiam.Location = new System.Drawing.Point(6, 11);
            this.lblTongTangGiam.Name = "lblTongTangGiam";
            this.lblTongTangGiam.Size = new System.Drawing.Size(81, 13);
            this.lblTongTangGiam.TabIndex = 0;
            this.lblTongTangGiam.Text = "Tổng tăng giảm";
            // 
            // lblTTT
            // 
            this.lblTTT.AccessibleDescription = "ACACTTA1L00068";
            this.lblTTT.AutoSize = true;
            this.lblTTT.Location = new System.Drawing.Point(6, 37);
            this.lblTTT.Name = "lblTTT";
            this.lblTTT.Size = new System.Drawing.Size(49, 13);
            this.lblTTT.TabIndex = 2;
            this.lblTTT.Text = "Tổng TT";
            // 
            // txtTongTangGiamNt
            // 
            this.txtTongTangGiamNt.AccessibleDescription = "";
            this.txtTongTangGiamNt.AccessibleName = "t_add_nt";
            this.txtTongTangGiamNt.BackColor = System.Drawing.SystemColors.Window;
            this.txtTongTangGiamNt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTongTangGiamNt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTangGiamNt.DecimalPlaces = 0;
            this.txtTongTangGiamNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTongTangGiamNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTongTangGiamNt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTongTangGiamNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtTongTangGiamNt.LeaveColor = System.Drawing.Color.White;
            this.txtTongTangGiamNt.Location = new System.Drawing.Point(108, 8);
            this.txtTongTangGiamNt.Name = "txtTongTangGiamNt";
            this.txtTongTangGiamNt.Size = new System.Drawing.Size(136, 20);
            this.txtTongTangGiamNt.TabIndex = 1;
            this.txtTongTangGiamNt.Tag = "readonly";
            this.txtTongTangGiamNt.Text = "0";
            this.txtTongTangGiamNt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongTangGiamNt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTongThanhToanNt
            // 
            this.txtTongThanhToanNt.AccessibleDescription = "";
            this.txtTongThanhToanNt.AccessibleName = "t_tt_nt";
            this.txtTongThanhToanNt.BackColor = System.Drawing.SystemColors.Window;
            this.txtTongThanhToanNt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTongThanhToanNt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongThanhToanNt.DecimalPlaces = 0;
            this.txtTongThanhToanNt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTongThanhToanNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTongThanhToanNt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTongThanhToanNt.HoverColor = System.Drawing.Color.Yellow;
            this.txtTongThanhToanNt.LeaveColor = System.Drawing.Color.White;
            this.txtTongThanhToanNt.Location = new System.Drawing.Point(108, 34);
            this.txtTongThanhToanNt.Name = "txtTongThanhToanNt";
            this.txtTongThanhToanNt.Size = new System.Drawing.Size(136, 20);
            this.txtTongThanhToanNt.TabIndex = 3;
            this.txtTongThanhToanNt.Tag = "readonly";
            this.txtTongThanhToanNt.Text = "0";
            this.txtTongThanhToanNt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongThanhToanNt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTongThanhToanNt.TextChanged += new System.EventHandler(this.txtTongThanhToanNt_TextChanged);
            // 
            // group4
            // 
            this.group4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group4.Controls.Add(this.chkAutoNext);
            this.group4.Controls.Add(this.txtSoct_tt);
            this.group4.Controls.Add(this.btnChonHD);
            this.group4.Controls.Add(this.lblKieuPostColor);
            this.group4.Controls.Add(this.cboChuyenData);
            this.group4.Controls.Add(this.v6Label28);
            this.group4.Controls.Add(this.btnChucNang);
            this.group4.Controls.Add(this.chkSuaTien);
            this.group4.Controls.Add(this.cboKieuPost);
            this.group4.Controls.Add(this.lblTongSoDong);
            this.group4.Controls.Add(this.v6Label20);
            this.group4.Location = new System.Drawing.Point(5, 399);
            this.group4.Name = "group4";
            this.group4.Size = new System.Drawing.Size(475, 117);
            this.group4.TabIndex = 4;
            this.group4.TabStop = false;
            // 
            // chkAutoNext
            // 
            this.chkAutoNext.AccessibleName = ".";
            this.chkAutoNext.AutoSize = true;
            this.chkAutoNext.Location = new System.Drawing.Point(6, 34);
            this.chkAutoNext.Name = "chkAutoNext";
            this.chkAutoNext.Size = new System.Drawing.Size(82, 17);
            this.chkAutoNext.TabIndex = 48;
            this.chkAutoNext.TabStop = false;
            this.chkAutoNext.Text = "Sửa liên tục";
            this.chkAutoNext.UseVisualStyleBackColor = true;
            // 
            // txtSoct_tt
            // 
            this.txtSoct_tt.AccessibleName = "SO_CT_TT";
            this.txtSoct_tt.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoct_tt.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoct_tt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoct_tt.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoct_tt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoct_tt.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoct_tt.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoct_tt.LeaveColor = System.Drawing.Color.White;
            this.txtSoct_tt.Location = new System.Drawing.Point(256, 55);
            this.txtSoct_tt.Name = "txtSoct_tt";
            this.txtSoct_tt.Size = new System.Drawing.Size(151, 20);
            this.txtSoct_tt.TabIndex = 43;
            this.txtSoct_tt.Tag = "disable";
            // 
            // btnChonHD
            // 
            this.btnChonHD.AccessibleDescription = "ASOCTSOAB00048";
            this.btnChonHD.Location = new System.Drawing.Point(169, 53);
            this.btnChonHD.Name = "btnChonHD";
            this.btnChonHD.Size = new System.Drawing.Size(81, 29);
            this.btnChonHD.TabIndex = 42;
            this.btnChonHD.Text = "Chọn PB HĐ";
            this.btnChonHD.UseVisualStyleBackColor = true;
            this.btnChonHD.Click += new System.EventHandler(this.btnChonHD_Click);
            // 
            // lblKieuPostColor
            // 
            this.lblKieuPostColor.AutoSize = true;
            this.lblKieuPostColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKieuPostColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblKieuPostColor.Location = new System.Drawing.Point(65, 10);
            this.lblKieuPostColor.Name = "lblKieuPostColor";
            this.lblKieuPostColor.Size = new System.Drawing.Size(57, 13);
            this.lblKieuPostColor.TabIndex = 41;
            this.lblKieuPostColor.Text = "KieuPost";
            // 
            // cboChuyenData
            // 
            this.cboChuyenData.AccessibleName = "IMTYPE";
            this.cboChuyenData.BackColor = System.Drawing.SystemColors.Window;
            this.cboChuyenData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChuyenData.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboChuyenData.FormattingEnabled = true;
            this.cboChuyenData.Location = new System.Drawing.Point(295, 88);
            this.cboChuyenData.Name = "cboChuyenData";
            this.cboChuyenData.Size = new System.Drawing.Size(112, 21);
            this.cboChuyenData.TabIndex = 39;
            this.cboChuyenData.TabStop = false;
            this.toolTipV6FormControl.SetToolTip(this.cboChuyenData, "Chuyển dữ liệu qua Database khác.");
            // 
            // v6Label28
            // 
            this.v6Label28.AccessibleDescription = "ASOCTSOAH00055";
            this.v6Label28.AutoSize = true;
            this.v6Label28.Location = new System.Drawing.Point(249, 93);
            this.v6Label28.Name = "v6Label28";
            this.v6Label28.Size = new System.Drawing.Size(36, 13);
            this.v6Label28.TabIndex = 40;
            this.v6Label28.Text = "DATA";
            // 
            // btnChucNang
            // 
            this.btnChucNang.AccessibleDescription = "AINCTINDB00026";
            this.btnChucNang.Location = new System.Drawing.Point(83, 53);
            this.btnChucNang.Menu = this.menuChucNang;
            this.btnChucNang.Name = "btnChucNang";
            this.btnChucNang.Size = new System.Drawing.Size(81, 29);
            this.btnChucNang.TabIndex = 38;
            this.btnChucNang.Tag = "cancel";
            this.btnChucNang.Text = "Chức năng";
            this.btnChucNang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChucNang.UseVisualStyleBackColor = true;
            // 
            // menuChucNang
            // 
            this.menuChucNang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThuCongNoMenu,
            this.ThuCuocContMent,
            this.TroGiupMenu,
            this.chonTuExcelMenu,
            this.xuLyKhacMenu,
            this.thayTheMenu,
            this.thayTheNhieuMenu,
            this.thayThe2Menu,
            this.thuNoTaiKhoanMenu,
            this.exportXmlMenu,
            this.importXmlMenu});
            this.menuChucNang.Name = "menuChucNang";
            this.menuChucNang.Size = new System.Drawing.Size(164, 246);
            this.menuChucNang.Paint += new System.Windows.Forms.PaintEventHandler(this.menuChucNang_Paint);
            // 
            // ThuCongNoMenu
            // 
            this.ThuCongNoMenu.AccessibleDescription = "INVOICEM00015";
            this.ThuCongNoMenu.Name = "ThuCongNoMenu";
            this.ThuCongNoMenu.Size = new System.Drawing.Size(163, 22);
            this.ThuCongNoMenu.Text = "Thu công nợ";
            this.ThuCongNoMenu.Click += new System.EventHandler(this.ThuCongNo_Click);
            // 
            // ThuCuocContMent
            // 
            this.ThuCuocContMent.AccessibleDescription = "INVOICEM00041";
            this.ThuCuocContMent.Name = "ThuCuocContMent";
            this.ThuCuocContMent.Size = new System.Drawing.Size(163, 22);
            this.ThuCuocContMent.Text = "Thu cược cont";
            this.ThuCuocContMent.Click += new System.EventHandler(this.ThuCuocCont_Click);
            // 
            // TroGiupMenu
            // 
            this.TroGiupMenu.AccessibleDescription = "INVOICEM00003";
            this.TroGiupMenu.Name = "TroGiupMenu";
            this.TroGiupMenu.Size = new System.Drawing.Size(163, 22);
            this.TroGiupMenu.Text = "Trợ giúp";
            this.TroGiupMenu.Click += new System.EventHandler(this.TroGiupMenu_Click);
            // 
            // chonTuExcelMenu
            // 
            this.chonTuExcelMenu.AccessibleDescription = "INVOICEM00004";
            this.chonTuExcelMenu.Name = "chonTuExcelMenu";
            this.chonTuExcelMenu.Size = new System.Drawing.Size(163, 22);
            this.chonTuExcelMenu.Text = "Chọn từ excel";
            this.chonTuExcelMenu.Visible = false;
            this.chonTuExcelMenu.Click += new System.EventHandler(this.chonTuExcelToolStripMenuItem_Click);
            // 
            // xuLyKhacMenu
            // 
            this.xuLyKhacMenu.AccessibleDescription = "INVOICEM00010";
            this.xuLyKhacMenu.Name = "xuLyKhacMenu";
            this.xuLyKhacMenu.Size = new System.Drawing.Size(163, 22);
            this.xuLyKhacMenu.Text = "Xử lý khác";
            this.xuLyKhacMenu.Click += new System.EventHandler(this.xuLyKhacMenu_Click);
            // 
            // thayTheMenu
            // 
            this.thayTheMenu.AccessibleDescription = "INVOICEM00008";
            this.thayTheMenu.Name = "thayTheMenu";
            this.thayTheMenu.Size = new System.Drawing.Size(163, 22);
            this.thayTheMenu.Text = "Thay thế";
            this.thayTheMenu.Click += new System.EventHandler(this.thayTheMenu_Click);
            // 
            // thayTheNhieuMenu
            // 
            this.thayTheNhieuMenu.AccessibleDescription = "INVOICEM00027";
            this.thayTheNhieuMenu.Name = "thayTheNhieuMenu";
            this.thayTheNhieuMenu.Size = new System.Drawing.Size(163, 22);
            this.thayTheNhieuMenu.Text = "Thay thế nhiều";
            this.thayTheNhieuMenu.Click += new System.EventHandler(this.thayTheNhieuMenu_Click);
            // 
            // thayThe2Menu
            // 
            this.thayThe2Menu.AccessibleDescription = "INVOICEM00009";
            this.thayThe2Menu.Name = "thayThe2Menu";
            this.thayThe2Menu.Size = new System.Drawing.Size(163, 22);
            this.thayThe2Menu.Text = "Sửa nhiều dòng";
            this.thayThe2Menu.Click += new System.EventHandler(this.thayThe2toolStripMenuItem_Click);
            // 
            // thuNoTaiKhoanMenu
            // 
            this.thuNoTaiKhoanMenu.AccessibleDescription = "INVOICEM00017";
            this.thuNoTaiKhoanMenu.Name = "thuNoTaiKhoanMenu";
            this.thuNoTaiKhoanMenu.Size = new System.Drawing.Size(163, 22);
            this.thuNoTaiKhoanMenu.Text = "Thu nợ tài khoản";
            this.thuNoTaiKhoanMenu.Click += new System.EventHandler(this.thuNoTaiKhoanToolStripMenuItem_Click);
            // 
            // exportXmlMenu
            // 
            this.exportXmlMenu.AccessibleDescription = "INVOICEM00043";
            this.exportXmlMenu.Name = "exportXmlMenu";
            this.exportXmlMenu.Size = new System.Drawing.Size(163, 22);
            this.exportXmlMenu.Tag = "cancel";
            this.exportXmlMenu.Text = "Export Xml";
            this.exportXmlMenu.Click += new System.EventHandler(this.exportXmlMenu_Click);
            // 
            // importXmlMenu
            // 
            this.importXmlMenu.AccessibleDescription = "INVOICEM00044";
            this.importXmlMenu.Name = "importXmlMenu";
            this.importXmlMenu.Size = new System.Drawing.Size(163, 22);
            this.importXmlMenu.Text = "Import Xml";
            this.importXmlMenu.Click += new System.EventHandler(this.importXmlMenu_Click);
            // 
            // chkSuaTien
            // 
            this.chkSuaTien.AccessibleDescription = "ACACTTA1H00002";
            this.chkSuaTien.AccessibleName = "sua_tien";
            this.chkSuaTien.AutoSize = true;
            this.chkSuaTien.Location = new System.Drawing.Point(6, 57);
            this.chkSuaTien.Name = "chkSuaTien";
            this.chkSuaTien.Size = new System.Drawing.Size(65, 17);
            this.chkSuaTien.TabIndex = 21;
            this.chkSuaTien.Tag = "";
            this.chkSuaTien.Text = "Sửa tiền";
            this.chkSuaTien.UseVisualStyleBackColor = true;
            this.chkSuaTien.CheckedChanged += new System.EventHandler(this.chkSuaTien_CheckedChanged);
            // 
            // cboKieuPost
            // 
            this.cboKieuPost.AccessibleName = "kieu_post";
            this.cboKieuPost.BackColor = System.Drawing.SystemColors.Window;
            this.cboKieuPost.ColorField = "ColorV";
            this.cboKieuPost.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKieuPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieuPost.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboKieuPost.FormattingEnabled = true;
            this.cboKieuPost.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboKieuPost.Location = new System.Drawing.Point(83, 88);
            this.cboKieuPost.Name = "cboKieuPost";
            this.cboKieuPost.Size = new System.Drawing.Size(160, 21);
            this.cboKieuPost.TabIndex = 10;
            this.cboKieuPost.Tag = "";
            this.cboKieuPost.SelectedIndexChanged += new System.EventHandler(this.cboKieuPost_SelectedIndexChanged);
            // 
            // lblTongSoDong
            // 
            this.lblTongSoDong.AutoSize = true;
            this.lblTongSoDong.Location = new System.Drawing.Point(6, 16);
            this.lblTongSoDong.Name = "lblTongSoDong";
            this.lblTongSoDong.Size = new System.Drawing.Size(40, 13);
            this.lblTongSoDong.TabIndex = 0;
            this.lblTongSoDong.Text = "0 dòng";
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "ACACTTA1L00069";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(6, 91);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(55, 13);
            this.v6Label20.TabIndex = 9;
            this.v6Label20.Text = "Trạng thái";
            // 
            // group3
            // 
            this.group3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group3.Controls.Add(this.txtTenGiaoDich);
            this.group3.Controls.Add(this.v6Label14);
            this.group3.Controls.Add(this.btnChonNhieuHD);
            this.group3.Controls.Add(this.txtMaDVCS);
            this.group3.Controls.Add(this.txtTenDVCS);
            this.group3.Controls.Add(this.lblMaDVCS);
            this.group3.Controls.Add(this.txtTyGia);
            this.group3.Controls.Add(this.cboMaNt);
            this.group3.Controls.Add(this.txtLoaiPhieu);
            this.group3.Controls.Add(this.txtMaKh);
            this.group3.Controls.Add(this.v6Label17);
            this.group3.Controls.Add(this.Txtdien_giai);
            this.group3.Controls.Add(this.v6Label12);
            this.group3.Controls.Add(this.txtMaSoThue);
            this.group3.Controls.Add(this.txtTenKh);
            this.group3.Controls.Add(this.txtDiaChi);
            this.group3.Controls.Add(this.v6Label11);
            this.group3.Controls.Add(this.lblMaKH);
            this.group3.Location = new System.Drawing.Point(5, 54);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(888, 122);
            this.group3.TabIndex = 2;
            this.group3.TabStop = false;
            // 
            // txtTenGiaoDich
            // 
            this.txtTenGiaoDich.AccessibleName = "ten_gd";
            this.txtTenGiaoDich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenGiaoDich.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenGiaoDich.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenGiaoDich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenGiaoDich.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenGiaoDich.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenGiaoDich.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenGiaoDich.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenGiaoDich.LeaveColor = System.Drawing.Color.White;
            this.txtTenGiaoDich.Location = new System.Drawing.Point(198, 31);
            this.txtTenGiaoDich.Name = "txtTenGiaoDich";
            this.txtTenGiaoDich.Size = new System.Drawing.Size(405, 20);
            this.txtTenGiaoDich.TabIndex = 5;
            this.txtTenGiaoDich.Tag = "disable";
            // 
            // v6Label14
            // 
            this.v6Label14.AccessibleDescription = "ACACTTA1L00055";
            this.v6Label14.AutoSize = true;
            this.v6Label14.Location = new System.Drawing.Point(3, 34);
            this.v6Label14.Name = "v6Label14";
            this.v6Label14.Size = new System.Drawing.Size(56, 13);
            this.v6Label14.TabIndex = 3;
            this.v6Label14.Text = "Loại phiếu";
            // 
            // btnChonNhieuHD
            // 
            this.btnChonNhieuHD.AccessibleDescription = "ACACTTA1R00052";
            this.btnChonNhieuHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChonNhieuHD.Image = global::V6ControlManager.Properties.Resources.Search24;
            this.btnChonNhieuHD.Location = new System.Drawing.Point(700, 83);
            this.btnChonNhieuHD.Name = "btnChonNhieuHD";
            this.btnChonNhieuHD.Size = new System.Drawing.Size(171, 33);
            this.btnChonNhieuHD.TabIndex = 17;
            this.btnChonNhieuHD.TabStop = false;
            this.btnChonNhieuHD.Text = "Chọn &nhiều hóa đơn";
            this.btnChonNhieuHD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipV6FormControl.SetToolTip(this.btnChonNhieuHD, "Shift để thêm");
            this.btnChonNhieuHD.UseVisualStyleBackColor = true;
            this.btnChonNhieuHD.Click += new System.EventHandler(this.btnChonNhieuHD_Click);
            this.btnChonNhieuHD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnChonNhieuHD_KeyDown);
            // 
            // txtMaDVCS
            // 
            this.txtMaDVCS.AccessibleName = "MA_DVCS";
            this.txtMaDVCS.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaDVCS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaDVCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaDVCS.BrotherFields = "ten_dvcs";
            this.txtMaDVCS.BrotherFields2 = "ten2";
            this.txtMaDVCS.CheckNotEmpty = true;
            this.txtMaDVCS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaDVCS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaDVCS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaDVCS.LeaveColor = System.Drawing.Color.White;
            this.txtMaDVCS.Location = new System.Drawing.Point(92, 9);
            this.txtMaDVCS.Name = "txtMaDVCS";
            this.txtMaDVCS.ShowName = true;
            this.txtMaDVCS.Size = new System.Drawing.Size(100, 20);
            this.txtMaDVCS.TabIndex = 1;
            this.txtMaDVCS.VVar = "ma_dvcs";
            this.txtMaDVCS.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMadvcs_V6LostFocus);
            // 
            // txtTenDVCS
            // 
            this.txtTenDVCS.AccessibleName = "ten_dvcs";
            this.txtTenDVCS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenDVCS.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenDVCS.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenDVCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenDVCS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenDVCS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenDVCS.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenDVCS.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenDVCS.LeaveColor = System.Drawing.Color.White;
            this.txtTenDVCS.Location = new System.Drawing.Point(198, 9);
            this.txtTenDVCS.Name = "txtTenDVCS";
            this.txtTenDVCS.Size = new System.Drawing.Size(405, 20);
            this.txtTenDVCS.TabIndex = 2;
            this.txtTenDVCS.Tag = "disable";
            // 
            // lblMaDVCS
            // 
            this.lblMaDVCS.AccessibleDescription = "ACACTTA1L00059";
            this.lblMaDVCS.AutoSize = true;
            this.lblMaDVCS.Location = new System.Drawing.Point(3, 12);
            this.lblMaDVCS.Name = "lblMaDVCS";
            this.lblMaDVCS.Size = new System.Drawing.Size(58, 13);
            this.lblMaDVCS.TabIndex = 0;
            this.lblMaDVCS.Text = "Mã đơn vị ";
            // 
            // txtTyGia
            // 
            this.txtTyGia.AccessibleDescription = "";
            this.txtTyGia.AccessibleName = "ty_gia";
            this.txtTyGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTyGia.BackColor = System.Drawing.Color.White;
            this.txtTyGia.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTyGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTyGia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTyGia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTyGia.HoverColor = System.Drawing.Color.Yellow;
            this.txtTyGia.LeaveColor = System.Drawing.Color.White;
            this.txtTyGia.Location = new System.Drawing.Point(771, 37);
            this.txtTyGia.Name = "txtTyGia";
            this.txtTyGia.Size = new System.Drawing.Size(100, 20);
            this.txtTyGia.TabIndex = 16;
            this.txtTyGia.Text = "0,000";
            this.txtTyGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTyGia.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTyGia.V6LostFocus += new V6Controls.ControlEventHandle(this.txtTyGia_V6LostFocus);
            // 
            // cboMaNt
            // 
            this.cboMaNt.AccessibleName = "ma_nt";
            this.cboMaNt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMaNt.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaNt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboMaNt.FormattingEnabled = true;
            this.cboMaNt.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboMaNt.Location = new System.Drawing.Point(653, 36);
            this.cboMaNt.Name = "cboMaNt";
            this.cboMaNt.Size = new System.Drawing.Size(98, 21);
            this.cboMaNt.TabIndex = 15;
            this.cboMaNt.TabStop = false;
            this.cboMaNt.SelectedValueChanged += new System.EventHandler(this.cboMaNt_SelectedValueChanged);
            // 
            // txtLoaiPhieu
            // 
            this.txtLoaiPhieu.AccessibleName = "ma_gd";
            this.txtLoaiPhieu.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoaiPhieu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtLoaiPhieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaiPhieu.BrotherFields = "ten_gd";
            this.txtLoaiPhieu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoaiPhieu.CheckNotEmpty = true;
            this.txtLoaiPhieu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtLoaiPhieu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLoaiPhieu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtLoaiPhieu.HoverColor = System.Drawing.Color.Yellow;
            this.txtLoaiPhieu.LeaveColor = System.Drawing.Color.White;
            this.txtLoaiPhieu.LimitCharacters = "123456789Aa";
            this.txtLoaiPhieu.Location = new System.Drawing.Point(92, 31);
            this.txtLoaiPhieu.MaxLength = 1;
            this.txtLoaiPhieu.Name = "txtLoaiPhieu";
            this.txtLoaiPhieu.Size = new System.Drawing.Size(28, 20);
            this.txtLoaiPhieu.TabIndex = 4;
            this.txtLoaiPhieu.UseChangeTextOnSetFormData = true;
            this.txtLoaiPhieu.VVar = "ma_gd";
            this.txtLoaiPhieu.V6LostFocus += new V6Controls.ControlEventHandle(this.txtLoaiPhieu_V6LostFocus);
            this.txtLoaiPhieu.V6LostFocusNoChange += new V6Controls.ControlEventHandle(this.txtLoaiPhieu_V6LostFocusNoChange);
            this.txtLoaiPhieu.TextChanged += new System.EventHandler(this.txtLoaiPhieu_TextChanged);
            // 
            // txtMaKh
            // 
            this.txtMaKh.AccessibleName = "ma_kh";
            this.txtMaKh.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaKh.CheckNotEmpty = true;
            this.txtMaKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKh.LeaveColor = System.Drawing.Color.White;
            this.txtMaKh.Location = new System.Drawing.Point(92, 53);
            this.txtMaKh.Name = "txtMaKh";
            this.txtMaKh.Size = new System.Drawing.Size(100, 20);
            this.txtMaKh.TabIndex = 7;
            this.txtMaKh.VVar = "ma_kh";
            this.txtMaKh.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMaKh_V6LostFocus);
            this.txtMaKh.Leave += new System.EventHandler(this.txtMaKh_Leave);
            // 
            // v6Label17
            // 
            this.v6Label17.AccessibleDescription = "ACACTTA1L00060";
            this.v6Label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.v6Label17.AutoSize = true;
            this.v6Label17.Location = new System.Drawing.Point(611, 39);
            this.v6Label17.Name = "v6Label17";
            this.v6Label17.Size = new System.Drawing.Size(36, 13);
            this.v6Label17.TabIndex = 14;
            this.v6Label17.Text = "Tỷ giá";
            // 
            // Txtdien_giai
            // 
            this.Txtdien_giai.AccessibleName = "dien_giai";
            this.Txtdien_giai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtdien_giai.BackColor = System.Drawing.SystemColors.Window;
            this.Txtdien_giai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtdien_giai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txtdien_giai.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtdien_giai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtdien_giai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtdien_giai.HoverColor = System.Drawing.Color.Yellow;
            this.Txtdien_giai.LeaveColor = System.Drawing.Color.White;
            this.Txtdien_giai.Location = new System.Drawing.Point(92, 99);
            this.Txtdien_giai.Name = "Txtdien_giai";
            this.Txtdien_giai.Size = new System.Drawing.Size(512, 20);
            this.Txtdien_giai.TabIndex = 13;
            // 
            // v6Label12
            // 
            this.v6Label12.AccessibleDescription = "ACACTTA1L00058";
            this.v6Label12.AutoSize = true;
            this.v6Label12.Location = new System.Drawing.Point(3, 102);
            this.v6Label12.Name = "v6Label12";
            this.v6Label12.Size = new System.Drawing.Size(48, 13);
            this.v6Label12.TabIndex = 12;
            this.v6Label12.Text = "Diễn giải";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.AccessibleName = "ma_so_thue";
            this.txtMaSoThue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSoThue.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaSoThue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaSoThue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaSoThue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSoThue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaSoThue.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSoThue.LeaveColor = System.Drawing.Color.White;
            this.txtMaSoThue.Location = new System.Drawing.Point(515, 76);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(88, 20);
            this.txtMaSoThue.TabIndex = 11;
            this.txtMaSoThue.Tag = "disable";
            // 
            // txtTenKh
            // 
            this.txtTenKh.AccessibleName = "ten_kh";
            this.txtTenKh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenKh.BackColor = System.Drawing.SystemColors.Window;
            this.txtTenKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTenKh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTenKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTenKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtTenKh.LeaveColor = System.Drawing.Color.White;
            this.txtTenKh.Location = new System.Drawing.Point(198, 53);
            this.txtTenKh.Name = "txtTenKh";
            this.txtTenKh.Size = new System.Drawing.Size(405, 20);
            this.txtTenKh.TabIndex = 8;
            this.txtTenKh.Tag = "disable";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.AccessibleName = "dia_chi";
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiaChi.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiaChi.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtDiaChi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtDiaChi.HoverColor = System.Drawing.Color.Yellow;
            this.txtDiaChi.LeaveColor = System.Drawing.Color.White;
            this.txtDiaChi.Location = new System.Drawing.Point(92, 76);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(417, 20);
            this.txtDiaChi.TabIndex = 10;
            // 
            // v6Label11
            // 
            this.v6Label11.AccessibleDescription = "ACACTTA1L00057";
            this.v6Label11.AutoSize = true;
            this.v6Label11.Location = new System.Drawing.Point(3, 79);
            this.v6Label11.Name = "v6Label11";
            this.v6Label11.Size = new System.Drawing.Size(40, 13);
            this.v6Label11.TabIndex = 9;
            this.v6Label11.Text = "Địa chỉ";
            // 
            // lblMaKH
            // 
            this.lblMaKH.AccessibleDescription = "ACACTTA1L00056";
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(3, 56);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(82, 13);
            this.lblMaKH.TabIndex = 6;
            this.lblMaKH.Text = "Mã khách hàng";
            // 
            // group2
            // 
            this.group2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group2.Controls.Add(this.TxtTen_tk);
            this.group2.Controls.Add(this.Txtma_nvien);
            this.group2.Controls.Add(this.TxtMa_bp);
            this.group2.Controls.Add(this.v6Label7);
            this.group2.Controls.Add(this.v6Label8);
            this.group2.Controls.Add(this.lblTK);
            this.group2.Controls.Add(this.v6ColorTextBox1);
            this.group2.Controls.Add(this.txtTk);
            this.group2.Location = new System.Drawing.Point(401, -5);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(491, 57);
            this.group2.TabIndex = 1;
            this.group2.TabStop = false;
            // 
            // TxtTen_tk
            // 
            this.TxtTen_tk.AccessibleName = "ten_tk";
            this.TxtTen_tk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTen_tk.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTen_tk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtTen_tk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTen_tk.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtTen_tk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTen_tk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtTen_tk.HoverColor = System.Drawing.Color.Yellow;
            this.TxtTen_tk.LeaveColor = System.Drawing.Color.White;
            this.TxtTen_tk.Location = new System.Drawing.Point(183, 10);
            this.TxtTen_tk.Name = "TxtTen_tk";
            this.TxtTen_tk.Size = new System.Drawing.Size(120, 20);
            this.TxtTen_tk.TabIndex = 2;
            this.TxtTen_tk.Tag = "disable";
            // 
            // Txtma_nvien
            // 
            this.Txtma_nvien.AccessibleName = "MA_NVIEN";
            this.Txtma_nvien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtma_nvien.BackColor = System.Drawing.Color.White;
            this.Txtma_nvien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtma_nvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txtma_nvien.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtma_nvien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtma_nvien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtma_nvien.HoverColor = System.Drawing.Color.Yellow;
            this.Txtma_nvien.LeaveColor = System.Drawing.Color.White;
            this.Txtma_nvien.Location = new System.Drawing.Point(411, 10);
            this.Txtma_nvien.Name = "Txtma_nvien";
            this.Txtma_nvien.Size = new System.Drawing.Size(76, 20);
            this.Txtma_nvien.TabIndex = 7;
            this.Txtma_nvien.VVar = "Ma_nvien";
            // 
            // TxtMa_bp
            // 
            this.TxtMa_bp.AccessibleName = "MA_BP";
            this.TxtMa_bp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMa_bp.BackColor = System.Drawing.Color.White;
            this.TxtMa_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_bp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMa_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_bp.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_bp.Location = new System.Drawing.Point(348, 10);
            this.TxtMa_bp.Name = "TxtMa_bp";
            this.TxtMa_bp.Size = new System.Drawing.Size(61, 20);
            this.TxtMa_bp.TabIndex = 6;
            this.TxtMa_bp.VVar = "Ma_bp";
            // 
            // v6Label7
            // 
            this.v6Label7.AccessibleDescription = "ACACTTA1L00054";
            this.v6Label7.AutoSize = true;
            this.v6Label7.Location = new System.Drawing.Point(6, 35);
            this.v6Label7.Name = "v6Label7";
            this.v6Label7.Size = new System.Drawing.Size(56, 13);
            this.v6Label7.TabIndex = 3;
            this.v6Label7.Text = "Người nộp";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "ACACTTA1L00036";
            this.v6Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(306, 15);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(41, 13);
            this.v6Label8.TabIndex = 5;
            this.v6Label8.Text = "BP/NV";
            // 
            // lblTK
            // 
            this.lblTK.AccessibleDescription = "ACACTTA1L00053";
            this.lblTK.AutoSize = true;
            this.lblTK.Location = new System.Drawing.Point(6, 13);
            this.lblTK.Name = "lblTK";
            this.lblTK.Size = new System.Drawing.Size(70, 13);
            this.lblTK.TabIndex = 0;
            this.lblTK.Text = "Tài khoản nợ";
            // 
            // v6ColorTextBox1
            // 
            this.v6ColorTextBox1.AccessibleName = "ong_ba";
            this.v6ColorTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.v6ColorTextBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.v6ColorTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.v6ColorTextBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.v6ColorTextBox1.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox1.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox1.Location = new System.Drawing.Point(76, 32);
            this.v6ColorTextBox1.Name = "v6ColorTextBox1";
            this.v6ColorTextBox1.Size = new System.Drawing.Size(319, 20);
            this.v6ColorTextBox1.TabIndex = 4;
            // 
            // txtTk
            // 
            this.txtTk.AccessibleName = "tk";
            this.txtTk.BackColor = System.Drawing.Color.White;
            this.txtTk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTk.BrotherFields = "ten_tk";
            this.txtTk.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTk.CheckNotEmpty = true;
            this.txtTk.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTk.HoverColor = System.Drawing.Color.Yellow;
            this.txtTk.LeaveColor = System.Drawing.Color.White;
            this.txtTk.Location = new System.Drawing.Point(76, 10);
            this.txtTk.Name = "txtTk";
            this.txtTk.Size = new System.Drawing.Size(105, 20);
            this.txtTk.TabIndex = 1;
            this.txtTk.VVar = "tk";
            this.txtTk.V6LostFocus += new V6Controls.ControlEventHandle(this.txtTk_V6LostFocus);
            // 
            // group1
            // 
            this.group1.Controls.Add(this.txtMa_sonb);
            this.group1.Controls.Add(this.v6Label2);
            this.group1.Controls.Add(this.dateNgayLCT);
            this.group1.Controls.Add(this.dateNgayCT);
            this.group1.Controls.Add(this.v6Label5);
            this.group1.Controls.Add(this.v6Label3);
            this.group1.Controls.Add(this.v6Label1);
            this.group1.Controls.Add(this.txtSoPhieu);
            this.group1.Location = new System.Drawing.Point(5, -5);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(390, 57);
            this.group1.TabIndex = 0;
            this.group1.TabStop = false;
            // 
            // txtMa_sonb
            // 
            this.txtMa_sonb.AccessibleName = "ma_sonb";
            this.txtMa_sonb.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa_sonb.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_sonb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMa_sonb.BrotherFields = "";
            this.txtMa_sonb.CheckNotEmpty = true;
            this.txtMa_sonb.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_sonb.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_sonb.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_sonb.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_sonb.LeaveColor = System.Drawing.Color.White;
            this.txtMa_sonb.Location = new System.Drawing.Point(92, 10);
            this.txtMa_sonb.Name = "txtMa_sonb";
            this.txtMa_sonb.Size = new System.Drawing.Size(100, 20);
            this.txtMa_sonb.TabIndex = 1;
            this.txtMa_sonb.UseChangeTextOnSetFormData = true;
            this.txtMa_sonb.VVar = "ma_sonb";
            this.txtMa_sonb.V6LostFocus += new V6Controls.ControlEventHandle(this.txtMa_sonb_V6LostFocus);
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "ACACTTA1L00064";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(7, 12);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(52, 13);
            this.v6Label2.TabIndex = 0;
            this.v6Label2.Text = "Số nội bộ";
            // 
            // dateNgayLCT
            // 
            this.dateNgayLCT.AccessibleName = "ngay_lct";
            this.dateNgayLCT.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgayLCT.CustomFormat = "dd/MM/yyyy";
            this.dateNgayLCT.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayLCT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayLCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayLCT.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayLCT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayLCT.LeaveColor = System.Drawing.Color.White;
            this.dateNgayLCT.Location = new System.Drawing.Point(287, 32);
            this.dateNgayLCT.Name = "dateNgayLCT";
            this.dateNgayLCT.Size = new System.Drawing.Size(96, 20);
            this.dateNgayLCT.TabIndex = 7;
            // 
            // dateNgayCT
            // 
            this.dateNgayCT.AccessibleName = "ngay_ct";
            this.dateNgayCT.BackColor = System.Drawing.SystemColors.Window;
            this.dateNgayCT.CustomFormat = "dd/MM/yyyy";
            this.dateNgayCT.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayCT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNgayCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayCT.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayCT.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayCT.LeaveColor = System.Drawing.Color.White;
            this.dateNgayCT.Location = new System.Drawing.Point(287, 10);
            this.dateNgayCT.Name = "dateNgayCT";
            this.dateNgayCT.Size = new System.Drawing.Size(96, 20);
            this.dateNgayCT.TabIndex = 5;
            this.dateNgayCT.ValueChanged += new System.EventHandler(this.dateNgayCT_ValueChanged);
            // 
            // v6Label5
            // 
            this.v6Label5.AccessibleDescription = "ACACTTA1L00062";
            this.v6Label5.AutoSize = true;
            this.v6Label5.Location = new System.Drawing.Point(198, 35);
            this.v6Label5.Name = "v6Label5";
            this.v6Label5.Size = new System.Drawing.Size(78, 13);
            this.v6Label5.TabIndex = 6;
            this.v6Label5.Text = "Ngày lập phiếu";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "ACACTTA1L00061";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(198, 13);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(83, 13);
            this.v6Label3.TabIndex = 4;
            this.v6Label3.Text = "Ngày hạch toán";
            this.v6Label3.Click += new System.EventHandler(this.v6Label3_Click);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "ACACTTA1L00063";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(7, 35);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(49, 13);
            this.v6Label1.TabIndex = 2;
            this.v6Label1.Text = "Số phiếu";
            this.v6Label1.Click += new System.EventHandler(this.v6Label1_Click);
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.AccessibleName = "so_ct";
            this.txtSoPhieu.BackColor = System.Drawing.SystemColors.Window;
            this.txtSoPhieu.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtSoPhieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoPhieu.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtSoPhieu.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSoPhieu.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtSoPhieu.HoverColor = System.Drawing.Color.Yellow;
            this.txtSoPhieu.LeaveColor = System.Drawing.Color.White;
            this.txtSoPhieu.Location = new System.Drawing.Point(92, 32);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(100, 20);
            this.txtSoPhieu.TabIndex = 3;
            this.txtSoPhieu.TextChanged += new System.EventHandler(this.txtSoPhieu_TextChanged);
            // 
            // Txtma_nk
            // 
            this.Txtma_nk.AccessibleName = "MA_NK";
            this.Txtma_nk.BackColor = System.Drawing.SystemColors.Window;
            this.Txtma_nk.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.Txtma_nk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txtma_nk.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtma_nk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Txtma_nk.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.Txtma_nk.HoverColor = System.Drawing.Color.Yellow;
            this.Txtma_nk.LeaveColor = System.Drawing.Color.White;
            this.Txtma_nk.Location = new System.Drawing.Point(711, 517);
            this.Txtma_nk.Name = "Txtma_nk";
            this.Txtma_nk.Size = new System.Drawing.Size(45, 20);
            this.Txtma_nk.TabIndex = 8;
            this.Txtma_nk.Visible = false;
            // 
            // txtMa_ct
            // 
            this.txtMa_ct.AccessibleName = "Ma_ct";
            this.txtMa_ct.BackColor = System.Drawing.SystemColors.Window;
            this.txtMa_ct.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_ct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMa_ct.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_ct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_ct.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_ct.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_ct.LeaveColor = System.Drawing.Color.White;
            this.txtMa_ct.Location = new System.Drawing.Point(757, 517);
            this.txtMa_ct.Name = "txtMa_ct";
            this.txtMa_ct.Size = new System.Drawing.Size(54, 20);
            this.txtMa_ct.TabIndex = 3;
            this.txtMa_ct.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MA_VT";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã vật tư";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TEN_VT";
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên vật tư";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UID";
            this.dataGridViewTextBoxColumn3.HeaderText = "UID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "STT_REC";
            this.dataGridViewTextBoxColumn4.HeaderText = "Rec";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "STT_REC0";
            this.dataGridViewTextBoxColumn5.HeaderText = "Rec0";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // lblDocSoTien
            // 
            this.lblDocSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocSoTien.Location = new System.Drawing.Point(6, 520);
            this.lblDocSoTien.Name = "lblDocSoTien";
            this.lblDocSoTien.Size = new System.Drawing.Size(883, 16);
            this.lblDocSoTien.TabIndex = 13;
            this.lblDocSoTien.Text = "0";
            this.lblDocSoTien.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "ACACTTA1R00046";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.Enabled = false;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel24;
            this.btnHuy.Location = new System.Drawing.Point(295, 539);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(74, 32);
            this.btnHuy.TabIndex = 19;
            this.btnHuy.Text = "&Hủy bỏ";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.Image = global::V6ControlManager.Properties.Resources.First;
            this.btnFirst.Location = new System.Drawing.Point(746, 552);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 23);
            this.btnFirst.TabIndex = 15;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Image = global::V6ControlManager.Properties.Resources.Back1;
            this.btnPrevious.Location = new System.Drawing.Point(775, 552);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(30, 23);
            this.btnPrevious.TabIndex = 16;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Image = global::V6ControlManager.Properties.Resources.Forward;
            this.btnNext.Location = new System.Drawing.Point(804, 552);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 23);
            this.btnNext.TabIndex = 17;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.Image = global::V6ControlManager.Properties.Resources.Last;
            this.btnLast.Location = new System.Drawing.Point(833, 552);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 23);
            this.btnLast.TabIndex = 18;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnQuayRa
            // 
            this.btnQuayRa.AccessibleDescription = "ACACTTA1R00045";
            this.btnQuayRa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuayRa.Image = global::V6ControlManager.Properties.Resources.BackArrow24;
            this.btnQuayRa.Location = new System.Drawing.Point(601, 539);
            this.btnQuayRa.Name = "btnQuayRa";
            this.btnQuayRa.Size = new System.Drawing.Size(76, 32);
            this.btnQuayRa.TabIndex = 14;
            this.btnQuayRa.Text = "&Quay ra";
            this.btnQuayRa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuayRa.UseVisualStyleBackColor = true;
            this.btnQuayRa.Click += new System.EventHandler(this.btnQuayRa_Click);
            // 
            // btnXem
            // 
            this.btnXem.AccessibleDescription = "ACACTTA1R00043";
            this.btnXem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXem.Enabled = false;
            this.btnXem.Image = global::V6ControlManager.Properties.Resources.Document24;
            this.btnXem.Location = new System.Drawing.Point(440, 539);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(70, 32);
            this.btnXem.TabIndex = 12;
            this.btnXem.Text = "Xem";
            this.btnXem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.AccessibleDescription = "ACACTTA1R00042";
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoa.Enabled = false;
            this.btnXoa.Image = global::V6ControlManager.Properties.Resources.DeleteXred24;
            this.btnXoa.Location = new System.Drawing.Point(369, 539);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(70, 32);
            this.btnXoa.TabIndex = 11;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.AccessibleDescription = "ACACTTA1R00041";
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSua.Enabled = false;
            this.btnSua.Image = global::V6ControlManager.Properties.Resources.Edit24;
            this.btnSua.Location = new System.Drawing.Point(295, 539);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(70, 32);
            this.btnSua.TabIndex = 10;
            this.btnSua.Text = "&Sửa";
            this.btnSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "ACACTTA1R00040";
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIn.AutoSize = true;
            this.btnIn.ContextMenuStrip = this.menuBtnIn;
            this.btnIn.Enabled = false;
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print24;
            this.btnIn.Location = new System.Drawing.Point(215, 539);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(80, 32);
            this.btnIn.SplitMenuStrip = this.menuBtnIn;
            this.btnIn.TabIndex = 9;
            this.btnIn.Text = "&In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // menuBtnIn
            // 
            this.menuBtnIn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inPhieuHachToanMenu,
            this.inKhacMenu});
            this.menuBtnIn.Name = "menuBtnIn";
            this.menuBtnIn.Size = new System.Drawing.Size(174, 48);
            // 
            // inPhieuHachToanMenu
            // 
            this.inPhieuHachToanMenu.AccessibleDescription = "INVOICEM00029";
            this.inPhieuHachToanMenu.Name = "inPhieuHachToanMenu";
            this.inPhieuHachToanMenu.Size = new System.Drawing.Size(173, 22);
            this.inPhieuHachToanMenu.Text = "In phiếu hạch toán";
            this.inPhieuHachToanMenu.Click += new System.EventHandler(this.inPhieuHachToanMenu_Click);
            // 
            // inKhacMenu
            // 
            this.inKhacMenu.AccessibleDescription = "INVOICEM00007";
            this.inKhacMenu.Name = "inKhacMenu";
            this.inKhacMenu.Size = new System.Drawing.Size(173, 22);
            this.inKhacMenu.Text = "In khác";
            this.inKhacMenu.Click += new System.EventHandler(this.inKhacMenu_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleDescription = "ACACTTA1R00051";
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.Enabled = false;
            this.btnCopy.Image = global::V6ControlManager.Properties.Resources.Copy24;
            this.btnCopy.Location = new System.Drawing.Point(144, 539);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(70, 32);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.AccessibleDescription = "ACACTTA1R00039";
            this.btnMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoi.Enabled = false;
            this.btnMoi.Image = global::V6ControlManager.Properties.Resources.Add24;
            this.btnMoi.Location = new System.Drawing.Point(73, 539);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(70, 32);
            this.btnMoi.TabIndex = 7;
            this.btnMoi.Text = "&Mới";
            this.btnMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoi.UseVisualStyleBackColor = true;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.AccessibleDescription = "ACACTTA1R00038";
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLuu.Enabled = false;
            this.btnLuu.Image = global::V6ControlManager.Properties.Resources.Save24;
            this.btnLuu.Location = new System.Drawing.Point(2, 539);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(70, 32);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Tag = "";
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnTim
            // 
            this.btnTim.AccessibleDescription = "ACACTTA1R00044";
            this.btnTim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTim.AutoSize = true;
            this.btnTim.ContextMenuStrip = this.menuBtnTim;
            this.btnTim.Enabled = false;
            this.btnTim.Image = global::V6ControlManager.Properties.Resources.Search24;
            this.btnTim.Location = new System.Drawing.Point(511, 539);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(90, 32);
            this.btnTim.SplitMenuStrip = this.menuBtnTim;
            this.btnTim.TabIndex = 13;
            this.btnTim.Text = "&Tìm";
            this.btnTim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // menuBtnTim
            // 
            this.menuBtnTim.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timTopCuoiKyMenu,
            this.timKhacMenu});
            this.menuBtnTim.Name = "menuBtnIn";
            this.menuBtnTim.Size = new System.Drawing.Size(154, 48);
            // 
            // timTopCuoiKyMenu
            // 
            this.timTopCuoiKyMenu.AccessibleDescription = "INVOICEM00045";
            this.timTopCuoiKyMenu.Name = "timTopCuoiKyMenu";
            this.timTopCuoiKyMenu.Size = new System.Drawing.Size(153, 22);
            this.timTopCuoiKyMenu.Text = "Top 5 chứng từ";
            this.timTopCuoiKyMenu.Click += new System.EventHandler(this.timTopCuoiKyMenu_Click);
            // 
            // timKhacMenu
            // 
            this.timKhacMenu.AccessibleDescription = "INVOICEM00046";
            this.timKhacMenu.Name = "timKhacMenu";
            this.timKhacMenu.Size = new System.Drawing.Size(153, 22);
            this.timKhacMenu.Text = "Khác...";
            // 
            // btnViewInfoData
            // 
            this.btnViewInfoData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewInfoData.Image = global::V6ControlManager.Properties.Resources.Help;
            this.btnViewInfoData.Location = new System.Drawing.Point(862, 552);
            this.btnViewInfoData.Name = "btnViewInfoData";
            this.btnViewInfoData.Size = new System.Drawing.Size(30, 23);
            this.btnViewInfoData.TabIndex = 21;
            this.btnViewInfoData.UseVisualStyleBackColor = true;
            this.btnViewInfoData.Click += new System.EventHandler(this.btnViewInfoData_Click);
            // 
            // lblNameT
            // 
            this.lblNameT.AccessibleDescription = "";
            this.lblNameT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNameT.Location = new System.Drawing.Point(203, 179);
            this.lblNameT.Name = "lblNameT";
            this.lblNameT.Size = new System.Drawing.Size(689, 18);
            this.lblNameT.TabIndex = 22;
            this.lblNameT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PhieuThuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNameT);
            this.Controls.Add(this.btnViewInfoData);
            this.Controls.Add(this.Txtma_nk);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.txtMa_ct);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnQuayRa);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnMoi);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.group5);
            this.Controls.Add(this.group4);
            this.Controls.Add(this.lblDocSoTien);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.group1);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "PhieuThuControl";
            this.Size = new System.Drawing.Size(899, 575);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.HoaDonBanHangKiemPhieuXuat_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabKhac.ResumeLayout(false);
            this.tabKhac.PerformLayout();
            this.tabChiTietBoSung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.group5.ResumeLayout(false);
            this.panelVND.ResumeLayout(false);
            this.panelVND.PerformLayout();
            this.panelNT.ResumeLayout(false);
            this.panelNT.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.menuChucNang.ResumeLayout(false);
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.menuBtnIn.ResumeLayout(false);
            this.menuBtnTim.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox group1;
        private V6Label v6Label3;
        private V6Label v6Label1;
        private V6VvarTextBox txtLoaiPhieu;
        private V6VvarTextBox txtSoPhieu;
        private System.Windows.Forms.GroupBox group3;
        private System.Windows.Forms.GroupBox group4;
        private System.Windows.Forms.GroupBox group5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabChiTiet;
        private System.Windows.Forms.TabPage tabKhac;
        private V6DateTimePicker dateNgayCT;
        private V6Controls.V6ColorDataGridView dataGridView1;
        private wyDay.Controls.SplitButton btnTim;
        private V6DateTimePicker dateNgayLCT;
        private V6Label v6Label5;
        private System.Windows.Forms.GroupBox group2;
        private V6Label v6Label7;
        private V6Label v6Label8;
        private V6Label lblTK;
        private V6VvarTextBox v6ColorTextBox1;
        private V6VvarTextBox txtTk;
        private V6Label lblMaKH;
        private V6VvarTextBox Txtdien_giai;
        private V6Label v6Label12;
        private V6VvarTextBox txtDiaChi;
        private V6Label v6Label11;
        private V6Label v6Label17;
        private V6VvarTextBox txtMaKh;
        private V6Label lblTongSoDong;
        private V6Controls.V6ColorComboBox cboKieuPost;
        private V6Label v6Label20;
        private V6Label lblTTT;
        private System.Windows.Forms.Panel panelVND;
        private System.Windows.Forms.Panel panelNT;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnMoi;
        private System.Windows.Forms.Button btnCopy;
        private wyDay.Controls.SplitButton btnIn;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnQuayRa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private V6VvarTextBox txtTenKh;
        private HD_Detail detail1;
        private V6VvarTextBox txtMaSoThue;
        private System.Windows.Forms.Button btnHuy;
        private V6Controls.V6ComboBox cboMaNt;
        private V6NumberTextBox txtTyGia;
        private V6Label lblDocSoTien;
        private V6VvarTextBox txtMa_ct;
        private NumberTienNt txtTongThanhToanNt;
        private NumberTien txtTongThanhToan;
        private V6VvarTextBox txtTenGiaoDich;
        private V6Label v6Label14;
        private V6VvarTextBox txtMaDVCS;
        private V6VvarTextBox txtTenDVCS;
        private V6Label lblMaDVCS;
        private V6Controls.V6CheckBox chkSuaTien;
        private V6FormButton btnChonNhieuHD;
        private V6VvarTextBox Txtma_nk;
        private V6VvarTextBox Txtma_nvien;
        private V6VvarTextBox TxtMa_bp;
        private V6VvarTextBox txtMa_sonb;
        private V6Label v6Label2;
        private Button btnViewInfoData;
        private Button btnInfos;
        private V6VvarTextBox txtGC_UD3;
        private V6VvarTextBox txtGC_UD2;
        private V6VvarTextBox txtGC_UD1;
        private Label label29;
        private Label label28;
        private Label label27;
        private V6NumberTextBox txtSL_UD3;
        private V6NumberTextBox txtSL_UD2;
        private V6NumberTextBox txtSL_UD1;
        private Label label16;
        private Label label17;
        private Label label22;
        private V6Controls.V6VvarTextBox txtMA_UD3;
        private V6Controls.V6VvarTextBox txtMA_UD2;
        private V6Controls.V6VvarTextBox txtMA_UD1;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private V6DateTimeColor txtNGAY_UD3;
        private V6DateTimeColor txtNGAY_UD2;
        private V6DateTimeColor txtNGAY_UD1;
        private V6VvarTextBox TxtTen_tk;
        private V6Label lblNameT;
        private TabPage tabChiTietBoSung;
        private HD_Detail detail3;
        private V6ColorDataGridView dataGridView3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn TEN_TK;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private V6Label lblTongTangGiam;
        private NumberTienNt txtTongTangGiamNt;
        private NumberTienNt txtTongTangGiam;
        private DataGridViewTextBoxColumn TK_I;
        private DataGridViewTextBoxColumn TEN_TK_I;
        private DataGridViewTextBoxColumn UID;
        private DataGridViewTextBoxColumn STT_REC;
        private DataGridViewTextBoxColumn STT_REC0;
        private V6VvarTextBox txtSoCtKemt;
        private Label label1;
        private V6VvarTextBox txtGhiChuChung;
        private Label label2;
        private ContextMenuStrip menuChucNang;
        private ToolStripMenuItem ThuCongNoMenu;
        private ToolStripMenuItem TroGiupMenu;
        private ToolStripMenuItem chonTuExcelMenu;
        private DropDownButton btnChucNang;
        private V6Controls.V6ComboBox cboChuyenData;
        private V6Label v6Label28;
        private ToolStripMenuItem xuLyKhacMenu;
        private ToolStripMenuItem thayTheMenu;
        private ToolStripMenuItem thayThe2Menu;
        private V6Label lblKieuPostColor;
        private ToolStripMenuItem thuNoTaiKhoanMenu;
        private GridViewSummary gridViewSummary3;
        private V6FormButton btnChonHD;
        private V6VvarTextBox txtSoct_tt;
        private ToolStripMenuItem thayTheNhieuMenu;
        private ContextMenuStrip menuBtnIn;
        private ToolStripMenuItem inPhieuHachToanMenu;
        private ToolStripMenuItem ThuCuocContMent;
        private ToolStripMenuItem exportXmlMenu;
        private ToolStripMenuItem importXmlMenu;
        private ContextMenuStrip menuBtnTim;
        private ToolStripMenuItem timTopCuoiKyMenu;
        private ToolStripMenuItem timKhacMenu;
        private ToolStripMenuItem inKhacMenu;
        private V6CheckBox chkAutoNext;
    }
}
