using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    public partial class AldmvtFilterForm : V6Form
    {
        public string QueryString { get; set; }

        public AldmvtFilterForm()
        {
            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            try
            {
                txtMaSp.SetInitFilter("loai_vt='55'");
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
                var where = "";
                if (txtMaSp.Text != "")
                {
                    where += string.Format(" and ma_sp like '{0}%'", txtMaSp.Text);
                }
                if (txtNhomVt1.Text != "")
                {
                    where += string.Format(" and nh_vt1 like '{0}%'", txtNhomVt1.Text);
                }
                if (txtNhomVt2.Text != "")
                {
                    where += string.Format(" and nh_vt2 like '{0}%'", txtNhomVt2.Text);
                }
                if (txtNhomVt3.Text != "")
                {
                    where += string.Format(" and nh_vt3 like '{0}%'", txtNhomVt3.Text);
                }
                
                if (where.Length > 4)
                {
                    where = where.Substring(4);
                    QueryString = string.Format("ma_sp in (Select ma_vt from Alvt where {0})", where);
                }
                
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

        private void btnCopyDinhMuc_Click(object sender, EventArgs e)
        {
            CopyDinhMuc();
        }

        private void CopyDinhMuc()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Ngay_ct1",
                        dateTuNgay.Value == null ? "" : dateTuNgay.Value.Value.ToString("yyyyMMdd")),
                    new SqlParameter("@Ngay_ct2", dateDenNgay.Value.ToString("yyyyMMdd")),
                    new SqlParameter("@Ma_ct", "S05"),
                    new SqlParameter("@User_id", V6Login.UserId),
                };
                var result = V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_COPY_ALDMVT_ALL", plist);
                if (result > 0)
                {
                    ShowMainMessage(V6Text.Finish);
                }
                else
                {
                    ShowMainMessage(V6Text.Fail);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CopyDinhMuc", ex);
            }
        }
    }
}
