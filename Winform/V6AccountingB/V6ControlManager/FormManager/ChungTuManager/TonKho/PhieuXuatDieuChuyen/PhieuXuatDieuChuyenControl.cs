﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen.ChonDeNghiXuat;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen.Loc;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen
{
    public partial class PhieuXuatDieuChuyenControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice85 Invoice = new V6Invoice85();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuXuatDieuChuyenControl()
        {
            InitializeComponent();
            MyInit();
        }
        public PhieuXuatDieuChuyenControl(string itemId)
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
        public PhieuXuatDieuChuyenControl(string maCt, string itemId, string sttRec)
            : base(new V6Invoice85(), itemId)
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
            txtMa_sonb.Upper();
            txtLoaiNX_PH.SetInitFilter("LOAI = 'X'");
            txtLoaiNX_PHN.SetInitFilter("LOAI = 'N'");
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

            txtMaKhoN.SetInitFilter("");
            
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
            SetGridViewFlag(dataGridView1);

            cboKieuPost.SelectedIndex = 0;

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            detail1.AddContexMenu(menuDetail1);
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
        public V6CheckTextBox _xuat_dd;
        public V6ColorTextBox _detail1Focus;
        public V6QRTextBox _qr_code0;
        public V6VvarTextBox _maVt, _Ma_lnx_i, _Ma_lnxn, _dvt1, _maKho2, _Ma_nx_i, _tkVt, _maLo, _maViTri, _maViTri2, _maViTriN;
        public V6ColorTextBox _soKhung, _soMay;
        public V6NumberTextBox _soLuong1, _soLuong, _he_so1T, _he_so1M, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_td1;
        public V6NumberTextBox _sl_101, _sl_102, _sl_103, _sl_104, _sl_01, _sl_02, _sl_03, _sl_04;
        public V6NumberTextBox _ton13, _ton13Qd, _gia, _gia_nt, _tien, _tien_nt, _gia1, _gia_nt1;
        public V6DateTimeColor _hanSd;

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
                        _maVt = (V6VvarTextBox) control;
                        if (_detail1Focus == null)
                        {
                            _detail1Focus = _maVt;
                        }
                        _maVt.Upper();
                        _maVt.BrotherFields = "ten_vt,ten_vt2,dvt,ma_qg";
                        _maVt.V6LostFocus += MaVatTu_V6LostFocus;

                        _maVt.V6LostFocusNoChange += delegate
                        {
                            if (_maVt.LO_YN)
                            {
                                if (_maVt.VITRI_YN)
                                {
                                    _maLo.Enabled = false;
                                }
                                else
                                {
                                    _maLo.Enabled = true;
                                }
                            }
                            else
                            {
                                _maLo.Enabled = false;
                            }
                            GetTon13();
                            GetLoDate13();
                        };
                        break;
                    case "MA_NX_I":
                        _Ma_nx_i = (V6VvarTextBox)control;
                        _Ma_nx_i.Upper();
                        _Ma_nx_i.FilterStart = true;
                        break;
                    case "MA_LNX_I":
                        _Ma_lnx_i = control as V6VvarTextBox;
                        if (_Ma_lnx_i != null)
                        {
                            _Ma_lnx_i.FilterStart = true;
                            _Ma_lnx_i.SetInitFilter("LOAI = 'X'");
                            _Ma_lnx_i.Upper();
                        }
                        break;
                    case "MA_LNXN":
                        _Ma_lnxn = control as V6VvarTextBox;
                        if (_Ma_lnxn != null)
                        {
                            _Ma_lnxn.FilterStart = true;
                            _Ma_lnxn.SetInitFilter("LOAI = 'N'");
                            _Ma_lnxn.Upper();
                        }
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
                    case "MA_KHO2":
                        _maKho2 = (V6VvarTextBox)control;
                        _maKho2.Upper();
                        _maKho2.V6LostFocus += delegate(object sender)
                        {
                            
                        };
                        break;
                    
                    case "TON13":
                        _ton13 = (V6NumberTextBox)control;
                        _ton13.Tag = "disable";
                        _ton13.StringValueChange += (sender, args) =>
                        {
                            //CheckSoLuong1();
                        };
                        break;
                    case "TON13QD":
                        _ton13Qd = control as V6NumberTextBox;
                        if (_ton13Qd.Tag == null || _ton13Qd.Tag.ToString() != "hide")
                        {
                            _ton13Qd.Tag = "disable";
                        }
                        break;
                    //_ton13.V6LostFocus += Ton13_V6LostFocus;
                    case "SO_LUONG1":
                        _soLuong1 = control as V6NumberTextBox;
                        if (_soLuong1 != null)
                        {
                            _soLuong1.V6LostFocus += delegate
                            {
                                CheckSoLuong1(_soLuong1);
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
                                    if (_gia_nt1.Value * _soLuong1.Value == 0)
                                    {
                                        _tien_nt.Enabled = true;
                                        _tien_nt.ReadOnly = false;
                                    }
                                    else
                                    {
                                        _tien_nt.Enabled = false;
                                        _tien_nt.ReadOnly = true;
                                    }
                                }
                            };
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _soLuong1.ReadOnlyTag();
                            }
                        }
                        break;

                    case "SO_LUONG":
                        _soLuong = (V6NumberTextBox)control;
                        _soLuong.Tag = "hide";
                        break;

                    case "SL_101":
                        _sl_101 = (V6NumberTextBox)control;
                        _sl_101.V6LostFocus += delegate(object sender)
                        {
                            TinhSoLuongTheoSoLuong1(_sl_01, _sl_101, _he_so1T.Value, _he_so1M.Value);
                        };
                        break;

                    case "SL_102":
                        _sl_102 = (V6NumberTextBox)control;
                        _sl_102.V6LostFocus += delegate(object sender)
                        {
                            TinhSoLuongTheoSoLuong1(_sl_02, _sl_102, _he_so1T.Value, _he_so1M.Value);
                        };
                        break;

                    case "SL_103":
                        _sl_103 = (V6NumberTextBox)control;
                        _sl_103.V6LostFocus += delegate(object sender)
                        {
                            TinhSoLuongTheoSoLuong1(_sl_03, _sl_103, _he_so1T.Value, _he_so1M.Value);
                        };
                        break;

                    case "SL_104":
                        _sl_104 = (V6NumberTextBox)control;
                        _sl_104.V6LostFocus += delegate(object sender)
                        {
                            TinhSoLuongTheoSoLuong1(_sl_04, _sl_104, _he_so1T.Value, _he_so1M.Value);
                        };
                        break;

                    case "SL_01":
                        _sl_01 = (V6NumberTextBox)control;
                        break;

                    case "SL_02":
                        _sl_02 = (V6NumberTextBox)control;
                        break;

                    case "SL_03":
                        _sl_03 = (V6NumberTextBox)control;
                        break;

                    case "SL_04":
                        _sl_01 = (V6NumberTextBox)control;
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
                    case "SL_TD1":
                        _sl_td1 = control as V6NumberTextBox;
                        if (_sl_td1 != null)
                        {

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
                    //_tien2.V6LostFocus;
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
                        _tien_nt = control as V6NumberTextBox;
                        if (_tien_nt != null)
                        {
                            _tien_nt.Enabled = chkSuaTien.Checked;
                            if (chkSuaTien.Checked)
                            {
                                _tien_nt.Tag = null;
                            }
                            else
                            {
                                _tien_nt.Tag = "disable";
                            }

                            _tien_nt.V6LostFocus += delegate
                            {
                                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * _sl_td1.Value, M_ROUND);
                                else _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * txtTyGia.Value, M_ROUND);
                                if (_maNt == _mMaNt0)
                                {
                                    _tien.Value = _tien_nt.Value;
                                }
                                if (_gia_nt1.Value == 0 && _soLuong1.Value != 0) TinhGiaNt1();
                                //TinhGiaVon();
                                TinhTienVon_GiaVon();
                            };
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien_nt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _tien_nt.ReadOnlyTag();
                            }
                        }
                        break;
                    
                    case "GIA_NT":
                        _gia_nt = control as V6NumberTextBox;
                        if (_gia_nt != null)
                        {
                            _gia_nt.V6LostFocus += delegate
                            {
                                TinhGia_Theo_GiaNt();
                                TinhTienVon(_gia_nt);
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

                    case "GIA_NT1":
                        _gia_nt1 = control as V6NumberTextBox;
                        if (_gia_nt1 != null)
                        {
                            _gia_nt1.V6LostFocus += delegate
                            {
                                TinhTienVon1(_gia_nt1);
                                TinhGiaVon();
                            };
                            _gia_nt1.TextChanged += delegate
                            {
                                if (!detail1.IsAddOrEdit) return;

                                if (!chkSuaTien.Checked)
                                {
                                    if (_gia_nt1.Value * _soLuong1.Value == 0)
                                    {
                                        _tien_nt.Enabled = true;
                                        _tien_nt.ReadOnly = false;
                                    }
                                    else
                                    {
                                        _tien_nt.Enabled = false;
                                        _tien_nt.ReadOnly = true;
                                    }
                                }
                            };
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia_nt1.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia_nt1.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA1":
                        _gia1 = control as V6NumberTextBox;
                        if (_gia1 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia1.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _gia1.ReadOnlyTag();
                            }
                        }
                        break;

                    case "PX_GIA_DDI":
                        _xuat_dd = control as V6CheckTextBox;
                        if (_xuat_dd != null)
                        {
                            _xuat_dd.TextChanged += delegate
                            {
                                if (_xuat_dd.Text != "")
                                {
                                    _gia_nt1.Enabled = true;
                                    _gia_nt.Enabled = true;
                                    if (chkSuaTien.Checked)
                                        _tien_nt.Enabled = true;
                                    else _tien_nt.Enabled = false;
                                }
                                else
                                {
                                    _gia_nt1.Enabled = false;
                                    _gia_nt.Enabled = false;
                                    _tien_nt.Enabled = false;
                                    _tien.Enabled = false;
                                }
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _xuat_dd.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                            {
                                _xuat_dd.ReadOnlyTag();
                            }
                        }
                        break;
                    
                    case "MA_LO":
                        _maLo = (V6VvarTextBox)control;
                        _maLo.GotFocus += (s, e) =>
                        {
                            if (NotAddEdit || _maLo.ReadOnly) return;

                            _maLo.CheckNotEmpty = _maVt.LO_YN && txtMaKhoX.LO_YN;
                            _dataLoDate = Invoice.GetLoDate(_maVt.Text, txtMaKhoX.Text, _sttRec, dateNgayCT.Date);
                            var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                            var getFilter = GetFilterMaLo(_dataLoDate, _sttRec0, _maVt.Text, txtMaKhoX.Text);
                            if (getFilter != "") filter += " and " + getFilter;
                            _maLo.SetInitFilter(filter);
                        };
                        _maLo.Leave += (sender, args) =>
                        {
                            if (!_maLo.ReadOnly)
                            {
                                CheckMaLoTon(_maLo.HaveValueChanged);
                            }
                        };
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        _hanSd.Tag = "disable";
                        break;
                    case "MA_VITRI":
                        _maViTri = control as V6VvarTextBox;
                        if (_maViTri != null)
                        {
                            _maViTri.GotFocus += (s, e) =>
                            {
                                _maViTri.CheckNotEmpty = _maVt.VITRI_YN;
                                var filter = "Ma_kho='" + txtMaKhoX.Text.Trim() + "'";

                                //if (("," + V6Options.GetValue("M_LST_CT_DV") + ",").Contains(MaCt))
                                //{
                                //    _dataViTri = Invoice.GetViTri(_maVt.Text, _maKhoI.Text, _sttRec, dateNgayCT.Date);
                                //    var getFilter = GetFilterMaViTri(_dataViTri, _sttRec0, _maVt.Text, _maKhoI.Text);
                                //    if (getFilter != "") filter += " and " + getFilter;
                                //}

                                _maViTri.SetInitFilter(filter);
                            };

                            _maViTri.Leave += delegate
                            {
                                if (!detail1.IsAddOrEdit) return;
                                if (!_maViTri.ReadOnly)
                                {
                                    CheckMaViTri();
                                    CheckMaVitriTon();
                                }
                            };
                        }
                        break;
                    case "MA_VITRI2":
                        _maViTri2 = control as V6VvarTextBox;
                        if (_maViTri2 != null)
                        {
                            _maViTri2.GotFocus += (s, e) =>
                            {
                                _maViTri2.CheckNotEmpty = _maVt.VITRI_YN;
                                var filter = "Ma_kho='" + _maKho2.Text.Trim() + "'";

                                if (("," + V6Options.GetValue("M_LST_CT_DV") + ",").Contains(Invoice.Mact))
                                {
                                    var dataViTri = Invoice.GetViTri("", _maKho2.Text, _sttRec, dateNgayCT.Date);
                                    var getFilter = GetFilterMaViTriNhap2(dataViTri, _sttRec0, "", _maKho2.Text);
                                    if (getFilter != "")
                                    {
                                        filter += " and " + getFilter;
                                    }
                                }

                                _maViTri2.SetInitFilter(filter);
                            };

                            _maViTri2.V6LostFocus += sender =>
                            {
                                //CheckMaViTri();
                            };
                        }
                        break;
                    case "MA_VITRIN":
                        _maViTriN = control as V6VvarTextBox;
                        if (_maViTriN != null)
                        {
                            _maViTriN.GotFocus += (s, e) =>
                            {
                                _maViTriN.CheckNotEmpty = _maVt.VITRI_YN;
                                var filter = "Ma_kho='" + txtMaKhoN.Text.Trim() + "'";

                                if (("," + V6Options.GetValue("M_LST_CT_DV") + ",").Contains(Invoice.Mact))
                                {
                                    _dataViTri = Invoice.GetViTri("", txtMaKhoN.Text, _sttRec, dateNgayCT.Date);
                                    var getFilter = GetFilterMaViTriNhap(_dataViTri, _sttRec0,"", txtMaKhoN.Text);
                                    if (getFilter != "")
                                    {
                                        filter += " and " + getFilter;
                                    }

                                }

                                _maViTriN.SetInitFilter(filter);
                            };

                            _maViTriN.V6LostFocus += sender =>
                            {
                                //CheckMaViTri();
                            };
                        }
                        break;
                    case "SO_KHUNG":
                        if (control is V6VvarTextBox)
                        {
                            var soKhung_vvar = (V6VvarTextBox)control;
                            _soKhung = soKhung_vvar;
                            soKhung_vvar.Enter += (s, e) =>
                            {
                                soKhung_vvar.CheckNotEmpty = _maVt.SKSM_YN;
                                var dataSKSM = V6BusinessHelper.GetSKSM(_maVt.Text, txtMaKhoX.Text, _sttRec, dateNgayCT.Date);
                                var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                                var getFilter = GetFilterSKSM_PXDC(dataSKSM, _sttRec0, _maVt.Text, txtMaKhoX.Text, detail1);
                                if (getFilter != "") filter += " and " + getFilter;
                                soKhung_vvar.SetInitFilter(filter);

                                soKhung_vvar.ExistRowInTable(true);
                            };
                            soKhung_vvar.Leave += (sender, args) =>
                            {
                                if (!soKhung_vvar.ReadOnly)
                                {
                                    if (soKhung_vvar.Data != null)
                                    {
                                        _soMay.Text = soKhung_vvar.Data["SO_MAY"].ToString().Trim();

                                        if (_maVt.GIA_TON == 2 || _xuat_dd.Checked)
                                        {
                                            var ton_dau = ObjectAndString.ObjectToDecimal(soKhung_vvar.Data == null ? 0 : soKhung_vvar.Data["TON_DAU"]);
                                            if (ton_dau != 0)
                                            {
                                                _gia_nt.Value = ObjectAndString.ObjectToDecimal(soKhung_vvar.Data == null ? 0 : soKhung_vvar.Data["DU_DAU"]) / ton_dau;
                                                //_gia_nt.CallDoV6LostFocus();
                                                TinhTienVon(_soKhung);
                                            }
                                        }
                                    }
                                    //CheckSoKhungTon(soKhung_vvar.HaveValueChanged);
                                }
                            };

                        }
                        else if (control is V6LookupProc)
                        {
                            var soKhung_proc = (V6LookupProc)control;
                            _soKhung = soKhung_proc;
                            soKhung_proc.MA_CT = Invoice.Mact;
                            soKhung_proc.Enter += (s, e) =>
                            {
                                soKhung_proc.CheckNotEmpty = _maVt.SKSM_YN;
                                var dataSKSM = V6BusinessHelper.GetSKSM(_maVt.Text, txtMaKhoX.Text, _sttRec, dateNgayCT.Date);
                                var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                                var getFilter = GetFilterSKSM_PXDC(dataSKSM, _sttRec0, _maVt.Text, txtMaKhoX.Text, detail1);
                                if (getFilter != "") filter += " and " + getFilter;
                                soKhung_proc.SetInitFilter(filter);

                                soKhung_proc.ExistRowInTable();
                            };
                            soKhung_proc.V6LostFocus += delegate(object sender)
                            {
                                _soMay.Text = soKhung_proc.Data == null ? "" : soKhung_proc.Data["SO_MAY"].ToString().Trim();
                                if (_maVt.GIA_TON == 2 || _xuat_dd.Checked)
                                {
                                    var ton_dau = ObjectAndString.ObjectToDecimal(soKhung_proc.Data == null ? 0 : soKhung_proc.Data["TON_DAU"]);
                                    if (ton_dau != 0)
                                    {
                                        _gia_nt.Value = ObjectAndString.ObjectToDecimal(soKhung_proc.Data == null ? 0 : soKhung_proc.Data["DU_DAU"]) / ton_dau;
                                        //_gia_nt.CallDoV6LostFocus();
                                        TinhTienVon(_soKhung);
                                    }
                                }
                            };
                        }
                        else if (control is V6LookupData)
                        {
                            var soKhung_lookup = (V6LookupData)control;
                            _soKhung = soKhung_lookup;
                            soKhung_lookup.Enter += (s, e) =>
                            {
                                soKhung_lookup.CheckNotEmpty = _maVt.SKSM_YN;
                                var dataSKSM = V6BusinessHelper.GetSKSM(_maVt.Text, txtMaKhoX.Text, _sttRec, dateNgayCT.Date);
                                var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                                var getFilter = GetFilterSKSM_PXDC(dataSKSM, _sttRec0, _maVt.Text, txtMaKhoX.Text, detail1);
                                if (getFilter != "") filter += " and " + getFilter;
                                soKhung_lookup.SetInitFilter(filter);

                                soKhung_lookup.LoadAutoCompleteSource(dataSKSM);
                                soKhung_lookup.ExistRowInData(dataSKSM);
                            };
                            soKhung_lookup.Leave += (sender, args) =>
                            {
                                if (!soKhung_lookup.ReadOnly)
                                {
                                    if (soKhung_lookup.Data != null)
                                    {
                                        _soMay.Text = soKhung_lookup.Data["SO_MAY"].ToString().Trim();
                                        if (soKhung_lookup.Data.Table.Columns.Contains("MA_TD2"))
                                        {
                                            string ma_td2 = ObjectAndString.ObjectToString(soKhung_lookup.Data["MA_TD2"]);
                                            if (!string.IsNullOrEmpty(ma_td2))
                                            {
                                                var _ma_td2 = V6ControlFormHelper.GetControlByAccessibleName(detail1.panel2, "MA_TD2");
                                                if (_ma_td2 != null && _ma_td2.Visible)
                                                {
                                                    _ma_td2.Text = ma_td2;
                                                }
                                            }
                                        }


                                        if (_maVt.GIA_TON == 2 || _xuat_dd.Checked)
                                        {
                                            var ton_dau = ObjectAndString.ObjectToDecimal(soKhung_lookup.Data == null ? 0 : soKhung_lookup.Data["TON_DAU"]);
                                            if (ton_dau != 0)
                                            {
                                                _gia_nt.Value = ObjectAndString.ObjectToDecimal(soKhung_lookup.Data == null ? 0 : soKhung_lookup.Data["DU_DAU"]) / ton_dau;
                                                //_gia_nt.CallDoV6LostFocus();
                                                TinhTienVon(_soKhung);
                                            }
                                        }
                                    }
                                    //CheckSoKhungTon(soKhung_vvar.HaveValueChanged);
                                }
                            };
                        }

                        break;
                    case "SO_MAY":
                        _soMay = (V6ColorTextBox)control;
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
                                _qr_code0.QR_LostAuto = false;
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

                                        if (_qr_code0.Data != null && _qr_code0.Data.ContainsKey("MA_KHO"))
                                        {
                                            if (txtMaKhoX.Text.ToUpper() != _qr_code0.Data["MA_KHO"].ToString().ToUpper())
                                            {
                                                ShowMainMessage("Không đúng mã kho.");
                                                //detail1.OnMoiClick();
                                                detail1.SetData(detail1.CarryData);
                                                Detail1FocusReset();
                                            }
                                            else
                                            {
                                                // Gọi lostfocus cho các Neighbor
                                                var fields = ObjectAndString.SplitString(_qr_code0.NeighborFields);
                                                foreach (string field in fields)
                                                {
                                                    var c = detail1.GetControlByAccessibleName(field);
                                                    if (c is V6ColorTextBox)
                                                    {
                                                        ((V6ColorTextBox)c).CallLeave();
                                                        ((V6ColorTextBox)c).CallDoV6LostFocus();
                                                    }
                                                    else if (c is V6ColorMaskedTextBox)
                                                    {
                                                        ((V6ColorMaskedTextBox)c).CallLeave();
                                                        ((V6ColorMaskedTextBox)c).CallDoV6LostFocus();
                                                    }
                                                    else if (c is V6DateTimePicker)
                                                    {
                                                        ((V6DateTimePicker)c).CallLeave();
                                                        ((V6DateTimePicker)c).CallDoV6LostFocus();
                                                    }
                                                }

                                                _soLuong1.Value = 1;
                                                _soLuong1.CallDoV6LostFocus();
                                                if (!string.IsNullOrEmpty(Invoice.ExtraInfo_QrGot))
                                                {
                                                    var c = detail1.GetControlByAccessibleName(Invoice.ExtraInfo_QrGot);
                                                    if (c != null) c.Focus();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Gọi lostfocus cho các Neighbor
                                            var fields = ObjectAndString.SplitString(_qr_code0.NeighborFields);
                                            foreach (string field in fields)
                                            {
                                                var c = detail1.GetControlByAccessibleName(field);
                                                if (c is V6ColorTextBox)
                                                {
                                                    ((V6ColorTextBox)c).CallLeave();
                                                    ((V6ColorTextBox)c).CallDoV6LostFocus();
                                                }
                                                else if (c is V6ColorMaskedTextBox)
                                                {
                                                    ((V6ColorMaskedTextBox)c).CallLeave();
                                                    ((V6ColorMaskedTextBox)c).CallDoV6LostFocus();
                                                }
                                                else if (c is V6DateTimePicker)
                                                {
                                                    ((V6DateTimePicker)c).CallLeave();
                                                    ((V6DateTimePicker)c).CallDoV6LostFocus();
                                                }
                                            }

                                            _soLuong1.Value = 1;
                                            _soLuong1.CallDoV6LostFocus();
                                            if (!string.IsNullOrEmpty(Invoice.ExtraInfo_QrGot))
                                            {
                                                var c = detail1.GetControlByAccessibleName(Invoice.ExtraInfo_QrGot);
                                                if (c != null) c.Focus();
                                            }
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

        private string GetFilterMaViTri(DataTable dataViTri, string sttRec0, string maVt, string maKhoI)
        {
            try
            {
                var list_maViTri = "";
                if (maVt == "" || maKhoI == "") return list_maViTri;


                for (int i = dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;

                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {

                        list_maViTri += string.Format(" and Ma_vitri<>'{0}'", data_maViTri);

                    }
                }

                //if (list_maViTri.Length > 4)
                //{
                //    list_maViTri = list_maViTri.Substring(4);
                //    list_maViTri = "(" + list_maViTri + ")";
                //}

                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                {

                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                    string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                    string c_maKhoI = txtMaKhoN.Text.ToUpper();
                    string c_maViTri = row["Ma_vitriN"].ToString().Trim().ToUpper();

                    //Add 31-07-2016
                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;


                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                    {
                        if (maVt == c_maVt && maKhoI == c_maKhoI)
                        {
                            list_maViTri += string.Format(" and Ma_vitri<>'{0}'", c_maViTri);
                        }
                    }
                }

                if (list_maViTri.Length > 4)
                {
                    list_maViTri = list_maViTri.Substring(4);
                    list_maViTri = "(" + list_maViTri + ")";
                    return list_maViTri;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=0)";
        }

        private string GetFilterMaViTriNhap(DataTable dataViTri, string sttRec0, string maVt, string maKhoI)
        {
            try
            {
                var list_maViTri = "";
                if (maKhoI == "") return list_maViTri;


                for (int i = dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = dataViTri.Rows[i];
                   // string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;

                    //Neu dung maVt va maKhoI
                    if ( maKhoI == data_maKhoI)
                    {

                        list_maViTri += string.Format(" and Ma_vitri<>'{0}'", data_maViTri);

                    }
                }

                //if (list_maViTri.Length > 4)
                //{
                //    list_maViTri = list_maViTri.Substring(4);
                //    list_maViTri = "(" + list_maViTri + ")";
                //}

                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                {

                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                   // string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                    string c_maKhoI = txtMaKhoN.Text.ToUpper();
                    string c_maViTri = row["Ma_vitriN"].ToString().Trim().ToUpper();

                    //Add 31-07-2016
                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;


                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                    {
                        if (maKhoI == c_maKhoI)
                        {
                            list_maViTri += string.Format(" and Ma_vitri<>'{0}'", c_maViTri);
                        }
                    }
                }

                if (list_maViTri.Length > 4)
                {
                    list_maViTri = list_maViTri.Substring(4);
                    list_maViTri = "(" + list_maViTri + ")";
                    return list_maViTri;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=1)";
        }

        private string GetFilterMaViTriNhap2(DataTable dataViTri, string sttRec0, string maVt, string maKhoI)
        {
            try
            {
                var list_maViTri = "";
                if (maKhoI == "") return list_maViTri;


                for (int i = dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = dataViTri.Rows[i];
                    // string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;

                    //Neu dung maVt va maKhoI
                    if (maKhoI == data_maKhoI)
                    {

                        list_maViTri += string.Format(" and Ma_vitri<>'{0}'", data_maViTri);

                    }
                }

                //if (list_maViTri.Length > 4)
                //{
                //    list_maViTri = list_maViTri.Substring(4);
                //    list_maViTri = "(" + list_maViTri + ")";
                //}

                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                {

                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                    // string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                    string c_maKhoI = row["MA_KHO2"].ToString().Trim().ToUpper();
                    string c_maViTri = row["MA_VITRI2"].ToString().Trim().ToUpper();

                    //Add 31-07-2016
                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;


                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                    {
                        if (maKhoI == c_maKhoI)
                        {
                            list_maViTri += string.Format(" and Ma_vitri<>'{0}'", c_maViTri);
                        }
                    }
                }

                if (list_maViTri.Length > 4)
                {
                    list_maViTri = list_maViTri.Substring(4);
                    list_maViTri = "(" + list_maViTri + ")";
                    return list_maViTri;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=1)";
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
            GetTon13();
            GetLoDate13();
            CheckSoLuong1();
        }

        private void CheckMaLoTon(bool isChanged)
        {
            if (NotAddEdit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            if (!_maVt.LO_YN) return;
            //Fix Tuanmh 15/11/2017
            if (!txtMaKhoX.LO_YN) return;

            try
            {
                Invoice.GetAlLoTon(dateNgayCT.Date, _sttRec, _maVt.Text, txtMaKhoX.Text);
                FixAlLoTon(Invoice.AlLoTon, AD);

                var inputUpper = _maLo.Text.Trim().ToUpper();
                if (Invoice.AlLoTon != null && Invoice.AlLoTon.Rows.Count > 0)
                {
                    var check = false;
                    foreach (DataRow row in Invoice.AlLoTon.Rows)
                    {
                        if (row["Ma_lo"].ToString().Trim().ToUpper() == inputUpper)
                        {
                            check = true;
                        }

                        if (check)
                        {
                            //
                            _maLo.Tag = row;
                            XuLyKhiNhanMaLo(row.ToDataDictionary(), isChanged);
                            break;
                        }
                    }

                    if (!check)
                    {
                        var initFilter = GetAlLoTonInitFilter();
                        var f = new FilterView(Invoice.AlLoTon, "Ma_lo", "ALLOTON", _maLo, initFilter);
                        if (f.ViewData != null && f.ViewData.Count > 0)
                        {
                            var d = f.ShowDialog(this);

                            //xu ly data
                            if (d == DialogResult.OK)
                            {
                                if (_maLo.Tag is DataRow)
                                    XuLyKhiNhanMaLo(((DataRow)_maLo.Tag).ToDataDictionary(), isChanged);
                                else if (_maLo.Tag is DataGridViewRow)
                                    XuLyKhiNhanMaLo(((DataGridViewRow)_maLo.Tag).ToDataDictionary(), isChanged);
                            }
                            else
                            {
                                _maLo.Text = _maLo.GotFocusText;
                            }
                        }
                        else
                        {
                            ShowParentMessage("AlLoTon" + V6Text.NoData);
                        }
                    }

                    GetLoDate13();

                    if (_maLo.GotFocusText == _maLo.LostFocusText
                        && (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO)))
                    {
                        if (_soLuong1.Value > _ton13.Value)
                        {
                            _soLuong1.Value = _ton13.Value < 0 ? 0 : _ton13.Value;
                            _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                            //TinhTienNt2();
                            TinhTienVon1();
                        }
                    }
                }
                else
                {
                    ShowMainMessage("AlLoTon null");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void FixAlLoTon(DataTable alLoTon, DataTable ad)
        {
            try
            {

                string sttRec0 = _sttRec0;
                //string maVt = _maVt.Text.Trim().ToUpper();
                //string maKhoI = _maKhoI.Text.Trim().ToUpper();
                //string maLo = _maLo.Text.Trim().ToUpper();
                // string maLo = _maLo.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                    return;

                //if (maVt == "" || maKhoI == "" || maLo == "") return;

                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vi_tri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                List<DataRow> empty_rows = new List<DataRow>();

                for (int i = alLoTon.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = alLoTon.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    //string data_maVi_Tri = data_row["Ma_vi_tri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maLo, maVi_Tri
                    //- so luong
                    decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                    decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                    decimal new_soLuong = data_soLuong;
                    decimal new_soLuong_qd = data_soLuong_qd;

                    foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                    {
                        string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        string c_maKhoI = txtMaKhoX.Text.Trim().ToUpper();// row["Ma_kho__i"].ToString().Trim().ToUpper();
                        string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                        //string c_maVi_Tri = row["Ma_vi_tri"].ToString().Trim().ToUpper();

                        decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                        decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                        if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                        {
                            if (data_maVt == c_maVt && data_maKhoI == c_maKhoI && data_maLo == c_maLo)
                            {
                                new_soLuong -= c_soLuong;
                                new_soLuong_qd -= c_soLuong_qd;
                            }
                        }
                    }

                    if (new_soLuong > 0)
                    {
                        data_row["Ton_dau"] = new_soLuong;
                        data_row["Ton_dau_qd"] = new_soLuong_qd;
                    }
                    else
                    {
                        empty_rows.Add(data_row);

                    }
                }

                //remove all empty_rows
                foreach (DataRow row in empty_rows)
                {
                    alLoTon.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyKhiNhanMaLo(IDictionary<string, object> row, bool isChanged)
        {
            try
            {
                //_maLo.Text = row["MA_LO"].ToString().Trim();
                _hanSd.Value = ObjectAndString.ObjectToDate(row["HSD"]);
                _ton13.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU"]);
                if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU_QD"]);
                _maLo.Enabled = true;
                
                if (isChanged) CheckSoLuong1();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void CheckMaViTri()
        {
            if (txtMaKhoX.Text != "")
            {
                _maViTri.SetInitFilter("Ma_kho='" + txtMaKhoX.Text.Trim() + "'");
            }
            XuLyLayThongTinKhiChonMaVitri();
            GetTon13();
            if (_maVt.VITRI_YN)
            {
                if (_maVt.LO_YN && _maVt.DATE_YN)
                {
                    GetViTriLoDate13();
                }
                else
                {
                    GetViTri13();
                }
            }
            else
            {
                GetLoDate13();
            }

            CheckSoLuong1();
        }

        private void CheckMaVitriTon()
        {
            if (NotAddEdit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            if (!_maVt.VITRI_YN) return;

            try
            {
                Invoice.GetAlVitriTon(dateNgayCT.Date, _sttRec, _maVt.Text, txtMaKhoX.Text);
                DataView vton = new DataView(Invoice.AlVitriTon);
                vton.RowFilter = Invoice.GetFilterMaViTriTon(_sttRec, dateNgayCT.Date, txtMaKhoX.Text, txtMaKhoN.Text,
                    txtLoaiNX_PH.Text, txtLoaiNX_PHN.Text);
                DataTable alVitriTonFilter = vton.ToTable();
                FixAlVitriTon(alVitriTonFilter, AD);

                var inputUpper = _maViTri.Text.Trim().ToUpper();
                if (Invoice.AlVitriTon != null)
                {
                    var check = false;
                    foreach (DataRow row in alVitriTonFilter.Rows)
                    {
                        if (row["Ma_vitri"].ToString().Trim().ToUpper() == inputUpper)
                        {
                            check = true;
                        }

                        if (check)
                        {
                            //
                            _maViTri.Tag = row;
                            XuLyKhiNhanMaVitri(row.ToDataDictionary());
                            break;
                        }
                    }

                    if (!check)
                    {
                        var initFilter = GetAlVitriTonInitFilter();
                        var f = new FilterView(alVitriTonFilter, "Ma_vitri", "ALVITRITON", _maViTri, initFilter);
                        if (f.ViewData != null && f.ViewData.Count > 0)
                        {
                            var d = f.ShowDialog(this);

                            //xu ly data
                            if (d == DialogResult.OK)
                            {
                                if (_maViTri.Tag is DataRow)
                                    XuLyKhiNhanMaVitri(((DataRow) _maViTri.Tag).ToDataDictionary());
                                else if (_maViTri.Tag is DataGridViewRow)
                                    XuLyKhiNhanMaVitri(((DataGridViewRow) _maViTri.Tag).ToDataDictionary());
                            }
                            else
                            {
                                _maViTri.Text = _maViTri.GotFocusText;
                            }
                        }
                        else
                        {
                            ShowParentMessage("AlVitriTon" + V6Text.NoData);
                        }
                    }
                    //else if (detail1.MODE == V6Mode.Add)
                    //{
                    //    //Check so ct da chon
                    //    check = false;
                    //    foreach (DataRow row in AD.Rows)
                    //    {
                    //        if (row["Ma_vitri"].ToString().Trim().ToUpper() == inputUpper)
                    //        {
                    //            check = true;
                    //            break;
                    //        }
                    //    }
                    //    if (check) this.ShowWarningMessage("Số hóa đơn đã chọn! " + inputUpper);
                    //}

                    GetViTriLoDate13();
                }
                else
                {
                    ShowMainMessage(V6Text.Text("KCTKTVT"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void FixAlVitriTon(DataTable alVitriTon, DataTable ad)
        {
            if (NotAddEdit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            try
            {
                string sttRec0 = _sttRec0;
                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                    return;

                List<DataRow> empty_rows = new List<DataRow>();

                for (int i = alVitriTon.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = alVitriTon.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maLo, maViTri
                    //- so luong
                    decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                    decimal new_soLuong = data_soLuong;

                    foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                    {
                        string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        string c_maKhoI = txtMaKhoX.Text;
                        string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                        string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                        decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                        if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                        {
                            if (data_maVt == c_maVt && data_maKhoI == c_maKhoI && data_maLo == c_maLo &&
                                data_maViTri == c_maViTri)
                            {
                                new_soLuong -= c_soLuong;
                            }
                        }
                    }

                    if (new_soLuong > 0)
                    {
                        data_row["Ton_dau"] = new_soLuong;
                    }
                    else
                    {
                        empty_rows.Add(data_row);

                    }
                }

                //remove all empty_rows
                foreach (DataRow row in empty_rows)
                {
                    alVitriTon.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Trừ số lượng đang hiện diện ở chi tiết.
        /// </summary>
        /// <param name="alVitriTon"></param>
        private void FixAlVitriTon_Chon(DataTable alVitriTon)
        {
            try
            {
                List<DataRow> empty_rows = new List<DataRow>();
                SortedDictionary<string, V6VvarTextBox> vt = new SortedDictionary<string, V6VvarTextBox>();
                
                for (int i = alVitriTon.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = alVitriTon.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maLo, maViTri
                    //- so luong
                    decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                    decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                    decimal new_soLuong = data_soLuong;
                    decimal new_soLuong_qd = data_soLuong_qd;

                    foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                    {
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        //string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                        string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                        string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                        if (!vt.ContainsKey(c_maVt))
                        {
                            vt[c_maVt] = new V6VvarTextBox() { VVar = "MA_VT", Text = c_maVt };
                        }
                        // Theo doi lo moi check
                        if (!vt[c_maVt].LO_YN || !vt[c_maVt].DATE_YN || !vt[c_maVt].VITRI_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                            continue;

                        decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                        decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]); //???

                        if (data_maVt == c_maVt && data_maKhoI == txtMaKhoX.Text && data_maLo == c_maLo &&
                            data_maViTri == c_maViTri)
                        {
                            new_soLuong -= c_soLuong;
                            new_soLuong_qd -= c_soLuong_qd;
                        }
                    }

                    if (new_soLuong > 0)
                    {
                        data_row["Ton_dau"] = new_soLuong;
                        data_row["Ton_dau_qd"] = new_soLuong_qd;
                    }
                    else
                    {
                        empty_rows.Add(data_row);
                    }
                }

                //remove all empty_rows
                foreach (DataRow row in empty_rows)
                {
                    alVitriTon.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void FixAlLoDateTon_Chon(DataTable alLoTon)
        {
            try
            {
                List<DataRow> empty_rows = new List<DataRow>();
                SortedDictionary<string, V6VvarTextBox> vt = new SortedDictionary<string, V6VvarTextBox>();

                for (int i = alLoTon.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = alLoTon.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    //string data_maVi_Tri = data_row["Ma_vi_tri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maLo, maVi_Tri
                    //- so luong
                    decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                    decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton_dau_qd"]);
                    decimal new_soLuong = data_soLuong;
                    decimal new_soLuong_qd = data_soLuong_qd;

                    foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                    {
                        //string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        //string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                        string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                        //string c_maVi_Tri = row["Ma_vi_tri"].ToString().Trim().ToUpper();

                        if (!vt.ContainsKey(c_maVt))
                        {
                            vt[c_maVt] = new V6VvarTextBox() { VVar = "MA_VT", Text = c_maVt };
                        }
                        // Theo doi lo moi check
                        if (!vt[c_maVt].LO_YN || !vt[c_maVt].DATE_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                            continue;

                        decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                        decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);

                        if (data_maVt == c_maVt && data_maKhoI == txtMaKhoX.Text && data_maLo == c_maLo)
                        {
                            new_soLuong -= c_soLuong;
                            new_soLuong_qd -= c_soLuong_qd;
                        }
                    }

                    if (new_soLuong > 0)
                    {
                        data_row["Ton_dau"] = new_soLuong;
                        data_row["Ton_dau_qd"] = new_soLuong_qd;
                    }
                    else
                    {
                        empty_rows.Add(data_row);
                    }
                }

                //remove all empty_rows
                foreach (DataRow row in empty_rows)
                {
                    alLoTon.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        

        private void XuLyKhiNhanMaVitri(IDictionary<string, object> row)//, DataRow row0)
        {
            try
            {
                _maLo.Text = row["MA_LO"].ToString().Trim();
                _hanSd.Value = ObjectAndString.ObjectToDate(row["HSD"]);
                _ton13.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU"]);
                if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E") _ton13Qd.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU_QD"]);
                _maLo.Enabled = false;
                
                CheckSoLuong1();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Đã tính luôn tiền vốn 1
        /// </summary>
        public void CheckSoLuong1(Control actionControl = null)
        {
            try
            {
                if (!(IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                          && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))) return;
                TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, actionControl);
                _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                if (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO))
                {
                    if (_soLuong1.Value > _ton13.Value)
                    {
                        ShowParentMessage(V6Text.StockoutWarning);
                        _soLuong1.Value = _ton13.Value < 0 ? 0 : _ton13.Value;
                        if (M_CAL_SL_QD_ALL == "1" && actionControl != _sl_qd)
                        {
                            if (M_TYPE_SL_QD_ALL == "1E")
                            {
                                _sl_qd.Value = _ton13Qd.Value < 0 ? 0 : _ton13Qd.Value;
                            }
                            else if (_hs_qd1.Value != 0)
                            {
                                _sl_qd.Value = _soLuong1.Value / _hs_qd1.Value;
                            }

                            //if (_hs_qd1.Value != 0)
                            //    _sl_qd.Value = _soLuong1.Value / _hs_qd1.Value;
                        }
                        _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
                        //TinhTienNt2(null);
                        TinhTienVon1();
                    }
                }
                TinhTienVon1(actionControl);
                TinhGiaVon();
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
                if (tabControl1.SelectedTab == tabChiTiet && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                {
                    detail1.btnNhan.Focus();
                    detail1.btnNhan.PerformClick();
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

        void MaKhoX_V6LostFocus(object sender)
        {
            XuLyChonMaKhoX();
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
                        txtDiaChi.Enabled = true;
                        txtMaSoThue.Enabled = false;

                        txtDiaChi.ReadOnlyTag(false);
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

        private void XuLyChonMaVt(string mavt)
        {
            XuLyLayThongTinKhiChonMaVt();
            XuLyDonViTinhKhiChonMaVt(mavt);
            GetGiaVonCoDinh(_maVt, _sl_td1, _gia_nt1);
            GetTon13();
            TinhTienVon1();
            if (_maVt.VITRI_YN)
            {
                if (_maVt.LO_YN && _maVt.DATE_YN)
                {
                    GetViTriLoDate();
                }
                else
                {
                    GetViTri();
                }
            }
            else
            {
                GetLoDate();
            }
            //GetLoDate();

            TinhTienVon_GiaVon();
        }

        private void XuLyChonMaKhoX()
        {
            string method_log = "";
            try
            {
                var makhoX_data = txtMaKhoX.Data;
                method_log += "1";
                method_log += "2";

                //GetTon13();
                //GetLoDate();
                if (_maVt.VITRI_YN)
                {
                    method_log += "3";
                    if (_maVt.LO_YN && _maVt.DATE_YN)
                    {
                        method_log += "4";
                        GetViTriLoDate();
                        method_log += "5";
                    }
                    else
                    {
                        method_log += "6";
                        GetViTri();
                        method_log += "7";
                    }
                }
                else
                {
                    method_log += "8";
                    //GetLoDate();
                    if (_maLo.Text == "") GetLoDate();
                    else GetLoDate13();

                    method_log += "9";
                }

                method_log += " 10 ";

                if (makhoX_data != null)
                {
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, detail1, "AM", txtMaKhoX);

                    method_log += "11";
                    TxtTen_kho.Text = makhoX_data["TEN_KHO"].ToString();
                    method_log += "12";
                    var tk_dl = makhoX_data["TK_DL"].ToString().Trim();
                    method_log += "13";
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        method_log += "14";
                        _tkVt.Text = tk_dl;
                        foreach (DataRow row in AD.Rows)
                        {
                            row["TK_VT"] = tk_dl;
                        }
                        method_log += "15";
                    }
                    else
                    {
                        method_log += "16";
                        //{Tuanmh 14/09/2017 Set lai TK_vt khi doi ma_kho
                        var mavt_data = _maVt.Data;
                        var tk_vt_tmp="";

                        if (mavt_data != null)
                        {
                            tk_vt_tmp = mavt_data["TK_VT"].ToString().Trim();
                            if (!string.IsNullOrEmpty(tk_vt_tmp))
                            {
                                _tkVt.Text = tk_vt_tmp;
                            }
                        }
                        method_log += "17";

                        SortedDictionary<string, V6VvarTextBox> vt = new SortedDictionary<string, V6VvarTextBox>();
                        foreach (DataRow row in AD.Rows)
                        {
                            string c_maVt = row["MA_VT"].ToString().Trim();
                            if (!vt.ContainsKey(c_maVt))
                            {
                                vt[c_maVt] = new V6VvarTextBox() { VVar = "MA_VT", Text = c_maVt };
                            }

                            mavt_data = vt[c_maVt].Data;
                            if (mavt_data != null)
                            {
                                tk_vt_tmp = mavt_data["TK_VT"].ToString().Trim();
                                if (!string.IsNullOrEmpty(tk_vt_tmp))
                                {
                                    row["TK_VT"] = tk_vt_tmp;
                                }
                            }
                        }
                        method_log += "18";
                    }
                }
                method_log += "19";
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyChonMaKhoX " + _sttRec + " " + method_log, ex);
            }
        }

        private void XuLyChonMaKhoN()
        {
            try
            {
                var data = txtMaKhoN.Data;
                if (data != null)
                {
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, detail1, "AM", txtMaKhoN);

                    TxtTen_khoN.Text = data["TEN_KHO"].ToString();
                    
                    var tk_dl = data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _Ma_nx_i.Text = tk_dl;

                        foreach (DataRow row in AD.Rows)
                        {
                            row["MA_NX_I"] = tk_dl;
                        }
                    }
                    else
                    {//{Tuanmh 14/09/2017 Set lai TK_vt khi doi ma_kho

                        var mavt_data = _maVt.Data;
                        var tk_vt_tmp = "";

                        if (mavt_data != null)
                        {
                            tk_vt_tmp = mavt_data["TK_VT"].ToString().Trim();
                            if (!string.IsNullOrEmpty(tk_vt_tmp))
                            {
                                _Ma_nx_i.Text = tk_vt_tmp;
                            }
                        }

                        SortedDictionary<string, V6VvarTextBox> vt = new SortedDictionary<string, V6VvarTextBox>();
                        foreach (DataRow row in AD.Rows)
                        {
                            string c_maVt = row["MA_VT"].ToString().Trim();
                            if (!vt.ContainsKey(c_maVt))
                            {
                                vt[c_maVt] = new V6VvarTextBox() { VVar = "MA_VT", Text = c_maVt };
                            }
                            mavt_data = vt[c_maVt].Data;
                            if (mavt_data != null)
                            {
                                tk_vt_tmp = mavt_data["TK_VT"].ToString().Trim();
                                if (!string.IsNullOrEmpty(tk_vt_tmp))
                                {
                                    row["MA_NX_I"] = tk_vt_tmp;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyChonMaKhoN " + _sttRec, ex);
            }
        }


        private void XuLyLayThongTinKhiChonMaLo()
        {
            try
            {
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
        public DataTable _dataViTri;
        private void XuLyLayThongTinKhiChonMaVitri()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetLoDate13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoX = txtMaKhoX.Text.Trim().ToUpper();
                string maLo = _maLo.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN )
                    return;
                //|| !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN
                

                if (maVt == "" || maKhoX == "" || maLo == "") return;

                _dataLoDate = Invoice.GetLoDate13(maVt, maKhoX, maLo, _sttRec, dateNgayCT.Date);
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
                    if (maVt == data_maVt && maKhoX == data_maKhoI && maLo == data_maLo)
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
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt&& maLo == c_maLo)
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

        public void GetViTri13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = txtMaKhoX.Text.Trim().ToUpper();
                string maViTri = _maViTri.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maViTri == "") return;

                _dataViTri = Invoice.GetViTri13(maVt, maKhoI, maViTri, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maViTri
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maViTri == data_maViTri)
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
                            string c_maKhoI = txtMaKhoX.Text.ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maViTri == c_maViTri)
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

        public void GetViTriLoDate13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = txtMaKhoX.Text.Trim().ToUpper();
                string maLo = _maLo.Text.Trim().ToUpper();
                string maViTri = _maViTri.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maLo == "" || maViTri == "") return;

                _dataViTri = Invoice.GetViTriLoDate13(maVt, maKhoI, maLo, maViTri, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                //for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                for (int i = 0; i <= _dataViTri.Rows.Count - 1; i++)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();

                    //Neu dung maVt, maKhoI, maLo, maViTri
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo && maViTri == data_maViTri)
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
                            string c_maKhoI = txtMaKhoX.Text.ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo && maViTri == c_maViTri)
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

        private string GetFilterMaLo(DataTable dataLoDate, string sttRec0, string maVt, string maKhoI)
        {
            try
            {
                var list_maLo = "";
                if (maVt == "" || maKhoI == "") return list_maLo;


                for (int i = dataLoDate.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = dataLoDate.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    if (data_maLo == "") continue;

                    //Neu dung maVt va maKhoI
                    if (maVt == data_maVt && maKhoI == data_maKhoI)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();

                            //Add 31-07-2016
                            //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                            //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && data_maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            list_maLo += string.Format(" or Ma_lo='{0}'", data_maLo);
                        }
                    }
                }

                if (list_maLo.Length > 3)
                {
                    list_maLo = list_maLo.Substring(3);
                    return "(" + list_maLo + ")";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            return "(1=0)";
        }

        private void GetLoDate()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoX = txtMaKhoX.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                    return;
                
                if (maVt == "" || maKhoX == "") return;

                _dataLoDate = Invoice.GetLoDate(maVt, maKhoX, _sttRec, dateNgayCT.Date);
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
                    if (maVt == data_maVt && maKhoX == data_maKhoI)
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
                            
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && data_maLo == c_maLo)// && maKhoI == c_maKhoI)
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


        public void GetViTri()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = txtMaKhoX.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = Invoice.GetViTri(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maViTri == "") continue;
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
                            string c_maKhoI = txtMaKhoX.Text.ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maViTri == c_maViTri)
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
                            _maViTri.Text = data_row["Ma_vitri"].ToString().Trim();
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

        public void GetViTriLoDate()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = txtMaKhoX.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN
                    || !txtMaKhoX.LO_YN || !txtMaKhoX.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = Invoice.GetViTriLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                if (_dataViTri.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                //for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                for (int i = 0; i <= _dataViTri.Rows.Count - 1; i++)
                {
                    DataRow data_row = _dataViTri.Rows[i];
                    string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                    string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                    string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                    string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();
                    if (data_maLo == "" || data_maViTri == "") continue;
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
                            string c_maKhoI = txtMaKhoX.Text.ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo && data_maViTri == c_maViTri)
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
                            _maViTri.Text = data_row["Ma_vitri"].ToString().Trim();
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                        else
                        {
                            ResetTonLoHsd(_ton13, _maLo, _hanSd, _ton13Qd);
                            _maViTri.Clear();
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
                if ((_maVt.LO_YN || _maVt.DATE_YN) && (txtMaKhoX.LO_YN || txtMaKhoX.DATE_YN))
                    return;

                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = txtMaKhoX.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataLoDate = Invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Date);
                    if (_dataLoDate != null && _dataLoDate.Rows.Count > 0)
                    {
                        string sttRec0 = _sttRec0;
                        //Trừ dần
                        for (int i = _dataLoDate.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow data_row = _dataLoDate.Rows[i];
                            string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                            string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();


                            //Neu dung maVt va maKhoI
                            if (maVt == data_maVt && maKhoI == data_maKhoI)
                            {
                                //- so luong
                                decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton00"]);
                                decimal data_soLuong_qd = ObjectAndString.ObjectToDecimal(data_row["Ton00QD"]);
                                decimal new_soLuong = data_soLuong;
                                decimal new_soLuong_qd = data_soLuong_qd;

                                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                                {
                                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                                    string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                                    string c_maKhoI = txtMaKhoX.Text.Trim().ToUpper();

                                    decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                                    decimal c_soLuong_qd = ObjectAndString.ObjectToDecimal(row["SL_QD"]);

                                    //Add 31-07-2016
                                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                                    {
                                        if (maVt == c_maVt && maKhoI == c_maKhoI)
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
                                    break;
                                }
                            }
                        }

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
                var data = _maVt.Data;
                if (data == null)
                {
                    _tkVt.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;
                }
                else
                {
                    SetADSelectMoreControlValue(Invoice, data);
                    _tkVt.Text = (data["TK_VT"] ?? "").ToString().Trim();
                    _hs_qd1.Value = ObjectAndString.ObjectToDecimal(data["HS_QD1"]);
                    _hs_qd2.Value = ObjectAndString.ObjectToDecimal(data["HS_QD2"]);
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

                if (_Ma_nx_i.Text == "")
                {
                    _Ma_nx_i.Text = Invoice.Alct["TK_NO"].ToString().Trim();
                }

                //VPA_GET_BROTHERS_DEFAULTVALUE();
                SetDefaultData_Brothers(Invoice, detail1, "AD", _maVt);
                SetDefaultDataDetail(Invoice, detail1.panelControls);

                var makhoN_data = txtMaKhoN.Data;
                if (makhoN_data != null)
                {
                    var tk_dl = makhoN_data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _Ma_nx_i.Text = tk_dl;
                    }
                }

                var makhoX_data = txtMaKhoX.Data;
                if (makhoX_data != null)
                {
                    var tk_dl = makhoX_data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _tkVt.Text = tk_dl;
                    }
                }
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

            GetTon13();
            if (txtMaKhoX.Text.Trim() != "" && _maLo.Text.Trim() != "")
            {
                GetLoDate13();
            }
            _soLuong.Value = _soLuong1.Value * _he_so1T.Value / _he_so1M.Value;
            
            CheckSoLuong1();
            //TinhTienVon1();
        }

        public void TinhGia_Theo_GiaNt()
        {
            try
            {
                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _gia_nt.Value;
                }
                else
                {
                    if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _gia.Value = V6BusinessHelper.Vround(_gia_nt.Value * _sl_td1.Value, M_ROUND);
                    else _gia.Value = V6BusinessHelper.Vround(_gia_nt.Value * txtTyGia.Value, M_ROUND_GIA);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGia1_Theo_GiaNt()
        {
            try
            {
                _gia_nt1.Value = _gia_nt.Value;
                if (_maNt == _mMaNt0)
                {
                    _gia1.Value = _gia_nt1.Value;
                }
                else
                {
                    if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * _sl_td1.Value, M_ROUND);
                    else _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * txtTyGia.Value, M_ROUND_GIA);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
    
        /// <summary>
        /// Tien_nt tien
        /// </summary>
        /// <param name="actionControl"></param>
        public void TinhTienVon1(Control actionControl = null)
        {
            _tien_nt.Value = V6BusinessHelper.Vround(_soLuong1.Value * _gia_nt1.Value, M_ROUND_NT);
            if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * _sl_td1.Value, M_ROUND);
            else _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * txtTyGia.Value, M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tien_nt.Value;
            }
        }

        /// <summary>
        /// Tien_nt tien
        /// </summary>
        /// <param name="actionControl"></param>
        public void TinhTienVon(Control actionControl)
        {
            try
            {
                if (actionControl == _soKhung)
                {
                    TinhGia1_Theo_GiaNt();
                }

                TinhGia_Theo_GiaNt();

                _tien_nt.Value = V6BusinessHelper.Vround(_soLuong.Value * _gia_nt.Value, M_ROUND_NT);
                
                if (_maNt == _mMaNt0)
                {
                    _tien.Value = _tien_nt.Value;
                }
                else
                {
                    if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * _sl_td1.Value, M_ROUND);
                    else _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * txtTyGia.Value, M_ROUND);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void TinhGiaNt1()
        {
            if (!chkSuaTien.Checked && _soLuong1.Value != 0)
            {
                _gia_nt1.Value = V6BusinessHelper.Vround(_tien_nt.Value / _soLuong1.Value, M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia1.Value = _gia_nt1.Value;
                }
                else
                {
                    _gia1.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong1.Value, M_ROUND_GIA);
                }
            }
        }

        public void TinhTienVon_GiaVon()
        {
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tien_nt.Value;
            }
            else
            {
                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * _sl_td1.Value, M_ROUND);
                else _tien.Value = V6BusinessHelper.Vround(_tien_nt.Value * txtTyGia.Value, M_ROUND);
            }

            if (_maNt == _mMaNt0)
            {
                _gia1.Value = _gia_nt1.Value;
            }
            else
            {
                if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * _sl_td1.Value, M_ROUND);
                else _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * txtTyGia.Value, M_ROUND_GIA);
            }

            if (_soLuong.Value != 0)
            {
                _gia_nt.Value = V6BusinessHelper.Vround(_tien_nt.Value / _soLuong.Value, M_ROUND_GIA_NT);

                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _gia_nt.Value;
                }
                else
                {
                    _gia.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong.Value, M_ROUND_GIA);
                }
            }
        }

        public void TinhGiaVon()
        {
            try
            {
                if (_maNt == _mMaNt0)
                {
                    _gia1.Value = _gia_nt1.Value;
                }
                else
                {
                    if (_maVt.GIA_TON == 5 && _sl_td1.Value != 0) _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * _sl_td1.Value, M_ROUND);
                    else _gia1.Value = V6BusinessHelper.Vround(_gia_nt1.Value * txtTyGia.Value, M_ROUND_GIA);
                }

                if (_soLuong.Value != 0)
                {
                    _gia_nt.Value = V6BusinessHelper.Vround(_tien_nt.Value / _soLuong.Value, M_ROUND_GIA_NT);
                    
                    if (_maNt == _mMaNt0)
                    {
                        _gia.Value = _gia_nt.Value;
                    }
                    else
                    {
                        _gia.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong.Value, M_ROUND_GIA);
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
                    chonDonHangMuaMenu.Enabled = false;
                    TroGiupMenu.Enabled = false;
                    chonTuExcelMenu.Enabled = false;
                }
                else //Cac truong hop khac
                {
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _dvt1.Enabled = true;
                    
                    //{Tuanmh 20/02/2016
                    bool is_gia_dichdanh = _maVt.GIA_TON == 2 || _xuat_dd.Text != "";

                    _tien_nt.Enabled = chkSuaTien.Checked && is_gia_dichdanh;
                    _tien.Enabled = is_gia_dichdanh && _tien_nt.Value == 0 && _maNt != _mMaNt0;

                    _gia_nt.Enabled =  is_gia_dichdanh;
                    _gia.Enabled =  is_gia_dichdanh && _gia_nt.Value==0;

                    _gia_nt1.Enabled =  is_gia_dichdanh;
                    _gia1.Enabled =  is_gia_dichdanh && _gia_nt1.Value == 0;

                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
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

                        V6VvarTextBox txtmavt = new V6VvarTextBox() {VVar = "MA_VT", Text = cell_MA_VT.Value.ToString()};
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
                        row.Cells["TIEN_NT2"].Value = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(cell_SO_LUONG1.Value)
                            * ObjectAndString.ObjectToDecimal(row.Cells["GIA_NT21"].Value), M_ROUND_NT);
                        row.Cells["TIEN0"].Value = _maNt == _mMaNt0
                            ? row.Cells["TIEN_NT2"].Value
                            : V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(row.Cells["TIEN_NT2"].Value) * txtTyGia.Value, M_ROUND);

                        //TinhTienVon(_soLuong1);
                        if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2_Row(row, FIELD);
                        if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1_Row(row, FIELD);

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
                        TinhChietKhauChiTiet_row_XUAT_TIEN_NT2(row, txtTyGia.Value);
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
                        //TinhVanChuyen_row(row);
                        //TinhGiamGiaCt_row(row);
                        //Tinh_thue_ct_row(row);

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

            var tTienNt = TinhTong(AD, "TIEN_NT");
            txtTongTienNt.Value = tTienNt;
            var tTien = TinhTong(AD, "TIEN");
            txtTongTien.Value = tTien;
        }
        

        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                //ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
                if (NotAddEdit) return;
                //Tính tổng thanh toán.//con phan nt va tien viet chua ro rang.
          
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();
                TinhTongValues();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
            ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
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
                TxtMa_gd.Text = "3";

                try
                {
                    cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();
                }
                catch
                {
                }

                if (AM_old != null)
                {
                    var txtt = new V6VvarTextBox();
                    txtt.VVar = txtMa_sonb.VVar;
                    var old_masonb = AM_old["Ma_sonb"].ToString().Trim();
                    txtt.Text = old_masonb;
                    if (txtt.Data != null && txtt.Data["Status"].ToString().Trim() == "1")
                    {
                        txtMa_sonb.Text = old_masonb;
                        if (txtSoPhieu.Text.Trim() == "")
                        {
                            txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(old_masonb, dateNgayCT.Date);
                        }
                        if (txtMaMauHD.Text.Trim() == "")
                        {
                            txtMaMauHD.Text = AM_old["MA_MAUHD"].ToString().Trim();
                        }
                        if (txtso_seri.Text.Trim() == "")
                        {
                            txtso_seri.Text = AM_old["SO_SERI"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyThayDoiMaDVCS()
        {
            try
            {
                string filter = V6Login.GetFilterKhoByDVCS(txtMaDVCS.Text.Trim());
                txtMaKhoX.SetInitFilter(filter);
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

                viewColumn = dataGridView1.Columns["GIA21"];
                if (viewColumn != null)
                    viewColumn.HeaderText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                column = dataGridView1.Columns["TIEN2"];
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
                    
                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA21");
                    if (c != null) c.Visible = true;
                    
                    var
                    dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["TIEN"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    
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

                    dataGridViewColumn = dataGridView1.Columns["GIA2"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    dataGridViewColumn = dataGridView1.Columns["TIEN"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;

                    dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
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
                _tien_nt.DecimalPlaces = decimalPlaces;
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

        private void XuLyThayDoiMaGia()
        {
            TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
        }
        
        /// <summary>
        /// Lấy dữ liệu AD dựa vào rec, tạo 1 copy gán vào AD81
        /// </summary>
        /// <param name="sttRec"></param>
        public void LoadAD(string sttRec )
        {
            try
            {
                if (ADTables == null) ADTables = new SortedDictionary<string, DataTable>();

                if (ADTables.ContainsKey(sttRec)) AD = ADTables[sttRec].Copy();
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
            catch(Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }


        #region ==== View invoice ====

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
        /// Cần set trước AD81 cho đúng với index
        /// </summary>
        private void ViewInvoice()
        {
            try
            {
                Mode = V6Mode.View;
                V6ControlFormHelper.SetFormDataRow(this, AM.Rows[CurrentIndex]);

                XuLyThayDoiMaDVCS();
                txtMaDVCS.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                txtMaKH2.ExistRowInTable();
                txtMaKhoX.ExistRowInTable();
                txtMaKhoN.ExistRowInTable();
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");
                
                //XuLyChonMaKhoX();
                //XuLyChonMaKhoN();
                SetGridViewData();
                XuLyThayDoiMaNt();

                Mode = V6Mode.View;

                FormatNumberControl();
                FormatNumberGridView();
                LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);

                OnInvoiceChanged(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice " + _sttRec, ex);
            }
            ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
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
                    //All_Objects["MA_NX"] = txtManx.Text;
                    //All_Objects["LOAI_CK"] = chkLoaiChietKhau.Checked ? "1" : "0";
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
                    //All_Objects["MA_NX"] = txtManx.Text;
                    //All_Objects["LOAI_CK"] = chkLoaiChietKhau.Checked ? "1" : "0";
                    All_Objects["MODE"] = "S";
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
                this.ShowErrorException(string.Format("{0} {1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                    if (Invoice.IS_AM2TH(row.ToDataDictionary()))
                    {
                        _sttRec2_TH = _sttRec;
                        DoDelete2_TH_Thread();
                    }
                    _AED_Success = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                }
                else
                {
                    _AED_Success = false;
                    deleteErrorMessage = V6Text.Text("XOA0");
                    Invoice.PostErrorLog(_sttRec, "X");
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

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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

                        //LoadAll(V6Mode.Add);

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
                        GoToFirstFocus(txtMa_sonb);
                    }
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
                                    "", dateNgayCT.Date, txtTongTien.Value, "E"); // !!!!! txtTongThanhToan

                            if (check_edit)
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
                                "", dateNgayCT.Date, txtTongTien.Value, "D"); // !!!!! txtTongThanhToan

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
                ResetAllADDefaultValue();
                InvokeFormEventFixCopyData();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetNewValues " + _sttRec, ex);
            }
        }

        private TimPhieuXuatDieuChuyenForm SearchForm
        {
            get
            {
                if (_timForm == null || _timForm.IsDisposed)
                    _timForm = new TimPhieuXuatDieuChuyenForm(Invoice, V6Mode.View);
                return _timForm;
            }
        }
        private TimPhieuXuatDieuChuyenForm _timForm;
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

        public decimal TongThanhToan { get { return txtTongTien.Value; } }
        public decimal TongThanhToanNT { get { return txtTongTienNt.Value; } }
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
            //cboKieuPost.SelectedValue = "1";
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
                        ViewInvoice(CurrentIndex);
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
            if (_Ma_lnxn != null && txtLoaiNX_PHN.Text != string.Empty)
            {
                if (_Ma_lnxn != null) _Ma_lnxn.Text = txtLoaiNX_PHN.Text;
            }
            SetDefaultDataHDDetail(Invoice, detail1);
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

                if (txtMaKhoN.Data != null)
                {
                    var tk_dl = txtMaKhoN.Data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _Ma_nx_i.Text = tk_dl;
                        data["MA_NX_I"] = tk_dl;
                    }
                }
                if (txtMaKhoX.Data != null)
                {
                    var tk_dl = txtMaKhoX.Data["TK_DL"].ToString().Trim();
                    if (!string.IsNullOrEmpty(tk_dl))
                    {
                        _tkVt.Text = tk_dl;
                        data["TK_VT"] = tk_dl;
                    }
                }
                

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                
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
                            dataGridView1.Rows[AD.Rows.Count - 1].Selected = true;
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

        #region ==== PhieuXuatDieuChuyenDetail Event ====
        private void detail1_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetailClickAdd();
            }
            else
            {
                dataGridView1.UnLock();
                ViewCurrentRowToDetail(dataGridView1, detail1);
            }
        }
        private void detail1_AddHandle(IDictionary<string, object> data)
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
        private void detail1_EditHandle(IDictionary<string, object> data)
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
        private void detail1_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail();
        }
        private void detail1_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            dataGridView1.UnLock();
            ViewCurrentRowToDetail(dataGridView1, detail1);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ValidateNgayCt(Invoice.Mact, dateNgayCT))
            {
                Xoa();
            }
        }
        
        private void cboMaNt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_ready0 && cboMaNt.SelectedValue != null)
            {
                _maNt = cboMaNt.SelectedValue.ToString().Trim();
                
                if(Mode == V6Mode.Add || Mode == V6Mode.Edit) GetTyGia();

                FormatGridView();
                XuLyThayDoiMaNt();
            }

            txtTyGia_V6LostFocus(sender);
        }

        #endregion am events

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            var fieldName = e.Column.DataPropertyName.ToUpper();
            if (_alct1Dic.ContainsKey(fieldName))
            {
                var row = _alct1Dic[fieldName];
                var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                if(fieldName == "GIA_NT1") fcaption += " "+ cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "GIA1") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN") fcaption += " " + _mMaNt0;

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

        private void txtMaKh_Leave(object sender, EventArgs e)
        {
            
        }

        private void PhieuXuatDieuChuyenControl_VisibleChanged(object sender, EventArgs e)
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
        
        private void chkSuaTien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                
                _tien_nt.Enabled = chkSuaTien.Checked && _xuat_dd.Text != "";
            }
            if (chkSuaTien.Checked)
            {
                _tien_nt.Tag = null;
            }
            else
            {
                _tien_nt.Tag = "disable";
            }
        }
        
        private void detail1_ClickEdit(object sender, HD_Detail_Eventargs e)
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
                        XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                        Detail1FocusReset();
                        GetLoDate13();
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
                    "", dateNgayCT.Date, txtMa_ct.Text, TongThanhToan, mode_vc, V6Login.UserId);

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



                // Kiểm tra xem có cần thiết kiểm tra tồn hay không.
                if (Invoice.CheckTonMaster_YN(cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec, txtMaDVCS.Text.Trim(), txtMaKh.Text.Trim(),
                        "", dateNgayCT.Date, txtMa_ct.Text, TongThanhToan, mode_vc, V6Login.UserId))
                {
                    var check_ton = ValidateData_Master_CheckTon(Invoice, dateNgayCT.Date, txtMaKhoX.Text.Trim());
                    if (!check_ton) return false;
                }
                else
                {

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
                if (_tkVt.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage(V6Text.Text("TKNOTCT"), "TKNOTCT");
                    return false;
                }

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
                ShowViewInfoData2_TH(Invoice);
            }
            else if (Mode == V6Mode.View)
            {
                ShowViewInfoData(Invoice);
            }
        }
        
        private void txtMa_sonb_V6LostFocus(object sender)
        {
            if (NotAddEdit) return;
            GetSoPhieu();
            var data = txtMa_sonb.Data;
            if (data != null)
            {
                txtMaMauHD.Text = (data["MA_MAUHD"] ?? "").ToString().Trim();
                txtso_seri.Text = (data["SO_SERI"] ?? "").ToString().Trim();
            }
        }

        private void txtMa_sonb_V6LostFocusNoChange(object sender)
        {
            if (NotAddEdit) return;
            if (txtSoPhieu.Text.Trim() == "")
            {
                var data = txtMa_sonb.Data;
                if (data != null)
                {
                    GetSoPhieu();
                }
            }
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
            LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);
        }

        private void txtMaKH2_TextChanged(object sender)
        {
            return;
            //if (IsReady)
            //{
            //    if (txtMaKH2.Data != null)
            //    {
            //        txtTenKH2.Text = txtMaKH2.Data["TEN_KH"].ToString();
            //    }
            //    else
            //    {
            //        txtTenKH2.Clear();
            //    }
            //}
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void txtTongTienNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
        }
        
        private void txtMaKhoN_V6LostFocus(object sender)
        {
            XuLyChonMaKhoN();
        }

        private void txtMaKhoN_V6LostFocusNoChange(object sender)
        {
            XuLyChonMaKhoN();
        }


        private void txtMaKhoX_V6LostFocusNoChange(object sender)
        {
            XuLyChonMaKhoX();
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
                chonExcel.CheckFields = "MA_VT,TIEN_NT0,SO_LUONG1,GIA_NT01";
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
                    if (cMaVt == string.Empty)
                    {
                        removeList.Add(row);
                        continue;
                    }
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    if (!exist)
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
            if (row0.ContainsKey("MA_VT")
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
                    //var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    //var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

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

                    if (exist)// && exist2)
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
                        _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaVt);
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
            FixDataGridViewSize(dataGridView1);
        }

        private void menuXemPhieuNhap_Click(object sender, EventArgs e)
        {
            XemPhieuNhap();
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
            if (row0.ContainsKey("MA_VT") && row0.ContainsKey("SO_LUONG1"))
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
                    //var cMaKhoI = data["MA_KHO_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    //var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

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

                    if (exist)// && exist2)
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
                        _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaVt);
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

        private void XemPhieuNhap()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@nXT", 1),
                    new SqlParameter("@Type", 0),
                    new SqlParameter("@Ngay_ct", dateNgayCT.Date),
                    new SqlParameter("@Stt_rec", _sttRec),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@M_lan", V6Login.SelectedLanguage),
                    new SqlParameter("@Advance", string.Format("Ma_kho='{0}' and Ma_vt='{1}'", txtMaKhoX.Text, _maVt.Text)),
                    new SqlParameter("@OutputInsert", ""),
                };
                var data0 = V6BusinessHelper.ExecuteProcedure("VPA_Get_IXB_VIEWF5", plist);
                if (data0 == null || data0.Tables.Count == 0)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }

                var data = data0.Tables[0];
                FilterView f = new FilterView(data, "MA_KH", "IXB_VIEWF5", new V6ColorTextBox(), "");
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    var ROW = f.SelectedRowData;
                    if (ROW == null || ROW.Count == 0) return;

                    var datamavt = _maVt.Data;

                    if (_xuat_dd.Checked || (datamavt != null && ObjectAndString.ObjectToDecimal(datamavt["GIA_TON"]) == 2))
                    {
                        _gia1.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                        _gia_nt1.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemPhieuNhap " + _sttRec, ex);
            }
        }

        private void txtMadvcs_V6LostFocus(object sender)
        {
            XuLyThayDoiMaDVCS();
        }

        private void txtMadvcs_TextChanged(object sender, EventArgs e)
        {
            XuLyThayDoiMaDVCS();
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
            FixMenuChucNangItemShiftText(chonTuExcelMenu, chonDeNghiXuatMenu, chonDonHangMuaMenu, importXmlMenu);
        }

        private void chonDeNghiXuatMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotAddEdit) return;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                //var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMaDVCS.Text.Trim();
                var message = "";
                if (ma_dvcs != "")
                {
                    IXY_PXDC_Form chon = new IXY_PXDC_Form(dateNgayCT.Date.Date, txtMaDVCS.Text, txtMaKh.Text);
                    _chon_px = "IXY";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    //if (ma_kh == "") message += V6Text.NoInput + lblMaKH.Text;
                    if (ma_dvcs == "") message += V6Text.NoInput + lblMaDVCS.Text;
                    this.ShowWarningMessage(message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chon_AcceptSelectEvent(List<IDictionary<string, object>> selectedDataList, ChonEventArgs e) 
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
                int addCount = 0, failCount = 0; _message = "";

                // biến giữ giá trị đầu tiên
                string ma_kh_soh = null, ma_kho_s0 = null, ma_kho_n_s0 = null;
                List<string> inserted_mavitri = new List<string>();
                var AM_somedata = new Dictionary<string, object>();
                var ad2am_dic = ObjectAndString.StringToStringDictionary(e.AD2AM, ',', ':');
                foreach (IDictionary<string, object> data in selectedDataList)
                {
                    // Lấy ma_kh_soh đầu tiên.
                    if (ma_kh_soh == null && data.ContainsKey("MA_KH_SOH"))
                    {
                        ma_kh_soh = data["MA_KH_SOH"].ToString().Trim();
                    }
                    // Gan MA_LNX_I va MA_LNXN
                    if (!data.ContainsKey("MA_LNX_I"))
                    {
                        data["MA_LNX_I"] = txtLoaiNX_PH.Text;
                    }
                    if (!data.ContainsKey("MA_LNXN"))
                    {
                        data["MA_LNXN"] = txtLoaiNX_PHN.Text;
                    }

                    foreach (KeyValuePair<string, string> item in ad2am_dic)
                    {
                        if (data.ContainsKey(item.Key) && !AM_somedata.ContainsKey(item.Value.ToUpper()))
                        {
                            AM_somedata[item.Value.ToUpper()] = data[item.Key.ToUpper()];
                        }
                    }

                    if (ma_kho_s0 == null && data.ContainsKey("MA_KHO"))
                    {
                        ma_kho_s0 = data["MA_KHO"].ToString().Trim();
                        txtMaKhoX.Text = ma_kho_s0;
                    }
                    if (ma_kho_n_s0 == null && data.ContainsKey("MA_KHO_N"))
                    {
                        ma_kho_n_s0 = data["MA_KHO_N"].ToString().Trim();
                        txtMaKhoN.Text = ma_kho_n_s0;
                    }
                    if (ma_kho_n_s0 == null && txtMaKhoN.Text != "")
                    {
                        ma_kho_n_s0 = txtMaKhoN.Text.Trim();
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

                    var newData_X = new SortedDictionary<string, object>(data);
                    string ma_vt = newData_X["MA_VT"].ToString().Trim();
                    V6VvarTextBox temp_vt = new V6VvarTextBox()
                    {
                        VVar = "MA_VT",
                    };
                    temp_vt.Text = ma_vt;
                    if (temp_vt.LO_YN && temp_vt.DATE_YN)
                    {
                        // Tách dòng nhiều lô cộng dồn cho đủ số lượng.
                        decimal total = ObjectAndString.ObjectToDecimal(newData_X["SO_LUONG"]);
                        decimal total_qd = ObjectAndString.ObjectToDecimal(newData_X["SL_QD"]);
                        decimal heso = 1;
                        string dvt1 = newData_X["DVT1"].ToString().Trim();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@p1", ma_vt),
                            new SqlParameter("@p2", dvt1),
                        };
                        var dataHeso = V6BusinessHelper.Select("Alqddvt", "*", "ma_vt=@p1 and dvt=@p2", "", "", plist).Data;
                        if (dataHeso.Rows.Count > 0)
                        {
                            heso = ObjectAndString.ObjectToDecimal(dataHeso.Rows[0]["HE_SO"]);
                        }
                        if (heso == 0) heso = 1;
                        decimal sum = 0, sum_qd = 0;

                        DataTable lodate_data;
                        if (temp_vt.VITRI_YN)
                        {
                            lodate_data = V6BusinessHelper.GetVitriLoDatePriority(ma_vt, _sttRec, dateNgayCT.Date);
                            FixAlVitriTon_Chon(lodate_data);
                        }
                        else
                        {
                            lodate_data = V6BusinessHelper.GetLoDatePriority(ma_vt, _sttRec, dateNgayCT.Date);
                            FixAlLoDateTon_Chon(lodate_data);
                        }

                        // Get Data

                        for (int i = lodate_data.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow data_row = lodate_data.Rows[i];
                            decimal row_ton_dau = ObjectAndString.ObjectToDecimal(data_row["TON_DAU"]);
                            decimal row_ton_dau_qd = ObjectAndString.ObjectToDecimal(data_row["TON_DAU_QD"]);
                            decimal insert = (total - sum) < row_ton_dau ? (total - sum) : row_ton_dau; // Lấy đủ số cần lấy, không đủ thì lấy hết.
                            decimal insert_qd = (total_qd - sum_qd) < row_ton_dau_qd ? (total_qd - sum_qd) : row_ton_dau_qd;
                            // Chỉ định lô
                            string malo_chidinh = newData_X["MA_LO"].ToString().Trim();
                            string malo_row = data_row["MA_LO"].ToString().Trim().ToUpper();

                            if (!string.IsNullOrEmpty(malo_chidinh))
                            {
                                // Có lô chỉ định
                                if (malo_chidinh == malo_row)
                                {
                                    newData_X["MA_LO"] = data_row["MA_LO"];
                                    newData_X["HSD"] = data_row["HSD"];
                                }
                                else
                                {
                                    continue; // bỏ qua lodate_data
                                }
                            }
                            else
                            {
                                // Không Có lô chỉ định
                                newData_X["MA_LO"] = data_row["MA_LO"];
                                newData_X["HSD"] = data_row["HSD"];
                            }
                            //newData["MA_KHO_I"] = data_row["MA_KHO"]; // end for set txt
                            if (temp_vt.VITRI_YN) newData_X["MA_VITRI"] = data_row["MA_VITRI"];

                            newData_X["DVT"] = data_row["DVT"];
                            newData_X["DVT1"] = dvt1;
                            newData_X["HE_SO"] = heso;
                            newData_X["SO_LUONG"] = insert;
                            newData_X["SO_LUONG1"] = insert / heso;
                            decimal gia2 = ObjectAndString.ObjectToDecimal(data["GIA2"]);
                            decimal tien_nt2 = V6BusinessHelper.Vround(insert * gia2, M_ROUND_NT);
                            newData_X["TIEN_NT2"] = tien_nt2;
                            newData_X["TIEN2"] = V6BusinessHelper.Vround(tien_nt2 * txtTyGia.Value, M_ROUND);
                            if (_maNt == _mMaNt0)
                            {
                                newData_X["TIEN2"] = tien_nt2;
                            }
                            if (M_SOA_MULTI_VAT == "1")
                            {
                                decimal thue_suat = ObjectAndString.ObjectToDecimal(data["THUE_SUAT_I"]);
                                decimal thue_nt = V6BusinessHelper.Vround(thue_suat * tien_nt2, M_ROUND_NT);
                                newData_X["THUE_NT"] = thue_nt;
                                newData_X["THUE"] = V6BusinessHelper.Vround(thue_nt * txtTyGia.Value, M_ROUND);
                                if (_maNt == _mMaNt0)
                                {
                                    newData_X["THUE"] = thue_nt;
                                }
                            }
                            var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);

                            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E")
                            {
                                newData_X["SL_QD"] = insert_qd;
                            }
                            else if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                            {
                                newData_X["SL_QD"] = insert / HS_QD1;
                            }

                            if (temp_vt.VITRI_YN)
                            {
                                XuLyNhap(newData_X, ma_kho_n_s0, ref inserted_mavitri, ref addCount, ref failCount);
                            }
                            else
                            {
                                if (XuLyThemDetail(newData_X))
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

                            sum += insert;
                            data_row["TON_DAU"] = row_ton_dau - insert;
                            sum_qd += insert_qd;
                            data_row["TON_DAU_QD"] = row_ton_dau_qd - insert_qd;
                            if (sum == total)
                            {
                                break;
                            }
                        }

                        if (sum < total)
                        {
                            // Lấy thêm lô khác cho đủ số lượng trong lodate_data (đã - sl)
                            for (int i = lodate_data.Rows.Count - 1; i >= 0; i--)
                            {
                                DataRow data_row = lodate_data.Rows[i];
                                decimal row_ton_dau = ObjectAndString.ObjectToDecimal(data_row["TON_DAU"]);
                                decimal row_ton_dau_qd = ObjectAndString.ObjectToDecimal(data_row["TON_DAU_QD"]);
                                decimal insert = (total - sum) < row_ton_dau ? (total - sum) : row_ton_dau; // Lấy đủ số cần lấy, không đủ thì lấy hết.
                                if (insert <= 0) continue;

                                decimal insert_qd = (total_qd - sum_qd) < row_ton_dau_qd ? (total_qd - sum_qd) : row_ton_dau_qd;

                                // Không Có lô chỉ định
                                newData_X["MA_LO"] = data_row["MA_LO"];
                                newData_X["HSD"] = data_row["HSD"];

                                //newData0["MA_KHO_I"] = data_row0["MA_KHO"]; // end for set txt
                                if (temp_vt.VITRI_YN) newData_X["MA_VITRI"] = data_row["MA_VITRI"];

                                newData_X["DVT"] = data_row["DVT"];
                                newData_X["DVT1"] = dvt1;
                                newData_X["HE_SO"] = heso;
                                newData_X["SO_LUONG"] = insert;
                                newData_X["SO_LUONG1"] = insert / heso;
                                decimal gia2 = ObjectAndString.ObjectToDecimal(data["GIA2"]);
                                decimal tien_nt2 = V6BusinessHelper.Vround(insert * gia2, M_ROUND_NT);
                                newData_X["TIEN_NT2"] = tien_nt2;
                                newData_X["TIEN2"] = V6BusinessHelper.Vround(tien_nt2 * txtTyGia.Value, M_ROUND);
                                if (_maNt == _mMaNt0)
                                {
                                    newData_X["TIEN2"] = tien_nt2;
                                }
                                if (M_SOA_MULTI_VAT == "1")
                                {
                                    decimal thue_suat = ObjectAndString.ObjectToDecimal(data["THUE_SUAT_I"]);
                                    decimal thue_nt = V6BusinessHelper.Vround(thue_suat * tien_nt2, M_ROUND_NT);
                                    newData_X["THUE_NT"] = thue_nt;
                                    newData_X["THUE"] = V6BusinessHelper.Vround(thue_nt * txtTyGia.Value, M_ROUND);
                                    if (_maNt == _mMaNt0)
                                    {
                                        newData_X["THUE"] = thue_nt;
                                    }
                                }
                                var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);

                                if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E")
                                {
                                    newData_X["SL_QD"] = insert_qd;
                                }
                                else if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                                {
                                    newData_X["SL_QD"] = insert / HS_QD1;
                                }

                                if (temp_vt.VITRI_YN)
                                {
                                    XuLyNhap(newData_X, ma_kho_n_s0, ref inserted_mavitri, ref addCount, ref failCount);
                                }
                                else
                                {
                                    if (XuLyThemDetail(newData_X))
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

                                sum += insert;
                                sum_qd += insert_qd;
                                if (sum == total)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (newData_X.ContainsKey("SO_LUONG"))
                        {
                            decimal insert = ObjectAndString.ObjectToDecimal(newData_X["SO_LUONG"]);
                            decimal insert_qd = ObjectAndString.ObjectToDecimal(newData_X["SL_QD"]);
                            decimal heso = 1;
                            string dvt1 = newData_X["DVT1"].ToString().Trim();
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
                            newData_X["SO_LUONG1"] = insert / heso;
                            var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);

                            if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E")
                            {
                                newData_X["SL_QD"] = insert_qd;
                            }
                            else if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                            {
                                newData_X["SL_QD"] = insert / HS_QD1;
                            }
                        }

                        if (temp_vt.VITRI_YN)
                        {
                            XuLyNhap(newData_X, ma_kho_n_s0, ref inserted_mavitri, ref addCount, ref failCount);
                        }
                        else
                        {
                            if (XuLyThemDetail(newData_X))
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
                    }
                } // end for

                txtMaKhoX.Text = ma_kho_s0;
                txtMaKhoN.Text = ma_kho_n_s0;

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

        /// <summary>
        /// Copy từ phiếu nhập kho.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ma_kho_n"></param>
        /// <param name="inserted_mavitri"></param>
        /// <param name="addCount"></param>
        /// <param name="failCount"></param>
        private void XuLyNhap(IDictionary<string, object> data, string ma_kho_n, ref List<string> inserted_mavitri, ref int addCount, ref int failCount)
        {
            //if (string.IsNullOrEmpty(ma_kho_n)) ma_kho_n = txtMaKhoN.Text.Trim();
            //if (string.IsNullOrEmpty(ma_kho_n))
            //{
            //    failCount++;
            //    return;
            //}
            //// Lấy ma_kh_soh đầu tiên.
            //if (ma_kh_soh == null && data.ContainsKey("MA_KH_SOH"))
            //{
            //    ma_kh_soh = data["MA_KH_SOH"].ToString().Trim();
            //}
            //foreach (KeyValuePair<string, string> item in ad2am_dic)
            //{
            //    if (data.ContainsKey(item.Key) && !AM_somedata.ContainsKey(item.Value.ToUpper()))
            //    {
            //        AM_somedata[item.Value.ToUpper()] = data[item.Key.ToUpper()];
            //    }
            //}

            //string c_makh = data.ContainsKey("MA_KH") ? data["MA_KH"].ToString().Trim().ToUpper() : "";
            //if (c_makh != "" && txtMaKh.Text == "")
            //{
            //    txtMaKh.ChangeText(c_makh);
            //}

            //if (c_makh != "" && c_makh != txtMaKh.Text.ToUpper())
            //{
            //    failCount++;
            //    _message += ". " + failCount + ":" + c_makh;
            //    continue;
            //}

            var newData = new SortedDictionary<string, object>(data);
            string ma_vt = newData["MA_VT"].ToString().Trim();
            //string ma_kho_n = newData["MA_KHO_N"].ToString().Trim();
            V6VvarTextBox temp_vt = new V6VvarTextBox()
            {
                VVar = "MA_VT",
            };
            temp_vt.Text = ma_vt;

            DataTable lodate_data = V6BusinessHelper.GetStockinVitriDatePriority(ma_vt, ma_kho_n, _sttRec, dateNgayCT.Date);

            if (temp_vt.VITRI_YN && lodate_data != null && lodate_data.Rows.Count > 0)
            {
                // Tách dòng nhiều lô cộng dồn cho đủ số lượng.
                decimal total = ObjectAndString.ObjectToDecimal(newData["SO_LUONG"]);
                decimal total_qd = ObjectAndString.ObjectToDecimal(newData["SL_QD"]);
                decimal heso = 1;
                string dvt1 = newData["DVT1"].ToString().Trim();
                string dvt = dvt1;
                SqlParameter[] plist =
                {
                    new SqlParameter("@p1", ma_vt),
                    new SqlParameter("@p2", dvt1),
                };
                var dataHeso = V6BusinessHelper.Select("Alqddvt", "*", "ma_vt=@p1 and dvt=@p2", "", "", plist).Data;
                if (dataHeso.Rows.Count > 0)
                {
                    heso = ObjectAndString.ObjectToDecimal(dataHeso.Rows[0]["HE_SO"]);
                    dvt = dataHeso.Rows[0]["DVTQD"].ToString().Trim();
                }
                if (heso == 0) heso = 1;
                decimal sum = 0, sum_qd = 0;


                // Get Data

                for (int i = lodate_data.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow data_row = lodate_data.Rows[i];
                    // Loại trừ số lượng đã thêm vào AD trước đó. !!!!!!!!!
                    // Mã vị trí đã chọn không được chọn lại. !!!!!!!!!!!!!
                    foreach (DataRow AD_row in AD.Rows)
                    {
                        if (ma_vt == AD_row["MA_VT"].ToString().Trim() &&
                            data_row["MA_VITRI"].ToString().Trim() == AD_row["MA_VITRIN"].ToString().Trim())
                        {
                            data_row["SL_VTMAX"] = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX"])
                                                   - ObjectAndString.ObjectToDecimal(AD_row["SO_LUONG"]);
                            data_row["SL_VTMAX_QD"] = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX_QD"])
                                                      - ObjectAndString.ObjectToDecimal(AD_row["SL_QD"]);
                        }
                    }

                    decimal row_SL_MAX = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX"]);
                    decimal row_SL_MAX_qd = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX_QD"]);
                    decimal insert = (total - sum) < row_SL_MAX ? (total - sum) : row_SL_MAX;
                        // Lấy đủ số cần lấy, không đủ thì lấy hết.
                    if (insert <= 0) continue;
                    decimal insert_qd = (total_qd - sum_qd) < row_SL_MAX_qd ? (total_qd - sum_qd) : row_SL_MAX_qd;
                    // Chỉ định lô
                    //string mavitri_chidinh = newData.ContainsKey("MA_VITRI")
                    //    ? newData["MA_VITRI"].ToString().Trim()
                    //    : "";
                    string mavitri_row = data_row["MA_VITRI"].ToString().Trim().ToUpper();

                    //if (!string.IsNullOrEmpty(mavitri_chidinh))
                    //{
                    //    // Có vị trí chỉ định
                    //    if (mavitri_chidinh == mavitri_row)
                    //    {
                    //        newData["MA_VITRI"] = data_row["MA_VITRI"];
                    //    }
                    //    else
                    //    {
                    //        continue; // bỏ qua lodate_data
                    //    }
                    //}
                    //else
                    {
                        // Không Có vị trí chỉ định
                        newData["MA_VITRIN"] = mavitri_row;
                    }

                    //newData["MA_KHO_I"] = data_row["MA_KHO"]; // khong chuyen truong
                    //if (temp_vt.VITRI_YN) newData["MA_VITRI"] = mavitri_row;

                    string c_MA_VITRI = newData["MA_VITRIN"].ToString().ToUpper();
                    if (c_MA_VITRI != "" && inserted_mavitri.Contains(c_MA_VITRI)) continue;
                    inserted_mavitri.Add(c_MA_VITRI);

                    newData["DVT"] = dvt;
                    newData["DVT1"] = dvt1;
                    newData["HE_SO"] = heso;
                    newData["SO_LUONG"] = insert;
                    newData["SO_LUONG1"] = insert/heso;

                    var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);

                    if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E")
                    {
                        newData["SL_QD"] = insert_qd;
                    }
                    else if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                    {
                        newData["SL_QD"] = insert/HS_QD1;
                    }

                    if (XuLyThemDetail(newData))
                    {
                        addCount++;
                        All_Objects["data"] = newData;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                    else failCount++;

                    sum += insert;
                    data_row["SL_VTMAX"] = row_SL_MAX - insert;
                    sum_qd += insert_qd;
                    data_row["SL_VTMAX_QD"] = row_SL_MAX_qd - insert_qd;
                    if (sum == total)
                    {
                        break;
                    }
                }

                if (sum < total)
                {
                    // Lấy thêm lô khác cho đủ số lượng trong lodate_data (đã - sl)
                    for (int i = lodate_data.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow data_row = lodate_data.Rows[i];
                        decimal row_ton_dau = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX"]);
                        decimal row_ton_dau_qd = ObjectAndString.ObjectToDecimal(data_row["SL_VTMAX_QD"]);
                        decimal insert = (total - sum) < row_ton_dau ? (total - sum) : row_ton_dau;
                            // Lấy đủ số cần lấy, không đủ thì lấy hết.
                        if (insert <= 0) continue;

                        decimal insert_qd = (total_qd - sum_qd) < row_ton_dau_qd ? (total_qd - sum_qd) : row_ton_dau_qd;

                        // Không Có lô chỉ định
                        newData["MA_VITRIN"] = data_row["MA_VITRI"];

                        //newData["MA_KHO_I"] = data_row["MA_KHO"]; // khong chuyen truong
                        //if (temp_vt.VITRI_YN) newData["MA_VITRIN"] = data_row["MA_VITRI"];

                        string c_MA_VITRI = newData["MA_VITRIN"].ToString().ToUpper();
                        if (c_MA_VITRI != "" && inserted_mavitri.Contains(c_MA_VITRI)) continue;
                        inserted_mavitri.Add(c_MA_VITRI);

                        newData["DVT"] = dvt;
                        newData["DVT1"] = dvt1;
                        newData["HE_SO"] = heso;
                        newData["SO_LUONG"] = insert;
                        newData["SO_LUONG1"] = insert/heso;

                        var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);

                        if (M_CAL_SL_QD_ALL == "1" && M_TYPE_SL_QD_ALL == "1E")
                        {
                            newData["SL_QD"] = insert_qd;
                        }
                        else if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                        {
                            newData["SL_QD"] = insert/HS_QD1;
                        }

                        if (XuLyThemDetail(newData))
                        {
                            addCount++;
                            All_Objects["data"] = newData;
                            InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                        }
                        else failCount++;

                        sum += insert;
                        sum_qd += insert_qd;
                        if (sum == total)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //string c_MA_VITRI = newData["MA_VITRI"].ToString().ToUpper();
                //if (c_MA_VITRI != "" && inserted_mavitri.Contains(c_MA_VITRI)) return; // continue
                //inserted_mavitri.Add(c_MA_VITRI);

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
                    newData["SO_LUONG1"] = insert/heso;
                    var HS_QD1 = ObjectAndString.ObjectToDecimal(temp_vt.Data["HS_QD1"]);
                    if (HS_QD1 != 0 && M_CAL_SL_QD_ALL == "1")
                    {
                        newData["SL_QD"] = insert/HS_QD1;
                    }
                }

                if (XuLyThemDetail(newData))
                {
                    addCount++;
                    All_Objects["data"] = newData;
                    InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                }
                else failCount++;
            }
        }

        private void inPhieuHachToanMenu_Click(object sender, EventArgs e)
        {
            InPhieuHachToan(Invoice, _sttRec, TongThanhToan, TongThanhToanNT);
        }

        private void xemVitriMenu_Click(object sender, EventArgs e)
        {
            XemVitri(txtMaKhoN.Text, dateNgayCT.Value, _maVt.Text);
        }

        private void XemVitri(string makho, DateTime ngayCt, string mavt)
        {
            try
            {
                AINVITRI02 control = new AINVITRI02(ItemID, "AINVITRI02", "AINVITRI02", "AINVITRI02", "AINVITRI02", "AINVITRI02");
                control.khoHangContainer.txtMaKho.Text = makho;
                control.khoHangContainer.dateCuoiNgay.Value = ngayCt;
                control.khoHangContainer.txtMavt.Text = mavt;
                control.khoHangContainer.V6Click += delegate(IDictionary<string, object> data)
                {
                    List<ViTriDetail> _list_vtd = data["LISTVITRIDETAIL"] as List<ViTriDetail>;
                    if (_list_vtd != null && _list_vtd.Count > 0 && _list_vtd[0]._rowDataVitriVattu == null)
                    {
                        _maViTri.Text = _list_vtd[0].MA_VITRI;
                    }
                };

                control.btnNhan.PerformClick();
                control.ShowToForm(this, "title", false, true, false);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void xemVitri2Menu_Click(object sender, EventArgs e)
        {
            XemVitriHangHu(_maKho2.Text, dateNgayCT.Value, _maVt.Text);
        }

        private void XemVitriHangHu(string makho, DateTime ngayCt, string mavt)
        {
            try
            {
                AINVITRI02 control = new AINVITRI02(ItemID, "AINVITRI02", "AINVITRI02", "AINVITRI02", "AINVITRI02", "AINVITRI02");
                control.khoHangContainer.txtMaKho.Text = makho;
                control.khoHangContainer.dateCuoiNgay.Value = ngayCt;
                control.khoHangContainer.txtMavt.Text = mavt;
                control.khoHangContainer.V6Click += delegate(IDictionary<string, object> data)
                {
                    List<ViTriDetail> _list_vtd = data["LISTVITRIDETAIL"] as List<ViTriDetail>;
                    if (_list_vtd != null && _list_vtd.Count > 0 && _list_vtd[0]._rowDataVitriVattu == null)
                    {
                        if (_orderList.Contains("SO_IMAGE"))
                        {
                            detail1.SetSomeData(new Dictionary<string, object>() { { "SO_IMAGE", _list_vtd[0].MA_VITRI } });
                        }
                    }
                };

                control.btnNhan.PerformClick();
                control.ShowToForm(this, "title", false, true, false);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
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

        private void txtLoaiNX_PHN_V6LostFocus(object sender)
        {
            try
            {
                if (txtLoaiNX_PHN.Text != string.Empty)
                {
                    V6ControlFormHelper.UpdateDKlist(AD, "MA_LNXN", txtLoaiNX_PHN.Text);
                    if (_Ma_lnxn != null) _Ma_lnxn.Text = txtLoaiNX_PHN.Text;
                    //VPA_GET_BROTHERS_DEFAULTVALUE();
                    SetDefaultData_Brothers(Invoice, detail1, "AM", txtLoaiNX_PHN);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuatHetKhoMenu_Click(object sender, EventArgs e)
        {
            ChucNang_XuatHetKho();
        }

        private void ChucNang_XuatHetKho()
        {
            try
            {
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                var xuatHetKho = new XuatHetKhoDataForm();
                xuatHetKho.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                xuatHetKho.AcceptData += xuatHetKho_AcceptData;
                xuatHetKho.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void xuatHetKho_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO"))
            {
                detail1.MODE = V6Mode.View;
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }

                string ma_khox = null;
                var tMA_VT = new V6VvarTextBox() { VVar = "MA_VT" };
                foreach (DataRow row in table.Rows)
                {
                    var data = row.ToDataDictionary(_sttRec);
                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO"].ToString().Trim();
                    if (string.IsNullOrEmpty(ma_khox)) ma_khox = cMaKhoI;
                    //var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    //var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    // { Tuanmh 31/08/2016 Them thong tin ALVT fix 28/12/2023
                    tMA_VT.Text = cMaVt;
                    var datavt = tMA_VT.Data;
                    var tonCuoi = ObjectAndString.ObjectToDecimal(data["TON_CUOI"]);
                    var duCuoi = ObjectAndString.ObjectToDecimal(data["DU_CUOI"]);
                    if (cMaVt == "" || cMaKhoI == "" || (Math.Abs(tonCuoi) + Math.Abs(duCuoi) == 0)) continue;

                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1T")) data.Add("HE_SO1T", 1);
                        if (!data.ContainsKey("HE_SO1M")) data.Add("HE_SO1M", 1);
                        if (!data.ContainsKey("SO_LUONG")) data.Add("SO_LUONG", data["TON_CUOI"]);
                        if (!data.ContainsKey("SO_LUONG1")) data.Add("SO_LUONG1", data["TON_CUOI"]);

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["DU_CUOI"]);
                        var __tien0 = __tien_nt0;

                        if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);

                        if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", __tien_nt0);
                        if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);

                    }

                    data["MA_KHO_I"] = cMaKhoI;
                    data["MA_NX_I"] = data["TK_VT"];

                    if (XuLyThemDetail(data))
                    {
                        count++;
                        All_Objects["data"] = data;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                }

                if (txtMaKhoX.Text == string.Empty && !string.IsNullOrEmpty(ma_khox)) txtMaKhoX.Text = ma_khox;
                
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

        private void inHoaDonDienTuMenu_Click(object sender, EventArgs e)
        {
            InHoaDonDienTu();
        }

        private void ChonViTriXuatMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKhoX.Text == "" || txtMaKhoN.Text == "")
                {
                    this.ShowWarningMessage(string.Format("{0} {1} = ({2}), {3} = ({4})", V6Text.NoInput,
                        lblMaKhoX, txtMaKhoX.Text == "" ? V6Text.Empty:txtMaKhoX.Text,
                        lblMaKhoN, txtMaKhoN.Text == "" ? V6Text.Empty:txtMaKhoN.Text));
                    return;
                }

                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;

                var chonVitriXuat = new ChonVitriXuatForm();
                chonVitriXuat.CheckFields = "MA_VT,MA_KHO_I,SO_LUONG1";
                chonVitriXuat.lineMakho.SetValue(txtMaKhoX.Text);
                chonVitriXuat.AcceptSelectEvent += chonViTriXuat_AcceptSelectEvent;
                chonVitriXuat.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        void chonViTriXuat_AcceptSelectEvent(List<IDictionary<string, object>> dataList, ChonEventArgs e)
        {
            var count = 0;
            _message = "";

            //if (dataList.Columns.Contains("MA_VT") && dataList.Columns.Contains("MA_KHO"))
            {
                detail1.MODE = V6Mode.View;
                dataGridView1.UnLock();
                bool flag_add = chon_accept_flag_add;
                chon_accept_flag_add = false;
                if (!flag_add)
                {
                    AD.Rows.Clear();
                }
                var tMA_VT = new V6VvarTextBox() { VVar = "MA_VT" };
                foreach (IDictionary<string, object> row in dataList)
                {
                    if (row["BOLD"].ToString().Trim() == "1") continue;

                    var data = new Dictionary<string, object>(row);
                    data["STT_REC"] = _sttRec;

                    var cMaVt = data["MA_VT"].ToString().Trim();
                    var cMaKhoI = data["MA_KHO"].ToString().Trim();
                    //var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    //var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);

                    // { Tuanmh 31/08/2016 Them thong tin ALVT fix 28/12/2023
                    tMA_VT.Text = cMaVt;
                    var datavt = tMA_VT.Data;
                    var tonCuoi = ObjectAndString.ObjectToDecimal(data["TON_CUOI"]);
                    var duCuoi = ObjectAndString.ObjectToDecimal(data["DU_CUOI"]);
                    if (cMaVt == "" || cMaKhoI == "" || (Math.Abs(tonCuoi) + Math.Abs(duCuoi) == 0)) continue;

                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1T")) data.Add("HE_SO1T", 1);
                        if (!data.ContainsKey("HE_SO1M")) data.Add("HE_SO1M", 1);
                        if (!data.ContainsKey("SO_LUONG")) data.Add("SO_LUONG", data["TON_CUOI"]);
                        if (!data.ContainsKey("SO_LUONG1")) data.Add("SO_LUONG1", data["TON_CUOI"]);

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["DU_CUOI"]);
                        var __tien0 = __tien_nt0;

                        if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);

                        if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", __tien_nt0);
                        if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);

                    }

                    data["MA_KHO_I"] = cMaKhoI;
                    data["MA_NX_I"] = data["TK_VT"];
                    data["MA_LNX_I"] = txtLoaiNX_PH.Text;
                    data["MA_LNXN"] = txtLoaiNX_PHN.Text;

                    if (XuLyThemDetail(data))
                    {
                        count++;
                        All_Objects["data"] = data;
                        InvokeFormEvent(FormDynamicEvent.AFTERADDDETAILSUCCESS);
                    }
                }
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
                if (Invoice.ExtraInfor_MaxRowSaveTemp > 2 && AD.Rows.Count >= Invoice.ExtraInfor_MaxRowSaveTemp)
                {
                    SaveTemp("MAXROWSAVETEMP");
                }
            }
            //else
            //{
            //    ShowParentMessage(V6Text.Text("LACKINFO"));
            //}
        }

        private void xuLyKhac21Menu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.XULYKHAC1);
        }

        private void xuLyKhac22Menu_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.XULYKHAC2);
        }
                
    }
}
