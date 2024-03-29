﻿using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlkcAddEditForm : AddEditControlVirtual
    {
        public AlkcAddEditForm()
        {
            InitializeComponent();
            if (Mode == V6Mode.Add)
            {
                TxtNam.Value = V6Setting.YearFilter;
                if (V6Login.MadvcsCount == 1)
                {
                    TxtMa_dvcs.Text = V6Login.Madvcs;
                }
            }
        }

        public override void DoBeforeEdit()
        {
        }

        public override void LoadDetails()
        {
            if (Mode == V6Mode.Add)
            {
                decimal maxvalue = V6BusinessHelper.GetMaxValueTable("ALKC", "STT", "NAM=" + TxtNam.Value);
                txtstt.Value = maxvalue + 1;
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtstt.Value == 0)
                errors += V6Text.Text("CHUANHAP") + " " + lblSTT.Text;
            if (txtTen_bt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenBt.Text;
            
            AldmConfig config = ConfigManager.GetAldmConfig(_MA_DM);
            if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
            {
                var key_list = ObjectAndString.SplitString(config.KEY);
                errors += CheckValid(_MA_DM, key_list);
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void chk_kc_vv_yn_CheckedChanged(object sender, EventArgs e)
        {
          
            string _group_kc = "";

            _group_kc = (chk_kc_vv_yn.Checked ? ",MA_VV" : "")
                        + (chk_kc_bpht_yn.Checked ? ",MA_BPHT" : "")
                        + (chk_kc_sp_yn.Checked ? ",MA_SP" : "")
                        + (chk_kc_td_yn.Checked ? ",MA_TD" : "")
                        + (chk_kc_td2_yn.Checked ? ",MA_TD2" : "")
                        + (chk_kc_td3_yn.Checked ? ",MA_TD3" : "")
                        + (chk_kc_phi_yn.Checked ? ",MA_PHI" : "")
                        + (chk_kc_ku_yn.Checked ? ",MA_KU" : "");

            if (_group_kc.Length > 0)
                _group_kc = _group_kc.Substring(1);

            txtgroup_kc.Text = _group_kc;
        }

        private void AlkcAddEditForm_Load(object sender, EventArgs e)
        {
            txtTk_no.ExistRowInTable();
            txtTk_Co.ExistRowInTable();
        }
    }
}
