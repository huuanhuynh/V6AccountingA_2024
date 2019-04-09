namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TieuKhoanAddEditForm : AddEditControlVirtual
    {
        public TieuKhoanAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txttk2.Text.Trim() == "" || txtten_tk2.Text.Trim() == "")
                errors += V6Init.V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6AccountingBusiness.V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "tk2",
                    txttk2.Text.Trim(), DataOld["tk2"].ToString());
                if (!b)
                    throw new System.Exception(V6Init.V6Text.DataExist
                                        + "tk2 = " + txttk2.Text.Trim());
            }
            //else if (Mode == V6Structs.V6Mode.Add)
            //{
            //    bool b = V6AccountingBusiness.V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NH",
            //        Txtma_nh.Text.Trim(), Txtma_nh.Text.Trim());
            //    if (!b)
            //        throw new System.Exception(V6Init.V6Text.ExistData
            //                            + "MA_NH = " + Txtma_nh.Text.Trim());
            //}

            if (errors.Length > 0) throw new System.Exception(errors);
        }
    }
}
