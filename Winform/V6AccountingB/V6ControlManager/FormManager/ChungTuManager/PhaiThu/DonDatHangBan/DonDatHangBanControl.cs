using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using V6Controls.Forms;
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
    public partial class DonDatHangBanControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice91 Invoice = new V6Invoice91();

        private string l_ma_km = "";
        private string _m_Ma_td;
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public DonDatHangBanControl()
        {
            InitializeComponent();
            MyInit();
        }
        public DonDatHangBanControl(string itemId)
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
        public DonDatHangBanControl(string maCt, string itemId, string sttRec)
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
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            LoadTag(Invoice, detail1.Controls);
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        private V6ColorTextBox _dvt;
        private V6CheckTextBox _tang, _xuat_dd;
        private V6VvarTextBox _maVt, _dvt1, _maKho, _maKhoI, _tkDt, _tkGv, _tkCkI, _tkVt, _maLo, _ma_thue_i, _tk_thue_i;
        private V6NumberTextBox _soLuong1, _soLuong, _he_so1T, _he_so1M, _giaNt2, _giaNt21,_tien2, _tienNt2, _ck, _ckNt,_gia2,_gia21;
        private V6NumberTextBox _ton13, _ton13Qd, _gia, _gia_nt, _tien, _tienNt, _pt_cki, _thue_suat_i, _thue_nt, _thue;
        private V6NumberTextBox _sl_qd, _sl_qd2, _tien_vcNt, _tien_vc, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4, _ggNt, _gg;
        private V6DateTimeColor _hanSd;
        
        private void LoadDetailControls()
        {
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);
            
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<int, AlctControls> item in dynamicControlList)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);
                
                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects);

                switch (NAME)
                {
                    case "MA_VT":
                        _maVt = (V6VvarTextBox) control;
                        _maVt.Upper();
                        _maVt.LO_YN = false;
                        _maVt.DATE_YN = false;

                        _maVt.BrotherFields = "ten_vt,ten_vt2,dvt,ma_kho,ma_qg,ma_vitri";
                   
                        _maVt.V6LostFocus += MaVatTu_V6LostFocus;

             
                        _maVt.V6LostFocusNoChange += delegate
                        {
                            if (_maVt.LO_YN)
                            {
                                _maLo.Enabled = true;
                            }
                            else
                            {
                                _maLo.Enabled = false;
                            }
                            GetTon13();
                        };
                        break;
                    case "TK_DT":
                        _tkDt = (V6VvarTextBox)control;
                        _tkDt.Upper();
                        _tkDt.SetInitFilter("Loai_tk = 1");
                        _tkDt.FilterStart = true;
                        break;
                    case "TK_GV":
                        _tkGv = (V6VvarTextBox)control;
                        _tkGv.Upper();
                        _tkGv.SetInitFilter("Loai_tk = 1");
                        _tkGv.FilterStart = true;
                        break;
                    case "TK_CKI":
                        _tkCkI = (V6VvarTextBox)control;
                        _tkCkI.Upper();
                        _tkCkI.SetInitFilter("Loai_tk = 1");
                        _tkCkI.FilterStart = true;
                        break;
                    case "TK_VT":
                        _tkVt = (V6VvarTextBox)control;
                        _tkVt.Upper();
                        _tkVt.SetInitFilter("Loai_tk = 1");
                        _tkVt.FilterStart = true;
                        break;
                    case "DVT1":
                        _dvt1 = (V6VvarTextBox)control;
                        _dvt1.Upper();
                        _dvt1.SetInitFilter("");
                        _dvt1.BrotherFields = "ten_dvt";
                        _dvt1.V6LostFocus += Dvt1_V6LostFocus;
                        _dvt1.V6LostFocusNoChange += Dvt1_V6LostFocusNoChange;
                        _dvt1.GotFocus += (s, e) =>
                        {
                            _dvt1.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");
                            _dvt1.ExistRowInTable(true);
                        };
                        break;
                    case "DVT":
                        _dvt = (V6ColorTextBox)control;
                        _dvt.Tag = "hide";
                        break;
                    case "MA_KHO":
                        _maKho = (V6VvarTextBox)control;
                        _maKho.Upper();
                        _maKho.V6LostFocus += MaKhoV6LostFocus;
                        _maKho.Tag = "hide";
                        break;
                    case "MA_KHO_I":
                        _maKhoI = (V6VvarTextBox)control;
                        _maKhoI.Upper();
                        _maKhoI.LO_YN = false;
                        _maKhoI.DATE_YN = false;

                        _maKhoI.V6LostFocus += MaKhoI_V6LostFocus;
                        break;
                    case "MA_THUE_I":
                        _ma_thue_i = control as V6VvarTextBox;
                        if (_ma_thue_i != null)
                        {
                            _ma_thue_i.V6LostFocus += delegate
                            {
                                XuLyThayDoiMaThue_i();
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    case "TK_THUE_I":
                        _tk_thue_i = control as V6VvarTextBox;
                        break;
                    case "THUE_SUAT_I":
                        _thue_suat_i = control as V6NumberTextBox;
                        break;
                    case "THUE":
                        _thue = control as V6NumberTextBox;
                        break;
                    case "THUE_NT":
                        _thue_nt = control as V6NumberTextBox;
                        if (_thue_nt != null)
                        {
                            _thue_nt.V6LostFocus += delegate
                            {
                                Tinh_TienThue_TheoTienThueNt(_thue_nt.Value, txtTyGia.Value, _thue, M_ROUND);
                            };
                        }
                        break;
                    case "TON13":
                        _ton13 = (V6NumberTextBox)control;
                        if (_ton13.Tag == null || _ton13.Tag.ToString() != "hide") _ton13.Tag = "disable";
                        break;
                    case "TON13QD":
                        _ton13Qd = control as V6NumberTextBox;
                        if (_ton13Qd.Tag == null || _ton13Qd.Tag.ToString() != "hide")
                        {
                            _ton13Qd.Tag = "disable";
                        }
                        break;
                    case "SO_LUONG1":
                        _soLuong1 = (V6NumberTextBox)control;
                        _soLuong1.V6LostFocus += SoLuong1_V6LostFocus;
                        _soLuong1.V6LostFocusNoChange += delegate
                        {
                            
                        };
                        if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        {
                            _soLuong1.ReadOnlyTag();
                        }
                        break;

                    case "SO_LUONG":
                        _soLuong = (V6NumberTextBox)control;
                        _soLuong.Tag = "hide";
                        break;
                    case "HE_SO1T":
                        _he_so1T = (V6NumberTextBox)control;
                        _he_so1T.Tag = "hide";
                        _he_so1T.DecimalPlaces = Invoice.ADStruct.ContainsKey("HE_SO1T")
                            ? Invoice.ADStruct["HE_SO1T"].MaxNumDecimal
                            : 6;
                        _he_so1T.StringValueChange += (sender, args) =>
                        {
                            if (_he_so1T.Value == 0)
                            {
                                _he_so1T.Value = 1;
                                return;
                            }
                            if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit) && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                            {
                                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                            }
                        };
                        break;
                    case "HE_SO1M":
                        _he_so1M = (V6NumberTextBox)control;
                        _he_so1M.Tag = "hide";
                        _he_so1M.DecimalPlaces = Invoice.ADStruct.ContainsKey("HE_SO1M")
                            ? Invoice.ADStruct["HE_SO1M"].MaxNumDecimal
                            : 6;
                        _he_so1M.StringValueChange += (sender, args) =>
                        {
                            if (_he_so1M.Value == 0)
                            {
                                _he_so1M.Value = 1;
                                return;
                            }
                            if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit) && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                            {
                                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                            }
                        };
                        break;
                    case "GIA_NT2":
                        _giaNt2 = (V6NumberTextBox)control;
                        break;
                    case "GIA2":
                        _gia2 = (V6NumberTextBox)control;
                        break;
                    case "GIA21":
                        _gia21 = (V6NumberTextBox)control;
                        break;
                    case "GIA_NT21":
                        _giaNt21 = control as V6NumberTextBox;
                        if (_giaNt21 != null)
                        {
                            _giaNt21.V6LostFocus += GiaNt21_V6LostFocus;
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _giaNt21.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _giaNt21.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN_NT2":
                        _tienNt2 = (V6NumberTextBox)control;
                        if (_tienNt2 != null)
                        {
                            _tienNt2.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _tienNt2.Tag = null;
                            }
                            else
                            {
                                if (_tienNt2.Tag == null || _tienNt2.Tag.ToString() != "hide") _tienNt2.Tag = "disable";
                            }

                            _tienNt2.V6LostFocus += TienNt2_V6LostFocus;
                        }
                        else
                        {
                        }
                        break;
                    case "TIEN2":
                        _tien2 = (V6NumberTextBox)control;
                        break;

                    //_tien2.V6LostFocus;
                    case "TIEN":
                        _tien = (V6NumberTextBox)control;
                        if (_tien != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN_NT":
                        _tienNt = control as V6NumberTextBox;
                        if (_tienNt != null)
                        {
                            _tienNt.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _tienNt.Tag = null;
                            }
                            else
                            {
                                if (_tienNt.Tag == null || _tienNt.Tag.ToString() != "hide") _tienNt.Tag = "disable";
                            }

                            _tienNt.V6LostFocus += delegate
                            {
                                TinhGiaVon();
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tienNt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tienNt.ReadOnlyTag();
                            }
                        }
                        break;
                    case "CK":
                        _ck = (V6NumberTextBox)control;
                        break;
                    //_tien2.V6LostFocus;
                    case "CK_NT":
                        _ckNt = control as V6NumberTextBox;
                        if (_ckNt != null)
                        {
                            _ckNt.V6LostFocus += delegate
                            {
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    case "PT_CKI":
                        _pt_cki = control as V6NumberTextBox;
                        if (_pt_cki != null)
                        {
                            _pt_cki.Enabled = !chkLoaiChietKhau.Checked;
                            
                            _pt_cki.V6LostFocus += delegate
                            {
                                TinhChietKhauChiTiet();
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    case "GIA_NT":
                        _gia_nt = control as V6NumberTextBox;
                        if (_gia_nt != null)
                        {
                            _gia_nt.V6LostFocus += delegate
                            {
                                TinhTienVon();
                            };
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia_nt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia_nt.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA":
                        _gia = control as V6NumberTextBox;
                        if (_gia != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TANG":
                        _tang = (V6CheckTextBox)control;
                        _tang.V6LostFocus += _tang_V6LostFocus;
                        break;
                    case "PX_GIA_DDI":
                        _xuat_dd = (V6CheckTextBox)control;
                        _xuat_dd.TextChanged += delegate
                        {
                            if (_xuat_dd.Text != "")
                            {
                                _gia_nt.Enabled = true;
                                if (chkSuaTien.Checked)
                                    _tienNt.Enabled = true;
                                else _tienNt.Enabled = false;
                            }
                            else
                            {
                                _gia_nt.Enabled = false;
                                _tienNt.Enabled = false;
                            }
                        };
                        break;
                    case "MA_LO":

                        _maLo = (V6VvarTextBox)control;
                        _maLo.V6LostFocus += _maLo_V6LostFocus;

                        _maLo.V6LostFocusNoChange += delegate
                        {
                            XuLyLayThongTinKhiChonMaLo();
                       
                        };
                        _maLo.GotFocus += (s, e) =>
                        {
                            _maLo.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");
                        };
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        if (_hanSd.Tag == null || _hanSd.Tag.ToString() != "hide") _hanSd.Tag = "disable";
                        break;
                    //{ Tuanmh 01/01/2017
                    case "SL_QD":
                        _sl_qd = control as V6NumberTextBox;
                        if (_sl_qd != null)
                        {
                            if (M_CAL_SL_QD_ALL == "0")
                            {
                                if (M_TYPE_SL_QD_ALL == "00")
                                {
                                    _sl_qd.Enabled = false;
                                    if (_sl_qd.IsVisibleTag()) _sl_qd.DisableTag();
                                }
                            }
                            else if (M_CAL_SL_QD_ALL == "1")
                            {
                                _sl_qd.EnableTag();
                            }
                            _sl_qd.V6LostFocus += delegate
                            {
                                TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                                TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                                TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_qd);
                                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;

                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    //CheckSoLuong1();
                                    chkT_THUE_NT.Checked = false;
                                    Tinh_thue_ct();
                                }
                                
                            };

                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _sl_qd.ReadOnlyTag();
                            }
                        }
                        break;
                    case "SL_QD2":
                        _sl_qd2 = (V6NumberTextBox)control;
                        _sl_qd2.Enabled = false;
                        if (_sl_qd2.Tag == null || _sl_qd2.Tag.ToString() != "hide") _sl_qd2.Tag = "disable";
                        break;
                    case "HS_QD1":
                        _hs_qd1 = (V6NumberTextBox)control;
                        _hs_qd1.Enabled = false;
                        if (_hs_qd1.Tag == null || _hs_qd1.Tag.ToString() != "hide") _hs_qd1.Tag = "disable";
                        break;
                    case "HS_QD2":
                        _hs_qd2 = (V6NumberTextBox)control;
                        _hs_qd2.Enabled = false;
                        if (_hs_qd2.Tag == null || _hs_qd2.Tag.ToString() != "hide") _hs_qd2.Tag = "disable";
                        break;
                    case "HS_QD3":
                        _hs_qd3 = (V6NumberTextBox)control;
                        _hs_qd3.V6LostFocus += Hs_qd3_V6LostFocus;
                        break;
                    case "HS_QD4":
                        _hs_qd4 = (V6NumberTextBox)control;
                        _hs_qd4.V6LostFocus += Hs_qd4_V6LostFocus;
                        break;
                    case "GG_NT":
                        _ggNt = control as V6NumberTextBox;
                        if (_ggNt != null)
                        {
                            _ggNt.V6LostFocus += delegate
                            {
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    case "GG":
                        _gg = control as V6NumberTextBox;
                        if (_gg != null)
                        {
                            _gg.V6LostFocus += delegate
                            {
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    case "TIEN_VC_NT":
                        _tien_vcNt = (V6NumberTextBox)control;
                        break;
                    case "TIEN_VC":
                        _tien_vc = (V6NumberTextBox)control;
                        break;
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
            }

            foreach (AlctControls item in dynamicControlList.Values)
            {
                detail1.AddControl(item);
            }
            
            detail1.SetStruct(Invoice.ADStruct);
            detail1.MODE = detail1.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
        }

        void _maLo_V6LostFocus(object sender)
        {
            CheckMaLo();
        }

        private void CheckMaLo()
        {
            if (_maVt.Text != "")
            {
                _maLo.SetInitFilter("Ma_vt='" + _maVt.Text.Trim()+"'");
            }
            XuLyLayThongTinKhiChonMaLo();
            
        }

        void _tang_V6LostFocus(object sender)
        {
            if (_tang.Text.Trim() != "")
            {
                SetTang();
            }
            else
            {
                GetGia();
            }
        }

        private void SetTang()
        {
            try
            {
                _giaNt2.Value = 0;
                _giaNt21.Value = 0;
                _tienNt2.Value = 0;
                _tien2.Value = 0;
                _ck.Value = 0;
                _ckNt.Value = 0;
                _gia2.Value = 0;
                _gia21.Value = 0;
                _gg.Value = 0;
                _ggNt.Value = 0;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        
        
        #endregion detail form

        #region ==== Override Methods ====

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese ? "F4-Nhận/thêm chi tiết, F7-Lưu và in, F8-Xóa chi tiết" : "F4-Add detail, F7-Save and print, F8-Delete detail");
        }
        
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
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    detail1.btnNhan.Focus();
                    detail1.btnNhan.PerformClick();
                }
                //else if (detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
                //{
                //    detail3.btnNhan.PerformClick();
                //}
                else
                {
                    btnLuu.PerformClick();
                }
            }
            else if (keyData == Keys.Escape)
            {
                if (detail1.MODE == V6Mode.Add)
                {
                    if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                    detail1.btnMoi.PerformClick();
                }
                else if (detail1.MODE == V6Mode.Edit)
                {
                    if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                    detail1.btnSua.PerformClick();
                }
                else
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
            else if (keyData == Keys.F3) // Copy detail
            {
                if (NotAddEdit)
                {
                    ShowParentMessage(V6Text.PreviewingMode);
                    return false;
                }

                detail1.btnNhan.Focus();
                if (detail1.MODE == V6Mode.Add)
                {
                    var detailData = detail1.GetData();
                    if (ValidateData_Detail(detailData))
                    {
                        if (XuLyThemDetail(detailData))
                        {
                            ShowParentMessage(V6Text.InvoiceF3AddDetailSuccess);
                        }
                    }
                }
                else if (detail1.MODE == V6Mode.Edit)
                {
                    var detailData = detail1.GetData();
                    if (ValidateData_Detail(detailData))
                    {
                        if (XuLySuaDetail(detailData))
                        {
                            // Chuyển detail1 từ mode Edit qua mod Add không thay đổi data.
                            detail1._mode = V6Mode.Add;
                            detail1.btnSua.Image = Properties.Resources.Pencil16;
                            detail1.btnMoi.Image = Properties.Resources.Cancel16;
                            detail1.toolTip1.SetToolTip(btnMoi, V6Text.Cancel);
                            detail1.btnMoi.Enabled = true;
                            detail1.btnSua.Enabled = false;
                            detail1.btnXoa.Enabled = false;
                            detail1.btnNhan.Enabled = true;
                            detail1.btnChucNang.Enabled = true;

                            ShowParentMessage(V6Text.InvoiceF3EditDetailSuccess);
                        }
                    }
                }
                else
                {
                    detail1._mode = V6Mode.Add;
                    detail1.AutoFocus();
                    detail1.SetFormControlsReadOnly(false);
                    detail1.btnMoi.Image = Properties.Resources.Cancel16;
                    detail1.toolTip1.SetToolTip(btnMoi, V6Text.Cancel);
                    detail1.btnMoi.Enabled = true;
                    detail1.btnSua.Enabled = false;
                    detail1.btnXoa.Enabled = false;
                    detail1.btnNhan.Enabled = true;
                    detail1.btnChucNang.Enabled = true;
                }
            }
            else if (keyData == Keys.F4)
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    detail1.btnNhan.Focus();
                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        var detailData = detail1.GetData();
                        if (ValidateData_Detail(detailData))
                        {
                            detail1.btnNhan.Focus();
                            detail1.btnNhan.PerformClick();
                        }
                    }

                    if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit)
                    {
                        detail1.OnMoiClick();
                    }
                }
            }
            else if (keyData == Keys.F7)
            {
                LuuVaIn();
            }
            else if (keyData == Keys.F8)
            {
                if (detail1.MODE == V6Mode.View && detail1.btnXoa.Enabled && detail1.btnXoa.Visible)
                    detail1.btnXoa.PerformClick();
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
        
        void MaKhoI_V6LostFocus(object sender)
        {
            XuLyChonMaKhoI();
        }
        
        void MaKhoV6LostFocus(object sender)
        {
            _maKhoI.Text = _maKho.Text;
        }

        private void MaVatTu_V6LostFocus(object sender)
        {
            XuLyChonMaVt(_maVt.Text);
        }
        
        void Dvt1_V6LostFocus(object sender)
        {
            XuLyThayDoiDvt1();
        }
        void Dvt1_V6LostFocusNoChange(object sender)
        {
            _dvt1.ExistRowInTable(true);
            if (_dvt1.Data != null)
            {
                var he_soT = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_soT"]);
                var he_soM = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_soM"]);
                if (he_soT == 0) he_soT = 1;
                if (he_soM == 0) he_soM = 1;
                if (_he_so1T.Value != he_soT) _he_so1T.Value = he_soT;
                if (_he_so1M.Value != he_soM) _he_so1M.Value = he_soM;
            }
            else
            {
                if (_he_so1T.Value != 1) _he_so1T.Value = 1;
                if (_he_so1M.Value != 1) _he_so1M.Value = 1;
            }
        }

        public void TienNt2_V6LostFocus(object sender)
        {
            TinhGiaNt2_TienNt2();
        }

        void SoLuong1_V6LostFocus(object sender)
        {
            TinhTienNt2(_soLuong1);
            Tinh_thue_ct();
        }

        public void GiaNt21_V6LostFocus(object sender)
        {
            chkT_THUE_NT.Checked = false;
            TinhTienNt2(_giaNt21);
            Tinh_thue_ct();
        }

        void Hs_qd4_V6LostFocus(object sender)
        {
            TinhGiamGiaCt();
        }
        void Hs_qd3_V6LostFocus(object sender)
        {
            TinhVanChuyen();
        }
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
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
            GetTon13();
        }
        private void XuLyChonMaVt(string mavt)
        {
            try
            {
                XuLyLayThongTinKhiChonMaVt();
                XuLyDonViTinhKhiChonMaVt(mavt);
                GetGia();
                GetTon13();
                TinhTienNt2();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyLayThongTinKhiChonMaKhoI()
        {
            _maKhoI.RefreshLoDateYnValue();
        }

        private void XuLyLayThongTinKhiChonMaLo()
        {
            try
            {
                _maLo.RefreshLoDateYnValue();
                if (_maVt.LO_YN)
                {
                    if (_maLo.Text.Trim() != "")
                    {
                        var data = _maLo.Data;
                        if (data != null)
                            _hanSd.Value = ObjectAndString.ObjectToDate(data["NGAY_HHSD"]);
                    }
                    else
                    {
                        _hanSd.Value = null;
                    }
                }
                else
                {
                    _hanSd.Value = null;
                }

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private DataTable _dataLoDate;

        private void GetLoDate13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                string maLo = _maLo.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maLo == "") return;

                _dataLoDate = Invoice.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Date);
                if (_dataLoDate.Rows.Count == 0)
                {
                   ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataLoDate.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataLoDate.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();

                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuong_qd = data_soLuong_qd;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuong_qd -= c_soLuong_qd;
                                }
                            }
                        }

                        //if (new_soLuong < 0) new_soLuong = 0;
                        {
                            _ton13.Value = new_soLuong * _he_so1M.Value / _he_so1T.Value;
                            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = new_soLuong_qd;
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void GetLoDate()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;
                
                if (maVt == "" || maKhoI == "") return;

                _dataLoDate = Invoice.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataLoDate.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataLoDate.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataLoDate.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    if (data_maLo == "") continue;
                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                        decimal new_soLuong = data_soLuong;
                        decimal new_soLuong_qd = data_soLuong_qd;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {
                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                    new_soLuong_qd -= c_soLuong_qd;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            _ton13.Value = new_soLuong * _he_so1M.Value / _he_so1T.Value;
                            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = new_soLuong_qd;
                            _maLo.Text = data_row["Ma_lo"].ToString().Trim();
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                        else
                        {
                            ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        public void GetTon13()
        {
            try
            {
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataLoDate = Invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                    if (_dataLoDate != null && _dataLoDate.Rows.Count > 0)
                    {
                        DataRow row0 = _dataLoDate.Rows[0];
                        _ton13.Value = ObjectAndString.ObjectToDecimal(row0["ton00"]);
                        if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = ObjectAndString.ObjectToDecimal(row0["ton00Qd"]);
                    }
                    else
                    {
                        _ton13.Value = 0;
                        if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        public void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
                _maVt.RefreshLoDateYnValue();
                var data = _maVt.Data;
                if (data == null)
                {
                    _tkDt.Text = "";
                    _tkGv.Text = "";
                    _tkCkI.Text = "";
                    _tkVt.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;

                    _ma_thue_i.Text = "";
                    _thue_suat_i.Value = 0;
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = 0 vì maVt.Data == null");
                }
                else
                {
                    SetADSelectMoreControlValue(Invoice, data);
                    //_tkDt.Text = (data["tk_dt"] ?? "").ToString().Trim();
                    //_tkGv.Text = (data["tk_gv"] ?? "").ToString().Trim();
                    SetControlValue(_tkDt, data["tk_dt"], Invoice.GetTemplateSettingAD("TK_DT"));
                    SetControlValue(_tkGv, data["tk_gv"], Invoice.GetTemplateSettingAD("TK_GV"));
                    SetControlValue(_soLuong1, data["PACKS1"], Invoice.GetTemplateSettingAD("PACKS1"));
                    //_tkCkI.Text = (data["tk_ck"] ?? "").ToString().Trim();
                    if (M_SOA_HT_KM_CK == "1")
                    {
                        _tkCkI.Text = (data["tk_ck"] ?? "").ToString().Trim();
                        _tkCkI.EnableTag(true);
                    }
                    else
                    {
                        _tkCkI.Text = "";
                        _tkCkI.EnableTag(false);
                    }

                    _tkVt.Text = (data["tk_vt"] ?? "").ToString().Trim();
                    _hs_qd1.Value = ObjectAndString.ObjectToDecimal(data["HS_QD1"]);
                    _hs_qd2.Value = ObjectAndString.ObjectToDecimal(data["HS_QD2"]);

                    if (M_SOA_MULTI_VAT == "1")
                    {
                        _ma_thue_i.Text = (data["ma_thue"] ?? "").ToString().Trim();
                        _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["thue_suat"]);

                        V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["thue_suat"]);

                        var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                        if (alThue.TotalRows > 0)
                        {
                            _tk_thue_i.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                            if(_tk_thue_i.Text != "") txtTkThueCo.Text = _tk_thue_i.Text;
                        }
                    }
                }
                
                if (_maVt.LO_YN == false)
                {   
                    _maLo.Text = "";
                    _hanSd.Value = null;
                    _maLo.Enabled = false;
                }
                else
                {
                    _maLo.Enabled = true;
                }

                SetDefaultDataDetail(Invoice, detail1.panelControls);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Setinitfilter, readonly-tag...
        /// </summary>
        /// <param name="mavt">Giá trị hiện tại của ô _mavt</param>
        /// <param name="changeMavt">Fix trạng thái của dvt khi sửa focusDvt=false</param>
        private void XuLyDonViTinhKhiChonMaVt(string mavt, bool changeMavt = true)
        {
            try
            {
                //Gán lại dvt và dvt1
                var data = _maVt.Data;
                if (data == null)
                {
                    _dvt.ChangeText("");
                    _dvt1.SetInitFilter("");
                    _dvt1.ChangeText("");
                    return;
                }

                if (changeMavt)
                {
                    _dvt.Text = data["dvt"].ToString().Trim();
                    _dvt1.SetInitFilter("ma_vt='" + mavt + "'");
                    _dvt1.Text = _dvt.Text;
                    _dvt1.ExistRowInTable(true);
                }

                if (data.Table.Columns.Contains("Nhieu_dvt"))
                {
                    var nhieuDvt = data["Nhieu_dvt"].ToString().Trim();
                    if (nhieuDvt == "1")
                    {
                        _dvt1.Tag = null;
                        _dvt1.ReadOnly = false;
                        if (changeMavt)
                        {
                            _he_so1T.Value = 1;
                            _he_so1M.Value = 1;
                        }
                    }
                    else
                    {
                        _dvt1.Tag = "readonly";
                        _dvt1.ReadOnly = true;
                        if (changeMavt) _dvt1.Focus();
                        if (changeMavt)
                        {
                            _he_so1T.Value = 1;
                            _he_so1M.Value = 1;
                        }
                    }
                }
                else
                {
                    _dvt1.ExistRowInTable(_dvt1.Text);
                    _dvt1.Tag = "readonly";
                    _dvt1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void GetGia()
        {
            try
            {
                if (txtMaGia.Text.Trim() == "") return;

                var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), _maVt.Text, _dvt1.Text, "", txtMaGia.Text);
                _giaNt21.Value = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                
                if (_dvt.Text.ToUpper().Trim() == _dvt1.Text.ToUpper().Trim())
                {
                    _giaNt2.Value = _giaNt21.Value;
                }
                else
                {
                    if (_soLuong.Value != 0)
                    {
                        _giaNt2.Value = _tienNt2.Value / _soLuong.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyThayDoiDvt1()
        {
            if (_dvt1.Data == null)
            {
                _he_so1T.Value = 1;
                _he_so1M.Value = 1;
                return;
            }

            var he_soT = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_soT"]);
            var he_soM = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_soM"]);
            if (he_soT == 0) he_soT = 1;
            if (he_soM == 0) he_soM = 1;
            _he_so1T.Value = he_soT;
            _he_so1M.Value = he_soM;

            GetGia();
            TinhTienNt2();
        }

        public void TinhTienNt2(Control actionControl = null)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                
                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                _tienNt2.Value = V6BusinessHelper.Vround(_soLuong1.Value * _giaNt21.Value, M_ROUND_NT);
                _tien2.Value = V6BusinessHelper.Vround(_tienNt2.Value * txtTyGia.Value, M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien2.Value = _tienNt2.Value;

                }

                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
                TinhGiaNt2();//!
                TinhVanChuyen();
                TinhGiamGiaCt();

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiamGiaCt()
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    _ggNt.Value = V6BusinessHelper.Vround((_soLuong1.Value * _hs_qd4.Value), M_ROUND_NT);
                    _gg.Value = V6BusinessHelper.Vround((_ggNt.Value * txtTyGia.Value), M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        _gg.Value = _ggNt.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiamGiaCtRow(DataGridViewRow row)
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

        public void TinhVanChuyen()
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    _tien_vcNt.Value = V6BusinessHelper.Vround((_soLuong1.Value * _hs_qd3.Value), M_ROUND_NT);
                    _tien_vc.Value = V6BusinessHelper.Vround((_tien_vcNt.Value * txtTyGia.Value), M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        _tien_vc.Value = _tien_vcNt.Value;

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhVanChuyen " + _sttRec, ex);
            }
        }

        public void TinhVanChuyenRow(DataGridViewRow row)
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

        public void TinhTienVon()
        {
            
            _tienNt.Value = V6BusinessHelper.Vround(_soLuong.Value * _gia_nt.Value, M_ROUND_NT);
            _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }
        }

        public void TinhGiaVon()
        {
            if (_soLuong.Value != 0)
            {

                _gia_nt.Value = V6BusinessHelper.Vround(_tienNt.Value / _soLuong.Value, M_ROUND_GIA_NT);
                _gia.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong.Value, M_ROUND_GIA);

                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _gia_nt.Value;

                }
            }
        }

        public void TinhGiaNt2()
        {
            try
            {
                _gia21.Value = V6BusinessHelper.Vround((_giaNt21.Value * txtTyGia.Value), M_ROUND_GIA);
                if (_maNt == _mMaNt0)
                {
                    _gia21.Value = _giaNt21.Value;
                }

                if (_soLuong.Value != 0)
                {
                    if (_he_so1T.Value == 1 && _he_so1M.Value == 1)
                    {
                        _giaNt2.Value = _giaNt21.Value;
                        _gia2.Value = _gia21.Value;
                    }
                    else
                    {
                        _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong.Value), M_ROUND_GIA_NT);
                        _gia2.Value = V6BusinessHelper.Vround((_tien2.Value / _soLuong.Value), M_ROUND_GIA);
                    }

                    if (_maNt == _mMaNt0)
                    {
                        _gia2.Value = _giaNt2.Value;
                    }
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
        public void TinhGiaNt2_TienNt2()
        {
            try
            {
                _tien2.Value = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien2.Value = _tienNt2.Value;

                }

                if (_soLuong1.Value != 0)
                {
                    _giaNt21.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong1.Value), M_ROUND_GIA_NT);
                    _gia21.Value = V6BusinessHelper.Vround((_tien2.Value / _soLuong1.Value), M_ROUND_GIA);
                    
                    if (_maNt == _mMaNt0)
                    {
                        _gia21.Value = _giaNt21.Value;
                    }
                }

                if (_he_so1T.Value == 1 && _he_so1M.Value == 1)
                {
                    _giaNt2.Value = _giaNt21.Value;
                    _gia2.Value = _gia21.Value;
                }
                else if (_soLuong.Value != 0)
                {
                    _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong.Value), M_ROUND_GIA_NT);
                    _gia2.Value = V6BusinessHelper.Vround((_tien2.Value / _soLuong.Value), M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        _gia2.Value = _giaNt2.Value;
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
                    detail1.MODE = V6Mode.Lock;
                    dataGridView1.ReadOnly = true;
                }
                else
                {
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _tienNt2.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;

                    _tienNt.Enabled = chkSuaTien.Checked && _xuat_dd.Text!="";

                    //{Tuanmh 20/02/2016
                    _ckNt.Enabled = !chkLoaiChietKhau.Checked;
                    _ck.Enabled = !chkLoaiChietKhau.Checked;
                    _gia21.Enabled = chkSuaTien.Checked && _giaNt21.Value==0;
                    _gia_nt.Enabled =  _xuat_dd.Text != "";
                    _gia.Enabled =  _xuat_dd.Text != "" && _gia_nt.Value==0;

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

                        _hs_qd4.EnableTag();
                        _gg.EnableTag();
                        _ggNt.EnableTag();
                    }
                    else
                    {
                        txtTongGiamNt.ReadOnly = false;
                        txtTongGiam.ReadOnly = false;

                        _hs_qd4.DisableTag();
                        _gg.DisableTag();
                        _ggNt.DisableTag();
                    }

                    if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "1" ||
                        V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                    {
                        TxtT_TIENVCNT.ReadOnly = true;
                        TxtT_TIENVC.ReadOnly = true;

                        _hs_qd3.EnableTag();
                        _tien_vc.EnableTag();
                        _tien_vcNt.EnableTag();
                    }
                    else
                    {
                        TxtT_TIENVCNT.ReadOnly = false;
                        TxtT_TIENVC.ReadOnly = false;

                        _hs_qd3.DisableTag();
                        _tien_vc.DisableTag();
                        _tien_vcNt.DisableTag();
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
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string FIELD = null;
            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var col = dataGridView1.Columns[e.ColumnIndex];
                FIELD = col.DataPropertyName.ToUpper();
                var cell = row.Cells[e.ColumnIndex];
                var cell_MA_VT = row.Cells["MA_VT"];
                var cell_SO_LUONG1 = row.Cells["SO_LUONG1"];

                ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        #region ==== SO_LUONG1 ====

                        V6VvarTextBox txtmavt = new V6VvarTextBox() { VVar = "MA_VT" };
                        txtmavt.Text = cell_MA_VT.Value.ToString();
                        txtmavt.RefreshLoDateYnValue();
                        if (txtmavt.Data != null && txtmavt.VITRI_YN)
                        {
                            var packs1 = ObjectAndString.ObjectToDecimal(txtmavt.Data["Packs1"]);
                            if (packs1 > 0 && ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) > packs1)
                            {
                                cell_SO_LUONG1.Value = packs1;
                            }
                        }

                        //_soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                        row.Cells["SO_LUONG"].Value = ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) * ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1"].Value);
                        //TinhTienVon1(_soLuong1);
                        row.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT21"].Value), M_ROUND_NT);
                        row.Cells["TIEN0"].Value = _maNt == _mMaNt0
                            ? row.Cells["TIEN_NT2"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT2"].Value) * txtTyGia.Value, M_ROUND);

                        //TinhTienVon(_soLuong1);
                        if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(row, FIELD);
                        //_tienNt.Value = _tienNt0.Value;
                        //_tien.Value = _tien0.Value;
                        row.Cells["TIEN_NT"].Value = row.Cells["TIEN_NT2"].Value;
                        row.Cells["TIEN"].Value = row.Cells["TIEN0"].Value;

                        #endregion ==== SO_LUONG1 ====
                        break;

                    case "GIA_NT21":
                        #region ==== GIA_NT21 ====

                        //TinhTienVon1(_gia_nt21);
                        row.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT21"].Value), M_ROUND_NT);
                        row.Cells["TIEN2"].Value = _maNt == _mMaNt0
                            ? row.Cells["TIEN_NT2"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT2"].Value) * txtTyGia.Value, M_ROUND);
                        //TinhChietKhauChiTiet
                        TinhChietKhauChiTietRow(false, row, txtTyGia.Value);
                        //TinhGiaVon();
                        row.Cells["GIA21"].Value = _maNt == _mMaNt0
                            ? row.Cells["GIA_NT21"].Value
                            : V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT21"].Value) * txtTyGia.Value), M_ROUND_GIA_NT);
                        //TinhGiaNt2
                        if (ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value) != 0)
                        {
                            row.Cells["GIA_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT2"].Value) / ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value), M_ROUND_GIA_NT);
                            row.Cells["GIA2"].Value = _maNt == _mMaNt0
                                ? row.Cells["GIA_NT2"].Value
                                : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN2"].Value)
                                    / ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value), M_ROUND_GIA);
                        }
                        TinhVanChuyenRow(row);
                        TinhGiamGiaCtRow(row);
                        Tinh_thue_ct_row(row);

                        #endregion ==== GIA_NT21 ====
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhTongThanhToan("CellEndEdit_" + FIELD);
        }

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string FIELD = null;
            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                var col = dataGridView1.Columns[e.ColumnIndex];
                FIELD = col.DataPropertyName.ToUpper();

                ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        #region ==== SO_LUONG1 ====
                        GetTonRow(row, detail1, dateNgayCT.Value);
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

        /// <summary>
        /// Gán công thức tính toán cho GridView theo các trường, Các công thức được tham chiếu từ các hàm xử lý (vd so_luong1.V6lostfocus...).
        /// </summary>
        private void SetGridViewFomular()
        {
            return;//Dùng sự kiện cell_endedit để viết lại sự kiện.
            #region ==== SO_LUONG1 ====
            //Ex:
            //--dataGridView1.ThemCongThuc("SO_LUONG1", "SO_LUONG=SO_LUONG1*HE_SO1");
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
            f = dataGridView1.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
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

        public void TinhChietKhauChiTiet()
        {
            try
            {
                _ckNt.Value = V6BusinessHelper.Vround(_tienNt2.Value * _pt_cki.Value/100, M_ROUND_NT);
                _ck.Value = V6BusinessHelper.Vround(_ckNt.Value * txtTyGia.Value, M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _ck.Value = _ckNt.Value;

                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                    var thue_nt = V6BusinessHelper.Vround(tien_nt2 * t_thue_nt / t_tien_nt2, M_ROUND);
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
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
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

                //var tygia = txtTyGia.Value;
                //txtTongTien2.Value = V6BusinessHelper.Vround(t_tien_nt2*tygia, M_ROUND);
                //txtTongGiam.Value = V6BusinessHelper.Vround(t_gg_nt*tygia, M_ROUND);
                //txtTongCk.Value = V6BusinessHelper.Vround(t_ck_nt*tygia, M_ROUND);
                txtConLai.Value = t_tt_nt - txtSL_UD1.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
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
            AM = Invoice.SearchAM("1=0", "1=0", "", "", "");//Làm AM khác null
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
                _maKhoI.SetInitFilter(filter);
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
                    //ShowIDs(["GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "DivTienVND", "DOCSOTIEN_VND"], true);
                    detail1.ShowIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "THUE", "THUE_NT" });
                    panelVND.Visible = true;
                    

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA21");
                    if (c != null) c.Visible = true;
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

                    // Show Dynamic control
                    _gia2.VisibleTag();
                    _gia21.VisibleTag();
                    _tien2.VisibleTag();
                    _thue.VisibleTag();
                    _ck.VisibleTag();
                    _tien_vc.VisibleTag();
                    _gg.VisibleTag();
                    if (_gia != null) _gia.VisibleTag();
                }
                else
                {


                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA = V6Setting.RoundGia;
                    M_ROUND_NT = M_ROUND;
                    M_ROUND_GIA_NT = M_ROUND_GIA;


                    txtTyGia.Enabled = false;
                    txtTyGia.Value = 1;
                    //ShowIDs(["GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "DivTienVND", "DOCSOTIEN_VND"], false);
                    detail1.HideIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "THUE"});
                    panelVND.Visible = false;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di

                    


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

                    //Hide Dynamic control
                    _gia2.InvisibleTag();
                    _gia21.InvisibleTag();
                    _tien2.InvisibleTag();
                    _thue.InvisibleTag();
                    _gia21.InvisibleTag();
                    _ck.InvisibleTag();
                    _tien_vc.InvisibleTag();
                    _gg.InvisibleTag();
                    if (_gia != null) _gia.InvisibleTag();
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

                //AD
                _tienNt.DecimalPlaces = decimalTienNt;
                _tienNt2.DecimalPlaces = decimalTienNt;
                _thue_nt.DecimalPlaces = decimalTienNt;
                _ggNt.DecimalPlaces = decimalTienNt;
                _tien_vcNt.DecimalPlaces = decimalTienNt;
                _ckNt.DecimalPlaces = decimalTienNt;
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

        private void XuLyThayDoiMaThue_i()
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    _tk_thue_i.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    //txtTkThueNo.Text = txtManx.Text;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue_i " + _sttRec, ex);
            }
            //TinhTongThanhToan("XuLyThayDoiMaThue_i");
        }

        public void Tinh_thue_ct()
        {
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_SOA_MULTI_VAT = " + M_SOA_MULTI_VAT);
            if (M_SOA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat_i.Value, _tienNt2.Value - _ckNt.Value - _ggNt.Value, _tien2.Value - _ck.Value - _gg.Value, _thue_nt, _thue);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tinh thue ct M_SOA_MULTY_VAT = 1.");
            }
        }

        public void Tinh_thue_ct_row(DataGridViewRow row)
        {
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_SOA_MULTI_VAT = " + M_SOA_MULTI_VAT);
            if (M_SOA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat_Row(_thue_suat_i.Value, _tienNt2.Value - _ckNt.Value - _ggNt.Value, _tien2.Value - _ck.Value - _gg.Value, row);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tinh thue ct M_SOA_MULTY_VAT = 1.");
            }
        }
        
        /// <summary>
        /// Lấy dữ liệu AD dựa vào rec, tạo 1 copy gán vào AD81
        /// </summary>
        /// <param name="sttRec"></param>
        public void LoadAD(string sttRec)
        {
            if (ADTables == null) ADTables = new SortedDictionary<string, DataTable>();
            if (ADTables.ContainsKey(sttRec)) AD = ADTables[sttRec].Copy();
            else
            {
                ADTables.Add(sttRec, Invoice.LoadAD(sttRec));
                AD = ADTables[sttRec].Copy();
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
                    _sttRec = AM.Rows[index]["Stt_rec"].ToString().Trim();
                    LoadAD(_sttRec);
                    CurrentIndex = index;
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
                    loadAM = Invoice.SearchAM("1=0", "1=0", "", "", "");
                }
                else
                {
                    loadAM = Invoice.SearchAM("", "Stt_rec='" + _sttRec + "'", "", "", "");
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

                OnInvoiceChanged(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
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
        public IDictionary<string, object> addDataAM;
        public List<IDictionary<string, object>> addDataAD;
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
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.AddFail + ": " + addErrorMessage);
                    ShowParentMessage(V6Text.AddFail + ": " + addErrorMessage);
                    Mode = V6Mode.Add;
                }

                ((Timer)sender).Dispose();
                if (_print_flag != V6PrintMode.DoNoThing)
                {
                    var temp = _print_flag;
                    _print_flag = V6PrintMode.DoNoThing;
                    BasePrint(Invoice, _sttRec_In, temp, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
            }
        }

        
        private void ReadyForAdd()
        {
            try
            {
                addDataAD = dataGridView1.GetData(_sttRec);// GetAdList();
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
                
                if (Invoice.InsertInvoice(addDataAM, addDataAD))
                {
                    _AED_Success = true;
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

            if (_print_flag == V6PrintMode.AutoClickPrint)
                Thread.Sleep(2000);
            _AED_Running = false;
        }
#endregion add

        #region ==== Edit Thread ====
        private List<IDictionary<string, object>> editDataAD;
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
                detail1.MODE = V6Mode.View;
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

                editDataAD = dataGridView1.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in editDataAD)
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
                if (_print_flag != V6PrintMode.DoNoThing)
                {
                    var temp = _print_flag;
                    _print_flag = V6PrintMode.DoNoThing;
                    BasePrint(Invoice, _sttRec_In, temp, TongThanhToan, TongThanhToanNT, true);
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
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, keys))
                {
                    _AED_Success = true;
                    ADTables.Remove(_sttRec);
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

            if (_print_flag == V6PrintMode.AutoClickPrint)
                Thread.Sleep(2000);
            _AED_Running = false;
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
        #endregion delete

        private void Luu()
        {
            try
            {
                InvokeFormEvent(FormDynamicEvent.BEFORELUU);

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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

                    addDataAM = PreparingDataAM(Invoice);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD2);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD3);
                    if (chkLoaiChietKhau.Checked)
                    {
                        V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "PT_CKI"}, AD);
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
                    detail1.DoAddButtonClick( );
                    var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                    if (readonly_list.Contains(detail1.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                    {
                        detail1.ChangeToViewMode();
                    }
                    else
                    {
                        SetDefaultDetail();
                    }
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

                                if (check_edit == true)
                                {
                                    Mode = V6Mode.Edit;
                                    detail1.MODE = V6Mode.View;
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
                            detail1.MODE = V6Mode.View;
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

        private void In()
        {
            try
            {
                if (IsViewingAnInvoice)
                {
                    if (V6Login.UserRight.AllowPrint("", Invoice.CodeMact))
                    {
                        var program = Invoice.PrintReportProcedure;
                        var repFile = Invoice.Alct["FORM"].ToString().Trim();
                        var repTitle = Invoice.Alct["TIEU_DE_CT"].ToString().Trim();
                        var repTitle2 = Invoice.Alct["TIEU_DE2"].ToString().Trim();

                        var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                            "", "", "", _sttRec);
                        c.TTT = txtTongThanhToan.Value;
                        c.TTT_NT = txtTongThanhToanNt.Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
                        c.PrintSuccess += (sender, stt_rec, DonDatHangBan_nd51) =>
                        {
                            if (DonDatHangBan_nd51 == 1)
                            {
                                Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                            }
                            if (!sender.IsDisposed) sender.Dispose();
                        };

                        c.ShowToForm(this, V6Text.PrintSOH, true);
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

        private TimDonDatHangBanForm _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) _timForm = new TimDonDatHangBanForm(this);
                    _timForm.ViewMode = true;
                    _timForm.Refresh0();
                    _timForm.Visible = false;
                    _timForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Xem " + _sttRec, ex);
            }
        }

        private void Tim()
        {
            try
            {
                if (V6Login.UserRight.AllowView("", Invoice.CodeMact))
                {
                    FormManagerHelper.HideMainMenu();

                    if (_timForm == null)
                        _timForm = new TimDonDatHangBanForm(this);
                    _timForm.ViewMode = false;
                    _timForm.Visible = false;
                    _timForm.ShowDialog(this);
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
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    ShowMainMessage(V6Text.DetailNotComplete);
                    return;
                }

                _print_flag = V6PrintMode.AutoClickPrint;
                _sttRec_In = _sttRec;
                btnLuu.Focus();
                if (ValidateData_Master())
                {
                    Luu();
                    Mode = V6Mode.View;// Status = "3";
                }
                else
                {
                    _print_flag = V6PrintMode.DoNoThing;
                }
            }
            else if (Mode == V6Mode.View)
            {
                BasePrint(Invoice, _sttRec, V6PrintMode.AutoClickPrint, TongThanhToan, TongThanhToanNT, true);
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
                detail1.SetData(null);
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
                var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                if (readonly_list.Contains(detail1.btnMoi.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    detail1.ChangeToViewMode();
                }
                else
                {
                    _maVt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private bool XuLyThemDetail(IDictionary<string, object> data)
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
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }

            try
            {
                var readonly_list = SetControlReadOnlyHide(new HD_Detail(){Name = "detail1"}, Invoice, Mode, V6Mode.Delete);
                if (readonly_list.Contains(detail1.btnXoa.Name, StringComparer.InvariantCultureIgnoreCase))
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
                            AD.Rows.Remove(currentRow);
                            dataGridView1.DataSource = AD;
                            detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
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

        #region ==== DonDatHangBanDetail Event ====
        private void DonDatHangBanDetail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd();
        }
        private void DonDatHangBanDetail1_AddHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data))
                {
                    All_Objects["data"] = data;
                    InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    return;
                }
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void DonDatHangBanDetail1_EditHandle(IDictionary<string,object> data)
        {
            dataGridView1.UnLock();
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data))
                {
                    All_Objects["data"] = data;
                    InvokeFormEvent(FormDynamicEvent.AFTEREDITDETAILSUCCESS);
                    return;
                }
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void DonDatHangBanDetail1_ClickDelete(object sender)
        {
            XuLyXoaDetail();
        }
        private void DonDatHangBanDetail1_ClickCancelEdit(object sender)
        {
            dataGridView1.UnLock();
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

        #endregion hoadoen detail event

        private void dateNgayCT_ValueChanged(object sender, EventArgs e)
        {
            if (!Invoice.M_NGAY_CT) dateNgayLCT.SetValue(dateNgayCT.Date);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            HuyBase();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
            }
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

            if (chkLoaiChietKhau.Checked)
            {
                _pt_cki.Enabled = false;
                _pt_cki.Tag = "disable";
            }
            else
            {
                chkSuaPtck.Checked = false;
                txtPtCk.ReadOnly = true;
                chkT_CK_NT.Checked = false;
                txtTongCkNt.ReadOnly = true;

                _pt_cki.Enabled = true;
                _pt_cki.Tag = null;
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
            var fieldName = e.Column.DataPropertyName.ToUpper();
            if (_alct1Dic.ContainsKey(fieldName))
            {
                var row = _alct1Dic[fieldName];
                var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                if(fieldName == "GIA_NT21") fcaption += " "+ cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT2") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "GIA21") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN2") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if(!new List<string> {"TEN_VT","MA_VT"}.Contains(fieldName))
            {
                e.Column.Visible = false;
            }
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
            V6PrintMode printMode = V6PrintMode.DoNoThing;
            if (Invoice.PrintMode == "1") printMode = V6PrintMode.AutoPrint;
            if (Invoice.PrintMode == "2") printMode = V6PrintMode.AutoClickPrint;
            BasePrint(Invoice, _sttRec, printMode, TongThanhToan, TongThanhToanNT, false);
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void txtPtCk_V6LostFocus(object sender)
        {
            TinhTongThanhToan("V6LostFocus txtPtCk_V6LostFocus ");

        }

        private void chkSuaTien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                _tienNt2.Enabled = chkSuaTien.Checked;
                _tienNt.Enabled = chkSuaTien.Checked && _xuat_dd.Text != "";
                _ckNt.Enabled = chkSuaTien.Checked;
            }
            if (chkSuaTien.Checked)
            {
                _tienNt2.Tag = null;
                _tienNt.Tag = null;
                _ckNt.Tag = null;
            }
            else
            {
                _tienNt2.Tag = "disable";
                _tienNt.Tag = "disable";
                _ckNt.Tag = "disable";
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

        private void DonDatHangBanDetail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);
                    if (_gv1EditingRow == null)
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                        return;
                    }
                    
                    detail1.ChangeToEditMode();
                    var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Edit);
                    if (readonly_list.Contains(detail1.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                    {
                        detail1.ChangeToViewMode();
                    }
                    else
                    {
                        dataGridView1.Lock();
                        _maVt.RefreshLoDateYnValue();
                        _maKhoI.RefreshLoDateYnValue();
                        XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                        _maVt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
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

        private bool ValidateData_Detail(IDictionary<string, object> data)
        {
            try
            {
                string firstErrorField;
                string errors = ValidateDetailData(detail1, Invoice, data, out firstErrorField);
                if (!string.IsNullOrEmpty(errors))
                {
                    this.ShowWarningMessage(errors);
                    //detail1.MODE = detail1.Old_mode;
                    detail1.SetFormControlsReadOnly(false);
                    var c = detail1.GetControlByAccessibleName(firstErrorField);
                    if (c != null) c.Focus();
                    return false;
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
                ViewFormVar();
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
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                if (!chkTempSuaCT.Checked) detail1.AutoFocus();
            }
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
                var chonExcel = new LoadExcelDataForm();
                chonExcel.Program = Event_program;
                chonExcel.All_Objects = All_Objects;
                chonExcel.DynamicFixMethodName = "DynamicFixExcel";
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                chonExcel.MA_CT = Invoice.Mact;
                chonExcel.AcceptData += chonExcel_AcceptData;
                chonExcel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chonExcel_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO_I")
                && table.Columns.Contains("TIEN_NT0") && table.Columns.Contains("SO_LUONG1")
                && table.Columns.Contains("GIA_NT01"))
            {
                if (table.Rows.Count > 0)
                {
                    bool flag_add = chon_accept_flag_add;
                    chon_accept_flag_add = false;
                    if (!flag_add)
                    {
                        AD.Rows.Clear();
                    }

                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        detail1.MODE = V6Mode.Init;
                    }
                }

                foreach (DataRow row in table.Rows)
                {
                    var data = row.ToDataDictionary(_sttRec);
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    //{ Tuanmh 31/08/2016 Them thong tin ALVT
                    _maVt.Text = cMaVt;
                    var datavt = _maVt.Data;


                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1")) data.Add("HE_SO1", 1);
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
            FixDataGridViewSize(dataGridView1);
        }


        private void btnApGia_Click(object sender, EventArgs e)
        {
            ApGiaBan();
            ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);
        }

        public override void ApGiaBan(bool auto = false)
        {
            try
            {
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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
                    return;
                }

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

                    var giaNt21 = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                    row["GIA_NT21"] = giaNt21;
                    //_soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                    tienNt2 = V6BusinessHelper.Vround((soLuong1 * giaNt21), M_ROUND_NT);
                    tien2 = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

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

                    //TinhGiaNt2();
                    row["Gia21"] = V6BusinessHelper.Vround((_giaNt21.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                    if (_maNt == _mMaNt0)
                    {
                        row["Gia21"] = row["Gia_nt21"];
                    }

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
            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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

        private void inKhacMenu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
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
                txtLoaiCt.Text = e.Loai_ct;
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                detail1.MODE = V6Mode.View;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }
                int addCount = 0, failCount = 0; _message = "";
                string ma_kh_soh = null;
                foreach (IDictionary<string, object> data in selectedDataList)
                {
                    // Lấy ma_kh_soh đầu tiên.
                    if (ma_kh_soh == null && data.ContainsKey("MA_KH_SOH"))
                    {
                        ma_kh_soh = data["MA_KH_SOH"].ToString().Trim();
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

                    if (XuLyThemDetail(newData)) addCount++;
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
            FixMenuChucNangItemShiftText(chonBaoGiaMenu, chonTuExcelMenu);
        }


    }
}
