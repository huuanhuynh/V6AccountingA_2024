using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TuDienTuDinhNghia3AddEditForm : AddEditControlVirtual
    {
        public TuDienTuDinhNghia3AddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_td3.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (Txtten_td3.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TD3",
                 TxtMa_td3.Text.Trim(), DataOld["MA_TD3"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TD3 = " + TxtMa_td3.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TD3",
                 TxtMa_td3.Text.Trim(), TxtMa_td3.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TD3 = " + TxtMa_td3.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }
    }
}
