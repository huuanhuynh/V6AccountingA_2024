using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using V6SqlConnect;

namespace DataAccessLayer.Implementations
{
    public class MenuServices : IMenuServices
    {
        public DataTable GetGrandMenu(int userId, string moduleId)
        {
            //Tự lấy thông tin user
            var UserInfo = SqlConnect.SelectV6UserById(userId);
            var codeUser = V6SqlconnectHelper.DeCrypt(UserInfo["Code_User"].ToString().Trim());
            var UserName = UserInfo["User_name"].ToString().Trim();

            var IsAdmin = UserName == codeUser.Substring(0, UserName.Length)
                && codeUser.Substring(codeUser.Length - 1) == "1";
            var Mrights = UserInfo["rights"].ToString().Trim();

            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE Itemid='A0000000'";
            sql += " AND Module_id=@moduleID AND HIDE_YN<>1";
            sql += " AND V2ID IN (SELECT V2ID FROM V6MENU WHERE Itemid NOT IN('A0000000','B0000000')";
            sql += " AND JOBID<>'B099' AND (1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)";
            sql += " AND hide_yn<>1 AND Module_id=@moduleID GROUP BY V2ID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID", moduleId));
            prList.Add(new SqlParameter("@isAdmin", IsAdmin));
            prList.Add(new SqlParameter("@mrights", Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }

        public DataTable GetParentMenu(int userId, string moduleId, string v2Id)
        {
            var UserInfo = SqlConnect.SelectV6UserById(userId);
            var codeUser = V6SqlconnectHelper.DeCrypt(UserInfo["Code_User"].ToString().Trim());
            var UserName = UserInfo["User_name"].ToString().Trim();
            var IsAdmin = UserName == codeUser.Substring(0, UserName.Length)
                && codeUser.Substring(codeUser.Length - 1) == "1";
            var Mrights = UserInfo["rights"].ToString().Trim();

            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE Itemid='B0000000'";
            sql += "\n AND Module_id=@moduleID AND HIDE_YN<>1";
            sql += "\n AND V2ID =@v2ID AND JOBID<>'B099'";
            sql += "\n AND V2ID+JOBID IN ";
            sql += "\n    (SELECT V2ID+JOBid FROM V6MENU WHERE (((1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)) AND hide_yn<>1 AND Module_id=@moduleID )";
            sql += "\n    GROUP BY V2ID,JOBID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID", moduleId));
            prList.Add(new SqlParameter("@v2ID", v2Id));
            prList.Add(new SqlParameter("@isAdmin", IsAdmin));
            prList.Add(new SqlParameter("@mrights", Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }

        public DataTable GetChildMenu(int userId, string moduleId, string v2Id, string jobId)
        {
            var UserInfo = SqlConnect.SelectV6UserById(userId);
            var codeUser = V6SqlconnectHelper.DeCrypt(UserInfo["Code_User"].ToString().Trim());
            var UserName = UserInfo["User_name"].ToString().Trim();
            var IsAdmin = UserName == codeUser.Substring(0, UserName.Length)
                && codeUser.Substring(codeUser.Length - 1) == "1";
            var Mrights = UserInfo["rights"].ToString().Trim();

            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE V2ID=@v2ID AND JOBID=@jobID";
            sql += "\n AND ((nhat_ky='') or (nhat_ky<>'' and program='0000'))";
            sql += "\n AND Itemid NOT IN('A0000000','B0000000')";
            sql += "\n AND V2ID+JOBID IN ";
            sql += "\n    (SELECT V2ID+JOBid FROM V6MENU WHERE (((1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)) AND hide_yn<>1 AND Module_id=@moduleID )";
            sql += "\n    GROUP BY V2ID,JOBID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID", moduleId));
            prList.Add(new SqlParameter("@v2ID", v2Id));
            prList.Add(new SqlParameter("@jobID", jobId));
            prList.Add(new SqlParameter("@isAdmin", IsAdmin));
            prList.Add(new SqlParameter("@mrights", Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }
    }
}
