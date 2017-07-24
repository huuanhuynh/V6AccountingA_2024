using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanAddEditForm : AddEditControlVirtual
    {
        public BoPhanAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARA00", "Ma_bp", TxtMa_bp.Text);
                TxtMa_bp.Enabled = !v;

                if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    TxtMa_dvcs.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_bp.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TxtTen_bp.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                 TxtMa_bp.Text.Trim(), DataOld["MA_BP"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_BP = " + TxtMa_bp.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                 TxtMa_bp.Text.Trim(), TxtMa_bp.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_BP = " + TxtMa_bp.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
