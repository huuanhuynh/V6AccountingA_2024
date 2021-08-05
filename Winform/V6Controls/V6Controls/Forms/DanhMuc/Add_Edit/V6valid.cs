using System;
using V6AccountingBusiness;
using V6Init;
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
                var max = V6BusinessHelper.GetMaxValueTable(_MA_DM, "STT", "1=1");
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
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtTen.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA", txtMa.Text.Trim(), DataOld["MA"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtMa.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA", txtMa.Text.Trim(), txtMa.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtMa.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }


        private void lblAfield_Click(object sender, EventArgs e)
        {
            this.ShowInfoMessage(@"AField: Các trường bắc buộc nhập cách nhau bằng dấu phẩy (,)");
        }
        private void lblAfield2_Click(object sender, EventArgs e)
        {
            this.ShowInfoMessage(@"AField2: Aname của vvarTextbox cần kiểm tra đúng giá trị");
        }
        private void lblAfield3_Click(object sender, EventArgs e)
        {
            this.ShowInfoMessage(@"Table|filter:*field-data?alvt.lo_yn=1,field2...
Table: tên bảng kiểm tra.
|filter: lọc bớt dữ liệu trên Table. Có thể bỏ qua.
*FIELD: trường kiểm tra trong Table, có * là bắt buộc có. không * thì có thể rỗng.
DATA: trường dữ liệu trên form (chi tiết).
Sau ? là điều kiện count = 0 sẽ không kiểm tra (where DATA = detail[DATA] and ?).");
        }

        

        

    }
}
