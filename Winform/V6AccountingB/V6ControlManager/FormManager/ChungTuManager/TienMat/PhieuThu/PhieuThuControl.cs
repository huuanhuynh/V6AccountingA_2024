using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuThu.Loc;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuThu
{
    public partial class PhieuThuControl : V6InvoiceControl
    {
        #region ==== Properties and Fields

        public V6Invoice41 Invoice;// = new V6Invoice41();
        private string _MA_GD = "";
        
        #endregion properties and fields

        #region ==== Contructor và Khởi tạo ====
        public PhieuThuControl()
        {
            InitializeComponent();
            MyInit();
        }

        public PhieuThuControl(string maCt, string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            Invoice = string.IsNullOrEmpty(maCt) ? new V6Invoice41() : new V6Invoice41(maCt);

            MyInit();
        }

        /// <summary>
        /// Khởi tạo form chứng từ.
        /// </summary>
        /// <param name="maCt">Mã chứng từ.</param>
        /// <param name="itemId"></param>
        /// <param name="sttRec">Có mã hợp lệ sẽ tải dữ liệu lên để sửa.</param>
        public PhieuThuControl(string maCt, string itemId, string sttRec)
            : base(maCt, itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            Invoice = string.IsNullOrEmpty(maCt) ? new V6Invoice41() : new V6Invoice41(maCt);
            MyInit();
            CallViewInvoice(sttRec, V6Mode.View);
        }

        private void MyInit()
        {
            LoadLanguage();
            LoadTag(Invoice, detail1.Controls);

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
            LoadDetail3Controls();
            LoadAdvanceControls(Invoice.Mact);
            lblNameT.Left = V6ControlFormHelper.GetAllTabTitleWidth(tabControl1) + 12;
            ResetForm();

            _MA_GD = (Invoice.Alct["M_MA_GD"] ?? "2").ToString().Trim().ToUpper();

            
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));
            txtLoaiPhieu.ChangeText(_MA_GD);
            
            LoadAll();
            InvokeFormEvent(FormDynamicEvent.INIT);
            V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
        }
        
        #endregion contructor

        #region ==== Khởi tạo Detail Form ====
        //T_TT_NT0,T_TT_QD, PHAI_TT_NT, MA_NT_I
        private V6ColorTextBox _soCt0, _maNtI, _sttRecTt, _soSeri0,_dien_giaii;
        private V6VvarTextBox _tkI;
        private V6NumberTextBox _t_tt_nt0, _t_tt_qd, _phaiTtNt, _psCo, _pscoNt, _tien, _tienNt,_tientt, _ttqd, _ty_gia_ht2;
        private V6DateTimeColor _ngayCt0;
        DataTable alct1_01, alct1_02, alct1_03, alct1_A;
        private void LoadDetailControls(string MAGD)
        {
            try
            {
                //Thêm các control thiết kế cứng.
                _sttRecTt = V6ControlFormHelper.CreateColorTextBox("STT_REC_TT", "sttRecTt", 10, false,false);
                _soSeri0 = V6ControlFormHelper.CreateColorTextBox("SO_SERI0", "so seri", 10, false,false);

                var _check_f_ps_co_nt = false;
                var _check_f_ps_co = false;
                var _check_f_tien_nt = false;
                var _check_f_tien = false;
                var _check_f_tien_tt = false;



                if (MAGD == "1")
                {
                    detail1.ShowLblName = false;
                }
                else if (MAGD == "A")
                {
                    detail1.ShowLblName = false;
                }
                else if (MAGD == "2")
                {
                    //detail1.ShowLblName = true;
                    detail1.lblName.AccessibleName = "TEN_TK_I";
                }
                else if(MAGD == "3")
                {
                    //detail1.ShowLblName = true;
                    detail1.lblName.AccessibleName = "TEN_TK_I";
                }
                
                //Lấy các control động
                var dynamicControlList = GetDynamicControlsAlct(MAGD);
                dynamicControlList.Add(9999, new AlctControls {DetailControl = _sttRecTt});
                dynamicControlList.Add(9998, new AlctControls {DetailControl = _soSeri0});

                #region ----//Thêm các control động vào danh sách, thêm sự kiện cho control động
                foreach (KeyValuePair<int, AlctControls> item in dynamicControlList)
                {
                    var control = item.Value.DetailControl;
                    ApplyControlEnterStatus(control);

                    var NAME = control.AccessibleName.ToUpper();
                    All_Objects[NAME] = control;
                    V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects);

                    switch (NAME)
                    {
                        case "SO_CT0":
                            _soCt0 = (V6ColorTextBox)control;
                            
                            _soCt0.GotFocus += _soCt0_GotFocus;
                            _soCt0.V6LostFocus += SoCt0_V6LostFocus;
                            _soCt0.V6LostFocusNoChange += SoCt0_V6LostFocusNoChange;
                            _soCt0.KeyDown += _soCt0_KeyDown;
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
                            _ngayCt0.Enabled = false;
                            break;
                        case "PS_CO":
                            _psCo = (V6NumberTextBox)control;
                            _check_f_ps_co = true;
                            break;
                        case "PS_CO_NT":
                            _pscoNt = (V6NumberTextBox)control;
                            _pscoNt.V6LostFocus += _pscoNt_V6LostFocus;
                            _check_f_ps_co_nt = true;
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
                            _ty_gia_ht2 = control as V6NumberTextBox;
                            if (_ty_gia_ht2 != null)
                            {
                                
                            }
                            break;

                    }
                    V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
                }
                #endregion ----//Thêm các control động vào danh sách, thêm sự kiện cho control động

                //Bo sung cac f cung
                if (_check_f_tien_nt == false)
                {
                    _tienNt = V6ControlFormHelper.CreateNumberTienNt("TIEN_NT", "tiennt", M_ROUND_NT,10, false,false);
                    dynamicControlList.Add(9997, new AlctControls {DetailControl = _tienNt});
                }
                if (_check_f_tien == false)
                {
                    _tien = V6ControlFormHelper.CreateNumberTien("TIEN", "tien", M_ROUND, 10, false, false);
                    dynamicControlList.Add(9996, new AlctControls {DetailControl = _tien});
                }
                if (_check_f_ps_co_nt == false)
                {
                    _pscoNt = V6ControlFormHelper.CreateNumberTienNt("PS_CO_NT", "pscont", M_ROUND_NT, 10, false, false);
                    dynamicControlList.Add(9995, new AlctControls {DetailControl = _pscoNt});
                }
                if (_check_f_ps_co == false)
                {
                    _psCo = V6ControlFormHelper.CreateNumberTien("PS_CO", "psco", M_ROUND, 10, false, false);
                    dynamicControlList.Add(9994, new AlctControls {DetailControl = _psCo});
                }
                if (_check_f_tien_tt == false)
                {
                    _tientt = V6ControlFormHelper.CreateNumberTien("TIEN_TT", "tientt", M_ROUND, 10, false, false);
                    dynamicControlList.Add(9993, new AlctControls {DetailControl = _tientt});
                }

                
                detail1.RemoveControls();

                foreach (AlctControls item in dynamicControlList.Values)
                {
                    detail1.AddControl(item);
                }

                detail1.SetStruct(Invoice.ADStruct);
                detail1.MODE = detail1.MODE;
                V6ControlFormHelper.RecaptionDataGridViewColumns(dataGridView1, _alct1Dic, _maNt, _mMaNt0);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                            if(_PsNoNt_33.Value != 0)
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
                    string label = "TK_I";
                    var lbl = detail3.GetControlByName("lblTK_I");
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



        void _ttqd_V6LostFocus(object sender)
        {
            try
            {
                //if (_maNtI.Text == _maNt) _tientt.Value = _ttqd.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void _tienNt_V6LostFocus(object sender)
        {
            try
            {

                _tien.Value = V6BusinessHelper.Vround(_tienNt.Value*txtTyGia.Value, M_ROUND);

                if (_pscoNt != null)
                _pscoNt.Value = _tienNt.Value;
                
                if (cboMaNt.SelectedValue.ToString() == _mMaNt0)
                {
                    _tien.Value = _tienNt.Value;
                    if(_psCo!=null && _pscoNt!=null)
                        _psCo.Value = _pscoNt.Value;
                }
                // Tuanmh 09/02/2016
                if (_tien != null)
                     _tientt.Value = _tien.Value;

                if (_ttqd!=null && _tienNt!=null && _maNtI.Text == _maNt)
                {
                    _ttqd.Value = _tienNt.Value;
                }
            }   
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        void _pscoNt_V6LostFocus(object sender)
        {
            try
            {

                _psCo.Value = V6BusinessHelper.Vround(_pscoNt.Value * txtTyGia.Value, M_ROUND);
                _tien.Value = _psCo.Value;
                if (_tienNt != null)

                _tienNt.Value = _pscoNt.Value;

                if (cboMaNt.SelectedValue.ToString() == _mMaNt0)
                {
                    if (_tien != null && _tienNt!=null)
                    _tien.Value = _tienNt.Value;

                    _psCo.Value = _pscoNt.Value;
                }
                // Tuanmh 09/02/2016
                if (_tien != null)
                     _tientt.Value = _tien.Value;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }


        void _soCt0_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView(Invoice.Alct0, "So_ct", "ARS20", _soCt0, initFilter);
                    f.dataGridView1.Space_Bar = false;
                    f.dataGridView1.Control_A = false;
                    
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
                        ShowParentMessage("Alct0_ARS20 " + V6Text.NoData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
        /// <param name="maGd"></param>
        /// <returns></returns>
        public SortedDictionary<int, AlctControls> GetDynamicControlsAlct(string maGd)
        {
            DataTable alct1;

            _orderList = new List<string>();
            if (maGd == "1")
            {
                if (alct1_01 == null)
                {
                    alct1_01 = Invoice.GetAlct1(maGd);
                }

                alct1 = alct1_01;
            }
            else if (maGd == "A" || maGd == "a")
            {
                if (alct1_A == null)
                {
                    alct1_A = Invoice.GetAlct1(maGd);
                }

                alct1 = alct1_A;
            }
            else if (maGd == "2")
            {
                if (alct1_02 == null)
                {
                    alct1_02 = Invoice.GetAlct1(maGd);
                }

                alct1 = alct1_02;
            }
            else if (maGd == "3")
            {
                if (alct1_03 == null)
                {
                    alct1_03 = Invoice.GetAlct1(maGd);
                }

                alct1 = alct1_03;
            }
            else
            {
                throw new Exception("Chọn lại ma_gd");
            }

            var dynamicControlList = V6ControlFormHelper.GetDynamicControlStructsAlct(alct1, out _orderList, out _alct1Dic);
            if (maGd != "1")
                _orderList.Insert(1, "TEN_TK_I");
            return dynamicControlList;
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
            if(_soCt0.Text.Trim() == "") CheckAlct0();
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
                        {
                            check = true;
                        }

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
                        var f = new FilterView(Invoice.Alct0, "So_ct", "ARS20", _soCt0, initFilter);
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
                            ShowParentMessage("Alct0_ARS20 " + V6Text.NoData);
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
                    this.ShowWarningMessage(GetType() + ".Alct0 null");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
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
                txtDiaChi.Text = (data["dia_chi"] ?? "").ToString().Trim();
                txtTenKh.Text = V6Setting.Language.Trim() == "V" ? (data["ten_kh"] ?? "").ToString().Trim() : (data["ten_kh2"] ?? "").ToString().Trim();

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
                        //txtMaSoThue.Enabled = true;

                        txtDiaChi.ReadOnlyTag(false);
                        txtDiaChi.TabStop = true;
                        txtTenKh.ReadOnlyTag(false);
                        txtTenKh.TabStop = true;
                    }
                }
                else
                {
                  //  txtTenKh.Enabled = true;
                  // txtDiaChi.Enabled = true;
                  //  txtMaSoThue.Enabled = true;

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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void XuLyKhiNhanSoCt(IDictionary<string, object> row)//, DataRow row0)
        {
            try
            {
                _tkI.Text = row["TK"].ToString().Trim();
                _t_tt_nt0.Value = ObjectAndString.ObjectToDecimal(row["TC_TT"]);
                _t_tt_qd.Value = ObjectAndString.ObjectToDecimal(row["T_TT_QD"]);
                _phaiTtNt.Value = ObjectAndString.ObjectToDecimal(row["CL_TT"]);
                _maNtI.Text = row["MA_NT"].ToString().Trim();
                _sttRecTt.Text = row["STT_REC"].ToString().Trim();

                _ngayCt0.Value = ObjectAndString.ObjectToDate(row["NGAY_CT"]);
                _soSeri0.Text = row["SO_SERI"].ToString().Trim();

                _tienNt.Value = _phaiTtNt.Value;
                _tien.Value = _phaiTtNt.Value;
                _ttqd.Value = _phaiTtNt.Value;
                _tientt.Value = _phaiTtNt.Value;

                _pscoNt.Value = _phaiTtNt.Value;
                _psCo.Value = _phaiTtNt.Value;

                _ty_gia_ht2.Value = ObjectAndString.ObjectToDecimal(row["TY_GIA"]);
                if (_maNtI.Text != _mMaNt0)
                {
                    _tientt.Value = V6BusinessHelper.Vround(_phaiTtNt.Value*_ty_gia_ht2.Value, M_ROUND);
                    _tien.Value = V6BusinessHelper.Vround(_phaiTtNt.Value * txtTyGia.Value, M_ROUND);
                    _psCo.Value = _tien.Value;
                }

                //{Tuanmh 21/08/2016
               
                if (Txtdien_giai.Text != "")
                {
                    _dien_giaii.Text = Txtdien_giai.Text.Trim() + " số " + row["SO_CT"].ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)row["NGAY_CT"]);
                }
                else
                {
                    _dien_giaii.Text = " Thu tiền bán hàng theo CT số" + row["SO_CT"].ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)row["NGAY_CT"]);
                }
                //}
                
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

                    ThuCongNo.Enabled = false;
                    TroGiupMenu.Enabled = false;
                    chonTuExcelToolStripMenuItem.Enabled = false;
                }
                else //Cac truong hop khac
                {
                    ThuCongNo.Enabled = true;
                    TroGiupMenu.Enabled = true;
                    chonTuExcelToolStripMenuItem.Enabled = true;

                    XuLyKhoaThongTinKhachHang();
                    txtTyGia.Enabled = _maNt != _mMaNt0;
                    //chkSuaPtck.Enabled = chkLoaiChietKhau.Checked;
                    //chkSuaTienCk.Enabled = chkLoaiChietKhau.Checked;

                    //txtPtCk.ReadOnly = !chkSuaPtck.Checked;
                    //txtTongCkNt.ReadOnly = !chkSuaTienCk.Checked;
                    dateNgayLCT.Enabled = Invoice.M_NGAY_CT;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }

            SetControlReadOnlyHide(this, Invoice, Mode);
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
        
        public void TinhTongValues()
        {
            var tTienNt = V6BusinessHelper.TinhTong(AD, "TIEN_NT");
            var tPsNoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_NO_NT", "OPER_TT");
            var tPsCoNt = V6BusinessHelper.TinhTongOper(AD3, "PS_CO_NT", "OPER_TT");
            txtTongTangGiamNt.Value = tPsNoNt;
            txtTongThanhToanNt.Value = V6BusinessHelper.Vround(tTienNt + tPsNoNt, M_ROUND_NT);

            var tTien = V6BusinessHelper.TinhTong(AD, "TIEN");
            var tPsNo = V6BusinessHelper.TinhTongOper(AD3, "PS_NO", "OPER_TT");
            var tPsCo = V6BusinessHelper.TinhTongOper(AD3, "PS_CO", "OPER_TT");
            txtTongTangGiam.Value = tPsNo;
            txtTongThanhToan.Value = V6BusinessHelper.Vround(tTien + tPsNo, M_ROUND);
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _sttRec, "TTTT(" + debug + ")"), ex);
            }
        }

        private void txtTyGia_V6LostFocus(object sender)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
            {
                XuLyThayDoiTyGia(txtTyGia, chkSuaTien);
                foreach (DataRow row in AD.Rows)
                {
                    row["TIEN_TT"] = row["PS_CO"];
                }
                
                TinhTongThanhToan("TyGia_V6LostFocus " + ((Control)sender).AccessibleName);
            }
        }


        #endregion tính toán hóa đơn

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
            txtTk.Text = Invoice.Alct["TK_NO"].ToString().Trim();
            cboKieuPost.SelectedValue = Invoice.Alct["M_K_POST"].ToString().Trim();

        }


        

        /// <summary>
        /// Ẩn hiện các cột của GridView
        /// </summary>
        private void XuLyThayDoiLoaiPhieuThu()
        {
            _MA_GD = txtLoaiPhieu.Text.Trim().ToUpper();
            
            try
            {
                //Loại 1
                if (_MA_GD == "1")
                {
                    LoadDetailControls(_MA_GD);
                    
                    var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = true;
                        dataGridViewColumn.DisplayIndex = 0;
                    }
                    var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.Visible = false;
                    }
                }
                if (_MA_GD == "A")
                {
                    LoadDetailControls("A");

                    var dataGridViewColumn = dataGridView1.Columns["TK_I"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = true;
                        dataGridViewColumn.DisplayIndex = 0;
                    }
                    var gridViewColumn = dataGridView1.Columns["TEN_TK_I"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.Visible = false;
                    }
                }
                //Loại 2
                if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    // Tuanmh 25/08/2017 su dung chung ma_gd="2"
                    //LoadDetailControls(_MA_GD);
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
                    LoadDetailControls(_MA_GD);
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

                FormatGridView();

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
                            column.Visible = true;
                            column.Width = 200;
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
                        = dataGridView1.Rows[0].Cells[(_MA_GD == "1" || _MA_GD == "A") ? "SO_CT0" : "TK_I"];
                    
                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                this.WriteExLog(GetType() + ".txtLoaiPhieu_TextChanged " + _sttRec, ex);
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
                
                var newText = (V6Setting.IsVietnamese ? "Ps có " : "Amount ") + _maNt;
                _pscoNt.GrayText = newText;
                var viewColumn = dataGridView1.Columns["PS_CO_NT"];
                if (viewColumn != null) viewColumn.HeaderText = newText;

                newText = (V6Setting.IsVietnamese ? "Thanh toán " : "Amount ") + _maNt;
                _tienNt.GrayText = newText;
                var column = dataGridView1.Columns["TIEN_NT"];
                if (column != null) column.HeaderText = newText;

                newText = (V6Setting.IsVietnamese ? "Ps có " : "Amount ") + _mMaNt0;
                _psCo.GrayText = newText;
                viewColumn = dataGridView1.Columns["PS_CO"];
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

                    if (_MA_GD == "1")
                        detail1.ShowIDs(new[] {"lblGIA21", "lblTIEN2"});
                    if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                        detail1.ShowIDs(new[] { "PS_CO", "lblGIA21", "lblTIEN2" });
                    if (_MA_GD == "3")
                        detail1.ShowIDs(new[] { "PS_CO", "lblGIA21", "lblTIEN2" });

                    var c = V6ControlFormHelper.GetControlByAccessibleName(detail1, "PS_CO");
                    if (c != null) c.Visible = true;
                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], true); //Hien ra
                    var dataGridViewColumn = dataGridView1.Columns["PS_CO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = true;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = true;

                    panelVND.Visible = true;

                    //Detail3
                    detail3.ShowIDs(new[] {"PS_NO", "PS_CO"});

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

                    detail1.HideIDs(new[] {"PS_CO", "lblGIA21", "TIEN", "lblTIEN2"});

                    //SetColsVisible(_GridID, ["GIA21", "TIEN2"], false); //An di
                    var dataGridViewColumn = dataGridView1.Columns["PS_CO"];
                    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                    var gridViewColumn = dataGridView1.Columns["TIEN"];
                    if (gridViewColumn != null) gridViewColumn.Visible = false;



                    //Detail3
                    detail3.HideIDs(new[] {"PS_NO", "PS_CO"});

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
                _pscoNt.DecimalPlaces = decimalPlaces;

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

        private void SetGridViewData()
        {
            HienThiTongSoDong(lblTongSoDong);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = AD;
            dataGridView3.DataSource = AD3;
            
            //ReorderDataGridViewColumns();
            FormatGridView();
        }

        private void ReorderDataGridViewColumns()
        {
            if (_MA_GD == "1" || _MA_GD == "A")
            {
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

            //V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, _orderList, i);
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView3, _orderList3);

            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Invoice.GRDS_AD, Invoice.GRDF_AD,
                V6Setting.IsVietnamese ? Invoice.GRDHV_AD : Invoice.GRDHE_AD);
            V6ControlFormHelper.FormatGridViewHideColumns(dataGridView1, Invoice.Mact);

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
        }

        private void FormatGridView()
        {
            //GridView1 !!!!!

            //GridView3
            var f = dataGridView3.Columns["so_luong"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            f = dataGridView3.Columns["SO_LUONG1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
            }
            f = dataGridView1.Columns["HE_SO1"];
            if (f != null)
            {
                f.DefaultCellStyle.Format = "N6";
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
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView3, _orderList3);
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
        /// Cần set trước AD81 cho đúng với index
        /// </summary>
        private void ViewInvoice()
        {
            try
            {
                Mode = V6Mode.View;
                V6ControlFormHelper.SetFormDataRow(this, AM.Rows[CurrentIndex]);

                //txtMadvcs.ExistRowInTable();
                if (V6Setting.Language.Trim() == "V")
                {
                    if (txtMadvcs.Data != null && txtMadvcs.Data.Table.Columns.Contains(txtTenDVCS.AccessibleName))
                        txtTenDVCS.Text = txtMadvcs.Data[txtTenDVCS.AccessibleName].ToString().Trim();
                    //txtMaKh.ExistRowInTable();
                    if (txtMaKh.Data != null && txtMaKh.Data.Table.Columns.Contains(txtTenKh.AccessibleName))
                        txtTenKh.Text = txtMaKh.Data[txtTenKh.AccessibleName].ToString().Trim();


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
                ViewLblKieuPost(lblKieuPostColor, cboKieuPost);

                SetGridViewData();
                XuLyThayDoiMaNt();
                XuLyThayDoiLoaiPhieuThu();
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
        private void DoAdd()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;

                if (Invoice.InsertInvoice(addDataAM, addDataAD, addDataAD3))
                {
                    flagAddSuccess = true;
                    Mode = V6Mode.View;
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
        private void DoAddThread()
        {
            try
            {
                ReadyForAdd();
                Timer checkAdd = new Timer();
                checkAdd.Interval = 500;
                checkAdd.Tick += checkAdd_Tick;
                InvokeFormEvent(FormDynamicEvent.BEFOREADD);
                InvokeFormEvent(FormDynamicEvent.BEFORESAVE);
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
        private bool flagAddFinish, flagAddSuccess;
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
                    //All_Objects["MA_NX"] = txtManx.Text;
                    //All_Objects["LOAI_CK"] = chkLoaiChietKhau.Checked ? "1" : "0";
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
        private IDictionary<string, object> addDataAM;
        private List<IDictionary<string, object>> addDataAD, addDataAD3;
        private string addErrorMessage = "";
#endregion add
        
        #region ==== Edit Thread ====
        private bool flagEditFinish, flagEditSuccess;
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
                    Invoice.PostErrorLog(_sttRec, "X", "Invoice41.DeleteInvoice return false." + Invoice.V6Message);
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
                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
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
                    txtLoaiPhieu.ChangeText(_MA_GD);

                    GetSttRec(Invoice.Mact);
                    V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
                   // GetSoPhieu();

                    GetM_ma_nt0();
                    GetTyGiaDefault();
                    GetDefault_Other();
                    SetDefaultData(Invoice);
                    XuLyKhoaThongTinTheoMaGD();
                    XuLyThayDoiLoaiPhieuThu();
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
                            //Tuanmh 24/07/2016 Check Debit Amount
                            bool check_edit = 
                                CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                    txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
                                    txtTk.Text, dateNgayCT.Date, txtTongThanhToan.Value, "E");

                            if (check_edit == true)
                            {
                                Mode = V6Mode.Edit;
                                detail1.MODE = V6Mode.View;
                                detail3.MODE = V6Mode.View;
                                txtMa_sonb.Focus();
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
                if (V6Login.UserRight.AllowDelete("", Invoice.CodeMact))
                {
                    var row = AM.Rows[CurrentIndex];
                    // Tuanmh 16/02/2016 Check level
                    if (V6Rights.CheckLevel(V6Login.Level, Convert.ToInt32(row["User_id2"]),
                        (row["Xtag"] ?? "").ToString().Trim()))
                    {
                        //Tuanmh 24/07/2016 Check Debit Amount
                        bool check_edit =
                            CheckEditAll(Invoice, cboKieuPost.SelectedValue.ToString().Trim(), cboKieuPost.SelectedValue.ToString().Trim(),
                                txtSoPhieu.Text.Trim(), txtMa_sonb.Text.Trim(), txtMadvcs.Text.Trim(), txtMaKh.Text.Trim(),
                                txtTk.Text, dateNgayCT.Date, txtTongThanhToan.Value, "D");

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
                            this.ShowWarningMessage("Chưa chọn phiếu thu.");
                        }
                        else
                        {
                            GetSttRec(Invoice.Mact);
                            SetNewValues();
                            V6ControlFormHelper.AddRunningList(_sttRec, Invoice.Name + " " + txtSoPhieu.Text);
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

        private void In0()
        {
            try
            {
                if (!IsViewingAnInvoice) return;
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
                    c.MA_NT =  _maNt;
                    c.Dock = DockStyle.Fill;
                    c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                    {
                        if (hoadon_nd51 == 1) Invoice.IncreaseSl_inAM(stt_rec, AM_current);
                        if (!sender.IsDisposed) sender.Dispose();
                    };
                    c.ShowToForm(this, Invoice.Mact == "TA1" ? V6Text.PrintTA1 : V6Text.PrintBC1, true);
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

        private TimPhieuThuForm _timForm;
        private void Xem()
        {
            try
            {
                if (IsHaveInvoice)
                {
                    if (_timForm == null) _timForm = new TimPhieuThuForm(this);
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
                        _timForm = new TimPhieuThuForm(this);
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
            SetData(null);
            detail1.SetData(null);
            //detail2.SetData(null);
            detail3.SetData(null);

            LoadAD("");
            SetGridViewData();
            XuLyThayDoiLoaiPhieuThu();
            ResetAllVars();
            EnableVisibleControls();
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
                SetControlReadOnlyHide(detail1, Invoice, V6Mode.Add);
                if (_MA_GD == "1" || _MA_GD == "A")
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private bool XuLyThemDetail(IDictionary<string,object> dic)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                //var dic = V6ControlFormHelper.GetFormDataDictionary(phieuThuDetail1);
                var rec0 = V6BusinessHelper.GetNewSttRec0(AD);
                dic.Add("STT_REC0", rec0);

                //Thêm thông tin...
                dic["MA_CT"] = Invoice.Mact;
                dic["NGAY_CT"] = dateNgayCT.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (_MA_GD == "1")
                {
                    if (!dic.ContainsKey("SO_CT0") || dic["SO_CT0"].ToString().Trim() == "") error += "\nSố hóa đơn rỗng.";
                }
                else if (_MA_GD == "A")
                {
                    if (!dic.ContainsKey("SO_CT0") || dic["SO_CT0"].ToString().Trim() == "") error += "\nSố hóa đơn rỗng.";
                }
                else if (_MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                {
                    if (!dic.ContainsKey("TK_I") || dic["TK_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00379") + " " + V6Text.Empty;
                }
                else if (_MA_GD == "3")
                {
                    if (!dic.ContainsKey("TK_I") || dic["TK_I"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00379") + " " + V6Text.Empty;
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
                    
                    if (AD.Rows.Count > 0)
                    {
                        var cIndex = AD.Rows.Count - 1;
                        dataGridView1.Rows[cIndex].Selected = true;
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, _MA_GD == "1" || _MA_GD == "A" ? "SO_CT0" : "TK_I");
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
        
        private bool XuLySuaDetail(IDictionary<string, object> data)
        {
            if (NotAddEdit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv1EditingRow == null)
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                    return false;
                }

                var cIndex = _gv1EditingRow.Index;
                
                if (cIndex >= 0 && cIndex < AD.Rows.Count)
                {
                    //Thêm thông tin...
                    data["MA_CT"] = Invoice.Mact;
                    data["NGAY_CT"] = dateNgayCT.Date;

                    //Kiem tra du lieu truoc khi them sua
                    var error = "";
                    if(_MA_GD == "1")
                        if (!data.ContainsKey("SO_CT0") || data["SO_CT0"].ToString().Trim() == "")
                        {
                            string label = "SO_CT0";
                            var lbl = detail1.GetControlByName("lbl" + label);
                            if (lbl != null) label = lbl.Text;
                            error += V6Text.NoInput + " [" + label + "]\n";
                        }
                    if (_MA_GD == "A")
                        if (!data.ContainsKey("SO_CT0") || data["SO_CT0"].ToString().Trim() == "")
                        {
                            string label = "SO_CT0";
                            var lbl = detail1.GetControlByName("lbl" + label);
                            if (lbl != null) label = lbl.Text;
                            error += V6Text.NoInput + " [" + label + "]\n";
                        }
                    if (_MA_GD == "2" || _MA_GD == "3" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9")
                        if (!data.ContainsKey("TK_I") || data["TK_I"].ToString().Trim() == "")
                        {
                            string label = "TK_I";
                            var lbl = detail1.GetControlByName("lblTK_I");
                            if (lbl != null) label = lbl.Text;
                            error += V6Text.NoInput + " [" + label + "]\n";
                        }
                    
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
        private void btnIn_Click(object sender, EventArgs e)
        {
            BasePrint(Invoice, _sttRec, V6PrintMode.DoNoThing, txtTongThanhToan.Value, txtTongThanhToanNt.Value, false);
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
                    this.ShowWarningMessage(V6Text.NoInput + lblMaDVCS.Text);
                    txtMadvcs.Focus();
                    return false;
                }
                if ((_MA_GD == "1" || _MA_GD == "2" || _MA_GD == "4" || _MA_GD == "5" || _MA_GD == "6" || _MA_GD == "7" || _MA_GD == "8" || _MA_GD == "9") 
                    && txtMaKh.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblMaKH.Text);
                    txtMaKh.Focus();
                    return false;
                }
                if (txtTk.Text.Trim() == "")
                {
                    this.ShowWarningMessage(V6Text.NoInput + lblTK.Text);
                    txtTk.Focus();
                    return false;
                }
                if (txtTk.Int_Data("Loai_tk") == 0)
                {
                    this.ShowWarningMessage(V6Text.Text("TKNOTCT"));
                    txtTk.Focus();
                    return false;
                }
                if (cboKieuPost.SelectedIndex == -1)
                {
                    this.ShowWarningMessage(V6Text.Text("CHUACHONKIEUPOST"));
                    cboKieuPost.Focus();
                    return false;
                }

                ValidateMasterData(Invoice);


                // Check Detail
                if (AD.Rows.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoInputDetail);
                    return false;
                }

                //Check nh_dk ps_no = ps_co
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

                //Tuanmh 16/02/2016 Check Voucher Is exist 
                {
                    DataTable DataCheckVC = Invoice.GetCheck_VC_Save(
                        cboKieuPost.SelectedValue.ToString().Trim(),
                        cboKieuPost.SelectedValue.ToString().Trim(),
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
                        txtTk.Text.Trim(), dateNgayCT.Date, txtMa_ct.Text, txtTongThanhToan.Value, mode_vc, V6Login.UserId);



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
            }
            return false;

        }

        private bool ValidateData_Detail(IDictionary<string, object> data)
        {
            try
             {
                 if (_tkI.Int_Data("Loai_tk") == 0)
                 {
                     this.ShowWarningMessage(V6Text.Text("TKNOTCT"));
                     return false;
                 }

                 string firstErrorField;
                 string errors = ValidateDetailData(detail1, Invoice, data, out firstErrorField);
                 if (!string.IsNullOrEmpty(errors))
                 {
                     this.ShowWarningMessage(errors);
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
        private void hoaDonDetail1_AddHandle(IDictionary<string,object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }

        private void hoaDonDetail1_EditHandle(IDictionary<string, object> data)
        {
            dataGridView1.UnLock();
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        private void hoaDonDetail1_DeleteClick(object sender)
        {
            XuLyXoaDetail();
        }
        private void phieuThuDetail1_ClickCancelEdit(object sender)
        {
            dataGridView1.UnLock();
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

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
                FormatGridView();
                XuLyThayDoiMaNt();
            }

            txtTyGia_V6LostFocus(sender);
        }
        
        #endregion am events
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //var FIELD = e.Column.DataPropertyName.ToUpper();

            //if (_alct1Dic.ContainsKey(FIELD))
            //{
            //    var row = _alct1Dic[FIELD];
            //    var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
            //    var fcaption = row[V6Setting.IsVietnamese ? "caption" : "caption2"].ToString().Trim();
            //    if(FIELD == "PS_CO_NT") fcaption += " "+ cboMaNt.SelectedValue;
            //    if (FIELD == "TIEN_NT") fcaption += " " + cboMaNt.SelectedValue;

            //    if (FIELD == "PS_CO") fcaption += " " + _mMaNt0;
            //    if (FIELD == "TIEN") fcaption += " " + _mMaNt0;

            //    if (!fstatus2) e.Column.Visible = false;

            //    e.Column.HeaderText = fcaption;
            //}
            //else if(!(new List<string> {"TEN_TK_I","TK_I"}).Contains(FIELD))
            //{
            //    e.Column.Visible = false;
            //}
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

        private void chkSuaTien_CheckedChanged(object sender, EventArgs e)
        {
            if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                _tienNt.Enabled = chkSuaTien.Checked;
            if (chkSuaTien.Checked)
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
                    if (_gv1EditingRow == null)
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                        return;
                    }
                    dataGridView1.Lock();
                    detail1.ChangeToEditMode();
                    SetControlReadOnlyHide(detail1, Invoice, V6Mode.Edit);

                    if (_MA_GD == "1" || _MA_GD == "A")
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
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
            
        }
        
        private void btnChonNhieuHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MA_GD == "1")
                {
                    detail1.MODE = V6Mode.View;

                    Invoice.GetSoct0(_sttRec, txtMaKh.Text, txtMadvcs.Text);

                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView(Invoice.Alct0, "So_ct", "ARS20", _soCt0, initFilter);
                    f.MultiSeletion = true;
                    f.ChoseEvent += data =>
                    {
                        var dic = detail1.GetData();
                        
                        dic["SO_CT0"] = data.Cells["SO_CT"].Value;
                        dic["TK_I"] = data.Cells["TK"].Value;
                        dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                        dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                        dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                        dic["TIEN"] = data.Cells["CL_TT"].Value;
                        dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                        dic["TIEN_TT"] = data.Cells["CL_TT"].Value;

                        dic["TT_QD"] = data.Cells["CL_TT"].Value;
                        dic["PS_CO"] = data.Cells["CL_TT"].Value;
                        dic["PS_CO_NT"] = data.Cells["CL_TT"].Value;

                        dic["MA_NT_I"] = data.Cells["MA_NT"].Value;
                        dic["STT_REC_TT"] = data.Cells["STT_REC"].Value;
                        dic["NGAY_CT0"] = data.Cells["NGAY_CT"].Value;
                        dic["SO_SERI0"] = data.Cells["SO_SERI"].Value;

                        var ty_gia_ht2_Value = ObjectAndString.ObjectToDecimal(data.Cells["ty_gia"]);
                        dic["TY_GIA_HT2"] = ty_gia_ht2_Value;
                        if (dic["MA_NT_I"].ToString().Trim() != _mMaNt0)
                        {
                            var tientt_Value = V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(dic["PHAI_TT_NT"]) * ty_gia_ht2_Value, M_ROUND);
                            dic["TIEN_TT"] = tientt_Value;
                            dic["TIEN"] = tientt_Value;
                            dic["PS_CO"] = tientt_Value;
                        }

                        //{Tuanmh 21/08/2016
                        if (Txtdien_giai.Text != "")
                        {
                            dic["DIEN_GIAII"] = Txtdien_giai.Text.Trim() + " số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        else
                        {
                            dic["DIEN_GIAII"] = " Thu tiền bán hàng theo CT số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        //}
                         

                        XuLyThemDetail(dic);
                    };

                    if (f.ViewData.Count > 0)
                    {
                        f.ShowDialog(this);
                    }
                    else
                    {
                        ShowParentMessage("Alct0_ARS20 " + V6Text.NoData);
                    }
                }
                else if (_MA_GD == "A")
                {
                    detail1.MODE = V6Mode.View;
                    

                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView_ARS20(Invoice, _soCt0, _sttRec, txtMadvcs.Text, initFilter);
                    f.MultiSeletion = true;
                    f.ChoseEvent += data =>
                    {
                        var dic = detail1.GetData();
                        
                        dic["SO_CT0"] = data.Cells["SO_CT"].Value;
                        dic["TK_I"] = data.Cells["TK"].Value;
                        if (data.Cells["MA_NT"].ToString().Trim() == _mMaNt0)
                        {
                            dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                            dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                            dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_TT"] = data.Cells["CL_TT"].Value;
                            dic["TT_QD"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO_NT"] = data.Cells["CL_TT"].Value;
                        }
                        else
                        {
                            dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                            dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                            dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_TT"] = data.Cells["CL_TT"].Value;
                            dic["TT_QD"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO_NT"] = data.Cells["CL_TT"].Value;
                        }
                        

                        dic["MA_NT_I"] = data.Cells["MA_NT"].Value;
                        dic["STT_REC_TT"] = data.Cells["STT_REC"].Value;
                        dic["NGAY_CT0"] = data.Cells["NGAY_CT"].Value;
                        dic["SO_SERI0"] = data.Cells["SO_SERI"].Value;

                        dic["MA_KH_I"] = data.Cells["MA_KH"].Value;
                        dic["TEN_KH_I"] = data.Cells["TEN_KH"].Value;

                        //{Tuanmh 21/08/2016
                        if (Txtdien_giai.Text != "")
                        {
                            dic["DIEN_GIAII"] = Txtdien_giai.Text.Trim() + " số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        else
                        {
                            dic["DIEN_GIAII"] = " Thu tiền bán hàng theo CT số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        //}

                        XuLyThemDetail(dic);
                    };
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
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
            else if (tabControl1.SelectedTab == tabChiTietBoSung)
            {
                detail3.AutoFocus();
            }
        }

        private void detail1_LabelNameTextChanged(object sender, EventArgs e)
        {
            lblNameT.Text = ((Label)sender).Text;
        }

        private void btnChonNhieuHD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
            }
        }

        private void txtTongThanhToanNt_TextChanged(object sender, EventArgs e)
        {
            ChungTu.ViewMoney(lblDocSoTien, txtTongThanhToanNt.Value, _maNt);
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            FixDataGridViewSize(dataGridView1, dataGridView3);
        }

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
            
        }

        private void ChucNang_ThuCongNo()
        {
            try
            {
                if (NotAddEdit) return;
                if (_MA_GD == "3")
                {
                    detail1.MODE = V6Mode.View;


                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView_ARSODU0(Invoice, new V6ColorTextBox(), _sttRec, txtMadvcs.Text, initFilter);
                    f.MultiSeletion = true;
                    f.ChoseEvent += data =>
                    {
                        var dic = detail1.GetData();

                        dic["SO_CT0"] = data.Cells["SO_CT"].Value;
                        dic["TK_I"] = data.Cells["TK"].Value;
                        if (data.Cells["MA_NT"].ToString().Trim() == _mMaNt0)
                        {
                            dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                            dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                            dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_TT"] = data.Cells["CL_TT"].Value;
                            dic["TT_QD"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO_NT"] = data.Cells["CL_TT"].Value;
                        }
                        else
                        {
                            dic["T_TT_NT0"] = data.Cells["TC_TT"].Value;
                            dic["T_TT_QD"] = data.Cells["T_TT_QD"].Value;
                            dic["PHAI_TT_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_NT"] = data.Cells["CL_TT"].Value;
                            dic["TIEN_TT"] = data.Cells["CL_TT"].Value;
                            dic["TT_QD"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO"] = data.Cells["CL_TT"].Value;
                            dic["PS_CO_NT"] = data.Cells["CL_TT"].Value;
                        }


                        dic["MA_NT_I"] = data.Cells["MA_NT"].Value;
                        dic["STT_REC_TT"] = data.Cells["STT_REC"].Value;
                        dic["NGAY_CT0"] = data.Cells["NGAY_CT"].Value;
                        dic["SO_SERI0"] = data.Cells["SO_SERI"].Value;

                        dic["MA_KH_I"] = data.Cells["MA_KH"].Value;
                        dic["TEN_KH_I"] = data.Cells["TEN_KH"].Value;

                        //{Tuanmh 21/08/2016
                        if (Txtdien_giai.Text != "")
                        {
                            dic["DIEN_GIAII"] = Txtdien_giai.Text.Trim() + " số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        else
                        {
                            dic["DIEN_GIAII"] = " Thu tiền bán hàng theo CT số " + data.Cells["SO_CT"].Value.ToString().Trim() + ", ngày " + ObjectAndString.ObjectToString((DateTime)data.Cells["NGAY_CT"].Value);
                        }
                        //}

                        XuLyThemDetail(dic);
                    };
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChucNang_ThuCongNo " + _sttRec, ex);
            }
        }

        private void TroGiupMenu_Click(object sender, EventArgs e)
        {
            ChucNang_TroGiup();
        }

        private void chonTuExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucNang_ChonTuExcel();
        }

        private void ThuCongNo_Click(object sender, EventArgs e)
        {
            ChucNang_ThuCongNo();
        }

        private void xuLyKhacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INKHAC);
        }

        private void txtLoaiPhieu_V6LostFocusNoChange(object sender)
        {
            //Tuanmh 15/04/2018 - SetInitFilter =null khi HaveValueChanged=false
            
            txtLoaiPhieu.SetInitFilter(string.Format("Ma_ct = '{0}'", Invoice.Mact));
            txtLoaiPhieu.ExistRowInTable(true);
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

        private void thuNoTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThuNo131();
        }

        public void ThuNo131()
        {
            try
            {
                if (NotAddEdit) return;
                if (_MA_GD == "3")
                {
                    detail1.MODE = V6Mode.View;


                    var initFilter = GetSoCt0InitFilter();
                    var f = new FilterView_ARSODU0TK(Invoice, new V6ColorTextBox(), _sttRec, txtMadvcs.Text, initFilter);
                    f.MultiSeletion = true;
                    f.ChoseEvent += data =>
                    {
                        var dic = detail1.GetData();

                        dic["TK_I"] = data.Cells["TK"].Value;

                        dic["TIEN"] = data.Cells["DU_NO"].Value;
                        dic["TIEN_NT"] = data.Cells["DU_NO"].Value;
                        dic["TIEN_TT"] = data.Cells["DU_NO"].Value;
                        dic["PS_CO"] = data.Cells["DU_NO"].Value;
                        dic["PS_CO_NT"] = data.Cells["DU_NO"].Value;


                        dic["MA_KH_I"] = data.Cells["MA_KH"].Value;
                        dic["TEN_KH_I"] = data.Cells["TEN_KH"].Value;

                        XuLyThemDetail(dic);
                    };
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ThuNo131 " + _sttRec, ex);
            }
        }
    }
}
