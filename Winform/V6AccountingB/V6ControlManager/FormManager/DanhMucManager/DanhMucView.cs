using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.DanhMucManager.PhanNhom;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using SortOrder = System.Windows.Forms.SortOrder;

namespace V6ControlManager.FormManager.DanhMucManager
{
    /// <summary>
    /// Hiển thị dữ liệu danh mục, không có parentData như CategoryView.
    /// </summary>
    public partial class DanhMucView : V6FormControl
    {
        private bool _aldm;
        public DanhMucView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="title"></param>
        /// <param name="tableName"></param>
        /// <param name="initFilter">Không có thì truyền null</param>
        /// <param name="sort">Không có thì truyền null</param>
        /// <param name="aldm">Có lấy thông tin quản lý dm trong aldm hay không?</param>
        public DanhMucView(string itemId, string title, string tableName, string initFilter, string sort, bool aldm)
        {
            m_itemId = itemId;
            _aldm = aldm;
            
            InitializeComponent();

            Title = title;
            _tableName = tableName;
            CurrentTable = V6TableHelper.ToV6TableName(tableName);
            _hideColumnDic = _categories.GetHideColumns(tableName);
            InitFilter = initFilter;
            
            SelectResult = new V6SelectResult();
            SelectResult.SortField = sort;

            if (aldm)
            {
                aldm_config = V6ControlsHelper.GetAldmConfigByTableName(_tableName);
                if (string.IsNullOrEmpty(SelectResult.SortField) && !string.IsNullOrEmpty(aldm_config.ORDER))
                    SelectResult.SortField = aldm_config.ORDER;
            }
            else
            {
                v6lookup_config = V6ControlsHelper.GetV6lookupConfigByTableName(_tableName);
                if (string.IsNullOrEmpty(SelectResult.SortField) && !string.IsNullOrEmpty(v6lookup_config.vOrder))
                    SelectResult.SortField = v6lookup_config.vOrder;
            }

            GetExtraInitFilter();

            if (CurrentTable == V6TableName.V_alts || CurrentTable == V6TableName.V_alcc
                || CurrentTable == V6TableName.V_alts01 || CurrentTable == V6TableName.V_alcc01)
            {
                btnCopy.Visible = false;
                btnDoiMa.Visible = false;
                btnIn.Visible = false;
            }

            if (CurrentTable == V6TableName.Alnhkh || CurrentTable == V6TableName.Alnhvt
                || CurrentTable == V6TableName.Alnhvv || CurrentTable == V6TableName.Alnhvitri
                || CurrentTable == V6TableName.Alnhphi || CurrentTable == V6TableName.Alnhts
                || CurrentTable == V6TableName.Alnhcc || CurrentTable == V6TableName.Alnhhd
                || CurrentTable == V6TableName.Alnhtk || CurrentTable == V6TableName.Alnhku)
            {
                btnNhom.Enabled = true;
            }
            else if (aldm && aldm_config.IsGroup)
            {
                btnNhom.Enabled = true;
            }

            dataGridView1.DataSource = new DataTable();
            MyInit();
        }

