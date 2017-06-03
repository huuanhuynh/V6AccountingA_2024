using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLSD1 : FilterBase
    {
        public AGLSD1()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            
            dateNgay_ct.Value = V6Setting.M_ngay_ct1;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string> { { "TAG", "TAG" } };
            if (lang == "V")
            {
                
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
            //@Ngay as SmallDateTime,
            //@Advance varchar(8000)


            var result = new List<SqlParameter>
            {
                new SqlParameter("@Ngay", dateNgay_ct.Value.ToString("yyyyMMdd")),
                
            };

            var and = radAnd.Checked;
            string cKey;

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","TK"
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

            result.Add(new SqlParameter("@Advance", cKey));

            return result;
        }

        
    }
}
