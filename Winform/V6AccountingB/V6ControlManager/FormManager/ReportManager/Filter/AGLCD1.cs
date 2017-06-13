using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLCD1 : FilterBase
    {
        public AGLCD1()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;


            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            TxtGroupby.Text = "1";

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
           // @Ngay_ct1 SmallDateTime, --Từ ngày
	       // @Ngay_ct2 SmallDateTime, --Đến ngày
	       // @Bu_tru CHAR(1) = '0', -- Bù trừ tài khoản công nợ
	       // @Advance AS VARCHAR(8000) = '' -- Điều kiện lọc danh mục khách hàng
           // @Kieu_F5 CHAR(1)

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;

            var result = new List<SqlParameter>();

           

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Bu_tru", TxtGroupby.Text.Trim()));
          
            
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
            var Kieu_F5=chkKieu_f5.Checked ? "1" : "0" ;

            result.Add(new SqlParameter("@Advance", cKey));
            result.Add(new SqlParameter("@Kieu_F5", Kieu_F5));
            return result;
        }

        
    }
}
