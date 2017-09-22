using System;
using System.Data;
using System.Windows.Forms;

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

        public DataViewerForm(object dataObject, bool showSum = true)
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

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void MyInit()
        {
            try
            {
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
                Text = string.Format("Data {0}/{1}. PageDown/PageUp show next data.", _current_index + 1, _dataset.Tables.Count);
            }
            else
            {
                dataGridView1.DataSource = _data_object;
            }

            dataGridView1.Format();
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
            else if (keyData == Keys.PageDown)
            {
                ShowData(_current_index+1);
            }
            else if (keyData == Keys.PageUp)
            {
                ShowData(_current_index-1);
            }

            OnHotKeyAction(keyData);
            return base.DoHotKey0(keyData);
        }
        
    }
}
