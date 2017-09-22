using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class PhanLoaiTaiSanAddEditForm : AddEditControlVirtual
    {
        public PhanLoaiTaiSanAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            try
            {

                var v = Categories.IsExistOneCode_List("ALTS", "LOAI_TS", Txtma_loai.Text);
                Txtma_loai.Enabled = !v;

                if (!V6Init.V6Login.IsAdmin && Txtma_loai.Text.ToUpper() != V6Init.V6Login.Madvcs.ToUpper())
                {
                    Txtma_loai.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("DisableWhenEdit " + ex.Message);
            }
            
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_loai.Text.Trim() == "" || txtTen_loai.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI",
                    Txtma_loai.Text.Trim(), DataOld["MA_LOAI"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_LOAI = " + Txtma_loai.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI",
                    Txtma_loai.Text.Trim(), Txtma_loai.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_LOAI = " + Txtma_loai.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
