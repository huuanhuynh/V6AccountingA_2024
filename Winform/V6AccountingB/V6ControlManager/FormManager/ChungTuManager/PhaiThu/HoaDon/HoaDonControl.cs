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
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonBaoGia;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonDonHang;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonPhieuNhap;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonPhieuXuat;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon
{
    /// <summary>
    /// Hóa đơn bán hàng kiêm phiếu xuất
    /// </summary>
    public partial class HoaDonControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        public V6Invoice81 Invoice = new V6Invoice81();
        private string _maGd = "1";
        private string _m_Ma_td = "0";

        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public HoaDonControl()
        {
            InitializeComponent();
            MyInit();
        }
        public HoaDonControl(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Dùng để khởi tạo sửa
        /// </summary>
        public HoaDonControl(string itemId, string sttRec)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
            
            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {
            LoadTag(Invoice, detail1.Controls);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            
            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtDiaChiGiaoHang.DisableUpperLower();
            
            txtManx.Upper();
            txtManx.FilterStart = true;
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

            txtDiaChiGiaoHang.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
            
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
            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            _maGd = (Invoice.Alct.Rows[0]["M_MA_GD"] ?? "1").ToString().Trim();
            _m_Ma_td = (Invoice.Alct.Rows[0]["M_MA_TD"] ?? "0").ToString().Trim();

            LoadDetailControls();
            detail1.AddContexMenu(menuDetail1);
            LoadDetail3Controls();
            ResetForm();
            
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }

        #endregion contructor

        #region ==== Khởi tạo Detail Form ====


        private V6ColorTextBox _dvt;
        private V6CheckTextBox _tang, _xuat_dd;
        private V6VvarTextBox _maVt, _dvt1, _maKhoI, _tkDt, _tkGv, _tkCkI, _tkVt, _maLo, _maViTri,_maTdi, _ma_thue_i, _tk_thue_i;
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _giaNt2, _giaNt21,_tien2, _tienNt2, _ck, _ckNt,_gia2,_gia21;
        private V6NumberTextBox _ton13, _gia, _gia_nt, _tien, _tienNt, _pt_cki, _thue_suat_i, _thue_nt, _thue;
        private V6NumberTextBox _sl_qd, _sl_qd2, _tien_vcNt, _tien_vc, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4,_ggNt,_gg;
        private V6DateTimeColor _hanSd;

        
        private void LoadDetailControls()
        {
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);
            //Lấy thông tin Alctct
            var alctct = V6BusinessHelper.GetAlctCt(Invoice.Mact);
            var alctct_GRD_HIDE = new string[]{};
            var alctct_GRD_READONLY = new string[]{};
            if (!V6Login.IsAdmin)
            {
                if (alctct != null && alctct.Rows.Count > 0)
                {
                    var GRD_HIDE = alctct.Rows[0]["GRD_HIDE"].ToString().ToUpper();
                    var GRD_READONLY = alctct.Rows[0]["GRD_READONLY"].ToString().ToUpper();
                    alctct_GRD_HIDE = ObjectAndString.SplitString(GRD_HIDE);
                    alctct_GRD_READONLY = ObjectAndString.SplitString(GRD_READONLY);
                }
            }
            
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
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
                            if (M_SOA_MULTI_VAT == "1")
                            {
                                var data = _maVt.Data;
                                if (data != null)
                                {
                                    _ma_thue_i.Text = (data["ma_thue"] ?? "").ToString().Trim();
                                    _thue_suat_i.Value = ObjectAndString.ObjectToDecimal(data["thue_suat"]);
                                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = maVt.Data[thue_suat] = " + data["thue_suat"]);
                                    if(!chkSuaTienThue.Checked) Tinh_thue_ct();
                                }
                                else
                                {
                                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Không gán thue_suat_i vì maVt.data == null");
                                }

                                var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                                if (alThue.TotalRows > 0)
                                {
                                    _tk_thue_i.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                                    txtTkThueCo.Text = _tk_thue_i.Text;
                                }
                            }

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
                    
                    case "MA_KHO_I":
                        _maKhoI = (V6VvarTextBox)control;
                        _maKhoI.Upper();
                        _maKhoI.LO_YN = false;
                        _maKhoI.DATE_YN = false;
                        _maKhoI.GotFocus += delegate
                        {
                            if(_maKhoI.Text.Trim() == "")
                            _maKhoI.Text = V6Setting.M_Ma_kho_default;
                        };
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

                            if (chkSuaTienThue.Checked && M_SOA_MULTI_VAT=="1")
                                _thue_nt.Enabled = true;
                            else _thue_nt.Enabled = false;

                        }
                        break;
                    case "TON13":
                        _ton13 = control as V6NumberTextBox;
                        if (_ton13.Tag == null || _ton13.Tag.ToString() != "hide")
                        {
                            _ton13.Tag = "disable";
                        }
                        _ton13.StringValueChange += (sender, args) =>
                        {
                            //CheckSoLuong1();
                        };
                        break;
                    //_ton13.V6LostFocus += Ton13_V6LostFocus;
                    case "SO_LUONG1":
                        _soLuong1 = control as V6NumberTextBox;
                        if (_soLuong1 != null)
                        {
                            _soLuong1.V6LostFocus += (sender) =>
                            {
                                CheckSoLuong1();
                                chkSuaTienThue.Checked = false;
                                Tinh_thue_ct();
                            };

                            _soLuong1.LostFocus += delegate
                            {
                                CheckSoLuong1();
                            };

                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _soLuong1.ReadOnlyTag();
                            }
                        }
                        break;

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
                                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                            }
                        };
                        break;
                    case "GIA_NT2":
                        _giaNt2 = control as V6NumberTextBox;
                        if (_giaNt2 != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _giaNt2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _giaNt2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA2":
                        _gia2 = control as V6NumberTextBox;
                        if (_gia2 != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _gia2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _gia2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA21":
                        _gia21 = control as V6NumberTextBox;
                        if (_gia21 != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _gia21.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _gia21.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA_NT21":
                        _giaNt21 = control as V6NumberTextBox;
                        if (_giaNt21 != null)
                        {
                            _giaNt21.V6LostFocus += GiaNt21_V6LostFocus;
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _giaNt21.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
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

                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _tienNt2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _tienNt2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN2":
                        _tien2 = control as V6NumberTextBox;
                        if (_tien2 != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _tien2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _tien2.ReadOnlyTag();
                            }
                        }
                        break;

                    case "TIEN":
                        _tien = (V6NumberTextBox)control;
                        if (_tien != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _tien.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
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
                                TinhTienVon_GiaVon();
                                
                            };

                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _tienNt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _tienNt.ReadOnlyTag();
                            }
                        }
                        break;
                    case "CK":
                        _ck = control as V6NumberTextBox;
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
                                TinhChietKhauChiTiet(true, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
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
                                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
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
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _gia_nt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _gia_nt.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA":
                        _gia = control as V6NumberTextBox;
                        if (_gia != null)
                        {
                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _gia.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
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
                        _xuat_dd = control as V6CheckTextBox;
                        if (_xuat_dd != null)
                        {
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

                            if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                            {
                                _xuat_dd.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                            {
                                _xuat_dd.ReadOnlyTag();
                            }
                        }
                        break;

                    case "MA_LO":
                        _maLo = (V6VvarTextBox)control;
                        _maLo.GotFocus += (s, e) =>
                        {
                            _maLo.CheckNotEmpty = _maVt.LO_YN && _maKhoI.LO_YN;

                            _dataLoDate = V6BusinessHelper.GetLoDate(_maVt.Text, _maKhoI.Text, _sttRec, dateNgayCT.Value);
                            var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                            var getFilter = GetFilterMaLo(_dataLoDate, _sttRec0, _maVt.Text, _maKhoI.Text);
                            if (getFilter != "") filter += " and " + getFilter;
                            _maLo.SetInitFilter(filter);
                        };
                        //_maLo.V6LostFocus += delegate
                        //{
                        //    CheckMaLo();
                        //};
                        //_maLo.V6LostFocusNoChange += delegate
                        //{
                        //    XuLyLayThongTinKhiChonMaLo();
                        //    // tuanmh 30/07/2016
                        //    GetLoDate13();
                        //    if (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO))
                        //    {
                        //        if (_soLuong1.Value > _ton13.Value)
                        //        {
                        //            _soLuong1.Value = _ton13.Value;
                        //            TinhTienNt2();
                        //        }
                        //    }
                        //};
                        _maLo.LostFocus += (sender, args) =>
                        {
                            if (!_maLo.ReadOnly)
                            {
                                CheckMaLoTon();
                            }
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
                                _sl_qd.Enabled = false;
                                if (_sl_qd.Tag == null || _sl_qd.Tag.ToString() != "hide") _sl_qd.Tag = "disable";
                            }
                            else if (M_CAL_SL_QD_ALL == "1")
                            {
                                _sl_qd.EnableTag();
                            }
                            _sl_qd.V6LostFocus += delegate
                            {
                                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    CheckSoLuong1();
                                    chkSuaTienThue.Checked = false;
                                    Tinh_thue_ct();
                                }
                            };

                            if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
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
                        _hs_qd4.V6LostFocus += Hs_qd4_V6LostFocus;
                        break;
                    case "HS_QD3":
                        _hs_qd3 = (V6NumberTextBox)control;
                        _hs_qd3.V6LostFocus += Hs_qd3_V6LostFocus;
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

                    case "MA_VITRI":
                        _maViTri = control as V6VvarTextBox;
                        if (_maViTri != null)
                        {
                            _maViTri.GotFocus += (s, e) =>
                            {
                                _maViTri.CheckNotEmpty = _maVt.VITRI_YN;
                                var filter = "Ma_kho='" + _maKhoI.Text.Trim() + "'";

                                //if (("," + V6Options.V6OptionValues["M_LST_CT_DV"] + ",").Contains(MaCt))
                                //{
                                //    _dataViTri = Invoice.GetViTri(_maVt.Text, _maKhoI.Text, _sttRec, dateNgayCT.Value);
                                //    var getFilter = GetFilterMaViTri(_dataViTri, _sttRec0, _maVt.Text, _maKhoI.Text);
                                //    if (getFilter != "") filter += " and " + getFilter;
                                //}

                                _maViTri.SetInitFilter(filter);
                            };

                            _maViTri.V6LostFocus += sender =>
                            {
                                CheckMaViTri();
                            };
                        }
                        break;
                    case "MA_TD_I":
                        _maTdi = (V6VvarTextBox)control;
                        _maTdi.Upper();
                        _maTdi.FilterStart = true;
                        _maTdi.EnableTag(_m_Ma_td == "0");
                        break;

                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
            }
            
            foreach (Control control in dynamicControlList.Values)
            {
                detail1.AddControl(control);
            }
            
            detail1.SetStruct(Invoice.ADStruct);
            detail1.MODE = detail1.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
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

            //V6ControlFormHelper.SetFormStruct (detail3, Invoice.AD3Struct);
            detail3.SetStruct(Invoice.AD3Struct);
            detail3.MODE = detail3.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView3, _alct3Dic, _maNt, _mMaNt0);
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
                this.ShowErrorMessage(GetType() + ".XuLyDetail3ClickAdd: " + ex.Message);
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
                this.ShowErrorMessage(GetType() + ".TruDanTheoNhomDk: " + ex.Message);
            }
        }

        private void Detail3_AddHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data) && XuLyThemDetail3(data))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
        }

        private void Detail3_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data) && XuLySuaDetail3(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }
        private bool XuLySuaDetail3(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                        //Thêm thắt vài thứ
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Value.Date;


                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "")
                            error += "\nTài khoản rỗng.";
                        //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "")
                        //    error += "\nMã kho rỗng.";
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
                            TinhTongThanhToan("xy ly sua detail3");
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
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết3: " + ex.Message);
            }
            return true;
        }

        private bool XuLyThemDetail3(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec03 = V6BusinessHelper.GetNewSttRec0(AD3);
                data["STT_REC0"] = _sttRec03;
                data["STT_REC"] = _sttRec;
                //Thêm thắt vài thứ
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Value.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "") error += "\nChưa nhập tài khoản.";
                //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\nMã kho rỗng.";
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
                    TinhTongThanhToan("xu ly them detail3");

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
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết: " + ex.Message);
            }
            return true;
        }
        private bool ValidateData_Detail3(SortedDictionary<string, object> data)
        {
            try
            {
                if (_tk_i_33.Int_Data("Tk_cn") == 1 && data["MA_KH_I"].ToString().Trim() == "")
                {
                    this.ShowWarningMessage("Tài khoản công nợ thiếu mã khách hàng !");
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail3", ex);
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
                this.ShowErrorMessage(GetType() + ".Detail1_ClickEdit: " + ex.Message);
            }
        }

        private void Detail3_DeleteHandle(object sender)
        {
            XuLyDeleteDetail3();
        }
        private void XuLyDeleteDetail3()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                        var details = "Tài khoản: " + currentRow["TK_I"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" +
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết: " + ex.Message);
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

        private void CheckMaLoTon()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            if (!_maVt.LO_YN) return;
            //Fix Tuanmh 15/11/2017
            if (!_maKhoI.LO_YN) return;

            try
            {
                Invoice.GetAlLoTon(dateNgayCT.Value, _sttRec, _maVt.Text, _maKhoI.Text);
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
                            XuLyKhiNhanMaLo(row.ToDataDictionary());
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
                                    XuLyKhiNhanMaLo(((DataRow)_maLo.Tag).ToDataDictionary());
                                else if (_maLo.Tag is DataGridViewRow)
                                    XuLyKhiNhanMaLo(((DataGridViewRow)_maLo.Tag).ToDataDictionary());
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
                            _soLuong1.Value = _ton13.Value;
                            TinhTienNt2();
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
                this.ShowErrorMessage(GetType() + ".CheckLoTon: " + ex.Message);
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
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
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
                    decimal new_soLuong = data_soLuong;

                    foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                    {
                        string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                        string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                        //string c_maVi_Tri = row["Ma_vi_tri"].ToString().Trim().ToUpper();

                        decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                        if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                        {
                            if (data_maVt == c_maVt && data_maKhoI == c_maKhoI && data_maLo == c_maLo)
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
                    alLoTon.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FixAlLoTon " + ex.Message);
            }
        }

        private void XuLyKhiNhanMaLo(IDictionary<string, object> row)//, DataRow row0)
        {
            try
            {
                //_maLo.Text = row["MA_LO"].ToString().Trim();
                _hanSd.Value = ObjectAndString.ObjectToDate(row["HSD"]);
                _ton13.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU"]);
                _maLo.Enabled = true;
                //_maLo.ReadOnlyTag();

                CheckSoLuong1();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyKhiNhanMaVitri: " + ex.Message);
            }
        }

        private void CheckMaViTri()
        {
            if (_maKhoI.Text != "")
            {
                _maViTri.SetInitFilter("Ma_kho='" + _maKhoI.Text.Trim() + "'");
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

        private void XuLyLayThongTinKhiChonMaVitri()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaVitri " + ex.Message);
            }
        }

        private DataTable _dataViTri;
        private void GetViTri13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                string maViTri = _maViTri.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maViTri == "") return;

                _dataViTri = Invoice.GetViTri13(maVt, maKhoI, maViTri, _sttRec, dateNgayCT.Value);
                if (_dataViTri.Rows.Count == 0)
                {
                    _ton13.Value = 0;
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
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong < 0) new_soLuong = 0;
                        {
                            _ton13.Value = new_soLuong / _heSo1.Value;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetViTri13 " + ex.Message);
            }
        }

        private void GetViTriLoDate13()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                string maLo = _maLo.Text.Trim().ToUpper();
                string maViTri = _maViTri.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "" || maLo == "" || maViTri == "") return;

                _dataViTri = Invoice.GetViTriLoDate13(maVt, maKhoI, maLo, maViTri, _sttRec, dateNgayCT.Value);
                if (_dataViTri.Rows.Count == 0)
                {
                    _ton13.Value = 0;
                    _maLo.Clear();
                    _hanSd.Value = null;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
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
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo && maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong < 0) new_soLuong = 0;
                        {
                            _ton13.Value = new_soLuong / _heSo1.Value;
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetViTriLoDate13 " + ex.Message);
            }
        }

        private void GetViTri()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.VITRI_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = Invoice.GetViTri(maVt, maKhoI, _sttRec, dateNgayCT.Value);
                if (_dataViTri.Rows.Count == 0)
                {
                    _ton13.Value = 0;
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
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong < 0) new_soLuong = 0;
                        {
                            _ton13.Value = new_soLuong / _heSo1.Value;
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

        private void GetViTriLoDate()
        {
            try
            {
                string sttRec0 = _sttRec0;
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN
                    || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = Invoice.GetViTriLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Value);
                if (_dataViTri.Rows.Count == 0)
                {
                    _ton13.Value = 0;
                    _maLo.Clear();
                    _hanSd.Value = null;
                }
                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

                for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
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
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo && data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            _ton13.Value = new_soLuong / _heSo1.Value;
                            _maLo.Text = data_row["Ma_lo"].ToString().Trim();
                            _maViTri.Text = data_row["Ma_vitri"].ToString().Trim();
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                        else
                        {
                            ResetTonLoHsd(_ton13, _maLo, _hanSd);
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

        /// <summary>
        /// Check so luong roi tinh tien Nt2
        /// </summary>
        private void CheckSoLuong1()
        {
            try
            {
                if (!(IsReady && (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                      && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))) return;

                _maVt.RefreshLoDateYnValue();
                if (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO))
                {
                    if (_soLuong1.Value > _ton13.Value)
                    {
                        ShowParentMessage(V6Text.StockoutWarning);
                        _soLuong1.Value = _ton13.Value;
                    }
                }
                TinhTienNt2();
                TinhTienVon();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckSoLuong1: " + ex.Message);
            }
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
                TinhTienNt2();
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
                this.ShowErrorMessage(GetType() + ".SetTang: " + ex.Message);
            }
        }

        
        
        #endregion detail form

        #region ==== Override Methods ====
        
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese ?
                "F4-Nhận/thêm chi tiết,F7-Lưu và in." :
                "F4-Add details,F7-Save and print.");
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
                    detail1.OnMoiClick();//.btnMoi.PerformClick();
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
        
        private void MaVatTu_V6LostFocus(object sender)
        {
            chkSuaTienThue.Checked = false;
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

        void TienNt2_V6LostFocus(object sender)
        {
            TinhGiaNt2_TienNt2();
        }

        void GiaNt21_V6LostFocus(object sender)
        {
            chkSuaTienThue.Checked = false;
            TinhTienNt2();
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
                    txtDiaChiGiaoHang.ParentData = null;
                    txtMaSoThue.Text = "";
                    txtTenKh.Text = "";
                    txtDiaChi.Text = "";
                    //txtMaGia.Text = "";
                    SetControlValue(txtMaGia, null, Invoice.GetTemplateSettingAM("MA_GIA"));
                    txtDienThoaiKH.Text = "";
                    return;
                }

                txtDiaChiGiaoHang.ParentData = data.ToDataDictionary();
                txtDiaChiGiaoHang.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));

                var mst = (data["ma_so_thue"] ?? "").ToString().Trim();
                txtMaSoThue.Text = mst;
                txtTenKh.Text = (data["ten_kh"] ?? "").ToString().Trim();
                txtDiaChi.Text = (data["dia_chi"] ?? "").ToString().Trim();
                txtDienThoaiKH.Text = (data["dien_thoai"] ?? "").ToString().Trim();
                txtHanTT.Value = ObjectAndString.ObjectToInt(data["han_tt"]);
                // Tuanmh 28/05/2016
                //txtMaGia.Text = (data["MA_GIA"] ?? "").ToString().Trim();
                SetControlValue(txtMaGia, data["MA_GIA"], Invoice.GetTemplateSettingAM("MA_GIA"));

                //Lay thong tin gan du lieu 20170320
                SetDefaultDataReference(Invoice, ItemID, "TXTMAKH", data);
                
                ////Lay thong tin gan du lieu 20161129
                //var infos = Invoice.LoadDataReferenceInfo(V6Setting.Language, ItemID);
                ////Duyet txtmakh
                ////from ONG_BA to DOI_TAC
                ////data[to] = from
                //SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
                //foreach (KeyValuePair<string, string> item in infos)
                //{
                //    if (item.Value.StartsWith("TXTMAKH."))
                //    {
                //        var getField = item.Value.Split('.')[1].Trim();
                //        if (data.Table.Columns.Contains(getField))
                //        {
                //            someData[item.Key] = data[getField];
                //        }
                //    }
                //}
                //SetSomeData(someData);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonMaKhachHang: " + ex.Message);
            }
        }

        private void XuLyChonMaHttt()
        {
            try
            {
              var data = txtMaHttt.Data;
                if (data == null)
                {
                    return;
                }
                
                SetDefaultDataReference(Invoice, ItemID, "TXTMAHTTT", data);

                ////Lay thong tin gan du lieu 20161129
                //var infos = Invoice.LoadDataReferenceInfo(V6Setting.Language, ItemID);
                ////Duyet txtmahttt
                ////from TK_NO to MA_NX
                ////data[to] = from
                ////Chuẩn bị dữ liệu để gán lên form
                //SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
                //foreach (DefaultValueInfo item in infos)
                //{
                //    if (item.Type1 == "0")
                //    {
                //        //Value null vẫn gán. không làm gì hết.
                //    }
                //    else if (item.Type1 == "1")
                //    {
                //        //Value khác null mới gán
                //        if (string.IsNullOrEmpty(item.Value)) continue;
                //    }
                //    else if (item.Type1 == "2")
                //    {
                //        //Kiểm tra value trên form theo Name. rỗng mới gán
                //        var fValue = V6ControlFormHelper.GetFormValue(this, item.Name).ToString().Trim();
                //        if (!string.IsNullOrEmpty(fValue)) continue;
                //    }

                //    if (item.Value.StartsWith("TXTMAHTTT."))//Lấy dữ liệu theo trường nào đó trong txtMaHttt.Data
                //    {
                //        var getField = item.Value.Split('.')[1].Trim();
                //        if (data.Table.Columns.Contains(getField))
                //        {
                //            someData[item.Name.ToUpper()] = item.Value;
                //        }
                //    }
                //    //else
                //    //{
                //    //    someData[item.Name.ToUpper()] = item.Value;
                //    //}
                //}
                //SetSomeData(someData);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonMaHttt: " + ex.Message);
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
                        txtMaSoThue.Enabled = false;

                        txtDiaChi.ReadOnlyTag();
                        txtDiaChi.TabStop = false;
                        txtTenKh.ReadOnlyTag();
                        txtTenKh.TabStop = false;
                    }
                    else
                    {
                        txtTenKh.Enabled = true;
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
                    txtMaSoThue.Enabled = true;

                    txtDiaChi.ReadOnlyTag(false);
                    txtDiaChi.TabStop = true;
                    txtTenKh.ReadOnlyTag(false);
                    txtTenKh.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyKhoaThongTinKhachHang: " + ex.Message);
            }
        }
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
            GetTon13();
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
                if (_maLo.Text == "") GetLoDate();
                else GetLoDate13();
            }
        }

        private void XuLyChonMaVt(string mavt)
        {
            XuLyLayThongTinKhiChonMaVt();
            XuLyDonViTinhKhiChonMaVt(mavt);

            //{Tuanmh 14-09/2017 get tk_dl from alkho
            if ( _maKhoI.Text !="")
                XuLyLayThongTinKhiChonMaKhoI();
            //}

            GetGia();
            GetTon13();
            TinhTienNt2();
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

            Tinh_thue_ct();
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
                this.WriteExLog(GetType() + ".XuLyLayThongTinKhiChonMaKhoI", ex);
            }
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
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaLo" + ex.Message);
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

                _dataLoDate = V6BusinessHelper.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Value);
                if (_dataLoDate.Rows.Count == 0)
                {
                    _ton13.Value = 0;
                    _maLo.Clear();
                    _hanSd.Value = null;
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
                    if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo)
                    {
                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal new_soLuong = data_soLuong;

                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {

                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();

                            //Add 31-07-2016
                            //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                            //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong < 0) new_soLuong = 0;
                        {
                            if (_heSo1.Value != 0)
                            {
                                _ton13.Value = new_soLuong/_heSo1.Value;
                            }
                            else
                            {
                                _ton13.Value = new_soLuong;
                            }

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
                var list_maLo ="";
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
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();

                            //Add 31-07-2016
                            //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                            //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);
                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo)
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
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                
                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataLoDate = V6BusinessHelper.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Value);
                if (_dataLoDate.Rows.Count == 0)
                {
                    ResetTonLoHsd(_ton13, _maLo, _hanSd);
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
                    if(data_maLo == "") continue;

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
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);

                            //Add 31-07-2016
                            //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                            //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                            if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                            {
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            _ton13.Value = new_soLuong/_heSo1.Value;
                            _maLo.Text = data_row["Ma_lo"].ToString().Trim();
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                        else
                        {
                            ResetTonLoHsd(_ton13, _maLo, _hanSd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetTon13()
        {
            try
            {
                if ((_maVt.LO_YN || _maVt.DATE_YN) && (_maKhoI.LO_YN || _maKhoI.DATE_YN))
                    return;

                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataLoDate = Invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Value);
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
                                decimal new_soLuong = data_soLuong;

                                foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                                {
                                    string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                                    string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                                    string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                                    
                                    decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]);

                                    //Add 31-07-2016
                                    //Nếu khi sửa chỉ trừ dần những dòng trên dòng đang đứng thì dùng dòng if sau:
                                    //if (detail1.MODE == V6Mode.Edit && c_sttRec0 == sttRec0) break;

                                    if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                                    {
                                        if (maVt == c_maVt && maKhoI == c_maKhoI)
                                        {
                                            new_soLuong -= c_soLuong;
                                        }
                                    }
                                }

                                if (new_soLuong < 0) new_soLuong = 0;
                                {
                                    _ton13.Value = new_soLuong / _heSo1.Value;
                                    break;
                                }
                            }
                        }

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

        private void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
                _maVt.RefreshLoDateYnValue();
                var data = _maVt.Data;
                if (data == null)
                {
                    //_tkGv.Text = "";
                    //_tkDt.Text = "";
                    SetControlValue(_tkDt, "", Invoice.GetTemplateSettingAD("TK_DT"));
                    SetControlValue(_tkGv, "", Invoice.GetTemplateSettingAD("TK_GV"));
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
                    //_tkDt.Text = (data["tk_dt"] ?? "").ToString().Trim();
                    //_tkGv.Text = (data["tk_gv"] ?? "").ToString().Trim();
                    SetControlValue(_tkDt, data["tk_dt"], Invoice.GetTemplateSettingAD("TK_DT"));
                    SetControlValue(_tkGv, data["tk_gv"], Invoice.GetTemplateSettingAD("TK_GV"));
                    SetControlValue(_soLuong1, data["PACKS1"], Invoice.GetTemplateSettingAD("PACKS1"));

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
                            txtTkThueCo.Text = _tk_thue_i.Text;
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

                if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                {
                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        _maTdi.Text = Txtma_td_ph.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaVt " + ex.Message);
            }
        }

        /// <summary>
        /// Setinitfilter, readonly-tag...
        /// </summary>
        /// <param name="mavt">Giá trị hiện tại của ô _mavt</param>
        /// <param name="changeMavt">Fix trạng thái của dvt khi sửa focusDvt=false</param>
        private void XuLyDonViTinhKhiChonMaVt(string mavt, bool changeMavt=true)
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
                        if(changeMavt) _heSo1.Value = 1;
                        
                    }
                    else
                    {
                        _dvt1.Tag = "readonly";
                        _dvt1.ReadOnly = true;
                        if(changeMavt) _dvt1.Focus();
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
                this.ShowErrorMessage(GetType() + ".XuLyDonViTinhKhiChonMaVt " + ex.Message);
            }
        }


        private void GetGia()
        {
            try
            {
                var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Value,
                        cboMaNt.SelectedValue.ToString().Trim(), _maVt.Text, _dvt1.Text, txtMaKh.Text, txtMaGia.Text);
                _giaNt21.Value = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                //ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                if (_dvt.Text.ToUpper().Trim() == _dvt1.Text.ToUpper().Trim())
                    _giaNt2.Value = _giaNt21.Value;
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
                this.ShowErrorMessage(GetType() + ".GetGia: " + ex.Message);
            }
        }

        private void XuLyThayDoiDvt1()
        {
            if (_dvt1.Data != null)
            {
                var he_so = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_so"]);
                if (he_so == 0) he_so = 1;
                _heSo1.Value = he_so;

                GetTon13();
                if (_maKhoI.Text.Trim() != "" && _maLo.Text.Trim() != "")
                {
                    GetLoDate13();
                }

                GetGia();
                CheckSoLuong1();
                TinhTienNt2();
            }
            else
            {
                _heSo1.Value = 1;
            }
        }

        private void TinhTienNt2()
        {
            try
            {
                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);

                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                _tienNt2.Value = V6BusinessHelper.Vround((_soLuong1.Value * _giaNt21.Value), M_ROUND_NT);
                _tien2.Value = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien2.Value = _tienNt2.Value;

                }

                TinhChietKhauChiTiet(false, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
                TinhGiaNt2();
                TinhVanChuyen();
                TinhGiamGiaCt();

                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienNt2", ex);
            }
        }

        private void TinhGiamGiaCt()
        {
            try
            {
                if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "2" ||
                    V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
                {
                    _ggNt.Value = V6BusinessHelper.Vround((_soLuong1.Value*_hs_qd4.Value), M_ROUND_NT);
                    _gg.Value = V6BusinessHelper.Vround((_ggNt.Value*txtTyGia.Value), M_ROUND);

                    if (_maNt == _mMaNt0)
                    {
                        _gg.Value = _ggNt.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiamGia: " + ex.Message);
            }
        }

        private void TinhVanChuyen()
        {
            try
            {
                if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "1" ||
                    V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
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
                this.ShowErrorException(GetType() + ".TinhVanChuyen", ex);
            }
        }
        
        private void TinhTienVon()
        {
            TinhGiaNt();

            _tienNt.Value = V6BusinessHelper.Vround((_soLuong.Value * _gia_nt.Value), M_ROUND_NT);
            _tien.Value = V6BusinessHelper.Vround((_tienNt.Value * txtTyGia.Value), M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }
        }
       
        private void TinhTienVon_GiaVon()
        {
            _tien.Value = V6BusinessHelper.Vround((_tienNt.Value * txtTyGia.Value), M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }

            if (_soLuong.Value != 0)
            {

                _gia_nt.Value = V6BusinessHelper.Vround((_tienNt.Value / _soLuong.Value), M_ROUND_GIA_NT);
                _gia.Value = V6BusinessHelper.Vround((_tien.Value / _soLuong.Value), M_ROUND_GIA);

                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _gia_nt.Value;

                }
            }
        }

        private void TinhGiaNt2()
        {
            try
            {

                _gia21.Value = V6BusinessHelper.Vround((_giaNt21.Value*txtTyGia.Value), M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia21.Value = _giaNt21.Value;
                }


                if (_soLuong.Value != 0)
                {
                    _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value/_soLuong.Value), M_ROUND_GIA_NT);

                    _gia2.Value = V6BusinessHelper.Vround((_tien2.Value/_soLuong.Value), M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        _gia2.Value = _giaNt21.Value;
                        _giaNt2.Value = _giaNt21.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaNt2: " + ex.Message);
            }
        }

        private void TinhGiaNt()
        {
            try
            {

                _gia.Value = V6BusinessHelper.Vround((_gia_nt.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _gia_nt.Value;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaNt: " + ex.Message);
            }
        }

        /// <summary>
        /// chay khi nhap tien
        /// </summary>
        private void TinhGiaNt2_TienNt2()
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
                    if (_maNt == _mMaNt0)
                    {
                        _gia21.Value = _giaNt21.Value;
                    }
                }

                if (_soLuong.Value != 0)
                {
                    _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong.Value),M_ROUND_GIA_NT);

                    _gia2.Value = V6BusinessHelper.Vround((_tien2.Value / _soLuong.Value), M_ROUND_GIA);
                    
                    if (_maNt == _mMaNt0)
                    {
                        _gia2.Value = _giaNt21.Value;
                        _giaNt2.Value = _giaNt21.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaNt2_TienNt2: " + ex.Message);
            }
        }
        #endregion methods
        

        #endregion detail events

        
        #region ==== Show Hide Enable Disable controls ====

        protected override void EnableFormControls()
        {
            try
            {
                var readOnly = Mode != V6Mode.Edit && Mode != V6Mode.Add;
                V6ControlFormHelper.SetFormControlsReadOnly(this, readOnly);

                if (readOnly)
                {
                    detail1.MODE = V6Mode.Lock;
                    detail3.MODE = V6Mode.Lock;

                    ChonDonHangBanMenu.Enabled = false;
                    chonBaoGiaToolStripMenuItem.Enabled = false;
                    chonTuExcelToolStripMenuItem.Enabled = false;
                    chonPhieuNhapToolStripMenuItem.Enabled = false;
                }
                else
                {
                    ChonDonHangBanMenu.Enabled = true;
                    chonBaoGiaToolStripMenuItem.Enabled = true;
                    chonTuExcelToolStripMenuItem.Enabled = true;
                    chonPhieuNhapToolStripMenuItem.Enabled = true;

                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _tienNt2.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;

                    

                    //{Tuanmh 02/12/2016
                    _ckNt.Enabled = !chkLoaiChietKhau.Checked;
                    _ck.Enabled = !chkLoaiChietKhau.Checked && _maNt != _mMaNt0;

                    _gia21.Enabled = chkSuaTien.Checked && _giaNt21.Value == 0 && _maNt != _mMaNt0;

                    bool is_gia_dichdanh = _maVt.GIA_TON == 2 || _xuat_dd.Text != "";
                    
                    _tienNt.Enabled = chkSuaTien.Checked && is_gia_dichdanh;
                    _tien.Enabled = is_gia_dichdanh && _tienNt.Value == 0 && _maNt != _mMaNt0;
                    
                    _gia_nt.Enabled = is_gia_dichdanh;
                    _gia.Enabled = is_gia_dichdanh && _gia_nt.Value == 0 && _maNt != _mMaNt0;
                    //}


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
                        txtTongThue.ReadOnly = !chkSuaTienThue.Checked;
                        txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;
                    }
                }

                //Cac truong hop khac
                if (!readOnly)
                {
                    chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
                    txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;

                    if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "2" ||
                       V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
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

                    if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "1" ||
                       V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
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
                this.ShowErrorMessage(GetType() + ".EnableFormControls: " + ex.Message);
            }
        }

        #region ==== DataGridView ====
        /// <summary>
        /// Gán dữ liệu sau đó sắp xếp và format lại GridView
        /// </summary>
        private void SetGridViewData()
        {
            HienThiTongSoDong(lblTongSoDong);
            dataGridView1.TableSource = AD;
            dataGridView3.TableSource = AD3;
            ReorderDataGridViewColumns();
            GridViewFormat();
        }
        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView3, _orderList3);
        }

        private void GridViewFormat()
        {
            var f = dataGridView1.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }
            f = dataGridView1.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }

            f = dataGridView1.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
            }
            f = dataGridView1.Columns["HS_QD1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
            }
            f = dataGridView1.Columns["HS_QD2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
            }
            f = dataGridView1.Columns["HS_QD3"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
            }
            f = dataGridView1.Columns["HS_QD4"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
            }

            f = dataGridView1.Columns["GIA2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }
            f = dataGridView1.Columns["GIA21"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }
            f = dataGridView1.Columns["GIA"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }
            f = dataGridView1.Columns["GIA_NT2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView1.Columns["GIA_NT21"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView1.Columns["GIA_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView1.Columns["TIEN2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }
            f = dataGridView1.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }
            f = dataGridView1.Columns["TIEN_NT2"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIENNT"];
            }
            f = dataGridView1.Columns["CK_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIENNT"];
            }
            f = dataGridView1.Columns["CK"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }

            //GridView3
            f = dataGridView3.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }
            f = dataGridView3.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }

            f = dataGridView3.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["N6"];
            }

            f = dataGridView3.Columns["GIA01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }
            f = dataGridView3.Columns["GIA"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIA"];
            }
            f = dataGridView3.Columns["GIA_NT0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView3.Columns["GIA_NT01"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView3.Columns["GIA_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_GIANT"];
            }
            f = dataGridView3.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }
            f = dataGridView3.Columns["TIEN0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
            }
            f = dataGridView3.Columns["TIEN_NT0"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIENNT"];
            }
            f = dataGridView3.Columns["CK_NT"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIENNT"];
            }
            f = dataGridView3.Columns["CK"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
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
            var tSoLuong = TinhTong(AD, "SO_LUONG1");
            txtTongSoLuong.Value = V6BusinessHelper.Vround(tSoLuong, M_ROUND_NUM);

            var tPsNoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_NO_NT", "OPER_TT");
            var tPsCoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_CO_NT", "OPER_TT");
            txtTongTangGiamNt.Value = tPsNoNt;
            var tTienNt2 = TinhTong(AD, "TIEN_NT2");
            txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2 + tPsNoNt, M_ROUND_NT);

            var tPsNo = V6BusinessHelper.TinhTongOper(AD3, "PS_NO", "OPER_TT");
            var tPsCo = V6BusinessHelper.TinhTongOper(AD3, "PS_CO", "OPER_TT");
            txtTongTangGiam.Value = tPsNo;
            var tTien2 = TinhTong(AD, "TIEN2");
            txtTongTien2.Value = V6BusinessHelper.Vround(tTien2 + tPsNo, M_ROUND);

            var tTienNt = TinhTong(AD, "TIEN_NT");
            txtTongTienNt.Value = V6BusinessHelper.Vround(tTienNt, M_ROUND_NT);
            var tTien = TinhTong(AD, "TIEN");
            txtTongTien.Value = V6BusinessHelper.Vround(tTien, M_ROUND);

            //{ Tuanmh 01/07/2016
            if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "2" ||
                V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
            {
                var t_gg_nt = TinhTong(AD, "GG_NT");
                var t_gg = TinhTong(AD, "GG");
                txtTongGiamNt.Value = V6BusinessHelper.Vround(t_gg_nt, M_ROUND_NT);
                txtTongGiam.Value = V6BusinessHelper.Vround(t_gg, M_ROUND);
            }

            if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "1" ||
                V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
            {
                var t_vc_nt = TinhTong(AD, "TIEN_VC_NT");
                var t_vc = TinhTong(AD, "TIEN_VC");
                TxtT_TIENVCNT.Value = V6BusinessHelper.Vround(t_vc_nt, M_ROUND_NT);
                TxtT_TIENVC.Value = V6BusinessHelper.Vround(t_vc, M_ROUND);
            }
            //}
    
        }


        private void TinhChietKhau()
        {
            try
            {
                var tTienNt2 = V6BusinessHelper.TinhTong(AD, "TIEN_NT2");
                var tyGia = txtTyGia.Value;
                var t_tien_nt2 = txtTongTienNt2.Value;
                txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);
                decimal t_ck_nt = 0, t_ck=0;



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
                        t_ck_nt = V6BusinessHelper.Vround(ptck * tTienNt2 / 100, M_ROUND_NT);
                        t_ck = V6BusinessHelper.Vround(t_ck_nt * tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;

                        txtTongCkNt.Value = t_ck_nt;
                        txtTongCk.Value = t_ck;
                    }

                    else if (chkSuaTienCk.Checked)
                    {
                        t_ck_nt = txtTongCkNt.Value;
                        t_ck = V6BusinessHelper.Vround(t_ck_nt * tyGia, M_ROUND);

                        if (_maNt == _mMaNt0)
                            t_ck = t_ck_nt;
                        txtTongCk.Value = t_ck;



                    }
                    //tính chiết khấu cho mỗi chi tiết

                    var t_ck_nt_check = 0m;
                    var t_ck_check = 0m;
                    var index_ck = -1;

                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt2 != 0)
                        {
                            var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt2"]);
                            var ck_nt = V6BusinessHelper.Vround(tien_nt2 * t_ck_nt / t_tien_nt2, M_ROUND_NT);
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

                        }
                    }
                    // Xu ly chenh lech
                    // Tìm dòng có số tiền
                    if (index_ck != -1)
                    {
                        decimal _ck_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CK_NT"]) + (t_ck_nt - t_ck_nt_check);
                        AD.Rows[index_ck]["CK_NT"] = _ck_nt;

                        decimal _ck = ObjectAndString.ObjectToDecimal(AD.Rows[index_ck]["CK"]) + (t_ck - t_ck_check);
                        AD.Rows[index_ck]["CK"] = _ck;
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
                this.ShowErrorMessage(GetType() + ".TinhChietKhau: " + ex.Message);
            }
        }
        private void TinhPhanBoGiamGia()
        {
            try
            {

                if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "2" ||
                    V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
                {
                    return;
                }
                
                decimal t_gg_nt = 0, t_gg = 0;
                
                t_gg_nt = txtTongGiamNt.Value;
                t_gg = txtTongGiam.Value;
                
                    //tính giam gia cho mỗi chi tiết

                var tTienNt2 = V6BusinessHelper.TinhTong(AD, "TIEN_NT2");
                var tyGia = txtTyGia.Value;
                var t_tien_nt2 = txtTongTienNt2.Value;
                txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);

                    var t_gg_nt_check = 0m;
                    var t_gg_check = 0m;
                    var index_gg = -1;

                    for (var i = 0; i < AD.Rows.Count; i++)
                    {
                        if (t_tien_nt2 != 0)
                        {
                            var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt2"]);
                            var gg_nt = V6BusinessHelper.Vround(tien_nt2 * t_gg_nt / t_tien_nt2, M_ROUND_NT);
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
                this.ShowErrorMessage(GetType() + ".TinhPhanBoGiamGia: " + ex.Message);
            }
        }
        

        private void TinhThue()
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

            if (chkSuaTienThue.Checked)//Tiền thuế gõ tự do
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
                t_thue_nt = t_tien_truocthue*thue_suat/100;
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
                if (t_tien_truocthue != 0)
                {
                    var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN_NT2"])
                        + ObjectAndString.ObjectToDecimal(AD.Rows[i]["TIEN_VC_NT"]) - ObjectAndString.ObjectToDecimal(AD.Rows[i]["CK_NT"]) - ObjectAndString.ObjectToDecimal(AD.Rows[i]["GG_NT"]);
                    var thue_nt = V6BusinessHelper.Vround(tien_nt2 * t_thue_nt / t_tien_truocthue, M_ROUND);
                    t_thue_nt_check = t_thue_nt_check + thue_nt;

                    var thue = V6BusinessHelper.Vround(thue_nt * ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thue = thue_nt;
                    t_thue_check += thue;

                    if (thue_nt != 0 && index == -1)
                        index = i;

                    if (!AD.Columns.Contains("Thue_nt")) AD.Columns.Add("Thue_nt", typeof(decimal));
                    if (!AD.Columns.Contains("Thue")) AD.Columns.Add("Thue", typeof(decimal));
                    AD.Rows[i]["Thue_nt"] = thue_nt;
                    AD.Rows[i]["Thue"] = thue;
                }
                else
                {
                    AD.Rows[i]["Thue_nt"] = 0m;
                    AD.Rows[i]["Thue"] = 0m;
                }
            }
            
            // Xu ly chenh lech
            // Tìm dòng có số tiền
            if (index != -1)
            {
                decimal thue_nt = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Thue_nt"]) + (t_thue_nt - t_thue_nt_check);
                AD.Rows[index]["Thue_nt"] = thue_nt;

                decimal thue = ObjectAndString.ObjectToDecimal(AD.Rows[index]["Thue"]) + (t_thue - t_thue_check);
                AD.Rows[index]["Thue"] = thue;
            }
        }

        // Tuanmh 08/04/2018 Tinh lai khi thue suat chi tiet tung mat hang
        private void TinhLaiTienThueCT()
        {
            if (chkSuaTienThue.Checked) return;

            //Tính tiền thuế theo thuế suất
            decimal thue_suati;
            decimal thue_nti;
            decimal thuei;
            decimal tien_truocthue_nti;

            var ty_gia = txtTyGia.Value;
            var temp_maVt = new V6VvarTextBox {VVar = "MA_VT"};

            //tính thuế riêng cho từng chi tiết
            for (var i = 0; i < AD.Rows.Count; i++)
            {
                var row = AD.Rows[i];
                temp_maVt.Text = ObjectAndString.ObjectToString(row["MA_VT"]).Trim();

                tien_truocthue_nti = ObjectAndString.ObjectToDecimal(row["TIEN_NT2"])
                                     + ObjectAndString.ObjectToDecimal(row["TIEN_VC_NT"])
                                     - ObjectAndString.ObjectToDecimal(row["CK_NT"])
                                     - ObjectAndString.ObjectToDecimal(row["GG_NT"]);
                
                //string mathuei = row["MA_THUE_I"].ToString().Trim();
                //if (string.IsNullOrEmpty(mathuei))
                {
                    var mavt_data = temp_maVt.Data;
                    if (mavt_data != null)
                    {
                        var mathue = mavt_data["MA_THUE"].ToString().Trim();
                        if (!string.IsNullOrEmpty(mathue))
                        {
                            row["MA_THUE_I"] = mathue;
                            row["THUE_SUAT_I"] = ObjectAndString.ObjectToDecimal(mavt_data["THUE_SUAT"]);

                            var alThue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE = '" + mathue + "'");
                            if (alThue.TotalRows > 0)
                            {
                                var tk_thue_i_Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                                row["TK_THUE_I"] = tk_thue_i_Text;
                                txtTkThueCo.Text = tk_thue_i_Text;
                            }
                        }
                    }
                }

                thue_suati = ObjectAndString.ObjectToDecimal(row["THUE_SUAT_I"]);

                if (tien_truocthue_nti != 0)
                {
                    thue_nti = V6BusinessHelper.Vround(tien_truocthue_nti*thue_suati/100, M_ROUND_NT);
                    thuei = V6BusinessHelper.Vround(thue_nti*ty_gia, M_ROUND);

                    if (_maNt == _mMaNt0)
                        thuei = thue_nti;


                    if (!AD.Columns.Contains("Thue_nt")) AD.Columns.Add("Thue_nt", typeof (decimal));
                    if (!AD.Columns.Contains("Thue")) AD.Columns.Add("Thue", typeof (decimal));

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

        public void TinhTongThanhToan(string debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
            
                HienThiTongSoDong(lblTongSoDong);
                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt2, T_CK_NT, PT_CK
                TinhPhanBoGiamGia();//Tuanmh bo sung 05/12/2017

                if (M_SOA_MULTI_VAT == "1")
                {
                    TinhLaiTienThueCT();
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

                var t_tt = txtTongTien2.Value - txtTongGiam.Value - txtTongCk.Value + txtTongThue.Value + TxtT_TIENVC.Value;
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                
                txtConLai.Value = t_tt_nt - txtSL_UD1.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TTTT " + _sttRec, ex, "Lỗi-TTT("+debug+")");
            }
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
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void LoadAlnt()
        {
            try
            {
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
                cboMaNt.DataSource = Invoice.Alnt;
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.Language=="V"?"Ten_nt":"Ten_nt2";
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
                cboKieuPost.DisplayMember = V6Setting.Language=="V"?"Ten_post":"Ten_post2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetM_ma_nt0()
        {
            _mMaNt0 = V6Options.V6OptionValues["M_MA_NT0"];
            //cboMaNt.SelectedValue = _mMaNt0;
            panelVND.Visible = false;
            TxtT_TIENVC.Visible = false;
        }

        private void GetTyGiaDefault()
        {
            var getMant = Invoice.Alct.Rows[0]["ma_nt"].ToString().Trim();
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
            txtTyGia.Value = Invoice.GetTyGia(_maNt, dateNgayCT.Value);
        }

        private void GetDefault_Other()
        {
            txtMa_ct.Text = Invoice.Mact;
            dateNgayCT.Value = V6Setting.M_SV_DATE;
            dateNgayLCT.Value = V6Setting.M_SV_DATE;
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
            Txtma_nk.Text = Invoice.Alct.Rows[0]["M_MA_NK"].ToString().Trim();
            //txtLoaiPhieu.Text = "1";
            //
            txtManx.Text = Invoice.Alct.Rows[0]["TK_NO"].ToString().Trim();
            txtTkThueNo.Text = txtManx.Text;
            try
            {
                cboKieuPost.SelectedValue = Invoice.Alct.Rows[0]["M_K_POST"].ToString().Trim();
            }
            catch
            {
                // ignored
            }
            
            if (AM_old != null)
            {
                txtMa_sonb.Text = AM_old["Ma_sonb"].ToString().Trim();
                if (TxtSo_ct.Text.Trim()=="")
                        TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

                if (txtso_seri.Text.Trim() == "")
                    txtso_seri.Text = AM_old["SO_SERI"].ToString().Trim();

            }
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
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaDVCS", ex);
            }
        }

        private void XuLyThayDoiMaNt()
        {
            try
            {
                if (!_ready0) return;
                
                var newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _maNt;
                _giaNt21.GrayText = newText;
                var viewColumn = dataGridView1.Columns["GIA_NT21"];
                if (viewColumn != null) viewColumn.HeaderText = newText;

                var column = dataGridView1.Columns["TIEN_NT2"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _maNt;
                if (column != null) column.HeaderText = newText;
                if(_tienNt2 != null) _tienNt2.GrayText = newText;

                column = dataGridView1.Columns["THUE_NT"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thuế " : "Tax ") + _maNt;
                column = dataGridView1.Columns["THUE"];
                if (column != null)
                    column.HeaderText = (V6Setting.IsVietnamese ? "Thuế " : "Tax ") + _mMaNt0;

                viewColumn = dataGridView1.Columns["GIA21"];
                newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                if (viewColumn != null) viewColumn.HeaderText = newText;
                if (_gia21 != null) _gia21.GrayText = newText;

                column = dataGridView1.Columns["TIEN2"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _mMaNt0;
                if (column != null) column.HeaderText = newText;
                if (_tien2 != null) _tien2.GrayText = newText;

                //GridView3
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
                    detail1.ShowIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "THUE", "lblTHUE" });
                    panelVND.Visible = true;
                    

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA21");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var gridViewColumn = dataGridView1.Columns["GIA21"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    gridViewColumn = dataGridView1.Columns["TIEN2"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;
                    gridViewColumn = dataGridView1.Columns["THUE"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    gridViewColumn = dataGridView1.Columns["GIA"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    gridViewColumn = dataGridView1.Columns["GIA2"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    if (chkLoaiChietKhau.Checked)
                    {
                        gridViewColumn = dataGridView1.Columns["CK"];
                        if (gridViewColumn != null) gridViewColumn.Visible = true;
                    }

                    gridViewColumn = dataGridView1.Columns["TIEN_VC"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;
                    gridViewColumn = dataGridView1.Columns["GG"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongThueNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    //TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    //TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    TxtT_TIENVCNT.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongGiamNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongTienNt2.DecimalPlaces = V6Options.M_IP_TIEN_NT;

                    // Show Dynamic control
                    _gia2.VisibleTag();
                    _gia21.VisibleTag();
                    _tien2.VisibleTag();
                    _thue.VisibleTag();
                    _gia21.VisibleTag();
                    _ck.VisibleTag();
                    _tien_vc.VisibleTag();
                    _gg.VisibleTag();
                    if (_gia != null) _gia.VisibleTag();

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
                    
                    detail1.HideIDs(new[] { "GIA21", "TIEN2" });
                    panelVND.Visible = false;
                    TxtT_TIENVC.Visible = false;
                    
                    var gridViewColumn = dataGridView1.Columns["GIA21"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    gridViewColumn = dataGridView1.Columns["TIEN2"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    gridViewColumn = dataGridView1.Columns["THUE"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    gridViewColumn = dataGridView1.Columns["GIA2"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    gridViewColumn = dataGridView1.Columns["GIA"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    if (chkLoaiChietKhau.Checked)
                    {
                        gridViewColumn = dataGridView1.Columns["CK"];
                        if (gridViewColumn != null) gridViewColumn.Visible = false;
                    }

                    gridViewColumn = dataGridView1.Columns["TIEN_VC"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    gridViewColumn = dataGridView1.Columns["GG"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    txtTongCkNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongThueNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    //TxtT_cp_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                    //TxtT_cp_nt_ao.DecimalPlaces = V6Options.M_IP_TIEN;
                    TxtT_TIENVCNT.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongGiamNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongTienNt2.DecimalPlaces = V6Options.M_IP_TIEN;

                    ////Hide Dynamic control
                    _gia2.InvisibleTag();
                    _gia21.InvisibleTag();
                    _tien2.InvisibleTag();
                    _thue.InvisibleTag();
                    _gia21.InvisibleTag();
                    _ck.InvisibleTag();
                    _tien_vc.InvisibleTag();
                    _gg.InvisibleTag();
                    if (_gia != null) _gia.InvisibleTag();
                    
                    //Detail3
                    detail3.HideIDs(new[] { "PS_NO", "PS_CO"});

                    gridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    // Show Dynamic control
                    if(_PsNoNt_33 != null) _PsNo_33.InvisibleTag();
                    if (_PsCo_33 != null) _PsCo_33.InvisibleTag();
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
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaNt", ex);
            }
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
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Gán thue_suat_i.Value = alThue.Data.Rows[0][THUE_SUAT] = " + alThue.Data.Rows[0]["THUE_SUAT"]);

                    txtTkThueCo.Text = _tk_thue_i.Text;
                }
                else
                {
                    V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - ko Gán thue_suat_i.Value vì alThueData không có.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue_i", ex);
            }
            //TinhTongThanhToan("XuLyThayDoiMaThue_i");
        }

        private void Tinh_thue_ct()
        {
            V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - M_SOA_MULTI_VAT = " + M_SOA_MULTI_VAT); 
            if (M_SOA_MULTI_VAT == "1")
            {
                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat_i.Value, _tienNt2.Value - _ckNt.Value - _ggNt.Value, _tien2.Value - _ck.Value - _gg.Value, _thue_nt, _thue);
                V6ControlFormHelper.AddLastAction("\n" + MethodBase.GetCurrentMethod().Name + " - Tính thuế ct M_SOA_MULTY_VAT = 1.");
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
                    if(textBox != null)
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


                _PsNoNt_33.DecimalPlaces = decimalPlaces;
                _PsCoNt_33.DecimalPlaces = decimalPlaces;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberControl", ex);
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

                //GridView3
                column = dataGridView3.Columns["Ps_co_nt"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberGridView", ex);
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
                    txtTkThueCo.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                    txtThueSuat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    if (txtTkThueNo.Text.Trim() == "") txtTkThueNo.Text = txtManx.Text;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue", ex);
            }

            TinhTongThanhToan("XuLyThayDoiMaThue");
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
                ADTables.Add(sttRec, Invoice.LoadAd81(sttRec));
                AD = ADTables[sttRec].Copy();
            }
            //Load AD3
            if (AD3Tables == null) AD3Tables = new SortedDictionary<string, DataTable>();
            if (AD3Tables.ContainsKey(sttRec)) AD3 = AD3Tables[sttRec].Copy();
            else
            {
                AD3Tables.Add(sttRec, Invoice.LoadAD3(sttRec));
                AD3 = AD3Tables[sttRec].Copy();
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
                this.ShowErrorException(GetType() + ".ViewInvoice", ex);
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
                txtMadvcs.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                txtLoaiPhieu.ExistRowInTable(true);

                XuLyThayDoiMaDVCS();
                //{Tuanmh 20/02/2016
                XuLyThayDoiMaNt();
                //}

                SetGridViewData();
                Mode = V6Mode.View;

                FormatNumberControl();
                FormatNumberGridView();
                FixValues();
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice", ex);
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
                this.WriteExLog(GetType() + "FixValues", ex);
            }
        }

        #endregion view invoice

        #region ==== Add Thread ====
        private bool flagAddFinish, flagAddSuccess;
        private SortedDictionary<string, object> addDataAM;
        private List<SortedDictionary<string, object>> addDataAD, addDataAD3;
        private string addErrorMessage = "";

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
                CheckForIllegalCrossThreadCalls = false;

                if (Invoice.InsertInvoice(addDataAM, addDataAD, addDataAD3))
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
                Invoice.PostErrorLog(_sttRec, "M", ex);
            }
            flagAddFinish = true;
        }
#endregion add

        #region ==== Edit Thread ====
        private bool flagEditFinish, flagEditSuccess;
        //private SortedDictionary<string, object> editDataAM;
        private List<SortedDictionary<string, object>> editDataAD, editDataAD3;
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
                editDataAD3 = dataGridView3.GetData(_sttRec);
                foreach (SortedDictionary<string, object> adRow in editDataAD3)
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
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, editDataAD3, keys))
                {
                    flagEditSuccess = true;
                    ADTables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
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
                Invoice.PostErrorLog(_sttRec, "S", ex);
            }
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
                        DisableAllFunctionButtons(btnLuu, btnMoi, btnCopy, btnIn, btnSua, btnHuy, btnXoa, btnXem, btnTim, btnQuayRa);
                        
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
                Invoice.PostErrorLog(_sttRec, "X", ex);
            }
        }

        private void checkDelete_Tick(object sender, EventArgs e)
        {
            if (flagDeleteFinish)
            {
                ((Timer)sender).Stop();

                if (flagDeleteSuccess)
                {
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
            //Xóa xong view lại cái khác (trong timer tick)
            try
            {
                
                var row = AM.Rows[CurrentIndex];
                _sttRec = row["Stt_rec"].ToString().Trim();
                if (Invoice.DeleteInvoice(_sttRec))
                {
                    flagDeleteSuccess = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = "Xóa không thành công.";
                    Invoice.PostErrorLog(_sttRec, "X");
                }
            }
            catch (Exception ex)
            {
                flagDeleteSuccess = false;
                deleteErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "X", ex);
            }
            flagDeleteFinish = true;
        }
        #endregion delete

        private void Luu()
        {
            try
            {
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
                    //V6ControlFormHelper.UpdateDKlistAll(GetData(), new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                    if (Mode == V6Mode.Add)
                    {
                        V6ControlFormHelper.DisableControls(btnLuu, btnHuy, btnQuayRa);
                        DoAddThread();
                        return;
                    }
                    if (Mode == V6Mode.Edit)
                    {
                        V6ControlFormHelper.DisableControls(btnLuu, btnHuy, btnQuayRa);
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
                this.ShowErrorMessage(GetType() + ".Luu: " + ex.Message);
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
                    txtLoaiPhieu.ChangeText(_maGd);
                    //LoadAll(V6Mode.Add);
                    GetSttRec(Invoice.Mact);

                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);

                    //GetSoPhieu();
                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    Txtma_td_ph.Text = base.GetCA();
                    detail1.DoAddButtonClick();
                    SetDefaultDetail();
                    detail3.MODE = V6Mode.Init;
                    txtMa_sonb.Focus();
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch(Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Moi: " + ex.Message);
            }
        }

        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);
                if (IsViewingAnInvoice)
                {
                    if (V6BusinessHelper.CheckEditVoucher(_sttRec, "ARS20") == 1)
                    {
                        if (V6Login.IsAdmin)
                        {
                            this.ShowWarningMessage(V6Text.EditWarning);
                        }
                        else
                        {
                            this.ShowWarningMessage(V6Text.EditDenied);
                            return;
                        }
                    }

                    if (V6Login.UserRight.AllowEdit("", Invoice.CodeMact)
                        && V6Login.UserRight.AllowEditDeleteMact(Invoice.Mact, _sttRec, "S"))
                    {
                        if (Mode == V6Mode.View)
                        {
                            // Tuanmh 16/02/2016 Check level
                            var row = AM.Rows[CurrentIndex];
                            if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                            {
                                Mode = V6Mode.Edit;
                                detail1.MODE = V6Mode.View;
                                detail3.MODE = V6Mode.View;
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(V6Text.Edit + ": " + ex.Message);
            }
        }

        private void Xoa()
        {
            try
            {
                if (IsViewingAnInvoice)
                {
                    if (V6BusinessHelper.CheckEditVoucher(_sttRec, "ARS20") == 1)
                    {
                        this.ShowWarningMessage(V6Text.DeleteDenied);
                        return;
                    }

                    if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact)
                        && V6Login.UserRight.AllowEditDeleteMact(Invoice.Mact, _sttRec, "X"))
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(V6Text.Delete + ": " + ex.Message);
            }
        }

        private void Copy()
        {
            try
            {
                if(IsViewingAnInvoice)
                if (V6Login.UserRight.AllowAdd("", Invoice.CodeMact))
                {
                    if (Mode == V6Mode.View)
                    {
                        if (string.IsNullOrEmpty(_sttRec))
                        {
                            this.ShowWarningMessage(V6Text.NoSelection);
                        }
                        else
                        {
                            GetSttRec(Invoice.Mact);
                            TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

                            //Thay the stt_rec new
                            foreach (DataRow dataRow in AD.Rows)
                            {
                                dataRow["STT_REC"] = _sttRec;
                                dataRow["STT_RECDH"] = DBNull.Value;
                                dataRow["STT_REC0DH"] = DBNull.Value;
                            }

                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);

                            Mode = V6Mode.Add;
                            detail1.MODE = V6Mode.View;
                            detail3.MODE = V6Mode.View;
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
                this.ShowErrorException(GetType() + ".Copy", ex);
            }
        }

        private void In0(string sttRec_In, V6PrintMode printMode, bool closeAfterPrint, int sec = 3)
        {
            try
            {
                if (IsViewingAnInvoice)
                {
                    if (V6Login.UserRight.AllowPrint("", Invoice.CodeMact))
                    {
                        var program = Invoice.PrintReportProcedure;
                        var repFile = Invoice.Alct.Rows[0]["FORM"].ToString().Trim();
                        var repTitle = Invoice.Alct.Rows[0]["TIEU_DE_CT"].ToString().Trim();
                        var repTitle2 = Invoice.Alct.Rows[0]["TIEU_DE2"].ToString().Trim();

                        var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                            "", "", "", sttRec_In);
                        c.TTT = txtTongThanhToan.Value;
                        c.TTT_NT = txtTongThanhToanNt.Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec);
                            if (!sender.IsDisposed) sender.Dispose();
                        };
                        c.PrintMode = printMode;
                        c.Close_after_print = closeAfterPrint;
                        c.ShowToForm(this, V6Text.PrintSOA, true);
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".In: " + ex.Message);
            }
        }

        private TimHoaDonForm _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) return;
                    _timForm.ViewMode = true;
                    _timForm.Refresh0();
                    _timForm.Visible = false;
                    _timForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Xem", ex);
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
                        _timForm = new TimHoaDonForm(this);
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
                this.WriteExLog(GetType() + ".Tim", ex);
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

        
        
        //private string _status = "";
        /// <summary>
        /// Gán Status đổi luôn cả Mode. 0Init12NewEdit3View
        /// </summary>
        //public string Status
        //{
        //    get
        //    {
        //        return _status;
        //    }
        //    set
        //    {
        //        _status = value;
        //        OnBillChanged();
        //        switch (value)
        //        {
        //            case "0":
        //                if (IsViewingAnInvoice) Mode = V6Mode.View;
        //                else Mode = V6Mode.Init;
        //                break;
        //            case "1":
        //                Mode = V6Mode.Edit;
        //                break;
        //            case "2":
        //                Mode = V6Mode.Edit;
        //                break;
        //            case "3":
        //                Mode = V6Mode.View;
        //                break;
        //        }
        //    }
        //}
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
                Luu();
                Mode = V6Mode.View;// Status = "3";
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
                detail3.SetData(null);
                
                LoadAD("");
                SetGridViewData();
                
                ResetAllVars();
                EnableFormControls();
                SetFormDefaultValues();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ResetForm: " + ex.Message);
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
                    chkSuaTienCk.Enabled = false;
                    chkSuaTienCk.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                    txtTongThueNt.ReadOnly = true;
                    break;
                case V6Mode.Add:
                case V6Mode.Edit:
                    chkSuaPtck.Enabled = true;
                    chkSuaPtck.Checked = false;
                    txtPtCk.ReadOnly = true;
                    chkSuaTienCk.Enabled = true;
                    chkSuaTienCk.Checked = false;
                    txtTongCkNt.ReadOnly = true;
                    txtTongThueNt.ReadOnly = true;
                break;
            }
        }

        private void Huy()
        {
            try
            {
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
                this.ShowErrorMessage(GetType() + ".Huy: " + ex.Message);
            }
        }
        
        private void GetSoPhieuInit()
        {
            var p = GetParentTabPage();
            if (p != null)
            {
                TxtSo_ct.Text = ((TabControl)(p.Parent)).TabPages.Count.ToString();
            }
            else
            {
                TxtSo_ct.Text = "01";    
            }
        }

        private void GetSoPhieu()
        {
            //TxtSo_ct.Text = V6BusinessHelper.GetSoCT("M", "", Invoice.Mact, "", V6LoginInfo.UserId);
            TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);
           
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
                _maVt.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd: " + ex.Message);
            }
        }
        
        private bool XuLyThemDetail(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = _sttRec;
                //Thêm thắt vài thứ
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Value.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\nMã vật tư rỗng.";
                if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\nMã kho rỗng.";
                if (error == "")
                {
                    //Tạo cột thiếu cho AD. (Làm chậm)
                    //foreach (KeyValuePair<string, object> item in data)
                    //{
                    //    if (!AD.Columns.Contains(item.Key))
                    //    {
                    //        AD.Columns.Add(item.Key, (item.Value ?? "").GetType());
                    //    }
                    //}
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD.NewRow();
                    foreach (DataColumn column in AD.Columns)
                    {
                        var KEY = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(KEY) ? data[KEY] : "")??DBNull.Value;
                        newRow[KEY] = value;
                    }
                    AD.Rows.Add(newRow);
                    dataGridView1.DataSource = AD;
                    
                    var tData = _maVt.Data;
                    if (tData == null || tData["MA_THUE"] == null) goto Next1;
                    var maThue = tData["MA_THUE"].ToString().Trim();
                    if (maThue == "") goto Next1;
                    if(V6Options.V6OptionValues["M_SOA_MULTI_VAT"] != "0") goto Next1;
                    
                    if (AD.Rows.Count == 1) // Neu la dong dau tien thi lay ma thue ra AM
                    {
                        txtMa_thue.ChangeText(maThue);
                    }
                    else if (maThue != txtMa_thue.Text)
                    {
                        if (_tien2.Value != 0)
                        {
                            var message = string.Format("Mã thuế của vật tư này ({0}) khác với mã thuế đã chọn ({1})",
                                maThue, txtMa_thue.Text);
                            if (V6Setting.Language != "V")
                            {
                                message = string.Format("This item tax code ({0}) is different from the selected tax code ({1})",
                                maThue, txtMa_thue.Text);
                            }
                            ShowParentMessage(message);
                            this.ShowWarningMessage(message);
                        }
                    }
                Next1:
                    TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
                    
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                        dataGridView1.CurrentCell
                            = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["Ma_vt"];
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
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết: " + ex.Message);
            }
            return true;
        }

        
        private bool XuLySuaDetail(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                        //Thêm thắt vài thứ
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Value.Date;
                        
                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                            error += "\nMã vật tư rỗng.";
                        if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "")
                            error += "\nMã kho rỗng.";

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

                            dataGridView1.DataSource = AD;
                            
                            var tData = _maVt.Data;
                            if(tData == null || tData["MA_THUE"] == null) goto Next1;
                            var maThue = tData["MA_THUE"].ToString().Trim();
                            if (maThue == "") goto Next1;
                            if (V6Options.V6OptionValues["M_SOA_MULTI_VAT"] != "0") goto Next1;

                            if (cIndex == 0) // Neu la dong dau tien thi lay ma thue ra AM
                            {
                                txtMa_thue.ChangeText(maThue);
                            }
                            else if (maThue != txtMa_thue.Text)
                            {
                                if (_tien2.Value != 0)
                                {
                                    var message =
                                        string.Format("Mã thuế của vật tư này ({0}) khác với mã thuế đã chọn ({1})",
                                            maThue, txtMa_thue.Text);
                                    ShowParentMessage(message);
                                    this.ShowWarningMessage(message);
                                }
                            }
                        Next1:
                            TinhTongThanhToan("xy ly sua detail");
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
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết: " + ex.Message);
            }
            return true;
        }

        private void XuLyXoaDetail()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết: " + ex.Message);
            }
        }
        
        #endregion details

        #region ==== AM Events ====
        private void Form_Load(object sender, EventArgs e)
        {
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
        private void hoaDonDetail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd();
        }
        private void hoaDonDetail1_AddHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLyThemDetail(data))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
        }
        private void hoaDonDetail1_EditHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLySuaDetail(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }
        private void hoaDonDetail1_ClickDelete(object sender)
        {
            XuLyXoaDetail();
        }
        private void hoaDonDetail1_ClickCancelEdit(object sender)
        {
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

        #endregion hoadoen detail event

        /// <summary>
        /// Thêm chi tiết hóa đơn
        /// </summary>
        

        private void dateNgayCT_ValueChanged(object sender, EventArgs e)
        {
            if (!Invoice.M_NGAY_CT) dateNgayLCT.Value = dateNgayCT.Value;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Huy();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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
                chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;
            }

            if (chkLoaiChietKhau.Checked)
            {
                _pt_cki.Enabled = false;
                _pt_cki.Tag = "disable";
                _ckNt.Enabled = false;
                _ckNt.Tag = "disable";
            }
            else
            {
                chkSuaPtck.Checked = false;
                txtPtCk.ReadOnly = true;
                chkSuaTienCk.Checked = false;
                txtTongCkNt.ReadOnly = true;

                _pt_cki.Enabled = true;
                _pt_cki.Tag = null;
                if (chkSuaTien.Checked)
                {
                   _ckNt.Enabled = true; //Bỏ rào để sử dụng nhập tiền ck
                   _ckNt.Tag = null;
                }
            }
            
            TinhTongThanhToan("LoaiChietKhau_Change");
        }

        private void cboMaNt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_ready0 && cboMaNt.SelectedValue != null)
            {
                _maNt = cboMaNt.SelectedValue.ToString().Trim();
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit) GetTyGia();
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
                txtTkThueCo.ReadOnlyTag(false);
                txtTkThueNo.ReadOnlyTag(false);
            }
            else
            {
                txtTkThueCo.ReadOnlyTag(true);
                txtTkThueNo.ReadOnlyTag(true);
            }
        }

        private void chkSuaTienCk_CheckedChanged(object sender, EventArgs e)
        {
            if(Mode == V6Mode.Add || Mode == V6Mode.Edit)
                txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;

        }

        private void chkSuaTienThue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
                    txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;

                    if (chkSuaTienThue.Checked && M_SOA_MULTI_VAT == "1")
                    {
                        _thue_nt.Enabled = true;
                        txtTongThueNt.ReadOnly = true;
                    }
                    else _thue_nt.Enabled = false;
                }

                TinhTongThanhToan("ckhSuaTienThue");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ckhSuaTienThue: " + ex.Message);
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
            else if(!(new List<string> {"TEN_VT","MA_VT"}).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
        }

        private void txtSoCt_TextChanged(object sender, EventArgs e)
        {
            SetTabPageText(TxtSo_ct.Text);

            if(Mode == V6Mode.Add || Mode == V6Mode.Edit)
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);
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
            BasePrint(Invoice, _sttRec, V6PrintMode.AutoClickPrint, TongThanhToan, TongThanhToanNT, false);
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
                _ckNt.Enabled = chkSuaTien.Checked;
                _tienNt.Enabled = chkSuaTien.Checked && _xuat_dd.Text != "";
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

        private void hoaDonDetail1_Load(object sender, EventArgs e)
        {

        }

        private void hoaDonDetail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);

                    _maVt.RefreshLoDateYnValue();
                    _maKhoI.RefreshLoDateYnValue();
                    XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                    _maVt.Focus();
                    GetLoDate13();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".hoaDonDetail1_ClickEdit: " + ex.Message);
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

                //Check nh_dk
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
                        checkChiTietError += string.Format("Kiểm tra nhóm định khoản (Phát sinh nợ <> Phát sinh có) {0}\n", item.Key);
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
                        TxtSo_ct.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec);
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
                        TxtSo_ct.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec,txtMadvcs.Text.Trim(),txtMaKh.Text.Trim(),
                        txtManx.Text.Trim(), dateNgayCT.Value, txtMa_ct.Text, txtTongThanhToan.Value, mode_vc, V6Login.UserId);

                    

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
                                
                                if(message != "") this.ShowWarningMessage(message);
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

                var check_ton = ValidateData_Master_CheckTon(Invoice, dateNgayCT.Value, null);
                if (!check_ton) return false;
                

                //OK
                return true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Master", ex);
            }
            return false;
        }

        private bool ValidateData_Detail(SortedDictionary<string, object> data)
        {
            try
            {
                if (_tkDt.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                    _tkDt.Focus();
                    return false;
                }

                ValidateDetailData(Invoice, data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail", ex);
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
            var data = txtMa_sonb.Data;
            if (data != null) txtso_seri.Text = (data["SO_SERI"] ?? "").ToString().Trim();
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
        }
        private void txtMaHttt_V6LostFocus(object sender)
        {
            XuLyChonMaHttt();
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
            else if (tabControl1.SelectedTab == tabChiTietBoSung)
            {
                detail3.AutoFocus();
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
            lblNameT.Text = ((Label) sender).Text;
        }

        private void txtMa_sonb_V6LostFocusNoChange(object sender)
        {
            var data = txtMa_sonb.Data;
            if (data != null) txtso_seri.Text = (data["SO_SERI"] ?? "").ToString().Trim();
            //Tuanmh 05/05/2017
            if (data != null)
            {
                if (TxtSo_ct.Text.Trim() == "")
                {
                    GetSoPhieu();
                }

            }
        }

        #region ==== Chức năng ====
        private void chonDonHangBanMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ChonDonHang();
        }
        
        private void chonBaoGiaMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ChonBaoGia();
        }

        private void ChucNang_ChonDonHang()
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CDH_HoaDonForm chon = new CDH_HoaDonForm(dateNgayCT.Value, txtMadvcs.Text, txtMaKh.Text);
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_kh == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID needs to enter!";
                    this.ShowWarningMessage(message);
                    if (ma_kh == "") txtMaKh.Focus();
                    else if (ma_dvcs == "") txtMadvcs.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ChonDonHang", ex);
            }
        }

        private void ChucNang_ChonBaoGia()
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CBG_HoaDonForm chon = new CBG_HoaDonForm(txtMadvcs.Text, txtMaKh.Text);
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_kh == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID needs to enter!";
                    this.ShowWarningMessage(message);
                    if (ma_kh == "") txtMaKh.Focus();
                    else if (ma_dvcs == "") txtMadvcs.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ChonBaoGia", ex);
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
                    if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                    {
                        newData["MA_TD_I"] = Txtma_td_ph.Text;
                    }

                    if (XuLyThemDetail(newData)) addCount++;
                    else failCount++;
                }
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed {1}.", addCount, failCount));
                //if (addCount > 0)
                //{
                //    co_chon_don_hang = true;
                //}
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".chon_AcceptSelectEvent", ex);
            }
        }

        private void chonTuExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ChonTuExcel();
        }

        private void ChucNang_ChonTuExcel()
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                var chonExcel = new LoadExcelDataForm();
                chonExcel.Program = Event_program;
                chonExcel.All_Objects = All_Objects;
                chonExcel.DynamicFixMethodName = "DynamicFixExcel";
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT2,SO_LUONG1,GIA_NT21";
                chonExcel.MA_CT = Invoice.Mact;
                chonExcel.AcceptData += chonExcel_AcceptData;
                chonExcel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChonTuExcel", ex);
            }
        }

        void chonExcel_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO_I")
                && table.Columns.Contains("TIEN_NT2") && table.Columns.Contains("SO_LUONG1")
                && table.Columns.Contains("GIA_NT21"))
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

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["TIEN_NT2"]);
                        var __gia_nt0 = ObjectAndString.ObjectToDecimal(data["GIA_NT21"]);
                        var __tien0 = V6BusinessHelper.Vround(__tien_nt0 * txtTyGia.Value, M_ROUND);
                        var __gia0 = V6BusinessHelper.Vround(__gia_nt0 * txtTyGia.Value, M_ROUND_GIA);

                        if (!data.ContainsKey("TIEN2")) data.Add("TIEN2", __tien0);
                        
                        if (!data.ContainsKey("GIA21")) data.Add("GIA21", __gia0);
                        if (!data.ContainsKey("GIA2")) data.Add("GIA2", __gia0);
                        if (!data.ContainsKey("GIA_NT2")) data.Add("GIA_NT2", data["GIA_NT21"]);

                        
                        if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                        {
                            //Ghi đè MA_TD_I
                            data["MA_TD_I"] = Txtma_td_ph.Text;
                        }

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

        private void xemCongNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Init) return;
                if (txtMaKh.Text == "" || txtManx.Text== "")  return;
                var datatk = txtManx.Data;
                if (datatk == null ||ObjectAndString.ObjectToDecimal(datatk["TK_CN"]) == 0) return;

                SqlParameter[] plist =
                {
                    new SqlParameter("@MA_KH", txtMaKh.Text),
                    new SqlParameter("@TK", txtManx.Text),
                    new SqlParameter("@Ngay_ct", dateNgayCT.Value.Date.ToString("yyyyMMdd")),
                    new SqlParameter("@Advance", string.Format("Ma_dvcs='{0}'", txtMadvcs.Text.Replace("'", "''"))),
                    new SqlParameter("@User_id",V6Login.UserId),
                    new SqlParameter("@Lan", V6Setting.Language),

                };
                var data = V6BusinessHelper.ExecuteProcedure("VPA_GetCongNo", plist).Tables[0];
                if (data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    var text_duno = ObjectAndString.NumberToString((ObjectAndString.ObjectToDecimal(row["NO_CK"]) - ObjectAndString.ObjectToDecimal(row["CO_CK"])), 2, V6Options.M_NUM_POINT, ".");
                    var showtext = V6Setting.Language == "V" ? "Số dư nợ cuối ngày " : "Ending balance ";

                    string message = string.Format(showtext + ": {0}--> {1}", dateNgayCT.Value.ToString("dd/MM/yyyy"), text_duno);
                    this.ShowMessage(message);
                }
                else
                {
                    this.ShowMessage(V6Text.NoData);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".xemCongNoToolStripMenuItem_Click " + _sttRec, ex);
            }
        }

        #endregion chức năng

        private void dateNgayCT_Leave(object sender, EventArgs e)
        {
            //string message = "";
            //bool check = V6BusinessHelper.CheckNgayCt(dateNgayCT.Value, out message);
            //if (!check)
            //{
            //    ShowParentMessage(message);
            //    dateNgayCT.Focus();
            //}
        }

        private void dateNgayCT_Validated(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                ValidateNgayCt(Invoice.Mact, dateNgayCT);
        }

        private void txtManx_Leave(object sender, EventArgs eventArgs)
        {
            if (chkSuaTkThue.Checked)
            {
                if (txtTkThueNo.Text.Trim() == "") txtTkThueNo.Text = txtManx.Text;
            }
            else
            {
                txtTkThueNo.Text = txtManx.Text;
            }
        }
        
        private void txtDiaChiGiaoHang_Enter(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (txtDiaChiGiaoHang.ReadOnly) return;
                var data = txtMaKh.Data;
                if (data == null)
                {
                    this.ShowWarningMessage("Chưa chọn mã khách hàng!", 300);
                    return;
                }
                txtDiaChiGiaoHang.ParentData = data.ToDataDictionary();
                txtDiaChiGiaoHang.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
            }
        }

        private void btnChonPX_Click(object sender, EventArgs e)
        {
            if (txtLoaiPhieu.Text.Trim() == "B")
            {
                ChonPhieuXuat_A();
            }
            else
            {
                this.ShowWarningMessage("Chưa chọn đúng loại phiếu=[B](Xuất bán không kiêm PX) !", 300);
            }
        }

        private void ChonPhieuXuat_A()
        {
            try
            {
                try
                {
                    var ma_kh = txtMaKh.Text.Trim();
                    var ma_dvcs = txtMadvcs.Text.Trim();
                    var message = "";
                    if (ma_kh != "" && ma_dvcs != "")
                    {
                        CPX_HoaDonForm chon = new CPX_HoaDonForm(dateNgayCT.Value, txtMadvcs.Text, txtMaKh.Text);
                        chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                        chon.ShowDialog(this);
                    }
                    else
                    {
                        if (ma_kh == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID must entry!\n";
                        if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID must entry!";
                        this.ShowWarningMessage(message);
                        if (ma_kh == "") txtMaKh.Focus();
                        else if (ma_dvcs == "") txtMadvcs.Focus();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessage(GetType() + ".XuLyChonPhieuXuat: " + ex.Message, "HoaDonControl");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChonPhieuXuat_A: " + ex.Message, "Hoa don ban hang Error");
            }
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView3);
        }

        private void btnApGia_Click(object sender, EventArgs e)
        {
            ApGiaBan();
        }

        private void ApGiaBan()
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
                    ShowParentMessage("Chọn mã giá trước.");
                    return;
                }
                if (this.ShowConfirmMessage("Có chắc chắn áp giá bán cho tất cả mặt hàng hay không?") != DialogResult.Yes)
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

                    var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Value,
                        cboMaNt.SelectedValue.ToString().Trim(), maVatTu, dvt1, txtMaKh.Text, txtMaGia.Text);

                    var giaNt21 = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                    row["GIA_NT21"] = giaNt21;
                    //_soLuong.Value = _soLuong1.Value * _heSo1.Value;
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
                    if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "1" ||
                    V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
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
                    if (V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "2" ||
                    V6Options.V6OptionValues["M_GIAVC_GIAGIAM_CT"] == "3")
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
                            row["THUE_NT"] = V6BusinessHelper.Vround(tienNt2 *  thue_suat_i/ 100, M_ROUND_NT);
                            row["THUE"] = V6BusinessHelper.Vround(tien2 * thue_suat_i / 100, M_ROUND);

                            if (_maNt == _mMaNt0)
                            {
                                row["THUE"] = row["THUE_NT"];
                            }
                        }
                        catch (Exception ex)
                        {
                            this.WriteExLog(GetType() + ".ApGiaBan TinhThueCt", ex);
                        }
                    }

                    //TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);//Nouse
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
                this.ShowErrorException(GetType() + ".ApGiaBan", ex);
            }
        }

        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
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
                this.WriteExLog(GetType() + ".txtTongGiamNt_V6LostFocus", ex);
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
                this.WriteExLog(GetType() + ".txtTongCkNt_V6LostFocus", ex);
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
                this.WriteExLog(GetType() + ".txtTongThueNt_V6LostFocus", ex);
            }
        }

        private void menuXemPhieuNhap_Click(object sender, EventArgs e)
        {
            XemPhieuNhap();
        }

        private void XemPhieuNhap()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@nXT", 1),
                    new SqlParameter("@Type", 0),
                    new SqlParameter("@Ngay_ct", dateNgayCT.Value.Date),
                    new SqlParameter("@Stt_rec", _sttRec),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@M_lan", V6Login.SelectedLanguage),
                    new SqlParameter("@Advance", string.Format("Ma_kho='{0}' and Ma_vt='{1}'", _maKhoI.Text, _maVt.Text)),
                    new SqlParameter("@OutputInsert", ""),
                };
                var data0 = V6BusinessHelper.ExecuteProcedure("VPA_Get_SOA_VIEWF5", plist);
                if (data0 == null || data0.Tables.Count == 0)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }

                var data = data0.Tables[0];
                FilterView f = new FilterView(data, "MA_KH", "SOA_VIEWF5", null, null);
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    var ROW = f.SelectedRowData;
                    if (ROW == null || ROW.Count == 0) return;

                    var datamavt = _maVt.Data;

                    if (_xuat_dd.Checked || (datamavt != null && ObjectAndString.ObjectToDecimal(datamavt["GIA_TON"]) == 2))
                    {
                        _gia.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                        _gia_nt.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemPhieuNhap", ex);
            }
        }
        

        private void Txtma_td_ph_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                {
                    V6ControlFormHelper.UpdateDKlist(AD, "MA_TD_I", Txtma_td_ph.Text);
                    if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                    {
                        _maTdi.Text = Txtma_td_ph.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Txtma_td_ph_Leave", ex);
            }
        }

        private void chonPhieuNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuLyChonPhieuNhap();
        }


        private void XuLyChonPhieuNhap()
        {
            try
            {
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CPN_HoaDonForm chon = new CPN_HoaDonForm(dateNgayCT.Value.Date, txtMadvcs.Text, txtMaKh.Text);
                    chon.AcceptSelectEvent += chonpn_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_kh == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID needs to enter!";
                    this.ShowWarningMessage(message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonPhieuNhap: " + ex.Message);
            }
        }

        void chonpn_AcceptSelectEvent(List<SortedDictionary<string, object>> selectedDataList)
        {
            try
            {
                detail1.MODE = V6Mode.View;
                AD.Rows.Clear();
                int addCount = 0, failCount = 0;
                foreach (SortedDictionary<string, object> data in selectedDataList)
                {
                    var newData = new SortedDictionary<string, object>(data);
                    if (!data.ContainsKey("GIA_NT21") && data.ContainsKey("MA_VT") && data.ContainsKey("DVT1"))
                    {   
                        var _maVt_Text=ObjectAndString.ObjectToString(data["MA_VT"]);
                        var _dvt1_Text = ObjectAndString.ObjectToString(data["DVT1"]);
                        decimal _giaNt21_Value = 0m, _giaNt2_Value = 0m, _gia21_Value = 0m, _gia2_Value = 0m,
                           _tienNt2_Value = 0m, _soLuong_Value = 0m, _tien2_Value = 0m;


                        var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Value,
                        cboMaNt.SelectedValue.ToString().Trim(), _maVt_Text, _dvt1_Text, txtMaKh.Text, txtMaGia.Text);

                        _giaNt21_Value = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);

                        var _soLuong1_Value = ObjectAndString.ObjectToDecimal(data["SO_LUONG1"]);
                        var _heSo1_Value = ObjectAndString.ObjectToDecimal(data["HE_SO1"]);
                        if (_heSo1_Value == 0) _heSo1_Value = 1;
                        
                        
                        _soLuong_Value = _soLuong1_Value * _heSo1_Value;
                        _tienNt2_Value = V6BusinessHelper.Vround((_soLuong1_Value * _giaNt21_Value), M_ROUND_NT);
                        _tien2_Value = V6BusinessHelper.Vround((_tienNt2_Value * txtTyGia.Value), M_ROUND);

                      
                        if (newData["DVT"] == data["DVT1"])
                        {
                            _giaNt2_Value = _giaNt21_Value;
                        }
                        else
                        {
                            if (_soLuong_Value != 0)
                            {
                                _giaNt2_Value = V6BusinessHelper.Vround(_tienNt2_Value / _soLuong_Value, M_ROUND_GIA_NT);

                            }
                        }
                        
                        _gia21_Value = V6BusinessHelper.Vround((_giaNt21_Value * txtTyGia.Value), M_ROUND_GIA);
                        _gia2_Value = V6BusinessHelper.Vround((_giaNt2_Value * txtTyGia.Value), M_ROUND_GIA);

                        if (_heSo1_Value == 1)
                            _giaNt2_Value = _giaNt21_Value;

                        if (_maNt == _mMaNt0)
                        {
                            _gia21_Value = _giaNt21_Value;
                            _gia2_Value = _giaNt2_Value;
                            _tien2_Value = _tienNt2_Value;
                            
                        }

                        

                        newData["SO_LUONG"] = _soLuong_Value;
                        newData["SO_LUONG1"] = _soLuong1_Value;
                        newData["GIA_NT2"] = _giaNt2_Value;
                        newData["GIA_NT21"] = _giaNt21_Value;
                        newData["GIA2"] = _gia2_Value;
                        newData["GIA21"] = _gia21_Value;
                        newData["TIEN_NT2"] = _tienNt2_Value;
                        newData["TIEN2"] = _tien2_Value;
                        if (_m_Ma_td == "1" && Txtma_td_ph.Text != "")
                        {
                           newData["MA_TD_I"] = Txtma_td_ph.Text;
                        }

                    }

                    if (XuLyThemDetail(newData)) addCount++;
                    else failCount++;
                }
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
                this.ShowErrorMessage(GetType() + ".chonpx_AcceptSelectEvent: " + ex.Message);
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
                if (this.ShowConfirmMessage("Có chắc bạn muốn xóa Chiết khấu - Khuyến mãi?") == DialogResult.Yes)
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
        private void TinhChietKhauKhuyenMai()
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
                    new SqlParameter("@dngay_ct", dateNgayCT.Value.ToString("yyyyMMdd")),
                    new SqlParameter("@cMa_kh", txtMaKh.Text),
                    new SqlParameter("@cMa_dvcs", txtMadvcs.Text),
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
                this.WriteExLog(GetType() + ".TinhChietKhauKhuyenMai", ex);
            }
        }
        private string l_ma_km = "";

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
                
                if (tong_giam!=0)
                {
                    txtTongGiam.Value = tong_giam;
                    txtTongGiamNt.Value = tong_giam_nt;

                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungChietKhauTH", ex);
            }
        }

        private void ApDungKhuyenMaiTH(DataTable ctkm1th)
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApDungKhuyenMaiTH", ex);
            }
        }

        private void ApDungKhuyenMai(DataTable datakmct)
        {
            try
            {
                if (datakmct != null && datakmct.Rows.Count > 0)
                    ShowMainMessage("Có khuyến mãi");
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
                this.WriteExLog(GetType() + ".ApDungKhuyenMai", ex);
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
                                    
                                    if(!l_ma_km.Contains(";" + CK_MA_KM + ";"))
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
                this.WriteExLog(GetType() + ".ApDungChietKhau", ex);
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
                    this.WriteExLog(GetType() + ".XoaKhuyenMai", ex);
                }
        }

        private void XoaChietKhau()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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

        #region ==== In khác ====
        private void inKhacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
        }


        #endregion ==== In khác ====

        private void txtLoaiPhieu_V6LostFocusNoChange(object sender)
        {
            txtLoaiPhieu.ExistRowInTable(true);
        }

    }
}
