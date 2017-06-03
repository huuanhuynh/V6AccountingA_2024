using System.Windows.Forms;
using V6Controls.Forms;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.Loc
{
    public partial class LocTTChiTietPhieuNhapKhau : UserControl
    {
        public LocTTChiTietPhieuNhapKhau()
        {
            InitializeComponent();
            txtMaSanPham.SetInitFilter("Loai_vt=55");
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";

            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            var result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);
            
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
