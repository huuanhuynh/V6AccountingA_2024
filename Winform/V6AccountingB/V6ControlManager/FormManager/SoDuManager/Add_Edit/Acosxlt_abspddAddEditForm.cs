using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class Acosxlt_abspddAddEditForm : SoDuAddEditControlVirtual
    {
        public Acosxlt_abspddAddEditForm()
        {
            InitializeComponent();
            TxtMa_sp.SetInitFilter("Loai_vt='55'");
        }

        public override void DoBeforeAdd()
        {
            if (Mode == V6Mode.Add)
            {
                TxtNam.Value = V6Setting.YearFilter;
                Txtthang.Value = V6Setting.MonthFilter;
            }
        }

        public override void DoBeforeEdit()
        {

            int decimal0 = V6Options.M_ROUND;
            int decimalnt = V6Options.M_ROUND_NT;

            txttien_dd.DecimalPlaces = decimal0;
            txttien_dd_nt.DecimalPlaces = decimalnt;

        }

        public override void ValidateData()
        {
            var errors = "";

            string checkV6Valid = CheckV6Valid(DataDic, _MA_DM);
            if (!string.IsNullOrEmpty(checkV6Valid))
            {
                errors += checkV6Valid;
            }
            if (errors.Length > 0) throw new Exception(errors);

            errors = "";
            AldmConfig config = ConfigManager.GetAldmConfig(_MA_DM);
            if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
            {
                var key_list = ObjectAndString.SplitString(config.KEY);
                errors += CheckValid(_MA_DM, key_list);
            }

            if (errors.Length > 0) throw new Exception(errors);

        }

        private void txtsl_dd_TextChanged(object sender, EventArgs e)
        {
            txtsl_qd.Value = txtsl_dd.Value*txttl_ht.Value/100;

        }
    }
}
