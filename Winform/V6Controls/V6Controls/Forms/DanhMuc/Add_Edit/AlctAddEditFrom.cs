using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Controls.Forms.Editor;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

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
            if (txtMaCt.Text.Trim() == "") throw new Exception (V6Text.Text("CHUANHAP") + lblMaCT.Text + "!");
        }

        private void LoadAlpost()
        {
            try
            {
                var mact = txtMaCt.Text;
                var Invoice = V6InvoiceBase.GetInvoiceBase(mact);
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

        public override void V6CtrlF12Execute()
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
            txtTYPE_VIEW.Visible = true;
            lblTYPE_VIEW.Visible = true;
            txtADSELECTMORE.Visible = true;
            lblADSELECTMORE.Visible = true;
            txtAMSELECTMORE.Visible = true;
            lblAMSELECTMORE.Visible = true;
            base.V6CtrlF12Execute();
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


        private Dictionary<string, AlbcFieldInfo> GetSourceFieldsInfo1()
        {
            var amStruct = V6BusinessHelper.GetTableStruct(txtAM.Text);
            return V6ControlFormHelper.GetSourceFieldsInfo(amStruct);
        }
        private Dictionary<string, AlbcFieldInfo> GetSourceFieldsInfo2()
        {
            var adStruct = V6BusinessHelper.GetTableStruct(txtAD.Text);
            return V6ControlFormHelper.GetSourceFieldsInfo(adStruct);
        }
        

        private void btnGRDS_V1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, AlbcFieldInfo> sourceFields = GetSourceFieldsInfo1();
                Dictionary<string, AlbcFieldInfo> targetInfoList = V6ControlFormHelper.GetTargetFieldsInfo(sourceFields, txtShowFields1.Text, txtFormats1.Text, txtHeaderV1.Text, txtHeaderE1.Text, "");

                V6ControlFormHelper.SelectFields(this, sourceFields, targetInfoList, txtShowFields1, txtFormats1, txtHeaderV1, txtHeaderE1);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnGRDS_V1_Click", ex);
            }
        }

        private void btnGRDS_V2_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, AlbcFieldInfo> sourceFields = GetSourceFieldsInfo2();
                Dictionary<string, AlbcFieldInfo> targetInfoList = V6ControlFormHelper.GetTargetFieldsInfo(sourceFields, txtShowFields2.Text, txtFormats2.Text, txtHeaderV2.Text, txtHeaderE2.Text, "");
                V6ControlFormHelper.SelectFields(this, sourceFields, targetInfoList, txtShowFields2, txtFormats2, txtHeaderV2, txtHeaderE2);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnGRDS_V2_Click", ex);
            }
        }

        private void xuatDinhDangGridViewMenu_Click(object sender, EventArgs e)
        {
            try
            {
                //var saveFile = V6ControlFormHelper.ChooseSaveFile("Xml|*.xml");
                var saveFile = new SaveFileDialog
                {
                    Filter = "XML files (*.Xml)|*.xml",
                    Title = "Xuất XML.",
                    FileName = "ALCT_GRIDVIEWFORMAT.xml"
                };
                if (saveFile.ShowDialog(this) == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(saveFile.FileName)) return;

                    DataSet _ds = new DataSet();
                    DataTable _dt = new DataTable("ALCT_GRIDVIEWFORMAT");
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data[txtShowFields1.AccessibleName] = txtShowFields1.Text;
                    data[txtFormats1.AccessibleName] = txtFormats1.Text;
                    data[txtHeaderV1.AccessibleName] = txtHeaderV1.Text;
                    data[txtHeaderE1.AccessibleName] = txtHeaderE1.Text;

                    data[txtShowFields2.AccessibleName] = txtShowFields2.Text;
                    data[txtFormats2.AccessibleName] = txtFormats2.Text;
                    data[txtHeaderV2.AccessibleName] = txtHeaderV2.Text;
                    data[txtHeaderE2.AccessibleName] = txtHeaderE2.Text;
                    _dt.AddRow(data, true);
                    _ds.Tables.Add(_dt);
                    
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
                    (_ds).WriteXml(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".WriteToFile", ex);
            }
        }

        private void nhapDinhDangGridViewMenu_Click(object sender, EventArgs e)
        {
            try
            {
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                
                var load_ds = new DataSet();
                IDictionary<string, object> load_data = null;
                load_ds.ReadXml(fs);
                if (load_ds.Tables.Count > 0 && load_ds.Tables[0].Rows.Count > 0)
                {
                    load_data = load_ds.Tables[0].Rows[0].ToDataDictionary();
                }
                fs.Close();

                if (load_data != null)
                {
                    if (load_data.ContainsKey(txtShowFields1.AccessibleName.ToUpper())) txtShowFields1.Text = "" + load_data[txtShowFields1.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtFormats1.AccessibleName.ToUpper())) txtFormats1.Text = "" + load_data[txtFormats1.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtHeaderV1.AccessibleName.ToUpper())) txtHeaderV1.Text = "" + load_data[txtHeaderV1.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtHeaderE1.AccessibleName.ToUpper())) txtHeaderE1.Text = "" + load_data[txtHeaderE1.AccessibleName.ToUpper()];

                    if (load_data.ContainsKey(txtShowFields2.AccessibleName.ToUpper())) txtShowFields2.Text = "" + load_data[txtShowFields2.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtFormats2.AccessibleName.ToUpper())) txtFormats2.Text = "" + load_data[txtFormats2.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtHeaderV2.AccessibleName.ToUpper())) txtHeaderV2.Text = "" + load_data[txtHeaderV2.AccessibleName.ToUpper()];
                    if (load_data.ContainsKey(txtHeaderE2.AccessibleName.ToUpper())) txtHeaderE2.Text = "" + load_data[txtHeaderE2.AccessibleName.ToUpper()];
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".LoadFromFile", ex);
            }
        }
    }
}
