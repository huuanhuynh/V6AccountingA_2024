using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.DonDatHangBan.Loc;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonBaoGia;
using V6Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.DonDatHangBan
{
    /// <summary>
    /// Hóa đơn bán hàng kiêm phiếu xuất
    /// </summary>
    public partial class DonDatHangBanControl_A1 : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice91 Invoice = new V6Invoice91();

        private string l_ma_km = "";
        private string _m_Ma_td;
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public DonDatHangBanControl_A1()
        {
            InitializeComponent();
            MyInit();
        }
        public DonDatHangBanControl_A1(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Khởi tạo form chứng từ.
        /// </summary>
        /// <param name="maCt">Mã chứng từ.</param>
        /// <param name="itemId"></param>
        /// <param name="sttRec">Có mã hợp lệ sẽ tải dữ liệu lên để sửa.</param>
        public DonDatHangBanControl_A1(string maCt, string itemId, string sttRec)
            : base(new V6Invoice91(), itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();

            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {   
            ReorderGroup1TabIndex();

            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtManx.Upper();
            txtManx.FilterStart = true;
            txtTkThueCo.FilterStart = true;
            txtTkThueNo.FilterStart = true;
            txtTkChietKhau.FilterStart = true;
            txtTkGt.FilterStart = true;
            txtTkThueCo.SetInitFilter("Loai_tk = 1");
            txtTkThueNo.SetInitFilter("Loai_tk = 1");
            txtTkChietKhau.SetInitFilter("Loai_tk = 1");
            txtTkGt.SetInitFilter("Loai_tk = 1");
            txtLoaiNX_PH.SetInitFilter("LOAI = 'X'");
            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                txtMa_sonb.SetInitFilter("MA_DVCS='" + V6Login.Madvcs + "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1" +
                    (V6Login.IsAdmin?"":" AND  dbo.VFA_Inlist_MEMO(MA_SONB,'" + V6Login.UserRight.RightSonb + "')=1"));
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1" +
                    (V6Login.IsAdmin ? "" : " AND  dbo.VFA_Inlist_MEMO(MA_SONB,'" + V6Login.UserRight.RightSonb + "')=1"));
                    
            }
            
            txtMaHttt.Upper();
            //V6ControlFormHelper.CreateGridViewStruct(dataGridView1, ad81Struct);
            
            var dataGridViewColumn = dataGridView1.Columns["UID"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (Guid);
            //,,,
            dataGridViewColumn = dataGridView1.Columns["MA_VT"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["TEN_VT"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["STT_REC"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["STT_REC0"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);

            SetGridViewFomular();
            SetGridViewEvent();

            cboKieuPost.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);

            _m_Ma_td = (Invoice.Alct["M_MA_TD"] ?? "0").ToString().Trim();
            
            LoadDetailControls();
            LoadAdvanceControls(Invoice.Mact);
            CreateCustomInfoTextBox(group4, txtTongSoLuong1, cboChuyenData);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            //LoadTagAndText(Invoice, detail1.Controls);
            HideControlByGRD_HIDE();
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        //private V6ColorTextBox _dvt;
        //private V6CheckTextBox _tang, _xuat_dd;
        //private V6VvarTextBox _maVt, _Ma_lnx_i, _dvt1, _maKho, _maKhoI, _tkDt, _tkGv, _tkCkI, _tkVt, _maLo, _ma_thue_i, _tk_thue_i;
        //private V6NumberTextBox _soLuong1, _soLuong, _he_so1T, _he_so1M, _giaNt2, _giaNt21, _gia_ban_nt, _gia_ban, _tien2, _tienNt2, _ck, _ckNt, _gia2, _gia21;
        //private V6NumberTextBox _ton13, _ton13s, _ton13Qd, _gia, _gia_nt, _tien, _tienNt, _pt_cki, _thue_suat_i, _thue_nt, _thue;
        //private V6NumberTextBox _sl_qd, _sl_qd2, _tien_vcNt, _tien_vc, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4, _ggNt, _gg;
        //private V6DateTimeColor _hanSd;

        private void LoadDetailControls()
        {
            LoadAD("");
            SetGridViewData();
            //Lấy các control động
            //detailControlList1 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);
            ChungTu.ApplyAlct1toGridView(Invoice.Alct1, dataGridView1, out _orderList, out _carryFields, out _alct1Dic);
            
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<string, AlctControls> item in detailControlList1)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);
                
                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                //if (control is V6ColorTextBox && item.Value.IsCarry)
                //{
                //    detail1.CarryFields.Add(NAME);
                //}
                // Gán tag hide và readonly theo GRD_xxxx
                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":"))
                {
                    control.InvisibleTag();
                }
                if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                {
                    control.ReadOnlyTag();
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects);

                switch (NAME)
                {
                    case "MA_VT":
                        //_mavt_default_initfilter = _maVt.InitFilter;!!
                        break;
                    case "MA_LNX_I":
                        
                        break;
                    case "TK_DT":
                        
                        break;
                    case "TK_GV":
                        
                        break;
                    case "TK_CKI":
                        
                        
                        break;
                    case "TK_VT":
                        
                        
                        break;
                    case "DVT1":
                        //_dvt1.SetInitFilter("");
                        //_dvt1.BrotherFields = "ten_dvt";
                        break;
                    case "MA_KHO":
                        break;
                    case "MA_KHO_I":
                        break;
                    case "MA_THUE_I":
                        
                        break;
                    case "TK_THUE_I":
                        
                        break;
                    case "THUE_SUAT_I":
                        //_thue_suat_i = control as V6NumberTextBox;
                        break;
                    case "THUE":
                        //_thue = control as V6NumberTextBox;
                        break;
                    case "THUE_NT":
                        
                        break;
                    case "TON13":
                        //_ton13 = (V6NumberTextBox)control;
                        //if (_ton13.Tag == null || _ton13.Tag.ToString() != "hide") _ton13.Tag = "disable";!!!!!
                        break;
                    case "TON13S":
                        //_ton13s = (V6NumberTextBox)control;
                        //_ton13.Tag = "disable";
                        break;
                    case "TON13QD":
                        //_ton13Qd = control as V6NumberTextBox;!!!!!
                        //if (_ton13Qd.Tag == null || _ton13Qd.Tag.ToString() != "hide")
                        //{
                        //    _ton13Qd.Tag = "disable";
                        //}
                        //break;
                    case "SO_LUONG1":
                        break;

                    case "SO_LUONG":
                        break;
                    case "HE_SO1T":
                        //_he_so1T.DecimalPlaces = Invoice.ADStruct.ContainsKey("HE_SO1T")
                            //? Invoice.ADStruct["HE_SO1T"].MaxNumDecimal
                            //: 6;
                        break;
                    case "HE_SO1M":
                        //_he_so1M.DecimalPlaces = Invoice.ADStruct.ContainsKey("HE_SO1M")
                        //    ? Invoice.ADStruct["HE_SO1M"].MaxNumDecimal
                        //    : 6;
                        break;
                    case "GIA_NT2":
                        
                        break;
                    case "GIA2":
                        //_gia2 = (V6NumberTextBox)control;
                        break;
                    case "GIA21":
                        //_gia21 = (V6NumberTextBox)control;
                        break;
                    case "GIA_NT21":
                        //if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //{
                        //    _giaNt21.InvisibleTag();
                        //}
                        //if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) ||
                        //                         Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //{
                        //    _giaNt21.ReadOnlyTag();
                        //}
                        //_gia_nt01.TextChanged += delegate
                        //{
                        //    if (!detail1.IsAddOrEdit) return;

                        //    if (!chkSuaTien.Checked)
                        //    {
                        //        if (_gia_nt01.Value * _soLuong1.Value == 0)
                        //        {
                        //            _tien_nt0.Enabled = true;
                        //            _tien_nt0.ReadOnly = false;
                        //        }
                        //        else
                        //        {
                        //            _tien_nt0.Enabled = false;
                        //            _tien_nt0.ReadOnly = true;
                        //        }
                        //    }
                        //};
                        break;
                    case "GIA_BAN":
                        //_gia_ban = control as V6NumberTextBox;
                        //if (_gia_ban != null)
                        //{
                        //    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //    {
                        //        _gia_ban.InvisibleTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _gia_ban.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    case "GIA_BAN_NT":
                        //_gia_ban_nt = control as V6NumberTextBox;
                        //if (_gia_ban_nt != null)
                        //{
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_HIDE.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _gia_ban_nt.InvisibleTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _gia_ban_nt.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    case "TIEN_NT2":
                        //_tien_nt2 = (V6NumberTextBox)control;
                        //if (_tien_nt2 != null)
                        {
                            //_tien_nt2.Enabled = chkSuaTien.Checked;
                            //if (chkSuaTien.Checked)
                            //{
                            //    _tien_nt2.Tag = null;
                            //}
                            //else
                            //{
                            //    if (_tien_nt2.Tag == null || _tien_nt2.Tag.ToString() != "hide") _tien_nt2.Tag = "disable";
                            //}

                            
                        }
                        
                        break;
                    case "TIEN2":
                        
                        break;

                    //_tien2.V6LostFocus;
                    case "TIEN":
                        //_tien = (V6NumberTextBox)control;!!!!!
                        //if (_tien != null)
                        //{
                        //    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //    {
                        //        _tien.InvisibleTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _tien.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    case "TIEN_NT":
                        //_tienNt.Enabled = chkSuaTien.Checked;!!!!
                        //if (chkSuaTien.Checked)
                        //{
                        //    _tienNt.Tag = null;
                        //}
                        //else
                        //{
                        //    if (_tienNt.Tag == null || _tienNt.Tag.ToString() != "hide") _tienNt.Tag = "disable";
                        //}

                        //if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //{
                        //    _tienNt.InvisibleTag();
                        //}

                        //if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) ||
                        //                         Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //{
                        //    _tienNt.ReadOnlyTag();
                        //}
                        break;
                    
                    case "CK_NT":
                        //_ckNt = control as V6NumberTextBox;
                        //if (_ckNt != null)
                        //{
                            
                        //}
                        break;
                    case "PT_CKI":
                        //_pt_cki = control as V6NumberTextBox;
                        //if (_pt_cki != null)
                        //{
                        //    _pt_cki.Enabled = !chkLoaiChietKhau.Checked;
                        //}
                        break;
                    case "GIA_NT":
                        //_gia_nt = control as V6NumberTextBox;!!!!!
                        //if (_gia_nt != null)
                        //{
                            
                        //    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //    {
                        //        _gia_nt.InvisibleTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _gia_nt.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    case "GIA":
                        //_gia = control as V6NumberTextBox;!!!!
                        //if (_gia != null)
                        //{
                        //    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                        //    {
                        //        _gia.InvisibleTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _gia.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    
                    case "PX_GIA_DDI":
                        
                        break;
                    case "MA_LO":

                        
                        
                        break;
                    case "HSD":
                        //_hanSd = (V6DateTimeColor)control;
                        //_hanSd.Enabled = false;
                        //if (_hanSd.Tag == null || _hanSd.Tag.ToString() != "hide") _hanSd.Tag = "disable";
                        break;
                    //{ Tuanmh 01/01/2017
                    case "SL_QD":
                        //_sl_qd = control as V6NumberTextBox;!!!!
                        //if (_sl_qd != null)
                        //{
                        //    if (M_CAL_SL_QD_ALL == "0")
                        //    {
                        //        if (M_TYPE_SL_QD_ALL == "00")
                        //        {
                        //            _sl_qd.Enabled = false;
                        //            if (_sl_qd.IsVisibleTag()) _sl_qd.DisableTag();
                        //        }
                        //    }
                        //    else if (M_CAL_SL_QD_ALL == "1")
                        //    {
                        //        _sl_qd.EnableTag();
                        //    }
                        //    if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        //    {
                        //        _sl_qd.ReadOnlyTag();
                        //    }
                        //}
                        break;
                    case "SL_QD2":
                        //_sl_qd2 = (V6NumberTextBox)control;!!!!
                        //_sl_qd2.Enabled = false;
                        //if (_sl_qd2.Tag == null || _sl_qd2.Tag.ToString() != "hide") _sl_qd2.Tag = "disable";
                        break;
                    case "HS_QD1":
                        //_hs_qd1 = (V6NumberTextBox)control;
                        //_hs_qd1.Enabled = false;
                        //if (_hs_qd1.Tag == null || _hs_qd1.Tag.ToString() != "hide") _hs_qd1.Tag = "disable";
                        break;
                    case "HS_QD2":
                        //_hs_qd2 = (V6NumberTextBox)control;
                        //_hs_qd2.Enabled = false;
                        //if (_hs_qd2.Tag == null || _hs_qd2.Tag.ToString() != "hide") _hs_qd2.Tag = "disable";
                        break;
                    case "HS_QD3":
                        //_hs_qd3 = (V6NumberTextBox)control;
                        //_hs_qd3.V6LostFocus += Hs_qd3_V6LostFocus;
                        break;
                    case "HS_QD4":
                        //_hs_qd4 = (V6NumberTextBox)control;
                        //_hs_qd4.V6LostFocus += Hs_qd4_V6LostFocus;
                        break;
                    case "GG_NT":
                        //_ggNt = control as V6NumberTextBox;
                        //if (_ggNt != null)
                        //{
                            
                        //}
                        break;
                    case "GG":
                        
                        break;
                    case "TIEN_VC_NT":
                        //_tien_vcNt = (V6NumberTextBox)control;
                        break;
                    case "TIEN_VC":
                        //_tien_vc = (V6NumberTextBox)control;
                        break;
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
            }

            //foreach (AlctControls item in detailControlList1.Values)
            //{
            //    detail1.AddControl(item);
            //}
            
            //detail1.SetStruct(Invoice.ADStruct);
            //detail1.MODE = detail1.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
        }

        

        private void CheckMaLo(DataGridViewRow grow)
        {
            string ma_vt = grow.Cells["MA_VT"].Value.ToString().Trim();
            if (ma_vt != "")
            {
                var ma_lo_column = dataGridView1.Columns["MA_LO"] as V6VvarDataGridViewColumn;
                if (ma_lo_column != null)
                {
                    ma_lo_column.InitFilter = "Ma_vt='" + ma_vt + "'";
                }
            }
            XuLyLayThongTinKhiChonMaLo(grow);
        }

        void _tang_V6LostFocus(DataGridViewCell cell_tang, DataGridViewRow grow)
        {
            if (cell_tang.Value.ToString().Trim() != "")
            {
                SetTang(grow);
            }
            else
            {
                GetGia(dataGridView1.CurrentRow);
            }
        }

        private void SetTang(DataGridViewRow grow)
        {
            try
            {
                SetCellValue(grow.Cells["GIA_NT2"], 0);
                SetCellValue(grow.Cells["GIA_NT21"], 0);
                SetCellValue(grow.Cells["TIEN_NT2"], 0);
                SetCellValue(grow.Cells["TIEN2"], 0);
                SetCellValue(grow.Cells["CK"], 0);
                SetCellValue(grow.Cells["CK_NT"], 0);
                SetCellValue(grow.Cells["GIA2"], 0);
                SetCellValue(grow.Cells["GIA21"], 0);
                SetCellValue(grow.Cells["GG"], 0);
                SetCellValue(grow.Cells["GG_NT"], 0);

                SetCellValue(grow.Cells["GIA_BAN_NT"], 0);
                SetCellValue(grow.Cells["GIA_BAN"], 0);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        
        
        #endregion detail form

        #region ==== Override Methods ====

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == (Keys.LButton | Keys.Space))//pageUp
            {
                if (btnPrevious.Enabled) btnPrevious.PerformClick();
            }
            else if (keyData == (Keys.RButton | Keys.Space))//PageDown
            {
                if (btnNext.Enabled) btnNext.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                //if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                //{
                //    detail1.btnNhan.Focus();
                //    detail1.btnNhan.PerformClick();
                //}
                //else if (detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
                //{
                //    detail3.btnNhan.PerformClick();
                //}
                //else
                {
                    btnLuu.PerformClick();
                }
            }
            else if (keyData == Keys.Escape)
            {
                //if (detail1.MODE == V6Mode.Add)
                //{
                //    if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                //    detail1.btnMoi.PerformClick();
                //}
                //else if (detail1.MODE == V6Mode.Edit)
                //{
                //    if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                //    detail1.btnSua.PerformClick();
                //}
                //else
                {
                    if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    {
                        btnHuy.PerformClick();
                    }
                    else
                    {
                        btnQuayRa.PerformClick();
                    }
                }
            }
            else if (keyData == Keys.F10) // Copy detail
            {
                if (NotAddEdit)
                {
                    ShowParentMessage(V6Text.PreviewingMode);
                    return false;
                }
                if (!Invoice.CheckRightKey("F10"))
                {
                    ShowParentMessage(V6Text.NoRight + " F10");
                    return false;
                }

                //detail1.btnNhan.Focus();
                //if (detail1.MODE == V6Mode.Add)
                //{
                //    var detailData = detail1.GetData();
                //    if (ValidateData_Detail(detailData))
                //    {
                //        if (XuLyThemDetail(detailData))
                //        {
                //            ShowParentMessage(V6Text.InvoiceF3AddDetailSuccess);
                //            All_Objects["data"] = detailData;
                //            InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                //        }
                //    }
                //}
                //else if (detail1.MODE == V6Mode.Edit)
                //{
                //    var detailData = detail1.GetData();
                //    if (ValidateData_Detail(detailData))
                //    {
                //        if (XuLySuaDetail(detailData))
                //        {
                //            detail1.ChangeToAddMode_KeepData();
                //            dataGridView1.Lock();
                //            ShowParentMessage(V6Text.InvoiceF3EditDetailSuccess);
                //            All_Objects["data"] = detailData;
                //            InvokeFormEvent(FormDynamicEvent.AFTEREDITDETAILSUCCESS);
                //        }
                //    }
                //}
                //else
                //{
                //    detail1.ChangeToAddMode_KeepData();
                //    dataGridView1.Lock();
                //}
            }
            else if (keyData == Keys.F4)
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    //detail1.btnNhan.Focus();
                    //if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    //{
                    //    var detailData = detail1.GetData();
                    //    if (ValidateData_Detail(detailData))
                    //    {
                    //        detail1.btnNhan.Focus();
                    //        detail1.btnNhan.PerformClick();
                    //    }
                    //}

                    //if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit)
                    //{
                    //    detail1.OnMoiClick();
                    //}
                }
            }
            else if (keyData == Keys.F7)
            {
                LuuVaIn();
            }
            else if (keyData == Keys.F8)
            {
                if (NotAddEdit) return false;
                if (dataGridView1.Focused && dataGridView1.EditingCell == null && dataGridView1.CurrentRow != null)
                {
                    XuLyXoaDetail();
                    return true;
                }

                return false;
            }
            else if (keyData == Keys.F9)
            {
                XuLyF9Base();
                return true;
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        
        #endregion override methods

        #region ==== Detail Events + Methods ====

        #region Events
        
        
        
        void Dvt1_V6LostFocusNoChange(DataGridViewCell cell_dvt1, DataGridViewRow grow)
        {
            if (NotAddEdit) return;

            var data = CELL_VVAR_DATA(cell_dvt1);
            if (data != null)
            {
                if (dataGridView1.Columns.Contains("TEN_DVT")) grow.Cells["TEN_DVT"].Value = data["TEN_DVT"];
                var he_soT = ObjectAndString.ObjectToDecimal(data["HE_SOT"]);
                var he_soM = ObjectAndString.ObjectToDecimal(data["HE_SOM"]);
                if (he_soT == 0) he_soT = 1;
                if (he_soM == 0) he_soM = 1;
                SetCellValue(grow.Cells["HE_SO1T"], he_soT);
                SetCellValue(grow.Cells["HE_SO1M"], he_soM);
            }
            //else
            //{
            //    if (dataGridView1.Columns.Contains("TEN_DVT")) SetCellValue(grow.Cells["TEN_DVT"], "");
            //    SetCellValue(grow.Cells["HE_SO1T"], 1);
            //    SetCellValue(grow.Cells["HE_SO1M"], 1);
            //}
        }

        public void TienNt2_V6LostFocus(DataGridViewCell cell_tien_nt2, DataGridViewRow grow)
        {
            TinhGiaNt2_NhapTienNt2(grow);
        }

        //void SoLuong1_V6LostFocus(DataGridViewRow grow)
        //{
        //    TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _soLuong1);
        //    TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _soLuong1);
        //    TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _soLuong1);
        //    CELL_DEC(grow, "SO_LUONG") = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
        //    TinhTienNt2(grow, "SO_LUONG1");
        //    Tinh_thue_ct(grow);
        //}

        

        //void Hs_qd4_V6LostFocus(object sender)
        //{
        //    TinhGiamGiaCt(dataGridView1.CurrentRow);
        //}
        //void Hs_qd3_V6LostFocus(object sender)
        //{
        //    TinhVanChuyen(dataGridView1.CurrentRow);
        //}
        #endregion events

        #region Methods
        
        private void XuLyChonMaKhachHang()
        {
            try
            {
                XuLyKhoaThongTinKhachHang();

                var data = txtMaKh.Data;
                if (data == null)
                {
                    txtMaSoThue.Text = "";
                    txtTenKh.Text = "";
                    txtDiaChi.Text = "";
                    //txtMaGia.Text = "";
                    SetControlValue(txtMaGia, null, Invoice.GetTemplateSettingAM("MA_GIA"));
                    return;
                }
                var mst = (data["ma_so_thue"] ?? "").ToString().Trim();
                txtMaSoThue.Text = mst;
                txtTenKh.Text = (data["ten_kh"] ?? "").ToString().Trim();
                txtDiaChi.Text = (data["dia_chi"] ?? "").ToString().Trim();

                // Tuanmh 28/05/2016
                //txtMaGia.Text = (data["MA_GIA"] ?? "").ToString().Trim();
                SetControlValue(txtMaGia, data["MA_GIA"], Invoice.GetTemplateSettingAM("MA_GIA"));

                SetDefaultDataReference(Invoice, ItemID, "TXTMAKH", data);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyKhoaThongTinKhachHang()
        {
            try
            {
                var data = txtMaKh.Data;
                if (data != null)
                {
                    var mst = (data["ma_so_thue"] ?? "").ToString().Trim();

                    if (mst != "")
                    {
                        txtTenKh.Enabled = false;
                        txtDiaChi.Enabled = false;
                        txtMaSoThue.Enabled = false;

                        txtDiaChi.ReadOnlyTag();
                        txtDiaChi.TabStop = false;
                        txtTenKh.ReadOnlyTag();
                        txtTenKh.TabStop = false;
                    }
                    else
                    {
                        txtTenKh.Enabled = true;
                        txtDiaChi.Enabled = true;
                        txtMaSoThue.Enabled = true;

                        txtDiaChi.ReadOnlyTag(false);
                        txtDiaChi.TabStop = true;
                        txtTenKh.ReadOnlyTag(false);
                        txtTenKh.TabStop = true;
                    }
                }
                else
                {
                    txtTenKh.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtMaSoThue.Enabled = true;

                    txtDiaChi.ReadOnlyTag(false);
                    txtDiaChi.TabStop = true;
                    txtTenKh.ReadOnlyTag(false);
                    txtTenKh.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyChonMaKhoI(DataGridViewRow grow, DataGridViewCell cell)
        {
            XuLyLayThongTinKhiChonMaKhoI(grow, cell);
            GetTon13(grow);
        }
        private void XuLyChonMaVt(DataGridViewCell cell_mavt, DataGridViewRow grow)
        {
            try
            {
                XuLyLayThongTinKhiChonMaVt(cell_mavt, grow);
                XuLyDonViTinhKhiChonMaVt(cell_mavt, grow);
                GetGia(grow);
                GetTon13(grow);
                TinhTienNt2(grow);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyLayThongTinKhiChonMaKhoI(DataGridViewRow grow, DataGridViewCell cell)
        {
        }

        private void XuLyLayThongTinKhiChonMaLo(DataGridViewRow grow)
        {
            try
            {
                if (IS(grow.Cells["MA_VT"], "LO_YN"))
                {
                    if (STR(grow, "MA_LO").Trim() != "")
                    {
                        var data = CELL_VVAR_DATA(grow.Cells["MA_LO"]);
                        if (data != null)
                            SetCellValue(grow.Cells["HSD"], ObjectAndString.ObjectToDate(data["NGAY_HHSD"]));
                    }
                    else
                    {
                        SetCellValue(grow.Cells["HSD"], null);
                    }
                }
                else
                {
                    SetCellValue(grow.Cells["HSD"], null);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private DataTable _dataLoDate;

        public void GetTon13(DataGridViewRow grow)
        {
            try
            {
                var row_data = grow.ToDataDictionary();
                string ma_vt = ObjectAndString.ObjectToString(row_data["MA_VT"]).Trim().ToUpper();
                string ma_kho_i = ObjectAndString.ObjectToString(row_data["MA_KHO_I"]).Trim().ToUpper();// _maKhoI.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataLoDate = Invoice.GetStock(ma_vt, ma_kho_i, _sttRec, dateNgayCT.Date);
                    if (_dataLoDate != null && _dataLoDate.Rows.Count > 0)
                    {
                        DataRow row0 = _dataLoDate.Rows[0];
                        grow.Cells["TON13"].Value = ObjectAndString.ObjectToDecimal(row0["ton00"]);
                        if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") grow.Cells["TON13QD"].Value = ObjectAndString.ObjectToDecimal(row0["ton00Qd"]);
                    }
                    else
                    {
                        grow.Cells["TON13"].Value = 0;
                        if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") grow.Cells["TON13QD"].Value = 0;
                    }

                    if (dataGridView1.Columns.Contains("TON13S")) grow.Cells["TON13S"].Value = grow.Cells["TON13"].Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        public void XuLyLayThongTinKhiChonMaVt(DataGridViewCell cell_mavt, DataGridViewRow grow)
        {
            try
            {
                var data = CELL_VVAR_DATA(cell_mavt);
                
                if (data == null)
                {
                    SetCellValue(grow.Cells["TK_DT"], "");
                    SetCellValue(grow.Cells["TK_GV"], "");
                    SetCellValue(grow.Cells["TK_CKI"], "");
                    SetCellValue(grow.Cells["TK_VT"], "");
                    SetCellValue(grow.Cells["HS_QD1"], 0);
                    SetCellValue(grow.Cells["HS_QD2"], 0);
                    SetCellValue(grow.Cells["MA_THUE_I"], "");
                    SetCellValue(grow.Cells["THUE_SUAT_I"], 0);
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = 0 vì maVt.Data == null");
                }
                else
                {
                    var BrotherFields = "ten_vt,ten_vt2,dvt,ma_kho,ma_qg,ma_vitri";
                    foreach (string field in ObjectAndString.SplitString(BrotherFields))
                    {
                        string FIELD = field.Trim().ToUpper();
                        if (!dataGridView1.Columns.Contains(FIELD)) continue;

                        if (data.ContainsKey(FIELD)) SetCellValue(grow.Cells[FIELD], data[FIELD]);
                        else grow.Cells[FIELD].Value = DBNull.Value;
                    }

                    SetADSelectMoreControlValue(Invoice, data);
                    
                    SetCellValue(grow.Cells["TK_DT"], data["TK_DT"], Invoice.GetTemplateSettingAD("TK_DT"));
                    SetCellValue(grow.Cells["TK_GV"], data["TK_GV"], Invoice.GetTemplateSettingAD("TK_GV"));
                    SetCellValue(grow.Cells["SO_LUONG1"], data["PACKS1"], Invoice.GetTemplateSettingAD("PACKS1"));
                    
                    if (M_SOA_HT_KM_CK == "1")
                    {
                        SetCellValue(grow.Cells["TK_CKI"], data["TK_CK"] ?? "");
                        dataGridView1.Columns["TK_CKI"].ReadOnly = false;
                    }
                    else
                    {
                        SetCellValue(grow.Cells["TK_CKI"], "");
                        dataGridView1.Columns["TK_CKI"].ReadOnly = true;
                    }

                    SetCellValue(grow.Cells["TK_VT"], data["TK_VT"] ?? "");
                    SetCellValue(grow.Cells["HS_QD1"], data["HS_QD1"]);
                    SetCellValue(grow.Cells["HS_QD2"], data["HS_QD2"]);

                    if (M_SOA_MULTI_VAT == "1")
                    {
                        SetCellValue(grow.Cells["MA_THUE_I"], data["MA_THUE"] ?? "");
                        SetCellValue(grow.Cells["THUE_SUAT_I"], data["THUE_SUAT"]);

                        V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["THUE_SUAT"]);

                        var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + STR(grow, "MA_THUE_I") + "'");
                        if (alThue.TotalRows > 0)
                        {
                            SetCellValue(grow.Cells["TK_THUE_I"], alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim());
                            if (STR(grow, "TK_THUE_I") != "") txtTkThueCo.Text = STR(grow, "TK_THUE_I");
                        }
                    }
                }

                var ma_lo_column = dataGridView1.Columns["MA_LO"];
                if (IS_NOT(cell_mavt, "LO_YN"))
                {   
                    SetCellValue(grow.Cells["MA_LO"], "");
                    SetCellValue(grow.Cells["HSD"], null);
                    ma_lo_column.ReadOnly = true;//.Enabled = false;
                }
                else
                {
                    ma_lo_column.ReadOnly = false; //_maLo.Enabled = true;
                }

                //SetDefaultDataDetail(Invoice, detail1.panelControls);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Setinitfilter, readonly-tag...
        /// </summary>
        /// <param name="cell_mavt"></param>
        /// <param name="grow"></param>
        /// <param name="changeMavt">Fix trạng thái của dvt khi sửa focusDvt=false</param>
        private void XuLyDonViTinhKhiChonMaVt(DataGridViewCell cell_mavt, DataGridViewRow grow, bool changeMavt = true)
        {
            try
            {
                //Gán lại dvt và dvt1
                string ma_vt = cell_mavt.Value.ToString().Trim();
                var data_mavt = CELL_VVAR_DATA(cell_mavt);
                if (data_mavt == null)
                {
                    SetCellValue(grow.Cells["DVT"], ""); // !!!!_dvt.ChangeText(""); xulychondvt
                    var dvt_column = dataGridView1.Columns["DVT"] as V6VvarDataGridViewColumn;
                    if (dvt_column != null) dvt_column.InitFilter = ""; // _dvt1.SetInitFilter("");
                    SetCellValue(grow.Cells["DVT1"], ""); // !!!!_dvt1.ChangeText("");
                    return;
                }

                if (changeMavt)
                {
                    SetCellValue(grow.Cells["DVT"], data_mavt["DVT"]);
                    var dvt1_column = dataGridView1.Columns["DVT1"] as V6VvarDataGridViewColumn;
                    if (dvt1_column != null) dvt1_column.InitFilter = "ma_vt='" + ma_vt + "'";
                    SetCellValue(grow.Cells["DVT1"], data_mavt["DVT"]);
                    // !!!!! _dvt1.ExistRowInTable(true);
                }

                var column_dvt = dataGridView1.Columns["DVT1"];// as V6VvarDataGridViewColumn;
                if (data_mavt.ContainsKey("NHIEU_DVT"))
                {
                    var nhieuDvt = data_mavt["NHIEU_DVT"].ToString().Trim();
                    if (nhieuDvt == "1")
                    {
                        column_dvt.ReadOnly = false;
                        if (changeMavt)
                        {
                            SetCellValue(grow.Cells["HE_SO1T"], 1);
                            SetCellValue(grow.Cells["HE_SO1M"], 1);
                        }
                    }
                    else
                    {
                        column_dvt.ReadOnly = true;
                        if (changeMavt)
                        {
                            //dataGridView1.CurrentCell = grow.Cells[column_dvt.Name]; // bỏ
                            SetCellValue(grow.Cells["HE_SO1T"], 1);
                            SetCellValue(grow.Cells["HE_SO1M"], 1);
                        }
                    }
                }
                else
                {
                    // !!!!! _dvt1.ExistRowInTable(_dvt1.Text); lấy tên dvt????
                    column_dvt.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void GetGia(DataGridViewRow grow)
        {
            try
            {
                if (txtMaGia.Text.Trim() == "") return;
                
                var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), STR(grow, "MA_VT"), STR(grow, "DVT1"), "", txtMaGia.Text);

                var gia_ban_nt_column = dataGridView1.Columns["GIA_BAN_NT"];

                if (gia_ban_nt_column != null && V6Options.GetValue("M_SOA_PRICE_INCLUDE_VAT") == "1")
                {
                    SetCellValue(grow.Cells["GIA_BAN_NT"], ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]));
                    if (_maNt == _mMaNt0)
                    {
                        grow.Cells["GIA_BAN"].Value = grow.Cells["GIA_BAN_NT"].Value;
                    }

                    TinhGiaNt21(grow);
                    TinhGiaNt2_NhapTienNt2(grow);
                }
                else
                {
                    grow.Cells["GIA_NT21"].Value = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);

                    if (STR(grow, "DVT").ToUpper() == STR(grow, "DVT1").ToUpper())
                    {
                        grow.Cells["GIA_NT2"].Value = grow.Cells["GIA_NT21"].Value;
                    }
                    else
                    {
                        if (CELL_DEC(grow, "SO_LUONG") != 0)
                        {
                            SetCellValue(grow.Cells["GIA_NT2"], CELL_DEC(grow, "TIEN_NT2") / CELL_DEC(grow, "SO_LUONG"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyThayDoiDvt1(DataGridViewCell cell_dvt, DataGridViewRow grow)
        {
            var data = CELL_VVAR_DATA(cell_dvt);
            if (data == null)
            {
                SetCellValue(grow.Cells["HE_SO1T"], 1);
                SetCellValue(grow.Cells["HE_SO1M"], 1);
                return;
            }

            var he_soT = ObjectAndString.ObjectToDecimal(data["HE_SOT"]);
            var he_soM = ObjectAndString.ObjectToDecimal(data["HE_SOM"]);
            if (he_soT == 0) he_soT = 1;
            if (he_soM == 0) he_soM = 1;
            SetCellValue(grow.Cells["HE_SO1T"], he_soT);
            SetCellValue(grow.Cells["HE_SO1M"], he_soM);
            if (dataGridView1.Columns.Contains("TEN_DVT")) grow.Cells["TEN_DVT"].Value = data["TEN_DVT"];

            GetGia(grow);
            TinhTienNt2(grow);
        }

        public void TinhTienNt2(DataGridViewRow grow, string actionControl = null)
        {
            try
            {
                grow.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "SO_LUONG1") * CELL_DEC(grow, "GIA_NT21"), M_ROUND_NT);
                if (_maNt == _mMaNt0)
                {
                    grow.Cells["TIEN2"].Value = grow.Cells["TIEN_NT2"].Value;
                }
                else
                {
                    grow.Cells["TIEN2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT2") * txtTyGia.Value, M_ROUND);
                }

                TinhChietKhauChiTiet(grow, txtTyGia.Value);
                TinhGiaNt2(grow);
                TinhVanChuyen(grow);
                TinhGiamGiaCt(grow);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiamGiaCt(DataGridViewRow grow)
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    SetCellValue(grow.Cells["GG_NT"], V6BusinessHelper.Vround(CELL_DEC(grow, "SO_LUONG1") * CELL_DEC(grow, "HS_QD4"), M_ROUND_NT));

                    if (_maNt == _mMaNt0)
                    {
                        SetCellValue(grow.Cells["GG"], grow.Cells["GG_NT"].Value);
                    }
                    else
                    {
                        SetCellValue(grow.Cells["GG"], V6BusinessHelper.Vround(CELL_DEC(grow, "GG_NT") * txtTyGia.Value, M_ROUND));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiamGiaCt_row(DataGridViewRow row)
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    var gg_nt = row.Cells["GG_NT"];
                    var gg = row.Cells["GG"];
                    var so_luong1 = row.Cells["SO_LUONG1"];
                    var hs_qd4 = row.Cells["HS_QD4"];

                    gg_nt.Value = V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(so_luong1.Value) * ObjectAndString.ObjectToDecimal(hs_qd4.Value)), M_ROUND_NT);
                    gg.Value = _maNt == _mMaNt0 ? gg_nt.Value : V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(gg_nt.Value) * txtTyGia.Value), M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhVanChuyen(DataGridViewRow grow)
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    var tien_vcNT = V6BusinessHelper.Vround((CELL_DEC(grow, "SO_LUONG1") * CELL_DEC(grow, "HS_QD3")), M_ROUND_NT);
                    SetCellValue(grow.Cells["TIEN_VC_NT"], tien_vcNT);
                    SetCellValue(grow.Cells["TIEN_VC"], V6BusinessHelper.Vround(tien_vcNT * txtTyGia.Value, M_ROUND));

                    if (_maNt == _mMaNt0)
                    {
                        grow.Cells["TIEN_VC"].Value = grow.Cells["TIEN_VC_NT"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhVanChuyen " + _sttRec, ex);
            }
        }

        public void TinhVanChuyen_row(DataGridViewRow row)
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    var tien_vc_nt = row.Cells["TIEN_VC_NT"];
                    var tien_vc = row.Cells["TIEN_VC"];
                    var so_luong1 = row.Cells["SO_LUONG1"];
                    var hs_qd3 = row.Cells["HS_QD3"];

                    tien_vc_nt.Value = V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(so_luong1.Value) * ObjectAndString.ObjectToDecimal(hs_qd3.Value)), M_ROUND_NT);
                    tien_vc.Value = _maNt == _mMaNt0 ? tien_vc_nt.Value : V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(tien_vc_nt.Value) * txtTyGia.Value), M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhVanChuyenG " + _sttRec, ex);
            }
        }

        public void TinhTienVon(DataGridViewRow grow)
        {
            var tien_nt = V6BusinessHelper.Vround(CELL_DEC(grow, "SO_LUONG") * CELL_DEC(grow, "GIA_NT"), M_ROUND_NT);
            SetCellValue(grow.Cells["TIEN_NT"], tien_nt);
            if (_maNt == _mMaNt0)
            {
                SetCellValue(grow.Cells["TIEN"], tien_nt);
            }
            else
            {
                SetCellValue(grow.Cells["TIEN"], V6BusinessHelper.Vround(tien_nt * txtTyGia.Value, M_ROUND));
            }
        }

        public void TinhGiaVon(DataGridViewRow grow)
        {
            if (CELL_DEC(grow, "SO_LUONG") != 0)
            {
                var gia_nt = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA_NT);
                SetCellValue(grow.Cells["GIA_NT"], gia_nt);

                if (_maNt == _mMaNt0)
                {
                    SetCellValue(grow.Cells["GIA"], gia_nt);
                }
                else
                {
                    SetCellValue(grow.Cells["GIA"], V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA));
                }
            }
        }

        public void TinhGiaNt2(DataGridViewRow grow)
        {
            try
            {
                if (_maNt == _mMaNt0)
                {
                    grow.Cells["GIA21"].Value = grow.Cells["GIA_NT21"].Value;
                }
                else
                {
                    grow.Cells["GIA21"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "GIA_NT21") * txtTyGia.Value, M_ROUND_GIA);
                }

                if (CELL_DEC(grow, "SO_LUONG") != 0)
                {
                    if (CELL_DEC(grow, "HE_SO1T") == 1 && CELL_DEC(grow, "HE_SO1M") == 1)
                    {
                        grow.Cells["GIA_NT2"].Value = grow.Cells["GIA_NT21"].Value;;
                        grow.Cells["GIA2"].Value = grow.Cells["GIA21"].Value;
                    }
                    else
                    {
                        grow.Cells["GIA_NT2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT2") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA_NT);
                        grow.Cells["GIA2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN2") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA);
                    }

                    if (_maNt == _mMaNt0)
                    {
                        grow.Cells["GIA2"].Value = grow.Cells["GIA_NT2"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Tính lại giá chưa thuế
        /// </summary>
        public void TinhGiaNt21(DataGridViewRow grow)
        {
            try
            {
                decimal thue_suat = 0m;
                if (M_SOA_MULTI_VAT == "1")
                {
                    thue_suat = CELL_DEC(grow, "THUE_SUAT_I");
                }
                else
                {
                    thue_suat = txtThueSuat.Value;
                }

                grow.Cells["GIA_NT21"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "GIA_BAN_NT") / (1 + (thue_suat / 100)), M_ROUND_GIA_NT);
                
                if (_maNt == _mMaNt0)
                {
                    grow.Cells["GIA21"].Value = grow.Cells["GIA_NT21"].Value;
                }
                else
                {
                    grow.Cells["GIA21"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "GIA_NT21") * txtTyGia.Value, M_ROUND_GIA);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// chay khi nhap tien
        /// </summary>
        public void TinhGiaNt2_NhapTienNt2(DataGridViewRow grow)
        {
            try
            {
                if (_maNt == _mMaNt0)
                {
                    grow.Cells["TIEN2"].Value = grow.Cells["TIEN_NT2"].Value;
                }
                else
                {
                    grow.Cells["TIEN2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT2") * txtTyGia.Value, M_ROUND);
                }

                if (CELL_DEC(grow, "SO_LUONG1") != 0)
                {
                    grow.Cells["GIA_NT21"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT2") / CELL_DEC(grow, "SO_LUONG1"), M_ROUND_GIA_NT);
                    
                    if (_maNt == _mMaNt0)
                    {
                        grow.Cells["GIA21"].Value = grow.Cells["GIA_NT21"].Value;
                    }
                    else
                    {
                        grow.Cells["GIA21"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN2") / CELL_DEC(grow, "SO_LUONG1"), M_ROUND_GIA);
                    }
                }

                if (CELL_DEC(grow, "HE_SO1T") == 1 && CELL_DEC(grow, "HE_SO1M") == 1)
                {
                    grow.Cells["GIA_NT2"].Value = grow.Cells["GIA_NT21"].Value;
                    grow.Cells["GIA2"].Value = grow.Cells["GIA21"].Value;
                }
                else if (CELL_DEC(grow, "SO_LUONG") != 0)
                {
                    grow.Cells["GIA_NT2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN_NT2") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA_NT);
                    grow.Cells["GIA2"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "TIEN2") / CELL_DEC(grow, "SO_LUONG"), M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        grow.Cells["GIA2"].Value = grow.Cells["GIA_NT2"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        #endregion methods
        

        #endregion detail events

        
        #region ==== Show Hide Enable Disable controls ====

        public override void EnableVisibleControls()
        {
            try
            {
                var readOnly = Mode != V6Mode.Edit && Mode != V6Mode.Add;
                V6ControlFormHelper.SetFormControlsReadOnly(this, readOnly);
                foreach (ToolStripMenuItem item in menuChucNang.Items)
                {
                    V6ControlFormHelper.SetFormMenuItemReadOnly(item, readOnly);
                }

                if (readOnly)
                {
                    //detail1.MODE = V6Mode.Lock;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.ReadOnly = true;
                }
                else
                {
                    dataGridView1.ReadOnly = false;
                    dataGridView1.AllowUserToAddRows = true;
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    var grow = dataGridView1.CurrentRow;
                    if (grow != null)
                    {
                        dataGridView1.LockColumn("TIEN_NT2", !chkSuaTien.Checked);
                        //_dvt1.Enabled = true;
                        dataGridView1.LockColumn("TIEN_NT", !(chkSuaTien.Checked && CELL_STRING_NULL(grow, "PX_GIA_DDI") != ""));
                    }
                    

                    //{Tuanmh 20/02/2016
                    var ck_nt_column = dataGridView1.Columns["CK_NT"];
                    if (ck_nt_column != null) ck_nt_column.ReadOnly = chkLoaiChietKhau.Checked;
                    var ck_column = dataGridView1.Columns["CK"];
                    if (ck_column != null) ck_column.ReadOnly = chkLoaiChietKhau.Checked;
                    var gia21_column = dataGridView1.Columns["gia21"];
                    if (gia21_column != null) gia21_column.ReadOnly = !(chkSuaTien.Checked && CELL_DEC(grow, "GIA_NT21") == 0);
                    dataGridView1.LockColumn("GIA21", !(chkSuaTien.Checked && CELL_DEC(grow, "GIA_NT21") == 0));
                    dataGridView1.LockColumn("GIA_NT", !(CELL_STRING_NULL(grow, "PX_GIA_DDI") != ""));
                    dataGridView1.LockColumn("GIA", !(CELL_STRING_NULL(grow, "PX_GIA_DDI") != "" && CELL_DEC(grow, "GIA_NT") == 0));

                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;

                    if (M_SOA_MULTI_VAT == "1")
                    {
                        txtMa_thue.ReadOnly = true;
                        txtTongThue.ReadOnly = true;
                        txtTongThueNt.ReadOnly = true;
                    }
                    else
                    {
                        txtMa_thue.ReadOnly = false;
                        txtTongThue.ReadOnly = !chkT_THUE_NT.Checked;
                        txtTongThueNt.ReadOnly = !chkT_THUE_NT.Checked;
                    }
                }

                //Cac truong hop khac
                if (!readOnly)
                {
                    chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    chkT_CK_NT.Enabled = chkLoaiChietKhau.Checked;

                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    txtTongCkNt.ReadOnly = !chkT_CK_NT.Checked;

                    if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                        V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                    {
                        txtTongGiamNt.ReadOnly = true;
                        txtTongGiam.ReadOnly = true;

                        dataGridView1.UnLockColumn("HS_QD4");
                        dataGridView1.UnLockColumn("GG");
                        dataGridView1.UnLockColumn("GG_NT");
                    }
                    else
                    {
                        txtTongGiamNt.ReadOnly = false;
                        txtTongGiam.ReadOnly = false;

                        dataGridView1.LockColumn("HS_QD4");
                        dataGridView1.LockColumn("GG");
                        dataGridView1.LockColumn("GG_NT");
                    }

                    if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                        V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                    {
                        TxtT_TIENVCNT.ReadOnly = true;
                        TxtT_TIENVC.ReadOnly = true;

                        dataGridView1.UnLockColumn("HS_QD3");
                        dataGridView1.UnLockColumn("TIEN_VC");
                        dataGridView1.UnLockColumn("TIEN_VCNT");
                    }
                    else
                    {
                        TxtT_TIENVCNT.ReadOnly = false;
                        TxtT_TIENVC.ReadOnly = false;

                        dataGridView1.LockColumn("HS_QD3");
                        dataGridView1.LockColumn("TIEN_VC");
                        dataGridView1.LockColumn("TIEN_VCNT");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            SetControlReadOnlyHide(this, Invoice, Mode, V6Mode.View);
        }

        public override void EnableNavigationButtons()
        {
            if (AM == null || AM.Rows.Count == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                if (CurrentIndex <= 0)
                {
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                }
                else
                {
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (CurrentIndex >= AM.Rows.Count - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
            }
        }

        public override void EnableFunctionButtons()
        {
            btnLuu.Enabled = false;
            btnHuy.Visible = false;
            btnHuy.Enabled = false;
            btnMoi.Enabled = false;
            btnCopy.Enabled = false;
            btnIn.Enabled = false;
            btnSua.Visible = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnXem.Enabled = false;
            btnTim.Enabled = false;
            btnQuayRa.Enabled = true;

            btnViewInfoData.Enabled = true;
            switch (Mode)
            {
                case V6Mode.Add:
                    btnLuu.Enabled = true;
                    btnHuy.Visible = true;
                    btnHuy.Enabled = true;
                    btnQuayRa.Enabled = true;

                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    break;
                case V6Mode.Edit:
                    btnLuu.Enabled = true;
                    btnHuy.Visible = true;
                    btnHuy.Enabled = true;
                    btnQuayRa.Enabled = true;

                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    break;
                case V6Mode.View:
                    btnMoi.Enabled = true;
                    btnSua.Visible = true;
                    if (IsViewingAnInvoice)
                    {
                        btnCopy.Enabled = true;
                        btnIn.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                    }
                    if (IsHaveInvoice)
                    {
                        btnXem.Enabled = true;
                    }
                    btnTim.Enabled = true;
                    btnViewInfoData.Enabled = true;
                    break;
                case V6Mode.Init:
                    btnMoi.Enabled = true;
                    btnTim.Enabled = true;
                    btnSua.Visible = true;
                    break;
                default:
                    btnQuayRa.Enabled = true;
                    break;
            }
        }
        #endregion enable...

        #region ==== DataGridView ====
        /// <summary>
        /// Gán dữ liệu sau đó sắp xếp và format lại GridView
        /// </summary>
        private void SetGridViewData()
        {
            HienThiTongSoDong(lblTongSoDong);
            dataGridView1.DataSource = AD;
            ReorderDataGridViewColumns();
            FormatGridView();
        }


        private void SetGridViewEvent()
        {
            dataGridView1.UserAddedRow += dataGridView1_UserAddedRow;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;

            dataGridView1.Leave += dataGridView1_Leave;
        }

        void dataGridView1_Leave(object sender, EventArgs e)
        {
            try
            {
                var grow = dataGridView1.CurrentRow;
                if (grow == null) return;
                if (_edittingRow != null)
                {
                    bool validate = ValidateData_Detail_Row(_edittingRow);
                    if (!validate)
                    {
                        DoNothing();
                        _edittingRow = null;
                        return;
                    }
                    else
                    {
                        _edittingRow = null;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private DataGridViewCell _focusCell0 = null;
        
        void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                int index = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None) - 1;
                if (index < 0) return;
                var grow = dataGridView1.Rows[index];

                // Tạo stt_rec0
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                SetCellValue(grow.Cells["STT_REC0"], _sttRec0);
                SetDefaultDetail();
                SetCarry(grow);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //var txtEdit = (TextBox)e.Control;
            //txtEdit.KeyDown += txtEdit_KeyDown;
        }

        //private bool _celledit_enterkey = false;
        
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Dùng như sự kiện V6_LostFocus
            string FIELD = null;
            DataGridViewCell cell = null;
            try
            {
                var grow = dataGridView1.Rows[e.RowIndex];
                var grow_data = grow.ToDataDictionary();
                _carryRowData = grow_data;
                var col = dataGridView1.Columns[e.ColumnIndex];
                FIELD = col.DataPropertyName.ToUpper();
                cell = grow.Cells[e.ColumnIndex];
                var cell_MA_VT = grow.Cells["MA_VT"];
                var cell_SO_LUONG1 = grow.Cells["SO_LUONG1"];
                decimal HE_SO1T = DEC(grow.Cells["HE_SO1T"]);
                decimal HE_SO1M = DEC(grow.Cells["HE_SO1M"]);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;

                //ShowMainMessage("cell_end_edit: " + FIELD);
                All_Objects["grow"] = grow;
                All_Objects["col"] = col;
                All_Objects["cell"] = cell;
                All_Objects["FIELD"] = FIELD;
                InvokeFormEvent(FormDynamicEvent.CELLENDEDIT);

                switch (FIELD)
                {
                    case "MA_VT":
                        XuLyChonMaVt(cell_MA_VT, grow);
                        break;
                    case "DVT1":
                        XuLyThayDoiDvt1(cell, grow);
                        break;
                    case "MA_KHO":
                        grow.Cells["MA_KHO_I"].Value = grow.Cells["MA_KHO"].Value;
                        break;
                    case "MA_KHO_I":
                        XuLyChonMaKhoI(grow, cell);
                        break;
                    case "MA_LO":
                        CheckMaLo(grow);
                        break;
                    case "SO_LUONG1":
                        //SoLuong1_V6LostFocus(grow);
                        #region ==== SO_LUONG1 ====

                        //V6VvarTextBox txtmavt = new V6VvarTextBox() { VVar = "MA_VT", Text = cell_MA_VT.Value.ToString() };
                        var ma_vt_DATA = CELL_VVAR_DATA(cell_MA_VT);
                        if (ma_vt_DATA != null && ObjectAndString.ObjectToBool(ma_vt_DATA["VITRI_YN"]))
                        {
                            var packs1 = ObjectAndString.ObjectToDecimal(ma_vt_DATA["Packs1"]);
                            if (packs1 > 0 && ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) > packs1)
                            {
                                cell_SO_LUONG1.Value = packs1;
                            }
                        }

                        grow.Cells["SO_LUONG"].Value = ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) * HE_SO1T / HE_SO1M;
                        grow.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(grow.Cells["GIA_NT21"].Value), M_ROUND_NT);
                        grow.Cells["TIEN2"].Value = _maNt == _mMaNt0
                            ? grow.Cells["TIEN_NT2"].Value
                            : V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(grow.Cells["TIEN_NT2"].Value) * txtTyGia.Value, M_ROUND);

                        //TinhTienVon(_soLuong1);
                        if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(grow, FIELD);
                        if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(grow, FIELD);
                        if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(grow, FIELD);
                        
                        // tinhtienNT2
                        TinhChietKhauChiTiet(grow, txtTyGia.Value);
                        TinhGiaNt2(grow);
                        TinhVanChuyen(grow);
                        TinhGiamGiaCt(grow);
                        Tinh_thue_ct(grow);

                        #endregion ==== SO_LUONG1 ====
                        break;
                    case "HE_SO1T":
                        //_he_so1T.StringValueChange += (sender, args) =>
                        if (CELL_DEC(grow, "HE_SO1T") == 0)
                        {
                            SetCellValue(cell, 1);
                            return;
                        }
                        if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit))
                        {
                            if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(grow, "HE_SO1T");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                            if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(grow, "HE_SO1T");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                            SetCellValue(grow.Cells["SO_LUONG"], Number.GiaTriBieuThuc("SO_LUONG1*HE_SO1T/HE_SO1M", grow_data));
                            if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(grow, "HE_SO1T");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                        }
                        break;
                    case "HE_SO1M":
                        //_he_so1M.StringValueChange += (sender, args) =>
                        if (CELL_DEC(grow, "HE_SO1M") == 0)
                        {
                            SetCellValue(grow.Cells["HE_SO1M"], 1);
                            return;
                        }
                        if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit))
                        {
                            if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(grow, "HE_SO1M");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                            if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(grow, "HE_SO1M");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                            SetCellValue(grow.Cells["SO_LUONG"], Number.GiaTriBieuThuc("SO_LUONG1*HE_SO1T/HE_SO1M", grow_data));
                            if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(grow, "HE_SO1M");// _soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                        }
                        break;
                    case "HS_QD3":
                        TinhVanChuyen(grow);
                        break;
                    case "HS_QD4":
                        TinhGiamGiaCt(grow);
                        break;
                    case "SL_QD":
                        //_sl_qd.V6LostFocus += delegate
                        //{
                        //    TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                        //    TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                        //    TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                        //    CELL_DEC(grow, "SO_LUONG") = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                        //    if (M_CAL_SL_QD_ALL == "1")
                        //    {
                        //        //CheckSoLuong1();
                        //        chkT_THUE_NT.Checked = false;
                        //        Tinh_thue_ct();
                        //    }
                        //};

                        #region ==== SL_QD ====
                        if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(grow, FIELD);
                        if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(grow, FIELD);
                        if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(grow, FIELD);
                        grow.Cells["SO_LUONG"].Value = ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) * HE_SO1T / HE_SO1M;
                        if (M_CAL_SL_QD_ALL == "1")
                        {
                            //CheckSoLuong1_row(row, HE_SO1T, HE_SO1M, FIELD);
                            chkT_THUE_NT.Checked = false;
                            Tinh_thue_ct_row_XUAT_TIEN2(grow);
                        }
                        #endregion ==== SL_QD ====
                        break;

                    case "GIA_BAN_NT":
                        grow.Cells["GIA_BAN"].Value = V6BusinessHelper.Vround(CELL_DEC(grow, "GIA_BAN_NT") * txtTyGia.Value, M_ROUND_GIA);
                        if (_maNt == _mMaNt0)
                        {
                            grow.Cells["GIA_BAN"].Value = grow.Cells["GIA_BAN_NT"].Value;
                        }

                        TinhGiaNt21(grow);
                        TinhGiaNt2(grow);
                        chkT_THUE_NT.Checked = false;
                        TinhTienNt2(grow, "GIA_BAN_NT");
                        Tinh_thue_ct(grow);
                        break;
                    case "GIA_NT":
                        TinhTienVon(grow);
                        break;

                    case "GIA_NT21":
                        //public void GiaNt21_V6LostFocus(object sender)
                        //{
                        //    chkT_THUE_NT.Checked = false;
                        //    TinhTienNt2(grow, "GIA_NT21");
                        //    Tinh_thue_ct();
                        //}
                        #region ==== GIA_NT21 ====

                        //TinhTienVon1(_gia_nt21);
                        grow.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(grow.Cells["GIA_NT21"].Value), M_ROUND_NT);
                        grow.Cells["TIEN2"].Value = _maNt == _mMaNt0
                            ? grow.Cells["TIEN_NT2"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(grow.Cells["TIEN_NT2"].Value) * txtTyGia.Value, M_ROUND);
                        //TinhChietKhauChiTiet
                        TinhChietKhauChiTiet_row_XUAT_TIEN_NT2(grow, txtTyGia.Value, false);
                        //TinhGiaVon();
                        grow.Cells["GIA21"].Value = _maNt == _mMaNt0
                            ? grow.Cells["GIA_NT21"].Value
                            : V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(grow.Cells["GIA_NT21"].Value) * txtTyGia.Value), M_ROUND_GIA_NT);
                        //TinhGiaNt2
                        if (ObjectAndString.ObjectToDecimal(grow.Cells["SO_LUONG"].Value) != 0)
                        {
                            grow.Cells["GIA_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(grow.Cells["TIEN_NT2"].Value) / ObjectAndString.ObjectToDecimal(grow.Cells["SO_LUONG"].Value), M_ROUND_GIA_NT);
                            grow.Cells["GIA2"].Value = _maNt == _mMaNt0
                                ? grow.Cells["GIA_NT2"].Value
                                : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(grow.Cells["TIEN2"].Value)
                                    / ObjectAndString.ObjectToDecimal(grow.Cells["SO_LUONG"].Value), M_ROUND_GIA);
                        }
                        TinhVanChuyen_row(grow);
                        TinhGiamGiaCt_row(grow);
                        Tinh_thue_ct_row_XUAT_TIEN2(grow);

                        #endregion ==== GIA_NT21 ====
                        break;
                    case"TIEN_NT":
                        TinhGiaVon(grow);
                        break;
                    case "TIEN_NT2":
                        TienNt2_V6LostFocus(cell, grow);
                        break;
                    case "PT_CKI":
                        TinhChietKhauChiTiet(grow, txtTyGia.Value);
                        Tinh_thue_ct(grow);
                        break;
                    case "CK_NT":
                        TinhChietKhauChiTiet(grow, txtTyGia.Value, true);
                        Tinh_thue_ct(grow);
                        break;
                    case "GG":
                        Tinh_thue_ct(grow);
                        break;
                    case "GG_NT":
                        Tinh_thue_ct(grow);
                        break;

                    case "MA_THUE_I":
                        XuLyThayDoiMaThue_i(grow);
                        Tinh_thue_ct(grow);
                        break;
                    case "THUE_NT":
                        Tinh_TienThue_TheoTienThueNt_row(grow, CELL_DEC(grow, "THUE_NT"), txtTyGia.Value, "THUE", M_ROUND);
                        break;

                    case "TANG":
                        _tang_V6LostFocus(cell, grow);
                        break;
                    case "PX_GIA_DDI":

                        if (cell.Value.ToString().Trim() != "")
                        {
                            dataGridView1.UnLockColumn("GIA_NT");
                            //_gia_nt.Enabled = true;
                            if (chkSuaTien.Checked)
                                dataGridView1.UnLockColumn("TIEN_NT");
                            else dataGridView1.LockColumn("TIEN_NT");
                        }
                        else
                        {
                            //_gia_nt.Enabled = false;
                            //_tienNt.Enabled = false;
                            dataGridView1.LockColumn("GIA_NT");
                            dataGridView1.LockColumn("TIEN_NT");
                        }

                        ;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            //{
            //    //var cell = dataGridView1.CurrentCell;
            //    SaveSelectedCellLocation(dataGridView1, 1);
            //    var flag = cell.OwningColumn.DisplayIndex;
            //    int count = 0;
            //    foreach (DataGridViewColumn item in dataGridView1.Columns.OfType<DataGridViewColumn>().OrderBy(x => x.DisplayIndex))
            //    {
            //        if (count == flag + 1)
            //        {
            //            _cellIndex[1] = item.Index;
            //            _rowIndex[1] = cell.RowIndex;
            //            _celledit_enterkey = true;
            //            break;
            //        }
            //        count++;
            //    }
            //}
            //if (_celledit_enterkey) LoadSelectedCellLocation(dataGridView1, 1);\
            InvokeFormEvent(FormDynamicEvent.CELLENDEDIT2);
            TinhTongThanhToan("CellEndEdit_" + FIELD);
        }

        private DataGridViewRow _edittingRow = null;
        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string FIELD = null;
            try
            {
                var grow = dataGridView1.Rows[e.RowIndex];
                _edittingRow = grow;
                var col = dataGridView1.Columns[e.ColumnIndex];
                _sttRec0 = STR(grow, "STT_REC0");
                FIELD = col.DataPropertyName.ToUpper();

                ShowMainMessage("cell_begin_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        #region ==== SO_LUONG1 ====
                        GetTonRow_A1(grow, dateNgayCT.Value);
                        #endregion ==== SO_LUONG1 ====
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (NotAddEdit) return;
            string FIELD = null;
            
            try
            {
                var grow = dataGridView1.CurrentRow;
                if (grow == null || grow.IsNewRow) return;
                var cell = dataGridView1.CurrentCell;
                var col = cell.OwningColumn;
                FIELD = col.DataPropertyName.ToUpper();

                if (_edittingRow != null && grow != _edittingRow)
                {
                    // Nhảy qua dòng khác.
                    bool validate = ValidateData_Detail_Row(_edittingRow);
                    if (!validate)
                    {
                        DoNothing();
                        _edittingRow = null;
                        return;
                    }
                    else
                    {
                        _edittingRow = null;
                    }
                }

                //var cell_MA_VT = row.Cells["MA_VT"];
                //var cell_SO_LUONG1 = row.Cells["SO_LUONG1"];
                //decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                //decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                //if (HE_SO1T == 0) HE_SO1T = 1;
                //if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;

                //ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "MA_VT":
                        var column_ma_vt = col as V6VvarDataGridViewColumn;

                        var setting = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MAKH2MAVT"));
                        if (setting.Contains(Invoice.Mact) && column_ma_vt != null)
                        {
                            string newFilter = Invoice.GetMaVtFilterByMaKH(txtMaKh.Text, txtMaDVCS.Text);
                            if (string.IsNullOrEmpty(_mavt_default_initfilter))
                            {
                                column_ma_vt.InitFilter = newFilter;
                            }
                            else if (!string.IsNullOrEmpty(newFilter))
                            {
                                column_ma_vt.InitFilter = string.Format("({0}) and ({1})", _mavt_default_initfilter, newFilter);
                            }
                        }
                        
                        break;
                    case "DVT1":
                        var column_dvt = col as V6VvarDataGridViewColumn;
                        if (column_dvt != null)
                        {
                            column_dvt.InitFilter = "ma_vt='" + grow.Cells["MA_VT"].Value.ToString().Trim() + "'"; // Chuyển vào begin_edit mới đúng.????
                        }
                        
                        //_dvt1.ExistRowInTable(true);
                        
                        break;
                    case "SO_LUONG1":
                        
                        if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(FIELD) || Invoice.GRD_READONLY.ContainsStartsWith(FIELD + ":")))
                        {
                            col.ReadOnly = true;
                        }
                        break;

                    case "MA_LO":
                        if (col.ReadOnly) return;
                        var malo_column = col as V6VvarDataGridViewColumn;
                        if (malo_column != null)
                        {
                            
                            //_maLo.SetInitFilter("ma_vt='" + grow.Cells["MA_VT"].Value.ToString().Trim() + "'");

                            var cell_mavt = grow.Cells["MA_VT"];
                            var cell_makhoi = grow.Cells["MA_KHO_I"];
                            malo_column.CheckNotEmpty = IS(cell_mavt, "LO_YN") && IS(cell_makhoi, "LO_YN");
                            malo_column.InitFilter = "ma_vt='" + cell_mavt.Value.ToString().Trim() + "'";
                        }
                        
                        break;
                    case "MA_LNX_I":
                        var ma_lnx_i_column = col as V6VvarDataGridViewColumn;
                        if (ma_lnx_i_column != null)
                        {
                            ma_lnx_i_column.InitFilter = "LOAI = 'X'";
                        }
                        break;
                    case "TK_DT":
                        var tk_dt_column = col as V6VvarDataGridViewColumn;
                        if (tk_dt_column != null)
                        {
                            tk_dt_column.InitFilter = "Loai_tk = 1";
                            tk_dt_column.FilterStart = true;
                        }
                        break;
                    case "TK_CKI":
                        var tk_cki_column = col as V6VvarDataGridViewColumn;
                        if (tk_cki_column != null)
                        {
                            tk_cki_column.InitFilter = "Loai_tk = 1";
                            tk_cki_column.FilterStart = true;
                        }
                        break;
                    case "TK_VT":
                        var tk_vt_column = col as V6VvarDataGridViewColumn;
                        if (tk_vt_column != null)
                        {
                            tk_vt_column.InitFilter = "Loai_tk = 1";
                            tk_vt_column.FilterStart = true;
                        }
                        break;
                    case "TK_GV":
                        var tk_gv_column = col as V6VvarDataGridViewColumn;
                        if (tk_gv_column != null)
                        {
                            tk_gv_column.InitFilter = "Loai_tk = 1";
                            tk_gv_column.FilterStart = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException("CellChanged", ex);
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DoNothing();
            if (NotAddEdit) return;
            string FIELD = null;
            try
            {
                var grow = dataGridView1.CurrentRow;
                if (grow == null || grow.IsNewRow) return;
                var cell = dataGridView1.CurrentCell;
                var col = cell.OwningColumn;
                FIELD = col.DataPropertyName.ToUpper();

                //var cell_MA_VT = row.Cells["MA_VT"];
                //var cell_SO_LUONG1 = row.Cells["SO_LUONG1"];
                //decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                //decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                //if (HE_SO1T == 0) HE_SO1T = 1;
                //if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;

                //ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "MA_VT":
                        if (string.IsNullOrEmpty(STR(cell))) goto End;
                        var column_ma_lo = dataGridView1.Columns["MA_LO"];
                        if (column_ma_lo == null) goto End;
                        
                        if (IS(cell, "LO_YN"))
                        {
                            column_ma_lo.ReadOnly = false;
                        }
                        else
                        {
                            column_ma_lo.ReadOnly = true;
                        }
                        GetTon13(dataGridView1.CurrentRow);
                        
                        break;
                    case "DVT1":
                        Dvt1_V6LostFocusNoChange(cell, grow);
                        break;
                    case "MA_LO":
                        XuLyLayThongTinKhiChonMaLo(grow);
                        break;
                }
                End: ;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("CellLeave", ex);
            }
        }

        decimal CELL_DEC(DataGridViewRow grow, string name)
        {
            if (grow == null) return 0;
            return ObjectAndString.ObjectToDecimal(grow.Cells[name].Value);
        }
        
        /// <summary>
        /// Nếu không có cell trả về null.
        /// </summary>
        /// <param name="grow"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        string CELL_STRING_NULL(DataGridViewRow grow, string name)
        {
            if (grow == null || !grow.DataGridView.Columns.Contains(name)) return null;
            return ObjectAndString.ObjectToString(grow.Cells[name].Value).Trim();
        }

        IDictionary<string, object> CELL_VVAR_DATA(DataGridViewCell cell)
        {
            var cell_tagData = cell.Tag as IDictionary<string, object>;
            if (cell_tagData != null && cell_tagData.ContainsKey("VVAR_DATA"))
            {
                var vvar_data = cell_tagData["VVAR_DATA"] as IDictionary<string, object>;
                return vvar_data;
            }
            else if (cell.Value.ToString().Trim() != "")
            {
                var col = cell.OwningColumn as V6VvarDataGridViewColumn;
                if (col != null)
                {
                    V6VvarTextBox vvarTextBox = new V6VvarTextBox();
                    vvarTextBox.VVar = col.Vvar;
                    vvarTextBox.Text = cell.Value.ToString().Trim();
                    if (vvarTextBox.Data != null)
                    {
                        if (cell_tagData == null) cell_tagData = new Dictionary<string, object>();
                        var vvar_data = vvarTextBox.Data.ToDataDictionary();
                        cell_tagData["VVAR_DATA"] = vvar_data;
                        cell.Tag = cell_tagData;
                        return vvar_data;
                    }
                }
                
            }

            return null;
        }

        bool IS(DataGridViewCell cell, string KEY)
        {
            var data = CELL_VVAR_DATA(cell);
            if (data.ContainsKey(KEY)) return ObjectAndString.ObjectToBool(data[KEY]);
            return false;
        }
        bool IS_NOT(DataGridViewCell cell, string KEY)
        {
            return !IS(cell, KEY);
        }
        object GetCellTag(DataGridViewCell cell, string KEY)
        {
            if (cell.Tag is IDictionary<string, object>)
            {
                var tagData =  ((IDictionary<string, object>)cell.Tag);
                if (tagData.ContainsKey(KEY)) return tagData[KEY];
            }
            //var data = CELL_VVAR_DATA(cell);
            //if (data.ContainsKey(KEY))
            //    return data[KEY];

            return null;
        }

        /// <summary>
        /// Gán công thức tính toán cho GridView theo các trường, Các công thức được tham chiếu từ các hàm xử lý (vd so_luong1.V6lostfocus...).
        /// </summary>
        private void SetGridViewFomular()
        {
            return;//Dùng sự kiện cell_endedit để viết lại sự kiện.
            #region ==== SO_LUONG1 ====
            //Ex:
            //--dataGridView1.ThemCongThuc("SO_LUONG1", "SO_LUONG=SO_LUONG1*HE SO1");
            //--dataGridView1.ThemCongThuc("SO_LUONG1", "THANH_TIEN=SO_LUONG*DON_GIA");


            #endregion ==== SO_LUONG1 ====

            //--dataGridView1.ThemCongThuc("DON_GIA", "THANH_TIEN=SO_LUONG*DON_GIA");
        }

        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
        }

        private void FormatGridView()
        {
            var f = dataGridView1.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            f = dataGridView1.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            
            f = dataGridView1.Columns["GIA2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView1.Columns["GIA21"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView1.Columns["GIA"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView1.Columns["GIA_NT2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["GIA_NT21"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["GIA_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["TIEN2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView1.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView1.Columns["TIEN_NT2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView1.Columns["CK_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView1.Columns["CK"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }

            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_AD, Invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, Invoice.Config2.GRDS_V1, Invoice.Config2.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config2.GRDHV_V1 : Invoice.Config2.GRDHE_V1);
            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView3, Invoice.Config3.GRDS_V1, Invoice.Config3.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config3.GRDHV_V1 : Invoice.Config3.GRDHE_V1);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView2, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3ChiPhi, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView4, Invoice.Mact);
        }
        #endregion datagridview
        
        #region ==== Tính toán hóa đơn ====
        
        public void TinhTongValues()
        {
            txtTongSoLuong1.Value = TinhTong(AD, "SO_LUONG1");
            txtTongSoLuong.Value = TinhTong(AD, "SO_LUONG");

            var tTienNt2 = TinhTong(AD, "TIEN_NT2");
            txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);

            var tTien2 = TinhTong(AD, "TIEN2");
            txtTongTien2.Value = V6BusinessHelper.Vround(tTien2, M_ROUND);

            //{ Tuanmh 01/07/2016
            if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
            {
                var t_gg_nt = TinhTong(AD, "GG_NT");
                var t_gg = TinhTong(AD, "GG");
                txtTongGiamNt.Value = V6BusinessHelper.Vround(t_gg_nt, M_ROUND_NT);
                txtTongGiam.Value = V6BusinessHelper.Vround(t_gg, M_ROUND);
            }

            if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
            {
                var t_vc_nt = TinhTong(AD, "TIEN_VC_NT");
                var t_vc = TinhTong(AD, "TIEN_VC");
                TxtT_TIENVCNT.Value = V6BusinessHelper.Vround(t_vc_nt, M_ROUND_NT);
                TxtT_TIENVC.Value = V6BusinessHelper.Vround(t_vc, M_ROUND);
            }

        }
        public void TinhChietKhau()
        {
            try
            {
                var tTienNt2 = TinhTong(AD, "TIEN_NT2");
                var tyGia = txtTyGia.Value;
                var t_tien_nt2 = txtTongTienNt2.Value;
                txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);
                decimal t_ck_nt = 0,t_ck;



                if (chkLoaiChietKhau.Checked)//==1
                {
                    //Chiết khấu chung, chia theo phần trăm
                    //Tính phần trăm chiết khấu. Nếu check sua_ptck thì lấy luôn
                    //Nếu nhập tiền chiết khấu
                    if (chkSuaPtck.Checked || (!chkT_CK_NT.Checked && txtPtCk.Value > 0))
                    {
                        var ptck = txtPtCk.Value;
                        txtTongCkNt.ReadOnly = false;
                        //
                        t_ck_nt = V6BusinessHelper.Vround(ptck*tTienNt2/100, M_ROUND_NT);
                        t_ck = V6BusinessHelper.Vround(t_ck_nt*tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;

                        txtTongCkNt.Value = t_ck_nt;
                        txtTongCk.Value = t_ck;
                    }

                    else if (chkT_CK_NT.Checked)
                    {
                        t_ck_nt = txtTongCkNt.Value;
                        t_ck = V6BusinessHelper.Vround(t_ck_nt*tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;
                        txtTongCk.Value = t_ck;



                    }
                    //tính chiết khấu cho mỗi chi tiết
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt2 != 0)
                        {
                            var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt2"]);
                            var ck_nt = V6BusinessHelper.Vround(tien_nt2 * t_ck_nt / t_tien_nt2, M_ROUND_NT);
                            var ck = V6BusinessHelper.Vround(ck_nt * tyGia, M_ROUND);

                            if (_maNt == _mMaNt0)
                                ck = ck_nt;

                            //gán lại ck_nt
                            if (AD.Columns.Contains("CK_NT")) AD.Rows[i]["CK_NT"] = ck_nt;
                            if (AD.Columns.Contains("CK")) AD.Rows[i]["CK"] = ck;
                            
                        }
                        else
                        {
                            var ck_nt =0m;
                            var ck = 0m;

                            if (_maNt == _mMaNt0)
                                ck = ck_nt;

                            //gán lại ck_nt
                            if (AD.Columns.Contains("CK_NT")) AD.Rows[i]["CK_NT"] = ck_nt;
                            if (AD.Columns.Contains("CK")) AD.Rows[i]["CK"] = ck;
                        }
                    }
                }
                else
                {
                    //Chiết khấu RIÊNG, CỘNG DỒN
                    t_ck_nt = TinhTong(AD, "CK_NT");
                    t_ck = TinhTong(AD, "CK");

                    txtTongCkNt.Value = V6BusinessHelper.Vround(t_ck_nt, M_ROUND_NT);
                    txtTongCk.Value = V6BusinessHelper.Vround(t_ck, M_ROUND);


                    //t_ck = V6BusinessHelper.Vround(t_ck_nt * tyGia, M_ROUND);
                    //if (_maNt == _mMaNt0)
                    //    t_ck = t_ck_nt;
                    //txtTongCk.Value = t_ck;
                    
                    txtPtCk.ReadOnly = true;
                    txtTongCkNt.ReadOnly = true;
                    txtTongCk.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiamGia()
        {
            try
            {
                if (!"123".Contains(V6Options.GetValue("M_GIAVC_GIAGIAM_CT")))
                {
                    var tTienNt2 = TinhTong(AD, "TIEN_NT2");
                    var tyGia = txtTyGia.Value;
                    var t_tien_nt2 = txtTongTienNt2.Value;
                    txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);
                    decimal t_gg_nt = 0, t_gg;
                    
                    //Giảm giá chung, chia theo phần trăm
                    {
                        t_gg_nt = txtTongGiamNt.Value;
                        t_gg = V6BusinessHelper.Vround(t_gg_nt*tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_gg = t_gg_nt;
                        txtTongGiam.Value = t_gg;
                    }

                    //tính chiết khấu cho mỗi chi tiết
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt2 != 0)
                        {
                            var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt2"]);
                            var gg_nt = V6BusinessHelper.Vround(tien_nt2*t_gg_nt/t_tien_nt2, M_ROUND_NT);
                            var gg = V6BusinessHelper.Vround(gg_nt*tyGia, M_ROUND);

                            if (_maNt == _mMaNt0)
                                gg = gg_nt;

                            //gán lại ck_nt
                            if (AD.Columns.Contains("GG_NT")) AD.Rows[i]["GG_NT"] = gg_nt;
                            if (AD.Columns.Contains("GG")) AD.Rows[i]["GG"] = gg;
                        }
                        else
                        {
                            var gg_nt = 0m;
                            var gg = 0m;

                            if (_maNt == _mMaNt0)
                                gg = gg_nt;

                            //gán lại ck_nt
                            if (AD.Columns.Contains("GG_NT")) AD.Rows[i]["GG_NT"] = gg_nt;
                            if (AD.Columns.Contains("GG")) AD.Rows[i]["GG"] = gg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhGiamGia " + _sttRec, ex);
            }
        }

        public void TinhThue()
        {
            //Tính tiền thuế theo thuế suất
            decimal thue_suat;
            decimal t_thue_nt;
            decimal t_thue;

            var ty_gia = txtTyGia.Value;
            var t_tien_nt2 = txtTongTienNt2.Value;
            var t_gg_nt = txtTongGiamNt.Value;
            var t_vc_nt = TxtT_TIENVCNT.Value;
            var t_ck_nt = txtTongCkNt.Value;

            var t_tien_truocthue = t_tien_nt2 - t_gg_nt - t_ck_nt + t_vc_nt;

            if (chkT_THUE_NT.Checked)//Tiền thuế gõ tự do
            {
                t_thue_nt = txtTongThueNt.Value;
                t_thue = V6BusinessHelper.Vround(t_thue_nt * ty_gia, M_ROUND);
                
                if (_maNt == _mMaNt0)
                    t_thue = t_thue_nt;
            }
            else
            {
                thue_suat = txtThueSuat.Value;
                //tiền thuế = (tiền hàng - tiền giảm - chiết khấu) * thuế suất
                t_thue_nt = t_tien_truocthue * thue_suat / 100;
                t_thue_nt = V6BusinessHelper.Vround(t_thue_nt, M_ROUND_NT);
                //sV("T_THUE_NT", t_thue_nt);
                
                t_thue = V6BusinessHelper.Vround(t_thue_nt * ty_gia, M_ROUND);
                if (_maNt == _mMaNt0)
                    t_thue = t_thue_nt;

            }
            // Tuanmh 25/07/2017
            txtTongThueNt.Value = t_thue_nt;
            txtTongThue.Value = t_thue;

            //tính thuế riêng cho từng chi tiết
            //tính phần trăm giá trị của từng chi tiết trên tổng tiền hàng rồi nhân với tổng thuế sẽ ra thuế 
            var t_thue_nt_check = 0m;
            var t_thue_check = 0m;
            var index = -1;
            for (var i = 0; i < AD.Rows.Count; i++)
            {
                if (t_tien_nt2 != 0)
                {
                    var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN_NT2"]);
                    var thue_nt = V6BusinessHelper.Vround(tien_nt2 * t_thue_nt / t_tien_nt2, M_ROUND_NT);
                    t_thue_nt_check = t_thue_nt_check + thue_nt;

                    var thue = V6BusinessHelper.Vround(thue_nt * ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thue = thue_nt;
                    t_thue_check += thue;

                    if (thue_nt != 0 && index == -1)
                        index = i;

                    AD.Rows[i]["Thue_nt"] = thue_nt;
                    AD.Rows[i]["Thue"] = thue;
                }
                else
                {
                    var thue_nt = 0m;
                    var thue = 0m;

                    t_thue_nt_check = t_thue_nt_check + thue_nt;
                    

                    if (_maNt == _mMaNt0)
                        thue = thue_nt;
                    t_thue_check += thue;

                    if (thue_nt != 0 && index == -1)
                        index = i;

                    AD.Rows[i]["Thue_nt"] = thue_nt;
                    AD.Rows[i]["Thue"] = thue;
                }
            }
            
            // Xu ly chenh lech
            // Tìm dòng có số tiền
            if (index != -1)
            {
                decimal _thue_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Thue_nt"]) + (t_thue_nt - t_thue_nt_check);
                AD.Rows[index]["Thue_nt"] = _thue_nt;

                decimal _thue = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Thue"]) + (t_thue - t_thue_check);
                AD.Rows[index]["Thue"] = _thue;
            }
        }

        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                if (NotAddEdit) return;
                //Tính tổng thanh toán.//Tính ck xong tính ttt lại.
            
                HienThiTongSoDong(lblTongSoDong);
                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt2, T_CK_NT, PT_CK
                TinhGiamGia();

                if (M_SOA_MULTI_VAT == "1")
                {
                    TinhTongThue_ct();
                }
                else
                {
                    TinhThue();
                }

                if (string.IsNullOrEmpty(_mMaNt0)) return;
                
                var t_tien_nt2 = txtTongTienNt2.Value;
                var t_gg_nt = txtTongGiamNt.Value;
                var t_ck_nt = txtTongCkNt.Value;
                var t_thue_nt = txtTongThueNt.Value;
                var t_vc_nt = TxtT_TIENVCNT.Value;
                
                var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt + t_thue_nt + t_vc_nt;
                txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

                var t_tt = txtTongTien2.Value - txtTongGiam.Value - txtTongCk.Value + txtTongThue.Value;
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                txtConLai.Value = t_tt_nt - txtSL_UD1.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        public void TinhTongThue_ct()
        {
            var t_thue_nt = TinhTong(AD, "THUE_NT");
            var t_thue = TinhTong(AD, "THUE");

            txtTongThueNt.Value = V6BusinessHelper.Vround(t_thue_nt, M_ROUND_NT);
            txtTongThue.Value = V6BusinessHelper.Vround(t_thue, M_ROUND);
            
            txtMa_thue.ReadOnly = true;
            txtTongThueNt.ReadOnly = true;
            txtTongThue.ReadOnly = true;
        }

        #endregion tính toán

        #region ==== AM Methods ====
        private void LoadAll()
        {
            AM = Invoice.SearchAM("1=0", "1=0", "", "", "", null);//Làm AM khác null
            EnableControls();
            GetSoPhieuInit();
            LoadAlnt(cboMaNt);
            LoadAlpost(cboKieuPost);
            LoadAlimtype(cboChuyenData);
            GetM_ma_nt0();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(Invoice.Mact, tabKhac, this);
            Ready();
        }

        private void GetM_ma_nt0()
        {
            _mMaNt0 = V6Options.M_MA_NT0;
            //cboMaNt.SelectedValue = _mMaNt0;
            panelVND.Visible = false;

        }

        private void GetTyGiaDefault()
        {
            var getMant = Invoice.Alct["ma_nt"].ToString().Trim();
            if (!string.IsNullOrEmpty(getMant))
            {
                cboMaNt.SelectedValue = getMant;
            }
            else
            {
                cboMaNt.SelectedValue = _mMaNt0;
            }
        }
        private void GetTyGia()
        {
            txtTyGia.Value = Invoice.GetTyGia(_maNt, dateNgayCT.Date);
        }

        private void GetDefault_Other()
        {
            try
            {
                txtMa_ct.Text = Invoice.Mact;
                dateNgayCT.SetValue(V6Setting.M_SV_DATE);
                dateNgayLCT.SetValue(V6Setting.M_SV_DATE);
                //Tuanmh 25/01/2016- Ma_dvcs
                if (V6Login.MadvcsCount >= 1)
                {
                    if (V6Login.Madvcs != "")
                    {
                        txtMaDVCS.Text = V6Login.Madvcs;
                        txtMaDVCS.ExistRowInTable();
                    }
                }

                //M_Ma_nk
                Txtma_nk.Text = Invoice.Alct["M_MA_NK"].ToString().Trim();
                //
                txtManx.Text = Invoice.Alct["TK_NO"].ToString().Trim();
                try
                {
                    cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();
                }
                catch
                {
                    // ignored
                }

                if (AM_old != null)
                {
                    txtMa_sonb.Text = AM_old["Ma_sonb"].ToString().Trim();
                    if (txtSoPhieu.Text.Trim() == "")
                        txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text, dateNgayCT.Date);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// _maKhoI.SetInitFilter
        /// </summary>
        private void XuLyThayDoiMaDVCS()
        {
            try
            {
                string filter = V6Login.GetFilterKhoByDVCS(txtMaDVCS.Text.Trim());
                SetColumnInitFilter(dataGridView1, "MA_KHO_I", filter);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaDVCS " + _sttRec, ex);
            }
        }

        

        private void XuLyThayDoiMaNt()
        {
            try
            {
                if (!_ready0) return;

                var viewColumn = dataGridView1.Columns["GIA_NT21"];
                if (viewColumn != null)
                    viewColumn.HeaderText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _maNt;
                var column = dataGridView1.Columns["TIEN_NT2"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _maNt;
                column = dataGridView1.Columns["THUE_NT"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thuế " : "Tax ") + _maNt;

                viewColumn = dataGridView1.Columns["GIA21"];
                if (viewColumn != null)
                    viewColumn.HeaderText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                column = dataGridView1.Columns["TIEN2"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _mMaNt0;
                column = dataGridView1.Columns["THUE"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thuế " : "Tax ") + _mMaNt0;

                if (_maNt.ToUpper() != _mMaNt0.ToUpper())
                {

                    M_ROUND_NT = V6Setting.RoundTienNt;
                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA_NT = V6Setting.RoundGiaNt;
                    M_ROUND_GIA = V6Setting.RoundGia;


                    txtTyGia.Enabled = true;
                    SetDetailControlVisible(detailControlList1, true, "GIA", "GIA2", "GIA21", "TIEN", "TIEN2", "THUE_NT", "THUE", "CK", "GG", "TIEN_VC");
                    panelVND.Visible = true;
                    

                    //var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA21");
                    //if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["GIA21"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["TIEN2"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    dataGridViewColumn = dataGridView1.Columns["THUE"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    
                    dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["TIEN"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["GIA2"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    if (chkLoaiChietKhau.Checked)
                    {
                        dataGridViewColumn = dataGridView1.Columns["CK"];
                        if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    }
                }
                else
                {

                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA = V6Setting.RoundGia;
                    M_ROUND_NT = M_ROUND;
                    M_ROUND_GIA_NT = M_ROUND_GIA;


                    txtTyGia.Enabled = false;
                    txtTyGia.Value = 1;
                    SetDetailControlVisible(detailControlList1, false, "GIA", "GIA01", "GIA2", "GIA21", "TIEN", "TIEN0", "TIEN2", "THUE", "CK", "GG", "TIEN_VC");
                    panelVND.Visible = false;
                    
                    var dataGridViewColumn = dataGridView1.Columns["GIA21"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    dataGridViewColumn = dataGridView1.Columns["TIEN2"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    dataGridViewColumn = dataGridView1.Columns["THUE"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    
                    dataGridViewColumn = dataGridView1.Columns["GIA2"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    dataGridViewColumn = dataGridView1.Columns["TIEN"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    if (chkLoaiChietKhau.Checked)
                    {
                        dataGridViewColumn = dataGridView1.Columns["CK"];
                        if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    }
                }

                FormatNumberControl();
                FormatNumberGridView();
                //detail1.FixControlsLocation();
                //detail2.FixControlsLocation();
                //detail3.FixControlsLocation();

                TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaNt " + _sttRec, ex);
            }
        }

        private void FormatNumberControl()
        {
            try
            {
                var decimalTienNt = _maNt == _mMaNt0 ? V6Options.M_IP_TIEN : V6Options.M_IP_TIEN_NT;
                //AM
                
                foreach (Control control in panelNT.Controls)
                {
                    V6NumberTextBox textBox = control as V6NumberTextBox;
                    if(textBox != null)
                        textBox.DecimalPlaces = decimalTienNt;
                }
                foreach (Control control in panelVND.Controls)
                {
                    V6NumberTextBox textBox = control as V6NumberTextBox;
                    if (textBox != null)
                        textBox.DecimalPlaces = V6Options.M_IP_TIEN;
                }

                //_tienNt.DecimalPlaces = decimalTienNt;
                var tien_nt_column = dataGridView1.Columns["TIEN_NT"];
                if (tien_nt_column != null)
                {
                    tien_nt_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
                //_tienNt2.DecimalPlaces = decimalTienNt;
                var tien_nt2_column = dataGridView1.Columns["TIEN_NT2"];
                if (tien_nt2_column != null)
                {
                    tien_nt2_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
                //_thue_nt.DecimalPlaces = decimalTienNt;
                var thue_nt_column = dataGridView1.Columns["THUE_NT"];
                if (thue_nt_column != null)
                {
                    thue_nt_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
                //_ggNt.DecimalPlaces = decimalTienNt;
                var gg_nt_column = dataGridView1.Columns["GG_NT"];
                if (gg_nt_column != null)
                {
                    gg_nt_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
                //_tien_vcNt.DecimalPlaces = decimalTienNt;
                var tien_vcnt_column = dataGridView1.Columns["TIEN_VCNT"];
                if (tien_vcnt_column != null)
                {
                    tien_vcnt_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
                //_ckNt.DecimalPlaces = decimalTienNt;
                var ck_nt_column = dataGridView1.Columns["CK_NT"];
                if (ck_nt_column != null)
                {
                    ck_nt_column.DefaultCellStyle.Format = "N" + decimalTienNt;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberControl " + _sttRec, ex);
            }
        }

        private void FormatNumberGridView()
        {
            try
            {
                var decimalPlaces = _maNt == _mMaNt0 ? V6Options.M_IP_TIEN : V6Options.M_IP_TIEN_NT;
                var column = dataGridView1.Columns["Ps_co_nt"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }

                decimalPlaces = _maNt == _mMaNt0 ? V6Options.M_IP_TIEN : V6Options.M_IP_TIEN_NT;
                column = dataGridView1.Columns["TIEN_NT2"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                column = dataGridView1.Columns["TIEN_NT21"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }

                decimalPlaces = _maNt == _mMaNt0 ? V6Options.M_IP_GIA : V6Options.M_IP_GIA_NT;
                column = dataGridView1.Columns["GIA_NT2"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                column = dataGridView1.Columns["GIA_NT21"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberGridView " + _sttRec, ex);
            }
        }

        private void XuLyThayDoiMaGia()
        {
            TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
        }

        private void XuLyThayDoiMaThue()
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + txtMa_thue.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    txtTkThueCo.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                    txtThueSuat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    txtTkThueNo.Text = txtManx.Text;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue " + _sttRec, ex);
            }
            TinhTongThanhToan("XuLyThayDoiMaThue");
        }

        private void XuLyThayDoiMaThue_i(DataGridViewRow grow)
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + STR(grow, "MA_THUE_I") + "'");
                if (alThue.TotalRows > 0)
                {
                    SetCellValue(grow.Cells["TK_THUE_I"], alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim());
                    SetCellValue(grow.Cells["THUE_SUAT_I"], ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue_i " + _sttRec, ex);
            }
            //TinhTongThanhToan("XuLyThayDoiMaThue_i");
        }

        public void Tinh_thue_ct(DataGridViewRow grow)
        {
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_SOA_MULTI_VAT = " + M_SOA_MULTI_VAT);
            if (M_SOA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat_Row(DEC(grow.Cells["THUE_SUAT_I"]),
                    DEC(grow.Cells["TIEN_NT2"]) - DEC(grow.Cells["CK_NT"]) - DEC(grow.Cells["GG_NT"]),
                    DEC(grow.Cells["TIEN2"]) - DEC(grow.Cells["CK"]) - DEC(grow.Cells["GG"]),
                    grow);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tinh thue ct M_SOA_MULTY_VAT = 1.");
            }
        }

        /// <summary>
        /// Tinh_TienThueNtVaTienThue_TheoThueSuat_Row có trừ ck hoặc gg bởi M_POA_VAT_WITH_CK_GG, THUE_SUAT_I TIEN_NT2 TIEN2
        /// </summary>
        /// <param name="row"></param>
        public void Tinh_thue_ct_row_XUAT_TIEN2(DataGridViewRow row)
        {
            if (M_SOA_MULTI_VAT == "1")
            {
                decimal ck_nt_value = 0, ck_value = 0, gg_nt_value = 0, gg_value = 0;
                
                ck_value = ObjectAndString.ObjectToDecimal(row.Cells["CK"].Value);
                ck_nt_value = ObjectAndString.ObjectToDecimal(row.Cells["CK_NT"].Value);
                gg_value = ObjectAndString.ObjectToDecimal(row.Cells["GG"].Value);
                gg_nt_value = ObjectAndString.ObjectToDecimal(row.Cells["GG_NT"].Value);
                
                Tinh_TienThueNtVaTienThue_TheoThueSuat_Row(ObjectAndString.ObjectToDecimal(row.Cells["THUE_SUAT_I"].Value),
                    ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT2"].Value) - ck_nt_value - gg_nt_value,
                    ObjectAndString.ObjectToDecimal(row.Cells["TIEN2"].Value) - ck_value - gg_value, row);
            }
        }
        
        /// <summary>
        /// Lấy dữ liệu AD dựa vào rec, tạo 1 copy gán vào AD81
        /// </summary>
        /// <param name="sttRec"></param>
        public void LoadAD(string sttRec )
        {
            if (ADTables == null) ADTables = new SortedDictionary<string, DataTable>();
            if (ADTables.ContainsKey(sttRec))
            {
                AD = ADTables[sttRec].Copy();
            }
            else
            {
                try
                {
                    ADTables[sttRec] = Invoice.LoadAD(sttRec);
                    AD = ADTables[sttRec].Copy();
                }
                catch
                {
                    ADTables[sttRec] = Invoice.LoadAD(sttRec);
                    AD = ADTables[sttRec].Copy();
                }
            }
        }


        public override void ShowParentMessage(string message)
        {
            try
            {
                var parent = Parent.Parent;
                for (int i = 0; i < 5; i++)
                {
                    if (parent is ChungTuChungContainer)
                    {
                        ((ChungTuChungContainer)parent)
                            .ShowMessage(message);
                        return;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
        
        #region ==== View invoice ====

        /// <summary>
        /// Hiển thị chứng từ theo vị trí. dữ liệu đã tải trong AM
        /// </summary>
        /// <param name="index">Vị trí cần hiển thị</param>
        public void ViewInvoice(int index)
        {
            if (AM != null && AM.Rows.Count > 0)
            {
                if (index < 0 || index >= AM.Rows.Count)
                {
                    index = 0;
                }

                if (index >= 0 && index < AM.Rows.Count)
                {
                    ResetForm();
                    _sttRec = AM.Rows[index]["Stt_rec"].ToString().Trim();
                    CurrentIndex = index;
                    LoadAD(_sttRec);
                    EnableNavigationButtons();
                    ViewInvoice();
                }
            }
        }

        /// <summary>
        /// Load lại data theo stt_rec ???
        /// </summary>
        /// <param name="sttrec"></param>
        /// <param name="mode">Mode hiện tại hoặc trước đó. Không phải mode sẽ trở thành.</param>
        public override void ViewInvoice(string sttrec, V6Mode mode)
        {
            try
            {
                Mode = V6Mode.View;
                
                //Co 2 truong hop them moi roi view va sua roi view
                _sttRec = sttrec;
                DataTable loadAM = null;
                if (string.IsNullOrEmpty(_sttRec))
                {
                    loadAM = Invoice.SearchAM("1=0", "1=0", "", "", "", null);
                }
                else
                {
                    loadAM = Invoice.SearchAM("", "Stt_rec='" + _sttRec + "'", "", "", "", null);
                }

                if (loadAM.Rows.Count == 1)
                {
                    var loadRow = loadAM.Rows[0];
                    if (_timForm != null && !_timForm.IsDisposed)
                        _timForm.UpdateAM(_sttRec, loadRow.ToDataDictionary(), V6Mode.Update);

                    if (mode == V6Mode.Edit)
                    {
                        var currentRow = AM.Rows[CurrentIndex];
                        for (int i = 0; i < AM.Columns.Count; i++)
                        {
                            currentRow[i] = loadRow[i];
                        }
                        ViewInvoice(CurrentIndex);
                    }
                    else if (mode == V6Mode.Add)
                    {
                        var newRow = AM.NewRow();
                        for (int i = 0; i < AM.Columns.Count; i++)
                        {
                            newRow[i] = loadRow[i];
                        }
                        AM.Rows.Add(newRow);
                        CurrentIndex = AM.Rows.Count - 1;
                        ViewInvoice(CurrentIndex);
                    }
                    else
                    {
                        var index = 0;
                        foreach (DataRow row in AM.Rows)
                        {
                            var rowrec = row["Stt_rec"].ToString().Trim();
                            if (rowrec == sttrec)
                            {
                                for (int i = 0; i < AM.Columns.Count; i++)
                                {
                                    row[i] = loadRow[i];
                                }
                                ViewInvoice(index);
                                return;
                            }
                            else index++;
                        }
                        //Thêm vào dòng mới cho AM
                        var newRow = AM.NewRow();
                        for (int i = 0; i < AM.Columns.Count; i++)
                        {
                            newRow[i] = loadRow[i];
                        }
                        AM.Rows.Add(newRow);
                        CurrentIndex = AM.Rows.Count - 1;
                        ViewInvoice(CurrentIndex);
                    }
                }
                OnAmChanged(AM);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Hiển thị hóa đơn đã tải với CurrentIndex
        /// Cần set trước AD81 cho đúng với index
        /// </summary>
        private void ViewInvoice()
        {
            try
            {
                Mode = V6Mode.View;
                V6ControlFormHelper.SetFormDataRow(this, AM.Rows[CurrentIndex]);
                txtMaDVCS.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");

                XuLyThayDoiMaDVCS();
                SetGridViewData();
                XuLyThayDoiMaNt();
                Mode = V6Mode.View;

                FormatNumberControl();
                FormatNumberGridView();
                FixValues();
                LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);

                OnInvoiceChanged(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void FixValues()
        {
            try
            {
                txtConLai.Value = txtTongThanhToanNt.Value - txtSL_UD1.Value;
                txtConLai.DecimalPlaces = txtTongThanhToanNt.DecimalPlaces;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "FixValues " + _sttRec, ex);
            }
        }
        #endregion view invoice

        #region ==== Add Thread ====
        public IDictionary<string, object> readyDataAM;
        public List<IDictionary<string, object>> readyDataAD;
        private string addErrorMessage = "";

        private void DoAddThread()
        {
            ReadyForAdd();
            Timer checkAdd = new Timer();
            checkAdd.Interval = 500;
            checkAdd.Tick += checkAdd_Tick;
            _AED_Running = true;
            _AED_Success = false;
            string inv = "";
            inv += InvokeFormEvent(FormDynamicEvent.BEFOREADD);
            inv += InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
            V6Tag invTag = new V6Tag(inv);
            if (invTag.Cancel)
            {
                this.ShowWarningMessage(invTag.DescriptionLang(V6Setting.IsVietnamese));
                Mode = V6Mode.Add;
                _AED_Running = false;
                return;
            }

            new Thread(DoAdd)
            {
                IsBackground = true
            }
            .Start();
            
            checkAdd.Start();
        }
        
        void checkAdd_Tick(object sender, EventArgs e)
        {
            if (!_AED_Running)
            {
                ((Timer)sender).Stop();

                if (_AED_Success)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.AddSuccess);
                    ShowParentMessage(V6Text.AddSuccess);
                    ViewInvoice(_sttRec, V6Mode.Add);
                    btnMoi.Focus();
                    All_Objects["mode"] = V6Mode.Add;
                    All_Objects["STT_REC"] = _sttRec;
                    All_Objects["MA_CT"] = Invoice.Mact;
                    All_Objects["MA_NT"] = MA_NT;
                    All_Objects["MA_NX"] = txtManx.Text;
                    All_Objects["LOAI_CK"] = chkLoaiChietKhau.Checked ? "1" : "0";
                    All_Objects["MODE"] = "M";
                    All_Objects["KIEU_POST"] = cboKieuPost.SelectedValue;
                    //All_Objects["AP_GIA"] = apgia;
                    All_Objects["USER_ID"] = V6Login.UserId;
                    //All_Objects["SAVE_VOUCHER"] = _sttRec;
                    InvokeFormEvent(FormDynamicEvent.AFTERADDSUCCESS);
                    InvokeFormEvent(FormDynamicEvent.AFTERSAVESUCCESS);

                    if (Invoice.SaveMode == "1")
                    {
                        btnMoi.PerformClick();
                        ShowParentMessage(V6Text.AddSuccess + ". " + V6Text.CreateNew);
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.AddFail + ": " + addErrorMessage);
                    ShowParentMessage(V6Text.AddFail + ": " + addErrorMessage);
                    Mode = V6Mode.Add;
                }

                ((Timer)sender).Dispose();
                if (_print_flag)
                {
                    
                    _print_flag = false;
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
            }
        }

        
        private void ReadyForAdd()
        {
            try
            {
                readyDataAD = dataGridView1.GetData(_sttRec);// GetAdList();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void DoAdd()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                
                if (Invoice.InsertInvoice(readyDataAM, readyDataAD))
                {
                    _AED_Success = true;
                    if (Invoice.IS_AM2TH(readyDataAM))
                    {
                        DoAdd2_TH_Thread();
                    }
                }
                else
                {
                    _AED_Success = false;
                    //addErrorMessage = V6Text.Text("ADD0");
                    addErrorMessage = Invoice.V6Message;
                    Invoice.PostErrorLog(_sttRec, "M");
                }
            }
            catch (Exception ex)
            {
                _AED_Success = false;
                addErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "M " + _sttRec, ex);
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            if (_print_flag)
                Thread.Sleep(2000);
            _AED_Running = false;
        }

        private void DoAdd2_TH_Thread()
        {
            try
            {
                Thread add2_TH = new Thread(DoAdd2_TH);
                add2_TH.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void DoAdd2_TH()
        {
            try
            {
                Invoice.InsertInvoice2_TH(readyDataAM, readyDataAD);
            }
            catch (Exception ex2_TH)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex2_TH);
            }
        }
#endregion add

        #region ==== Edit Thread ====
        private string editErrorMessage = "";

        private void DoEditThread()
        {
            ReadyForEdit();
            Timer checkEdit = new Timer();
            checkEdit.Interval = 500;
            checkEdit.Tick += checkEdit_Tick;
            _AED_Running = true;
            _AED_Success = false;
            string inv = "";
            inv += InvokeFormEvent(FormDynamicEvent.BEFOREEDIT);
            inv += InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
            V6Tag invTag = new V6Tag(inv);
            if (invTag.Cancel)
            {
                this.ShowWarningMessage(invTag.DescriptionLang(V6Setting.IsVietnamese));
                Mode = V6Mode.Edit;
                //detail1.MODE = V6Mode.View;
                GoToFirstFocus(txtMa_sonb);
                _AED_Running = false;
                return;
            }

            new Thread(DoEdit)
            {
                IsBackground = true
            }
            .Start();
            
            checkEdit.Start();
        }
        private void ReadyForEdit()
        {
            try
            {
                var am_DATE0 = AM.Rows[CurrentIndex]["Date0"];
                var am_TIME0 = AM.Rows[CurrentIndex]["Time0"];
                var am_U_ID0 = AM.Rows[CurrentIndex]["User_id0"];

                readyDataAD = dataGridView1.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in readyDataAD)
                {
                    adRow["DATE0"] = am_DATE0;
                    adRow["TIME0"] = am_TIME0;
                    adRow["USER_ID0"] = am_U_ID0;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void checkEdit_Tick(object sender, EventArgs e)
        {
            if (!_AED_Running)
            {
                ((Timer)sender).Stop();

                if (_AED_Success)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.EditSuccess);
                    ShowParentMessage(V6Text.EditSuccess);
                    ViewInvoice(_sttRec, V6Mode.Edit);
                    btnMoi.Focus();
                    All_Objects["mode"] = V6Mode.Edit;
                    All_Objects["STT_REC"] = _sttRec;
                    All_Objects["MA_CT"] = Invoice.Mact;
                    All_Objects["MA_NT"] = MA_NT;
                    All_Objects["MA_NX"] = txtManx.Text;
                    All_Objects["LOAI_CK"] = chkLoaiChietKhau.Checked ? "1" : "0";
                    All_Objects["MODE"] = "M";
                    All_Objects["KIEU_POST"] = cboKieuPost.SelectedValue;
                    //All_Objects["AP_GIA"] = apgia;
                    All_Objects["USER_ID"] = V6Login.UserId;
                    //All_Objects["SAVE_VOUCHER"] = _sttRec;
                    InvokeFormEvent(FormDynamicEvent.AFTEREDITSUCCESS);
                    InvokeFormEvent(FormDynamicEvent.AFTERSAVESUCCESS);
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.EditFail + ": " + editErrorMessage);
                    ShowParentMessage(V6Text.EditFail + ": " + editErrorMessage);
                    Mode = V6Mode.Edit;
                }

                ((Timer)sender).Dispose();
                if (_print_flag)
                {
                    
                    _print_flag = false;
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
            }
        }

        private void DoEdit()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                var keys = new SortedDictionary<string, object> { { "STT_REC", _sttRec } };
                if (Invoice.UpdateInvoice(readyDataAM, readyDataAD, keys))
                {
                    _AED_Success = true;
                    ADTables.Remove(_sttRec);
                    // WriteDBlog.
                    SaveEditLog(AM_current.ToDataDictionary(), readyDataAM);

                    DoEdit2_TH_Thread(keys);
                }
                else
                {
                    _AED_Success = false;
                    editErrorMessage = Invoice.V6Message;
                    Invoice.PostErrorLog(_sttRec, "S", editErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _AED_Success = false;
                editErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "S " + _sttRec, ex);
            }

            if (_print_flag)
                Thread.Sleep(2000);
            _AED_Running = false;
        }

        private void DoEdit2_TH_Thread(SortedDictionary<string, object> keys)
        {
            try
            {
                _keys_TH = keys;
                Thread edit2_TH = new Thread(DoEdit2_TH);
                edit2_TH.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private IDictionary<string, object> _keys_TH;
        private void DoEdit2_TH()
        {
            // Nếu có cấu hình KEY_AM2TH, Xét đúng kiều kiện thì update nếu tồn tại hoặc insert. Sai điều kiện thì xóa.
            try
            {
                if (Invoice.Have_KEY_AM2TH)
                {

                    if (Invoice.Exist2_TH(_sttRec))
                    {
                        if (Invoice.IS_AM2TH(readyDataAM))
                            Invoice.UpdateInvoice2_TH(readyDataAM, readyDataAD, _keys_TH);
                        else Invoice.DeleteInvoice2_TH(_sttRec);
                    }
                    else
                    {
                        if (Invoice.IS_AM2TH(readyDataAM))
                            Invoice.InsertInvoice2_TH(readyDataAM, readyDataAD);
                    }

                }
            }
            catch (Exception ex2_TH)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex2_TH);
            }
        }
        #endregion edit

        #region ==== Delete Thread ====
        private string deleteErrorMessage = "";

        private void DoDeleteThread()
        {
            try
            {
                if (Mode == V6Mode.View)
                {
                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                    {
                        DisableAllFunctionButtons(btnLuu, btnMoi, btnCopy, btnIn, btnSua, btnHuy, btnXoa, btnXem, btnTim,
                            btnQuayRa);

                        Timer checkDelete = new Timer();
                        checkDelete.Interval = 500;
                        checkDelete.Tick += checkDelete_Tick;
                        _AED_Running = true;
                        _AED_Success = false;
                        InvokeFormEvent(FormDynamicEvent.BEFOREDELETE);
                        new Thread(DoDelete)
                        {
                            IsBackground = true
                        }
                            .Start();

                        checkDelete.Start();
                    }
                    else
                    {
                        EnableFunctionButtons();
                    }
                }
            }
            catch (Exception ex)
            {
                Invoice.PostErrorLog(_sttRec, "X " + _sttRec, ex);
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void checkDelete_Tick(object sender, EventArgs e)
        {
            if (!_AED_Running)
            {
                ((Timer)sender).Stop();

                if (_AED_Success)
                {
                    if (_timForm != null && !_timForm.IsDisposed)
                        _timForm.UpdateAM(_sttRec, null, V6Mode.Delete);

                    All_Objects["mode"] = V6Mode.Delete;
                    All_Objects["STT_REC"] = _sttRec;
                    All_Objects["MA_CT"] = Invoice.Mact;
                    All_Objects["USER_ID"] = V6Login.UserId;
                    InvokeFormEvent(FormDynamicEvent.AFTERDELETESUCCESS);
                    V6ControlFormHelper.ShowMainMessage(V6Text.DeleteSuccess);
                    ShowParentMessage(V6Text.DeleteSuccess);
                    //View lai cai khac
                    if (CurrentIndex >= AM.Rows.Count) CurrentIndex--;
                    if (CurrentIndex >= 0)
                    {
                        ViewInvoice(CurrentIndex);
                        btnMoi.Focus();
                    }
                    else
                    {
                        ResetForm();
                        Mode = V6Mode.Init;
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.DeleteFail + ": " + deleteErrorMessage);
                    ShowParentMessage(V6Text.DeleteFail + ": " + deleteErrorMessage);
                }

                ((Timer)sender).Dispose();
            }
        }

        private void DoDelete()
        {
            //Xóa xong view lại cái khác (trong timer tick)
            try
            {
                var row = AM.Rows[CurrentIndex];
                _sttRec = row["Stt_rec"].ToString().Trim();
                if (Invoice.DeleteInvoice(_sttRec))
                {
                    _AED_Success = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                    if (Invoice.IS_AM2TH(row.ToDataDictionary()))
                    {
                        _sttRec2_TH = _sttRec;
                        DoDelete2_TH_Thread();
                    }
                }
                else
                {
                    _AED_Success = false;
                    deleteErrorMessage = V6Text.Text("XOA0");
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice81.DeleteInvoice return false." + Invoice.V6Message);
                }
            }
            catch (Exception ex)
            {
                _AED_Success = false;
                deleteErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "X " + _sttRec, ex);
            }
            _AED_Running = false;
        }

        private void DoDelete2_TH_Thread()
        {
            try
            {
                Thread delete2_TH = new Thread(DoDelete2_TH);
                delete2_TH.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private string _sttRec2_TH;
        private void DoDelete2_TH()
        {
            try
            {
                Invoice.DeleteInvoice2_TH(_sttRec2_TH);
            }
            catch (Exception ex2_TH)
            {
                this.WriteExLog(string.Format("{0}.{1} 2_TH {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex2_TH);
            }
        }
        #endregion delete

        private void Luu()
        {
            try
            {
                InvokeFormEvent(FormDynamicEvent.BEFORELUU);

                if (false )//detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    EnableFunctionButtons();
                }
                else
                {
                    if (chkAuto_Ck.Checked && (V6Options.M_SOA_TINH_CK_KM == "11" || V6Options.M_SOA_TINH_CK_KM == "12"))
                    {
                        TinhChietKhauKhuyenMai();
                        TinhTongThanhToan("TinhCK_KM_LUU");
                    }

                    V6ControlFormHelper.RemoveRunningList(_sttRec);

                    readyDataAM = PreparingDataAM(Invoice);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD2);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD3);
                    if (chkLoaiChietKhau.Checked)
                    {
                        V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "PT_CKI"}, AD);
                    }

                    if (Mode == V6Mode.Add)
                    {
                        DoAddThread();
                        return;
                    }
                    if (Mode == V6Mode.Edit)
                    {
                        DoEditThread();
                        return;
                    }
                    if (Mode == V6Mode.View)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void Moi()
        {
            try
            {
                if (V6Login.UserRight.AllowAdd("", Invoice.CodeMact))
                {
                    FormManagerHelper.HideMainMenu();

                    if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    {
                        if (true)
                        {
                            if (this.ShowConfirmMessage(V6Text.DiscardConfirm) != DialogResult.Yes)
                                return;
                        }
                    }

                    AM_old = IsViewingAnInvoice ? AM.Rows[CurrentIndex] : null;

                    ResetForm();
                    Mode = V6Mode.Add;

                    //LoadAll(V6Mode.Add);

                    GetSttRec(Invoice.Mact);

                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);

                    //GetSoPhieu();
                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    GET_AM_OLD_EXTRA();
                    //detail1.DoAddButtonClick( );
                    //var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                    //if (readonly_list.Contains(detail1.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                    //{
                    //    detail1.ChangeToViewMode();
                    //    dataGridView1.UnLock();
                    //}
                    //else
                    //{
                    //    dataGridView1.Lock();
                    
                    //}
                    GoToFirstFocus(txtMa_sonb);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch(Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                if (IsViewingAnInvoice)
                {
                    InitEditLog();
                    if (V6Login.UserRight.AllowEdit("", Invoice.CodeMact))
                    {
                        if (Mode == V6Mode.View)
                        {
                            // Tuanmh 16/02/2016 Check level
                            var row = AM.Rows[CurrentIndex];
                            if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                            {
                                //Tuanmh 24/07/2016 Check Debit Amount
                                bool check_edit = 
                                CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMaDVCS.Text.Trim(), txtMaKh.Text.Trim(),
                                        txtManx.Text.Trim(), dateNgayCT.Date, txtTongThanhToan.Value, "E");

                                if (check_edit)
                                {
                                    Mode = V6Mode.Edit;
                                    //detail1.MODE = V6Mode.View;
                                    GoToFirstFocus(txtMa_sonb);
                                }
                            }
                            else
                            {
                                V6ControlFormHelper.NoRightWarning();
                            }
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Xoa()
        {
            try
            {
                if (!IsViewingAnInvoice) return;
                if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact))
                {
                    var row = AM.Rows[CurrentIndex];
                    // Tuanmh 16/02/2016 Check level
                    if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                    {
                        //Tuanmh 24/07/2016 Check Debit Amount
                        bool check_edit =
                            CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMaDVCS.Text.Trim(), txtMaKh.Text.Trim(),
                                txtManx.Text.Trim(), dateNgayCT.Date, txtTongThanhToan.Value, "D");

                        if (check_edit)
                        {
                            DoDeleteThread();
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void Copy()
        {
            try
            {
                if (!IsViewingAnInvoice) return;
                if (V6Login.UserRight.AllowCopy("", Invoice.CodeMact))
                {
                    if (Mode == V6Mode.View)
                    {
                        if (string.IsNullOrEmpty(_sttRec))
                        {
                            this.ShowWarningMessage(V6Text.NoSelection);
                        }
                        else
                        {
                            AM_old = IsViewingAnInvoice ? AM.Rows[CurrentIndex] : null;
                            GetSttRec(Invoice.Mact);
                            SetNewValues();
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                            Mode = V6Mode.Add;
                            //detail1.MODE = V6Mode.View;
                            GoToFirstFocus(txtMa_sonb);
                        }
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Copy " + _sttRec, ex);
            }
        }

        private void SetNewValues()
        {
            try
            {
                dateNgayCT.SetValue(V6Setting.M_SV_DATE);
                dateNgayLCT.SetValue(V6Setting.M_SV_DATE);
                txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text, dateNgayCT.Date);
                ResetAMADbyConfig(Invoice);
                foreach (DataRow dataRow in AD.Rows)
                {
                    dataRow["STT_REC"] = _sttRec;
                }
                InvokeFormEventFixCopyData();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetNewValues " + _sttRec, ex);
            }
        }



        private TimDonDatHangBanForm SearchForm
        {
            get
            {
                if (_timForm == null || _timForm.IsDisposed)
                    _timForm = new TimDonDatHangBanForm(Invoice, V6Mode.View);
                return _timForm;
            }
        }
        private TimDonDatHangBanForm _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    SearchForm.ViewMode = true;
                    SearchForm.Refresh0();
                    if (SearchForm._locKetQua.dataGridView1.CurrentCell != null)
                    {
                        int cIndex = SearchForm._locKetQua.dataGridView1.CurrentCell.ColumnIndex;
                        SearchForm._locKetQua.dataGridView1.CurrentCell =
                            SearchForm._locKetQua.dataGridView1.Rows[CurrentIndex].Cells[cIndex];
                    }

                    if (SearchForm.ShowDialog(this) == DialogResult.OK && SearchForm._formChungTu_AM != null)
                    {
                        AM = SearchForm._formChungTu_AM;
                        ViewInvoice(SearchForm._locKetQua.CurrentSttRec, V6Mode.View);
                    }

                    btnSua.Focus();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Xem " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Tìm chứng từ.
        /// </summary>
        public void Tim()
        {
            Tim("");
        }

        /// <summary>
        /// Tìm chứng từ với cờ. 1 Top cuối kỳ.
        /// </summary>
        /// <param name="flag"></param>
        public void Tim(string flag)
        {
            try
            {
                if (V6Login.UserRight.AllowView("", Invoice.CodeMact))
                {
                    FormManagerHelper.HideMainMenu();

                    SearchForm.ViewMode = false;
                    SearchForm.Visible = false;
                    if (flag == "1")
                    {
                        SearchForm.ViewMode = true;
                        SearchForm.SearchTopCuoiKy();
                    }

                    if (SearchForm.ShowDialog(this) == DialogResult.OK)
                    {
                        AM = SearchForm._formChungTu_AM;
                        ViewInvoice(SearchForm._locKetQua.CurrentSttRec, V6Mode.View);
                    }

                    btnSua.Focus();
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Tim " + _sttRec, ex);
            }
        }


        private void QuayRa()
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (this.ShowConfirmMessage(V6Text.DiscardConfirm) != DialogResult.Yes)
                {
                    return;
                }
            }
            Parent.Dispose();
        }

        public decimal TongThanhToan { get { return txtTongThanhToan.Value; } }
        public decimal TongThanhToanNT { get { return txtTongThanhToanNt.Value; } }
        /// <summary>
        /// Lưu và in (click nút in, chọn máy in, không in ngay), có hiển thị form in trước 3 giây.
        /// </summary>
        private void LuuVaIn()
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (false )//detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    ShowMainMessage(V6Text.DetailNotComplete);
                    return;
                }

                _print_flag = true;
                _sttRec_In = _sttRec;
                btnLuu.Focus();
                if (ValidateData_Master())
                {
                    Luu();
                    Mode = V6Mode.View;// Status = "3";
                }
                else
                {
                    _print_flag = false;
                }
            }
            else if (Mode == V6Mode.View)
            {
                BasePrint(Invoice, _sttRec, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
            }
        }

        #region ==== Navigation function ====
        private void First()
        {
            ViewInvoice(0);
        }

        private void Previous()
        {
            ViewInvoice(--CurrentIndex);
        }

        private void Next()
        {
            ViewInvoice(++CurrentIndex);
        }

        private void Last()
        {
            ViewInvoice(AM.Rows.Count-1);
        }
        #endregion navi f

        /// <summary>
        /// Xóa dữ liệu đang hiển thị
        /// </summary>
        private void ResetForm()
        {
            try
            {
                SetData(null);
                //detail1.SetData(null);
                //detail2.SetData(null);
                //detail3.SetData(null);
                
                LoadAD("");
                SetGridViewData();
                
                ResetAllVars();
                EnableVisibleControls();
                SetFormDefaultValues();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void ResetAllVars()
        {
            _sttRec = "";
            _carryRowData = new SortedDictionary<string, object>();
            CurrentIndex = -1;
        }

        private void SetFormDefaultValues()
        {
            chkLoaiChietKhau.Checked = true;//loai ck chung
            //cboKieuPost.SelectedValue = "1";
            switch (Mode)
            {
                case V6Mode.Init:
                case V6Mode.View:
                    chkSuaPtck.Enabled = false;
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkT_CK_NT.Enabled = false;
                    chkT_CK_NT.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                    break;
                case V6Mode.Add:
                case V6Mode.Edit:
                    chkSuaPtck.Enabled = true;
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkT_CK_NT.Enabled = true;
                    chkT_CK_NT.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                break;
            }
        }

        public override void Huy()
        {
            try
            {
                dataGridView1.UnLock();
                if (Mode == V6Mode.Edit)
                {
                    if (this.ShowConfirmMessage(V6Text.DiscardConfirm) == DialogResult.Yes)
                    {
                        V6ControlFormHelper.RemoveRunningList(_sttRec);
                        ViewInvoice(_sttRec, Mode);
                        btnMoi.Focus();
                    }
                }
                if (Mode == V6Mode.Add)
                {
                    if (this.ShowConfirmMessage(V6Text.DiscardConfirm) == DialogResult.Yes)
                    {
                        V6ControlFormHelper.RemoveRunningList(_sttRec);

                        if (AM != null && AM.Rows.Count > 0)
                        {
                            ViewInvoice(CurrentIndex);
                            btnMoi.Focus();
                        }
                        else
                        {
                            Mode = V6Mode.View;
                            ResetForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void GetSoPhieuInit()
        {
            var p = GetParentTabPage();
            if (p != null)
            {
                txtSoPhieu.Text = ((TabControl)p.Parent).TabPages.Count.ToString();
            }
            else
            {
                txtSoPhieu.Text = "01";    
            }
        }

        private void GetSoPhieu()
        {
            //TxtSo_ct.Text = V6BusinessHelper.GetSoCT("M", "", Invoice.Mact, "", V6LoginInfo.UserId);
            txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text, dateNgayCT.Date);
        }

        private void SetTabPageText(string text)
        {
            var p = GetParentTabPage();
            if (p != null)
            {
                p.Text = text;
            }
        }

        

        #endregion AM Methods

        #region ==== Detail control events ====

        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                //var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                //if (readonly_list.Contains(detail1.btnMoi.Name, StringComparer.InvariantCultureIgnoreCase))
                //{
                //    detail1.ChangeToViewMode();
                //    dataGridView1.UnLock();
                //}
                //else
                //{
                //    dataGridView1.Lock();
                //    _maVt.Focus();
                //}
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public override void SetDefaultDetail()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                var grow = dataGridView1.Rows[dataGridView1.RowCount - 1];
                var ma_lnx_i_column = dataGridView1.Columns["MA_LNX_I"];
                if (ma_lnx_i_column != null && txtLoaiNX_PH.Text != string.Empty)
                {
                    SetCellValue(grow.Cells[ma_lnx_i_column.Name], txtLoaiNX_PH.Text);
                }
            }
        }

        public void SetCarry(DataGridViewRow grow)
        {
            try
            {
                foreach (string field in _carryFields)
                {
                    if (_carryRowData.ContainsKey(field))
                    {
                        SetCellValue(grow.Cells[field], _carryRowData[field]);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public override bool XuLyThemDetail(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                if (error == "")
                {
                    UpdateDetailChangeLog(_sttRec0, detailControlList1, null, data);
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD.NewRow();
                    foreach (DataColumn column in AD.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(key) ? data[key] : "")??DBNull.Value;
                        newRow[key] = value;
                    }
                    AD.Rows.Add(newRow);
                    dataGridView1.DataSource = AD;
                    
                    if (AD.Rows.Count > 0)
                    {
                        dataGridView1.Rows[AD.Rows.Count - 1].Selected = true;
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "Ma_vt");
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckData + error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
                return false;
            }
            TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
            return true;
        }

        
        private bool XuLySuaDetail(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv1EditingRow != null)
                {
                    var cIndex = _gv1EditingRow.Index;

                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        //Thêm thông tin...
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Date;
                        
                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                        if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;

                        if (error == "")
                        {
                            //Sửa dòng dữ liệu trên DataRow vì DBNull lỗi khi xử lý trên dgv.?
                            var currentRow = AD.Rows[cIndex];
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, detailControlList1, currentRow.ToDataDictionary(), data);
                            foreach (DataColumn column in AD.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = AD;
                        }
                        else
                        {
                            this.ShowWarningMessage(V6Text.CheckData + error);
                            return false;
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
                return false;
            }
            TinhTongThanhToan("xy ly sua detail");
            return true;
        }

        private void XuLyXoaDetail()
        {
            try
            {
                var readonly_list = SetControlReadOnlyHide(new HD_Detail(){Name = "detail1"}, Invoice, Mode, V6Mode.Delete);
                if (readonly_list.Contains("btnXoa", StringComparer.InvariantCultureIgnoreCase))
                {
                    this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                    return;
                }

                if (dataGridView1.CurrentRow != null)
                {
                    var cIndex = dataGridView1.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        var currentRow = AD.Rows[cIndex];
                        var details = "Mã vật tư: " + currentRow["Ma_vt"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details) == DialogResult.Yes)
                        {
                            var delete_data = currentRow.ToDataDictionary();
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, detailControlList1, delete_data, null);
                            AD.Rows.Remove(currentRow);
                            dataGridView1.DataSource = AD;
                            //detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
                            TinhTongThanhToan("xu ly xoa detail");

                            All_Objects["data"] = delete_data;
                            InvokeFormEvent(FormDynamicEvent.AFTERDELETEDETAILSUCCESS);
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        #endregion details

        #region ==== AM Events ====
        private void Form_Load(object sender, EventArgs e)
        {
            LoadTag(1, Invoice.Mact, Invoice.Mact, m_itemId, "");
            SetStatus2Text();
            btnMoi.Focus();
            if (ClickSuaOnLoad)
            {
                ClickSuaOnLoad = false;
                btnSua.PerformClick();
            }
        }
        
        #region ==== Command Buttons ====
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DisableAllFunctionButtons(btnLuu, btnMoi, btnCopy, btnIn, btnSua, btnHuy, btnXoa, btnXem, btnTim, btnQuayRa);
            if (ValidateData_Master())
            {
                Luu();
            }
            else
            {
                EnableFunctionButtons();
            }
        }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            Moi();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ValidateNgayCt(Invoice.Mact, dateNgayCT))
            {
                Sua();
            }
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            Xem();
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            Tim();
        }
        private void btnQuayRa_Click(object sender, EventArgs e)
        {
            QuayRa();
        }
        #endregion command buttons
        

        #region ==== Navigation button ====
        private void btnFirst_Click(object sender, EventArgs e)
        {
            First();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Last();
        }
        #endregion navi

        
        private void dateNgayCT_ValueChanged(object sender, EventArgs e)
        {
            if (!Invoice.M_NGAY_CT) dateNgayLCT.SetValue(dateNgayCT.Date);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            HuyBase();
        }

        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ValidateNgayCt(Invoice.Mact, dateNgayCT))
            {
                Xoa();
            }
        }

        private void chkLoaiChietKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                chkT_CK_NT.Enabled = chkLoaiChietKhau.Checked;
            }

            var pt_cki_column = dataGridView1.Columns["PT_CKI"];
            if (chkLoaiChietKhau.Checked)
            {
                pt_cki_column.ReadOnly = true;
            }
            else
            {
                chkSuaPtck.Checked = false;
                txtPtCk.ReadOnly = true;
                chkT_CK_NT.Checked = false;
                txtTongCkNt.ReadOnly = true;

                pt_cki_column.ReadOnly = false;
            }
            
            TinhTongThanhToan("LoaiChietKhau_Change");
        }

        private void cboMaNt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_ready0 && cboMaNt.SelectedValue != null)
            {
                _maNt = cboMaNt.SelectedValue.ToString().Trim();
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit) GetTyGia();
                FormatGridView();
                XuLyThayDoiMaNt();
            }

            txtTyGia_V6LostFocus(sender);
        }

        private void txtMaGia_V6LostFocus(object sender)
        {
            XuLyThayDoiMaGia();
        }

        private void txtThueSuat_V6LostFocus(object sender)
        {
            TinhTongThanhToan("V6LostFocus " + ((Control)sender).AccessibleName);
        }

        #endregion am events

        private void txtMa_thue_V6LostFocus(object sender)
        {
            XuLyThayDoiMaThue();
        }

        private void chkSuaTkThue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSuaTkThue.Checked)
            {
                txtTkThueCo.ReadOnly = false;
                txtTkThueNo.ReadOnly = false;
                txtTkThueCo.Tag = null;
                txtTkThueNo.Tag = null;
            }
            else
            {
                txtTkThueCo.ReadOnly = true;
                txtTkThueNo.ReadOnly = true;
                txtTkThueCo.Tag = "readonly";
                txtTkThueNo.Tag = "readonly";
            }
        }

        private void chkSuaTienCk_CheckedChanged(object sender, EventArgs e)
        {
            if(Mode == V6Mode.Add || Mode == V6Mode.Edit)
                txtTongCkNt.ReadOnly = !chkT_CK_NT.Checked;

        }

        private void chkSuaTienThue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    txtTongThueNt.ReadOnly = !chkT_THUE_NT.Checked;
                }

                TinhTongThanhToan("ckhSuaTienThue");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //var fieldName = e.Column.DataPropertyName.ToUpper();
            //if (_alct1Dic.ContainsKey(fieldName))
            //{
            //    var row = _alct1Dic[fieldName];
            //    var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
            //    var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
            //    if(fieldName == "GIA_NT21") fcaption += " "+ cboMaNt.SelectedValue;
            //    if (fieldName == "TIEN_NT2") fcaption += " " + cboMaNt.SelectedValue;

            //    if (fieldName == "GIA21") fcaption += " " + _mMaNt0;
            //    if (fieldName == "TIEN2") fcaption += " " + _mMaNt0;

            //    if (!fstatus2) e.Column.Visible = false;

            //    e.Column.HeaderText = fcaption;
            //}
            //else if(!new List<string> {"TEN_VT","MA_VT"}.Contains(fieldName))
            //{
            //    e.Column.Visible = false;
            //}
        }

        private void txtSoCt_TextChanged(object sender, EventArgs e)
        {
            SetTabPageText(txtSoPhieu.Text);

            if(Mode == V6Mode.Add || Mode == V6Mode.Edit)
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
        }

        private void DonDatHangBanBanHangKiemPhieuXuat_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                SetStatus2Text();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            BasePrint(Invoice, _sttRec, V6PrintMode.None, TongThanhToan, TongThanhToanNT, false);
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void txtPtCk_V6LostFocus(object sender)
        {
            if (chkLoaiChietKhau.Checked && txtPtCk.Value == 0)
            {
                chkSuaPtck.Checked = false;
            }
            TinhTongThanhToan("V6LostFocus txtPtCk_V6LostFocus ");
        }

        private void chkSuaTien_CheckedChanged(object sender, EventArgs e)
        {
            var grow = dataGridView1.CurrentRow;
            if (grow == null) return;

            var ck_nt_column = dataGridView1.Columns["CK_NT"];
            var tien_nt2_column = dataGridView1.Columns["TIEN_NT2"];
            var tien_nt_column = dataGridView1.Columns["TIEN_NT"];
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                tien_nt2_column.ReadOnly = !chkSuaTien.Checked;
                tien_nt_column.ReadOnly = !(chkSuaTien.Checked && STR(grow, "PX_GIA_DDI") != "");
                ck_nt_column.ReadOnly = !chkSuaTien.Checked;
            }
            if (chkSuaTien.Checked)
            {
                tien_nt2_column.ReadOnly = false;
                tien_nt_column.ReadOnly = false;
                ck_nt_column.ReadOnly = false;
            }
            else
            {
                tien_nt2_column.ReadOnly = true;// "disable";
                tien_nt_column.ReadOnly = true;// "disable";
                ck_nt_column.ReadOnly = true;// "disable";
            }
        }

        private void chkSuaPtck_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                txtPtCk.ReadOnly = !chkSuaPtck.Checked;
        }

        private void DonDatHangBanDetail1_Load(object sender, EventArgs e)
        {

        }

        //private void DonDatHangBanDetail1_ClickEdit(object sender, HD_Detail_Eventargs e)
        //{
        //    try
        //    {
        //        if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
        //        {
        //            ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);
        //            if (_gv1EditingRow == null)
        //            {
        //                this.ShowWarningMessage(V6Text.NoSelection);
        //                return;
        //            }
                    
        //            detail1.ChangeToEditMode();
        //            var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Edit);
        //            if (readonly_list.Contains(detail1.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
        //            {
        //                detail1.ChangeToViewMode();
        //                dataGridView1.UnLock();
        //            }
        //            else
        //            {
        //                dataGridView1.Lock();
        //                XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
        //                _maVt.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
        //    }
        //}
        
        private bool ValidateData_Master()
        {
            try
            {
                InvokeFormEvent(FormDynamicEvent.BEFOREVALIDATE);

                // Check Master
                if (!ValidateNgayCt(Invoice.Mact, dateNgayCT))
                {
                    return false;
                }

                if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaDVCS.Text);
                    txtMaDVCS.Focus();
                    return false;
                }

                if (txtMaKh.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaKH.Text);
                    txtMaKh.Focus();
                    return false;
                }

                if (cboKieuPost.SelectedIndex == -1)
                {
                    this.ShowWarningMessage(V6Text.Text("CHUACHONKIEUPOST"));
                    cboKieuPost.Focus();
                    return false;
                }

                if (!ValidateMasterData(Invoice)) return false;


                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoInputDetail);
                    return false;
                }
                else
                {
                    string message = "";
                    foreach (DataGridViewRow grow in dataGridView1.Rows)
                    {
                        if (IS_NOT_VALIDATE(grow))
                        {
                            message += string.Format("\n\tMA_VT: {0} Line: {1}", MA_VT(grow), grow.Index + 1);
                        }
                    }

                    if (!string.IsNullOrEmpty(message))
                    {
                        this.ShowWarningMessage(V6Text.DetailNotComplete + "\n" + message);
                        return false;
                    }
                }
                

                //Tuanmh 24/07/2016 Check Debit Amount
                {
                    var mode_vc = "V";
                    if (Mode == V6Mode.Edit)
                    {
                        mode_vc = "E";
                    }
                    else if (Mode == V6Mode.Add)
                    {
                        mode_vc = "A";

                    }

                    DataTable DataCheck_Save_All = Invoice.GetCheck_Save_All(cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec, txtMaDVCS.Text.Trim(), txtMaKh.Text.Trim(),
                        txtManx.Text.Trim(), dateNgayCT.Date, txtMa_ct.Text, txtTongThanhToan.Value, mode_vc, V6Login.UserId);



                    if (DataCheck_Save_All != null && DataCheck_Save_All.Rows.Count > 0)
                    {
                        var chksave_all = DataCheck_Save_All.Rows[0]["chksave_all"].ToString();
                        var chk_yn = DataCheck_Save_All.Rows[0]["chk_yn"].ToString();
                        var mess = DataCheck_Save_All.Rows[0]["mess"].ToString().Trim();
                        var mess2 = DataCheck_Save_All.Rows[0]["mess2"].ToString().Trim();
                        var message = V6Setting.IsVietnamese ? mess : mess2;

                        switch (chksave_all)
                        {
                            case "00":
                            case "04":
                                // Save: OK --Loai_kh in ALKH
                                // Save: OK --Thau
                                break;
                            case "01":
                            case "02":
                            case "03":

                                if (message != "") this.ShowWarningMessage(message);
                                if (chk_yn == "0")
                                {
                                    return false;
                                }
                                break;

                            case "06":
                            case "07":
                            case "08":
                                // Save but mess
                                if (message != "") this.ShowWarningMessage(message);
                                break;


                        }
                    }
                }

                // Tuanmh 16/02/2016 Check Voucher Is exist 
                {
                    DataTable DataCheckVC = Invoice.GetCheck_VC_Save(cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec);
                    if (DataCheckVC != null && DataCheckVC.Rows.Count > 0)
                    {
                        var chkso_ct = DataCheckVC.Rows[0]["chkso_ct"].ToString();
                        switch (chkso_ct)
                        {
                            case "0":
                                // Save: OK
                                break;
                            case "1":
                                // Save: OK But Notice
                                this.ShowWarningMessage(V6Text.Voucher_exist);
                                break;
                            case "2":
                                // Save: Not Save
                                this.ShowWarningMessage(V6Text.Voucher_exist_not_save);
                                //Tuanmh 26/09/2019 Get new for exist
                                GetSoPhieu();
                                txtSoPhieu.Focus();
                                return false;
                        }
                    }
                }

                //OK
                return true;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ValidateData_Master " + _sttRec, ex);
            }
            return false;
        }

        
        private bool ValidateData_Detail_Row(DataGridViewRow grow)
        {
            try
            {
                string firstErrorField;
                string errors = ValidateDetailData_Row(Invoice, grow, out firstErrorField);
                if (!string.IsNullOrEmpty(errors))
                {
                    var cell = grow.Cells[firstErrorField];
                    cell.Style.BackColor = Color.Red;
                    
                    
                    this.ShowWarningMessage(errors);
                    //dataGridView1.LoadSelectedCellLocation();
                    //var cell = grow.Cells[firstErrorField];
                    //dataGridView1.CurrentCell = cell;
                    //_focusCell = cell;
                    //V6ControlFormHelper.SetGridviewCurrentCellByIndex(dataGridView1, cell.RowIndex, cell.ColumnIndex, this);
                    //dataGridView1.ClearSelection();
                    //cell.Selected = true;
                    //dataGridView1.CurrentCell = _edittingRow.Cells[firstErrorField];
                    //dataGridView1.CurrentCell.Selected = true;
                    //  ??!!!  InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    return false;
                }
                else
                {
                    bool alt = 1 == grow.Index % 2;
                    var rowBackColor =
                        alt ? grow.DataGridView.AlternatingRowsDefaultCellStyle.BackColor
                            : grow.DataGridView.RowsDefaultCellStyle.BackColor;
                    foreach (DataGridViewCell cell in grow.Cells)
                    {
                        if (cell.Style.BackColor == Color.Red) cell.Style.BackColor = rowBackColor;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            return true;
        }

        private void btnViewInfoData_Click(object sender, EventArgs e)
        {
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            if (shift_is_down)
            {
                ShowViewInfoData2_TH(Invoice);
            }
            else if (Mode == V6Mode.View)
            {
                ShowViewInfoData(Invoice);
            }
        }

        private void txtMa_sonb_V6LostFocus(object sender)
        {
            GetSoPhieu();
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
            LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);
        }
        
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        
        private void btnInfos_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ProcessUserDefineInfo(Invoice.Mact, tabKhac, this, _sttRec);
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab == tabChiTiet)
            //{
            //    if (!chkTempSuaCT.Checked) detail1.AutoFocus();
            //}
        }

        private void txtMadvcs_V6LostFocus(object sender)
        {
            XuLyThayDoiMaDVCS();
        }

        private void detail1_LabelNameTextChanged(object sender, EventArgs e)
        {
            lblNameT.Text = ((Label)sender).Text;
        }

        #region ==== Chức năng methods ====

        

        private void ChucNang_TroGiup()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void ChucNang_ChonTuExcel(bool add)
        {
            try
            {
                if (NotAddEdit) return;

                chon_accept_flag_add = add;
                List<string> dateColumns = new List<string>();
                foreach (DataColumn column in AD.Columns)
                {
                    if (ObjectAndString.IsDateTimeType(column.DataType))
                    {
                        dateColumns.Add(column.ColumnName);
                    }
                }
                var chonExcel = new LoadExcelDataForm();
                chonExcel.CheckDateFields = dateColumns;
                chonExcel.Program = Event_program;
                chonExcel.All_Objects = All_Objects;
                chonExcel.DynamicFixMethodName = "DynamicFixExcel";
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                chonExcel.MA_CT = Invoice.Mact;
                chonExcel.LoadDataComplete += chonExcel_LoadDataComplete;
                chonExcel.AcceptData += chonExcel_AcceptData;
                chonExcel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chonExcel_LoadDataComplete(object sender)
        {
            try
            {
                LoadExcelDataForm chonExcel = (LoadExcelDataForm)sender;
                DataTable errorData = new DataTable("ErrorData");
                List<DataGridViewRow> removeList = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in chonExcel.dataGridView1.Rows)
                {
                    string cMaVt = ("" + row.Cells["MA_VT"].Value).Trim();
                    string cMaKhoI = ("" + row.Cells["MA_KHO_I"].Value).Trim();
                    if (cMaVt == string.Empty && cMaKhoI == string.Empty)
                    {
                        removeList.Add(row);
                        continue;
                    }
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);
                    if (!exist || !exist2)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        errorData.AddRow(row.ToDataDictionary(), true);
                    }
                }
                while (removeList.Count > 0)
                {
                    chonExcel.dataGridView1.Rows.Remove(removeList[0]);
                    removeList.RemoveAt(0);
                }
                if (errorData.Rows.Count > 0)
                {
                    DataViewerForm viewer = new DataViewerForm(errorData);
                    viewer.Text = V6Text.WrongData;
                    viewer.FormClosing += (o, args) =>
                    {
                        if (V6ControlFormHelper.ShowConfirmMessage(V6Text.Export + " " + V6Text.WrongData + "?") == DialogResult.Yes)
                        {
                            V6ControlFormHelper.ExportExcel_ChooseFile(this, errorData, "errorData");
                        }
                    };
                    viewer.ShowDialog(chonExcel);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chonExcel_AcceptData(DataTable table)
        {
            chonExcel_AcceptData(table.ToListDataDictionary());
        }
        void chonExcel_AcceptData(List<IDictionary<string,object>> table)
        {
            var count = 0;
            _message = "";
            //detail1.MODE = V6Mode.View;
            dataGridView1.UnLock();
            if (table == null || table.Count == 0) return;
            var row0 = table[0];
            if (row0.ContainsKey("MA_VT") && row0.ContainsKey("MA_KHO_I")
                                          && row0.ContainsKey("TIEN_NT0") && row0.ContainsKey("SO_LUONG1")
                                          && row0.ContainsKey("GIA_NT01"))
            {
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }

                foreach (IDictionary<string, object> row in table)
                {
                    var data = row;
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    //{ Tuanmh 31/08/2016 Them thong tin ALVT
                    V6VvarTextBox _maVt = new V6VvarTextBox();
                    _maVt.VVar = "MA_VT";
                    _maVt.Text = cMaVt;
                    var datavt = _maVt.Data;
                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1T")) data.Add("HE_SO1T", 1);
                        if (!data.ContainsKey("HE_SO1M")) data.Add("HE_SO1M", 1);
                        if (!data.ContainsKey("SO_LUONG")) data.Add("SO_LUONG", data["SO_LUONG1"]);

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["TIEN_NT0"]);
                        var __gia_nt0 = ObjectAndString.ObjectToDecimal(data["GIA_NT01"]);
                        var __tien0 = V6BusinessHelper.Vround(__tien_nt0 * txtTyGia.Value, M_ROUND);
                        var __gia0 = V6BusinessHelper.Vround(__gia_nt0 * txtTyGia.Value, M_ROUND_GIA);

                        if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);

                        if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", data["TIEN_NT0"]);
                        if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);
                        if (!data.ContainsKey("GIA01")) data.Add("GIA01", __gia0);
                        if (!data.ContainsKey("GIA0")) data.Add("GIA0", __gia0);
                        if (!data.ContainsKey("GIA")) data.Add("GIA", __gia0);
                        if (!data.ContainsKey("GIA1")) data.Add("GIA1", __gia0);
                        if (!data.ContainsKey("GIA_NT0")) data.Add("GIA_NT0", data["GIA_NT01"]);
                        if (!data.ContainsKey("GIA_NT")) data.Add("GIA_NT", data["GIA_NT01"]);
                        if (!data.ContainsKey("GIA_NT1")) data.Add("GIA_NT1", data["GIA_NT01"]);
                    }
                    //}



                    if (exist && exist2)
                    {
                        if (XuLyThemDetail(data))
                        {
                            count++;
                            All_Objects["data"] = data;
                            InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                        }
                    }
                    else
                    {
                        if (!exist) _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaVt);
                        if (!exist2) _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaKhoI);
                    }
                }
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
            }
            else
            {
                ShowParentMessage(V6Text.Text("LACKINFO"));
            }
        }

        #endregion chức năng

        private void TroGiupMenu_Click(object sender, EventArgs e)
        {
            ChucNang_TroGiup();
        }

        private void chonTuExcelMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonTuExcel(shift);
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            ////FixDataGridViewSize(dataGridView1);
            //var gpa = dataGridView1.Parent;
            //if (gpa != null)
            //{
            //    dataGridView1.Top = 52;
            //    if (HaveGridViewSummary(gpa))
            //    {
            //        dataGridView1.Height = gpa.Height - 75;
            //    }
            //    else
            //    {
            //        dataGridView1.Height = gpa.Height - 55;
            //    }
            //    dataGridView1.Left = 2;
            //    dataGridView1.Width = gpa.Width - 5;
            //}
        }


        private void btnApGia_Click(object sender, EventArgs e)
        {
            ApGiaBan();
            //ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);
        }

        private bool _flag_next = false;
        public override void ApGiaBan(bool auto = false)
        {
            try
            {
                if (NotAddEdit) return;
                if (_flag_next)
                {
                    _flag_next = false;
                    return;
                }
                if (AD == null || AD.Rows.Count == 0) return;
                if (false )//detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    return;
                }
                if (txtMaGia.Text.Trim() == "")
                {
                    ShowParentMessage(V6Text.NoInput + btnApGia.Text);
                    return;
                }
                if (this.ShowConfirmMessage(V6Text.Text("ASKAPGIABANALL")) != DialogResult.Yes)
                {
                    if (ActiveControl == txtMaKh)
                    {
                        _flag_next = true;
                        SelectNextControl(ActiveControl, true, true, true, true);
                        _flag_next = false;
                    }
                    return;
                }

                var gia_ban_nt_column = dataGridView1.Columns["GIA_BAN_NT"];

                foreach (DataRow row in AD.Rows)
                {
                    var maVatTu = row["MA_VT"].ToString().Trim();
                    var dvt = row["DVT"].ToString().Trim();
                    var dvt1 = row["DVT1"].ToString().Trim();
                    var pt_cki = ObjectAndString.ObjectToDecimal(row["PT_CKI"]);
                    var soLuong = ObjectAndString.ObjectToDecimal(row["SO_LUONG"]);
                    var soLuong1 = ObjectAndString.ObjectToDecimal(row["SO_LUONG1"]);
                    var tienNt2 = ObjectAndString.ObjectToDecimal(row["TIEN_NT2"]);
                    var tien2 = ObjectAndString.ObjectToDecimal(row["TIEN2"]);

                    var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), maVatTu, dvt1, txtMaKh.Text, txtMaGia.Text);

                    var gia_nt2 = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                    decimal gia_nt21 = gia_nt2;
                    decimal gia_ban_nt = gia_nt2;

                    if (gia_ban_nt_column != null && V6Options.GetValue("M_SOA_PRICE_INCLUDE_VAT") == "1")
                    {
                        row["GIA_BAN_NT"] = gia_ban_nt;
                        if (_maNt == _mMaNt0)
                        {
                            row["GIA_BAN"] = gia_ban_nt;
                        }

                        //TinhGiaNt21()
                        decimal thue_suat = 0m;
                        if (M_SOA_MULTI_VAT == "1")
                        {
                            thue_suat = ObjectAndString.ObjectToDecimal(row["thue_suat_i"]);
                        }
                        else
                        {
                            thue_suat = txtThueSuat.Value;
                        }

                        gia_nt21 = V6BusinessHelper.Vround(gia_ban_nt / (1 + (thue_suat / 100)), M_ROUND_GIA_NT);
                        row["GIA_NT21"] = gia_nt21;
                        row["GIA21"] = V6BusinessHelper.Vround((gia_nt21 * txtTyGia.Value), M_ROUND_GIA);
                        if (_maNt == _mMaNt0)
                        {
                            row["GIA21"] = gia_nt21;
                        }
                    }
                    else
                    {
                        row["GIA_NT21"] = gia_nt21;
                        row["Gia21"] = V6BusinessHelper.Vround((gia_nt21 * txtTyGia.Value), M_ROUND_GIA_NT);
                        if (_maNt == _mMaNt0)
                        {
                            row["Gia21"] = row["Gia_nt21"];
                        }
                    }
                    
                    
                    //CELL_DEC(grow, "SO_LUONG") = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                    tienNt2 = V6BusinessHelper.Vround((soLuong1 * gia_nt21), M_ROUND_NT);
                    tien2 = V6BusinessHelper.Vround((tienNt2 * txtTyGia.Value), M_ROUND);

                    row["tien_Nt2"] = tienNt2;
                    row["tien2"] = tien2;

                    //_tien2.Value = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        row["tien2"] = tienNt2;
                    }

                    //TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
                    var ck_nt = V6BusinessHelper.Vround(tienNt2 * pt_cki / 100, M_ROUND_NT);
                    row["ck_nt"] = ck_nt;
                    row["ck"] = V6BusinessHelper.Vround(ck_nt * txtTyGia.Value, M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        row["ck"] = row["ck_nt"];
                    }
                    //End TinhChietKhauChiTiet

                    

                    if (soLuong != 0)
                    {
                        row["gia_nt2"] = V6BusinessHelper.Vround((tienNt2 / soLuong), M_ROUND_GIA_NT);
                        //var tien2 = ObjectAndString.ObjectToDecimal(row["tien2"]);
                        row["gia2"] = V6BusinessHelper.Vround((tien2 / soLuong), M_ROUND_GIA);

                        if (_maNt == _mMaNt0)
                        {
                            row["gia2"] = row["gia_nt21"];
                            row["gia_nt2"] = row["gia_nt21"];
                        }
                    }
                    //End TinhGiaNt2

                    //TinhVanChuyen();
                    if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                    {
                        var hs_qd3 = ObjectAndString.ObjectToDecimal(row["hs_qd3"]);
                        var tien_vcNt = V6BusinessHelper.Vround((soLuong1 * hs_qd3), M_ROUND_NT);
                        row["tien_vc_Nt"] = tien_vcNt;
                        row["tien_vc"] = V6BusinessHelper.Vround((tien_vcNt * txtTyGia.Value), M_ROUND);

                        if (_maNt == _mMaNt0)
                        {
                            row["tien_vc"] = tien_vcNt;
                        }
                    }

                    //TinhGiamGiaCt();
                    if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                    {
                        var hs_qd4 = ObjectAndString.ObjectToDecimal(row["hs_qd4"]);
                        var ggNt = V6BusinessHelper.Vround((soLuong1 * hs_qd4), M_ROUND_NT);
                        row["gg_nt"] = ggNt;
                        row["gg"] = V6BusinessHelper.Vround((ggNt * txtTyGia.Value), M_ROUND);

                        if (_maNt == _mMaNt0)
                        {
                            row["gg"] = ggNt;
                        }
                    }

                    //TinhThueCt
                    if (M_SOA_MULTI_VAT == "1")
                    {
                        try
                        {
                            var thue_suat_i = ObjectAndString.ObjectToDecimal(row["THUE_SUAT_I"]);
                            row["THUE_NT"] = V6BusinessHelper.Vround(tienNt2 * thue_suat_i / 100, M_ROUND_NT);
                            row["THUE"] = V6BusinessHelper.Vround(tien2 * thue_suat_i / 100, M_ROUND);

                            if (_maNt == _mMaNt0)
                            {
                                row["THUE"] = row["THUE_NT"];
                            }
                        }
                        catch (Exception ex)
                        {
                            this.WriteExLog(GetType() + ".ApGiaBan TinhThueCt " + _sttRec, ex);
                        }
                    }

                    //====================

                    if (dvt.ToUpper().Trim() == dvt1.ToUpper().Trim())
                    {
                        row["GIA_NT2"] = row["GIA_NT21"];
                    }
                    else
                    {
                        if (soLuong != 0)
                        {
                            row["GIA_NT2"] = tienNt2 / soLuong;
                        }
                    }
                }

                dataGridView1.DataSource = AD;

                TinhTongThanhToan("ApGiaBan");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ApGiaBan " + _sttRec, ex);
            }
        }

        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                //Tuanmh 04/01/2020 Round lai tien_nt(_maNt) theo round cua tien (_mMaNt0) khi _maNt=_mMaNt0
                XuLyThayDoiMaNt(txtTyGia, chkSuaTien, _maNt, _mMaNt0);

                XuLyThayDoiTyGia(txtTyGia, chkSuaTien);
                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
        }


        private void txtTongGiamNt_V6LostFocus(object sender)
        {
            try
            {
                txtTongGiam.Value = V6BusinessHelper.Vround(txtTongGiamNt.Value * txtTyGia.Value, M_ROUND);
                if (MA_NT == _mMaNt0)
                {
                    txtTongGiam.Value = txtTongGiamNt.Value;
                }
                TinhTongThanhToan("txtTongGiamNt_V6LostFocus");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtTongGiamNt_V6LostFocus " + _sttRec, ex);
            }
        }

        private void txtTongCkNt_V6LostFocus(object sender)
        {
            try
            {
                txtTongCk.Value = V6BusinessHelper.Vround(txtTongCkNt.Value * txtTyGia.Value, M_ROUND);
                if (MA_NT == _mMaNt0)
                {
                    txtTongCk.Value = txtTongCkNt.Value;
                }
                TinhTongThanhToan("txtTongCkNt_V6LostFocus");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtTongCkNt_V6LostFocus " + _sttRec, ex);
            }
        }

        private void txtTongThueNt_V6LostFocus(object sender)
        {
            try
            {
                txtTongThue.Value = V6BusinessHelper.Vround(txtTongThueNt.Value * txtTyGia.Value, M_ROUND);
                if (MA_NT == _mMaNt0)
                {
                    txtTongThue.Value = txtTongThueNt.Value;
                }
                TinhTongThanhToan("txtTongThueNt_V6LostFocus");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtTongThueNt_V6LostFocus " + _sttRec, ex);
            }
        }

        private void btnTinhCKKM_Click(object sender, EventArgs e)
        {
            if (false )//detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                ShowParentMessage(V6Text.DetailNotComplete);
                return;
            }

            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            if (shift_is_down)
            {
                if (this.ShowConfirmMessage(V6Text.Text("ASKXOACKKM")) == DialogResult.Yes)
                {
                    XoaKhuyenMai();
                    XoaChietKhau();
                    TinhTongThanhToan("btnTinhCKKM_Click xoa ck_km");
                }
            }
            else
            {
                TinhChietKhauKhuyenMai();
                TinhTongThanhToan("btnTinhCKKM_Click");
            }
        }

        #region ==== TINH KHUYEN MAI ====
        private string MA_KM_Field = "MA_KMB";
        public void TinhChietKhauKhuyenMai()
        {
            try
            {
                XoaKhuyenMai();
                XoaChietKhau();
                if (AD.Rows.Count == 0) return;

                string lstt_rec0 = "",
                    lma_vt = "",
                    lso_luong1 = "",
                    lso_luong = "",
                    lgia_nt21 = "",
                    ltien_nt2 = "",
                    ldvt1 = "",
                    lgia_nt2 = "",
                    ltien2 = "",
                    lgia2 = "",
                    lma_kho_i = "",
                    lma_lo = "", lhsd = "", lma_vitri = "";
                foreach (DataRow row in AD.Rows)
                {
                    lstt_rec0 += ";" + row["STT_REC0"].ToString().Trim();
                    lma_vt += ";" + row["MA_VT"].ToString().Trim();
                    lso_luong1 += ";" + ObjectAndString.ObjectToDecimal(row["SO_LUONG1"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    lso_luong += ";" + ObjectAndString.ObjectToDecimal(row["SO_LUONG"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    lgia_nt21 += ";" + ObjectAndString.ObjectToDecimal(row["GIA_NT21"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    ltien_nt2 += ";" + ObjectAndString.ObjectToDecimal(row["TIEN_NT2"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    ldvt1 += ";" + row["DVT1"].ToString().Trim();
                    lgia_nt2 += ";" + ObjectAndString.ObjectToDecimal(row["GIA_NT2"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    ltien2 += ";" + ObjectAndString.ObjectToDecimal(row["TIEN2"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    lgia2 += ";" + ObjectAndString.ObjectToDecimal(row["GIA2"].ToString().Trim()).ToString(CultureInfo.InvariantCulture);
                    lma_kho_i += ";" + row["MA_KHO_I"].ToString().Trim();
                    lma_lo += ";" + row["MA_LO"].ToString().Trim();
                    lhsd += ";" + ObjectAndString.ObjectToString(row["HSD"], "yyyyMMdd");
                    lma_vitri += ";" + row["MA_VITRI"].ToString().Trim();
                }
                lstt_rec0 = lstt_rec0.Substring(1);
                lma_vt = lma_vt.Substring(1);
                lso_luong1 = lso_luong1.Substring(1);
                lso_luong = lso_luong.Substring(1);
                lgia_nt21 = lgia_nt21.Substring(1);
                ltien_nt2 = ltien_nt2.Substring(1);
                ldvt1 = ldvt1.Substring(1);
                lgia_nt2 = lgia_nt2.Substring(1);
                ltien2 = ltien2.Substring(1);
                lgia2 = lgia2.Substring(1);
                lma_kho_i = lma_kho_i.Substring(1);
                lma_lo = lma_lo.Substring(1);
                lhsd = lhsd.Substring(1);
                lma_vitri = lma_vitri.Substring(1);
                //Select cac chuong trinh km trong thoi gian hoa don
                SqlParameter[] plist =
                {
                    new SqlParameter("@cStt_rec", _sttRec),
                    new SqlParameter("@cMode", Mode == V6Mode.Add ? "M" : "S"),
                    new SqlParameter("@cMa_ct", Invoice.Mact),
                    new SqlParameter("@dngay_ct", dateNgayCT.YYYYMMDD),
                    new SqlParameter("@cMa_kh", txtMaKh.Text),
                    new SqlParameter("@cMa_dvcs", txtMaDVCS.Text),
                    new SqlParameter("@cMa_nt", _maNt),
                    new SqlParameter("@nT_so_luong", txtTongSoLuong.Value),
                    new SqlParameter("@nTso_luong1", TinhTong(AD, "SO_LUONG1")),
                    new SqlParameter("@nT_tien_nt2", TinhTong(AD, "TIEN_NT2")),
                    new SqlParameter("@nT_tien2", TinhTong(AD, "TIEN2")),
                    new SqlParameter("@Advance", "1=1"),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@lad01", lstt_rec0),
                    new SqlParameter("@lad02", lma_vt),
                    new SqlParameter("@lad03", lso_luong1),
                    new SqlParameter("@lad04", lgia_nt21),
                    new SqlParameter("@lad05", ltien_nt2),
                    new SqlParameter("@lad06", ldvt1),
                    new SqlParameter("@lad07", lgia_nt2),
                    new SqlParameter("@lad08", ltien2),
                    new SqlParameter("@lad09", lgia2),
                    new SqlParameter("@lad10", lma_kho_i),
                    new SqlParameter("@lad11", lso_luong),
                    new SqlParameter("@lad12", lma_lo),//ma_lo
                    new SqlParameter("@lad13", lhsd),//hsd
                    new SqlParameter("@lad14", lma_vitri),//ma_vitri
                    new SqlParameter("@Advance2", "1=1"),
                };
                DataSet dsctkm = V6BusinessHelper.ExecuteProcedure("VPA_Get_ALKMB", plist);
                DataTable ctkm1 = dsctkm.Tables[0];
                DataTable ctck1 = dsctkm.Tables[1];

                DataTable ctkm1th = dsctkm.Tables[2];
                DataTable ctck1th = dsctkm.Tables[3];

                //Hiển thị chọn chương trình. [Tag]
                if ((V6Options.M_SOA_TINH_CK_KM == "02" || V6Options.M_SOA_TINH_CK_KM == "12")
                    && (ctkm1th.Rows.Count + ctck1th.Rows.Count > 1))
                {
                    new ChonKhuyenMaiForm(dsctkm).ShowDialog(this);
                }

                l_ma_km = ";";
                //Áp dụng khuyến mãi.
                if (ctkm1 != null && ctkm1.Rows.Count > 0)
                {
                    ApDungKhuyenMai(ctkm1);
                }

                //Áp dụng chiết khấu.
                if (ctck1 != null && ctck1.Rows.Count > 0)
                {
                    ApDungChietKhau(ctck1);
                }

                //Áp dụng khuyến mãi tổng hợp.
                if (ctkm1th != null && ctkm1th.Rows.Count > 0)
                {
                    ApDungKhuyenMaiTH(ctkm1th);
                }

                //Áp dụng chiết khấu tổng hợp.
                if (ctck1th != null && ctck1th.Rows.Count > 0)
                {
                    ApDungChietKhauTH(ctck1th);
                }

                //bỏ dấu ; ở 2 đầu chuỗi
                l_ma_km = l_ma_km.Trim(new[] { ';' });
                TxtL_AM_INFO.Text = l_ma_km;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhChietKhauKhuyenMai " + _sttRec, ex);
            }
        }
        
        private void ApDungChietKhauTH(DataTable ctck1th)
        {
            try
            {
                decimal tong_giam = 0m, tong_giam_nt = 0m;
                string MA_KM = "";


                foreach (DataRow row in ctck1th.Rows)
                {
                    var tag = (row["tag"] ?? "").ToString().Trim();
                    if (tag == "") continue;
                    tong_giam += ObjectAndString.ObjectToDecimal(row["T_GG"]);
                    tong_giam_nt += ObjectAndString.ObjectToDecimal(row["T_GG_NT"]);
                    if (ObjectAndString.ObjectToDecimal(row["T_GG"]) != 0)
                    {
                        MA_KM = ObjectAndString.ObjectToString(row["MA_KM"]).Trim();

                        if (!l_ma_km.Contains(";" + MA_KM + ";"))
                        {
                            l_ma_km = l_ma_km + MA_KM + ";";
                        }
                    }
                }

                foreach (DataRow ad_row in AD.Rows)
                {
                    if ((ad_row["MA_KMB"] ?? "").ToString().Trim() == "")
                    {
                        ad_row["MA_KMB"] = MA_KM;
                    }
                }

                if (tong_giam != 0)
                {
                    txtTongGiam.Value = tong_giam;
                    txtTongGiamNt.Value = tong_giam_nt;

                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungChietKhauTH " + _sttRec, ex);
            }
        }

        private void ApDungKhuyenMaiTH(DataTable ctkm1th)
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungKhuyenMaiTH " + _sttRec, ex);
            }
        }

        private void ApDungKhuyenMai(DataTable datakmct)
        {
            try
            {
                if (datakmct != null && datakmct.Rows.Count > 0)
                    ShowMainMessage(V6Text.Text("COKM"));
                else return;

                foreach (DataRow row in datakmct.Rows)
                {
                    var tag = (row["tag"] ?? "").ToString().Trim();
                    if (tag == "") continue;

                    var CK_MA_KM = row["MA_KM"].ToString().Trim().ToUpper();
                    if (!l_ma_km.Contains(";" + CK_MA_KM + ";"))
                    {
                        l_ma_km = l_ma_km + CK_MA_KM + ";";
                    }

                    var data = GenDataKM(row.ToDataDictionary());
                    if (IsOkDataKM(data)) XuLyThemDetail(data);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungKhuyenMai " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu khuyến mãi.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsOkDataKM(IDictionary<string, object> data)
        {
            if (!data.ContainsKey("MA_VT") || data["MA_VT"] == null || data["MA_VT"].ToString().Trim() == "")
                return false;
            if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"] == null || data["MA_KHO_I"].ToString().Trim() == "")
                return false;

            return true;
        }

        /// <summary>
        /// Áp dụng kết quả chiết khấu vào dữ liệu chi tiết.
        /// </summary>
        /// <param name="ctck1">Dữ liệu chiết khấu.</param>
        private void ApDungChietKhau(DataTable ctck1)
        {
            try
            {
                Boolean chietkhau_yn = false;

                foreach (DataRow ck_row in ctck1.Rows)
                {
                    var tag = (ck_row["tag"] ?? "").ToString().Trim();
                    if (tag == "") continue;

                    ////Sửa thành chiết khấu riêng.
                    //if (chkLoaiChietKhau.Checked)
                    //{
                    //    ShowParentMessage("Chiết khấu khuyến mãi không áp dụng khi đặt chiết khấu chung.");
                    //    chkLoaiChietKhau.Focus();
                    //    return;
                    //}

                    var CK_MA_KM = ck_row["MA_KM"].ToString().Trim().ToUpper();
                    var ck_stt_rec0 = ck_row["STT_REC0"].ToString().Trim();
                    var pt_ck = ObjectAndString.ObjectToDecimal(ck_row["PT_CK"]);
                    var ck = ObjectAndString.ObjectToDecimal(ck_row["CK"]);


                    if (pt_ck == 0 && ck == 0) continue;

                    foreach (DataRow ad_row in AD.Rows)
                    {
                        var ad_stt_rec0 = ad_row["STT_REC0"].ToString().Trim();

                        if (ck_stt_rec0 == ad_stt_rec0)
                        {
                            if (pt_ck != 0)
                            {
                                ad_row["PT_CKI"] = pt_ck;
                                //Tinh tien ck
                                if (ck == 0)
                                {
                                    ad_row["CK"] = V6BusinessHelper.Vround(
                                        ObjectAndString.ObjectToDecimal(ad_row["TIEN2"]) * pt_ck / 100, M_ROUND);

                                    ad_row["ck_Nt"] = V6BusinessHelper.Vround(
                                        ObjectAndString.ObjectToDecimal(ad_row["TIEN_NT2"]) * pt_ck / 100, M_ROUND_NT);
                                    ad_row["CK"] = V6BusinessHelper.Vround(
                                        ObjectAndString.ObjectToDecimal(ad_row["CK_NT"]) * txtTyGia.Value, M_ROUND);

                                    if (_maNt == _mMaNt0)
                                    {
                                        ad_row["CK"] = ad_row["ck_Nt"];
                                    }
                                    ad_row["MA_KMB"] = CK_MA_KM;
                                    ad_row["AUTO_YN"] = "1";
                                    chietkhau_yn = true;

                                    if (!l_ma_km.Contains(";" + CK_MA_KM + ";"))
                                    {
                                        l_ma_km = l_ma_km + CK_MA_KM + ";";
                                    }

                                }
                            }

                            if (ck != 0)
                            {
                                ad_row["CK"] = ck;
                                ad_row["MA_KMB"] = CK_MA_KM;
                                ad_row["AUTO_YN"] = "1";
                                chietkhau_yn = true;

                                if (!l_ma_km.Contains(";" + CK_MA_KM + ";"))
                                {
                                    l_ma_km = l_ma_km + CK_MA_KM + ";";
                                }
                            }
                        }
                    }
                }

                if (chietkhau_yn)
                {
                    chkLoaiChietKhau.Checked = false;// Co CK rieng set lai

                }
                dataGridView1.DataSource = AD;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungChietKhau " + _sttRec, ex);
            }
        }

        private void XoaKhuyenMai()
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                try
                {
                    var removeList = new List<DataRow>();
                    bool khuyenmai_yn = false;
                    foreach (DataRow row in AD.Rows)
                    {
                        if (IsKhuyenMai(row))
                        {
                            removeList.Add(row);
                            khuyenmai_yn = true;
                        }
                    }
                    if (khuyenmai_yn)
                    {
                        foreach (DataRow row in removeList)
                        {
                            AD.Rows.Remove(row);
                        }
                        TxtL_AM_INFO.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".XoaKhuyenMai " + _sttRec, ex);
                }
        }

        private void XoaChietKhau()
        {
            if (NotAddEdit)
            {
                return;
            }
            //Xoa theo stt_rec0, ma_kmb, gan ck = 0, pt_cki = 0;
            //Tính lại tiền.
            bool chietkhau_yn = false;

            foreach (DataRow row in AD.Rows)
            {
                string ma_kmb = (row["MA_KMB"] ?? "").ToString().Trim();
                if (ma_kmb != "")
                {
                    row["MA_KMB"] = "";
                    row["PT_CKI"] = 0m;
                    row["CK"] = 0m;
                    row["CK_NT"] = 0m;
                    row["GG"] = 0m;
                    row["GG_NT"] = 0m;
                    row["AUTO_YN"] = "0";
                    chietkhau_yn = true;
                }
            }

            if (chietkhau_yn)
            {
                txtTongCk.Value = 0;
                txtTongCkNt.Value = 0;
                txtTongGiam.Value = 0;
                txtTongGiamNt.Value = 0;
                TxtL_AM_INFO.Text = "";
            }
        }

        private bool IsKhuyenMai(DataRow row)
        {
            if (row.Table.Columns.Contains(MA_KM_Field)
                && (row[MA_KM_Field] ?? "").ToString().Trim() != ""
                && row["TANG"].ToString().Trim().ToLower() == "a"
                && row["AUTO_YN"].ToString().Trim() == "1"
                )
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Thêm bớt sửa đổi dữ liệu km trước khi thêm.
        /// </summary>
        /// <param name="dataDic"></param>
        /// <returns></returns>
        private IDictionary<string, object> GenDataKM(IDictionary<string, object> dataDic)
        {
            //toDataDictionary["fix_value"] = "fix_value";
            dataDic["AUTO_YN"] = "1";
            return dataDic;
        }

        #endregion tinh khuyen mai

        private void xuLyKhacMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotAddEdit) return;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                ReportR45db2SelectorForm r45Selector = new ReportR45db2SelectorForm(Invoice);
                if (r45Selector.ShowDialog(this) == DialogResult.OK)
                {
                    chonExcel_AcceptData(r45Selector.dataGridView1.GetSelectedData());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void txtManx_Leave(object sender, EventArgs e)
        {
            if (NotAddEdit) return;

            if (chkSuaTkThue.Checked)
            {
                if (txtTkThueNo.Text.Trim() == "") txtTkThueNo.Text = txtManx.Text;
            }
            else
            {
                txtTkThueNo.Text = txtManx.Text;
            }
        }

        private void thayTheMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(Invoice);
        }

        private void thayTheNhieuMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(Invoice, true);
        }

        private void thayThe2Menu_Click(object sender, EventArgs e)
        {
            ChucNang_SuaNhieuDong(Invoice);
        }

        private void chonBaoGiaMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonBaoGia(shift);
        }

        private void ChucNang_ChonBaoGia(bool add = false)
        {
            try
            {
                if (NotAddEdit) return;

                chon_accept_flag_add = add;
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                if (ma_dvcs != "")
                {
                    CBG_HoaDonForm chon = new CBG_HoaDonForm(txtMaDVCS.Text, txtMaKh.Text);
                    _chon_px = "SOR";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_dvcs == "") message += V6Text.NoInput + lblMaDVCS.Text;
                    this.ShowWarningMessage(message);
                    if (ma_dvcs == "") txtMaDVCS.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ChonBaoGia " + _sttRec, ex);
            }
        }

        void chon_AcceptSelectEvent(List<IDictionary<string, object>> selectedDataList, ChonEventArgs e)
        {
            try
            {
                //detail1.MODE = V6Mode.View;
                dataGridView1.UnLock();
                txtLoaiCt.Text = e.Loai_ct;
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }
                int addCount = 0, failCount = 0; _message = "";
                string ma_kh_soh = null;
                var AM_somedata = new Dictionary<string, object>();
                var ad2am_dic = ObjectAndString.StringToStringDictionary(e.AD2AM, ',', ':');
                foreach (IDictionary<string, object> data in selectedDataList)
                {
                    // Lấy ma_kh_soh đầu tiên.
                    if (ma_kh_soh == null && data.ContainsKey("MA_KH_SOH"))
                    {
                        ma_kh_soh = data["MA_KH_SOH"].ToString().Trim();
                    }
                    
                    foreach (KeyValuePair<string, string> item in ad2am_dic)
                    {
                        if (data.ContainsKey(item.Key) && !AM_somedata.ContainsKey(item.Value.ToUpper()))
                        {
                            AM_somedata[item.Value.ToUpper()] = data[item.Key.ToUpper()];
                        }
                    }

                    string c_makh = data.ContainsKey("MA_KH") ? data["MA_KH"].ToString().Trim().ToUpper() : "";
                    if (c_makh != "" && txtMaKh.Text == "")
                    {
                        txtMaKh.ChangeText(c_makh);
                    }

                    if (c_makh != "" && c_makh != txtMaKh.Text.ToUpper())
                    {
                        failCount++;
                        _message += ". " + failCount + ":" + c_makh;
                        continue;
                    }

                    var newData = new SortedDictionary<string, object>(data);
                    if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                    {
                        newData["MA_TD_I"] = Txtma_td_ph.Text;
                    }

                    if (XuLyThemDetail(newData))
                    {
                        addCount++;
                        All_Objects["data"] = newData;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                    else failCount++;
                }

                if (!string.IsNullOrEmpty(ma_kh_soh))
                {
                    if (txtMaKh.Text == "")
                    {
                        txtMaKh.ChangeText(ma_kh_soh);
                        txtMaKh.CallLeave();
                    }
                }
                
                if (!string.IsNullOrEmpty(e.AD2AM))
                {
                    SetSomeData(AM_somedata);
                }
                All_Objects["selectedDataList"] = selectedDataList;
                InvokeFormEvent("AFTERCHON_" + _chon_px);
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed: {1}{2}", addCount, failCount, _message));
                //if (addCount > 0)
                //{
                //    co_chon_don_hang = true;
                //}
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void cboKieuPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");
        }

        private void txtDiaChi2_Enter(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (txtDiaChi2.ReadOnly) return;
                var data = txtMaKh.Data;
                if (data == null)
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaKH.Text, 300);
                    return;
                }
                txtDiaChi2.ParentData = data.ToDataDictionary();
                txtDiaChi2.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
            }
        }

        private void chkTempSuaCT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTempSuaCT.Checked)
            {
                SetGridViewReadonly(dataGridView1, Invoice);
            }
            else
            {
                dataGridView1.ReadOnly = true;
            }
        }

        private void menuChucNang_Paint(object sender, PaintEventArgs e)
        {
            FixMenuChucNangItemShiftText(chonBaoGiaMenu, chonDonHangMuaMenu, chonTuExcelMenu, importXmlMenu);
        }

        private void inPhieuHachToanMenu_Click(object sender, EventArgs e)
        {
            InPhieuHachToan(Invoice, _sttRec, TongThanhToan, TongThanhToanNT);
        }

        private void txtLoaiNX_PH_V6LostFocus(object sender)
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                var grow = dataGridView1.CurrentRow;
                if (txtLoaiNX_PH.Text != string.Empty)
                {
                    V6ControlFormHelper.UpdateDKlist(AD, "MA_LNX_I", txtLoaiNX_PH.Text);
                    SetCellValue(grow.Cells["MA_LNX_I"], txtLoaiNX_PH.Text);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void exportXmlMenu_Click(object sender, EventArgs e)
        {
            ExportXml();
        }

        private void importXmlMenu_Click(object sender, EventArgs e)
        {
            ImportXml();
        }

        private void timTopCuoiKyMenu_Click(object sender, EventArgs e)
        {
            Tim("1");
        }

        private void chonALVTMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotAddEdit) return;
                var grow = dataGridView1.CurrentRow;
                if (grow == null) return;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                //var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                string filter1 = GetCellTag(grow.Cells["MA_VT"], "INITFILTER").ToString();
                var setting = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MAKH2MAVT"));
                if (setting.Contains(Invoice.Mact))
                    
                {
                    string newFilter = Invoice.GetMaVtFilterByMaKH(txtMaKh.Text, txtMaDVCS.Text);
                    if (string.IsNullOrEmpty(filter1))
                    {
                        filter1 = newFilter;
                    }
                    else if (!string.IsNullOrEmpty(newFilter) && ! GetCellTag(grow.Cells["MA_VT"], "INITFILTER").ToString().Contains(newFilter))
                    {
                        filter1 = string.Format("({0}) and ({1})", filter1, newFilter);
                    }
                };

                var form = new AlvtSelectorForm(Invoice, filter1);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //chonAlvt_AcceptData((DataTable)form.dataGridView2.DataSource, detail1, _maVt, txtTyGia.Value, dataGridView1);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void inKhacMenu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.F8)
            //    {
            //        XuLyXoaDetail();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            //}
            //e.SuppressKeyPress = true;
            //int iColumn = dataGridView1.CurrentCell.ColumnIndex;
            //int iRow = dataGridView1.CurrentCell.RowIndex;
            //if (iColumn == dataGridView1.ColumnCount - 1)
            //{
            //    if (dataGridView1.RowCount > (iRow + 1))
            //    {
            //        dataGridView1.CurrentCell = dataGridView1[1, iRow + 1];
            //    }
            //    else
            //    {
            //        //focus next control
            //    }
            //}
            //else
            //    dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
        }


        


    }
}
