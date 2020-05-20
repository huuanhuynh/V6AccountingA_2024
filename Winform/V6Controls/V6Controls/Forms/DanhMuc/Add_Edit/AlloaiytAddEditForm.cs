using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlloaiytAddEditForm : AddEditControlVirtual
    {
        public AlloaiytAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "LOAI_YT", txtLoai_yt.Text);
                txtLoai_yt.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtLoai_yt.Text.Trim() == "" || txtghi_chu.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "LOAI_YT",
                    txtLoai_yt.Text.Trim(), DataOld["LOAI_YT"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "LOAI_YT = " + txtLoai_yt.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "LOAI_YT",
                    txtLoai_yt.Text.Trim(), txtLoai_yt.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "LOAI_YT = " + txtLoai_yt.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
