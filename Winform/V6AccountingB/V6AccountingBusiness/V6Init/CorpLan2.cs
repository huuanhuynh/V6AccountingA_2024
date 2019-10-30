using System.Collections.Generic;
using System.Data;
using System.Linq;
using V6SqlConnect;
using V6Tools;

namespace V6Init
{
    /// <summary>
    /// Fields caption. Chuyên tiêu đề cho cột dữ liệu
    /// </summary>
    public static class CorpLan2
    {
        private static SortedDictionary<string, string> fieldHeaderDictionaryV = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> fieldHeaderDictionaryE = new SortedDictionary<string, string>();

        public static string GetFieldHeader(string fieldName)
        {
            return GetFieldHeader(fieldName, V6Setting.Language);
        }

        public static string GetFieldHeader(string fieldName, string lang)
        {
            if (string.IsNullOrEmpty(lang)) lang = V6Setting.Language;
            var DIC = lang == "V" ? fieldHeaderDictionaryV : fieldHeaderDictionaryE;
            var fieldUpper = fieldName.ToUpper();
            if (DIC.ContainsKey(fieldUpper))
            {
                return DIC[fieldUpper];
            }
            else
            {
                DataTable t = SqlConnect.Select("Corplan2", " distinct ID," + lang,
                    "ID='"+fieldName+"'", "", lang).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                DIC.AddRange(d);
                if (DIC.ContainsKey(fieldUpper))
                {
                    return DIC[fieldUpper];
                }
            }
            return fieldUpper;
        }

        /// <summary>
        /// Lấy header trong Corplan2
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string>GetFieldsHeader(List<string> columns, string lang="")
        {
            if(string.IsNullOrEmpty(lang)) lang = V6Setting.Language;
            var DIC = lang == "V" ? fieldHeaderDictionaryV : fieldHeaderDictionaryE;
            var result = new SortedDictionary<string, string>();
            var haventList = new List<string>();//Danh sách field chưa có.

            foreach (string column in columns)
            {
                string COLUMN_NAME = column.ToUpper();

                if (DIC.ContainsKey(COLUMN_NAME))
                {
                    result.Add(COLUMN_NAME, DIC[COLUMN_NAME]);
                }
                else
                {
                    haventList.Add(COLUMN_NAME);
                }
            }


            string where = "";
            for (int i = 0; i < haventList.Count; i++)
            {
                where += " OR ID='" + haventList[i] + "'";
            }

            if (where.Length > 3)
            {
                where = where.Substring(3);
                DataTable t = SqlConnect.Select("Corplan2", " distinct ID," + lang, where, "", lang).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                DIC.AddRange(d);
                result.AddRange(d);
            }
            return result;
        }
    }
}
