﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    public partial class AMAP01Filter : FilterBase
    {
        public AMAP01Filter()
        {
            InitializeComponent();
            MyInit();
            Ready();
        }

        private void MyInit()
        {
            try
            {
                if (V6Login.MadvcsCount <= 1)
                {
                    //lineMaDvcs.Enabled = false;
                }
                TxtStatus.VvarTextBox.Text = "1";
                chkkh_yn.Checked = true;
                Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
                Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
                Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
                Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
                Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
                Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
                lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
                lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
                lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                //GridViewHideFields.Add("TAG", "TAG");
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
            var condition = "";
            //if (flag == "DAY")
            //{
            //    condition = string.Format("MA_KHO='{0}' and MA_VITRI like '__{1}%'", lineMakho.StringValue.Replace("'", "''"),
            //        lineMaVitri.StringValue.Substring(2).Replace("'", "''"));
                
            //}
            //else
            //{
            //    condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", lineMakho.StringValue.Replace("'", "''"),
            //        lineMaVitri.StringValue.Replace("'", "''"));
            //}

            var and = radAnd.Checked;
            var cKey = "";
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "MA_KH","NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9","STATUS"
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

            var result = new List<SqlParameter>
            {
                new SqlParameter("@MA_KHO", lineMakho.StringValue),
                new SqlParameter("@MA_HINH", lineMaVitri.StringValue),
                new SqlParameter("@Advance", condition),
                new SqlParameter("@Advance1", cKey),
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
                if (data.ContainsKey("MA_HINH"))
                {
                    lineMaVitri.VvarTextBox.Text = data["MA_HINH"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
    }
}
