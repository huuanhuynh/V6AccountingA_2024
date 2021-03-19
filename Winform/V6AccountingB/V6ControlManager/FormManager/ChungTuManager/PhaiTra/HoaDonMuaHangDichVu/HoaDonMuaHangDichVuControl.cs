using System;
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
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.HoaDonMuaHangDichVu.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.HoaDonMuaHangDichVu
{
    public partial class HoaDonMuaHangDichVuControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        // ReSharper disable once InconsistentNaming
        public V6Invoice31 Invoice = new V6Invoice31();
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public HoaDonMuaHangDichVuControl()
        {
            InitializeComponent();
            MyInit();
        }
        public HoaDonMuaHangDichVuControl(string itemId)
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
        public HoaDonMuaHangDichVuControl(string maCt, string itemId, string sttRec)
            : base(new V6Invoice31(), itemId)
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
            txtTkThueNo.FilterStart = true;
            txtTkThueNo.SetInitFilter("Loai_tk=1");
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
            dataGridViewColumn = dataGridView1.Columns["TK_VT"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["TEN_TK"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["STT_REC"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["STT_REC0"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);


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

            All_Objects["thisForm"] = this;
            CreateFormProgram(Invoice);
            
            LoadDetailControls();
            LoadDetail2Controls();
            LoadDetail3Controls();
            LoadAdvanceControls(Invoice.Mact);
            CreateCustomInfoTextBox(group4, txtTkThueNo, cboChuyenData);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            LoadTagAndText(Invoice, detail1.Controls);
            HideControlByGRD_HIDE();
            ResetForm();

            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        private V6ColorTextBox _dvt;
        private V6VvarTextBox _tk_vt;
        private V6NumberTextBox  _soLuong, _giaNt, _gia;
        private V6NumberTextBox _tienNt, _tien, _mau_bc22;
        //private V6DateTimeColor _hanSd;
        private V6ColorTextBox _so_ct022,_so_seri022,_ten_kh22,
            _dia_chi22, _ma_so_thue22, _ten_vt22, _dien_giaii;
        private V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22, _ma_thue22;
        private V6DateTimeColor _ngay_ct022;
        private V6NumberTextBox _t_tien22, _t_tien_nt22, _thue_suat22, _t_thue22, _t_thue_nt22, _gia_Nt022, _han_tt22;

        private void LoadDetailControls()
        {
            detail1.lblName.AccessibleName = "TEN_TK";
            //Lấy các control động
            detailControlList1 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Alct1, out _orderList, out _alct1Dic);

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
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects);

                switch (NAME)
                {
                    case "TK_VT":
                        _tk_vt = (V6VvarTextBox) control;
                        _tk_vt.Upper();
                        _tk_vt.SetInitFilter("Loai_tk = 1");
                        _tk_vt.BrotherFields = "ten_tk,ten_tk2";
                    
                   
                        _tk_vt.V6LostFocus += TkVatTuV6LostFocus;
                        break;
                    case "DVT":
                        _dvt= control as V6ColorTextBox;
                        if (_dvt != null)
                        {
                            _dvt.Leave += delegate
                            {
                                if (!detail1.IsAddOrEdit) return;
                                if (_dvt.Text.Trim() == "")
                                {
                                    _soLuong.Enabled = false;
                                    _soLuong.Value = 0;
                                    _giaNt.Enabled = false;
                                    _giaNt.Value = 0;
                                    _tienNt.Focus();
                                }
                                else
                                {
                                    _soLuong.Enabled = true;
                                    _giaNt.Enabled = true;
                                    _soLuong.Focus();
                                }
                            };
                        }
                        break;
                    case "SO_LUONG":
                        _soLuong = (V6NumberTextBox) control;
                        _soLuong.V6LostFocus += delegate
                        {
                            TinhTienNt();
                        };
                        break;
                    case "GIA":

                        _gia = (V6NumberTextBox)control;
                        _gia.V6LostFocus += delegate
                        {
                            //TinhTienNt();
                        };
                        break;
                    case "GIA_NT":

                        _giaNt = (V6NumberTextBox)control;
                        _giaNt.V6LostFocus += delegate
                        {
                            TinhTienNt();
                        };
                        break;
                    case "TIEN":
                        _tien = (V6NumberTextBox) control;
                        break;
                    case "TIEN_NT":
                        _tienNt = control as V6NumberTextBox;
                        if (_tienNt != null)
                        {
                      
                            _tienNt.V6LostFocus += delegate
                            {
                                _tien.Value = V6BusinessHelper.Vround((_tienNt.Value * txtTyGia.Value), M_ROUND);

                            };
                        }
                        break;
                    case "DIEN_GIAII":
                        _dien_giaii = (V6ColorTextBox) control;
                        _dien_giaii.GotFocus += _dien_giaii_GotFocus;
                        break;
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
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
            detailControlList2 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Alct2, out _orderList2, out _alct2Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<string, AlctControls> item in detailControlList2)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                // Gán tag hide và readonly theo GRD_xxxx
                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":"))
                {
                    control.InvisibleTag();
                }
                if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                {
                    control.ReadOnlyTag();
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "_DETAIL2");
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
                            _mau_bc22.GotFocus += MauBc22GotFocus;
                        }
                        break;
                    case "TEN_KH":
                        _ten_kh22 = control as V6ColorTextBox;
                        if (_ten_kh22 != null)
                        {

                        }
                        break;
                    case "TEN_VT":
                        _ten_vt22 = control as V6ColorTextBox;
                        if (_ten_vt22 != null)
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
                    case "GIA_NT0":
                        _gia_Nt022 = control as V6NumberTextBox;
                        if (_gia_Nt022 != null)
                        {

                        }
                        break;
                    case "HAN_TT":
                        _han_tt22 = control as V6NumberTextBox;
                        break;
                    case "T_TIEN":
                        _t_tien22 = control as V6NumberTextBox;
                        if (_ngay_ct022 != null)
                        {

                        }
                        break;
                    case "T_TIEN_NT":
                        _t_tien_nt22 = control as V6NumberTextBox;
                        if (_t_tien_nt22 != null)
                        {
                            _t_tien_nt22.V6LostFocus += delegate
                            {
                                //TinhTien22TienThue22();
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
                    case "MA_THUE":
                        _ma_thue22 = control as V6VvarTextBox;
                        _ma_thue22.CheckNotEmpty = true;
                        _ma_thue22.CheckOnLeave = true;
                        if (_ma_thue22 != null)
                        {
                            _ma_thue22.V6LostFocus += delegate
                            {
                                _thue_suat22.Enabled = false;
                                _thue_suat22.Tag = "disable";
                                _thue_suat22.Value = ObjectAndString.ObjectToDecimal(_ma_thue22.Data["THUE_SUAT"]);
                                _tk_thue_no22.Text = _ma_thue22.Data["TK_THUE_NO"].ToString().Trim();
                                _tk_du22.Text = txtManx.Text;
                                //TinhTien22TienThue22();
                                Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat22.Value, _t_tien_nt22.Value, _t_tien22.Value, _t_thue_nt22, _t_thue22);
                            };
                        }
                        break;
                    case "THUE_SUAT":
                        _thue_suat22 = control as V6NumberTextBox;
                        if (_thue_suat22 != null)
                        {
                            _thue_suat22.Enabled = false;
                            _thue_suat22.Tag = "disable";

                            _thue_suat22.V6LostFocus += delegate
                            {
                                //TinhTien22TienThue22();
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
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2_DETAIL2");
            }

            foreach (AlctControls control in detailControlList2.Values)
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
            detailControlList3 = V6ControlFormHelper.GetDynamicControlStructsAlct(Invoice.Alct3, out _orderList3, out _alct3Dic);
            //Thêm các control động vào danh sách
            foreach (KeyValuePair<string, AlctControls> item in detailControlList3)
            {
                var control = item.Value.DetailControl;
                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                // Gán tag hide và readonly theo GRD_xxxx
                if (!V6Login.IsAdmin && Invoice.GRD_HIDE.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":"))
                {
                    control.InvisibleTag();
                }
                if (!V6Login.IsAdmin && (Invoice.GRD_READONLY.Contains(NAME) || Invoice.GRD_READONLY.ContainsStartsWith(NAME + ":")))
                {
                    control.ReadOnlyTag();
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "_DETAIL3");
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
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2_DETAIL3");
            }

            foreach (AlctControls control in detailControlList3.Values)
            {
                detail3.AddControl(control);
            }

            detail3.SetStruct(Invoice.AD3Struct);
            detail3.MODE = detail3.MODE;
            V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView3, _alct3Dic, _maNt, _mMaNt0);
        }
        
        private void Detail3_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetail3ClickAdd(sender);
            }
            else
            {
                dataGridView3.UnLock();
            }
        }
        private void XuLyDetail3ClickAdd(object sender)
        {
            try
            {
                var readonly_list = SetControlReadOnlyHide(detail3, Invoice, Mode, V6Mode.Add);
                if (readonly_list.Contains(detail3.btnMoi.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    detail3.ChangeToViewMode();
                    dataGridView3.UnLock();
                }
                else
                {
                    TruDanTheoNhomDk();
                    _tk_i_33.Focus();
                }
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
                if (XuLyThemDetail3(data))
                {
                    dataGridView3.UnLock();
                    return;
                }
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }

        private void Detail3_EditHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data))
            {
                if (XuLySuaDetail3(data))
                {
                    dataGridView3.UnLock();
                    return;
                }
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
        
        private void Detail3_ClickEdit(object sender, HD_Detail_Eventargs e)
        {
            try
            {
                if (AD3 != null && AD3.Rows.Count > 0 && dataGridView3.DataSource != null)
                {
                    detail3.ChangeToEditMode();

                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView3, detail3, out _gv3EditingRow, out _sttRec03);
                    if (!string.IsNullOrEmpty(_sttRec03))
                    {
                        var readonly_list = SetControlReadOnlyHide(detail3, Invoice, Mode, V6Mode.Edit);
                        if (readonly_list.Contains(detail3.btnSua.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            detail3.ChangeToViewMode();
                            dataGridView3.UnLock();
                        }
                        else
                        {
                            _tk_i_33.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void Detail3_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail3();
        }
        private void XuLyXoaDetail3()
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                var readonly_list = SetControlReadOnlyHide(new HD_Detail() { Name = "detail3" }, Invoice, Mode, V6Mode.Delete);
                if (readonly_list.Contains(detail3.btnXoa.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                    return;
                }

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
                            detail3.SetData(dataGridView3.CurrentRow.ToDataDictionary());
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

        private void Detail3_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            dataGridView3.UnLock();
            detail3.SetData(_gv3EditingRow.ToDataDictionary());
        }

        private void detail3_LabelNameTextChanged(object sender, EventArgs e)
        {
            lblNameT.Text = ((Label)sender).Text;
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        void MauBc22GotFocus(object sender, EventArgs e)
        {
            if (_mau_bc22.Value == 0)
            {
                _mau_bc22.Value = 1;
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

                if (tabControl1.SelectedTab == tabChiTietBoSung)
                {
                    detail3.btnNhan.Focus();
                    if (detail3.MODE == V6Mode.Add)
                    {
                        var detail3Data = detail3.GetData();
                        if (ValidateData_Detail3(detail3Data))
                        {
                            if (XuLyThemDetail3(detail3Data))
                            {
                                ShowParentMessage(V6Text.InvoiceF3AddDetailSuccess);
                                All_Objects["data"] = detail3Data;
                                InvokeFormEvent(FormDynamicEvent.AFTERADDDETAIL3SUCCESS);
                            }
                        }
                    }
                    else if (detail3.MODE == V6Mode.Edit)
                    {
                        var detail3Data = detail3.GetData();
                        if (ValidateData_Detail3(detail3Data))
                        {
                            if (XuLySuaDetail3(detail3Data))
                            {
                                detail3.ChangeToAddMode_KeepData();
                                dataGridView3.Lock();
                                ShowParentMessage(V6Text.InvoiceF3EditDetailSuccess);
                                All_Objects["data"] = detail3Data;
                                InvokeFormEvent(FormDynamicEvent.AFTEREDITDETAIL3SUCCESS);
                            }
                        }
                    }
                    else
                    {
                        detail3.ChangeToAddMode_KeepData();
                        dataGridView3.Lock();
                    }
                    goto End_F10;
                }

                if (tabControl1.SelectedTab != tabChiTiet)
                {
                    tabControl1.SelectedTab = tabChiTiet;
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
            End_F10:
                DoNothing();
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
      
     
        private void TkVatTuV6LostFocus(object sender)
        {
            //  XuLyChonTkVt(_tk_vt.Text);
            SetDefaultDataDetail(Invoice, detail1.panelControls);
        }


        public void TinhTienNt()
        {
            try
            {
                var tyGia = txtTyGia.Value;
                _gia.Value = V6BusinessHelper.Vround(_giaNt.Value * tyGia, M_ROUND_NT);
                if (_maNt == _mMaNt0)
                {
                    _gia.Value = _giaNt.Value;
                }

                _tienNt.Value = V6BusinessHelper.Vround((_soLuong.Value * _giaNt.Value), M_ROUND_NT);
                _tien.Value = V6BusinessHelper.Vround((_tienNt.Value * tyGia), M_ROUND);

                if (_maNt == _mMaNt0)
                {
                    _tien.Value = _tienNt.Value;
                }


            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                    detail3.MODE = V6Mode.Lock;
                }
                else
                {
                    detail2.MODE = V6Mode.View;
                    detail3.MODE = V6Mode.View;
                    XuLyKhoaThongTinKhachHang();

                    txtTyGia.Enabled = _maNt != _mMaNt0;

                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
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
            dataGridView2.DataSource = AD2;
            dataGridView3.DataSource = AD3;
            
            ReorderDataGridViewColumns();
            FormatGridView();
        }
        private void ReorderDataGridViewColumns()
        {
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView2, _orderList2);
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
            f = dataGridView1.Columns["TIEN"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
            }
            f = dataGridView1.Columns["TIEN0"];
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
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, Invoice.Config2.GRDS_V1, Invoice.Config2.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config2.GRDHV_V1 : Invoice.Config2.GRDHE_V1);
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView3, Invoice.Config3.GRDS_V1, Invoice.Config3.GRDF_V1, V6Setting.IsVietnamese ? Invoice.Config3.GRDHV_V1 : Invoice.Config3.GRDHE_V1);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView2, Invoice.Mact);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3, Invoice.Mact);
            //V6ControlFormHelper.FormatGridViewHideColumns(dataGridView3ChiPhi, Invoice.Mact);
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
        
        public void TinhTongValues()
        {
            
            var t_tien_nt = TinhTong(AD, "TIEN_NT");
            txtTongTienNt.Value = V6BusinessHelper.Vround(t_tien_nt, M_ROUND_NT);

            var tong_thue_nt = TinhTong(AD2, "T_THUE_NT");
            txtTongThueNt.Value = V6BusinessHelper.Vround(tong_thue_nt, M_ROUND_NT);

            var t_tien = TinhTong(AD, "TIEN");
            txtTongTien.Value = V6BusinessHelper.Vround(t_tien, M_ROUND);

            var tong_thue = TinhTong(AD2, "T_THUE");
            txtTongThue.Value = V6BusinessHelper.Vround(tong_thue, M_ROUND_NT);
            
            var tPsNoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_NO_NT", "OPER_TT");
            var tPsCoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_CO_NT", "OPER_TT");
            txtTongTangGiamNt.Value = tPsNoNt;
            var tPsNo = V6BusinessHelper.TinhTongOper(AD3, "PS_NO", "OPER_TT");
            var tPsCo = V6BusinessHelper.TinhTongOper(AD3, "PS_CO", "OPER_TT");
            txtTongTangGiam.Value = tPsNo;

            txtTongThanhToanNt.Value = V6BusinessHelper.Vround(t_tien_nt + tong_thue_nt + tPsNoNt, M_ROUND_NT);
            txtTongThanhToan.Value = V6BusinessHelper.Vround(t_tien + tong_thue + tPsNo, M_ROUND);
        }


        public override void TinhTongThanhToan(string debug)
        {
            try
            {
                if (NotAddEdit) return;
                HienThiTongSoDong(lblTongSoDong);

                //XuLyThayDoiTyGia();

                TinhTongValues();

                //TinhThue();// Đã cho vào TinhTongValue
                if (string.IsNullOrEmpty(_mMaNt0)) return;

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
        void _dien_giaii_GotFocus(object sender, EventArgs e)
        {
            if (_dien_giaii.Text == "")
            {
                _dien_giaii.Text =Txtdien_giai.Text;
            }
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void XuLyThayDoiMaNt()
        {
            try
            {
                if (!_ready0) return;

                var viewColumn = dataGridView1.Columns["GIA_NT"];
                var newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _maNt;
                if (viewColumn != null) viewColumn.HeaderText = newText;
                if (_giaNt != null) _giaNt.GrayText = newText;

                var column = dataGridView1.Columns["TIEN_NT"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _maNt;
                if (column != null) column.HeaderText = newText;
                if (_tienNt != null) _tienNt.GrayText = newText;

                viewColumn = dataGridView1.Columns["GIA"];
                newText = (V6Setting.IsVietnamese ? "Đơn giá " : "Price ") + _mMaNt0;
                if (viewColumn != null) viewColumn.HeaderText = newText;
                if (_gia != null) _gia.GrayText = newText;

                column = dataGridView1.Columns["TIEN"];
                newText = (V6Setting.IsVietnamese ? "Thành tiền " : "Amount ") + _mMaNt0;
                if (column != null) column.HeaderText = newText;
                if (_tien != null) _tien.GrayText = newText;

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
                    SetDetailControlVisible(detailControlList1, true, "GIA", "GIA01", "GIA2", "GIA21", "TIEN", "TIEN0", "TIEN2", "THUE", "CK", "GG", "TIEN_VC");
                    panelVND.Visible = true;
                    

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "GIA");
                    if (c != null) c.Visible = true;
                    var dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                  
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                    txtTongTienNt.DecimalPlaces = V6Options.M_IP_TIEN_NT;

                    SetDetailControlVisible(detailControlList2, true, "T_TIEN", "T_THUE");

                    //Detail3
                    SetDetailControlVisible(detailControlList3, true, "PS_NO", "PS_CO");

                    gridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;
                    
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
                    
                    var dataGridViewColumn = dataGridView1.Columns["GIA"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    
                    txtTongThanhToanNt.DecimalPlaces = V6Options.M_IP_TIEN;
                    txtTongTienNt.DecimalPlaces = V6Options.M_IP_TIEN;

                    SetDetailControlVisible(detailControlList2, false, "T_TIEN", "T_THUE");

                    //Detail3
                    SetDetailControlVisible(detailControlList3, false, "PS_NO", "PS_CO");

                    gridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;
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
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatNumberGridView " + _sttRec, ex);
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
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");

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
        public List<IDictionary<string, object>> readyDataAD, readyDataAD2, readyDataAD3;
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
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
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
                readyDataAD3 = dataGridView3.GetData(_sttRec);
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
                
                if (Invoice.InsertInvoice(readyDataAM, readyDataAD, readyDataAD2, readyDataAD3))
                {
                    _AED_Success = true;
                }
                else
                {
                    _AED_Success = false;
                    addErrorMessage = V6Text.Text("ADD0");
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

            if (_print_flag)
                Thread.Sleep(2000);
            _AED_Running = false;
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
                detail3.MODE = V6Mode.View;
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
                readyDataAD3 = dataGridView3.GetData(_sttRec);
                foreach (IDictionary<string, object> adRow in readyDataAD3)
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
                    BasePrint(Invoice, _sttRec_In, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
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
                if (Invoice.UpdateInvoice(readyDataAM, readyDataAD, readyDataAD2, readyDataAD3, keys))
                {
                    _AED_Success = true;
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                    // WriteDBlog.
                    SaveEditLog(AM_current.ToDataDictionary(), readyDataAM);
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
                    _AED_Success = true;
                    AM.Rows.Remove(row);
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
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
        #endregion delete

        private void Luu()
        {
            try
            {
                InvokeFormEvent(FormDynamicEvent.BEFORELUU);

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    tabControl1.SelectedTab = tabChiTiet;
                    EnableFunctionButtons();
                }
                else if (detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    tabControl1.SelectedTab = tabThue;
                    EnableFunctionButtons();
                }
                else if (detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    tabControl1.SelectedTab = tabChiTietBoSung;
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

                    GetSttRec(Invoice.Mact);
                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                    //GetSoPhieu();
                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    GET_AM_OLD_EXTRA();
                    detail1.DoAddButtonClick( );
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
                    detail3.MODE = V6Mode.Init;
                    GoToFirstFocus(txtMa_sonb);
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

                            if (check_edit)
                            {
                                Mode = V6Mode.Edit;
                                detail1.MODE = V6Mode.View;
                                detail2.MODE = V6Mode.View;
                                detail3.MODE = V6Mode.View;
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
                            AM_old = IsViewingAnInvoice ? AM.Rows[CurrentIndex] : null;
                            GetSttRec(Invoice.Mact);
                            SetNewValues();
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                            Mode = V6Mode.Add;
                            detail1.MODE = V6Mode.View;
                            detail2.MODE = V6Mode.View;
                            detail3.MODE = V6Mode.View;
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


        private TimHoaDonMuaHangDichVuForm SearchForm
        {
            get
            {
                if (_timForm == null || _timForm.IsDisposed)
                    _timForm = new TimHoaDonMuaHangDichVuForm(Invoice, V6Mode.View);
                return _timForm;
            }
        }
        private TimHoaDonMuaHangDichVuForm _timForm;
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
                BasePrint(Invoice, _sttRec, V6PrintMode.None, TongThanhToan, TongThanhToanNT, true);
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
        }

        private void SetFormDefaultValues()
        {
            
            cboKieuPost.SelectedIndex = 1;
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
                dataGridView2.UnLock();
                dataGridView3.UnLock();
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
                    _tk_vt.Focus();
                }
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
                    _ten_vt22.Text = Txtdien_giai.Text;

                    _t_tien22.Value = txtTongTien.Value;
                    _t_tien_nt22.Value = txtTongTienNt.Value;
                    //_thue_suat22.Value = txtThueSuat.Value;
                    //_tk_thue_no22.Text = txtTkThueNo.Text.Trim();
                    _tk_du22.Text = txtManx.Text.Trim();
                    _han_tt22.Value = txtHanTT.Value;
                    _mau_bc22.Value = 1;

                    _thue_suat22.Enabled = false;

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
                    //TinhTien22TienThue22();
                    Tinh_TienThueNtVaTienThue_TheoThueSuat(_thue_suat22.Value, _t_tien_nt22.Value, _t_tien22.Value, _t_thue_nt22, _t_thue22);
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
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = _sttRec;
                //Thêm thông tin...
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("TK_VT") || data["TK_VT"].ToString().Trim() == "")
                {
                    var label = "TK_VT";
                    var lbl = detail1.GetControlByName("lbl" + label);
                    if (lbl != null) label = lbl.Text;
                    error += V6Text.NoInput + " [" + label + "]\n";
                }
                //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
                if (error == "")
                {
                    UpdateDetailChangeLog(_sttRec0, detailControlList1, null, data);
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
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "TK_VT");
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
                        if (!data.ContainsKey("TK_VT") || data["TK_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("FILTERL00165") + " " + V6Text.Empty;
                        //if (!data.ContainsKey("MA_KHO_I") || data["MA_KHO_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00166") + " " + V6Text.Empty;
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
                        var details = V6Text.FieldCaption("TK_VT") + ": " + currentRow["TK_VT"];
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
            LoadTag(1, Invoice.Mact, Invoice.Mact, m_itemId, "");
            SetStatus2Text();
            btnMoi.Focus();
            if (ClickSuaOnLoad)
            {
                ClickSuaOnLoad = false;
                btnSua.PerformClick();
            }
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
                            _tk_vt.Focus();
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
        
        public void TinhTongThanhToan_V6LostFocus(object sender)
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

        private void chkSuaTkThue_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSuaTkThue.Checked)
            {
                
                txtTkThueNo.ReadOnly = false;
                txtTkThueNo.Tag = null;
            }
            else
            {
                
                txtTkThueNo.ReadOnly = true;
                txtTkThueNo.Tag = "readonly";
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
                if(fieldName == "GIA_NT") fcaption += " "+ cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "GIA") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if(!(new List<string> {"TEN_TK","TK_VT"}).Contains(fieldName))
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
            else if (!(new List<string> { "TEN_TK", "TK_VT" }).Contains(fieldName))
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
            BasePrint(Invoice, _sttRec, V6PrintMode.None, TongThanhToan, TongThanhToanNT, false);
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }
        
        private void chkSuaTien_CheckedChanged(object sender, EventArgs e)
        {
            //!!!
        }
        
        private void hoaDonDetail2_Load(object sender, EventArgs e)
        {

        }
        
        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
            LoadCustomInfo(dateNgayCT.Value, txtMaKh.Text);
        }

        private void txtManx_V6LostFocus(object sender)
        {
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

                if (!ValidateMasterData(Invoice)) return false;


                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoInputDetail);
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
                        checkChiTietError += string.Format(V6Text.Text("KTNDKPSNKPSC") + " {0}\n", item.Key);
                    }
                }
                if (checkChiTietError.Length > 0)
                {
                    this.ShowWarningMessage(checkChiTietError);
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

        private void detail1_LabelNameTextChanged(object sender, EventArgs e)
        {
            lblNameT.Text = ((Label)sender).Text;
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView2, dataGridView3);
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
            try
            {
                if (NotAddEdit) return;
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                chon_accept_flag_add = shift;
                ReportR45db2SelectorForm r45Selector = new ReportR45db2SelectorForm(Invoice);
                if (r45Selector.ShowDialog(this) == DialogResult.OK)
                {
                    chonExcel_AcceptData(r45Selector.dataGridView1.GetSelectedData());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void thayTheMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(Invoice);
        }

        private void thayTheNhieuMenu_Click(object sender, EventArgs e)
        {
            ChucNang_ThayThe(Invoice, true);
        }

        private void thayThe2toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_SuaNhieuDong(Invoice);
        }

        private void cboKieuPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewLblKieuPost(lblKieuPostColor, cboKieuPost, Invoice.Alct["M_MA_VV"].ToString().Trim() == "1");
        }

        private void inPhieuHachToanMenu_Click(object sender, EventArgs e)
        {
            InPhieuHachToan(Invoice, _sttRec, TongThanhToan, TongThanhToanNT);
        }

        private void exportXmlMenu_Click(object sender, EventArgs e)
        {
            ExportXml();
        }

        private void importXmlMenu_Click(object sender, EventArgs e)
        {
            ImportXml();
        }

        private void menuChucNang_Paint(object sender, PaintEventArgs e)
        {
            FixMenuChucNangItemShiftText(importXmlMenu);
        }

        private void timTopCuoiKyMenu_Click(object sender, EventArgs e)
        {
            Tim("1");
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
                chonExcel.Program = Event_program;
                chonExcel.All_Objects = All_Objects;
                chonExcel.DynamicFixMethodName = "DynamicFixExcel";
                chonExcel.CheckFields = "MA_VT,MA_KHO_I,TIEN_NT0,SO_LUONG1,GIA_NT01";
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

        void chonExcel_AcceptData(DataTable table)
        {
            chonExcel_AcceptData(table.ToListDataDictionary());
        }
        void chonExcel_AcceptData(List<IDictionary<string, object>> table)
        {
            var count = 0;
            _message = "";
            detail1.MODE = V6Mode.View;
            dataGridView1.UnLock();
            if (table == null || table.Count == 0) return;
            var row0 = table[0];
            if (row0.ContainsKey("TK_VT") && row0.ContainsKey("TIEN_NT"))
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

                foreach (IDictionary<string, object> row in table)
                {
                    var data = row;
                    var cTK_VT = data["TK_VT"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALTK", "TK", cTK_VT);
                    
                    var __tien_nt = ObjectAndString.ToObject<decimal>(data["TIEN_NT"]);
                    var __tien = V6BusinessHelper.Vround(__tien_nt * txtTyGia.Value, M_ROUND);
                    if (!data.ContainsKey("TIEN")) data.Add("TIEN", __tien);

                    if (exist)
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
                        if (!exist) _message += string.Format("{0} [{1}]", V6Text.NotExist, cTK_VT);
                    }
                }
                ShowParentMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
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
    }
}
