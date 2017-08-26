﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINVITRI01_filter : FilterBase
    {
        public AINVITRI01_filter()
        {
            InitializeComponent();

            lineMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                lineMaDvcs.Enabled = false;
            }
            
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
            //_hideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            //_hideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            //_hideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            //_hideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            var condition = "";
            if (flag == "DAY")
            {
                condition = string.Format("MA_KHO='{0}' and MA_VITRI like '__{1}%'", lineMakho.StringValue.Replace("'", "''"),
                    lineMaVitri.StringValue.Substring(2).Replace("'", "''"));
                
            }
            else
            {
                condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", lineMakho.StringValue.Replace("'", "''"),
                    lineMaVitri.StringValue.Replace("'", "''"));
            }

            result = new List<SqlParameter>
            {
                new SqlParameter("@EndDate", dateCuoiNgay.Value.ToString("yyyyMMdd")),
                new SqlParameter("@Condition", condition),
                new SqlParameter("@Vttonkho", "1"),
                new SqlParameter("@Kieu_in", "1"),
            };

            return result;
        }

        private string flag = "VITRI";
        public override void SetData(SortedDictionary<string, object> data)
        {
            try
            {
                if (data.ContainsKey("FLAG")) flag = data["FLAG"].ToString().Trim();

                if (data.ContainsKey("MA_KHO")) lineMakho.VvarTextBox.Text = data["MA_KHO"].ToString().Trim();
                if (data.ContainsKey("MA_DVCS")) lineMaDvcs.VvarTextBox.Text = data["MA_DVCS"].ToString().Trim();
                if (data.ContainsKey("MA_VITRI")) lineMaVitri.VvarTextBox.Text = data["MA_VITRI"].ToString().Trim();
             
                if (data.ContainsKey("CUOI_NGAY")) dateCuoiNgay.Value = ObjectAndString.ObjectToFullDateTime(data["CUOI_NGAY"]);
                
                if (data.ContainsKey("VT_TONKHO")) txtVtTonKho.Text = data["VT_TONKHO"].ToString().Trim();
                if (data.ContainsKey("KIEU_IN")) txtKieuIn.Text = data["KIEU_IN"].ToString().Trim();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
    }
}
