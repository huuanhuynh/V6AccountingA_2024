using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbnghiBsAddEditForm : SoDuAddEditControlVirtual
    {
        public AbnghiBsAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeAdd()
        {
            txtNam.Value = V6Setting.YearFilter;
            txtThang1.Value = V6Setting.MonthFilter;
        }

        public override void DoBeforeEdit()
        {
            int decimal0 = V6Options.M_ROUND;
            int decimalnt = V6Options.M_ROUND_NT;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKH.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";
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
                    throw new Exception(V6Text.EditDenied + "MA_KH = " + txtMaKH.Text.Trim());
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
                    throw new Exception(V6Text.AddDenied + "MA_KH = " + txtMaKH.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void AbnghiBsAddEditForm_Load(object sender, EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            TxtMa_dvcs.ExistRowInTable();
        }

       
       
    }
}
