﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLAUTO: FilterBase
    {
        public AGLAUTO()
        {
            InitializeComponent();
            F3 = true;
            F5 = false;
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            TxtLoai_bc.Text = "*";
            maChungTu.VvarTextBox.Text = "GL3";

            var filter = "(MA_CT='GL3' or MA_CT='GL4' or MA_CT='GL5' or MA_CT='GL6' or MA_CT='GL7' or MA_CT='IXF')";
            maChungTu.VvarTextBox.SetInitFilter(filter);
            maChungTu.VvarTextBox.CheckNotEmpty = true;
            maChungTu.VvarTextBox.CheckOnLeave = true;

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
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
            }
            else
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
            }
        }
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            
            if(maChungTu.StringValue == "") throw new Exception(V6Text.NoInput + maChungTu.Caption);

            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.YYYYMMDD));
            
            var and = radAnd.Checked;

            var cKey = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_BPHT","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "TK", "TK_DU", "MA_SP","MA_PHI","MA_KU","MA_CT"
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
                cKey = "1=1";
            }

            if (TxtLoai_bc.Text == "1")
                cKey = cKey + " AND ((ISNULL(PS_NO,0)+ISNULL(PS_NO_NT,0))<>0)";
            else if ( TxtLoai_bc.Text == "2")
            {
                cKey = cKey + " AND ((ISNULL(PS_CO,0)+ISNULL(PS_CO_NT,0))<>0)";
            }

            if (cKey == "")
            {
                cKey = "1=1";
            }

            result.Add(new SqlParameter("@Condition", cKey));
            return result;
        }

        
    }
}
