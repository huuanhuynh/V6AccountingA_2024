using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6SqlConnect;
using V6Tools;

namespace V6Init
{
    /// <summary>
    /// Ngôn ngữ giao diện
    /// </summary>
    public class V6AutoCorrect
    {
        private static SortedDictionary<string, string> dataDictionary = new SortedDictionary<string, string>();
        private static string tableName = "V6Correct";

        public static bool GetText(string ma_crect, out string value_out)
        {
            var MA_CRECT = ma_crect.ToUpper();
            if (dataDictionary.ContainsKey(MA_CRECT))
            {
                value_out = dataDictionary[MA_CRECT];
                return true;
            }
            else
            {
                DataTable t = SqlConnect.Select(tableName, " TOP 20 MA_CRECT," + V6Setting.Language,
                    "MA_CRECT like @p and STATUS='1'", "", V6Setting.Language, new SqlParameter("@p", "%"+ma_crect+"%")).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                dataDictionary.AddRange(d);
                if (dataDictionary.ContainsKey(MA_CRECT))
                {
                    value_out = dataDictionary[MA_CRECT];
                    return true;
                }
            }

            value_out = null;
            return false;
        }
        
        public static string GetDefaultText(string ma_crect)
        {
            var row = GetRow(ma_crect);
            return row != null ? row["D"].ToString() : ma_crect;
        }

        public static DataRow GetRow(string ma_crect)
        {
            DataTable t = SqlConnect.Select(tableName, " distinct *, " + V6Setting.Language + " as [SelectedLanguage]",
                "MA_CRECT=@p", "", V6Setting.Language, new SqlParameter("@p", ma_crect)).Data;
            if (t != null && t.Rows.Count > 0)
            {
                return t.Rows[0];
            }
            return null;
        }

        private static SortedDictionary<string, string> ExceptMA_CRECT = new SortedDictionary<string, string>()
        {
            {"INVOICEM00020", "INVOICEM00020"},
            {"", ""},
        };
        public static SortedDictionary<string, string> GetTextDic(List<string> ma_crects, string lang, string formName)
        {
            var result = new SortedDictionary<string, string>();
            var getMA_CRECTs = new List<string>();

            foreach (string ma_crect in ma_crects)
            {
                string COLUMN_NAME = ma_crect.ToUpper();

                if (dataDictionary.ContainsKey(COLUMN_NAME))
                {
                    if (result.ContainsKey(COLUMN_NAME))
                    {
                        if(COLUMN_NAME.StartsWith("REPORTM0") || !ExceptMA_CRECT.ContainsKey(COLUMN_NAME))
                            Logger.WriteToLog(string.Format("V6AutoCorrect.GetTexDic form: {0}, samekey: {1}", formName, COLUMN_NAME));
                    }
                    result[COLUMN_NAME] = dataDictionary[COLUMN_NAME];
                }
                else
                {
                    getMA_CRECTs.Add(ma_crect);
                }
            }

            string where0 = lang == "V" ? "CHANGE_V='1' And " : "";
            string whereIDin = "";
            for (int i = 0; i < getMA_CRECTs.Count; i++)
            {
                whereIDin += ",'" + getMA_CRECTs[i] + "'";
            }

            if (whereIDin.Length > 3)
            {
                whereIDin = " ma_crect in (" + whereIDin.Substring(1) + ")";
                var where = where0 + whereIDin;
                DataTable t = SqlConnect.Select(tableName, " distinct ma_crect," + lang + ",D", where, "", lang).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        lang == "V" && (row[1]??"").ToString().Trim() == ""
                        ? row[2].ToString().Trim()
                        : (row[1]??"").ToString().Trim()
                    );

                dataDictionary.AddRange(d);
                result.AddRange(d);
            }
            return result;
        }

        /// <summary>
        /// Thay đổi giá trị cho ngôn ngữ hiện tại.
        /// </summary>
        /// <param name="updateId"></param>
        /// <param name="language_field">Trường update, V,E,F,C...</param>
        /// <param name="updateText"></param>
        /// <param name="change_v">Có dịch tiếng Việt, chỉ có tác dụng khi đang sử dụng tiếng Việt.</param>
        public static bool Update(string updateId, string language_field, string updateText, string change_v=null)
        {
            if (string.IsNullOrEmpty(updateId)) throw new ArgumentException("updateID");
            var is_Vietnamese = language_field == "V";
            var change_v_update = is_Vietnamese? ", CHANGE_V='"+change_v+"'":"";
            string sql = string.Format("Update {0} Set {1}=@newValue" + change_v_update + " Where ma_crect=@updateId", tableName, language_field);
            SqlParameter[] plist =
            {
                new SqlParameter("@newValue", updateText), 
                new SqlParameter("@updateId", updateId), 
            };
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql, plist);
            if (result > 0)
            {
                //Cập nhập từ điển đã tải
                dataDictionary[updateId.ToUpper()] = updateText;
                return true;
            }
            return false;
        }
    }
}
