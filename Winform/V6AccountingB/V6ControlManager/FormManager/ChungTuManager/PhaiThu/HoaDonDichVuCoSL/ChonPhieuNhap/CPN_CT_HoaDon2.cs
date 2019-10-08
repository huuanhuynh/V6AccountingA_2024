using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls.Forms;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonPhieuNhap
{
    public partial class CPN_CT_HoaDonDichVuCoSL : UserControl
    {
        public CPN_CT_HoaDonDichVuCoSL()
        {
            InitializeComponent();
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tLable = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTinChiTiet);
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

        /// <summary>
        /// GetFilterSql_TTChiTiet + change fields name.
        /// </summary>
        /// <param name="Invoice"></param>
        /// <param name="tableStruct"></param>
        /// <param name="tableLable"></param>
        /// <param name="oper"></param>
        /// <param name="and"></param>
        /// <returns></returns>
        public string GetFilterSql_Advance(V6Invoice74 Invoice, V6TableStruct tableStruct, string tableLable, string oper = "=", bool and = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tLable = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var result = "";
            SortedDictionary<string, object> keys = V6ControlFormHelper.GetFormDataDictionary(grbThongTinChiTiet);
            var new_keys = new SortedDictionary<string, object>(keys);

            IDictionary<string, object> dic = null;
            if (Invoice.EXTRA_INFOR.ContainsKey("CDHMAP"))
            {
                var cdhMap = Invoice.EXTRA_INFOR["CDHMAP"];
                dic = ObjectAndString.StringToDictionary(cdhMap, ',', ':');
                foreach (KeyValuePair<string, object> item in dic)
                {
                    if (new_keys.ContainsKey(item.Key))
                    {
                        string newField = item.Value.ToString().ToUpper();
                        new_keys[newField] = new_keys[item.Key];
                        new_keys.Remove(item.Key);
                    }
                }
            }

            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            if (result.Length > 0)
            {
                result = "(" + result + ")";
            }

            //advance
            var rAdvance = panelFilter2.GetQueryString_Mapping(tableStruct, dic, tableLable, and);
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
