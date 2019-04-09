using System;
using V6AccountingBusiness;
using V6Init;
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
            txtMa_NT.ExistRowInTable();
        }

        public override void DoBeforeAdd()
        {
            txtMa_NT.ExistRowInTable();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_NT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (TxtNgay_ct.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblNgayCT.Text;
            
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(TableName.ToString(), 0,
                    "MA_NT", "NGAY_CT",
                    txtMa_NT.Text, TxtNgay_ct.YYYYMMDD,
                    DataOld["MA_NT"].ToString(), DataOld["NGAY_CT"].ToString());

                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "," + lblNgayCT.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_OneDate(TableName.ToString(), 1,
                    "MA_NT", "NGAY_CT",
                    txtMa_NT.Text, TxtNgay_ct.YYYYMMDD,
                    txtMa_NT.Text, TxtNgay_ct.YYYYMMDD);

                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "," + lblNgayCT.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
        
    }
}
