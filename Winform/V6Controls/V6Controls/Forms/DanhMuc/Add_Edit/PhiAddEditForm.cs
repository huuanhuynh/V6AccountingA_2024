using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class PhiAddEditForm : AddEditControlVirtual
    {
        public PhiAddEditForm()
        {
            InitializeComponent();
            Txtnh_phi1.SetInitFilter("Loai_nh=1");
            Txtnh_phi2.SetInitFilter("Loai_nh=2");
            Txtnh_phi3.SetInitFilter("Loai_nh=3");

        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_phi.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TxtTen_phi.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_PHI",
                 TxtMa_phi.Text.Trim(), DataOld["MA_PHI"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_PHI = " + TxtMa_phi.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_PHI",
                 TxtMa_phi.Text.Trim(), TxtMa_phi.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_PHI = " + TxtMa_phi.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
