using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class LoaiNhapXuatAddEditForm : AddEditControlVirtual
    {
        public LoaiNhapXuatAddEditForm()
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
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_LNX", TXTma_lnx.Text);
                TXTma_lnx.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("LoaiNhapXuatAddEditForm DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_lnx.Text.Trim() == "" || txtten_loai.Text.Trim() == "" || TxtLoai.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM, 0, "MA_LNX", TXTma_lnx.Text.Trim(), DataOld["MA_LNX"].ToString(),
                    "LOAI",TxtLoai.Text.Trim(), DataOld["LOAI"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_LNX = " + TXTma_lnx.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM, 1, "MA_LNX",TXTma_lnx.Text.Trim(), TXTma_lnx.Text.Trim(),
                    "LOAI",TxtLoai.Text.Trim(),TxtLoai.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_LNX = " + TXTma_lnx.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
