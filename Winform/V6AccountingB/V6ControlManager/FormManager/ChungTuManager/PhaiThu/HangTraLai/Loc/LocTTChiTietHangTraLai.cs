using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls.Forms;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai.Loc
{
    public partial class LocTTChiTietHangTraLai : UserControl
    {
        public LocTTChiTietHangTraLai()
        {
            InitializeComponent();
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tLable = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            if (result.Length > 0)
            {
                result = "(" + result + ")";
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
