using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls.Forms.Editor;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Alreport
{
    public partial class ALREPORT1_AddEdit : AddEditControlVirtual
    {
        public ALREPORT1_AddEdit()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, EventArgs e)
        {
           // txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (Mode == V6Mode.Edit)
            {
               
            }
        }
  
        public override void V6F3Execute()
        {
            
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_bc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa_bc.Text;
            
            if (errors.Length > 0) throw new Exception(errors);
        }

        public override SortedDictionary<string, object> GetData()
        {
            var data = base.GetData();
            data["UID_CT"] = ParentData["UID"];
            return data;
        }

        private void DoEditFilter()
        {
            try
            {
                DefineInfoEditorForm f = new DefineInfoEditorForm(txtFilter.Text);
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtFilter.Text = f.FILTER_DEFINE;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditFilter", ex);
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

        private void DoEditXml()
        {
            try
            {
                var file_xml = txtMa_bc.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }
        
        private void DoEditXml_M()
        {
            try
            {
                var file_xml = txtMa_bc.Text.Trim().ToUpper() + "_M.xml";
                new XmlEditorForm(txtDmethod_M, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml_M", ex);
            }
        }

        private void btnEditFilter_Click(object sender, EventArgs e)
        {
            DoEditFilter();
        }

        private void btnEditXml_Click(object sender, EventArgs e)
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
