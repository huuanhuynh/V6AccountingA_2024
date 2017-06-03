using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls
{
    /// <summary>
    /// Cần nâng cấp thêm phần lọc từ đầu. Nếu 0 rows thì lọc or nhiều trường.
    /// Nếu còn 1 dòng thì bấm enter ở vsearch là chọn luôn.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view
    /// </summary>
    public partial class V6LookupTextboxForm : Form
    {
        //private readonly TextBox _senderTextBox;
        public string _senderText;
        private IDictionary<string, object> _senderParentData = null; 
        public DataRow selectedDataRow;
        public AldmConfig LookupInfo;
        //private string _table_name, _ma_dm;
        private readonly string LookupInfo_F_NAME;
        private DataTable tableRoot;
        
        internal string InitStrFilter;
        
        
        private HelpProvider _helpProvider1;
        public bool _multiSelect, _filterStart;

        //private string ma_kh = "";
        //private string ma_kho = "ma_kho";
        //private string ma_vt = "ma_vt";

        public V6LookupTextboxForm(IDictionary<string, object> sender, string senderText, AldmConfig lookupInfo, string initStrFilter, string lookupInfo_F_Name,
            bool multiSelect = false, bool filterStart = false)
        {
            _senderParentData = sender;
            _senderText = senderText;
            InitStrFilter = initStrFilter;
            //_senderTextBox = sender;
            LookupInfo = lookupInfo;
            LookupInfo_F_NAME = lookupInfo_F_Name;
            _multiSelect = multiSelect;
            _filterStart = filterStart;

            InitializeComponent();
            Init();
        }
        
        private void Init()
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
                

                //this.HelpButtonClicked +=new CancelEventHandler(V6LookupTextboxForm_HelpButtonClicked);
                _helpProvider1 = new HelpProvider();
                _helpProvider1.SetHelpString(txtV_Search, ChuyenMaTiengViet.ToUnSign("Gõ bất kỳ thông tin bạn nhớ để tìm kiếm!"));
                _helpProvider1.SetHelpString(btnVSearch, ChuyenMaTiengViet.ToUnSign("Tìm..."));
                _helpProvider1.SetHelpString(rbtLocTiep, ChuyenMaTiengViet.ToUnSign("Click chọn để lọc tiếp từ kết quả đã lọc!"));

                _helpProvider1.SetHelpString(dataGridView1, ChuyenMaTiengViet.ToUnSign("Chọn một dòng và nhấn enter để nhận giá trị!"));
                _helpProvider1.SetHelpString(btnTatCa, ChuyenMaTiengViet.ToUnSign("Hiện tất cả."));
                //helpProvider1.SetHelpString(, "Hien tat ca danh muc.");
                toolStripStatusLabel1.Text = "F1-Hướng dẫn, F3-Sửa, F4-Thêm, Enter-Chọn, ESC-Quay ra";
                toolStripStatusLabel2.Text = _multiSelect ? ", Space-Chọn/Bỏ chọn, (Ctrl+A)-Chọn tất cả, (Ctrl+U)-Bỏ chọn tất cả" : "";
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Init : " + ex.Message, "V6LookupTextboxForm");
            }
        }

        public DataTable ThemDuLieuVaoBangChinh(string initFilter, string vSearchFilter)
        {
            if (string.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                throw new Exception("Table_Name!");
            }
            
            try
            {
                if (_multiSelect && vSearchFilter.Contains(","))
                {
                    var tbl = V6BusinessHelper.Select(LookupInfo.TABLE_NAME, "*", initFilter).Data;
                    return tbl;
                }
                else
                {
                    var where = initFilter;
                    if (!string.IsNullOrEmpty(vSearchFilter))
                    {
                        where += " AND (" + vSearchFilter + ")";
                    }
                    var tbl = V6BusinessHelper.Select(LookupInfo.TABLE_NAME, "*", where).Data;
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
                    throw new Exception("StandardDAO.KhoiTaoDataGridView : " + e.Message);
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
                string comboboxValue = cbbDieuKien.SelectedValue.ToString().Trim();
                if (comboboxValue == "char")
                {
                    cbbKyHieu.Items.AddRange(new object[] { "$", "<>" });
                }
                else if (comboboxValue == "money" || comboboxValue == "tinyint" || comboboxValue == "smalldatetime" || comboboxValue.Contains("numeric"))
                {
                    cbbKyHieu.Items.AddRange(new object[] { "=", ">", ">=", "<", "<=", "<>" });
                }
            }
            catch (Exception e)
            {
                throw new Exception("StandardDAO.AnDauTrongComBoBox : " + e.Message);
            }
        }

        private void LayThongTinTieuDe()
        {
            Text = V6Setting.Language == "V" ? LookupInfo.TITLE : LookupInfo.TITLE2;
        }

        private void NapCacFieldDKLoc()
        {
            if (!String.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                try
                {
                    SqlParameter[] plist = { new SqlParameter("@p", LookupInfo.TABLE_NAME) };
                    var ds = V6BusinessHelper.Select("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME,DATA_TYPE",
                        "TABLE_NAME = @p", "", "", plist).Data;

                    cbbDieuKien.DisplayMember = "COLUMN_NAME";
                    cbbDieuKien.ValueMember = "DATA_TYPE";
                    cbbDieuKien.DataSource = ds;
                    cbbDieuKien.DisplayMember = "COLUMN_NAME";
                    cbbDieuKien.ValueMember = "DATA_TYPE";
                }
                catch (Exception e)
                {
                    throw new Exception("StandardDAO.NapCacFieldDKLoc : " + e.Message);
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
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm.btnTimKiem_Click ; " + ex.Message);
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
                    throw new Exception("StandardDAO.TimKiemDMKH if 1 : " + e.Message);
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
                    throw new Exception("StandardDAO.TimKiemDMKH if 2 : " + e.Message);
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
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm.btnTatCa_Click : " + ex.Message);
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
                throw new Exception("StandardDAO.LayTatCaDanhMuc : " + e.Message);
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
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm.cbbDieuKien_TextChanged : " + ex.Message);   
            }
        }

        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;

                    if (_multiSelect)
                    {
                        var selectedValues = "";
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                selectedValues += "," + row.Cells[LookupInfo_F_NAME].Value.ToString().Trim();
                            }
                        }

                        if (selectedValues.Length > 0) selectedValues = selectedValues.Substring(1);

                        _senderText = selectedValues;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            string selectedValue = currentRow.Cells[LookupInfo_F_NAME].Value.ToString().Trim();
                            
                            XuLyEnterChonGiaTri(selectedValue);
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    SelectCurrentRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.A))
                {
                    if(_multiSelect) dataGridView1.SelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    if (_multiSelect) dataGridView1.UnSelectAllRow();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("KeyDown" + ex.Message);
            }
        }

        private void XuLyEnterChonGiaTri(string selectedValue)
        {
            if (_multiSelect)
            {
                _senderText = selectedValue;
                selectedDataRow = null;
            }
            else
            {
                try
                {
                    _senderText = selectedValue;
                    var selectData = V6BusinessHelper.Select(LookupInfo.TABLE_NAME, "*",
                        "[" + LookupInfo_F_NAME + "]=N'" + selectedValue + "'");
                    if (selectData.Data != null && selectData.Data.Rows.Count == 1)
                    {
                        selectedDataRow = selectData.Data.Rows[0];
                    }
                    else
                    {
                        selectedDataRow = null;
                    }
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
                if(_multiSelect) cRow.ChangeSelect();
            }
        }

        void a_UpdateSuccessEvent(SortedDictionary<string, object> data)
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

                if (keyData == Keys.F3) //Sua
                {
                    if (V6Login.UserRight.AllowEdit("", LookupInfo.TABLE_NAME.ToUpper() + "6"))
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                            //string selectedValue = currentRow
                            //    .Cells[LookupInfo_F_NAME].Value.ToString().Trim();
                            var g = currentRow.Cells["UID"].Value.ToString();
                            Guid uid = new Guid(g);
                            var keys = new SortedDictionary<string, object>
                            {
                                //{LookupInfo_F_NAME, selectedValue},
                                {"UID", uid}
                            };
                            var f = new FormAddEdit(V6TableHelper.ToV6TableName(LookupInfo.TABLE_NAME), V6Mode.Edit, keys, null);

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
                    if (V6Login.UserRight.AllowAdd("", LookupInfo.TABLE_NAME.ToUpper() + "6"))
                    {
                        DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                        var data = row != null ? row.ToDataDictionary() : null;
                        var f = new FormAddEdit(V6TableHelper.ToV6TableName(LookupInfo.TABLE_NAME), V6Mode.Add, null, data);
                        
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
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm CmdKey: " + ex.Message);
                return false;
            }
            dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(keyData));
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void a_InsertSuccessEvent(SortedDictionary<string, object> dataDic)
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
                
                XuLyEnterChonGiaTri(selectedValue);
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
                if (_multiSelect && txtV_Search.Text.Contains(","))
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
                string vSearchFields = LookupInfo.F_SEARCH;
                _vSearchFilter = GenVSearchFilter(vSearchFields);
                dataGridView1.DataSource = LayTatCaDanhMuc(_vSearchFilter);

                if(dataGridView1.RowCount > 0)
                    dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm.btnVSearch_Click : " + ex.Message);
            }
        }

        private void V6LookupTextboxForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    Close();
            //}
        }
        #endregion        

        //private void V6LookupTextboxForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (_senderTextBox is V6LookupTextBox)
        //    {
        //        var txt = _senderTextBox as V6LookupTextBox;
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
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextboxForm.btnTatCa_Click : " + ex.Message);
            }
        }

        private void V6LookupTextboxForm_Load(object sender, EventArgs e)
        {
            //Chọn dòng đã chọn cho trường hợp multi
            if (_multiSelect && _vSearchFilter.Contains(","))
            {
                var sss = _vSearchFilter.Split(',');

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (string s in sss)
                    {
                        if (row.Cells[LookupInfo_F_NAME].Value.ToString().Trim().ToUpper() == s.ToUpper())
                        {
                            row.Select();
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
