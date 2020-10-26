﻿using System;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AbspddAddEditForm : SoDuAddEditControlVirtual
    {
        public AbspddAddEditForm()
        {
            InitializeComponent();
            TxtMa_sp.SetInitFilter("Loai_vt='55'");
            
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.YearFilter;
            Txtthang.Value = V6Setting.MonthFilter;
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
            if (TxtMa_bpht.Text.Trim() == "")
                errors += V6Text.NoInput + lblMaBPHT.Text;
            if (TxtMa_sp.Text.Trim() == "")
            {
                throw new Exception(V6Text.Text("LACKINFO"));
            }
            else
            {
                // Check data 
                if (Mode == V6Mode.Edit)
                {

                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(_MA_DM, 0,
                        "MA_BPHT", TxtMa_bpht.Text.Trim(), DataOld["MA_BPHT"].ToString(),
                        "MA_SP", TxtMa_sp.Text.Trim(), DataOld["MA_SP"].ToString(),
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.EditDenied);

                }
                else if (Mode == V6Mode.Add)
                {
                    
                    bool b = V6BusinessHelper.IsValidTwoCode_OneNumeric(_MA_DM, 1,
                        "MA_BPHT", TxtMa_bpht.Text.Trim(), TxtMa_bpht.Text.Trim(),
                        "MA_SP", TxtMa_sp.Text.Trim(), TxtMa_sp.Text,
                        "NAM", Convert.ToInt32(TxtNam.Value), Convert.ToInt32(TxtNam.Value));

                    if (!b)
                        throw new Exception(V6Text.Exist + V6Text.AddDenied);

                }

                if (errors.Length > 0) throw new Exception(errors);
            }
        }

        private void txtsl_dd_TextChanged(object sender, EventArgs e)
        {
            txtsl_qd.Value = txtsl_dd.Value*txttl_ht.Value/100;

        }

        private void txtsl_dd_StringValueChange(object sender, V6Controls.StringValueChangeEventArgs e)
        {
            SetToolTip((Control)sender, DocSo.DocSoThanhChu(txtsl_dd.Value));
        }
    }
}
