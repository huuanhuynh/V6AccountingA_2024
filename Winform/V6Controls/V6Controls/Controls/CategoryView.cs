using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using SortOrder = System.Windows.Forms.SortOrder;

namespace V6Controls.Controls
{
    /// <summary>
    /// Có parent value so với DanhMucView.
    /// </summary>
    public partial class CategoryView : V6FormControl
    {
        private readonly bool _cancel;

        /// <summary>
        /// Chọn trường filter trên form.
        /// </summary>
        private string FILTER_FIELD
        {
            get
            {
                var result = _aldmConfig.IS_ALDM ? _aldmConfig.FILTER_FIELD : _v6LookupConfig.FILTER_FIELD;

                if (string.IsNullOrEmpty(result) && _MA_DM == "CORPLAN")
                {
                    result = "SFILE";
                }
                else if (string.IsNullOrEmpty(result) && _MA_DM == "CORPLAN1")
                {
                    result = "SFILE";
                }
                else if (string.IsNullOrEmpty(result) && _MA_DM == "CORPLAN2")
                {
                    result = "SFILE";
                }

                return result;
            }
        }

        public CategoryView()
        {
            InitializeComponent();
            _cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="title"></param>
        /// <param name="ma_dm"></param>
        /// <param name="initFilter">Không có thì truyền null</param>
        /// <param name="sort">Không có thì truyền null</param>
        /// <param name="parentData">Dữ liệu cha</param>
        public CategoryView(string itemId, string title, string ma_dm, string initFilter, string sort,
            IDictionary<string, object> parentData)
        {
            m_itemId = itemId;
            Title = title;
            _MA_DM = ma_dm.ToUpper();
            _parentData = parentData;

            InitializeComponent();

            
            _hideColumnDic = _categories.GetHideColumns(ma_dm);
            InitFilter = initFilter;
            SelectResult = new V6SelectResult {SortField = sort};

            //bool is_aldm = false, check_admin = false, check_v6 = false;
            

            _aldmConfig = ConfigManager.GetAldmConfigByTableName(_MA_DM);
            if (_aldmConfig.IS_ALDM)
            {
                if (string.IsNullOrEmpty(SelectResult.SortField) && !string.IsNullOrEmpty(_aldmConfig.ORDER))
                    SelectResult.SortField = _aldmConfig.ORDER;
            }
            else
            {
                _v6LookupConfig = V6Lookup.GetV6lookupConfigByTableName(_MA_DM);
                if (string.IsNullOrEmpty(SelectResult.SortField) && !string.IsNullOrEmpty(_v6LookupConfig.vOrder))
                    SelectResult.SortField = _v6LookupConfig.vOrder;
            }

            if (_MA_DM == "V_ALTS" || _MA_DM == "V_ALCC" || _MA_DM == "V_ALTS01" || _MA_DM == "V_ALCC01")
            {
                btnCopy.Visible = false;
            }

            if (_MA_DM == "ALNHKH" || _MA_DM == "ALNHVT" || _MA_DM == "ALNHVV")
            {
                
            }

            dataGridView1.DataSource = new DataTable();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                //string filter_field = FILTER_FIELD;

                //if (!string.IsNullOrEmpty(filter_field))
                //{
                //    var data = V6BusinessHelper.Select(LOAD_TABLE, "distinct " + filter_field, "", "", filter_field).Data;
                //    data.Rows.Add(data.NewRow());
                //    var view = new DataView(data);
                //    view.Sort = filter_field;
                //    cboFilter.DisplayMember = filter_field;
                //    cboFilter.ValueMember = filter_field;
                //    cboFilter.DataSource = view;
                //    cboFilter.DisplayMember = filter_field;
                //    cboFilter.ValueMember = filter_field;
                //    cboFilter.Visible = true;
                //    lblFilter.Visible = true;
                //}
                if (_aldmConfig != null && _aldmConfig.HaveInfo)
                {
                    if (_aldmConfig.EXTRA_INFOR.ContainsKey("PAGESIZE"))
                    {
                        string ps = _aldmConfig.EXTRA_INFOR["PAGESIZE"];
                        if (comboBox1.Items.Contains(ps))
                        {
                            comboBox1.SelectedItem = ps;
                        }
                    }
                    if (_aldmConfig.EXTRA_INFOR.ContainsKey("VIEWSUM"))
                    {
                        //VIEWSUM:1:COLUMN1,COLUMN2:COLUMN1 > 0
                        var sss = ObjectAndString.SplitStringBy(_aldmConfig.EXTRA_INFOR["VIEWSUM"], ':');
                        if (sss.Length > 0 && ObjectAndString.ObjectToBool(sss[0]))
                        {
                            GridViewSummary gsum = new GridViewSummary();
                            Controls.Add(gsum);
                            dataGridView1.Height -= gsum.Height;
                            gsum.DataGridView = dataGridView1;
                            if (sss.Length > 1)
                            {
                                gsum.NoSumColumns = sss[1].Replace(',', ';');
                            }
                            if (sss.Length > 2)
                            {
                                var ccc = ObjectAndString.SplitStringBy(sss[2], ' ');
                                if (ccc.Length >= 2)
                                {
                                    gsum.SumCondition = new Condition(ccc[0], ccc[1], ccc.Length > 2 ? ccc[2] : "");
                                }
                            }
                        }

                    }
                }

                All_Objects["thisForm"] = this;
                //CreateFormProgram();
                V6ControlFormHelper.ApplyDynamicFormControlEvents(this, _MA_DM, Event_program, All_Objects);
                InvokeFormEvent(FormDynamicEvent.INIT);
                Ready();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void DanhMucView_Load(object sender, EventArgs e)
        {
            if (_cancel) return;

            LoadTable(SelectResult.SortField);
            dataGridView1.Focus();
            if (_MA_DM == "ALKH")
            {
                KeyFields = new[] {"MA_KH"};
            }
            MakeStatus2Text();
        }

        public AldmConfig _aldmConfig;
        public V6lookupConfig _v6LookupConfig;

        private readonly V6Categories _categories = new V6Categories();
        public SortedDictionary<string, string> _hideColumnDic;
        public IDictionary<string, object> _parentData;
        /// <summary>
        /// Tên gốc gửi vào
        /// </summary>
        private readonly string _MA_DM;
        /// <summary>
        /// Mã danh mục cha
        /// </summary>
        public string _MA_DM_P {get; set;}
        private string CONFIG_TABLE_NAME
        {
            get
            {
                string table = _MA_DM;// CurrentTable.ToString();
                // Tuanmh 01/07/2019 set TABLE_VIEW
                //if (CurrentTable == V6TableName.None && _aldmConfig != null)
                if (_aldmConfig != null && _aldmConfig.IS_ALDM)
                {
                    if (!string.IsNullOrEmpty(_aldmConfig.TABLE_NAME)
                        && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_NAME))
                    {
                        table = _aldmConfig.TABLE_NAME;
                    }
                    else
                    {
                        table = _MA_DM;
                    }

                    //if (string.IsNullOrEmpty(sortField)) sortField = aldm_config.ORDER;
                }
                return table;
            }
        }

        private string LOAD_TABLE
        {
            get
            {
                string load_table = _MA_DM;// CurrentTable.ToString();
                // Tuanmh 01/07/2019 set TABLE_VIEW
                //if (CurrentTable == V6TableName.None && _aldmConfig != null)
                if (_aldmConfig != null)
                {
                    if (!string.IsNullOrEmpty(_aldmConfig.TABLE_VIEW)
                        && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_VIEW))
                    {
                        load_table = _aldmConfig.TABLE_VIEW;
                    }
                    else
                    {
                        load_table = _MA_DM;
                    }

                    //if (string.IsNullOrEmpty(sortField)) sortField = aldm_config.ORDER;
                }
                return load_table;
            }
        }
        
