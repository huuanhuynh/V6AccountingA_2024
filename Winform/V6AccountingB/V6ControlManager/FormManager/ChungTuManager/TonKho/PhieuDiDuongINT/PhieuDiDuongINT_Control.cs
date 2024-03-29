﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDiDuongINT.ChonDonHangBan;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDiDuongINT.ChonDonHangMua;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDiDuongINT.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDiDuongINT
{
    public partial class PhieuDiDuongINT_Control : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice97INT Invoice = new V6Invoice97INT();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuDiDuongINT_Control()
        {
            InitializeComponent();
            MyInit();
        }
        public PhieuDiDuongINT_Control(string itemId)
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
        public PhieuDiDuongINT_Control(string maCt, string itemId, string sttRec)
            : base(new V6Invoice97INT(), itemId)
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
            txtLoaiNX_PH.SetInitFilter("LOAI = 'N'");
            txtDiaChi2.DisableUpperLower();

            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                //txtMa_sonb.SetInitFilter("MA_DVCS='" + V6Login.Madvcs + "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1" +
                txtMa_sonb.SetInitFilter("MA_DVCS={MA_DVCS} AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1" +
                    (V6Login.IsAdmin ? "" : " AND  dbo.VFA_Inlist_MEMO(MA_SONB,'" + V6Login.UserRight.RightSonb + "')=1"));
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1" +
                    (V6Login.IsAdmin ? "" : " AND  dbo.VFA_Inlist_MEMO(MA_SONB,'" + V6Login.UserRight.RightSonb + "')=1"));
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

            SetGridViewFomular();
            SetGridViewEvent();
            SetGridViewFlag(dataGridView1, dataGridView2);

            cboKieuPost.SelectedIndex = 0;
            if (!V6Setting.IsVietnamese)
            {
                cboLoai_pb.Items.AddRange(new object[] {
                "1 - Allocated by value",
                "2 - Allocated by quantity"});
            }
            cboLoai_pb.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            LoadDetail2Controls();
            LoadAdvanceControls(Invoice.Mact);
            CreateCustomInfoTextBox(group4, txtTongSoLuong1, cboChuyenData);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            LoadTagAndText(Invoice, detail1.Controls);
            HideControlByGRD_HIDE();
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Invoice.Mact, Name_Methods, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        public V6ColorTextBox _dvt, txtPTEN_KHC, txtPONG_BAC, txtPDIEN_THOAIC;
        public V6ColorTextBox _detail1Focus;
        public V6QRTextBox _qr_code0;
        public V6VvarTextBox _maVt, _Ma_lnx_i, _dvt1, _maKho, _maKhoI, _tkVt, _maLo, _ma_thue_i, txtPMA_KHC;
        public V6NumberTextBox _gia_nt, _gia_nt01, _tien0, _tien_nt0, _gia0, _gia01, _gia, _gia_Nt0,
            _ck, _ckNt, _pt_cki, _thue, _thue_nt, _thue_suat_i;
        public V6NumberTextBox _soLuong1, _soLuong, _he_so1T, _he_so1M, _ton13, _ton13Qd, _tienNt, _tien, _mau_bc22, _sl_td1;
        public V6NumberTextBox _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4, _ggNt, _gg, _tien_vcNt, _tien_vc, _cpNt, _cp;
        public V6DateTimeColor _hanSd;
        public V6ColorTextBox _so_ct022,_so_seri022,_ten_kh22, _dia_chi22,_ma_so_thue22;
        public V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22;
        public V6DateTimeColor _ngay_ct022;
        public V6NumberTextBox _t_tien22, _t_tien_nt22, _thue_suat22, _t_thue22, _t_thue_nt22, _gia_Nt022;

        public void Detail1FocusReset()
        {
            if (_detail1Focus != null) _detail1Focus.Focus();
            else _maVt.Focus();
        }

        private void LoadDetailControls()
        {
            //Lấy các control động
            detailControlList1 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Mact, Invoice.Alct1, out _orderList, out _alct1Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<string, AlctControls> item in detailControlList1)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                if (control is V6ColorTextBox && item.Value.IsCarry)
                {
                    detail1.CarryFields.Add(NAME);
                }
                // Gán tag hide và readonly theo GRD_xxxx
                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":"))
                {
                    control.InvisibleTag();
                }
                if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                {
                    control.ReadOnlyTag();
                }
                // bỏ bớt dòng này do đã dùng ApplyDynamicFormControlEvents
                //V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects); a

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
                        if (_detail1Focus == null)
                        {
                            _detail1Focus = _maVt;
                        }
                        _maVt.Upper();
                        _maVt.BrotherFields = "ten_vt,ten_vt2,dvt,ma_qg";
                        //_maVt.BrotherFields = "dvt";
                        _mavt_default_initfilter = _maVt.InitFilter;
                        var setting = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MAKH2MAVT"));
                        if (setting.Contains(Invoice.Mact))
                        _maVt.Enter += (sender, args) =>
                        {
                            string newFilter = Invoice.GetMaVtFilterByMaKH(txtMaKh.Text, txtMaDVCS.Text);
                            if(string.IsNullOrEmpty(_mavt_default_initfilter)) _maVt.SetInitFilter(newFilter);
                            else if (!string.IsNullOrEmpty(newFilter))
                            {
                                _maVt.SetInitFilter(string.Format("({0}) and ({1})", _mavt_default_initfilter, newFilter));
                            }
                        };
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
                    case "MA_LNX_I":
                        _Ma_lnx_i = control as V6VvarTextBox;
                        if (_Ma_lnx_i != null)
                        {
                            _Ma_lnx_i.FilterStart = true;
                            _Ma_lnx_i.SetInitFilter("LOAI = 'N'");
                            _Ma_lnx_i.Upper();
                        }
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
                    case "TON13QD":
                        _ton13Qd = control as V6NumberTextBox;
                        if (_ton13Qd.Tag == null || _ton13Qd.Tag.ToString() != "hide")
                        {
                            _ton13Qd.Tag = "disable";
                        }
                        break;
                    case "SO_LUONG1":
                        _soLuong1 = (V6NumberTextBox)control;
                        _soLuong1.V6LostFocus += delegate
                        {
                            CheckSoLuong1(_soLuong1);
                        };
                        _soLuong1.V6LostFocusNoChange += delegate
                        {

                        };
                        _soLuong1.Leave += delegate
                        {
                            if (!detail1.IsAddOrEdit) return;
                            SetControlValue(_sl_td1, _soLuong1.Value, Invoice.GetTemplateSettingAD("SL_TD1"));
                        };
                        _soLuong1.TextChanged += delegate
                        {
                            if (!detail1.IsAddOrEdit) return;
                            if (!chkSuaTien.Checked)
                            {
                                if (_gia_nt01.Value * _soLuong1.Value == 0)
                                {
                                    _tien_nt0.Enabled = true;
                                    _tien_nt0.ReadOnly = false;
                                }
                                else
                                {
                                    _tien_nt0.Enabled = false;
                                    _tien_nt0.ReadOnly = true;
                                }
                            }
                        };
                        break;
                    //_soLuong1.Tag = "hide";
                    case "SO_LUONG":
                        _soLuong = (V6NumberTextBox)control;
                        _soLuong.Tag = "hide";
                        break;
                    case "SL_TD1":
                        _sl_td1 = control as V6NumberTextBox;
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
                                //if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                                //if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
                                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                                //if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1T);
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
                                //if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                                //if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                                //if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _he_so1M);
                            }
                        };
                        break;
                        break;
                    case "GIA_NT":
                        _gia_nt = (V6NumberTextBox)control;
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
                        _gia_nt01 = (V6NumberTextBox)control;
                        _gia_nt01.V6LostFocus += delegate
                        {
                            chkT_THUE_NT.Checked = false;
                            TinhTienNt0(_gia_nt01);
                            Tinh_thue_ct();
                        };
                        _gia_nt01.TextChanged += delegate
                        {
                            if (!detail1.IsAddOrEdit) return;

                            if (!chkSuaTien.Checked)
                            {
                                if (_gia_nt01.Value * _soLuong1.Value == 0)
                                {
                                    _tien_nt0.Enabled = true;
                                    _tien_nt0.ReadOnly = false;
                                }
                                else
                                {
                                    _tien_nt0.Enabled = false;
                                    _tien_nt0.ReadOnly = true;
                                }
                            }
                        };
                        break;
                    case "TIEN":
                        _tien = (V6NumberTextBox)control;
                        break;
                    case "TIEN_NT":
                        _tienNt = (V6NumberTextBox) control;
                        break;
                    case "TIEN_NT0":
                        _tien_nt0 = (V6NumberTextBox)control;

                        _tien_nt0.Enabled = chkSuaTien.Checked;
                        if (chkSuaTien.Checked)
                        {
                            _tien_nt0.Tag = null;
                        }
                        else
                        {
                            _tien_nt0.Tag = "disable";
                        }
                    
                        _tien_nt0.V6LostFocus += delegate
                        {
                            TinhGiaNt();
                            TinhTienVon_GiaVon();
                        };
                        _tien_nt0.V6LostFocus += delegate
                        {
                            if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * _sl_td1.Value, M_ROUND);
                            else _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);
                            TinhTienNt();// Trong TinhTienNt da co TinhGiaNt
                            if (_gia_nt01.Value == 0 && _soLuong1.Value != 0) TinhGiaNt01();
                            TinhTienVon_GiaVon();
                        };
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
                                TinhChietKhauChiTiet(true, _ck, _ckNt, txtTyGia, _tien_nt0, _pt_cki);
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
                                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tien_nt0, _pt_cki);
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
                                _maLo.CheckNotEmpty = _maVt.LO_YN && _maKhoI.LO_YN;
                                _maLo.SetInitFilter("Ma_vt='" + _maVt.Text.Trim() + "'");
                            }
                        };
                        _maLo.Leave += _maLo_V6LostFocus;
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        _hanSd.Tag = "disable";
                        break;
                    default:
                        if (!(_detail1Focus is V6QRTextBox) && control is V6QRTextBox && control.Visible)
                        {
                            _detail1Focus = (V6QRTextBox)control;
                        }

                        if (NAME == "QR_CODE0")
                        {
                            if (control is V6QRTextBox)
                            {
                                _qr_code0 = (V6QRTextBox)control;
                                _qr_code0.V6LostFocus += (sender) =>
                                {
                                    if (_qr_code0.Text.Trim() == "")
                                    {
                                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(detail1,
                                            ObjectAndString.SplitString(_qr_code0.NeighborFields), false);
                                        if (_maVt != null) _maVt.Focus();
                                    }
                                    else
                                    {
                                        //readonly
                                        V6ControlFormHelper.SetListControlReadOnlyByAccessibleNames(detail1,
                                            ObjectAndString.SplitString(_qr_code0.NeighborFields), true);
                                        _soLuong1.Value = 1;
                                        _soLuong1.CallDoV6LostFocus();
                                        if (!string.IsNullOrEmpty(Invoice.ExtraInfo_QrGot))
                                        {
                                            var c = detail1.GetControlByAccessibleName(Invoice.ExtraInfo_QrGot);
                                            if (c != null) c.Focus();
                                        }
                                    }
                                };
                            }
                        }
                        break;
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "2");
            }

            foreach (AlctControls item in detailControlList1.Values)
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
            TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
            TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
            TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
            _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
            TinhTienNt0(actionControl);
            Tinh_thue_ct();
        }

        private void LoadDetail2Controls()
        {
            detail2.lblName.AccessibleName = "";
            //Lấy các control động
            detailControlList2 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Mact, Invoice.Alct2, out _orderList2, out _alct2Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<string, AlctControls> item in detailControlList2)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();

                if (control is V6ColorTextBox && item.Value.IsCarry)
                {
                    detail2.CarryFields.Add(NAME);
                }
                // Gán tag hide và readonly theo GRD_xxxx
                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":"))
                {
                    control.InvisibleTag();
                }
                if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                {
                    control.ReadOnlyTag();
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "_DETAIL2");
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
                        _mau_bc22 = control as V6NumberTextBox;
                        if (_mau_bc22 != null)
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
                            //if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
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
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "2_DETAIL2");
            }
            
            foreach (AlctControls control in detailControlList2.Values)
            {
                detail2.AddControl(control);                
            }

            detail2.SetStruct(Invoice.AD2Struct);
            detail2.MODE = detail2.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView2, _alct2Dic, _maNt, _mMaNt0);
        }

        public void TinhTienThue22()
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

        void _maLo_V6LostFocus(object sender, EventArgs eventArgs)
        {
            if (detail1.IsAddOrEdit) CheckMaLo();
        }
        
        private void CheckMaLo()
        {
           
            XuLyLayThongTinKhiChonMaLo();
           
        }
         private void XuLyLayThongTinKhiChonMaLo()
        {
            try
            {
                if (_maVt.LO_YN)
                {
                    _maLo.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");
                    _maLo.ExistRowInTable(true);
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
                if (tabControl1.SelectedTab == tabChiTiet && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                {
                    detail1.btnNhan.Focus();
                    detail1.btnNhan.PerformClick();
                }
                else if (tabControl1.SelectedTab == tabThue && (detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit))
                {
                    detail2.btnNhan.Focus();
                    detail2.btnNhan.PerformClick();
                }
                else
                {
                    btnLuu.PerformClick();
                }
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.F4))
            {
                XuLyKhacQR("A" + Invoice.Mact + "_XULYKHAC4", true, _qr_code0);
            }
            else if (keyData == (Keys.Shift | Keys.F4))
            {
                XuLyKhacQR("A" + Invoice.Mact + "_XULYKHAC4", true, _qr_code0);
            }
            else if (keyData == (Keys.Control | Keys.F4))
            {
                XuLyKhacQR("A" + Invoice.Mact + "_XULYKHAC4", false, _qr_code0);
            }
            else if (keyData == (Keys.Control | Keys.T))
            {
                _ctrl_T = true;
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

                detail1.btnNhan.Focus();
                if (detail1.MODE == V6Mode.Add)
                {
                    var detailData = detail1.GetData();
                    if (ValidateData_Detail(detailData))
                    {
                        if (XuLyThemDetail(detailData))
                        {
                            ShowParentMessage(V6Text.InvoiceF3AddDetailSuccess);
                            All_Objects["data"] = detailData;
                            InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
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
                            detail1.ChangeToAddMode_KeepData();
                            dataGridView1.Lock();
                            ShowParentMessage(V6Text.InvoiceF3EditDetailSuccess);
                            All_Objects["data"] = detailData;
                            InvokeFormEvent(FormDynamicEvent.AFTEREDITDETAILSUCCESS);
                        }
                    }
                }
                else
                {
                    detail1.ChangeToAddMode_KeepData();
                    dataGridView1.Lock();
                }
            }
            else if (keyData == Keys.F4)
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    detail1.btnNhan.Focus();
                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        if (_maVt.Text == "")
                        {
                            Detail1FocusReset();
                        }
                        else
                        {
                            var detailData = detail1.GetData();
                            if (ValidateData_Detail(detailData))
                            {
                                detail1.btnNhan.Focus();
                                detail1.btnNhan.PerformClick();
                            }
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
        
        void MaKhoI_V6LostFocus(object sender)
        {
            XuLyChonMaKhoI();
        }

        
        public void XuLyLayThongTinKhiChonMaKhoI()
        {
            try
            {

                //VPA_GET_BROTHERS_DEFAULTVALUE();
                SetDefaultData_Brothers(Invoice, detail1, "AD", _maKhoI);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name + _sttRec, ex);
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
                    _ma_thue_i.Text = (data["MA_THUE"] ?? "").ToString().Trim();
                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["THUE_SUAT"]);
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["THUE_SUAT"]);
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
                //VPA_GET_BROTHERS_DEFAULTVALUE();
                SetDefaultData_Brothers(Invoice, this, "AM", txtMaKh);
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
            GetGiaVonCoDinh(_maVt, _sl_td1, _gia_nt01);
            TinhTienNt0();
            Tinh_thue_ct();

            chkT_THUE_NT.Checked = false;
            TinhTienNt0(_maVt);
            Tinh_thue_ct();
        }
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
          
        }
        
        public void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
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
                    SetADSelectMoreControlValue(Invoice, data);
                    _tkVt.Text = (data["TK_VT"] ?? "").ToString().Trim();
                    _hs_qd1.Value = ObjectAndString.ObjectToDecimal(data["HS_QD1"]);
                    _hs_qd2.Value = ObjectAndString.ObjectToDecimal(data["HS_QD2"]);
                    txtPMA_KHC.ChangeText(("" + data["PMA_KHC"]).Trim());

                    if (M_POA_MULTI_VAT == "1")
                    {
                        _ma_thue_i.Text = (data["MA_THUE"] ?? "").ToString().Trim();
                        _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["THUE_SUAT"]);
                        V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["THUE_SUAT"]);
                    }
                }

                if (_maVt.LO_YN)
                {
                    _maLo.Enabled = true;
                    _maLo.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");
                    _maLo.ExistRowInTable(true);
                    if (_maLo.Data != null)
                    {
                        _hanSd.Value = ObjectAndString.ObjectToDate(_maLo.Data["NGAY_HHSD"]);
                    }
                    else
                    {
                        _hanSd.Value = null;
                    }
                }
                else
                {
                    _maLo.Text = "";
                    _hanSd.Value = null;
                    _maLo.Enabled = false;
                }
                //VPA_GET_BROTHERS_DEFAULTVALUE();
                SetDefaultData_Brothers(Invoice, detail1, "AD", _maVt);

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

            TinhTienNt0();
        }

        /// <summary>
        /// Tính tiền nt, tính giá nt
        /// </summary>
        public void TinhTienNt()
        {
            try
            {
                //Tuanmh 19/12/2016 Chua Kiem tra Null

                _tienNt.Value = _tien_nt0.Value + _cpNt.Value - _ckNt.Value - _ggNt.Value + _tien_vcNt.Value;
                _tien.Value = _tien0.Value + _cp.Value - _ck.Value - _gg.Value + _tien_vc.Value;

                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tien_nt0.Value;
                    _tien.Value = _tienNt.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienNt " + _sttRec, ex);
            }
            TinhGiaNt();
        }

        public void TinhTienNt0(Control actionControl = null)
        {
            try
            {
                _tien_nt0.Value = V6BusinessHelper.Vround(_soLuong1.Value * _gia_nt01.Value, M_ROUND_NT);
                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * _sl_td1.Value, M_ROUND);
                else _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tien_nt0.Value;
                }
                _tienNt.Value = _tien_nt0.Value;
                _tien.Value = _tien0.Value;

                //Tuanmh 19/12/2016
                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tien_nt0, _pt_cki);

                TinhGiaNt();
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

        public void TinhGiaNt()
        {
            try
            {
                if (_soLuong1.Value != 0)
                {
                    _gia01.Value = V6BusinessHelper.Vround((_gia_nt01.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                    if (_maNt == _mMaNt0)
                    {
                        _gia01.Value = _gia_nt01.Value;
                    }
                }

                if (_soLuong.Value != 0)
                {
                    _gia_Nt0.Value = V6BusinessHelper.Vround((_tien_nt0.Value / _soLuong.Value),M_ROUND_GIA_NT);
                    _gia0.Value = V6BusinessHelper.Vround((_tien0.Value / _soLuong.Value), M_ROUND_GIA);

                    _gia_nt.Value = V6BusinessHelper.Vround((_tienNt.Value / _soLuong.Value), M_ROUND_GIA_NT);
                    _gia.Value = V6BusinessHelper.Vround((_tien.Value / _soLuong.Value), M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        _gia0.Value = _gia_Nt0.Value;
                        _gia.Value = _gia_nt.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiaNt01()
        {
            if (!chkSuaTien.Checked && _soLuong1.Value != 0)
            {
                _gia_nt01.Value = V6BusinessHelper.Vround(_tien_nt0.Value / _soLuong1.Value, M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia01.Value = _gia_nt01.Value;
                }
                else
                {
                    _gia01.Value = V6BusinessHelper.Vround(_tien0.Value / _soLuong1.Value, M_ROUND_GIA);
                }
            }
        }

        public void TinhTienVon_GiaVon()
        {
            try
            {
                _tien0.Value = V6BusinessHelper.Vround((_tien_nt0.Value * txtTyGia.Value), M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tien_nt0.Value;
                }
                else
                {
                    if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * _sl_td1.Value, M_ROUND);
                    else _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);
                }

                _tien.Value = _tien0.Value;
                _tienNt.Value = _tien_nt0.Value;



                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _gia01.Value = V6BusinessHelper.Vround(_gia_nt01.Value * _sl_td1.Value, M_ROUND);
                else _gia01.Value = V6BusinessHelper.Vround(_gia_nt01.Value * txtTyGia.Value, M_ROUND_GIA_NT);

                if (_maNt == _mMaNt0)
                {
                    _gia01.Value = _gia_nt01.Value;
                }

                if (_soLuong.Value != 0)
                {
                    if (_he_so1T.Value == 1 && _he_so1M.Value == 1)
                    {
                        _gia_nt.Value = _gia_nt01.Value;
                        _gia.Value = _gia01.Value;
                    }
                    else
                    {
                        _gia_nt.Value = V6BusinessHelper.Vround(_tienNt.Value / _soLuong.Value, M_ROUND_GIA_NT);
                        _gia.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong.Value, M_ROUND_GIA);
                    }

                    if (_maNt == _mMaNt0)
                    {
                        _gia.Value = _gia_nt.Value;
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
                    detail2.MODE = V6Mode.Lock;
                    dataGridView1.ReadOnly = true;
                }
                else //Cac truong hop khac
                {
                    detail2.MODE = V6Mode.View;
                    dataGridView2.UnLock();
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;
                    _tien_nt0.Enabled = chkSuaTien.Checked;
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

            SetControlReadOnlyHide(this, Invoice, Mode, V6Mode.View);
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
                decimal HE_SO1T = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1T"].Value);
                decimal HE_SO1M = ObjectAndString.ObjectToDecimal(row.Cells["HE_SO1M"].Value);
                if (HE_SO1T == 0) HE_SO1T = 1;
                if (HE_SO1M == 0) HE_SO1M = 1;
                //decimal HE_SO = HE_SO1T / HE_SO1M;

                UpdateDateTime4(row);
                ShowMainMessage("cell_end_edit: " + FIELD);

                switch (FIELD)
                {
                    case "SO_LUONG1":
                        #region ==== SO_LUONG1 ====

                        V6VvarTextBox txtmavt = new V6VvarTextBox() { VVar = "MA_VT", Text = cell_MA_VT.Value.ToString() };
                        if (txtmavt.Data != null && txtmavt.VITRI_YN)
                        {
                            var packs1 = ObjectAndString.ObjectToDecimal(txtmavt.Data["Packs1"]);
                            if (packs1 > 0 && ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) > packs1)
                            {
                                cell_SO_LUONG1.Value = packs1;
                            }
                        }

                        //_soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                        row.Cells["SO_LUONG"].Value = ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value) * HE_SO1T / HE_SO1M;
                        //TinhTienVon1(_soLuong1);
                        row.Cells["TIEN_NT0"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT01"].Value), M_ROUND_NT);
                        row.Cells["TIEN0"].Value = _maNt == _mMaNt0
                            ? row.Cells["TIEN_NT0"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT0"].Value) * txtTyGia.Value, M_ROUND);

                        //TinhTienVon(_soLuong1);
                        if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(row, FIELD);

                        #endregion ==== SO_LUONG1 ====
                        break;

                    case "GIA_NT01":
                        #region ==== GIA_NT01 ====

                        //TinhTienVon1(_gia_nt01);
                        row.Cells["TIEN_NT0"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT01"].Value), M_ROUND_NT);
                        row.Cells["TIEN0"].Value = _maNt == _mMaNt0
                            ? row.Cells["TIEN_NT0"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT0"].Value) * txtTyGia.Value, M_ROUND);
                        //TinhChietKhauChiTiet
                        TinhChietKhauChiTiet_row_NHAP_TIEN_NT0(false, row, txtTyGia.Value);
                        //TinhGiaVon();
                        row.Cells["GIA01"].Value = _maNt == _mMaNt0
                            ? row.Cells["GIA_NT01"].Value
                            : V6BusinessHelper.Vround((ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT01"].Value) * txtTyGia.Value), M_ROUND_GIA_NT);
                        //TinhGiaNt0
                        if (ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value) != 0)
                        {
                            row.Cells["GIA_NT0"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT0"].Value) / ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value), M_ROUND_GIA_NT);
                            row.Cells["GIA0"].Value = _maNt == _mMaNt0
                                ? row.Cells["GIA_NT0"].Value
                                : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN0"].Value)
                                    / ObjectAndString.ObjectToDecimal(row.Cells["SO_LUONG"].Value), M_ROUND_GIA);
                        }
                        TinhVanChuyen_row(row);
                        TinhGiamGiaCt_row(row);
                        Tinh_thue_ct_row_NHAP_TIEN0(row);

                        #endregion ==== GIA_NT01 ====
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

                ShowMainMessage("cell_begin_edit: " + FIELD);

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
            //--dataGridView1.ThemCongThuc("SO_LUONG1", "SO_LUONG=SO_LUONG1*HE SO1");
            //--dataGridView1.ThemCongThuc("SO_LUONG1", "THANH_TIEN=SO_LUONG*DON_GIA");


            #endregion ==== SO_LUONG1 ====

            //--dataGridView1.ThemCongThuc("DON_GIA", "THANH_TIEN=SO_LUONG*DON_GIA");
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
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, Invoice.Config2.GRDS_V1, Invoice.Config2.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config2.GRDHV_V1 : Invoice.Config2.GRDHE_V1);
            //V6ControlFormHelper.FormatGridViewAndHeader(dataGridView3, Invoice.Config3.GRDS_V1, Invoice.Config3.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config3.GRDHV_V1 : Invoice.Config3.GRDHE_V1);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView2, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3ChiPhi, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView4, Invoice.Mact);
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


        #region ==== Tính toán hóa đơn ====

        public override void XuLyThayDoiTyGia(V6NumberTextBox txtTyGia, CheckBox chkSuaTien)
        {
            try
            {
                var ty_gia = txtTyGia.Value;
                var ty_gia_von = txtTyGia.Value;


                // Tuanmh 25/05/2017
                if (ty_gia == 0 || chkSuaTien.Checked) return;

                {
                    foreach (DataRow row in AD.Rows)
                    {
                        string ma_vt = row["MA_VT"].ToString().Trim();
                        V6VvarTextBox vvar_ma_vt = new V6VvarTextBox() { VVar = "MA_VT" };
                        vvar_ma_vt.Text = ma_vt;
                        if (vvar_ma_vt.Data != null)
                        {
                            decimal ma_vt_sl_td3 = ObjectAndString.ObjectToDecimal(vvar_ma_vt.Data["SL_TD3"]);
                            if (vvar_ma_vt.GIA_TON == 5 && ma_vt_sl_td3 != 0) ty_gia_von = ma_vt_sl_td3;
                        }

                        FixTyGia(AD, row, ty_gia_von, "Tien", "Tien_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia_von, "Tien0", "TIEN_NT0", M_ROUND);
                        FixTyGia(AD, row, ty_gia_von, "GIA", "GIA_NT", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia_von, "GIA01", "GIA_NT01", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia_von, "GIA1", "GIA_NT1", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia_von, "GIA0", "GIA_NT0", M_ROUND_GIA);


                        FixTyGia(AD, row, ty_gia, "Tien2", "Tien_nt2", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien1", "Tien1_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Tien_vc", "Tien_vc_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "Thue", "Thue_nt", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "CP", "CP_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "GIA2", "GIA_NT2", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "GIA21", "GIA_NT21", M_ROUND_GIA);
                        FixTyGia(AD, row, ty_gia, "NK", "NK_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "CK", "CK_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "GG", "GG_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "TT", "TT_NT", M_ROUND);

                        FixTyGia(AD, row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(AD, row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail1") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        if (_maVt.Data != null)
                        {
                            if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) ty_gia_von = _sl_td1.Value;
                        }

                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "Tien", "Tien_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "Tien0", "TIEN_NT0", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "GIA", "GIA_NT", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "GIA01", "GIA_NT01", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "GIA1", "GIA_NT1", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia_von, "GIA0", "GIA_NT0", M_ROUND_GIA);

                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien2", "Tien_nt2", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien1", "Tien1_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Tien_vc", "Tien_vc_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "Thue", "Thue_nt", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "CP", "CP_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA2", "GIA_NT2", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GIA21", "GIA_NT21", M_ROUND_GIA);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "NK", "NK_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "CK", "CK_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "GG", "GG_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "TT", "TT_NT", M_ROUND);

                        FixTyGiaDetail(AD, detailControl, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGiaDetail(AD, detailControl, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                }

                if (AD2 != null)
                {
                    foreach (DataRow row in AD2.Rows)
                    {
                        FixTyGia(AD2, row, ty_gia, "t_tien", "t_tien_nt", M_ROUND);
                        FixTyGia(AD2, row, ty_gia, "t_thue", "t_thue_nt", M_ROUND);
                        FixTyGia(AD2, row, ty_gia, "t_tt", "t_tt_nt", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail2") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_tien", "t_tien_nt", M_ROUND);
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_thue", "t_thue_nt", M_ROUND);
                        FixTyGiaDetail(AD2, detailControl, ty_gia, "t_tt", "t_tt_nt", M_ROUND);
                    }
                }

                if (AD3 != null)
                {
                    foreach (DataRow row in AD3.Rows)
                    {
                        FixTyGia(AD3, row, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGia(AD3, row, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                    HD_Detail detailControl = GetControlByName("detail3") as HD_Detail;
                    if (detailControl != null && (detailControl.MODE == V6Mode.Add || detailControl.MODE == V6Mode.Edit))
                    {
                        FixTyGiaDetail(AD3, detailControl, ty_gia, "PS_NO", "PS_NO_NT", M_ROUND);
                        FixTyGiaDetail(AD3, detailControl, ty_gia, "PS_CO", "PS_CO_NT", M_ROUND);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void TinhTongValues()
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

        public void TinhPhanBoGiamGia()
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

                var tTienNt0 = TinhTong(AD, "TIEN_NT0");
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

        public void TinhThue()
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
                txtManx.Text = Invoice.Alct["TK_CO"].ToString().Trim();
                cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();
                TxtTk_i_ao.Text = txtManx.Text;
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
                    SetDetailControlVisible(detailControlList1, true, "GIA01", "TIEN0");
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

                    SetDetailControlVisible(detailControlList2, true, "T_TIEN", "T_THUE");
                }
                else
                {
                    M_ROUND = V6Setting.RoundTien;
                    M_ROUND_GIA = V6Setting.RoundGia;
                    M_ROUND_NT = M_ROUND;
                    M_ROUND_GIA_NT = M_ROUND_GIA;


                    txtTyGia.Enabled = false;
                    txtTyGia.Value = 1;
                    SetDetailControlVisible(detailControlList1, false, "GIA01", "TIEN0");
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

                    SetDetailControlVisible(detailControlList2, false, "T_TIEN", "T_THUE");
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
                    if (textBox != null)
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
                _tien_nt0.DecimalPlaces = decimalTienNt;
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
                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat_i.Value, _tien_nt0.Value - _ckNt.Value - _ggNt.Value, _tien0.Value - _ck.Value - _gg.Value, _thue_nt, _thue);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tinh thue ct M_POA_MULTY_VAT = 1.");
            }
        }

        /// <summary>
        /// Tinh_TienThueNtVaTienThue_TheoThueSuat_Row có trừ ck hoặc gg bởi M_POA_VAT_WITH_CK_GG, THUE_SUAT_I TIEN_NT0 TIEN0
        /// </summary>
        /// <param name="row"></param>
        public void Tinh_thue_ct_row_NHAP_TIEN0(DataGridViewRow row)
        {
            if (M_SOA_MULTI_VAT == "1")
            {
                decimal ck_nt_value = 0, ck_value = 0, gg_nt_value = 0, gg_value = 0;
                string M_POA_VAT_WITH_CK_GG = V6Options.GetValue("M_POA_VAT_WITH_CK_GG");
                if (M_POA_VAT_WITH_CK_GG.Length > 0 && M_POA_VAT_WITH_CK_GG[0] == '1')
                {
                    ck_value = ObjectAndString.ObjectToDecimal(row.Cells["CK"].Value);
                    ck_nt_value = ObjectAndString.ObjectToDecimal(row.Cells["CK_NT"].Value);
                }
                if (M_POA_VAT_WITH_CK_GG.Length > 1 && M_POA_VAT_WITH_CK_GG[1] == '1')
                {
                    gg_value = ObjectAndString.ObjectToDecimal(row.Cells["GG"].Value);
                    gg_nt_value = ObjectAndString.ObjectToDecimal(row.Cells["GG_NT"].Value);
                }
                Tinh_TienThueNtVaTienThue_TheoThueSuat_Row(ObjectAndString.ObjectToDecimal(row.Cells["THUE_SUAT_I"].Value),
                    ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT0"].Value) - ck_nt_value - gg_nt_value,
                    ObjectAndString.ObjectToDecimal(row.Cells["TIEN0"].Value) - ck_value - gg_value, row);
            }
        }
        
        /// <summary>
        /// Lấy dữ liệu AD va AD2 dựa vào rec, tạo 1 copy gán vào AD
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

            //Load AD2
            if (AD2Tables == null) AD2Tables = new SortedDictionary<string, DataTable>();
            if (AD2Tables.ContainsKey(sttRec))
            {
                AD2 = AD2Tables[sttRec].Copy();
            }
            else
            {
                try
                {
                    AD2Tables[sttRec] = Invoice.LoadAD2(sttRec);
                    AD2 = AD2Tables[sttRec].Copy();
                }
                catch
                {
                    AD2Tables[sttRec] = Invoice.LoadAD2(sttRec);
                    AD2 = AD2Tables[sttRec].Copy();
                }
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
                    CurrentIndex = index;
                    LoadAD(_sttRec);
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
        /// Cần set trước AD cho đúng với index
        /// </summary>
        private void ViewInvoice()
        {
            try
            {
                Mode = V6Mode.View;
                var row = AM.Rows[CurrentIndex];
                V6ControlFormHelper.SetFormDataRow(this, row);
                txtMaDVCS.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                TxtMa_kh_i_ao.Text = row["Ma_kh_i"].ToString().Trim();
                TxtT_cp_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp"]);
                TxtTk_i_ao.Text = row["Tk_i"].ToString().Trim();
                TxtT_cp_nt_ao.Value = ObjectAndString.ObjectToDecimal(row["T_Cp_nt"]);
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");

                XuLyThayDoiMaDVCS();
                SetGridViewData();
                XuLyThayDoiMaNt();
                Mode = V6Mode.View;
                //btnSua.Focus();
                FormatNumberControl();
                FormatNumberGridView();
                LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);

                OnInvoiceChanged(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        #region ==== Add Thread ====
        public IDictionary<string, object> readyDataAM;
        public List<IDictionary<string, object>> readyDataAD, readyDataAD2;
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
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.DoNoThing, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
            }
        }

        private void ReadyForAdd()
        {
            try
            {
                readyDataAD = dataGridView1.GetData(_sttRec);
                readyDataAD2 = dataGridView2.GetData(_sttRec);
                All_Objects["readyDataAD"] = readyDataAD;
                All_Objects["readyDataAD2"] = readyDataAD2;
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
                
                if (Invoice.InsertInvoice(readyDataAM, readyDataAD, readyDataAD2))
                {
                    _AED_Success = true;
                    if (Invoice.IS_AM2TH(readyDataAM))
                    {
                        DoAdd2_TH_Thread();
                    }
                }
                else
                {
                    base.SaveTemp("SAVEFAIL1");
                    _AED_Success = false;
                    addErrorMessage = V6Text.Text("ADD0");
                    Invoice.PostErrorLog(_sttRec, "M");
                }
            }
            catch (Exception ex)
            {
                base.SaveTemp("SAVEFAIL2");
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
                Invoice.InsertInvoice2_TH(readyDataAM, readyDataAD, readyDataAD2);
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
                detail1.MODE = V6Mode.View;
                detail2.MODE = V6Mode.View;
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
                readyDataAD2 = dataGridView2.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in readyDataAD2)
                {
                    adRow["DATE0"] = am_DATE0;
                    adRow["TIME0"] = am_TIME0;
                    adRow["USER_ID0"] = am_U_ID0;
                }
                All_Objects["readyDataAD"] = readyDataAD;
                All_Objects["readyDataAD2"] = readyDataAD2;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        void checkEdit_Tick(object sender, EventArgs e)
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
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.DoNoThing, TongThanhToan, TongThanhToanNT, true);
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
                if (Invoice.UpdateInvoice(readyDataAM, readyDataAD, readyDataAD2, keys))
                {
                    _AED_Success = true;
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    // WriteDBlog.
                    SaveEditLog(AM_current.ToDataDictionary(), readyDataAM);

                    DoEdit2_TH_Thread(keys);
                }
                else
                {
                    _AED_Success = false;
                    editErrorMessage = V6Text.Text("SUA0");
                    Invoice.PostErrorLog(_sttRec, "S");
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
                            Invoice.UpdateInvoice2_TH(readyDataAM, readyDataAD, readyDataAD2, _keys_TH);
                        else Invoice.DeleteInvoice2_TH(_sttRec);
                    }
                    else
                    {
                        if (Invoice.IS_AM2TH(readyDataAM))
                            Invoice.InsertInvoice2_TH(readyDataAM, readyDataAD, readyDataAD2);
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
        void checkDelete_Tick(object sender, EventArgs e)
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
            //Xóa xong view lại cái khác
            try
            {
                var row = AM.Rows[CurrentIndex];
                _sttRec = row["Stt_rec"].ToString().Trim();
                if (Invoice.DeleteInvoice(_sttRec))
                {
                    if (Invoice.IS_AM2TH(row.ToDataDictionary()))
                    {
                        _sttRec2_TH = _sttRec;
                        DoDelete2_TH_Thread();
                    }
                    _AED_Success = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                }
                else
                {
                    _AED_Success = false;
                    deleteErrorMessage = V6Text.Text("XOA0");
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice.DeleteInvoice return false.");
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

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||
                    detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit )
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    EnableFunctionButtons();
                }
                else
                {
                    V6ControlFormHelper.RemoveRunningList(_sttRec);
                    readyDataAM = PreparingDataAM(Invoice);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD2);
                    V6ControlFormHelper.UpdateDKlistAll(readyDataAM, new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD3);

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

        public void Moi()
        {
            _autoloadtop_acted = true;
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

                    bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
                    bool alt = (ModifierKeys & Keys.Alt) == Keys.Alt;
                    DataSet loadtempDS = null;
                    if (ctrl && alt)
                    {
                        loadtempDS = LoadTemp();
                    }

                    if (ctrl && alt && loadtempDS != null)
                    {
                        AM_old = null;

                        ResetForm();
                        Mode = V6Mode.Add;
                        //txtLoaiPhieu.ChangeText(_maGd);
                        //LoadAll(V6Mode.Add);
                        GetSttRec(Invoice.Mact);

                        V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);

                        // set data temp
                        V6ControlFormHelper.SetFormDataRow(this, loadtempDS.Tables["AM"].Rows[0]);
                        txtMaDVCS.ExistRowInTable();
                        txtMaKh.ExistRowInTable();
                        //txtLoaiPhieu.ExistRowInTable(true);
                        ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");

                        XuLyThayDoiMaDVCS();
                        //{Tuanmh 20/02/2016
                        XuLyThayDoiMaNt();

                        // add gridview data...
                        if (loadtempDS.Tables.Contains("AD"))
                        {
                            AD.AddRowByTable(loadtempDS.Tables["AD"]);
                        }
                        if (loadtempDS.Tables.Contains("AD2"))
                        {
                            AD2.AddRowByTable(loadtempDS.Tables["AD2"]);
                        }
                        if (loadtempDS.Tables.Contains("AD3"))
                        {
                            AD3.AddRowByTable(loadtempDS.Tables["AD3"]);
                        }

                        detail2.MODE = V6Mode.View;
                        GoToFirstFocus(txtMa_sonb);
                    }
                    else // bình thường
                    {

                        AM_old = IsViewingAnInvoice ? AM.Rows[CurrentIndex] : null;
                        ResetForm();
                        Mode = V6Mode.Add;

                        GetSttRec(Invoice.Mact);
                        V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                        //GetSoPhieu();
                        GetM_ma_nt0();
                        GetTyGiaDefault();
                        GetDefault_Other();
                        SetDefaultData(Invoice);
                        GET_AM_OLD_EXTRA();
                        detail1.DoAddButtonClick();
                        var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                        if (readonly_list.Contains(detail1.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            detail1.ChangeToViewMode();
                            dataGridView1.UnLock();
                        }
                        else
                        {
                            dataGridView1.Lock();
                            SetDefaultDetail();
                        }
                        detail2.MODE = V6Mode.Init;
                        GoToFirstFocus(txtMa_sonb);
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

        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                if (!IsViewingAnInvoice) return;
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

                            if (check_edit == true)
                            {
                                Mode = V6Mode.Edit;
                                detail1.MODE = V6Mode.View;
                                detail2.MODE = V6Mode.View;
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
                if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact)
                    && V6Login.UserRight.AllowEditDeleteMact(Invoice.Mact, _sttRec, "X"))
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
                    if (string.IsNullOrEmpty(_sttRec))
                    {
                        this.ShowWarningMessage("Chưa chọn phiếu nhập.");
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
                ResetAllADDefaultValue();
                InvokeFormEventFixCopyData();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetNewValues " + _sttRec, ex);
            }
        }

        private Tim_PhieuDiDuongINT_Form SearchForm
        {
            get
            {
                if (_timForm == null || _timForm.IsDisposed)
                    _timForm = new Tim_PhieuDiDuongINT_Form(Invoice, V6Mode.View);
                return _timForm;
            }
        }
        private Tim_PhieuDiDuongINT_Form _timForm;
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
            _autoloadtop_acted = true;
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
                        ResetADTables();
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
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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
                BasePrint(Invoice, _sttRec, V6PrintMode.DoNoThing, TongThanhToan, TongThanhToanNT, true);
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

        public override void Huy()
        {
            try
            {
                dataGridView1.UnLock();
                dataGridView2.UnLock();
                dataGridView3ChiPhi.UnLock();
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

        private void XuLyDetailClickAdd(object sender)
        {
            try
            {
                SetDefaultDetail();
                var readonly_list = SetControlReadOnlyHide(detail1, Invoice, Mode, V6Mode.Add);
                if (readonly_list.Contains(detail1.btnMoi.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    detail1.ChangeToViewMode();
                    dataGridView1.UnLock();
                }
                else
                {
                    dataGridView1.Lock();
                    Detail1FocusReset();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public override void SetDefaultDetail()
        {
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            if (!shift_is_down) return;

            if (_Ma_lnx_i != null && txtLoaiNX_PH.Text != string.Empty)
            {
                if (_Ma_lnx_i != null) _Ma_lnx_i.Text = txtLoaiNX_PH.Text;
            }
            SetDefaultDataHDDetail(Invoice, detail1);
        }

        private void XuLyDetail2ClickAdd(object sender)
        {
            try
            {
                if (AD2 == null || AD2.Rows.Count == 0)
                {
                    detail2.CarryData = null;

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
                    _mau_bc22.Value = 1;
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

                var readonly_list = SetControlReadOnlyHide(detail2, Invoice, Mode, V6Mode.Add);
                if (readonly_list.Contains(detail2.btnMoi.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    detail2.ChangeToViewMode();
                    dataGridView2.UnLock();
                }
                else
                {
                    _mau_bc22.Focus();
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
                var svDate = V6BusinessHelper.GetServerDateTime();
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;
                // Add
                {
                    var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
                    var date = svDate.Date;
                    data["TIME04"] = time;
                    data["DATE04"] = date;
                    data["USER_ID04"] = V6Login.UserId;
                    data["TIME24"] = time;
                    data["DATE24"] = date;
                    data["USER_ID24"] = V6Login.UserId;
                }

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                if (error == "")
                {
                    // Check cộng số lượng // Bộ check gồm MA_VT DVT1 MA_KHO

                    DataRow containsRow = null;
                    int con_index = -1;
                    if (_detail1Focus is V6QRTextBox
                        && Invoice.ExtraInfo_QrChecks != null && Invoice.ExtraInfo_QrSums != null
                        && ADContains(data, Invoice.ExtraInfo_QrChecks, out containsRow, out con_index))
                    {
                        foreach (string SUM_FIELD in Invoice.ExtraInfo_QrSums)
                        {
                            if (!data.ContainsKey(SUM_FIELD)) continue;
                            var column = AD.Columns[SUM_FIELD];
                            object value = ObjectAndString.ObjectTo(column.DataType,
                                ObjectAndString.ObjectToDecimal(containsRow[SUM_FIELD])
                                + ObjectAndString.ObjectToDecimal(data[SUM_FIELD]));
                            containsRow[SUM_FIELD] = value;
                        }

                        dataGridView1.DataSource = AD;
                        // tô màu gridview
                        var sum_color = Color.DarkOrange;
                        if (con_index >= 0)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (i == con_index) continue;
                                var cellStyle = dataGridView1.Rows[i].DefaultCellStyle;
                                if (cellStyle.BackColor == sum_color)
                                {
                                    if (i % 2 == 0) cellStyle.BackColor = dataGridView1.RowsDefaultCellStyle.BackColor;
                                    else cellStyle.BackColor = dataGridView1.AlternatingRowsDefaultCellStyle.BackColor;
                                }
                            }
                            dataGridView1.Rows[con_index].DefaultCellStyle.BackColor = sum_color;
                        }
                    }
                    else  // Hoặc thêm dòng như bình thường.
                    {
                        UpdateDetailChangeLog(_sttRec0, detailControlList1, null, data);
                        //Tạo dòng dữ liệu mới.
                        var newRow = AD.NewRow();
                        foreach (DataColumn column in AD.Columns)
                        {
                            var key = column.ColumnName.ToUpper();
                            object value = ObjectAndString.ObjectTo(column.DataType,
                                data.ContainsKey(key) ? data[key] : "") ?? DBNull.Value;
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

        public override bool XuLyThemDetail2(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                var svDate = V6BusinessHelper.GetServerDateTime();
                _sttRec02 = V6BusinessHelper.GetNewSttRec0(AD2);
                data.Add("STT_REC0", _sttRec02);
                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;
                // Add
                {
                    var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
                    var date = svDate.Date;
                    data["TIME04"] = time;
                    data["DATE04"] = date;
                    data["USER_ID04"] = V6Login.UserId;
                    data["TIME24"] = time;
                    data["DATE24"] = date;
                    data["USER_ID24"] = V6Login.UserId;
                }

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
                        var svDate = V6BusinessHelper.GetServerDateTime();
                        //Thêm thông tin...
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Date;
                        // Edit
                        {
                            var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
                            var date = svDate.Date;
                            data["TIME24"] = time;
                            data["DATE24"] = date;
                            data["USER_ID24"] = V6Login.UserId;
                        }

                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                        if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
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
                        var svDate = V6BusinessHelper.GetServerDateTime();
                        //Thêm thông tin...
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Date;
                        // Edit
                        {
                            var time = ObjectAndString.ObjectToString(svDate, "HH:mm:ss");
                            var date = svDate.Date;
                            data["TIME24"] = time;
                            data["DATE24"] = date;
                            data["USER_ID24"] = V6Login.UserId;
                        }

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
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + details) == DialogResult.Yes)
                        {
                            var delete_data = currentRow.ToDataDictionary();
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, detailControlList1, delete_data, null);
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
        private void XuLyXoaDetail2()
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                var readonly_list = SetControlReadOnlyHide(new HD_Detail() { Name = "detail2" }, Invoice, Mode, V6Mode.Delete);
                if (readonly_list.Contains(detail2.btnXoa.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                    return;
                }

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
                            detail2.SetData(dataGridView2.CurrentRow.ToDataDictionary());
                            TinhTongThanhToan("xu ly xoa detail2");
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
            //LoadTag(1, Invoice.Mact, Invoice.Mact, m_itemId, "");
            SetStatus2Text();

            if (IsViewingAnInvoice)
            {
                if (ClickSuaOnLoad)
                {
                    ClickSuaOnLoad = false;
                    btnSua.PerformClick();
                }
            }
            else if (Invoice.ExtraInfor_AutoLoadTop)
            {
                AutoLoadTop(timTopCuoiKyMenu);
            }
            else
            {
                btnMoi.Focus();
            }
        }

        #region ==== Command Buttons ====
        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
            bool alt = (ModifierKeys & Keys.Alt) == Keys.Alt;
            if (ctrl && alt)
            {
                SaveTemp("CTRLALT");
                return;
            }

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

        
        private void Detail1_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetailClickAdd(sender);
            }
            else
            {
                dataGridView1.UnLock();
                ViewCurrentRowToDetail(dataGridView1, detail1);
            }
        }
        private void detail2_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetail2ClickAdd(sender);
            }
            else
            {
                dataGridView2.UnLock();
                ViewCurrentRowToDetail(dataGridView2, detail2);
            }
        }

        private void hoaDonDetail1_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data))
                {
                    dataGridView1.UnLock();
                    All_Objects["data"] = data;
                    InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                    {
                        SaveTemp("MAXROWSAVETEMP");
                    }
                    return;
                }
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail2(data))
            {
                if (XuLyThemDetail2(data))
                {
                    dataGridView2.UnLock();
                    return;
                }
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        

        private void hoaDonDetail1_ClickEdit(object sender, HD_Detail_Eventargs e)
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
                        dataGridView1.UnLock();
                    }
                    else
                    {
                        dataGridView1.Lock();
                        if (!string.IsNullOrEmpty(_sttRec0))
                        {
                            XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                            Detail1FocusReset();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void detail2_ClickEdit(object sender, HD_Detail_Eventargs e)
        {
            try
            {
                if (AD2 != null && AD2.Rows.Count > 0 && dataGridView2.DataSource != null)
                {
                    detail2.ChangeToEditMode();
                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView2, detail2, out _gv2EditingRow, out _sttRec02);
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

                        var readonly_list = SetControlReadOnlyHide(detail2, Invoice, Mode, V6Mode.Edit);
                        if (readonly_list.Contains(detail2.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            detail2.ChangeToViewMode();
                            dataGridView2.UnLock();
                        }
                        else
                        {
                            _mau_bc22.Focus();
                        }
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
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data))
                {
                    dataGridView1.UnLock();
                    All_Objects["data"] = data;
                    InvokeFormEvent(FormDynamicEvent.AFTEREDITDETAILSUCCESS);
                    GotoNextDetailEdit(dataGridView1, detail1, chkAutoNext.Checked);
                    if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                    {
                        SaveTemp("MAXROWSAVETEMP_EDIT");
                    }
                    return;
                }
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail2_EditHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail2(data))
            {
                if (XuLySuaDetail2(data))
                {
                    dataGridView2.UnLock();
                    return;
                }
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail1_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail();
        }
        private void hoaDonDetail2_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail2();
        }
        private void hoaDonDetail1_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            dataGridView1.UnLock();
            ViewCurrentRowToDetail(dataGridView1, detail1);
        }
        private void hoaDonDetail2_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            dataGridView2.UnLock();
            detail2.SetData(_gv2EditingRow.ToDataDictionary());
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
            ViewCurrentRowToDetail(dataGridView1, detail1);
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
                //Tuanmh 04/01/2020 Round lai tien_nt(_maNt) theo round cua tien (_mMaNt0) khi _maNt=_mMaNt0
                XuLyThayDoiMaNt(txtTyGia, chkSuaTien, _maNt, _mMaNt0);

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
            BasePrint(Invoice, _sttRec, V6PrintMode.DoNoThing, TongThanhToan, TongThanhToanNT, false);
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
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                _tien_nt0.Enabled = chkSuaTien.Checked;
            if (chkSuaTien.Checked)
            {
                _tien_nt0.Tag = null;
            }
            else
            {
                _tien_nt0.Tag = "disable";
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

        public void TinhPhanBoChiPhi0(string loai_pb)
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

        public void TinhPhanBoChiPhi(string loai_pb)
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

        private void chonTuExcelVATMenu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabThue;
            ChonExcelVAT();
        }

        private void xuLyQRCODEMenu_Click(object sender, EventArgs e)
        {
            string program = "A" + Invoice.Mact + "_XULYKHAC4";
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            XuLyKhacQR(program, shift, _qr_code0);
        }

        public override void qrTransfer_AcceptData(List<IDictionary<string, object>> table, ChonEventArgs e)
        {
            var count = 0;
            _message = "";
            detail1.MODE = V6Mode.View;
            dataGridView1.UnLock();
            if (table == null || table.Count == 0) return;
            var row0 = table[0];
            if (row0.ContainsKey("MA_VT") && row0.ContainsKey("MA_KHO_I") && row0.ContainsKey("SO_LUONG1"))
            {
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }

                var AM_somedata = new Dictionary<string, object>();
                var ad2am_dic = ObjectAndString.StringToStringDictionary(e.AD2AM, ',', ':');
                var tMA_VT = new V6VvarTextBox() { VVar = "MA_VT" };
                foreach (IDictionary<string, object> row in table)
                {
                    var data = row;
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    // { Tuanmh 31/08/2016 Them thong tin ALVT fix 28/12/2023
                    tMA_VT.Text = cMaVt;
                    var datavt = tMA_VT.Data;
                    foreach (KeyValuePair<string, string> item in ad2am_dic)
                    {
                        if (data.ContainsKey(item.Key) && !AM_somedata.ContainsKey(item.Value.ToUpper()))
                        {
                            AM_somedata[item.Value.ToUpper()] = data[item.Key.ToUpper()];
                        }
                    }

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

                        //var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["TIEN_NT0"]);
                        //var __gia_nt0 = ObjectAndString.ObjectToDecimal(data["GIA_NT01"]);
                        //var __tien0 = V6BusinessHelper.Vround(__tien_nt0 * txtTyGia.Value, M_ROUND);
                        //var __gia0 = V6BusinessHelper.Vround(__gia_nt0 * txtTyGia.Value, M_ROUND_GIA);

                        //if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);
                        //if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", data["TIEN_NT0"]);
                        //if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);
                        //if (!data.ContainsKey("GIA01")) data.Add("GIA01", __gia0);
                        //if (!data.ContainsKey("GIA0")) data.Add("GIA0", __gia0);
                        //if (!data.ContainsKey("GIA")) data.Add("GIA", __gia0);
                        //if (!data.ContainsKey("GIA1")) data.Add("GIA1", __gia0);
                        //if (!data.ContainsKey("GIA_NT0")) data.Add("GIA_NT0", data["GIA_NT01"]);
                        //if (!data.ContainsKey("GIA_NT")) data.Add("GIA_NT", data["GIA_NT01"]);
                        //if (!data.ContainsKey("GIA_NT1")) data.Add("GIA_NT1", data["GIA_NT01"]);
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

                if (!string.IsNullOrEmpty(e.AD2AM))
                {
                    SetSomeData(AM_somedata);
                }
                InvokeFormEvent("AFTERACCEPTDATAQR");
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
                if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                {
                    SaveTemp("MAXROWSAVETEMP");
                }
            }
            else
            {
                ShowParentMessage(V6Text.Text("LACKINFO"));
            }
        }

        private void txtMA_BP_V6LostFocus(object sender)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, this, "AM", txtMA_BP);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name + _sttRec, ex);
            }
        }

        private void txtMA_NVIEN_V6LostFocus(object sender)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, this, "AM", txtMA_NVIEN);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name + _sttRec, ex);
            }
        }

        private void lblDetailUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotAddEdit) return;
                if (detail1.IsAddOrEdit) return;

                
                dataGridView1.MoveCurrentRowUp();
                ResetSttRec0();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name + _sttRec, ex);
            }
        }

        private void lblDetailDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotAddEdit) return;
                if (detail1.IsAddOrEdit) return;

                
                dataGridView1.MoveCurrentRowDown();
                ResetSttRec0();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name + _sttRec, ex);
            }
        }

        private void lblTrangThai_Click(object sender, EventArgs e)
        {
            ViewTrangThaiHistory();
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

        public void TinhT_CpNt()
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
            LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);
        }

        private void txtManx_V6LostFocus(object sender)
        {
            if (TxtTk_i_ao.Text.Trim() == "") TxtTk_i_ao.Text = txtManx.Text;
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
                    this.ShowWarningMessage(V6Text.Text("CHUACHONKIEUPOST"), "CHUACHONKIEUPOST");
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
                return false;
            }

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
                this.WriteExLog(GetType() + ".ValidateData_Detail3 " + _sttRec, ex);
            }
            return true;
        }

        public override bool ValidateData_Detail2(IDictionary<string, object>  data)
        {
            try
            {
                string firstErrorField;
                string errors = ValidateDetail2Data(detail2, Invoice, data, out firstErrorField);
                if (!string.IsNullOrEmpty(errors))
                {
                    this.ShowWarningMessage(errors);
                    detail2.SetFormControlsReadOnly(false);
                    var c = detail2.GetControlByAccessibleName(firstErrorField);
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
            pb_changed = false;
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                if (!chkTempSuaCT.Checked) detail1.AutoFocus();
            }
            else if (tabControl1.SelectedTab == tabThue)
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
                chonExcel.Program = Form_program;
                chonExcel.All_Objects = All_Objects;
                chonExcel.DynamicFixMethodName = "DynamicFixExcel";
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,SO_LUONG1,GIA_NT01,TIEN_NT0";
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

        public void chonExcel_AcceptData(DataTable table)
        {
            chonExcel_AcceptData(table.ToListDataDictionary(), new ChonEventArgs());
        }
        public override void chonExcel_AcceptData(List<IDictionary<string, object>> table, ChonEventArgs e)
        {
            var count = 0;
            _message = "";
            detail1.MODE = V6Mode.View;
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

                var AM_somedata = new Dictionary<string, object>();
                var ad2am_dic = ObjectAndString.StringToStringDictionary(e.AD2AM, ',', ':');
                var tMA_VT = new V6VvarTextBox() { VVar = "MA_VT" };
                foreach (IDictionary<string, object> row in table)
                {
                    var data = row;
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    // { Tuanmh 31/08/2016 Them thong tin ALVT fix 28/12/2023
                    tMA_VT.Text = cMaVt;
                    var datavt = tMA_VT.Data;
                    foreach (KeyValuePair<string, string> item in ad2am_dic)
                    {
                        if (data.ContainsKey(item.Key) && !AM_somedata.ContainsKey(item.Value.ToUpper()))
                        {
                            AM_somedata[item.Value.ToUpper()] = data[item.Key.ToUpper()];
                        }
                    }

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

                if (!string.IsNullOrEmpty(e.AD2AM))
                {
                    SetSomeData(AM_somedata);
                }
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
                if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                {
                    SaveTemp("MAXROWSAVETEMP");
                }
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

        private void xuLyKhacMenu_Click(object sender, EventArgs e)
        {
            string program = "A" + Invoice.Mact + "_XULYKHAC";
            XuLyKhac(program);
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

        private void chonDonHangMuaMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonDonHangMua(shift);
        }
        
        private void chonDonHangBanMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonDonHangBan(shift);
        }

        private void ChucNang_ChonDonHangMua(bool add = false)
        {
            try
            {
                chon_accept_flag_add = add;
                //var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                if (ma_dvcs != "")
                {
                    CDHM_INT_Form chon = new CDHM_INT_Form(dateNgayCT.Date, txtMaDVCS.Text, txtMaKh.Text);
                    _chon_px = "POH";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    //if (ma_kh == "") message += V6Text.NoInput + lblMaKH.Text;
                    if (ma_dvcs == "") message += V6Text.NoInput + lblMaDVCS.Text;
                    this.ShowWarningMessage(message);
                    //if (ma_kh == "") txtMaKh.Focus();
                    if (ma_dvcs == "") txtMaDVCS.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang ChonDonHangMua " + _sttRec, ex);
            }
        }

        private void ChucNang_ChonDonHangBan(bool add = false)
        {
            try
            {
                if (NotAddEdit) return;

                chon_accept_flag_add = add;
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                if (ma_dvcs != "")
                {
                    CDHB_PhieuDiDuongINT_Form chon = new CDHB_PhieuDiDuongINT_Form(dateNgayCT.Date, txtMaDVCS.Text, txtMaKh.Text);
                    _chon_px = "SOH";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID can not empty!";
                    this.ShowWarningMessage(message);
                    if (ma_dvcs == "") txtMaDVCS.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ChonDonHang " + _sttRec, ex);
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
                dataGridView1.UnLock();
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
                    All_Objects["data"] = newData;
                    InvokeFormEvent("DONDATHANGMUA_CDHB");
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
                        txtMaKh.CallDoV6LostFocus();
                    }
                }
                
                if (!string.IsNullOrEmpty(e.AD2AM))
                {
                    SetSomeData(AM_somedata);
                }
                All_Objects["selectedDataList"] = selectedDataList;
                InvokeFormEvent("AFTERCHON_" + _chon_px);
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed: {1}{2}", addCount, failCount, _message));
                
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
            FixMenuChucNangItemShiftText(chonDonHangBanMenu, chonDonHangMuaMenu, chonTuExcelMenu, importXmlMenu);
        }

        private void btnApGia_Click(object sender, EventArgs e)
        {
            ApGiaMua();
            ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);
        }

        private bool _flag_next = false;
        /// <summary>
        /// Áp giá mua.
        /// </summary>
        /// <param name="auto">Dùng khi gọi trong code động.</param>
        public override void ApGiaMua(bool auto = false)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            try
            {
                if (NotAddEdit) return;
                if (AD == null || AD.Rows.Count == 0) return;
                string shift_stt_rec0 = dataGridView1.CurrentRow.Cells["STT_REC0"].Value.ToString().Trim().ToUpper();

                if (NotAddEdit) return;
                if (_flag_next)
                {
                    _flag_next = false;
                    return;
                }

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    if (!auto) this.ShowWarningMessage(V6Text.DetailNotComplete);
                    return;
                }
                if (txtMaGia.Text.Trim() == "")
                {
                    ShowParentMessage(V6Text.NoInput + btnApGia.Text);
                    return;
                }
                if (!auto && this.ShowConfirmMessage(V6Text.Text("ASKAPGIAMUAALL")) != DialogResult.Yes)
                {
                    return;
                }
                if (auto)
                {
                    if (All_Objects.ContainsKey("txtMaKh.CallDoV6LostFocus") && ObjectAndString.ObjectToBool(All_Objects["txtMaKh.CallDoV6LostFocus"]))
                    {
                        All_Objects["txtMaKh.CallDoV6LostFocus"] = 0;
                    }
                    else
                    {
                        if (this.ShowConfirmMessage(V6Text.Text("ASKAPGIAMUAALL")) != DialogResult.Yes)
                        {
                            if (ActiveControl == txtMaKh)
                            {
                                _flag_next = true;
                                SelectNextControl(ActiveControl, true, true, true, true);
                                _flag_next = false;
                            }
                            return;
                        }
                    }
                }

                foreach (DataRow row in AD.Rows)
                {
                    var stt_rec0 = row["STT_REC0"].ToString().Trim().ToUpper();
                    if (shift && shift_stt_rec0 != stt_rec0)
                    {
                        continue;
                    }
                    var maVatTu = row["MA_VT"].ToString().Trim();
                    var dvt = row["DVT"].ToString().Trim();
                    var dvt1 = row["DVT1"].ToString().Trim();
                    var pt_cki = ObjectAndString.ObjectToDecimal(row["PT_CKI"]);
                    var soLuong = ObjectAndString.ObjectToDecimal(row["SO_LUONG"]);
                    var soLuong1 = ObjectAndString.ObjectToDecimal(row["SO_LUONG1"]);
                    var tienNt0 = ObjectAndString.ObjectToDecimal(row["TIEN_NT0"]);
                    var tien0 = ObjectAndString.ObjectToDecimal(row["TIEN0"]);

                    var tienNt = ObjectAndString.ObjectToDecimal(row["TIEN_NT"]);
                    var tien = ObjectAndString.ObjectToDecimal(row["TIEN"]);
                    var giaNt = ObjectAndString.ObjectToDecimal(row["GIA_NT"]);
                    var gia = ObjectAndString.ObjectToDecimal(row["GIA"]);

                    var dataGia = Invoice.GetGiaMua("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), maVatTu, dvt1, txtMaKh.Text, txtMaGia.Text);

                    var giaNt01 = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT0"]);
                    row["GIA_NT01"] = giaNt01;
                    //_soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                    tienNt0 = V6BusinessHelper.Vround((soLuong1 * giaNt01), M_ROUND_NT);
                    tien0 = V6BusinessHelper.Vround((tienNt0 * txtTyGia.Value), M_ROUND);

                    row["tien_Nt0"] = tienNt0;
                    row["tien0"] = tien0;

                    //_tien2.Value = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        row["tien0"] = tienNt0;
                    }

                    //TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
                    var ck_nt = V6BusinessHelper.Vround(tienNt0 * pt_cki / 100, M_ROUND_NT);
                    row["ck_nt"] = ck_nt;
                    row["ck"] = V6BusinessHelper.Vround(ck_nt * txtTyGia.Value, M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        row["ck"] = row["ck_nt"];
                    }
                    //End TinhChietKhauChiTiet

                    //TinhGiaNt0();
                    row["Gia01"] = V6BusinessHelper.Vround((giaNt01 * txtTyGia.Value), M_ROUND_GIA_NT);
                    if (_maNt == _mMaNt0)
                    {
                        row["Gia01"] = row["Gia_nt01"];
                    }

                    if (soLuong != 0)
                    {
                        row["gia_nt0"] = V6BusinessHelper.Vround((tienNt0 / soLuong), M_ROUND_GIA_NT);
                        row["gia0"] = V6BusinessHelper.Vround((tien0 / soLuong), M_ROUND_GIA);

                        if (_maNt == _mMaNt0)
                        {
                            row["gia0"] = row["gia_nt01"];
                            row["gia_nt0"] = row["gia_nt01"];
                        }
                    }
                    //End TinhGiaNt2

                    //====================

                    if (dvt.ToUpper().Trim() == dvt1.ToUpper().Trim())
                    {
                        row["GIA_NT0"] = row["GIA_NT01"];
                    }
                    else
                    {
                        if (soLuong != 0)
                        {
                            row["GIA_NT0"] = tienNt0 / soLuong;
                        }
                    }

                    // TinhTien va Gia
                    tienNt = tienNt0 + ObjectAndString.ObjectToDecimal(row["CP_NT"]) + ObjectAndString.ObjectToDecimal(row["CK_NT"]) +
                        ObjectAndString.ObjectToDecimal(row["GG_NT"]) + ObjectAndString.ObjectToDecimal(row["TIEN_VC_NT"]);
                    tien = tien0 + ObjectAndString.ObjectToDecimal(row["CP"]) + ObjectAndString.ObjectToDecimal(row["CK"]) +
                        ObjectAndString.ObjectToDecimal(row["GG"]) + ObjectAndString.ObjectToDecimal(row["TIEN_VC"]);

                    if (soLuong != 0)
                    {
                        giaNt = V6BusinessHelper.Vround((tienNt / soLuong), M_ROUND_GIA_NT);
                        gia = V6BusinessHelper.Vround((tien / soLuong), M_ROUND_GIA);
                    }

                    if (_maNt == _mMaNt0)
                    {
                        tien = tienNt;
                        gia = giaNt;
                    }

                    row["GIA_NT"] = giaNt;
                    row["GIA"] = gia;
                    row["TIEN_NT"] = tienNt;
                    row["TIEN"] = tien;

                }

                dataGridView1.DataSource = AD;

                TinhTongThanhToan("ApGiaMua");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void inPhieuHachToanMenu_Click(object sender, EventArgs e)
        {
            InPhieuHachToan(Invoice, _sttRec, TongThanhToan, TongThanhToanNT);
        }

        private void txtLoaiNX_PH_V6LostFocus(object sender)
        {
            try
            {
                if (txtLoaiNX_PH.Text != string.Empty)
                {
                    V6ControlFormHelper.UpdateDKlist(AD, "MA_LNX_I", txtLoaiNX_PH.Text);
                    if (_Ma_lnx_i != null) _Ma_lnx_i.Text = txtLoaiNX_PH.Text;
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, this, "AM", txtLoaiNX_PH);
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
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                //var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                string filter1 = _maVt.InitFilter;
                var setting = ObjectAndString.SplitString(V6Options.GetValueNull("M_FILTER_MAKH2MAVT"));
                if (setting.Contains(Invoice.Mact))
                    
                {
                    string newFilter = Invoice.GetMaVtFilterByMaKH(txtMaKh.Text, txtMaDVCS.Text);
                    if (string.IsNullOrEmpty(filter1))
                    {
                        filter1 = newFilter;
                    }
                    else if (!string.IsNullOrEmpty(newFilter) && !_maVt.InitFilter.Contains(newFilter))
                    {
                        filter1 = string.Format("({0}) and ({1})", filter1, newFilter);
                    }
                };

                var form = new AlvtSelectorForm(Invoice, filter1);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    chonAlvt_AcceptData((DataTable)form.dataGridView2.DataSource, detail1, _maVt, txtTyGia.Value, dataGridView1);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void inKhacMenu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
        }

        private void xuLyKhac1Menu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.XULYKHAC1);
        }

        private void xuLyKhac2Menu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.XULYKHAC2);
        }

    }
}
