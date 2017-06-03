using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Structs;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ALREPORT : AddEditControlVirtual
    {
        public ALREPORT()
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


    }
}
