using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    public partial class YearMonthFilterForm : V6Form
    {
        public string QueryString { get; set; }
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
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        private void CallFilterOkClick()
        {
            try
            {
                if (_ptablename != "ABHHVT") goto CHECK;
                //if (_ptablename == "ABHHVT")
                if (txtYear.Value > 1900 && txtYear.Value < 9999)
                {
                    goto GO;
                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                CHECK:
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)TxtThang.Value, (int)txtYear.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                GO:
                V6Setting.YearFilter = (int) txtYear.Value;
                V6Setting.MonthFilter = (int) TxtThang.Value;
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
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CallFilterOkClick", ex);
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
            //try
            //{
            //    //var txt = (V6NumberTextBox)sender;
            //    //if (txt.Value < 1) txt.Value = 0;
            //    //if (txt.Value > 12) txt.Value = 12;
            //}
            //catch (Exception)
            //{

            //}
        }

        private void TxtThang_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    //var txt = (V6NumberTextBox)sender;
            //    //if (txt.Value < 1) txt.Value = 1;
            //    //if (txt.Value > 12) txt.Value = 12;
            //}
            //catch (Exception)
            //{

            //}
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    var txt = (V6NumberTextBox)sender;
            //    if (txt.Value <= 0) txt.Value = V6Setting.M_SV_DATE.Year;
            //}
            //catch (Exception)
            //{

            //}
        }
    }
}
