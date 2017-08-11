using System.Data.SqlClient;
using V6AccountingBusiness;
using System;
using V6Structs;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinNgoaiNguForm : AddEditControlVirtual
    {
        public ThongTinNgoaiNguForm()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

          
        }
        public override void DoBeforeAdd()
        {
            int loai = 3;
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", "HR1"),
                new SqlParameter("@loai", loai),
                new SqlParameter("@id_name", "ID"),
                new SqlParameter("@tablename", "HRLSTLANGUAGE"),
            };
            //var txtID_Text = V6BusinessHelper.ExecuteFunctionScalar("VFA_Getstt_rec_tt_like", plist);
            var txtID_Text = V6BusinessHelper.ExecuteProcedureScalar("VPA_sGet_Key_Like_stt_rec_tt", plist);
            txtID.Text = "" + txtID_Text;
        }

        private void txtclass_TextChanged(object sender, System.EventArgs e)
        {
           
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtName.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
            if (errors.Length > 0) throw new Exception(errors);

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "NAME",
                 txtName.Text.Trim(), DataOld["NAME"].ToString());
                if (!b)
                    throw new Exception("Không được sửa tên đã tồn tại: "
                                                    + "NAME = " + txtName.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "NAME",
                 txtName.Text.Trim(), txtName.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm tên đã tồn tại: "
                                                    + "NAME = " + txtName.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}
