using V6AccountingBusiness;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TaiKhoanNganHangAddEditForm : AddEditControlVirtual
    {
        public TaiKhoanNganHangAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", TableName);
                var aldm = V6BusinessHelper.Select(V6Structs.V6TableName.Aldm, keys, "*").Data;
                string F8_table = "", code_field = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "TKNH", txtTKNH.Text);
                txtTKNH.Enabled = !v;

                if (!V6Init.V6Login.IsAdmin && txtTKNH.Text.ToUpper() != V6Init.V6Login.Madvcs.ToUpper())
                {
                    txtTKNH.Enabled = false;
                }

            }
            catch (System.Exception ex)
            {
                V6Tools.Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtTenTKNH.Text.Trim() == "" || txtTKNH.Text.Trim() == "" || txtTK.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "TKNH",
                    txtTKNH.Text.Trim(), DataOld["TKNH"].ToString());
                if (!b)
                    throw new System.Exception(V6Init.V6Text.ExistData
                                        + "TKNH = " + txtTKNH.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "TKNH",
                    txtTKNH.Text.Trim(), txtTKNH.Text.Trim());
                if (!b)
                    throw new System.Exception(V6Init.V6Text.ExistData
                                        + "TKNH = " + txtTKNH.Text.Trim());
            }

            if (errors.Length > 0) throw new System.Exception(errors);
        }
    }
}
