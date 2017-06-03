using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOBCGIA2 : FilterBase
    {
        public ASOBCGIA2()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            SetParentRowEvent += ASOTH1F5_SetParentRowEvent;
            SetHideFields("V");
        }

        void ASOTH1F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            txtma_gia.VvarTextBox.Text = row["MA_GIA"].ToString().Trim();
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");

                _hideFields.Add("GIA_NT2", "GIA_NT2");
                _hideFields.Add("GIA_NT", "GIA_NT");
                _hideFields.Add("TT_NT", "TT_NT");
                _hideFields.Add("TIEN_NT2", "TIEN_NT2");
                
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
            //@dPrice SMALLDATETIME, 
            //@cMaGia VARCHAR(32) 
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("CR_NGAY_CT2", dateNgay_ct2.Value);
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@dPrice", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@cMaGia",txtma_gia.StringValue ));
            result.AddRange(InitFilters);
            return result;
        }

        
    }
}
