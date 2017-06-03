using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARSD2 : FilterBase
    {
        public AARSD2()
        {
            InitializeComponent();
            TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = false;

            TxtTk.Text = (V6Setting.M_TK_CN ?? "131").Trim();
            TxtMa_kh.Text = (V6Setting.M_Ma_kh ?? "").Trim();

            dateNgay_ct0.Value = V6Setting.M_ngay_ct1;


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
            }
            else
            {
                
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
               
          
            //@ma_kh VARCHAR(50),
            //@tk VARCHAR(50),
            //@ngay_ct SMALLDATETIME,
            //@ma_dvcs VARCHAR(50)

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


            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@ma_kh", TxtMa_kh.Text.Trim()));
            result.Add(new SqlParameter("@tk", TxtTk.Text.Trim()));
            result.Add(new SqlParameter("@ngay_ct", dateNgay_ct0.Value.ToString("yyyyMMdd")));


            var and = radAnd.Checked;
            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
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
                cKey = "";
            }


            result.Add(new SqlParameter("@Advance", cKey));
            return result;
                        
                        
                       
        }

        
    }
}
