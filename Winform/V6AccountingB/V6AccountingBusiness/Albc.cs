using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6SqlConnect;

namespace V6AccountingBusiness
{
    public static class Albc
    {
        private static string _tableName = "Albc";

        /// <summary>
        /// report,caption,caption2,title,mau,lan,mau_tu_in
        /// </summary>
        /// <param name="ma_file"></param>
        /// <param name="ma_ct"></param>
        /// <param name="stt_rec"></param>
        /// <returns></returns>
        public static DataTable GetMauInData(string ma_file, string ma_ct = "", string stt_rec = "")
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@isadmin", V6Login.IsAdmin),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@lang", V6Login.SelectedLanguage),
                new SqlParameter("@ma_file", ma_file),
                new SqlParameter("@Advance", ""),
                new SqlParameter("@ma_ct", ma_ct),
                new SqlParameter("@stt_rec", stt_rec),
            };
            return V6BusinessHelper.ExecuteProcedure("VPA_GetAlbc", plist).Tables[0];
        }

        public static DataRow GetRow(string mau, string lan, string report)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@p0", mau),
                new SqlParameter("@p1", lan),
                new SqlParameter("@p2", report),
            };
            var sl = SqlConnect.Select(_tableName, "*", "mau=@p0 and lan=@p1 and report=@p2", "", "",
                param);
            if (sl.Data != null && sl.Data.Rows.Count>0)
            {
                return sl.Data.Rows[0];
            }
            return null;
        }
    }
}
