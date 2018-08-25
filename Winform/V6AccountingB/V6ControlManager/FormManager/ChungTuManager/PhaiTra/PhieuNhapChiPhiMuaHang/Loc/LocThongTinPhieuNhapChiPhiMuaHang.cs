using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang.Loc
{
    public partial class LocThongTinPhieuNhapChiPhiMuaHang : UserControl
    {
        public LocThongTinPhieuNhapChiPhiMuaHang()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                soTienTu.DecimalPlaces = V6Options.M_ROUND_NT;
                soTienDen.DecimalPlaces = V6Options.M_ROUND_NT;

                txtNhomKH1.SetInitFilter("LOAI_NH = 1");
                txtNhomKH2.SetInitFilter("LOAI_NH = 2");
                txtNhomKH3.SetInitFilter("LOAI_NH = 3");
                txtNhomKH4.SetInitFilter("LOAI_NH = 4");
                txtNhomKH5.SetInitFilter("LOAI_NH = 5");
                txtNhomKH6.SetInitFilter("LOAI_NH = 6");
                txtNhomKH7.SetInitFilter("LOAI_NH = 7");
                txtNhomKH8.SetInitFilter("LOAI_NH = 8");
                txtNhomKH9.SetInitFilter("LOAI_NH = 9");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var and_or = and ? " AND " : " OR ";
            var tu_so = ctTuSo.Text.Trim().Replace("'", "");
            var den_so = ctDenSo.Text.Trim().Replace("'", "");

            var result = "";
            //so chung tu
            if (chkLike.Checked)
            {
                if (tu_so != "")
                {
                    result += (result.Length > 0 ? and_or : "")
                        + tbL
                        + string.Format("so_ct like '%{0}'",
                        tu_so + ((tu_so.Contains("_") || tu_so.Contains("%")) ? "" : "%"));
                }
            }
            else
            {
                if (tu_so != "" && den_so == "")
                {
                    result += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                        result.Length > 0 ? and_or : "",
                        tbL,
                        tu_so);
                }
                else if (tu_so == "" && den_so != "")
                {
                    result += string.Format("{0} LTrim(RTrim({1}so_ct)) = '{2}'",
                       result.Length > 0 ? and_or : "",
                       tbL,
                       den_so);
                }
                else if (tu_so != "" && den_so != "")
                {
                    result += string.Format("{0} (LTrim(RTrim({1}so_ct)) >= '{2}' and LTrim(RTrim({1}so_ct)) <= '{3}')",
                        result.Length > 0 ? and_or : "",
                        tbL,
                        tu_so, den_so)
                    ;
                }
            }

            if (soTienTu.Value != 0)
            {

                if (soTienDen.Value != 0)
                {
                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT >=" + soTienTu.Value.ToString(CultureInfo.InvariantCulture);

                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT <=" + soTienDen.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result += (result.Length > 0 ? and_or : "")
                   + tbL + "T_TT_NT =" + soTienTu.Value.ToString(CultureInfo.InvariantCulture);

                }
            }
            else
            {
                if (soTienDen.Value != 0)
                {
                    result += (result.Length > 0 ? and_or : "")
                              + tbL + "T_TT_NT <=" + soTienDen.Value.ToString(CultureInfo.InvariantCulture);
                }
            }

            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            var result2 = SqlGenerator.GenWhere2_oper(tableStruct, keys, oper, and, tableLable);
            if (result2.Length > 0)
            {
                if (result.Length > 0)
                    result += string.Format("{0}({1})",
                        and_or, result2);
                else result = "(" + result2 + ")";
            }
            
            //advance
            var rAdvance = panelFilter1.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + " " + rAdvance;
            }
            // dien_giai
            if (dienGiai.Text.Trim().Length > 0)
            {
                result += (result.Length > 0 ? and_or : "")
                    + tbL
                    + string.Format("dien_giai like N'%{0}%'",
                    dienGiai.Text.Replace("'", "''"));
            }

            string where_nhkh = GetNhKhFilterSql(tbL, "like", true);
            if (where_nhkh.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "")
                    + tbL
                    + string.Format(" MA_KH in (Select Ma_kh from ALKH where {0})", where_nhkh);
            }

            return result;
        }

        public string GetNhKhFilterSql(string tableLable, string oper = "=", bool and = true)
        {
            var result = "";

            var keys = new SortedDictionary<string, object>();
            if (txtNhomKH1.Text.Trim() != "")
                keys.Add("NH_KH1", txtNhomKH1.Text.Trim());
            if (txtNhomKH2.Text.Trim() != "")
                keys.Add("NH_KH2", txtNhomKH2.Text.Trim());
            if (txtNhomKH3.Text.Trim() != "")
                keys.Add("NH_KH3", txtNhomKH3.Text.Trim());
            if (txtNhomKH4.Text.Trim() != "")
                keys.Add("NH_KH4", txtNhomKH4.Text.Trim());
            if (txtNhomKH5.Text.Trim() != "")
                keys.Add("NH_KH5", txtNhomKH5.Text.Trim());
            if (txtNhomKH6.Text.Trim() != "")
                keys.Add("NH_KH6", txtNhomKH6.Text.Trim());
            if (txtNhomKH7.Text.Trim() != "")
                keys.Add("NH_KH7", txtNhomKH4.Text.Trim());
            if (txtNhomKH8.Text.Trim() != "")
                keys.Add("NH_KH8", txtNhomKH5.Text.Trim());
            if (txtNhomKH9.Text.Trim() != "")
                keys.Add("NH_KH9", txtNhomKH6.Text.Trim());

            if (keys.Count > 0)
            {
                var struAlvt = V6BusinessHelper.GetTableStruct("ALKH");
                result = SqlGenerator.GenWhere2(struAlvt, keys, oper, and, tableLable);
            }
            return result;
        }

        public void CreateDynamicFilter(V6TableStruct amStruct, string advAM)
        {
            panelFilter1.AddMultiFilterLine(amStruct, advAM);
        }

        private void chkLike_CheckedChanged(object sender, System.EventArgs e)
        {
            ctDenSo.Enabled = !chkLike.Checked;
        }

    }
}
