using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhanVienAddEditForm : AddEditControlVirtual
    {
        public NhanVienAddEditForm()
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
            var v = Categories.IsExistOneCode_List("ARI70,ARA00", "MA_NVIEN", TxtMa_nvien.Text);
            TxtMa_nvien.Enabled = !v;

            if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
            {
                TxtMa_dvcs.Enabled = false;
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_nvien.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaNvien.Text;
            if (TxtTen_nvien.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenNvien.Text;
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaDVCS.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NVIEN",
                 TxtMa_nvien.Text.Trim(), DataOld["MA_NVIEN"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaNvien.Text + "=" + TxtMa_nvien.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NVIEN",
                 TxtMa_nvien.Text.Trim(), TxtMa_nvien.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaNvien.Text + "=" + TxtMa_nvien.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }

}
