using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AARSO1 : FilterBase
    {
        public AARSO1()
        {
            InitializeComponent();
            TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;

            TxtTk.Text = (V6Setting.M_TK_CN ?? "131").Trim();
            TxtMa_kh.Text = (V6Setting.M_Ma_kh ?? "").Trim();
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
          
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
     //       @tk varchar(50),
     //@ma_kh VARCHAR(50),
     //@ngay_ct1 varchar(8),
     //@ngay_ct2 varchar(8),
     //@chi_tiet INT,
     //@Advance nvarchar(max),
     //@Lang VARCHAR(50) = 'V',
     //@User_id INT = 1
 

             var result = new List<SqlParameter>();

            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }
            if (TxtMa_kh.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn khách hàng!");
            }

            V6Setting.M_TK = TxtTk.Text;
            V6Setting.M_TK_CN = TxtTk.Text;
            V6Setting.M_Ma_kh = TxtMa_kh.Text;
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            result.Add(new SqlParameter("@Tk", TxtTk.Text.Trim()));
            result.Add(new SqlParameter("@ma_kh", TxtMa_kh.Text.Trim()));
           
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            if (Chk_Chitiet.Checked)
                  result.Add(new SqlParameter("@chi_tiet",1));
            else
                  result.Add(new SqlParameter("@chi_tiet",0));
                                                    
                      
             
            
            var and = radAnd.Checked;
            
            var cKey = "";
          
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
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
                cKey = "";
            }
                     

            result.Add(new SqlParameter("@Advance", cKey));
            var parent = this.Parent.Parent.Parent as ReportR.ReportRViewBase;
            result.Add(new SqlParameter("@Lang",parent.LAN));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            return result;
        }

        
    }
}
