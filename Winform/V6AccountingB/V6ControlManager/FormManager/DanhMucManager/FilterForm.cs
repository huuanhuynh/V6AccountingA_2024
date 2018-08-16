using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Structs;

namespace V6ControlManager.FormManager.DanhMucManager
{
    public partial class FilterForm : V6Form
    {
        public delegate void FilterApply(string query);

        public event FilterApply FilterApplyEvent;

        protected virtual void CallFilterApplyEvent()
        {
            FilterApply handler = FilterApplyEvent;
            if (handler != null) handler(QueryString);
        }

        private string QueryString
        {
            get
            {
                return panel1.QueryString;
            }
        }
        private readonly V6TableStruct _structTable;
        private readonly string[] _fields;

        public FilterForm()
        {
            InitializeComponent();
        }

        public FilterForm(V6TableStruct structTable, string[] fields )
        {
            InitializeComponent();
            _structTable = structTable;
            _fields = fields;
            MyInit();
        }

        private void MyInit()
        {
            MadeControls();
        }

        private void MadeControls()
        {
            try
            {
                panel1.AddMultiFilterLine(_structTable, string.Join(",", _fields));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MadeControls error!\n" + ex.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallFilterApplyEvent();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CallFilterApplyEvent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
