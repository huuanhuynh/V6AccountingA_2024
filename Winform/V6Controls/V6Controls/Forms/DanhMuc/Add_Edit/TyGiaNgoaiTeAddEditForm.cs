using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TyGiaNgoaiTeAddEditForm : AddEditControlVirtual
    {
        public TyGiaNgoaiTeAddEditForm()
        {
            InitializeComponent();
        }
        
        public override void DoBeforeEdit()
        {
            TxtMa_nt.ExistRowInTable();
        }

        public override void DoBeforeAdd()
        {
            TxtMa_nt.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            
            if (TxtMa_nt.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";

            
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(TableName.ToString(), 0,
                    "MA_NT", "NGAY_CT",
                    TxtMa_nt.Text, TxtNgay_ct.YYYYMMDD,
                    DataOld["MA_NT"].ToString(), DataOld["NGAY_CT"].ToString());

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(TableName.ToString(), 1,
                    "MA_NT", "NGAY_CT",
                    TxtMa_nt.Text, TxtNgay_ct.YYYYMMDD,
                    TxtMa_nt.Text, TxtNgay_ct.YYYYMMDD);

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");
            }

            if (errors.Length > 0) throw new Exception(errors);
        }


        
    }
}
