using System;
using V6Init;
using V6Structs;
using V6Tools;
using V6AccountingBusiness;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AbnghiAddEditForm : AddEditControlVirtual
    {
        public AbnghiAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
            else if (Mode == V6Mode.Edit)
            {
                
            }
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            txtMaKH.Focus();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                //var v = Categories.IsExistOneCode_List("ABKH,ARA00,ARI70", "Ma_kh", txtMaKH.Text);
                //txtMaKH.Enabled = !v;

                //if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                //{
                //    TxtMa_dvcs.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKH.Text.Trim() == "")
                errors +=V6Text.CheckInfor+ "\r\n";
            if (V6Login.MadvcsTotal > 0 && TxtMa_dvcs.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidFiveCode_TwoNumeric(TableName.ToString(), 0,
                    "MA_DVCS", TxtMa_dvcs.Text.Trim(), DataOld["MA_DVCS"].ToString(),
                    "MA_KH", txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString(),
                    "MA_KHO", txtMaKho.Text.Trim(), DataOld["MA_KHO"].ToString(),
                    "MA_VITRI", txtMaVitri.Text.Trim(), DataOld["MA_VITRI"].ToString(),
                    "MA_VT", txtMaVt.Text.Trim(), DataOld["MA_VT"].ToString(),
                    "NAM", Convert.ToInt32(txtNam.Value), Convert.ToInt32(txtNam.Value),
                     "THANG", Convert.ToInt32(txtThang1.Value), Convert.ToInt32(txtThang1.Value));
                if (!b)
                    throw new Exception(V6Text.EditDenied+ "MA_KH = " + txtMaKH.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidFiveCode_TwoNumeric(TableName.ToString(), 1,
                     "MA_DVCS", TxtMa_dvcs.Text.Trim(), TxtMa_dvcs.Text.Trim(),
                    "MA_KH", txtMaKH.Text.Trim(), txtMaKH.Text.Trim(),
                    "MA_KHO", txtMaKho.Text.Trim(), txtMaKho.Text.Trim(),
                    "MA_VITRI", txtMaVitri.Text.Trim(), txtMaVitri.Text.Trim(),
                    "MA_VT", txtMaVt.Text.Trim(), txtMaVt.Text.Trim(),
                    "NAM", Convert.ToInt32(txtNam.Value), Convert.ToInt32(txtNam.Value),
                     "THANG", Convert.ToInt32(txtThang1.Value), Convert.ToInt32(txtThang1.Value));
                if (!b)
                    throw new Exception(V6Text.AddDenied+ "MA_KH = " + txtMaKH.Text.Trim());
            }

            if(errors.Length>0) throw new Exception(errors);
        }
        
        private void txtThang1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }


    }
}
