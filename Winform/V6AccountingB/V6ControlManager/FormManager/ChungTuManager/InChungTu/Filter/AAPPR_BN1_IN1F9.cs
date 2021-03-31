using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public partial class AAPPR_BN1_IN1F9 : InChungTuFilterBase
    {
        public AAPPR_BN1_IN1F9()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            SetFieldValueEvent += Form_SetFieldValueEvent;

            SetHideFields("V");
        }

        void Form_SetFieldValueEvent(string sttrec)
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
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("SO_TKNH1", soTkTaiNH1.Text);
            RptExtraParameters.Add("TEN_NH1", tenNH1.Text);
            RptExtraParameters.Add("TINH_TP1", tinhTP1.Text);
            RptExtraParameters.Add("TEN_DV_NHANTIEN", tenDVnhanTien.Text);
            RptExtraParameters.Add("SO_TKNH2", soTKtaiNH2.Text);
            RptExtraParameters.Add("TEN_NH2", tenNH2.Text);
            RptExtraParameters.Add("TINH_TP2", tinhTP2.Text);
            RptExtraParameters.Add("NOIDUNG_TT", noiDungTT.Text);
            RptExtraParameters.Add("MAU_RIENG", mauRieng.Text);

            var result = new List<SqlParameter>();
            
            
            result.Add(new SqlParameter("@stt_rec", TxtStt_rec.Text.Trim()));
            //result.Add(new SqlParameter("@HoaDonMau", "0"));
            
            
            return result;
        }

        
    }
}
