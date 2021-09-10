using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLSD2 : FilterBase
    {
        public AGLSD2()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            txtTK.Text = "11";

            dateNgay_ct.SetValue(V6Setting.M_ngay_ct1);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string> { { "TAG", "TAG" } };
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
            //@Ma_dvcs varchar(50),
            //@Tk as varchar(50) = ''

            //txtMaDvcs.StringValue.Trim() + "%"
            var ma_dvcs = txtMaDvcs.IsSelected ? txtMaDvcs.StringValue.Trim() + "%" : "%";

            var result = new List<SqlParameter>
            {
                new SqlParameter("@Ngay", dateNgay_ct.YYYYMMDD),
                new SqlParameter("@Ma_dvcs",ma_dvcs),
                new SqlParameter("@Tk", txtTK.Text.Trim() + "%")
            };
            
            return result;
        }

        
    }
}
