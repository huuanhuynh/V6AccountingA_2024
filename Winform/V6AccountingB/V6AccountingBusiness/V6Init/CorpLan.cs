using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6AccountingBusiness;
using V6SqlConnect;
using V6Tools;

namespace V6Init
{
    /// <summary>
    /// Ngôn ngữ giao diện
    /// </summary>
    public class CorpLan
    {
        private static SortedDictionary<string, string> dataDictionary = new SortedDictionary<string, string>();
        private static string tableName = "Corplan";

        public static string GetText(string id)
        {
            var ID = id.ToUpper();
            if (dataDictionary.ContainsKey(ID))
            {
                return dataDictionary[ID];
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
                if (dataDictionary.ContainsKey(ID))
                {
                    return dataDictionary[ID];
                }
            }
            return ID;
        }
        public static void SetText(string id, string text)
        {
            dataDictionary[id.ToUpper()] = text;
        }

        public static void RemoveText(string id)
        {
            string ID = id.ToUpper();
            if (dataDictionary.ContainsKey(ID)) dataDictionary.Remove(ID);
        }

        /// <summary>
        /// Lấy text ngôn ngữ theo ID, không có trả về null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTextNull(string id)
        {
            var ID = id.ToUpper();
            if (dataDictionary.ContainsKey(ID))
            {
                return dataDictionary[ID];
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
                if (dataDictionary.ContainsKey(ID))
                {
                    return dataDictionary[ID];
                }
            }
            return null;
        }
        
        public static string GetDefaultText(string id)
        {
            var row = GetRow(id);
            return row != null ? row["D"].ToString() : id;
        }

        public static DataRow GetRow(string id)
        {
            DataTable t = SqlConnect.Select(tableName, " distinct *, " + V6Setting.Language + " as [SelectedLanguage]",
                "ID=@p", "", V6Setting.Language, new SqlParameter("@p", id)).Data;
            if (t != null && t.Rows.Count > 0)
            {
                return t.Rows[0];
            }
            return null;
        }

        private static SortedDictionary<string, string> ExceptID = new SortedDictionary<string, string>()
        {
            {"INVOICEM00020", "INVOICEM00020"},
            {"", ""},
        };
        public static SortedDictionary<string, string> GetTextDic(List<string> ids, string lang, string formName)
        {
            var result = new SortedDictionary<string, string>();
            var getIds = new List<string>();

            foreach (string id in ids)
            {
                string COLUMN_NAME = id.ToUpper();

                if (dataDictionary.ContainsKey(COLUMN_NAME))
                {
                    if (result.ContainsKey(COLUMN_NAME))
                    {
                        if(COLUMN_NAME.StartsWith("REPORTM0") || !ExceptID.ContainsKey(COLUMN_NAME))
                        Logger.WriteToLog(string.Format("CorpLan.GetTexDic form: {0}, samekey: {1}", formName, COLUMN_NAME), "V6LangLog");
                    }
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
        /// <param name="language_field">Trường update, V,E,F,C...</param>
        /// <param name="updateText"></param>
        /// <param name="change_v">Có dịch tiếng Việt, chỉ có tác dụng khi đang sử dụng tiếng Việt.</param>
        public static bool Update(string updateId, string language_field, string updateText, string change_v=null)
        {
            if (string.IsNullOrEmpty(updateId)) throw new ArgumentException("updateID");
            var is_Vietnamese = language_field == "V";
            var change_v_update = is_Vietnamese? ", CHANGE_V='"+change_v+"'":"";
            string sql = string.Format("Update {0} Set {1}=@newValue" + change_v_update + " Where ID=@updateId", tableName, language_field);
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

        /// <summary>
        /// Thêm dữ liệu vào bảng ngôn ngữ.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="language_field">Trường update, V,E,F,C...</param>
        /// <param name="text"></param>
        /// <param name="change_v">Có dịch tiếng Việt, chỉ có tác dụng khi đang sử dụng tiếng Việt.</param>
        public static bool Insert(string ID, string language_field, string text, string change_v = null)
        {
            if (string.IsNullOrEmpty(ID)) throw new ArgumentException("ID");
            //var is_Vietnamese = language_field == "V";
            
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["ID"] = ID;
            data[language_field] = text;
            if (language_field == "V")
            {
                data["CHANGE_V"] = change_v;
                data["E"] = text;
                data["D"] = text;
            }
            else
            {
                data["CHANGE_V"] = "0";
                data["V"] = text;
                data["D"] = text;
            }

            var result = V6BusinessHelper.Insert(tableName, data);
            if (result)
            {
                //Cập nhập từ điển đã tải
                dataDictionary[ID.ToUpper()] = text;
                return true;
            }
            return false;
        }
    }
}
