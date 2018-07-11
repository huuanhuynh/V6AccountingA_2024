using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ACOPHITH2 : FilterBase
    {
        public ACOPHITH2()
        {
            InitializeComponent();
           
            F3 = false;
            F5 = true;
            fstart = 5;
            ffixcolumn = 6;
            TxtGroupby.Text = "1";
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

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
            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Tk1 varchar(max),
            //@TkDu varchar(100),
            //@TkKoGT varchar(100),
            //@Condition nvarchar(max),
            //@NoCo int,
            //@M_LAN CHAR(1) = 'V',
            //@M_FC CHAR(2) = 'VN'
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", "");


            var result = new List<SqlParameter>();

            
          
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;

            if (TxtTk.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn tài khoản!");
            }

            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@noco", TxtGroupby.Text.Trim()));
            result.Add(new SqlParameter("@Tk1", TxtTk.Text.Trim()));
            result.Add(new SqlParameter("@TkDu", txtTk_du.StringValue));
            result.Add(new SqlParameter("@TkKoGT", Tkgt_filterLine.StringValue));
            result.Add(new SqlParameter("@M_LAN", V6Setting.Language.Trim()));

            var parent = this.Parent.Parent.Parent as ReportD.ReportDViewBase;
            result.Add(new SqlParameter("@M_FC", parent.MAU));

            var and = radAnd.Checked;

            string cKey;
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_BPHT","MA_DVCS","MA_BP", "MA_KH", "MA_VV","TK_DU", "MA_SP","MA_PHI","MA_KU","MA_CT","TK"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
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
           
            if (TxtGroupby.Text == "1")
                cKey = cKey + " AND ((ISNULL(PS_NO,0)+ISNULL(PS_NO_NT,0))<>0)";
            else if (TxtGroupby.Text == "2")
            {
                cKey = cKey + " AND ((ISNULL(PS_CO,0)+ISNULL(PS_CO_NT,0))<>0)";
            }
                
            
            result.Add(new SqlParameter("@Condition", cKey));


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
