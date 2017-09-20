using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TaiKhoanAddEditForm : AddEditControlVirtual
    {
        public TaiKhoanAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }
        private void MyInit()
        {
            txtNhomtk1.SetInitFilter("Loai_nh=1");
            txtNhomtk2.SetInitFilter("Loai_nh=2");
            txtNhomtk3.SetInitFilter("Loai_nh=3");
            //KeyField1 = "TK";
        }
        public override void FixFormData()
        {
            if (TxtTk_me.Text.Trim() != "")
            {
                txtLoai_tk.Value = 1;
            }
            else
            {
                txtLoai_tk.Value = 0;
            }
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

                var v = Categories.IsExistOneCode_List("ABKH,ARA00", "Tk", TxtTk.Text);
                TxtTk.Enabled = !v;

                if (!V6Init.V6Login.IsAdmin && TxtTk.Text.ToUpper() != V6Init.V6Login.Madvcs.ToUpper())
                {
                    TxtTk.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("MA_NH DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtTk.Text.Trim() == "" || TxtTen_tk.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (TxtTk_me.Text.Trim() != "")
            {
                var check_tk_me = Categories.IsExistOneCode_List("ABKH,ARA00", "Tk", TxtTk_me.Text.Trim());
                if (check_tk_me)
                    errors += "Tài khoản mẹ đã có phát sinh !\r\n";
            }

           
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "TK",
                 TxtTk.Text.Trim(), DataOld["TK"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                                    + "TK = " + TxtTk.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "TK",
                 TxtTk.Text.Trim(), TxtTk.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                                    + "TK = " + TxtTk.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
