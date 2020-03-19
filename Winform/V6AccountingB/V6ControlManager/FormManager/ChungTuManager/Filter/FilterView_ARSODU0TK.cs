using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ChungTuManager.Filter
{
    /// <summary>
    /// Cần nâng cấp thêm phần lọc từ đầu. Nếu 0 rows thì lọc or nhiều trường.
    /// Nếu còn 1 dòng thì bấm enter ở vsearch là chọn luôn.
    /// Có thể cần nâng cấp phần phân trang giống danh mục view
    /// </summary>
    public partial class FilterView_ARSODU0TK : Form
    {
        private V6InvoiceBase Invoice;
        internal string InitStrFilter;
        private string _sttRec;
        private readonly V6ColorTextBox _senderTextBox;

        public bool MultiSeletion { get; set; }

        public delegate void ChoseDelegate(DataGridViewRow data);

        public event ChoseDelegate ChoseEvent;
        public string Report_GRDSV1 = "";
        public string Report_GRDFV1 = "";
        public string Report_GRDHV_V1 = "";
        public string Report_GRDHE_V1 = "";
        public FilterView_ARSODU0TK(V6InvoiceBase invoice, V6ColorTextBox sender, string sttRec, string maDvcs, string initStrFilter)
        {
            InitializeComponent();

            Invoice = invoice;
            _sttRec = sttRec;
            txtMaDVCS.Text = maDvcs;
            InitStrFilter = initStrFilter;
            _senderTextBox = sender;
            Init();
        }

        private void Init()
        {
            try
            {
                var aldm_data = V6BusinessHelper.Select("aldm", "GRDS_V1,GRDF_V1,GRDHV_V1,GRDHE_V1,Title,Title2", "ma_dm='" + "ARSODU0TK" + "'").Data;
                if (aldm_data != null && aldm_data.Rows.Count > 0)
                {
                    Text = aldm_data.Rows[0][V6Setting.IsVietnamese ? "Title" : "Title2"].ToString().Trim();
                    Report_GRDSV1 = aldm_data.Rows[0][0].ToString().Trim();
                    Report_GRDFV1 = aldm_data.Rows[0][1].ToString().Trim();
                    Report_GRDHV_V1 = aldm_data.Rows[0][2].ToString().Trim();
                    Report_GRDHE_V1 = aldm_data.Rows[0][3].ToString().Trim();
                }

                taiKhoan.VvarTextBox.Text = "131";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Init : " + ex.Message, "ChungTuManager FilterView_ARSODU0TK");
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

                dataGridView1.ShowColumnsAldm("ARS20");
                dataGridView1.HideColumnsAldm("ARS20");

                //Tuanmh 21/08/2016
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, Report_GRDSV1, Report_GRDFV1,
                        V6Setting.IsVietnamese ? Report_GRDHV_V1 : Report_GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FilterView FormatDataGridView: " + ex.Message);
            }
        }

        private void SearchData()
        {
            _filterString = GenFilterString();
            try
            {
                DateTime ngay_ct = v6ColorDateTimePick2.Date;
                var alct0 = Invoice.GetSoDu0TK_All_Cust(_sttRec, txtMaDVCS.Text.Trim(), ngay_ct, _filterString);
                dataGridView1.DataSource = alct0;
                FormatDataGridView();

                dataGridView1.Refresh();
            }
            catch (Exception e)
            {
                throw new Exception("SearchData: " + e.Message);
            }
        }

        public List<IDictionary<string, object>> SelectedDataList;
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    SelectedDataList = new List<IDictionary<string, object>>();
                    if (MultiSeletion)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                SelectedDataList.Add(row.ToDataDictionary());
                                OnChoseEvent(row);
                            }
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else if (dataGridView1.SelectedCells.Count > 0)
                    {
                        var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                        SelectedDataList.Add(currentRow.ToDataDictionary());
                        string selectedValue = currentRow.Cells[_senderTextBox.AccessibleName].Value.ToString().Trim();
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
                else if(e.KeyData == (Keys.Control | Keys.A) && MultiSeletion)
                {
                    e.Handled = true;
                    dataGridView1.SelectAllRow();
                }
                //else if (e.KeyData == (Keys.Control | Keys.U))
                //{
                //    dataGridView1.UnSelectAllRow();
                //}
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_senderTextBox == null) return;

            if (dataGridView1.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var currentRow = dataGridView1.Rows[e.RowIndex];
                string selectedValue = currentRow.Cells[_senderTextBox.AccessibleName].Value.ToString().Trim();
                _senderTextBox.Text = selectedValue;
                _senderTextBox.Tag = currentRow;
                //Close();
                DialogResult = DialogResult.OK;
                //Dispose();
                _senderTextBox.SetLooking(false);
            }
        }

        string _filterString = "";  //"field1,field2,..."
        private string GenFilterString()
        {
            var result = "";
            try
            {
                // không dùng v6ColorDateTimePick1, v6ColorDateTimePick2 cho vào tham số ngay_ct
                //result = string.Format("Ngay_ct BETWEEN '{0}' AND '{1}'",
                //    v6ColorDateTimePick1.YYYYMMDD,
                //    v6ColorDateTimePick2.YYYYMMDD);

                if (TxtMa_bp.Text.Trim() != "")
                {
                    result += string.Format(" and {0}", TxtMa_bp.Query);
                }
                if (txtMaKh.IsSelected)
                {
                    result += string.Format(" and {0}", txtMaKh.QueryCheck);
                }
                if (taiKhoan.IsSelected)
                {
                    result += string.Format(" and {0}", taiKhoan.QueryCheck);
                }
                if (Txtma_nvien.Text.Trim() != "")
                {
                    result += string.Format(" and {0}", Txtma_nvien.Query);
                }

                var cacNhomKhachQuery = "";
                if (nhomKhach1.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach1.QueryCheck);
                }
                if (nhomKhach2.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach2.QueryCheck);
                }
                if (nhomKhach3.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach3.QueryCheck);
                }
                if (nhomKhach4.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach4.QueryCheck);
                }
                if (nhomKhach5.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach5.QueryCheck);
                }
                if (nhomKhach6.QueryCheck != "")
                {
                    cacNhomKhachQuery += string.Format(" and {0}", nhomKhach6.QueryCheck);
                }
                //,,,
                var where_nhkh = "";
                if (cacNhomKhachQuery.Length > 4)
                {
                    cacNhomKhachQuery = cacNhomKhachQuery.Substring(4);
                    where_nhkh = string.Format("Ma_kh in (Select ma_kh from Alkh where {0})", cacNhomKhachQuery);
                }
                if (where_nhkh != "")
                {
                    result += string.Format(" And {0}", where_nhkh);
                }
            }
            catch
            {
                // ignored
            }
            if (result.Length>4)
            {
                result = result.Substring(4); // Bỏ " and"
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
                SearchData();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnVSearch_Click: " + ex.Message);
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    Close();
            //}
        }
        
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_senderTextBox != null) _senderTextBox.SetLooking(false);
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
