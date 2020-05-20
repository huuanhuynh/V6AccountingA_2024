using System.Data.SqlClient;
using V6AccountingBusiness;
using System;
using V6Init;
using V6Structs;


namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class ThongTinDieuKienSongForm : AddEditControlVirtual
    {
        public ThongTinDieuKienSongForm()
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
                new SqlParameter("@tablename", "HRLSTLIVINGARR"),
            };
            //var txtID_Text = V6BusinessHelper.ExecuteFunctionScalar("VFA_Getstt_rec_tt_like", plist);
            var txtID_Text = V6BusinessHelper.ExecuteProcedureScalar("VPA_sGet_Key_Like_stt_rec_tt", plist);
            txtID.Text = "" + txtID_Text;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtName.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblName.Text;
            if (errors.Length > 0) throw new Exception(errors);

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "NAME",
                 txtName.Text.Trim(), DataOld["NAME"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblName.Text + "=" + txtName.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "NAME",
                 txtName.Text.Trim(), txtName.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblName.Text + "=" + txtName.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
