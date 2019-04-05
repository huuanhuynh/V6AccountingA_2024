using System.Data.SqlClient;
using V6AccountingBusiness;
using System;
using V6Structs;
using V6Init;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinTinhThanhForm : AddEditControlVirtual
    {
        public ThongTinTinhThanhForm()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {
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


                var v = Categories.IsExistOneCode_List(F8_table, "CODE", txtID.Text);
                txtID.Enabled = !v;

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("DisableWhenEdit " + ex.Message);
            }
        }
        public override void DoBeforeAdd()
        {
            int loai = 3;
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", "HR1"),
                new SqlParameter("@loai", loai),
                new SqlParameter("@id_name", "ID"),
                new SqlParameter("@tablename", "HRLSTPCS")
            };
            //var txtID_Text = V6BusinessHelper.ExecuteFunctionScalar("VFA_Getstt_rec_tt_like", plist);
            var txtID_Text = V6BusinessHelper.ExecuteProcedureScalar("VPA_sGet_Key_Like_stt_rec_tt", plist);
            txtID.Text = "" + txtID_Text;
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaCode.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaTinh.Text;
            if (txtName.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenTinh.Text;

            if (Mode == V6Mode.Edit)
            {

                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "CODE",
                 txtMaCode.Text.Trim(), DataOld["CODE"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "CODE = " + txtMaCode.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "CODE",
                    txtMaCode.Text.Trim(), txtMaCode.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "CODE = " + txtMaCode.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
