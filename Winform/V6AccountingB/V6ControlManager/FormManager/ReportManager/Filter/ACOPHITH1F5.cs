using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOPHITH1F5 : FilterBase
    {
        public ACOPHITH1F5()
        {
            InitializeComponent();
            F3 = false;
            F5 = true;
            SetParentRowEvent += ACOPHITH1F5_SetParentRowEvent;

            //txtMaDvcs.VvarTextBox.Text = V6LoginInfo.Madvcs;
            //if (V6LoginInfo.MadvcsCount <= 1)
            //{
            //    txtMaDvcs.Enabled = false;
            //}
            SetHideFields("V");
        }

        void ACOPHITH1F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            if (row["TK_DU"].ToString().Trim() != "")
                Tk_du_filterLine.IsSelected = true;
            else
                Tk_du_filterLine.IsSelected = false;

            Tk_du_filterLine.VvarTextBox.Text = row["TK_DU"].ToString().Trim();

            Tk_filterLine.VvarTextBox.Text = row["TK"].ToString().Trim();
            txtma_phi.VvarTextBox.Text = row["MA_PHI"].ToString().Trim();

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
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                "MA_PHI","TK","TK_DU"

            }, true);

            result.Add(new SqlParameter("@Condition2", keyf5));
            return result;
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }
    }
}
