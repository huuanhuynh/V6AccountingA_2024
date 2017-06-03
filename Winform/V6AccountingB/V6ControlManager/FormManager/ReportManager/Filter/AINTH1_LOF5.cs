using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINTH1_LOF5 : FilterBase
    {
        public AINTH1_LOF5()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            SetParentRowEvent += ASOTH1F5_SetParentRowEvent;

            //txtMaDvcs.VvarTextBox.Text = V6LoginInfo.Madvcs;
            //if (V6LoginInfo.MadvcsCount <= 1)
            //{
            //    txtMaDvcs.Enabled = false;
            //}
            SetHideFields("V");
        }

        void ASOTH1F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
            txtma_lo.VvarTextBox.Text = row["MA_LO"].ToString().Trim();
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
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", V6Setting.M_ngay_ct1);
            RptExtraParameters.Add("NGAY_CT2", V6Setting.M_ngay_ct2);


            RptExtraParameters.Add("MA_KHO", ParentFilterData["MA_KHO"]);
            RptExtraParameters.Add("MA_VT", ma_vt_filterLine.VvarTextBox.Text);


            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_VT","MA_LO"
                
            }, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
