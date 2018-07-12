using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSD2DK : FilterBase
    {
        public AINSD2DK()
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
                GridViewHideFields = new SortedDictionary<string, string>();
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
            //@Group

            //Check Group
            var max = 0;
            var truestep = 0;
            foreach (Control txt in groupBoxNhom.Controls)
            {
                if (txt is TextBox)
                {
                    var current = ObjectAndString.ObjectToInt(txt.Text);
                    if (current > max) max = current;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                foreach (Control txt in groupBoxNhom.Controls)
                {
                    if (txt is TextBox)
                    {
                        var current = ObjectAndString.ObjectToInt(txt.Text);
                        if (current - truestep == 1)
                        {
                            truestep = current;
                            break;
                        }
                    }
                }    
            }
            if (max != truestep)
            {
                throw new Exception("Kiểm tra nhóm");
            }

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
                "MA_VT","NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6"
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
            result.Add(new SqlParameter("@Group", lblGroupString.Text));

            return result;
        }
        
        private void nhom1_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
            foreach (Control control in groupBoxNhom.Controls)
            {
                if(control != current && control.Text == current.Text)
                {
                    control.Text = "0";
                }
            }
            lblGroupString.Text = V6BusinessHelper.GenGroup(
                "NH_VT", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
        }

        
    }
}