        /// <summary>
        /// Nơi chứa dữ liệu
        /// </summary>
        public V6SelectResult SelectResult { get; set; }

        public bool EnableAdd
        {
            get { return btnThem.Enabled; }
            set { btnThem.Enabled = value; }
        }

        public bool EnableCopy
        {
            get { return btnCopy.Enabled; }
            set { btnCopy.Enabled = value; }
        }

        public bool EnableEdit
        {
            get { return btnSua.Enabled; }
            set { btnSua.Enabled = value; }
        }

        public bool EnableDelete
        {
            get { return btnXoa.Enabled; }
            set { btnXoa.Enabled = value; }
        }
        
        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public string[] KeyFields { get; set; }

        public string ReportFile { get; set; }
        public string ReportTitle { get; set; }
        public string ReportTitle2 { get; set; }

        private void btnThem_EnabledChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = btnThem.Enabled;
        }

        private bool formated;
        private void SetFormatGridView()
        {
            if (formated) return;
            try
            {
                if (_MA_DM == "ALTK0")
                {
                    //dataGridView1.Columns[0].DefaultCellStyle.Padding;
                    dataGridView1.CellFormatting += (s, e) =>
                    {
                        if (e.ColumnIndex == 1)
                        {
                            int b = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Bac_tk"].Value);
                            int l = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Loai_tk"].Value);
                            if (l == 0) dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                            if (b == 1) b = 0;
                            var p = b * 8;
                            e.CellStyle.Padding = new Padding(p, 0, 0, 0);
                        }
                    };
                }
                else if (_MA_DM == "ALMAUBCCT")
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, "BOLD", "=", 1, true, false, Color.White);
                }
            
