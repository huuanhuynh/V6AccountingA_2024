using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanSuDungCongCuAddEditForm : AddEditControlVirtual
    {
        public BoPhanSuDungCongCuAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ALCC", "MA_BP", txtma_bp.Text);
                txtma_bp.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("BoPhanSuDungCongCuAddEditForm DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtma_bp.Text.Trim() == "" || TxtTen_bp.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                    txtma_bp.Text.Trim(), DataOld["MA_BP"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_BP = " + txtma_bp.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                    txtma_bp.Text.Trim(), txtma_bp.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_BP = " + txtma_bp.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
