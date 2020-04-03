using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KhaiBaoLoaiLuong : AddEditControlVirtual
    {
        public KhaiBaoLoaiLuong()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
       //     txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void DoBeforeEdit()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", TableName);
                var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                string F8_table = "";

                if (aldm.Rows.Count == 1)
                {
                    var row = aldm.Rows[0];
                    F8_table = row["F8_TABLE"].ToString().Trim();
                    //code_field = row[""].ToString().Trim();
                }

                var v = Categories.IsExistOneCode_List(F8_table, "MA_LOAI_LG", txtMaLoaiLuong.Text);
                txtMaLoaiLuong.Enabled = !v;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaLoaiLuong.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaLoaiLuong.Text;
            if (txtTenLoaiLg.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenLoaiLg.Text;

            if (Mode == V6Mode.Edit)
            {
             
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI_LG",
                 txtMaLoaiLuong.Text.Trim(), DataOld["MA_LOAI_LG"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaLoaiLuong.Text + "=" + txtMaLoaiLuong.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI_LG",
                    txtMaLoaiLuong.Text.Trim(), txtMaLoaiLuong.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaLoaiLuong.Text + "=" + txtMaLoaiLuong.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
