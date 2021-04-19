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
                var v = Categories.IsExistOneCode_List("ARI70,ARA00", "Ma_dvcs", txtMa_dvcs.Text);
                txtMa_dvcs.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "DonViCoSoAddEditForm.DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_dvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVCS.Text;
            if (txtTenDvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenDVCS.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_DVCS",
                 txtMa_dvcs.Text.Trim(), DataOld["MA_DVCS"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblDVCS.Text + "=" + txtMa_dvcs.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_DVCS",
                 txtMa_dvcs.Text.Trim(), txtMa_dvcs.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblDVCS.Text + "=" + txtMa_dvcs.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void AfterUpdate()
        {
            try
            {
                // Nếu MA_DVCS bị sửa trùng dvcs login
                string MA_DVCS_OLD = DataOld["MA_DVCS"].ToString().Trim().ToUpper();
                string MA_DVCS_NEW = txtMa_dvcs.Text.Trim().ToUpper();
                if (V6Login.Madvcs.ToUpper() == MA_DVCS_OLD && MA_DVCS_NEW != MA_DVCS_OLD)
                {
                    V6Login.Madvcs = MA_DVCS_NEW;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AfterUpdate", ex);
            }
        }
    }
}
