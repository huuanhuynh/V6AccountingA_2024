using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6valid : AddEditControlVirtual
    {
        public V6valid()
        {
            InitializeComponent();
        }

        private void V6valid_Load(object sender, EventArgs e)
        {
            txtMaCt.Upper();
            txtMaCt.SetInitFilter("");

            if (Mode == V6Mode.Add)
            {
                var max = V6BusinessHelper.GetMaxValueTable(TableName.ToString(), "STT", "1=1");
                txtSTT.Value = max + 1;
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                txtMaCt.ExistRowInTable();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("DisableControlWhenEdit " + ex.Message, ex.Source);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtTen.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA", txtMa.Text.Trim(), DataOld["MA"].ToString());
                if (!b) throw new Exception("Không được sửa thành mã đã tồn tại: " + "MA = " + txtMa.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA", txtMa.Text.Trim(), txtMa.Text.Trim());
                if (!b) throw new Exception("Không được thêm mã đã tồn tại: " + "MA = " + txtMa.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
