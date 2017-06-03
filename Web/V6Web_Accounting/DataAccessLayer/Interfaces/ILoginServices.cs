

using System;
using System.Data;

namespace DataAccessLayer.Interfaces
{
    public interface ILoginServices
    {
        void StartSql();
        DateTime GetServerDateTime();
        DataTable GetModuleTable();
        DataTable GetLanguageTable();
        DataTable GetAgentTable(string key, string lang);
        DataTable GetUserRow(string userName);
        int CountDvcs();
        bool CheckLogin(string username, string epass, string dvcs);
    }
}
