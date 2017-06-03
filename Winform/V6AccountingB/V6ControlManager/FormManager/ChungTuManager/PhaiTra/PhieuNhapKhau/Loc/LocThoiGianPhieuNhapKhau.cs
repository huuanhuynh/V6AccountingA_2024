using System.Windows.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau.Loc
{
    public partial class LocThoiGianPhieuNhapKhau : UserControl
    {
        public LocThoiGianPhieuNhapKhau()
        {
            InitializeComponent();

            v6ColorDateTimePick1.Value = V6Setting.M_ngay_ct1;
            v6ColorDateTimePick2.Value = V6Setting.M_ngay_ct2;
        }
        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            var result = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length>0?tableLable+".":"",
                v6ColorDateTimePick1.Value.ToString("yyyyMMdd"),
                v6ColorDateTimePick2.Value.ToString("yyyyMMdd")
                );
            
            return result;
        }

        public void Focus1()
        {
            v6ColorDateTimePick1.Focus();
        }
    }
}
