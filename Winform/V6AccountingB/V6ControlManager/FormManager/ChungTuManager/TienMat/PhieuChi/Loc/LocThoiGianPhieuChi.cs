using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi.Loc
{
    public partial class LocThoiGianPhieuChi : UserControl
    {
        public LocThoiGianPhieuChi()
        {
            InitializeComponent();

            v6ColorDateTimePick1.Value = V6Setting.M_ngay_ct1;
            v6ColorDateTimePick2.Value = V6Setting.M_ngay_ct2;
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = v6ColorDateTimePick1.Value;
            V6Setting.M_ngay_ct2 = v6ColorDateTimePick2.Value;

            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            var dateFilter = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length>0?tableLable+".":"",
                v6ColorDateTimePick1.Value.ToString("yyyyMMdd"),
                v6ColorDateTimePick2.Value.ToString("yyyyMMdd")
                );
            if (result.Length > 0)
            {
                result = dateFilter + " and (" + result + ")";
            }
            else
            {
                result = dateFilter;
            }

            return result;
        }

        public void Focus1()
        {
            v6ColorDateTimePick1.Focus();
        }
    }
}
