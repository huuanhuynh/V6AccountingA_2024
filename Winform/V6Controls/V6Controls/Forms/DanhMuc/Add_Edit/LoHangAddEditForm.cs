using System;
using V6AccountingBusiness;
using V6Init;
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
            txtMaVT.SetInitFilter("Lo_yn='1'");
            KeyField1 = "MA_LO";
            KeyField2 = "MA_VT";
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistTwoCode_List("ABLO,ARI70", "Ma_vt", txtMaVT.Text.Trim(), "Ma_lo",
                txtMaLo.Text.Trim());
            txtMaLo.Enabled = !v;
            txtMaVT.Enabled = !v;
            txtngay_hhsd.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaLo.Text.Trim() == "")
            {
                errors += V6Text.Text("CHUANHAP") + " " + lblMaLo.Text;
                txtMaLo.Focus();
            }
            if (txtMaVT.Text.Trim() == "")
            {
                errors += V6Text.Text("CHUANHAP") + " " + lblMaVT.Text;
                txtMaVT.Focus();
            }
            txtMaVT.RefreshLoDateYnValue();
            if (txtMaVT.DATE_YN)
            {
                if (txtngay_hhsd.Value == null)
                {
                    errors += V6Text.Text("CHUANHAP") + " " + lblNgayHHSD.Text;
                    txtngay_hhsd.Focus();
                }
            }

            
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM.ToString(), 0,
                    "MA_VT", txtMaVT.Text.Trim(), DataOld["MA_VT"].ToString(),
                    "MA_LO", txtMaLo.Text.Trim(), DataOld["MA_LO"].ToString());

                if (!b) errors += V6Text.DataExist + V6Text.EditDenied;

            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM.ToString(), 1,
                    "MA_VT", txtMaVT.Text, txtMaVT.Text,
                    "MA_LO", txtMaLo.Text, txtMaLo.Text);

                if (!b) errors += V6Text.DataExist + V6Text.AddDenied;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void TxtMa_lo_TextChanged(object sender, EventArgs e)
        {
            if (_ready0 && Mode == V6Mode.Add)
            {
                txtTenLo.Text = txtMaLo.Text;
                txtSoLo.Text = txtMaLo.Text;
            }
        }
    }
}
