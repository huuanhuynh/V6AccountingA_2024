using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    public partial class AMAP01GFilter : FilterBase
    {
        public AMAP01GFilter()
        {
            InitializeComponent();

            if (V6Login.MadvcsCount <= 1)
            {
                //lineMaDvcs.Enabled = false;
            }
            //TxtStatus.VvarTextBox.Text = "1";
           // chkkh_yn.Checked = true;
            TxtType.Text = "0";

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");

            txtnh_vitri1.VvarTextBox.SetInitFilter("loai_nh=1");
            txtnh_vitri2.VvarTextBox.SetInitFilter("loai_nh=2");
            txtnh_vitri3.VvarTextBox.SetInitFilter("loai_nh=3");
            txtnh_vitri4.VvarTextBox.SetInitFilter("loai_nh=4");
            txtnh_vitri5.VvarTextBox.SetInitFilter("loai_nh=5");
            txtnh_vitri6.VvarTextBox.SetInitFilter("loai_nh=6");
            Ready();
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                //_hideFields.Add("TAG", "TAG");
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

            //var condition = "";
            //condition = string.Format("MA_VITRI like '{0}%'", lineMaVitri.StringValue.Replace("'", "''"));


            var and = radAnd.Checked;
            var cKey0 = "";
            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_VITRI",
                "NH_VITRI1",
                "NH_VITRI2",
                "NH_VITRI3",
                "NH_VITRI4",
                "NH_VITRI5",
                "NH_VITRI6",
                "MA_RGB"
            }, and);


            var key1 = GetFilterStringByFields(new List<string>()
            {
                "MA_KH",
                "NH_KH1",
                "NH_KH2",
                "NH_KH3",
                "NH_KH4",
                "NH_KH5",
                "NH_KH6",
                "STATUS"
            }, and);

            if (chkkh_yn.Checked)
            {
                key1 = key1 + " and kh_yn=1";
            }
            if (chkcc_yn.Checked)
            {
                key1 = key1 + " and cc_yn=1";
            }
            if (chknv_yn.Checked)
            {
                key1 = key1 + " and kh_nv=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = string.Format(" ma_kh in (select ma_kh from alkh where {0} )", key1);
            }

            if (!string.IsNullOrEmpty(key0))
            {
                cKey0 = string.Format(" MA_HINH in (select ma_vitri from alvitri where {0} )", key0);
            }

            //-- Type: 0- Sum theo ma_rgb, 1 - Nhom theo ma_rgb,nh_vitri1,ma_vitri, 2- Nhom theo day: nh_vitri1,ma_vitri

            var cGroup = "";
            var cType = "";

            cType = TxtType.Text.Trim();
            
            switch (cType)
            {
                case "0":
                    cGroup = "MA_RGB";
                    break;
                case "1":
                    cGroup = "MA_RGB,NH_VITRI1";
                    break;
                case "2":
                    cGroup = "NH_VITRI1";
                    break;
                default:
                    break;
            }


            var result = new List<SqlParameter>
            {
                new SqlParameter("@MA_KHO", lineMakho.StringValue),
                new SqlParameter("@Advance", cKey0),
                new SqlParameter("@Advance1", cKey),
                new SqlParameter("@Type", cType),
                new SqlParameter("@Group", cGroup),

            };

            return result;
        }

        private string flag = "VITRI";
        public override void SetData(IDictionary<string, object> data)
        {
            try
            {
                if (data.ContainsKey("FLAG")) flag = data["FLAG"].ToString().Trim();

                if (data.ContainsKey("MA_KHO"))
                {
                    lineMakho.VvarTextBox.Text = data["MA_KHO"].ToString().Trim();
                }
                //if (data.ContainsKey("MA_HINH"))
                //{
                //    lineMaVitri.VvarTextBox.Text = data["MA_HINH"].ToString().Trim();
                //}
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
    }
}
