﻿using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public partial class AARCTAR1 : InChungTuFilterBase
    {
        public AARCTAR1()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            // parameters for rpt
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("HOA_TTKH", false);
            RptExtraParameters.Add("HOA_TENKH", false);
            RptExtraParameters.Add("HOA_DIACHIKH", false);
            //TxtStt_rec = _report_stt_rec;
            SetFieldValueEvent += ASOCTSOA_SetFieldValueEvent;

            SetHideFields("V");
        }

        void ASOCTSOA_SetFieldValueEvent(string sttrec)
        {
            TxtStt_rec.Text = sttrec;
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");

                _hideFields.Add("T_TT_NT0", "T_TT_NT0");
                _hideFields.Add("T_TT_NT", "T_TT_NT");
                _hideFields.Add("DA_TT_NT", "DA_TT_NT");
                _hideFields.Add("CON_PT_NT", "CON_PT_NT");
                _hideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                _hideFields.Add("T_THUE_NT", "T_THUE_NT");
            }
            else
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@stt_rec; varchar(50),
            
            var result = new List<SqlParameter>();
            
            
            result.Add(new SqlParameter("@stt_rec", TxtStt_rec.Text.Trim()));
            result.Add(new SqlParameter("@HoaDonMau", chkMau.Checked ? "1" : "0"));
            
            
            return result;
        }

        private void chkHoaTTKH_CheckedChanged(object sender, System.EventArgs e)
        {
            RptExtraParameters["HOA_TTKH"] = chkHoaTTKH.Checked;
        }

        private void chkHoaTenKH_CheckedChanged(object sender, System.EventArgs e)
        {
            RptExtraParameters["HOA_TENKH"] = chkHoaTenKH.Checked;
        }

        private void chkHoaDCKH_CheckedChanged(object sender, System.EventArgs e)
        {
            RptExtraParameters["HOA_DIACHIKH"] = chkHoaDCKH.Checked;
        }

        
    }
}
