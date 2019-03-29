﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.ChonDonHang;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau
{
    public partial class PhieuNhapKhauControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        //Stopwatch w = new Stopwatch();
        //private string wm = "";
        // ReSharper disable once InconsistentNaming
        public V6Invoice72 Invoice = new V6Invoice72();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuNhapKhauControl()
        {
            InitializeComponent();
            MyInit();
        }
        public PhieuNhapKhauControl(string itemId)
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
        public PhieuNhapKhauControl(string maCt, string itemId, string sttRec)
            : base(maCt, itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {   
            LoadLanguage();
            LoadTag(Invoice, detail1.Controls);

            //_amStruct = Invoice.GetAMStruct();
            //_adStruct = Invoice.ADStruct();
            //_ad2Struct = Invoice.AD2Struct();
            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtManx.Upper();
            txtManx.FilterStart = true;
            
            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                txtMa_sonb.SetInitFilter("MA_DVCS='"+V6Login.Madvcs+ "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }


            //V6ControlFormHelper.CreateGridViewStruct(dataGridView1, adStruct);
            
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

            //dataGridViewColumn = dataGridView2.Columns["UID"];
            //if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(Guid);
            //if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);
            //dataGridViewColumn = dataGridView2.Columns["STT_REC"];
            //if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);
            //dataGridViewColumn = dataGridView2.Columns["STT_REC0"];
            //if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);

            //GridView3
            dataGridViewColumn = dataGridView3.Columns["UID"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(Guid);
            //,,,
            dataGridViewColumn = dataGridView3.Columns["TK_I"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);
            dataGridViewColumn = dataGridView3.Columns["TEN_TK"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);
            dataGridViewColumn = dataGridView3.Columns["STT_REC"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);
            dataGridViewColumn = dataGridView3.Columns["STT_REC0"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof(string);

            cboKieuPost.SelectedIndex = 0;
            cboLoai_pb.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            detail1.AddContexMenu(menuDetail1);
            LoadDetail2Controls();
            LoadDetail3Controls();
            LoadAdvanceControls(Invoice.Mact);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        private V6ColorTextBox _dvt;
        private V6VvarTextBox _maVt, _dvt1, _maKho, _maKhoI, _tkVt,_maLo, _ma_thue_i;
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _giaNt, _giaNt01, _tien0, _tienNt0,
            _ck, _ckNt, _gia0, _gia01, _gia, _gia_Nt0, _mau_bc, _pt_cki, _cpNt, _cp, _ggNt, _gg;
        private V6NumberTextBox _ton13, _tienNt, _tien, _nkNt, _nk, _ts_nk, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2,
            _thue, _thue_nt, _thue_suat_i;
        private V6DateTimeColor _hanSd;
        private V6ColorTextBox _so_ct022,_so_seri022,_ten_kh22,
            _dia_chi22,_ma_so_thue22;
        private V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22, _ma_thue22;
        private V6DateTimeColor _ngay_ct022;
        private V6NumberTextBox _t_tien22, _t_tien_nt22, _thue_suat22, _t_thue22, _t_thue_nt22, _tien_cn_nt22, _tien_cn22, _han_tt22;
        
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
                        //_maVt.BrotherFields = "dvt";
                   
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
                    case "TK_VT":
                        _tkVt = (V6VvarTextBox)control;
                        _tkVt.Upper();
                        _tkVt.SetInitFilter("Loai_tk = 1");
                        break;
                    case "DVT1":
                        //select * from dbo.vALqddvt WHERE ma_vt='DTBANH'
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
                    case "THUE_SUAT_I":
                        _thue_suat_i = control as V6NumberTextBox;
                        if (_thue_suat_i != null)
                        {
                            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " _thue_suat_i ok.");
                        }
                        else
                        {
                            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " _thue_suat_i null.");
                        }
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

                            if (chkT_THUE_NT.Checked && M_POA_MULTI_VAT == "1")
                                _thue_nt.Enabled = true;
                            else _thue_nt.Enabled = false;

                        }
                        break;

                    case "TON13":
                        _ton13 = (V6NumberTextBox)control;
                        _ton13.Tag = "disable";
                        break;
                    //_ton13.V6LostFocus += Ton13_V6LostFocus;
                    case "SO_LUONG1":
                        _soLuong1 = (V6NumberTextBox)control;
                        _soLuong1.V6LostFocus += delegate
                        {
                            CheckSoLuong1(_soLuong1);
                        };
                        _soLuong1.V6LostFocusNoChange += delegate
                        {

                        };
                        if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                        {
                            _soLuong1.ReadOnlyTag();
                        }
                        break;
                    //_soLuong1.Tag = "hide";
                    case "SO_LUONG":
                        _soLuong = (V6NumberTextBox)control;
                        _soLuong.Tag = "hide";
                        break;
                    case "HE_SO1":
                        _heSo1 = (V6NumberTextBox)control;
                        _heSo1.Tag = "hide";
                        _heSo1.DecimalPlaces = Invoice.ADStruct.ContainsKey("HE_SO1")
                            ? Invoice.ADStruct["HE_SO1"].MaxNumDecimal
                            : 6;
                        _heSo1.StringValueChange += (sender, args) =>
                        {
                            if (_heSo1.Value == 0)
                            {
                                _heSo1.Value = 1;
                                return;
                            }
                            if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit) && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                            {
                                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _heSo1);
                                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _heSo1);
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _heSo1);
                            }
                        };
                        break;
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
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    CheckSoLuong1(_sl_qd);
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
                        _sl_qd2 = control as V6NumberTextBox;
                        if (_sl_qd2 != null)
                        {
                            _sl_qd2.DisableTag();
                        }
                        break;
                    case "HS_QD1":
                        _hs_qd1 = control as V6NumberTextBox;
                        if (_hs_qd1 != null)
                        {
                            _hs_qd1.DisableTag();
                        }
                        break;
                    case "HS_QD2":
                        _hs_qd2 = control as V6NumberTextBox;
                        if (_hs_qd2 != null)
                        {
                            _hs_qd2.DisableTag();
                        }
                        break;
                    case "GIA_NT":
                        _giaNt = control as V6NumberTextBox;
                        if (_giaNt != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _giaNt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _giaNt.ReadOnlyTag();
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
                    case "GIA0":
                        _gia0 = control as V6NumberTextBox;
                        if (_gia0 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia0.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia0.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA01":
                        _gia01 = control as V6NumberTextBox;
                        if (_gia01 != null)
                        {
                            _gia01.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _gia01.EnableTag();
                            }
                            else
                            {
                                _gia01.DisableTag();
                            }

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia01.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia01.ReadOnlyTag();
                            }
                        }
                        break;
                    //_soLuong.Tag = "hide";
                    case "GIA_NT0":
                        _gia_Nt0 = control as V6NumberTextBox;
                        if (_gia_Nt0 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia_Nt0.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia_Nt0.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA_NT01":
                        _giaNt01 = control as V6NumberTextBox;
                        if (_giaNt01 != null)
                        {
                            _giaNt01.V6LostFocus += GiaNt01_V6LostFocus;
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _giaNt01.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _giaNt01.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN":
                        _tien = control as V6NumberTextBox;
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
                        _tienNt =  control as V6NumberTextBox;
                        if (_tienNt != null)
                        {
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
                    case "TIEN_NT0":
                        _tienNt0 = control as V6NumberTextBox;
                        if (_tienNt0 != null)
                        {
                            _tienNt0.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _tienNt0.EnableTag();
                            }
                            else
                            {
                                _tienNt0.DisableTag();
                            }

                            _tienNt0.V6LostFocus += TienNt0_V6LostFocus;
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tienNt0.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tienNt0.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN0":
                        _tien0 = control as V6NumberTextBox;
                        if (_tien0 != null)
                        {
                            _tien0.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _tien0.EnableTag();
                            }
                            else
                            {
                                _tien0.DisableTag();
                            }
                            
                            // Tuanmh 25/05/2017
                            _tien0.V6LostFocus += Tien0_V6LostFocus;

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien0.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien0.ReadOnlyTag();
                            }
                        }
                        break;
                    
                    case "CK":
                        _ck = (V6NumberTextBox)control;
                        if (_ck != null)
                        {
                            _ck.V6LostFocus += delegate
                            {
                                Tinh_thue_ct();
                            };
                        }
                        break;
                    //_tien2.V6LostFocus;
                    case "CK_NT":
                        _ckNt = control as V6NumberTextBox;
                        if (_ckNt != null)
                        {
                            _ckNt.V6LostFocus += delegate
                            {
                                TinhChietKhauChiTiet(true, _ck, _ckNt, txtTyGia, _tienNt0, _pt_cki);
                                Tinh_thue_ct();
                                TinhTienNt();
                            };
                        }
                        break;
                    case "PT_CKI":
                        _pt_cki = control as V6NumberTextBox;
                        if (_pt_cki != null)
                        {
                            _pt_cki.V6LostFocus += delegate
                            {
                                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt0, _pt_cki);
                                Tinh_thue_ct();
                                TinhTienNt();
                            };
                        }
                        break;
                    case "CP":
                        _cp = (V6NumberTextBox)control;
                        break;
                    case "CP_NT":
                        _cpNt = control as V6NumberTextBox;
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
                    //_tien2.V6LostFocus;
                    case "MA_LO":
                        //_maLo = (V6ColorTextBox)control;
                        _maLo = (V6VvarTextBox)control;
                        _maLo.Upper();

                        _maLo.GotFocus += delegate
                        {
                            if (_maVt.Text != "")
                            {
                                _maLo.SetInitFilter("Ma_vt='" + _maVt.Text.Trim() + "'");
                                // Tuanmh 05/05/2018 sai HSD
                                _maLo.ExistRowInTable(true);
                            }
                        };
                        _maLo.V6LostFocus += _maLo_V6LostFocus;
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        _hanSd.Tag = "disable";
                        break;
                    case "NK_NT":
                        _nkNt = (V6NumberTextBox)control;
                        _nkNt.V6LostFocus += delegate
                        {
                            TinhTienNt();
                        };
                        break;
                    case "NK":
                        _nk = (V6NumberTextBox)control;
                        _nk.V6LostFocus += delegate
                        {
                            TinhTienNt();
                        };
                        break;
                    case "TS_NK":
                        _ts_nk = (V6NumberTextBox)control;
                        _ts_nk.V6LostFocus += delegate
                        {
                            TinhThueNhapKhau();
                        };
                        break;

                    // NK_NT=ROUND(TIEN_NT0*TS_NK/100,M_ROUND)
                        // TINH TONG T_NK_NT=SUM(NK_NT)
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

        /// <summary>
        ///  Hàm giả cho giống tên bên HoaDon
        /// </summary>
        public void CheckSoLuong1(Control actionControl)
        {
            TinhTienNt0(actionControl);
            Tinh_thue_ct();
        }

        private void LoadDetail2Controls()
        {
            //Tạo trước control phòng khi alct không có
            detail2.lblName.AccessibleName = "";

            
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct2,  out _orderList2,out _alct2Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                switch (NAME)
                {
                    case "SO_CT0":
                        _so_ct022 = control as V6ColorTextBox;
                        if (_so_ct022 != null)
                        {
                        
                        }
                        break;
                    case "SO_SERI0":
                        _so_seri022 = control as V6ColorTextBox;
                        if (_so_seri022 != null)
                        {
                            _so_seri022.Upper();
                        }
                        break;
                    case "MAU_BC":
                        _mau_bc = control as V6NumberTextBox;
                        if (_mau_bc != null)
                        {
                            _mau_bc.LimitCharacters = "123458";
                            _mau_bc.MaxNumLength = 1;
                            _mau_bc.MaxLength = 1;
                            _mau_bc.GotFocus += _mau_bc_GotFocus;
                        }
                        break;
                    case "TEN_KH":
                        _ten_kh22 = control as V6ColorTextBox;
                        if (_ten_kh22 != null)
                        {

                        }
                        break;
                    case "DIA_CHI":
                        _dia_chi22 = control as V6ColorTextBox;
                        if (_dia_chi22 != null)
                        {

                        }
                        break;
                    case "MA_SO_THUE":
                        _ma_so_thue22 = control as V6ColorTextBox;
                        if (_ma_so_thue22 != null)
                        {

                        }
                        break;
                    case "MA_KH":
                        _ma_kh22 = control as V6VvarTextBox;
                        if (_ma_kh22 != null)
                        {
                            _ma_kh22.CheckOnLeave = true;
                            _ma_kh22.V6LostFocus += MaKhach22_V6LostFocus;

                            //_makh2.V6LostFocusNoChange += delegate
                            //{
                            //    XuLyChonMaKhach2();
                            //}; 
                        }
                        break;
                    case "TK_THUE_NO":

                        _tk_thue_no22 = (V6VvarTextBox)control;
                        _tk_thue_no22.Upper();
                        _tk_thue_no22.SetInitFilter("Loai_tk=1");
                        _tk_thue_no22.FilterStart = true;
                        break;
                    case "TK_DU":

                        _tk_du22 = (V6VvarTextBox)control;
                        _tk_du22.Upper();
                        _tk_du22.SetInitFilter("Loai_tk=1");
                        _tk_du22.FilterStart = true;
                        break;
                    case "NGAY_CT0":
                        _ngay_ct022 = control as V6DateTimeColor;
                        if (_ngay_ct022 != null)
                        {
                        
                        }
                        break;
                    case "HAN_TT":
                        _han_tt22 = control as V6NumberTextBox;
                        break;
                    case "T_TIEN":
                        _t_tien22 = control as V6NumberTextBox;
                        if (_t_tien22 != null)
                        {
                            _t_tien22.V6LostFocus += delegate
                            {
                                //TinhTienThue22();
                                TienChanged(_t_tien22.Value, _thue_suat22.Value, _t_thue22);
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _t_tien22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _t_tien22.ReadOnlyTag();
                            }
                        }
                        break;
                    case "T_TIEN_NT":
                        _t_tien_nt22 = control as V6NumberTextBox;
                        if (_t_tien_nt22 != null)
                        {
                            _t_tien_nt22.V6LostFocus += delegate
                            {
                                TienNtChanged(_t_tien_nt22.Value, txtTyGia.Value, _thue_suat22.Value,
                                    _t_tien22, _t_thue_nt22, _t_thue22);
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _t_tien_nt22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _t_tien_nt22.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN_CN_NT":
                        _tien_cn_nt22 = control as V6NumberTextBox;
                        if (_tien_cn_nt22 != null)
                        {
                            _tien_cn_nt22.GotFocus += delegate
                            {
                                if (_tien_cn_nt22.Value == 0)
                                    _tien_cn_nt22.Value = txtTongThanhToanNt.Value;
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien_cn_nt22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien_cn_nt22.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN_CN":
                        _tien_cn22 = control as V6NumberTextBox;
                        if (_tien_cn22 != null)
                        {
                            _tien_cn22.GotFocus += delegate
                            {
                                if (_tien_cn22.Value == 0)
                                    _tien_cn22.Value = txtTongThanhToan.Value;
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien_cn22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien_cn22.ReadOnlyTag();
                            }
                        }
                        break;
                    case "MA_THUE":
                        _ma_thue22 = control as V6VvarTextBox;
                        if (_ma_thue22 != null)
                        {
                            _ma_thue22.CheckNotEmpty = true;
                            _ma_thue22.CheckOnLeave = true;
                            _ma_thue22.V6LostFocus += delegate
                            {
                                if (_ma_thue22.Data != null)
                                {
                                    _thue_suat22.Value = ObjectAndString.ObjectToDecimal(_ma_thue22.Data["Thue_suat"]);
                                    _tk_thue_no22.Text = _ma_thue22.Data["Tk_thue_no"].ToString();
                                }
                                else
                                {

                                }
                                //TinhTienThue22();
                                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat22.Value, _t_tien_nt22.Value, _t_tien22.Value, _t_thue_nt22, _t_thue22);
                            };
                        }
                        break;
                    case "THUE_SUAT":
                        _thue_suat22 = control as V6NumberTextBox;
                        if (_thue_suat22 != null)
                        {
                            _thue_suat22.ReadOnlyTag();
                            _thue_suat22.V6LostFocus += delegate
                            {
                                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat22.Value, _t_tien_nt22.Value, _t_tien22.Value, _t_thue_nt22, _t_thue22);
                            };
                        }
                        break;
                    case "T_THUE":
                        _t_thue22 = control as V6NumberTextBox;
                        if (_t_thue22 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _t_thue22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _t_thue22.ReadOnlyTag();
                            }
                        }
                        break;
                    case "T_THUE_NT":
                        _t_thue_nt22 = control as V6NumberTextBox;
                        if (_t_thue_nt22 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _t_thue_nt22.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _t_thue_nt22.ReadOnlyTag();
                            }

                            _t_thue_nt22.V6LostFocus += delegate
                            {
                                Tinh_TienThue_TheoTienThueNt(_t_thue_nt22.Value, txtTyGia.Value, _t_thue22, M_ROUND);
                            };
                        }
                        break;
                }

            }

            foreach (Control control in dynamicControlList.Values)
            {
                detail2.AddControl(control);                
            }

            detail2.SetStruct(Invoice.AD2Struct);
            detail2.MODE = detail2.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView2, _alct2Dic, _maNt, _mMaNt0);
        }


        private V6ColorTextBox _operTT_33, _nh_dk_33;
        private V6VvarTextBox _tk_i_33, _ma_kh_i_33;
        private V6NumberTextBox _PsNoNt_33, _PsCoNt_33, _PsNo_33, _PsCo_33, _mau_bc_33,
            _gia_nt_33, _tien_nt_33, _gia_33, _tien_33;
        private void LoadDetail3Controls()
        {
            detail3.lblName.AccessibleName = "TEN_TK";
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct3, out _orderList3, out _alct3Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();

                #region ==== Hứng control ====
                if (NAME == "TK_I")
                {
                    _tk_i_33 = (V6VvarTextBox)control;
                    _tk_i_33.Upper();
                    _tk_i_33.FilterStart = true;
                    _tk_i_33.SetInitFilter("Loai_tk = 1");
                    _tk_i_33.BrotherFields = "ten_tk,ten_tk2";
                    _tk_i_33.V6LostFocus += delegate
                    {
                        if (_tk_i_33.Data != null)
                        {
                            var data = _tk_i_33.Data;
                            var tk_cn = ObjectAndString.ObjectToInt(data["tk_cn"]);
                            if (tk_cn == 1)
                            {
                                _ma_kh_i_33.CheckNotEmpty = true;
                            }
                            else
                            {
                                _ma_kh_i_33.CheckNotEmpty = false;
                            }
                        }
                    };
                }
                else if (NAME == "MA_KH_I")
                {
                    _ma_kh_i_33 = control as V6VvarTextBox;
                    if (_ma_kh_i_33 != null)
                    {
                        _ma_kh_i_33.GotFocus += delegate
                        {
                            if (_ma_kh_i_33.Text.Trim() == "") _ma_kh_i_33.Text = txtMaKh.Text;
                        };
                    }
                }
                else if (NAME == "PS_NO")
                {
                    _PsNo_33 = (V6NumberTextBox)control;
                }
                else if (NAME == "PS_NO_NT")
                {
                    _PsNoNt_33 = control as V6NumberTextBox;
                    if (_PsNoNt_33 != null)
                    {
                        _PsNoNt_33.V6LostFocus += delegate
                        {
                            _PsNo_33.Value = V6BusinessHelper.Vround((_PsNoNt_33.Value * txtTyGia.Value), M_ROUND);
                            if (_PsNoNt_33.Value != 0)
                            {
                                _PsCoNt_33.Value = 0;
                                _PsCo_33.Value = 0;
                            }
                        };
                    }
                }
                else if (NAME == "PS_CO")
                {
                    _PsCo_33 = (V6NumberTextBox)control;
                }
                else if (NAME == "PS_CO_NT")
                {
                    _PsCoNt_33 = control as V6NumberTextBox;
                    if (_PsCoNt_33 != null)
                    {

                        _PsCoNt_33.V6LostFocus += delegate
                        {
                            _PsCo_33.Value = V6BusinessHelper.Vround((_PsCoNt_33.Value * txtTyGia.Value), M_ROUND);
                            if (_PsCoNt_33.Value != 0)
                            {
                                _PsNoNt_33.Value = 0;
                                _PsNo_33.Value = 0;
                            }
                        };
                    }
                }
                else if (NAME == "OPER_TT")
                {
                    _operTT_33 = control as V6ColorTextBox;
                    if (_operTT_33 != null)
                    {
                        _operTT_33.LimitCharacters = "0+-";
                        _operTT_33.MaxLength = 1;
                    }
                }
                else if (NAME == "GIA")
                {
                    _gia_33 = control as V6NumberTextBox;
                }
                else if (NAME == "TIEN")
                {
                    _tien_33 = control as V6NumberTextBox;
                }
                else if (NAME == "GIA_NT")
                {
                    _gia_nt_33 = control as V6NumberTextBox;
                }
                else if (NAME == "TIEN_NT")
                {
                    _tien_nt_33 = control as V6NumberTextBox;
                }
                else if (NAME == "NH_DK")
                {
                    _nh_dk_33 = control as V6ColorTextBox;
                }

                #endregion hứng control

            }

            foreach (Control control in dynamicControlList.Values)
            {
                detail3.AddControl(control);
            }

            detail3.SetStruct(Invoice.AD3Struct);
            detail3.MODE = detail3.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView3ChiPhi, _alct3Dic, _maNt, _mMaNt0);
        }
        
        private void Detail3_ClickAdd(object sender)
        {
            XuLyDetail3ClickAdd(sender);
        }
        private void XuLyDetail3ClickAdd(object sender)
        {
            try
            {
                TruDanTheoNhomDk();
                _tk_i_33.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Tính toán và gán giá trị còn lại cho ps_no hoặc ps có theo nhóm dk
        /// </summary>
        private void TruDanTheoNhomDk()
        {
            try
            {
                var groupDic = new SortedDictionary<string, decimal[]>();
                foreach (DataRow row in AD3.Rows)
                {
                    var nhomDK = row["Nh_dk"].ToString().Trim();
                    var ps_no = ObjectAndString.ObjectToDecimal(row["Ps_no_nt"]);
                    var ps_co = ObjectAndString.ObjectToDecimal(row["Ps_co_nt"]);
                    if (groupDic.ContainsKey(nhomDK))
                    {
                        var group = groupDic[nhomDK];
                        group[0] += ps_no;
                        group[1] += ps_co;
                        groupDic[nhomDK] = group;
                    }
                    else
                    {
                        var group = new decimal[] { 0, 0 };
                        group[0] += ps_no;
                        group[1] += ps_co;
                        groupDic[nhomDK] = group;
                    }
                }

                foreach (KeyValuePair<string, decimal[]> item in groupDic)
                {
                    var group = item.Value;
                    if (group[0] != group[1])
                    {
                        if (_nh_dk_33 != null) _nh_dk_33.Text = item.Key;
                        if (group[0] > group[1])
                        {
                            _PsCoNt_33.Value = group[0] - group[1];
                            _PsCo_33.Value = V6BusinessHelper.Vround(_PsCoNt_33.Value * txtTyGia.Value, M_ROUND);
                        }
                        else
                        {
                            _PsNoNt_33.Value = group[1] - group[0];
                            _PsNo_33.Value = V6BusinessHelper.Vround(_PsNoNt_33.Value * txtTyGia.Value, M_ROUND);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Detail3_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data))
            {
                if (XuLyThemDetail3(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }

        private void Detail3_EditHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data))
            {
                if (XuLySuaDetail3(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private bool XuLySuaDetail3(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv3EditingRow != null)
                {
                    var cIndex = _gv3EditingRow.Index;


                    if (cIndex >= 0 && cIndex < AD3.Rows.Count)
                    {
                        //Thêm thông tin...
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Date;


                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00379") + " " + V6Text.Empty;
                        //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = AD3.Rows[cIndex];
                            foreach (DataColumn column in AD3.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            dataGridView3.DataSource = AD3;
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
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhTongThanhToan("xy ly sua detail3");
            return true;
        }

        private bool XuLyThemDetail3(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                _sttRec03 = V6BusinessHelper.GetNewSttRec0(AD3);
                data["STT_REC0"] = _sttRec03;
                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "")
                {
                    var label = "TK_I";
                    var lbl = detail1.GetControlByName("lbl" + label);
                    if (lbl != null) label = lbl.Text;
                    error += V6Text.NoInput + " [" + label + "]\n";
                }
                //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD3.NewRow();
                    foreach (DataColumn column in AD3.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(key) ? data[key] : "") ?? DBNull.Value;
                        newRow[key] = value;
                    }
                    AD3.Rows.Add(newRow);
                    dataGridView3.DataSource = AD3;
                    
                    if (AD3.Rows.Count > 0)
                    {
                        var cIndex = AD3.Rows.Count - 1;
                        dataGridView3.Rows[cIndex].Selected = true;
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
            TinhTongThanhToan("xu ly them detail3");
            return true;
        }

        private bool ValidateData_Detail3(IDictionary<string, object> data)
        {
            try
            {
                if (_tk_i_33.Int_Data("Tk_cn") == 1 && data["MA_KH_I"].ToString().Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.Text("TKCNTHIEUMAKH"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail3 " + _sttRec, ex);
            }
            return true;
        }
        
        private void Detail3_ClickEdit(object sender)
        {
            try
            {
                if (AD3 != null && AD3.Rows.Count > 0 && dataGridView3.DataSource != null)
                {
                    detail3.ChangeToEditMode();

                    _sttRec03 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView3, detail3, out _gv3EditingRow);
                    if (!string.IsNullOrEmpty(_sttRec03))
                    {
                        _tk_i_33.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Detail3_DeleteHandle(object sender)
        {
            XuLyDeleteDetail3();
        }
        private void XuLyDeleteDetail3()
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (dataGridView3.CurrentRow != null)
                {
                    var cIndex = dataGridView3.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < AD3.Rows.Count)
                    {
                        var currentRow = AD3.Rows[cIndex];
                        var details = V6Text.FieldCaption("TK") + ": " + currentRow["TK_I"];
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm +
                                                                   details)
                            == DialogResult.Yes)
                        {
                            AD3.Rows.Remove(currentRow);
                            dataGridView3.DataSource = AD3;
                            detail3.SetData(null);
                            TinhTongThanhToan("xu ly xoa detail3");
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

        private void Detail3_ClickCancelEdit(object sender)
        {
            detail3.SetData(_gv3EditingRow.ToDataDictionary());
        }

        private void detail3_LabelNameTextChanged(object sender, EventArgs e)
        {
            lblNameT.Text = ((Label)sender).Text;
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (detail3.IsViewOrLock)
                detail3.SetData(dataGridView3.GetCurrentRowData());
        }

        private void dataGridView3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            var fieldName = e.Column.DataPropertyName.ToUpper();
            if (_alct3Dic.ContainsKey(fieldName))
            {
                var row = _alct3Dic[fieldName];
                var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                if (fieldName == "GIA_NT") fcaption += " " + cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "GIA") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if (!(new List<string> { "TEN_TK", "TK_I" }).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        
        void _mau_bc_GotFocus(object sender, EventArgs e)
        {
            if (_mau_bc.Value == 0)
            {
                _mau_bc.Value = 1;
            }
        }
        void _maLo_V6LostFocus(object sender)
        {
            //Check malo
            //throw new NotImplementedException();
            CheckMaLo();
        }
        
        private void CheckMaLo()
        {
           
            XuLyLayThongTinKhiChonMaLo();
           
        }
        private void XuLyLayThongTinKhiChonMaLo()
        {
            try
            {
                _maLo.RefreshLoDateYnValue();
                if (_maVt.LO_YN)
                {
                    //Tuanmh 05/05/2018 Sai HSD
                    _maLo.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");

                    var data = _maLo.Data;
                    if (data != null)
                    {
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
                else if (detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
                {
                    detail3.btnNhan.PerformClick();
                }
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
                else if (detail3.MODE == V6Mode.Add)
                {
                    if (tabControl1.SelectedTab != tabChiTietBoSung) tabControl1.SelectedTab = tabChiTietBoSung;
                    detail3.btnMoi.PerformClick();
                }
                else if (detail3.MODE == V6Mode.Edit)
                {
                    if (tabControl1.SelectedTab != tabChiTietBoSung) tabControl1.SelectedTab = tabChiTietBoSung;
                    detail3.btnSua.PerformClick();
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

        private void XuLyLayThongTinKhiChonMaKhoI()
        {
            try
            {
                _maKhoI.RefreshLoDateYnValue();
                var makho_data = _maKhoI.Data;
                if (makho_data != null)
                {
                    var tk_dl = makho_data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _tkVt.Text = tk_dl;
                    }
                    else
                    {  //Tuanmh 14/09/2017 Set lai TK_vt khi doi ma_kho
                        var mavt_data = _maVt.Data;
                        if (mavt_data != null)
                        {
                            var tk_vt = mavt_data["TK_VT"].ToString().Trim();
                            if (!string.IsNullOrEmpty(tk_vt))
                            {
                                _tkVt.Text = tk_vt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyLayThongTinKhiChonMaKhoI " + _sttRec, ex);
            }
        }

        void MaKhoV6LostFocus(object sender)
        {
            _maKhoI.Text = _maKho.Text;
        }

        private void MaVatTu_V6LostFocus(object sender)
        {
            chkT_THUE_NT.Checked = false;
            if (M_POA_MULTI_VAT == "1")
            {
                var data = _maVt.Data;
                if (data != null)
                {
                    _ma_thue_i.Text = (data["ma_thue"] ?? "").ToString().Trim();
                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["thue_suat"]);
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["thue_suat"]);
                    if (!chkT_THUE_NT.Checked) Tinh_thue_ct();
                }
                else
                {
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Ko gán thue_suat_i vì maVt.data == null");
                }
            }
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
                var he_so = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_so"]);
                if (he_so == 0) he_so = 1;
                if (_heSo1.Value != he_so) _heSo1.Value = he_so;
            }
            else
            {
                _heSo1.Value = 1;
            }
        }

        public void TienNt0_V6LostFocus(object sender)
        {
            _tien0.Value = V6BusinessHelper.Vround((_tienNt0.Value * txtTyGia.Value), M_ROUND);
            TinhTienNt();// Trong TinhTienNt da co TinhGiaNt
            //TinhGiaNt();
        }
        void Tien0_V6LostFocus(object sender)
        {
            TinhTienNt();// Trong TinhTienNt da co TinhGiaNt
            //TinhGiaNt();

        }
        
        public void GiaNt01_V6LostFocus(object sender)
        {
            chkT_THUE_NT.Checked = false;
            TinhTienNt0(_giaNt01);
            TinhThueNhapKhau();
            Tinh_thue_ct();
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
                    return;
                }
                var mst = (data["ma_so_thue"] ?? "").ToString().Trim();
                txtMaSoThue.Text = mst;
                txtTenKh.Text = (data["ten_kh"] ?? "").ToString().Trim();
                txtDiaChi.Text = (data["dia_chi"] ?? "").ToString().Trim();

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
                        txtTenKh.Enabled = false;
                        txtDiaChi.Enabled = true;
                        txtMaSoThue.Enabled = false;

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

        private void XuLyChonMaKhach22()
        {
            try
            {
                if (_ma_kh22.Text != "")
                {
                    var data = _ma_kh22.Data;
                    if (data != null)
                    {
                        _ten_kh22.Text = (data["TEN_KH"] ?? "").ToString().Trim();
                        _dia_chi22.Text = (data["DIA_CHI"] ?? "").ToString().Trim();
                        _ma_so_thue22.Text = (data["MA_SO_THUE"] ?? "").ToString().Trim();

                        _ten_kh22.Enabled = _ten_kh22.Text == "";
                        _dia_chi22.Enabled = _dia_chi22.Text == "";
                        _ma_so_thue22.Enabled = _ma_so_thue22.Text == "";
                    }
                }
                else
                {
                    _ten_kh22.Enabled = true;
                    _dia_chi22.Enabled = true;
                    _ma_so_thue22.Enabled = true;
                    _ten_kh22.Focus();
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void XuLyChonMaVt(string mavt)
        {
            XuLyLayThongTinKhiChonMaVt();
            XuLyDonViTinhKhiChonMaVt(mavt);
            //{Tuanmh 14-09/2017 get tk_dl from alkho
            if (_maKhoI.Text != "")
                XuLyLayThongTinKhiChonMaKhoI();
            //}
            //  GetGia();
            GetTon13();
            TinhTienNt0();
            Tinh_thue_ct();
        }
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
            GetTon13();
        }

        public void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
                _maVt.RefreshLoDateYnValue();
                var data = _maVt.Data;
                if (data == null)
                {
                    _tkVt.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;
                    _ma_thue_i.Text = "";
                }
                else
                {
                    SetADSelectMoreControlValue(Invoice, data);
                    _tkVt.Text = (data["tk_vt"] ?? "").ToString().Trim();
                    _hs_qd1.Value = ObjectAndString.ObjectToDecimal(data["HS_QD1"]);
                    _hs_qd2.Value = ObjectAndString.ObjectToDecimal(data["HS_QD2"]);
                    if (M_POA_MULTI_VAT == "1")
                    {
                        _ma_thue_i.Text = (data["ma_thue"] ?? "").ToString().Trim();
                        _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["thue_suat"]);
                        V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["thue_suat"]);
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
                        if (changeMavt) _heSo1.Value = 1;

                    }
                    else
                    {
                        _dvt1.Tag = "readonly";
                        _dvt1.ReadOnly = true;
                        if (changeMavt) _dvt1.Focus();
                        if (changeMavt) _heSo1.Value = 1;
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
        
        private void XuLyThayDoiDvt1()
        {
            if (_dvt1.Data == null) return;
            var he_so = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_so"]);
            if (he_so == 0) he_so = 1;
            _heSo1.Value = he_so;
          
            TinhTienNt0();
        }

        public void TinhTienNt0(Control actionControl = null)
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                _tienNt0.Value = V6BusinessHelper.Vround((_soLuong1.Value * _giaNt01.Value), M_ROUND_NT);
                _tien0.Value = V6BusinessHelper.Vround((_tienNt0.Value * txtTyGia.Value), M_ROUND);

                //_tienNt.Value = _tienNt0.Value;
                //_tien.Value = _tien0.Value;

                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt0, _pt_cki);
                
                TinhThueNhapKhau();

                TinhTienNt();
                TinhGiaNt();

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhTienNt()
        {
            try
            {
                //Tuanmh 19/12/2016 Chua Kiem tra Null
                //Tuanmh 25/05/2017 Bo sung-> + Thue_nk
                _tienNt.Value = _tienNt0.Value + _cpNt.Value - _ckNt.Value - _ggNt.Value+_nkNt.Value;
                _tien.Value = _tien0.Value + _cp.Value - _ck.Value - _gg.Value + _nk.Value;
                
                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tienNt0.Value;
                    _tien.Value = _tienNt.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhGiaNt();
        }

        public void TinhGiaNt()
        {
            try
            {
                if (_soLuong1.Value != 0)
                {
                    _gia01.Value = V6BusinessHelper.Vround(_giaNt01.Value * txtTyGia.Value, M_ROUND_GIA_NT);
                    if (_maNt == _mMaNt0)
                    {
                        _gia01.Value = _giaNt01.Value;
                    }
                }

                if (_soLuong.Value != 0)
                {
                    _gia_Nt0.Value = V6BusinessHelper.Vround((_tienNt0.Value / _soLuong.Value),M_ROUND_GIA_NT);
                    _gia0.Value = V6BusinessHelper.Vround((_tien0.Value / _soLuong.Value), M_ROUND_GIA);

                    _giaNt.Value = V6BusinessHelper.Vround((_tienNt.Value / _soLuong.Value), M_ROUND_GIA_NT);
                    _gia.Value = V6BusinessHelper.Vround((_tien.Value / _soLuong.Value), M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        _gia0.Value = _gia_Nt0.Value;
                        _gia.Value = _giaNt.Value;
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

                if (readOnly)
                {
                    detail1.MODE = V6Mode.Lock;
                    detail2.MODE = V6Mode.Lock;
                    detail3.MODE = V6Mode.Lock;
                    dataGridView3ChiPhi.ReadOnly = true;

                    ChonDonHangMuaMenu.Enabled = false;
                    TroGiupMenu.Enabled = false;
                    chonTuExcelToolStripMenuItem.Enabled = false;
                }
                else //Cac truong hop khac
                {
                    ChonDonHangMuaMenu.Enabled = true;
                    TroGiupMenu.Enabled = true;
                    chonTuExcelToolStripMenuItem.Enabled = true;

                    XuLyKhoaThongTinKhachHang();
                    SetGridViewChiPhiEditAble(txtLoai_pb.Text, chkSuaTien.Checked, dataGridView3ChiPhi, "CP,CP_NT,NK,NK_NT", "CP_NT,NK_NT");

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _tienNt0.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;
                
                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;

                    XuLyHienThiChietKhau_PhieuNhap(chkLoaiChietKhau.Checked, chkSuaTienCk.Checked, _pt_cki, _ckNt, txtTongCkNt, chkSuaPtck);
                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                }

                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains("TIEN"))
                {
                    panelNT.Visible = false;
                    panelVND.Visible = false;
                    lblDocSoTien.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            SetControlReadOnlyHide(this, Invoice, Mode);
        }

        #region ==== DataGridView ====

        /// <summary>
        /// Gán dữ liệu sau đó sắp xếp và format lại GridView
        /// </summary>
        private void SetGridViewData()
        {
            HienThiTongSoDong(lblTongSoDong);
            dataGridView1.DataSource = AD;
            dataGridView3ChiPhi.DataSource = AD;//!!!

            dataGridView2.DataSource = AD2;
            dataGridView3.DataSource = AD3;
            
            ReorderDataGridViewColumns();
            FormatGridView();
        }
        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView2, _orderList2);
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
            
            f = dataGridView1.Columns["GIA01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView1.Columns["GIA"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView1.Columns["GIA_NT0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["GIA_NT01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["GIA_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView1.Columns["TIEN0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView1.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView1.Columns["TIEN_NT0"];
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
            

            //Format GridView2
            f = dataGridView2.Columns["SO_LUONG"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            f = dataGridView2.Columns["T_TIEN_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView2.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView2.Columns["T_THUE_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView2.Columns["T_THUE"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }

            //GridView3
            f = dataGridView3.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            f = dataGridView3.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }

            f = dataGridView3.Columns["GIA01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView3.Columns["GIA"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
            }
            f = dataGridView3.Columns["GIA_NT0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView3.Columns["GIA_NT01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView3.Columns["GIA_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIANT");
            }
            f = dataGridView3.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView3.Columns["TIEN0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView3.Columns["TIEN_NT0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView3.Columns["CK_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIENNT");
            }
            f = dataGridView3.Columns["CK"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }

            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_AD, Invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView2, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3, Invoice.Mact);
        }
#endregion datagridview


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

            btnViewInfoData.Enabled = false;
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


        #region ==== Tính toán hóa đơn ====

        private bool NhapThueTuDong()
        {
            try
            {
                if ((Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    //&& _MA_GD != "A" && _MA_GD != "1"
                    && M_POA_MULTI_VAT == "1"
                    && !chkT_THUE_NT.Checked)
                {
                    //Xoa AD2
                    //AD2.Rows.Clear();
                    List<DataRow> removeList = new List<DataRow>();
                    foreach (DataRow row in AD2.Rows)
                    {
                        if (row["AUTO_YN"].ToString().Trim() == "1") removeList.Add(row);
                    }
                    while (removeList.Count > 0)
                    {
                        AD.Rows.Remove(removeList[0]);
                        removeList.RemoveAt(0);
                    }
                    //Them AD2 theo AD
                    //ma_thue_i / ngay_cti0 so_cti0 so_serii0 AD nhóm lại sum chuyển qua -> ADGT
                    Dictionary<string, Dictionary<string, object>> groupSumData = new Dictionary<string, Dictionary<string, object>>();

                    for (int i = 0; i < AD.Rows.Count; i++)
                    {
                        var adRow = AD.Rows[i];
                        string KEY = string.Format("[{0}][{1}][{2}][{3}]", adRow["ma_thue_i"].ToString().Trim(), ObjectAndString.ObjectToString(adRow["ngay_cti0"], "yyyyMMdd"),
                            adRow["so_cti0"].ToString().Trim(), adRow["so_serii0"].ToString().Trim());

                        //Tuanmh 23/12/2018 Check value
                        var _so_cti0=adRow["so_cti0"].ToString().Trim();
                        var _ngay_cti0 = ObjectAndString.ObjectToString(adRow["ngay_cti0"], "yyyyMMdd");
                        if (_so_cti0 == "" || _ngay_cti0=="") continue;

                        Dictionary<string, object> newRow;
                        if (groupSumData.ContainsKey(KEY))
                        {
                            // Cộng dồn giá trị.
                            newRow = groupSumData[KEY];
                            //++
                            newRow["T_TIEN_NT"] = ObjectAndString.ObjectToDecimal(newRow["T_TIEN_NT"]) + ObjectAndString.ObjectToDecimal(adRow["TIEN_NT0"]);
                            newRow["T_TIEN"] = ObjectAndString.ObjectToDecimal(newRow["T_TIEN"]) + ObjectAndString.ObjectToDecimal(adRow["TIEN0"]);
                            newRow["T_THUE_NT"] = ObjectAndString.ObjectToDecimal(newRow["T_THUE_NT"]) + ObjectAndString.ObjectToDecimal(adRow["THUE_NT"]);
                            newRow["T_THUE"] = ObjectAndString.ObjectToDecimal(newRow["T_THUE"]) + ObjectAndString.ObjectToDecimal(adRow["THUE"]);
                        }
                        else
                        {
                            newRow = new Dictionary<string, object>();
                            groupSumData.Add(KEY, newRow);

                            //neu co ngay_cti0, so_cti0 trong ad thi them vao ad2
                            if (adRow["Ngay_cti0"] != DBNull.Value && adRow["So_cti0"] != null &&
                                adRow["So_cti0"].ToString().Trim() != "")
                            {
                                for (int j = 0; j < AD.Columns.Count; j++)
                                {
                                    if (AD2.Columns.Contains(AD.Columns[j].ColumnName))
                                    {
                                        newRow[AD.Columns[j].ColumnName.ToUpper()] = adRow[AD.Columns[j].ColumnName];
                                    }
                                }

                                _sttRec02 = V6BusinessHelper.GetNewSttRec0(AD2);
                                newRow["STT_REC0"] = _sttRec02;
                                newRow["MAU_BC"] = 1;

                                newRow["MA_THUE"] = adRow["ma_thue_i"];
                                newRow["NGAY_CT0"] = adRow["ngay_cti0"];
                                newRow["SO_CT0"] = adRow["so_cti0"];
                                newRow["SO_SERI0"] = adRow["so_serii0"];

                                newRow["MA_KH"] = txtMaKh.Text;
                                newRow["TEN_KH"] = txtTenKh.Text;
                                newRow["DIA_CHI"] = txtDiaChi.Text;
                                newRow["TEN_VT"] = adRow["ten_vt"];
                                newRow["MA_SO_THUE"] = txtMaSoThue.Text;
                                //newRow["GHI_CHU"] = adRow["ghi_chu_t"];
                                //newRow["MA_KH2"] = adRow["ma_kh2_t"];

                                // Tuanmh 25/12/2018
                                string mathuei = adRow["MA_THUE_I"].ToString().Trim();
                                if (!string.IsNullOrEmpty(mathuei))
                                {

                                    var alThue = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE = '" + mathuei + "'");
                                    if (alThue.TotalRows > 0)
                                    {
                                        var tk_thue_i_Text = alThue.Data.Rows[0]["TK_THUE_NO"].ToString().Trim();
                                        newRow["TK_THUE_NO"] = tk_thue_i_Text;

                                    }
                                }

                                //newRow["TK_THUE_NO"] = adRow["tk_thue_i"];
                                newRow["TK_DU"] = txtManx.Text;
                                newRow["MA_VV"] = adRow["ma_vv_i"];
                                newRow["AUTO_YN"] = "1";
                                newRow["THUE_SUAT"] = adRow["THUE_SUAT_I"];

                                newRow["T_TIEN_NT"] = adRow["TIEN_NT0"];
                                newRow["T_TIEN"] = adRow["TIEN0"];
                                newRow["T_THUE_NT"] = adRow["thue_nt"];
                                newRow["T_THUE"] = adRow["thue"];
                            }
                        }
                    }

                    // Insert groupData vào AD2
                    foreach (KeyValuePair<string, Dictionary<string, object>> item in groupSumData)
                    {
                        AD2.AddRow(item.Value);
                    }
                    dataGridView2.DataSource = AD2;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
                return false;
            }
            return true;
        }

        public void TinhTongValues()
        {
            txtTongSoLuong1.Value = TinhTong(AD, "SO_LUONG1");
            txtTongSoLuong.Value = TinhTong(AD, "SO_LUONG");

            var tTienNt0 = TinhTong(AD, "TIEN_NT0");
            txtTongTienNt0.Value = V6BusinessHelper.Vround(tTienNt0, M_ROUND_NT);

            var tTien0 = TinhTong(AD, "TIEN0");
            txtTongTien0.Value = V6BusinessHelper.Vround(tTien0, M_ROUND);

            txtT_nk_nt.Value = TinhTong(AD, "NK_NT");
            txtT_nk_nt_ao.Value = txtT_nk_nt.Value;
            txtT_nk.Value = TinhTong(AD, "NK");
            txtT_nk_ao.Value = txtT_nk.Value;

            var tPsNoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_NO_NT", "OPER_TT");
            var tPsCoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_CO_NT", "OPER_TT");
            txtTongTangGiamNt.Value = tPsNoNt;
            

            var tPsNo = V6BusinessHelper.TinhTongOper(AD3, "PS_NO", "OPER_TT");
            var tPsCo = V6BusinessHelper.TinhTongOper(AD3, "PS_CO", "OPER_TT");
            txtTongTangGiam.Value = tPsNo;
        }

        public void TinhChietKhau()
        {
            try
            {
                var tTienNt0 = TinhTong(AD, "TIEN_NT0");
                var tyGia = txtTyGia.Value;
                var t_tien_nt0 = txtTongTienNt0.Value;
                txtTongTienNt0.Value = V6BusinessHelper.Vround(tTienNt0, M_ROUND_NT);
                decimal t_ck_nt = 0, t_ck = 0;



                if (chkLoaiChietKhau.Checked)//==1
                {
                    //Chiết khấu chung, chia theo phần trăm
                    //Tính phần trăm chiết khấu. Nếu check sua_ptck thì lấy luôn
                    //Nếu nhập tiền chiết khấu
                    if (chkSuaPtck.Checked || (!chkSuaTienCk.Checked && txtPtCk.Value > 0))
                    {
                        var ptck = txtPtCk.Value;
                        txtTongCkNt.ReadOnly = false;
                        //
                        t_ck_nt = V6BusinessHelper.Vround(ptck*tTienNt0/100, M_ROUND_NT);
                        t_ck = V6BusinessHelper.Vround(t_ck_nt*tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;

                        txtTongCkNt.Value = t_ck_nt;
                        txtTongCk.Value = t_ck;
                    }

                    else if (chkSuaTienCk.Checked)
                    {
                        t_ck_nt = txtTongCkNt.Value;
                        t_ck = V6BusinessHelper.Vround(t_ck_nt*tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;
                        txtTongCk.Value = t_ck;



                    }
                    var t_ck_nt_check = 0m;
                    var t_ck_check = 0m;
                    var index_ck = -1;

                    //tính chiết khấu cho mỗi chi tiết
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt0 != 0)
                        {
                            var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"]);
                            var tien0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien0"]);

                            var cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP_NT"]);
                            var cp = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP"]);
                            var gg_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["GG_NT"]);
                            var gg = ObjectAndString.ObjectToDecimal(AD.Rows[i]["GG"]);

                            var nk_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["NK_NT"]);
                            var nk = ObjectAndString.ObjectToDecimal(AD.Rows[i]["NK"]);

                            var ck_nt = V6BusinessHelper.Vround(tien_nt0 * t_ck_nt / t_tien_nt0, M_ROUND_NT);
                            var ck = V6BusinessHelper.Vround(ck_nt * tyGia, M_ROUND);

                            if (_maNt == _mMaNt0)
                                ck = ck_nt;


                            t_ck_nt_check = t_ck_nt_check + ck_nt;
                            t_ck_check += ck;

                            if (ck_nt != 0 && index_ck == -1)
                                index_ck = i;


                            //gán lại ck_nt
                            if (AD.Columns.Contains("CK_NT")) AD.Rows[i]["CK_NT"] = ck_nt;
                            if (AD.Columns.Contains("CK")) AD.Rows[i]["CK"] = ck;
                            if (AD.Columns.Contains("PT_CKI")) AD.Rows[i]["PT_CKI"] = txtPtCk.Value;
                            // gán lại tiền
                            if (AD.Columns.Contains("TIEN_NT")) AD.Rows[i]["TIEN_NT"] = tien_nt0 + cp_nt - ck_nt - gg_nt+nk_nt;
                            if (AD.Columns.Contains("TIEN")) AD.Rows[i]["TIEN"] = tien0 + cp - ck - gg+nk;
                        }
                    }
                    // Xu ly chenh lech
                    // Tìm dòng có số tiền
                    if (index_ck != -1)
                    {
                        decimal ck_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CK_NT"]) + (t_ck_nt - t_ck_nt_check);
                        AD.Rows[index_ck]["CK_NT"] = ck_nt;
                        decimal ck = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CK"]) + (t_ck - t_ck_check);
                        AD.Rows[index_ck]["CK"] = ck;

                        var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["Tien_nt0"]);
                        var tien0 = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["Tien0"]);
                        var cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CP_NT"]);
                        var cp = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CP"]);
                        var gg_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["GG_NT"]);
                        var gg = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["GG"]);

                        var nk_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["NK_NT"]);
                        var nk = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["NK"]);

                        decimal tien_nt = tien_nt0 + cp_nt - ck_nt - gg_nt+nk_nt;
                        AD.Rows[index_ck]["TIEN_NT"] = tien_nt;
                        decimal tien = tien0 + cp - ck - gg + nk;
                        AD.Rows[index_ck]["TIEN"] = tien;
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
                var tTienNt0 = TinhTong(AD, "TIEN_NT0");
                var tyGia = txtTyGia.Value;
                var t_tien_nt0 = txtTongTienNt0.Value;
                txtTongTienNt0.Value = V6BusinessHelper.Vround(tTienNt0, M_ROUND_NT);
                decimal t_gg_nt = 0, t_gg = 0;


                if (txtTongGiam.Value != 0)
                {

                    t_gg_nt = txtTongGiamNt.Value;
                    t_gg = V6BusinessHelper.Vround(t_gg_nt * tyGia, M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        t_gg = t_gg_nt;
                    }

                    txtTongGiamNt.Value = t_gg_nt;
                    txtTongGiam.Value = t_gg;

                    var t_gg_nt_check = 0m;
                    var t_gg_check = 0m;
                    var index_gg = -1;

                    //tính gg cho mỗi chi tiết
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt0 != 0)
                        {
                            var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"]);
                            var tien0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien0"]);

                            var cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP_NT"]);
                            var cp = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP"]);
                            var ck_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CK_NT"]);
                            var ck = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CK"]);

                            var nk_nt = ObjectAndString.ObjectToDecimal(AD.Rows[i]["NK_NT"]);
                            var nk = ObjectAndString.ObjectToDecimal(AD.Rows[i]["NK"]);

                            var gg_nt = V6BusinessHelper.Vround(tien_nt0 * t_gg_nt / t_tien_nt0, M_ROUND_NT);
                            var gg = V6BusinessHelper.Vround(gg_nt * tyGia, M_ROUND);

                            // Fix
                            if (_maNt == _mMaNt0)
                            {
                                gg = gg_nt;
                            }

                            t_gg_nt_check = t_gg_nt_check + gg_nt;
                            t_gg_check += gg;

                            if (gg_nt != 0 && index_gg == -1)
                                index_gg = i;

                            // gán lại chiết khấu
                            if (AD.Columns.Contains("GG_NT")) AD.Rows[i]["GG_NT"] = gg_nt;
                            if (AD.Columns.Contains("GG")) AD.Rows[i]["GG"] = gg;

                            // gán lại tiền
                            if (AD.Columns.Contains("TIEN_NT"))
                                AD.Rows[i]["TIEN_NT"] = tien_nt0 + cp_nt - ck_nt - gg_nt+nk_nt;
                            if (AD.Columns.Contains("TIEN")) AD.Rows[i]["TIEN"] = tien0 + cp - ck - gg+nk;

                        }
                    }
                    // Xu ly chenh lech
                    // Tìm dòng có số tiền
                    if (index_gg != -1)
                    {
                        decimal gg_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["GG_NT"]) +
                                        (t_gg_nt - t_gg_nt_check);
                        AD.Rows[index_gg]["GG_NT"] = gg_nt;
                        decimal gg = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["GG"]) + (t_gg - t_gg_check);
                        AD.Rows[index_gg]["GG"] = gg;

                        var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["Tien_nt0"]);
                        var tien0 = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["Tien0"]);
                        var cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["CP_NT"]);
                        var cp = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["CP"]);
                        var ck_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["CK_NT"]);
                        var ck = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["CK"]);

                        var nk_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["NK_NT"]);
                        var nk = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["NK"]);

                        decimal tien_nt = tien_nt0 + cp_nt - ck_nt - gg_nt +nk_nt;
                        AD.Rows[index_gg]["TIEN_NT"] = tien_nt;
                        decimal tien = tien0 + cp - ck - gg + nk;
                        AD.Rows[index_gg]["TIEN"] = tien;
                    }
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
            var thue_suat = txtThueSuat.Value;
            var t_thue_nt = 0m;
            var t_thue = 0m;
            var ty_gia = txtTyGia.Value;
            var t_tien_nt2 = txtTongTienNt0.Value;
            var t_tien2 = txtTongTien0.Value;
            //var t_gg_nt = txtTongGiamNt.Value;

            //var t_ck_nt = txtTongCkNt.Value;

            if (chkT_THUE_NT.Checked)//Tiền thuế gõ tự do
            {
                t_thue_nt = txtTongThueNt.Value;
                t_thue = V6BusinessHelper.Vround(t_thue_nt * ty_gia, M_ROUND);
            }
            else                   //Tiền thuế cộng dồn AD2
            {
                t_thue_nt = V6BusinessHelper.TinhTong(AD2, "T_THUE_NT");
                t_thue = V6BusinessHelper.TinhTong(AD2, "T_THUE");
                //tiền thuế = (tiền hàng - tiền giảm - chiết khấu) * thuế suất
                //t_thue_nt = (t_tien_nt2 - t_gg_nt - t_ck_nt)*thue_suat/100;
                //t_thue_nt = V6BusinessHelper.Vround(t_thue_nt, M_ROUND_NT);
                //sV("T_THUE_NT", t_thue_nt);
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
                    var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN_NT0"]);
                    var thue_nt = V6BusinessHelper.Vround(tien_nt2 * t_thue_nt / t_tien_nt2, M_ROUND);
                    t_thue_nt_check = t_thue_nt_check + thue_nt;

                    //var thue = V6BusinessHelper.Vround(thue_nt * ty_gia, M_ROUND);
                    //if (_maNt == _mMaNt0) thue = thue_nt;
                    
                    if (thue_nt != 0 && index == -1)
                        index = i;

                    AD.Rows[i]["Thue_nt"] = thue_nt;
                    
                }
                if (t_tien2 != 0)
                {
                    var tien2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN0"]);
                    var thue = V6BusinessHelper.Vround(tien2 * t_thue / t_tien2, M_ROUND);
                    t_thue_check = t_thue_check + thue;
                    
                    
                    if (thue != 0 && index == -1)
                        index = i;
                    
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

        public void TinhThueNhapKhau()
        {
            try
            {
                _nkNt.Value = V6BusinessHelper.Vround(_tienNt0.Value * _ts_nk.Value / 100, M_ROUND_NT);
                _nk.Value = V6BusinessHelper.Vround(_nkNt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _nk.Value = _nkNt.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        // Tuanmh 08/04/2018 Tinh lai khi thue suat chi tiet tung mat hang
        public void TinhLaiTienThueCT()
        {
            if (chkT_THUE_NT.Checked) return;

            //Tính tiền thuế theo thuế suất
            decimal thue_suati;
            decimal thue_nti;
            decimal thuei;
            decimal tien_truocthue_nti;

            var ty_gia = txtTyGia.Value;
            //var temp_maVt0 = new V6VvarTextBox { VVar = "MA_VT" };

            //tính thuế riêng cho từng chi tiết
            for (var i = 0; i < AD.Rows.Count; i++)
            {
                var row = AD.Rows[i];
                //temp_maVt.Text = ObjectAndString.ObjectToString(row["MA_VT"]).Trim();

                tien_truocthue_nti = ObjectAndString.ObjectToDecimal(row["TIEN_NT0"])
                                     + ObjectAndString.ObjectToDecimal(row["TIEN_VC_NT"])
                                     - ObjectAndString.ObjectToDecimal(row["CK_NT"])
                                     - ObjectAndString.ObjectToDecimal(row["GG_NT"]);

                //// Tuanmh 25/12/2018
                //string mathuei = row["MA_THUE_I"].ToString().Trim();
                //if (string.IsNullOrEmpty(mathuei))
                //{

                //    var alThue = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE = '" + mathuei + "'");
                //    if (alThue.TotalRows > 0)
                //    {
                //        var tk_thue_i_Text = alThue.Data.Rows[0]["TK_THUE_NO"].ToString().Trim();
                //        row["TK_THUE_I"] = tk_thue_i_Text;

                //    }
                //}

                thue_suati = ObjectAndString.ObjectToDecimal(row["THUE_SUAT_I"]);

                if (tien_truocthue_nti != 0)
                {
                    thue_nti = V6BusinessHelper.Vround(tien_truocthue_nti * thue_suati / 100, M_ROUND_NT);
                    thuei = V6BusinessHelper.Vround(thue_nti * ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thuei = thue_nti;


                    if (!AD.Columns.Contains("Thue_nt")) AD.Columns.Add("Thue_nt", typeof(decimal));
                    if (!AD.Columns.Contains("Thue")) AD.Columns.Add("Thue", typeof(decimal));

                    row["Thue_nt"] = thue_nti;
                    row["Thue"] = thuei;
                }
                else
                {
                    row["Thue_nt"] = 0m;
                    row["Thue"] = 0m;
                }
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

        private void XuLyThayDoiMaThue_i()
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = alThue.Data.Rows[0][THUE_SUAT] = " + alThue.Data.Rows[0]["THUE_SUAT"]);
                }
                else
                {
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - ko Gán thue_suat_i.Value vi alThueData ko co.");
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
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_POA_MULTI_VAT = " + M_POA_MULTI_VAT);
            if (M_POA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat_i.Value, _tienNt0.Value - _ckNt.Value - _ggNt.Value, _tien0.Value - _ck.Value - _gg.Value, _thue_nt, _thue);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tinh thue ct M_POA_MULTY_VAT = 1.");
            }
        }

        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
                if (NotAddEdit) return;
           
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();
                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt2, T_CK_NT, PT_CK
                TinhGiamGia();
                //TinhThue();
                if (M_POA_MULTI_VAT == "1")
                {
                    TinhLaiTienThueCT();
                    TinhTongThue_ct();
                }
                else
                {
                    TinhThue();
                }
                if (string.IsNullOrEmpty(_mMaNt0)) return;
                
                var t_tien_nt2 = txtTongTienNt0.Value;
                var t_gg_nt = txtTongGiamNt.Value;
                var t_ck_nt = txtTongCkNt.Value;
                var t_thue_nt = txtTongThueNt.Value;
                var T_nk_nt = txtT_nk_nt.Value;
                var t_cp_nt = txtT_cp_nt.Value;
                var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt ;
                var t_tc_nt = t_tt_nt + t_cp_nt+T_nk_nt;

                var t_tien2 = txtTongTien0.Value;
                var t_gg = txtTongGiam.Value;
                var t_ck = txtTongCk.Value;
                var t_thue = txtTongThue.Value;
                var T_nk = txtT_nk.Value;
                var t_cp = txtT_cp.Value;
                var t_tt = t_tien2 - t_gg - t_ck;
                var t_tc = t_tt + t_cp + T_nk;

                if (txtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                {
                    t_tt_nt = t_tt_nt + t_cp_nt;
                    t_tt = t_tt + t_cp;
                }
                TxtT_tc_nt.Value = V6BusinessHelper.Vround(t_tc_nt, M_ROUND_NT);
                txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

                TxtT_tc.Value = V6BusinessHelper.Vround(t_tc, M_ROUND);
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                //var tygia = txtTyGia.Value;
                //txtTongTien0.Value = V6BusinessHelper.Vround(t_tien_nt2*tygia, M_ROUND);
                //txtTongGiam.Value = V6BusinessHelper.Vround(t_gg_nt*tygia, M_ROUND);
                //txtTongCk.Value = V6BusinessHelper.Vround(t_ck_nt*tygia, M_ROUND);
                //TxtT_tc.Value = V6BusinessHelper.Vround(t_tc_nt * tygia, M_ROUND);
               
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
        }

        public void TinhTongCong()
        {
            var t_tien_nt2 = txtTongTienNt0.Value;
            var t_gg_nt = txtTongGiamNt.Value;
            var t_ck_nt = txtTongCkNt.Value;
            var t_thue_nt = txtTongThueNt.Value;
            var T_nk_nt = txtT_nk_nt.Value;
            var t_cp_nt = txtT_cp_nt.Value;
            var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt;
            var t_tc_nt = t_tt_nt + t_cp_nt + T_nk_nt;

            var t_tien2 = txtTongTien0.Value;
            var t_gg = txtTongGiam.Value;
            var t_ck = txtTongCk.Value;
            var t_thue = txtTongThue.Value;
            var T_nk = txtT_nk.Value;
            var t_cp = txtT_cp.Value;
            var t_tt = t_tien2 - t_gg - t_ck;
            var t_tc = t_tt + t_cp + T_nk;

            if (txtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
            {
                t_tt_nt = t_tt_nt + t_cp_nt;
                t_tt = t_tt + t_cp;
            }
            TxtT_tc_nt.Value = V6BusinessHelper.Vround(t_tc_nt, M_ROUND_NT);
            txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

            TxtT_tc.Value = V6BusinessHelper.Vround(t_tc, M_ROUND);
            txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);
        }

        #endregion tính toán

        #region ==== AM Methods ====
        private void LoadAll()
        {
            AM = Invoice.SearchAM("1=0", "1=0", "", "", "");//Làm AM khác null
            EnableControls();
            GetSoPhieuInit();
            LoadAlMaGia();
            LoadAlnt();
            LoadAlpost();
            GetM_ma_nt0();
            //LoadInfoDefine();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(Invoice.Mact, tabKhac, this);
            Ready();
        }

        private void LoadAlMaGia()
        {
        }

        private void LoadAlnt()
        {
            try
            {
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
                cboMaNt.DataSource = Invoice.Alnt;
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void LoadAlpost()
        {
            try
            {
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
                cboKieuPost.DataSource = Invoice.AlPost;
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void GetM_ma_nt0()
        {
            _mMaNt0 = V6Options.M_MA_NT0;
            //cboMaNt.SelectedValue = _mMaNt0;
            panelVND.Visible = false;
            txtT_cp_ao.Visible = false;
            txtT_cp.Visible = false;
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
            txtMa_ct.Text = Invoice.Mact;
            dateNgayCT.SetValue(V6Setting.M_SV_DATE);
            dateNgayLCT.SetValue(V6Setting.M_SV_DATE);
            //Tuanmh 25/01/2016- Ma_dvcs
            if (V6Login.MadvcsCount >= 1)
            {
                if (V6Login.Madvcs != "")
                {
                    txtMadvcs.Text = V6Login.Madvcs;
                    txtMadvcs.ExistRowInTable();
                }
            }

            //M_Ma_nk
            Txtma_nk.Text = Invoice.Alct["M_MA_NK"].ToString().Trim();
            TxtMa_gd.Text = "7";
            //
            txtManx.Text = Invoice.Alct["TK_CO"].ToString().Trim();
            cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();
            txtTk_i_ao.Text = txtManx.Text;

        }

        /// <summary>
        /// _maKhoI.SetInitFilter
        /// </summary>
        private void XuLyThayDoiMaDVCS()
        {
            try
            {
                string filter = V6Login.GetFilterKhoByDVCS(txtMadvcs.Text.Trim());
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

                var viewColumn = dataGridView1.Columns["GIA_NT01"];
                if (viewColumn != null)
                    viewColumn.HeaderText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _maNt;
                var column = dataGridView1.Columns["TIEN_NT0"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _maNt;

                viewColumn = dataGridView1.Columns["GIA01"];
                if (viewColumn != null)
                    viewColumn.HeaderText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                column = dataGridView1.Columns["TIEN0"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _mMaNt0;

                //GridView3
                string newText;
                viewColumn = dataGridView3.Columns["GIA_NT"];
                newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _maNt;
                if (viewColumn != null) viewColumn.HeaderText = newText;
                if (_gia_nt_33 != null) _gia_nt_33.GrayText = newText;

                column = dataGridView3.Columns["TIEN_NT"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _maNt;
                if (column != null) column.HeaderText = newText;
                if (_tien_nt_33 != null) _tien_nt_33.GrayText = newText;

                viewColumn = dataGridView3.Columns["GIA"];
                newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                if (viewColumn != null) viewColumn.HeaderText = newText;
                if (_gia_33 != null) _gia_33.GrayText = newText;

                column = dataGridView3.Columns["TIEN"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _mMaNt0;
                if (column != null) column.HeaderText = newText;
                if (_tien_33 != null) _tien_33.GrayText = newText;

                if (_maNt.ToUpper() != _mMaNt0.ToUpper())
                {

                    M_ROUND_NT = V6Setting.RoundTienNt;
                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA_NT = V6Setting.RoundGiaNt;
                    M_ROUND_GIA = V6Setting.RoundGia;


                    txtTyGia.Enabled = true;
                    //ShowIDs(["GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "DivTienVND", "DOCSOTIEN_VND"], true);
                    detail1.ShowIDs(new[] { "GIA01", "lblGIA01", "TIEN0", "lblTIEN0" });
                    panelVND.Visible = true;
                    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains("TIEN"))
                    {
                        panelNT.Visible = false;
                        panelVND.Visible = false;
                        lblDocSoTien.Visible = false;
                    }
                    txtT_cp_ao.Visible = true;
                    txtT_cp.Visible = true;

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA01");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtT_cp.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_cp_ao.DecimalPlaces = V6Options.M_IP_TIEN;

                    txtT_nk_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtT_nk_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    

                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongTienNt0.DecimalPlaces = V6Options.M_IP_TIEN_NT;

                    _t_tien22.Visible = true;
                    _t_thue22.Visible = true;

                    //Detail3
                    detail3.ShowIDs(new[] { "PS_NO", "PS_CO" });

                    gridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    // Show Dynamic control
                    if (_PsNoNt_33 != null) _PsNo_33.VisibleTag();
                    if (_PsCo_33 != null) _PsCo_33.VisibleTag();
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
                    detail1.HideIDs(new[] { "GIA01", "lblGIA01", "TIEN0", "lblTIEN0" });
                    panelVND.Visible = false;
                    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains("TIEN"))
                    {
                        panelNT.Visible = false;
                        panelVND.Visible = false;
                        lblDocSoTien.Visible = false;
                    }
                    txtT_cp_ao.Visible = false;
                    txtT_cp.Visible = false;
                    txtT_nk_ao.Visible = false;

                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_cp.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_cp_ao.DecimalPlaces = V6Options.M_IP_TIEN;

                    txtT_nk_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtT_nk_ao.DecimalPlaces = V6Options.M_IP_TIEN;

                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongTienNt0.DecimalPlaces = V6Options.M_IP_TIEN;

                    _t_tien22.Visible = false;
                    _t_thue22.Visible = false;

                    //Detail3
                    detail3.HideIDs(new[] { "PS_NO", "PS_CO" });

                    gridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    // Show Dynamic control
                    if (_PsNoNt_33 != null) _PsNo_33.InvisibleTag();
                    if (_PsCo_33 != null) _PsCo_33.InvisibleTag();
                }

                FormatNumberControl();
                FormatNumberGridView();

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
                var decimalPlaces = _maNt == _mMaNt0 ? V6Options.M_IP_TIEN : V6Options.M_IP_TIEN_NT;
                //AM
                foreach (Control control in panelNT.Controls)
                {
                    V6NumberTextBox textBox = control as V6NumberTextBox;
                    if (textBox != null)
                        textBox.DecimalPlaces = decimalPlaces;
                }
                foreach (Control control in panelVND.Controls)
                {
                    V6NumberTextBox textBox = control as V6NumberTextBox;
                    if (textBox != null)
                        textBox.DecimalPlaces = V6Options.M_IP_TIEN;
                }
                //AD
                //_tienNt.DecimalPlaces = decimalPlaces;
                //_phaiTtNt.DecimalPlaces = decimalPlaces;
                _tienNt.DecimalPlaces = decimalPlaces;

                _t_tien_nt22.DecimalPlaces = decimalPlaces;
                _t_thue_nt22.DecimalPlaces = decimalPlaces;

                _PsNoNt_33.DecimalPlaces = decimalPlaces;
                _PsCoNt_33.DecimalPlaces = decimalPlaces;
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
                //GridView3
                column = dataGridView3.Columns["Ps_co_nt"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }

                if (_maNt != _mMaNt0)
                {
                    column = dataGridView3ChiPhi.Columns["CP_NT"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN_NT;
                    }
                    column = dataGridView3ChiPhi.Columns["NK_NT"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN_NT;
                    }

                    column = dataGridView3ChiPhi.Columns["CP"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["NK"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }

                    column = dataGridView3ChiPhi.Columns["TIEN_NT0"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN_NT;
                    }
                    column = dataGridView3ChiPhi.Columns["TIEN0"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                }
                else
                {
                    column = dataGridView3ChiPhi.Columns["CP_NT"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["NK_NT"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["CP"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["NK"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["TIEN_NT0"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
                    column = dataGridView3ChiPhi.Columns["TIEN0"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN;
                    }
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
                var alThue = V6BusinessHelper.Select("ALTHUE", "*",
                    "MA_THUE = '" + txtMa_thue.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    txtTkThueNo.Text = alThue.Data.Rows[0]["TK_THUE_NO"].ToString().Trim();
                    txtThueSuat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    txtTkThueCo.Text = txtManx.Text;
                }

                TinhTongThanhToan("XuLyThayDoiMaThue");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue " + _sttRec, ex);
            }
        }
        
        /// <summary>
        /// Lấy dữ liệu AD va AD2 dựa vào rec, tạo 1 copy gán vào AD
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

            //Load AD2
            if (AD2Tables == null) AD2Tables = new SortedDictionary<string, DataTable>();
            if (AD2Tables.ContainsKey(sttRec)) AD2 = AD2Tables[sttRec].Copy();
            else
            {
                AD2Tables.Add(sttRec, Invoice.LoadAd2(sttRec));
                AD2 = AD2Tables[sttRec].Copy();
            }

            //Load AD3
            if (AD3Tables == null) AD3Tables = new SortedDictionary<string, DataTable>();
            if (AD3Tables.ContainsKey(sttRec)) AD3 = AD3Tables[sttRec].Copy();
            else
            {
                AD3Tables.Add(sttRec, Invoice.LoadAd3(sttRec));
                AD3 = AD3Tables[sttRec].Copy();
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
        public override void ViewInvoice(string sttrec, V6Mode mode)
        {
            try
            {
                Mode = V6Mode.View;

                //Co 2 truong hop them moi roi view va sua roi view
                _sttRec = sttrec;
                var loadAM = Invoice.SearchAM("", "Stt_rec='" + _sttRec + "'", "", "", "");
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
        }

        /// <summary>
        /// Hiển thị hóa đơn đã tải với CurrentIndex
        /// Cần set trước AD cho đúng với index
        /// </summary>
        private void ViewInvoice()
        {
            try
            {
                Mode = V6Mode.View;
                var row = AM.Rows[CurrentIndex];
                V6ControlFormHelper.SetFormDataRow(this, row);
                txtMadvcs.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                txtMa_kh_i_ao.Text = row["Ma_kh_i"].ToString().Trim();
                txtT_cp_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp"]);
                txtTk_i_ao.Text = row["Tk_i"].ToString().Trim();
                txtT_cp_nt_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp_nt"]);

                txtT_nk_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Nk"]);
                txtT_nk_nt_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Nk_nt"]);
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost);

                XuLyThayDoiMaDVCS();
                SetGridViewData();
                XuLyThayDoiMaNt();
                Mode = V6Mode.View;
                //btnSua.Focus();
                FormatNumberControl();
                FormatNumberGridView();
                //Tinh toan rieng
                TinhTongCong();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
        }

        #region ==== Add Thread ====
        private bool flagAddFinish, flagAddSuccess;
        private IDictionary<string, object> addDataAM;
        private List<IDictionary<string, object>> addDataAD, addDataAD2, addDataAD3;
        private string addErrorMessage = "";

        /// <summary>
        /// Thêm dữ liệu vào csdl (AM, AD, POST)
        /// </summary>
        private void DoAddThread()
        {
            ReadyForAdd();
            Timer checkAdd = new Timer();
            checkAdd.Interval = 500;
            checkAdd.Tick += checkAdd_Tick;
            flagAddFinish = false;
            flagAddSuccess = false;
            InvokeFormEvent(FormDynamicEvent.BEFOREADD);
            InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
            new Thread(DoAdd)
            {
                IsBackground = true
            }
            .Start();
            
            checkAdd.Start();
        }
        
        void checkAdd_Tick(object sender, EventArgs e)
        {
            if (flagAddFinish)
            {
                ((Timer)sender).Stop();

                if (flagAddSuccess)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.AddSuccess);
                    ShowParentMessage(V6Text.AddSuccess);
                    ViewInvoice(_sttRec, V6Mode.Add);
                    btnMoi.Focus();
                    All_Objects["mode"] = V6Mode.Add;
                    All_Objects["AM_DATA"] = addDataAM;
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
                addDataAD = dataGridView1.GetData(_sttRec);
                addDataAD2 = dataGridView2.GetData(_sttRec);
                addDataAD3 = dataGridView3.GetData(_sttRec);
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
                CheckForIllegalCrossThreadCalls = false;//han che loi trong Thread khi goi control
                
                if (Invoice.InsertInvoice(addDataAM, addDataAD, addDataAD2, addDataAD3))
                {
                    flagAddSuccess = true;
                }
                else
                {
                    flagAddSuccess = false;
                    addErrorMessage = V6Text.Text("ADD0");
                    Invoice.PostErrorLog(_sttRec, "M");
                }
            }
            catch (Exception ex)
            {
                flagAddSuccess = false;
                addErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "M " + _sttRec, ex);
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            if (_print_flag == V6PrintMode.AutoClickPrint)
                Thread.Sleep(2000);
            flagAddFinish = true;
        }
#endregion add

        #region ==== Edit Thread ====
        private bool flagEditFinish, flagEditSuccess;
        private List<IDictionary<string, object>> editDataAD, editDataAD2, editDataAD3;
        private string editErrorMessage = "";

        private void DoEditThread()
        {
            ReadyForEdit();
            Timer checkEdit = new Timer();
            checkEdit.Interval = 500;
            checkEdit.Tick += checkEdit_Tick;
            flagEditFinish = false;
            flagEditSuccess = false;
            InvokeFormEvent(FormDynamicEvent.BEFOREEDIT);
            InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
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
                editDataAD2 = dataGridView2.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in editDataAD2)
                {
                    adRow["DATE0"] = am_DATE0;
                    adRow["TIME0"] = am_TIME0;
                    adRow["USER_ID0"] = am_U_ID0;
                }
                editDataAD3 = dataGridView3.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in editDataAD3)
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
        void checkEdit_Tick(object sender, EventArgs e)
        {
            if (flagEditFinish)
            {
                ((Timer)sender).Stop();
                
                
                

                if (flagEditSuccess)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.EditSuccess);
                    ShowParentMessage(V6Text.EditSuccess);
                    ViewInvoice(_sttRec, V6Mode.Edit);
                    //wm += "\nView finish " + w.Elapsed;
                    btnMoi.Focus();
                    All_Objects["mode"] = V6Mode.Edit;
                    All_Objects["AM_DATA"] = addDataAM;
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

                //wm += "\nReady Invoice.InsertInvoice " + w.Elapsed;
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, editDataAD2, editDataAD3, keys))
                {
                    flagEditSuccess = true;
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                    Mode = V6Mode.View;
                }
                else
                {
                    flagEditSuccess = false;
                    editErrorMessage = V6Text.Text("SUA0");
                    Invoice.PostErrorLog(_sttRec, "S");
                }
                //wm += "\nFinish Invoice.InsertInvoice " + w.Elapsed;
            }
            catch (Exception ex)
            {
                flagEditSuccess = false;
                editErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "S " + _sttRec, ex);
            }

            if (_print_flag == V6PrintMode.AutoClickPrint)
                Thread.Sleep(2000);
            flagEditFinish = true;
        }
#endregion edit

        #region ==== Delete Thread ====
        private bool flagDeleteFinish, flagDeleteSuccess;
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
                        flagDeleteFinish = false;
                        flagDeleteSuccess = false;
                        InvokeFormEvent(FormDynamicEvent.BEFOREDELETE);
                        new Thread(DoDelete)
                        {
                            IsBackground = true
                        }
                            .Start();

                        checkDelete.Start();
                        detail1.MODE = V6Mode.Lock;
                        detail2.MODE = V6Mode.Lock;
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
        void checkDelete_Tick(object sender, EventArgs e)
        {
            if (flagDeleteFinish)
            {
                ((Timer)sender).Stop();

                if (flagDeleteSuccess)
                {
                    if (_timForm != null && !_timForm.IsDisposed)
                        _timForm.UpdateAM(_sttRec, null, V6Mode.Delete);

                    All_Objects["mode"] = V6Mode.Delete;
                    All_Objects["AM_DATA"] = addDataAM;
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
            //Xóa xong view lại cái khác
            try
            {
                
                var row = AM.Rows[CurrentIndex];
                _sttRec = row["Stt_rec"].ToString().Trim();
                if (Invoice.DeleteInvoice(_sttRec))
                {
                    flagDeleteSuccess = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = V6Text.Text("XOA0");
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice.DeleteInvoice return false.");
                }
                        
            }
            catch (Exception ex)
            {
                flagDeleteSuccess = false;
                deleteErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "X " + _sttRec, ex);
            }
            flagDeleteFinish = true;
        }
        #endregion delete

        private void Luu()
        {
            try
            {
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||
                    detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit ||
                    detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    EnableFunctionButtons();
                }
                else
                {
                    V6ControlFormHelper.RemoveRunningList(_sttRec);
                    addDataAM = PreparingDataAM(Invoice);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD2);
                    V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD3);
                    
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

                    ResetForm();
                    Mode = V6Mode.Add;

                    GetSttRec(Invoice.Mact);
                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                    //GetSoPhieu();
                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    detail1.DoAddButtonClick();
                    SetControlReadOnlyHide(detail1, Invoice, V6Mode.Add);
                    SetDefaultDetail();
                    detail2.MODE = V6Mode.Init;
                    detail3.MODE = V6Mode.Init;
                    txtMa_sonb.Focus();
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
        
        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                if (IsViewingAnInvoice)
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
                                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
                                        txtManx.Text.Trim(), dateNgayCT.Date, txtTongThanhToan.Value, "E");

                                if (check_edit == true)
                                {
                                    Mode = V6Mode.Edit;
                                    detail1.MODE = V6Mode.View;
                                    detail2.MODE = V6Mode.View;
                                    detail3.MODE = V6Mode.View;
                                    //SetDataGridView3ChiPhiReadOnly();
                                    txtMa_sonb.Focus();
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
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Sua " + _sttRec, ex);
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
                                txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
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
                this.ShowErrorException(GetType() + ".Xoa " + _sttRec, ex);
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
                            this.ShowWarningMessage("Chưa chọn phiếu nhập.");
                        }
                        else
                        {
                            GetSttRec(Invoice.Mact);
                            SetNewValues();
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                            Mode = V6Mode.Add;
                            detail1.MODE = V6Mode.View;
                            detail2.MODE = V6Mode.View;
                            detail3.MODE = V6Mode.View;
                            //SetDataGridView3ChiPhiReadOnly();
                            txtMa_sonb.Focus();
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
                txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);
                dateNgayCT.SetValue(V6Setting.M_SV_DATE);
                dateNgayLCT.SetValue(V6Setting.M_SV_DATE);
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
                if (!IsViewingAnInvoice) return;
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
                    c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                    {
                        if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                        if (!sender.IsDisposed) sender.Dispose();
                    };
                    c.ShowToForm(this, V6Text.PrintPOB, true);
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

        private TimPhieuNhapKhauForm _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) _timForm = new TimPhieuNhapKhauForm(this);
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
                        _timForm = new TimPhieuNhapKhauForm(this);
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
        //public string MA_KHOPH { get { return txtMa_khoPH.Text.Trim(); } set { txtMa_khoPH.Text = value; } }
        //public string MA_VITRIPH { get { return txtMa_vitriPH.Text.Trim(); } set { txtMa_vitriPH.Text = value; } }
        /// <summary>
        /// Lưu và in (click nút in, chọn máy in, không in ngay), có hiển thị form in trước 3 giây.
        /// </summary>
        private void LuuVaIn()
        {
            if ((Mode == V6Mode.Add || Mode == V6Mode.Edit))
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
                detail2.SetData(null);
                detail3.SetData(null);

                LoadAD("");
                SetGridViewData();
                
                ResetAllVars();
                EnableVisibleControls();
                SetFormDefaultValues();
                btnMoi.Focus();
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
            cboLoai_pb.SelectedIndex = -1;
        }

        private void SetFormDefaultValues()
        {
            chkLoaiChietKhau.Checked = true;//loai ck chung
            cboKieuPost.SelectedIndex = 1;
            txtLoai_pb.Text = "0";
            switch (Mode)
            {
                case V6Mode.Init:
                case V6Mode.View:
                    chkSuaPtck.Enabled = false;
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkSuaTienCk.Enabled = false;
                    chkSuaTienCk.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                    break;
                case V6Mode.Add:
                case V6Mode.Edit:
                    chkSuaPtck.Enabled = true;
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkSuaTienCk.Enabled = true;
                    chkSuaTienCk.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                break;
            }
        }

        private void Huy()
        {
            try
            {
                dataGridView1.UnLock();
                if (Mode == V6Mode.Edit)
                {
                    if (this.ShowConfirmMessage(V6Text.DiscardConfirm) == DialogResult.Yes)
                    {
                        V6ControlFormHelper.RemoveRunningList(_sttRec);
                        Mode = V6Mode.View;
                        ViewInvoice(CurrentIndex);
                        btnMoi.Focus();
                    }
                }
                if (Mode == V6Mode.Add)
                {
                    if (this.ShowConfirmMessage(V6Text.DiscardConfirm) == DialogResult.Yes)
                    {
                        V6ControlFormHelper.RemoveRunningList(_sttRec);
                        Mode = V6Mode.View;
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
                txtSoPhieu.Text = ((TabControl)(p.Parent)).TabPages.Count.ToString();
            }
            else
            {
                txtSoPhieu.Text = "01";    
            }
            
        }

        private void GetSoPhieu()
        {
            txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

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

        private void XuLyDetailClickAdd(object sender)
        {
            try
            {
                SetDefaultDetail();
                SetControlReadOnlyHide(detail1, Invoice, V6Mode.Add);
                _maVt.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void XuLyDetail2ClickAdd(object sender)
        {
            try
            {
                //V6ControlFormHelper.SetFormDataDic(sender, new SortedDictionary<string, object>());
                if (AD2 == null || AD2.Rows.Count == 0)
                {
                    _so_ct022.Text = txtSoCt0.Text;
                    _ngay_ct022.Value = txtNgayCt0.Value;
                    _so_seri022.Text = txtSoSeri0.Text;
                    _ma_kh22.Text = txtMaKh.Text;
                    _ten_kh22.Text = txtTenKh.Text;
                    _dia_chi22.Text = txtDiaChi.Text;
                    _ma_so_thue22.Text = txtMaSoThue.Text;
                    _t_tien22.Value = txtTongTien0.Value + txtT_nk.Value;
                    _t_tien_nt22.Value = txtTongTienNt0.Value;
                    _thue_suat22.Value = txtThueSuat.Value;
                    _tk_thue_no22.Text = txtTkThueNo.Text.Trim();
                    _tk_du22.Text = txtTkThueCo.Text.Trim();
                    _han_tt22.Value = txtHanTT.Value;
                    _mau_bc.Value = 1;
                    if (_ma_kh22.Text.Trim() == "")
                    {
                        //enable
                    }
                    else
                    {
                        _ten_kh22.Enabled = _ten_kh22.Text.Trim() == "";
                        _dia_chi22.Enabled = _dia_chi22.Text.Trim() == "";
                        _ma_so_thue22.Enabled = _ma_so_thue22.Text.Trim() == "";
                    }
                    //TinhTienThue22();
                    Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat22.Value, _t_tien_nt22.Value, _t_tien22.Value, _t_thue_nt22, _t_thue22);
                }
                else
                {
                    _so_ct022.Text = txtSoCt0.Text;
                    _ngay_ct022.Value = txtNgayCt0.Value;
                    _so_seri022.Text = txtSoSeri0.Text;
                    _ma_kh22.Text = txtMaKh.Text;
                    _ten_kh22.Text = txtTenKh.Text;
                    _dia_chi22.Text = txtDiaChi.Text;
                    _ma_so_thue22.Text = txtMaSoThue.Text;

                    _tk_thue_no22.Text = txtTkThueNo.Text.Trim();
                    _tk_du22.Text = txtTkThueCo.Text.Trim();
                    _han_tt22.Value = txtHanTT.Value;
                    _mau_bc.Value = 1;
                }
                _mau_bc.Focus();
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
                    
                    if (AD.Rows.Count > 0)
                    {
                        var cIndex = AD.Rows.Count - 1;
                        dataGridView1.Rows[cIndex].Selected = true;
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

        private bool XuLyThemDetail2(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                _sttRec02 = V6BusinessHelper.GetNewSttRec0(AD2);
                data.Add("STT_REC0", _sttRec02);

                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("SO_CT0") || data["SO_CT0"].ToString().Trim() == "")
                {
                    var label = "SO_CT0";
                    var lbl = detail2.GetControlByName("lbl" + label);
                    if (lbl != null) label = lbl.Text;
                    error += V6Text.NoInput + " [" + label + "]\n";
                }
                if (!data.ContainsKey("NGAY_CT0") || data["NGAY_CT0"] == DBNull.Value)
                {
                    var label = "NGAY_CT0";
                    var lbl = detail2.GetControlByName("lbl" + label);
                    if (lbl != null) label = lbl.Text;
                    error += V6Text.NoInput + " [" + label + "]\n";
                }
                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD2.NewRow();
                    foreach (DataColumn column in AD2.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(key) ? data[key] : "") ?? DBNull.Value;
                        newRow[key] = value;
                    }
                    AD2.Rows.Add(newRow);
                    dataGridView2.DataSource = AD2;
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
            TinhTongThanhToan("xu ly them detail2");
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
                            //Sửa dòng dữ liệu.
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

        
        private bool XuLySuaDetail2(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv2EditingRow != null)
                {
                    var cIndex2 = _gv2EditingRow.Index;

                    if (cIndex2 >= 0 && cIndex2 < AD2.Rows.Count)
                    {
                        //Thêm thông tin...
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Date;


                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("SO_CT0") || data["SO_CT0"].ToString().Trim() == "")
                        {
                            var label = "SO_CT0";
                            var lbl = detail2.GetControlByName("lbl" + label);
                            if (lbl != null) label = lbl.Text;
                            error += V6Text.NoInput + " [" + label + "]\n";
                        }
                        if (!data.ContainsKey("NGAY_CT0") || data["NGAY_CT0"] == DBNull.Value)
                        {
                            var label = "NGAY_CT0";
                            var lbl = detail2.GetControlByName("lbl" + label);
                            if (lbl != null) label = lbl.Text;
                            error += V6Text.NoInput + " [" + label + "]\n";
                        }
                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = AD2.Rows[cIndex2];
                            foreach (DataColumn column in AD2.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            dataGridView2.DataSource = AD2;
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
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhTongThanhToan("xy ly sua detail2");
            return true;
        }

        private void XuLyDeleteDetail()
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var cIndex = dataGridView1.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        var currentRow = AD.Rows[cIndex];
                        var details = "Mã vật tư: " + currentRow["Ma_vt"];
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm +
                                                                   details)
                            == DialogResult.Yes)
                        {
                            AD.Rows.Remove(currentRow);
                            dataGridView1.DataSource = AD;
                            detail1.SetData(null);
                            TinhTongThanhToan("xu ly xoa detail");
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
        private void XuLyDeleteDetail2()
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (dataGridView2.CurrentRow != null)
                {
                    var cIndex2 = dataGridView2.CurrentRow.Index;
                    if (cIndex2 >= 0 && cIndex2 < AD2.Rows.Count)
                    {
                        var currentRow = AD2.Rows[cIndex2];
                        var details = "stt_rec0: " + currentRow["Stt_rec0"];
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm +
                                                                   details)
                            == DialogResult.Yes)
                        {
                            AD2.Rows.Remove(currentRow);
                            dataGridView2.DataSource = AD2;
                            detail2.SetData(null);
                            TinhTongThanhToan("xu ly xoa detail");
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

        #region ==== hoadonDetail Event ====
        private void hoaDonDetail1_Load(object sender, EventArgs e)
        {

        }

        
        private void hoaDonDetail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd(sender);
        }
        private void detail2_ClickAdd(object sender)
        {
            XuLyDetail2ClickAdd(sender);
        }

        private void hoaDonDetail1_AddHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_AddHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail2(data))
            {
                if (XuLyThemDetail2(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        

        private void hoaDonDetail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                    if (_gv1EditingRow == null)
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                        return;
                    }
                    dataGridView1.Lock();
                    detail1.ChangeToEditMode();
                    SetControlReadOnlyHide(detail1, Invoice, V6Mode.Edit);
                    
                    if (!string.IsNullOrEmpty(_sttRec0))
                    {
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

        private void hoaDonDetail2_ClickEdit(object sender)
        {
            try
            {
                if (AD2 != null && AD2.Rows.Count > 0 && dataGridView2.DataSource != null)
                {
                    detail2.ChangeToEditMode();
                    _sttRec02 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView2, detail2, out _gv2EditingRow);
                    if (!string.IsNullOrEmpty(_sttRec02))
                    {
                        if (_ma_kh22.Text.Trim() == "")
                        {
                            //enable true
                        }
                        else
                        {
                            _ten_kh22.Enabled = _ten_kh22.Text.Trim() == "";
                            _dia_chi22.Enabled = _dia_chi22.Text.Trim() == "";
                            _ma_so_thue22.Enabled = _ma_so_thue22.Text.Trim() == "";
                        }
                        _mau_bc.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void hoaDonDetail1_EditHandle(IDictionary<string, object> data)
        {
            dataGridView1.UnLock();
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_EditHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail2(data))
            {
                if (XuLySuaDetail2(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail1_DeleteHandle(object sender)
        {
            XuLyDeleteDetail();
        }
        private void hoaDonDetail2_DeleteHandle(object sender)
        {
            XuLyDeleteDetail2();
        }
        private void hoaDonDetail1_ClickCancelEdit(object sender)
        {
            dataGridView1.UnLock();
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }
        private void hoaDonDetail2_ClickCancelEdit(object sender)
        {
            detail2.SetData(_gv2EditingRow.ToDataDictionary());
        }

        #endregion hoadoen detail event
        
        private void dateNgayCT_ValueChanged(object sender, EventArgs e)
        {
            if (!Invoice.M_NGAY_CT) dateNgayLCT.SetValue(dateNgayCT.Date);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
            }
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (detail2.IsViewOrLock)
                detail2.SetData(dataGridView2.GetCurrentRowData());
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
                XuLyHienThiChietKhau_PhieuNhap(chkLoaiChietKhau.Checked, chkSuaTienCk.Checked, _pt_cki, _ckNt, txtTongCkNt, chkSuaPtck);

                TinhTongThanhToan("LoaiChietKhau_Change");
            }
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
        

        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSuaTien);
                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
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
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyHienThiChietKhau_PhieuNhap(chkLoaiChietKhau.Checked, chkSuaTienCk.Checked, _pt_cki, _ckNt, txtTongCkNt, chkSuaPtck);
            }
        }

        private void chkSuaTienThue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    txtTongThueNt.ReadOnly = !chkT_THUE_NT.Checked;
                    if (chkT_THUE_NT.Checked && M_POA_MULTI_VAT == "1")
                    {
                        //_thue_nt.Enabled = true;
                        txtTongThueNt.ReadOnly = true;
                    }
                    else
                    {
                        //_thue_nt.Enabled = false;
                    }
                }

                TinhTongThanhToan("chkSuaTienThue_CheckedChanged");
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
                if(fieldName == "GIA_NT01") fcaption += " "+ cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT0") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "GIA01") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN0") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if(!(new List<string> {"TEN_VT","MA_VT"}).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
        }
        private void dataGridView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            var fieldName = e.Column.DataPropertyName.ToUpper();
            if (_alct2Dic.ContainsKey(fieldName))
            {
                var row = _alct2Dic[fieldName];
                var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                if (fieldName == "T_THUE_NT") fcaption += " " + cboMaNt.SelectedValue;
                if (fieldName == "T_TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "T_THUE") fcaption += " " + _mMaNt0;
                if (fieldName == "T_TIEN") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if (!(new List<string> { "TEN_VT", "MA_VT" }).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
        }

        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {
            SetTabPageText(txtSoPhieu.Text);
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
        }

        private void txtMaKh_Leave(object sender, EventArgs e)
        {
            //if (txtDiaChi.Text.Trim() == "")
            //{
            //    txtDiaChi.Enabled = true;
            //}
            //else
            //{
            //    txtDiaChi.Enabled = false;
            //}
        }

        private void HoaDonBanHangKiemPhieuXuat_VisibleChanged(object sender, EventArgs e)
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
                _tienNt0.Enabled = chkSuaTien.Checked;
            if (chkSuaTien.Checked)
            {
                _tienNt0.EnableTag();
                _tien0.EnableTag();
                _gia01.EnableTag();
            }
            else
            {
                _tienNt0.DisableTag();
                _tien0.DisableTag();
                _gia01.DisableTag();
            }

            if (!dataGridView3ChiPhi.ReadOnly)
            {
                if (_maNt != _mMaNt0)
                {

                    dataGridView3ChiPhi.SetEditColumn(chkSuaTien.Checked ? "CP,CP_NT,NK,NK_NT".Split(',') : "CP_NT,NK_NT".Split(','));
                }
                else
                {
                    dataGridView3ChiPhi.SetEditColumn("CP_NT,NK_NT".Split(','));
                }

            }
        }

        private void chkSuaPtck_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                txtPtCk.ReadOnly = !chkSuaPtck.Checked;
        }
        private void MaKhach22_V6LostFocus(object sender)
        {
            XuLyChonMaKhach22();
        }
        
        private void hoaDonDetail2_Load(object sender, EventArgs e)
        {

        }

        private void Txt_cp_ao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender == txtMa_kh_i_ao)
                {
                    Txtma_kh_i.Text = txtMa_kh_i_ao.Text;
                }
                else if (sender == txtTk_i_ao)
                {
                    TxtTk_i.Text = txtTk_i_ao.Text;
                }
                else if (sender == txtT_cp_nt_ao)
                {
                    txtT_cp_nt.Value = txtT_cp_nt_ao.Value;
                }
                else if (sender == txtT_cp_ao)
                {
                    txtT_cp.Value = txtT_cp_ao.Value;
                }
            }
            catch // (Exception)
            {
                // ignored
            }
        }
        
        private void txtT_nk_ao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender == txtMa_kh_i_ao)
                {
                    Txtma_kh_i.Text = txtMa_kh_i_ao.Text;
                }
                else if (sender == txtTk_i_ao)
                {
                    TxtTk_i.Text = txtTk_i_ao.Text;
                }
                else if (sender == txtT_nk_nt_ao)
                {
                    txtT_nk_nt.Value = txtT_nk_nt_ao.Value;
                }
                else if (sender == txtT_nk_ao)
                {
                    txtT_nk.Value = txtT_nk_ao.Value;
                }
            }
            catch // (Exception)
            {
                // ignored
            }
        }

        private void cboLoai_pb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Lấy giá trị
            var loai_pb = "";
            if (cboLoai_pb.SelectedIndex == 0)
                loai_pb = "0";
            else if (cboLoai_pb.SelectedIndex == 1)
                loai_pb = "1";
            else if (cboLoai_pb.SelectedIndex == 2)
                loai_pb = "2";

            SetGridViewChiPhiEditAble(loai_pb, chkSuaTien.Checked, dataGridView3ChiPhi);

            //if (loai_pb == "1" || loai_pb == "2")
            //{
            //    TinhPhanBoChiPhi(loai_pb);
            //}
            //Đổi giá trị textbox
            txtLoai_pb.Text = loai_pb;
        }

        
        private bool pb_changed;
        private void TxtLoai_pb_TextChanged(object sender, EventArgs e)
        {
            //Chống lặp
            if (pb_changed)
            {
                pb_changed = false;
                return;
            }
            //Lấy giá trị
            var loai_bp = txtLoai_pb.Text.Trim();
            if ("012".Contains(loai_bp))
            {
                pb_changed = true;
            }

            //Đổi cbo
            if (txtLoai_pb.Text == "1")
                cboLoai_pb.SelectedIndex = 1;
            else if (txtLoai_pb.Text == "2")
                cboLoai_pb.SelectedIndex = 2;
            else if (txtLoai_pb.Text == "0")
                cboLoai_pb.SelectedIndex = 0;
            else cboLoai_pb.SelectedIndex = -1;
        }

        public void TinhPhanBoChiPhi(string loai_pb)
        {
            try
            {
                if (loai_pb.Length > 1) loai_pb = loai_pb.Left(1);
                if (loai_pb == "0") return;

                var t_he_so = loai_pb == "1"? txtTongTienNt0.Value : txtTongSoLuong1.Value;
                var t_cp_nt_check = 0m;
                var t_cp_check = 0m;
                var index = -1;
                
                if (t_he_so != 0)
                {
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        var heso_01 = ObjectAndString.ObjectToDecimal(AD.Rows[i][loai_pb == "1" ? "TIEN_NT0" : "SO_LUONG1"]);
                        var cp_nt = V6BusinessHelper.Vround((heso_01 / t_he_so) * txtT_cp_nt.Value, M_ROUND_NT);
                        t_cp_nt_check = t_cp_nt_check + cp_nt;
                        var cp = V6BusinessHelper.Vround(cp_nt*txtTyGia.Value, M_ROUND);
                        if (txtT_cp_nt.Value == 0)
                            cp = V6BusinessHelper.Vround((heso_01 / t_he_so) * txtT_cp.Value, M_ROUND);
                        

                        if (_maNt == _mMaNt0 && txtT_cp_nt.Value != 0)
                            cp = cp_nt;

                        t_cp_check = t_cp_check + cp;

                        if (index == -1)
                        {
                            if (cp_nt != 0) index = i;
                            else if (cp != 0) index = i;
                        }
                        //if (cp_nt != 0 && index == -1)
                        //    index = i;
                        //else if (cp_nt == 0 && index == -1 && cp != 0)
                        //    index = i;

                        AD.Rows[i]["Cp_nt"] = cp_nt;
                        AD.Rows[i]["Cp"] = cp;

                        //tuanmh 14/10/2016
                        if (txtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                        {
                            AD.Rows[i]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp_nt"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Nk_nt"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck_nt"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg_nt"]);
                            AD.Rows[i]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien0"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Nk"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg"]);
                        }
                    }
                }

                // Xu ly chenh lech
                // Tìm dòng có số tiền
                if (index != -1)
                {
                    decimal cp_nt_value = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"]) + (txtT_cp_nt.Value - t_cp_nt_check);
                    decimal cp_value = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"]) + (txtT_cp.Value - t_cp_check);

                    AD.Rows[index]["Cp_nt"] = cp_nt_value;
                    AD.Rows[index]["Cp"] = cp_value;

                    if (txtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                    {
                        AD.Rows[index]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien_nt0"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk_nt"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Ck_nt"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Gg_nt"]);
                        AD.Rows[index]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien0"]) 
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Ck"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Gg"]);
                    }
                }

                dataGridView1.DataSource = AD;
                dataGridView3ChiPhi.DataSource = AD;
                //SetDataGridView3ChiPhiReadOnly();

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhPhanBoNhapKhau(string loai_pb)
        {
            try
            {
                if (loai_pb.Length > 1) loai_pb = loai_pb.Left(1);
                if (loai_pb == "0") return;

                var t_he_so = loai_pb == "1" ? txtTongTienNt0.Value : txtTongSoLuong1.Value;
                var t_nk_nt_check = 0m;
                var t_nk_check = 0m;
                var index = -1;

                if (t_he_so != 0)
                {

                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        var heso_01 = ObjectAndString.ObjectToDecimal(AD.Rows[i][loai_pb == "1" ? "TIEN_NT0" : "SO_LUONG1"]);

                        var nk_nt = V6BusinessHelper.Vround((heso_01 / t_he_so) * txtT_nk_nt.Value, M_ROUND_NT);

                        t_nk_nt_check = t_nk_nt_check + nk_nt;

                        //var nk = V6BusinessHelper.Vround(nk_nt * txtTyGia.Value, M_ROUND);
                        var nk = V6BusinessHelper.Vround(nk_nt * txtTyGia.Value, M_ROUND);
                        if (txtT_nk_nt.Value == 0)
                            nk = V6BusinessHelper.Vround((heso_01 / t_he_so) * txtT_nk.Value, M_ROUND);

                        if (_maNt == _mMaNt0 && txtT_nk_nt.Value != 0)
                            nk = nk_nt;

                        t_nk_check = t_nk_check + nk;

                        if (index == -1)
                        {
                            if (nk_nt != 0) index = i;
                            else if (nk != 0) index = i;
                        }

                        AD.Rows[i]["Nk_nt"] = nk_nt;
                        AD.Rows[i]["Nk"] = nk;

                        //tuanmh 14/10/2016
                        //if (TxtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                        {
                            AD.Rows[i]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp_nt"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Nk_nt"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck_nt"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg_nt"]);
                            AD.Rows[i]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien0"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp"])
                                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Nk"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck"])
                                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg"]);
                        }
                    }
                }

                // Xu ly chenh lech
                // Tìm dòng có số tiền
                if (index != -1)
                {
                    decimal _nk_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk_nt"]) + (txtT_nk_ao.Value - t_nk_nt_check);
                    decimal _nk = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk"]) + (txtT_nk.Value - t_nk_check);

                    AD.Rows[index]["Nk_nt"] = _nk_nt;
                    AD.Rows[index]["Nk"] = _nk;

                    //if (TxtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                    {
                        AD.Rows[index]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien_nt0"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk_nt"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Ck_nt"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Gg_nt"]);
                        AD.Rows[index]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien0"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"])
                                                    + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Nk"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Ck"])
                                                    - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Gg"]);
                    }
                }

                dataGridView1.DataSource = AD;
                dataGridView3ChiPhi.DataSource = AD;
                //SetDataGridView3ChiPhiReadOnly();

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XoaPhanBoChiPhi()
        {
            try
            {
                for (var i = 0; i < AD.Rows.Count; i++)
                {
                    AD.Rows[i]["Cp_nt"] = 0;
                    AD.Rows[i]["Cp"] = 0;
                }
                dataGridView1.DataSource = AD;
                dataGridView3ChiPhi.DataSource = AD;
                //SetDataGridView3ChiPhiReadOnly();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XoaPhanBoNhapKhau()
        {
            try
            {
                for (var i = 0; i < AD.Rows.Count; i++)
                {
                    AD.Rows[i]["Nk_nt"] = 0;
                    AD.Rows[i]["Nk"] = 0;
                }
                dataGridView1.DataSource = AD;
                dataGridView3ChiPhi.DataSource = AD;
                //SetDataGridView3ChiPhiReadOnly();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        //private void SetDataGridView3ChiPhiReadOnly()
        //{
        //    if (Mode == V6Mode.Edit || Mode == V6Mode.Add)
        //    {
        //        dataGridView3ChiPhi.ReadOnly = false;
        //        foreach (DataGridViewColumn column in dataGridView3ChiPhi.Columns)
        //        {
        //            if (column.DataPropertyName.ToUpper() == "CP" || column.DataPropertyName.ToUpper() == "CP_NT")
        //                column.ReadOnly = false;
        //            else column.ReadOnly = true;
        //        }
        //    }
        //    else
        //    {
        //        dataGridView3ChiPhi.ReadOnly = true;
        //    }
        //}

        List<string> gridView3ChiPhiFields = new List<string>() { "MA_VT", "TEN_VT", "DVT1", "SO_LUONG1", "CP_NT", "CP", "NK_NT", "NK", "TIEN_NT0", "TIEN0" };
        private void dataGridView3ChiPhi_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            try
            {
                if (gridView3ChiPhiFields.Contains(e.Column.DataPropertyName.ToUpper()))
                {
                    e.Column.HeaderText = CorpLan2.GetFieldHeader(e.Column.DataPropertyName);
                    e.Column.Visible = true;
                }
                else
                {
                    e.Column.Visible = false;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void TxtT_cp_nt_ao_V6LostFocus(object sender)
        {
            TinhT_CpNt();
            var loai_pb = txtLoai_pb.Text.Trim();
            TinhPhanBoChiPhi(loai_pb);
        }
        
        private void TxtT_nk_nt_ao_V6LostFocus(object sender)
        {
            TinhT_NkNt();
            var loai_pb = txtLoai_pb.Text.Trim();
            TinhPhanBoNhapKhau(loai_pb);
        }

        public void TinhT_CpNt()
        {
            try
            {
                if (txtT_cp_nt_ao.Value != 0)
                {
                    txtT_cp_ao.Value = V6BusinessHelper.Vround((txtT_cp_nt_ao.Value * txtTyGia.Value), M_ROUND);
                }
                if (_maNt == _mMaNt0 && txtT_cp_nt_ao.Value != 0)
                {
                    txtT_cp_ao.Value = txtT_cp_nt_ao.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhT_NkNt()
        {
            try
            {
                if (txtT_nk_nt_ao.Value != 0)
                {
                    txtT_nk_ao.Value = V6BusinessHelper.Vround((txtT_nk_nt_ao.Value * txtTyGia.Value), M_ROUND);
                }
                if (_maNt == _mMaNt0 && txtT_nk_nt_ao.Value != 0)
                {
                    txtT_nk_ao.Value = txtT_nk_nt_ao.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            txtMa_kh_i_ao.Text = txtMaKh.Text;
            XuLyChonMaKhachHang();
        }

        private void txtManx_V6LostFocus(object sender)
        {
            //Tuanmh 09/09/2017 Loi Tk_i
            if (txtTk_i_ao.Text.Trim() == "" || txtT_cp_nt_ao.Value + txtT_cp_ao.Value == 0)
            {
                txtTk_i_ao.Text = txtManx.Text;
            }
        }

        private bool ValidateData_Master()
        {
            try
            {
                // Check Master
                if (!ValidateNgayCt(Invoice.Mact, dateNgayCT))
                {
                    return false;
                }

                if (V6Login.MadvcsTotal > 0 && txtMadvcs.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaDVCS.Text);
                    txtMadvcs.Focus();
                    return false;
                }
                if (txtMaKh.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaKH.Text);
                    txtMaKh.Focus();
                    return false;
                }
                if (txtManx.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaNX.Text);
                    txtManx.Focus();
                    return false;
                }
                if (txtManx.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage(V6Text.Text("TKNOTCT"));
                    txtManx.Focus();
                    return false;
                }
                if (cboKieuPost.SelectedIndex == -1)
                {
                    this.ShowWarningMessage(V6Text.Text("CHUACHONKIEUPOST"));
                    cboKieuPost.Focus();
                    return false;
                }

                ValidateMasterData(Invoice);


                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoInputDetail);
                    return false;
                }
                if (!CheckPhanBo(dataGridView3ChiPhi, txtT_cp_nt.Value))
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKPHANBO"));
                    return false;
                }

                //Nhập thuế tự động
                if (!NhapThueTuDong())
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKTHUE"));
                    return false;
                }

                //Check nh_dk ps_no = ps_co
                var groupDic = new SortedDictionary<string, decimal[]>();
                foreach (DataRow row in AD3.Rows)
                {
                    var nhomDK = row["Nh_dk"].ToString().Trim();
                    var ps_no = ObjectAndString.ObjectToDecimal(row["Ps_no_nt"]);
                    var ps_co = ObjectAndString.ObjectToDecimal(row["Ps_co_nt"]);
                    if (groupDic.ContainsKey(nhomDK))
                    {
                        var group = groupDic[nhomDK];
                        group[0] += ps_no;
                        group[1] += ps_co;
                        groupDic[nhomDK] = group;
                    }
                    else
                    {
                        var group = new decimal[] { 0, 0 };
                        group[0] += ps_no;
                        group[1] += ps_co;
                        groupDic[nhomDK] = group;
                    }
                }
                var checkChiTietError = "";
                foreach (KeyValuePair<string, decimal[]> item in groupDic)
                {
                    var group = item.Value;
                    if (group[0] != group[1])
                    {
                        checkChiTietError += string.Format(V6Text.Text("KTNDKPSNKPSC") + " {0}\n", item.Key);
                    }
                }
                if (checkChiTietError.Length > 0)
                {
                    this.ShowWarningMessage(checkChiTietError);
                    return false;
                }

                //Tuanmh 16/02/2016 Check Voucher Is exist 
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
                                return false;
                                
                        }
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
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec, txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
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
        private bool ValidateData_Detail2(IDictionary<string, object>  data)
        {
            try
            {
                //if (_tkDt.Int_Data("Loai_tk") == 0)
                //{
                //    this.ShowWarningMessage(V6Text.Text("TKNOTCT"));
                //    return false;
                //}
                string firstErrorField;
                string errors = ValidateDetail2Data(detail2, Invoice, data, out firstErrorField);
                if (!string.IsNullOrEmpty(errors))
                {
                    this.ShowWarningMessage(errors);
                    var c = detail1.GetControlByAccessibleName(firstErrorField);
                    if (c != null) c.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail2 " + _sttRec, ex);
            }
            return true;
        }
        private void btnViewInfoData_Click(object sender, EventArgs e)
        {
            ShowViewInfoData(Invoice);
        }
        
        private void txtMa_sonb_V6LostFocus(object sender)
        {
            GetSoPhieu();
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
                detail1.AutoFocus();
            }
            else if (tabControl1.SelectedTab == tabThue)
            {
                detail2.AutoFocus();
            }
            else if (tabControl1.SelectedTab == tabChiTietBoSung)
            {
                detail3.AutoFocus();
            }
        }

        private void btnTinhPB_Click(object sender, EventArgs e)
        {
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;

            if(shift_is_down) TinhPhanBoNhapKhau(txtLoai_pb.Text);
            else TinhPhanBoChiPhi(txtLoai_pb.Text.Trim());
        }

        private void btnXoaPB_Click(object sender, EventArgs e)
        {
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;

            if (shift_is_down) XoaPhanBoNhapKhau();
            else XoaPhanBoChiPhi();
        }

        private void txtMadvcs_TextChanged(object sender, EventArgs e)
        {
            XuLyThayDoiMaDVCS();
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

        private void ChucNang_ChonDonHangMua(bool add = false)
        {
            try
            {
                chon_accept_flag_add = add;
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CDH_PNKForm chon = new CDH_PNKForm(dateNgayCT.Date, txtMadvcs.Text, txtMaKh.Text);
                    _chon_px = "DH";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_kh == "")
                        message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "")
                        message += V6Text.NoInput + lblMaDVCS.Text;
                    this.ShowWarningMessage(message);
                    if (ma_kh == "") txtMaKh.Focus();
                    else if (ma_dvcs == "") txtMadvcs.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(
                    string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private string _chon_px = "";
        void  chon_AcceptSelectEvent(List<IDictionary<string, object>> selectedDataList)
        {
            try
            {
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                detail1.MODE = V6Mode.View;
                if (flag_add)
                {
                    DoNothing();
                }
                else
                {
                    AD.Rows.Clear();
                }
                int addCount = 0, failCount = 0;
                foreach (IDictionary<string, object> data in selectedDataList)
                {
                    var newData = new SortedDictionary<string, object>(data);
                    string ma_vt = newData["MA_VT"].ToString().Trim();
                    V6VvarTextBox temp_vt = new V6VvarTextBox()
                    {
                        VVar = "MA_VT",
                    };
                    temp_vt.Text = ma_vt;
                    temp_vt.RefreshLoDateYnValue();

                    if (newData.ContainsKey("SO_LUONG"))
                    {
                        decimal insert = ObjectAndString.ObjectToDecimal(newData["SO_LUONG"]);
                        decimal heso = 1;
                        string dvt1 = newData["DVT1"].ToString().Trim();
                        SqlParameter[] plist =
                            {
                                new SqlParameter("@p1", ma_vt),
                                new SqlParameter("@p2", dvt1),
                            };
                        var dataHeso =
                            V6BusinessHelper.Select("Alqddvt", "*", "ma_vt=@p1 and dvt=@p2", "", "", plist).Data;
                        if (dataHeso.Rows.Count > 0)
                        {
                            heso = ObjectAndString.ObjectToDecimal(dataHeso.Rows[0]["HE_SO"]);
                        }
                        if (heso == 0) heso = 1;
                        newData["SO_LUONG1"] = insert / heso;
                        var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);
                        if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                        {
                            newData["SL_QD"] = insert / HS_QD1;
                        }
                    }

                    if (XuLyThemDetail(newData)) addCount++;
                    else failCount++;
                }
                All_Objects["selectedDataList"] = selectedDataList;
                InvokeFormEvent("AFTERCHON_" + _chon_px);
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed {1}.", addCount, failCount));
                if (addCount > 0)
                {
                    co_chon_don_hang = true;
                }
                else
                {
                    co_chon_don_hang = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

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

        private void ChucNang_ChonTuExcel()
        {
            try
            {
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

        private void ChonDonHangMuaMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonDonHangMua(shift);
        }

        private void TroGiupMenu_Click(object sender, EventArgs e)
        {
            ChucNang_TroGiup();
        }

        private void chonTuExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ChonTuExcel();
        }

        private decimal _num;
        private decimal _con_lai;
        private void dataGridView3ChiPhi_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                // Tính số cần nhập dựa theo TxtT_cp_nt_ao
                decimal num = 0;
                string FIELD = dataGridView3ChiPhi.EditingColumn.DataPropertyName.ToUpper();
                if (FIELD == "CP_NT" || FIELD == "CP" || FIELD == "NK_NT" || FIELD == "NK")
                {
                    if (FIELD == "CP_NT") num = txtT_cp_nt_ao.Value;
                    else if (FIELD == "CP") num = txtT_cp_ao.Value;
                    else if (FIELD == "NK_NT") num = txtT_nk_nt_ao.Value;
                    else if (FIELD == "NK") num = txtT_nk_ao.Value;
                    _num = num;
                    if (_num == 0) return;
                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != dataGridView3ChiPhi.EditingRow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells[FIELD].Value);
                        }
                    }
                }

                _con_lai = num;
                var text = V6Text.CheckData + ": " + ObjectAndString.NumberToString(num, 2, V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                V6ControlFormHelper.SetStatusText(text);
                ShowParentMessage(text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3ChiPhi_CellBeginEdit " + _sttRec, ex);
            }
        }

        private void dataGridView3ChiPhi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_num == 0) return;
                // Cập nhập cột đang sửa không được lớn hơn giá trị còn lại.
                string FIELD = dataGridView3ChiPhi.EditingColumn.DataPropertyName.ToUpper();
                if (FIELD == "CP_NT" || FIELD == "NK_NT" || FIELD == "CP" || FIELD == "NK")
                {
                    var inputValue = ObjectAndString.ObjectToDecimal(dataGridView3ChiPhi.EditingCell.Value);
                    if (inputValue > _con_lai)
                    {
                        ShowParentMessage(V6Text.CheckData + ": " + _con_lai);
                        inputValue = _con_lai;
                        dataGridView3ChiPhi.EditingCell.Value = inputValue;
                    }
                    if (FIELD == "CP_NT" || FIELD == "NK_NT")
                    {
                        var foreignValue = V6BusinessHelper.Vround(inputValue*txtTyGia.Value, M_ROUND);
                        dataGridView3ChiPhi.EditingRow.Cells[FIELD.Substring(0, FIELD.Length - 3)].Value = foreignValue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3ChiPhi_CellEndEdit " + _sttRec, ex);
            }
        }

        private void dataGridView3ChiPhi_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //V6ControlFormHelper.ApplyNumberTextBox(e.Control);
            //e.Control.KeyPress += (sender1, e1) =>
            //{
            //    if (!char.IsControl(e1.KeyChar) && !char.IsDigit(e1.KeyChar) && e1.KeyChar != '.')
            //    {
            //        e1.Handled = true;
            //    }
            //};
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView2, dataGridView3, dataGridView3ChiPhi);
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

        private void menuXemPhieuNhap_Click(object sender, EventArgs e)
        {
            XemPhieuNhapView(dateNgayCT.Date, Invoice.Mact, _maKhoI.Text, _maVt.Text);
        }

        private void xuLyKhacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
        }

        private void thayTheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(Invoice);
        }

        private void thayThe2toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_SuaNhieuDong(Invoice);
        }

        private DataTable _dataLoDate;
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
                    }
                    else
                    {
                        _ton13.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void cboKieuPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Invoice.Alct["M_MA_VV"].ToString().Trim() == "1")
            {
                lblKieuPostColor.Visible = true;
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost);
            }
            else
            {
                lblKieuPostColor.Visible = false;
            }
        }

        private void dataGridView3ChiPhi_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var ccell = dataGridView3ChiPhi.CurrentCell;
                if (ccell == null) return;
                var ccolumn = ccell.OwningColumn;
                var crow = ccell.OwningRow;
                if (ccolumn.ReadOnly) return;

                decimal currentCellValue = ObjectAndString.ObjectToDecimal(ccell.Value);
                if (currentCellValue != 0) return;

                decimal num = 0;
                string FIELD = ccolumn.DataPropertyName.ToUpper();
                if (FIELD == "CP_NT" || FIELD == "NK_NT" || FIELD == "CP" || FIELD == "NK")
                {
                    if (FIELD == "CP_NT") num = txtT_cp_nt_ao.Value;
                    else if (FIELD == "NK_NT") num = txtT_nk_nt_ao.Value;
                    else if (FIELD == "CP") num = txtT_cp_ao.Value;
                    else if (FIELD == "NK") num = txtT_nk_ao.Value;

                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != crow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells[FIELD].Value);
                        }
                    }

                    ccell.Value = num;
                    if (FIELD == "CP_NT" || FIELD == "NK_NT")
                    {
                        var foreignValue = V6BusinessHelper.Vround(num*txtTyGia.Value, M_ROUND);
                        crow.Cells[FIELD.Substring(0, FIELD.Length - 3)].Value = foreignValue;
                    }
                }
                //else if (FIELD == "CP")
                //{
                //    num = TxtT_cp_ao.Value;
                //    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                //    {
                //        if (row != crow)
                //        {
                //            num -= ObjectAndString.ObjectToDecimal(row.Cells["CP"].Value);
                //        }
                //    }
                //    ccell.Value = num;
                //}
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3ChiPhi_CurrentCellChanged " + _sttRec, ex);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabThue)
            {
                NhapThueTuDong();
            }
        }
    }
}
