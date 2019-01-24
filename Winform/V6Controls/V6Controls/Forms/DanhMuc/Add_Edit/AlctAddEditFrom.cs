using System;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls.Forms.Editor;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlctAddEditFrom : AddEditControlVirtual
    {
        private bool _ready;

        public AlctAddEditFrom()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, EventArgs e)
        {
            LoadAlpost();
            txtMaCt.Focus();
            _ready = true;
        }

        public override void DoBeforeEdit()
        {
            //var v = Categories.IsExistOneCode_List("ABKH,ARA00,ARI70", "Ma_kh", txtMaCt.Text);
            //txtMaCt.Enabled = !v;
        }

        public override void ValidateData()
        {
            if (Mode != V6Mode.Add) return;
            if (txtMaCt.Text.Trim() == "") throw new Exception("Chưa nhập mã chứng từ!");
        }

        private void LoadAlpost()
        {
            try
            {
                var mact = txtMaCt.Text;
                var Invoice = new V6InvoiceBase(mact);
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.Language == "V" ? "Ten_post" : "Ten_post2";
                cboKieuPost.DataSource = Invoice.AlPost;
                cboKieuPost.ValueMember = "kieu_post";
                cboKieuPost.DisplayMember = V6Setting.Language == "V" ? "Ten_post" : "Ten_post2";

                cboKieuPost.SelectedValue = txtTrangThaiNgamDinh.Text.Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadAlpost " + ex.Message);
            }
        }

        private void cboKieuPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_ready)
                {
                    txtTrangThaiNgamDinh.Text = cboKieuPost.SelectedValue.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(
                    V6Login.ClientName + " " + GetType() + ".cboKieuPost_SelectedIndexChanged " + ex.Message,
                    Application.ProductName);
            }
        }

        public override void V6F3Execute()
        {
            //ShowTopLeftMessage("V6 Confirm ......OK....");
            txtDmethod.Visible = true;
            lblXML.Visible = true;
            btnEditXml.Visible = true;
            chkWriteLog.Visible = true;
            txtExtraInfo.Visible = true;
            lblThongTinThem.Visible = true;
            txtResetCopy.Visible = true;
            lblResetCopy.Visible = true;
            txtADSELECTMORE.Visible = true;
            lblADSELECTMORE.Visible = true;
            txtAMSELECTMORE.Visible = true;
            lblAMSELECTMORE.Visible = true;
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = txtMaCt.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }

        private void btnEditXml_Click(object sender, System.EventArgs e)
        {
            DoEditXml();
        }

    }
}
