using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ASOTH5 : FilterBase
    {
        public ASOTH5()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                F3 = false;
                F5 = false;
                F7 = true;

                fstart = 11;
                ffixcolumn = 6;
                cbbLoaiBaoCao.SelectedIndex = 1;
                dateNgay_ct0.SetValue(V6Setting.M_ngay_ct1);
                txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;

                if (V6Login.MadvcsCount <= 1)
                {
                    txtMaDvcs.Enabled = false;
                    txtMaDvcs.VvarTextBox.ReadOnly = true;
                }

                TxtKieu_bc.Text = "3";
                TxtKy_bc.Value = 3;
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
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

              //@Kieu_bc char(1),
              //@Ky_bc int,  
              //@Ngay_ct00 smalldatetime, 
              //@cKey nvarchar(MAX)='',
              //@Maubc int = 0,
              //@mLan char(1)='V' 
              //@Loaibc char(1)='G' 

            // S: So luong, G: gia tri , C: Ca hai
            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("TongCong", RLan == "V" ? "Tổng cộng" : "Total");
            RptExtraParameters.Add("LoaiBaoCao", cbbLoaiBaoCao.SelectedIndex == 0 ? "S":cbbLoaiBaoCao.SelectedIndex == 1 ? "G" : "C");


            var result = new List<SqlParameter>();


            if (TxtKy_bc.Value <= 1)
            {                
                //throw new Exception("Chọn kỳ >1!");
            }

            result.Add(new SqlParameter("@Kieu_bc", TxtKieu_bc.Text.Trim()));
            result.Add(new SqlParameter("@Ky_bc", TxtKy_bc.Value));
            result.Add(new SqlParameter("@Ngay_ct00", dateNgay_ct0.YYYYMMDD));
                       
            var and = radAnd.Checked;
            
            var cKey = "";
            

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS", "MA_KHO","MA_BP", "MA_KH", "MA_VV", "MA_NX", "MA_VT","MA_NVIEN"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
               "NH_KH1","NH_KH2","NH_KH3","NH_KH4","NH_KH5","NH_KH6","NH_KH7","NH_KH8","NH_KH9"
            }, and);
            var key2 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1","NH_VT2","NH_VT3","NH_VT4","NH_VT5","NH_VT6", "MA_QG", "MA_NSX", "TK_VT"
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
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key2);
            }


            result.Add(new SqlParameter("@cKey", cKey));

            // Lay theo loại báo cáo - VND- NT 
            
            result.Add(new SqlParameter("@Maubc",RTien=="VN"?0:1));
            result.Add(new SqlParameter("@mLan",RLan));
            result.Add(new SqlParameter("@Loaibc", cbbLoaiBaoCao.SelectedIndex == 0 ? "S" : cbbLoaiBaoCao.SelectedIndex == 1 ? "G" : "C"));
            result.Add(new SqlParameter("@giam_tru", chkGiamTru.Checked ? 1 : 0));

          
            return result;
        }

        //public override DataTable GetChartData(DataTable tbl, string[] fields)
        //{
        //    //string[] fields = { "MA_VT", "so_luong1", "so_luong2", "tien1", "tien2" };
        //    DataTable newTbl = new DataTable("DataTable1");

        //    for (int i = 0, index = 0; i < fields.Length; i++)
        //    {
        //        var field = fields[i];
        //        var column = tbl.Columns[field];
        //        if (column != null)
        //        {
        //            newTbl.Columns.Add(index == 0 ? "NAME" : "COL" + index, column.DataType);
        //            index++;
        //        }
        //    }
        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        var newRow = newTbl.NewRow();
        //        for (int i = 0, index = 0; i < fields.Length; i++)
        //        {
        //            var field = fields[i];
        //            var column = tbl.Columns[field];
        //            if (column != null)
        //            {
        //                newRow[index == 0 ? "NAME" : "COL" + index] = row[field];
        //                index++;
        //            }
        //        }
        //        newTbl.Rows.Add(newRow);
        //    }
        //    return newTbl;
        //}

        public override DataTable GenTableForReportType()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Value", typeof(string));
            DataRow newRow = tbl.NewRow();
            newRow["Name"] = "Số lượng";
            newRow["Value"] = "Ten_vt,So_luong1,So_luong2";
            tbl.Rows.Add(newRow);

            newRow = tbl.NewRow();
            newRow["Name"] = "Tiền";
            newRow["Value"] = "Ten_vt,Tien1,Tien2";
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
