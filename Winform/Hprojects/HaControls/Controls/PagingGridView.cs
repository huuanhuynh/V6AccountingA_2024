using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using H_Controls.Controls.Lookup.Filter;
using H_DatabaseAccess;
using H_Utility.Helper;

namespace H_Controls.Controls
{
    public partial class PagingGridView : UserControl
    {

        public string TableName { get { return _model.TableName; } set { _model.TableName = value; } }
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DisplayFields { get; set; }
        public string DisplayHeaders { get; set; }

        [Description("Trường sẽ chọn lấy dữ liệu khi lookup.")]
        public string IdField { get; set; }

        public string SortField { get { return _model.SortField; } set { _model.SortField = value; } }
        public bool IsAscending { get { return _model.IsAscending; } set { _model.IsAscending = value; } }

        public string InitFilter;
        public string Where { get;set; }
        
        [DefaultValue(true)]
        public bool AllowAddRows
        {
            get { return dataGridView1.AllowUserToAddRows; }
            set { dataGridView1.AllowUserToAddRows = value; }
        }
        public bool AllowDeletedRows
        {
            get { return dataGridView1.AllowUserToDeleteRows; }
            set { dataGridView1.AllowUserToDeleteRows = value; }
        }
        public bool AllowOrderColumns
        {
            get { return dataGridView1.AllowUserToOrderColumns; }
            set { dataGridView1.AllowUserToOrderColumns = value; }
        }

        public bool Readonly
        {
            get { return dataGridView1.ReadOnly; }
            set { dataGridView1.ReadOnly = value; }
        }

        /// <summary>
        /// Chưa hoàn chỉnh. Cần lấy thêm thông tin khi load data.
        /// </summary>
        public int TotalRow
        {
            get { return _model.TotalRow; }
        }

        public delegate void ColorDataGridViewChangeRow(SortedDictionary<string, object> data);

        public event ColorDataGridViewChangeRow ChangeRow;
        protected virtual void OnChangeRow(SortedDictionary<string, object> data)
        {
            var handler = ChangeRow;
            if (handler != null) handler(data);
        }

        public event DataGridViewCellMouseEventHandler CellDoubleClick;
        protected virtual void OnCellDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            var handler = CellDoubleClick;
            if (handler != null) handler(this, e);
        }


        public PagingGridView()
        {
            InitializeComponent();
            MyInit();
        }
        
