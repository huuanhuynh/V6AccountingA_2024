using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.TongHop.PhieuKeToan.Loc
{
    public partial class LocTTChiTietPhieuKeToan : UserControl
    {
        public LocTTChiTietPhieuKeToan()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                txtMaSanPham.SetInitFilter("Loai_vt=55");

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
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public string GetFilterSql(V6TableStruct tableStruct, string tbL,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tbL);

            if (txtTaiKhoan.Text.Trim() != "")
            {
                var sqlTk = "";
                if (txtDieuKienTK.Text == "1")
                {
                    sqlTk = string.Format("(TK_I like '{0}%' and PS_NO_NT <> 0)", txtTaiKhoan.Text);
                }
                else if (txtDieuKienTK.Text == "2")
                {
                    sqlTk = string.Format("(TK_I like '{0}%' and PS_CO_NT <> 0)", txtTaiKhoan.Text);
                }
                else
                {
                    sqlTk = string.Format("TK_I like '{0}%'", txtTaiKhoan.Text);
                }

                result += (result.Length > 0 ? and_or : "") + sqlTk;
            }
            
            //advance
            var rAdvance = panelFilter2.GetQueryString(tableStruct, tbL, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + rAdvance;
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

        public void CreateDynamicFilter2(V6TableStruct adStruct, string advAD)
        {
            panelFilter2.AddMultiFilterLine(adStruct, advAD);
        }
    }
}
