using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARCD1 : FilterBase
    {
        public AARCD1()
        {
            InitializeComponent();
            TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = true;
            
            String2 = "TEN_KH";
            String1 = "MA_KH";

            TxtTk.Text = (V6Setting.M_TK_CN??"131").Trim();
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

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
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

           

            //@Tk VARCHAR(50), -- Tài khoản công nợ
            //@Ngay_ct1 SmallDateTime, --Từ ngày
            //@Ngay_ct2 SmallDateTime, --Đến ngày
            //@Advance AS VARCHAR(8000) 

            var result = new List<SqlParameter>();

            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }

            V6Setting.M_TK = TxtTk.Text;
            V6Setting.M_TK_CN = TxtTk.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;
            

            result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            
             
            
            var and = radAnd.Checked;
            
            var cKey = "";
            //foreach (Control c in groupBox1.Controls)
            //{
            //    var line = c as FilterLineBase;
            //    if (line != null)
            //    {
            //        cKey += line.IsSelected ? ((and?"\nand ": "\nor  ") + line.Query) : "";
            //    }
            //}

            //if (cKey.Length > 0)
            //{
            //    if (and)
            //    {
            //        cKey = string.Format("(1=1 {0})", cKey);
            //    }
            //    else
            //    {
            //        cKey = string.Format("(1=2 {0})", cKey);
            //    }
            //}
            //else
            //{
            //    cKey = "1=1";
            //}

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KH"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
              "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
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
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            if (!string.IsNullOrEmpty(key2))
            {
              //  cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }


            result.Add(new SqlParameter("@Advance", cKey));
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
