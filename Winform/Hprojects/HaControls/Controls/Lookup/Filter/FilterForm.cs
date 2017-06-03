using System;
using System.Windows.Forms;

namespace H_Controls.Controls.Lookup.Filter
{
    public partial class FilterForm : BaseForm
    {
        public delegate void FilterOkClickDelegate(string query);

        public event FilterOkClickDelegate FilterOkClick;

        protected virtual void OnFilterOkClick()
        {
            FilterOkClickDelegate handler = FilterOkClick;
            if (handler != null) handler(QueryString);
        }

        private string QueryString
        {
            get
            {
                return panel1.QueryString;
            }
        }
        private readonly DataGridView _gridView;
        private readonly string[] _fields;

        public FilterForm()
        {
            InitializeComponent();
        }

        public FilterForm(DataGridView gridView, string[] fields )
        {
            InitializeComponent();
            _gridView = gridView;
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
                panel1.AddMultiFilterLine(_gridView, _fields);
            }
            catch (Exception ex)
            {
                HControlHelper.ShowErrorMessage("MadeControls error!\n" + ex.Message);
            }
        }

        //private void AddFilterLineControl(int index, string fieldName)
        //{
        //    FilterLineDynamic lineControl = new FilterLineDynamic();
        //    lineControl.FieldName = fieldName.ToUpper();
        //    lineControl.FieldCaption = CorpLan2.GetFieldHeader(fieldName);
        //    if (_structTable.ContainsKey(fieldName.Trim().ToUpper()))
        //    {
        //        if (",nchar,nvarchar,ntext,char,varchar,text,xml,"
        //            .Contains(","+_structTable[fieldName.ToUpper()].SqlDataType+","))
        //        {
        //            lineControl.AddTextBox();
        //        }
        //        else if(",date,smalldatetime,datetime,"
        //            .Contains(","+_structTable[fieldName.ToUpper()].SqlDataType+","))
        //        {
        //            lineControl.AddDateTimePick();
        //        }
        //        else
        //        {
        //            lineControl.AddNumberTextBox();
        //        }
        //    }
        //    lineControl.Location = new Point(10, 10 + 30 * index);
        //    panel1.Controls.Add(lineControl);
        //}

         

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            OnFilterOkClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
