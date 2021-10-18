using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Tools;

namespace V6Controls.Forms.Viewer
{
    public partial class DataViewerForm : V6Form
    {
        public DataViewerForm()
        {
            InitializeComponent();
        }

        private readonly object _data_object;
        private DataSet _dataset = null;
        private DataTable _tbl = null;
        private int _current_index = 0;
        public object CurrentCellValue { get { return dataGridView1.CurrentCell == null ? null : dataGridView1.CurrentCell.Value; } }
        public IDictionary<string, object> CurrentRowData = null;
        public List<IDictionary<string, object>> SelectedDataList = null;
        /// <summary>
        /// Trường dữ liệu để lấy SelectedValue sau khi nhận.
        /// </summary>
        public string ValueField = null;
        /// <summary>
        /// Giá trị chọn theo SelectField sau khi nhận.
        /// </summary>
        public object SelectedValue = null;

        public DataViewerForm(object dataObject)
        {
            InitializeComponent();
            //if (!showSum)
            //{
            //    dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
            //    gridViewSummary1.Visible = false;
            //}
            _data_object = dataObject;
            MyInit();
        }

        public DataViewerForm(object dataObject, string valueField, bool showSum)
        {
            InitializeComponent();
            if (!showSum)
            {
                dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
                gridViewSummary1.Visible = false;
            }
            _data_object = dataObject;
            ValueField = valueField.Trim().ToUpper();
            MyInit();
        }

        public DataViewerForm(object dataObject, bool showSum)
        {
            InitializeComponent();
            if (!showSum)
            {
                dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
                gridViewSummary1.Visible = false;
            }
            _data_object = dataObject;
            MyInit();
        }

        /// <summary>
        /// Khởi tạo DataViewForm.
        /// </summary>
        /// <param name="dataObject">Dữ liệu hiển thị lên gridview.</param>
        /// <param name="showSum">Hiện dòng tổng.</param>
        /// <param name="ctrl_s">Bật chức năng lọc dữ liệu</param>
        public DataViewerForm(object dataObject, bool showSum, bool ctrl_s)
        {
            InitializeComponent();
            if (!showSum)
            {
                dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
                gridViewSummary1.Visible = false;
            }
            dataGridView1.Control_S = ctrl_s;
            _data_object = dataObject;
            MyInit();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void MyInit()
        {
            try
            {
                Text += ", Ctrl + F : Find";
                if (dataGridView1.Control_S)
                {
                    Text += ", Ctrl + S : Filter, Ctrl + Shift + S : reset filter";
                }
                if (_data_object is DataTable)
                {
                    _tbl = (DataTable) _data_object;
                    if (_tbl.DataSet != null)
                    {
                        _dataset = _tbl.DataSet;
                    }
                }
                else if (_data_object is DataSet)
                {
                    _dataset = (DataSet) _data_object;
                }
                ShowData(_current_index);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit " + ex.Message);
            }
        }

        public void FormatGridView(string showFields, string formatStrings, string headerString)
        {
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
        }

        private void ShowData(int currentIndex)
        {
            _current_index = currentIndex;
            if (_dataset != null && _dataset.Tables.Count>1)
            {
                if (_current_index >= _dataset.Tables.Count)
                {
                    _current_index = 0;
                }
                else if (_current_index < 0)
                {
                    _current_index = _dataset.Tables.Count-1;
                }

                dataGridView1.DataSource = _dataset.Tables[_current_index];
                Text = string.Format("Data {0}/{1}. Ctrl + PageDown/PageUp | Left/Right change data.", _current_index + 1, _dataset.Tables.Count);
            }
            else
            {
                dataGridView1.DataSource = _data_object;
            }

            //dataGridView1.Format();
        }

        public event Action<Keys> HotKeyAction;
        protected virtual void OnHotKeyAction(Keys keyData)
        {
            var handler = HotKeyAction;
            if (handler != null) handler(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                OK();
            }
            else if (keyData == (Keys.Control | Keys.PageDown) || keyData == (Keys.Control | Keys.Right))
            {
                ShowData(_current_index+1);
            }
            else if (keyData == (Keys.Control | Keys.PageUp) || keyData == (Keys.Control | Keys.Left))
            {
                ShowData(_current_index-1);
            }

            OnHotKeyAction(keyData);
            return base.DoHotKey0(keyData);
        }

        private void OK()
        {
            try
            {
                CurrentRowData = dataGridView1.CurrentRow != null ? dataGridView1.CurrentRow.ToDataDictionary() : null;
                SelectedDataList = dataGridView1.GetSelectedData();
                SelectedValue = CurrentRowData != null && CurrentRowData.ContainsKey(ValueField) ? CurrentRowData[ValueField] : null;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".OK", ex);
            }
        }
        
    }
}
