using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class VuViecAddEditForm : AddEditControlVirtual
    {
        public VuViecAddEditForm()
        {
            InitializeComponent();
            

        }

        private void VuViecAddEditForm_Load(object sender, System.EventArgs e)
        {
            TxtMa_kh.ExistRowInTable();
            TxtNh_vv1.SetInitFilter("Loai_nh=1");
            TxtNh_vv2.SetInitFilter("Loai_nh=2");
            TxtNh_vv3.SetInitFilter("Loai_nh=3");
            
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABVV,ARA00,ARI70", "MA_VV", TxtMa_vv.Text);
            TxtMa_vv.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_vv.Text.Trim() == "" || TxtTen_vv.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_VV",
                    TxtMa_vv.Text.Trim(), DataOld["MA_VV"].ToString());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_VV = " + TxtMa_vv.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_VV",
                    TxtMa_vv.Text.Trim(), TxtMa_vv.Text.Trim());
                if (!b)
                    throw new Exception(V6Init.V6Text.ExistData
                                        + "MA_VV = " + TxtMa_vv.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
