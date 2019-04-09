using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class DonViCoSoAddEditForm : AddEditControlVirtual
    {
        public DonViCoSoAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARA00", "Ma_dvcs", TxtMa_dvcs.Text);
                TxtMa_dvcs.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "DonViCoSoAddEditForm.DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVCS.Text;
            if (txtTenDvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenDVCS.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_DVCS",
                 TxtMa_dvcs.Text.Trim(), DataOld["MA_DVCS"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblDVCS.Text + "=" + TxtMa_dvcs.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_DVCS",
                 TxtMa_dvcs.Text.Trim(), TxtMa_dvcs.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblDVCS.Text + "=" + TxtMa_dvcs.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
