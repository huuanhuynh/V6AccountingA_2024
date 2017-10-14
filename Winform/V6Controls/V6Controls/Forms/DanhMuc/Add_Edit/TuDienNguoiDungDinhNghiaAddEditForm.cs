using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TuDienNguoiDungDinhNghiaAddEditForm : AddEditControlVirtual
    {
        public TuDienNguoiDungDinhNghiaAddEditForm()
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

                var v = Categories.IsExistOneCode_List(F8_table, "MA_TD", Txtma_td.Text);
                Txtma_td.Enabled = !v;

                

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("TuDienNguoiDungDinhNghiaAddEditForm DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_td.Text.Trim() == "" || TxtTen_td.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            var KEY_LIST = new[] {"MA_TD"};
            errors += CheckValid(TableName.ToString(), KEY_LIST);

            //if (Mode == V6Mode.Edit)
            //{
            //    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TD",
            //        Txtma_td.Text.Trim(), DataOld["MA_TD"].ToString());
            //    if (!b)
            //        throw new Exception(V6Init.V6Text.ExistData
            //                            + "MA_TD = " + Txtma_td.Text.Trim());
            //}
            //else if (Mode == V6Mode.Add)
            //{
            //    bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TD",
            //        Txtma_td.Text.Trim(), Txtma_td.Text.Trim());
            //    if (!b)
            //        throw new Exception(V6Init.V6Text.ExistData
            //                            + "MA_TD = " + Txtma_td.Text.Trim());
            //}

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
