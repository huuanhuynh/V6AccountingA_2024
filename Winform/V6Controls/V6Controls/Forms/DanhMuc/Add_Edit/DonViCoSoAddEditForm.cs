using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class DonViCoSoAddEditForm : AddEditControlVirtual
    {
        public DonViCoSoAddEditForm()
        {
            InitializeComponent();
        }

        private void v6ColorTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARA00", "Ma_dvcs", TxtMa_dvcs.Text);
                TxtMa_dvcs.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_dvcs.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTenDvcs.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_DVCS",
                 TxtMa_dvcs.Text.Trim(), DataOld["MA_DVCS"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_DVCS = " + TxtMa_dvcs.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_DVCS",
                 TxtMa_dvcs.Text.Trim(), TxtMa_dvcs.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_DVCS = " + TxtMa_dvcs.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
