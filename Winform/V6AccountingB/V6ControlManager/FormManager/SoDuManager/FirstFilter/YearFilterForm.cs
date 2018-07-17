using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    /// <summary>
    /// Tạo chuỗi where "nam = ????".
    /// Trả về dialogResult.
    /// Giá trị lưu trong property QueryString.
    /// </summary>
    public partial class YearFilterForm : V6Form
    {
        public string QueryString { get; set; }
        private readonly string _ptablename;

        public YearFilterForm()
        {
            InitializeComponent();
            MyInit();
        }
        public YearFilterForm(string ptablename)
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
                if (txtYear.Value > 1900 && txtYear.Value < 9999  && (_ptablename.ToUpper() == "ALKC" || _ptablename.ToUpper() == "S04"))
                {
                    V6Setting.YearFilter = (int)txtYear.Value;
                    var where = "";
                    if (txtYear.Text != "")
                    {
                        where += "nam = " + V6Setting.YearFilter;
                    }

                    QueryString = where;

                    if (_ptablename.ToUpper() == "ALKC")
                    {
                        CopyAlkc2NextYear((int)txtYear.Value);
                    }
                    if (_ptablename.ToUpper() == "S04")
                    {
                        CopyAlpb2NextYear((int)txtYear.Value);
                    }
                    DialogResult = DialogResult.OK;
                }
                else if (txtYear.Value > 1900 && txtYear.Value < 9999 && V6Setting.M_Ngay_ks.Year < txtYear.Value)
                {
                    V6Setting.YearFilter = (int) txtYear.Value;
                    var where = "";
                    if (txtYear.Text != "")
                    {
                        where += "nam = " + V6Setting.YearFilter;
                    }

                    QueryString = where;

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowWarningMessage("Nhập năm!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CallFilterOkClick: " + ex.Message);
            }
        }

        private void CopyAlkc2NextYear(int pyear)
        {
           
            SqlParameter[] plist =
                        {
                            new SqlParameter("@nYear",pyear)
                        };

            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CopyAlkc2NextYear", plist);

        }
        private void CopyAlpb2NextYear(int pyear)
        {

            SqlParameter[] plist =
                        {
                            new SqlParameter("@nYear",pyear)
                        };

            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CopyAlpb2NextYear", plist);

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
    }
}
