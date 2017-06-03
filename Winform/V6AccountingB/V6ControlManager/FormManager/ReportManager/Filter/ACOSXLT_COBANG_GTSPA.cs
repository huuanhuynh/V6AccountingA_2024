﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOSXLT_COBANG_GTSPA : FilterBase
    {
        public ACOSXLT_COBANG_GTSPA()
        {
            fstart = 13;
            ffixcolumn = 6;
            InitializeComponent();
            
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY2;
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
                   //@nam numeric(4, 0),
                   //@thang numeric(2, 0),
                   //@ma_bpht char(16),
                   //@ma_sp char(16),
                   //@condition as nvarchar(max)
            V6Setting.M_NAM = (int)txtNam.Value;
            V6Setting.M_KY1 = (int)txtThang1.Value;
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TONGCONG", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LOAIBAOCAO", "D");

            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nam", (int)txtNam.Value));
            result.Add(new SqlParameter("@thang", (int)txtThang1.Value));
            result.Add(new SqlParameter("@ma_bpht", txtMa_bpht.Text));
            result.Add(new SqlParameter("@ma_sp", txtMa_sp.Text));
            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey = " AND " + string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey = " AND " + string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "";
            }

            result.Add(new SqlParameter("@condition", cKey));

            return result;
        }

        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox) sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }
        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

        private DataSet _ds;
        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            string showFields = "";
            string formatStrings = "";
            string headerString = "";
            if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                var data = _ds.Tables[1];
                if (data.Columns.Contains("GRDS_V1")) showFields = data.Rows[0]["GRDS_V1"].ToString();
                if (data.Columns.Contains("GRDF_V1")) formatStrings = data.Rows[0]["GRDF_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (data.Columns.Contains(f)) headerString = data.Rows[0][f].ToString();
            }
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
        }

    }
}
