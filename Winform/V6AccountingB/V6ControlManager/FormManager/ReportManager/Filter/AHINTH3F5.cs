using System.Collections.Generic;
using System.Data.SqlClient;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AHINTH3F5 : FilterBase
    {
        public AHINTH3F5()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            SetParentRowEvent += ASOTH2F5_SetParentRowEvent;

            //txtMaDvcs.VvarTextBox.Text = V6LoginInfo.Madvcs;
            //if (V6LoginInfo.MadvcsCount <= 1)
            //{
            //    txtMaDvcs.Enabled = false;
            //}
            SetHideFields("V");
        }

        void ASOTH2F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            if (String1 == "1")
                ma_vt_filterLine.VvarTextBox.Text = row["MA0"].ToString().Trim();
            if (String1 == "2")
                ma_kh_filterLine.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "3")
                ma_vv_filterLine.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "4")
                ma_nx_filterLine.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "5")
                ma_bp_filterLine.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "6")
                ma_nvien_filterline.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "7")
                ma_bpht_filterline.VvarTextBox.Text = row["MA0"].ToString().Trim();
            else if (String1 == "8")
                ma_dvcs_filterline.VvarTextBox.Text = row["MA0"].ToString().Trim();

            if (String2 == "1")
                ma_vt_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            if (String2 == "2")
                ma_kh_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "3")
                ma_vv_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "4")
                ma_nx_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "5")
                ma_bp_filterLine.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "6")
                ma_nvien_filterline.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "7")
                ma_bpht_filterline.VvarTextBox.Text = row["MA"].ToString().Trim();
            else if (String2 == "8")
                ma_dvcs_filterline.VvarTextBox.Text = row["MA"].ToString().Trim();
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string>();
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");

                GridViewHideFields.Add("GIA_NT2", "GIA_NT2");
                GridViewHideFields.Add("GIA_NT", "GIA_NT");
                GridViewHideFields.Add("TT_NT", "TT_NT");
                GridViewHideFields.Add("TIEN_NT2", "TIEN_NT2");
                
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

            //String1: Nhóm theo
            //String2: Chi tiết theo
            //1-Theo mặt hàng, 2-Theo khách, 3-Theo vv, 4-Theo mã nx, 5-Theo bộ phận
            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                String1=="1"?"MA_VT":
                String1=="2"?"MA_KH":
                String1=="3"?"MA_VV":
                String1=="4"?"MA_NX":
                String1=="5"?"MA_BP":""
                ,
                String2=="1"?"MA_VT":
                String2=="2"?"MA_KH":
                String2=="3"?"MA_VV":
                String2=="4"?"MA_NX":
                String2=="5"?"MA_BP":""
                
            }, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

        
    }
}
