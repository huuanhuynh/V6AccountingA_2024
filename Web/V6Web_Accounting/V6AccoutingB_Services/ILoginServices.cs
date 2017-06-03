using System.Collections.Generic;
using System.Data;
using V6Soft.Models.Accounting.DTO;

namespace V6AccoutingB_Services
{
    public interface ILoginServices
    {
        //DataTable TblLanguage { get; set; }
        //DataTable TblModule { get; set; }
        DataTable GetModuleTable();
        DataTable GetLanguageTable();
        DataTable GetAgentTable(string key);
        DataTable GetUserRow(string userName);
        //void LoadLoginSettings();
        Dictionary<string, object> GetDefaultValues();
        void SetDefaultValues();
        bool DoCheckLogin(LoginRequestModel loginModel);
        int CountDvcs();
        void SetDefaultValuesAPI(Dictionary<string, object> values);
    }
}
