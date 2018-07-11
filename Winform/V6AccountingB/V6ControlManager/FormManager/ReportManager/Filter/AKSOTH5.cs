using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AKSOTH5: FilterBase
    {
        public AKSOTH5()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            F7 = true;

            fstart = 14;
            ffixcolumn = 6;

            String2 = "TEN_VT";
            String1 = "MA_VT";

            cbbLoaiBaoCao.SelectedIndex = 0;
 
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            TxtKieu_bc.Text = "1";
            chkGiamTru.Checked = true;

            Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
            lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
            lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
            lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");

        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

                //@ngay_ct1 char(8),
                //@ngay_ct2 char(8),
                //@giam_tru tinyint,
                //@Maubc int = 0,
                //@mLan char(1)='V',
                //@Loaibc char(1)='G',    
                //@advance nvarchar(max)

            // S: So luong, G: gia tri , C: Ca hai
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", cbbLoaiBaoCao.SelectedIndex == 0 ? "S":cbbLoaiBaoCao.SelectedIndex == 1 ? "G" : "C");


            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));
            result.Add(new SqlParameter("@Maubc", RTien == "VN" ? 0 : 1));
            result.Add(new SqlParameter("@mLan", RLan));
            result.Add(new SqlParameter("@Loaibc", cbbLoaiBaoCao.SelectedIndex == 0 ? "S" : cbbLoaiBaoCao.SelectedIndex == 1 ? "G" : "C"));
            

            var and = radAnd.Checked;

            var cKey = "";


            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_KHO","MA_DVCS","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                 "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
               "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
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
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }


            result.Add(new SqlParameter("@advance", cKey));
            result.Add(new SqlParameter("@Group", lblGroupStringVT.Text));
            
            // Lay theo loại báo cáo - VND- NT 
            
         
          
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

        
        private void NH_KH1_Leave(object sender, EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
            {
                if (current.Text.Trim() == "") current.Text = "0";
            }
        }

        private void NH_KH_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
            {
                if (current.Text.Trim() == "") return;
                foreach (Control control in groupBoxNhom.Controls)
                {
                    if (control != current && control.Text == current.Text)
                    {
                        control.Text = "0";
                    }
                }
            }

            
            var paramDic = new Dictionary<string, string>
            {

                {"NH_KH1", NH_KH1.Text},
                {"NH_KH2", NH_KH2.Text},
                {"NH_KH3", NH_KH3.Text},
                {"NH_KH4", NH_KH4.Text},
                {"NH_KH5", NH_KH5.Text},
                {"NH_KH6", NH_KH6.Text},

                {"MA_KH", NH_MA_KH.Text},
                
            };
            var lblGroupStringVTText = GenGroup(paramDic);

            //var lblGroupStringKHText =
            //    V6BusinessHelper.GenGroup("NH_KH", NH_KH1.Text, NH_KH2.Text, NH_KH3.Text, NH_KH4.Text, NH_KH5.Text, NH_KH6.Text);

            lblGroupStringVT.Text = lblGroupStringVTText;

            String1 = lblGroupStringVT.Text;
        }

        private string GenGroup(Dictionary<string, string> names_groups)
        {
            var result = "";
            var dic = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> item in names_groups)
            {
                var groupName = item.Key;
                var groupSort = ("00" + item.Value).Right(2);
                if (groupSort != "00") dic[groupSort] = groupName;
            }
            //for (int i = 0; i < groups.Length; i++)
            //{
            //    var groupName = names[i];
            //    var groupSort = groups[i];
            //    if (groupSort != "0") dic[groupSort] = groupName;
            //}
            foreach (KeyValuePair<string, string> item in dic)
            {
                result += "," + item.Value;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

    }
}
