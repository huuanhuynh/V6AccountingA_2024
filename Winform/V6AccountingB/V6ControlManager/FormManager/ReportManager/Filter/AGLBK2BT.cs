using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLBK2BT : FilterBase
    {
        public AGLBK2BT()
        {
            InitializeComponent();
           
            F3 = true;
            F5 = false;
            fstart = 32;
            ffixcolumn = 6;
            TxtGroupby.Text = "1";
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");


            txtnh_phi1.VvarTextBox.SetInitFilter("loai_nh=1");
            txtnh_phi2.VvarTextBox.SetInitFilter("loai_nh=2");
            txtnh_phi3.VvarTextBox.SetInitFilter("loai_nh=3");
            SetHideFields("V");
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
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", "");

            //@ngay_ct1 char(8), ---Từ ngày
            //@ngay_ct2 char(8), ---Đến ngày
            //@tk VARCHAR(16) = '%', ---Tài khoản
            //@noco char(1), ---1:no; 2:co
            //@condition varchar(max) , 
            //@M_LAN VARCHAR(8) = 'V',
            //@M_FC VARCHAR(8) = 'VN'



            var result = new List<SqlParameter>();

            
          
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;

                        
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }
            result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));
            result.Add(new SqlParameter("@noco", TxtGroupby.Text.Trim()));
            result.Add(new SqlParameter("@M_LAN", V6Setting.Language.Trim()));
            var parent = this.Parent.Parent.Parent as ReportD.ReportDViewBase;
            result.Add(new SqlParameter("@M_FC", parent.MAU));
            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_BPHT","MA_DVCS","MA_BP", "MA_KH", "MA_VV","TK_DU", "MA_SP","MA_KU","MA_CT","TK","MA_PHI","MA_SONB"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);

            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_PHI1","NH_PHI2","NH_PHI3"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }
            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }
            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_phi in (select ma_phi from alphi where {0} )", key2);
            }
           
            if (TxtGroupby.Text == "1")
                cKey = cKey + " AND ((ISNULL(PS_NO,0)+ISNULL(PS_NO_NT,0))<>0)";
            else if (TxtGroupby.Text == "2")
            {
                cKey = cKey + " AND ((ISNULL(PS_CO,0)+ISNULL(PS_CO_NT,0))<>0)";
            }
                
            
            result.Add(new SqlParameter("@condition", cKey));


            return result;
        }

      
    }
}
