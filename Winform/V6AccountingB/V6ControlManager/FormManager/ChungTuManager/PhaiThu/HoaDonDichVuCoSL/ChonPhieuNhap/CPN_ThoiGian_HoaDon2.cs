﻿using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL.ChonPhieuNhap
{
    public partial class CPN_ThoiGian_HoaDonDichVuCoSL : UserControl
    {
        public CPN_ThoiGian_HoaDonDichVuCoSL()
        {
            InitializeComponent();

            v6ColorDateTimePick1.SetValue(V6Setting.M_ngay_ct1);
            v6ColorDateTimePick2.SetValue(V6Setting.M_ngay_ct2);
        }
        public string GetFilterSql_ThoiGian(V6TableStruct tableStruct, string tableLable,
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
