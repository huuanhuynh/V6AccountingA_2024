using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ARSD_AR : FilterBase
    {
        public ARSD_AR()
        {
            InitializeComponent();

            F3 = true;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            ctDenSo.Enabled =false;
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            if (TxtMa_kh.Text.Trim() == "")
            {
                throw new Exception(V6Text.Text("CHUACHONKH"));
            }

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

            if (rdo_khongintattoan.Checked)

                cKey += " and (tat_toan=0) ";
            else
                cKey += " and (tat_toan=1) ";

                //@mua_ban CHAR(1),
                //@ma_ct VARCHAR(50),
                //@ngay_ct_1 smalldatetime,
                //@ngay_ct_2 smalldatetime,
                //@ma_kh VARCHAR(50),
                //@ma_dvcs VARCHAR(50),
                //@m_tk_tk_vt VARCHAR(50)
                //@advance
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@mua_ban", "B"));
            result.Add(new SqlParameter("@ma_ct", txtma_ct.IsSelected ? txtma_ct.StringValue : "" + "%"));
            result.Add(new SqlParameter("@ngay_ct_1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@ngay_ct_2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@ma_kh", TxtMa_kh.Text.Trim()+ "%"));
            result.Add(new SqlParameter("@ma_dvcs", txtMaDvcs.IsSelected ? txtMaDvcs.StringValue : "" + "%"));
            result.Add(new SqlParameter("@m_tk_tk_vt", "111,112"));
            result.Add(new SqlParameter("@advance", cKey));

            
            return result;
        }

        private void chkLike_CheckedChanged(object sender, EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }
    }
}
