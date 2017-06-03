using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Xuly
{
    public partial class ZACOSXLT_TINHGIA_Filter: FilterBase
    {
        public ZACOSXLT_TINHGIA_Filter()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgay_ct1.Value = V6Setting.M_SV_DATE.AddMonths(-1);
                dateNgay_ct2.Value = V6Setting.M_SV_DATE;
                LoadListBoxData();
                F9 = true;
                F7 = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ma_bpht", lineMaBpHt.StringValue));
            result.Add(new SqlParameter("@Tinhgia_dc", chkTinhGiaDC.Checked ? 1 : 0));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            
            UpdateValues();
            return result;
        }

        public override void UpdateValues()
        {
            var selectedObject = listBox1.SelectedItem as DataRowView;
            if (selectedObject != null)
            {
                String1 = selectedObject["Proc"].ToString().Trim();
            }
            String2 = "";
            foreach (object item in listBox1.Items)
            {
                var cRow = item as DataRowView;
                if (cRow != null)
                {
                    String2 += "," + cRow["Proc"].ToString().Trim();
                }
            }
            if (String2.Length > 0) String2 = String2.Substring(1);
            
        }

        private void LoadListBoxData()
        {
            //acosxlt_proc
            listBox1.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            listBox1.ValueMember = "Proc";
            listBox1.DataSource = V6BusinessHelper.Select("acosxlt_proc", "", "Status='1'", "", "Stt").Data;
            listBox1.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            listBox1.ValueMember = "Proc";
        }


        public override void Call1(object s = null)
        {
            var index = ObjectAndString.ObjectToInt(s);
            if (listBox1.Items.Count > index && listBox1.SelectedIndex != index)
            {
                listBox1.SelectedIndex = index;
            }
            UpdateValues();
        }
    }
}
