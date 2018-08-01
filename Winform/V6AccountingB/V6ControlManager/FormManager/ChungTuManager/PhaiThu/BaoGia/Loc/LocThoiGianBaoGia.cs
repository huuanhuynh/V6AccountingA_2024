using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;//!!!!!
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia.Loc
{
    public partial class LocThoiGianBaoGia : UserControl
    {
        public LocThoiGianBaoGia()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                v6ColorDateTimePick1.SetValue(V6Setting.M_ngay_ct1);
                v6ColorDateTimePick2.SetValue(V6Setting.M_ngay_ct2);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        public string GetFilterSql(V6TableStruct tableStruct, string tableLable,
            string oper = "=", bool and = true)
        {
            V6Setting.M_ngay_ct1 = v6ColorDateTimePick1.Date;
            V6Setting.M_ngay_ct2 = v6ColorDateTimePick2.Date;

            var result = "";
            var keys = V6ControlFormHelper.GetFormDataDictionary(groupBox1);
            result = SqlGenerator.GenWhere2(tableStruct, keys, oper, and, tableLable);

            var dateFilter = string.Format("{0}ngay_ct BETWEEN '{1}' AND '{2}'",
                tableLable.Length>0?tableLable+".":"",
                v6ColorDateTimePick1.YYYYMMDD,
                v6ColorDateTimePick2.YYYYMMDD
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
