using System;
using V6AccountingBusiness;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TinhTrangDichVuAddEditForm : AddEditControlVirtual
    {
        public TinhTrangDichVuAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = V6BusinessHelper.Select(V6Structs.V6TableName.Aldm, keys, "*").Data;
                string F8_table = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "TT_VT", txtTT_VT.Text);
                txtTT_VT.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("TinhTrangDichVu DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtTT_VT.Text.Trim() == "" || txtten_tt.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "TT_VT",
                    txtTT_VT.Text.Trim(), DataOld["TT_VT"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "TT_VT = " + txtTT_VT.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "TT_VT",
                    txtTT_VT.Text.Trim(), txtTT_VT.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.DataExist
                                        + "TT_VT = " + txtTT_VT.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

     
    }
}
