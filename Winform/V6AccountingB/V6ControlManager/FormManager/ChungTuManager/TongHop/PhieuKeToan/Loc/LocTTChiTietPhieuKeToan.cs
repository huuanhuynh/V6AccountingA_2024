using System.Windows.Forms;
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
            txtMaSanPham.SetInitFilter("Loai_vt=55");
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            if (txtTaiKhoan.Text.Trim() != "")
            {
                var sqlTk = "";
                if (txtDieuKienTK.Text == "1")
                {
                    sqlTk = string.Format("(TK_I = '{0}' and PS_NO_NT <> 0)", txtTaiKhoan.Text);
                }
                else if (txtDieuKienTK.Text == "2")
                {
                    sqlTk = string.Format("(TK_I = '{0}' and PS_CO_NT <> 0)", txtTaiKhoan.Text);
                }
                else
                {
                    sqlTk = string.Format("TK_I = '{0}'", txtTaiKhoan.Text);
                }

                result += (result.Length > 0 ? and_or : "") + sqlTk;
            }
            
            //advance
            var rAdvance = panelFilter2.GetQueryString(tableStruct, tableLable, and);
            if (rAdvance.Length > 0)
            {
                result += (result.Length > 0 ? and_or : "") + rAdvance;
            }
            
            return result;
        }

        public void CreateDynamicFilter2(V6TableStruct adStruct, string advAD)
        {
            panelFilter2.AddMultiFilterLine(adStruct, advAD);
        }
    }
}
