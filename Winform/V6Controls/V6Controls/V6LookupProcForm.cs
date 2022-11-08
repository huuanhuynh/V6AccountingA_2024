using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// Cần nâng cấp thêm phần lọc từ đầu. Nếu 0 rows thì lọc or nhiều trường.
    /// Nếu còn 1 dòng thì bấm enter ở vsearch là chọn luôn.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view
    /// </summary>
    public partial class V6LookupProcForm : Form
    {
        private readonly V6LookupProc _sender;
        public string _senderText { get; set; }
        private readonly IDictionary<string, object> _senderParentData;
        public IDictionary<string, object> _selectedData;
        public List<IDictionary<string, object>> _selectedDataList;
        public AldmConfig LookupInfo;
        //private string _table_name, _ma_dm;
        private readonly string LookupInfo_F_NAME;
        private DataTable tableRoot;
        
        internal string InitStrFilter;
        
        
        private HelpProvider _helpProvider1;
        public LookupMode _lookupMode;
        public bool _filterStart;
        
        /// <summary>
        /// Sự kiện xảy ra khi nhận ở lookupMode == Data.
        /// </summary>
        public event DataSelectHandler AcceptSelectedtData;
        protected virtual void OnAccepSelectedtData(string idlist, List<IDictionary<string, object>> datalist)
        {
            var handler = AcceptSelectedtData;
            if (handler != null) handler(idlist, datalist);
        }

        public V6LookupProcForm(V6LookupProc sender, IDictionary<string, object> parentData, string senderText, AldmConfig lookupInfo, string initStrFilter, string lookupInfo_F_Name, LookupMode lookupMode = LookupMode.Single, bool filterStart = false)
        {
            _sender = sender;
            _senderParentData = parentData;
            _senderText = senderText;
            InitStrFilter = initStrFilter;
            //_senderTextBox = sender;
            LookupInfo = lookupInfo;
            LookupInfo_F_NAME = lookupInfo_F_Name;
            _lookupMode = lookupMode;
            _filterStart = filterStart;

            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            try
            {
                txtV_Search.Text = _senderText;

                _vSearchFilter = GenVSearchFilter(LookupInfo.F_SEARCH);
                tableRoot = ThemDuLieuVaoBangChinh(InitStrFilter, _vSearchFilter);
                KhoiTaoDataGridView();

                NapCacFieldDKLoc();
                if(cbbDieuKien.Items.Count>0)
                cbbDieuKien.SelectedIndex = 0;
                LayThongTinTieuDe();
                AnDauTrongComBoBoxDau();
                ChonGiaTriKhoiTaoCho_cbbKyHieu();
                

                //this.HelpButtonClicked +=new CancelEventHandler(V6LookupProcForm_HelpButtonClicked);
                _helpProvider1 = new HelpProvider();
                _helpProvider1.SetHelpString(txtV_Search, ChuyenMaTiengViet.ToUnSign(V6Text.Text("TYPETOSEARCH")));
                _helpProvider1.SetHelpString(btnVSearch, ChuyenMaTiengViet.ToUnSign(V6Text.Text("TIM")));
                _helpProvider1.SetHelpString(rbtLocTiep, ChuyenMaTiengViet.ToUnSign(V6Text.Text("CHKLOCTIEP")));

                _helpProvider1.SetHelpString(dataGridView1, ChuyenMaTiengViet.ToUnSign(V6Text.Text("SELECT1ENTER")));
                _helpProvider1.SetHelpString(btnTatCa, ChuyenMaTiengViet.ToUnSign("Hiện tất cả."));
                //helpProvider1.SetHelpString(, "Hien tat ca danh muc.");
                toolStripStatusLabel1.Text = string.Format("F1-Hướng dẫn{3}{4}, Enter-Chọn, ESC-Quay ra",
                    "0", "1", "2",
                    LookupInfo.F3 ? ", F3-Sửa" : "",
                    LookupInfo.F4 ? ", F4-Thêm" : "");
                toolStripStatusLabel2.Text = _lookupMode==LookupMode.Multi || _lookupMode == LookupMode.Data ? ", Space-Chọn/Bỏ chọn, (Ctrl+A)-Chọn tất cả, (Ctrl+U)-Bỏ chọn tất cả" : "";

                if (LookupInfo != null && LookupInfo.EXTRA_INFOR.ContainsKey("VIEWSUM"))
                {
                    //VIEWSUM:1:COLUMN1,COLUMN2:COLUMN1 > 0
                    var sss = ObjectAndString.SplitStringBy(LookupInfo.EXTRA_INFOR["VIEWSUM"], ':');
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
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Init : " + ex.Message, "V6LookupProcForm");
            }
        }

        public DataTable ThemDuLieuVaoBangChinh(string initFilter, string vSearchFilter)
        {
            if (string.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                this.WriteToLog(GetType() + ".ThemDuLieuVaoBangChinh", "LookupInfo.TABLE_NAME = empty, Ma_dm = " + LookupInfo.MA_DM);
                throw new Exception("Table_Name!");
            }
            
            try
            {
                List<SqlParameter> plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("@ma_ct", _sender.MA_CT));
                plist.Add(new SqlParameter("@stt_rec", _sender.STT_REC));
                plist.Add(new SqlParameter("@MA_KH", _sender.MA_KH));
                plist.Add(new SqlParameter("@ngay_ct", _sender.NGAY_CT.Date));
                plist.Add(new SqlParameter("@Kieu_post", _sender.KIEU_POST));
                plist.Add(new SqlParameter("@MODE", _sender.MODE));
                plist.Add(new SqlParameter("@user_id", V6Login.UserId));

                if ((_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data) && vSearchFilter.Contains(","))
                {
                    plist.Add(new SqlParameter("@advance", initFilter));
                    var tbl = V6BusinessHelper.ExecuteProcedure(LookupInfo.TABLE_NAME, plist.ToArray()).Tables[0];
                    return tbl;
                }
                else
                {
                    var where = initFilter;
                    if (!string.IsNullOrEmpty(vSearchFilter))
                    {
                        if (string.IsNullOrEmpty(where))
                        {
                            where = vSearchFilter;
                        }
                        else
                        {
                            where += " AND (" + vSearchFilter + ")";
                        }
                    }

                    plist.Add(new SqlParameter("@advance", where));

                    plist.Add(new SqlParameter("@advance2", _sender.Advance2));
                    plist.Add(new SqlParameter("@advance3", _sender.Advance3));
                    var tbl = V6BusinessHelper.ExecuteProcedure(LookupInfo.TABLE_NAME, plist.ToArray()).Tables[0];
                    return tbl;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(string.Format("{0} {1}.ThemDuLieuVaoBangChinh {2}", V6Login.ClientName, GetType(), LookupInfo.TABLE_NAME), ex, ProductName);
            }
            
            return null;
        }

        

        private void KhoiTaoDataGridView()
        {
            if (tableRoot != null)
            {
                try
                {
                    dataGridView1.DataSource = tableRoot;
                    FormatGridView();

                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Select();
                    }
                    else
                    {
                        txtV_Search.Select();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(GetType() + ".KhoiTaoDataGridView : " + e.Message);
                }
            }
            else
            {
                this.WriteToLog(GetType() + ".KhoiTaoDataGridView", "TableRoot null.");
            }
        }

        private void FormatGridView()
        {
            try
            {
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, LookupInfo.GRDS_V1, LookupInfo.GRDF_V1,
                        V6Setting.Language == "V" ? LookupInfo.GRDHV_V1 : LookupInfo.GRDHE_V1);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(string.Format("{0} {1}.FormatGridView {2}", V6Login.ClientName, GetType(), LookupInfo.TABLE_NAME), ex, ProductName);
            }
        }


        private void ChonGiaTriKhoiTaoCho_cbbKyHieu()
        {
            if (cbbKyHieu.Items.Count != 0)
            {
                cbbKyHieu.SelectedIndex = 0;
            }
        }

        private void AnDauTrongComBoBoxDau()
        {
            try
            {
                Type comboboxValue = cbbDieuKien.SelectedValue as Type;
                if (ObjectAndString.IsStringType(comboboxValue))
                {
                    cbbKyHieu.Items.AddRange(new object[] { "$", "<>" });
                }
                else if (ObjectAndString.IsNumberType(comboboxValue))
                {
                    cbbKyHieu.Items.AddRange(new object[] { "=", ">", ">=", "<", "<=", "<>" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(GetType() + ".AnDauTrongComBoBox : " + e.Message);
            }
        }

        private void LayThongTinTieuDe()
        {
            Text = V6Setting.Language == "V" ? LookupInfo.TITLE : LookupInfo.TITLE2;
        }

        private class LookupProcFormCboDieuKienItem
        {
            public string Name { get; set; } 
            public Type Value { get; set; } 
        }
        private void NapCacFieldDKLoc()
        {
            if (!String.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                try
                {
                    //SqlParameter[] plist = { new SqlParameter("@p", LookupInfo.TABLE_NAME) };
                    //var ds0 = V6BusinessHelper.Select("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME,DATA_TYPE",
                    //    "TABLE_NAME = @p", "", "", plist).Data;

                    List<LookupProcFormCboDieuKienItem> columnList = new List<LookupProcFormCboDieuKienItem>();
                    foreach (DataColumn column in tableRoot.Columns)
                    {
                        columnList.Add(new LookupProcFormCboDieuKienItem(){Name = column.ColumnName.ToUpper(), Value = column.DataType});
                    }

                    cbbDieuKien.DisplayMember = "Name";
                    cbbDieuKien.ValueMember = "Value";
                    cbbDieuKien.DataSource = columnList;
                    cbbDieuKien.DisplayMember = "Name";
                    cbbDieuKien.ValueMember = "Value";
                }
                catch (Exception e)
                {
                    throw new Exception(GetType() + ".NapCacFieldDKLoc : " + e.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                TimKiemDMKH(false);//Tìm kiếm bằng nút - không phải trường hợp tìm kiếm nhanh ==> false
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm.btnTimKiem_Click ; " + ex.Message);
            }
        }

        BindingSource source = null;
        Boolean flagAllClick = false; //Dùng để nhận biết nút "All" có được nhấn
        private void TimKiemDMKH(bool isRapidSearch)
        {
            if (rbtLocTuDau.Checked && cbbGoiY.Text != "") // IF 1
            {
                try
                {
                    myView = new DataView(tableRoot);
                    //Thiết lập điều kiện lọc cho đối tượng "source"
                    if (isRapidSearch) //Mặc định là chọn theo điều kiện thuộc("$") khi tìm kiếm nhanh
                        source = LocTheoDieuKien(cbbDieuKien.Text, "$", cbbGoiY.Text);
                    else // trường hợp tìm kiếm bình thường
                        source = LocTheoDieuKien(cbbDieuKien.Text, cbbKyHieu.Text, cbbGoiY.Text);
                    //Lọc đối tượng DataView theo điều kiện lọc "source" vừa được thiết lập
                    source.DataSource = myView;
                    dataGridView1.DataSource = source;
                    tempTable = myView.ToTable(); //Lưu lại view đã lọc dùng để lọc tiếp tục
                }
                catch (Exception e)
                {
                    throw new Exception(GetType() + ".TimKiemDMKH if 1 : " + e.Message);
                }
            }
            if (rbtLocTiep.Checked && cbbGoiY.Text != "") //IF 2
            {
                try
                {
                    if (flagAllClick)
                    {
                        tempTable = tableRoot;
                        flagAllClick = false;
                    }
                    if (tempTable != null)
                    {
                        tempView = new DataView(tempTable); //Gán view vừa được lọc vào 1 view khác để tiếp tục lọc theo dk khác
                        //Mặc định là chọn theo điều kiện thuộc("$") khi tìm kiếm nhanh (isRapidSearch)
                        // trường hợp khác tìm kiếm bình thường
                        source = LocTheoDieuKien(cbbDieuKien.Text, isRapidSearch ? "$" : cbbKyHieu.Text, cbbGoiY.Text);
                        source.DataSource = tempView;
                        dataGridView1.DataSource = source;
                        tempTable = tempView.ToTable(); //Lưu lại các giá trị vừa lọc đưa vào bảng tạm để dùng trong trường hợp user muốn lọc tiếp
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(GetType() + ".TimKiemDMKH if 2 : " + e.Message);
                }
            }
        }

        private BindingSource LocTheoDieuKien(string _dkLoc, string _kyHieu, string _bieuThuc)
        {
            BindingSource dataSource = new BindingSource();
            string strFilter = "";
            switch (_kyHieu)
            {
                case "$":
                    {
                        strFilter = _dkLoc + " like '%" + _bieuThuc + "%'"; // BHN in BHN001
                        break;
                    }
                case "=":
                    {
                        strFilter = _dkLoc + " = '" + _bieuThuc + "'"; // BHN in BHN001
                        break;
                    }
                case ">":
                    {
                        strFilter = _dkLoc + " > '" + _bieuThuc + "'";
                        break;
                    }
                case ">=":
                    {
                        strFilter = _dkLoc + " >= '" + _bieuThuc + "'";
                        break;
                    }
                case "<":
                    {
                        strFilter = _dkLoc + " < '" + _bieuThuc + "'";
                        break;
                    }
                case "<=":
                    {
                        strFilter = _dkLoc + " <= '" + _bieuThuc + "'";
                        break;
                    }
                case "<>":
                    {
                        strFilter = _dkLoc + " <> '" + _bieuThuc + "'";
                        break;
                    }
            }
            dataSource.Filter = strFilter;
            return dataSource;
        }

        private void cbbGoiY_Leave(object sender, EventArgs e)
        {
            //Neu chuoi tim kiem chua chua trong Combobox thi them vao, nguoc lai thi khong
            int n = cbbGoiY.Items.IndexOf(cbbGoiY.Text.Trim());
            if (n < 0)
                cbbGoiY.Items.Add(cbbGoiY.Text.Trim());
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = LayTatCaDanhMuc();
                if (dataGridView1.RowCount > 0) dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm.btnTatCa_Click : " + ex.Message);
            }
        }

        public DataView myView = null;
        public DataView tempView = null;
        public DataTable tempTable = null;
        private DataTable LayTatCaDanhMuc(string vSearchFilter = "")
        {
            try
            {
                tableRoot = ThemDuLieuVaoBangChinh(InitStrFilter, vSearchFilter);
            }
            catch (Exception e)
            {
                throw new Exception(GetType() + ".LayTatCaDanhMuc : " + e.Message);
            }
            myView = new DataView(tableRoot);
            //Huuan add: cập nhập lại tempTable
            tempTable = myView.ToTable();
            return tableRoot;
        }

        private void cbbDieuKien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cbbKyHieu.Items.Clear();
                
                AnDauTrongComBoBoxDau();
                ChonGiaTriKhoiTaoCho_cbbKyHieu();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm.cbbDieuKien_TextChanged : " + ex.Message);   
            }
        }

        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;

                    if (_lookupMode == LookupMode.Single)
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            string selectedValue = currentRow.Cells[LookupInfo_F_NAME].Value.ToString().Trim();

                            XuLyEnterChonGiaTri(selectedValue, currentRow.ToDataDictionary());
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else if (_lookupMode == LookupMode.Multi)
                    {
                        var selectedValues = "";
                        _selectedDataList = new List<IDictionary<string, object>>();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                _selectedDataList.Add(row.ToDataDictionary());
                                selectedValues += "," + row.Cells[LookupInfo_F_NAME].Value.ToString().Trim();
                            }
                        }

                        if (selectedValues.Length > 0) selectedValues = selectedValues.Substring(1);
                        _senderText = selectedValues;
                        DialogResult = DialogResult.OK;
                    }
                    else if (_lookupMode == LookupMode.Data)
                    {
                        //Gom Data
                        _selectedDataList = new List<IDictionary<string, object>>();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                _selectedDataList.Add(row.ToDataDictionary());
                            }
                        }
                        //Gọi sự kiện AcceptData
                        OnAccepSelectedtData("idlist", _selectedDataList);
                        DialogResult = DialogResult.OK;
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    SelectCurrentRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    if(_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data) dataGridView1.SelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    if (_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data) dataGridView1.UnSelectAllRow();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("KeyDown" + ex.Message);
            }
        }

        private void XuLyEnterChonGiaTri(string selectedValue, IDictionary<string, object> selectedData)
        {
            if (_lookupMode == LookupMode.Multi)
            {
                _senderText = selectedValue;
                _selectedData = null;
            }
            else
            {
                try
                {
                    _senderText = selectedValue;
                    _selectedData = selectedData;
                }
                catch (Exception e)
                {
                    throw new Exception("XuLyEnterChonGiaTri : " + e.Message);
                }
            }
        }

        public void SelectCurrentRow()
        {
            var cRow = dataGridView1.CurrentRow;
            if (cRow != null)
            {
                if (_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data) cRow.ChangeSelect();
            }
        }

        void a_UpdateSuccessEvent(IDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6Lookup UpdateSuccessEvent " + ex.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if(keyData == (Keys.Control | Keys.F))
                {
                    txtV_Search.Focus();
                    txtV_Search.Select();
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    //Close();
                    DialogResult = DialogResult.Cancel;
                    return true;
                }
                else if (keyData == Keys.Enter)
                {
                    if (txtV_Search.Focused)
                    {
                        if (txtV_Search.Text.Trim() == "")
                        {
                            btnTatCa_Click(null, null);
                        }
                        else if (!txtV_Search.HaveValueChanged && dataGridView1.Rows.Count == 1)
                        {
                            dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(keyData));
                        }
                        else
                        {
                            btnVSearch.PerformClick();
                        }
                    }
                    else
                    {
                        //if(dataGridView1.Focused)
                        {
                            dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(keyData));
                        }
                    }
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    if (dataGridView1.Focused &&
                        (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow == dataGridView1.Rows[0]))
                    {
                        txtV_Search.Focus();
                    }
                }
                else if (keyData == Keys.Down)
                {
                    if (txtV_Search.Focused)
                    {
                        dataGridView1.Focus();
                    }
                    else if (dataGridView1.Focused &&
                        (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow == dataGridView1.Rows[dataGridView1.RowCount-1]))
                    {
                        txtV_Search.Focus();
                    }
                }

                if (keyData == Keys.F3) //Sua
                {
                    if (!LookupInfo.F3)
                    {
                        return false;
                    }

                    if (V6Login.UserRight.AllowEdit("", LookupInfo.TABLE_NAME.ToUpper() + "6"))
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            var g = currentRow.Cells["UID"].Value.ToString();
                            Guid uid = new Guid(g);
                            var keys = new SortedDictionary<string, object>
                            {
                                {"UID", uid}
                            };
                            var f = new FormAddEdit(LookupInfo.TABLE_NAME, V6Mode.Edit, keys, null);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl(this);
                            f.ParentData = _senderParentData;
                            f.UpdateSuccessEvent += a_UpdateSuccessEvent;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                    return true;
                }
                else if (keyData == Keys.F4)
                {
                    if (!LookupInfo.F4)
                    {
                        return false;
                    }

                    if (V6Login.UserRight.AllowAdd("", LookupInfo.TABLE_NAME.ToUpper() + "6"))
                    {
                        DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                        var data = row != null ? row.ToDataDictionary() : null;
                        var f = new FormAddEdit(LookupInfo.TABLE_NAME, V6Mode.Add, null, data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
                        f.ParentData = _senderParentData;
                        if(data == null) f.SetParentData();
                        f.InsertSuccessEvent += a_InsertSuccessEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm CmdKey: " + ex.Message);
                return false;
            }
            if (dataGridView1.Focused)
            dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(keyData));
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, LookupInfo.TABLE_NAME);
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                V6ControlFormHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + LookupInfo.TABLE_NAME, ex);
            }
        }

        void a_InsertSuccessEvent(IDictionary<string, object> dataDic)
        {
            try
            {
                txtV_Search.Text = dataDic[LookupInfo_F_NAME].ToString().Trim();
                btnVSearch.PerformClick();
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(string.Format("{0} {1}.InsertSuccessEvent {2}", V6Login.ClientName, GetType(), LookupInfo.TABLE_NAME), ex, ProductName);
            }
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var currentRow = dataGridView1.Rows[e.RowIndex];
                string selectedValue = currentRow
                    .Cells[LookupInfo_F_NAME].Value.ToString().Trim();
                
                XuLyEnterChonGiaTri(selectedValue, currentRow.ToDataDictionary());
                //Close();
                DialogResult = DialogResult.OK;                
            }
        }

        private void cbbGoiY_KeyUp(object sender, KeyEventArgs e)
        {
            TimKiemDMKH(true); //Trường hợp tìm kiếm trong sự kiện keydown ==> trường hợp tìm kiếm nhanh ==> true
        }        

        #region //====================huuan add===================================
        string _vSearchFilter = "";  //"field1,field2,..."
        /// <summary>
        /// Tạo chuỗi where or nhiều trường.
        /// Trường hợp multi để nguyên để xử lý.
        /// </summary>
        /// <param name="vSearchFields"></param>
        /// <returns></returns>
        private string GenVSearchFilter(string vSearchFields)
        {
            var result = "";
            try
            {
                if ((_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                    && txtV_Search.Text.Contains(","))
                {
                    return txtV_Search.Text.Trim();
                }
                else
                {
                    var tbStruct = V6BusinessHelper.GetTableStruct(LookupInfo.TABLE_NAME);
                    string[] items = vSearchFields.Split(new []{',',';'}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in items)
                    {
                        string ITEM = item.Trim().ToUpper();
                        if (tbStruct.ContainsKey(ITEM) && ObjectAndString.IsNumberType(tbStruct[ITEM].DataType))
                        {
                            decimal vSearchDecimal = ObjectAndString.StringToDecimal(txtV_Search.Text);
                            if (vSearchDecimal != 0) result += " or " + item.Trim() + " = " + vSearchDecimal;
                        }
                        else
                        {
                            result += " or " + item.Trim() + " like N'" + (_filterStart?"":"%") +
                                   txtV_Search.Text.Trim().Replace("'", "''") + "%'";
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
            if (result.Length>3)
            {
                result = result.Substring(3);//bỏ chữ " or" ở đầu chuỗi
            }
            return result;
        }
        
        private void btnVSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string vSearchFields = LookupInfo.F_SEARCH;
                _vSearchFilter = GenVSearchFilter(vSearchFields);
                dataGridView1.DataSource = LayTatCaDanhMuc(_vSearchFilter);
                txtV_Search.ResetFocusText();
                if(dataGridView1.RowCount > 0)
                    dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm.btnVSearch_Click : " + ex.Message);
            }
        }

        private void V6LookupProcForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    Close();
            //}
        }
        #endregion        

        //private void V6LookupProcForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (_senderTextBox is V6LookupProc)
        //    {
        //        var txt = _senderTextBox as V6LookupProc;
        //        //Kiem tra neu gia tri khong hop le thi xoa            
        //        if (!_multiSelect && !txt.ExistRowInTable())
        //        {
        //            txt.Clear();
        //            if (txt.CheckNotEmpty || txt.CheckOnLeave)
        //                txt._lockFocus = true;
        //            else txt._lockFocus = false;
        //        }
        //        else
        //        {
        //            txt._lockFocus = false;
        //        }
        //        txt.SetLooking(false);
        //    }
        //}

        private void btnESC_Click(object sender, EventArgs e)
        {
            //Close();
            DialogResult = DialogResult.Cancel;
        }

        private void btnTimAll_Click(object sender, EventArgs e)
        {
            try
            {
                string vSearchValue = LookupInfo.F_SEARCH;
                _vSearchFilter = GenVSearchFilter(vSearchValue);
                dataGridView1.DataSource = LayTatCaDanhMuc(_vSearchFilter);
                if (dataGridView1.RowCount > 0) dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProcForm.btnTatCa_Click : " + ex.Message);
            }
        }

        private void V6LookupProcForm_Load(object sender, EventArgs e)
        {
            //Chọn dòng đã chọn cho trường hợp multi
            if ((_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                && _vSearchFilter.Contains(","))
            {
                var sss = _vSearchFilter.Split(',');

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (string s in sss)
                    {
                        if (row.Cells[LookupInfo_F_NAME].Value.ToString().Trim().ToUpper() == s.ToUpper())
                        {
                            dataGridView1.Select(row);
                        }
                    }
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

    }
}
