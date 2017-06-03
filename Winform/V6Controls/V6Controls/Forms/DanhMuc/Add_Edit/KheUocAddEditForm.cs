using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KheUocAddEditForm : AddEditControlVirtual
    {
        public KheUocAddEditForm()
        {
            InitializeComponent();
        }

        private void KheUocAddEditForm_Load(object sender, EventArgs e)
        {
            Txtnh_ku1.SetInitFilter("Loai_nh=1");
            Txtnh_ku2.SetInitFilter("Loai_nh=2");
            Txtnh_ku3.SetInitFilter("Loai_nh=3");
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABKU,ARA00,ARI70", "MA_KU", TxtMa_ku.Text);
            TxtMa_ku.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_ku.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (TxtTen_ku.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KU",
                 TxtMa_ku.Text.Trim(), DataOld["MA_KU"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KU = " + TxtMa_ku.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KU",
                 TxtMa_ku.Text.Trim(), TxtMa_ku.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KU = " + TxtMa_ku.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
