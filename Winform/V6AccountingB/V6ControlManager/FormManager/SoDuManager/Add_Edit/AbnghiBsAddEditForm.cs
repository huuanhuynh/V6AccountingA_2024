using System;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbnghiBsAddEditForm : SoDuAddEditControlVirtual
    {
        public AbnghiBsAddEditForm()
        {
            InitializeComponent();
        }

        private void AbnghiBsAddEditForm_Load(object sender, EventArgs e)
        {
            txtMaKH.ExistRowInTable();
            TxtMa_dvcs.ExistRowInTable();
            txtMaKH.Focus();
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

            AldmConfig config = ConfigManager.GetAldmConfig(_MA_DM);
            if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
            {
                var key_list = ObjectAndString.SplitString(config.KEY);
                errors += CheckValid(_MA_DM, key_list);
            }
            
            if (errors.Length > 0) throw new Exception(errors);
        }
        
       
    }
}
