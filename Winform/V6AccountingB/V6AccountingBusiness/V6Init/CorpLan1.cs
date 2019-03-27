using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6SqlConnect;
using V6Tools;

namespace V6Init
{
    public class CorpLan1
    {
        private static SortedDictionary<string, string> dataDictionary = new SortedDictionary<string, string>();

        //public string this[string key]
        //{
        //    get
        //    {
        //        return "";
        //    }
        //}

        public static string GetText(string id)
        {
            var fieldUpper = id.ToUpper();
            if (dataDictionary.ContainsKey(fieldUpper))
            {
                return dataDictionary[fieldUpper];
            }
            else
            {
                DataTable t = SqlConnect.Select("Corplan1", " distinct ID," + V6Setting.Language,
                    "ID=@p", "", V6Setting.Language, new SqlParameter("@p",id)).Data;
                if (t.Rows.Count > 0)
                {
                    var d = t.Rows.Cast<DataRow>().ToDictionary(
                        row =>
                            row[0].ToString().Trim().ToUpper(),
                        row =>
                            row[1].ToString().Replace("\\r\\n", "\r\n").Trim().Length > 0
                                ? row[1].ToString().Trim()
                                : row[0].ToString());

                    dataDictionary.AddRange(d);
                }
                if (dataDictionary.ContainsKey(fieldUpper))
                {
                    return dataDictionary[fieldUpper];
                }
            }
            return id;
        }

        public static SortedDictionary<string, string>GetTextDic(List<string> ids, string lang)
        {
            var result = new SortedDictionary<string, string>();
            var haventList = new List<string>();

            foreach (string id in ids)
            {
                string columnName = id.ToUpper();

                if (dataDictionary.ContainsKey(columnName))
                {
                    result.Add(columnName, dataDictionary[columnName]);
                }
                else
                {
                    haventList.Add(id);
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
                DataTable t = SqlConnect.Select("Corplan1", " distinct ID," + lang, where, "", lang).Data;
                var d = t.Rows.Cast<DataRow>().ToDictionary(
                    row =>
                        row[0].ToString().Trim().ToUpper(),
                    row =>
                        row[1].ToString().Trim().Length > 0
                            ? row[1].ToString().Trim()
                            : row[0].ToString().Trim());

                dataDictionary.AddRange(d);
                result.AddRange(d);
            }
            return result;
        }
    }
}
