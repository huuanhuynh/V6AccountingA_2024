using System;
using System.Collections.Generic;
using System.Data;
using V6Init;
using V6Soft.Models.Accounting.DTO;
using V6SqlConnect;

namespace V6AccoutingB_Services
{
    public class LoginServices : ILoginServices
    {
        

        public DataTable GetModuleTable()
        {
            return V6Menu.ModuleTable;
        }

        public DataTable GetLanguageTable()
        {
            return SqlConnect.Select("CorpLang").Data;
        }

        public DataTable GetAgentTable(string key)
        {
            var data = SqlConnect.Select("Aldvcs", "Ma_dvcs," + 
                (V6Setting.Language == "V" ? "Ten_dvcs as Name" : "Ten_dvcs2 as Name"),
                key, "", "ma_dvcs").Data;
            return data;
        }

        

        public DataTable GetUserRow(string userName)
        {
            return SqlConnect.SelectV6User(userName);
        }
        
        public Dictionary<string, object> GetDefaultValues()
        {
            var result = new Dictionary<string,object>();
            var date = (DateTime)SqlConnect.ExecuteScalar(CommandType.Text, "Select GETDATE()");
            var v6op = V6Options.V6OptionValues;
            result["M_SV_DATE"] = date;
            result["V6OPTIONVALUES"] = v6op;
            return result;
        } 
        /// <summary>
        /// Initialize default value.
        /// </summary>
        public void SetDefaultValues()
        {
            V6Setting.M_SV_DATE = (DateTime)SqlConnect.ExecuteScalar(CommandType.Text, "Select GETDATE()");
            V6Setting.M_ngay_ct1 = V6Setting.M_SV_DATE;
            V6Setting.M_ngay_ct2 = V6Setting.M_SV_DATE;
        }
        public void SetDefaultValuesAPI(Dictionary<string, object> values)
        {
            if (values.ContainsKey("M_SV_DATE"))
            {
                V6Setting.M_SV_DATE = (DateTime) values["M_SV_DATE"];
                V6Setting.M_ngay_ct1 = V6Setting.M_SV_DATE;
                V6Setting.M_ngay_ct2 = V6Setting.M_SV_DATE;
            }

            //if(values.ContainsKey("V6OPTIONVALUES"))
            //    V6Options.V6OptionValues = (SortedDictionary<string,string>)values["V6OPTIONVALUES"];
        }

        public bool DoCheckLogin(LoginRequestModel loginModel)
        {
            if (V6LoginInfo.CheckLogin(loginModel.UserName,
                V6Tools.UtilityHelper.EnCrypt(loginModel.UserName + loginModel.Password),
                loginModel.Dvcs))
            //if (V6LoginInfo.Login(loginModel.UserName, loginModel.Password, loginModel.Dvcs))
            {
                SetDefaultValues();
                V6Setting.Language = loginModel.SelectedLanguage;
                V6Options.V6OptionValues["MODULE_ID"] = loginModel.SelectedModuleId;
                V6LoginInfo.GetUserInfo(loginModel.UserName);
                var countDvcs =
                    Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Count(Ma_dvcs) from Aldvcs"));

                V6LoginInfo.MadvcsTotal = countDvcs;
                var key = V6LoginInfo.IsAdmin
                    ? ""
                    : countDvcs > 0 ? "dbo.VFA_Inlist_MEMO(ma_dvcs, '" + V6LoginInfo.UserInfo["r_dvcs"] + "')=1" : "";
                DataTable data =
                    SqlConnect.Select("Aldvcs", "Ma_dvcs," + (V6Setting.Language == "V" ? "Ten_dvcs as Name" : "Ten_dvcs2 as Name"),
                    key, "", "ma_dvcs").Data;

                V6LoginInfo.MadvcsCount = data.Rows.Count;
                
                return true;
            }
            return false;
        }

        public int CountDvcs()
        {
            var count = (int) SqlConnect.ExecuteScalar
                (CommandType.Text, "Select Count(Ma_dvcs) from Aldvcs");
            return count;
        }

        
    }
}
