using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomGiaKhachHangAddEditForm : AddEditControlVirtual
    {
        public NhomGiaKhachHangAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", TableName);
                var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                string F8_table = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "MA_NH", TXTMA_NH.Text);
                TXTMA_NH.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("NhomGiaKhachHangAddEditForm DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_NH.Text.Trim() == "" || TXTten_nh.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NH",
                    TXTMA_NH.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_NH = " + TXTMA_NH.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NH",
                    TXTMA_NH.Text.Trim(), TXTMA_NH.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "MA_NH = " + TXTMA_NH.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