        /// <summary>
        /// Thay đổi InitFilter.
        /// </summary>
        private void GetExtraInitFilter()
        {
            try
            {
                //Lấy Thêm AdvanceFilter từ Procedure.
                string filterType = V6ControlFormHelper.FindFilterType(this);
                if (string.IsNullOrEmpty(filterType))
                {
                    filterType = "0";
                }

                SqlParameter[] plist =
                {
                    new SqlParameter("@IsAldm", _aldm),
                    new SqlParameter("@TableName", _tableName),
                    new SqlParameter("@Type", filterType),
                    new SqlParameter("@User_id", V6Login.UserId),
                };

                var extra_initfilter = (V6BusinessHelper.ExecuteProcedureScalar("VPA_GetAdvanceFilter", plist) ?? "").ToString().Trim();
                if (extra_initfilter != "")
                {
                    AddInitFilter(extra_initfilter);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetExtraInitFilter", ex);
            }
        }

        private void MyInit()
        {
            try
            {
                string filter_field = FILTER_FIELD;
                if (!string.IsNullOrEmpty(filter_field))
                {
                    //Get filter data
                    var data = V6BusinessHelper.Select(LOAD_TABLE, "distinct " + filter_field, "", "", filter_field).Data;
                    data.Rows.Add(data.NewRow());
                    var view = new DataView(data);
                    view.Sort = filter_field;
                    cboFilter.DisplayMember = filter_field;
                    cboFilter.ValueMember = filter_field;
                    cboFilter.DataSource = view;
                    cboFilter.DisplayMember = filter_field;
                    cboFilter.ValueMember = filter_field;
                    cboFilter.Visible = true;
                    lblFilter.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void DanhMucView_Load(object sender, EventArgs e)
        {

            LoadTable(CurrentTable, SelectResult.SortField);
            FormManagerHelper.HideMainMenu();
            dataGridView1.Focus();
            if (CurrentTable == V6TableName.Alkh)
            {
                KeyFields = new[] {"MA_KH"};
            }
            MakeStatus2Text();
            SetStatus2Text();
        }

        
        private readonly V6Categories _categories = new V6Categories();
        private SortedDictionary<string, string> _hideColumnDic; 
        /// <summary>
        /// Tên gốc gửi vào
        /// </summary>
        private string _tableName;
        /// <summary>
        /// Tên theo enum V6TableName
        /// </summary>
        [DefaultValue(V6TableName.None)]
        [Description("Tên theo enum V6TableName")]
        public V6TableName CurrentTable { get; set; }
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

        public bool EnableChangeCode
        {
            get { return btnDoiMa.Enabled; }
            set { btnDoiMa.Enabled = value; }
        }
        
        public bool EnableChangeGroup
        {
            get { return btnNhom.Enabled; }
            set { btnNhom.Enabled = value; }
        }

        public bool EnableFullScreen
        {
            get { return btnFull.Enabled; }
            set { btnFull.Enabled = value; }
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
        
        private void SetFormatGridView()
        {
            try
            {
                if (CurrentTable == V6TableName.Altk0)
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
                else if (CurrentTable == V6TableName.Alvv)
                {
                    dataGridView1.CellFormatting += (s, e) =>
                    {
                        if (e.ColumnIndex == 1)
                        {
                            int b = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Bac_vv"].Value);
                            int l = ObjectAndString.ObjectToInt(dataGridView1.Rows[e.RowIndex].Cells["Loai_vv"].Value);
                            if (l == 0) dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                            if (b == 1) b = 0;
                            var p = b * 8;
                            e.CellStyle.Padding = new Padding(p, 0, 0, 0);
                        }
                    };
                }
                else if(CurrentTable == V6TableName.Almaubcct)
                {
                    V6ControlFormHelper.FormatGridView(dataGridView1, "BOLD", "=", 1, true, false, Color.White);
                }
            
                // Đè format cũ
                if (_aldm)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, aldm_config.GRDS_V1, aldm_config.GRDF_V1,
                        V6Setting.IsVietnamese ? aldm_config.GRDHV_V1 : aldm_config.GRDHE_V1);
                    V6ControlFormHelper.FormatGridView(dataGridView1, aldm_config.FIELDV, aldm_config.OPERV, aldm_config.VALUEV, aldm_config.BOLD_YN,
                        aldm_config.COLOR_YN, Color.FromName(aldm_config.COLORV));

                    int frozen = ObjectAndString.ObjectToInt(aldm_config.FROZENV);
                    dataGridView1.SetFrozen(frozen);
                }
                else
                {
                    string showFields = v6lookup_config.GRDS_V1;
                    string formatStrings = v6lookup_config.GRDF_V1;
                    string headerString = V6Setting.IsVietnamese ? v6lookup_config.GRDHV_V1 : v6lookup_config.GRDHE_V1;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
                    V6ControlFormHelper.FormatGridView(dataGridView1, v6lookup_config.FIELDV, v6lookup_config.OPERV, v6lookup_config.VALUEV, v6lookup_config.BOLD_YN,
                        v6lookup_config.COLOR_YN, Color.FromName(v6lookup_config.COLORV));

                    int frozen = ObjectAndString.ObjectToInt(v6lookup_config.FROZENV);
                    dataGridView1.SetFrozen(frozen);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetFormatGridView", ex);
            }
        }

        private AldmConfig aldm_config;
        private V6lookupConfig v6lookup_config;
        #region ==== Do method ====

        public override void DoHotKey(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.F6))
                {
                    
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey", ex);
            }
            base.DoHotKey(keyData);
        }

        public override void DisableZoomButton()
        {
            btnFull.Enabled = false;
        }

        private void DoAdd()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
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
                        if (CurrentTable == V6TableName.Notable)
                        {
                            var f = new FormAddEdit(_tableName, V6Mode.Add, keys, _data);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                        else
                        {
                            var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        if (CurrentTable == V6TableName.Notable)
                        {
                            var f = new FormAddEdit(_tableName);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                        else
                        {
                            //this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                            var f = new FormAddEdit(CurrentTable);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoAdd " + _tableName, ex);
            }
        }

        private void DoAddCopy()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
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
                        if (CurrentTable == V6TableName.Notable)
                        {
                            var f = new FormAddEdit(_tableName, V6Mode.Add, keys, _data);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                        else
                        {
                            var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                            f.InsertSuccessEvent += f_InsertSuccess;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoAddCopy " + _tableName, ex);
            }
        }

        private void DoEdit()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
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
                        if (CurrentTable == V6TableName.Notable)
                        {
                            var f = new FormAddEdit(_tableName, V6Mode.Edit, keys, _data);
                            f.UpdateSuccessEvent += f_UpdateSuccess;
                            f.CallReloadEvent += FCallReloadEvent;
                            f.ShowDialog(this);
                        }
                        else
                        {
                            var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, _data);
                            f.UpdateSuccessEvent += f_UpdateSuccess;
                            f.CallReloadEvent += FCallReloadEvent;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
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
        private void f_UpdateSuccess(SortedDictionary<string, object> data)
        {
            try
            {
                if (CurrentTable == V6TableName.Notable
                    && !string.IsNullOrEmpty(aldm_config.TABLE_VIEW)
                    && V6BusinessHelper.IsExistDatabaseTable(aldm_config.TABLE_VIEW))
                {
                    ReLoad();
                }
                else
                {
                    if (data == null) return;
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                    V6ControlFormHelper.UpdateGridViewRow(row, data);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
        }

        private void DoChangeCode()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        _data = row.ToDataDictionary();

                        var f = ChangeCode.ChangeCodeManager.GetChangeCodeControl(_tableName, _data);
                        if (f != null)
                        {
                            f.DoChangeCodeFinish += f_DoChangeCodeFinish;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        private void f_DoChangeCodeFinish(SortedDictionary<string, object> data)
        {
            ReLoad();
        }

        private void DoDelete()
        {
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                var confirm = false;
                var t = 0;

                if (row != null)
                {
                    _data = row.ToDataDictionary();
                    if (CurrentTable == V6TableName.V_alts)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();


                        data.Add("MA_GIAM_TS", "");
                        data.Add("NGAY_GIAM", null);
                        data.Add("LY_DO_GIAM", "");
                        data.Add("SO_CT", "");


                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + so_the_ts, V6Text.Delete)
                            == DialogResult.Yes)
                        {
                            confirm = true;
                            t = _categories.Update(CurrentTable, data, keys);
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alcc)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();


                        data.Add("MA_GIAM_CC", "");
                        data.Add("NGAY_GIAM", "");
                        data.Add("LY_DO_GIAM", "");
                        data.Add("SO_CT", "");


                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + so_the_cc, V6Text.DeleteConfirm)
                            == DialogResult.Yes)
                        {
                            confirm = true;
                            t = _categories.Update(CurrentTable, data, keys);
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alts01)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("NGAY_KH1", "");
                        var so_the_ts = row.Cells["SO_THE_TS"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + so_the_ts, V6Text.DeleteConfirm)
                            == DialogResult.Yes)
                        {
                            confirm = true;
                            t = _categories.Update(CurrentTable, data, keys);
                        }
                    }
                    else if (CurrentTable == V6TableName.V_alcc01)
                    {
                        var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("NGAY_PB1", "");
                        var so_the_cc = row.Cells["SO_THE_CC"].Value.ToString().Trim();
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + so_the_cc, V6Text.DeleteConfirm)
                            == DialogResult.Yes)
                        {
                            confirm = true;
                            t = _categories.Update(CurrentTable, data, keys);
                        }
                    }
                    else if (CurrentTable == V6TableName.V6user)
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

                        //var code = UtilityHelper.DeCrypt(row.Cells["CODE_USER"].Value.ToString().Trim());
                        //if (!string.IsNullOrEmpty(code))
                        //{
                        //    var isAdmin = code.Right(1) == "1";

                        //}
                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + userId, V6Text.Delete)
                                == DialogResult.Yes)
                            {
                                confirm = true;
                                t = _categories.Delete(CurrentTable, keys);
                            }
                        }

                    }
                    else
                    {
                        // Tuanmh 23/08/2017
                        //id = _aldm ? aldm_config.KEY:
                        var id = _aldm ? aldm_config.TABLE_KEY: v6lookup_config.vValue;
                        
                        var id_check = _aldm ? aldm_config.DOI_MA : v6lookup_config.vValue;

                        var listTable = _aldm?aldm_config.F8_TABLE: v6lookup_config.ListTable;
                        var value = "";
                        var value_show = "";

                        if (String.IsNullOrEmpty(listTable) == false)
                        {
                            value = string.IsNullOrEmpty(id) ? "" : row.Cells[id].Value.ToString().Trim();
                            value_show = value;
                            var v = _categories.IsExistOneCode_List(listTable, id_check, value);
                            if (v)
                            {
                                this.ShowWarningMessage(V6Text.DaPhatSinh_KhongDuocXoa);
                                return;
                            }
                            // Change id-> f_name
                            if (_aldm)
                            {
                               value_show = string.IsNullOrEmpty(aldm_config.F_NAME) ? "" : row.Cells[aldm_config.F_NAME].Value.ToString().Trim();
                            }
                        }

                        if (dataGridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> {{"UID", row.Cells["UID"].Value}};

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value_show, V6Text.DeleteConfirm)
                                == DialogResult.Yes)
                            {
                                confirm = true;
                                t = _categories.Delete(_tableName, keys);

                                if (t > 0)
                                {
                                    if (CurrentTable == V6TableName.Alhd)
                                    {
                                        try
                                        {
                                            var ma = row.Cells["Ma_hd"].Value.ToString().Trim();
                                            SqlParameter[] plist =
                                            {
                                                new SqlParameter("@cMa_hd", ma),
                                            };
                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_DELETE_ALHDCT", plist);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorException(GetType() + ".DoDelete Alhd after", ex);
                                        }
                                    }
                                    else if (CurrentTable == V6TableName.Altk0)
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
                                            this.ShowErrorException(GetType() + ".DoDelete Altk0 after", ex);
                                        }

                                    }
                                    else if (CurrentTable == V6TableName.Alkh)
                                    {
                                        try
                                        {
                                            var ma_kh = row.Cells["Ma_kh"].Value.ToString().Trim();
                                            SqlParameter[] plist =
                                            {
                                                new SqlParameter("@cMa_kh", ma_kh), 
                                            };
                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_DELETE_ALKHCT", plist);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorException(GetType() + ".DoDelete Alkh after", ex);
                                        }
                                    }
                                    else if (CurrentTable == V6TableName.Alreport)
                                    {
                                        try
                                        {
                                            var ma_bc = row.Cells["Ma_bc"].Value.ToString().Trim();
                                            SqlParameter[] plist =
                                            {
                                                new SqlParameter("@ma_bc", ma_bc), 
                                            };
                                            V6BusinessHelper.ExecuteSqlNoneQuery("Delete Alreport1 where Ma_bc=@ma_bc", plist);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorException(GetType() + ".DoDelete Alreport after", ex);
                                        }
                                    }
                                    else if (CurrentTable == V6TableName.Alvt)
                                    {
                                        try
                                        {
                                            var ma_vt = row.Cells["Ma_vt"].Value.ToString().Trim();
                                            SqlParameter[] plist =
                                            {
                                                new SqlParameter("@cMa_vt", ma_vt),
                                            };
                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_DELETE_ALVTCT", plist);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorException(GetType() + ".DoDelete Alvt after", ex);
                                        }
                                    }
                                    if (CurrentTable == V6TableName.Alvitri)
                                    {
                                        try
                                        {
                                            var ma_vitri = row.Cells["Ma_vitri"].Value.ToString().Trim();
                                            SqlParameter[] plist =
                                            {
                                                new SqlParameter("@cMa_vitri", ma_vitri),
                                            };
                                            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_DELETE_ALVITRICT", plist);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.ShowErrorException(GetType() + ".DoDelete Alvitri after", ex);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ShowWarningMessage("Không có khóa UID. Vẫn xóa!");

                            _categories.Delete(_tableName, _data);
                        }
                    }

                    if (confirm)
                    {
                        if (t > 0)
                        {
                            ReLoad();
                            V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                        }
                    }
                    
                    var aev = AddEditManager.Init_Control(CurrentTable, _tableName); //ảo
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

                        V6ControlFormHelper.Copy_Here2Data(CurrentTable, V6Mode.Delete,
                            aev.KeyField1, aev.KeyField2, aev.KeyField3,
                            oldKey1, oldKey2, oldKey3,
                            oldKey1, oldKey2, oldKey3,
                            uid);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1}.DoDelete {2} {3}", V6Login.ClientName, GetType(), _tableName, ex.Message));
            }
        }

        private void DoView()
        {
            try
            {
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
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
                        if (CurrentTable == V6TableName.Notable)
                        {
                            var f = new FormAddEdit(_tableName, V6Mode.View, keys, _data);
                            f.ShowDialog(this);
                        }
                        else
                        {
                            var f = new FormAddEdit(CurrentTable, V6Mode.View, keys, _data);
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        private void DoPrint()
        {
            try
            {
                FormManagerHelper.ShowDanhMucPrint(this, _tableName, ReportFile, ReportTitle, ReportTitle2);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }
        
        private void DoGroup()
        {
            try
            {
                V6TableName name = V6TableHelper.ToV6TableName(_tableName);
                if (name == V6TableName.Alnhkh)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhkh", "Alkh");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhvt)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhvt", "Alvt");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhvv)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhvv", "Alvv");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhvitri)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhvitri", "Alvitri");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhphi)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhphi", "Alphi");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhts)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhts", "Alts");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhcc)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhcc", "Alcc");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhhd)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhhd", "Alhd");
                    
                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhtk)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhtk", "Altk0");

                    form.ShowDialog(this);
                    return;
                }
                else if (name == V6TableName.Alnhku)
                {
                    PhanNhomForm form = new PhanNhomForm("Alnhku", "Alku");

                    form.ShowDialog(this);
                    return;
                }
                else if(_aldm && aldm_config.IsGroup)
                {
                    string L_ALDM = aldm_config.L_ALDM;
                    var sss = ObjectAndString.SplitString(L_ALDM);
                    if (sss.Length >= 3)
                    {
                        PhanNhomForm form = new PhanNhomForm(_tableName, sss[0], sss[1], sss[2]);
                        form.ShowDialog(this);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        #endregion do method


        /// <summary>
        /// Được gọi từ DanhMucControl
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sort"></param>
        public void LoadTable(V6TableName tableName, string sort)
        {
            SelectResult = new V6SelectResult();
            CloseFilterForm();
            int pageSize = 20;
            if (comboBox1.SelectedIndex >= 0)
            {
                int.TryParse(comboBox1.Text, out pageSize);
            }
            //else comboBox1.Text = "20";//gây lỗi index changed
            LoadTable(tableName, 1, pageSize, sort, true);
        }

        private string LOAD_TABLE
        {
            get
            {
                string load_table = _tableName;// CurrentTable.ToString();
                if (CurrentTable == V6TableName.Notable && aldm_config != null)
                {
                    if (!string.IsNullOrEmpty(aldm_config.TABLE_VIEW)
                        && V6BusinessHelper.IsExistDatabaseTable(aldm_config.TABLE_VIEW))
                    {
                        load_table = aldm_config.TABLE_VIEW;
                    }
                    else
                    {
                        load_table = _tableName;
                    }

                    //if (string.IsNullOrEmpty(sortField)) sortField = aldm_config.ORDER;
                }
                return load_table;
            }
        }

        private void LoadTable(V6TableName tableName, int page, int size, string sortField, bool ascending)
        {
            try { 
                if (page < 1) page = 1;
                CurrentTable = tableName;
                string load_table = LOAD_TABLE;
                if (aldm_config != null && CurrentTable == V6TableName.Notable)
                {
                    //if (!string.IsNullOrEmpty(aldm_config.TABLE_VIEW)
                    //    && V6BusinessHelper.IsExistDatabaseTable(aldm_config.TABLE_VIEW))
                    //{
                    //    load_table = aldm_config.TABLE_VIEW;
                    //}
                    //else
                    //{
                    //    load_table = _tableName;
                    //}
                    
                    if (string.IsNullOrEmpty(sortField)) sortField = aldm_config.ORDER;
                }
                else
                {
                    //if (string.IsNullOrEmpty(sortField)) sortField = v6lookup_config.vOrder;
                }

                _last_filter = GetWhere();
                var sr = _categories.SelectPaging(load_table, "*", page, size, _last_filter, sortField, @ascending);
                
                SelectResult.Data = sr.Data;
                SelectResult.Page = sr.Page;
                SelectResult.TotalRows = sr.TotalRows;
                SelectResult.PageSize = sr.PageSize;
                SelectResult.Fields = sr.Fields;
                SelectResult.FieldsHeaderDictionary = sr.FieldsHeaderDictionary;
                SelectResult.Where = _last_filter;
                SelectResult.SortField = sr.SortField;
                SelectResult.IsSortOrderAscending = sr.IsSortOrderAscending;

                ViewResultToForm();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _tableName), ex);
            }
        }

        private void LoadAtPage(int page)
        {
            LoadTable(CurrentTable, page, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
        }


        public void ViewResultToForm()
        {
            
            dataGridView1.DataSource = SelectResult.Data;

            var column = dataGridView1.Columns[SelectResult.SortField];
            if (column != null)
                column.HeaderCell.SortGlyphDirection = SelectResult.IsSortOrderAscending ? SortOrder.Ascending : SortOrder.Descending;

            //var st = V6BusinessHelper.GetTableStruct("V6struct1".ToString());
            
            if(SelectResult.FieldsHeaderDictionary != null && SelectResult.FieldsHeaderDictionary.Count>0)
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                var field = dataGridView1.Columns[i].DataPropertyName.ToUpper();
                if(SelectResult.FieldsHeaderDictionary.ContainsKey(field))
                {
                    dataGridView1.Columns[i].HeaderText =
                        SelectResult.FieldsHeaderDictionary[field];
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
            try { 
            LoadTable(CurrentTable, 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        public void Previous()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.Page - 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        public void Next()
        {
            try
            {
                if (SelectResult.Page == SelectResult.TotalPages) return;
                LoadTable(CurrentTable, SelectResult.Page + 1, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        public void Last()
        {
            try { 
            LoadTable(CurrentTable, SelectResult.TotalPages, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _tableName, ex.Message));
            }
        }

        /// <summary>
        /// Reload and setFormatGridView
        /// </summary>
        public void ReLoad()
        {
            try
            {
                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize, SelectResult.SortField, SelectResult.IsSortOrderAscending);
                LoadSelectedCellLocation(dataGridView1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ReLoad", ex);
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
            if (V6Login.UserRight.AllowAdd("", _tableName.ToUpper() + "6"))
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
            if (V6Login.UserRight.AllowCopy("", _tableName.ToUpper() + "6"))
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
            if (V6Login.UserRight.AllowView("", _tableName.ToUpper() + "6"))
            {
                DoView();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowPrint("", _tableName.ToUpper() + "6"))
            {
                DoPrint();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }
        
        private void btnNhom_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", _tableName.ToUpper() + "6"))
            {
                DoGroup();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        //Reload
        private void f_InsertSuccess(SortedDictionary<string, object> data)
        {
            try
            {
                ReLoad();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".f_InsertSuccess", ex);
            }
        }


        private SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", _tableName.ToUpper() + "6"))
            {
                DoEdit();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnDoiMa_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", _tableName.ToUpper() + "6")
                && V6Login.UserRight.AllowEdit("", _tableName.ToUpper() + "6"))
            {
                DoChangeCode();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowDelete("", _tableName.ToUpper() + "6"))
            {
                DoDelete();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }


        private FilterForm _filterForm;
        private string InitFilter;
        private string FILTER_FIELD
        {
            get
            {
                if (_aldm) return aldm_config == null ? null : aldm_config.FILTER_FIELD;
                return v6lookup_config.FILTER_FIELD;
            }
        }
        /// <summary>
        /// Chuỗi lọc khi tìm kiếm
        /// </summary>
        private string _search;

        /// <summary>
        /// Lấy chuỗi lọc cuối cùng khi tải dữ liệu.
        /// </summary>
        /// <returns></returns>
        private string GetWhere()
        {
            string result = "";
            if (!string.IsNullOrEmpty(InitFilter))
            {
                result = InitFilter;
            }

            //Thêm lọc Filter_Field
            if (cboFilter.Visible && cboFilter.SelectedIndex > 0)
            {
                string filter = string.Format("{0}='{1}'", FILTER_FIELD, cboFilter.SelectedValue);
                result += string.Format("{0}{1}", result.Length > 0 ? " and " : "", filter);
            }

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
            V6TableStruct structTable = V6BusinessHelper.GetTableStruct(_tableName);
            //var keys = new SortedDictionary<string, object>();
            string[] fields = _aldm ? ObjectAndString.SplitString(aldm_config.F_SEARCH):
                V6Lookup.GetDefaultLookupFields(_tableName);
            _filterForm = new FilterForm(structTable, fields);
            _filterForm.FilterApplyEvent += FilterFilterApplyEvent;
            _filterForm.Opacity = 0.9;
            _filterForm.TopMost = true;
            //_filterForm.Location = Location;
            _filterForm.Show(this);
        }

        void FilterFilterApplyEvent(string query)
        {
            _search = query;
            LoadAtPage(1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _search = "";
            cboFilter.SelectedIndex = -1;
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
                if (EnableChangeCode) text += ", F6-Đổi mã";
                if (EnableDelete) text += ", F8-Xóa";
            }
            else
            {
                if (EnableEdit) text += ", F3-Edit";
                if (EnableAdd) text += ", F4-New";
                text += ", F5-Search";
                if (EnableChangeCode) text += ", F6-Change code";
                if (EnableDelete) text += ", F8-Delete";
            }
            status2text = text.Substring(2);
        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(status2text);
        }
        
        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
            if (_hideColumnDic.ContainsKey(e.Column.DataPropertyName.ToUpper()))
            {
                e.Column.Visible = false;
            }
        }

        private void btnFull_Click(object sender, EventArgs e)
        {
            var container = Parent;
            var child = this;
            if (container is Form)
            {
                ((Form)container).Close();
            }
            else
            {

                var f = new V6Form
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false,
                    FormBorderStyle = FormBorderStyle.None
                };
                f.Controls.Add(child);
                f.FormClosing += (se, a) =>
                {
                    container.Controls.Add(child);
                    btnFull.Image = Properties.Resources.ZoomIn24;
                    btnFull.Text = V6Text.ZoomIn;
                };
                btnFull.Image = Properties.Resources.ZoomOut24;
                btnFull.Text = V6Text.ZoomOut;
                f.ShowDialog(container);
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
                
                LoadTable(CurrentTable, SelectResult.Page, SelectResult.PageSize, sort_field, new_sortOrder);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Thêm chuỗi where vào InitFilter. Trả về chuỗi đã kết hợp (có thể không cần hứng).
        /// </summary>
        /// <param name="where">Chuỗi filter thêm</param>
        /// <param name="and"></param>
        /// <returns></returns>
        public string AddInitFilter(string where, bool and = true)
        {
            if (string.IsNullOrEmpty(where)) return InitFilter;
            if (string.IsNullOrEmpty(InitFilter))
            {
                InitFilter = where;
                return InitFilter;
            }
            
            InitFilter = string.Format("({0}) {1} ({2})", InitFilter, and ? "and" : "or", where);
            return InitFilter;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAtPage(1);
        }

        
        
        
    }
}
