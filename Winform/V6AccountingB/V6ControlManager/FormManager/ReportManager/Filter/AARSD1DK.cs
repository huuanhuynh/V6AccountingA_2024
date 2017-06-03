using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARSD1DK : FilterBase
    {
        public AARSD1DK()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = false;

            String2 = "TEN_KH";
            String1 = "MA_KH";

            dateNgay_ct0.Value = V6Setting.M_ngay_ct1;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

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
                _hideFields.Add("TAG", "TAG");
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
            RptExtraParameters.Add("M_TK",txtTk.IsSelected ? txtTk.StringValue : "");

            var tk_key = new Dictionary<string, object>();
            tk_key.Add("TK", txtTk.IsSelected ? txtTk.StringValue : "");
            RptExtraParameters.Add("M_TEN_TK", "" + V6SqlConnect.SqlConnect.SelectOneValue("ALTK", "TEN_TK", tk_key));
                      
            //@EndDate	varchar(8),
            //@Filter		nvarchar(max),
            //@Condition	nvarchar(max)

            var result = new List<SqlParameter>();

          

            result.Add(new SqlParameter("@EndDate", dateNgay_ct0.Value.ToString("yyyyMMdd")));
           
                        
            
            var and = radAnd.Checked;
            
            var cKey = "";
            var cFilter = "1=1";
         
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "TK","MA_DVCS"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "MA_KH","NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                //"NH_VT1","NH_VT2","NH_VT3", "MA_QG", "MA_NSX", "TK_VT"
                ""
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

            if (!string.IsNullOrEmpty(key1))
            {
                cFilter = cFilter + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);

            }
            else
            {
                cFilter = "1=1";
            }

            if (!string.IsNullOrEmpty(key2))
            {
              //  cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }

            result.Add(new SqlParameter("@Filter", cFilter));
            result.Add(new SqlParameter("@Condition", cKey));
            result.Add(new SqlParameter("@Group", lblGroupString.Text));
            return result;
        }
        private void NH_KH_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
                foreach (Control control in groupBoxNhom.Controls)
                {
                    if (control != current && control.Text == current.Text)
                    {
                        control.Text = "0";
                    }
                }

            String2 = V6BusinessHelper.GenGroup(
                "TEN_NH", NH_KH1.Text, NH_KH2.Text, NH_KH3.Text, NH_KH4.Text, NH_KH5.Text, NH_KH6.Text);
            if (String2.Length > 0) String2 += ",";
            String2 += "TEN_KH";

            lblGroupString.Text = V6BusinessHelper.GenGroup(
                "NH_KH", NH_KH1.Text, NH_KH2.Text, NH_KH3.Text, NH_KH4.Text, NH_KH5.Text, NH_KH6.Text);
            String1 = lblGroupString.Text + (lblGroupString.Text.Length > 0 ? "," : "") + "MA_KH";

        }
        
        
    }
}
