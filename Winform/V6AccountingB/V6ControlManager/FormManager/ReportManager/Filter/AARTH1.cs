using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARTH1 : FilterBase
    {
        public AARTH1()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;

            TxtTk.SetInitFilter("tk_cn=1");

            TxtTk.Text = (V6Setting.M_TK_CN ?? "131").Trim();
            TxtMa_kh.Text = (V6Setting.M_Ma_kh ?? "").Trim();
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;


            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
           

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
            //@Tk_cn char(16),
            //@Ma_kh varchar(50),
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Filter nvarchar(MAX)

            var result = new List<SqlParameter>();


            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }
            if (TxtMa_kh.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn khách hàng!");
            }

            V6Setting.M_TK = TxtTk.Text;
            V6Setting.M_TK_CN = TxtTk.Text;
            V6Setting.M_Ma_kh = TxtMa_kh.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            result.Add(new SqlParameter("@Tk_cn", TxtTk.Text.Trim()));
            result.Add(new SqlParameter("@Ma_kh", TxtMa_kh.Text.Trim()));
            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.Value.ToString("yyyyMMdd")));


            var and = radAnd.Checked;

            string cKey;
             var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
            }, and);
            
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = " AND "+string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = " AND " +string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "";
            }

            result.Add(new SqlParameter("@Filter", cKey));

            return result;
            
        
        }

        
    }
}
