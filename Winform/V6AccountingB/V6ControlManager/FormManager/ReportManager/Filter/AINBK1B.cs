using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINBK1B : FilterBase
    {
        public AINBK1B()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;
            ctDenSo.Enabled = false;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            Txtma_ct.VvarTextBox.Text = "SOA,SOB,IXA,IXB,IXC";

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields = new SortedDictionary<string, string>();
                _hideFields.Add("TAG", "TAG");

            }
            else
            {

            }
            _hideFields.Add("STT_REC", "STT_REC");
            _hideFields.Add("STT_REC0", "STT_REC0");

        }
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@ngay_ct1 char(8),
            //@ngay_ct2 char(8),
            //@advance nvarchar(max),	
            //@pListVoucher nvarchar(max),
            //@KindFilter int
            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;


            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));

            


            var and = radAnd.Checked;
            
            var cKey = "";
          

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX","MA_NVIEN","MA_GD"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey =  string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey =  string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            // Tu so den so
            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");
            var invoice = new V6AccountingBusiness.Invoices.V6Invoice81();
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
                var dinh_dang = invoice.Alct.Rows[0]["DinhDang"].ToString().Trim();
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
            

            result.Add(new SqlParameter("@advance", cKey));

            result.Add(new SqlParameter("@pListVoucher", Txtma_ct.StringValue));
            
            result.Add(new SqlParameter("@KindFilter", (int)txtKind.Value));

            return result;
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

        
    }
}
