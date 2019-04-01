using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Acosxlsx_alloaiytAddEditForm : AddEditControlVirtual
    {
        public Acosxlsx_alloaiytAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtLoai_yt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblLoai_yt.Text;
            if (txtghi_chu.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenLoai_yt.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "LOAI_YT", txtLoai_yt.Text.Trim(), DataOld["LOAI_YT"].ToString());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.ExistData, lblLoai_yt.Text, txtLoai_yt.Text));
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "LOAI_YT", txtLoai_yt.Text.Trim(), txtLoai_yt.Text.Trim());
                if (!b) throw new Exception(string.Format("{0} {1} = {2}", V6Text.ExistData, lblLoai_yt.Text, txtLoai_yt.Text));
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
