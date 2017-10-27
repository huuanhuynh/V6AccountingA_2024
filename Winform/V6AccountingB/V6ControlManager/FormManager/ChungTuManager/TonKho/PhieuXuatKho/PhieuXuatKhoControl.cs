using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho
{
    public partial class PhieuXuatKhoControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice84 Invoice = new V6Invoice84();
        private string _maGd = "2";
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuXuatKhoControl()
        {
            InitializeComponent();
            MyInit();
        }
        public PhieuXuatKhoControl(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Dùng để khởi tạo sửa
        /// </summary>
        public PhieuXuatKhoControl(string itemId, string sttRec)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {   
            LoadLanguage();
            LoadTag(Invoice, detail1.panelControls);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;

            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                txtMa_sonb.SetInitFilter("MA_DVCS='" + V6Login.Madvcs + "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
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
            cboKieuPost.SelectedIndex = 0;

            LoadDetailControls();
            detail1.AddContexMenu(menuDetail1);
            ResetForm();

            _maGd = (Invoice.Alct.Rows[0]["M_MA_GD"] ?? "2").ToString().Trim();
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));

            LoadAll();
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====


        private V6ColorTextBox _dvt;
        private V6CheckTextBox _xuat_dd;
        private V6VvarTextBox _maVt, _dvt1, _maKho2, _Ma_nx_i, _tkVt, _maLo, _maKhoI, _maViTri;
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2, _sl_td1;
        private V6NumberTextBox _ton13, _gia, _gia_nt, _tien, _tienNt, _gia1, _gia_nt1;
        private V6DateTimeColor _hanSd;
        string[] alctct_GRD_HIDE = new string[] { };
        string[] alctct_GRD_READONLY = new string[] { };
        
        private void LoadDetailControls()
        {
            try
            {
                //Lấy các control động
                var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);
                //Lấy thông tin Alctct
                var alctct = V6BusinessHelper.GetAlctCt(Invoice.Mact);

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
                    switch (NAME)
                    {
                        case "MA_VT":
                            _maVt = (V6VvarTextBox)control;
                            _maVt.Upper();
                            _maVt.LO_YN = false;
                            _maVt.DATE_YN = false;

                            _maVt.BrotherFields = "ten_vt,ten_vt2,dvt,ma_qg,ma_vitri";

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

                                //Tuanmh 21/08/2017
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
                                    
                                    GetTon13();
                                    GetLoDate13();
                                }
                            };
                            break;
                        case "MA_NX_I":
                            _Ma_nx_i = (V6VvarTextBox)control;
                            _Ma_nx_i.Upper();
                            _Ma_nx_i.FilterStart = true;

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
                            };
                            break;
                        case "DVT":
                            _dvt = (V6ColorTextBox)control;
                            _dvt.Tag = "hide";
                            break;
                        case "MA_KHO2":
                            _maKho2 = (V6VvarTextBox)control;
                            _maKho2.Upper();
                            _maKho2.V6LostFocus += MaKho2V6LostFocus;
                            _maKho2.Tag = "hide";
                            break;
                        case "MA_KHO_I":
                            _maKhoI = (V6VvarTextBox)control;
                            _maKhoI.Upper();
                            _maKhoI.LO_YN = false;
                            _maKhoI.DATE_YN = false;

                            _maKhoI.V6LostFocus += MaKhoI_V6LostFocus;

                            _maKhoI.V6LostFocusNoChange += delegate
                            {
                                XuLyChonMaKhoI();
                            };

                            break;
                        case "SL_QD":
                            _sl_qd = control as V6NumberTextBox;
                            if (_sl_qd != null)
                            {
                                _sl_qd.DisableTag();
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
                        case "TON13":
                            _ton13 = (V6NumberTextBox)control;
                            _ton13.Tag = "disable";
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
                                _soLuong1.V6LostFocus += delegate
                                {
                                    CheckSoLuong1();
                                };
                                _soLuong1.LostFocus += delegate
                                {
                                    SetControlValue(_sl_td1, _soLuong1.Value, Invoice.GetTemplateSettingAD("SL_TD1"));
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
                                    _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                                }
                            };
                            break;

                        //_tien2.V6LostFocus;
                        case "TIEN":
                            _tien = control as V6NumberTextBox;
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
                                _tienNt.Enabled = chkSua_Tien.Checked;
                                if (chkSua_Tien.Checked)
                                {
                                    _tienNt.Tag = null;
                                }
                                else
                                {
                                    _tienNt.Tag = "disable";
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

                        case "GIA_NT1":
                            _gia_nt1 = control as V6NumberTextBox;
                            if (_gia_nt1 != null)
                            {
                                _gia_nt1.V6LostFocus += delegate
                                {
                                    TinhTienVon1();
                                    TinhGiaVon();
                                };
                                if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                                {
                                    _gia_nt1.InvisibleTag();
                                }
                                if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                                {
                                    _gia_nt1.ReadOnlyTag();
                                }
                            }
                            break;
                        case "GIA1":
                            _gia1 = control as V6NumberTextBox;
                            if (_gia1 != null)
                            {
                                if (!V6Login.IsAdmin && alctct_GRD_HIDE.Contains(NAME))
                                {
                                    _gia1.InvisibleTag();
                                }
                                if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
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
                                        if (chkSua_Tien.Checked)
                                        {
                                            _tienNt.Enabled = true;
                                        }
                                        else
                                        {
                                            _tienNt.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        _gia_nt1.Enabled = false;
                                        _gia_nt.Enabled = false;
                                        _tienNt.Enabled = false;
                                        _tien.Enabled = false;
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

                                _dataViTri = Invoice.GetLoDate(_maVt.Text, _maKhoI.Text, _sttRec, dateNgayCT.Value);
                                var filter = "Ma_vt='" + _maVt.Text.Trim() + "'";
                                var getFilter = GetFilterMaLo(_dataViTri, _sttRec0, _maVt.Text, _maKhoI.Text);
                                if (getFilter != "") filter += " and " + getFilter;
                                _maLo.SetInitFilter(filter);
                            };
                            //_maLo.V6LostFocus += sender =>
                            //{
                            //    CheckMaLo();
                            //};

                            //_maLo.V6LostFocusNoChange += delegate
                            //{
                            //    XuLyLayThongTinKhiChonMaLo();
                            //    GetLoDate13();
                            //    if (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO))
                            //    {
                            //        if (_soLuong1.Value > _ton13.Value)
                            //        {
                            //            _soLuong1.Value = _ton13.Value;
                            //            TinhTienVon1();
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
                            _hanSd.Tag = "disable";
                            break;
                        case "MA_VITRI":
                            _maViTri = control as V6VvarTextBox;
                            if (_maViTri != null)
                            {
                                _maViTri.GotFocus += (s, e) =>
                                {
                                    _maViTri.CheckNotEmpty = _maVt.VITRI_YN;
                                    var filter = "Ma_kho='" + _maKhoI.Text.Trim() + "'";
                                    _maViTri.SetInitFilter(filter);
                                };

                                _maViTri.LostFocus += delegate
                                {
                                    //CheckMaViTri();
                                    if (!_maViTri.ReadOnly)
                                    {
                                        CheckMaVitriTon();
                                    }
                                };
                            }
                            break;
                    }

                }

                foreach (Control control in dynamicControlList.Values)
                {
                    detail1.AddControl(control);
                }

                detail1.SetStruct(Invoice.ADStruct);
                detail1.MODE = detail1.MODE;
                V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadDetailControls: " + ex.Message);
            }
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

        private void CheckMaVitriTon()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
            if (detail1.MODE != V6Mode.Add && detail1.MODE != V6Mode.Edit) return;
            if (!_maVt.VITRI_YN) return;

            try
            {
                Invoice.GetAlVitriTon(dateNgayCT.Value, _sttRec, _maVt.Text, _maKhoI.Text);
                FixAlVitriTon(Invoice.AlVitriTon, AD);

                var inputUpper = _maViTri.Text.Trim().ToUpper();
                if (Invoice.AlVitriTon != null && Invoice.AlVitriTon.Rows.Count > 0)
                {
                    var check = false;
                    foreach (DataRow row in Invoice.AlVitriTon.Rows)
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
                        var f = new FilterView(Invoice.AlVitriTon, "Ma_vitri", "ALVITRITON", _maViTri, initFilter);
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
                    ShowMainMessage("AlvitriTon null");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckVitriTon: " + ex.Message);
            }
        }

        private void FixAlVitriTon(DataTable alVitriTon, DataTable ad)
        {
            try
            {

                string sttRec0 = _sttRec0;
                //string maVt = _maVt.Text.Trim().ToUpper();
                //string maKhoI = _maKhoI.Text.Trim().ToUpper();
                //string maLo = _maLo.Text.Trim().ToUpper();
               // string maViTri = _maViTri.Text.Trim().ToUpper();

                // Theo doi lo moi check
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                //if (maVt == "" || maKhoI == "" || maLo == "") return;

                //Xử lý - tồn
                //, Ma_kho, Ma_vt, Ma_vitri, Ma_lo, Hsd, Dvt, Tk_dl, Stt_ntxt,
                //  Ten_vt, Ten_vt2, Nh_vt1, Nh_vt2, Nh_vt3, Ton_dau, Du_dau, Du_dau_nt

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
                        //if (maVt == data_maVt && maKhoI == data_maKhoI && maLo == data_maLo && maViTri == data_maViTri)
                        //{


                        string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                        string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                        string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
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
                this.ShowErrorMessage(GetType() + ".FixAlVitriTon " + ex.Message);
            }
        }

        private void XuLyKhiNhanMaVitri(IDictionary<string, object> row)//, DataRow row0)
        {
            try
            {
                _maLo.Text = row["MA_LO"].ToString().Trim();
                _hanSd.Value = ObjectAndString.ObjectToDate(row["HSD"]);
                _ton13.Value = ObjectAndString.ObjectToDecimal(row["TON_DAU"]);
                _maLo.Enabled = false;
                _maLo.ReadOnlyTag();

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
                TinhTienVon1();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckSoLuong1: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy động danh sách control (textbox) từ bảng Alct
        /// </summary>
        /// <param name="mact"></param>
        /// <returns></returns>
        public SortedDictionary<int, Control> GetDynamicControlsAlct(string mact)
        {
            //exec [VPA_GET_AUTO_COLULMN] 'SOA','','','','';//08/12/2015
            var result = new SortedDictionary<int, Control>();
            
            var alct1 = Invoice.Alct1;
            _orderList = new List<string>();
            _alct1Dic = new SortedDictionary<string, DataRow>();
            
            foreach (DataRow row in alct1.Rows)
            {
                var visible = 1 == Convert.ToInt32(row["visible"]);
                if (!visible) continue;

                var fcolumn = row["fcolumn"].ToString().Trim().ToUpper();
                _orderList.Add(fcolumn);
                _alct1Dic.Add(fcolumn, row);

                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                var fvvar = row["fvvar"].ToString().Trim();
                var fstatus = Convert.ToBoolean(row["fstatus"]);
                
                var width = Convert.ToInt32(row["width"]);
                var ftype = row["ftype"].ToString().Trim();
                var fOrder = Convert.ToInt32(row["forder"]);

                int decimals;

                switch (ftype)
                {
                    case "A0":
                        if (fcolumn == "TANG")
                        {
                            result.Add(fOrder, V6ControlFormHelper.
                                CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus));
                        }
                        else if(fcolumn == "PX_GIA_DDI")
                        {
                            result.Add(fOrder, V6ControlFormHelper.
                                CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus));
                        }
                        break;
                    case "C0":
                        if (fvvar != "")
                        {
                            var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                            var notempty = Convert.ToBoolean(row["notempty"]);
                            result.Add(fOrder, V6ControlFormHelper.
                                CreateVvarTextBox(fcolumn, fvvar, fcaption, width, fstatus, checkvvar, notempty));
                        }
                        else
                        {
                            result.Add(fOrder, V6ControlFormHelper.
                                CreateColorTextBox(fcolumn, fcaption, width, fstatus));
                        }
                        break;
                    case "N9"://Kieu so bat ky
                        decimals = row["fdecimal"] == null ? V6Setting.DecimalsNumber : Convert.ToInt32(row["fdecimal"]);
                        result.Add(fOrder, V6ControlFormHelper.
                                CreateNumberTextBox(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;

                    case "N0"://Tien
                        decimals =V6Options.M_IP_TIEN;// row["fdecimal"] == null ? V6Setting.DecilalsNumber : Convert.ToInt32(row["fdecimal"]);
                        result.Add(fOrder, V6ControlFormHelper.
                                CreateNumberTien(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;

                    case "N1"://Ngoai te
                        decimals =  V6Options.M_IP_TIEN_NT;

                        result.Add(fOrder, V6ControlFormHelper.
                                CreateNumberTienNt(fcolumn, fcaption, decimals, width, fstatus)
                                );
                        break;
                    case "N2"://so luong

                        decimals = V6Options.M_IP_SL;

                        result.Add(fOrder, V6ControlFormHelper.
                            CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;
                    case "N3"://GIA

                        decimals = V6Options.M_IP_GIA;

                        result.Add(fOrder, V6ControlFormHelper.
                            CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;
                    case "N4"://Gia nt

                        decimals = V6Options.M_IP_GIA_NT;

                        result.Add(fOrder, V6ControlFormHelper.
                            CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;
                    case "N5"://Ty gia
                        decimals = V6Options.M_IP_TY_GIA;

                        result.Add(fOrder, V6ControlFormHelper.
                            CreateNumberTyGia(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;
                    case "D0": // Allow null
                        result.Add(fOrder, V6ControlFormHelper.
                               CreateDateTimeColor(fcolumn, fcaption, width, fstatus));
                        break;
                    case "D1": // Not null
                        result.Add(fOrder, V6ControlFormHelper.
                               CreateDateTimePick(fcolumn, fcaption, width, fstatus));
                        break;
                }
            }
            
            return result;
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
        
        void MaKho2V6LostFocus(object sender)
        {
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
        
        void SoLuong1_V6LostFocus(object sender)
        {
            if (V6Options.M_CHK_XUAT == "0" && (_maVt.LO_YN || _maVt.VT_TON_KHO))
            {
                if (_soLuong1.Value > _ton13.Value)
                {
                    this.ShowWarningMessage(V6Text.StockoutWarning);
                    _soLuong1.Value = _ton13.Value;
                }
            }
            _soLuong.Value = _soLuong1.Value * _heSo1.Value;

            
            TinhTienVon();
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonMaKhachHang: " + ex.Message);
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
                    }
                    else
                    {
                        txtTenKh.Enabled = false;
                        txtDiaChi.Enabled = true;
                        txtMaSoThue.Enabled = false;
                    }
                }
                else
                {
                    txtTenKh.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtMaSoThue.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyKhoaThongTinKhachHang: " + ex.Message);
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

            GetTon13();
            if (_maVt.VITRI_YN)
            {
                if (_maVt.LO_YN && _maVt.DATE_YN)
                {
                    if (_maKhoI.Text == "")
                    {
                        GetViTriLoDate13();    
                    }
                    else
                    {
                        GetViTriLoDate();    
                    }
                    
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
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaLo " + ex.Message);
            }
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

                _dataViTri = Invoice.GetLoDate13(maVt, maKhoI, maLo, _sttRec, dateNgayCT.Value);
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

                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
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
                            _ton13.Value = new_soLuong / _heSo1.Value;
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetLoDate13 " + ex.Message);
            }
        }

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
                if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN)
                    return;


                // Tuanmh 21/08/2017 Get default Ma_kho
                if (maKhoI == "")
                {

                    _ton13.Value = 0;
                    _dataViTri = Invoice.GetViTriLoDate13(maVt, maKhoI, maLo, maViTri, _sttRec, dateNgayCT.Value);
                    if (_dataViTri.Rows.Count == 0)
                    {
                        _ton13.Value = 0;
                        _maLo.Clear();
                        _hanSd.Value = null;
                    }

                    //for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                    for (int i = 0; i <= _dataViTri.Rows.Count - 1; i++)
                    {
                        DataRow data_row = _dataViTri.Rows[i];
                        string data_maVt = data_row["Ma_vt"].ToString().Trim().ToUpper();
                        string data_maKhoI = data_row["Ma_kho"].ToString().Trim().ToUpper();
                        string data_maLo = data_row["Ma_lo"].ToString().Trim().ToUpper();
                        string data_maViTri = data_row["Ma_vitri"].ToString().Trim().ToUpper();

                        //- so luong
                        decimal data_soLuong = ObjectAndString.ObjectToDecimal(data_row["Ton_dau"]);
                        decimal new_soLuong = data_soLuong;

                        bool check_makhoi = false;



                        foreach (DataRow row in AD.Rows) //Duyet qua cac dong chi tiet
                        {


                            string c_sttRec0 = row["Stt_rec0"].ToString().Trim();
                            string c_maVt = row["Ma_vt"].ToString().Trim().ToUpper();
                            string c_maKhoI = row["Ma_kho_i"].ToString().Trim().ToUpper();
                            string c_maLo = row["Ma_lo"].ToString().Trim().ToUpper();
                            string c_maViTri = row["Ma_vitri"].ToString().Trim().ToUpper();

                            //Neu dung maVt, maLo, maViTri
                            if (c_maVt == data_maVt && c_maKhoI == data_maKhoI && c_maLo == data_maLo &&
                                c_maViTri == data_maViTri)
                            {

                                decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
                                if (detail1.MODE == V6Mode.Add || (detail1.MODE == V6Mode.Edit && c_sttRec0 != sttRec0))
                                {
                                    new_soLuong -= c_soLuong;
                                    check_makhoi = true;
                                    
                                }
                            }
                        }


                        if (new_soLuong > 0 && check_makhoi == true)
                        {
                            _maKhoI.Text = data_maKhoI;
                            _maLo.Text = data_maLo;
                            _maViTri.Text = data_maViTri;

                            //maKhoI = data_maKhoI;
                            //maLo = data_maLo;
                            //maViTri = data_maViTri;

                            _ton13.Value = new_soLuong / _heSo1.Value;
                            _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                            check_makhoi = true;
                            break;
                        }
                        else
                        {
                            check_makhoi = false;
                        }

                        if (check_makhoi == false)
                        {
                            if (new_soLuong > 0)
                            {
                                _maKhoI.Text = data_maKhoI;
                                _maLo.Text = data_maLo;
                                _maViTri.Text = data_maViTri;

                                _ton13.Value = new_soLuong/_heSo1.Value;
                                _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                                break;
                            }
                            else
                            {
                                _maKhoI.Text = "";
                                _maLo.Text = "";
                                _maViTri.Text = "";

                                _ton13.Value = 0;
                                _hanSd.Value = null;
                            }
                        }
                    }
                }
                else
                {
                    if (!_maVt.LO_YN || !_maVt.DATE_YN || !_maVt.VITRI_YN || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                        return;

                    if (maVt == "" || maKhoI == "" || maLo == "" || maViTri == "") return;

                    _ton13.Value = 0;
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
                                _ton13.Value = new_soLuong/_heSo1.Value;
                                _hanSd.Value = ObjectAndString.ObjectToDate(data_row["HSD"]);
                                _maLo.Text = data_maLo;
                                _maViTri.Text = data_maViTri;

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GetViTriLoDate13 " + ex.Message);
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
                if (!_maVt.LO_YN || !_maVt.DATE_YN
                    || !_maKhoI.LO_YN || !_maKhoI.DATE_YN)
                    return;

                if (maVt == "" || maKhoI == "") return;

                _dataViTri = Invoice.GetLoDate(maVt, maKhoI, _sttRec, dateNgayCT.Value);
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
                            decimal c_soLuong = ObjectAndString.ObjectToDecimal(row["So_luong"]); //???
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
                                if (maVt == c_maVt && maKhoI == c_maKhoI && data_maLo == c_maLo &&
                                    data_maViTri == c_maViTri)
                                {
                                    new_soLuong -= c_soLuong;
                                }
                            }
                        }

                        if (new_soLuong > 0)
                        {
                            _ton13.Value = new_soLuong/_heSo1.Value;
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
                    _dataViTri = Invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Value);
                    if (_dataViTri != null && _dataViTri.Rows.Count > 0)
                    {
                        string sttRec0 = _sttRec0;
                        //Trừ dần
                        for (int i = _dataViTri.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow data_row = _dataViTri.Rows[i];
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
                this.ShowErrorException(string.Format("{0} {1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                    _tkVt.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;
                }
                else
                {
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

                if (_Ma_nx_i.Text == "")
                {
                    _Ma_nx_i.Text = Invoice.Alct.Rows[0]["TK_NO"].ToString().Trim();
                }

                SetDefaultDataDetail(Invoice, detail1.panelControls);
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
                this.ShowErrorMessage(GetType() + ".XuLyDonViTinhKhiChonMaVt " + ex.Message);
            }
        }

        private void XuLyThayDoiDvt1()
        {
            if (_dvt1.Data == null) return;
            var he_so = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_so"]);
            if (he_so == 0) he_so = 1;
            _heSo1.Value = he_so;

            GetTon13();
            if (_maKhoI.Text.Trim() != "" && _maLo.Text.Trim() != "")
            {
                GetLoDate13();
            }
            _soLuong.Value = _soLuong1.Value * _heSo1.Value;
            
            CheckSoLuong1();
            //TinhTienVon1();
        }

        private void TinhTienVon1()
        {

            _soLuong.Value = _soLuong1.Value * _heSo1.Value;
            _tienNt.Value = V6BusinessHelper.Vround(_soLuong1.Value * _gia_nt1.Value, M_ROUND_NT);
            _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }
            TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
        }
        private void TinhTienVon()
        {
            try
            {
                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                _tienNt.Value = V6BusinessHelper.Vround(_soLuong.Value * _gia_nt.Value, M_ROUND_NT);
                _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien.Value = _tienNt.Value;
                }
                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhTienVon: " + ex.Message);
            }
        }
        private void TinhGiaVon()
        {
            try
            {

                _gia1.Value = V6BusinessHelper.Vround((_gia_nt1.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia1.Value = _gia_nt1.Value;
                }


                if (_soLuong.Value != 0 && _gia_nt.Value == 0)
                {
                    _gia_nt.Value = V6BusinessHelper.Vround(_tienNt.Value / _soLuong.Value, M_ROUND_GIA_NT);
                    _gia.Value = V6BusinessHelper.Vround(_tien.Value / _soLuong.Value, M_ROUND_GIA);

                    if (_maNt == _mMaNt0)
                    {
                        _gia.Value = _gia_nt.Value;

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaVon: " + ex.Message);
            }
        }
        
        private void TinhTienVon_GiaVon()
        {
            try
            {
                _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien.Value = _tienNt.Value;
                }

                _gia1.Value = V6BusinessHelper.Vround((_gia_nt1.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia1.Value = _gia_nt1.Value;
                }


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
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhTienVon_GiaVon: " + ex.Message);
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
                }
                else
                {
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _dvt1.Enabled = true;

                   

                    //{Tuanmh 20/02/2016

                    bool is_gia_dichdanh = _maVt.GIA_TON == 2 || _xuat_dd.Text != "";

                    _tienNt.Enabled = chkSua_Tien.Checked && is_gia_dichdanh;
                    _tien.Enabled = is_gia_dichdanh && _tienNt.Value == 0 && _maNt != _mMaNt0;

                    _gia_nt.Enabled = is_gia_dichdanh;
                    _gia.Enabled =  is_gia_dichdanh && _gia_nt.Value==0;
                    _gia_nt1.Enabled = is_gia_dichdanh;
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
            dataGridView1.DataSource = AD;
            ReorderDataGridViewColumns();
            GridViewFormat();
        }
        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
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
        
        private decimal TinhTong(string colName)
        {
            var total = 0m;
            try
            {
                if (AD != null && AD.Columns.Contains(colName))
                {
                    for (var j = 0; j < AD.Rows.Count; j++)
                    {
                        total += ObjectAndString.ObjectToDecimal(AD.Rows[j][colName]);
                    }
                    return total;
                }
                return total;
            }
            catch
            {
                return total;
            }
        }

        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSua_Tien);
                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
        }

        private void TinhTongValues()
        {
            var tSoLuong = TinhTong("SO_LUONG1");
            txtTongSoLuong.Value = V6BusinessHelper.Vround(tSoLuong, M_ROUND_NUM);
            var tTienNt = TinhTong("TIEN_NT");
            txtTongTienNt.Value = tTienNt;
            var tTien = TinhTong("TIEN");
            txtTongTien.Value = tTien;
        }
        

        private void TinhTongThanhToan(string debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
                //Tính tổng thanh toán.//con phan nt va tien viet chua ro rang.
            
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();
                TinhTongValues();
    
                if (string.IsNullOrEmpty(_mMaNt0)) return;
                
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format(" {0} {1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "Lỗi-TTT("+debug+")"), ex);
            }
        }

        #endregion tính toán

        #region ==== AM Methods ====
        private void LoadAll()
        {
            AM = Invoice.SearchAM("1=0", "1=0", "", "", "");//Làm AM khác null
            EnableControls();
            GetSoPhieuInit();
            LoadAlnt();
            LoadAlpost();
            GetM_ma_nt0();
            V6ControlFormHelper.LoadAndSetFormInfoDefine(Invoice.Mact, tabKhac, this);
            Ready();
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
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, ex.Message));
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
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, ex.Message));
            }
        }

        private void GetM_ma_nt0()
        {
            _mMaNt0 = V6Options.V6OptionValues["M_MA_NT0"];
            //cboMaNt.SelectedValue = _mMaNt0;
            panelVND.Visible = false;
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
            
            try
            {
                cboKieuPost.SelectedValue = Invoice.Alct.Rows[0]["M_K_POST"].ToString().Trim();
            }
            catch
            {}
            
            if (AM_old != null)
            {
                txtMa_sonb.Text = AM_old["Ma_sonb"].ToString().Trim();
                if (TxtSo_ct.Text.Trim()=="")
                        TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

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
                    //ShowIDs(["GIA21", "lblGIA21", "TIEN2", "lblTIEN2", "DivTienVND", "DOCSOTIEN_VND"], true);
                    detail1.ShowIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2" });
                    panelVND.Visible = true;
                    
                    var c = V6ControlFormHelper.GetControlByAccesibleName(detail1, "GIA21");
                    if (c != null) c.Visible = true;
                    
                    
                    var
                    dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["TIEN"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    
                    if (_gia != null) _gia.Visible = true;
                    if (_gia != null) _gia.Tag = null;

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
                    detail1.HideIDs(new[] { "GIA21", "lblGIA21", "TIEN2", "lblTIEN2" });
                    panelVND.Visible = false;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di

                    
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


                    if(_gia != null) _gia.Visible = false;
                    if (_gia != null) _gia.Tag = "hide";
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
        
        /// <summary>
        /// Lấy dữ liệu AD dựa vào rec, tạo 1 copy gán vào AD81
        /// </summary>
        /// <param name="sttRec"></param>
        public void LoadAD(string sttRec)
        {
            try
            {
                if (ADTables == null) ADTables = new SortedDictionary<string, DataTable>();

                if (ADTables.ContainsKey(sttRec)) AD = ADTables[sttRec].Copy();
                else
                {
                    ADTables.Add(sttRec, Invoice.LoadAd(sttRec));
                    AD = ADTables[sttRec].Copy();
                }
            }
            catch(Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadAD: " + ex.Message);
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
                this.ShowErrorMessage(GetType() + ".ViewInvoice2 " + ex.Message);
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
                txtLoaiPhieu.ExistRowInTable();
                txtMaKh.ExistRowInTable();

                XuLyThayDoiMaDVCS();
                //{Tuanmh 20/02/2016
                XuLyThayDoiMaNt();
                //}

                SetGridViewData();
                Mode = V6Mode.View;

                FormatNumberControl();
                FormatNumberGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewInvoice0 " + ex.Message);
            }
        }
        #endregion view invoice

        #region ==== Add Thread ====
        private bool flagAddFinish, flagAddSuccess;
        private SortedDictionary<string, object> addDataAM;
        private List<SortedDictionary<string, object>> addDataAD;
        private string addErrorMessage = "";

        private void DoAddThread()
        {
            ReadyForAdd();
            Timer checkAdd = new Timer();
            checkAdd.Interval = 500;
            checkAdd.Tick += checkAdd_Tick;
            flagAddFinish = false;
            flagAddSuccess = false;

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
                addDataAD = dataGridView1.GetData(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, ex.Message));
            }
        }
        
        private void DoAdd()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                
                if (Invoice.InsertInvoice(addDataAM, addDataAD))
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
        private List<SortedDictionary<string, object>> editDataAD;
        private string editErrorMessage = "";

        private void DoEditThread()
        {
            ReadyForEdit();
            Timer checkEdit = new Timer();
            checkEdit.Interval = 500;
            checkEdit.Tick += checkEdit_Tick;
            flagEditFinish = false;
            flagEditSuccess = false;

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
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.EditFail + ": " + editErrorMessage);
                    ShowParentMessage(V6Text.EditFail + ": " + editErrorMessage);
                    Mode = V6Mode.Edit;
                }

                ((Timer)sender).Dispose();
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
                    flagEditSuccess = true;
                    ADTables.Remove(_sttRec);
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
                        DisableAllFunctionButtons(btnLuu, btnMoi, btnCopy, btnIn, btnSua, btnHuy, btnXoa, btnXem, btnTim,
                            btnQuayRa);

                        Timer checkDelete = new Timer();
                        checkDelete.Interval = 500;
                        checkDelete.Tick += checkDelete_Tick;
                        flagDeleteFinish = false;
                        flagDeleteSuccess = false;

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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void checkDelete_Tick(object sender, EventArgs e)
        {
            if (flagDeleteFinish)
            {
                ((Timer)sender).Stop();

                if (flagDeleteSuccess)
                {
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
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = "Xóa không thành công.";
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice81.DeleteInvoice return false." + Invoice.V6Message);
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
                    this.ShowWarningMessage("Chưa hoàn tất chi tiết!");
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
                    detail1.DoAddButtonClick();
                    SetDefaultDetail();
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
                this.ShowErrorMessage(GetType() + ".Sua: " + ex.Message);
            }
        }

        private void Xoa()
        {
            try
            {
                if (IsViewingAnInvoice)
                {
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Xoa: " + ex.Message);
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
                            this.ShowWarningMessage("Chưa chọn chứng từ.");
                        }
                        else
                        {
                            GetSttRec(Invoice.Mact);
                            TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

                            //Thay the stt_rec new
                            foreach (DataRow dataRow in AD.Rows)
                            {
                                dataRow["STT_REC"] = _sttRec;
                            }
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);
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
                this.ShowErrorException(GetType() + ".Copy", ex);
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
                        var repFile = Invoice.Alct.Rows[0]["FORM"].ToString().Trim();
                        var repTitle = Invoice.Alct.Rows[0]["TIEU_DE_CT"].ToString().Trim();
                        var repTitle2 = Invoice.Alct.Rows[0]["TIEU_DE2"].ToString().Trim();

                        var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                            "", "", "", _sttRec);
                        c.TTT = txtTongTien.Value;
                        c.TTT_NT = txtTongTienNt.Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec);
                            if (!sender.IsDisposed) sender.Dispose();
                        };

                        c.ShowToForm(this, V6Text.PrintIXA);
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

        private TimPhieuXuatKhoForm _timForm;
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
                        _timForm = new TimPhieuXuatKhoForm(this);
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
                TxtSo_ct.Text = ((TabControl)p.Parent).TabPages.Count.ToString();
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

        private bool XuLyThemDetail(SortedDictionary<string, object> data)
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
                //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\nMã kho rỗng.";
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
                    TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);
                    
                    if (AD.Rows.Count > 0)
                    {
                        dataGridView1.Rows[AD.Rows.Count - 1].Selected = true;
                        dataGridView1.CurrentCell
                            = dataGridView1.Rows[AD.Rows.Count - 1].Cells["Ma_vt"];
                    }
                }
                else
                {
                    this.ShowWarningMessage("Kiểm tra lại dữ liệu:" + error);
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
                        //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "")
                        //    error += "\nMã kho rỗng.";
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
                            TinhTongThanhToan("xy ly sua detail");
                        }
                        else
                        {
                            this.ShowWarningMessage("Kiểm tra lại dữ liệu:" + error);
                            return false;
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng.");
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
                    this.ShowWarningMessage("Hãy chọn 1 dòng!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết: " + ex.Message);
            }
        }
        
        #endregion details

        #region ==== AM Events ====
        private void PhieuXuatKhoControl_Load(object sender, EventArgs e)
        {
            V6ControlFormHelper.SetStatusText2("Chứng từ.");
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

        #region ==== PhieuXuatKhoDetail Event ====
        private void detail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd();
        }
        private void detail1_AddHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLyThemDetail(data))
            {
                return;
            }
            throw new Exception("Add failed.");
        }
        private void detail1_EditHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLySuaDetail(data))
            {
                return;
            }
            throw new Exception("Edit failed.");
        }
        private void detail1_ClickDelete(object sender)
        {
            XuLyXoaDetail();
        }
        private void detail1_ClickCancelEdit(object sender)
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
                detail1.SetData(dataGridView1.GetCurrentRowData());
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
            SetTabPageText(TxtSo_ct.Text);
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + TxtSo_ct.Text);
        }

        private void txtMaKh_Leave(object sender, EventArgs e)
        {
            
        }

        private void PhieuXuatKhoControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                V6ControlFormHelper.SetStatusText2("Chứng từ.");
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            In();
        }
        
        private void chkSua_Tien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                _tienNt.Enabled = chkSua_Tien.Checked && _xuat_dd.Text != "";
            }
            if (chkSua_Tien.Checked)
            {
                _tienNt.Tag = null;
            }
            else
            {
                _tienNt.Tag = "disable";
            }
        }
        
        private void detail1_ClickEdit(object sender)
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
                this.ShowErrorMessage(GetType() + ".detail1_ClickEdit: " + ex.Message);
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
                if (cboKieuPost.SelectedIndex == -1)
                {
                    this.ShowWarningMessage("Chưa chọn kiểu post!");
                    cboKieuPost.Focus();
                    return false;
                }

                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage("Chưa nhập chi tiết!");
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

        private bool ValidateData_Detail(SortedDictionary<string, object> dic)
        {
            try
            {
                if (_tkVt.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                    return false;
                }
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
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            //e.Cancel = false;
        }

        private void txtTongTienNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongTienNt.Value, _maNt);
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
                this.ShowErrorMessage(GetType() + ".ChucNang TroGiup: " + ex.Message, "PhieuNhapMua Error");
            }
        }

        private void ChucNang_ChonTuExcel()
        {
            try
            {
                var chonExcel = new LoadExcelDataForm();
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                chonExcel.MA_CT = Invoice.Mact;
                chonExcel.AcceptData += chonExcel_AcceptData;
                chonExcel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChonTuExcel: " + ex.Message);
            }
        }
        
        private void ChucNang_XuatHetKho()
        {
            try
            {
                var xuatHetKho = new XuatHetKhoDataForm();
                xuatHetKho.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                xuatHetKho.AcceptData += xuatHetKho_AcceptData;
                xuatHetKho.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuatHetKho: " + ex.Message);
            }
        }

        private void ChucNang_TinhHaoHut()
        {
            try
            {
                var xuatHetKho = new TinhHaoHutDataForm();
                xuatHetKho.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
                xuatHetKho.AcceptData += tinhHaoHut_AcceptData;
                xuatHetKho.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuatHetKho: " + ex.Message);
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

        void xuatHetKho_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO"))
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
                    var cMaKhoI = data["MA_KHO"].ToString().Trim();
                    //var exist = V6BusinessHelper.IsExistOneCode_List("ALVT", "MA_VT", cMaVt);
                    //var exist2 = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KHO", cMaKhoI);
                    
                    //{ Tuanmh 31/08/2016 Them thong tin ALVT
                    _maVt.Text = cMaVt;
                    var datavt = _maVt.Data;
                    var tonCuoi = ObjectAndString.ObjectToDecimal(data["TON_CUOI"]);
                    var duCuoi = ObjectAndString.ObjectToDecimal(data["DU_CUOI"]);
                    if (cMaVt == "" || cMaKhoI == "" || (Math.Abs(tonCuoi)+Math.Abs(duCuoi)==0)) continue;

                    if (datavt != null)
                    {
                        //Nếu dữ liệu không (!) chứa mã nào thì thêm vào dữ liệu cho mã đó.
                        if (!data.ContainsKey("TEN_VT")) data.Add("TEN_VT", (datavt["TEN_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT1")) data.Add("DVT1", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("DVT")) data.Add("DVT", (datavt["DVT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("TK_VT")) data.Add("TK_VT", (datavt["TK_VT"] ?? "").ToString().Trim());
                        if (!data.ContainsKey("HE_SO1")) data.Add("HE_SO1", 1);
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
                    }
                    
                }
                ShowParentMessage(count > 0
                ? string.Format("Đã thêm {0} chi tiết.", count) + _message
                : "Không thêm được chi tiết nào." + _message);
            }
            else
            {
                ShowParentMessage("Không có đủ thông tin!");
            }
        }

        void tinhHaoHut_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_VT") && table.Columns.Contains("MA_KHO"))
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
                    var cMaKhoI = data["MA_KHO"].ToString().Trim();
                    
                   
                    //{ Tuanmh 31/08/2016 Them thong tin ALVT
                    _maVt.Text = cMaVt;
                    var datavt = _maVt.Data;
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
                        if (!data.ContainsKey("HE_SO1")) data.Add("HE_SO1", 1);
                        if (!data.ContainsKey("SO_LUONG")) data.Add("SO_LUONG", data["TON_CUOI"]);
                        if (!data.ContainsKey("SO_LUONG1")) data.Add("SO_LUONG1", data["TON_CUOI"]);

                        var __tien_nt0 = ObjectAndString.ToObject<decimal>(data["DU_CUOI"]);
                        var __tien0 = __tien_nt0;

                        if (!data.ContainsKey("TIEN0")) data.Add("TIEN0", __tien0);

                        if (!data.ContainsKey("TIEN_NT")) data.Add("TIEN_NT", __tien_nt0);
                        if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien0);

                    }

                    data["MA_KHO_I"] = cMaKhoI;
                    //data["MA_NX_I"] = data["TK_VT"];

                    if (XuLyThemDetail(data))
                    {
                        count++;
                    }

                }
                ShowParentMessage(count > 0
                ? string.Format("Đã thêm {0} chi tiết.", count) + _message
                : "Không thêm được chi tiết nào." + _message);
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

        private void XuatHetKhoMenu_Click(object sender, EventArgs e)
        {
            ChucNang_XuatHetKho();
        }

        private void tinhHaoHutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_TinhHaoHut();
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1);
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
                var data0 = V6BusinessHelper.ExecuteProcedure("VPA_Get_IXA_VIEWF5", plist);
                if (data0 == null || data0.Tables.Count == 0)
                {
                    ShowParentMessage(V6Text.NoData);
                    return;
                }

                var data = data0.Tables[0];
                FilterView f = new FilterView(data, "MA_KH", "IXA_VIEWF5", new V6ColorTextBox(), "");
                if (f.ViewData.Count > 0)
                {
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        var ROW = f.SelectedRowData;
                        if (ROW == null || ROW.Count == 0) return;

                        var datamavt = _maVt.Data;

                        if (_xuat_dd.Checked ||
                            (datamavt != null && ObjectAndString.ObjectToDecimal(datamavt["GIA_TON"]) == 2))
                        {
                            _gia1.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                            _gia_nt1.ChangeValue(ObjectAndString.ObjectToDecimal(ROW["GIA"]));
                        }
                    }
                }
                else
                {
                    ShowParentMessage("IXA_VIEWF5" + V6Text.NoData);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XemPhieuNhap", ex);
            }
        }

    }
}