        public PagingGridView(string tableName, string sortField)
        {
            InitializeComponent();
            TableName = tableName;
            SortField = sortField;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _model.TableName = TableName;
                _model.PageSize = 20;
                _model.Page = 1;
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            }
            catch
            {
                // ignored
            }
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var data = dataGridView1.CurrentRow.ToDataDictionary();
            OnChangeRow(data);
        }

        /// <summary>
        /// Lấy thông tin rồi gán format. Thường chạy lần đầu?
        /// </summary>
        public void LoadData()
        {
            //GetInfo();
            LoadTable();
            FormatGridview();
        }

        public void Reset()
        {
            LoadTable();
        }

        /// <summary>
        /// lấy dữ liệu lên (cần có thông tin)
        /// Được gọi từ DanhMucControl
        /// </summary>
        public void LoadTable()
        {
            try
            {
                int pageSize = 20;
                if (comboBox1.SelectedIndex >= 0)
                {
                    int.TryParse(comboBox1.Text, out pageSize);
                }
                //else comboBox1.Text = "20";//gây lỗi index changed
                LoadTable0(1, pageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView LoadTable " + ex.Message);
            }
        }

        //private DataTable _data;
        private SelectPageModel _model = new SelectPageModel();
        private void LoadTable0(int page, int size, string sortField, bool ascending)
        {
            try
            {
                if (page < 1) page = 1;

                //var fields = "*";
                //if (DisplayFields != null && DisplayFields.Length > 0)
                //{
                //    fields = DisplayFields;
                //}

                //var  data = DatabaseAccessHelper.SelectPage(HaControlSetting.DBA, TableName, "*", page, size, GetWhere(), sortField, ascending);
                _model.Page = page;
                _model.PageSize = size;
                _model.SortField = sortField;
                _model.IsAscending = ascending;
                _model.Where = GetWhere();
                _model = DatabaseAccessHelper.SelectPage(HaControlSetting.DBA, _model);

                //_currentPage = page;
                //_pagesize = size;
                //_sortField = sortField;
                //_isAscending = ascending;

                ViewResultToForm(_model.Data);
                
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView LoadTable0(args) " + ex.Message);
            }
        }

        /// <summary>
        /// Chỉ gọi 1 lần, gọi nhiều bị lỗi.
        /// </summary>
        private void FormatGridview()
        {
            try
            {
                HControlHelper.FormatGridViewAndHeader(dataGridView1,
                    DisplayFields,
                    null,
                    DisplayHeaders
                    );
            }
            catch (Exception)
            {
                
            }
        }

        public void ViewResultToForm(DataTable data)
        {
            try
            {
                try
                {
                    dataGridView1.DataSource = data;
                    FormatGridview();
                }
                catch (Exception)
                {
                    
                }


                if (dataGridView1.Columns.Contains(_model.SortField))
                {
                    var column = dataGridView1.Columns[_model.SortField];
                    if (column != null)
                        column.HeaderCell.SortGlyphDirection = _model.IsAscending ? SortOrder.Ascending : SortOrder.Descending;
                }

                txtCurrentPage.Text = "" + _model.Page;
                txtCurrentPage.BackColor = Color.White;
                lblTotalPage.Text = string.Format(
                     "Trang {0}/{1} của {2} dòng {3}", _model.Page, _model.TotalPage, _model.TotalRow,
                    string.IsNullOrEmpty(Where) ? "" : string.Format("(Đã lọc của {0} dòng) {1}",
                        _model.TotalRowFull, Where));

                if (_model.Page <= 1)
                {
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                }
                else
                {
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (_model.Page >= _model.TotalPage)
                {
                    btnLast.Enabled = false;
                    btnNext.Enabled = false;
                }
                else
                {
                    btnLast.Enabled = true;
                    btnNext.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView ViewResult " + ex.Message);
            }

        }

        private string GetWhere()
        {
            string where = Where??"";
            string result = "";
            if (string.IsNullOrEmpty(InitFilter))
            {
                result = where;
            }
            else
            {
                if (string.IsNullOrEmpty(where))
                    result = InitFilter;
                else
                    result = string.Format("{0} and({1})", InitFilter, where);
            }
            return result;
        }
        
        private void LoadAtPage(int page)
        {
            try
            {
                if (page < 1) page = 1;
                if (page > _model.TotalPage) page = _model.TotalPage;
                _model.Page = page;
                LoadTable0(_model.Page, _model.PageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView LoadAtPage " + ex.Message);
            }
            
        }

        private void ThayDoiLuongHienThi(int num)
        {
            _model.PageSize = num;
            ReLoad();
        }

        public void First()
        {
            try
            {
                LoadTable0(1, _model.PageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView First " + ex.Message);
            }
        }

        public void Previous()
        {
            try
            {
                LoadTable0(_model.Page -1, _model.PageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView Previous " + ex.Message);
            }
        }

        public void Next()
        {
            try
            {
                if (_model.Page == _model.TotalPage) return;

                LoadTable0(_model.Page + 1, _model.PageSize, _model.SortField, _model.IsAscending);

            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView Next " + ex.Message);
            }
        }

        public void Last()
        {
            try
            {
                LoadTable0(_model.TotalPage, _model.PageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView Last " + ex.Message);
            }
        }

        public void Search()
        {
            try
            {
                //var tStruct = TableStructManager.GetTableStruct(HaControlSetting.DBA, TableName);
                FilterForm f = new FilterForm(dataGridView1, DisplayFields.Split(','));
                f.TopMost = true;
                
                f.FilterOkClick += f_FilterOkClick;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView Search " + ex.Message);
            }
        }

        public void FilterAnyFields(string searchText)
        {
            var query = "";
            if (!string.IsNullOrEmpty(searchText))
            {
                var fields = DisplayFields.Split(',');
                TableStruct structTable = TableStructManager.GetTableStruct(HaControlSetting.DBA, TableName);
                IDictionary<string, object> keys = new SortedDictionary<string, object>();
                foreach (string field in fields)
                {
                    keys.Add(field.ToUpper(), searchText);
                }
                query = SqlGenerator.GenWhere(structTable, keys, "like", false);
            }
            //if (chkSearchByDate.Checked)
            //{
            //    query = string.Format("({0}) And {1} And {2}", query,
            //        SqlGenerator.GenWhere2(new Dictionary<string, object>() { { "NGAYTAO", dateFrom.Value } }, ">="),
            //        SqlGenerator.GenWhere2(new Dictionary<string, object>() { { "NGAYTAO", dateFrom.Value } }, "<="));
            //}

            Filter(query);
        }

        void f_FilterOkClick(string query)
        {
            Filter(query);
        }

        public void Filter(string query)
        {
            Where = query;
            LoadTable();
        }

        public void ReLoad()
        {
            try
            {
                LoadTable0(_model.Page, _model.PageSize, _model.SortField, _model.IsAscending);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("PagingGridView Reload " + ex.Message);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Where = "";
            First();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OnCellDoubleClick(e);
        }

        private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int page;
                int.TryParse(txtCurrentPage.Text, out page);
                LoadAtPage(page);

            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var column = dataGridView1.Columns[e.ColumnIndex];
                var columnName = column.DataPropertyName;
                //if (columnName == "RowNum") return;

                foreach (DataGridViewColumn column0 in dataGridView1.Columns)
                {
                    if (column0 != column) column0.HeaderCell.SortGlyphDirection = SortOrder.None;
                }

                _model.IsAscending = column.HeaderCell.SortGlyphDirection != SortOrder.Ascending;
                _model.SortField = column.DataPropertyName;

                LoadTable0(_model.Page, _model.PageSize, _model.SortField, _model.IsAscending);

            }
            catch
            {
                // ignored
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var num = 20;
            int.TryParse(comboBox1.Text, out num);
            if (num <= 0) num = 20;
            ThayDoiLuongHienThi(num);
        }

        
    }
}
