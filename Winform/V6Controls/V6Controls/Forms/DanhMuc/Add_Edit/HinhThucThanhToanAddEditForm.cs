using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HinhThucThanhToanAddEditForm : AddEditControlVirtual
    {
        public HinhThucThanhToanAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            TxtTk_no.FilterStart = true;
            TxtTk_no.SetInitFilter("loai_tk=1");
            
        }
        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARS20", "Ma_httt", txtMaHttt.Text);
                txtMaHttt.Enabled = !v;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HTTT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaHttt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaHttt.Text;
            if (txtTenHttt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenHttt.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_HTTT",
                 txtMaHttt.Text.Trim(), DataOld["MA_HTTT"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: " + "MA_HTTT = " + txtMaHttt.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_HTTT",
                 txtMaHttt.Text.Trim(), txtMaHttt.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_HTTT = " + txtMaHttt.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
