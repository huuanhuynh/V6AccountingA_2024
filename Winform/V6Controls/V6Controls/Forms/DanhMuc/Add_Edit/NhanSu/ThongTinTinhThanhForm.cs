﻿using System.Data.SqlClient;
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
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "CODE", txtID.Text);
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
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "CODE",
                 txtMaCode.Text.Trim(), DataOld["CODE"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaTinh.Text + "=" + txtMaCode.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "CODE",
                    txtMaCode.Text.Trim(), txtMaCode.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaTinh.Text + "=" + txtMaCode.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