                // Đè format cũ
                if (_aldmConfig.IS_ALDM)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1,
                            V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
                    var conditionColor = ObjectAndString.StringToColor(_aldmConfig.COLORV);
                    V6ControlFormHelper.FormatGridView(dataGridView1, _aldmConfig.FIELDV, _aldmConfig.OPERV, _aldmConfig.VALUEV,
                        _aldmConfig.BOLD_YN, _aldmConfig.COLOR_YN, conditionColor);
                    int frozen = ObjectAndString.ObjectToInt(_aldmConfig.FROZENV);
                    dataGridView1.SetFrozen(frozen);
                }
                else
                {
                    string showFields = _v6LookupConfig.GRDS_V1;
                    string formatStrings = _v6LookupConfig.GRDF_V1;
                    string headerString = V6Setting.IsVietnamese ? _v6LookupConfig.GRDHV_V1 : _v6LookupConfig.GRDHE_V1;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
                    var conditionColor = ObjectAndString.StringToColor(_v6LookupConfig.COLORV);
                    V6ControlFormHelper.FormatGridView(dataGridView1, _v6LookupConfig.FIELDV, _v6LookupConfig.OPERV, _v6LookupConfig.VALUEV,
                        _v6LookupConfig.BOLD_YN, _v6LookupConfig.COLOR_YN, conditionColor);
                    int frozen = ObjectAndString.ObjectToInt(_v6LookupConfig.FROZENV);
                    dataGridView1.SetFrozen(frozen);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetFormatGridView", ex);
            }
            formated = true;
        }

        #region ==== Do method ====

        public override void DoHotKey(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.F6))
                {

                }
                else if (keyData == Keys.Escape)
                {
                    Dispose();
                }
                //else if (keyData == Keys.F9)
                //{
                //    if (!V6BusinessHelper.CheckRightKey("", "F9", _tableName)) return;
                //    All_Objects["dataGridView1"] = dataGridView1;
                //    InvokeFormEvent(FormDynamicEvent.F9);
                //}
                else if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
                {
                    return;
                }
                else if (keyData == Keys.PageUp)
                {
                    if (btnPrevious.Enabled) btnPrevious.PerformClick();
                }
                else if (keyData == Keys.PageDown)
                {
                    if (btnNext.Enabled) btnNext.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".DoHotKey " + ex.Message, Application.ProductName);
            }
            base.DoHotKey(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                if (Navigation(keyData)) return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void DoAdd()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                FormAddEdit f;
                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);

                    if (KeyFields != null)
                        foreach (var keyField in KeyFields)
                        {
                            if (dataGridView1.Columns.Contains(keyField))
                            {
                                keys[keyField] = row.Cells[keyField].Value;
                            }
                        }

                    _data = row.ToDataDictionary();
                    
                    f = new FormAddEdit(_MA_DM, V6Mode.Add, keys, _data);

                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ParentData = _parentData;
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
                else
                {
                    f = new FormAddEdit(_MA_DM, V6Mode.Add, null, null);

                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ParentData = _parentData;
                    f.SetParentData();
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, _MA_DM);
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                //V6ControlFormHelper.CreateAdvanceFormControls(form, ma_ct, All_Objects);
                V6ControlFormHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void DoAddCopy()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);

                    if (KeyFields != null)
                        foreach (var keyField in KeyFields)
                        {
                            if (dataGridView1.Columns.Contains(keyField))
                            {
                                keys[keyField] = row.Cells[keyField].Value;
                            }
                        }

                    _data = row.ToDataDictionary();
                    
                    var f = new FormAddEdit(_MA_DM, V6Mode.Add, keys, _data);
                    f.IS_COPY = true;
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ParentData = _parentData;
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoAddCopy: " + ex.Message);
            }
        }

        private void DoEdit()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);

                    if (KeyFields != null)
                        foreach (var keyField in KeyFields)
                        {
                            if (dataGridView1.Columns.Contains(keyField))
                            {
                                keys[keyField] = row.Cells[keyField].Value;
                            }
                        }
                    _data = row.ToDataDictionary();
                    
                    var f = new FormAddEdit(_MA_DM, V6Mode.Edit, keys, _data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ParentData = _parentData;
                    f.UpdateSuccessEvent += f_UpdateSuccess;
                    f.CallReloadEvent += FCallReloadEvent;
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEdit: " + ex.Message);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs eventArgs)
        {
            ReLoad();
        }
        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".f_UpdateSuccess: " + ex.Message);
            }
        }

        
        private void DoDelete()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    _data = row.ToDataDictionary();
                    if (_MA_DM == "V_ALTS")
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        int count = V6BusinessHelper.SelectCount(CONFIG_TABLE_NAME, keys, "UID");
                        if (count != 1)
                        {
                            if (_aldmConfig.HaveInfo)
                            {
                                SqlParameter[] plist =
                                {
                                    new SqlParameter("@TableName", _aldmConfig.TABLE_NAME),
                                    new SqlParameter("@Fields", _aldmConfig.KEY),
                                    new SqlParameter("@uid", row.Cells["UID"].Value.ToString()),
                                    new SqlParameter("@mode", "X"),
                                    new SqlParameter("@User_id", V6Login.UserId),
                                };
                                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_FIX_CONFLICT_AL_ALL", plist);
                                ReLoad();
                            }
                            throw new Exception("Trùng khóa! Đã tự động sửa lỗi.\n Vui lòng thực hiện lại!\nDATA_COUNT = " + count);
                        }

                        SortedDictionary<string, object> data = new SortedDictionary<string, object>
                        {
                            {"MA_GIAM_TS", ""}, {"NGAY_GIAM", null}, {"LY_DO_GIAM", ""}, {"SO_CT", ""}
                        };

                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_ts, V6Text.Delete)
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CONFIG_TABLE_NAME, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                            }
                        }
                    }
                    else if (_MA_DM == "V_ALCC")
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>
                        {
                            {"MA_GIAM_CC", ""}, {"NGAY_GIAM", ""}, {"LY_DO_GIAM", ""}, {"SO_CT", ""}
                        };

                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_cc, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CONFIG_TABLE_NAME, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                            }
                        }
                    }
                    else if (_MA_DM == "V_ALTS01")
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object> {{"NGAY_KH1", ""}};
                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_ts, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CONFIG_TABLE_NAME, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                            }
                        }
                    }
                    else if (_MA_DM == "V_ALCC01")
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object> {{"NGAY_PB1", ""}};
                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + so_the_cc, "Xóa?")
                            == DialogResult.Yes)
                        {
                            var t = _categories.Update(CONFIG_TABLE_NAME, data, keys);
                            if (t > 0)
                            {
                                ReLoad();
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted + t);
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                            }
                        }
                    }
                    else if (_MA_DM == "V6USER")
                    {
                        if (!V6Login.IsAdmin)
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.NoRight);
                            return;
                        }
                        
                        var userId = ObjectAndString.ObjectToInt(row.Cells["USER_ID"].Value);
                        if (V6Login.UserId == userId)
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.DeleteDenied);
                            return;
                        }

                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + userId, V6Text.Delete)
                                == DialogResult.Yes)
                            {
                                var t = _categories.Delete(CONFIG_TABLE_NAME, keys);

                                if (t > 0)
                                {
                                    ReLoad();
                                    V6ControlFormHelper.ShowMainMessage(V6Text.Deleted + userId);
                                }
                                else
                                {
                                    V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                                }
                            }
                        }

                    }
                    // PHAT 23/08/2017
                    //else if (CurrentTable == V6TableName.None)
                    //{
                      
                    //    if (dataGridView1.Columns.Contains("UID"))
                    //    {
                    //        var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };

                    //        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" , V6Text.Delete)
                    //            == DialogResult.Yes)
                    //        {
                    //            var t = _categories.Delete(CONFIG_TABLE_NAME, keys);

                    //            if (t > 0)
                    //            {
                    //                ReLoad();
                    //                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                    //            }
                    //            else
                    //            {
                    //                V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                    //            }
                    //        }
                    //    }
                    //}
                    else
                    {

                        var id = _aldmConfig.IS_ALDM ? _aldmConfig.TABLE_KEY : _v6LookupConfig.vValue;
                        var listTable = _aldmConfig.IS_ALDM ? _aldmConfig.F8_TABLE : _v6LookupConfig.ListTable;
                        var value = "";
                        
                        if (String.IsNullOrEmpty(listTable) == false)
                        {
                            value = string.IsNullOrEmpty(id) ? "" : row.Cells[id].Value.ToString().Trim();
                            var v = _categories.IsExistOneCode_List(listTable, id, value);
                            if (v)
                            {
                                //khong duoc
                                this.ShowWarningMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                                return;
                            }
                        }

                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + "\n" + value, "Xóa?") == DialogResult.Yes)
                            {
                                var t = _categories.Delete(CONFIG_TABLE_NAME, keys);

                                if (t > 0)
                                {
                                    if (_MA_DM == "ALTK0")
                                    {
                                        try
                                        {
                                            var tk = row.Cells["TK"].Value.ToString().Trim();
                                            var tk_me_new = row.Cells["TK_ME"].Value.ToString().Trim();

                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UpdateLoaiTk",
                                                new SqlParameter("@tk", tk),
                                                new SqlParameter("@tk_me_old", tk_me_new),
                                                new SqlParameter("@tk_me_new", tk_me_new),
                                                new SqlParameter("@action", "D")
                                                );
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorMessage(GetType() + ".UpdateLoaiTk: " + ex.Message);
                                        }

                                    }
                                    ReLoad();
                                    V6ControlFormHelper.ShowMainMessage(V6Text.Deleted + t);
                                }
                                else
                                {
                                    V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                                }
                            }
                        }
                        else
                        {
                            this.ShowWarningMessage(V6Text.NoUID);

                            _categories.Delete(CONFIG_TABLE_NAME, _data);
                        }
                    }
                    //PHAT 23/08/2017
                    //if (CurrentTable != V6TableName.None)
                    {
                        var aev = AddEditManager.Init_Control(_MA_DM);//ảo
                        if (!string.IsNullOrEmpty(aev.KeyField1))
                        {
                            var oldKey1 = _data[aev.KeyField1].ToString().Trim();
                            var oldKey2 = "";
                            if (!string.IsNullOrEmpty(aev.KeyField2) && _data.ContainsKey(aev.KeyField2))
                                oldKey2 = _data[aev.KeyField2].ToString().Trim();
                            var oldKey3 = "";
                            if (!string.IsNullOrEmpty(aev.KeyField3) && _data.ContainsKey(aev.KeyField3))
                                oldKey3 = _data[aev.KeyField3].ToString().Trim();

                            var uid = _data.ContainsKey("UID") ? _data["UID"].ToString() : "";

                            V6ControlFormHelper.Copy_Here2Data(_MA_DM, V6Mode.Delete,
                                aev.KeyField1, aev.KeyField2, aev.KeyField3,
                                oldKey1, oldKey2, oldKey3,
                                oldKey1, oldKey2, oldKey3,
                                uid);
                        }
                    } 
                  
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Xóa lỗi!\n" + ex.Message);
            }
        }

        private void DoView()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);

                    if (KeyFields != null)
                        foreach (var keyField in KeyFields)
                        {
                            if (dataGridView1.Columns.Contains(keyField))
                            {
                                keys[keyField] = row.Cells[keyField].Value;
                            }
                        }

                    _data = row.ToDataDictionary();
                    
                    var f = new FormAddEdit(_MA_DM, V6Mode.View, keys, _data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView DoView");
            }
        }
        
        #endregion do method


        /// <summary>
        /// Được gọi từ DanhMucControl
        /// </summary>
        /// <param name="sort"></param>
        public void LoadTable(string sort)
        {
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            int pageSize = 20;
            if (comboBox1.SelectedIndex >= 0)
            {
                int.TryParse(comboBox1.Text, out pageSize);
            }
            //else comboBox1.Text = "20";//gây lỗi index changed
            LoadTable(1, pageSize, sort, true);
        }

        private void LoadTable(int page, int size, string sortField, bool ascending)
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (page < 1) page = 1;
                
                if (string.IsNullOrEmpty(sortField))
                {
                    if (_aldmConfig != null)
                    {
                        sortField = _aldmConfig.ORDER;
                    }
                }

                _last_filter = GetWhere();
                var sr = _categories.SelectPaging(LOAD_TABLE, "*", page, size, _last_filter, sortField, ascending);

                SelectResult.Data = sr.Data;
                SelectResult.Page = sr.Page;
                SelectResult.TotalRows = sr.TotalRows;
                SelectResult.PageSize = sr.PageSize;
                SelectResult.Fields = sr.Fields;
                SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                SelectResult.Where = _last_filter; // sr.Where;
                SelectResult.SortField = sr.SortField;
                SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _MA_DM), ex);
            }
        }

        private void LoadAtPage(int page)
        {
            LoadTable(page, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            dataGridView1.SetFrozen(0);
            dataGridView1.DataSource = SelectResult.Data;
            LoadSelectedCellLocation(dataGridView1);

            if (!string.IsNullOrEmpty(SelectResult.SortField))
            {
                var column = dataGridView1.Columns[SelectResult.SortField];
                if (column != null) column.HeaderCell.SortGlyphDirection = SelectResult.IsSortOrderAscending
                        ? SortOrder.Ascending
                        : SortOrder.Descending;
            }

            if(SelectResult.FieldsHeaderDictionary != null && SelectResult.FieldsHeaderDictionary.Count>0)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    var field = dataGridView1.Columns[i].DataPropertyName.ToUpper();
                    if(SelectResult.FieldsHeaderDictionary.ContainsKey(field))
                    {
                        dataGridView1.Columns[i].HeaderText =
                            SelectResult.FieldsHeaderDictionary[field];
                    }
                }
            }

            txtCurrentPage.Text = SelectResult.Page.ToString(CultureInfo.InvariantCulture);
            txtCurrentPage.BackColor = Color.White;
            
            lblTotalPage.Text = string.Format(
                V6Setting.IsVietnamese
                    ? "Trang {0}/{1} của {2} dòng {3}"
                    : "Page {0}/{1} of {2} row(s) {3}",
                SelectResult.Page, SelectResult.TotalPages, SelectResult.TotalRows,
                string.IsNullOrEmpty(_last_filter)
                    ? ""
                    : (V6Setting.IsVietnamese ? "(Đã lọc)" : "(filtered)"));

            if (SelectResult.Page <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
            }

            if (SelectResult.Page >= SelectResult.TotalPages)
            {
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            SetFormatGridView();
        }

        public void First()
        {
            try
            {
                LoadTable(1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView First");
            }
        }

        public void Previous()
        {
            try
            {
                LoadTable(SelectResult.Page - 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView Previous");
            }
        }

        public void Next()
        {
            try
            {
                if (SelectResult.Page == SelectResult.TotalPages) return;
                LoadTable(SelectResult.Page + 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView Next");
            }
        }

        public void Last()
        {
            try
            {
                LoadTable(SelectResult.TotalPages, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView Last");
            }
        }

        /// <summary>
        /// Tải lại ngay trang đó.
        /// </summary>
        public void ReLoad()
        {
            try
            {
                LoadTable(SelectResult.Page, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "DanhMucView Reload");
            }
        }
        

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

        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    ((TextBox)sender).BackColor = Color.Red;
            //}
        }

        private void txtCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            var txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                int page;
                int.TryParse(txt.Text, out page);
                if (page < 1) page = 1;
                LoadAtPage(page);
            }
            else
            {
                if (txt.SelectionLength > 0)
                {
                    txt.Text = txt.Text.Substring(0, txt.SelectionStart) +
                        txt.Text.Substring(txt.SelectionStart + txt.SelectionLength);
                }

                if (e.KeyValue >= '0' && e.KeyValue <= '9')
                {
                    //txt.Text += (char) e.KeyValue;
                    txtCurrentPage.Text = txt.Text.Insert(txt.SelectionStart, ((char)e.KeyValue).ToString());
                    txt.BackColor = Color.Red;
                }
                if (e.KeyValue >= 96 && e.KeyValue <= 105)
                {
                    
                    var n = "";
                    switch (e.KeyValue)
                    {
                        case 96:
                            n = "0";
                            break;
                        case 97:
                            n = "1";
                            break;
                        case 98:
                            n = "2";
                            break;
                        case 99:
                            n = "3";
                            break;
                        case 100:
                            n = "4";
                            break;
                        case 101:
                            n = "5";
                            break;
                        case 102:
                            n = "6";
                            break;
                        case 103:
                            n = "7";
                            break;
                        case 104:
                            n = "8";
                            break;
                        case 105:
                            n = "9";
                            break;
                    }
                    //txt.Text += n;
                    txt.Text = txt.Text.Insert(txt.SelectionStart, n);
                    txt.BackColor = Color.Red;
                }
                if (e.KeyCode == Keys.Back) // && txt.SelectionStart>0)
                {
                    if (txt.TextLength > 0)
                    {
                        txt.Text = txt.Text.Substring(0, txt.TextLength - 1);
                    }
                }
            }
            txt.SelectionStart = txt.TextLength;
        }

        private void txtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", _MA_DM.ToUpper() + "6") || (!string.IsNullOrEmpty(_MA_DM_P) && V6Login.UserRight.AllowAdd("", _MA_DM_P.ToUpper() + "6")))
            {
                DoAdd();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowCopy("", _MA_DM.ToUpper() + "6") || (!string.IsNullOrEmpty(_MA_DM_P) && V6Login.UserRight.AllowCopy("", _MA_DM_P.ToUpper() + "6")))
            {
                DoAddCopy();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowView("", _MA_DM.ToUpper() + "6") || (!string.IsNullOrEmpty(_MA_DM_P) && V6Login.UserRight.AllowView("", _MA_DM_P.ToUpper() + "6")))
            {
                DoView();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        
        //Reload
        private void f_InsertSuccess(IDictionary<string, object> data)
        {
            try
            {
                if (_aldmConfig.HaveInfo && _aldmConfig.EXTRA_INFOR.ContainsKey("F4_RELOAD") && _aldmConfig.EXTRA_INFOR["F4_RELOAD"].Trim() != "")
                {
                    // F4_RELOAD:FIELD1,FIELD2
                    string[] sss = ObjectAndString.SplitStringBy(_aldmConfig.EXTRA_INFOR["F4_RELOAD"], ':');
                    string[] keys_field = sss[0].ToUpper().Split(',');
                    V6TableStruct structTable = V6BusinessHelper.GetTableStruct(LOAD_TABLE);
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    foreach (string KEY in keys_field)
                    {
                        if (data.ContainsKey(KEY))
                        {
                            keys[KEY] = data[KEY];
                        }
                        else
                        {
                            goto Default_Reload;
                        }
                    }

                    // FIELD1 like 'Value1%' and FIELD2 like 'Value2%'
                    string new_query = SqlGenerator.GenWhere2_oper(structTable, keys, "start");
                    FilterFilterApplyEvent(new_query);

                    return;
                }

                Default_Reload:
                ReLoad();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".f_InsertSuccess", ex);
            }
        }

        /// <summary>
        /// Kiểm tra có cấu hình Fpass
        /// </summary>
        /// <param name="index">0 cho F3, 1 cho F6, 2 cho F8</param>
        /// <returns></returns>
        bool NO_CONFIG_FPASS(int index)
        {
            if (_aldmConfig.NoInfo) return true;
            if (!_aldmConfig.EXTRA_INFOR.ContainsKey("F368_PASS")) return true;
            string f368 = _aldmConfig.EXTRA_INFOR["F368_PASS"].Trim();
            if (f368.Length > index && f368[index] == '1') return false;
            return true;
        }

        private IDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", _MA_DM.ToUpper() + "6") || (!string.IsNullOrEmpty(_MA_DM_P) && V6Login.UserRight.AllowEdit("", _MA_DM_P.ToUpper() + "6")))
            {
                if (NO_CONFIG_FPASS(0) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
                {
                    DoEdit();
                }
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowDelete("", _MA_DM.ToUpper() + "6") || (!string.IsNullOrEmpty(_MA_DM_P) && V6Login.UserRight.AllowDelete("", _MA_DM_P.ToUpper() + "6")))
            {
                if (NO_CONFIG_FPASS(2) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
                {
                    DoDelete();
                }
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }


        private FilterForm _filterForm;
        private string _initFilter;
        public string InitFilter
        {
            get
            {
                if (_initFilter == null)
                {
                    _initFilter = V6Login.GetInitFilter(_MA_DM, V6ControlFormHelper.FindFilterType(this));
                }
                return ("" + _initFilter).Replace("{MA_DVCS}", "'" + V6Login.Madvcs + "'");
            }
            set { _initFilter = value; }
        }
        private string _search;

        private string GetWhere()
        {
            string result = "";
            if (!string.IsNullOrEmpty(InitFilter))
            {
                result = InitFilter;
            }

            ////Thêm lọc Filter_Field
            //if (cboFilter.Visible && cboFilter.SelectedIndex > 0)
            //{
            //    string filter = string.Format("{0}='{1}'", FILTER_FIELD, cboFilter.SelectedValue);
            //    result += string.Format("{0}{1}", result.Length > 0 ? " and " : "", filter);
            //}

            //Thêm lọc where
            if (!string.IsNullOrEmpty(_search))
            {
                result += string.Format("{0}({1})", result.Length > 0 ? " and " : "", _search);
            }

            return result;
        }

        private void CloseFilterForm()
        {
            if (_filterForm != null)
            {
                _filterForm.Close();
                _filterForm.Dispose();
                _filterForm = null;
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                V6TableStruct structTable = V6BusinessHelper.GetTableStruct(LOAD_TABLE);
                if (_aldmConfig.IS_ALDM ? (!_aldmConfig.HaveInfo) : (!_v6LookupConfig.HaveInfo))
                {
                    this.ShowWarningMessage(V6Text.NoDefine, 500);
                    return;
                }
                string[] fields = _aldmConfig.IS_ALDM ? ObjectAndString.SplitString(_aldmConfig.F_SEARCH) :
                    ObjectAndString.SplitString(V6Setting.IsVietnamese ? _v6LookupConfig.vFields : _v6LookupConfig.eFields);
                if (fields.Length == 0 && _MA_DM == "CORPLAN")
                {
                    // Hỗ trợ cho CorpLan
                    fields = new[] { "Sfile", "ID", "Ctype", "D", "V", "E" };
                }
                else if (fields.Length == 0 && _MA_DM == "CORPLAN1")
                {
                    fields = new[] { "ID", "Ctype", "D", "V", "E" };
                }
                else if (fields.Length == 0 && _MA_DM == "CORPLAN2")
                {
                    fields = new[] { "ID", "Ctype", "D", "V", "E" };
                }
                _filterForm = new FilterForm(structTable, fields);
                _filterForm.FilterApplyEvent += FilterFilterApplyEvent;
                _filterForm.Opacity = 0.9;
                _filterForm.TopMost = true;
                //_filterForm.Location = Location;
                _filterForm.Show(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Find_Click", ex);
            }
        }

        void FilterFilterApplyEvent(string query)
        {
            _search = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _search = "";
            LoadAtPage(1);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                var f = Parent;
                base.Dispose();
                if (f is Form) f.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectResult.PageSize = int.Parse(comboBox1.Text);
            LoadAtPage(1);
        }

        private string status2text = "";
        private string _last_filter;

        private void MakeStatus2Text()
        {
            var text = "";
            if (V6Setting.IsVietnamese)
            {
                if (EnableEdit) text += ", F3-Sửa";
                if (EnableAdd) text += ", F4-Thêm";
                text += ", F5-Tìm";
                //if (EnableChangeCode) text += ", F6-Đổi mã";
                if (EnableDelete) text += ", F8-Xóa";
            }
            else
            {
                if (EnableEdit) text += ", F3-Edit";
                if (EnableAdd) text += ", F4-New";
                text += ", F5-Search";
                //if (EnableChangeCode) text += ", F6-Change code";
                if (EnableDelete) text += ", F8-Delete";
            }
            status2text = text.Substring(2);
        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(status2text, "ST2" + CONFIG_TABLE_NAME);
        }
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            if (_hideColumnDic.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var column = dataGridView1.Columns[e.ColumnIndex];
                var columnName = column.DataPropertyName;
                if (columnName == "RowNum") return;

                foreach (DataGridViewColumn column0 in dataGridView1.Columns)
                {
                    if(column0 != column) column0.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                var new_sortOrder = column.HeaderCell.SortGlyphDirection != SortOrder.Ascending;
                var sort_field = column.DataPropertyName;
                
                LoadTable(SelectResult.Page, SelectResult.PageSize, sort_field, new_sortOrder);
            }
            catch
            {
                // ignored
            }
        }

        public string AddInitFilter(string where, bool and = true)
        {
            if (string.IsNullOrEmpty(where)) return InitFilter;
            _initFilter += (string.IsNullOrEmpty(InitFilter) ? "" : (and ? " and " : " or ")) + where;
            return InitFilter;
        }

    }
}
