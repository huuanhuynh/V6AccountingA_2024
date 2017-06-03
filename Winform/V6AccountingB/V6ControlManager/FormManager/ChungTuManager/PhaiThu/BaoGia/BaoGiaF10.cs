using System;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia
{
    public partial class BaoGiaF10 : V6Form
    {
        V6Invoice93 Invoice = new V6Invoice93();

        public BaoGiaF10(string sttRec, object currentKieuPost)
        {
            _sttRec = sttRec;
            InitializeComponent();
            MyInit(currentKieuPost);
        }

        private void MyInit(object currentKieuPost)
        {
            try
            {
                Text = V6Text.Action;
                LoadAlpost();
                //cboKieuPost.SelectedValue = currentKieuPost;
                cboKieuPost.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.Init {1} {2}", GetType(), _sttRec, currentKieuPost), ex);
            }
        }

        private void LoadAlpost()
        {
            try
            {
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
                cboKieuPost.DataSource = Invoice.AlPost.Copy();
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.IsVietnamese ? "Ten_post" : "Ten_post2";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
