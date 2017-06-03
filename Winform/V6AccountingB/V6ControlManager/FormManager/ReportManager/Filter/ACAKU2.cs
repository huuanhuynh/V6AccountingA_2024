using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACAKU2 : FilterBase
    {
        public ACAKU2()
        {
            InitializeComponent();
           
            F3 = true;
            F5 = false;
          
        

           
            

            Txtnh_ku1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_ku2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_ku3.VvarTextBox.SetInitFilter("loai_nh=3");
          
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");

                _hideFields.Add("T_TT_NT0", "T_TT_NT0");
                _hideFields.Add("T_TT_NT", "T_TT_NT");
                _hideFields.Add("DA_TT_NT", "DA_TT_NT");
                _hideFields.Add("CON_PT_NT", "CON_PT_NT");
                _hideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                _hideFields.Add("T_THUE_NT", "T_THUE_NT");
            }
            else
            {
                _hideFields.Add("TAG", "TAG");
                _hideFields.Add("STT_REC", "STT_REC");
                _hideFields.Add("STT_REC0", "STT_REC0");
                _hideFields.Add("STT_REC_TT", "STT_REC_TT");
                _hideFields.Add("MA_TT", "MA_TT");
                _hideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
        //            @ngay_ct1 smalldatetime,
        //@ngay_ct2   smalldatetime,
        //	@ngay_ct3 smalldatetime,
        //    @ngay_ct4   smalldatetime,
      
        //    @tk         varchar(50),
        //    @m_lan      varchar(50),
        //    @mau_bc     int,
        //    @User_id INT = 1,
        //    @Condition nvarchar(max)


            var result = new List<SqlParameter>();
         
            result.Add(new SqlParameter("@Ngay_ct1", txtNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct2", txtNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct3", txtNgay_ct3.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ngay_ct4", txtNgay_ct4.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@M_LAN", V6Setting.Language.Trim()));
            result.Add(new SqlParameter("@mau_bc", RTien == "VN" ? 1 : 2));
            result.Add(new SqlParameter("@Tk", Tk_filterLine.StringValue.Trim()));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
               "MA_VV","MA_KU","TK"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KU1","NH_KU2","NH_KU3"
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
                cKey = cKey + string.Format(" and ma_ku in (select ma_ku from alku where {0} )", key1);
            }
      
            
            result.Add(new SqlParameter("@Condition", cKey));

            return result;
        }

   

    }
}
