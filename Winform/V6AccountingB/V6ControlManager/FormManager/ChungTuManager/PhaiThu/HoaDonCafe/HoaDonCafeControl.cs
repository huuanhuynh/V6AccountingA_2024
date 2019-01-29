using System;
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
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonBaoGia;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonDonHang;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon.ChonPhieuXuat;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    /// <summary>
    /// Hóa đơn bán hàng kiêm phiếu xuất
    /// </summary>
    public partial class HoaDonCafeControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        public V6Invoice83 Invoice = new V6Invoice83();
        private string _maGd = "1";

        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public HoaDonCafeControl()
        {
            InitializeComponent();
            MyInit();
        }
        public HoaDonCafeControl(string itemId)
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
        public HoaDonCafeControl(string maCt, string itemId, string sttRec)
            : base(maCt, itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
            
            CallViewInvoice(sttRec, V6Mode.View);
        }

        public string MA_KHOPH { get { return txtMa_khoPH.Text.Trim(); } set { txtMa_khoPH.Text = value; } }
        public string MA_VITRIPH { get { return txtMa_vitriPH.Text.Trim(); } set { txtMa_vitriPH.Text = value; } }

        private string _status = "";

        /// <summary>
        /// Gán Status đổi luôn cả Mode. 0Init12NewEdit3View
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnBillChanged();
                switch (value)
                {
                    case "0":
                        Mode = IsViewingAnInvoice ? V6Mode.View : V6Mode.Init;
                        break;
                    case "1":
                        Mode = V6Mode.Edit;
                        break;
                    case "2":
                        Mode = V6Mode.Edit;
                        break;
                    case "3":
                        Mode = V6Mode.View;
                        break;
                }
            }
        }

        public decimal TongThanhToan { get { return txtTongThanhToan.Value; } }
        public decimal TongThanhToanNT { get { return txtTongThanhToanNt.Value; } }
        public string GC_UD1 { get { return txtGC_UD1.Text; } }

        public string Time0
        {
            get
            {
                var data = CurrentIndexAM_Data;
                if (data != null)
                {
                    return data["TIME0"].ToString();//.Left(5);
                }
                return "";
            }
            set
            {
                if (CurrentIndex >= 0 && CurrentIndex < AM.Rows.Count)
                {
                    var row = AM.Rows[CurrentIndex];
                    row["TIME0"] = value.Left(8);
                }
            }
        }
        public string Time2
        {
            get
            {
                var data = CurrentIndexAM_Data;
                if (data != null)
                {
                    return data["TIME2"].ToString();
                }
                return "";
            }
            set
            {
                if (CurrentIndex >= 0 && CurrentIndex < AM.Rows.Count)
                {
                    var row = AM.Rows[CurrentIndex];
                    row["TIME2"] = value.Left(8);
                }
            }
        }


        private void MyInit()
        {
            LoadTag(Invoice, detail1.Controls);
            
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
            
            LoadDetailControls();
            LoadDetail3Controls();
            LoadAdvanceControls(Invoice.Mact);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            ResetForm();
            
            _maGd = (Invoice.Alct["M_MA_GD"] ?? "1").ToString().Trim();
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }

        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        private V6ColorTextBox _dvt;
        private V6CheckTextBox _tang, _xuat_dd;
        private V6VvarTextBox _maVt, _dvt1, _maKhoI, _tkDt, _tkGv, _tkCkI, _tkVt, _maLo; //_maKho
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _giaNt2, _giaNt21,_tien2, _tienNt2, _ck, _ckNt,_gia2,_gia21;
        private V6NumberTextBox _ton13,_gia,_gia_nt, _tien, _tienNt, _pt_cki;
        private V6NumberTextBox _sl_qd, _sl_qd2, _tien_vcNt, _tien_vc, _hs_qd1, _hs_qd2, _hs_qd3, _hs_qd4,_ggNt,_gg;
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

                        _maVt.Font = new Font(_maVt.Font.FontFamily, 10f, FontStyle.Bold);
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
                        // Check Barcode 17-May-17
                        _maVt.CheckBarCode = true;
                        _maVt.TrimBarcode = V6Options.M_BARCODE_ELENGTH;// V6Options
                        //_maVt.InputBarcode += (sender, args) =>
                        //{
                        //    if (V6Options.M_AUTO_BARCODE)
                        //    {
                        //        XuLyAutoBarcode();
                        //    }
                        //};
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
                            _soLuong1.Font = new Font(_soLuong1.Font.FontFamily, 10f, FontStyle.Bold);
                            _soLuong1.LostFocus += delegate
                            {
                                CheckSoLuong1();
                            };

                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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
                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                    _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                }
                                else if (M_CAL_SL_QD_ALL == "0")
                                {
                                    _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                    TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                }
                                else if (M_CAL_SL_QD_ALL == "2")
                                {
                                    _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                    TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                }
                            }
                        };
                        break;
                    case "GIA_NT2":
                        _giaNt2 = control as V6NumberTextBox;
                        if (_giaNt2 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _giaNt2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _giaNt2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA2":
                        _gia2 = control as V6NumberTextBox;
                        if (_gia2 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _gia2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA21":
                        _gia21 = control as V6NumberTextBox;
                        if (_gia21 != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _gia21.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _gia21.ReadOnlyTag();
                            }
                        }
                        break;
                    case "GIA_NT21":
                        _giaNt21 = control as V6NumberTextBox;
                        if (_giaNt21 != null)
                        {
                            _giaNt21.Font = new Font(_giaNt21.Font.FontFamily, 10f, FontStyle.Bold);
                            _giaNt21.V6LostFocus += GiaNt21_V6LostFocus;
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _giaNt21.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _giaNt21.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN_NT2":
                        _tienNt2 = (V6NumberTextBox)control;
                        if (_tienNt2 != null)
                        {
                            _tienNt2.Font = new Font(_tienNt2.Font.FontFamily, 10f, FontStyle.Bold);

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

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tienNt2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _tienNt2.ReadOnlyTag();
                            }
                        }
                        break;
                    case "TIEN2":
                        _tien2 = control as V6NumberTextBox;
                        if (_tien2 != null)
                        {
                            _tien2.Font = new Font(_tien2.Font.FontFamily, 10f, FontStyle.Bold);
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien2.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
                            {
                                _tien2.ReadOnlyTag();
                            }
                        }
                        break;

                    case "TIEN":
                        _tien = (V6NumberTextBox)control;
                        if (_tien != null)
                        {
                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tien.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _tienNt.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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
                                TinhChietKhauChiTiet(true, _ck, _ckNt, txtTyGia, _tienNt2, _pt_cki);
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
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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

                            if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME))
                            {
                                _xuat_dd.InvisibleTag();
                            }
                            if (!V6Login.IsAdmin && Invoice.GRD_READONLY.Contains(NAME))
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

                            _dataLoDate = V6BusinessHelper.GetLoDate(_maVt.Text, _maKhoI.Text, _sttRec, dateNgayCT.Date);
                            var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                            var getFilter = GetFilterMaLo(_dataLoDate, _sttRec0, _maVt.Text, _maKhoI.Text);
                            if (getFilter != "") filter += " and " + getFilter;
                            _maLo.SetInitFilter(filter);
                        };
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
                                TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                if (M_CAL_SL_QD_ALL == "1")
                                {
                                    CheckSoLuong1();
                                    chkSuaTienThue.Checked = false;
                                    //Tinh_thue_ct();
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
                        _hs_qd4.V6LostFocus += Hs_qd4_V6LostFocus;
                        break;
                    case "HS_QD3":
                        _hs_qd3 = (V6NumberTextBox)control;
                        _hs_qd3.V6LostFocus += Hs_qd3_V6LostFocus;
                        break;
                    case "GG_NT":
                        _ggNt = (V6NumberTextBox)control;
                        break;
                    case "GG":
                        _gg = (V6NumberTextBox)control;
                        break;
                    case "TIEN_VC_NT":
                        _tien_vcNt = (V6NumberTextBox)control;
                        break;
                    case "TIEN_VC":
                        _tien_vc = (V6NumberTextBox)control;
                        break;
                        //}

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
                    this.ShowWarningMessage("Tài khoản công nợ thiếu mã khách hàng !");
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

        private void CheckMaLoTon()
        {
            if (NotAddEdit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            if (!_maVt.LO_YN) return;
            //Fix Tuanmh 15/11/2017
            if (!_maKhoI.LO_YN) return;

            try
            {
                Invoice.GetAlLoTon(dateNgayCT.Date, _sttRec, _maVt.Text, _maKhoI.Text);
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
                            _soLuong1.Value = _ton13.Value < 0 ? 0 : _ton13.Value;
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        /// <summary>
        /// Check so luong roi tinh tien Nt2
        /// </summary>
        public void CheckSoLuong1()
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
                        _soLuong1.Value = _ton13.Value < 0 ? 0 : _ton13.Value;
                        if (M_CAL_SL_QD_ALL == "1")
                        {
                            if (_hs_qd1.Value != 0)
                                _sl_qd.Value = _soLuong1.Value / _hs_qd1.Value;
                        }
                    }
                }
                TinhTienNt2();
                TinhTienVon();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        
        
        #endregion detail form

        #region ==== Override Methods ====

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Setting.IsVietnamese ?
                "F3-Sửa số lượng, F4-Thêm chi tiết, F6-Chuyển vị trí, F7-Lưu và in, F8-Dọn dẹp." :
                "F3-Edit quantity, F4-Add details, F6-Move location, F7-Save and print, F8-Reset.");
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
            else if (keyData == Keys.F3)
            {
                if (dataGridView1.Focused && Mode == V6Mode.Edit && detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit)
                {
                    detail1.OnSuaClick();
                    _soLuong1.Focus();
                }
                else if (Mode == V6Mode.Edit && detail1.MODE == V6Mode.Edit)
                {
                    _soLuong1.Focus();
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
                detail1.btnNhan.Focus();
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                {
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
            else if (keyData == Keys.F6)
            {
                ChuyenBan();
            }
            else if (keyData == Keys.F8)
            {
                DonBan();
            }
            else if (keyData == Keys.F7)
            {
                LuuVaIn();
            }
            else if (keyData == Keys.F8)//Bị đè bởi DonBan();
            {
                if (detail1.MODE == V6Mode.View && detail1.btnXoa.Enabled && detail1.btnXoa.Visible)
                    detail1.btnXoa.PerformClick();
            }
            else if (keyData == (Keys.F10))
            {
                ChonHoaDonMau();
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

        public void TienNt2_V6LostFocus(object sender)
        {
            TinhGiaNt2_TienNt2();
        }

        public void GiaNt21_V6LostFocus(object sender)
        {
            TinhTienNt2();
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
            if (_maLo.Text == "") GetLoDate();
            else GetLoDate13();
        }
        private void XuLyChonMaVt(string mavt)
        {
            XuLyLayThongTinKhiChonMaVt();
            XuLyDonViTinhKhiChonMaVt(mavt);
            //{Tuanmh 14-09/2017 get tk_dl from alkho
            if (_maKhoI.Text != "")
                XuLyLayThongTinKhiChonMaKhoI();
            //}

            GetGia();
            GetTon13();
            GetLoDate();

            if (_soLuong1.Value != 0) TinhTienNt2();
            
            if (V6Options.M_AUTO_SAVEDETAIL)
            {
                XuLyAutoSaveDetail();
            }
        }

        private void XuLyAutoSaveDetail()
        {
            try
            {
                //detail1.OnNhanClick();
                detail1.OnAddHandle();
                detail1.OnMoiClick();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
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

                _dataLoDate = V6BusinessHelper.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Date);
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

                        //if (new_soLuong < 0) new_soLuong = 0;
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

                _dataLoDate = V6BusinessHelper.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Date);
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
                            _ton13.Value = new_soLuong / _heSo1.Value;
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

        public void GetTon13()
        {
            try
            {
                if ((_maVt.LO_YN || _maVt.DATE_YN) && (_maKhoI.LO_YN || _maKhoI.DATE_YN))
                    return;

                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
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

                                //if (new_soLuong < 0) new_soLuong = 0;
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
        public void XuLyLayThongTinKhiChonMaVt()
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

                }
                else
                {
                    SetADSelectMoreControlValue(Invoice, data);
                    //_tkDt.Text = (data["tk_dt"] ?? "").ToString().Trim();
                    //_tkGv.Text = (data["tk_gv"] ?? "").ToString().Trim();
                    SetControlValue(_tkDt, data["tk_dt"], Invoice.GetTemplateSettingAD("TK_DT"));
                    SetControlValue(_tkGv, data["tk_gv"], Invoice.GetTemplateSettingAD("TK_GV"));
                    SetControlValue(_soLuong1, data["PACKS1"], Invoice.GetTemplateSettingAD("PACKS1"));
                    if (V6Options.GetValue("M_SOA_HT_KM_CK") == "1")
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }


        public void GetGia()
        {
            try
            {
                if (txtMaGia.Text.Trim() == "") return;

                var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), _maVt.Text, _dvt1.Text, txtMaKh.Text, txtMaGia.Text);
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
                //TinhTienNt2();
            }
            else
            {
                _heSo1.Value = 1;
            }
        }

        public void TinhTienNt2()
        {
            try
            {
                if (M_CAL_SL_QD_ALL == "0") TinhSoluongQuyDoi_0(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                if (M_CAL_SL_QD_ALL == "2") TinhSoluongQuyDoi_2(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
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
                if (M_CAL_SL_QD_ALL == "1") TinhSoluongQuyDoi_1(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienNt2 " + _sttRec, ex);
            }
        }
        public void TinhGiamGiaCt()
        {
            try
            {
                if (V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "2" ||
                    V6Options.GetValue("M_GIAVC_GIAGIAM_CT") == "3")
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

        public void TinhTienVon()
        {
            TinhGiaNt();
            
            _tienNt.Value = V6BusinessHelper.Vround((_soLuong.Value * _gia_nt.Value), M_ROUND_NT);
            _tien.Value = V6BusinessHelper.Vround((_tienNt.Value * txtTyGia.Value), M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }
        }

        public void TinhGiaNt()
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        public void TinhTienVon_GiaVon()
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

        public void TinhGiaNt2()
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

                if (readOnly)
                {
                    detail1.MODE = V6Mode.Lock;
                    detail3.MODE = V6Mode.Lock;
                }
                else
                {
                    XuLyKhoaThongTinKhachHang();
                    
                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _tienNt2.Enabled = chkSuaTien.Checked;
                    _dvt1.Enabled = true;

                    _tienNt.Enabled = chkSuaTien.Checked && _xuat_dd.Text!="";

                    //{Tuanmh 02/12/2016
                    _ckNt.Enabled = !chkLoaiChietKhau.Checked;
                    _ck.Enabled = !chkLoaiChietKhau.Checked && _maNt != _mMaNt0;
                    _gia21.Enabled = chkSuaTien.Checked && _giaNt21.Value == 0 && _maNt != _mMaNt0;
                    _gia_nt.Enabled =  _xuat_dd.Text != "";
                    _gia.Enabled = _xuat_dd.Text != "" && _gia_nt.Value == 0 && _maNt != _mMaNt0;
                    _tien.Enabled = _xuat_dd.Text == "" && _tienNt.Value == 0 && _maNt != _mMaNt0;

                    //}


                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
                }

                //Cac truong hop khac
                if (!readOnly)
                {
                    chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                    txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
                    txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;

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

            SetControlReadOnlyHide(this, Invoice, Mode);
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
            FormatGridView();
        }
        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView3, _orderList3);
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

            f = dataGridView3.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("N6");
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
            btnTim.Enabled = true;//
            btnQuayRa.Enabled = true;

            btnViewInfoData.Enabled = false;
            switch (Mode)
            {
                case V6Mode.Add:
                    btnLuu.Enabled = true;
                    btnHuy.Visible = true;
                    btnHuy.Enabled = true;
                    btnQuayRa.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    break;
                case V6Mode.Edit:
                    btnLuu.Enabled = true;
                    btnHuy.Visible = true;
                    btnHuy.Enabled = true;
                    btnXoa.Visible = true;
                    btnXoa.Enabled = true;

                    btnQuayRa.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
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

                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    break;
                case V6Mode.Init:
                    btnMoi.Enabled = true;
                    btnTim.Enabled = true;
                    btnSua.Visible = true;
                    if (IsViewingAnInvoice) btnSua.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    break;
                default:
                    btnQuayRa.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    break;
            }
        }

        #endregion enable...


        #region ==== Tính toán hóa đơn ====
        
        public void TinhTongValues()
        {
            txtTongSoLuong1.Value = TinhTong(AD, "SO_LUONG1");
            txtTongSoLuong.Value = TinhTong(AD, "SO_LUONG");

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
            //}
    
        }


        public void TinhChietKhau()
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
            
                HienThiTongSoDong(lblTongSoDong);
                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt2, T_CK_NT, PT_CK
                TinhThue();
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

                //var tygia = txtTyGia.Value;
                //txtTongTien2.Value = V6BusinessHelper.Vround(t_tien_nt2*tygia, M_ROUND);
                //txtTongGiam.Value = V6BusinessHelper.Vround(t_gg_nt*tygia, M_ROUND);
                //TxtT_TIENVC.Value = V6BusinessHelper.Vround(t_vc_nt * tygia, M_ROUND);                
                //txtTongCk.Value = V6BusinessHelper.Vround(t_ck_nt*tygia, M_ROUND);
                //Co_Thay_Doi = true;
                txtConLai.Value = t_tt_nt - txtSL_UD1.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
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
            _mMaNt0 = V6Options.M_MA_NT0;
            //cboMaNt.SelectedValue = _mMaNt0;
            panelVND.Visible = false;
            TxtT_TIENVC.Visible = false;
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
            //Tuanmh 14/05/2017- Ma_dvcs
            if (V6Login.MadvcsCount >= 1)
            {
                if (MA_KHOPH != "")
                {
                    var alkho = V6BusinessHelper.Select("ALKHO", "MA_DVCS", "MA_KHO = '" + MA_KHOPH.Trim() + "'");
                    if (alkho.TotalRows > 0)
                    {
                        txtMadvcs.Text = alkho.Data.Rows[0]["MA_DVCS"].ToString().Trim();
                    }

                }
                else
                {
                    if (V6Login.Madvcs != "")
                    {
                        txtMadvcs.Text = V6Login.Madvcs;
                        txtMadvcs.ExistRowInTable();
                    }
                }
            }
           //Tuanmh 04/06/2017 ma_kh in alvitri
            if (MA_VITRIPH != "")
            {
                var alvitri = V6BusinessHelper.Select("ALVITRI", "MA_KH", "MA_VITRI = '" + MA_VITRIPH.Trim() + "'");
                if (alvitri.TotalRows > 0)
                {
                    if (!string.IsNullOrEmpty(alvitri.Data.Rows[0]["MA_KH"].ToString()))
                    {
                        txtMaKh.Text = alvitri.Data.Rows[0]["MA_KH"].ToString().Trim();
                        XuLyChonMaKhachHang();

                    }
                }
            }
            //M_Ma_nk
            Txtma_nk.Text = Invoice.Alct["M_MA_NK"].ToString().Trim();
            //txtLoaiPhieu.Text = "1";
            //
            txtManx.Text = Invoice.Alct["TK_NO"].ToString().Trim();
            txtTkThueNo.Text = txtManx.Text;
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
                if (txtSoPhieu.Text.Trim()=="")
                        txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

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
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaDVCS " + _sttRec, ex);
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
                    detail1.ShowIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2" });
                    panelVND.Visible = true;
                    

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA21");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var gridViewColumn = dataGridView1.Columns["GIA21"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    gridViewColumn = dataGridView1.Columns["TIEN2"];
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
                    _gia21.InvisibleTag();
                    _ck.InvisibleTag();
                    _tien_vc.InvisibleTag();
                    _gg.InvisibleTag();
                    if (_gia != null) _gia.InvisibleTag();
                    
                    //Detail3
                    detail3.HideIDs(new[] { "PS_NO", "PS_CO" });

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

                //GridView3
                column = dataGridView3.Columns["Ps_co_nt"];
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
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue " + _sttRec, ex);
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
                ADTables.Add(sttRec, Invoice.LoadAD(sttRec));
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

        
        public override void ShowParentMessage(string message)
        {
            try
            {
                var parent = Parent.Parent;
                for (int i = 0; i < 5; i++)
                {
                    if (parent is HoaDonCafeContainer)
                    {
                        ((HoaDonCafeContainer)parent)
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
                //Mode = V6Mode.View;
                _sttRec = sttrec;
                // Tìm trong AM.Rows. Nếu có thì view luôn
                //var index = 0;
                for (int i = 0; i < AM.Rows.Count; i++)
                {
                    var rowrec = AM.Rows[i]["Stt_rec"].ToString().Trim();
                    if (rowrec == sttrec)
                    {
                        ViewInvoice(i);
                        return;
                    }
                }

                //Nếu không có sẵn trong dữ liệu cục bộ. Tải lại dữ liệu, Thêm vào dòng mới cho AM
                var loadAM = Invoice.SearchAM("", "Stt_rec='" + _sttRec + "'", "", "", "");
                if (loadAM.Rows.Count == 1)
                {
                    var loadRow = loadAM.Rows[0];
                    if (_timForm != null && !_timForm.IsDisposed)
                        _timForm.UpdateAM(_sttRec, loadRow.ToDataDictionary(), V6Mode.Update);
                    var newRow = AM.NewRow();
                    for (int i = 0; i < AM.Columns.Count; i++)
                    {
                        newRow[i] = loadRow[i];
                    }
                    AM.Rows.Add(newRow);
                    CurrentIndex = AM.Rows.Count - 1;
                    ViewInvoice(CurrentIndex);
                }
                else // Không có dữ liệu theo stt_Rec
                {
                    ResetForm();
                }
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
                //Mode = V6Mode.View;
                V6ControlFormHelper.SetFormDataRow(this, AM.Rows[CurrentIndex]);
                txtMadvcs.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost);

                XuLyThayDoiMaDVCS();
                SetGridViewData();
                XuLyThayDoiMaNt();
                //Mode = V6Mode.View; // Cafe hoi khac!
                FormatNumberControl();
                FormatNumberGridView();
                FixValues();
                EnableFunctionButtons();//Add hoadonCafe
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
        private bool flagAddFinish, flagAddSuccess;

        public IDictionary<string, object> CurrentIndexAM_Data
        {
            get
            {
                if (CurrentIndex >= 0 && CurrentIndex < AM.Rows.Count)
                {
                    return AM.Rows[CurrentIndex].ToDataDictionary();
                }
                return null;
            }
        }

        private IDictionary<string, object> addDataAM;
        private List<IDictionary<string, object>> addDataAD, addDataAD3;
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
                if (!_post) goto End;

                if (flagAddSuccess)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.AddSuccess);
                    ShowParentMessage(V6Text.AddSuccess);
                    //ViewInvoice(_sttRec, V6Mode.Add);
                    if (!_post) GetCurrentIndex();
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
                    //Mode = V6Mode.Edit;
                }

                End:
                ((Timer)sender).Dispose();
                //if (_post && _print_flag != V6PrintMode.DoNoThing)
                //{
                //    var temp = _print_flag;
                //    _print_flag = V6PrintMode.DoNoThing;
                //    BasePrint(Invoice, _sttRec_In, temp, TongThanhToan, TongThanhToanNT, true);
                //    SetStatus2Text();
                //}
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

                if (Invoice.InsertInvoice(addDataAM, addDataAD, addDataAD3, _post))
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
        //private SortedDictionary<string, object> editDataAM;
        private List<IDictionary<string, object>> editDataAD, editDataAD3;
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

        private void checkEdit_Tick(object sender, EventArgs e)
        {
            if (flagEditFinish)
            {
                ((Timer)sender).Stop();
                if (!_post) goto End;

                if (flagEditSuccess)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                    ShowParentMessage(V6Text.Finish);
                    //if (_post) ViewInvoice(_sttRec, V6Mode.Edit);
                    //Status = "3";
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
                    //Mode = V6Mode.Edit;
                }

            End:
                ((Timer)sender).Dispose();
                if (_post && _print_flag != V6PrintMode.DoNoThing)
                {
                    var temp = _print_flag;
                    _print_flag = V6PrintMode.DoNoThing;
                    BasePrint(Invoice, _sttRec_In, temp, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
                //if (_post && _print_flag)
                //{
                //    _print_flag = false;
                //    In(_sttRec_In, V6PrintMode.AutoPrint, 3);
                //    SetStatus2Text();
                //}
            }
        }

        private void DoEdit()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                var keys = new SortedDictionary<string, object> { { "STT_REC", _sttRec } };
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, editDataAD3, keys, _post))
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
                ReadyForDelete();

                //if (Mode == V6Mode.View)
                {
                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                    {
                        DisableAllFunctionButtons(btnLuu, btnMoi, btnCopy, btnIn, btnSua, btnHuy, btnXoa, btnXem, btnTim, btnQuayRa);
                        // Cập nhập giá trị để thay đổi trên button
                        txtTongThanhToanNt.Value = 0;

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
                        Status = "0";
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
            }
        }

        private void checkDelete_Tick(object sender, EventArgs e)
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
                        //ViewInvoice(CurrentIndex);
                        btnMoi.Focus();
                    }
                    else
                    {
                        //ResetForm();
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

        private void ReadyForDelete()
        {
            _deleteRow = AM.Rows[CurrentIndex];
        }

        private DataRow _deleteRow;
        private string _delete_SttRec;

        private void DoDelete()
        {
            //Xóa xong view lại cái khác (trong timer tick)
            try
            {
                //var row = AM.Rows[CurrentIndex];
                _delete_SttRec = _deleteRow["Stt_rec"].ToString().Trim();
                if (Invoice.DeleteInvoice(_delete_SttRec))
                {
                    flagDeleteSuccess = true;
                    AM.Rows.Remove(_deleteRow);
                    ADTables.Remove(_delete_SttRec);
                    AD3Tables.Remove(_delete_SttRec);
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = "Xóa không thành công.";
                    Invoice.PostErrorLog(_delete_SttRec, "X");
                }
            }
            catch (Exception ex)
            {
                flagDeleteSuccess = false;
                deleteErrorMessage = ex.Message;
                Invoice.PostErrorLog(_delete_SttRec, "X " + _sttRec, ex);
            }
            flagDeleteFinish = true;
        }
        #endregion delete

        private bool _post;

        /// <summary>
        /// Lưu thông tin vào csdl
        /// </summary>
        /// <param name="ma_vitriPH"></param>
        /// <param name="post"></param>
        /// <param name="ma_khoPH"></param>
        internal void Luu(string ma_khoPH, string ma_vitriPH, bool post)
        {
            _post = post;
            try
            {
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    ShowMainMessage(V6Text.DetailNotComplete);
                    return;
                }

                if (_post) V6ControlFormHelper.RemoveRunningList(_sttRec);

                var time2 = V6BusinessHelper.GetServerDateTime().ToString("HH:mm:ss");
                Time2 = time2;
                addDataAM = PreparingDataAM(Invoice);
                addDataAM["TIME2"] = time2;
                addDataAM["MA_KHOPH"] = ma_khoPH;
                addDataAM["MA_VITRIPH"] = ma_vitriPH;
                
                addDataAM["STATUS"] = _post ? "3" : Status;
                //Update to AM
                V6BusinessHelper.UpdateRowToDataTable(AM, "Stt_rec", _sttRec, addDataAM);

                V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] {"SO_CT", "NGAY_CT", "MA_CT"}, AD);
                V6ControlFormHelper.UpdateDKlist(AD, "MA_KHO_I", addDataAM["MA_KHOPH"]);
                if (!string.IsNullOrEmpty(ma_vitriPH))
                    V6ControlFormHelper.UpdateDKlist(AD, "MA_VITRI", addDataAM["MA_VITRIPH"]);
                V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] {"SO_CT", "NGAY_CT", "MA_CT"}, AD2);
                V6ControlFormHelper.UpdateDKlistAll(addDataAM, new[] {"SO_CT", "NGAY_CT", "MA_CT"}, AD3);
                //V6ControlFormHelper.UpdateDKlistAll(GetData(), new[] { "SO_CT", "NGAY_CT", "MA_CT" }, AD);
                if (Mode == V6Mode.Add)
                {
                    //V6ControlFormHelper.DisableControls(btnLuu, btnHuy, btnQuayRa);
                    DoAddThread();
                    if(!post) GetCurrentIndex();
                    return;
                }
                if (Mode == V6Mode.Edit)
                {
                    //V6ControlFormHelper.DisableControls(btnLuu, btnHuy, btnQuayRa);
                    if (!post) GetCurrentIndex();
                    DoEditThread();
                    return;
                }
                if (Mode == V6Mode.View)
                {
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void GetCurrentIndex()
        {
            try
            {
                if (AM == null || AM.Rows.Count == 0)
                {
                    ShowMainMessage(V6Text.NoData);
                    CurrentIndex = -1;
                    return;
                }

                for (int i = 0; i < AM.Rows.Count; i++)
                {
                    var row = AM.Rows[i];
                    if (_sttRec == row["Stt_rec"].ToString().Trim())
                    {
                        CurrentIndex = i;
                        return;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }


        private void Moi()
        {
            try
            {
                Status = "1";
                if (V6Login.UserRight.AllowAdd("", Invoice.CodeMact))
                {
                    FormManagerHelper.HideMainMenu();

                    //if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    //{
                    //    if (true)
                    //    {
                    //        if (this.ShowConfirmMessage(V6Text.DiscardConfirm) != DialogResult.Yes)
                    //            return;
                    //    }
                    //}

                    AM_old = IsViewingAnInvoice ? AM.Rows[CurrentIndex] : null;

                    ResetForm();
                    
                    txtLoaiPhieu.ChangeText(_maGd);
                    //LoadAll(V6Mode.Add);

                    GetSttRec(Invoice.Mact);
                    Mode = V6Mode.Add;

                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + MA_VITRIPH);

                    //GetSoPhieu();
                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    //GetSoPhieu();

                    SetDefaultData(Invoice);
                    detail1.DoAddButtonClick();
                    SetControlReadOnlyHide(detail1, Invoice, V6Mode.Add);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + MA_VITRIPH);
                if (!IsViewingAnInvoice) return;
                if (V6BusinessHelper.CheckEditVoucher(_sttRec, "ARS20", "S", Invoice.Mact) == 1)
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
                            //Tuanmh 24/07/2016 Check Debit Amount
                            bool check_edit = 
                                CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                    txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
                                    txtManx.Text.Trim(), dateNgayCT.Date, txtTongThanhToan.Value, "E");

                            if (check_edit == true)
                            {
                                Status = "1";
                                //Mode = V6Mode.Edit;
                                detail1.MODE = V6Mode.View;
                                detail3.MODE = V6Mode.View;
                                txtMa_sonb.Focus();
                                Luu(MA_KHOPH, MA_VITRIPH, false);
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
                if (V6BusinessHelper.CheckEditVoucher(_sttRec, "ARS20", "X", Invoice.Mact) == 1)
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
                        //Tuanmh 24/07/2016 Check Debit Amount
                        bool check_edit = 
                            CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
                                txtManx.Text.Trim(), dateNgayCT.Date, txtTongThanhToan.Value, "D");

                        if (check_edit == true)
                        {
                            DoDeleteThread();
                            ResetForm();
                            Status = "0";
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
                            GetSttRec(Invoice.Mact);
                            SetNewValues();
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + MA_VITRIPH);
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

        private void In(string sttRec_In, V6PrintMode printMode, int sec = 3)
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
                            "", "", "", sttRec_In);
                        c.TTT = txtTongThanhToan.Value;
                        c.TTT_NT = txtTongThanhToanNt.Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                            if (!sender.IsDisposed) sender.Dispose();
                        };

                        c.PrintMode = printMode;

                        c.ShowToForm(this, V6Text.PrintSOC, true);
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

        private TimHoaDonFormCafe _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) _timForm = new TimHoaDonFormCafe(this);
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
                        _timForm = new TimHoaDonFormCafe(this);
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
            return;
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                if (this.ShowConfirmMessage(V6Text.DiscardConfirm) != DialogResult.Yes)
                {
                    return;
                }
            }
            //Parent.Dispose();
            ResetForm();
            Mode = V6Mode.Init;
        }
        
        /// <summary>
        /// Chuyển bàn
        /// </summary>
        private void ChuyenBan()
        {
            OnChangeTable(new ChangeTableEventArgs());
        }

        /// <summary>
        /// Dọn bàn
        /// </summary>
        private void DonBan()
        {
            if (Status == "1" || Status == "2")
            {
                this.ShowWarningMessage(V6Text.NotAllow);
                return;
            }
            if (Status == "3")
            {
                ResetForm();
                Status = "0";
                ShowParentMessage("");
            }
        }

        //private V6PrintMode _print_flag = false;
        /// <summary>
        /// Lưu và in, có hiển thị form in trước 3 giây.
        /// </summary>
        private void LuuVaIn()
        {
            if (Status == "1" || Status == "2")
            {
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    ShowMainMessage(V6Text.DetailNotComplete);
                    return;
                }
                //_print_flag = true;
                //_sttRec_In = _sttRec;
                //Luu(MA_KHOPH, MA_VITRIPH, true);
                //Status = "3";

                var payform = new PayForm(TongThanhToan, txtTongTienNt2.DecimalPlaces);
                var dr = payform.ShowDialog(this);
                if (dr == DialogResult.Yes) // luu va in
                {
                    _print_flag = V6PrintMode.AutoPrint;
                    _sttRec_In = _sttRec;
                    txtSL_UD1.Value = payform.KhachDua;
                    if (ValidateData_Master())
                    {
                        Luu(MA_KHOPH, MA_VITRIPH, true);
                        Status = "3";
                    }
                    else
                    {
                        _print_flag = V6PrintMode.DoNoThing;
                    }
                    
                }
                else if (dr == DialogResult.OK) // luu ko in
                {
                    _print_flag = V6PrintMode.DoNoThing;
                    _sttRec_In = _sttRec;
                    txtSL_UD1.Value = payform.KhachDua;
                    if (ValidateData_Master())
                    {
                        Luu(MA_KHOPH, MA_VITRIPH, true);
                        Status = "3";
                    }
                }
                else
                {
                    DoNothing();
                }
            }
        }

        #region ==== Navigation function ====
        private void First()
        {
            var index = SearchFirstMaVitriIndex(CurrentIndex);
            if (index >= 0)
            {
                ViewInvoice(index);
                FixStatusByData();
                OnBillChanged();
            }
            else
            {
                ShowParentMessage("!!!!");
            }
        }

        private void Previous()
        {
            var index = SearchPreviousMaVitriIndex(CurrentIndex);
            if (index >= 0)
            {
                ViewInvoice(index);
                FixStatusByData();
                OnBillChanged();
            }
            else
            {
                ShowParentMessage("!!!!");
            }
        }

        /// <summary>
        /// Tìm các chứng từ có cùng ma_vitri
        /// </summary>
        private void Next()
        {
            var index = SearchNextMaVitriIndex(CurrentIndex);
            if (index >= 0)
            {
                ViewInvoice(index);
                //OnViewNext();
                FixStatusByData();
                
                OnBillChanged();
            }
            else
            {
                ShowParentMessage("!!!!");
            }
        }

        private void Last()
        {
            var index = SearchLastMaVitriIndex(CurrentIndex);
            if (index >= 0)
            {
                ViewInvoice(index);
                FixStatusByData();
                OnBillChanged();
            }
            else
            {
                ShowParentMessage("!!!!");
            }
        }

        public void FixStatusByData()
        {
            var AM_row = AM.Rows[CurrentIndex];
            var AM_status = AM_row["Status"].ToString().Trim();
            Status = AM_status;
        }

        private int SearchFirstMaVitriIndex(int currentIndex)
        {
            try
            {
                for (int i = 0; i < AM.Rows.Count; i++)
                {
                    var c_row = AM.Rows[i];
                    var c_ma_vitri = c_row["ma_vitriPH"].ToString().Trim().ToUpper();
                    if (c_ma_vitri == MA_VITRIPH)
                    {
                        return i == currentIndex ? -1 : i;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SearchNextMaVitriIndex " + _sttRec, ex);
            }
            return -1;
        }
        private int SearchPreviousMaVitriIndex(int currentIndex)
        {
            try
            {
                for (int i = (currentIndex - 1); i >= 0; i--)
                {
                    var c_row = AM.Rows[i];
                    var c_ma_vitri = c_row["ma_vitriPH"].ToString().Trim().ToUpper();
                    if (c_ma_vitri == MA_VITRIPH)
                    {
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SearchNextMaVitriIndex " + _sttRec, ex);
            }
            return -1;
        }
        private int SearchNextMaVitriIndex(int currentIndex)
        {
            try
            {
                for (int i = (currentIndex + 1); i < AM.Rows.Count; i++)
                {
                    var c_row = AM.Rows[i];
                    var c_ma_vitri = c_row["ma_vitriPH"].ToString().Trim().ToUpper();
                    if (c_ma_vitri == MA_VITRIPH)
                    {
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SearchNextMaVitriIndex " + _sttRec, ex);
            }
            return -1;
        }
        private int SearchLastMaVitriIndex(int currentIndex)
        {
            try
            {
                for (int i = AM.Rows.Count-1; i >= 0; i--)
                {
                    var c_row = AM.Rows[i];
                    var c_ma_vitri = c_row["ma_vitriPH"].ToString().Trim().ToUpper();
                    if (c_ma_vitri == MA_VITRIPH)
                    {
                        return i == currentIndex ? -1 : i;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SearchNextMaVitriIndex " + _sttRec, ex);
            }
            return -1;
        }


        #endregion navi f

        /// <summary>
        /// Xóa dữ liệu đang hiển thị
        /// </summary>
        public void ResetForm()
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
                EnableVisibleControls();
                SetFormDefaultValues();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public event EventHandler ResetAllVar;
        protected virtual void OnResetAllVar()
        {
            var handler = ResetAllVar;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void ResetAllVars()
        {
            _sttRec = "";
            CurrentIndex = -1;
            OnResetAllVar();
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
                txtSoPhieu.Text = ((TabControl)(p.Parent)).TabPages.Count.ToString();
            }
            else
            {
                txtSoPhieu.Text = "01";    
            }
        }

        private void GetSoPhieu(string ma_so_nb)
        {
            txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(ma_so_nb);
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
                SetControlReadOnlyHide(detail1, Invoice, V6Mode.Add);
                _maVt.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public override void SetDefaultDetail()
        {
            //Tuanmh 14/05/2017
            //MA_KHO_I
            _maKhoI.Text = MA_KHOPH;

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
                    var ma_kho = data["MA_KHO_I"].ToString().Trim();
                    var ma_vt = data["MA_VT"].ToString().Trim();

                    var gia_nt21 = ObjectAndString.ObjectToDecimal(data["GIA_NT21"]);
                    var cong_don_so_luong = false;
                    DataRow f_row = null;
                    if (V6Options.M_ADD_DETAILQUANTITY)
                    {
                        //Kiem tra ma_vt ton tai trong AD
                        
                        foreach (DataRow row in AD.Rows)
                        {
                            var c_ma_kho = row["ma_kho_i"].ToString().Trim();
                            var c_ma_vt = row["ma_vt"].ToString().Trim();
                            var c_gia_nt21 = ObjectAndString.ObjectToDecimal(row["gia_nt21"]);
                            if (c_ma_kho == ma_kho && c_ma_vt == ma_vt && c_gia_nt21 == gia_nt21)
                            {
                                f_row = row;
                                cong_don_so_luong = true;
                                break;
                            }
                        }
                    }

                    if (cong_don_so_luong)
                    {
                        //Xu ly sua du lieu cho f_row
                        //Cac truong cong don
                        var field_list = "so_luong1,so_luong,tien_nt2,tien2,gg_nt,gg,ck_nt,ck".Split(',');
                        foreach (string field in field_list)
                        {
                            var FIELD = field.Trim().ToUpper();
                            f_row[FIELD] = ObjectAndString.ObjectToDecimal(f_row[FIELD]) + ObjectAndString.ObjectToDecimal(data[FIELD]);
                        }
                    }
                    else
                    {
                        //Tạo dòng dữ liệu mới.
                        _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                        data["STT_REC0"] = _sttRec0;

                        var newRow = AD.NewRow();
                        foreach (DataColumn column in AD.Columns)
                        {
                            var KEY = column.ColumnName.ToUpper();
                            object value = ObjectAndString.ObjectTo(column.DataType,
                                data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                            newRow[KEY] = value;
                        }
                        AD.Rows.Add(newRow);
                    }

                    dataGridView1.DataSource = AD;
                    
                    var tData = _maVt.Data;
                    if (tData == null || tData["MA_THUE"] == null) goto Next1;
                    var maThue = tData["MA_THUE"].ToString().Trim();
                    if (maThue == "") goto Next1;
                    if(V6Options.GetValue("M_SOA_MULTI_VAT") != "0") goto Next1;
                    
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
                    
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "Ma_vt");
                    }
                }
                else
                {
                    ShowParentMessage(V6Text.CheckData + MA_VITRIPH + " " + error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
                return false;
            }
        Next1:
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
                            if (V6Options.GetValue("M_SOA_MULTI_VAT") != "0") goto Next1;

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
        Next1:
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
                            Luu(MA_KHOPH, MA_VITRIPH, false);
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
                Luu(MA_KHOPH, MA_VITRIPH, true);
                Status = "3";
            }
            else
            {
                EnableFunctionButtons();
            }
        }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            Moi();
            Luu(MA_KHOPH, MA_VITRIPH, false);
            CurrentIndex = AM.Rows.Count - 1;//!!!!!
            //Time0, Time2
            var time = V6BusinessHelper.GetServerDateTime();
            Time0 = time.ToString("HH:mm:ss");
            //OnBillChanged();
            Mode = V6Mode.Edit;
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
            Status = "0";
        }
        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            Luu(MA_KHOPH, MA_VITRIPH, false);
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
        private void hoaDonDetail1_AddHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data))
                {
                    Luu(MA_KHOPH, MA_VITRIPH, false);
                    return;
                }
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail1_EditHandle(IDictionary<string,object> data)
        {
            dataGridView1.UnLock();
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data))
                {
                    Luu(MA_KHOPH, MA_VITRIPH, false);
                    return;
                }
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail1_ClickDelete(object sender)
        {
            XuLyXoaDetail();
        }
        private void hoaDonDetail1_ClickCancelEdit(object sender)
        {
            dataGridView1.UnLock();
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

        #endregion hoadoen detail event

        /// <summary>
        /// Thêm chi tiết hóa đơn
        /// </summary>
        

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
                //_ckNt.Enabled = true; //Bỏ rào để sử dụng nhập tiền ck
                //_ckNt.Tag = null;
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
                    txtTongThueNt.ReadOnly = !chkSuaTienThue.Checked;

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
            else if(!(new List<string> {"TEN_VT","MA_VT"}).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
        }

        private void txtSoCt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + MA_VITRIPH);
                return; // Có thay đổi so với các chứng từ khác
                SetTabPageText(txtSoPhieu.Text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtSoCt_TextChanged " + _sttRec, ex);
            }
        }
        
        private void btnIn_Click(object sender, EventArgs e)
        {
            In(_sttRec, V6PrintMode.DoNoThing);
            SetStatus2Text();
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
            OnBillChanged();
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
            }
            if (chkSuaTien.Checked)
            {
                _tienNt2.Tag = null;
                _tienNt.Tag = null;
            }
            else
            {
                _tienNt2.Tag = "disable";
                _tienNt.Tag = "disable";
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
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                    if (_gv1EditingRow == null)
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                        return;
                    }
                    dataGridView1.Lock();
                    detail1.ChangeToEditMode();
                    SetControlReadOnlyHide(detail1, Invoice, V6Mode.Edit);

                    _maVt.RefreshLoDateYnValue();
                    _maKhoI.RefreshLoDateYnValue();
                    XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                    _maVt.Focus();
                    GetLoDate13();
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
                        txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), _sttRec,txtMadvcs.Text.Trim(),txtMaKh.Text.Trim(),
                        txtManx.Text.Trim(), dateNgayCT.Date, txtMa_ct.Text, txtTongThanhToan.Value, mode_vc,V6Login.UserId);

                    

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
                if (_tkDt.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                    _tkDt.Focus();
                    return false;
                }

                string errors = ValidateDetailData(Invoice, data);
                if (!string.IsNullOrEmpty(errors))
                {
                    this.ShowWarningMessage(errors);
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
            ShowViewInfoData(Invoice);
        }

        private void txtMa_sonb_V6LostFocus(object sender)
        {
            GetSoPhieu(txtMa_sonb.Text);
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
                if (txtSoPhieu.Text.Trim() == "")
                {
                    GetSoPhieu(txtMa_sonb.Text);
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
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CDH_HoaDonForm chon = new CDH_HoaDonForm(dateNgayCT.Date, txtMadvcs.Text, txtMaKh.Text);
                    _chon_px = "DH";
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
                this.ShowErrorException(GetType() + ".ChucNang_ChonDonHang " + _sttRec, ex);
            }
        }

        private void ChucNang_ChonBaoGia()
        {
            try
            {
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CBG_HoaDonForm chon = new CBG_HoaDonForm(txtMadvcs.Text, txtMaKh.Text);
                    _chon_px = "BG";
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
                this.ShowErrorException(GetType() + ".ChucNang_ChonBaoGia " + _sttRec, ex);
            }
        }

        private string _chon_px = "";
        void chon_AcceptSelectEvent(List<IDictionary<string, object>> selectedDataList)
        {
            try
            {
                detail1.MODE = V6Mode.View;
                AD.Rows.Clear();
                int addCount = 0, failCount = 0;
                foreach (IDictionary<string, object> data in selectedDataList)
                {
                    if (XuLyThemDetail(data)) addCount++;
                    else failCount++;
                }
                All_Objects["selectedDataList"] = selectedDataList;
                InvokeFormEvent("AFTERCHON_" + _chon_px);
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed {1}.", addCount, failCount));
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

        private void chonTuExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ChonTuExcel();
        }

        private void ChucNang_ChonTuExcel()
        {
            try
            {
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
                this.ShowErrorException(GetType() + ".ChonTuExcel " + _sttRec, ex);
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

        private void dateNgayCT_Leave(object sender, EventArgs e)
        {
            //string message = "";
            //bool check = V6BusinessHelper.CheckNgayCt(dateNgayCT.Date, out message);
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

        private void txtManx_LostFocus(object sender, EventArgs eventArgs)
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
            var data = txtMaKh.Data;
            if (data == null)
            {
                this.ShowWarningMessage("Chưa chọn mã khách hàng!", 300);
                return;
            }
            txtDiaChiGiaoHang.ParentData = data.ToDataDictionary();
            txtDiaChiGiaoHang.SetInitFilter(string.Format("MA_KH='{0}'", txtMaKh.Text));
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
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CPX_HoaDonForm chon = new CPX_HoaDonForm(dateNgayCT.Date, txtMadvcs.Text, txtMaKh.Text);
                    _chon_px = "PX";
                    chon.AcceptSelectEvent += chon_AcceptSelectEvent;
                    chon.ShowDialog(this);
                }
                else
                {
                    if (ma_kh == "")
                        message += V6Setting.IsVietnamese
                            ? "Chưa chọn mã khách hàng!\n"
                            : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "")
                        message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID needs to enter!";
                    this.ShowWarningMessage(message);
                    if (ma_kh == "") txtMaKh.Focus();
                    else if (ma_dvcs == "") txtMadvcs.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView3);
        }

        private void btnApGia_Click(object sender, EventArgs e)
        {
            ApGiaBan();
            _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
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

                    var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Date,
                        cboMaNt.SelectedValue.ToString().Trim(), maVatTu, dvt1, txtMaKh.Text, txtMaGia.Text);

                    var giaNt21 = ObjectAndString.ObjectToDecimal(dataGia["GIA_NT2"]);
                    row["GIA_NT21"] = giaNt21;
                    //_soLuong.Value = _soLuong1.Value * _heSo1.Value;
                    tienNt2 = V6BusinessHelper.Vround((soLuong1 * giaNt21), M_ROUND_NT);
                    row["tien_Nt2"] = tienNt2;
                    _tien2.Value = V6BusinessHelper.Vround((_tienNt2.Value * txtTyGia.Value), M_ROUND);

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
                        var tien2 = ObjectAndString.ObjectToDecimal(row["tien2"]);
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
                this.ShowErrorException(GetType() + ".ApGiaBan " + _sttRec, ex);
            }
        }

        private void txtGC_UD1_TextChanged(object sender, EventArgs e)
        {
            OnBillChanged();
        }

        private void chonHoaDonMauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChonHoaDonMau();
        }

        void ChonHoaDonMau()
        {
            if (Status != "1" && Status != "2")
            {
                ShowParentMessage("Mode!");
                return;
            }

            var advance = string.Format("Ma_kho='{0}' and Ma_vitri='{1}'", MA_KHOPH, MA_VITRIPH);
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", Invoice.Mact), 
                new SqlParameter("@ngay_ct", dateNgayCT.Date.Date), //.ToString("yyyyMMdd")
                new SqlParameter("@user_id", V6Login.UserId), 
                new SqlParameter("@advance", advance), 
            };
            DataTable table = V6BusinessHelper.ExecuteProcedure("VPA_Get_Voucher_Template", plist).Tables[0];
            //Check field giong excel.
            var checkFields = "MA_VT,MA_KHO_I,TIEN_NT2,SO_LUONG1,GIA_NT21";
            if (!V6ControlFormHelper.CheckDataFields(table, ObjectAndString.SplitString(checkFields)))
            {
                this.ShowWarningMessage("Dữ liệu không hợp lệ! " + checkFields);
                return;
            }

            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO_I")
                && table.Columns.Contains("TIEN_NT2") && table.Columns.Contains("SO_LUONG1")
                && table.Columns.Contains("GIA_NT21"))
            {
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

                if (count > 0) Luu(MA_KHOPH, MA_VITRIPH, false);
                
                ShowParentMessage(count > 0
                ? string.Format("Đã thêm {0} chi tiết.", count) + _message
                : "Không thêm được chi tiết nào." + _message);
            }
            else
            {
                ShowParentMessage("Không có đủ thông tin!");
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

        private void inKhacToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
