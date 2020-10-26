using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanAddEditForm : AddEditControlVirtual
    {
        public BoPhanAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARA00", "Ma_bp", TxtMa_bp.Text);
                TxtMa_bp.Enabled = !v;

                if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    TxtMa_dvcs.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_bp.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaBp.Text;
            if (TxtTen_bp.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenBp.Text;
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaDVCS.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_BP",
                 TxtMa_bp.Text.Trim(), DataOld["MA_BP"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaBp.Text + "=" + TxtMa_bp.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_BP",
                 TxtMa_bp.Text.Trim(), TxtMa_bp.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaBp.Text + "=" + TxtMa_bp.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
