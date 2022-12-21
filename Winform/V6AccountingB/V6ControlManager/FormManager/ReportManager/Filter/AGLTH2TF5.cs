using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTH2F5 : FilterBase
    {
        public AGLTH2F5()
        {
            InitializeComponent();
            //TxtTk.SetInitFilter("tk_cn=1");
            F3 = true;
            F5 = false;
            SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
            LoadLanguage();
        }

        string loai = "", field_name = "";
        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            lineMA.VvarTextBox.Text = row["MA"].ToString().Trim();
            loai = row["RGROUPBY"].ToString().Trim();
            switch (loai)
            {
                case "1":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "TK đối ứng" : "Ref account";
                    field_name = "TK_DU";
                    break;
                case "2":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Tiểu khoản" : "Account details";
                    field_name = "TK";
                    break;
                case "3":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Khách hàng" : "Customer";
                    field_name = "MA_KH";
                    break;
                case "4":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Vụ việc" : "Job";
                    field_name = "MA_VV";
                    break;
                case "5":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Bộ phận" : "Department";
                    field_name = "MA_BP";
                    break;
                case "6":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Mã nh.viên" : "Employee";
                    field_name = "MA_NVIEN";
                    break;
                case "7":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "BP Hạch toán" : "Cost center";
                    field_name = "MA_BPHT";
                    break;
                case "8":
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "Mã ĐVCS" : "Agency";
                    field_name = "MA_DVCS";
                    break;
                default:
                    lineMA.Caption = V6Login.SelectedLanguage == "V" ? "TK đối ứng" : "Ref account";
                    field_name = "TK_DU";
                    break;
            }
            lineMA.FieldName = field_name;
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);

            var keyf5 = GetFilterStringByFields(new List<string>()
            {
                field_name,
                
            }, true);

            result.Add(new SqlParameter("@condition2", keyf5));
            return result;
        }

        
    }
}
