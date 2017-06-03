using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class LoHangAddEditForm : AddEditControlVirtual
    {
        public LoHangAddEditForm()
        {
            InitializeComponent();

            MyInit();
        }

        private void MyInit()
        {
            TxtMa_vt.SetInitFilter("Lo_yn='1'");
            KeyField1 = "MA_LO";
            KeyField2 = "MA_VT";
        }

        public override void DoBeforeEdit()
        {
           
                var v = Categories.IsExistTwoCode_List("ABLO,ARI70", "Ma_vt", TxtMa_vt.Text.Trim(), "Ma_lo",
                    TxtMa_lo.Text.Trim());
                TxtMa_lo.Enabled = !v;
                TxtMa_vt.Enabled = !v;
                txtngay_hhsd.Enabled = !v;
            
        }

        public override void ValidateData()
        {
            var errors = "";



            if (TxtMa_lo.Text.Trim() == "")
            {
                errors += "Chưa nhập mã lô!\r\n";
                TxtMa_lo.Focus();
            }
            if (TxtMa_vt.Text.Trim() == "")
            {
                errors += "Chưa nhập mã vật tư!\r\n";
                TxtMa_vt.Focus();
            }
            TxtMa_vt.RefreshLoDateYnValue();
            if (TxtMa_vt.DATE_YN)
            {
                if (txtngay_hhsd.Value == null)
                {
                    errors += "Chưa nhập hạn sử dụng\r\n";
                    txtngay_hhsd.Focus();
                }
            }

            
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 0,
                    "MA_VT", TxtMa_vt.Text.Trim(), DataOld["MA_VT"].ToString(),
                    "MA_LO", TxtMa_lo.Text.Trim(), DataOld["MA_LO"].ToString());

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");

            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 1,
                    "MA_VT", TxtMa_vt.Text, TxtMa_vt.Text,
                    "MA_LO", TxtMa_lo.Text, TxtMa_lo.Text);

                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: ");
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void TxtMa_lo_TextChanged(object sender, EventArgs e)
        {
            if (_ready0 && Mode == V6Mode.Add)
            {
                txtTenLo.Text = TxtMa_lo.Text;
                txtSoLo.Text = TxtMa_lo.Text;
            }
        }
    }
}
