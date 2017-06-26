using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomViTriAddEditForm : AddEditControlVirtual
    {
        public NhomViTriAddEditForm()
        {
            
            InitializeComponent();
        }
        public override void ValidateData()
        {
            
            var errors = "";
            if (txtMa_nh.Text.Trim() == "")
                errors += V6Text.CheckInfor + "!\r\n";

            if (txtTen_nh.Text.Trim() == "")
                errors += V6Text.CheckInfor + "!\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NH",
                 txtMa_nh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception(V6Text.EditDenied+ " MA_NH = " + txtMa_nh.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NH",
                 txtMa_nh.Text.Trim(), txtMa_nh.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.AddDenied+ " MA_NH = " + txtMa_nh.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
