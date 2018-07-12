using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSD2SLDK : FilterBase
    {
        public AINSD2SLDK()
        {
            InitializeComponent();

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

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
            GridViewHideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            GridViewHideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            GridViewHideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            GridViewHideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
               
            //@Date	varchar(12),
            //@Filter		nvarchar(max),
            //@Condition	nvarchar(max)


            if (TxtMakho.VvarTextBox.Text != "")
            {
                V6Setting.M_Ma_kho = TxtMakho.VvarTextBox.Text;
            }
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            

            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@Date", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            
           
            
            var and = radAnd.Checked;
            
            string cKey;
            var cKey_Filter = "1=1";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_KHO","MA_VT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3", "NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
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
                cKey_Filter = cKey_Filter+string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }
            
            result.Add(new SqlParameter("@Filter", cKey_Filter));
            result.Add(new SqlParameter("@Condition", cKey));

            return result;
        }

        private void Txtnh_vt4_Load(object sender, System.EventArgs e)
        {

        }
    }
}
