using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AAPTTBK3 : FilterBase
    {
        public AAPTTBK3()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                txtTk.VvarTextBox.SetInitFilter("tk_cn=1");

                F3 = true;
                F5 = false;

                txtTk.VvarTextBox.Text = (V6Setting.M_TK_CN ?? "331").Trim();
                txtSo_ngay.Value = 30;
                dateNgay_ct2_ptt.SetValue(V6Setting.M_ngay_ct2);
                dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

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
                lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
                lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
                lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");

                Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
                Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
                Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
                Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
                Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
                Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

                SetHideFields("V");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");

                GridViewHideFields.Add("T_TT_NT0", "T_TT_NT0");
                GridViewHideFields.Add("T_TT_NT", "T_TT_NT");
                GridViewHideFields.Add("DA_TT_NT", "DA_TT_NT");
                GridViewHideFields.Add("CON_PT_NT", "CON_PT_NT");
                GridViewHideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                GridViewHideFields.Add("T_THUE_NT", "T_THUE_NT");
            }
            else
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@cttt varchar(32),
            //@Ngay_ct2 SmallDateTime,
            //@Ngay_ct2_ptt SmallDateTime,
            //@so_ngay int,
            //@cKey nvarchar(MAX)

            var result = new List<SqlParameter>();

            if (txtTk.VvarTextBox.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }

            V6Setting.M_TK = txtTk.VvarTextBox.Text;
            V6Setting.M_TK_CN = txtTk.VvarTextBox.Text;
           V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;


            result.Add(new SqlParameter("@cttt","V_AAPTTBK3"));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2_ptt", dateNgay_ct2_ptt.YYYYMMDD));
            result.Add(new SqlParameter("@so_ngay",txtSo_ngay.Value));

            var and = radAnd.Checked;
            
            string cKey;
            
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH","MA_BP","MA_NVIEN","TK","MA_HTTT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3", "NH_VT4", "NH_VT5", "NH_VT6","MA_VT"
                
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
                cKey = cKey + string.Format(" and stt_rec in (select DISTINCT stt_rec from ARI70 where ma_ct IN ('POA','POB','POC') AND ma_vt in (select ma_vt from alvt where {0} ))", key2);
            }


            result.Add(new SqlParameter("@cKey", cKey));
            return result;
        }

        
    }
}
