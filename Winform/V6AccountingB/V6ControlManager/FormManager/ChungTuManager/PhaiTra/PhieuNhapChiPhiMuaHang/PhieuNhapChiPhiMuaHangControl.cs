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
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang.ChonPhieuNhap;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang
{
    public partial class PhieuNhapChiPhiMuaHangControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice73 Invoice = new V6Invoice73();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuNhapChiPhiMuaHangControl()
        {
            InitializeComponent();
            MyInit();
        }
        public PhieuNhapChiPhiMuaHangControl(string itemId)
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
        public PhieuNhapChiPhiMuaHangControl(string maCt, string itemId, string sttRec)
            : base(new V6Invoice73(), itemId)
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
            txtTkThueCo.SetInitFilter("Loai_tk = 1");
            txtTkThueNo.SetInitFilter("Loai_tk = 1");
            txtLoaiNX_PH.SetInitFilter("LOAI = 'N'");
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

            SetGridViewFlag(dataGridView1, dataGridView2);
            
            cboKieuPost.SelectedIndex = 0;
            if (!V6Setting.IsVietnamese)
            {
                cboLoai_pb.Items.AddRange(new object[] {
                "0 - Enter directly",
                "1 - Allocated by value",
                "2 - Allocated by quantity"});
            }
            cboLoai_pb.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            //detail1.AddContexMenu(menuDetail1);
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
        public V6ColorTextBox _dvt;
        public V6ColorTextBox _detail1Focus;
        public V6QRTextBox _qr_code0;
        public V6VvarTextBox _maVt, _Ma_lnx_i, _dvt1, _maKho, _maKhoI, _tkVt, _maLo;
        public V6NumberTextBox _soLuong1, _soLuong, _he_so1T, _he_so1M, _gia_nt, _gia_nt01, _tien0, _tien_nt0,
            _ck, _ckNt, _gia0, _gia01, _gia, _gia_Nt0, _sl_td1;
        public V6NumberTextBox _ton13, _ton13Qd, _tienNt, _tien, _mau_bc22;
        public V6DateTimeColor _hanSd;
        public V6ColorTextBox _so_ct022,_so_seri022,_ten_kh22,
            _dia_chi22,_ma_so_thue22;
        public V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22, _ma_thue22;
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
                    case "MA_VT":
                        _maVt = control as V6VvarTextBox;
                        if (_detail1Focus == null)
                        {
                            _detail1Focus = _maVt;
                        }
                        _maVt.Upper();
                        _maVt.BrotherFields = "ten_vt,ten_vt2,dvt,ma_qg";
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
                    //_maKhoI.Tag = "hide";
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
                        _soLuong1.V6LostFocus += SoLuong1_V6LostFocus;
                        _soLuong1.V6LostFocusNoChange += delegate
                        {
                            if (!detail1.IsAddOrEdit) return;
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
                    case "GIA_NT":
                        _gia_nt = control as V6NumberTextBox;
                        if (_gia_nt != null)
                        {
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
                        _gia_nt01 = control as V6NumberTextBox;
                        if (_gia_nt01 != null)
                        {
                            _gia_nt01.V6LostFocus += delegate
                            {
                                TinhTienNt0(_gia_nt01);
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
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia_nt01.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia_nt01.ReadOnlyTag();
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
                        _tienNt = control as V6NumberTextBox;
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
                        _tien_nt0 = control as V6NumberTextBox;
                        if (_tien_nt0 != null)
                        {
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
                                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * _sl_td1.Value, M_ROUND);
                                else _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);
                                TinhGiaNt();
                                if (_gia_nt01.Value == 0 && _soLuong1.Value != 0) TinhGiaNt01();
                                TinhTienVon_GiaVon();
                            };
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien_nt0.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien_nt0.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN0":
                        _tien0 = control as V6NumberTextBox;
                        if (_tien0 != null)
                        {
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
                        _ck = control as V6NumberTextBox;
                        break;
                    
                    case "CK_NT":
                        _ckNt = control as V6NumberTextBox;
                        break;
                    
                    case "MA_LO":
                        _maLo = control as V6VvarTextBox;
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
                        _hanSd = control as V6DateTimeColor;
                        if (_hanSd != null)
                        {
                            _hanSd.DisableTag();
                        }
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
                            _mau_bc22.MaxNumLength = 1;
                            _mau_bc22.MaxLength = 1;
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
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia_Nt022.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia_Nt022.ReadOnlyTag();
                            }
                        }
                        break;
                    case "T_TIEN":
                        _t_tien22 = control as V6NumberTextBox;
                        if (_t_tien22 != null)
                        {
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
                                TinhTienThue22();
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
                    case "MA_THUE":
                        _ma_thue22 = control as V6VvarTextBox;
                        _ma_thue22.CheckNotEmpty = true;
                        _ma_thue22.CheckOnLeave = true;
                        if (_ma_thue22 != null)
                        {
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
                                TinhTienThue22();
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
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    detail1.OnXoaClick();
                }
                //if (detail1.MODE == V6Mode.View && detail1.btnXoa.Enabled && detail1.btnXoa.Visible)
                //    detail1.btnXoa.PerformClick();
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


        void SoLuong1_V6LostFocus(object sender)
        {
            //TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, );
            //TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, );
            //TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, );
            _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
            TinhTienNt0();
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
            // GetGia();
            GetGiaVonCoDinh(_maVt, _sl_td1, _gia_nt);
            // TinhTienNt0();

            TinhTienNt0(_maVt);
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
                }
                else
                {
                    SetADSelectMoreControlValue(Invoice, data);
                    _tkVt.Text = (data["TK_VT"] ?? "").ToString().Trim();    
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

        public void TinhTienNt0(Control actionControl = null)
        {
            try
            {
                _tien_nt0.Value = V6BusinessHelper.Vround((_soLuong1.Value * _gia_nt01.Value), M_ROUND_NT);
                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * _sl_td1.Value, M_ROUND);
                else _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien0.Value = _tien_nt0.Value;
                }
                _tienNt.Value = _tien_nt0.Value;
                _tien.Value = _tien0.Value;


                TinhGiaNt();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                _tien0.Value = V6BusinessHelper.Vround(_tien_nt0.Value * txtTyGia.Value, M_ROUND);
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
                    dataGridView3ChiPhi.ReadOnly = true;
                }
                else
                {
                    detail2.MODE = V6Mode.View;
                    XuLyKhoaThongTinKhachHang();
                    SetGridViewChiPhiEditAble(TxtLoai_pb.Text, chkSuaTien.Checked, dataGridView3ChiPhi);

                    txtTyGia.Enabled = _maNt != _mMaNt0;
                    _tien_nt0.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;

                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;

                    cboLoai_pb_SelectedIndexChanged(cboLoai_pb, EventArgs.Empty);
                }

                //Cac truong hop khac
                if (!readOnly)
                {
                    

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
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView3ChiPhi, Invoice.GRDS_AD, Invoice.GRDF_AD,
                       V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
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
                        V6VvarTextBox vvar_ma_vt = new V6VvarTextBox() { VVar = "MA_VT", Text = ma_vt };
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
        }
      

        public void TinhThue()
        {   
            if (AD2 != null && AD2.Rows.Count > 0)
            {
                txtThueSuat.Value = ObjectAndString.ObjectToDecimal(AD2.AsEnumerable().Max(row => row["THUE_SUAT"]));
                txtTkThueNo.ChangeText(ObjectAndString.ObjectToString(AD2.AsEnumerable().Max(row => row["TK_THUE_NO"])));
            }
            //Tính tiền thuế theo thuế suất
            var thue_suat = txtThueSuat.Value;
            var t_thue_nt = 0m;
            var t_thue = 0m;
            var ty_gia = txtTyGia.Value;
            var t_tien_nt2 = TxtT_cp_nt.Value;
            var t_tien2 = TxtT_cp.Value;
            

            if (chkSuaTienThue.Checked)//Tiền thuế gõ tự do
            {
                t_thue_nt = txtTongThueNt.Value;
                t_thue = V6BusinessHelper.Vround(t_thue_nt*thue_suat/100, M_ROUND);
            }
            else
            {
                t_thue_nt = TinhTong(AD2, "T_THUE_NT");
                t_thue = TinhTong(AD2, "T_THUE");
                //tiền thuế = (tiền hàng - tiền giảm - chiết khấu) * thuế suất
                //t_thue_nt = (t_tien_nt2 )*thue_suat/100;
                //t_thue_nt = V6BusinessHelper.Vround(t_thue_nt, M_ROUND_NT);
            }
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
                    var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP_NT"]);    
                    var thue_nt = V6BusinessHelper.Vround((tien_nt2 / t_tien_nt2 )* t_thue_nt, M_ROUND);
                    t_thue_nt_check = t_thue_nt_check + thue_nt;

                    var thue = V6BusinessHelper.Vround(thue_nt * ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thue = thue_nt;
                    if (thue_nt != 0 && index == -1)
                        index = i;

                    AD.Rows[i]["Thue_nt"] = thue_nt;
                    AD.Rows[i]["Thue"] = thue;
                }
                if (t_tien2 != 0)
                {
                    var tien2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["CP"]);
                    var thue = V6BusinessHelper.Vround((tien2 / t_tien2) * t_thue, M_ROUND);
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


        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                if (NotAddEdit) return;
            
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();

                TinhTongValues();
                
                TinhThue();
                if (string.IsNullOrEmpty(_mMaNt0)) return;
                
                var t_tien_nt2 = TxtT_cp_nt.Value;
                var t_thue_nt = txtTongThueNt.Value;
                
                var t_tt_nt = t_tien_nt2  + t_thue_nt;
                txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

                var t_tt = TxtT_cp.Value + txtTongThue.Value;
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                //var tygia = txtTyGia.Value;
                //TxtT_cp.Value = V6BusinessHelper.Vround(t_tien_nt2*tygia, M_ROUND);
                
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
                TxtMa_gd.Text = "8";
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
                    SetDetailControlVisible(detailControlList1, true, "GIA", "GIA01", "GIA2", "GIA21", "TIEN", "TIEN0", "TIEN2", "THUE", "CK", "GG", "TIEN_VC");
                    panelVND.Visible = true;
                    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains("TIEN0"))
                    {
                        panelNT.Visible = false;
                        panelVND.Visible = false;
                        lblDocSoTien.Visible = false;
                        TxtT_cp_nt_ao.Visible = false;
                    }
                    TxtT_cp_ao.Visible = true;
                    TxtT_cp.Visible = true;

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA01");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    
                    TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    TxtT_cp.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    
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
                    SetDetailControlVisible(detailControlList1, false, "GIA", "GIA01", "GIA2", "GIA21", "TIEN", "TIEN0", "TIEN2", "THUE", "CK", "GG", "TIEN_VC");
                    panelVND.Visible = false;
                    if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains("TIEN0"))
                    {
                        panelNT.Visible = false;
                        panelVND.Visible = false;
                        lblDocSoTien.Visible = false;
                        TxtT_cp_nt_ao.Visible = false;
                    }
                    TxtT_cp_ao.Visible = false;
                    TxtT_cp.Visible = false;

                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di
                    var dataGridViewColumn = dataGridView1.Columns["GIA01"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN0"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    
                    TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_cp_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN;

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
                _tienNt.DecimalPlaces = decimalPlaces;
                _ckNt.DecimalPlaces = decimalPlaces;

                _t_tien_nt22.DecimalPlaces = decimalPlaces;
                _t_thue_nt22.DecimalPlaces = decimalPlaces;
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

                if (_maNt != _mMaNt0)
                {
                    column = dataGridView3ChiPhi.Columns["CP_NT"];
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = "N" + V6Options.M_IP_TIEN_NT;
                    }
                    column = dataGridView3ChiPhi.Columns["CP"];
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
                    column = dataGridView3ChiPhi.Columns["CP"];
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue " + _sttRec, ex);
            }
            TinhTongThanhToan("XuLyThayDoiMaThue");
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
                //Tuanmh 20/02/2016

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
            }
        }

        private void ReadyForAdd()
        {
            try
            {
                readyDataAD = dataGridView1.GetData(_sttRec);
                readyDataAD2 = dataGridView2.GetData(_sttRec);
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

                if (//detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||
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

                        //detail3.MODE = V6Mode.View;
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
                                //SetDataGridView3ChiPhiReadOnly();
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
                this.ShowErrorException(GetType() + ".Sua " + _sttRec, ex);
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

        private TimPhieuNhapChiPhiMuaHangForm SearchForm
        {
            get
            {
                if (_timForm == null || _timForm.IsDisposed)
                    _timForm = new TimPhieuNhapChiPhiMuaHangForm(Invoice, V6Mode.View);
                return _timForm;
            }
        }
        private TimPhieuNhapChiPhiMuaHangForm _timForm;
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
                //detail3.SetData(null);

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
            cboLoai_pb.SelectedIndex = -1;
        }

        private void SetFormDefaultValues()
        {
            chkLoaiChietKhau.Checked = true;//loai ck chung
            cboKieuPost.SelectedIndex = 1;
            TxtT_cp_nt_ao.Value = 0;
            TxtT_cp_ao.Value = 0;
            TxtLoai_pb.Text = "0";
            switch (Mode)
            {
                case V6Mode.Init:
                case V6Mode.View:
                    
                    break;
                case V6Mode.Add:
                case V6Mode.Edit:
                    
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
                    Detail1FocusReset();
                    dataGridView1.Lock();
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
                    _t_tien22.Value = TxtT_cp.Value;
                    _t_tien_nt22.Value = TxtT_cp_nt.Value;
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
                {// Check cộng số lượng // Bộ check gồm MA_VT DVT1 MA_KHO

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
                
            }
            if (!chkLoaiChietKhau.Checked)
            {
              
            }
            else
            {
                
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

        

        private void chkSuaTienThue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;

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
            BasePrint(Invoice, _sttRec, V6PrintMode.DoNoThing, TongThanhToan, TongThanhToanNT, false);
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void txtPtCk_V6LostFocus(object sender)
        {
            //if (chkLoaiChietKhau.Checked && txtPtCk.Value == 0)
            //{
            //    chkSuaPtck.Checked = false;
            //}
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

            if (!dataGridView3ChiPhi.ReadOnly)
            {
                if (_maNt != _mMaNt0)
                {

                    dataGridView3ChiPhi.SetEditColumn(chkSuaTien.Checked ? "CP,CP_NT".Split(',') : "CP_NT".Split(','));
                }
                else
                {
                    dataGridView3ChiPhi.SetEditColumn("CP_NT".Split(','));
                }

            }
        }

        private void hoaDonDetail2_Load(object sender, EventArgs e)
        {

        }

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

            pb_changed = false;
        }

        public void TinhPhanBoChiPhi(string loai_pb)
        {
            try
            {
                if (loai_pb.Length > 1) loai_pb = loai_pb.Left(1);
                if (loai_pb != "1" && loai_pb != "2") return;
                var t_tien_nt0 = TinhTong(AD, "TIEN_NT0");
                
                var t_he_so = loai_pb == "1" ? t_tien_nt0 : txtTongSoLuong1.Value;
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
            TinhTongThanhToan("TxtT_cp_nt_ao_V6LostFocus");
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
                if (txtManx.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaNX.Text);
                    txtManx.Focus();
                    return false;
                }
                if (txtManx.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage(V6Text.Text("TKNOTCT"), "TKNOTCT");
                    txtManx.Focus();
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

                if (!CheckPhanBo(dataGridView3ChiPhi, TxtT_cp_nt.Value))
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKPHANBO"), "CHECKPHANBO");
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
                this.WriteExLog(GetType() + ".ValidateData_Detail " + _sttRec, ex);
            }
            return true;
        }

        public override bool ValidateData_Detail2(IDictionary<string, object>  data)
        {
            try
            {
                //if (_tkDt.Int_Data("Loai_tk") == 0)
                //{
                //    this.ShowWarningMessage(V6Text.Text("TKNOTCT"), "TKNOTCT");
                //    return false;
                //}
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
            else
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
            e.Cancel = true;
            e.ThrowException = false;
        }

        private void btnInfos_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ProcessUserDefineInfo(Invoice.Mact, tabKhac, this, _sttRec);
        }

        private void btnChonPN_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            XuLyChonPhieuNhap(false, shift);
        }
        private void btnChonPN2_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            XuLyChonPhieuNhap(true, shift);
        }

        private void XuLyChonPhieuNhap(bool multiselect, bool add)
        {
            try
            {
                chon_accept_flag_add = add;
                CPN_PhieuNhapChiPhiMuaHangForm chon = new CPN_PhieuNhapChiPhiMuaHangForm(this, multiselect);
                _chon_px = "PN";
                chon.AcceptSelectEvent += chonpx_AcceptSelectEvent;
                chon.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chonpx_AcceptSelectEvent(List<IDictionary<string, object>> selectedDataList, ChonEventArgs e)
        {
            try
            {
                txtLoaiCt.Text = e.Loai_ct;
                detail1.MODE = V6Mode.View;
                dataGridView1.UnLock();
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }

                if (!e.multiSelect)
                {
                    txtSO_PN.Text = e.extraData["SO_CT"].ToString().Trim();
                    dateNgayPN.Value = ObjectAndString.ObjectToDate(e.extraData["NGAY_CT"]);
                }

                int addCount = 0, failCount = 0;
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

                    if (XuLyThemDetail(data))
                    {
                        addCount++;
                        All_Objects["data"] = data;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                    else
                    {
                        failCount++;
                    }
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
                if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                {
                    SaveTemp("MAXROWSAVETEMP");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void txtMaKh_TextChanged(object sender, EventArgs e)
        {
            TxtMa_kh_i_ao.Text = txtMaKh.Text;
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
                if (table.Count > 0)
                {
                    bool flag_add = chon_accept_flag_add;
                    chon_accept_flag_add = false;
                    if (!flag_add)
                    {
                        AD.Rows.Clear();
                    }

                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        detail1.MODE = V6Mode.View;
                    }
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

        private void txtManx_TextChanged(object sender, EventArgs e)
        {
            TxtTk_i_ao.Text = txtManx.Text;
        }

        
        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabThue)
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

        private decimal _con_lai;
        private void dataGridView3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                // Tính số cần nhập dựa theo TxtT_cp_nt_ao
                decimal num;
                if (dataGridView3ChiPhi.EditingColumn.DataPropertyName.ToUpper() == "CP_NT")
                {
                    num = TxtT_cp_nt_ao.Value;
                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != dataGridView3ChiPhi.EditingRow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells["CP_NT"].Value);
                        }
                    }
                }
                else
                {
                    num = TxtT_cp_ao.Value;
                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != dataGridView3ChiPhi.EditingRow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells["CP"].Value);
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
                this.WriteExLog(GetType() + ".dataGridView3_CellBeginEdit " + _sttRec, ex);
            }
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Cập nhập cột cp (hoặc cp_nt)
                if (dataGridView3ChiPhi.EditingColumn.DataPropertyName.ToUpper() == "CP_NT")
                {
                    var cp_nt = ObjectAndString.ObjectToDecimal(dataGridView3ChiPhi.EditingCell.Value);
                    if (cp_nt > _con_lai)
                    {
                        ShowParentMessage(V6Text.CheckData + ": " + _con_lai);
                        cp_nt = _con_lai;
                        dataGridView3ChiPhi.EditingCell.Value = cp_nt;
                    }
                    var cp = V6BusinessHelper.Vround(cp_nt * txtTyGia.Value,M_ROUND);
                    dataGridView3ChiPhi.EditingRow.Cells["CP"].Value = cp;
                }
                else
                {
                    var cp = ObjectAndString.ObjectToDecimal(dataGridView3ChiPhi.EditingCell.Value);
                    if (cp > _con_lai)
                    {
                        ShowParentMessage(V6Text.CheckData + ": " + _con_lai);
                        cp = _con_lai;
                        dataGridView3ChiPhi.EditingCell.Value = cp;
                    }
                    // Tuanmh 16/11/2018 Không tính lại CP_NT
                    //if (cp == 0)
                    //{
                    //    dataGridView3ChiPhi.EditingRow.Cells["CP_NT"].Value = cp;
                    //}
                    //else
                    //{
                    //    var cp_nt = cp / txtTyGia.Value;
                    //    dataGridView3ChiPhi.EditingRow.Cells["CP_NT"].Value = cp_nt;
                    //}
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3_CellEndEdit " + _sttRec, ex);
            }
        }

        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                return;
                

                // Thử nghiệm MyTextBoxCell
                //var column_cp_nt = dataGridView1.Columns["CP_NT"];
                //if (column_cp_nt != null)
                //    column_cp_nt.CellTemplate = new MyNumberTextBoxCell();
                //var column_cp = dataGridView1.Columns["CP"];
                //if (column_cp != null)
                //    column_cp.CellTemplate = new MyNumberTextBoxCell();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3_CellEnter " + _sttRec, ex);
            }
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //V6ControlFormHelper.ApplyNumberTextBox(e.Control);
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView2, dataGridView3ChiPhi);
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

                decimal num;
                if (ccolumn.DataPropertyName.ToUpper() == "CP_NT")
                {
                    num = TxtT_cp_nt_ao.Value;
                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != crow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells["CP_NT"].Value);
                        }
                    }

                    ccell.Value = num;
                    var cp = V6BusinessHelper.Vround(num * txtTyGia.Value,M_ROUND);
                    crow.Cells["CP"].Value = cp;
                }
                else if (ccolumn.DataPropertyName.ToUpper() == "CP")
                {
                    num = TxtT_cp_ao.Value;
                    foreach (DataGridViewRow row in dataGridView3ChiPhi.Rows)
                    {
                        if (row != crow)
                        {
                            num -= ObjectAndString.ObjectToDecimal(row.Cells["CP"].Value);
                        }
                    }

                    ccell.Value = num;
                    //if (num == 0)
                    //{
                    //    crow.Cells["CP_NT"].Value = num;
                    //}
                    //else
                    //{
                    //    var cp_nt = num / txtTyGia.Value;
                    //    crow.Cells["CP_NT"].Value = cp_nt;
                    //}
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView3ChiPhi_CurrentCellChanged " + _sttRec, ex);
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

        private void menuChucNang_Paint(object sender, PaintEventArgs e)
        {
            FixMenuChucNangItemShiftText();
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
                }

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
                chonExcel.CheckFields = "MA_VT,MA_KHO_I";
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
                if (table.Count > 0)
                {
                    bool flag_add = chon_accept_flag_add;
                    chon_accept_flag_add = false;
                    if (!flag_add)
                    {
                        AD.Rows.Clear();
                    }

                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        detail1.MODE = V6Mode.View;
                    }
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

        private void chonTuExcelMenu_Click(object sender, EventArgs e)
        {
            bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
            ChucNang_ChonTuExcel(shift);
        }

        private void hoaDonDetail2_AddHandle()
        {

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
