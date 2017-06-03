using System;
using System.Data;
using DataAccessLayer.Interfaces;
using V6SqlConnect;

namespace DataAccessLayer.Implementations
{
    public class LoginServices : ILoginServices
    {
        public void StartSql()
        {
            SqlConnect.StartSqlConnect("V6Soft", AppDomain.CurrentDomain.BaseDirectory + "bin\\");
        }

        public DateTime GetServerDateTime()
        {
            return SqlConnect.GetServerDateTime();
        }

        public DataTable GetModuleTable()
        {
            DataTable moduleTable = SqlConnect.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1", "module_id", "module_id").Data;

            foreach (DataRow row in moduleTable.Rows)
            {
                if (row["module_id"].ToString().Trim() == "A")
                    row["name"] = "Accounting";
                else if (row["module_id"].ToString().Trim() == "H")
                    row["name"] = "Human Resource Management";
                else if (row["module_id"].ToString().Trim() == "C")
                    row["name"] = "Customer Relation Management";
            }

            return moduleTable;
        }

        public DataTable GetLanguageTable()
        {
            DataTable langTable = SqlConnect.Select("CorpLang").Data;
            return langTable;
        }
        
        public DataTable GetAgentTable(string key, string lang)
        {
            var LANG = lang.ToUpper();
            var data = SqlConnect.Select("Aldvcs", "Ma_dvcs,"
                    + (LANG == "V" ? "Ten_dvcs as Name" : "Ten_dvcs2 as Name"),
                    key, "", "ma_dvcs").Data;
            return data;
        }

        public DataTable GetUserRow(string userName)
        {
            DataRow row = SqlConnect.SelectV6User(userName);
            return row.Table;
        }

        public int CountDvcs()
        {
            return Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Count(Ma_dvcs) from Aldvcs"));
        }

        public bool CheckLogin(string username, string epass, string madvcs)
        {
            
            if (epass == "") return false;

            DataRow row = SqlConnect.SelectV6User(username);

            if (row != null)
            {
                if (row.Table.Columns.Contains("password") &&
                    (row["password"].ToString().Trim() == epass))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
