using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOVVTH4 : FilterBase
    {
        public ACOVVTH4()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = false;
            F5 = false;

            fstart = 10;
            ffixcolumn = 6;

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
         

            Txtnh_vv1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vv2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vv3.VvarTextBox.SetInitFilter("loai_nh=3");

           
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string>();
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


                //@ngay_ct1 AS SmallDateTime,
                //@ngay_ct2 AS SmallDateTime,
                //@key_nh_vv nvarchar(max),
                //@advance nvarchar(max),
                //@cach_tinh char(1)

            // S: So luong, G: gia tri , C: Ca hai
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", "");
            

            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));

            var and = radAnd.Checked;

            var cKey = "";
            var cKey1 = "";
            //foreach (Control c in groupBox1.Controls)
            //{
            //    var line = c as FilterLineBase;
            //    if (line != null)
            //    {
            //        cKey += line.IsSelected ? ((and?"\nand ": "\nor  ") + line.Query) : "";
            //    }
            //}

            //if (cKey.Length > 0)
            //{
            //    if (and)
            //    {
            //        cKey = string.Format("(1=1 {0})", cKey);
            //    }
            //    else
            //    {
            //        cKey = string.Format("(1=2 {0})", cKey);
            //    }
            //}
            //else
            //{
            //    cKey = "1=1";
            //}

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_VV"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
              "NH_VV1", "NH_VV2", "NH_VV3"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                //"NH_VT1","NH_VT2","NH_VT3", "MA_QG", "MA_NSX", "TK_VT"
                ""
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
                cKey1 = cKey1 + string.Format(" and ma_vv in (select ma_vv from alvv where {0} )", key1);
            }
            else
            {
                cKey1 = "1=1";
            }
            if (!string.IsNullOrEmpty(key2))
            {
                //  cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }

            result.Add(new SqlParameter("@key_nh_vv", cKey1));
            result.Add(new SqlParameter("@advance", cKey));
            result.Add(new SqlParameter("@cach_tinh", '1'));
            
            return result;
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

        private void btnSuaChiTieu_Click(object sender, EventArgs e)
        {
            string tableName = "COVVTH4";
            string keys = "FIELD_NAME";
            var data = V6BusinessHelper.SelectTable(tableName);

            V6ControlFormHelper.ShowDataEditorForm(this, data, tableName, null, keys, false, false);
        }
    }
}
