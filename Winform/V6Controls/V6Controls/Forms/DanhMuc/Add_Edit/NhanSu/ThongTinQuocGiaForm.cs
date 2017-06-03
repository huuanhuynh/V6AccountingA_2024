using System.Data.SqlClient;
using V6AccountingBusiness;
using System;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinQuocGiaForm : AddEditControlVirtual
    {
        public ThongTinQuocGiaForm()
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
                new SqlParameter("@tablename", "HRLSTNATIONAL")
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
        }
    }
}
