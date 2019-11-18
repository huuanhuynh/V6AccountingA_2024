using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6Alct2AddEditForm : AddEditControlVirtual
    {
        public V6Alct2AddEditForm()
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

            //txtDmethod.Visible = true;
            //lblXML.Visible = true;
            //btnEditXml.Visible = true;

            //lblFilterM.Visible = true;
            //txtFilter_M.Visible = true;
            //btnEditFilterM.Visible = true;
            //lblDmethodM.Visible = true;
            //txtDmethod_M.Visible = true;
            //btnEditXmlM.Visible = true;
            txtLimits.Visible = true;
            base.V6F3Execute();
        }
    }
}
