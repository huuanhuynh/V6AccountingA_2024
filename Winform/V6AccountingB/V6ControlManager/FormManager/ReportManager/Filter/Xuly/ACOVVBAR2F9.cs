using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    public partial class ACOVVBAR2F9 : FilterBase
    {
        public ACOVVBAR2F9()
        {
            InitializeComponent();
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
        }

        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            //ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //RptExtraParameters = new SortedDictionary<string, object>();
            //RptExtraParameters.Add("NGAY_CT1", V6Setting.M_ngay_ct1);
            //RptExtraParameters.Add("NGAY_CT2", V6Setting.M_ngay_ct2);
            //RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            //RptExtraParameters.Add("MA_VT", ma_vt_filterLine.VvarTextBox.Text.Trim());

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            result.Add(new SqlParameter("@Ma_vv", ma_vv_filterLine.StringValue.Trim()));
            

            return result;
        }

        public override void LoadDataFinish(DataSet ds)
        {
            try
            {
                if (ds.Tables.Count <= 0) return;
                DataTable data = ds.Tables[0];
                data.Columns.Add("DocSoSLTD1");
                foreach (DataRow row in data.Rows)
                {
                    row["DocSoSLTD1"] = DocSo.DocSoThanhChu(ObjectAndString.ObjectToDecimal(row["SL_TD1"]));
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDataFinish", ex);
            }
        }
    }
}
