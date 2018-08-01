using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCD3 : FilterBase
    {
        public AGLCD3()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = false;

            TxtTk.Text = (V6Setting.M_TK ?? "111").Trim();

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
           

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");
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
            
            //@Ngay_ct1 SmallDateTime, --Từ ngày
            //@Ngay_ct2 SmallDateTime, --Đến ngày
            //@Advance AS VARCHAR(8000) = '' 


            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
                
            }

            V6Setting.M_TK = TxtTk.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            var result = new List<SqlParameter>();

           

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
                     
            
            var and = radAnd.Checked;
            
            var cKey = " TK LIKE '"+TxtTk.Text.Trim()+"%'";
            
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
            }, and);
          
            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey =cKey+" AND "+ string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = cKey + " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            

            result.Add(new SqlParameter("@Advance", cKey));
            return result;
        }

       

        
    }
}
