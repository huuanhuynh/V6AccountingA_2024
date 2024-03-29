﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOTKTH4 : FilterBase
    {
        public ACOTKTH4()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = false;
            fstart = 7;
            ffixcolumn = 6;
            TxtKieu_bc.Text = "3";
            TxtKy_bc.Value = 3;
            TxtGroupby.Text = "1";
            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");


            txtnh_phi1.VvarTextBox.SetInitFilter("loai_nh=1");
            txtnh_phi2.VvarTextBox.SetInitFilter("loai_nh=2");
            txtnh_phi3.VvarTextBox.SetInitFilter("loai_nh=3");
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
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");

                GridViewHideFields.Add("T_TT_NT0", "T_TT_NT0");
                GridViewHideFields.Add("T_TT_NT", "T_TT_NT");
                GridViewHideFields.Add("DA_TT_NT", "DA_TT_NT");
                GridViewHideFields.Add("CON_PT_NT", "CON_PT_NT");
                GridViewHideFields.Add("T_TIEN_NT2", "T_TIEN_NT2");
                GridViewHideFields.Add("T_THUE_NT", "T_THUE_NT");
            }
            else
            {
                GridViewHideFields.Add("TAG", "TAG");
                GridViewHideFields.Add("STT_REC", "STT_REC");
                GridViewHideFields.Add("STT_REC0", "STT_REC0");
                GridViewHideFields.Add("STT_REC_TT", "STT_REC_TT");
                GridViewHideFields.Add("MA_TT", "MA_TT");
                GridViewHideFields.Add("MA_GD", "MA_GD");
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", "");


            //@ngay_ct00 varchar(8),
            //@kieu_bc char(1),
            //@so_ky_bc int,
            //@tk varchar(16),
            //@tk_du varchar(16),
            //@tk_gt varchar(16),
            //@key nvarchar(max),
            //@key_gt nvarchar(max),
            //@no_co char(1),
            //@M_LAN VARCHAR(8) = 'V',
            //@M_FC VARCHAR(8) = 'VN'


            var result = new List<SqlParameter>();

            if (TxtKieu_bc.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn loại!");
            }

            if (TxtKy_bc.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn số kỳ!");
            }

            V6Setting.M_ngay_ct1 = dateNgay_ct1.Date;


            result.Add(new SqlParameter("@ngay_ct00", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Kieu_bc", TxtKieu_bc.Text.Trim()));
            result.Add(new SqlParameter("@so_ky_bc", TxtKy_bc.Value));
            result.Add(new SqlParameter("@Tk", Tk_filterLine.StringValue));
            result.Add(new SqlParameter("@Tk_du", txtTk_du.StringValue));
            result.Add(new SqlParameter("@Tk_gt", Tkgt_filterLine.StringValue));
            result.Add(new SqlParameter("@no_co", TxtGroupby.Text.Trim()));
            result.Add(new SqlParameter("@M_LAN", V6Setting.Language.Trim()));
            var parent = this.Parent.Parent.Parent;
            if (parent is ReportDViewBase)
            {
                result.Add(new SqlParameter("@M_FC", (parent as ReportDViewBase).MAU));
            }
            else if (parent is ReportD_DX)
            {
                result.Add(new SqlParameter("@M_FC", (parent as ReportD_DX).MAU));
            }


            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_BPHT","MA_DVCS","MA_BP", "MA_KH", "MA_VV","TK_DU", "MA_SP","MA_KU","MA_CT","TK","MA_PHI","MA_SONB"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);

            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_PHI1","NH_PHI2","NH_PHI3"
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
                cKey = cKey + string.Format(" and ma_kh in (select ma_kh from alkh where {0} )", key1);
            }
            if (!string.IsNullOrEmpty(key2))
            {
                cKey = cKey + string.Format(" and ma_phi in (select ma_phi from alphi where {0} )", key2);
            }

            result.Add(new SqlParameter("@key_gt", "1=1"));
            result.Add(new SqlParameter("@key", cKey));


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



    }
}
