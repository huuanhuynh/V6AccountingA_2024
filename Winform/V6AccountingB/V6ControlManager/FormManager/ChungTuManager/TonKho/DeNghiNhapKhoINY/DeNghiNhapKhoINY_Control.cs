﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonDonHang;
using V6ControlManager.FormManager.ChungTuManager.TonKho.DeNghiNhapKhoINY.ChonDonHangBan;
using V6ControlManager.FormManager.ChungTuManager.TonKho.DeNghiNhapKhoINY.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.DeNghiNhapKhoINY
{
    public partial class DeNghiNhapKhoINY_Control : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice94INY Invoice = new V6Invoice94INY();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public DeNghiNhapKhoINY_Control()
        {
            InitializeComponent();
            MyInit();
        }
        public DeNghiNhapKhoINY_Control(string itemId)
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
        public DeNghiNhapKhoINY_Control(string maCt, string itemId, string sttRec)
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

            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtManx.Upper();
            txtDiaChi2.DisableUpperLower();

            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                txtMa_sonb.SetInitFilter("MA_DVCS='"+V6Login.Madvcs+ "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }

            txtDiaChi2.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
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

            cboKieuPost.SelectedIndex = 0;
            cboLoai_pb.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            LoadDetail2Controls();
            LoadAdvanceControls(Invoice.Mact);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        private V6ColorTextBox _dvt, txtPTEN_KHC, txtPONG_BAC, txtPDIEN_THOAIC;
        private V6VvarTextBox _maVt, _dvt1, _maKho, _maKhoI, _tkVt, _maLo, _ma_thue_i, txtPMA_KHC;
        private V6NumberTextBox _giaNt, _giaNt01, _tien0, _tienNt0, _gia0, _gia01, _gia, _gia_Nt0,
            _ck, _ckNt, _pt_cki, _thue, _thue_nt, _thue_suat_i;
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _ton13, _tienNt, _tien, _mau_bc;
        private V6NumberTextBox _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4, _ggNt, _gg, _tien_vcNt, _tien_vc, _cpNt, _cp;
        private V6DateTimeColor _hanSd;
        private V6ColorTextBox _so_ct022,_so_seri022,_ten_kh22, _dia_chi22,_ma_so_thue22;
        private V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22;
        private V6DateTimeColor _ngay_ct022;
        private V6NumberTextBox _t_tien22, _t_tien_nt22, _thue_suat22, _t_thue22, _t_thue_nt22, _gia_Nt022;

        
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
                    case "PMA_KHC":
                        txtPMA_KHC = control as V6VvarTextBox;
                        if (txtPMA_KHC != null)
                        {
                            txtPMA_KHC.TextChanged += delegate(object sender, EventArgs args)
                            {
                                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                                {
                                    if (txtPMA_KHC.Text.Trim() == "")
                                    {
                                        V6ControlFormHelper.SetControlReadOnly(txtPTEN_KHC, false);
                                        V6ControlFormHelper.SetControlReadOnly(txtPONG_BAC, false);
                                        V6ControlFormHelper.SetControlReadOnly(txtPDIEN_THOAIC, false);
                                    }
                                    else
                                    {
                                        V6ControlFormHelper.SetControlReadOnly(txtPTEN_KHC, true);
                                        V6ControlFormHelper.SetControlReadOnly(txtPONG_BAC, true);
                                        V6ControlFormHelper.SetControlReadOnly(txtPDIEN_THOAIC, true);
                                    }
                                }
                            };

                            txtPMA_KHC.V6LostFocus += delegate(object sender)
                            {
                                var data = txtPMA_KHC.Data;
                                if (data != null)
                                {
                                    SetControlValue(txtPTEN_KHC, data["TEN_KH"]);
                                    SetControlValue(txtPONG_BAC, data["DOI_TAC"]);
                                    SetControlValue(txtPDIEN_THOAIC, data["DIEN_THOAI"]);
                                }
                                else
                                {
                                    SetControlValue(txtPTEN_KHC, null);
                                    SetControlValue(txtPONG_BAC, null);
                                    SetControlValue(txtPDIEN_THOAIC, null);
                                }
                            };
                        }
                        break;
                    case "PTEN_KHC":
                        txtPTEN_KHC = control as V6ColorTextBox;
                        break;
                    case "PONG_BAC":
                        txtPONG_BAC = control as V6ColorTextBox;
                        break;
                    case "PDIEN_THOAIC":
                        txtPDIEN_THOAIC = control as V6ColorTextBox;
                        break;

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
                            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Hứng thue_suat_i ok.");
                        }
                        else
                        {
                            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Hứng thue_suat_i không được.");
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
                            CheckSoLuong1();
                        };
                        _soLuong1.V6LostFocusNoChange += delegate
                        {

                        };
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
                            if (IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit) && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                            {
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                            }
                        };
                        break;
                    case "GIA_NT":
                        _giaNt = (V6NumberTextBox)control;
                        break;
                    case "GIA":
                        _gia = (V6NumberTextBox)control;
                        break;
                    case "GIA0":
                        _gia0 = (V6NumberTextBox)control;
                        break;
                    case "GIA01":
                        _gia01 = (V6NumberTextBox)control;
                        break;
                    //_soLuong.Tag = "hide";
                    case "GIA_NT0":
                        _gia_Nt0 = (V6NumberTextBox)control;
                        break;
                    case "GIA_NT01":
                        _giaNt01 = (V6NumberTextBox)control;
                        _giaNt01.V6LostFocus += GiaNt01_V6LostFocus;
                        break;
                    case "TIEN":
                        _tien = (V6NumberTextBox)control;
                        break;
                    case "TIEN_NT":
                        _tienNt = (V6NumberTextBox) control;
                        break;
                    case "TIEN_NT0":
                        _tienNt0 = (V6NumberTextBox)control;

                        _tienNt0.Enabled = chkSuaTien.Checked;
                        if (chkSuaTien.Checked)
                        {
                            _tienNt0.Tag = null;
                        }
                        else
                        {
                            _tienNt0.Tag = "disable";
                        }
                    
                        _tienNt0.V6LostFocus += TienNt0_V6LostFocus;
                        break;
                    case "TIEN0":
                        _tien0 = (V6NumberTextBox)control;
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
                    case "SL_QD":
                        _sl_qd = control as V6NumberTextBox;
                        if (_sl_qd != null)
                        {
                            if (M_CAL_SL_QD_ALL == "0")
                            {
                                _sl_qd.Enabled = false;
                                if (_sl_qd.Tag == null || _sl_qd.Tag.ToString() != "hide") _sl_qd.Tag = "disable";
                            }
                            else if (M_CAL_SL_QD_ALL == "1")
                            {
                                _sl_qd.EnableTag();
                            }
                            _sl_qd.V6LostFocus += delegate
                            {
                                TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;

                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    CheckSoLuong1();
                                    chkT_THUE_NT.Checked = false;
                                    Tinh_thue_ct();
                                }
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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
                    case "HS_QD4":
                        _hs_qd4 = (V6NumberTextBox)control;
                        _hs_qd4.V6LostFocus += delegate(object sender)
                        {
                            TinhGiamGiaCt();
                        };
                        break;
                    case "HS_QD3":
                        _hs_qd3 = (V6NumberTextBox)control;
                        if (_hs_qd3 != null)
                        {
                            _hs_qd3.V6LostFocus += delegate(object sender)
                            {
                                TinhVanChuyen();
                            };
                        }
                        break;
                    case "CP":
                        _cp = (V6NumberTextBox)control;
                        break;
                    case "CP_NT":
                        _cpNt = control as V6NumberTextBox;
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
                    case "MA_LO":
                        //_maLo = (V6ColorTextBox)control;
                        _maLo = (V6VvarTextBox)control;
                        _maLo.Upper();

                        _maLo.GotFocus += delegate
                        {
                            if (_maVt.Text != "")
                            {
                                _maLo.SetInitFilter("Ma_vt='" + _maVt.Text.Trim() + "'");
                            }
                        };
                        _maLo.V6LostFocus += _maLo_V6LostFocus;
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        _hanSd.Tag = "disable";
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

        /// <summary>
        ///  Hàm giả cho giống tên bên HoaDon
        /// </summary>
        private void CheckSoLuong1()
        {
            TinhTienNt0();
            Tinh_thue_ct();
        }

        private void LoadDetail2Controls()
        {
            detail2.lblName.AccessibleName = "";
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct2, out _orderList2, out _alct2Dic);
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
                            _ma_kh22.V6LostFocus += delegate
                            {
                                XuLyChonMaKhach22();
                            };
                        }
                        break;
                    case "TK_THUE_NO":

                        _tk_thue_no22 = (V6VvarTextBox)control;
                        _tk_thue_no22.Upper();
                        _tk_thue_no22.SetInitFilter("Loai_tk=1");
                        break;
                    case "TK_DU":

                        _tk_du22 = (V6VvarTextBox)control;
                        _tk_du22.Upper();
                        _tk_du22.SetInitFilter("Loai_tk=1");
                        break;
                    case "NGAY_CT0":
                        _ngay_ct022 = control as V6DateTimeColor;
                        if (_ngay_ct022 != null)
                        {
                        
                        }
                        break;
                    case "GIA_NT0":
                        _gia_Nt022 = control as V6NumberTextBox;
                        if (_gia_Nt022 != null)
                        {

                        }
                        break;
                    case "T_TIEN":
                        _t_tien22 = control as V6NumberTextBox;
                        if (_ngay_ct022 != null)
                        {
                            //if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            //{
                            //    _gia_Nt022.InvisibleTag();
                            //}
                            //if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            //{
                            //    _gia_Nt022.ReadOnlyTag();
                            //}
                        }
                        break;
                    case "T_TIEN_NT":
                        _t_tien_nt22 = control as V6NumberTextBox;
                        if (_t_tien_nt22 != null)
                        {
                            _t_tien_nt22.V6LostFocus += delegate
                            {
                                TinhTienThue22();
                            };
                        }
                        break;
                    case "THUE_SUAT":
                        _thue_suat22 = control as V6NumberTextBox;
                        if (_thue_suat22 != null)
                        {
                            _thue_suat22.V6LostFocus += delegate
                            {
                                TinhTienThue22();
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
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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

        private void TinhTienThue22()
        {
            try
            {
                _t_tien22.Value = V6BusinessHelper.Vround(_t_tien_nt22.Value*txtTyGia.Value, M_ROUND);
                _t_thue_nt22.Value = V6BusinessHelper.Vround(_t_tien_nt22.Value * _thue_suat22.Value / 100, M_ROUND_NT);
                _t_thue22.Value = V6BusinessHelper.Vround(_t_thue_nt22.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _t_tien22.Enabled = false;
                    _t_thue22.Enabled = false;

                    _t_tien22.Value = _t_tien_nt22.Value;
                    _t_thue22.Value = _t_thue_nt22.Value;
                }
                else
                {
                    _t_tien22.Enabled = true;
                    _t_thue22.Enabled = true;
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese ?
                "F4-Nhận/thêm chi tiết, F7-Lưu và in, F8-Xóa chi tiết" :
                "F4-Add detail, F7-Save and print, F8-Delete detail");
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == (Keys.LButton | Keys.Space))//pageUp
            {
                if (btnPrevious.Enabled) btnPrevious.PerformClick();
            }
            else if (keyData == (Keys.RButton | Keys.Space))//PageDown(Next)
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
            else if (keyData == Keys.F4)
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        string error = ValidateDetailData(Invoice, detail1.GetData());
                        if (string.IsNullOrEmpty(error))
                        {
                            detail1.btnNhan.Focus();
                            detail1.btnNhan.PerformClick();
                        }
                        else
                        {
                            ShowMainMessage(error);
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
            _maKhoI.RefreshLoDateYnValue();
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
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Không gán thue_suat_i vì maVt.data == null");
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
                _heSo1.Value = he_so;
            }
            else
            {
                _heSo1.Value = 1;
            }
        }

        void TienNt0_V6LostFocus(object sender)
        {
            TinhGiaNt();
        }
        
        void GiaNt01_V6LostFocus(object sender)
        {
            chkT_THUE_NT.Checked = false;
            TinhTienNt0();
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
                    txtDiaChi2.ParentData = null;
                    txtMaSoThue.Text = "";
                    txtTenKh.Text = "";
                    txtDiaChi.Text = "";
                    return;
                }

                txtDiaChi2.ParentData = data.ToDataDictionary();
                txtDiaChi2.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
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
            //  GetGia();
            TinhTienNt0();
            Tinh_thue_ct();
        }
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
          
        }

        
        private void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
                _maVt.RefreshLoDateYnValue();
                var data = _maVt.Data;
                if (data == null)
                {
                    _tkVt.Text = "";
                    _ma_thue_i.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;
                    SetControlValue(txtPMA_KHC, null);
                }
                else
                {
                    _tkVt.Text = (data["tk_vt"] ?? "").ToString().Trim();
                    _hs_qd1.Value = ObjectAndString.ObjectToDecimal(data["HS_QD1"]);
                    _hs_qd2.Value = ObjectAndString.ObjectToDecimal(data["HS_QD2"]);
                    txtPMA_KHC.ChangeText(data["PMA_KHC"].ToString());

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

        /// <summary>
        /// Tính tiền nt, tính giá nt
        /// </summary>
        private void TinhTienNt()
        {
            try
            {
                //Tuanmh 19/12/2016 Chua Kiem tra Null

                _tienNt.Value = _tienNt0.Value + _cpNt.Value - _ckNt.Value - _ggNt.Value + _tien_vcNt.Value;
                _tien.Value = _tien0.Value + _cp.Value - _ck.Value - _gg.Value + _tien_vc.Value;

                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tienNt0.Value;
                    _tien.Value = _tienNt.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienNt " + _sttRec, ex);
            }
            TinhGiaNt();
        }

        private void TinhTienNt0()
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);

                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                _tienNt0.Value = V6BusinessHelper.Vround((_soLuong1.Value * _giaNt01.Value), M_ROUND_NT);
                _tien0.Value = V6BusinessHelper.Vround((_tienNt0.Value * txtTyGia.Value), M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tienNt0.Value;
                }
                _tienNt.Value = _tienNt0.Value;
                _tien.Value = _tien0.Value;

                //Tuanmh 19/12/2016
                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt0, _pt_cki);

                TinhGiaNt();
                TinhVanChuyen();
                TinhGiamGiaCt();
                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void TinhGiamGiaCt()
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

        private void TinhVanChuyen()
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

        private void TinhGiaNt()
        {
            try
            {
                if (_soLuong1.Value != 0)
                {
                    _gia01.Value = V6BusinessHelper.Vround((_giaNt01.Value * txtTyGia.Value), M_ROUND_GIA_NT);
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
                        _gia0.Value = _giaNt01.Value;
                        _gia_Nt0.Value = _giaNt01.Value;
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
        
        protected override void EnableVisibleControls()
        {
            try
            {
                var readOnly = Mode != V6Mode.Edit && Mode != V6Mode.Add;
                V6ControlFormHelper.SetFormControlsReadOnly(this, readOnly);

                if (readOnly)
                {
                    detail1.MODE = V6Mode.Lock;
                    detail2.MODE = V6Mode.Lock;

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

                    txtTyGia.Enabled = _maNt != _mMaNt0;
                    _tienNt0.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;

                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
                    if (M_POA_MULTI_VAT == "1")
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
                        txtT_TIENVCNT.ReadOnly = true;
                        txtT_TIENVC.ReadOnly = true;

                        _hs_qd3.EnableTag();
                        _tien_vc.EnableTag();
                        _tien_vcNt.EnableTag();
                    }
                    else
                    {
                        txtT_TIENVCNT.ReadOnly = false;
                        txtT_TIENVC.ReadOnly = false;

                        _hs_qd3.DisableTag();
                        _tien_vc.DisableTag();
                        _tien_vcNt.DisableTag();
                    }
                }

                //Cac truong hop khac
                if (!readOnly)
                {
                    XuLyHienThiChietKhau_PhieuNhap(chkLoaiChietKhau.Checked, chkSuaTienCk.Checked, _pt_cki, _ckNt, txtTongCkNt, chkSuaPtck);
                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;

                    chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
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

            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_AD, Invoice.GRDF_AD,
                        V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
        }
        #endregion datagridview


        protected override void EnableNavigationButtons()
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

        protected override void EnableFunctionButtons()
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
        
        private void TinhTongValues()
        {
            txtTongSoLuong1.Value = TinhTong(AD, "SO_LUONG1");
            txtTongSoLuong.Value = TinhTong(AD, "SO_LUONG");

            var tTienNt0 = TinhTong(AD, "TIEN_NT0");
            txtTongTienNt0.Value = V6BusinessHelper.Vround(tTienNt0, M_ROUND_NT);

            var tTien0 = TinhTong(AD, "TIEN0");
            txtTongTien0.Value = V6BusinessHelper.Vround(tTien0, M_ROUND);

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
                txtT_TIENVCNT.Value = V6BusinessHelper.Vround(t_vc_nt, M_ROUND_NT);
                txtT_TIENVC.Value = V6BusinessHelper.Vround(t_vc, M_ROUND);
            }
        }
        private void TinhChietKhau()
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
                    //tính chiết khấu cho mỗi chi tiết
                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt0 != 0)
                        {
                            var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"]);
                            var ck_nt = V6BusinessHelper.Vround( tien_nt0/t_tien_nt0*t_ck_nt, M_ROUND_NT);
                            var ck = V6BusinessHelper.Vround(ck_nt * tyGia, M_ROUND);

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

        private void TinhPhanBoGiamGia()
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
                {
                    return;
                }

                decimal t_gg_nt = 0, t_gg = 0;

                t_gg_nt = txtTongGiamNt.Value;
                t_gg = txtTongGiam.Value;

                //tính giam gia cho mỗi chi tiết

                var tTienNt0 = V6BusinessHelper.TinhTong(AD, "TIEN_NT0");
                var tyGia = txtTyGia.Value;
                var t_tien_nt0 = txtTongTienNt0.Value;
                txtTongTienNt0.Value = V6BusinessHelper.Vround(tTienNt0, M_ROUND_NT);

                var t_gg_nt_check = 0m;
                var t_gg_check = 0m;
                var index_gg = -1;

                for (var i = 0; i < AD.Rows.Count; i++)
                {
                    if (t_tien_nt0 != 0)
                    {
                        var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"]);
                        var gg_nt = V6BusinessHelper.Vround(tien_nt0 * t_gg_nt / t_tien_nt0, M_ROUND_NT);
                        var gg = V6BusinessHelper.Vround(gg_nt * tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            gg = gg_nt;


                        t_gg_nt_check = t_gg_nt_check + gg_nt;
                        t_gg_check += gg;

                        if (gg_nt != 0 && index_gg == -1)
                            index_gg = i;


                        //gán lại gg_nt
                        if (AD.Columns.Contains("GG_NT")) AD.Rows[i]["GG_NT"] = gg_nt;
                        if (AD.Columns.Contains("GG")) AD.Rows[i]["GG"] = gg;


                    }
                }
                // Xu ly chenh lech
                // Tìm dòng có số tiền
                if (index_gg != -1)
                {
                    decimal _gg_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["GG_NT"]) + (t_gg_nt - t_gg_nt_check);
                    AD.Rows[index_gg]["GG_NT"] = _gg_nt;

                    decimal _gg = ObjectAndString.ObjectToDecimal(AD.Rows[index_gg]["GG"]) + (t_gg - t_gg_check);
                    AD.Rows[index_gg]["GG"] = _gg;
                }
            }

            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void TinhThue()
        {
            //Tính tiền thuế theo thuế suất
            var thue_suat = 0m;
            var t_thue_nt = 0m;
            var ty_gia = txtTyGia.Value;
            var t_tien_nt0 = txtTongTienNt0.Value;
            var t_gg_nt = txtTongGiamNt.Value;
            var t_ck_nt = txtTongCkNt.Value;

            if (chkT_THUE_NT.Checked)//Tiền thuế gõ tự do
            {
                t_thue_nt = txtTongThueNt.Value;
            }
            else
            {
                thue_suat = txtThueSuat.Value;
                //tiền thuế = (tiền hàng - tiền giảm - chiết khấu) * thuế suất
                t_thue_nt = (t_tien_nt0 - t_gg_nt - t_ck_nt)*thue_suat/100;
                t_thue_nt = V6BusinessHelper.Vround(t_thue_nt, M_ROUND_NT);
                //sV("T_THUE_NT", t_thue_nt);
                txtTongThueNt.Value = t_thue_nt;
            }

            //tính thuế riêng cho từng chi tiết
            //tính phần trăm giá trị của từng chi tiết trên tổng tiền hàng rồi nhân với tổng thuế sẽ ra thuế 
            var t_thue_nt_check = 0m;
            var index = -1;
            for (var i = 0; i < AD.Rows.Count; i++)
            {
                if (t_tien_nt0 != 0)
                {
                    var tien_nt0 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN_NT0"]);    
                    var thue_nt = V6BusinessHelper.Vround((tien_nt0 / t_tien_nt0 )* t_thue_nt, M_ROUND);
                    t_thue_nt_check = t_thue_nt_check + thue_nt;

                    var thue = V6BusinessHelper.Vround(thue_nt * ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thue = thue_nt;
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
            }
        }


        // Tuanmh 08/04/2018 Tinh lai khi thue suat chi tiet tung mat hang
        private void TinhLaiTienThueCT()
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

                //string mathuei = row["MA_THUE_I"].ToString().Trim();
                //if (string.IsNullOrEmpty(mathuei))
                //{
                //    var mavt_data = temp_maVt.Data;
                //    if (mavt_data != null)
                //    {
                //        var mathue = mavt_data["MA_THUE"].ToString().Trim();
                //        if (!string.IsNullOrEmpty(mathue))
                //        {
                //            row["MA_THUE_I"] = mathue;
                //            row["THUE_SUAT_I"] = ObjectAndString.ObjectToDecimal(mavt_data["THUE_SUAT"]);

                //            var alThue = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE = '" + mathue + "'");
                //            if (alThue.TotalRows > 0)
                //            {
                //                var tk_thue_i_Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                //                row["TK_THUE_I"] = tk_thue_i_Text;
                //                txtTkThueCo.Text = tk_thue_i_Text;
                //            }
                //        }
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

        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
                if (NotAddEdit) return;
            
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();

                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt0, T_CK_NT, PT_CK
                TinhPhanBoGiamGia();//Tuanmh bo sung 05/12/2017
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
                
                var t_tien_nt0 = txtTongTienNt0.Value;
                var t_gg_nt = txtTongGiamNt.Value;
                var t_ck_nt = txtTongCkNt.Value;
                var t_thue_nt = txtTongThueNt.Value;

                var t_tt_nt = t_tien_nt0 - t_gg_nt - t_ck_nt + t_thue_nt;
                txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

                var t_tt = txtTongTien0.Value - txtTongGiam.Value - txtTongCk.Value + txtTongThue.Value;
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                //var tygia = txtTyGia.Value;
                //txtTongTien0.Value = V6BusinessHelper.Vround(t_tien_nt0*tygia, M_ROUND);
                //txtTongGiam.Value = V6BusinessHelper.Vround(t_gg_nt*tygia, M_ROUND);
                //txtTongCk.Value = V6BusinessHelper.Vround(t_ck_nt*tygia, M_ROUND);
                //txtTongThue.Value = V6BusinessHelper.Vround(t_thue_nt*tygia, M_ROUND);
                // txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt_nt*tygia, M_ROUND);
                //Co_Thay_Doi = true;
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
        }

        private void TinhTongThue_ct()
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
            LoadAlMaGia();
            LoadAlnt();
            LoadAlpost();
            GetM_ma_nt0();
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
                cboMaNt.DisplayMember = "Ten_nt";
                cboMaNt.DataSource = Invoice.Alnt;
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = "Ten_nt";
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
            TxtT_cp_ao.Visible = false;
            TxtT_cp.Visible = false;

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
            //
            txtManx.Text = Invoice.Alct["TK_CO"].ToString().Trim();
            cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();
            TxtTk_i_ao.Text = txtManx.Text;

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
                    TxtT_cp_ao.Visible = true;
                    TxtT_cp.Visible = true;

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA01");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongTienNt0.DecimalPlaces = V6Options.M_IP_TIEN_NT;

                    _t_tien22.Visible = true;
                    _t_thue22.Visible = true;

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
                    TxtT_cp_ao.Visible = false;
                    TxtT_cp.Visible = false;

                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongTienNt0.DecimalPlaces = V6Options.M_IP_TIEN;

                    _t_tien22.Visible = false;
                    _t_thue22.Visible = false;
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberGridView " + _sttRec, ex);
            }
        }
        
        private void XuLyThayDoiMaThue()
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE30", "*",
                                  "MA_THUE = '" + txtMa_thue.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    txtTkThueNo.Text = alThue.Data.Rows[0]["TK_THUE_NO"].ToString().Trim();
                    txtThueSuat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    txtTkThueCo.Text = txtManx.Text;
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
                var alThue = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = alThue.Data.Rows[0][THUE_SUAT] = " + alThue.Data.Rows[0]["THUE_SUAT"]);
                }
                else
                {
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - ko Gán thue_suat_i.Value vì alThueData không có.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue_i " + _sttRec, ex);
            }
            //TinhTongThanhToan("XuLyThayDoiMaThue_i");
        }

        private void Tinh_thue_ct()
        {
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_POA_MULTI_VAT = " + M_POA_MULTI_VAT);
            if (M_POA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat_i.Value, _tienNt0.Value - _ckNt.Value - _ggNt.Value, _tien0.Value - _ck.Value - _gg.Value, _thue_nt, _thue);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tính thuế ct M_POA_MULTY_VAT = 1.");
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
        }

        
        protected override void ShowParentMessage(string message)
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
                TxtMa_kh_i_ao.Text = row["Ma_kh_i"].ToString().Trim();
                TxtT_cp_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp"]);
                TxtTk_i_ao.Text = row["Tk_i"].ToString().Trim();
                TxtT_cp_nt_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp_nt"]);

                XuLyThayDoiMaDVCS();
                SetGridViewData();
                XuLyThayDoiMaNt();
                Mode = V6Mode.View;
                //btnSua.Focus();
                FormatNumberControl();
                FormatNumberGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
        }

        #region ==== Add Thread ====
        private bool flagAddFinish, flagAddSuccess;
        private SortedDictionary<string, object> addDataAM;
        private List<SortedDictionary<string, object>> addDataAD, addDataAD2;
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
                
                if (Invoice.InsertInvoice(addDataAM, addDataAD, addDataAD2))
                {
                    flagAddSuccess = true;
                }
                else
                {
                    flagAddSuccess = false;
                    addErrorMessage = "Thêm không thành công";
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
        private List<SortedDictionary<string, object>> editDataAD, editDataAD2;
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
                foreach (SortedDictionary<string, object> adRow in editDataAD)
                {
                    adRow["DATE0"] = am_DATE0;
                    adRow["TIME0"] = am_TIME0;
                    adRow["USER_ID0"] = am_U_ID0;
                }
                editDataAD2 = dataGridView2.GetData(_sttRec);
                foreach (SortedDictionary<string, object> adRow in editDataAD2)
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
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, editDataAD2, keys))
                {
                    flagEditSuccess = true;
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                }
                else
                {
                    flagEditSuccess = false;
                    editErrorMessage = "Sửa không thành công";
                    Invoice.PostErrorLog(_sttRec, "S");
                }
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
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = "Xóa không thành công";
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
                    detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit )
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
                if(IsViewingAnInvoice)
                if (V6Login.UserRight.AllowEdit("", Invoice.CodeMact))
                {
                    if (Mode == V6Mode.View)
                    {
                        // Tuanmh 16/02/2016 Check level
                        var row = AM.Rows[CurrentIndex];
                        if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                        {
                            Mode = V6Mode.Edit;
                            detail1.MODE = V6Mode.View;
                            detail2.MODE = V6Mode.View;
                            txtMa_sonb.Focus();
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Xoa()
        {
            try
            {
                if(IsViewingAnInvoice)
                if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact))
                {
                    var row = AM.Rows[CurrentIndex];
                    // Tuanmh 16/02/2016 Check level
                    if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                    {
                        DoDeleteThread();
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
                if(IsViewingAnInvoice)
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
                if(IsViewingAnInvoice)
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
                    c.ShowToForm(this, V6Text.PrintPOH, true);
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

        private Tim_DeNghiNhapKhoINY_Form _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) _timForm = new Tim_DeNghiNhapKhoINY_Form(this);
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
                        _timForm = new Tim_DeNghiNhapKhoINY_Form(this);
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
                detail2.SetData(null);
                
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
        }

        private void SetFormDefaultValues()
        {
            chkLoaiChietKhau.Checked = true;//loai ck chung
            cboKieuPost.SelectedIndex = 1;
            TxtLoai_pb.Text = "0";
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
                if (AD2 == null || AD2.Rows.Count == 0)
                {
                    _so_ct022.Text = txtSoCt0.Text;
                    _ngay_ct022.Value = txtNgayCt0.Value;
                    _so_seri022.Text = txtSoSeri0.Text;
                    _ma_kh22.Text = txtMaKh.Text;
                    _ten_kh22.Text = txtTenKh.Text;
                    _dia_chi22.Text = txtDiaChi.Text;
                    _ma_so_thue22.Text = txtMaSoThue.Text;
                    _t_tien22.Value = txtTongTien0.Value;
                    _t_tien_nt22.Value = txtTongTienNt0.Value;
                    _thue_suat22.Value = txtThueSuat.Value;
                    _tk_thue_no22.Text = txtTkThueNo.Text.Trim();
                    _tk_du22.Text = txtManx.Text.Trim();
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
                    TinhTienThue22();
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
                return true;
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
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\nMã vật tư rỗng.";
                if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\nMã kho rỗng.";
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
            }
            TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
            return true;
        }

        private bool XuLyThemDetail2(SortedDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;//Coi nhu thanh cong de quay ve mode view
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
                    error += "\nSố hóa đơn rỗng.";
                if (!data.ContainsKey("NGAY_CT0") || data["NGAY_CT0"] == DBNull.Value)
                    error += "\nNgày hóa đơn rỗng.";
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
                    return false;//Loi phat hien, return false
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhTongThanhToan("xu ly them detail2");
            return true;
        }

        
        private bool XuLySuaDetail(SortedDictionary<string, object> data)
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
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                            error += "\nMã vật tư rỗng.";
                        if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "")
                            error += "\nMã kho rỗng.";
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
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            TinhTongThanhToan("xy ly sua detail");
            return true;
        }

        
        private bool XuLySuaDetail2(SortedDictionary<string, object> data)
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
                            error += "\nSố hóa đơn rỗng.";
                        if (!data.ContainsKey("NGAY_CT0") || data["NGAY_CT0"] == DBNull.Value)
                            error += "\nNgày hóa đơn rỗng.";
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

        private void hoaDonDetail1_AddHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_AddHandle(SortedDictionary<string,object> data)
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
        private void hoaDonDetail1_EditHandle(SortedDictionary<string, object> data)
        {
            dataGridView1.UnLock();
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_EditHandle(SortedDictionary<string,object> data)
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

                chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                if (!chkLoaiChietKhau.Checked)
                {
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkSuaTienCk.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                }
                else
                {

                }

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
        
        private void txtThueSuat_V6LostFocus(object sender)
        {
            TinhTongThanhToan("V6LostFocus " + ((Control)sender).AccessibleName);
        }


        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSuaTien);
                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
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
                txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
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
                        _thue_nt.Enabled = true;
                        txtTongThueNt.ReadOnly = true;
                    }
                    else
                    {
                        _thue_nt.Enabled = false;
                    }
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
                _tienNt0.Tag = null;
            }
            else
            {
                _tienNt0.Tag = "disable";
            }
        }

        private void chkSuaPtck_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                txtPtCk.ReadOnly = !chkSuaPtck.Checked;
        }
        

        private void hoaDonDetail2_Load(object sender, EventArgs e)
        {

        }

        private bool pb_changed;
        private void Txt_ao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender == TxtMa_kh_i_ao)
                {
                    Txtma_kh_i.Text = TxtMa_kh_i_ao.Text;
                }
                else if (sender == TxtTk_i_ao)
                {
                    TxtTk_i.Text = TxtTk_i_ao.Text;
                }
                else if (sender == TxtT_cp_nt_ao)
                {
                    TxtT_cp_nt.Value = TxtT_cp_nt_ao.Value;
                }
                else if (sender == TxtT_cp_ao)
                {
                    TxtT_cp.Value = TxtT_cp_ao.Value;
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
            TxtLoai_pb.Text = loai_pb;
        }

        private void TinhPhanBoChiPhi0(string loai_pb)
        {
            try
            {
                if (loai_pb.Length > 1) loai_pb = loai_pb.Left(1);
                if (loai_pb != "1" && loai_pb != "2") return;
                var t_he_so = loai_pb == "1"? txtTongTienNt0.Value : txtTongSoLuong1.Value;
                var t_cp_nt_check = 0m;
                var t_cp_check = 0m;
                var index = -1;
                
                if (t_he_so != 0)
                {

                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        var heso_01 = ObjectAndString.ObjectToDecimal(AD.Rows[i][loai_pb == "1" ? "TIEN_NT0" : "SO_LUONG1"]);

                        var cp_nt = V6BusinessHelper.Vround((heso_01 / t_he_so) * TxtT_cp_nt.Value, M_ROUND_NT);

                        t_cp_nt_check = t_cp_nt_check + cp_nt;

                        var cp = V6BusinessHelper.Vround(cp_nt * txtTyGia.Value, M_ROUND);

                        if (_maNt == _mMaNt0)
                            cp = cp_nt;

                        t_cp_check = t_cp_check + cp;

                        if (cp_nt != 0 && index == -1)
                            index = i;

                        AD.Rows[i]["Cp_nt"] = cp_nt;
                        AD.Rows[i]["Cp"] = cp;
                    }
                }

                // Xu ly chenh lech
                // Tìm dòng có số tiền
                if (index != -1)
                {
                    decimal _cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"]) + (TxtT_cp_nt.Value - t_cp_nt_check);
                    decimal _cp = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"]) + (TxtT_cp.Value - t_cp_check);

                    AD.Rows[index]["Cp_nt"] = _cp_nt;
                    AD.Rows[index]["Cp"] = _cp;

                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void TinhPhanBoChiPhi(string loai_pb)
        {
            try
            {
                if (loai_pb.Length > 1) loai_pb = loai_pb.Left(1);
                if (loai_pb != "1" && loai_pb != "2") return;
                var t_he_so = loai_pb == "1" ? txtTongTienNt0.Value : txtTongSoLuong1.Value;
                var t_cp_nt_check = 0m;
                var t_cp_check = 0m;
                var index = -1;

                if (t_he_so != 0)
                {

                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        var heso_01 = ObjectAndString.ObjectToDecimal(AD.Rows[i][loai_pb == "1" ? "TIEN_NT0" : "SO_LUONG1"]);

                        var cp_nt = V6BusinessHelper.Vround((heso_01 / t_he_so) * TxtT_cp_nt.Value, M_ROUND_NT);

                        t_cp_nt_check = t_cp_nt_check + cp_nt;

                        var cp = V6BusinessHelper.Vround(cp_nt * txtTyGia.Value, M_ROUND);

                        if (_maNt == _mMaNt0)
                            cp = cp_nt;

                        t_cp_check = t_cp_check + cp;

                        if (cp_nt != 0 && index == -1)
                            index = i;

                        AD.Rows[i]["Cp_nt"] = cp_nt;
                        AD.Rows[i]["Cp"] = cp;
                        //tuanmh 14/10/2016
                        if (TxtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                        {
                            AD.Rows[i]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt0"])
                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp_nt"])
                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_vc_nt"])
                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck_nt"])
                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg_nt"]);

                            AD.Rows[i]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien0"])
                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Cp"])
                                + ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_vc"])
                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Ck"])
                                - ObjectAndString.ObjectToDecimal(AD.Rows[i]["Gg"]);
                        }
                    }
                }

                // Xu ly chenh lech
                // Tìm dòng có số tiền
                if (index != -1)
                {
                    decimal _cp_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"]) + (TxtT_cp_nt.Value - t_cp_nt_check);
                    decimal _cp = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"]) + (TxtT_cp.Value - t_cp_check);

                    AD.Rows[index]["Cp_nt"] = _cp_nt;
                    AD.Rows[index]["Cp"] = _cp;

                    if (TxtMa_kh_i_ao.Text.Trim() == txtMaKh.Text.Trim())
                    {
                        AD.Rows[index]["Tien_nt"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien_nt0"])
                            + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp_nt"])
                            + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien_vc_nt"])
                            - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Ck_nt"])
                            - ObjectAndString.ObjectToDecimal(AD.Rows[index]["Gg_nt"]);
                        AD.Rows[index]["Tien"] = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien0"])
                            + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Cp"])
                            + ObjectAndString.ObjectToDecimal(AD.Rows[index]["Tien_vc"])
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
                this.ShowErrorException(GetType() + ".TinhPhanBoChiPhi " + _sttRec, ex);
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
                this.ShowErrorException(GetType() + ".XoaPhanBoChiPhi " + _sttRec, ex);
            }
        }

        List<string> gridView3Fields = new List<string>() { "MA_VT", "TEN_VT", "DVT1", "SO_LUONG1", "CP_NT", "CP", "TIEN_NT0", "TIEN0" };
        private void dataGridView3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            try
            {
                if (gridView3Fields.Contains(e.Column.DataPropertyName.ToUpper()))
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
            var loai_pb = TxtLoai_pb.Text.Trim();
            TinhPhanBoChiPhi(loai_pb);
        }

        private void TinhT_CpNt()
        {
            try
            {
                
                TxtT_cp_ao.Value = V6BusinessHelper.Vround((TxtT_cp_nt_ao.Value * txtTyGia.Value), M_ROUND);
                if (_maNt == _mMaNt0)
                    TxtT_cp_ao.Value = TxtT_cp_nt_ao.Value;
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            TxtMa_kh_i_ao.Text = txtMaKh.Text;
            XuLyChonMaKhachHang();
        }

        private void txtManx_V6LostFocus(object sender)
        {
            if (TxtTk_i_ao.Text.Trim() == "") TxtTk_i_ao.Text = txtManx.Text;
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
                    this.ShowWarningMessage("Chưa nhập mã đơn vị!");
                    txtMadvcs.Focus();
                    return false;
                }
                if (txtMaKh.Text.Trim() == "")
                {
                    this.ShowWarningMessage("Chưa nhập mã khách hàng!");
                    txtMaKh.Focus();
                    return false;
                }
                if (txtManx.Text.Trim() == "")
                {
                    this.ShowWarningMessage("Chưa nhập tài khoản!");
                    txtManx.Focus();
                    return false;
                }
                if (txtManx.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage("Tài khoản không phải chi tiết!");
                    txtManx.Focus();
                    return false;
                }
                if (cboKieuPost.SelectedIndex == -1)
                {
                    this.ShowWarningMessage("Chưa chọn kiểu post!");
                    cboKieuPost.Focus();
                    return false;
                }

                ValidateMasterData(Invoice);


                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage("Chưa nhập chi tiết!");
                    return false;
                }
                //Tuanmh 16/02/2016 Check Voucher Is exist 
                {
                    DataTable DataCheckVC = Invoice.GetCheck_VC_Save(cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec);
                    if (DataCheckVC!=null && DataCheckVC.Rows.Count > 0)
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
                return false;
            }

        }

        private bool ValidateData_Detail(SortedDictionary<string, object> data)
        {
            try
            {
                //if (_tkDt.Int_Data("Loai_tk") == 0)
                //{
                //    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                //    return false;
                //}

                string errors = ValidateDetailData(Invoice, data);
                if (!string.IsNullOrEmpty(errors))
                {
                    this.ShowWarningMessage(errors);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail3 " + _sttRec, ex);
            }
            return true;
        }

        private bool ValidateData_Detail2(SortedDictionary<string, object> data)
        {
            try
            {
                //if (_tkDt.Int_Data("Loai_tk") == 0)
                //{
                //    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                //    return false;
                //}
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

        private void TxtLoai_pb_TextChanged(object sender, EventArgs e)
        {
            //Chống lặp
            if (pb_changed)
            {
                pb_changed = false;
                return;
            }
            //Lấy giá trị
            var loai_bp = TxtLoai_pb.Text.Trim();
            if ("012".Contains(loai_bp))
            {
                pb_changed = true;
            }

            //Đổi cbo
            if (TxtLoai_pb.Text == "1")
                cboLoai_pb.SelectedIndex = 1;
            else if (TxtLoai_pb.Text == "2")
                cboLoai_pb.SelectedIndex = 2;
            else if (TxtLoai_pb.Text == "0")
                cboLoai_pb.SelectedIndex = 0;
            else cboLoai_pb.SelectedIndex = -1;

            //cboLoai_pb.Tag = TxtLoai_pb.Text;
            //if (TxtLoai_pb.Text.Trim() == "1")
            //    cboLoai_pb.SelectedIndex = 0;
            //else if (TxtLoai_pb.Text.Trim() == "2")
            //    cboLoai_pb.SelectedIndex = 1;
            //else
            //    cboLoai_pb.SelectedIndex = -1;
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                detail1.AutoFocus();
            }
            else if (tabControl1.SelectedTab == tabVAT)
            {
                detail2.AutoFocus();
            }
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
                        if (!exist) _message += " Danh mục vật tư không tồn tại mã: " + cMaVt;
                        if (!exist2) _message += " Danh mục kho không tồn tại mã: " + cMaKhoI;
                    }
                }
                ShowParentMessage(count > 0
                ? string.Format("Đã thêm {0} chi tiết từ excel.", count) + _message
                : "Không thêm được chi tiết nào từ excel." + _message);
            }
            else
            {
                ShowParentMessage("Không có đủ thông tin!");
            }


        }

        #endregion chức năng

        private void TroGiupMenu_Click(object sender, EventArgs e)
        {
            ChucNang_TroGiup();
        }

        private void chonTuExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ChonTuExcel();
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView2, dataGridView3ChiPhi);
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

        private void txtDiaChi2_Enter(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (txtDiaChi2.ReadOnly) return;
                var data = txtMaKh.Data;
                if (data == null)
                {
                    this.ShowWarningMessage("Chưa chọn mã khách hàng!", 300);
                    return;
                }
                txtDiaChi2.ParentData = data.ToDataDictionary();
                txtDiaChi2.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
            }
        }

        private void ChonDonHangMuaMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ChonDonHang();
        }

        private void ChucNang_ChonDonHang()
        {
            try
            {
                if (NotAddEdit) return;

               
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if ( ma_dvcs != "")
                {
                    CDHB_DeNghiNhapKhoINYForm chon = new CDHB_DeNghiNhapKhoINYForm(dateNgayCT.Date, txtMadvcs.Text, "");
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID can not empty!";
                    this.ShowWarningMessage(message);
                    if (ma_dvcs == "") txtMadvcs.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ChonDonHang " + _sttRec, ex);
            }
        }

        void chon_AcceptSelectEvent(List<SortedDictionary<string, object>> selectedDataList)
        {
            try
            {
                detail1.MODE = V6Mode.View;
                AD.Rows.Clear();
                int addCount = 0, failCount = 0;
                foreach (SortedDictionary<string, object> data in selectedDataList)
                {
                    var newData = new SortedDictionary<string, object>(data);
                    All_Objects["data"] = newData;
                    InvokeFormEvent("DONDATHANGMUA_CDHB");
                    if (XuLyThemDetail(newData)) addCount++;
                    else failCount++;
                }
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed {1}.", addCount, failCount));
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void btnTinhPB_Click(object sender, EventArgs e)
        {
            TinhPhanBoChiPhi(TxtLoai_pb.Text.Trim());
        }

        private void btnXoaPB_Click(object sender, EventArgs e)
        {
            XoaPhanBoChiPhi();
        }
    }
}