using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class APDMO_AP : FilterBase
    {
        public APDMO_AP()
        {
            InitializeComponent();

            F3 = true;
            F4 = true;
            F8 = true;
            F9 = true;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            txtma_ct.VvarTextBox.Text = "S0K";
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            ctDenSo.Enabled = false;
         
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");

            var invoice = new V6AccountingBusiness.Invoices.V6Invoice81();
            var cKey = "1=1";
            var and_or = " and ";
            var tbL = "";

            //so chung tu
            if (chkLike.Checked)
            {
                if (tu_so != "")
                {
                    cKey += (cKey.Length > 0 ? and_or : "")
                       + string.Format("so_ct like '%{0}'",
                        tu_so + ((tu_so.Contains("_") || tu_so.Contains("%")) ? "" : "%"));
                }
            }
            else
            {
                var dinh_dang = invoice.Alct["DinhDang"].ToString().Trim();
                if (!string.IsNullOrEmpty(dinh_dang))
                {
                    if (tu_so != "") tu_so = (dinh_dang + tu_so).Right(dinh_dang.Length);
                    if (den_so != "") den_so = (dinh_dang + den_so).Right(dinh_dang.Length);
                }
                if (tu_so != "" && den_so == "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so);
                }
                else if (tu_so == "" && den_so != "")
                {
                    cKey += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                       cKey.Length > 0 ? and_or : "",
                       tbL,
                       den_so);
                }
                else if (tu_so != "" && den_so != "")
                {
                    cKey += string.Format("{0} (LTrim(RTrim({1}so_ct)) >= '{2}' and LTrim(RTrim({1}so_ct)) <= '{3}')",
                        cKey.Length > 0 ? and_or : "",
                        tbL,
                        tu_so, den_so)
                    ;
                }
            }

            var and = radAnd.Checked;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH","TK"
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

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@mua_ban", "M"));
            result.Add(new SqlParameter("@ma_ct", txtma_ct.IsSelected ? txtma_ct.StringValue : "" + "%"));
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@advance", cKey));


            return result;
        }
        
        private void Filter_Load(object sender, EventArgs e)
        {

        }


        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }
        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

       
    }
}
