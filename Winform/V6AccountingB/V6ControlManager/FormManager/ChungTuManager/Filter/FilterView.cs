using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.Filter
{
    /// <summary>
    /// Form chọn dữ liệu có filter. Nếu không gán dữ liệu cho control nào thì gửi sender bằng null hoặc new V6ColorTextBox.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view.
    /// </summary>
    public partial class FilterView : V6Form
    {
        //public string CONSTRING = "";
        public string DataField { get; set; }
        public IDictionary<string, object> SelectedRowData = null; 
        
        private string Ma_dm;
        
        //Form _anonymousForm;
        private DataTable _data;
        internal string InitStrFilter;
        private readonly V6ColorTextBox _senderTextBox;
        private string SenderTextBox_Text { get { return _senderTextBox == null ? "" : _senderTextBox.Text; } }

        private HelpProvider _helpProvider1;
        public bool MultiSeletion { get; set; }

        public delegate void ChoseDelegate(DataGridViewRow data);

        public event ChoseDelegate ChoseEvent;
        public string  Report_GRDSV1 = "";
        public string  Report_GRDFV1 = "";
        public string  Report_GRDHV_V1 = "";
        public string  Report_GRDHE_V1 = "";

        private DataView myView;
        public DataView ViewData { get { return myView; } }

        /// <summary>
        /// <para>Form chọn dữ liệu có filter. Nếu không gán dữ liệu cho control nào thì gửi sender bằng null hoặc new V6ColorTextBox.</para>
        /// <para>Kiểm tra số lượng dữ liệu hiển thị = ViewData.Count.</para>
        /// </summary>
        /// <param name="data">Dữ liệu gửi vào.</param>
        /// <param name="dataField">Trường lấy dữ liệu.</param>
        /// <param name="maDm"></param>
        /// <param name="sender">Control nhận dữ liệu</param>
        /// <param name="initStrFilter">Lọc dữ liệu ban đầu. Không có thì truyền null hoặc rỗng.</param>
        public FilterView(DataTable data, string dataField, string maDm, V6ColorTextBox sender, string initStrFilter)
        {
            if (initStrFilter == null) initStrFilter = "";
            DataField = dataField.Replace("'", "''");
            Ma_dm = maDm;


            var aldm_data =
                V6BusinessHelper.Select("aldm", "GRDS_V1,GRDF_V1,GRDHV_V1,GRDHE_V1,Title,Title2", "ma_dm='" + maDm + "'").Data;
            if (aldm_data != null && aldm_data.Rows.Count > 0)
            {
                Text = aldm_data.Rows[0][V6Setting.IsVietnamese?"Title":"Title2"].ToString().Trim();
                Report_GRDSV1 = aldm_data.Rows[0][0].ToString().Trim();
                Report_GRDFV1 = aldm_data.Rows[0][1].ToString().Trim();
                Report_GRDHV_V1 = aldm_data.Rows[0][2].ToString().Trim();
                Report_GRDHE_V1 = aldm_data.Rows[0][3].ToString().Trim();
            }

            _data = data;
            InitStrFilter = initStrFilter;
            _senderTextBox = sender;
            InitializeComponent();
            Init();
        }



        private void Init()
        {
            try
            {
                //DialogResult = DialogResult.No;
                txtV_Search.Text = SenderTextBox_Text;
                myView = new DataView(_data);
                dataGridView1.DataSource = myView;
                FormatDataGridView();

                //Tuanmh 21/08/2016
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1,
                        V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);

                ViewFilter();
                //vMaFile = LstConfig[1];
                
                _helpProvider1 = new HelpProvider();
                _helpProvider1.SetHelpString(txtV_Search, "Gõ bất kỳ thông tin bạn nhớ để tìm kiếm!");
                _helpProvider1.SetHelpString(btnVSearch, "Tìm...");
                //_helpProvider1.SetHelpString(rbtLocTiep, "Click chọn để lọc tiếp từ kết quả đã lọc!");

                _helpProvider1.SetHelpString(dataGridView1, "Chọn một dòng và nhấn enter để nhận giá trị!");
                //_helpProvider1.SetHelpString(btnTatCa, "Hiện tất cả.");
                //helpProvider1.SetHelpString(, "Hien tat ca danh muc.");
                txtV_Search.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Init : " + ex.Message, "FilterView");
            }
        }

        private void FormatDataGridView()
        {
            try
            {
                var columns = (from DataGridViewColumn column in dataGridView1.Columns select column.DataPropertyName).ToList();
                var FieldsHeaderDictionary = CorpLan2.GetFieldsHeader(columns, V6Setting.Language);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    var FIELD = column.DataPropertyName.ToUpper();
                    if (FieldsHeaderDictionary.ContainsKey(FIELD))
                        column.HeaderText = FieldsHeaderDictionary[FIELD];
                }
                
                dataGridView1.ShowColumnsAldm(Ma_dm);
                dataGridView1.HideColumnsAldm(Ma_dm);
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FilterView FormatDataGridView: " + ex.Message);
            }
        }

        private void ViewFilter()
        {
            _filterString = GenFilterString(DataField);
            try
            {
                myView.RowFilter = _filterString;
                dataGridView1.Refresh();
            }
            catch (Exception e)
            {
                throw new Exception("ViewFilter: " + e.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    if (txtV_Search.Focused && dataGridView1.Rows.Count != 1)
                    {
                        if (txtV_Search.Text.Trim() == "")
                        {
                            btnVSearch.PerformClick();
                        }
                        else
                        {
                            btnVSearch.PerformClick();
                        }
                        return true;
                    }

                    //e.Handled = true;
                    if (MultiSeletion)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                                OnChoseEvent(row);
                        }

                        if (dataGridView1.CurrentRow != null)
                        {
                            SelectedRowData = dataGridView1.CurrentRow.ToDataDictionary();
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else if (dataGridView1.SelectedCells.Count > 0)
                    {
                        var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                        object selectedValue = currentRow.Cells[DataField].Value;
                        if (_senderTextBox != null)
                        {
                            V6ControlFormHelper.SetControlValue(_senderTextBox, selectedValue);
                            _senderTextBox.Tag = currentRow;
                        }

                        if (dataGridView1.CurrentRow != null)
                        {
                            SelectedRowData = dataGridView1.CurrentRow.ToDataDictionary();
                        }
                        DialogResult = DialogResult.OK;
                        if (_senderTextBox != null) _senderTextBox.SetLooking(false);
                    }
                    return true;
                }
                else if (keyData == Keys.Space && MultiSeletion)
                {
                    var cRow = dataGridView1.CurrentRow;
                    if (cRow != null)
                    {
                        cRow.ChangeSelect();
                    }
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    return true;
                }
                else
                {
                    return base.DoHotKey0(keyData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return false;
        }
        
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var currentRow = dataGridView1.Rows[e.RowIndex];
                object selectedValue = currentRow.Cells[DataField].Value;
                if (_senderTextBox != null)
                {
                    V6ControlFormHelper.SetControlValue(_senderTextBox, selectedValue);
                    _senderTextBox.Tag = currentRow;
                }

                if (dataGridView1.CurrentRow != null)
                {
                    SelectedRowData = dataGridView1.CurrentRow.ToDataDictionary();
                }
                DialogResult = DialogResult.OK;
                if (_senderTextBox != null) _senderTextBox.SetLooking(false);
            }
        }

        #region //====================huuan add===================================
        string _filterString = "";  //"field1,field2,..."
        /// <summary>
        /// Tạo chuỗi filter từ InitFilter và chuỗi nhập + fields
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private string GenFilterString(string fields)
        {
            var result = "";
            if (txtV_Search.Text.Trim() == "") return "";

            try
            {
                string[] items = fields.Split(',');
                foreach (string item in items)
                {
                    var column = dataGridView1.Columns[item];
                    if (column != null)
                    {
                        var valueType = column.ValueType;
                        if (ObjectAndString.IsStringType(valueType))
                        {
                            result += " or " + item.Trim() + " like  '%" + txtV_Search.Text.Trim() + "%'";//dùng cho RowFilter không dùng N'value'
                        }
                        else if (ObjectAndString.IsNumberType(valueType))
                        {
                            result += " or " + item.Trim() + " = " + ObjectAndString.StringToDecimal(txtV_Search.Text.Trim()) ;
                        }
                        else if (ObjectAndString.IsDateTimeType(valueType))
                        {
                            result += " or " + item.Trim() + " = '" + ObjectAndString.StringToDate(txtV_Search.Text.Trim()).ToString("yyyyMMdd") + "'";
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
                if (!string.IsNullOrEmpty(InitStrFilter))
                    result = string.Format("{0} And ({1})", InitStrFilter, result);
            }
            else
            {
                result = InitStrFilter;
            }
            return result;
        }
        
        private void btnVSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewFilter();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnVSearch_Click: " + ex.Message, "FilterView");
            }
        }

        #endregion        

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_senderTextBox != null) _senderTextBox.SetLooking(false);
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        protected virtual void OnChoseEvent(DataGridViewRow data)
        {
            var handler = ChoseEvent;
            if (handler != null) handler(data);
        }
    }
}
