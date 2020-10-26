using System;
using V6AccountingBusiness;
using V6Init;
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
                GiaiThich();
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
                errors += V6Text.Text("CHUANHAP") + " " + lblDVT.Text;
            if (TxtDvtqd.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVTQD.Text;
            if (TxtMa_vt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaVT.Text;

            if (TxtDvt.Text.Trim() == TxtDvtqd.Text.Trim())
                errors += "Trùng đơn vị tính !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM,0,
                    "MA_VT",TxtMa_vt.Text, DataOld["MA_VT"].ToString(),
                    "DVT", TxtDvt.Text, DataOld["DVT"].ToString());

                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaVT.Text + "," + lblDVT.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(_MA_DM, 1,
                    "MA_VT", TxtMa_vt.Text, TxtMa_vt.Text,
                    "DVT", TxtDvt.Text, TxtDvt.Text);

                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaVT.Text + "," + lblDVT.Text;
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

        private void txtHE_SOT_TextChanged(object sender, EventArgs e)
        {
            GiaiThich();
        }

        private void txtHE_SOM_TextChanged(object sender, EventArgs e)
        {
            if (txtHE_SOM.Value == 0) return;
            GiaiThich();
        }

        private void GiaiThich()
        {
            if (IsReady)
            {
                if (txtHE_SOM.Value == 0) txtHE_SOM.Value = 1;
                txtHE_SO.Value = txtHE_SOT.Value/txtHE_SOM.Value;
                lblGiaiThich.Text = string.Format("1 {0} = {2:0}/{3:0} = {4:0.00} {1}", TxtDvt.Text, TxtDvtqd.Text, txtHE_SOT.Value, txtHE_SOM.Value, txtHE_SO.Value);
            }
        }

        private void txtHE_SOT_V6LostFocus(object sender)
        {
            if (txtHE_SOT.Value == 0) txtHE_SOT.Value = 1;
        }

        private void txtHE_SOM_V6LostFocus(object sender)
        {
            if (txtHE_SOM.Value == 0) txtHE_SOM.Value = 1;
            GiaiThich();
        }
        
    }
}
