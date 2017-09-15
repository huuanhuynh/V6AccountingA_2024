using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public partial class ASOCTSOA : InChungTuFilterBase
    {
        public ASOCTSOA()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            // parameters for rpt
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("HOA_TTKH", false);
            RptExtraParameters.Add("HOA_TENKH", false);
            RptExtraParameters.Add("HOA_DIACHIKH", false);
            
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

        private DataRow _tbl2Row = null;
        public override void Call1(object s = null)
        {
            _tbl2Row = s as DataRow;
            var parent = FindParent<InChungTuViewBase>() as InChungTuViewBase;
            if (_tbl2Row != null && parent != null)
            {
                var t_tien_nt = ObjectAndString.ObjectToDecimal(_tbl2Row["T_TIEN_NT"]);
                var t_tien = ObjectAndString.ObjectToDecimal(_tbl2Row["T_TIEN"]);
                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();

                RptExtraParameters["SOTIENVIETBANGCHU_TIENBANNT"] = V6BusinessHelper.MoneyToWords(t_tien_nt, parent.LAN, ma_nt);
                RptExtraParameters["SOTIENVIETBANGCHU_TIENBAN"] = V6BusinessHelper.MoneyToWords(t_tien, parent.LAN, V6Options.M_MA_NT0);

            }
        }
    }
}
