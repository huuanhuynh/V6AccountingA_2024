using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6Controls.Controls;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.Alreport
{
    public partial class ALREPORT_AddEdit : AddEditControlVirtual
    {
        public ALREPORT_AddEdit()
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

        private void CopyFilter()
        {
            try
            {
                var not_in = "";
                {
                    not_in += string.Format(",'{0}'", TXTMA_BC.Text);
                }
                if (not_in.Length > 1) not_in = not_in.Substring(1);

                //SHOW SELECT
                CopyFilterSelectForm selectForm = new CopyFilterSelectForm();
                selectForm.NotInList = not_in;
                if (selectForm.ShowDialog(this) == DialogResult.OK)
                {
                    var add_count = 0;

                    //copy all
                    //var selected_ma_bc = selectForm.SelectedID;
                    //var keys = new SortedDictionary<string, object>
                    //{
                    //    {"MA_BC", selected_ma_bc}
                    //};
                    //var alreport1_data = Categories.Select(V6TableName.Alreport1, keys).Data;
                    //foreach (DataRow row in alreport1_data.Rows)
                    //{
                    //    var data = row.ToDataDictionary();
                    //    data["MA_BC"] = TXTMA_BC.Text.Trim();
                    //    data["UID_CT"] = DataOld["UID"];
                    //    if (Categories.Insert(V6TableName.Alreport1, data))
                    //    {
                    //        add_count++;
                    //    };
                    //}

                    //copy selected
                    foreach (DataGridViewRow row in selectForm.dataGridView1.Rows)
                    {
                        if (!row.IsSelect()) continue;
                        
                        var data = row.ToDataDictionary();
                        //Ghi đè dữ liệu mới.
                        data["MA_BC"] = TXTMA_BC.Text.Trim();
                        data["UID_CT"] = DataOld["UID"];
                        if (Categories.Insert(V6TableName.Alreport1, data))
                        {
                            add_count++;
                        };
                    }

                    ShowMainMessage(V6Setting.IsVietnamese 
                        ? string.Format("Đã thêm {0} chi tiết.", add_count)
                        : string.Format("{0} detail(s) added.", add_count));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CopyFilter", ex);
            }
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            try
            {
                var uid_ct = DataOld["UID"].ToString();
                var ma_bc_old = DataOld["MA_BC"].ToString().Trim();
                var data = new Dictionary<string, object>();

                CategoryView dmView = new CategoryView(ItemID, "title", "ALREPORT1", "uid_ct='" + uid_ct + "'", null, DataOld);
                if (Mode == V6Mode.View)
                {
                    dmView.EnableAdd = false;
                    dmView.EnableCopy = false;
                    dmView.EnableDelete = false;
                    dmView.EnableEdit = false;
                }
                dmView.ToFullForm(btnBoSung.Text);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + " BoSung_Click " + ex.Message);
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

        private void btnCopyFilter_Click(object sender, EventArgs e)
        {
            CopyFilter();
        }

        private void btnEditXml_Click(object sender, EventArgs e)
        {
            DoEditXml();
        }


    }
}
