using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls
{
    public enum LookupMode
    {
        /// <summary>
        /// Chọn 1 dòng khi lookup, nhận về 1 mã trong senderTextBox.
        /// </summary>
        Single = 0,
        /// <summary>
        /// Chọn nhiều dòng khi lookup, nhận về danh sách mã trong senderTextBox.
        /// </summary>
        Multi = 1,
        /// <summary>
        /// Chọn nhiều dòng dữ liệu khi lookup, nhận về dữ liệu trong sự kiện.
        /// </summary>
        Data = 2
    }
    /// <summary>
    /// Cần nâng cấp thêm phần lọc từ đầu. Nếu 0 rows thì lọc or nhiều trường.
    /// Nếu còn 1 dòng thì bấm enter ở vsearch là chọn luôn.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view
    /// </summary>
    public partial class V6VvarTextBoxForm : V6Form
    {
        public V6lookupConfig _config;
        
        //public string VVar;
        V6VvarTextBoxFormDAO _standDao;
        internal string InitStrFilter;
        private readonly V6VvarTextBox _senderTextBox;

        private HelpProvider _helpProvider1;
        public LookupMode _lookupMode;
        public bool _filterStart;
        public SortedDictionary<string, string> _f2_selected = new SortedDictionary<string, string>();

        /// <summary>
        /// Sự kiện xảy ra khi nhận ở lookupMode == Data.
        /// </summary>
        public event DataSelectHandler AcceptSelectedtData;
        protected virtual void OnAccepSelectedtData(string idlist, List<IDictionary<string, object>> datalist)
        {
            var handler = AcceptSelectedtData;
            if (handler != null) handler(idlist, datalist);
        }


        public V6VvarTextBoxForm(V6VvarTextBox sender, V6lookupConfig lookupInfo, string initStrFilter,
            LookupMode lookupMode = LookupMode.Single, bool filterStart = false)
        {            
            _config = lookupInfo;
            InitStrFilter = initStrFilter;
            _senderTextBox = sender;
            _lookupMode = lookupMode;
            _filterStart = filterStart;

            InitializeComponent();
            Init();
        }
        
        private void Init()
        {
            try
            {
                txtV_Search.Text = _senderTextBox.Text;
                
                _vSearchFilter = GenVSearchFilter(_config.V_Search);
                //vMaFile = LstConfig[1];
                _standDao = new V6VvarTextBoxFormDAO(this, _senderTextBox, _vSearchFilter);
                _standDao.NapCacFieldDKLoc();
                cbbDieuKien.SelectedIndex = 0;
                _standDao.LayThongTinTieuDe();
                _standDao.AnDauTrongComBoBoxDau();
                _standDao.ChonGiaTriKhoiTaoCho_cbbKyHieu();
                _standDao.KhoiTaoDataGridView();

                //this.HelpButtonClicked +=new CancelEventHandler(Form_HelpButtonClicked);
                _helpProvider1 = new HelpProvider();
                _helpProvider1.SetHelpString(txtV_Search, ChuyenMaTiengViet.ToUnSign("Gõ bất kỳ thông tin bạn nhớ để tìm kiếm!"));
                _helpProvider1.SetHelpString(btnVSearch, ChuyenMaTiengViet.ToUnSign("Tìm..."));
                _helpProvider1.SetHelpString(rbtLocTiep, ChuyenMaTiengViet.ToUnSign("Click chọn để lọc tiếp từ kết quả đã lọc!"));

                _helpProvider1.SetHelpString(dataGridView1, ChuyenMaTiengViet.ToUnSign("Chọn một dòng và nhấn enter để nhận giá trị!"));
                _helpProvider1.SetHelpString(btnTatCa, ChuyenMaTiengViet.ToUnSign("Hiện tất cả."));
                //helpProvider1.SetHelpString(, "Hien tat ca danh muc.");
                toolStripStatusLabel1.Text = V6Setting.IsVietnamese ? "F1-Hướng dẫn, F2-Xem, F3-Sửa, F4-Thêm, Enter-Chọn, ESC-Quay ra" : "F1-Help, F2-View, F3-Edit, F4-Add, Enter-Choose, ESC-Back";
                toolStripStatusLabel2.Text = _lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data ? ", Space-Chọn/Bỏ chọn, (Ctrl+A)-Chọn tất cả, (Ctrl+U)-Bỏ chọn tất cả" : "";
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".Init", ex);
            }
        }        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                _standDao.TimKiemDMKH(false);//Tìm kiếm bằng nút - không phải trường hợp tìm kiếm nhanh ==> false
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".btnTimKiem_Click", ex);
            }
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
                dataGridView1.DataSource = _standDao.LayTatCaDanhMuc();
                ApplyF2Selected();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".btnTatCa_Click", ex);
            }
        }

        private void cbbDieuKien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cbbKyHieu.Items.Clear();
                V6VvarTextBoxFormDAO standDao = new V6VvarTextBoxFormDAO(this);
                standDao.AnDauTrongComBoBoxDau();
                standDao.ChonGiaTriKhoiTaoCho_cbbKyHieu();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".cbbDieuKien_TextChanged", ex);
            }
        }

        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;

                    if(_lookupMode == LookupMode.Single)
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            string selectedValue = currentRow
                                .Cells[_config.vValue].Value.ToString().Trim();
                            _standDao.XuLyEnterChonGiaTri(selectedValue, _senderTextBox);
                            Close();

                            _senderTextBox.SetLooking(false);
                        }
                    }
                    else if (_lookupMode == LookupMode.Multi)
                    {
                        var selectedValues = "";
                        //foreach (DataGridViewRow row in dataGridView1.Rows)
                        //{
                        //    if (row.IsSelect())
                        //    {
                        //        selectedValues += "," + row.Cells[_config.vValue].Value.ToString().Trim();
                        //    }
                        //}

                        selectedValues = "";
                        foreach (KeyValuePair<string, string> item in _f2_selected)
                        {
                            selectedValues += "," + item.Value;
                        }

                        if (selectedValues.Length > 0) selectedValues = selectedValues.Substring(1);

                        _senderTextBox.Text = selectedValues;
                        Close();
                    }
                    else if (_lookupMode == LookupMode.Data)
                    {
                        //Gom Data
                        List<IDictionary<string, object>> datalist = new List<IDictionary<string, object>>();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                datalist.Add(row.ToDataDictionary());
                            }
                        }
                        //Gọi sự kiện AcceptData
                        //_selectedDataList = datalist;
                        OnAccepSelectedtData("idlist", datalist);
                        Close();
                    }
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                {
                    _standDao.ThietLapGridViewKhiNhanLeft_Right();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    _standDao.ThietLapGridViewKhiNhanUp_Down();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    SelectedValueWithCheckBox(dataGridView1);
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    if (_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                    {
                        dataGridView1.SelectAllRow();
                    }
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    if (_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                    {
                        dataGridView1.UnSelectAllRow();
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException("KeyDown", ex);
            }
        }

        public void SelectedValueWithCheckBox(DataGridView gridView)
        {
            var cRow = gridView.CurrentRow;
            if (cRow != null)
            {
                if (_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                {
                    cRow.ChangeSelect();
                    //var cRow_value = cRow.Cells[_config.vValue].Value.ToString().Trim();
                    //var cRow_key = cRow_value.ToUpper();
                    //if (cRow.IsSelect())
                    //{
                    //    _f2_selected[cRow_key] = cRow_value;
                    //}
                    //else if (_f2_selected.ContainsKey(cRow_key))
                    //{
                    //    _f2_selected.Remove(cRow_key);
                    //}
                }


                //Đổi giá trị x hoặc "" cho cột đầu tiên nếu sử dụng
                var ch1 = (DataGridViewTextBoxCell)cRow.Cells[0];
                if (ch1.Value == null)
                    ch1.Value = "";

                switch (ch1.Value.ToString())
                {
                    case "X":
                    {
                        ch1.Value = "";
                        //string valueToDelete = dgr.CurrentRow.Cells[1].Value.ToString();
                        //if (LISTVALUE.Contains(valueToDelete))
                        //    LISTVALUE.Remove(valueToDelete);
                        break;
                    }
                    case "":
                    {
                        //if (categoryName == "DMNHVT") //Vì Bảng DMNHVT có thêm cột loại đứng ở vị trí [1] và mã ở vị trí [2]
                        //{
                        //    ch1.Value = "X";
                        //    LISTVALUE.Add(dgr.CurrentRow.Cells[2].Value.ToString());
                        //}
                        //else
                        //{
                        ch1.Value = "X";
                        //LISTVALUE.Add(dgr.CurrentRow.Cells[1].Value.ToString());
                        //}
                        break;
                    }
                }
            }
        }

        void f_UpdateSuccessEvent(IDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
                _senderTextBox.Reset();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".a_UpdateSuccessEvent", ex);
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
                    Close();
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
                        else if (dataGridView1.Rows.Count == 1)
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

                if (keyData == Keys.F2)
                {
                    //if (!LstConfig.F2)
                    //{
                    //    return false;
                    //}

                    if (_config.vMa_file.ToUpper() == "ALTK" || _config.vMa_file.ToUpper() == "ALTK0")
                        return true;

                    if (V6Login.UserRight.AllowView("", _config.vMa_file.ToUpper() + "6"))
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            string selectedValue = currentRow
                                .Cells[_config.vValue].Value.ToString().Trim();
                            var g = currentRow.Cells["UID"].Value.ToString();
                            Guid uid = new Guid(g);
                            var keys = new SortedDictionary<string, object>
                            {
                                {_config.vValue, selectedValue},
                                {"UID", uid}
                            };
                            var f = _config.V6TableName == V6TableName.Notable
                                ? new FormAddEdit(_config.vMa_file, V6Mode.View, keys, null)
                                : new FormAddEdit(_config.V6TableName, V6Mode.View, keys, null);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl();
                            f.ParentData = _senderTextBox.ParentData;
                            //f.UpdateSuccessEvent += f_UpdateSuccessEvent;
                            f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                    return true;
                }
                else if (keyData == Keys.F3) //Sua
                {
                    if (!_config.F3)
                    {
                        return false;
                    }

                    if (_config.vMa_file.ToUpper() == "ALTK" || _config.vMa_file.ToUpper() == "ALTK0")
                        return true;

                    if (V6Login.UserRight.AllowEdit("", _config.vMa_file.ToUpper() + "6"))
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            string selectedValue = currentRow
                                .Cells[_config.vValue].Value.ToString().Trim();
                            var g = currentRow.Cells["UID"].Value.ToString();
                            Guid uid = new Guid(g);
                            var keys = new SortedDictionary<string, object>
                            {
                                {_config.vValue, selectedValue},
                                {"UID", uid}
                            };
                            var f = _config.V6TableName == V6TableName.Notable
                                ? new FormAddEdit(_config.vMa_file, V6Mode.Edit, keys, null)
                                : new FormAddEdit(_config.V6TableName, V6Mode.Edit, keys, null);
                            f.AfterInitControl += f_AfterInitControl;
                            f.InitFormControl();
                            f.ParentData = _senderTextBox.ParentData;
                            f.UpdateSuccessEvent += f_UpdateSuccessEvent;
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
                    if (!_config.F4)
                    {
                        return false;
                    }

                    if (_config.vMa_file.ToUpper() == "ALTK" || _config.vMa_file.ToUpper() == "ALTK0")
                        return true;

                    if (V6Login.UserRight.AllowAdd("", _config.vMa_file.ToUpper() + "6"))
                    {
                        DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                        var data = row != null ? row.ToDataDictionary() : null;
                        var f = _config.V6TableName == V6TableName.Notable
                            ? new FormAddEdit(_config.vMa_file, V6Mode.Add, null, data)
                            : new FormAddEdit(_config.V6TableName, V6Mode.Add, null, data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl();
                        f.ParentData = _senderTextBox.ParentData;
                        if(data == null) f.SetParentData();
                        f.InsertSuccessEvent += f_InsertSuccessEvent;
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
                V6ControlFormHelper.ShowErrorException(GetType() + ".ProcessCmdKey", ex);
                return false;
            }
            dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(keyData));
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, _config.V6TableName == V6TableName.Notable?_config.vMa_file:_config.V6TableName.ToString());
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                V6ControlFormHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls ", ex);
            }
        }

        void f_InsertSuccessEvent(IDictionary<string, object> dataDic)
        {
            try
            {
                txtV_Search.Text = dataDic[_config.vValue.ToUpper()].ToString().Trim();
                btnVSearch.PerformClick();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".a_InsertSuccessEvent", ex);
            }
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var currentRow = dataGridView1.Rows[e.RowIndex];
                string selectedValue = currentRow
                    .Cells[_config.vValue].Value.ToString().Trim();
                _standDao.XuLyEnterChonGiaTri(selectedValue, _senderTextBox);
                Close();
            }
        }

        private void cbbGoiY_KeyUp(object sender, KeyEventArgs e)
        {
            _standDao.TimKiemDMKH(true); //Trường hợp tìm kiếm trong sự kiện keydown ==> trường hợp tìm kiếm nhanh ==> true
        }        

        #region //====================huuan add===================================
        string _vSearchFilter = "";
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
                    string[] items = vSearchFields.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in items)
                    {
                        result += " or " + item.Trim() + " like N'" + (_filterStart?"":"%") +
                                  txtV_Search.Text.Trim().Replace("'", "''") + "%'";
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
                string vSearchFields = _config.V_Search;
                _vSearchFilter = GenVSearchFilter(vSearchFields);
                dataGridView1.DataSource = _standDao.LayTatCaDanhMuc(_vSearchFilter);
                ApplyF2Selected();

                if(_standDao.tableRoot.Rows.Count>0)
                    dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".btnVSearch_Click", ex);
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    Close();
            //}
        }
        #endregion        

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Kiem tra neu gia tri khong hop le thi xoa            
            if (_lookupMode == LookupMode.Single && !_senderTextBox.ExistRowInTable())
            {
                _senderTextBox.Clear();
                if (_senderTextBox.CheckNotEmpty || _senderTextBox.CheckOnLeave)
                    _senderTextBox._lockFocus = true;
                else _senderTextBox._lockFocus = false;
            }
            else if (_lookupMode == LookupMode.Multi)
            {
                _senderTextBox._lockFocus = false;
            }
            else if (_lookupMode == LookupMode.Data)
            {
                //DoNothing();
            }

            _senderTextBox.SetLooking(false);
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTimAll_Click(object sender, EventArgs e)
        {
            try
            {
                string vSearchFields = _config.V_Search;
                _vSearchFilter = GenVSearchFilter(vSearchFields);
                dataGridView1.DataSource = _standDao.LayTatCaDanhMuc(_vSearchFilter);
                ApplyF2Selected();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".btnTatCa_Click", ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //Chọn dòng cho trường hợp multi
            if ((_lookupMode == LookupMode.Multi || _lookupMode == LookupMode.Data)
                && _vSearchFilter.Contains(","))
            {
                var sss = _vSearchFilter.Split(',');
                foreach (string s in sss)
                {
                    _f2_selected[s.ToUpper()] = s;
                }

                ApplyF2Selected();
            }
        }

        private void ApplyF2Selected()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (_f2_selected.ContainsKey(row.Cells[_config.vValue].Value.ToString().Trim().ToUpper()))
                {
                    row.Select();
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void dataGridView1_RowSelectChanged(object sender, DataGridViewRow cRow)
        {
            if (_lookupMode == LookupMode.Multi)// || _lookupMode == LookupMode.Data)
            {
                //cRow.ChangeSelect();
                var cRow_value = cRow.Cells[_config.vValue].Value.ToString().Trim();
                var cRow_key = cRow_value.ToUpper();
                if (cRow.IsSelect())
                {
                    _f2_selected[cRow_key] = cRow_value;
                }
                else if (_f2_selected.ContainsKey(cRow_key))
                {
                    _f2_selected.Remove(cRow_key);
                }
            }
        }
        
    }
}
