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
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai.ChonPhieuXuat;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai
{
    public partial class HangTraLaiControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        public V6Invoice76 Invoice = new V6Invoice76();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public HangTraLaiControl()
        {
            InitializeComponent();
            MyInit();
        }
        public HangTraLaiControl(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Dùng để khởi tạo sửa
        /// </summary>
        public HangTraLaiControl(string itemId, string sttRec)
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
            cboKieuPost.SelectedIndex = 0;

            LoadDetailControls();
            detail1.AddContexMenu(menuDetail1);
            ResetForm();

            LoadAll();
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====


        private V6ColorTextBox _dvt;
        private V6CheckTextBox _tang, _xuat_dd;
        private V6VvarTextBox _maVt, _dvt1, _maKho, _maKhoI, _tkTl, _tkGv, _tkCkI, _tkVt, _maLo;
        private V6NumberTextBox _soLuong1, _soLuong, _heSo1, _giaNt2, _giaNt21,_tien2, _tienNt2, _ck, _ckNt,_gia2,_gia21;
        private V6NumberTextBox _ton13, _gia, _gia_nt, _tien, _tienNt, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2;
        private V6DateTimeColor _hanSd;

        
        private void LoadDetailControls()
        {
            //Lấy các control động
            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);
            //Lấy thông tin Alctct
            var alctct = V6BusinessHelper.GetAlctCt(Invoice.Mact);
            var alctct_GRD_HIDE = new string[] { };
            var alctct_GRD_READONLY = new string[] { };
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
                

                if (control is V6NumberTextBox)
                {
                    //toolTip1.SetToolTip(control, ((V6NumberTextBox)control).TextTitle);
                    control.Enter += delegate(object sender, EventArgs e)
                    {
                        var s = ((V6NumberTextBox)sender).AccessibleName + ": " + ((V6NumberTextBox)sender).GrayText;
                        V6ControlFormHelper.SetStatusText(s);

                        var location = control.Location;
                        location.Y -= 22;
                        //toolTip1.Show(((V6NumberTextBox)sender).TextTitle, ((V6NumberTextBox)sender).Parent, location);
                    };
                }
                else if (control is V6ColorTextBox)
                {
                    //toolTip1.SetToolTip(control,((V6ColorTextBox)control).TextTitle);
                    control.Enter += delegate(object sender, EventArgs e)
                    {
                        var s = ((V6ColorTextBox)sender).AccessibleName + ": " + ((V6ColorTextBox)sender).GrayText;
                        V6ControlFormHelper.SetStatusText(s);

                        var location = control.Location;
                        location.Y -= 22;
                        //toolTip1.Show(((V6ColorTextBox)sender).TextTitle, ((V6ColorTextBox)sender).Parent, location);
                    };
                }
                else if (control is V6DateTimePick)
                {
                    //toolTip1.SetToolTip(control, ((V6ColorDateTimePick)control).TextTitle);
                    control.Enter += delegate(object sender, EventArgs e)
                    {
                        var s = ((V6DateTimePick)sender).AccessibleName + ": " + ((V6DateTimePick)sender).TextTitle;
                        V6ControlFormHelper.SetStatusText(s);

                        var location = control.Location;
                        location.Y -= 22;
                        //toolTip1.Show(((V6DateTimePick)sender).TextTitle, ((V6DateTimePick)sender).Parent, location);
                    };
                }

                var NAME = control.AccessibleName.ToUpper();

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
                    case "TK_TL":
                        _tkTl = (V6VvarTextBox)control;
                        _tkTl.Upper();
                        _tkTl.SetInitFilter("Loai_tk = 1");
                        _tkTl.FilterStart = true;
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
                    case "TON13":
                        _ton13 = (V6NumberTextBox)control;
                        _ton13.Tag = "disable";
                        break;
                    //_ton13.V6LostFocus += Ton13_V6LostFocus;
                    case "SO_LUONG1":
                        _soLuong1 = (V6NumberTextBox)control;
                        _soLuong1.V6LostFocus += SoLuong1_V6LostFocus;
                        _soLuong1.V6LostFocusNoChange += delegate
                        {

                        };
                        if (!V6Login.IsAdmin && alctct_GRD_READONLY.Contains(NAME))
                        {
                            _soLuong1.ReadOnlyTag();
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
                                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
                            }
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
                        _tienNt2 = control as V6NumberTextBox;
                        if (_tienNt2 != null)
                        {
                            _tienNt2.Enabled = chkSua_Tien.Checked;
                            if (chkSua_Tien.Checked)
                            {
                                _tienNt2.Tag = null;
                            }
                            else
                            {
                                _tienNt2.Tag = "disable";
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
                            _tien.V6LostFocus += delegate
                            {
                                TinhGiaVon();
                            };

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
                    case "CK":
                        _ck = (V6NumberTextBox)control;
                        break;
                    //_tien2.V6LostFocus;
                    case "CK_NT":
                        _ckNt = (V6NumberTextBox)control;
                        break;
                    //_tien2.V6LostFocus;
                    case "GIA_NT":
                        _gia_nt = control as V6NumberTextBox;
                        if (_gia_nt != null)
                        {
                            _gia_nt.V6LostFocus += delegate
                            {
                                TinhTienVon();
                                TinhGiaVon();
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
                    case "PN_GIA_TBI":
                        _xuat_dd = control as V6CheckTextBox;
                        if (_xuat_dd != null)
                        {
                            _xuat_dd.TextChanged += delegate
                            {
                                if (_xuat_dd.Text == "")
                                {
                                    _gia_nt.Enabled = true;
                                    if (chkSua_Tien.Checked)
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
                        _maLo.V6LostFocus += _maLo_V6LostFocus;

                        _maLo.V6LostFocusNoChange += delegate
                        {
                            XuLyLayThongTinKhiChonMaLo();
                       
                        };
                        _maLo.GotFocus += (s, e) =>
                        {
                            _maLo.CheckNotEmpty = _maVt.LO_YN && _maKhoI.LO_YN;
                            _maLo.SetInitFilter("ma_vt='" + _maVt.Text.Trim() + "'");
                        };
                        break;
                    case "HSD":
                        _hanSd = (V6DateTimeColor)control;
                        _hanSd.Enabled = false;
                        _hanSd.Tag = "disable";
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
            //GetLoDate13();
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
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SetTang: " + ex.Message, ex.Source);
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
                        else if(fcolumn == "PN_GIA_TBI")
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
                return base.DoHotKey0(keyData);

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
            //TinhGiaNt2();
            TinhGiaNt2_TienNt2();
        }

        void SoLuong1_V6LostFocus(object sender)
        {
           TinhTienNt2();
        }

        void GiaNt21_V6LostFocus(object sender)
        {
            TinhTienNt2();
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
                    txtMaGia.Text = "";
                    return;
                }
                var mst = (data["ma_so_thue"] ?? "").ToString().Trim();
                txtMaSoThue.Text = mst;
                txtTenKh.Text = (data["ten_kh"] ?? "").ToString().Trim();
                txtDiaChi.Text = (data["dia_chi"] ?? "").ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonMaKhachHang: " + ex.Message, "HangTraLaiControl");
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
                        txtTenKh.Enabled = true;
                        txtDiaChi.Enabled = true;
                        txtMaSoThue.Enabled = true;
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
                this.ShowErrorMessage(GetType() + ".XuLyKhoaThongTinKhachHang: " + ex.Message, "HangTraLaiControl");
            }
        }

        private void XuLyChonMaVt(string mavt)
        {
            XuLyLayThongTinKhiChonMaVt();
            XuLyDonViTinhKhiChonMaVt(mavt);
            GetGia();
            GetTon13();
            //GetLoDate();
        }
        private void XuLyChonMaKhoI()
        {
            XuLyLayThongTinKhiChonMaKhoI();
            GetTon13();
            //GetLoDate();
        }

        private void XuLyChonPhieuXuat()
        {
            try
            {
                var ma_kh = txtMaKh.Text.Trim();
                var ma_dvcs = txtMadvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    CPXHangTraLaiForm chonpx = new CPXHangTraLaiForm(this, txtMadvcs.Text, txtMaKh.Text);
                    chonpx.AcceptSelectEvent += chonpx_AcceptSelectEvent;
                    chonpx.ShowDialog();
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
                this.ShowErrorMessage(GetType() + ".XuLyChonPhieuXuat: " + ex.Message, "HangTraLaiControl");
            }
        }

        private bool co_chon_phieu_xuat { get; set; }
        void chonpx_AcceptSelectEvent(List<SortedDictionary<string, object>> selectedDataList)
        {
            try
            {
                detail1.MODE = V6Mode.View;
                AD.Rows.Clear();
                int addCount = 0, failCount = 0;
                foreach (SortedDictionary<string, object> data in selectedDataList)
                {
                    data["PN_GIA_TBI"] = "a";
                    if (XuLyThemDetail(data)) addCount++;
                    else failCount ++;
                }
                V6ControlFormHelper.ShowMainMessage(string.Format("Succeed {0}. Failed {1}.", addCount, failCount));
                if (addCount > 0)
                {
                    co_chon_phieu_xuat = true;
                }
                else
                {
                    co_chon_phieu_xuat = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".chonpx_AcceptSelectEvent: " + ex.Message, "HangTraLaiControl");
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
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaLo" + ex.Message, "HangTraLaiControl");
            }
        }

        private DataTable _dataLoDate;

        private void GetTon13()
        {
            try
            {
                string maVt = _maVt.Text.Trim().ToUpper();
                string maKhoI = _maKhoI.Text.Trim().ToUpper();
                // Get ton kho theo ma_kho,ma_vt 18/01/2016
                //if (V6Options.M_CHK_XUAT == "0")
                {
                    _dataLoDate = Invoice.GetStock(maVt, maKhoI, _sttRec, dateNgayCT.Value);
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
        private void XuLyLayThongTinKhiChonMaVt()
        {
            try
            {
                _maVt.RefreshLoDateYnValue();
                var data = _maVt.Data;
                if (data == null)
                {
                    _tkTl.Text = "";
                    _tkGv.Text = "";
                    _tkCkI.Text = "";
                    _tkVt.Text = "";
                    _hs_qd1.Value = 0;
                    _hs_qd2.Value = 0;
                }
                else
                {
                    _tkTl.Text = (data["tk_tl"] ?? "").ToString().Trim();
                    _tkGv.Text = (data["tk_gv"] ?? "").ToString().Trim();
                    _tkCkI.Text = (data["tk_ck"] ?? "").ToString().Trim();
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
                this.ShowErrorMessage(GetType() + ".XuLyLayThongTinKhiChonMaVt" + ex.Message, "HangTraLaiControl");
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

        private void GetGia()
        {
            try
            {
                var dataGia = Invoice.GetGiaBan("MA_VT", Invoice.Mact, dateNgayCT.Value,
                        cboMaNt.SelectedValue.ToString().Trim(), _maVt.Text, _dvt1.Text, "", txtMaGia.Text);
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
            if (_dvt1.Data == null) return;
            var he_so = ObjectAndString.ObjectToDecimal(_dvt1.Data["he_so"]);
            if (he_so == 0) he_so = 1;
            _heSo1.Value = he_so;
            GetGia();
            TinhTienNt2();
        }

        private void TinhTienNt2()
        {
            try
            {
                _soLuong.Value = _soLuong1.Value * _heSo1.Value;
                _tienNt2.Value = V6BusinessHelper.Vround(_soLuong1.Value * _giaNt21.Value, M_ROUND_NT);
                _tien2.Value = V6BusinessHelper.Vround(_tienNt2.Value * txtTyGia.Value, M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien2.Value = _tienNt2.Value;
                }

                TinhGiaNt2();
                TinhSoluongQuyDoi(_soLuong1, _sl_qd, _sl_qd2, _hs_qd1, _hs_qd2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienNt2", ex);
            }
        }
        private void TinhTienVon()
        {
            _tienNt.Value = V6BusinessHelper.Vround(_soLuong.Value * _gia_nt.Value, M_ROUND_NT);
            _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;
            }
        }
        private void TinhTien()
        {
            _tien.Value = V6BusinessHelper.Vround(_tienNt.Value * txtTyGia.Value, M_ROUND);
            if (_maNt == _mMaNt0)
            {
                _tien.Value = _tienNt.Value;

            }
        }
        private void TinhGiaVon()
        {
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
        private void TinhGiaVon_TienVon()
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

                _gia21.Value = V6BusinessHelper.Vround((_giaNt21.Value * txtTyGia.Value), M_ROUND_GIA_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia21.Value = _giaNt21.Value;
                }


                if (_soLuong.Value != 0)
                {
                    _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong.Value), M_ROUND_GIA_NT);

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
                this.ShowErrorMessage(GetType() + ".TinhGiaNt2: " + ex.Message);
            }
        }

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
                    _giaNt2.Value = V6BusinessHelper.Vround((_tienNt2.Value / _soLuong.Value), M_ROUND_GIA_NT);

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
                }
                else
                {
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    _tienNt2.Enabled = chkSua_Tien.Checked;
                    _dvt1.Enabled = true;

                    _tienNt.Enabled = chkSua_Tien.Checked && _xuat_dd.Text=="";

                    //{Tuanmh 02/12/2016
                    _ckNt.Enabled = !chkLoaiChietKhau.Checked;
                    _ck.Enabled = !chkLoaiChietKhau.Checked && _maNt != _mMaNt0;
                    _gia21.Enabled = chkSua_Tien.Checked && _giaNt21.Value==0 && _maNt != _mMaNt0;
                    _gia_nt.Enabled =  _xuat_dd.Text == "";
                    _gia.Enabled = _xuat_dd.Text == "" && _gia_nt.Value == 0 && _maNt != _mMaNt0;
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

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".EnableFormControls: " + ex.Message, "HangTraLaiControl");
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
        
        private void TinhTongValues()
        {
            var tSoLuong = TinhTong("SO_LUONG1");
            txtTongSoLuong.Value = V6BusinessHelper.Vround(tSoLuong, M_ROUND_NUM);

            var tTienNt2 = TinhTong("TIEN_NT2");
            txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);

            var tTien2 = TinhTong("TIEN2");
            txtTongTien2.Value = V6BusinessHelper.Vround(tTien2, M_ROUND);
        }
        private void TinhChietKhau()
        {
            try
            {
                var tTienNt2 = TinhTong("TIEN_NT2");
                var tyGia = txtTyGia.Value;
                var t_tien_nt2 = txtTongTienNt2.Value;
                txtTongTienNt2.Value = V6BusinessHelper.Vround(tTienNt2, M_ROUND_NT);
                decimal t_ck_nt = 0,t_ck;



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
                        t_ck_nt = V6BusinessHelper.Vround(ptck*tTienNt2/100, M_ROUND_NT);
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
                        if (t_tien_nt2 != 0)
                        {
                            var tien_nt2 = ObjectAndString.ObjectToDecimal(AD.Rows[i]["Tien_nt2"]);
                            var ck_nt = V6BusinessHelper.Vround( tien_nt2/t_tien_nt2*t_ck_nt, M_ROUND_NT);
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
                    t_ck_nt = TinhTong("CK_NT");
                    t_ck = TinhTong("CK");

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
                this.ShowErrorMessage(GetType() + ".TinhChietKhau: " + ex.Message, "HangTraLaiControl");
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
            var t_ck_nt = txtTongCkNt.Value;

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
                t_thue_nt = (t_tien_nt2 - t_gg_nt - t_ck_nt)*thue_suat/100;
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
                    var thue_nt = V6BusinessHelper.Vround(tien_nt2 / t_tien_nt2* t_thue_nt, M_ROUND);
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

        private void TinhTongThanhToan(string debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
                //Tính tổng thanh toán.//con phan nt va tien viet chua ro rang.
            
                HienThiTongSoDong(lblTongSoDong);
                TinhTongValues();
                TinhChietKhau(); //Đã tính //t_tien_nt2, T_CK_NT, PT_CK
                TinhThue();
                if (string.IsNullOrEmpty(_mMaNt0)) return;
                
                var t_tien_nt2 = txtTongTienNt2.Value;
                var t_gg_nt = txtTongGiamNt.Value;
                var t_ck_nt = txtTongCkNt.Value;
                var t_thue_nt = txtTongThueNt.Value;

                var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt + t_thue_nt;
                txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tt_nt, M_ROUND_NT);

                var t_tt = txtTongTien2.Value - txtTongGiam.Value - txtTongCk.Value + txtTongThue.Value;
                txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tt, M_ROUND);

                //var tygia = txtTyGia.Value;
                //txtTongTien2.Value = V6BusinessHelper.Vround(t_tien_nt2*tygia, M_ROUND);
                //txtTongGiam.Value = V6BusinessHelper.Vround(t_gg_nt*tygia, M_ROUND);
                //txtTongCk.Value = V6BusinessHelper.Vround(t_ck_nt*tygia, M_ROUND);
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
            TxtMa_gd.Text = "3";
            //
            txtManx.Text = Invoice.Alct.Rows[0]["TK_NO"].ToString().Trim();
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
                if (TxtSo_ct.Text.Trim()=="" && txtMa_sonb.Text.Trim() != "")
                    TxtSo_ct.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);

            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định khi bấm mới chi tiết.
        /// </summary>
        protected override void SetDefaultDetail()
        {
            try
            {
                _xuat_dd.TextValue = "a";
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// _maKhoI.SetInitFilter
        /// </summary>
        private void XuLyThayDoiMaDVCS()
        {
            try
            {
                if (txtMadvcs.Text.Trim() != "")
                {
                    _maKhoI.SetInitFilter(string.Format("MA_DVCS='{0}'", txtMadvcs.Text));
                }
                else
                {
                    _maKhoI.SetInitFilter("");
                }
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
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["GIA21"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;

                    dataGridViewColumn = dataGridView1.Columns["TIEN2"];
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
                    _gia2.Visible = true;
                    _gia21.Visible = true;
                    _tien2.Visible = true;
                    _gia21.Visible = true;
                    
                    _ck.Visible = true;
                    if (_gia != null) _gia.Visible = true;

                    _gia2.Tag = null;
                    _gia21.Tag = null;
                    _tien2.Tag = null;
                    _gia21.Tag = null;
                    _ck.Tag = null;

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

                    if (chkLoaiChietKhau.Checked)
                    {
                        dataGridViewColumn = dataGridView1.Columns["CK"];
                        if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    }

                    ////Hide Dynamic control
                    _gia2.Visible = false;
                    _gia21.Visible = false;
                    _tien2.Visible = false;
                    _gia21.Visible = false;
                    _ck.Visible = false;

                    if(_gia != null)
                    _gia.Visible = false;

                    _gia2.Tag = "hide";
                    _gia21.Tag = "hide";
                    _tien2.Tag = "hide";
                    _gia21.Tag = "hide";
                    _ck.Tag = "hide";

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
                this.ShowErrorMessage(GetType() + ".XuLyThayDoiMaNt: " + ex.Message, "HangTraLaiControl");
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
                this.ShowErrorMessage(GetType() + ".FormatNumberControl: " + ex.Message, "HangTraLaiControl");
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
                this.ShowErrorMessage(GetType() + ".FormatNumberGridView: " + ex.Message, "HangTraLaiControl");
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
                    txtTkThueNo.Text = alThue.Data.Rows[0]["TK_THUE_CO"].ToString().Trim();
                    txtThueSuat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                    if (txtTkThueCo.Text.Trim() == "") txtTkThueCo.Text = txtManx.Text;
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
                ADTables.Add(sttRec, Invoice.LoadAD(sttRec));
                AD = ADTables[sttRec].Copy();
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
                    if (parent is HangTraLaiContainer)
                    {
                        ((HangTraLaiContainer)parent)
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
                this.ShowErrorException(GetType() + ".ViewInvoice", ex);
            }
        }
        #endregion view invoice

        #region ==== Add Thread ====
        private bool flagAddFinish, flagAddSuccess;
        private SortedDictionary<string, object> amDataAdd;
        private List<SortedDictionary<string, object>> adDataAdd;
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
                amDataAdd = GetData();
                //Tạo rec truoc trong Moi()
                amDataAdd["STT_REC"] = _sttRec;
                amDataAdd["MA_CT"] = Invoice.Mact;

                adDataAdd = dataGridView1.GetData(_sttRec);
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
                
                if (Invoice.InsertInvoice(amDataAdd, adDataAdd))
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
        private SortedDictionary<string, object> editDataAM;
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
                editDataAM = GetData();

                editDataAM["STT_REC"] = _sttRec;
                editDataAM["MA_CT"] = Invoice.Mact;

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
                if (Invoice.UpdateInvoice(editDataAM, editDataAD, keys))
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

                    var dataAM = GetData();
                    var fields = new string[]{"SO_CT","NGAY_CT","MA_CT"};
                    V6ControlFormHelper.UpdateDKlistAll(dataAM, fields, AD);
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
                            if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"])))
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
                        if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"])))
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
                        c.TTT = txtTongThanhToan.Value;
                        c.TTT_NT = txtTongThanhToanNt.Value;
                        c.MA_NT = _maNt;
                        c.Dock = DockStyle.Fill;
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

        private TimHangTraLaiForm _timForm;
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
                    _timForm.ShowDialog();
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
                        _timForm = new TimHangTraLaiForm(this);
                    _timForm.ViewMode = false;
                    _timForm.Visible = false;
                    _timForm.ShowDialog();
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
                data["STT_REC0"]= _sttRec0;

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
                return false;
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
        private void HangTraLaiControl_Load(object sender, EventArgs e)
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

        #region ==== HoaDonDetail Event ====
        private void HoaDonDetail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd();
        }
        private void HoaDonDetail1_AddHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLyThemDetail(data))
            {
                return;
            }
            throw new Exception("Add failed.");
        }
        private void HoaDonDetail1_EditHandle(SortedDictionary<string,object> data)
        {
            if (ValidateData_Detail(data) && XuLySuaDetail(data))
            {
                return;
            }
            throw new Exception("Edit failed.");
        }
        private void HoaDonDetail1_ClickDelete(object sender)
        {
            XuLyXoaDetail();
        }
        private void HoaDonDetail1_ClickCancelEdit(object sender)
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
            if(detail1.IsViewOrLock)
                detail1.SetData(dataGridView1.GetCurrentRowData());
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
            else if(!new List<string> {"TEN_VT","MA_VT"}.Contains(fieldName))
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

        private void txtMaKh_Leave(object sender, EventArgs e)
        {
            
            

        }

        private void HangTraLaiControl_VisibleChanged(object sender, EventArgs e)
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

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void txtPtCk_V6LostFocus(object sender)
        {
            TinhTongThanhToan("V6LostFocus txtPtCk_V6LostFocus ");

        }

        private void chkSua_Tien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                _tienNt2.Enabled = chkSua_Tien.Checked;
                
                _tienNt.Enabled = chkSua_Tien.Checked && _xuat_dd.Text != "";
            }
            if (chkSua_Tien.Checked)
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

        private void HoaDonDetail1_Load(object sender, EventArgs e)
        {

        }

        private void HoaDonDetail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                    //Bật tắt Xuất ĐD
                    if (_xuat_dd.Text == "")
                    {
                        _gia_nt.Enabled = true;
                        if (chkSua_Tien.Checked)
                            _tienNt.Enabled = true;
                        else _tienNt.Enabled = false;
                    }
                    else
                    {
                        _gia_nt.Enabled = false;
                        _tienNt.Enabled = false;
                    }

                    _maVt.RefreshLoDateYnValue();
                    _maKhoI.RefreshLoDateYnValue();
                    XuLyDonViTinhKhiChonMaVt(_maVt.Text, false);
                    _maVt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".HoaDonDetail1_ClickEdit: " + ex.Message);
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
                if (_tkTl.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage("Tài khoản không phải chi tiết !");
                    _tkTl.Focus();
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
        }

        private void btnChonPX_Click(object sender, EventArgs e)
        {
            XuLyChonPhieuXuat();
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

        private void txtManx_V6LostFocusNoChange(object sender)
        {
            if (txtTkThueCo.Text.Trim() == "") txtTkThueCo.Text = txtManx.Text;
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1);
        }


        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSua_Tien);
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
            XemPhieuNhapView(dateNgayCT.Value, Invoice.Mact, _maKhoI.Text, _maVt.Text);
        }


    }
}
