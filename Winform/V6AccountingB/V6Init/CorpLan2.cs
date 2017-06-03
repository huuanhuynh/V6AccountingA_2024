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
        private static SortedDictionary<string, string> fieldHeaderDictionary = new SortedDictionary<string, string>();

        public static string GetFieldHeader(string fieldName)
        {
            var fieldUpper = fieldName.ToUpper();
            if (fieldHeaderDictionary.ContainsKey(fieldUpper))
            {
                return fieldHeaderDictionary[fieldUpper];
            }
            else
            {
                DataTable t = SqlConnect.Select("Corplan2", " distinct ID," + V6Setting.Language,
                    "ID='"+fieldName+"'", "", V6Setting.Language).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                fieldHeaderDictionary.AddRange(d);
                if (fieldHeaderDictionary.ContainsKey(fieldUpper))
                {
                    return fieldHeaderDictionary[fieldUpper];
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
            var result = new SortedDictionary<string, string>();
            var haventList = new List<string>();//Danh sách field chưa có.

            foreach (string column in columns)
            {
                string COLUMN_NAME = column.ToUpper();
                
                if (fieldHeaderDictionary.ContainsKey(COLUMN_NAME))
                {
                    result.Add(COLUMN_NAME, fieldHeaderDictionary[COLUMN_NAME]);
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

                fieldHeaderDictionary.AddRange(d);
                result.AddRange(d);
            }
            return result;
        }
    }
}
