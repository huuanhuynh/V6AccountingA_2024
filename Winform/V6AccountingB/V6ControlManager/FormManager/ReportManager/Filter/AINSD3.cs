using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSD3 : FilterBase
    {
        public AINSD3()
        {
            InitializeComponent();
            F3 = false;
            F5 = false;
            fstart = 5;
            ffixcolumn = 6;
            
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            if (V6Setting.Language != "V")
            {
                cbbLoaiBaoCao.Items[0] = V6Text.Text("SL");
                cbbLoaiBaoCao.Items[1] = V6Text.Text("Tien");

                cbbKhoHangGuiBan.Items[0] = "1 - " + V6Text.Text("CTTKHGB");
                cbbKhoHangGuiBan.Items[1] = "2 - " + V6Text.Text("GOPCKHGB");

                cbbKieuIn.Items[0] = "0 - Quantity < 0";
                cbbKieuIn.Items[1] = "1 - Quantity >= 0";
                cbbKieuIn.Items[2] = "2 - All";

                cbbLoaiTonKho.Items[0] = "0 - No";
                cbbLoaiTonKho.Items[1] = "1 - Inventory management";
                cbbLoaiTonKho.Items[2] = "2 - All";
            }

            cbbLoaiBaoCao.SelectedIndex = 0;//.Text = "Tồn";
            cbbKhoHangGuiBan.SelectedIndex = 0;//.Text = "1";
            cbbKieuIn.SelectedIndex = 2;//.Text = "2";
            cbbLoaiTonKho.SelectedIndex = 2;//.Text = "2";
            

            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");


            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
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
                //@loaibaocao varchar(2),			--'T' hay 'D' Tồn đầu (số lượng) hay dư đầu (tiền)
                //@date2 varchar(30),				--đến ngày
                //@Advance varchar(max),
                //@Advance2 varchar(max),
                //@khohangguiban varchar(2),		
                //@kieuin varchar(2),				--having sum() 0: tồn<0, 1: tồn>=0, khác: tất cả
                //@loaitonkho varchar(2)	
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng": "Total");
            RptExtraParameters.Add("LoaiBaoCao", cbbLoaiBaoCao.SelectedIndex == 0 ? "T" : "D");
            

            var result = new List<SqlParameter>();

            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            
            
            result.Add(new SqlParameter("@loaibaocao", cbbLoaiBaoCao.SelectedIndex == 0?"T":"D"));
            result.Add(new SqlParameter("@date2", dateNgay_ct2.YYYYMMDD));



            var and = radAnd.Checked;

            var cKey = "1=1";
            var cKey_SD = "1=1";

           
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "MA_VT","NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6"
            }, and);

            if (txtMaDvcs.IsSelected)
            {
                var ss = txtMaDvcs.VvarTextBox.Text.Trim().Split(',');
                var orString = "";
                var orString1 = "";

                foreach (string s in ss)
                {
                    orString += string.Format(" OR a.Ma_dvcs Like '{0}%'", s.Trim());
                    orString1 += string.Format(" OR b.Ma_dvcs Like '{0}%'", s.Trim());
                }
                orString = orString.Substring(4);
                orString1 = orString1.Substring(4);

                cKey = cKey + string.Format(" AND ({0})", orString);
                cKey_SD = cKey_SD + string.Format(" AND ({0})", orString1);
                
                
            }
            if (txtMakho.IsSelected)
            {

                var ss2 = txtMakho.VvarTextBox.Text.Trim().Split(',');
                var orString2 = "";
                var orString21 = "";

                foreach (string s in ss2)
                {
                    orString2 += string.Format(" OR a.Ma_kho Like '{0}%'", s.Trim());
                    orString21 += string.Format(" OR b.Ma_kho Like '{0}%'", s.Trim());
                }
                orString2 = orString2.Substring(4);
                orString21 = orString21.Substring(4);


                cKey = cKey + string.Format(" AND ({0})", orString2);
                cKey_SD = cKey_SD + string.Format(" AND ({0})", orString21);
                
            }
            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and a.ma_vt in (select ma_vt from alvt where {0} )", key1);
                cKey_SD = cKey_SD + string.Format(" and b.ma_vt in (select ma_vt from alvt where {0} )", key1);
            }
            result.Add(new SqlParameter("@Advance", cKey_SD));
            result.Add(new SqlParameter("@Advance2", cKey));
            result.Add(new SqlParameter("@khohangguiban", cbbKhoHangGuiBan.SelectedIndex == 0 ? "1" : "2"));
            result.Add(new SqlParameter("@kieuin", cbbKieuIn.SelectedIndex == 0 ? "0":cbbKieuIn.SelectedIndex == 1 ?"1":"2"));
            result.Add(new SqlParameter("@loaitonkho", cbbLoaiTonKho.SelectedIndex == 0 ? "0" : cbbLoaiTonKho.SelectedIndex == 1 ? "1":"2"));
            return result;

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
            if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count>0)
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
