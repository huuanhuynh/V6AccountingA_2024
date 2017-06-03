using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6SqlConnect;
using V6Tools;

namespace V6Init
{
    public class CorpLan
    {
        private static SortedDictionary<string, string> dataDictionary = new SortedDictionary<string, string>();
        private static string tableName = "Corplan";

        public static string GetText(string id)
        {
            var fieldUpper = id.ToUpper();
            if (dataDictionary.ContainsKey(fieldUpper))
            {
                return dataDictionary[fieldUpper];
            }
            else
            {
                DataTable t = SqlConnect.Select(tableName, " distinct ID," + V6Setting.Language,
                    "ID=@p", "", V6Setting.Language, new SqlParameter("@p", id)).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                dataDictionary.AddRange(d);
                if (dataDictionary.ContainsKey(fieldUpper))
                {
                    return dataDictionary[fieldUpper];
                }
            }
            return fieldUpper;
        }

        public static SortedDictionary<string, string> GetTextDic(List<string> ids, string lang, string formName)
        {
            var result = new SortedDictionary<string, string>();
            var getIds = new List<string>();

            foreach (string id in ids)
            {
                string COLUMN_NAME = id.ToUpper();

                if (dataDictionary.ContainsKey(COLUMN_NAME))
                {
                    //if (result.ContainsKey(COLUMN_NAME))
                    //{
                    //    Logger.WriteToLog(string.Format("CorpLan.GetTexDic form: {0}, samekey: {1}", formName, COLUMN_NAME));
                    //}
                    result[COLUMN_NAME] = dataDictionary[COLUMN_NAME];
                }
                else
                {
                    getIds.Add(id);
                }
            }

            string where0 = lang == "V" ? "CHANGE_V='1' And " : "";
            string whereIDin = "";
            for (int i = 0; i < getIds.Count; i++)
            {
                whereIDin += ",'" + getIds[i] + "'";
            }

            if (whereIDin.Length > 3)
            {
                whereIDin = " ID in (" + whereIDin.Substring(1) + ")";
                var where = where0 + whereIDin;
                DataTable t = SqlConnect.Select(tableName, " distinct ID," + lang + ",D", where, "", lang).Data;
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
        /// <param name="updateText"></param>
        public static bool Update(string updateId, string updateText)
        {
            if (string.IsNullOrEmpty(updateId)) throw new ArgumentException("updateID");
            string sql = string.Format("Update {0} Set {1}=@newValue Where ID=@updateId", tableName, V6Setting.Language);
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
