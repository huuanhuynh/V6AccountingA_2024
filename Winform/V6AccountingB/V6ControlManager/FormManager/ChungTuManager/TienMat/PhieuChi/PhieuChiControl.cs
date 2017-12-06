using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi
{
    public partial class PhieuChiControl : V6InvoiceControl
    {
        #region ==== Properties and Fields
        public V6Invoice51 Invoice;
        private string _MA_GD = "";
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuChiControl()
        {
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// mact = CA1: Phiếu chi, BN1: Báo nợ
        /// </summary>
        /// <param name="mact"></param>
        /// <param name="itemId">trong menu3</param>
        public PhieuChiControl(string mact, string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            Invoice = string.IsNullOrEmpty(mact) ? new V6Invoice51() : new V6Invoice51(mact);

            MyInit();
        }

        /// <summary>
        /// mact = CA1: Phiếu chi, BN1: Báo nợ
        /// Dùng để khởi tạo sửa
        /// </summary>
        public PhieuChiControl(string mact, string itemId, string sttRec)
        {
            m_itemId = itemId;
            InitializeComponent();
            Invoice = string.IsNullOrEmpty(mact) ? new V6Invoice51() : new V6Invoice51(mact);
            MyInit();
            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {
            LoadLanguage();
            LoadTag(Invoice, detail1.Controls);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;

            V6ControlFormHelper.SetFormStruct(this, Invoice.AMStruct);
            txtMaKh.Upper();
            txtTk.Upper();
            txtTk.SetInitFilter("Loai_tk=1");
            txtTk.FilterStart = true;

            txtMa_sonb.Upper();
            if (V6Login.MadvcsCount == 1)
            {
                txtMa_sonb.SetInitFilter("MA_DVCS='" + V6Login.Madvcs + "' AND dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }
            else
            {
                txtMa_sonb.SetInitFilter("dbo.VFV_InList0('" + Invoice.Mact + "',MA_CTNB,'" + ",')=1");
            }


            //V6ControlFormHelper.CreateGridViewStruct(dataGridView1, ad81Struct);
            
            var dataGridViewColumn = dataGridView1.Columns["UID"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (Guid);
            //,,,
            dataGridViewColumn = dataGridView1.Columns["TK_I"];
            if (dataGridViewColumn != null) dataGridViewColumn.ValueType = typeof (string);
            dataGridViewColumn = dataGridView1.Columns["TEN_TK_I"];
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
            
            LoadDetailControls("2");
            LoadDetail2Controls();
            LoadDetail3Controls();
            ResetForm();

            _MA_GD = (Invoice.Alct.Rows[0]["M_MA_GD"] ?? "2").ToString().Trim();
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));
            txtLoaiPhieu.ChangeText(_MA_GD);
            
            LoadAll();
            InvokeFormEvent("INIT");
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====


        //T_TT_NT0,T_TT_QD, PHAI_TT_NT, MA_NT_I
        private V6ColorTextBox _soCt0, _maNtI, _sttRecTt, _ten_kh_t, _ten_vt_t, _dia_chi_t, _mst_t,_dien_giaii;
        private V6VvarTextBox _tkI, _ma_kh_t, _ma_thue_i, _tk_thue_i;
        private V6NumberTextBox _t_tt_nt0, _t_tt_qd, _phaiTtNt, _psno, _psnoNt, _tien, _tienNt, _tientt, _ttqd, _mau_bc,_ty_gia_ht2,
            _thue_suat, _thue_nt, _thue, _tt, _tt_nt;
        private V6DateTimeColor _ngayCt0;

        private V6ColorTextBox _so_ct022, _so_seri022, _ten_kh22,
            _dia_chi22, _ma_so_thue22, _so_seri0;
        private V6VvarTextBox _ma_kh22, _tk_du22, _tk_thue_no22;
        private V6DateTimeColor _ngay_ct022;
        private V6NumberTextBox _t_tien22, _t_tien_nt22, _thue_suat22, _t_thue22, _t_thue_nt22, _gia_Nt022;

        
        DataTable alct1_01, alct1_02, alct1_03;
        private void LoadDetailControls(string maGd)
        {
            try
            {
                _MA_GD = maGd;
                //Thêm các control thiết kế cứng.
                _sttRecTt = V6ControlFormHelper.CreateColorTextBox("STT_REC_TT", "sttRecTt", 10, false, false);
                //_soSeri0 = V6ControlFormHelper.CreateColorTextBox("SO_SERI0", "so seri", 10, false);

                var _check_f_ps_no_nt = false;
                var _check_f_ps_no = false;
                var _check_f_tien_nt = false;
                var _check_f_tien = false;
                var _check_f_tien_tt = false;



                if (_MA_GD == "1")
                {
                    detail1.ShowLblName = false;
                }
                else if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    //detail1.ShowLblName = true;
                    detail1.lblName.AccessibleName = "TEN_TK_I";
                }
                else if(_MA_GD == "3")
                {
                    //detail1.ShowLblName = true;
                    detail1.lblName.AccessibleName = "TEN_TK_I";
                }
                //Lấy các control động
                var dynamicControlList =GetDynamicControlsAlct();
                dynamicControlList.Add(9999, _sttRecTt);
                //dynamicControlList.Add(9998, _soSeri0);

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
                        case "MA_KH_T":
                            _ma_kh_t = control as V6VvarTextBox;
                            if (_ma_kh_t != null)
                            {
                                _ma_kh_t.CheckOnLeave = true;
                                _ma_kh_t.GotFocus += delegate
                                {
                                    if (_ngayCt0.Value != null && _ma_kh_t.Text.Trim() == "" && _ten_kh_t.Text.Trim() == "")
                                    {
                                        if (txtMaSoThue.Text != "")
                                        {
                                            _ma_kh_t.Text = txtMaKh.Text;
                                            _mst_t.Text = txtMaSoThue.Text;
                                        }
                                        _ten_kh_t.Text = txtTenKh.Text;
                                        _dia_chi_t.Text = txtDiaChi.Text;
                                    }
                                };
                                _ma_kh_t.V6LostFocus += delegate
                                {
                                    XuLyChonMaKhachT();
                                };
                            }
                            break;
                        case "TEN_KH_T":
                            _ten_kh_t = control as V6ColorTextBox;
                            break;
                        case "TEN_VT_T":
                            _ten_vt_t = control as V6ColorTextBox;
                            if (_ten_vt_t != null)
                            {
                                _ten_vt_t.GotFocus += delegate
                                {
                                    if (_ten_vt_t.Text.Trim() == "")
                                    {
                                        _ten_vt_t.Text = _dien_giaii.Text;
                                    }
                                };
                            }
                            break;
                        case "DIA_CHI_T":
                            _dia_chi_t = control as V6ColorTextBox;
                            break;
                        case "MST_T":
                            _mst_t = control as V6ColorTextBox;
                            break;
                        case "MA_THUE_I":
                            _ma_thue_i = control as V6VvarTextBox;
                            if (_ma_thue_i != null)
                            {
                                _ma_thue_i.V6LostFocus += delegate
                                {
                                    XuLyThayDoiMaThue();
                                };
                            }
                            break;
                        case "THUE_SUAT":
                            _thue_suat = control as V6NumberTextBox;
                            _thue_suat.Enabled = false;
                            _thue_suat.Tag = "disable";
                            break;
                        case "THUE_NT":
                            _thue_nt = control as V6NumberTextBox;
                            if (_thue_nt != null)
                            {
                                _thue_nt.V6LostFocus += delegate
                                {

                                    //10/08/2017 Tinh thue 1 chi tiet dang dung
                                    _thue.Value = V6BusinessHelper.Vround(_thue_nt.Value * txtTyGia.Value, M_ROUND);
                                    if (_maNt == _mMaNt0)
                                    {
                                        _thue.Value = _thue_nt.Value;
                                    }

                                    _tt_nt.Value = _psnoNt.Value + _thue_nt.Value;
                                    _tt.Value = _psno.Value + _thue.Value;
                                };
                            }
                            break;
                        case "THUE":
                            _thue = control as V6NumberTextBox;
                            break;
                        case "TK_THUE_I":
                            _tk_thue_i = control as V6VvarTextBox;
                            if (_tk_thue_i != null)
                            {
                                _tk_thue_i.SetInitFilter("Loai_tk=1");
                                _tk_thue_i.FilterStart = true;
                            }
                            break;

                        case "SO_CT0":
                            _soCt0 = (V6ColorTextBox)control;
                            if (_MA_GD == "1")
                            {
                                _soCt0.GotFocus += _soCt0_GotFocus;
                                _soCt0.V6LostFocus += SoCt0_V6LostFocus;
                                _soCt0.V6LostFocusNoChange += SoCt0_V6LostFocusNoChange;
                                _soCt0.KeyDown += _soCt0_KeyDown;
                            }
                            break;
                        case "TK_I":
                            _tkI = (V6VvarTextBox)control;
                            _tkI.Upper();
                            _tkI.V6LostFocus += delegate
                            {
                                if (V6Setting.IsVietnamese)
                                {
                                    if (_tkI.Data != null && _tkI.Data.Table.Columns.Contains("ten_tk"))
                                        detail1.lblName.Text = _tkI.Data["ten_tk"].ToString().Trim();
                                }
                                else
                                {
                                    if (_tkI.Data != null && _tkI.Data.Table.Columns.Contains("ten_tk2"))
                                        detail1.lblName.Text = _tkI.Data["ten_tk2"].ToString().Trim();
                                }

                                SetDefaultDataDetail(Invoice, detail1.panelControls);
                            };
                            _tkI.SetInitFilter("Loai_tk=1");
                            _tkI.FilterStart = true;

                            break;
                        case "T_TT_NT0":
                            _t_tt_nt0 = (V6NumberTextBox)control;
                            _t_tt_nt0.Enabled = false;
                            break;
                        case "T_TT_QD":
                            _t_tt_qd = (V6NumberTextBox)control;
                            _t_tt_qd.Enabled = false;
                            break;
                        case "TT":
                            _tt = control as V6NumberTextBox;
                            if (_tt != null)
                            {
                                _tt.DisableTag();
                            }
                            break;
                        case "TT_NT":
                            _tt_nt = control as V6NumberTextBox;
                            if (_tt_nt != null)
                            {
                                _tt_nt.DisableTag();
                            }
                            break;
                        case "PHAI_TT_NT":
                            _phaiTtNt = (V6NumberTextBox)control;
                            _phaiTtNt.Enabled = false;
                            break;
                        case "MA_NT_I":
                            _maNtI = (V6ColorTextBox)control;
                            _maNtI.Enabled = false;
                            break;
                        case "NGAY_CT0":
                            _ngayCt0 = (V6DateTimeColor)control;
                            if (_MA_GD == "1")
                            {
                                _ngayCt0.Enabled = false;
                            }
                            break;
                        case "PS_NO":
                            _psno = (V6NumberTextBox)control;
                            _check_f_ps_no = true;
                            break;
                        case "PS_NO_NT":
                            _psnoNt = (V6NumberTextBox)control;
                            _psnoNt.V6LostFocus += _psnoNt_V6LostFocus;
                            _check_f_ps_no_nt = true;
                            break;
                        case "TIEN":
                            _tien = (V6NumberTextBox)control;
                            _check_f_tien = true;
                            break;
                        case "TIEN_NT":
                            _tienNt = (V6NumberTextBox)control;
                            _tienNt.V6LostFocus += _tienNt_V6LostFocus;
                            _check_f_tien_nt = true;
                            break;
                        case "TIEN_TT":
                            _tientt = (V6NumberTextBox)control;
                            _check_f_tien_tt = true;
                            break;
                        case "TT_QD":
                            _ttqd = (V6NumberTextBox)control;
                            _ttqd.V6LostFocus += _ttqd_V6LostFocus;
                            break;
                        case "DIEN_GIAII":
                            _dien_giaii = (V6ColorTextBox)control;
                            _dien_giaii.GotFocus += _dien_giaii_GotFocus;
                            break;

                        case "MA_KH_I":
                            var ma_kh_i = control as V6VvarTextBox;
                            if (ma_kh_i != null)
                            {
                                ma_kh_i.BrotherFields = "TEN_KH";
                                ma_kh_i.NeighborFields = "TEN_KH_I";
                            }
                            break;
                        case "TEN_KH_I":
                            control.DisableTag();
                            break;

                        case "TY_GIA_HT2":
                            _ty_gia_ht2 = (V6NumberTextBox)control;
                            _ty_gia_ht2.Enabled = false;
                           // _ty_gia_ht2.Visible = false;
                            break;
                        case "SO_SERI0":
                            _so_seri0 = (V6ColorTextBox)control;
                            _so_seri0.Upper();
                            break;

                    }
                    V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
                }

                //Bo sung cac f cung
                if (_check_f_tien_nt == false)
                {
                    _tienNt = V6ControlFormHelper.CreateNumberTienNt("TIEN_NT", "tiennt", M_ROUND_NT,10, false);
                    dynamicControlList.Add(9997, _tienNt);
                }
                if (_check_f_tien == false)
                {
                    _tien = V6ControlFormHelper.CreateNumberTien("TIEN", "tien", M_ROUND, 10, false);
                    dynamicControlList.Add(9996, _tien);
                }
                if (_check_f_ps_no_nt == false)
                {
                    _psnoNt = V6ControlFormHelper.CreateNumberTienNt("PS_NO_NT", "psnont", M_ROUND_NT, 10, false);
                    dynamicControlList.Add(9995, _psnoNt);
                }
                if (_check_f_ps_no == false)
                {
                    _psno = V6ControlFormHelper.CreateNumberTien("PS_NO", "psno", M_ROUND, 10, false);
                    dynamicControlList.Add(9994, _psno);
                }
                if (_check_f_tien_tt == false)
                {
                    _tientt = V6ControlFormHelper.CreateNumberTien("TIEN_TT", "tientt", M_ROUND, 10, false);
                    dynamicControlList.Add(9993, _tientt);
                }

                
                
                detail1.RemoveControls();

                foreach (Control control in dynamicControlList.Values)
                {
                    
                    detail1.AddControl(control);
                }

                //V6ControlFormHelper.SetFormStruct (detail1, Invoice.ADStruct);
                detail1.SetStruct(Invoice.ADStruct);
                detail1.MODE = detail1.MODE;
                V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadDetailControl: " +ex.Message);
            }
        }

        private void LoadDetail2Controls()
        {
            try
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
                            _tk_du22.FilterStart=true;
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

                            }
                            break;
                        case "T_THUE_NT":
                            _t_thue_nt22 = control as V6NumberTextBox;
                            if (_t_thue_nt22 != null)
                            {

                            }
                            break;
                    }
                }

                foreach (Control control in dynamicControlList.Values)
                {
                    detail2.AddControl(control);
                }

                //V6ControlFormHelper.SetFormStruct (detail2, Invoice.AD2Struct);
                detail2.SetStruct(Invoice.AD2Struct);
                detail2.MODE = detail2.MODE;
                V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView2, _alct2Dic, _maNt, _mMaNt0);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadDetail2Controls: " + ex.Message);
            }
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
                //#region ---- Set Event ----
                //if (control is V6NumberTextBox)
                //{
                //    //toolTip1.SetToolTip(control, ((V6NumberTextBox)control).TextTitle);
                //    control.Enter += delegate(object sender, EventArgs e)
                //    {
                //        var s = ((V6NumberTextBox)sender).AccessibleName + ": " + ((V6NumberTextBox)sender).GrayText;
                //        V6ControlFormHelper.SetStatusText(s);

                //        var location = control.Location;
                //        location.Y -= 22;
                //        //toolTip1.Show(((V6NumberTextBox)sender).TextTitle, ((V6NumberTextBox)sender).Parent, location);
                //    };
                //}
                //else if (control is V6ColorTextBox)
                //{
                //    //toolTip1.SetToolTip(control,((V6ColorTextBox)control).TextTitle);
                //    control.Enter += delegate(object sender, EventArgs e)
                //    {
                //        var s = ((V6ColorTextBox)sender).AccessibleName + ": " + ((V6ColorTextBox)sender).GrayText;
                //        V6ControlFormHelper.SetStatusText(s);

                //        var location = control.Location;
                //        location.Y -= 22;
                //        //toolTip1.Show(((V6ColorTextBox)sender).TextTitle, ((V6ColorTextBox)sender).Parent, location);
                //    };
                //}
                //else if (control is V6DateTimePick)
                //{
                //    //toolTip1.SetToolTip(control, ((V6ColorDateTimePick)control).TextTitle);
                //    control.Enter += delegate(object sender, EventArgs e)
                //    {
                //        var s = ((V6DateTimePick)sender).AccessibleName + ": " + ((V6DateTimePick)sender).TextTitle;
                //        V6ControlFormHelper.SetStatusText(s);

                //        var location = control.Location;
                //        location.Y -= 22;
                //        //toolTip1.Show(((V6DateTimePick)sender).TextTitle, ((V6DateTimePick)sender).Parent, location);
                //    };
                //}
                //#endregion set event

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
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd: " + ex.Message);
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
                            _PsCo_33.Value = V6BusinessHelper.Vround(_PsCoNt_33.Value*txtTyGia.Value, M_ROUND);
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
                    _sttRec03 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView3, detail3, out _gv3EditingRow);
                    detail3.ChangeToEditMode();
                    
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
        
        void _mau_bc_GotFocus(object sender, EventArgs e)
        {
            if (_mau_bc.Value == 0)
            {
                _mau_bc.Value = 1;
            }
        }

        void _ttqd_V6LostFocus(object sender)
        {
            try
            {
                _tientt.Value = _ttqd.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + "._ttqd_V6LostFocus: " + ex.Message);
            }
        }

        void _tienNt_V6LostFocus(object sender)
        {
            try
            {
                _tien.Value = V6BusinessHelper.Vround(_tienNt.Value*txtTyGia.Value, M_ROUND);
                TinhTienThue();

                if (_psnoNt != null)
                _psnoNt.Value = _tienNt.Value;
                
                if (cboMaNt.SelectedValue.ToString() == _mMaNt0)
                {
                    _tien.Value = _tienNt.Value;
                    if(_psno!=null && _psnoNt!=null)
                        _psno.Value = _psnoNt.Value;
                }
                // Tuanmh 09/02/2016
                if (_tien != null)
                     _tientt.Value = _tien.Value;

                if (_ttqd != null && _tienNt != null && _maNtI.Text == _maNt)
                {
                    _ttqd.Value = _tienNt.Value;
                }
            }   
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + "._tienNt_V6LostFocus: " + ex.Message);
            }
        }
        void _psnoNt_V6LostFocus(object sender)
        {
            try
            {
                
                _psno.Value = V6BusinessHelper.Vround(_psnoNt.Value * txtTyGia.Value, M_ROUND);
                _tien.Value = _psno.Value;
                if (_tienNt != null)

                _tienNt.Value = _psnoNt.Value;

                if (cboMaNt.SelectedValue.ToString() == _mMaNt0)
                {
                    if (_tien != null && _tienNt!=null)
                    _tien.Value = _tienNt.Value;

                    _psno.Value = _psnoNt.Value;
                }
                // Tuanmh 09/02/2016
                if (_tien != null)
                     _tientt.Value = _tien.Value;

                //10/08/2017 Tinh thue 1 chi tiet dang dung
                _thue_nt.Value = V6BusinessHelper.Vround(_thue_suat.Value * _tienNt.Value / 100, M_ROUND);
                _thue.Value = V6BusinessHelper.Vround(_thue_nt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _thue.Value = _thue_nt.Value;
                }

                _tt_nt.Value = _psnoNt.Value + _thue_nt.Value;
                _tt.Value = _psno.Value + _thue.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + "._psnoNt_V6LostFocus: " + ex.Message);
            }
        }


        void _soCt0_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView(Invoice.Alct0, "So_ct", "ARS30", _soCt0, initFilter);
                    if (f.ViewData.Count > 0)
                    {
                        var d = f.ShowDialog(this);
                        //xu ly data
                        if (d == DialogResult.OK)
                        {
                            XuLyKhiNhanSoCt(((DataGridViewRow) _soCt0.Tag).ToDataDictionary());
                        }
                        else
                        {
                            _soCt0.Text = _soCt0.GotFocusText;
                        }
                    }
                    else
                    {
                        ShowParentMessage("Alct0_ARS30 " + V6Text.NoData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + "._soCt0_KeyDown: " + ex.Message);
            }
        }

        void _soCt0_GotFocus(object sender, EventArgs e)
        {
            Invoice.GetSoct0(_sttRec, txtMaKh.Text, txtMadvcs.Text);
        }
        void _dien_giaii_GotFocus(object sender, EventArgs e)
        {
            if (_dien_giaii.Text == "")
            {
                _dien_giaii.Text = Txtdien_giai.Text;
            }
        }

        /// <summary>
        /// Lấy động danh sách control (textbox) từ bảng Alct
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<int, Control> GetDynamicControlsAlct()
        {
            //exec [VPA_GET_AUTO_COLULMN] 'SOA','','','','';//08/12/2015
            var result = new SortedDictionary<int, Control>();

            DataTable alct1;

            _orderList = new List<string>();
            if (_MA_GD == "1")
            {
                if (alct1_01 == null)
                {
                    alct1_01 = Invoice.GetAlct1(_MA_GD);
                }

                    alct1 = alct1_01;
            }
            else if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
            {
                if (alct1_02 == null)
                {
                    alct1_02 = Invoice.GetAlct1(_MA_GD);
                }

                    alct1 = alct1_02;
            }
            else if (_MA_GD == "3")
            {
                if (alct1_03 == null)
                {
                    alct1_03 = Invoice.GetAlct1(_MA_GD);
                }

                    alct1 = alct1_03;
            }
            else
            {
                throw  new Exception("Chọn lại ma_gd");
            }

            var dynamicControlList = V6ControlFormHelper.GetDynamicControlsAlct(alct1, out _orderList, out _alct1Dic);
            if (_MA_GD != "1")
                _orderList.Insert(1, "TEN_TK_I");
            return dynamicControlList;

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
                                CreateLimitTextBox(fcolumn, "x", fcaption, width, fstatus));
                        }
                        else
                        {
                            
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
                        decimals = V6Options.M_IP_TIEN_NT;

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
            if (_MA_GD != "1")
                _orderList.Insert(1, "TEN_TK_I");
            return result;
        }
        public SortedDictionary<int, Control> GetDynamicControlsAlct2
            (DataTable alctData, out SortedDictionary<string, DataRow> alctDic, out List<string> orderList)
        {
            //exec [VPA_GET_AUTO_COLULMN] 'POA','','','','';//08/12/2015
            var result = new SortedDictionary<int, Control>();

            //var alctData = Invoice.Alct1;
            var alct1Dic = new SortedDictionary<string, DataRow>();
            var order1List = new List<string>();

            foreach (DataRow row in alctData.Rows)
            {
                var visible = 1 == Convert.ToInt32(row["visible"]);
                if (!visible) continue;

                var fcolumn = row["fcolumn"].ToString().Trim().ToUpper();

                alct1Dic.Add(fcolumn, row);
                order1List.Add(fcolumn);

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
                                CreateLimitTextBox(fcolumn, "x", fcaption, width, fstatus));
                        }
                        else
                        {

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
                        decimals = V6Options.M_IP_TIEN;// row["fdecimal"] == null ? V6Setting.DecilalsNumber : Convert.ToInt32(row["fdecimal"]);
                        result.Add(fOrder, V6ControlFormHelper.
                                CreateNumberTien(fcolumn, fcaption, decimals, width, fstatus)
                                );

                        break;

                    case "N1"://Ngoai te
                        decimals = V6Options.M_IP_TIEN_NT;

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
            alctDic = alct1Dic;
            orderList = order1List;

            return result;
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
                    btnQuayRa.PerformClick();
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
            else if (keyData == Keys.F)
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
      

        void SoCt0_V6LostFocus(object sender)
        {
            CheckAlct0();
            SetDefaultDataDetail(Invoice, detail1.panelControls);
        }
        void SoCt0_V6LostFocusNoChange(object sender)
        {
            if (_soCt0.Text.Trim() == "") CheckAlct0();
        }
        

        #endregion events

        #region Methods
        
        private void CheckAlct0()
        {
            try
            {
                var inputUpper = _soCt0.Text.Trim().ToUpper();
                if (Invoice.Alct0 != null)
                {
                    var check = false;
                    foreach (DataRow row in Invoice.Alct0.Rows)
                    {
                        if (row["So_ct"].ToString().Trim().ToUpper() == inputUpper)
                            check = true;
                        if (check)
                        {
                            //
                            _soCt0.Tag = row;
                            XuLyKhiNhanSoCt(row.ToDataDictionary());
                            break;
                        }
                    }

                    if (!check)
                    {
                        var initFilter = GetSoCt0InitFilter();
                        var f = new FilterView(Invoice.Alct0, "So_ct", "ARS30", _soCt0, initFilter);
                        if (f.ViewData.Count > 0)
                        {
                            var d = f.ShowDialog(this);
                            //xu ly data
                            if (d == DialogResult.OK)
                            {
                                if (_soCt0.Tag is DataRow)
                                    XuLyKhiNhanSoCt(((DataRow) _soCt0.Tag).ToDataDictionary());
                                else if (_soCt0.Tag is DataGridViewRow)
                                    XuLyKhiNhanSoCt(((DataGridViewRow) _soCt0.Tag).ToDataDictionary());
                            }
                            else
                            {
                                _soCt0.Text = _soCt0.GotFocusText;
                            }
                        }
                        else
                        {
                            ShowParentMessage("Alct0_ARS30 " + V6Text.NoData);
                        }
                    }
                    else if(detail1.MODE == V6Mode.Add)
                    {
                        //Check so ct da chon
                        check = false;
                        foreach (DataRow row in AD.Rows)
                        {
                            if (row["So_ct0"].ToString().Trim().ToUpper() == inputUpper)
                            {
                                check = true;
                                break;
                            }
                        }
                        if(check) this.ShowWarningMessage("Số hóa đơn đã chọn! " + inputUpper);
                    }
                }
                else
                {
                    this.ShowErrorMessage(GetType() + ".Alct0 null");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckAlct0: " + ex.Message);
            }
        }

        private void XuLyKhiNhanSoCt(IDictionary<string,object> row)
        {
            try
            {
                _tkI.Text = row["TK"].ToString().Trim();
                _t_tt_nt0.Value = ObjectAndString.ObjectToDecimal(row["TC_TT"]);
                _t_tt_qd.Value = ObjectAndString.ObjectToDecimal(row["T_TT_QD"]);
                _phaiTtNt.Value = ObjectAndString.ObjectToDecimal(row["CL_TT"]);

                _ty_gia_ht2.Value = ObjectAndString.ObjectToDecimal(row["TY_GIA"]);

                _maNtI.Text = row["MA_NT"].ToString().Trim();
                _sttRecTt.Text = row["STT_REC"].ToString().Trim();

                _ngayCt0.Value = ObjectAndString.ObjectToDate(row["NGAY_CT0"]);
                
                _tienNt.Value = _phaiTtNt.Value;
                _tien.Value = _phaiTtNt.Value;
                _ttqd.Value = _phaiTtNt.Value;
                _tientt.Value = _phaiTtNt.Value;

                _psnoNt.Value = _phaiTtNt.Value;
                _psno.Value = _phaiTtNt.Value;

                if (_maNtI.Text != _mMaNt0)
                {
                    _tientt.Value = V6BusinessHelper.Vround(_phaiTtNt.Value * _ty_gia_ht2.Value, M_ROUND);
                    _tien.Value = V6BusinessHelper.Vround(_phaiTtNt.Value * txtTyGia.Value, M_ROUND);
                    _psno.Value = _tien.Value;
                }

                //{Tuanmh 21/08/2016

                if (Txtdien_giai.Text != "")
                {
                    _dien_giaii.Text = Txtdien_giai.Text.Trim() + " số " + row["SO_CT0"].ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC((DateTime)row["NGAY_CT0"]);
                }
                else
                {
                    _dien_giaii.Text = " Chi tiền theo CT số " + row["SO_CT0"].ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC((DateTime)row["NGAY_CT0"]);
                }
                //}

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyKhiNhanSoCt_DataRow: " + ex.Message);
            }
        }
        
        //private void XuLyKhiNhanSoCt(DataGridViewRow row)
        //{
        //    try
        //    {
        //        var ngay_ct = ObjectAndString.ObjectToDate(row.Cells["NGAY_CT"].Value);
        //        var ngay_ct0 = ObjectAndString.ObjectToDate(row.Cells["NGAY_CT0"].Value);
        //        if(ngay_ct == null) this.ShowWarningMessage("Ngày chứng từ trống.");
        //        if(ngay_ct0 == null) this.ShowWarningMessage("Ngày hóa đơn trống.");
        //        _tkI.Text = row.Cells["TK"].Value.ToString().Trim();
        //        _t_tt_nt0.Value = ObjectAndString.ObjectToDecimal(row.Cells["TC_TT"].Value);
        //        _t_tt_qd.Value = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_QD"].Value);
        //        _phaiTtNt.Value = ObjectAndString.ObjectToDecimal(row.Cells["CL_TT"].Value);
        //        _ty_gia_ht2.Value = ObjectAndString.ObjectToDecimal(row.Cells["TY_GIA"].Value);
        //        _maNtI.Text = row.Cells["MA_NT"].Value.ToString().Trim();
        //        _sttRecTt.Text = row.Cells["STT_REC"].Value.ToString().Trim();
        //        _ngayCt0.Value = ObjectAndString.ObjectToDate(ngay_ct0);
        //        _tienNt.Value = _phaiTtNt.Value;
        //        _tien.Value = _phaiTtNt.Value;
        //        _ttqd.Value = _phaiTtNt.Value;
        //        _psnoNt.Value = _phaiTtNt.Value;
        //        _psno.Value = _phaiTtNt.Value;
        //        //{Tuanmh 21/08/2016
        //        if (Txtdien_giai.Text != "")
        //        {
        //            _dien_giaii.Text = Txtdien_giai.Text.Trim() + " số " + row.Cells["SO_CT0"].Value.ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC(ngay_ct0);
        //        }
        //        else
        //        {
        //            _dien_giaii.Text = " Chi tiền theo CT số " + row.Cells["SO_CT0"].Value.ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC(ngay_ct0);
        //        }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorMessage(GetType() + ".XuLyKhiNhanSoCt -DataGridViewRow: " + ex.Message);
        //    }
        //}

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
                    detail2.MODE = V6Mode.Lock;
                    detail3.MODE = V6Mode.Lock;
                }
                else //Cac truong hop khac
                {
                    detail1.MODE = V6Mode.Init;
                    detail3.MODE = V6Mode.Init;
                    Detail2ModeByChkSuaThue();
                    
                    XuLyKhoaThongTinKhachHang();
                    txtTyGia.Enabled = _maNt != _mMaNt0;
                    
                    Txtty_gia_ht.Visible = _maNt != _mMaNt0;

                    //chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    //chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                    //txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    //txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".EnableFormControls: " + ex.Message);
            }
        }

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

        private bool NhapThueTuDong()
        {
            try
            {
                if ((Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    && _MA_GD != "A" && _MA_GD != "1"
                    && !chkSuaThue.Checked)
                {
                    //Xoa AD2
                    AD2.Rows.Clear();
                    //Them AD2 theo AD
                    for (int i = 0; i < AD.Rows.Count; i++)
                    {
                        var adRow = AD.Rows[i];
                        var newRow = AD2.NewRow();
                        //neu co ngay_ct0, so_ct0 trong ad thi them vao ad2
                        if (adRow["Ngay_ct0"] != DBNull.Value && adRow["So_ct0"] != null &&
                            adRow["So_ct0"].ToString().Trim() != "")
                        {
                            for (int j = 0; j < AD.Columns.Count; j++)
                            {
                                if (AD2.Columns.Contains(AD.Columns[j].ColumnName))
                                {
                                    newRow[AD.Columns[j].ColumnName] = adRow[AD.Columns[j].ColumnName];
                                }
                            }

                            //mau_bc,ma_thue_i, ma_kh_t, dia_chi_t, ten_vt_t, mst_t, ghi_chu_t,ma_kh2_t
                            newRow["mau_bc"] = adRow["mau_bc"];
                            newRow["ma_thue"] = adRow["ma_thue_i"];
                            newRow["ma_kh"] = adRow["ma_kh_t"];
                            newRow["ten_kh"] = adRow["ten_kh_t"];
                            newRow["dia_chi"] = adRow["dia_chi_t"];
                            newRow["ten_vt"] = adRow["ten_vt_t"];
                            newRow["ma_so_thue"] = adRow["mst_t"];
                            newRow["ghi_chu"] = adRow["ghi_chu_t"];
                            newRow["ma_kh2"] = adRow["ma_kh2_t"];

                            newRow["t_tien_nt"] = adRow["ps_no_nt"];
                            newRow["t_tien"] = adRow["ps_no"];
                            newRow["thue_suat"] = adRow["thue_suat"];
                            newRow["t_thue_nt"] = adRow["thue_nt"];
                            newRow["t_thue"] = adRow["thue"];

                            newRow["tk_thue_no"] = adRow["tk_thue_i"];
                            newRow["tk_du"] = txtTk.Text;
                            newRow["ma_vv"] = adRow["ma_vv_i"];

                            _sttRec02 = V6BusinessHelper.GetNewSttRec0(AD2);
                            newRow["STT_REC0"] = _sttRec02;
                            AD2.Rows.Add(newRow);
                            dataGridView2.DataSource = AD2;
                            
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".NhapThueTuDong " + ex.Message);
                return false;
            }
            return true;
        }

        private void TinhTongValues()
        {
            //TongTien
            var tTienNt = V6BusinessHelper.TinhTong(AD, "PS_NO_NT");
            var tPsNoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_NO_NT", "OPER_TT");
            var tPsCoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_CO_NT", "OPER_TT");
            txtTongTangGiamNt.Value = tPsNoNt;
            txtTongTienNt.Value = V6BusinessHelper.Vround(tTienNt, M_ROUND_NT);
            
            var tTien = V6BusinessHelper.TinhTong(AD, "PS_NO");
            var tPsNo = V6BusinessHelper.TinhTongOper(AD3, "PS_NO", "OPER_TT");
            var tPsCo = V6BusinessHelper.TinhTongOper(AD3, "PS_CO", "OPER_TT");
            txtTongTangGiam.Value = tPsNo;
            txtTongTien.Value = V6BusinessHelper.Vround(tTien, M_ROUND);

            //TongThue
            var tThueNt = V6BusinessHelper.TinhTong(AD, "THUE_NT");
            txtTongThueNt.Value = V6BusinessHelper.Vround(tThueNt, M_ROUND_NT);
            var tThue = V6BusinessHelper.TinhTong(AD, "THUE");
            txtTongThue.Value = V6BusinessHelper.Vround(tThue, M_ROUND);

            //TongThanhToan
            var tTienTTNt = tTienNt + tThueNt + tPsNoNt;
            txtTongThanhToanNt.Value = V6BusinessHelper.Vround(tTienTTNt, M_ROUND_NT);
            var tTienTT = tTien + tThue + tPsNo;
            txtTongThanhToan.Value = V6BusinessHelper.Vround(tTienTT, M_ROUND);
        }

        private void TinhTongThanhToan(string  debug)
        {
            try
            {
                ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;
            
                HienThiTongSoDong(lblTongSoDong);
                //XuLyThayDoiTyGia();
                TinhTongValues();
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
            //
            txtTk.Text = Invoice.Alct.Rows[0]["TK_CO"].ToString().Trim();
            cboKieuPost.SelectedValue = Invoice.Alct.Rows[0]["M_K_POST"].ToString().Trim();

        }

        private void XuLyThayDoiMaThue()
        {
            try
            {
                var alThue = V6BusinessHelper.Select("ALTHUE30", "*",
                                  "MA_THUE = '" + _ma_thue_i.Text.Trim() + "'");
                if (alThue.TotalRows > 0)
                {
                    _tk_thue_i.Text = alThue.Data.Rows[0]["TK_THUE_NO"].ToString().Trim();
                    _thue_suat.Value = ObjectAndString.ObjectToDecimal(alThue.Data.Rows[0]["THUE_SUAT"]);
                }
                //Tinh thue 1 chi tiet dang dung
                _thue_nt.Value = V6BusinessHelper.Vround(_thue_suat.Value * _tienNt.Value / 100, M_ROUND);
                _thue.Value = V6BusinessHelper.Vround(_thue_nt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _thue.Value = _thue_nt.Value;
                }
                _tt_nt.Value = _psnoNt.Value + _thue_nt.Value;
                _tt.Value = _psno.Value + _thue.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyThayDoiMaThue", ex);
            }
        }

        /// <summary>
        /// Ẩn hiện các cột của GridView
        /// </summary>
        private void XuLyThayDoiLoaiPhieuThu()
        {
            _MA_GD = txtLoaiPhieu.Text;
            try
            {
                if (_MA_GD == "1")
                {
                    LoadDetailControls("1");
                    
                    //alct = alct1_01;

                    var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.Visible = false;
                    }
                }
                //Loại 2
                if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    LoadDetailControls("2");
                    //alct = alct1_02;
                    var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = true;
                        dataGridViewColumn.DisplayIndex = 0;
                    }

                    var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.Visible = true;
                        gridViewColumn.DisplayIndex = 1;
                    }
                }
                //Loại 3
                if (_MA_GD == "3")
                {
                    LoadDetailControls("3");
                    //alct = alct1_03;
                    var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = true;
                        dataGridViewColumn.DisplayIndex = 0;
                    }

                    var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.Visible = true;
                        gridViewColumn.DisplayIndex = 1;
                    }
                }

                GridViewFormat();
                
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    var field = column.DataPropertyName.ToUpper();
                    if (_orderList.Contains(field))
                    {
                        //column.Visible = true;
                        //if (field != "TK_I")
                        //column.DisplayIndex = _orderList.IndexOf(column.DataPropertyName.ToUpper()) + index;
                    }
                    else
                    {
                        if ("A3".Contains(_MA_GD) && field == "TEN_KH_I")
                        {
                            continue;
                        }

                        if (field != "TK_I" && field != "TEN_TK_I")
                        {
                            column.Visible = false;
                        }
                    }
                }

                if (dataGridView1.DataSource != null && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.CurrentCell
                        = dataGridView1.Rows[0].Cells[_MA_GD == "1" ? "SO_CT0" : "TK_I"];
                    
                    //detail1.SetData(dataGridView1.GetCurrentRowData());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyThayDoiLoaiPhieuThu: " + ex.Message);
            }
            finally
            {
                XuLyThayDoiMaNt();
            }
        }

        private void XuLyKhoaThongTinTheoMaGD()
        {
            try
            {
                _MA_GD = txtLoaiPhieu.Text.Trim().ToUpper();
                if (_MA_GD == "1")
                {
                    _tkI.Enabled = false;
                    MoKhoaThongTinKH();
                }
                else if (_MA_GD == "A")
                {
                    //Enable 
                    _tkI.Enabled = true;
                    KhoaThongTinKH();
                }
                else if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    //Enable 
                    _tkI.Enabled = true;
                    MoKhoaThongTinKH();
                }
                else if (_MA_GD == "3")
                {
                    _tkI.Enabled = true;
                    KhoaThongTinKH();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtLoaiPhieu_TextChanged", ex);
            }
        }

        private void MoKhoaThongTinKH()
        {
            txtMaKh.Enabled = true;
            txtDiaChi.Enabled = true;

            txtDiaChi.ReadOnlyTag(false);
            txtDiaChi.TabStop = true;
            txtTenKh.ReadOnlyTag(false);
            txtTenKh.TabStop = true;
        }

        private void KhoaThongTinKH()
        {
            txtMaKh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtMaKh.Text = "";
            txtTenKh.Text = "";
            txtDiaChi.Text = "";
            txtMaSoThue.Text = "";
        }

        private void XuLyThayDoiMaNt()
        {
            try
            {
                if (!_ready0) return;

                var newText = (V6Setting.IsVietnamese ? "Ps nợ " : "Amount ") + _maNt;
                _psnoNt.GrayText = newText;
                var viewColumn = dataGridView1.Columns["PS_NO_NT"];
                if (viewColumn != null) viewColumn.HeaderText = newText;

                newText = (V6Setting.IsVietnamese ? "Thanh toán " : "Amount ") + _maNt;
                var column = dataGridView1.Columns["TIEN_NT"];
                if (column != null) column.HeaderText = newText;
                _tienNt.GrayText = newText;

                newText = (V6Setting.IsVietnamese ? "Ps nợ " : "Amount ") + _mMaNt0;
                _psno.GrayText = newText;
                viewColumn = dataGridView1.Columns["PS_NO"];
                if (viewColumn != null) viewColumn.HeaderText = newText;

                newText = (V6Setting.IsVietnamese ? "Thanh toán " : "Amount ") + _mMaNt0;
                _tien.GrayText = newText;
                column = dataGridView1.Columns["TIEN"];
                if (column != null) column.HeaderText = newText;

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
                    chkSuaTggs.Visible = true;
                    Txtty_gia_ht.Visible = true;
                    
                    //_psno.VisibleTag();
                    detail1.ShowIDs(new[]{"ps_no","tt", "thue"});
                    
                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "PS_NO");
                    if (c != null) c.Visible = true;

                    //SetColsVisible
                    var dataGridViewColumn = dataGridView1.Columns["PS_NO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    panelVND.Visible = true;

                    //Detail3
                    detail3.ShowIDs(new[] { "PS_NO", "PS_CO" });

                    dataGridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
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
                    panelVND.Visible = false;

                    chkSuaTggs.Visible = false;
                    Txtty_gia_ht.Visible = false;
                    Txtty_gia_ht.Value = 1;
                    
                    detail1.HideIDs(new []{"ps_no", "tt", "thue"});
                    
                    //SetColsVisible
                    var dataGridViewColumn = dataGridView1.Columns["PS_NO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    //Detail3
                    detail3.HideIDs(new[] { "PS_NO", "PS_CO" });

                    dataGridViewColumn = dataGridView3.Columns["PS_NO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    gridViewColumn = dataGridView3.Columns["PS_CO"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;

                    // Show Dynamic control
                    if (_PsNoNt_33 != null) _PsNo_33.InvisibleTag();
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
                _psnoNt.DecimalPlaces = decimalPlaces;

                _t_tien_nt22.DecimalPlaces = decimalPlaces;
                _t_thue_nt22.DecimalPlaces = decimalPlaces;

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
                var column = dataGridView1.Columns["PS_NO_NT"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                //{04/12/2016
                column = dataGridView1.Columns["TY_GIA_HT2"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                column = dataGridView1.Columns["T_TT_NT0"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                column = dataGridView1.Columns["T_TT_QD"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
                column = dataGridView1.Columns["PHAI_TT_NT"];
                if (column != null)
                {
                    column.DefaultCellStyle.Format = "N" + decimalPlaces;
                }
               //}

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

        private void SetGridViewData()
        {
            HienThiTongSoDong(lblTongSoDong);
            dataGridView1.DataSource = AD;
            dataGridView2.DataSource = AD2;
            dataGridView3.DataSource = AD3;
            ReorderDataGridViewColumns();
            GridViewFormat();
        }

        private void ReorderDataGridViewColumns()
        {

            int i;
            if (_MA_GD == "1")
            {
                i = 0;
                var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = false;
                    //dataGridViewColumn.DisplayIndex = 0;
                }

                var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = false;
                    //gridViewColumn.DisplayIndex = 1;
                }
            }
            else //if (_maGd == "2")
            {
                i = 2;
                var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = true;
                    dataGridViewColumn.DisplayIndex = 0;
                }

                var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = true;
                    gridViewColumn.DisplayIndex = 1;
                }
            }

            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList, i);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView2, _orderList2);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView3, _orderList3);
        }

        private void GridViewFormat()
        {
            //GridView1 !!!!!

            //GridView3
            var f = dataGridView3.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }
            f = dataGridView3.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_SL"];
            }
            f = dataGridView1.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
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
                ADTables.Add(sttRec, Invoice.LoadAd(sttRec));
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
        /// <summary>
        /// Hiển thị phiếu theo sttrec
        /// </summary>
        /// <param name="sttrec"></param>
        /// <param name="mode">Mode trước đó</param>
        public override void ViewInvoice(string sttrec, V6Mode mode)
        {
            try
            {
                detail1.MODE = V6Mode.View;

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
                XuLyThayDoiLoaiPhieuThu();
                //Tuanmh 20/02/2016
                XuLyThayDoiMaNt();

                //txtMadvcs.ExistRowInTable();
                if (V6Setting.Language.Trim() == "V")
                {
                    if (txtMadvcs.Data != null && txtMadvcs.Data.Table.Columns.Contains(txtTenDVCS.AccessibleName))
                        txtTenDVCS.Text = txtMadvcs.Data[txtTenDVCS.AccessibleName].ToString().Trim();
                    //txtMaKh.ExistRowInTable();
                    if (txtMaKh.Data != null && txtMaKh.Data.Table.Columns.Contains(txtTenKh.AccessibleName))
                        txtTenKh.Text = txtMaKh.Data[txtTenKh.AccessibleName].ToString().Trim();
                    //txtLoaiPhieu.ExistRowInTable();
                    if (txtLoaiPhieu.Data != null &&
                        txtLoaiPhieu.Data.Table.Columns.Contains(txtTenGiaoDich.AccessibleName))
                        txtTenGiaoDich.Text = txtLoaiPhieu.Data[txtTenGiaoDich.AccessibleName].ToString().Trim();

                    //txtTk.ExistRowInTable();
                    if (txtTk.Data != null && txtTk.Data.Table.Columns.Contains(TxtTen_tk.AccessibleName))
                        TxtTen_tk.Text = txtTk.Data[TxtTen_tk.AccessibleName].ToString().Trim();

                }
                else
                {
                    if (txtMadvcs.Data != null && txtMadvcs.Data.Table.Columns.Contains("TEN_DVCS2"))
                        txtTenDVCS.Text = txtMadvcs.Data["TEN_DVCS2"].ToString().Trim();
                    //txtMaKh.ExistRowInTable();
                    if (txtMaKh.Data != null && txtMaKh.Data.Table.Columns.Contains("TEN_KH2"))
                        txtTenKh.Text = txtMaKh.Data["TEN_KH2"].ToString().Trim();
                    //txtLoaiPhieu.ExistRowInTable();
                    if (txtLoaiPhieu.Data != null &&
                        txtLoaiPhieu.Data.Table.Columns.Contains("TEN_GD2"))
                        txtTenGiaoDich.Text = txtLoaiPhieu.Data["TEN_GD2"].ToString().Trim();

                    //txtTk.ExistRowInTable();
                    if (txtTk.Data != null && txtTk.Data.Table.Columns.Contains("TEN_TK2"))
                        TxtTen_tk.Text = txtTk.Data["TEN_TK2"].ToString().Trim();
                }
                txtMadvcs.ExistRowInTable();
                txtMaKh.ExistRowInTable();
                txtTk.ExistRowInTable();

                SetGridViewData();
                Mode = V6Mode.View;
                //btnSua.Focus();
                FormatNumberControl();
                FormatNumberGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewInvoice", ex);
            }
        }

        #region ==== Add Thread ====
        private SortedDictionary<string, object> addDataAM;
        private List<SortedDictionary<string, object>> addDataADList, addDataAD2, addDataAD3;
        private string addErrorMessage = "";
        private void DoAdd()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;//!!!

                if (Invoice.InsertInvoice(addDataAM, addDataADList, addDataAD2, addDataAD3))
                {
                    flagSuccess = true;
                    Mode = V6Mode.View;
                }
                else
                {
                    flagSuccess = false;
                    addErrorMessage = "Thêm không thành công";
                    Invoice.PostErrorLog(_sttRec, "M");
                }
            }
            catch (Exception ex)
            {
                flagSuccess = false;
                addErrorMessage = ex.Message;
                Invoice.PostErrorLog(_sttRec, "M", ex);
            }
            flagAddFinish = true;
        }
        private void DoAddThread()
        {
            try
            {
                ReadyForAdd();
                Timer checkAdd = new Timer();
                checkAdd.Interval = 500;
                checkAdd.Tick += checkAdd_Tick;
                new Thread(DoAdd)
                {
                    IsBackground = true
                }
                .Start();
                flagAddFinish = false;
                checkAdd.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private bool flagAddFinish, flagSuccess;
        void checkAdd_Tick(object sender, EventArgs e)
        {
            if (flagAddFinish)
            {
                ((Timer)sender).Stop();

                if (flagSuccess)
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
                addDataADList = dataGridView1.GetData(_sttRec);
                addDataAD2 = dataGridView2.GetData(_sttRec);
                addDataAD3 = dataGridView3.GetData(_sttRec);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
#endregion add
        
        #region ==== Edit Thread ====
        private bool flagEditFinish, flagEditSuccess;
        private List<SortedDictionary<string, object>> editDataAD, editDataAD2, editDataAD3;
        private string editErrorMessage = "";

        private void DoEditThread()
        {
            V6ControlFormHelper.AddLastAction("\nDoEditThread(), ReadyForEdit() Begin: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            ReadyForEdit();
            V6ControlFormHelper.AddLastAction("\nReadyForEdit() End: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
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
                editDataAD2 = dataGridView2.GetData(_sttRec);
                foreach (SortedDictionary<string, object> adRow in editDataAD2)
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
                ((Timer) sender).Stop();
                //Ghi log add edit time.
                this.WriteToLog(string.Format("AddEditTime({0})", _sttRec), "Xem LastAction! ");
                
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

                ((Timer) sender).Dispose();
            }
            else
            {
                V6ControlFormHelper.AddLastAction("\ncheckEdit_Tick(): " + Invoice.V6Message);
            }
        }

        private void DoEdit()
        {
            V6ControlFormHelper.AddLastAction("\nDoEdit() Begin: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                var keys = new SortedDictionary<string, object> { { "STT_REC", _sttRec } };
                V6ControlFormHelper.AddLastAction("\nInvoice.UpdateInvoice() Begin: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (Invoice.UpdateInvoice(addDataAM, editDataAD, editDataAD2, editDataAD3, keys))
                {
                    V6ControlFormHelper.AddLastAction("\nInvoice.UpdateInvoice() End Succes: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    flagEditSuccess = true;
                    ADTables.Remove(_sttRec);
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                }
                else
                {
                    V6ControlFormHelper.AddLastAction("\nInvoice.UpdateInvoice() End Fail: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
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
            V6ControlFormHelper.AddLastAction("\nDoEdit() End: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
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
                if (_print_flag != V6PrintMode.DoNoThing)
                {
                    var temp = _print_flag;
                    _print_flag = V6PrintMode.DoNoThing;
                    BasePrint(Invoice, _sttRec_In, temp, TongThanhToan, TongThanhToanNT, true);
                    SetStatus2Text();
                }
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
                    AD2Tables.Remove(_sttRec);
                    AD3Tables.Remove(_sttRec);
                }
                else
                {
                    flagDeleteSuccess = false;
                    deleteErrorMessage = "Xóa không thành công.";
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice41.DeleteInvoice return false." + Invoice.V6Message);
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
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||
                    detail2.MODE == V6Mode.Add || detail2.MODE == V6Mode.Edit)
                {
                    this.ShowWarningMessage(V6Text.DetailNotComplete);
                    EnableFunctionButtons();
                }
                else
                {
                    V6ControlFormHelper.RemoveRunningList(_sttRec);
                    TinhToanTruocKhiLuu();
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

                    ResetForm();
                    Mode = V6Mode.Add;

                    txtLoaiPhieu.ChangeText(_MA_GD);

                    GetSttRec(Invoice.Mact);
                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + txtSoPhieu.Text);
                    //GetSoPhieu();

                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    XuLyKhoaThongTinTheoMaGD();
                    XuLyThayDoiLoaiPhieuThu();
                    detail1.DoAddButtonClick();
                    SetDefaultDetail();
                    
                    txtMa_sonb.Focus();
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Moi: " + ex.Message);
            }
        }

        private void Sua()
        {
            try
            {
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + txtSoPhieu.Text);
                if(IsViewingAnInvoice)
                if (V6Login.UserRight.AllowEdit("", Invoice.CodeMact))
                {
                    if(string.IsNullOrEmpty(_sttRec))
                    {
                        this.ShowInfoMessage(V6Text.NoSelection);
                    }
                    else if (Mode == V6Mode.View)
                    {
                        var row = AM.Rows[CurrentIndex];

                        // Tuanmh 16/02/2016 Check level
                        if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]), (row["Xtag"]??"").ToString().Trim()))
                        {
                            Mode = V6Mode.Edit;
                            detail1.MODE = V6Mode.View;
                            if(chkSuaThue.Checked)
                                detail2.MODE = V6Mode.View;
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
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Sua: " + ex.Message);
            }
        }

        private void Xoa()
        {
            try
            {
                if(IsViewingAnInvoice)
                if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact))
                {
                    if (Mode == V6Mode.View)
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

                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
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
                            this.ShowWarningMessage("Chưa chọn phiếu thu.");
                        }
                        else
                        {
                            GetSttRec(Invoice.Mact);
                            txtSoPhieu.Text = V6BusinessHelper.GetNewSoCt(txtMa_sonb.Text);
                            
                            //Thay the stt_rec new
                            foreach (DataRow dataRow in AD.Rows)
                            {
                                dataRow["STT_REC"] = _sttRec;
                            }
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + txtSoPhieu.Text);
                            Mode = V6Mode.Add;
                            detail1.MODE = V6Mode.View;
                            if (chkSuaThue.Checked)
                                detail2.MODE = V6Mode.View;
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

        private void In()
        {
            try
            {
                if(IsViewingAnInvoice)
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
                    c.MA_NT =  _maNt;
                    c.Dock = DockStyle.Fill;
                    c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                    {
                        if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec);
                        if (!sender.IsDisposed) sender.Dispose();
                    };
                    c.ShowToForm(this, V6Text.PrintSOA, true);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".In: " + ex.Message);
            }
        }

        private TimPhieuChiForm _timForm;
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
                        _timForm = new TimPhieuChiForm(this, _orderList3);
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
            SetData(null);
            detail1.SetData(null);
            detail2.SetData(null);
            detail3.SetData(null);

            LoadAD("");
            SetGridViewData();
                
            ResetAllVars();
            EnableFormControls();
            SetFormDefaultValues();
            btnMoi.Focus();
        }

        private void ResetAllVars()
        {
            _sttRec = "";
        }

        private void SetFormDefaultValues()
        {
            //chkLoaiChietKhau.Checked = true;//loai ck chung
            cboKieuPost.SelectedIndex = 1;
            switch (Mode)
            {
                case V6Mode.Init:
                case V6Mode.View:
                    //chkSuaPtck.Enabled = false;
                    //chkSuaPtck.Checked = false;
                    //txtPtCk.ReadOnly = true;
                    //chkSuaTienCk.Enabled = false;
                    //chkSuaTienCk.Checked = false;
                    //txtTongCkNt.ReadOnly = true;
                    break;
                case V6Mode.Add:
                case V6Mode.Edit:
                    //chkSuaPtck.Enabled = true;
                    //chkSuaPtck.Checked = false;
                    //txtPtCk.ReadOnly = true;
                    //chkSuaTienCk.Enabled = true;
                    //chkSuaTienCk.Checked = false;
                    //txtTongCkNt.ReadOnly = true;
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
                this.ShowErrorMessage(GetType() + ".Huy: " + ex.Message);
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
            //txtSoPhieu.Text = V6BusinessHelper.GetSoCT("M", "", Invoice.Mact, "", V6LoginInfo.UserId);
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

        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                if (_MA_GD == "1")
                {
                    _soCt0.Focus();
                }
                else
                {
                    _tkI.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd" + ex.Message);
            }
        }
        /// <summary>
        /// Khi bấm nhận thêm detail, them kt co soct0 ngayct0 them ad2?
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private bool XuLyThemDetail(SortedDictionary<string,object> dic)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                //var dic = V6ControlFormHelper.GetFormDataDictionary(phieuThuDetail1);
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                dic.Add("STT_REC0", _sttRec0);

                //Thêm thắt vài thứ
                dic["MA_CT"] = Invoice.Mact;
                dic["NGAY_CT"] = dateNgayCT.Value.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (_MA_GD == "1")
                {
                    if (!dic.ContainsKey("SO_CT0") || dic["SO_CT0"].ToString().Trim() == "") error += "\nSố hóa đơn rỗng.";
                }else
                    if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    if (!dic.ContainsKey("TK_I") || dic["TK_I"].ToString().Trim() == "") error += "\nTài khoản rỗng.";
                }else
                if (_MA_GD == "3")
                {
                    if (!dic.ContainsKey("TK_I") || dic["TK_I"].ToString().Trim() == "") error += "\nTài khoản rỗng.";
                }
                
                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD.NewRow();
                    foreach (DataColumn column in AD.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            dic.ContainsKey(key) ? dic[key] : "")??DBNull.Value;
                        newRow[key] = value;
                    }

                    AD.Rows.Add(newRow);
                    dataGridView1.DataSource = AD;
                    TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);

                    if (AD.Rows.Count > 0)
                    {
                        var cIndex = AD.Rows.Count - 1;
                        dataGridView1.Rows[cIndex].Selected = true;
                        dataGridView1.CurrentCell
                            = dataGridView1.Rows[cIndex].Cells[_MA_GD == "1" ? "SO_CT0" : "TK_I"];
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
                if (_gv1EditingRow == null)
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    return false;
                }

                var cIndex = _gv1EditingRow.Index;
                
                if (cIndex >= 0 && cIndex < AD.Rows.Count)
                {
                    //Thêm thắt vài thứ
                    data["MA_CT"] = Invoice.Mact;
                    data["NGAY_CT"] = dateNgayCT.Value.Date;

                    //Kiem tra du lieu truoc khi them sua
                    var error = "";
                    if(_MA_GD == "1")
                        if (!data.ContainsKey("SO_CT0") || data["SO_CT0"].ToString().Trim() == "") error += "\nChưa chọn hóa đơn.";
                    if (_MA_GD == "2" || _MA_GD == "3" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                        if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "") error += "\nChưa nhập tài khoản.";
                    
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
                        this.ShowWarningMessage(V6Text.CheckData + error);
                        return false;
                    }
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
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return;
            }
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var cIndex = dataGridView1.CurrentRow.Index;
                    var currentRow = AD.Rows[cIndex];
                    
                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm)
                        == DialogResult.Yes)
                    {
                        AD.Rows.Remove(currentRow);
                        dataGridView1.DataSource = AD;
                        detail1.SetData(null);
                        TinhTongThanhToan("xu ly xoa detail");
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
        private void btnIn_Click(object sender, EventArgs e)
        {
            In();
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
                if ((_MA_GD == "1" || _MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                    && txtMaKh.Text.Trim() == "")
                {
                    this.ShowWarningMessage("Chưa nhập mã khách hàng!");
                    txtMaKh.Focus();
                    return false;
                }
                if (txtTk.Text.Trim() == "")
                {
                    this.ShowWarningMessage("Chưa nhập tài khoản!");
                    txtTk.Focus();
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

                //Nhập thuế tự động
                if (!NhapThueTuDong())
                {
                    this.ShowWarningMessage("Kiểm tra thuế!");
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
                 if (_tkI.Int_Data("Loai_tk") == 0)
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

        private bool ValidateData_Detail2(SortedDictionary<string, object> data)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ValidateData_Detail2: " + ex.Message);
            }
            return true;
        }

        private void TinhToanTruocKhiLuu()
        {
            V6ControlFormHelper.AddLastAction("\nTinhToanTruocKhiLuu() Begin: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ngay_ct", dateNgayCT.Value.Date),
                    new SqlParameter("@Tk", txtTk.Text),
                    new SqlParameter("@Ma_kh", txtMaKh.Text),
                    new SqlParameter("@Ma_dvcs", txtMadvcs.Text),
                    new SqlParameter("@Stt_rec", _sttRec),
                    new SqlParameter("@Loai_cl", 0),
                    new SqlParameter("@Get_cl", 1),
                    new SqlParameter("@OutputInsert", ""),
                };
                var Acatinhtg = V6BusinessHelper.ExecuteProcedure("Acatinhtg", plist);
                if (Acatinhtg != null && Acatinhtg.Tables.Count > 0 && Acatinhtg.Tables[0].Rows.Count > 0
                    && _maNt != _mMaNt0)
                {
                    var tggs_row = Acatinhtg.Tables[0].Rows[0];
                    var ty_gia = ObjectAndString.ObjectToDecimal(tggs_row["TY_GIA"]);
                    if (ty_gia != 0 && !chkSuaTggs.Checked)
                    {
                        Txtty_gia_ht.Value = ty_gia;
                        foreach (DataRow ad_row in AD.Rows)
                        {
                            if (txtTyGia.Value > ty_gia)
                            {
                                ad_row["TY_GIA_HT2"] = ty_gia;
                                ad_row["TIEN_TT"] =V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(ad_row["TIEN_NT"])*ty_gia,M_ROUND);
                            }
                            else
                            {
                                ad_row["TY_GIA_HT2"] = txtTyGia.Value;
                                ad_row["TIEN_TT"] = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(ad_row["TIEN_NT"]) * ty_gia, M_ROUND);
                            }
                        }
                        dataGridView1.DataSource = AD;
                        //TinhTongValues();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhToanTruocKhiLuu", ex);
            }
            V6ControlFormHelper.AddLastAction("\nTinhToanTruocKhiLuu() End: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }


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

        private void hoaDonDetail1_AddClick(object sender)
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

        private void hoaDonDetail1_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail(data) && XuLySuaDetail(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }
        private void hoaDonDetail1_DeleteClick(object sender)
        {
            XuLyXoaDetail();
        }
        private void phieuThuDetail1_ClickCancelEdit(object sender)
        {
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

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
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (detail3.IsViewOrLock)
                detail3.SetData(dataGridView3.GetCurrentRowData());
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
                XuLyThayDoiMaNt();
            }

            txtTyGia_V6LostFocus(sender);
        }
        
        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSua_Tien);
                foreach (DataRow row in AD.Rows)
                {
                    row["TIEN_TT"] = row["PS_NO"];
                }

                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
        }

        #endregion am events
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //!!!
            var fieldName = e.Column.DataPropertyName.ToUpper();

            if (_alct1Dic.ContainsKey(fieldName))
            {
                var row = _alct1Dic[fieldName];
                var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
                if(fieldName == "PS_NO_NT") fcaption += " "+ cboMaNt.SelectedValue;
                if (fieldName == "TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

                if (fieldName == "PS_NO") fcaption += " " + _mMaNt0;
                if (fieldName == "TIEN") fcaption += " " + _mMaNt0;

                if (!fstatus2) e.Column.Visible = false;

                e.Column.HeaderText = fcaption;
            }
            else if(!(new List<string> {"TEN_TK_I","TK_I"}).Contains(fieldName))
            {
                e.Column.Visible = false;
            }
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

        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {
            SetTabPageText(txtSoPhieu.Text);
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + txtSoPhieu.Text);
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

        private void txtLoaiPhieu_TextChanged(object sender, EventArgs e)
        {
            XuLyKhoaThongTinTheoMaGD();
        }

        private void txtLoaiPhieu_V6LostFocus(object sender)
        {
            XuLyThayDoiLoaiPhieuThu();
        }
        
        private void v6Label3_Click(object sender, EventArgs e)
        {

        }

        private void v6Label1_Click(object sender, EventArgs e)
        {

        }

        private void chkSua_Tien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                _tienNt.Enabled = chkSua_Tien.Checked;
            if (chkSua_Tien.Checked)
            {
                _tienNt.Tag = null;
            }
            else
            {
                _tienNt.Tag = "disable";
            }
        }

        private void phieuThuDetail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                    detail1.ChangeToEditMode();
                    
                    if (_MA_GD == "1")
                    {
                        _soCt0.Focus();
                    }
                    else
                    {
                        _tkI.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".phieuThuDetail1_ClickEdit: " + ex.Message);
            }
            
        }
        
        private void btnChonNhieuHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoaiPhieu.Text == "1")
                {
                    detail1.MODE = V6Mode.View;

                    Invoice.GetSoct0(_sttRec, txtMaKh.Text, txtMadvcs.Text);

                    var initFilter = GetSoCt0InitFilter();
                    var filter_view = new FilterView(Invoice.Alct0, "So_ct", "ARS30", _soCt0, initFilter);
                    filter_view.MultiSeletion = true;
                    filter_view.ChoseEvent += data =>
                    {
                        var dic = detail1.GetData();

                        //_tkI.Text = row.Cells["TK"].Value.ToString().Trim();
                        //_tTtNt0.Value = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_NT0"].Value);
                        //_tTtQd.Value = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_QD"].Value);
                        //_phaiTtNt.Value = ObjectAndString.ObjectToDecimal(row.Cells["PHAI_TT_NT"].Value);
                        //_maNtI.Text = row.Cells["MA_NT"].Value.ToString().Trim();
                        //_sttRecTt.Text = row.Cells["STT_REC"].Value.ToString().Trim();
                        //_ngayCt0.Value = ObjectAndString.ObjectToDate(row.Cells["NGAY_CT"].Value);
                        //_soSeri0.Text = row.Cells["SO_SERI"].Value.ToString().Trim();

                        var ngay_ct0 = ObjectAndString.ObjectToDate(data.Cells["NGAY_CT0"].Value);
                        if (ngay_ct0 == null) this.ShowWarningMessage("Ngày chứng từ trống.");


                        dic["SO_CT0"] = data.Cells["SO_CT"].Value;
                        dic["TK_I"] = data.Cells["TK"].Value;
                        dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                        dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                        dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                        dic["TIEN"] = data.Cells["CL_TT"].Value;
                        dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                        dic["TIEN_TT"] = data.Cells["CL_TT"].Value;
                        dic["TT_QD"] = data.Cells["CL_TT"].Value;
                        dic["PS_NO"] = data.Cells["CL_TT"].Value;
                        dic["PS_NO_NT"] = data.Cells["CL_TT"].Value;

                        dic["MA_NT_I"] = data.Cells["MA_NT"].Value;
                        dic["STT_REC_TT"] = data.Cells["STT_REC"].Value;
                        dic["NGAY_CT0"] = ngay_ct0;
                        dic["SO_SERI0"] = data.Cells["SO_SERI"].Value;

                        
                        var ty_gia_ht2_Value = ObjectAndString.ObjectToDecimal(data.Cells["ty_gia"]);
                        dic["TY_GIA_HT2"] = ty_gia_ht2_Value;

                        if (dic["MA_NT_I"].ToString().Trim() != _mMaNt0)
                        {
                            var tientt_Value = V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(dic["PHAI_TT_NT"]) * ty_gia_ht2_Value, M_ROUND);
                            dic["TIEN_TT"] = tientt_Value;
                            dic["TIEN"] = tientt_Value;
                            dic["PS_NO"] = tientt_Value;
                        }
                        //{Tuanmh 21/08/2016
                        if (Txtdien_giai.Text != "") 
                        {
                            dic["DIEN_GIAII"] = Txtdien_giai.Text.Trim() + " số " + data.Cells["SO_CT0"].Value.ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC(ngay_ct0);
                        }
                        else
                        {
                            dic["DIEN_GIAII"] = " Chi tiền theo CT số " + data.Cells["SO_CT0"].Value.ToString().Trim() + ", ngày " + V6BusinessHelper.V6_DTOC(ngay_ct0);
                        }
                        //}

                        XuLyThemDetail(dic);
                    };

                    if (filter_view.ViewData.Count > 0)
                    {
                        filter_view.ShowDialog(this);
                    }
                    else
                    {
                        ShowParentMessage("Alct0_ARS30 " + V6Text.NoData);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChonNhieuHoaDon: " + ex.Message);
            }
        }

        private void btnViewInfoData_Click(object sender, EventArgs e)
        {
            ShowViewInfoData(Invoice);
        }

        private void txtMa_sonb_V6LostFocus(object sender)
        {
            GetSoPhieu();
        }

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

                        txtDiaChi.ReadOnlyTag();
                        txtDiaChi.TabStop = false;
                        txtTenKh.ReadOnlyTag();
                        txtTenKh.TabStop = false;
                    }
                    else
                    {
                        txtTenKh.Enabled = false;
                        txtDiaChi.Enabled = true;
                        //txtMaSoThue.Enabled = true;

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
                    //txtMaSoThue.Enabled = true;

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

        private void XuLyChonTaiKhoan()
        {
            try
            {
                XuLyThongTinTaiKhoan();

                var data = txtTk.Data;
                if (data != null)
                {
                    TxtTen_tk.Text = V6Setting.Language.Trim() == "V"
                        ? (data["ten_tk"] ?? "").ToString().Trim()
                        : (data["ten_tk2"] ?? "").ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonTaiKhoan: " + ex.Message);
            }
        }

        private void txtTk_V6LostFocus(object sender)
        {
            XuLyChonTaiKhoan();
        }
        private void XuLyThongTinTaiKhoan()
        {
            try
            {
                TxtTen_tk.Enabled = false;


            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyThongTinTaiKhoan: " + ex.Message);
            }
        }


        private void txtMadvcs_V6LostFocus(object sender)
        {
            XuLyChonDonViCoso();
        }
        private void XuLyChonDonViCoso()
        {
            try
            {
                XuLyThongDonViCoSo();

                var data = txtMadvcs.Data;

                txtTenDVCS.Text = V6Setting.Language.Trim() == "V" ? (data["ten_dvcs"] ?? "").ToString().Trim() : (data["ten_dvcs2"] ?? "").ToString().Trim();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonDonViCoso: " + ex.Message);
            }
        }
        private void XuLyThongDonViCoSo()
        {
            try
            {
                txtTenDVCS.Enabled = false;


            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyThongDonViCoSo: " + ex.Message);
            }
        }
        private void XuLyChonMaKhachT()
        {
            try
            {
                if (_ma_kh_t.Text != "")
                {
                    var data = _ma_kh_t.Data;
                    if (data != null)
                    {
                        if(_ten_kh_t!=null)
                        _ten_kh_t.Text = (data["TEN_KH"] ?? "").ToString().Trim();
                        if (_dia_chi_t != null)
                        _dia_chi_t.Text = (data["DIA_CHI"] ?? "").ToString().Trim();
                        if (_mst_t != null)
                        _mst_t.Text = (data["MA_SO_THUE"] ?? "").ToString().Trim();
                        
                        bool enable_infor = true;
                        if (_mst_t != null &&   _mst_t.Text !="")
                        {
                            enable_infor = false;
                        }
                        if (_mst_t != null)
                        _mst_t.Enabled = enable_infor;
                        if (_ten_kh_t != null)
                        _ten_kh_t.Enabled = enable_infor;
                        if (_dia_chi_t != null)
                        _dia_chi_t.Enabled = enable_infor;

                    }
                }
                else
                {
                    if (_ten_kh_t != null)
                    _ten_kh_t.Enabled = true;
                    if (_dia_chi_t != null)
                    _dia_chi_t.Enabled = true;
                    if (_mst_t != null) 
                    _mst_t.Enabled = true;
                    if (_ten_kh_t != null) _ten_kh_t.Focus();
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyChonMaKhach2: " + ex.Message);
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
                this.ShowErrorMessage(GetType() + ".XuLyChonMaKhach2 " + ex.Message);
            }
        }

        private void TinhTienThue()
        {
            try
            {
                _thue_nt.Value = V6BusinessHelper.Vround(_tienNt.Value * _thue_suat.Value / 100, M_ROUND_NT);
                _thue.Value = V6BusinessHelper.Vround(_thue_nt.Value * txtTyGia.Value, M_ROUND);
                if (_maNt == _mMaNt0)
                {
                    _tien.Enabled = false;
                    _thue.Enabled = false;

                    _tien.Value = _tienNt.Value;
                    _thue.Value = _thue_nt.Value;
                }
                else
                {
                    _tien.Enabled = true;
                    _thue.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhTienThue: " + ex.Message);
            }
        }

        private void TinhTienThue22()
        {
            try
            {
                _t_tien22.Value = V6BusinessHelper.Vround(_t_tien_nt22.Value * txtTyGia.Value, M_ROUND);
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
                this.ShowErrorMessage(GetType() + ".TinhTienThue22: " + ex.Message);
            }
        }

        private void detail2_ClickAdd(object sender)
        {
            if (chkSuaThue.Checked)
            {
                XuLyDetail2ClickAdd(sender);
            }
        }

        private void XuLyDetail2ClickAdd(object sender)
        {
            try
            {
                if (AD2 == null || AD2.Rows.Count == 0)
                {
                    //_so_ct022.Text = txtSoCt0.Text;
                    //_ngay_ct022.Value = txtNgayCt0.Value;
                    //_so_seri022.Text = txtSoSeri0.Text;
                    _ma_kh22.Text = txtMaKh.Text;
                    _ten_kh22.Text = txtTenKh.Text;
                    _dia_chi22.Text = txtDiaChi.Text;
                    _ma_so_thue22.Text = txtMaSoThue.Text;
                    //_t_tien22.Value = txtTongTien0.Value;
                    //_t_tien_nt22.Value = txtTongTienNt0.Value;
                    //_thue_suat22.Value = txtThueSuat.Value;
                    //_tk_thue_no22.Text = txtTkThueNo.Text.Trim();
                    _tk_du22.Text = txtTk.Text.Trim();
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
                this.ShowErrorMessage(GetType() + ".XuLyDetail2ClickAdd: " + ex.Message);
            }
        }

        private bool XuLyThemDetail2(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;//Coi nhu thanh cong de quay ve mode view
            }
            try
            {
                _sttRec02 = V6BusinessHelper.GetNewSttRec0(AD2);
                data.Add("STT_REC0", _sttRec02);

                data["STT_REC"] = _sttRec;
                //Thêm thắt vài thứ
                data["MA_CT"] = Invoice.Mact;
                data["NGAY_CT"] = dateNgayCT.Value.Date;

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
                    TinhTongThanhToan("xu ly them detail2");
                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckData + error);
                    return false;//Loi phat hien, return false
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết2: " + ex.Message);
            }
            return true;
        }

        private bool XuLySuaDetail2(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                        //Thêm thắt vài thứ
                        data["MA_CT"] = Invoice.Mact;
                        data["NGAY_CT"] = dateNgayCT.Value.Date;


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
                            TinhTongThanhToan("xy ly sua detail2");
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
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết2: " + ex.Message);
            }
            return true;
        }

        private void XuLyDeleteDetail2()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết2: " + ex.Message);
            }
        }

        private void detail2_ClickCancelEdit(object sender)
        {
            detail2.SetData(_gv2EditingRow.ToDataDictionary());
        }

        private void detail2_ClickEdit(object sender)
        {
            if(chkSuaThue.Checked)
            try
            {
                if (AD2 != null && AD2.Rows.Count > 0 && dataGridView2.DataSource != null)
                {
                    _sttRec02 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView2, detail2, out _gv2EditingRow);
                    detail2.ChangeToEditMode();
                    
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
                this.ShowErrorMessage(GetType() + ".hoaDonDetail2_ClickEdit: " + ex.Message);
            }
        }

        private void detail2_DeleteHandle(object sender)
        {
            XuLyDeleteDetail2();
        }

        private void detail2_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail2(data) && XuLySuaDetail2(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabThue)
            {
                NhapThueTuDong();
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
        
        private void detail2_AddHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail2(data) && XuLyThemDetail2(data))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (detail2.IsViewOrLock)
                detail2.SetData(dataGridView2.GetCurrentRowData());
        }

        private void chkSuaThue_CheckedChanged(object sender, EventArgs e)
        {
            Detail2ModeByChkSuaThue();
        }

        private void Detail2ModeByChkSuaThue()
        {
            if (!chkSuaThue.Checked)
            {
                detail2.MODE = V6Mode.Lock;
            }
            else
            {
                detail2.MODE = V6Mode.View;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }
        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void txtMaKh_V6LostFocus(object sender)
        {
            XuLyChonMaKhachHang();
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

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView2, dataGridView3);
        }

        

    }
}
