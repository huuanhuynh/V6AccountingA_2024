using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class KhoHangAddEditForm : AddEditControlVirtual
    {
        public KhoHangAddEditForm()
        {
            InitializeComponent();
            if (V6Login.MadvcsTotal > 0)
            {
                TxtMa_dvcs.CheckNotEmpty = true;
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                    TxtMa_dvcs.Enabled = false;
                }
            
            }
            else
            {
                TxtMa_dvcs.Enabled = false;
            }
        

        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ABVT,ABLO,ARI70,ARS90", "Ma_kho", TxtMa_kho.Text);
            TxtMa_kho.Enabled = !v;
            TxtMa_dvcs.Enabled = !v;
        }

        private void TxtKho_dl_V6LostFocus(object sender)
        {

            if (TxtKho_dl.Value == 1)
            {
                TxtTk_dl.Enabled = true;
                TxtTk_dl.ReadOnly = false;
            }
            else
            {
                TxtTk_dl.ReadOnly = true;
                TxtTk_dl.Enabled = false;
                TxtTk_dl.Text = "";
            }


        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_kho.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (TxtTen_kho.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KHO",
                 TxtMa_kho.Text.Trim(), DataOld["MA_KHO"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: " + "MA_KHO = " + TxtMa_kho.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KHO",
                 TxtMa_kho.Text.Trim(), TxtMa_kho.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KHO = " + TxtMa_kho.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}
