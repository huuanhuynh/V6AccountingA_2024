using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    public partial class YearMonthFilterForm : V6Form
    {
        //public delegate void FilterOkHandle(string query);
        //public event FilterOkHandle FilterOkClick;
        
        public string QueryString { get; set; }
        private V6TableStruct _structTable;
        private string[] _fields;
        private string _ptablename;

        public YearMonthFilterForm()
        {
            InitializeComponent();
            MyInit();
        }
        public YearMonthFilterForm(string ptablename)
        {
            _ptablename = ptablename;
            InitializeComponent();
            MyInit();
        }
        private void MyInit()
        {
            try
            {
                txtYear.Value = V6Setting.M_Nam_bd;
                TxtThang.Value = V6Setting.M_SV_DATE.Month;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit: " + ex.Message, "AldmvtFilterForm");
            }
        }

        private void CallFilterOkClick()
        {
            try
            {
                if ((_ptablename == "ABHHVT" && txtYear.Value > 1900 && txtYear.Value < 9999)
                    || (txtYear.Value > 1900 && txtYear.Value < 9999 && V6Setting.M_Ngay_ks.Year < txtYear.Value))

                {
                    V6Setting.YearFilter = (int) txtYear.Value;
                    V6Setting.MonthFilter = (int)TxtThang.Value;

                    var where = "";
                    if (txtYear.Text != "")
                    {
                        where += "nam = " + V6Setting.YearFilter;
                    }
                    if (TxtThang.Text != "")
                    {

                        where += (where == "" ? "" : " and ") + "thang = " + V6Setting.MonthFilter;
                    }
                    QueryString = where;
                    
                    DialogResult = DialogResult.OK;

                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CallFilterOkClick: " + ex.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallFilterOkClick();
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
            CallFilterOkClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TxtThang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }

        private void TxtThang_Leave(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
           
                try
                {
                    var txt = (V6NumberTextBox)sender;
                    if (txt.Value <= 0) txt.Value = V6Setting.M_SV_DATE.Year;
                }
                catch (Exception)
                {

                }
        
        }
    }
}
