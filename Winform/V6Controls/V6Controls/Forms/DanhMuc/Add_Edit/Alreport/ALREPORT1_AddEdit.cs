using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Alreport
{
    public partial class ALREPORT1_AddEdit : AddEditControlVirtual
    {
        public ALREPORT1_AddEdit()
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
               
            }

        }
  
        public override void V6F3Execute()
        {
            
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_BC.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            
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
                ALREPORT1_FilterEditorForm f = new ALREPORT1_FilterEditorForm(txtFilter.Text);
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

        private void DoEditXml()
        {
            try
            {
                var file_xml = TXTMA_BC.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtDmethod, file_xml, "Table0", "event,using,method,content".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
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

        private void txtDmethod_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        
    }
}
