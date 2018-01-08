using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AFABCTSNVBP : FilterBase
    {
        public AFABCTSNVBP()
        {
            InitializeComponent();

            // Count columns beginning 0
            fstart = 3;
            ffixcolumn = 6;
            txtdau_cuoi.Value = 2;
            txtThang1.Value =V6Setting.M_ngay_ct1.Month;
            txtNam.Value = V6Setting.M_ngay_ct2.Year;
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            txtNam.Value = V6Setting.M_NAM;
            txtThang1.Value = V6Setting.M_KY2;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            
        }

        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //      @nNam int,
            //@nKy int,
            //@cDau_cuoi int,
            //@ma_bpts varchar(50),
            //@cKey nvarchar(MAX)

            //RptExtraParameters = new SortedDictionary<string, object>();
            //RptExtraParameters.Add("TONGCONG", RLan == "V" ? "Tổng cộng" : "Total");
            //RptExtraParameters.Add("LOAIBAOCAO", "D");
            V6Setting.M_NAM = (int)txtNam.Value;

            if (txtdau_cuoi.Value == 1)
            {
                V6Setting.M_KY1 = (int)txtThang1.Value;
            }
            else
            {
                V6Setting.M_KY2 = (int)txtThang1.Value;
            }



            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@nNam", (int)txtNam.Value));
            result.Add(new SqlParameter("@nKy", (int)txtThang1.Value));
            result.Add(new SqlParameter("@ma_bpts", TxtMa_bp.Text.Trim()));
            result.Add(new SqlParameter("@cDau_cuoi", (int)txtdau_cuoi.Value));


            var and = radAnd.Checked;

            var cKey = "1=1";
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "NH_TS1","NH_TS2","NH_TS3","SO_THE_TS","LOAI_TS","MA_DVCS"
            }, and);
           
            if (!string.IsNullOrEmpty(key0))
            {
                cKey =cKey+" AND "+ string.Format(" SO_THE_TS  in (select SO_THE_TS  from ALTS where {0} )", key0);
            }

            result.Add(new SqlParameter("@cKey", cKey));
            
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

        public override DataTable GenTableForReportType()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Value", typeof(string));
            DataRow newRow = tbl.NewRow();
            //newRow["Name"] = "Số lượng";
            //newRow["Value"] = "Ten_vt,So_luong1,So_luong2";
            tbl.Rows.Add(newRow);

            newRow = tbl.NewRow();
            // newRow["Name"] = "Tiền";
            //newRow["Value"] = "Ten_vt,Tien1,Tien2";
            tbl.Rows.Add(newRow);

            return tbl;
        }

        //Dynamic report -> After
        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

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
