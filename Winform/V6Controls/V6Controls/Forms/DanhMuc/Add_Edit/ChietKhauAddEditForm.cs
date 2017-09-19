using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ChietKhauAddEditForm : AddEditControlVirtual
    {
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", TableName);
                var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                string F8_table = "", code_field = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "MA_CK", TXTma_ck.Text);
                TXTma_ck.Enabled = !v;

                if (!V6Init.V6Login.IsAdmin && TXTma_ck.Text.ToUpper() != V6Init.V6Login.Madvcs.ToUpper())
                {
                    TXTma_ck.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }
        public ChietKhauAddEditForm()
        {
            InitializeComponent();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_ck.Text.Trim() == "" || TXTten_ck.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CK",
                    TXTma_ck.Text.Trim(), DataOld["MA_CK"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_CK = " + TXTma_ck.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CK",
                    TXTma_ck.Text.Trim(), TXTma_ck.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_CK = " + TXTma_ck.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
