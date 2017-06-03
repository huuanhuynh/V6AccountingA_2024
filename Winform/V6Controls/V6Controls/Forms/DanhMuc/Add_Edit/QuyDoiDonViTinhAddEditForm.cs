using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class QuyDoiDonViTinhAddEditForm : AddEditControlVirtual
    {
        public QuyDoiDonViTinhAddEditForm()
        {
            InitializeComponent();
            TxtMa_vt.SetInitFilter("NHIEU_DVT='1'");
        }

        private void QuyDoiDonViTinhAddEditForm_Load(object sender, System.EventArgs e)
        {
            if (TxtMa_vt.Data != null)
            {
                TxtDvtC.Text = TxtMa_vt.Data["Dvt"].ToString();
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistTwoCode_List("ARI70", "Ma_vt", TxtMa_vt.Text.Trim(), "DVT1", TxtDvt.Text.Trim());
                TxtDvt.Enabled = !v;
                TxtMa_vt.Enabled = !v;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DisableControl " + ex.Message);
            }

        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtDvt.Text.Trim() == "")
                errors += "Chưa nhập ĐVT!\r\n";
            if (TxtDvtqd.Text.Trim() == "")
                errors += "Chưa nhập ĐVT QĐ!\r\n";
            if (TxtMa_vt.Text.Trim() == "")
                errors += "Chưa nhập mã vật tư!\r\n";

            if (TxtDvt.Text.Trim() == TxtDvtqd.Text.Trim())
                errors += "Trùng đơn vị tính !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(),0,
                    "MA_VT",TxtMa_vt.Text, DataOld["MA_VT"].ToString(),
                    "DVT", TxtDvt.Text, DataOld["DVT"].ToString());

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 1,
                    "MA_VT", TxtMa_vt.Text, TxtMa_vt.Text,
                    "DVT", TxtDvt.Text, TxtDvt.Text);

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void TxtMa_vt_V6LostFocus(object sender)
        {
            if (TxtMa_vt.Data != null)
            {
                TxtDvtC.Text = TxtMa_vt.Data["Dvt"].ToString();
                if (Mode == V6Mode.Add)
                {
                    TxtDvtqd.Text = TxtMa_vt.Data["Dvt"].ToString();
                    TxtXtype.Text = "1";

                }
            }
          

        }

        
    }
}
