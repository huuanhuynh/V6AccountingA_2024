using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.Filter
{
    /// <summary>
    /// Cần nâng cấp thêm phần lọc từ đầu. Nếu 0 rows thì lọc or nhiều trường.
    /// Nếu còn 1 dòng thì bấm enter ở vsearch là chọn luôn.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view
    /// </summary>
    public partial class FilterView : Form
    {
        //public string CONSTRING = "";
        public string DataField { get; set; }
        
        private string Ma_dm;
        
        //Form _anonymousForm;
        private DataTable _data;
        internal string InitStrFilter;
        private readonly V6ColorTextBox _senderTextBox;

        private HelpProvider _helpProvider1;
        public bool MultiSeletion { get; set; }

        public delegate void ChoseDelegate(DataGridViewRow data);

        public event ChoseDelegate ChoseEvent;
        public string  Report_GRDSV1 = "";
        public string  Report_GRDFV1 = "";
        public string  Report_GRDHV_V1 = "";
         public string  Report_GRDHE_V1 = "";

        public FilterView(DataTable data, string dataField, string maDm, V6ColorTextBox sender, string initStrFilter)
        {            
            DataField = dataField.Replace("'","''");
            Ma_dm = maDm;
            
                
            var aldm_data = V6BusinessHelper.Select("aldm","GRDS_V1,GRDF_V1,GRDHV_V1,GRDHE_V1","ma_dm='" + maDm + "'").Data;
               if (aldm_data != null && aldm_data.Rows.Count > 0)
             {
                
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
                txtV_Search.Text = _senderTextBox.Text;
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

        private DataView myView;
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

        
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    if (MultiSeletion)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if(row.IsSelect())
                                OnChoseEvent(row);
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else if (dataGridView1.SelectedCells.Count > 0)
                    {
                        var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                        string selectedValue = currentRow.Cells[DataField].Value.ToString().Trim();
                        _senderTextBox.Text = selectedValue;
                        _senderTextBox.Tag = currentRow;
                        //Close();
                        DialogResult = DialogResult.OK;
                        _senderTextBox.SetLooking(false);
                    }
                }
                else if (e.KeyCode == Keys.Space && MultiSeletion)
                {
                    var cRow = dataGridView1.CurrentRow;
                    if (cRow != null)
                    {
                        cRow.ChangeSelect();
                    }
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }
        
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var currentRow = dataGridView1.Rows[e.RowIndex];
                string selectedValue = currentRow.Cells[DataField].Value.ToString().Trim();
                _senderTextBox.Text = selectedValue;
                _senderTextBox.Tag = currentRow;
                //Close();
                DialogResult = DialogResult.OK;
                //Dispose();
                _senderTextBox.SetLooking(false);
            }
        }

        #region //====================huuan add===================================
        string _filterString = "";  //"field1,field2,..."
        private string GenFilterString(string fields)
        {
            var result = "";
            try
            {
                string[] items = fields.Split(',');
                foreach (string item in items)
                {
                    result += " or " + item.Trim() + " like '%" +txtV_Search.Text.Trim()+ "%'";
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

        private void txtVSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtV_Search.Text.Trim() == "")
            {
                btnVSearch.PerformClick();
            }
            else if (dataGridView1.Rows.Count == 1)
            {
                dataGridView1_KeyDown(dataGridView1, new KeyEventArgs(e.KeyData));
            }
            else
            {
                btnVSearch.PerformClick();
            }

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

        private void Standard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    Close();
            //}
        }
        #endregion        

        private void Standard_FormClosing(object sender, FormClosingEventArgs e)
        {
            _senderTextBox.SetLooking(false);
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        protected virtual void OnChoseEvent(DataGridViewRow data)
        {
            var handler = ChoseEvent;
            if (handler != null) handler(data);
        }
    }
}
