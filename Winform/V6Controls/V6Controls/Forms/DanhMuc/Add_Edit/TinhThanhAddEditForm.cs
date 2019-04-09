using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TinhThanhAddEditForm : AddEditControlVirtual
    {
        public TinhThanhAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_tinh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtten_tinh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TINH",
                 txtma_tinh.Text.Trim(), DataOld["MA_TINH"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_TINH = " + txtma_tinh.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TINH",
                 txtma_tinh.Text.Trim(), txtma_tinh.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TINH = " + txtma_tinh.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
