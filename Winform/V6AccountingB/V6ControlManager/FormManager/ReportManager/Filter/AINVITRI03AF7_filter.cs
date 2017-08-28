using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Init;
using V6ReportControls;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINVITRI03AF7_filter : FilterBase
    {
        public AINVITRI03AF7_filter()
        {
            InitializeComponent();

            Ready();
        }

        public void SetHideFields(string lang)
        {
            _hideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                //_hideFields.Add("TAG", "TAG");
            }
            else
            {
                
            }
            //_hideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            //_hideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            //_hideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            //_hideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            var advance = "";
            var and_or = radAnd.Checked ? "and " : "or  ";
            foreach (Control control in groupBox1.Controls)
            {
                var lineControl = control as FilterLineBase;
                if (lineControl != null && lineControl.IsSelected)
                {
                    advance += and_or + lineControl.Query;
                }
            }
            if (advance.Length > 4) advance = advance.Substring(4);

            result = new List<SqlParameter>
            {
                new SqlParameter("@nam", dateYear.Value.Year),
                new SqlParameter("@thang", dateMonth.Value.Month),
                new SqlParameter("@Ma_kh", txtMaKh.Text),
                new SqlParameter("@Advance", advance),//mavitri like???
            };

            return result;
        }

        private string flag = "VITRI";
        public override void SetData(IDictionary<string, object> data)
        {
            try
            {
                if (data.ContainsKey("FLAG")) flag = data["FLAG"].ToString().Trim();

                if (data.ContainsKey("MA_KHO")) lineMaKho.VvarTextBox.Text = data["MA_KHO"].ToString().Trim();
                if (data.ContainsKey("MA_VITRI")) lineMaVitri.VvarTextBox.Text = data["MA_VITRI"].ToString().Trim();
                if (data.ContainsKey("MA_VT")) lineMaVatTu.VvarTextBox.Text = data["MA_VT"].ToString().Trim();

                var now = DateTime.Now;
                if (data.ContainsKey("NAM")) dateYear.Value = new DateTime(ObjectAndString.ObjectToInt(data["NAM"]), now.Month, now.Day);
                if (data.ContainsKey("THANG")) dateMonth.Value = new DateTime(now.Year, ObjectAndString.ObjectToInt(data["THANG"]), now.Day);

                if (data.ContainsKey("MA_KH")) txtMaKh.Text = data["MA_KH"].ToString().Trim();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
    }
}
