using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class LyDoTangGiamCCDCAddEditForm : AddEditControlVirtual
    {
        public LyDoTangGiamCCDCAddEditForm()
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
                string F8_table = "", code_field = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "MA_TG_CC", Txtma_tg_cc.Text);
                Txtma_tg_cc.Enabled = !v;

                if (!V6Init.V6Login.IsAdmin && Txtma_tg_cc.Text.ToUpper() != V6Init.V6Login.Madvcs.ToUpper())
                {
                    Txtma_tg_cc.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_tg_cc.Text.Trim() == "" || TxtTen_tg_cc.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TG_CC",
                    Txtma_tg_cc.Text.Trim(), DataOld["MA_TG_CC"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_TG_CC = " + Txtma_tg_cc.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TG_CC",
                    Txtma_tg_cc.Text.Trim(), Txtma_tg_cc.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_TG_CC = " + Txtma_tg_cc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
