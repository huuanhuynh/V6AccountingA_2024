using System;
using System.Windows.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Alreport;
using V6Controls.Forms.Editor;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6Alct1AddEditForm : AddEditControlVirtual
    {
        public V6Alct1AddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
           // txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (Mode == V6Mode.Edit)
            {
                var loai = DataOld["LOAI"].ToString();
                if(loai=="1")
                {
                    ChkVisible.Enabled = false;
                    TxtForder.Enabled = false;
                    TxtWidth.Enabled = false;
                }
            }
        }
        public override void ValidateData()
        {
           
        }

        public override void V6F3Execute()
        {
            ChkVisible.Enabled = true; //ChkVisible.Visible = true;
            TxtForder.Enabled = true;
            TxtWidth.Enabled = true;

            lblFtype.Visible = true;
            txtFtype.Visible = true;
            txtFtype.Enabled = true;
            txtFdecimal.Visible = true;
            lblFdecimal.Visible = true;
            txtFdecimal.Enabled = true;

            lblCheckVvar.Visible = true;
            chkCheckVvar.Visible = true;
            lblNotEmpty.Visible = true;
            chkNotEmpty.Visible = true;

            txtDmethod.Visible = true;
            lblXML.Visible = true;
            btnEditXml.Visible = true;

            lblFilterM.Visible = true;
            txtFilter_M.Visible = true;
            btnEditFilterM.Visible = true;
            lblDmethodM.Visible = true;
            txtDmethod_M.Visible = true;
            btnEditXmlM.Visible = true;
            txtLimits.Visible = true;
            base.V6F3Execute();
        }

        public override void V6F3ExecuteUndo()
        {
            ChkVisible.Enabled = false;
            TxtForder.Enabled = false;
            TxtWidth.Enabled = false;
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml =  TxtFcolumn.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }

        private void DoEditFilter_M()
        {
            try
            {
                DefineInfoEditorForm f = new DefineInfoEditorForm(txtFilter_M.Text);
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtFilter_M.Text = f.FILTER_DEFINE;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditFilter_M", ex);
            }
        }

        private void DoEditXml_M()
        {
            try
            {
                var file_xml = TxtFcolumn.Text.Trim().ToUpper() + "_M.xml";
                new XmlEditorForm(txtDmethod_M, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml_M", ex);
            }
        }

        private void btnEditXml_Click(object sender, System.EventArgs e)
        {
            DoEditXml();
        }

        private void btnEditFilterM_Click(object sender, EventArgs e)
        {
            DoEditFilter_M();
        }

        private void btnEditXmlM_Click(object sender, EventArgs e)
        {
            DoEditXml_M();
        }
    }
}
