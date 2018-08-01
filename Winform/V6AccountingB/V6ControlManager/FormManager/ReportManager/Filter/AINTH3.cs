using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINTH3 : FilterBase
    {
        public AINTH3()
        {
            InitializeComponent();
            F3 = false;
            F5 = true;
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            chkGiamTru.Checked = true;
            String1 = txtLoaiBaoCao.Text;
            String2 = txtChiTietTheo.Text;

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");

            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields.Add("TAG", "TAG");

            }
            else
            {

            }
            GridViewHideFields.Add("STT_REC", "STT_REC");
            GridViewHideFields.Add("STT_REC0", "STT_REC0");

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

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;


            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));

            


            var and = radAnd.Checked;
            
            var cKey = "";
          

            var key0 = GetFilterStringByFields(new List<string>()
            {
              "MA_VT","MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX","MA_NVIEN","MA_CT","MA_BPHT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
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
            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }
            result.Add(new SqlParameter("@Loai_bc", (int)txtLoaiBaoCao.Value));
            result.Add(new SqlParameter("@Ct_theo", (int)txtChiTietTheo.Value));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));
            result.Add(new SqlParameter("@advance", cKey));

            return result;
        }

        private void txtLoaiBaoCao_ChiTietTheo_TextChanged(object sender, System.EventArgs e)
        {
            String1 = txtLoaiBaoCao.Text;
            String2 = txtChiTietTheo.Text;
        }

        
    }
}
